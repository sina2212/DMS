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
    /// Interaction logic for ChoosingEmployee.xaml
    /// </summary>
    public partial class ChoosingEmployee : UserControl
    {
        public int empcode { get; set; }
        public string empname { get; set; }
        public bool alreadyadded { get; set; }
        public event EventHandler Add;
        public event EventHandler Remove;
        public ChoosingEmployee(string name, int code)
        {
            InitializeComponent();
            empcode = code;
            empname = name;
            alreadyadded = false;
        }

        private void Chooseing(object sender, MouseButtonEventArgs e)
        {
            icon1.Background = Brushes.Green;
            empcode = int.Parse(code.Text.ToString());
            empname = name.Text.ToString();
            alreadyadded = true;
            Add(sender, e);
        }

        private void ChoosingEmployeeload(object sender, RoutedEventArgs e)
        {
            name.Text = empname;
            code.Text = empcode.ToString();
            alreadyadded = false;
        }

        private void Cancel(object sender, MouseButtonEventArgs e)
        {
            icon1.Background = Brushes.White;
            alreadyadded = false;
            Remove(sender, e);
        }
    }
}
