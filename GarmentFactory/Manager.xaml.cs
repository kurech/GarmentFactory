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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void menuOrderList_Click(object sender, RoutedEventArgs e)
        {
            dpMakeOrder.Visibility = Visibility.Hidden;
            dpMakeOrder.IsEnabled = false;
            dpOrderList.Visibility = Visibility.Visible;
            dpOrderList.IsEnabled = true;
        }

        private void menuMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            dpMakeOrder.Visibility = Visibility.Visible;
            dpMakeOrder.IsEnabled = true;
            dpOrderList.Visibility = Visibility.Hidden;
            dpOrderList.IsEnabled = false;
        }

        private void menuOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void txtCount1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCount1.Clear();
        }

        int c = 0;
        private void lblAddProduct_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (c == 0)
            {
                lblAddProduct.Margin = new Thickness(20, -70, 0, 0);
                secondProduct.Visibility = Visibility.Visible;
                c++;
            }
            else if (c == 1)
            {
                lblAddProduct.Margin = new Thickness(20, 0, 0, 0);
                thirdProduct.Visibility = Visibility.Visible;
                lblAddProduct.Visibility = Visibility.Hidden;
                line.Margin = new Thickness(20, -58, 20, 0);
                c = 2;
            }
        }

        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateOrderList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Product product in garmentFactoryEntities.Product)
                {
                    cmbProduct1.Items.Add(product.IdProduct + " " + product.Name);
                    cmbProduct2.Items.Add(product.IdProduct + " " + product.Name);
                    cmbProduct3.Items.Add(product.IdProduct + " " + product.Name);
                }
            }
        }
    
    }
}
