using System;
using System.Collections.Generic;
using System.IO;
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

            PrintDoc(fabricReport, "Отчет по списанию тканей");
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

        string text = "";
        private void PrintDoc(DataGrid dataGrid, string header)
        {
            foreach (var a in dataGrid.Items)
            {
                text += a + "\n";
            }
            
            PrintDialog printDlg = new PrintDialog();
            TextBlock visual = new TextBlock();
            visual.Inlines.Add(text);
            visual.Margin = new Thickness(5);
            visual.TextWrapping = TextWrapping.Wrap;
            Size pageSize = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight);
            visual.Measure(pageSize);
            visual.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
            if (printDlg.ShowDialog() == true)
                printDlg.PrintVisual(visual, header);
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
                var fabricrep = from Fabric in garmentFactoryEntities.Fabric
                                join Picture in garmentFactoryEntities.Picture on Fabric.IdPicture equals Picture.IdPicture
                                join FabricStructure in garmentFactoryEntities.FabricStructure on Fabric.IdFabric equals FabricStructure.IdFabric
                                join Structure in garmentFactoryEntities.Structure on FabricStructure.IdStructure equals Structure.IdStructure
                                join FabricColor in garmentFactoryEntities.FabricColor on Fabric.IdFabric equals FabricColor.IdFabric
                                join Color in garmentFactoryEntities.Color on FabricColor.IdColor equals Color.IdColor
                                select new
                                {
                                    Артикул = Fabric.IdFabric,
                                    Название = Fabric.Name,
                                    Цвет = Color.Color1,
                                    Состав = Structure.Structure1,
                                    Рисунок = Picture.Picture1
                                };
                fabricReport.ItemsSource = fabricrep.ToList();
            }
        }
    }
}
