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
using System.Windows.Shapes;

namespace GarmentFactory
{
    /// <summary>
    /// Логика взаимодействия для Director.xaml
    /// </summary>
    public partial class Director : Window
    {
        public Director()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void menuFabricReport_Click(object sender, RoutedEventArgs e)
        {
            dpFabricReport.Visibility = Visibility.Visible;
            dpFabricReport.IsEnabled = true;
            dpFurnitureReport.Visibility = Visibility.Hidden;
            dpFurnitureReport.IsEnabled = false;
        }

        private void menuFurnitureReport_Click(object sender, RoutedEventArgs e)
        {
            dpFabricReport.Visibility = Visibility.Hidden;
            dpFabricReport.IsEnabled = false;
            dpFurnitureReport.Visibility = Visibility.Visible;
            dpFurnitureReport.IsEnabled = true;
        }

        private void menuOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}
