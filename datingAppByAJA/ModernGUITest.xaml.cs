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
        public ModernGUITest()
        {
            InitializeComponent();
        }

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
            string email = EmailTextBox.Text;
            var connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");

            // Überprüuft ob der User schon existiert
            string userCheck = $"SELECT * FROM {DBVerbindung.userTable} WHERE email LIKE '{email}'";

            var commandUserCheck = new MySqlCommand(userCheck, connection);
            try
            {
                // SQL Befehl wird ausgeführt
                connection.Open();
                var reader = commandUserCheck.ExecuteReader();

                // SQL Reader wird ausgeführt
                while (reader.Read())
                {
                    // Guckt ob die E-Mail schon in der Datenbank vorhanden ist
                    if (reader["passwordUser"].ToString() == password)
                    {

                        UserDaten.username = reader["username"].ToString();

                        DatingMainScreen DMS = new DatingMainScreen();
                        DMS.Show();

                        MainWindow window1 = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                        if (window1 != null)
                        {
                            window1.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Die Zugangsdaten sind Falsch!");
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                // Fehler Ausgabe
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

        private void EmailTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (EmailTextBox.Text == "E-Mail")
            {
                EmailTextBox.Clear();
            }
        }

        private void EmailTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            
            if (EmailTextBox.Text == "") 
            { EmailTextBox.Text = "E-Mail"; }


        }
    }
}


