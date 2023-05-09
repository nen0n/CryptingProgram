using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace CryptingProgram
{
    public class XOR : Crypting
    {
        public override string CryptText(Crypt crypt, string inputText, string key)
        {
            string gamma = GammaGenerator(key, inputText);
            string outputText = "";
            for (int i = 0; i < inputText.Length; i++)
            {
                outputText += (char)(gamma[i] ^ inputText[i % inputText.Length]);
            }
            return outputText;
        }

        private string GammaGenerator(string key, string inputText)
        {
            byte[] keyBytes = Encoding.Unicode.GetBytes(key); // Переведення в байти ключа
            byte[] hashBytes = SHA256.Create().ComputeHash(keyBytes); // Хешування байтів
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // Переведення в стрічку
            string gamma = String.Concat(Enumerable.Repeat(hashString, inputText.Length / hashString.Length + 1));
            return gamma;
        }
    }
}
