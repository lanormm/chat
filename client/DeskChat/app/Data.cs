using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DeskChat.app
{
    public class Data
    {
        public Socket socket;
        public const int bufferSize = 1024;
        public byte[] buffer = new byte[bufferSize];
        public StringBuilder str = new StringBuilder();

        public dynamic data;
    }
}
