using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace server
{

    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024 * 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();

        public dynamic data;
    }

    public class SocketTask
    {

        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        bool connected = false;
        private Action act;
        private Socket client;
        private User user;
        public SocketTask(User user)
        {
            act = new Action(handleClient);
            this.user = user;
            this.client = user.Socket;
        }

        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        public void handleClient()
        {
            connected = client.Poll(1000, SelectMode.SelectRead);

            while (true)
            {
                connected = client.Poll(1000,SelectMode.SelectRead);
                if (connected && client.Available == 0) break;

                // receive  
                if(client.Available > 0)
                {
                    StateObject obj = Receive(client);
                    handleRequest(obj);
                }
            }
            Console.WriteLine("Cliente " + ServerDispatcher.get().ServerSockets.Find(u => u.Socket == client).Alias + " desconectou ");
            client.Shutdown(SocketShutdown.Both);
            client.Disconnect(false);
            ServerDispatcher.get().ServerSockets.Remove(user);
            alertQuit();            
        }

        public void handleRequest(StateObject obj)
        {
            dynamic data = obj.data;
            switch ((int)data["command"])
            {
                case (int)DispatcherCodes.CONNECT:
                    user.ID = data["id"];
                    user.Alias = data["alias"];
                    user.status = UserStatus.DISPONIVEL;
                    user.PKEY = Convert.FromBase64String(data["PKEY"]);

                    ServerDispatcher.get().Rooms.Find(x => x.ID == "group#00").Subscribers.Add(user);
                    ServerDispatcher.get().Rooms.Find(x => x.ID == "group#01").Subscribers.Add(user);
                    
                    send(listUsers());
                    sendDone.WaitOne();
                    alertNewUser();
                    break;                
                case (int)DispatcherCodes.DISCONNECT:                    
                    break;
                case (int)DispatcherCodes.SEND:

                    if ((bool)data["isGroup"])
                    {
                        Group group = ServerDispatcher.get().Rooms.Find(s => s.ID == data["id"]);
                        List<Socket> sockets = new List<Socket>(ServerDispatcher.get().ServerSockets.Select(s => s.Socket));
                        
                        foreach (Socket item in sockets)
                        {
                            dynamic message = new
                            {
                                message = data["message"],                                
                                id = group.ID,
                                sender = user.ID,
                                command = DispatcherCodes.SEND,
                            };
                            send(serializer.Serialize(message), item);
                            sendDone.WaitOne();
                        }
                    }
                    else
                    {
                        User sendto = ServerDispatcher.get().ServerSockets.Find(x => x.ID == data["id"]);
                        dynamic message = new
                        {
                            id = user.ID,
                            sender = sendto.ID,
                            command = DispatcherCodes.SEND,
                            message = data["message"],
                            key = data["key"],
                            iv = data["iv"]
                        };
                        send(serializer.Serialize(message), sendto.Socket);
                        sendDone.WaitOne();
                        send(serializer.Serialize(message), client);
                        sendDone.WaitOne();
                    }                    
                    break;
            }
        }

        public void send(string _data)
        {
            byte[] data = Encoding.UTF8.GetBytes(_data);
            client.BeginSend(data, 0, data.Length, 0, new AsyncCallback(sendCallBack), client);
        }

        public void send(string _data,Socket socket)
        {
            byte[] data = Encoding.UTF8.GetBytes(_data);
            socket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(sendCallBack),socket);
        }

        public void sendCallBack(IAsyncResult ar)
        {
            Socket handler = (Socket)ar.AsyncState;
            int bytesSent = handler.EndSend(ar);
            //handler.Shutdown(SocketShutdown.Send);
            sendDone.Set();
        }

        public void releaseClient()
        {
            client.Disconnect(false);
            ServerDispatcher.get().ServerSockets.Remove(user);
        }

        public StateObject Receive(Socket client)
        {
            StateObject data = new StateObject();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            int bytes = 0;

            while (client.Available > 0)
            {
                bytes = client.Receive(data.buffer);
                int lastIndex = Array.FindLastIndex(data.buffer, b => b != 0);
                Array.Resize(ref data.buffer, lastIndex + 1);
                string str = Encoding.ASCII.GetString(data.buffer);
                data.sb.Append(str);
            }
            data.data = serializer.DeserializeObject(data.sb.ToString());
            return data;
        }

        public Action getTask()
        {
            return act;
        }


        public String listUsers()
        {
            List<dynamic> users_ = new List<dynamic>(),groups_ = new List<dynamic>();
            foreach (User item in ServerDispatcher.get().ServerSockets)
            {
                if (item.ID == user.ID) continue;
                users_.Add(new
                {
                    id = item.ID,
                    alias = item.Alias,
                    status = item.status,
                    PKEY = Convert.ToBase64String(item.PKEY)
                });
            }
            

            foreach (Group group in ServerDispatcher.get().Rooms)
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportCspBlob(user.PKEY);

                byte[] encryptedKey = rsa.Encrypt(group.des.Key, false);
                byte[] encryptedIV = rsa.Encrypt(group.des.IV, false);

                groups_.Add(new {
                    id = group.ID,
                    alias = group.Alias,
                    leader = group.leader,
                    subscribers = group.Subscribers.Select(x => x.ID).ToList(),
                    PKEY = Convert.ToBase64String(group.rsa.ExportCspBlob(false)),
                    key = Convert.ToBase64String(encryptedKey),
                    iv = Convert.ToBase64String(encryptedIV)
                });
            }
            dynamic handshake = new
            {
                users = users_,
                rooms = groups_,
                command = DispatcherCodes.CONNECT
            };
            string str = format(handshake);
            return str;
        }

        public void alertNewUser()
        {
            var payload = new
            {
                alias = user.Alias,
                id = user.ID,
                status = user.status,
                PKEY = Convert.ToBase64String(user.PKEY),
                command = DispatcherCodes.NEW_USER,
                groups = ServerDispatcher.get().Rooms.Where(v => v.Subscribers.Exists(x => x.ID == user.ID)).Select(x => x.ID).ToList()
            };
            List<User> users = ServerDispatcher.get().ServerSockets.Where(s => s.ID != user.ID).ToList();
            foreach (User u in users)
            {
                send(format(payload), u.Socket);
                sendDone.WaitOne();
            }
        }

        public string format(dynamic str)
        {
            return serializer.Serialize(str);
        }

        public void alertQuit()
        {
            var payload = new
            {
                user = user.ID,
                command = DispatcherCodes.QUIT
            };
            List<Socket> sockets = ServerDispatcher.get().ServerSockets.Select(s => s.Socket).ToList();
            Socket.Select(null, sockets, null, 1000);
            foreach (Socket sock in sockets)
            {
                send(format(payload), sock);
                sendDone.WaitOne();
            }
        }

        public void alertStatus(UserStatus status)
        {
            
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
