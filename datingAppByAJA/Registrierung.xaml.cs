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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string password = passwortEingabe.Password;
            string email = emailEingabe.Text;
            string passwordwdh = passwortEingabeWiederholen.Password;
            MessageBox.Show("Es wird jetzt zum Server verbunden");
            var con =
                new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");

            if (password == passwordwdh)
            {
                string query = $"Insert into datingapp_table(password, email)" +
                $" values('{password}','{email}')";
                MessageBox.Show("Daten geschrieben");
                var command = new MySqlCommand(query, con);

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();

                    MainWindow window1 = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (window1 != null)
                    {
                        window1.Main.Source = new Uri("ModernGUITest.xaml", UriKind.Relative);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Passwörter sind nicht identisch");
            }
        }
    }
}
