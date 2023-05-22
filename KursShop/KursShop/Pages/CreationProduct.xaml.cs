using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace KursShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreationProduct.xaml
    /// </summary>
    public partial class CreationProduct : Page
    {
        private byte[] _ImageProduct;

        static public Entities.Product CPcurrentProduct { get; set; }
        static public bool Edit { get; set; }

        public CreationProduct()
        {
            InitializeComponent();

            if(Edit)
            {
                NameProduct.Text = ProductsList.NameProduct;
                CostProduct.Text = ProductsList.CostProduct;
                TagProduct.SelectedIndex = ProductsList.TagProduct;
                QuantityProduct.Text = ProductsList.QuantityProduct;
                ImageProduct.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(ProductsList.ImageProduct);
                _ImageProduct = ProductsList.ImageProduct;
                CreateHead.Text = "Редактирование";
                Edit = true;
                CPcurrentProduct = ProductsList.currentProduct;
            }

        }

        public void SelectImage(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openImage = new OpenFileDialog();
                openImage.Filter = "Image |*.png; *.jpg; *.jpeg";

                if (openImage.ShowDialog() == true)
                {
                    _ImageProduct = File.ReadAllBytes(openImage.FileName);
                    ImageProduct.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_ImageProduct);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении изображения.");
            }

        }

        public void SaveOrAddButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Edit)
                {
                    CPcurrentProduct.name = NameProduct.Text;
                    CPcurrentProduct.cost = Convert.ToInt32(CostProduct.Text);
                    CPcurrentProduct.id_tag = Convert.ToInt32(TagProduct.SelectedIndex);
                    CPcurrentProduct.quantity_product = Convert.ToInt32(QuantityProduct.Text);
                    CPcurrentProduct.imageData = _ImageProduct;
                    App.Context.SaveChanges();
                    MessageBox.Show("Продукт успешно сохранен!", "Внимание!");

                    NavigationService.Navigate(new ProductsList());
                }
                else
                {
                    var ProductAdd = new Entities.Product
                    {
                        name = NameProduct.Text,
                        cost = Convert.ToInt32(CostProduct.Text),
                        id_tag = TagProduct.SelectedIndex,
                        quantity_product = Convert.ToInt32(QuantityProduct.Text),
                        imageData = _ImageProduct
                    };
                    App.Context.Product.Add(ProductAdd);
                    App.Context.SaveChanges();
                    MessageBox.Show("Продукт успешно создан!", "Внимание!");

                    NavigationService.Navigate(new ProductsList());
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Введите корректные данные!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }
    }
}
