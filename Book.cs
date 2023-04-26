using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingProgram
{
    class Book : Crypting
    {
        public override string CryptText(Crypt crypt, string inputText, string key)
        {
            string inputTextToLower = inputText.ToLower();
            string keyToLower = key.ToLower();
            string keyWithDash = keyToLower.Replace("\r\n", "I");
            keyWithDash = keyWithDash.Replace("\n", "I");
            string keyWithoutSpaceWithDash = String.Concat(keyWithDash.Where(letter => Char.IsLetter(letter)));
            string[] substrings = keyWithoutSpaceWithDash.Split('I');
            char[,] matrix = new char[10, 10];
            string outputText = "";
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    matrix[i, j] = substrings[i][j];
                }
            }

            if (crypt == Crypt.Encrypt)
            {
                Random random = new Random();
                bool found;
                foreach (char letter in inputTextToLower)
                {
                    found = false;
                    int randX = random.Next(0, 9);
                    int randY = random.Next(0, 9);
                    for(int i = 0 + randX; i < 10 + randX; i++)
                    {
                        for(int j = 0 + randY; j < 10 + randY; j++)
                        {
                            if (!found)
                            {
                                if (letter == matrix[i % 10, j % 10])
                                {
                                    outputText += (i % 10 + 1).ToString() + "/" + (j % 10 + 1).ToString() + ", ";
                                    found = true;
                                }
                            }
                        }
                    }
                    if(found == false)
                    {
                        outputText += (randX + 1).ToString() + "/" + (randY + 1).ToString() + ", ";
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
                    outputText += matrix[i - 1, j - 1];
                }
            }
            return outputText;
        }
    }
}
