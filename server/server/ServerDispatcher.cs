using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public enum DispatcherCodes
    {
        CONNECT, DISCONNECT,
        CREATE_ROOM, QUIT_ROOM, JOIN_ROOM, NEW_ROOM,
        SEND, RECEIVE,
        QUIT, DROP_USER, STATUS_UPDATE, NEW_USER,
        ACK
    }
    public enum UserStatus
    {
        DISPONIVEL, OCUPADO, AUSENTE
    }
    public class ServerDispatcher
    {
        private static ServerDispatcher instance;
        private ServerDispatcher() { }

        public List<User> ServerSockets = new List<User>();
        public List<Group> Rooms = new List<Group>();

        public static ServerDispatcher get()
        {
            if(instance == null)
            {
                instance = new ServerDispatcher();
            }
            return instance;
        }

        public void handle(dynamic data,Socket origin)
        {
            User u;
            switch((int)data["command"])
            {
                case (int)DispatcherCodes.CONNECT:
                    u = ServerSockets.Find(s => s.Socket == origin);
                    u.ID = data["id"];
                    u.Alias = data["alias"];
                    send(origin);
                    break;
                case (int)DispatcherCodes.SEND:
                    if ((bool)data["isGroup"])
                    {

                    }
                    break;
            }
        }

        public void send(Socket origin)
        {
            byte[] data = Encoding.ASCII.GetBytes("Olá!!");
            origin.BeginSend(data, 0, data.Length,0, new AsyncCallback(sendCallBack),origin);
        }

        public void sendCallBack(IAsyncResult ar)
        {
            Socket handler = (Socket) ar.AsyncState;
            int bytesSent = handler.EndSend(ar);
        }
    }
}
