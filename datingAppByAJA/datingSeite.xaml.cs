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

        private void Favorisieren_Button_Click(object sender, RoutedEventArgs e)
        {
            //Wenn man den Button drückt wird der Rot
            Favorisieren_Button.Background = new SolidColorBrush(Colors.Red);
        }

        private void Btn_Upload_Click(object sender, RoutedEventArgs e)
        {

            var connection = new MySqlConnection($"server={DBVerbindung.serverMySql};user id={DBVerbindung.userIdMySql};password={DBVerbindung.passwordMySql};database={DBVerbindung.userTable};database{DBVerbindung.userpicturesTable}");

                using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO '{DBVerbindung.userpicturesTable}'(myimage) VALUES (@IM)", connection))
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
                        bi.EndInit();

                        //Pictures.Source = bi;
                    }
                    catch (System.Exception c) { Console.Write("Exception" + c); }
                }                
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save picture as ";
            save.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (bi != null)
            {
                if (save.ShowDialog() == true)
                {
                    JpegBitmapEncoder jpg = new JpegBitmapEncoder();
                    jpg.Frames.Add(BitmapFrame.Create(bi));
                    using (Stream stm = File.Create(save.FileName))
                    {
                        jpg.Save(stm);
                    }
                }
            }
        }
    }
  }

