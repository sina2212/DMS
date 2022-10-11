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
    /// Interaction logic for InterviewerCard.xaml
    /// </summary>
    public partial class InterviewerCard : UserControl
    {
        #region Property
        public string Interviewer { get; set; }
        public string result { get; set; }
        public string comment { get; set; }
        public event EventHandler Add;
        #endregion

        public InterviewerCard()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Interviewer = combo1.Text;
            result = combo2.Text;
            comment = txt1.Text;
            Add(sender, e);
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void close(object sender, RoutedEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
