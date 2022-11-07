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
                string sendenMitAdminRechte = $"Insert into {DBVerbindung.userTable}(passwordUser, email, adminRechte)" + $" values('{password}','{email}', 1)";

                var command = new MySqlCommand(sendenMitAdminRechte, connection);
                try
                {
                    // SQL Befehl wird ausgeführt
                    connection.Open();
                    command.ExecuteNonQuery();
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
                string senden = $"Insert into {DBVerbindung.userTable}(passwordUser, email)" + $" values('{password}','{email}')";

                var command = new MySqlCommand(senden, connection);
                try
                {
                    // SQL Befehl wird ausgeführt
                    connection.Open();
                    command.ExecuteNonQuery();
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

                var command = new MySqlCommand($"SELECT * FROM {DBVerbindung.userTable} WHERE email LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();
                lstbxAnzeige.Items.Add("iduser # passwordUser # email # geschlecht # firstname # lastname # adminRechte");
                while (reader.Read())
                {
                    lstbxAnzeige.Items.Add(
                        reader["iduser"] + " # " +
                        reader["passwordUser"] + " # " +
                        reader["email"] + " # " +
                        reader["geschlecht"] + " # " +
                        reader["firstname"] + " # " +
                        reader["lastname"] + " # " +
                        reader["adminRechte"] + " # "
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
            Console.WriteLine("TEST!");
            try
            {
                connection.Open();

                var command = new MySqlCommand($"SELECT * FROM {DBVerbindung.userTable} WHERE iduser LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string iduserSV = Convert.ToString(reader["iduser"]);
                    if (iduserSV == eingabe)
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
                string mySqlBefehl = $"DELETE FROM `{DBVerbindung.userTable}` WHERE (`iduser` = '{eingabe}')";
                var command = new MySqlCommand(mySqlBefehl, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch
                {
                    MessageBox.Show("Fehler beim Löschen.");
                }
                finally
                {
                    MessageBox.Show("Der Nutzer konnte gelöscht werden");
                }
            }
        }
    }
}
