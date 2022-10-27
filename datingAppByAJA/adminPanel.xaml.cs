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
        string serverMySql = "datingapp-mysql.arbnor.me";
        string userIdMySql = "root";
        string passwordMySql = "r3a3ri6UzQxyvgx9mCn3UEkm7";
        string databaseMySql = "datingapp_db";
        public adminPanel()
        {
            InitializeComponent();
        }

        private void speicherBtn_Click(object sender, RoutedEventArgs e)
        {
            string password = passwortEingabe.Password.ToString();
            string email = emailEingabe.Text;
            var connection = new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            

            // Überprüft ob die adminCheckbox angeklickt ist
            if (adminCheckbox.IsChecked == true)
            {
                // Nutzer mit Admin Rechte wird erstellt
                string sendenMitAdminRechte = $"Insert into datingapp_table(password, email, adminRechte)" + $" values('{password}','{email}', 1)";

                var command = new MySqlCommand(sendenMitAdminRechte, connection);
                try
                {
                    // SQL Befehl wird ausgeführt
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    // Fehler Ausgabe
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // Nutzer ohne Admin Rechte wird erstellt
                string senden = $"Insert into datingapp_table(password, email)" + $" values('{password}','{email}')";

                var command = new MySqlCommand(senden, connection);
                try
                {
                    // SQL Befehl wird ausgeführt
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    // Fehler Ausgabe
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show("Daten geschrieben");
        }

        private void abfragenBtn_Click(object sender, RoutedEventArgs e)
        {
            var connection = new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            try
            {
                string eingabe = suchEingabe.Text;

                connection.Open();

                var command = new MySqlCommand($"SELECT * FROM datingapp_table WHERE email LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();
                lstbxAnzeige.Items.Add("iduser # password # email # geschlecht # firstname # lastname # adminRechte");
                while (reader.Read())
                {
                    lstbxAnzeige.Items.Add(
                        reader["iduser"] + " # " +
                        reader["password"] + " # " +
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
            var connection = new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            Console.WriteLine("TEST!");
            try
            {
                connection.Open();

                var command = new MySqlCommand($"SELECT * FROM datingapp_table WHERE iduser LIKE \"{eingabe}\"", connection);
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
                string mySqlBefehl = $"DELETE FROM `datingapp_table` WHERE (`iduser` = '{eingabe}')";
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

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            string eingabe = kontoLoeschungText.Text;
            var connection = new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");

            try
            {
                connection.Open();
                var command = new MySqlCommand($"SELECT * datingapp_table WHERE iduser LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    MessageBox.Show(Convert.ToString(reader["iduser"]));
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
