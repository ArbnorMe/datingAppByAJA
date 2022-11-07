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
using System.Windows.Shapes;

namespace datingAppByAJA
{
    /// <summary>
    /// Interaktionslogik für DatingMainScreen.xaml
    /// </summary>
    public partial class DatingMainScreen : Window
    {
        public DatingMainScreen()
        {
            InitializeComponent();
        }
        private void Windows_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnHome(object sender, RoutedEventArgs e)
        {
            Main.Content = new datingSeite();
        }

        private void BtnEinstellung(object sender, RoutedEventArgs e)
        {
            Main.Content = new Einstellungen();
        }
    }
}
