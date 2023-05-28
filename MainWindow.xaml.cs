using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CryptingProgram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string selectedInputFile;
        public string selectedOutputPath;
        private readonly string pattern_only_two_numbers = @"^(-?\d+,-?\d+)$";
        private readonly string patternoOnlyThreeNumbers = @"^(-?\d+,-?\d+,-?\d+)$";
        private Caesar caesar = new Caesar();
        private TritemiusLinear tritemiusLinear = new TritemiusLinear();
        private TritemiusNonlinear tritemiusNonlinear = new TritemiusNonlinear();
        private TritemiusSlogan tritemiusSlogan = new TritemiusSlogan();
        private XOR xor = new XOR();
        private Book book = new Book();
        private DES des = new DES();
        private RSA rsa = new RSA();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Info_Button_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow info = new InfoWindow();
            info.Show();
        }

        private BigInteger[] arrayParser(string input)
        {
            BigInteger[] array = new BigInteger[input.Split(',').Length];
            string[] numbers = input.Split(',');
            for(int i = 0; i < numbers.Length; i++)
            {
                array[i] = new BigInteger(Int64.Parse(numbers[i]));
            }
            return array;
        }

        private bool Error_Encrypt()
        {
            if (Type_of_crypt.SelectedIndex == 0)
            {
                MessageBox.Show("No encryption type selected.");
                return false;
            }
            if (OneLineKey_Encrypt.Text == "" && Type_of_crypt.SelectedIndex < 8)
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
                    MessageBox.Show("The verse was written incorrectly.");
                    return false;
                }
            }
            if (Type_of_crypt.SelectedIndex == 7)
            {
                if (Type_of_DES.SelectedIndex == 0)
                {
                    MessageBox.Show("No DES type selected.");
                    return false;
                }
                if (OneLineIV_Encrypt.Text == "")
                {
                    MessageBox.Show("No IV entered.");
                    return false;
                }
                try
                {
                    switch (Type_of_DES.SelectedIndex)
                    {
                        case 1:
                            des.CryptText(Crypting.Crypt.Encrypt, System.Security.Cryptography.CipherMode.CBC, Encrypt_Text.Text, OneLineKey_Encrypt.Text, OneLineIV_Encrypt.Text);
                            break;
                        case 2:
                            des.CryptText(Crypting.Crypt.Encrypt, System.Security.Cryptography.CipherMode.CFB, Encrypt_Text.Text, OneLineKey_Encrypt.Text, OneLineIV_Encrypt.Text);
                            break;
                        case 3:
                            des.CryptText(Crypting.Crypt.Encrypt, System.Security.Cryptography.CipherMode.ECB, Encrypt_Text.Text, OneLineKey_Encrypt.Text, OneLineIV_Encrypt.Text);
                            break;
                    }
                }

                catch
                {
                    MessageBox.Show("Something gone wrong. Try again!\n");
                    return false;
                }
            }
            if(Type_of_crypt.SelectedIndex == 8)
            {
                if(N_Knap.Text =="")
                {
                    MessageBox.Show("No length entered.");
                    return false;
                }
                if(Open_Knap_Encrypt.Text == "")
                {
                    MessageBox.Show("No opened key entered.");
                    return false;
                }
                if(!int.TryParse(N_Knap.Text, out int i))
                {
                    MessageBox.Show("Length isn`t a number");
                }
                try
                {
                    Knapsack knapsack = new Knapsack(int.Parse(N_Knap.Text));
                    Knapsack.encrypt(Encrypt.Text, arrayParser(Open_Knap_Encrypt.Text));
                }
                catch
                {
                    MessageBox.Show("Open key is broken!\n");
                    return false;
                }
            }
            if(Type_of_crypt.SelectedIndex == 9)
            {
                if(PublicKey_RSA_Encrypt.Text == "")
                {
                    MessageBox.Show("No public key entered.");
                    return false;
                }
                try
                {
                    RSA.encrypt(Encrypt_Text.Text, PublicKey_RSA_Encrypt.Text);
                }
                catch
                {
                    MessageBox.Show("Public key is broken!\n");
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
            if (OneLineKey_Decrypt.Text == "" && Type_of_crypt.SelectedIndex < 8)
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
                    return false;
                }
            }
            if(Type_of_crypt.SelectedIndex == 7)
            {
                if (Type_of_DES.SelectedIndex == 0)
                {
                    MessageBox.Show("No DES type selected.");
                    return false;
                }
                if (OneLineIV_Decrypt.Text == "")
                {
                    MessageBox.Show("No IV entered.");
                    return false;
                }
                try
                {
                    switch (Type_of_DES.SelectedIndex)
                    {
                        case 1:
                            des.CryptText(Crypting.Crypt.Decrypt, System.Security.Cryptography.CipherMode.CBC, Decrypt_Text.Text, OneLineKey_Decrypt.Text, OneLineIV_Decrypt.Text);
                            break;
                        case 2:
                            des.CryptText(Crypting.Crypt.Decrypt, System.Security.Cryptography.CipherMode.CFB, Decrypt_Text.Text, OneLineKey_Decrypt.Text, OneLineIV_Decrypt.Text);
                            break;
                        case 3:
                            des.CryptText(Crypting.Crypt.Decrypt, System.Security.Cryptography.CipherMode.ECB, Decrypt_Text.Text, OneLineKey_Decrypt.Text, OneLineIV_Decrypt.Text);
                            break;
                    }
                }
                catch
                {
                    MessageBox.Show("Something gone wrong. Try again!");
                    return false;
                }
            }
            if (Type_of_crypt.SelectedIndex == 8)
            {
                if (T_Knap_Decrypt.Text == "")
                {
                    MessageBox.Show("No T entered.");
                    return false;
                }
                if (M_Knap_Decrypt.Text == "")
                {
                    MessageBox.Show("No M entered.");
                    return false;
                }
                if (Closed_Knap_Decrypt.Text == "")
                {
                    MessageBox.Show("No closed key entered.");
                    return false;
                }
                if (!BigInteger.TryParse(T_Knap_Decrypt.Text, out BigInteger i))
                {
                    MessageBox.Show("T isn`t a number");
                }
                if (!BigInteger.TryParse(M_Knap_Decrypt.Text, out BigInteger j))
                {
                    MessageBox.Show("M isn`t a number");
                }
                try
                {
                    Knapsack.decrypt(Decrypt_Text.Text, arrayParser(Closed_Knap_Decrypt.Text), BigInteger.Parse(M_Knap_Decrypt.Text), BigInteger.Parse(T_Knap_Decrypt.Text));
                }
                catch
                {
                    MessageBox.Show("Closed key is broken!\n");
                    return false;
                }
            }
            if (Type_of_crypt.SelectedIndex == 9)
            {
                if (PrivateKey_RSA_Decrypt.Text == "")
                {
                    MessageBox.Show("No private key entered.");
                    return false;
                }
                try
                {
                    RSA.decrypt(Decrypt_Text.Text, PrivateKey_RSA_Decrypt.Text);
                }
                catch
                {
                    MessageBox.Show("Private key is broken!\n");
                    return false;
                }
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
                    case 7:
                        switch (Type_of_DES.SelectedIndex)
                        {
                            case 1:
                                Decrypt_Text.Text = des.CryptText(Crypting.Crypt.Encrypt, System.Security.Cryptography.CipherMode.CBC, Encrypt_Text.Text, OneLineKey_Encrypt.Text, OneLineIV_Encrypt.Text);
                                break;
                            case 2:
                                Decrypt_Text.Text = des.CryptText(Crypting.Crypt.Encrypt, System.Security.Cryptography.CipherMode.CFB, Encrypt_Text.Text, OneLineKey_Encrypt.Text, OneLineIV_Encrypt.Text);
                                break;
                            case 3:
                                Decrypt_Text.Text = des.CryptText(Crypting.Crypt.Encrypt, System.Security.Cryptography.CipherMode.ECB, Encrypt_Text.Text, OneLineKey_Encrypt.Text, OneLineIV_Encrypt.Text);
                                break;
                        }
                        break;
                    case 8:
                        Decrypt_Text.Text = Knapsack.encrypt(Encrypt_Text.Text, arrayParser(Open_Knap_Encrypt.Text));
                        break;
                    case 9:
                        Decrypt_Text.Text = RSA.encrypt(Encrypt_Text.Text, PublicKey_RSA_Encrypt.Text);
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
                    case 7:
                        switch (Type_of_DES.SelectedIndex)
                        {
                            case 1:
                                Encrypt_Text.Text = des.CryptText(Crypting.Crypt.Decrypt, System.Security.Cryptography.CipherMode.CBC, Decrypt_Text.Text, OneLineKey_Decrypt.Text, OneLineIV_Decrypt.Text);
                                break;
                            case 2:
                                Encrypt_Text.Text = des.CryptText(Crypting.Crypt.Decrypt, System.Security.Cryptography.CipherMode.CFB, Decrypt_Text.Text, OneLineKey_Decrypt.Text, OneLineIV_Decrypt.Text);
                                break;
                            case 3:
                                Encrypt_Text.Text = des.CryptText(Crypting.Crypt.Decrypt, System.Security.Cryptography.CipherMode.ECB, Decrypt_Text.Text, OneLineKey_Decrypt.Text, OneLineIV_Decrypt.Text);
                                break;
                        }
                        break;
                    case 8:
                        Encrypt_Text.Text = Knapsack.decrypt(Decrypt_Text.Text, arrayParser(Closed_Knap_Decrypt.Text), BigInteger.Parse(M_Knap_Decrypt.Text), BigInteger.Parse(T_Knap_Decrypt.Text));
                        break;
                    case 9:
                        Encrypt_Text.Text = RSA.decrypt(Decrypt_Text.Text, PrivateKey_RSA_Decrypt.Text);
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
                    if (rand == 0)
                    {
                        OneLineKey_Encrypt.Text = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\VerseFirst.txt");
                    }
                    if (rand == 1)
                    {
                        OneLineKey_Encrypt.Text = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\VerseSecond.txt");
                    }
                    if (rand == 2)
                    {
                        OneLineKey_Encrypt.Text = File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\VerseThird.txt");
                    }
                    break;
                case 7:
                    {
                        OneLineKey_Encrypt.Text = "";
                        for (int i = 0; i < 8; i++)
                        {
                            OneLineKey_Encrypt.Text += chars[Random.Next(chars.Length)];
                            OneLineKey_Encrypt.Text = OneLineKey_Encrypt.Text.ToUpper();
                        }
                        OneLineIV_Encrypt.Text = "";
                        for (int i = 0; i < 8; i++)
                        {
                            OneLineIV_Encrypt.Text += chars[Random.Next(chars.Length)];
                            OneLineIV_Encrypt.Text = OneLineIV_Encrypt.Text.ToUpper();
                        }
                    }
                    break;
                case 8:
                    try
                    {
                        Knapsack knapsack = new Knapsack(int.Parse(N_Knap.Text));
                        Open_Knap_Encrypt.Text = string.Join(",", knapsack.openKey);
                        T_Knap_Encrypt.Text = knapsack.t.ToString();
                        M_Knap_Encrypt.Text = knapsack.m.ToString();
                        Closed_Knap_Encrypt.Text = string.Join(",", knapsack.closedKey);
                    }
                    catch
                    {
                        MessageBox.Show("Length isn`t a number");
                    }
                    break;

                case 9:
                    rsa = new RSA();
                    PublicKey_RSA_Encrypt.Text = rsa.publicKey;
                    PrivateKey_RSA_Encrypt.Text = rsa.privateKey;
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

            thickness = OneLineIV_Decrypt.Margin;
            OneLineIV_Decrypt.Margin = OneLineIV_Encrypt.Margin;
            OneLineIV_Encrypt.Margin = thickness;

            thickness = N_Encrypt.Margin;
            N_Encrypt.Margin = T_Decrypt.Margin;
            T_Decrypt.Margin = thickness;

            thickness = N_Knap.Margin;
            N_Knap.Margin = T_Knap_Decrypt.Margin;
            T_Knap_Decrypt.Margin = thickness;

            thickness = Open_Encrypt.Margin;
            Open_Encrypt.Margin = M_Decrypt.Margin;
            M_Decrypt.Margin = thickness;

            thickness = Open_Knap_Encrypt.Margin;
            Open_Knap_Encrypt.Margin = M_Knap_Decrypt.Margin;
            M_Knap_Decrypt.Margin = thickness;

            thickness = T_Knap_Encrypt.Margin;
            T_Knap_Encrypt.Margin = Closed_Knap_Decrypt.Margin;
            Closed_Knap_Decrypt.Margin = thickness;

            thickness = T_Encrypt.Margin;
            T_Encrypt.Margin = Closed_Decrypt.Margin;
            Closed_Decrypt.Margin = thickness;

            thickness = M_Encrypt.Margin;
            M_Encrypt.Margin = M_Encrypt_Copy.Margin;
            M_Encrypt_Copy.Margin = thickness;

            thickness = M_Knap_Encrypt.Margin;
            M_Knap_Encrypt.Margin = M_Knap_Encrypt_Copy.Margin;
            M_Knap_Encrypt_Copy.Margin = thickness;

            thickness = Closed_Knap_Encrypt.Margin;
            Closed_Knap_Encrypt.Margin = Closed_Knap_Encrypt_Copy.Margin;
            Closed_Knap_Encrypt_Copy.Margin = thickness;

            thickness = Closed_Encrypt.Margin;
            Closed_Encrypt.Margin = Closed_Encrypt_Copy.Margin;
            Closed_Encrypt_Copy.Margin = thickness;

            thickness = PbKey_RSA_Decrypt.Margin;
            PbKey_RSA_Decrypt.Margin = PbKey_RSA_Encrypt.Margin;
            PbKey_RSA_Encrypt.Margin = thickness;

            thickness = PrKey_RSA_Decrypt.Margin;
            PrKey_RSA_Decrypt.Margin = PrKey_RSA_Encrypt.Margin;
            PrKey_RSA_Encrypt.Margin = thickness;

            thickness = PrivateKey_RSA_Decrypt.Margin;
            PrivateKey_RSA_Decrypt.Margin = PrivateKey_RSA_Encrypt.Margin;
            PrivateKey_RSA_Encrypt.Margin = thickness;

            thickness = PublicKey_RSA_Decrypt.Margin;
            PublicKey_RSA_Decrypt.Margin = PublicKey_RSA_Encrypt.Margin;
            PublicKey_RSA_Encrypt.Margin = thickness;
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
            OneLineIV_Encrypt.Text = "";
            T_Knap_Encrypt.Text = "";
            M_Knap_Encrypt.Text = "";
            Open_Knap_Encrypt.Text = "";
            Closed_Knap_Encrypt.Text = "";
            N_Knap.Text = "";
            PublicKey_RSA_Encrypt.Text = "";
            PrivateKey_RSA_Encrypt.Text = "";
        }

        private void Delete_Button_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            Decrypt_Text.Text = "";
            OneLineKey_Decrypt.Text = "";
            OneLineIV_Decrypt.Text = "";
            T_Knap_Decrypt.Text = "";
            M_Knap_Decrypt.Text = "";
            Closed_Knap_Decrypt.Text = "";
            PublicKey_RSA_Decrypt.Text = "";
            PrivateKey_RSA_Decrypt.Text = "";
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
            if(Type_of_crypt.SelectedIndex == 7)
            {
                Type_of_DES.Visibility = Visibility.Visible;
                IV_Decrypt.Visibility = Visibility.Visible;
                IV_Encrypt.Visibility = Visibility.Visible;
                OneLineIV_Decrypt.Visibility = Visibility.Visible;
                OneLineIV_Encrypt.Visibility = Visibility.Visible;
            }
            else
            {
                Type_of_DES.Visibility = Visibility.Hidden;
                IV_Decrypt.Visibility = Visibility.Hidden;
                IV_Encrypt.Visibility = Visibility.Hidden;
                OneLineIV_Decrypt.Visibility = Visibility.Hidden;
                OneLineIV_Encrypt.Visibility = Visibility.Hidden;
            }
            if(Type_of_crypt.SelectedIndex == 8)
            {
                N_Encrypt.Visibility = Visibility.Visible;
                N_Knap.Visibility = Visibility.Visible;
                T_Decrypt.Visibility = Visibility.Visible;
                T_Encrypt.Visibility = Visibility.Visible;
                T_Knap_Decrypt.Visibility = Visibility.Visible;
                T_Knap_Encrypt.Visibility = Visibility.Visible;
                M_Decrypt.Visibility = Visibility.Visible;
                M_Encrypt.Visibility = Visibility.Visible;
                M_Knap_Decrypt.Visibility = Visibility.Visible;
                M_Knap_Encrypt.Visibility = Visibility.Visible;
                Open_Knap_Encrypt.Visibility = Visibility.Visible;
                Open_Encrypt.Visibility = Visibility.Visible;
                Closed_Knap_Decrypt.Visibility = Visibility.Visible;
                Closed_Knap_Encrypt.Visibility = Visibility.Visible;
                Closed_Decrypt.Visibility = Visibility.Visible;
                Closed_Encrypt.Visibility = Visibility.Visible;

                Key_Encrypt.Visibility = Visibility.Hidden;
                Key_Decrypt.Visibility = Visibility.Hidden;
                OneLineKey_Decrypt.Visibility = Visibility.Hidden;
                OneLineKey_Encrypt.Visibility = Visibility.Hidden;

                Random_Key_Encrypt.Content = "Random Open Key";
            }
            else
            {
                Key_Encrypt.Visibility = Visibility.Visible;
                Key_Decrypt.Visibility = Visibility.Visible;
                OneLineKey_Decrypt.Visibility = Visibility.Visible;
                OneLineKey_Encrypt.Visibility = Visibility.Visible;

                N_Encrypt.Visibility = Visibility.Hidden;
                N_Knap.Visibility = Visibility.Hidden;
                T_Decrypt.Visibility = Visibility.Hidden;
                T_Encrypt.Visibility = Visibility.Hidden;
                T_Knap_Decrypt.Visibility = Visibility.Hidden;
                T_Knap_Encrypt.Visibility = Visibility.Hidden;
                M_Decrypt.Visibility = Visibility.Hidden;
                M_Encrypt.Visibility = Visibility.Hidden;
                M_Knap_Decrypt.Visibility = Visibility.Hidden;
                M_Knap_Encrypt.Visibility = Visibility.Hidden;
                Open_Knap_Encrypt.Visibility = Visibility.Hidden;
                Open_Encrypt.Visibility = Visibility.Hidden;
                Closed_Knap_Decrypt.Visibility = Visibility.Hidden;
                Closed_Knap_Encrypt.Visibility = Visibility.Hidden;
                Closed_Decrypt.Visibility = Visibility.Hidden;
                Closed_Encrypt.Visibility = Visibility.Hidden;

                Random_Key_Encrypt.Content = "Random Key";
            }
            if(Type_of_crypt.SelectedIndex == 9)
            {
                PublicKey_RSA_Encrypt.Visibility = Visibility.Visible;
                PrivateKey_RSA_Decrypt.Visibility = Visibility.Visible;
                PrivateKey_RSA_Encrypt.Visibility = Visibility.Visible;
                PrKey_RSA_Decrypt.Visibility = Visibility.Visible;
                PrKey_RSA_Encrypt.Visibility = Visibility.Visible;
                PbKey_RSA_Encrypt.Visibility = Visibility.Visible;

                Key_Encrypt.Visibility = Visibility.Hidden;
                Key_Decrypt.Visibility = Visibility.Hidden;
                OneLineKey_Decrypt.Visibility = Visibility.Hidden;
                OneLineKey_Encrypt.Visibility = Visibility.Hidden;

                Random_Key_Encrypt.Content = "Generate public key";
            }
            else
            {
                PublicKey_RSA_Encrypt.Visibility = Visibility.Hidden;
                PrivateKey_RSA_Decrypt.Visibility = Visibility.Hidden;
                PrivateKey_RSA_Encrypt.Visibility = Visibility.Hidden;
                PrKey_RSA_Decrypt.Visibility = Visibility.Hidden;
                PrKey_RSA_Encrypt.Visibility = Visibility.Hidden;
                PbKey_RSA_Encrypt.Visibility = Visibility.Hidden;

                Key_Encrypt.Visibility = Visibility.Visible;
                Key_Decrypt.Visibility = Visibility.Visible;
                OneLineKey_Decrypt.Visibility = Visibility.Visible;
                OneLineKey_Encrypt.Visibility = Visibility.Visible;

                Random_Key_Encrypt.Content = "Random Key";
            }
        }
    }
}
