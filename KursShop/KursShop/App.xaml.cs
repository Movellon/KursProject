using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KursShop
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.MusicDBEntities Context
        { get; } = new Entities.MusicDBEntities();

        public static Entities.Clients CurrentUser = null;

        public string LoginClient { get; set; }

        static public int AccessType { get; set; }

        public static string ModeApp { get; set; }

        private Visibility VisibilityBtnAdmin
        {
            get
            {
                if (ModeApp == "Admin")
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //Entities.Products_Client.
            /*Entities.Products_Client Vip;
            do
            {
                Vip = App.Context.Products_Client.FirstOrDefault(p => p.id_clients == LoginClient);
                Context.Products_Client.Remove(Vip);
                Context.SaveChanges();
                MessageBox.Show("asd");
            } while (Vip != null);

            MessageBox.Show("Закрытие приложения");*/
        }
    }
}
