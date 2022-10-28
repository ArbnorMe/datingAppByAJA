using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySqlConnector;

namespace datingAppByAJA
{
    /// <summary>
    /// Interaktionslogik für ModernGUITest.xaml
    /// </summary>
    public partial class ModernGUITest : Page
    {
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window1 != null)
            {
                window1.Main.Source = new Uri("Registrierung.xaml", UriKind.Relative);
            }
        }

        private void Login_Click_1(object sender, RoutedEventArgs e)
        {
            string password = PasswortPasswordBox.Password.ToString();
            string email = UsernameTextBox.Text;
            var connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");
            string query = $"Insert into userTable(password, email)" +
                $" values('{password}','{email}')";
            MessageBox.Show("Daten werden geschrieben");
            var command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PasswortPasswordBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PasswortPasswordBox.Password == "Password")
            {
                PasswortPasswordBox.Clear();
            }
        }

        private void PasswortPasswordBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PasswortPasswordBox.Password == "")
            {
                PasswortPasswordBox.Password = "Password";
            }
        }

        private void UsernameTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (UsernameTextBox.Text == "Username")
            {
                UsernameTextBox.Clear();
            }
        }

        private void UsernameTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            
            if (UsernameTextBox.Text == "") 
            { UsernameTextBox.Text = "Username"; }


        }
    }
}


