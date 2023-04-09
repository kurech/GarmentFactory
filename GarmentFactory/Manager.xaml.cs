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
            try
            {
                using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
                {
                    Order order = new Order() { Date = DateTime.Now, ExecutionStage = "Новый", IdCustomer = cmbCustomers.Text, IdManager = "shh" };
                    garmentFactoryEntities.Order.Add(order);
                    garmentFactoryEntities.SaveChanges();

                    foreach (Product product in garmentFactoryEntities.Product)
                    {
                        if (cmbProduct1.Text == product.IdProduct + " " + product.Name)
                        {
                            CustomProducts custprod = new CustomProducts()
                            {
                                IdOrder = order.IdOrder,
                                IdProduct = product.IdProduct,
                                Count = txtCount1.Text
                            };
                            garmentFactoryEntities.CustomProducts.Add(custprod);
                        }
                        else if (cmbProduct2.Text == product.IdProduct + " " + product.Name)
                        {
                            CustomProducts custprod = new CustomProducts()
                            {
                                IdOrder = order.IdOrder,
                                IdProduct = product.IdProduct,
                                Count = txtCount2.Text
                            };
                            garmentFactoryEntities.CustomProducts.Add(custprod);
                        }
                        else if (cmbProduct3.Text == product.IdProduct + " " + product.Name)
                        {
                            CustomProducts custprod = new CustomProducts()
                            {
                                IdOrder = order.IdOrder,
                                IdProduct = product.IdProduct,
                                Count = txtCount3.Text
                            };
                            garmentFactoryEntities.CustomProducts.Add(custprod);
                        }
                        else if (cmbProduct4.Text == product.IdProduct + " " + product.Name)
                        {
                            CustomProducts custprod = new CustomProducts()
                            {
                                IdOrder = order.IdOrder,
                                IdProduct = product.IdProduct,
                                Count = txtCount4.Text
                            };
                            garmentFactoryEntities.CustomProducts.Add(custprod);
                        }
                        else if (cmbProduct5.Text == product.IdProduct + " " + product.Name)
                        {
                            CustomProducts custprod = new CustomProducts()
                            {
                                IdOrder = order.IdOrder,
                                IdProduct = product.IdProduct,
                                Count = txtCount5.Text
                            };
                            garmentFactoryEntities.CustomProducts.Add(custprod);
                        }
                    }
                    garmentFactoryEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    cmbProduct4.Items.Add(product.IdProduct + " " + product.Name);
                    cmbProduct5.Items.Add(product.IdProduct + " " + product.Name);
                    cmbProduct6.Items.Add(product.IdProduct + " " + product.Name);
                    cmbProduct7.Items.Add(product.IdProduct + " " + product.Name);
                    cmbProduct8.Items.Add(product.IdProduct + " " + product.Name);
                    cmbProduct9.Items.Add(product.IdProduct + " " + product.Name);
                    cmbProduct10.Items.Add(product.IdProduct + " " + product.Name);
                }

                foreach (User user in garmentFactoryEntities.User)
                {
                    if (user.Role == "Заказчик")
                        cmbCustomers.Items.Add(user.Login);
                }
            }
        }

        public double Cost(Product product, int count) // метод, который считает стоимость изделия
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                string fabricId = garmentFactoryEntities.FabricProduct.FirstOrDefault(fb => fb.IdProduct == product.IdProduct).IdFabric;
                string furnitureId = garmentFactoryEntities.FurnitureProduct.FirstOrDefault(fb => fb.IdProduct == product.IdProduct).IdFurniture;

                return (garmentFactoryEntities.Fabric.FirstOrDefault(fb => fb.IdFabric == fabricId).Price + garmentFactoryEntities.Furniture.FirstOrDefault(fb => fb.IdFurniture == furnitureId).Price) * count;
            }
        }

        private void txtCount1_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Product product in garmentFactoryEntities.Product)
                {
                    if (txtCount1.Text != "Количество" && txtCount1.Text != string.Empty)
                    {
                        if (cmbProduct1.Text == product.IdProduct + " " + product.Name)
                            txtCost1.Text = Cost(product, int.Parse(txtCount1.Text)).ToString();
                    }
                    if (txtCount1.Text == string.Empty)
                        txtCost1.Clear();
                }
            }
        }

        private void txtCount2_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Product product in garmentFactoryEntities.Product)
                {
                    if (txtCount2.Text != "Количество" && txtCount2.Text != string.Empty)
                    {
                        if (cmbProduct2.Text == product.IdProduct + " " + product.Name)
                            txtCost2.Text = Cost(product, int.Parse(txtCount2.Text)).ToString();
                    }
                    if (txtCount2.Text == string.Empty)
                        txtCost2.Clear();
                }
            }
        }

        private void txtCount2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCount2.Clear();
        }

        private void txtCount3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCount3.Clear();
        }

        private void txtCount3_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Product product in garmentFactoryEntities.Product)
                {
                    if (txtCount3.Text != "Количество" && txtCount3.Text != string.Empty)
                    {
                        if (cmbProduct3.Text == product.IdProduct + " " + product.Name)
                            txtCost3.Text = Cost(product, int.Parse(txtCount3.Text)).ToString();
                    }
                    if (txtCount3.Text == string.Empty)
                        txtCost3.Clear();
                }
            }
        }

        private void txtCount5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCount5.Clear();
        }

        private void txtCount5_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Product product in garmentFactoryEntities.Product)
                {
                    if (txtCount5.Text != "Количество" && txtCount5.Text != string.Empty)
                    {
                        if (cmbProduct5.Text == product.IdProduct + " " + product.Name)
                            txtCost5.Text = Cost(product, int.Parse(txtCount5.Text)).ToString();
                    }
                    if (txtCount5.Text == string.Empty)
                        txtCost5.Clear();
                }
            }
        }

        private void txtCount4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCount4.Clear();
        }

        private void txtCount4_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Product product in garmentFactoryEntities.Product)
                {
                    if (txtCount4.Text != "Количество" && txtCount4.Text != string.Empty)
                    {
                        if (cmbProduct4.Text == product.IdProduct + " " + product.Name)
                            txtCost4.Text = Cost(product, int.Parse(txtCount4.Text)).ToString();
                    }
                    if (txtCount4.Text == string.Empty)
                        txtCost4.Clear();
                }
            }
        }
    }
}
