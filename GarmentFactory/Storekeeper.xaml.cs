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
            using (GarmentFactoryEntities db = new GarmentFactoryEntities())
            {
                ListProducts.ItemsSource = db.Product.OrderBy(tour => tour.IdProduct).ToList();
            }
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
            dpInvent.Visibility = Visibility.Hidden;
            dpInvent.IsEnabled = false;
            dpFabricFurnitureWriteOff.Visibility = Visibility.Hidden;
            dpFabricFurnitureWriteOff.IsEnabled = false;
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
            dpInvent.Visibility = Visibility.Hidden;
            dpInvent.IsEnabled = false;
            dpFabricFurnitureWriteOff.Visibility = Visibility.Hidden;
            dpFabricFurnitureWriteOff.IsEnabled = false;
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
                foreach (Picture picture in garmentFactoryEntities.Picture)
                {
                    cmbPicture.Items.Add(picture.Picture1);
                }

                foreach (Type type in garmentFactoryEntities.Type)
                {
                    cmbType.Items.Add(type.Type1);
                }

                foreach (Fabric fabric in garmentFactoryEntities.Fabric)
                {
                    cmbChoseFabric.Items.Add(fabric.IdFabric + " " + fabric.Name);
                }

                foreach (Furniture furniture in garmentFactoryEntities.Furniture)
                {
                    cmbChoseFurniture.Items.Add(furniture.IdFurniture + " " + furniture.Name);
                }

                var fabriclist = from Fabric in garmentFactoryEntities.Fabric
                             join Picture in garmentFactoryEntities.Picture on Fabric.IdPicture equals Picture.IdPicture
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

                var furniturelist = from Furniture in garmentFactoryEntities.Furniture
                             join FurnitureType in garmentFactoryEntities.FurnitureType on Furniture.IdFurniture equals FurnitureType.IdFurniture
                             join Type in garmentFactoryEntities.Type on FurnitureType.IdType equals Type.IdType
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

        public void RefreshFabrics(string type)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                if (type == "сантиметр")
                {
                    var fabriclist = from Fabric in garmentFactoryEntities.Fabric
                                     join Picture in garmentFactoryEntities.Picture on Fabric.IdPicture equals Picture.IdPicture
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
                }
                else if (type == "метр")
                {
                    var fabriclist = from Fabric in garmentFactoryEntities.Fabric
                                     join Picture in garmentFactoryEntities.Picture on Fabric.IdPicture equals Picture.IdPicture
                                     select new
                                     {
                                         Артикул = Fabric.IdFabric,
                                         Название = Fabric.Name,
                                         Рисунок = Picture.Picture1,
                                         Ширина = (double)Fabric.Width / 100,
                                         Длина = (double)Fabric.Length / 100,
                                         Цена = Fabric.Price
                                     };
                    fabricGrid.ItemsSource = fabriclist.ToList();
                }
                else if (type == "миллиметр")
                {
                    var fabriclist = from Fabric in garmentFactoryEntities.Fabric
                                     join Picture in garmentFactoryEntities.Picture on Fabric.IdPicture equals Picture.IdPicture
                                     select new
                                     {
                                         Артикул = Fabric.IdFabric,
                                         Название = Fabric.Name,
                                         Рисунок = Picture.Picture1,
                                         Ширина = Fabric.Width * 10,
                                         Длина = Fabric.Length * 10,
                                         Цена = Fabric.Price
                                     };
                    fabricGrid.ItemsSource = fabriclist.ToList();
                }
                else
                {
                    var fabriclist = from Fabric in garmentFactoryEntities.Fabric
                                     join Picture in garmentFactoryEntities.Picture on Fabric.IdPicture equals Picture.IdPicture
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
                }
            }
        }

        private void btnAddFabric_Click(object sender, RoutedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Picture picture in garmentFactoryEntities.Picture)
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
                        garmentFactoryEntities.Fabric.Add(fabric);
                    }
                }
                garmentFactoryEntities.SaveChanges();
            }
        }

        private void btnAddFurniture_Click(object sender, RoutedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Type type in garmentFactoryEntities.Type)
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
                        garmentFactoryEntities.Furniture.Add(furniture);

                        FurnitureType furnitureType = new FurnitureType()
                        {
                            IdFurniture = furniture.IdFurniture,
                            IdType = selectedType.IdType
                        };
                        garmentFactoryEntities.FurnitureType.Add(furnitureType);
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
            dpInvent.Visibility = Visibility.Hidden;
            dpInvent.IsEnabled = false;
            dpFabricFurnitureWriteOff.Visibility = Visibility.Hidden;
            dpFabricFurnitureWriteOff.IsEnabled = false;
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
            dpInvent.Visibility = Visibility.Hidden;
            dpInvent.IsEnabled = false;
            dpFabricFurnitureWriteOff.Visibility = Visibility.Hidden;
            dpFabricFurnitureWriteOff.IsEnabled = false;
        }

        private void txtRecalculatedData_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtRecalculatedData.Clear();
        }

        private void btnFabricWriteOff_Click(object sender, RoutedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                foreach (Fabric fabric in garmentFactoryEntities.Fabric)
                {
                    if (cmbChoseFabric.SelectedItem.ToString() == (fabric.IdFabric + " " + fabric.Name))
                    {
                        MessageBox.Show("Уран");
                    }
                }
            }
                
        }

        private void menuInventory_Click(object sender, RoutedEventArgs e)
        {
            dpFabricsList.Visibility = Visibility.Hidden;
            dpFabricsList.IsEnabled = false;
            dpFurnitureList.Visibility = Visibility.Hidden;
            dpFurnitureList.IsEnabled = false;
            dpFabricReceipt.Visibility = Visibility.Hidden;
            dpFabricReceipt.IsEnabled = false;
            dpFurnitureReceipt.Visibility = Visibility.Hidden;
            dpFurnitureReceipt.IsEnabled = false;
            dpInvent.Visibility = Visibility.Visible;
            dpInvent.IsEnabled = true;
            dpFabricFurnitureWriteOff.Visibility = Visibility.Hidden;
            dpFabricFurnitureWriteOff.IsEnabled = false;
        }

        private void menuFabricFurnitureWriteOff_Click(object sender, RoutedEventArgs e)
        {
            dpFabricsList.Visibility = Visibility.Hidden;
            dpFabricsList.IsEnabled = false;
            dpFurnitureList.Visibility = Visibility.Hidden;
            dpFurnitureList.IsEnabled = false;
            dpFabricReceipt.Visibility = Visibility.Hidden;
            dpFabricReceipt.IsEnabled = false;
            dpFurnitureReceipt.Visibility = Visibility.Hidden;
            dpFurnitureReceipt.IsEnabled = false;
            dpInvent.Visibility = Visibility.Hidden;
            dpInvent.IsEnabled = false;
            dpFabricFurnitureWriteOff.Visibility = Visibility.Visible;
            dpFabricFurnitureWriteOff.IsEnabled = true;
        }

        private void btnFurnitureWriteOff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbsearchFabric_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cmbsearchFabric.SelectedItem;
            RefreshFabrics(typeItem.Content.ToString());
        }

        private void searchFabric_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (GarmentFactoryEntities db = new GarmentFactoryEntities())
            {
                if (searchFabric.Text.Length > 1)
                {
                    RefreshFabrics(cmbsearchFabric.Text);
                    string FindedName = searchFabric.Text;
                    var fabriclist = from Fabric in db.Fabric
                                     join Picture in db.Picture on Fabric.IdPicture equals Picture.IdPicture
                                     where Fabric.IdFabric == FindedName
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
                }
                else
                    RefreshFabrics(cmbsearchFabric.Text);
            }
        }

        private void menuCompanyProducts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOneOfTheProducts_Click(object sender, RoutedEventArgs e)
        {
            OneOfTheProducts oneOfTheProducts = new OneOfTheProducts(sender, this);
            oneOfTheProducts.Show();
        }
    }
}
