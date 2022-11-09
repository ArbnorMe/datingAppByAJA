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
using System.IO.MemoryMappedFiles;
using Microsoft.Win32;

namespace datingAppByAJA
{
    /// <summary>
    /// Interaktionslogik für Einstellungen.xaml
    /// </summary>
    public partial class Einstellungen : Page
    {
        BitmapImage bi = null;
        public Einstellungen()
        {
            InitializeComponent();
        }

        public string imgLocation = "";
       
        private void Besteatigen_Click(object sender, RoutedEventArgs e)
        {
            //Sind die 3 Boxen wo man seine Daten eingeben kann
            string name = nameEingabe.Text;
            string vorname = vornameEingabe.Text;
            string geschlecht = GeschlechtBox.Text;
            var con = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");
            //Die 3 Boxen werden damit in der Datenbank gespeichert
            string query = $"UPDATE {DBVerbindung.informationsTable} SET firstname = '{vorname}', lastname = '{name}', geschlecht = '{geschlecht}'  WHERE email = '{UserDaten.email}';";
            var command = new MySqlCommand(query, con);
            
            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Deine Einstellungen wurden geändert!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        public void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            // Stumpf von Phillip geklaut
            MySqlConnection connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");
            string sql = $"UPDATE {DBVerbindung.userpicturesTable} SET MyImage = LOAD_FILE('{Convert.FromBase64String(imgLocation)}'), FileName = '{bi}' WHERE email = 'Fenyu.de';";
            var command = new MySqlCommand(sql, connection);
            try
            {
 
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Funktioniert mein Lord!");
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            command.Dispose();
            connection.Close();
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
                    byte[] imagearray = System.IO.File.ReadAllBytes(open.FileName);
                    string texte = Convert.ToBase64String(imagearray);
                    imgLocation = texte;
                    //MessageBox.Show(texte);
                }
                catch (Exception ex)
                {
                    Console.Write("Exception: " + ex);
                }
            }
        }

        private void Btn_load_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = new byte[0];
            string sql = $"SELECT * FROM {DBVerbindung.userTable} WHERE email = {UserDaten.email};";
            try
            {
                //Bytearray auslesen aus der Datenbank
                //in bytes speichern
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unbekannter Datenbankfehler:\n\r" + ex.Message);
                return;
            }
            pictureBox.Source = LoadImage(bytes);
        }
        //Damit wenn man mit der Maus reingeht verschwindet der Template Text
        private void nameEingabe_MouseEnter(object sender, MouseEventArgs e)
        {
            if (nameEingabe.Text == "Name")
            {
                nameEingabe.Clear();
            }
        }

        private void nameEingabe_MouseLeave(object sender, MouseEventArgs e)
        {
            if (nameEingabe.Text == "")
            { nameEingabe.Text = "Name"; }
        }

        private void vornameEingabe_MouseEnter(object sender, MouseEventArgs e)
        {
            if (vornameEingabe.Text == "Vorname")
            {
                vornameEingabe.Clear();
            }
        }

        private void vornameEingabe_MouseLeave(object sender, MouseEventArgs e)
        {
            if (vornameEingabe.Text == "")
            {
                vornameEingabe.Text = "Vorname";
            }
        }
    }
}
    


