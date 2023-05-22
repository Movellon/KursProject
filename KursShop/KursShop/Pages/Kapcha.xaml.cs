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

namespace KursShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для Kapcha.xaml
    /// </summary>
    public partial class Kapcha : Window
    {

        private string TrueInput = "";
        public Kapcha()
        {
            InitializeComponent();

            CreateKapcha();
        }

        private void BtnKapcha_Click(object sender, RoutedEventArgs e)
        {

            if (KapchaOutput.Text == TrueInput)
            {
                login.ActiveKapcha = false;
                Close();
            } 
            else
            {
                CreateKapcha();
                KapchaInput.Text = "";
            }
        }

        private void CreateKapcha()
        {
            Random random = new Random();

            byte[] bytes = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                int rndNumbOrAl = random.Next(0, 2);

                if (rndNumbOrAl == 0)
                {
                    byte rnd = Convert.ToByte(random.Next(65, 122));
                    bytes[i] = rnd;
                }
                else
                {
                    byte rndNumber = Convert.ToByte(random.Next(48, 57));
                    bytes[i] = rndNumber;
                }
            }

            TrueInput = Encoding.Default.GetString(bytes);

            KapchaOutput.Text = TrueInput;
        }
    }
}
