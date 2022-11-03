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
    /// Логика взаимодействия для Storekeeper.xaml
    /// </summary>
    public partial class Storekeeper : Window
    {
        public Storekeeper()
        {
            InitializeComponent();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void menuFabricList_Click(object sender, RoutedEventArgs e)
        {
            dpFabricsList.Visibility = Visibility.Visible;
            dpFabricsList.IsEnabled = true;
            dpFurnitureList.Visibility = Visibility.Hidden;
            dpFurnitureList.IsEnabled = false;
        }

        private void menuFurnitureList_Click(object sender, RoutedEventArgs e)
        {
            dpFabricsList.Visibility = Visibility.Hidden;
            dpFabricsList.IsEnabled = false;
            dpFurnitureList.Visibility = Visibility.Visible;
            dpFurnitureList.IsEnabled = true;
        }

        private void menuOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                var query1 = from fabric in garmentFactoryEntities.Fabrics select new { Артикул = fabric.IdFabric, Наименование = fabric.Name, Цвет = fabric.FabricColors, Рисунок = fabric.Picture, Состав = fabric.FabricStructures, Ширина = fabric.Width, Длина = fabric.Length, Цена = fabric.Price };
                fabricGrid.ItemsSource = query1.ToList();

                var query2 = from furniture in garmentFactoryEntities.Furnitures select new { Артикул = furniture.IdFurniture, Наименование = furniture.Name, Ширина = furniture.Width, Длина = furniture.Length, Тип = furniture.FurnitureTypes, Цена = furniture.Price, Вес = furniture.Weight };
                furnitureGrid.ItemsSource = query2.ToList();
            }
        }
    }
}
