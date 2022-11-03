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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GarmentFactory
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
                {
                    foreach (User user in garmentFactoryEntities.Users)
                    {
                        if (user.Login == txtLogin.Text && user.Password == txtPassword.Text)
                        { 
                            if (user.Role == "Заказчик")
                            {
                                Customer customer = new Customer();
                                customer.Show();
                                this.Hide();
                            }
                            else if (user.Role == "Кладовщик")
                            {
                                Storekeeper storekeeper = new Storekeeper();
                                storekeeper.Show();
                                this.Hide();
                            }
                            else if (user.Role == "Менеджер")
                            {
                                Manager manager = new Manager();
                                manager.Show();
                                this.Hide();
                            }
                            else if (user.Role == "Директор")
                            {
                                Director director = new Director();
                                director.Show();
                                this.Hide();
                            }
                            MessageBox.Show($"{user.Login}, добро пожаловать!", "Seccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblReg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void txtLogin_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLogin.Clear();
        }

        private void txtPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Clear();
        }
    }
}
