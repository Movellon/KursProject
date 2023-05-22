using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

namespace KursShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsList.xaml
    /// </summary>
    public partial class ProductsList : Page
    {
        public static string NameProduct { get; set; }
        public static string CostProduct { get; set; }
        public static string QuantityProduct { get; set; }
        public static int TagProduct { get; set; }
        public static byte[] ImageProduct { get; set; }
        //public static bool Edit { get; set; }

        public static Entities.Product currentProduct { get; set; }

        public static Visibility VisibilityBtnAdmin { get; set; }

        private void UpdateProduct()
        {
            try
            {

                var Products = App.Context.Product.ToList();

                if (CBCost.SelectedIndex == 1)
                {
                    Products = Products.OrderByDescending(p => p.cost).ToList();
                }
                else if (CBCost.SelectedIndex == 2)
                {
                    Products = Products.OrderBy(p => p.cost).ToList();
                }


                if (CBType.SelectedIndex == 1)
                {
                    Products = Products.Where(p => p.id_tag == 1).ToList();
                }
                else if (CBType.SelectedIndex == 2)
                {
                    Products = Products.Where(p => p.id_tag == 2).ToList();
                }
                else if (CBType.SelectedIndex == 3)
                {
                    Products = Products.Where(p => p.id_tag == 3).ToList();
                }
                else if (CBType.SelectedIndex == 4)
                {
                    Products = Products.Where(p => p.id_tag == 4).ToList();
                }

                ListViewProduct.ItemsSource = Products;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        public ProductsList()
        {

            InitializeComponent();

            try
            {

                ListViewProduct.ItemsSource = App.Context.Product.ToList();

                CBCost.SelectedIndex = 0;
                CBType.SelectedIndex = 0;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }

        

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentProduct = (sender as Button).DataContext as Entities.Product;

                this.NavigationService.Navigate(new Uri("/Pages/CreationProduct.xaml", UriKind.Relative));

                NameProduct = currentProduct.name;
                CostProduct = currentProduct.cost.ToString();
                QuantityProduct = currentProduct.quantity_product.ToString();
                TagProduct = Convert.ToInt32(currentProduct.id_tag);
                ImageProduct = currentProduct.imageData;
                CreationProduct.Edit = true;
                CreationProduct.CPcurrentProduct = currentProduct;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentProduct = (sender as Button).DataContext as Entities.Product;
                if (MessageBox.Show($"Вы уверены, что хотите удалить продукт \"{currentProduct.name}\"", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    App.Context.Product.Remove(currentProduct);
                    App.Context.SaveChanges();
                    UpdateProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var BacketProduct = (sender as Button).DataContext as Entities.Product;

                var ProductAddQuantity = App.Context.Products_Client.Where(p => p.id_product == BacketProduct.id_product && p.id_clients == MainWindow.IdClients).FirstOrDefault();

                if (ProductAddQuantity == null)
                {
                    var AddProductToBascket = new Entities.Products_Client
                    {
                        id_product = BacketProduct.id_product,
                        id_clients = MainWindow.IdClients,
                        quantity = 1,
                        nameProduct = BacketProduct.name,
                        imageData = BacketProduct.imageData
                    };

                    App.Context.Products_Client.Add(AddProductToBascket);
                    App.Context.SaveChanges();

                    MessageBox.Show("Продукт добавлен в корзину!");
                }
                else
                {

                    ProductAddQuantity.quantity += 1;

                    App.Context.SaveChanges();

                    MessageBox.Show("Продукт добавлен в корзину!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            CreationProduct.Edit = false;
            this.NavigationService.Navigate(new Uri("/Pages/CreationProduct.xaml", UriKind.Relative));
        }

        private void CBCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void CBType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }
    }
}
