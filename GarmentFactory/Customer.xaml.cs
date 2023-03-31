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
    /// Логика взаимодействия для Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void menuOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        int c = 0;
        private void lblAddProduct_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(c == 0)
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

        private void menuMakeOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            if(cmbProduct1.Text != "Изделие" && cmbProduct2.Text != "Изделие" && cmbProduct3.Text != "Изделие")
            {
                int num;
                int sum = 0;
                if (int.TryParse(txtCost1.Text, out num) == true && int.TryParse(txtCount1.Text, out num) == true)
                {
                    sum += int.Parse(txtCost1.Text) * int.Parse(txtCount1.Text);
                }
                if (int.TryParse(txtCost2.Text, out num) == true && int.TryParse(txtCount2.Text, out num) == true)
                {
                    sum += int.Parse(txtCost2.Text) * int.Parse(txtCount2.Text);
                }
                if (int.TryParse(txtCost2.Text, out num) == true && int.TryParse(txtCount3.Text, out num) == true)
                {
                    sum += int.Parse(txtCost3.Text) * int.Parse(txtCount3.Text);
                }

                totalCost.Content = sum + " ₽";
            }
            else
            {
                MessageBox.Show("Выберите пожалуйста изделие!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void txtCount1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCount1.Clear();
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
