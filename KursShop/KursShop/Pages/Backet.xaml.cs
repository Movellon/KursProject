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

namespace KursShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для Backet.xaml
    /// </summary>
    public partial class Backet : Page
    {
        public Backet()
        {
            InitializeComponent();
            try
            {
                ListViewBascket.ItemsSource = App.Context.Products_Client.Where(p => p.id_clients == MainWindow.IdClients).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }

        void UpdateBacket ()
        {
            try
            {
                var Backet = App.Context.Products_Client.ToList();

                ListViewBascket.ItemsSource = Backet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }

        private void DelProductBacket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var BascketProduct = (sender as Button).DataContext as Entities.Products_Client;
                if (MessageBox.Show($"Вы уверены, что хотите удалить продукт \"{BascketProduct.nameProduct}\" из корзины?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    App.Context.Products_Client.Remove(BascketProduct);
                    App.Context.SaveChanges();
                    UpdateBacket();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }

        private void BuyProduct_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                AppLog appLog = new AppLog();

                appLog.CreateReport();
            //}
            /*catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }*/
        }
    }
}
