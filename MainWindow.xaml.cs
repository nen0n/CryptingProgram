using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace CryptingProgram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string selectedInputFile;
        public string selectedOutputPath;
        private string pattern_only_two_numbers = @"^(-?\d+,-?\d+)$";
        private string patternoOnlyThreeNumbers = @"^(-?\d+,-?\d+,-?\d+)$";
        private Caesar caesar = new Caesar();
        private TritemiusLinear tritemiusLinear = new TritemiusLinear();
        private TritemiusNonlinear tritemiusNonlinear = new TritemiusNonlinear();
        private TritemiusSlogan tritemiusSlogan = new TritemiusSlogan();
        private XOR xor = new XOR();
        private Book book = new Book();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Info_Button_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow info = new InfoWindow();
            info.Show();
        }

        private bool Error_Encrypt()
        {
            if (Type_of_crypt.SelectedIndex == 0)
            {
                MessageBox.Show("No encryption type selected.");
                return false;
            }
            if (OneLineKey_Encrypt.Text == "")
            {
                MessageBox.Show("No encryption key entered.");
                return false;
            }
            if (Encrypt_Text.Text == "")
            {
                MessageBox.Show("No encryption text entered.");
                return false;
            }
            if(Type_of_crypt.SelectedIndex == 1 && !int.TryParse(OneLineKey_Encrypt.Text, out int result))
            {
                MessageBox.Show("The encryption key was entered incorrectly. You need to enter a number.");
                return false;
            }
            if(Type_of_crypt.SelectedIndex == 2 && !Regex.IsMatch(OneLineKey_Encrypt.Text, pattern_only_two_numbers))
            {
                MessageBox.Show("The encryption key was entered incorrectly. You need to enter numbers separated by commas. (1,2)");
                return false;
            }
            if (Type_of_crypt.SelectedIndex == 3 && !Regex.IsMatch(OneLineKey_Encrypt.Text, patternoOnlyThreeNumbers))
            {
                MessageBox.Show("The encryption key was entered incorrectly. You need to enter numbers separated by commas. (1,2,3)");
                return false;
            }
            if (Type_of_crypt.SelectedIndex == 6)
            {
                try
                {
                    book.CryptText(Crypting.Crypt.Encrypt, Encrypt_Text.Text, OneLineKey_Encrypt.Text);
                }
                catch
                {
                    MessageBox.Show("The verse was written incorrectly");
                    return false;
                }
            }
            return true;
        }

        private bool Error_Decrypt()
        {
            if (Type_of_crypt.SelectedIndex == 0)
            {
                MessageBox.Show("No encryption type selected.");
                return false;
            }
            if (OneLineKey_Decrypt.Text == "")
            {
                MessageBox.Show("No encryption key entered.");
                return false;
            }
            if (Decrypt_Text.Text == "")
            {
                MessageBox.Show("The encryption key was entered incorrectly. You need to enter a number.");
                return false;
            }
            if (Type_of_crypt.SelectedIndex == 1 && !int.TryParse(OneLineKey_Decrypt.Text, out int result))
            {
                MessageBox.Show("The encryption key was entered incorrectly. You need to enter a number.");
                return false;
            }
            if (Type_of_crypt.SelectedIndex == 2 && !Regex.IsMatch(OneLineKey_Decrypt.Text, pattern_only_two_numbers))
            {
                MessageBox.Show("The encryption key was entered incorrectly. You need to enter numbers separated by commas. (1,2)");
                return false;
            }
            if (Type_of_crypt.SelectedIndex == 3 && !Regex.IsMatch(OneLineKey_Decrypt.Text, patternoOnlyThreeNumbers))
            {
                MessageBox.Show("The encryption key was entered incorrectly. You need to enter numbers separated by commas. (1,2,3)");
                return false;
            }
            if(Type_of_crypt.SelectedIndex == 6)
            {
                try
                {
                    book.CryptText(Crypting.Crypt.Decrypt, Decrypt_Text.Text, OneLineKey_Decrypt.Text);
                }
                catch
                {
                    MessageBox.Show("The verse was written incorrectly");
                }
                return false;
            }
            return true;
        }

        private void Encrypt_Button_Click(object sender, RoutedEventArgs e)
        {
            if(Error_Encrypt())
            {
                switch(Type_of_crypt.SelectedIndex)
                {
                    case 1:
                        Decrypt_Text.Text = caesar.CryptText(Crypting.Crypt.Encrypt, Encrypt_Text.Text, OneLineKey_Encrypt.Text);
                        break;
                    case 2:
                        Decrypt_Text.Text = tritemiusLinear.CryptText(Crypting.Crypt.Encrypt, Encrypt_Text.Text, OneLineKey_Encrypt.Text);
                        break;
                    case 3:
                        Decrypt_Text.Text = tritemiusNonlinear.CryptText(Crypting.Crypt.Encrypt, Encrypt_Text.Text, OneLineKey_Encrypt.Text);
                        break;
                    case 4:
                        Decrypt_Text.Text = tritemiusSlogan.CryptText(Crypting.Crypt.Encrypt, Encrypt_Text.Text, OneLineKey_Encrypt.Text);
                        break;
                    case 5:
                        Decrypt_Text.Text = xor.CryptText(Crypting.Crypt.Encrypt, Encrypt_Text.Text, OneLineKey_Encrypt.Text);
                        break;
                    case 6:
                        Decrypt_Text.Text = book.CryptText(Crypting.Crypt.Encrypt, Encrypt_Text.Text, OneLineKey_Encrypt.Text);
                        break;
                }
            }
        }

        private void Decrypt_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Error_Decrypt())
            {
                switch (Type_of_crypt.SelectedIndex)
                {
                    case 1:
                        Encrypt_Text.Text = caesar.CryptText(Crypting.Crypt.Decrypt, Decrypt_Text.Text, OneLineKey_Decrypt.Text);
                        break;
                    case 2:
                        Encrypt_Text.Text = tritemiusLinear.CryptText(Crypting.Crypt.Decrypt, Decrypt_Text.Text, OneLineKey_Decrypt.Text);
                        break;
                    case 3:
                        Encrypt_Text.Text = tritemiusNonlinear.CryptText(Crypting.Crypt.Decrypt, Decrypt_Text.Text, OneLineKey_Decrypt.Text);
                        break;
                    case 4:
                        Encrypt_Text.Text = tritemiusSlogan.CryptText(Crypting.Crypt.Decrypt, Decrypt_Text.Text, OneLineKey_Decrypt.Text);
                        break;
                    case 5:
                        Encrypt_Text.Text = xor.CryptText(Crypting.Crypt.Decrypt, Decrypt_Text.Text, OneLineKey_Decrypt.Text);
                        break;
                    case 6:
                        Encrypt_Text.Text = book.CryptText(Crypting.Crypt.Decrypt, Decrypt_Text.Text, OneLineKey_Decrypt.Text);
                        break;
                }
            }
        }

        private void Random_Key_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            var Random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            switch (Type_of_crypt.SelectedIndex)
            {
                case 1:
                    OneLineKey_Encrypt.Text = Random.Next(-10, 10).ToString();
                    break;
                case 2:
                    OneLineKey_Encrypt.Text = Random.Next(-10, 10).ToString() + "," + Random.Next(-10, 10).ToString();
                    break;
                case 3:
                    OneLineKey_Encrypt.Text = Random.Next(-10, 10).ToString() + "," + Random.Next(-10, 10).ToString() + "," + Random.Next(-10, 10).ToString();
                    break;
                case 4:
                    OneLineKey_Encrypt.Text = "";
                    for (int i = 0; i < Random.Next(6, 10); i++)
                    {
                        OneLineKey_Encrypt.Text += chars[Random.Next(chars.Length)];
                    }
                    break;
                case 5:
                    OneLineKey_Encrypt.Text = "";
                    for (int i = 0; i < Random.Next(6, 10); i++)
                    {
                        OneLineKey_Encrypt.Text += chars[Random.Next(chars.Length)];
                    }
                    break;
                case 6:
                    Random random = new Random();
                    var rand = random.Next(3);
                    if(rand == 0)
                    {
                        OneLineKey_Encrypt.Text = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\VerseFirst.txt");
                    }
                    if(rand == 1)
                    {
                        OneLineKey_Encrypt.Text = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\VerseSecond.txt");
                    }
                    if(rand == 2)
                    {
                        OneLineKey_Encrypt.Text = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\VerseThird.txt");
                    }    
                    break;
            }
        }

        private void Open_Encrypt_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                selectedInputFile = openFileDialog.FileName;
            }

            Encrypt_Text.Text = FilesWork.ReadFile(selectedInputFile);
        }

        private void Open_Decrypt_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                selectedInputFile = openFileDialog.FileName;
            }

            Decrypt_Text.Text = FilesWork.ReadFile(selectedInputFile);
        }

        private void Save_Decrypt_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select a folder";
            openFileDialog.Filter = "Folders|\n";
            openFileDialog.DereferenceLinks = true;
            openFileDialog.FileName = "Select";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedOutputPath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }
            string outputFilePath = selectedOutputPath + @"\output (" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ").txt";
            FilesWork.LoadFile(outputFilePath, Decrypt_Text.Text);
        }

        private void Swap_Button_Click(object sender, RoutedEventArgs e)
        {
            Thickness thickness;
            thickness = Encrypt_Text.Margin;
            Encrypt_Text.Margin = Decrypt_Text.Margin;
            Decrypt_Text.Margin = thickness;

            thickness = Encrypt.Margin;
            Encrypt.Margin = Decrypt.Margin;
            Decrypt.Margin = thickness;

            thickness = Random_Key_Encrypt.Margin;
            Random_Key_Encrypt.Margin = Random_Key_Decrypt.Margin;
            Random_Key_Decrypt.Margin = thickness;

            thickness = OneLineKey_Encrypt.Margin;
            OneLineKey_Encrypt.Margin = OneLineKey_Decrypt.Margin;
            OneLineKey_Decrypt.Margin = thickness;

            thickness = Open_Encrypt_Button.Margin;
            Open_Encrypt_Button.Margin = Open_Decrypt_Button.Margin;
            Open_Decrypt_Button.Margin = thickness;

            thickness = Delete_Button_Encrypt.Margin;
            Delete_Button_Encrypt.Margin = Save_Decrypt_Button.Margin;
            Save_Decrypt_Button.Margin = thickness;

            thickness = Button_Left.Margin;
            Button_Left.Margin = Copy_Button_Decrypt.Margin;
            Copy_Button_Decrypt.Margin = thickness;

            thickness = Button_Right.Margin;
            Button_Right.Margin = Delete_Button_Decrypt.Margin;
            Delete_Button_Decrypt.Margin = thickness;

            thickness = Encrypt_Button.Margin;
            Encrypt_Button.Margin = Decrypt_Button.Margin;
            Decrypt_Button.Margin = thickness;
        }

        private void Copy_Button_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Decrypt_Text.Text);
        }

        private void Copy_Button_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Encrypt_Text.Text);
        }

        private void Delete_Button_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            Encrypt_Text.Text = "";
            OneLineKey_Encrypt.Text = "";
        }

        private void Delete_Button_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            Decrypt_Text.Text = "";
            OneLineKey_Decrypt.Text = "";
        }

        private void Type_of_crypt_DropDownClosed(object sender, EventArgs e)
        {
            if(Type_of_crypt.SelectedIndex == 6)
            {
                OneLineKey_Decrypt.Height = 57;
                OneLineKey_Encrypt.Height = 57;
                OneLineKey_Decrypt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                OneLineKey_Encrypt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            }
            else
            {
                OneLineKey_Decrypt.Height = 23;
                OneLineKey_Encrypt.Height = 23;
                OneLineKey_Decrypt.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                OneLineKey_Encrypt.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }
    }
}
