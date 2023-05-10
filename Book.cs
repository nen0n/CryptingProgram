using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CryptingProgram
{
    class Book : Crypting
    {
        public override string CryptText(Crypt crypt, string inputText, string key)
        {
            string outputText = "";
            inputText = inputText.ToLower();
            var matrixOfVerse = MatrixGenerator(key);
            if (crypt == Crypt.Encrypt)
            {
                Random random = new Random();
                bool foundRigthPair;
                List<string> foundedPairs = new List<string> { };
                foreach (char letter in inputText)
                {
                    foundedPairs.Clear();
                    foundRigthPair = false;
                    for (int i = 0; i < 10; i++)
                    {
                        for(int j = 0; j < 10; j++)
                        {
                            if (letter == matrixOfVerse[i, j])
                            {
                                foundedPairs.Add((i + 1).ToString() + "/" + (j + 1).ToString() + ", ");
                                foundRigthPair = true;
                            }
                        }
                    }
                    if (foundRigthPair == false)
                    {
                        outputText += random.Next(1, 11).ToString() + "/" + random.Next(1, 11).ToString() + ", ";
                    }
                    else
                    {
                        outputText += foundedPairs[random.Next(0, foundedPairs.Count)];
                    }
                }
            }
            if (crypt == Crypt.Decrypt)
            {
                string[] pairs = inputText.Split(',');
                for(int pair = 0; pair < pairs.Length - 1; pair++)
                {
                    string[] twoCoordinates = pairs[pair].Split('/');
                    int i = int.Parse(twoCoordinates[0]);
                    int j = int.Parse(twoCoordinates[1]);
                    outputText += matrixOfVerse[i - 1, j - 1];
                }
            }
            return outputText;
        }

        private char[,] MatrixGenerator(string key)
        {
            char[,] matrixOfVerse = new char[10, 10];
            string[] linesOfVerse = key.ToLower().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for(int line = 0; line < 10; line++)
            {
                linesOfVerse[line] = string.Concat(linesOfVerse[line].Where(letter => Char.IsLetter(letter)));
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    matrixOfVerse[i, j] = linesOfVerse[i][j];
                }
            }
            return matrixOfVerse;
        }
    }
}
