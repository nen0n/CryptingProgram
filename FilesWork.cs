using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CryptingProgram
{
    public class FilesWork
    {
        public static string ReadFile(string filePath)
        {
            string fileContents;

            try
            {
                fileContents = File.ReadAllText(filePath);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("File not found");
            }
            catch (Exception)
            {
                throw new Exception("Problems with file");
            }

            return fileContents; 
        }

        public static void LoadFile(string filePath, string fileContents)
        {
            File.WriteAllText(filePath, fileContents); 
        }
    }
}
