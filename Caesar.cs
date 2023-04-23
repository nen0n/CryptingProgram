using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingProgram
{
    public class Caesar : Crypting
    {
        public override string CryptText(Crypt crypt, string inputText, string key)
        {
            string outputText = "";
            if (crypt == Crypt.Encrypt)
            {
                foreach (char letter in inputText)
                {
                    outputText += (char)(((int)letter + int.Parse(key)) % SizeOfUnicode);
                }
            }
            if (crypt == Crypt.Decrypt)
            {
                foreach (char letter in inputText)
                {
                    outputText += (char)(((int)letter + SizeOfUnicode - int.Parse(key) % SizeOfUnicode) % SizeOfUnicode);
                }
            }
            return outputText;
        }
    }
}
