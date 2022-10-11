using DMS_Beta.Controllers;
using DMS_Beta.Models;
using DMS_Beta.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Styles
{
    /// <summary>
    /// Interaction logic for PartnersCard.xaml
    /// </summary>
    public partial class PartnersCard : UserControl
    {
        #region variables
        private Employee emp;
        private ImageBrush cover_;

        public ImageBrush Cover
        {
            get { return cover_; }
            set { cover_ = value; }
        }
        public Employee Employe
        {
            get { return emp; }
            set { emp = value; }
        }

        #endregion
        public PartnersCard(Employee emp)
        {
            InitializeComponent();
            this.Employe = emp;
            Cover = new ImageBrush();
            Cover.Stretch = Stretch.None;
            AttachmentController controller = new AttachmentController();
            Cover = controller.CreateImage(Employe.Avatar, Cover, Stretch.UniformToFill);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PartnerWindow window = new PartnerWindow(emp);
            window.ShowDialog();
        }

        private void CardLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            border.Background = Cover;
            dep_name.Text = Employe.DepName;
            realname.Text = Employe.RealName;
        }
    }
}
