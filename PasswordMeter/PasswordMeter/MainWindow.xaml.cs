using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordMeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Input velden: userNameTextBox en passwordTextBox
        /// Output veld: resultTextBlock
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
        }

        private void passwordMeterButton_Click(object sender, RoutedEventArgs e)
        {
            string inputUsername = userNameTextBox.Text.Trim();
            string inputPassword = passwordTextBox.Text.Trim();

            int passwordStrength = 0;

            if (string.IsNullOrEmpty(inputUsername) || string.IsNullOrEmpty(inputPassword)) 
            {
                resultTextBlock.Text = "Please enter a username and password";
                return;
            }

            if (!inputPassword.Contains(inputUsername))
            {
                passwordStrength++;
            }

            if (inputPassword.Length >= 10)
            {
                passwordStrength++;
            }

            bool hasDigit = false;
            bool hasUpper = false;
            bool hasLower = false;

            foreach (char c in inputPassword.ToCharArray())
            {
                if(char.IsDigit(c))
                {
                    hasDigit = true;
                }

                if(char.IsUpper(c))
                {
                    hasUpper = true;
                }

                if(char.IsLower(c))
                {
                    hasLower = true;
                }
            }

            if (hasDigit)
            {
                passwordStrength++;
            }

            if (hasUpper)
            {
                passwordStrength++;
            }

            if (hasLower)
            {
                passwordStrength++;
            }

            //if (passwordStrength == 5)
            //{
            //    resultTextBlock.Text = "Strong password";
            //}
            //else if (passwordStrength == 4)
            //{
            //    resultTextBlock.Text = "Medium password";
            //}
            //else
            //{
            //    resultTextBlock.Text = "Weak password";
            //}

            switch(passwordStrength)
            {
                case 5:
                    resultTextBlock.Text = "Strong password";
                    resultTextBlock.Foreground = Brushes.Green;
                    break;
                case 4:
                    resultTextBlock.Text = "Medium password";
                    resultTextBlock.Foreground = Brushes.Orange;
                    break;
                default:
                    resultTextBlock.Text = "Weak password";
                    resultTextBlock.Foreground = Brushes.Red;
                    break;
            }

            Random rnd = new Random();
            StringBuilder passwordBuilder = new StringBuilder();

            // 5 letters from username:
            for (int i = 0; i < 5; i++)
            {
                int randomPosition = rnd.Next(0, inputUsername.Length);
                string randomChar = inputUsername.Substring(randomPosition, 1);

                passwordBuilder.Append(randomChar.ToLower());
            }

            // 5 random digits
            for (int i = 0; i < 5; i++)
            {
                int randomNumber = rnd.Next(0, 10);
                passwordBuilder.Append(randomNumber);
            }

            // 2 letters from username
            for (int i = 0; i < 2; i++)
            {
                int randomPosition = rnd.Next(0, inputUsername.Length);
                string randomChar = inputUsername.Substring(randomPosition, 1);

                passwordBuilder.Append(randomChar.ToUpper());
            }

            // random number of exclamation marks
            for (int i = 0; i < rnd.Next(1, 6); i++)
            {
                passwordBuilder.Append("!");
            }

            // resultTextBlock.Text = passwordBuilder.ToString();

            MessageBoxResult result = MessageBox.Show("Gegenereerde wachtwoord", "Zwak wachtwoord", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if(result == MessageBoxResult.Yes)
            {
                passwordTextBox.Text = passwordBuilder.ToString();
            }

            string input = Interaction.InputBox("Type close to exit, Afsluiten", "close");

            if (input.Equals("close", StringComparison.CurrentCultureIgnoreCase))
            {
                this.Close();
            }
        }
    }
}
