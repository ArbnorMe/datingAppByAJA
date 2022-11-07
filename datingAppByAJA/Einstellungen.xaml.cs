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




        private void NameEingabeEnter(object sender, MouseEventArgs e)
        {
            if (NameText.Text == "Eingabe")
            {
                NameText.Clear();
            }
        }

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
            string name = NameText.Text;
            string vorname = VornameText.Text;
            var con =
                new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.databaseMySql}");

            string query = $"Insert into {DBVerbindung.informationsTable}(firstname, lastname)" + $" values('{name}','{vorname}')";
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
