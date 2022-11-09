using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySqlConnector;
using System.Linq;

namespace datingAppByAJA
{
    /// <summary>
    /// Interaktionslogik für Registrierung.xaml
    /// </summary>
    public partial class Registrierung : Page
    {
        public Registrierung()
        {
            InitializeComponent();
        }

        private void registrierenBtn(object sender, RoutedEventArgs e)
        {
            string nutzername = nutzernameEingabe.Text;
            string email = emailEingabe.Text;
            string password = passwortEingabe.Password;
            string passwordwdh = passwortEingabeWiederholen.Password;
            bool userVorhanden = false;

            var connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");

            // Überprüuft ob der User schon existiert
            string userCheck = $"SELECT * FROM {DBVerbindung.userTable} WHERE email LIKE '{email}'";

            var commandUserCheck = new MySqlCommand(userCheck, connection);
            try
            {
                // SQL connection wird ausgeführt
                connection.Open();
                var reader = commandUserCheck.ExecuteReader();

                // SQL Reader wird ausgeführt
                while (reader.Read())
                {
                    // Guckt ob die E-Mail schon in der Datenbank vorhanden ist
                    if (reader["email"].ToString() == email)
                    {
                        userVorhanden = true;
                        connection.Close();
                        MessageBox.Show("Der Nutzer ist schon vorhanden!");
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Fehler Ausgabe
                MessageBox.Show(ex.Message);
            }

            if (userVorhanden == false)
            {
                if (password == passwordwdh)
                {
                    // Nutzer ohne Admin Rechte wird erstellt
                    string user_table = $"Insert into {DBVerbindung.userTable}(username, passwordUser, email)" + $" values('{nutzername}','{password}','{email}')";
                    string informationen_table = $"Insert into {DBVerbindung.informationsTable}(email)" + $" values('{email}')";
                    string userpictures_table = $"Insert into {DBVerbindung.userpicturesTable}(email)" + $" values('{email}')";

                    var command1 = new MySqlCommand(user_table, connection);
                    var command2 = new MySqlCommand(informationen_table, connection);
                    var command3 = new MySqlCommand(userpictures_table, connection);
                    try
                    {
                        // SQL Befehl wird ausgeführt
                        command1.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Fehler Ausgabe
                        MessageBox.Show("command1 Fehler: " + ex.Message);
                    }
                    try
                    {
                        // SQL Befehl wird ausgeführt
                        command2.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Fehler Ausgabe
                        MessageBox.Show("command2 Fehler: " + ex.Message);
                    }
                    try
                    {
                        // SQL Befehl wird ausgeführt
                        command3.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Fehler Ausgabe
                        MessageBox.Show("command3 Fehler: " + ex.Message);
                    }
                    connection.Close();
                        // Dieser Code greift auf die MainWindow zu und setzt den Frame vom Registrieren Seite auf Login Seite
                    MainWindow window1 = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (window1 != null)
                    {
                        window1.Main.Source = new Uri("Login.xaml", UriKind.Relative);
                    }
                    MessageBox.Show("Der Nutzer " + nutzername + " wurde erstellt.");
                }
                else
                {
                    MessageBox.Show("Die Passwörter sind nicht identisch!");
                }
            }
            else
            {
                MessageBox.Show("Der Nutzername ist leider schon vergeben!");
            }
        }
    }
}
