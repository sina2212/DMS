using System;
using System.Windows;
using System.Windows.Controls;

namespace DMS_Beta.Styles
{
    /// <summary>
    /// Interaction logic for AddressCard.xaml
    /// </summary>
    public partial class AddressCard : UserControl
    {
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public event EventHandler Add;
        public AddressCard()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            state = combo1.Text;
            city = combo2.Text;
            address = txt1.Text;
            Add(sender, e);
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void PackIcon_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
