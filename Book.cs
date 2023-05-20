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
        private int lengthOfMatrix = 10;


        public override string CryptText(Crypt crypt, string inputText, string key)
        {
            string outputText = "";
            inputText = inputText.ToLower();
            var matrixOfVerse = MatrixGenerator(key);
            if (crypt == Crypt.Encrypt)
            {
                if (ContainLetter(inputText, matrixOfVerse))
                {
                    Random random = new Random();
                    bool foundRigthPair;
                    List<string> foundedPairs = new List<string> { };
                    foreach (char letter in inputText)
                    {
                        foundedPairs.Clear();
                        foundRigthPair = false;
                        if (letter == ' ')
                        {
                            outputText += ", ";
                        }
                        else
                        {
                            for (int i = 0; i < lengthOfMatrix; i++)
                            {
                                for (int j = 0; j < lengthOfMatrix; j++)
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
                }
            }
            if (crypt == Crypt.Decrypt)
            {
                string[] pairs = inputText.Split(',');
                for (int pair = 0; pair < pairs.Length - 1; pair++)
                {
                    if(pairs[pair] == " ")
                    {
                        outputText += " ";
                    }
                    else
                    {
                        string[] twoCoordinates = pairs[pair].Split('/');
                        int i = int.Parse(twoCoordinates[0]);
                        int j = int.Parse(twoCoordinates[1]);
                        outputText += matrixOfVerse[i - 1, j - 1];
                    }
                }
            }
            return outputText;
        }

        private char[,] MatrixGenerator(string key)
        {
            char[,] matrixOfVerse = new char[lengthOfMatrix, lengthOfMatrix];
            string[] linesOfVerse = key.ToLower().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for(int line = 0; line < lengthOfMatrix; line++)
            {
                linesOfVerse[line] = string.Concat(linesOfVerse[line].Where(letter => Char.IsLetter(letter)));
            }
            for (int i = 0; i < lengthOfMatrix; i++)
            {
                for (int j = 0; j < lengthOfMatrix; j++)
                {
                    matrixOfVerse[i, j] = linesOfVerse[i][j];
                }
            }
            return matrixOfVerse;
        }

        private bool ContainLetter(string inputText, char[,] matrix)
        {
            bool containAllLetters = true;
            HashSet<char> verseLetters = new HashSet<char>();
            HashSet<char> inputLetters = new HashSet<char>();
            List<char> absentLettes = new List<char> { };

            for (int i = 0; i < lengthOfMatrix; i++)
            {
                for (int j = 0; j < lengthOfMatrix; j++)
                {
                    verseLetters.Add(matrix[i, j]);
                }
            }
            for(int i = 0; i < inputText.Length; i++)
            {
                inputLetters.Add(inputText[i]);
            }
            foreach (char letter in inputLetters)
            {
                if(letter != ' ')
                {
                    if(!verseLetters.Contains(letter))
                    {
                        containAllLetters = false;
                        absentLettes.Add(letter);
                    }
                }
            }
            if (containAllLetters == false)
            {
                string errorText = "No such symbols in Verse: ";
                foreach(char symbol in absentLettes)
                {
                    errorText += "'" + symbol + "' ";
                }
                System.Windows.MessageBox.Show(errorText);
            }

            return containAllLetters;
        }
    }
}
