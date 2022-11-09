using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
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
    /// Interaktionslogik für Einstellungen.xaml
    /// </summary>
    public partial class Einstellungen : Page
    {
        public Einstellungen()
        {
            InitializeComponent();
        }



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

        private void Bestätigen_Click(object sender, RoutedEventArgs e)
        {
            //Sind die 3 Boxen wo man seine Daten eingeben kann
            string name = NameText.Text;
            string vorname = VornameText.Text;
            string geschlecht = GeschlechtBox.Text;
            var con =
                new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");
            //Die 3 Boxen werden damit in der Datenbank gespeichert
            string query = $"Insert into {DBVerbindung.informationsTable}(firstname, lastname, geschlecht)" + $" values('{name}','{vorname}','{geschlecht}')";
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
