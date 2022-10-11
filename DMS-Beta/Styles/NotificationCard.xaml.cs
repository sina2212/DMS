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

namespace DMS_Beta.Styles
{
    /// <summary>
    /// Interaction logic for NotificationCard.xaml
    /// </summary>
    public partial class NotificationCard : UserControl
    {
        public NotificationCard()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, MouseButtonEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
