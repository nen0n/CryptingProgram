using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingProgram
{
    public class TritemiusLinear : Crypting
    {
        public override string CryptText(Crypt crypt, string inputText, string key)
        {
            int position = 0;
            string outputText = "";
            string[] substrings = key.Split(',');
            int a = int.Parse(substrings[0]);
            int b = int.Parse(substrings[1]);
            if (crypt == Crypt.Encrypt)
            {
                foreach (char letter in inputText)
                {
                    outputText += (char)(((int)letter + (a * position + b)) % SizeOfUnicode);
                    position++;
                }
            }
            if (crypt == Crypt.Decrypt)
            {
                foreach (char letter in inputText)
                {
                    outputText += (char)(((int)letter + SizeOfUnicode - (a * position + b) % SizeOfUnicode ) % SizeOfUnicode);
                    position++;
                }
            }
            return outputText;
        }
    }
}
