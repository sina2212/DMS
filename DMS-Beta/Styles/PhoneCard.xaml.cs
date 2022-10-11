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
    /// Interaction logic for PhoneCard.xaml
    /// </summary>
    public partial class PhoneCard : UserControl
    {
        public string sub { get; set; }
        public string phone { get; set; }
        public event EventHandler Add;
        public PhoneCard()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            sub = txt1.Text;
            phone = txt2.Text;
            Add(sender, e);
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void PackIcon_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
