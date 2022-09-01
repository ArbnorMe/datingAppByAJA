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
    /// Made by Anton, Joshua and Arbnor
    public partial class MainWindow : Window
    {
        string serverMySql = "datingapp.mysql.arbnor.me";
        string userIdMySql = "root";
        string passwordMySql = "frVnoGZ53KaBZ58L9428";
        string databaseMySql = "datingApp";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnStartseite(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainWindow();
        }

        private void BtnRegistrieren(object sender, RoutedEventArgs e)
        {
            Main.Content = new Registrierung();
        }
    }
}
