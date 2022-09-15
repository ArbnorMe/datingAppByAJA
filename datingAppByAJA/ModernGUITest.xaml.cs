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
        string serverMySql = "datingapp.mysql.arbnor.me";
        string userIdMySql = "root";
        string passwordMySql = "frVnoGZ53KaBZ58L9428";
        string databaseMySql = "datingApp";

        public ModernGUITest()
        {
            InitializeComponent();
        }

        private void TextBox1_Click(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window1 != null)
            {
                window1.Main.Source = new Uri("Registrierung.xaml", UriKind.Relative);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string password = PasswortPasswordBox.Password.ToString();
            string email = UsernameTextBox.Text;
            var connection = new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
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
    }
}


