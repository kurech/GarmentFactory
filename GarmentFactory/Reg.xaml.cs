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
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
                {
                    foreach(User user1 in garmentFactoryEntities.User)
                    {
                        if(user1.Login == txtLogin.Text)
                        {
                            break;
                        }
                    }
                    User user = new User()
                    {
                        Login = txtLogin.Text,
                        Password = txtPassword.Text,
                        Role = "Заказчик"
                    };
                    garmentFactoryEntities.User.Add(user);
                    garmentFactoryEntities.SaveChanges();
                    MessageBox.Show($"Аккаунт {user.Login} успешно зарегистрирован!", "Seccess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
