using KursShop.Pages;
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

namespace KursShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string IdClients { get; set; }

        public static int IdRoleUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            FrameMain.Navigate(new Pages.login());
            try
            {
                var Proverka = App.Context.Tags.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
                Close();
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonProductsList_Click(object sender, RoutedEventArgs e)
        {
            if (IdClients != null)
                FrameMain.Navigate(new Pages.ProductsList());
            else
                MessageBox.Show("Пройдите авторизацию!");
        }

        private void Button_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }

        private void BacketPage_Click(object sender, RoutedEventArgs e)
        {
            if (IdClients != null)
                FrameMain.Navigate(new Pages.Backet());
            else
                MessageBox.Show("Пройдите авторизацию!");
        }
    }
}
