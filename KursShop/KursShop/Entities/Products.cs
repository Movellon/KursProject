using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursShop.Entities
{
    public partial class Product
    {
        public Visibility VisibilityButton
        {
            get
            {
                if(MainWindow.IdRoleUser == 1)
                {
                    return Visibility.Collapsed;
                }
                else if(MainWindow.IdRoleUser == 2)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
        }
    }
}
