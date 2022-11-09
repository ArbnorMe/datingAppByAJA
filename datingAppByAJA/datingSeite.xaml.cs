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
using System.IO;
using Microsoft.Win32;

namespace datingAppByAJA
{
    /// <summary>
    /// Interaktionslogik für datingSeite.xaml
    /// </summary>
    public partial class datingSeite : Page
    {
        BitmapImage bi = null;
        public datingSeite()
        {
            InitializeComponent();
        }

        public string imgLocation = "";

        private void Favorisieren_Button_Click(object sender, RoutedEventArgs e)
        {
            Favorisieren_Button.Background = new SolidColorBrush(Colors.Red);
        }

        private void Btn_Upload_Click(object sender, RoutedEventArgs e)
        {
            // MySQL verbindung
            var connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");
            // Image location
            string imgLocation = "";

            MySqlCommand cmd;

            try
            {
                byte[] images = null;
                FileStream streem = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(streem);
                images = brs.ReadBytes((int)streem.Length);

                connection.Open();
                string sqlQuery = "Insert into bioData(email,Name,Image)Values'" + UserDaten.email + "','" + UserDaten.username + "Profilbild',@images";
                cmd = new MySqlCommand(sqlQuery, connection);
                cmd.Parameters.Add(new SqlParameter(""))
            }
            catch(Exception ex)
            {
                // Fehleranzeige
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
        }
    }
  }

