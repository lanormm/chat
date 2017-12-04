using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class User
    {
        public Socket Socket { get; set; }
        public String ID { get; set; }
        public UserStatus status { get; set; }
        public String Alias { get; set; }
        public byte[] PKEY { get; set; }

        public byte[] SessionKey { get; set; }

        public User(Socket socket)
        {
            Socket = socket;
        }
    }
}
