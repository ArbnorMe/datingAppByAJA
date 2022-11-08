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

            if (password == passwordwdh && userVorhanden == false)
            {
                // Nutzer ohne Admin Rechte wird erstellt
                string user_table = $"Insert into {DBVerbindung.userTable}(passwordUser, email)" + $" values('{password}','{email}')";
                string informationen_table = $"Insert into {DBVerbindung.informationsTable}(email)" + $" values('{email}')";

                var command1 = new MySqlCommand(user_table, connection);
                var command2 = new MySqlCommand(informationen_table, connection);
                try
                {
                    // SQL Befehl wird ausgeführt
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Nutzer wurde erstellt.");
                }
                catch (Exception ex)
                {
                    // Fehler Ausgabe
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Die Passwörter sind nicht identisch!");
            }
        }
    }
}
