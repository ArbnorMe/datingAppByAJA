using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string passwort = passwortEingabe.Text;
            string email = emailEingabe.Text;
            var con =
                new MySqlConnection("server=datingapp.mysql.arbnor.me;user id=root;password=frVnoGZ53KaBZ58L9428;database=datingApp");

            string query = $"Insert into user(email, password)" + 
                $" values('{email}','{passwort}')";
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
    }
}
