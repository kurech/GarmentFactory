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
        List<string> printContentFabric = new List<string>();
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

            PrintDoc();
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
        
        private void PrintDoc()
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(dpFabricReport, "Отчет по списанию тканей");
        }

        private void menuFabricRemains_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuFurnitureRemains_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (GarmentFactoryEntities garmentFactoryEntities = new GarmentFactoryEntities())
            {
                var fabricrep = from Fabric in garmentFactoryEntities.Fabrics
                                join Picture in garmentFactoryEntities.Pictures on Fabric.IdPicture equals Picture.IdPicture
                                join FabricStructure in garmentFactoryEntities.FabricStructures on Fabric.IdFabric equals FabricStructure.IdFabric
                                join Structure in garmentFactoryEntities.Structures on FabricStructure.IdStructure equals Structure.IdStructure
                                join FabricColor in garmentFactoryEntities.FabricColors on Fabric.IdFabric equals FabricColor.IdFabric
                                join Color in garmentFactoryEntities.Colors on FabricColor.IdColor equals Color.IdColor
                                select new
                                {
                                    Артикул = Fabric.IdFabric,
                                    Название = Fabric.Name,
                                    Цвет = Color.Color1,
                                    Состав = Structure.Structure1,
                                    Рисунок = Picture.Picture1
                                };
                MessageBox.Show(fabricrep.Count().ToString());
                fabricReport.ItemsSource = fabricrep.ToList();

                /*var furniturelist = from Furniture in garmentFactoryEntities.Furnitures
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
                furnitureGrid.ItemsSource = furniturelist.ToList();*/
            }
        }
    }
}
