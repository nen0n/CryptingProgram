using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingProgram
{
    class TritemiusSlogan : Crypting
    {
        public override string CryptText(Crypt crypt, string inputText, string key)
        {
            int position = 0;
            string outputText = "";
            if (crypt == Crypt.Encrypt)
            {
                foreach (char letter in inputText)
                {
                    outputText += (char)(((int)letter + (int)key[position % key.Length]) % SizeOfUnicode);
                    position++;
                }
            }
            if (crypt == Crypt.Decrypt)
            {
                foreach (char letter in inputText)
                {
                    outputText += (char)(((int)letter - (int)key[position % key.Length]) % SizeOfUnicode);
                    position++;
                }
            }
            return outputText;
        }
    }
}
