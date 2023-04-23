using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingProgram
{
    public abstract class Crypting
    {
        protected int SizeOfUnicode = 1114112;

        public enum Crypt
        {
            Encrypt,
            Decrypt
        }

        public abstract string CryptText(Crypt crypt, string inputText, string key);
    }
}
