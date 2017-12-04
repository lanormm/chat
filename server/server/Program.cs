using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    
    public class Program
    {
        public static Socket listener;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            iniciaGrupos();

            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5442);
            
            listener = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(100);

            ServerDispatcher dispatcher = ServerDispatcher.get();

            Console.WriteLine("Escutando conexões...");
            while (true)
            {                
                Socket sock = listener.Accept();
                //sock.SendBufferSize = 1024 * 500;
                sock.Blocking = false;
                User u = new User(sock);
                dispatcher.ServerSockets.Add(u);
                new Task(new SocketTask(u).getTask()).Start();
                
            }
        }

        static void iniciaGrupos()
        {            
            ServerDispatcher.get().Rooms.Add(new Group()
            {
                ID = "group#00",
                Alias = "Política",
                leader = null,
                des = new DESCryptoServiceProvider(),
                rsa = new RSACryptoServiceProvider(),
            });
            ServerDispatcher.get().Rooms.Add(new Group()
            {
                ID = "group#01",
                Alias = "Assuntos Gerais",
                des = new DESCryptoServiceProvider(),
                rsa = new RSACryptoServiceProvider(),
                leader = null
            });
        }
    }
}
