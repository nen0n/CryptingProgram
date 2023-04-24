using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace CryptingProgram
{
    public class XOR : Crypting
    {
        public override string CryptText(Crypt crypt, string plainText, string key)
        {
            byte[] keyBytes = Encoding.Unicode.GetBytes(key); // Переведення в байти ключа
            byte[] hashBytes = SHA256.Create().ComputeHash(keyBytes); // Хешування байтів
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // Переведення в стрічку
            string gamma = String.Concat(Enumerable.Repeat(hashString, 100)); // Повторення тексту 100 разів

            byte[] byteKey = Encoding.Unicode.GetBytes(gamma);
            byte[] inputBytes = Encoding.Unicode.GetBytes(plainText);
            string output;
            try
            {
                for (int i = 0; i < inputBytes.Length; i++)
                {
                    inputBytes[i] ^= byteKey[i % byteKey.Length]; // XOR оператор
                }
                output = Encoding.Unicode.GetString(inputBytes);
            }
            catch
            {
                output = "";
            }

            return output;
        }
    }
}
