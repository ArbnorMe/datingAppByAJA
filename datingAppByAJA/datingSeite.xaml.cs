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

        // Image Location
        public string imgLocation = "";

        private void Favorisieren_Button_Click(object sender, RoutedEventArgs e)
        {
            //Wenn man den Button drückt wird der Rot
            Favorisieren_Button.Background = new SolidColorBrush(Colors.Red);
        }

        private void Btn_Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open Picture";
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == true)
            {
                try
                {
                    bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(open.FileName, UriKind.RelativeOrAbsolute);
                    imgLocation = open.FileName;
                    bi.EndInit();

                    pictureBox.Source = bi;
                }
                catch (Exception ex)
                {
                    Console.Write("Exception: " + ex);
                }
            }
        }
       

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            // MySQL verbindung
            var connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");

            MySqlCommand cmd;

            try
            {
                byte[] images = null;
                FileStream streem = new FileStream(imgLocation, FileMode.Open, FileAccess.Read); // Hier findet der Fehler statt
                BinaryReader brs = new BinaryReader(streem);
                images = brs.ReadBytes((int)streem.Length);

                connection.Open();
                string sqlQuery = $"Insert into {DBVerbindung.userpicturesTable}(email,Name,Image)Values'" + UserDaten.email + "','" + UserDaten.username + "Profilbild',@images";
                cmd = new MySqlCommand(sqlQuery, connection);
                cmd.Parameters.Add(new MySqlParameter("@images", images));
                int n = cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show(n.ToString() + " Datei wurde erfolgreich gesichert....... ");
            }
            catch (Exception ex)
            {
                // Fehleranzeige
                MessageBox.Show(ex.Message);
            }
        }
    }
  }

