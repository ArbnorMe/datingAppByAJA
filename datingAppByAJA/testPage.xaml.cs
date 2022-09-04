using System;
using System.Threading;
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
    /// Interaktionslogik für testPage.xaml
    /// </summary>
    public partial class testPage : Page
    {
        string serverMySql = "datingapp.mysql.arbnor.me";
        string userIdMySql = "root";
        string passwordMySql = "frVnoGZ53KaBZ58L9428";
        string databaseMySql = "datingApp";
        public testPage()
        {
            InitializeComponent();
        }

        private void speicherBtn_Click(object sender, RoutedEventArgs e)
        {
            string password = passwortEingabe.Text;
            string email = emailEingabe.Text;
            var con =
                new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            string query = $"Insert into userTable(password, email)" +
                $" values('{password}','{email}')";
            MessageBox.Show("Daten geschrieben");
            var command = new MySqlCommand(query, con);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
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

                var command = new MySqlCommand($"SELECT * FROM datingApp.userTable WHERE password LIKE \"{eingabe}\"", connection);
                var reader = command.ExecuteReader();
                lstbxAnzeige.Items.Add("iduser # password # email # geschlecht # firstname # lastname");
                while (reader.Read())
                {
                    lstbxAnzeige.Items.Add(
                        reader["iduser"] +  " # " +
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

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test\"");
        }
    }
}
