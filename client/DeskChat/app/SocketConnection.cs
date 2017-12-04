using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DeskChat.MainWindow;
using System.Json;
using DeskChat.models;
using static DeskChat.home.ListChats;
using System.Windows.Threading;
using System.Windows;
using System.Net;
using DeskChat.group;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.IO;

namespace DeskChat.app
{
    public enum DispatcherCodes
    {
        CONNECT, DISCONNECT,
        CREATE_ROOM, QUIT_ROOM, JOIN_ROOM,NEW_ROOM,
        SEND, RECEIVE,
        QUIT,DROP_USER,STATUS_UPDATE,NEW_USER,
        ACK
    }    

    public delegate void SendMessage(String message,Room room);
    public delegate void TriggerUpdateUserStatus(UserStatus status);
    public delegate void NewRoom(GroupRoom group);
    public delegate void DropUser(String roomId,UserChat user);    

    public class SocketConnection
    {        

        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        RSACryptoServiceProvider RSAprovider = new RSACryptoServiceProvider();
        RSAParameters publicKey, privateKey;
        DESCryptoServiceProvider DESprovider = new DESCryptoServiceProvider();
        byte[] Key, IV;

        public event ModelPopulated modelPopulatedChanged;
        public event UpdateChatMessage chatMessageUpated;
        public event UserAlreadyDropped dropped;

        protected Task task;
        protected Action action;
        CancellationToken token;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private static SocketConnection instance;

        int port = 5442;
        IPHostEntry ipHostInfo;
        IPAddress ipAddress;
        IPEndPoint remoteEP;
        Socket client;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        private SocketConnection()
        {  
            token = tokenSource.Token;
            publicKey = RSAprovider.ExportParameters(false);
            privateKey = RSAprovider.ExportParameters(true);
            Key = DESprovider.Key;
            IV = DESprovider.IV;

            byte[] blob = RSAprovider.ExportCspBlob(false);

            try
            {
                //task = Task.Run(action = new Action(run), token);
                task = Task.Run(() =>
                {
                    ipHostInfo = Dns.GetHostEntry("localhost");
                    ipAddress = ipHostInfo.AddressList[1];
                    remoteEP = new IPEndPoint(ipAddress, port);

                    client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);

                    connectDone.Reset();
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();

                    byte[] key = RSAprovider.ExportCspBlob(false);
                    String skey = Convert.ToBase64String(key);
                    dynamic objec = new
                    {
                        alias = User.getInstance().Alias,
                        id = User.getInstance().Id,
                        status = (int)User.getInstance().Status,
                        command = DispatcherCodes.CONNECT,
                        PKEY = skey
                    };
                    
                    /*
                    JsonObject jObj = new JsonObject();
                    jObj.Add("alias", User.getInstance().Alias);
                    jObj.Add("id", User.getInstance().Id);
                    jObj.Add("status", (int)User.getInstance().Status);
                    jObj.Add("type", DispatcherCodes.CONNECT.ToString());*/

                    string sel = serializer.Serialize(objec);
                    send(sel);
                    sendDone.WaitOne();
                    
                    while (!task.IsCanceled && client.Connected)
                    {
                        try
                        {
                            if (client.Available > 0)
                            {
                                receive();
                                //receiveDone.WaitOne();
                            }
                        }
                        catch(ObjectDisposedException ex)
                        {

                        }
                        
                    }
                    
                },token);
            }
            catch (AggregateException e)
            {

            }
        }

        public void receive()
        {
            StringBuilder sb = new StringBuilder();
            byte[] temp = new byte[1024];
            bool isRead = client.Poll(1000, SelectMode.SelectRead);
            while(isRead)
            {
                temp = new byte[1024];                
                client.Receive(temp);
                sb.Append(Encoding.UTF8.GetString(temp));
                isRead = client.Poll(1000, SelectMode.SelectRead);
            }
            string res = sb.ToString().Trim('\0');
            //dynamic data = serializer.DeserializeObject(res);
            actionHandler(res);
        }


        public void send(JsonObject obj)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(obj.ToString());
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        public void send(string obj)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(obj.ToString());
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }
        
        public void actionHandler(String response)
        {
            if (response.GetType() == typeof(string))
            {
                var json = JsonValue.Parse((String)response);
                if (json["command"] == (int)DispatcherCodes.CONNECT)
                {
                    receiveIncomingConfig(json);
                }
                else if (json["command"] == (int)DispatcherCodes.SEND)
                {
                    forwardChat(json);
                }
                else if (json["command"] == (int)DispatcherCodes.NEW_USER)
                {
                    UserChat newUser = new UserChat()
                    {
                        Alias = json["alias"],
                        Id = json["id"],
                        Status = (UserStatus)Convert.ToInt32(json["status"].ToString()),
                        PUBLIC_KEY = Convert.FromBase64String(json["PKEY"]),
                    };
                    foreach (JsonValue item in json["groups"])
                    {
                        String groupId = item.ToString().Replace(@"\", "").Replace("\"", "");
                        GroupRoom room = (GroupRoom)Rooms.getInstance().RoomsList.FirstOrDefault(x => x.Id == groupId);
                        if (room != null)
                        {
                            room.Subscribers.Add(newUser);
                        }
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Users.getInstance().UserCollection.Add(newUser);
                    }, DispatcherPriority.ContextIdle);
                }
                else if (json["command"] == (int)DispatcherCodes.QUIT)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UserChat user = Users.getInstance().UserCollection.FirstOrDefault(x => x.Id == json["user"]);
                        Users.getInstance().UserCollection.Remove(user);
                    }, DispatcherPriority.ContextIdle);
                }
                else if (json["command"] == (int)DispatcherCodes.STATUS_UPDATE)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UserChat _user = Users.getInstance().UserCollection.FirstOrDefault(s => s.Id == json["user"]);
                        if (_user != null)
                        {
                            Users.getInstance().UserCollection.Remove(_user);
                            _user.Status = (UserStatus)Convert.ToInt32(json["status"].ToString().Replace(@"\", "").Replace("\"", ""));
                            Users.getInstance().UserCollection.Add(_user);
                        }
                    }, DispatcherPriority.ContextIdle);
                }
                else if (json["command"] == (int)DispatcherCodes.NEW_ROOM)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        GroupRoom room = new GroupRoom();
                        room.Alias = json["alias"];
                        room.Id = json["id"];
                        room.PUBLIC_KEY = Convert.FromBase64String(json["key"]);
                        UserChat _leader = Users.getInstance().UserCollection.FirstOrDefault(x => x.Id == json["leader"]);
                        room.Leader = _leader;
                        foreach (JsonValue item in json["subscribers"])
                        {
                            String userId = item.ToString().Replace(@"\", "").Replace("\"", "");
                            UserChat user = Users.getInstance().UserCollection.FirstOrDefault(x => x.Id == userId);
                            if (user != null)
                            {
                                room.Subscribers.Add(user);
                            }
                        }
                        Rooms.getInstance().RoomsList.Add(room);
                    }, DispatcherPriority.ContextIdle);
                }
                else if (json["command"] == (int)DispatcherCodes.DROP_USER)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        GroupRoom room = (GroupRoom)Rooms.getInstance().RoomsList.FirstOrDefault(x => x.Id == json["room_id"]);
                        UserChat user = Users.getInstance().UserCollection.FirstOrDefault(x => x.Id == json["user_id"]);
                        if (user == null)
                        {
                            if (dropped != null)
                            {
                                dropped();
                            }
                            else
                            {
                                MessageBox.Show("alguém removeu você de um grupo...");
                            }
                            Rooms.getInstance().RoomsList.Remove(room);
                        }
                        room.Subscribers.Remove(user);
                    }, DispatcherPriority.ContextIdle);
                }
            }
            response = null;
        }
        

        public static SocketConnection getInstance()
        {
            if (instance == null)
            {
                instance = new SocketConnection();
            }
            return instance;
        }

        public void sendMessage(String message, Room room)
        {
            /*
            JsonObject data = new JsonObject();
            data.Add("message", message);
            data.Add("id", room.Id);
            data.Add("isGroup", room is GroupRoom);
            data.Add("senderId", User.getInstance().Id);
            data.Add("type", CHAT_COMMAND);
            */
            
            string encryptedMessage = "";

            byte[] encryptedKey = new byte[1], encryptedIV = new byte[1];
            UserChat owner;
            RSACryptoServiceProvider ownerProvider = new RSACryptoServiceProvider();
            if (room is UserRoom)
            {
                owner = Users.getInstance().UserCollection.First(s => s.Id == room.Id);
                ownerProvider.ImportCspBlob(owner.PUBLIC_KEY);
                encryptedMessage = Encrypt(message, Key, IV);
                encryptedKey = ownerProvider.Encrypt(Key, false);
                encryptedIV = ownerProvider.Encrypt(IV, false);

                dynamic _message = new
                {
                    id = room.Id,
                    isGroup = room is GroupRoom,
                    message = encryptedMessage,
                    key = Convert.ToBase64String(encryptedKey),
                    iv = Convert.ToBase64String(encryptedIV),
                    senderId = User.getInstance().Id,
                    command = DispatcherCodes.SEND
                };

                send(serializer.Serialize(_message));
                sendDone.WaitOne();
            }
            else
            {
                encryptedMessage = Encrypt(message, (room as GroupRoom).Key, (room as GroupRoom).IV);
                /*
                ownerProvider.ImportCspBlob((room as GroupRoom).PUBLIC_KEY);
                Encrypt(message, (room as GroupRoom).Key, (room as GroupRoom).IV);
                encryptedKey = ownerProvider.Encrypt((room as GroupRoom).Key, false);
                encryptedIV = ownerProvider.Encrypt((room as GroupRoom).IV, false);
                */
                dynamic _message = new
                {
                    id = room.Id,
                    isGroup = room is GroupRoom,
                    message = encryptedMessage,
                    senderId = User.getInstance().Id,
                    command = DispatcherCodes.SEND
                };

                send(serializer.Serialize(_message));
                sendDone.WaitOne();
            }  
        }

        public void createGroup(GroupRoom group)
        {
            JsonObject data = new JsonObject();
            data.Add("leader", group.Leader.Id);
            data.Add("isGroup", true);
            data.Add("command",(int)DispatcherCodes.CREATE_ROOM);
            data.Add("id", group.Id);
            data.Add("alias", group.Alias);
            send(data);
            sendDone.WaitOne();
        }

        public void dropUser(String roomId,UserChat user)
        {
            JsonObject data = new JsonObject();
            data.Add("user_id", user.Id);
            data.Add("room_id", roomId);
            data.Add("command", (int)DispatcherCodes.DROP_USER);
            send(data);
            sendDone.WaitOne();
        }

        protected void receiveIncomingConfig(JsonValue data)
        {
            var users = data["users"];
            var rooms = data["rooms"];
            List<UserRoom> userRooms = new List<UserRoom>();
            foreach (JsonObject item in users)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    UserChat u;
                    Users.getInstance().UserCollection.Add(u = new UserChat()
                    {
                        Alias = item["alias"],
                        Id = item["id"],
                        PUBLIC_KEY = Convert.FromBase64String(item["PKEY"]),
                        Status = (UserStatus)Convert.ToInt32(item["status"].ToString().Replace(@"\", "").Replace("\"", ""))
                    });
                }, DispatcherPriority.ContextIdle);
            }
            foreach (JsonObject room in rooms)
            {
                RSACryptoServiceProvider decryptor = new RSACryptoServiceProvider();
                decryptor.ImportCspBlob(RSAprovider.ExportCspBlob(true));
                Application.Current.Dispatcher.Invoke(()=>
                {

                    GroupRoom gr = new GroupRoom()
                    {
                        Alias = room["alias"],
                        Id = room["id"],
                        PUBLIC_KEY = Convert.FromBase64String(room["PKEY"]),
                        Key = decryptor.Decrypt(Convert.FromBase64String(room["key"]),false),
                        IV = decryptor.Decrypt(Convert.FromBase64String(room["iv"]), false)
                    };
                    Rooms.getInstance().RoomsList.Add(gr);
                    foreach(JsonPrimitive subscriberId in room["subscribers"])
                    {
                        UserChat _subscriber = Users.getInstance().UserCollection.FirstOrDefault(s => s.Id == subscriberId);
                        if(_subscriber != null)
                        {
                            gr.Subscribers.Add(_subscriber);
                        }
                    }
                }, DispatcherPriority.ContextIdle);
            }
            Application.Current.Dispatcher.Invoke(modelPopulatedChanged, DispatcherPriority.ContextIdle);
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);                
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /*String response = null;
        public void ReceiveCallback(IAsyncResult ar)
        {
            Data data = (Data)ar.AsyncState;
            int bytesRead = client.EndReceive(ar);
            
            if (bytesRead > 0)
            {
                data.str.Append(Encoding.ASCII.GetString(data.buffer, 0, bytesRead));
                //  Get the rest of the data.  
                client.BeginReceive(data.buffer, 0, Data.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), data);
            }
            else
            {
                response = data.str.ToString();
                receiveDone.Set();
            }
        }*/

        public void updateUserStatus(UserStatus status)
        {
            JsonObject obj = new JsonObject();
            obj.Add("command", (int)DispatcherCodes.STATUS_UPDATE);
            obj.Add("status", (int)status);
            obj.Add("id", User.getInstance().Id);

            send(obj);
            sendDone.WaitOne();
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        bool isNewRoom = false;
        private void forwardChat(JsonValue json)
        {
            isNewRoom = false;
            Room room = Rooms.getInstance().RoomsList.FirstOrDefault(s => s.Id == json["id"]);
            UserChat user = null;
            String message = ""; 
            if(room is GroupRoom)
            {
                user = Users.getInstance().UserCollection.FirstOrDefault(s => s.Id == json["sender"]);
                if (user == null)
                { 
                    message = String.Format("{0} disse: {1}", "Você", Decrypt(json["message"], 
                        (room as GroupRoom).Key, (room as GroupRoom).IV));
                }
                else
                {
                    message = String.Format("{0} disse: {1}", user.Alias, Decrypt(json["message"],
                        (room as GroupRoom).Key, (room as GroupRoom).IV));

                }
            }
            else
            {
                if (room == null)
                {
                    room = Rooms.getInstance().RoomsList.FirstOrDefault(s => s.Id == json["sender"]);
                }                
                if (room == null && Users.getInstance().UserCollection.FirstOrDefault(s => s.Id == json["id"]) != null)
                {
                    //room = Rooms.getInstance().RoomsList.FirstOrDefault(s => s.Id == json["id"]);
                    room = new UserRoom()
                    {
                        Alias = User.getInstance().Alias,
                        Id = User.getInstance().Id,
                        Chat = new Chat(),
                        SessionKey = Convert.FromBase64String(json["key"]),
                        SessionIV = Convert.FromBase64String(json["iv"]),
                    };                    
                    isNewRoom = true;
                }
                if (!isNewRoom && room != null && room.Id != json["id"])
                {
                    string userMessage = Decrypt(json["message"], Key, IV);
                    message = String.Format("{0} disse: {1}", "Você", userMessage);
                }
                else
                {
                    user = Users.getInstance().UserCollection.FirstOrDefault(s => s.Id == json["id"]);
                    RSACryptoServiceProvider decryptor = new RSACryptoServiceProvider();
                    decryptor.ImportCspBlob(RSAprovider.ExportCspBlob(true));
                    
                    byte[] encryptedKey = Convert.FromBase64String(json["key"]);
                    byte[] encryptedIV = Convert.FromBase64String(json["iv"]);

                    byte[] decryptedKey = decryptor.Decrypt(encryptedKey, false);
                    byte[] decryptedIV = decryptor.Decrypt(encryptedIV, false);

                    string decryptedMessage = Decrypt(json["message"], decryptedKey, decryptedIV);
                    
                    message = String.Format("{0} disse: {1}", user.Alias, decryptedMessage);
                }                
            }
            Application.Current.Dispatcher.Invoke(()=> {
                 if (isNewRoom)
                 {
                     Rooms.getInstance().RoomsList.Add(room);
                 }
                 room.Chat.col.Add(message);
              }, DispatcherPriority.ContextIdle);
        }

        public void close()
        {
            if (client.Connected)
            {
                client.Close();
            }
            
        }
        public void cancel()
        {
            tokenSource.Cancel();
        }

        public string Encrypt(string originalString, byte[] KEY, byte[] IV)
        {            
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(KEY, IV), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public string Decrypt(string cryptedString, byte[] KEY, byte[] IV)
        {            
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(KEY, IV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}
