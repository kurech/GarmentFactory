using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            dpFabricReceipt.Visibility = Visibility.Hidden;
            dpFabricReceipt.IsEnabled = false;
            dpFurnitureReceipt.Visibility = Visibility.Hidden;
            dpFurnitureReceipt.IsEnabled = false;
        }

        private void menuFurnitureList_Click(object sender, RoutedEventArgs e)
        {
            dpFabricsList.Visibility = Visibility.Hidden;
            dpFabricsList.IsEnabled = false;
            dpFurnitureList.Visibility = Visibility.Visible;
            dpFurnitureList.IsEnabled = true;
            dpFabricReceipt.Visibility = Visibility.Hidden;
            dpFabricReceipt.IsEnabled = false;
            dpFurnitureReceipt.Visibility = Visibility.Hidden;
            dpFurnitureReceipt.IsEnabled = false;
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
                foreach (Picture picture in garmentFactoryEntities.Pictures)
                {
                    cmbPicture.Items.Add(picture.Picture1);
                }

                foreach (Type type in garmentFactoryEntities.Types)
                {
                    cmbType.Items.Add(type.Type1);
                }

                var fabriclist = from Fabric in garmentFactoryEntities.Fabrics
                             join Picture in garmentFactoryEntities.Pictures on Fabric.IdPicture equals Picture.IdPicture
                             select new
                             {
                                 Артикул = Fabric.IdFabric,
                                 Название = Fabric.Name,
                                 Рисунок = Picture.Picture1,
                                 Ширина = Fabric.Width,
                                 Длина = Fabric.Length,
                                 Цена = Fabric.Price
                             };
                fabricGrid.ItemsSource = fabriclist.ToList();

                var furniturelist = from Furniture in garmentFactoryEntities.Furnitures
                             join FurnitureType in garmentFactoryEntities.FurnitureTypes on Furniture.IdFurniture equals FurnitureType.IdFurniture
                             join Type in garmentFactoryEntities.Types on FurnitureType.IdType equals Type.IdType
                             select new
                             {
                                 Артикул = Furniture.IdFurniture,
                                 Название = Furniture.Name,
                                 Тип = Type.Type1,
                                 Ширина = Furniture.Width,
                                 Длина = Furniture.Length,
                                 Вес = Furniture.Weight,
                                 Цена = Furniture.Price
                             };
                furnitureGrid.ItemsSource = furniturelist.ToList();
            }
        }

        private void btnAddFabric_Click(object sender, RoutedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Picture picture in garmentFactoryEntities.Pictures)
                {
                    if(cmbPicture.Text == picture.Picture1)
                    {
                        var selectedPicture = picture;

                        Fabric fabric = new Fabric()
                        {
                            IdFabric = txtArtikulFabric.Text,
                            Name = txtNameFabric.Text,
                            IdPicture = selectedPicture.IdPicture,
                            Width = int.Parse(txtWidthFabric.Text),
                            Length = int.Parse(txtLengthFabric.Text),
                            Price = int.Parse(txtPriceFabric.Text)
                        };
                        garmentFactoryEntities.Fabrics.Add(fabric);
                    }
                }
                garmentFactoryEntities.SaveChanges();
            }
        }

        private void btnAddFurniture_Click(object sender, RoutedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Type type in garmentFactoryEntities.Types)
                {
                    if (cmbType.Text == type.Type1)
                    {
                        var selectedType = type;

                        Furniture furniture = new Furniture()
                        {
                            IdFurniture = txtArtikulFurniture.Text,
                            Name = txtNameFurniture.Text,
                            Width = Convert.ToDouble(txtWidthFurniture.Text),
                            Length = Convert.ToDouble(txtLengthFurniture.Text),
                            Price = Convert.ToDouble(txtPriceFurniture.Text)
                        };
                        garmentFactoryEntities.Furnitures.Add(furniture);

                        FurnitureType furnitureType = new FurnitureType()
                        {
                            IdFurniture = furniture.IdFurniture,
                            IdType = selectedType.IdType
                        };
                        garmentFactoryEntities.FurnitureTypes.Add(furnitureType);
                    }
                }
                garmentFactoryEntities.SaveChanges();
            }
        }

        private void menuFabricReceipt_Click(object sender, RoutedEventArgs e)
        {
            dpFabricsList.Visibility = Visibility.Hidden;
            dpFabricsList.IsEnabled = false;
            dpFurnitureList.Visibility = Visibility.Hidden;
            dpFurnitureList.IsEnabled = false;
            dpFabricReceipt.Visibility = Visibility.Visible;
            dpFabricReceipt.IsEnabled = true;
            dpFurnitureReceipt.Visibility = Visibility.Hidden;
            dpFurnitureReceipt.IsEnabled = false;
        }

        private void menuFurnitureReceipt_Click(object sender, RoutedEventArgs e)
        {
            dpFabricsList.Visibility = Visibility.Hidden;
            dpFabricsList.IsEnabled = false;
            dpFurnitureList.Visibility = Visibility.Hidden;
            dpFurnitureList.IsEnabled = false;
            dpFabricReceipt.Visibility = Visibility.Hidden;
            dpFabricReceipt.IsEnabled = false;
            dpFurnitureReceipt.Visibility = Visibility.Visible;
            dpFurnitureReceipt.IsEnabled = true;
        }

        private void txtRecalculatedData_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
