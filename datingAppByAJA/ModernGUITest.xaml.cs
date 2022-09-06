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

namespace datingAppByAJA
{
    /// <summary>
    /// Interaktionslogik für ModernGUITest.xaml
    /// </summary>
    public partial class ModernGUITest : Page
    {
        public ModernGUITest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window1 != null)
            {
                window1.Main.Source = new Uri("Registrierung.xaml", UriKind.Relative);
            }
        }
    }
}


