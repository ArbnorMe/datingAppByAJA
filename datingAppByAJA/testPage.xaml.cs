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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pbStatus.Value++;
            string password = passwortEingabe.Text;
            string email = emailEingabe.Text;
            var con =
                new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            pbStatus.Value++;
            string query = $"Insert into userTable(password, email)" +
                $" values('{password}','{email}')";
            MessageBox.Show("Daten geschrieben");
            var command = new MySqlCommand(query, con);
            pbStatus.Value++;
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                pbStatus.Value++;
            }
            catch (Exception ex)
            {
                pbStatus.Value++;
                MessageBox.Show(ex.Message);
            }
            pbStatus.Value = 4;

            Thread.Sleep(3000);
            pbStatus.Value = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string suchen = suchEingabe.Text;
            var con =
                new MySqlConnection($"server={serverMySql};user id={userIdMySql};password={passwordMySql};database={databaseMySql}");
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM datingApp.userTable WHERE password LIKE \"{suchen}\"");
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Du kannst Werte aus Spaltennamen abrufen
                Console.WriteLine(reader["email"].ToString());
                //Oder gebe den Wert aus der columnID zurück, in diesem Fall 0
                Console.WriteLine(reader.GetInt32(0));
            }
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test\"");
        }
    }
}
