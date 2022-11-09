using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
    /// Interaktionslogik für adminPanel.xaml
    /// </summary>
    public partial class adminPanel : Page
    {
        public adminPanel()
        {
            InitializeComponent();
        }

        private void speicherBtn_Click(object sender, RoutedEventArgs e)
        {

            string password = passwortEingabe.Password.ToString();
            string email = emailEingabe.Text;
            string nutzername = usernameEingabe.Text;
            bool userVorhanden = false;
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
                    if(reader["email"].ToString() == email)
                    {
                        userVorhanden = true;
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

            // Überprüft ob die adminCheckbox angeklickt ist
            if (adminCheckbox.IsChecked == true && userVorhanden == false)
            {
                // Nutzer mit Admin Rechte wird erstellt
                string user_table = $"Insert into {DBVerbindung.userTable}(username, passwordUser, email, adminRechte)" + $" values('{nutzername}','{password}','{email}', 1)";
                string informationen_table = $"Insert into {DBVerbindung.informationsTable}(email)" + $" values('{email}')";
                string userpictures_table = $"Insert into {DBVerbindung.userpicturesTable}(email)" + $" values('{email}')";

                var command1 = new MySqlCommand(user_table, connection);
                var command2 = new MySqlCommand(informationen_table, connection);
                var command3 = new MySqlCommand(userpictures_table, connection);

                try
                {
                    // SQL Befehl wird ausgeführt
                    connection.Open();
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Nutzer mit Admin Rechte wurde erstellt.");
                }
                catch (Exception ex)
                {
                    // Fehler Ausgabe
                    MessageBox.Show(ex.Message);
                }
            }
            else if (userVorhanden == false)
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
                    connection.Open();
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
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
                MessageBox.Show("Der User ist schon vorhanden");
            }
        }

        private void abfragenBtn_Click(object sender, RoutedEventArgs e)
        {
            var connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");
            try
            {
                string eingabe = suchEingabe.Text;

                connection.Open();

                var command = new MySqlCommand($"SELECT * FROM {DBVerbindung.userTable} WHERE username LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();
                lstbxAnzeige.Items.Add("iduser # username # passwordUser # email # adminRechte");
                while (reader.Read())
                {
                    lstbxAnzeige.Items.Add(
                        reader["iduser"] + " # " +
                        reader["username"] + " # " +
                        reader["passwordUser"] + " # " +
                        reader["email"] + " # " +
                        reader["adminRechte"]
                        );
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void accDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string eingabe = kontoLoeschungText.Text;
            bool besteatigung = false;
            var connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");

            try
            {
                connection.Open();

                var command = new MySqlCommand($"SELECT * FROM {DBVerbindung.userTable} WHERE email LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string emailSV = Convert.ToString(reader["email"]);
                    if (emailSV == eingabe)
                    {
                        besteatigung = true;
                    }
                    else
                    {
                        besteatigung = false;
                        MessageBox.Show("Es wurde nichts in der DB gefunden.");
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Die DB Suche konnte nicht richtig ausgeführt werden.");
            }

            if (besteatigung == true)
            {
                string mySqlBefehl1 = $"DELETE FROM {DBVerbindung.userTable} WHERE (email = '{eingabe}')";
                string mySqlBefehl2 = $"DELETE FROM {DBVerbindung.userpicturesTable} WHERE (email = '{eingabe}')";
                string mySqlBefehl3 = $"DELETE FROM {DBVerbindung.informationsTable} WHERE (email = '{eingabe}')";
                var command1 = new MySqlCommand(mySqlBefehl1, connection);
                var command2 = new MySqlCommand(mySqlBefehl2, connection);
                var command3 = new MySqlCommand(mySqlBefehl3, connection);

                try
                {
                    connection.Open();
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Der Nutzer konnte gelöscht werden");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Löschen. \nFehler: " + ex);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(UserDaten.username);
        }
    }
}
