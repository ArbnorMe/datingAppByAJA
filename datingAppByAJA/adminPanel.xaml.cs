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
        string serverMySql = "datingapp.mysql.arbnor.me";
        string userIdMySql = "root";
        string passwordMySql = "frVnoGZ53KaBZ58L9428";
        string databaseMySql = "datingApp";
        public adminPanel()
        {
            InitializeComponent();
        }

        private void speicherBtn_Click(object sender, RoutedEventArgs e)
        {
            string password = passwortEingabe.Password.ToString();
            string email = emailEingabe.Text;
            var connection = new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            string query = $"Insert into userTable(password, email)" +
                $" values('{password}','{email}')";
            MessageBox.Show("Daten geschrieben");
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

        private void abfragenBtn_Click(object sender, RoutedEventArgs e)
        {
            var connection = new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            try
            {
                string eingabe = suchEingabe.Text;

                connection.Open();

                var command = new MySqlCommand($"SELECT * FROM datingApp.userTable WHERE email LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();
                lstbxAnzeige.Items.Add("iduser # password # email # geschlecht # firstname # lastname");
                while (reader.Read())
                {
                    lstbxAnzeige.Items.Add(
                        reader["iduser"] + " # " +
                        reader["password"] + " # " +
                        reader["email"] + " # " +
                        reader["geschlecht"] + " # " +
                        reader["firstname"] + " # " +
                        reader["lastname"] + " # "
                        );
                }

                reader.Close();
                connection.Close();
            }

            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void accDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string eingabe = kontoLoeschungText.Text;
            bool besteatigung = false;
            var connection = new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            Console.WriteLine("TEST!");
            try
            {
                connection.Open();

                var command = new MySqlCommand($"SELECT * FROM datingApp.userTable WHERE iduser LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string iduserSV = Convert.ToString(reader["iduser"]);
                    if (iduserSV == eingabe)
                    {
                        besteatigung = true;
                        Console.WriteLine("FEHLER!");
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
                MessageBox.Show("Fehler");
            }

            if (besteatigung == true)
            {
                string mySqlBefehl = $"DELETE FROM `datingApp`.`userTable` WHERE (`iduser` = '{eingabe}')";
                var command = new MySqlCommand(mySqlBefehl, connection);
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
}
