using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Text.RegularExpressions;

namespace CryptingProgram
{
    public class Knapsack
    {
        private Random random = new Random();
        private int N;
        public BigInteger[] closedKey;
        public BigInteger m;
        public BigInteger t;
        public BigInteger[] openKey;

        public Knapsack(int n)
        {
            N = n;
            generateClosedKey();
            generateM();
            generateMutuallySimple();
            generateOpenKey();
        }

        public static string encrypt(string message, BigInteger[] openKey)
        {
            byte[] messageByteArray = ASCIIEncoding.ASCII.GetBytes(message);
            string messageBinaryString = "";
            foreach (byte b in messageByteArray)
            {
                messageBinaryString += string.Format("{0}", Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            char[] messageBinaryCharArray = messageBinaryString.ToString().ToCharArray();
            string encryptedMessage = "";
            for (int i = 0; i < messageBinaryCharArray.Length; i += openKey.Length)
            {
                BigInteger sum = BigInteger.Zero;
                for (int j = 0; j < openKey.Length; j++)
                {
                    sum += (BigInteger)char.GetNumericValue(messageBinaryCharArray[i + j]) * openKey[j];
                }
                if (i == 0)
                {
                    encryptedMessage += sum;
                }
                else
                {
                    encryptedMessage += "," + sum;
                }
            }
            return encryptedMessage;
        }

        public static string decrypt(string encryptedMessage, BigInteger[] closedKey, BigInteger m, BigInteger t)
        {
            BigInteger tInverse = inverseT(t, m);
            string[] encryptedMessageArray = encryptedMessage.Split(',');
            string decryptedBinaryMessage = "";
            List<char> tempBuilder = new List<char>();
            foreach (string s in encryptedMessageArray)
            {
                BigInteger sum = BigInteger.Parse(s);
                sum = sum * tInverse % m;
                for (int i = closedKey.Length - 1; i >= 0; i--)
                {
                    if (sum >= closedKey[i])
                    {
                        tempBuilder.Add('1');
                        sum = sum - closedKey[i];
                    }
                    else tempBuilder.Add('0');
                }
                tempBuilder.Reverse();
                decryptedBinaryMessage += string.Concat(tempBuilder);
                tempBuilder.Clear();
            }
            string[] decryptedBinaryMessageArray = Regex.Split(decryptedBinaryMessage.ToString(), "(?<=\\G.{8})");
            int last = 0;
            if (decryptedBinaryMessageArray.Last() == "")
            {
                last = 1;
            }
            byte[] decryptedByteMessageArray = new byte[decryptedBinaryMessageArray.Length];
            for (int i = 0; i < decryptedBinaryMessageArray.Length - last; i++)
            {
                decryptedByteMessageArray[i] = (byte)Convert.ToInt32(decryptedBinaryMessageArray[i], 2);
            }
            return Encoding.UTF8.GetString(decryptedByteMessageArray);
        }

        private void generateClosedKey()
        {
            closedKey = new BigInteger[N];
            BigInteger sum = BigInteger.Zero;
            for (int i = 0; i < closedKey.Length; i++)
            {
                closedKey[i] = new BigInteger(random.Next(Int32.MaxValue)) + sum;
                sum += closedKey[i];
            }
        }

        private void generateM()
        {
            BigInteger sum = BigInteger.Zero;
            foreach (BigInteger value in closedKey)
            {
                sum += value;
            }
            m = new BigInteger(random.Next(Int32.MaxValue)) + sum;
        }

        public void generateMutuallySimple()
        {
            BigInteger integer = new BigInteger(random.Next(Int32.MaxValue)) % m;
            while (GCD(integer, m) != 1)
            {
                integer += 1;
            }
            t = integer;
        }

        public void generateOpenKey()
        {
            openKey = new BigInteger[N];
            for (int i = 0; i < openKey.Length; i++)
            {
                openKey[i] = closedKey[i] * t % m;
            }
        }

        private static BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        static BigInteger inverseT(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0)
                v = (v + n) % n;
            return v;
        }
    }
}
