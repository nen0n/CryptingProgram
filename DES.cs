using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace CryptingProgram
{
    class DES
    {
        public string CryptText(Crypting.Crypt crypt, CipherMode mode,  string inputText, string key, string IV)
        {
            string outputText = "";
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
            cryptic.Key = ASCIIEncoding.ASCII.GetBytes(key);
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            cryptic.Mode = mode;
            if (crypt == Crypting.Crypt.Encrypt)
            {
                FileStream stream = new FileStream(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\DES.txt", FileMode.OpenOrCreate, FileAccess.Write);
                stream.SetLength(0);
                CryptoStream crStream = new CryptoStream(stream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] data = UnicodeEncoding.Unicode.GetBytes(inputText);
                crStream.Write(data, 0, data.Length);
                crStream.Close();
                stream.Close();
                outputText = FilesWork.ReadFile(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\DES.txt");
            }
            if (crypt == Crypting.Crypt.Decrypt)
            {
                FileStream stream = new FileStream(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\DES.txt", FileMode.Open, FileAccess.Read);
                CryptoStream crStream = new CryptoStream(stream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(crStream);
                outputText = reader.ReadToEnd();
                crStream.Close();
                stream.Close();
            }
            return outputText;
        }
    }
}
