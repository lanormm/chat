using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class DES
    {
        private DESCryptoServiceProvider provider =new DESCryptoServiceProvider();
        byte[] Key, IV;

        private static DES des;

        private DES()
        {
            Key = provider.Key;
            IV = provider.IV;
        }

        public static DES getInstance()
        {
            if(des == null)
            {
                des = new DES();
            }

            return des;
        }

        public byte[] getKey()
        {
            return Key;
        }

        public byte[] getIV()
        {
            return IV;
        }
    }
}
