using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptingProgram
{
    class RSA
    {
        public string publicKey;
        public string privateKey;

        public RSA()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            publicKey = getKeyString(rsa.ExportParameters(false));
            privateKey = getKeyString(rsa.ExportParameters(true));
        }


        public static string getKeyString(RSAParameters publicKey)
        {
            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }

        public static string encrypt(string textToEncrypt, string publicKeyString)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
            CspParameters csp = new CspParameters() { KeyContainerName = "RSAContainer" };
            using (var rsa = new RSACryptoServiceProvider(csp))
            {
                try
                {
                    rsa.FromXmlString(publicKeyString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string decrypt(string textToDecrypt, string privateKeyString)
        {
            var bytesToDescrypt = Encoding.UTF8.GetBytes(textToDecrypt);
            CspParameters csp = new CspParameters() { KeyContainerName = "RSAContainer" };
            using (var rsa = new RSACryptoServiceProvider(csp))
            {
                try
                {
                    rsa.FromXmlString(privateKeyString);
                    var resultBytes = Convert.FromBase64String(textToDecrypt);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
