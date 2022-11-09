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



        //Damit wenn man mit der Maus reingeht verschwindet der Template Text
        private void NameEingabeEnter(object sender, MouseEventArgs e)
        {
            if (NameText.Text == "Eingabe")
            {
                NameText.Clear();
            }
        }
        //Damit wenn man mit der Maus rausgeht erscheint der Template Text, wenn dort nichts reingeschrieben wurde
        private void NameEingabeLeave(object sender, MouseEventArgs e)
        {
            if (NameText.Text == "")
            { NameText.Text = "Eingabe"; }
        }

        private void VornameEnter(object sender, MouseEventArgs e)
        {
            if (VornameText.Text == "Eingabe")
            {
                VornameText.Clear();
            }
        }

        private void VornameLeave(object sender, MouseEventArgs e)
        {
            if (VornameText.Text == "")
            { VornameText.Text = "Eingabe"; }
        }

        private void Besteatigen_Click(object sender, RoutedEventArgs e)
        {
            //Sind die 3 Boxen wo man seine Daten eingeben kann
            string name = NameText.Text;
            string vorname = VornameText.Text;
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

