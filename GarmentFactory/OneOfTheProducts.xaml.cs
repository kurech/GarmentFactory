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
    /// Логика взаимодействия для OneOfTheProducts.xaml
    /// </summary>
    public partial class OneOfTheProducts : Window
    {
        public Product _product;
        public OneOfTheProducts(object o, Storekeeper storekeeper)
        {
            InitializeComponent();

            _product = (o as Button).DataContext as Product;
            BitmapImage bitmapImage = new BitmapImage();
            BitmapImage defaultImage = new BitmapImage();
            try
            {
                bitmapImage = new BitmapImage(new Uri($"pack://application:,,,/Resources/{_product.IdProduct}.jpg"));
            }
            catch { defaultImage = new BitmapImage(new Uri($"pack://application:,,,/Resources/4022221441666.jpg")); }
            lblProductName.Content = _product.Name;
            imgProductPhoto.Source = bitmapImage.UriSource != null ? bitmapImage : defaultImage;

            lblProductWidth.Content = "Ширина: " + _product.Width;
            lblProductHeight.Content = "Длина: " + _product.Lenght;
            this.Title = $"Продукт {_product.Name}";
        }
    }
}
