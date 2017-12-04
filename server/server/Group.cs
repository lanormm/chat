using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class Group
    {
        public String ID { get; set; }
        public List<User> Subscribers { get; }
        public String Alias { get; set; }
        public User leader { get; set; }

        public RSACryptoServiceProvider rsa { get; set; }
        public DESCryptoServiceProvider des { get; set; }

        public Group()
        {
            this.Subscribers = new List<User>();
        }
    }
}
