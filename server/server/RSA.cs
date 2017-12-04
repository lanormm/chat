using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class RSA
    {
        private RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
        private RSAParameters publicKey, privateKey;

        private static RSA rsa;



        private RSA()
        {
            publicKey = provider.ExportParameters(false);
            privateKey = provider.ExportParameters(true);
        }

        public static RSA getInstance()
        {
            if(rsa == null)
            {
                rsa = new RSA();
            }
            return rsa;
        }
    }
}
