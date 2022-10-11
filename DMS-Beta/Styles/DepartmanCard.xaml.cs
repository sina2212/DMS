using DMS_Beta.Models;
using DMS_Beta.Windows;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text;
using System.IO;
using DMS_Beta.Controllers;

namespace DMS_Beta.Styles
{
    /// <summary>
    /// Interaction logic for DepartmanCard.xaml
    /// </summary>
    public partial class DepartmanCard : UserControl
    {
        #region Variables
        private string departmanname;
        private ImageBrush departmanimage;
        private int departmancode;

        public int DepartmanCode
        {
            get { return departmancode; }
            set { departmancode = value; }
        }

        public ImageBrush DepartmanImage
        {
            get { return departmanimage; }
            set { departmanimage = value; }
        }
        public string DepartmanName
        {
            get { return departmanname; }
            set { departmanname = value; }
        }
        #endregion
        public DepartmanCard(Departman d, Attachment a)
        {
            InitializeComponent();
            this.DepartmanName = d.Name;
            DepartmanImage = new ImageBrush();
            DepartmanImage.Stretch = Stretch.None;
            AttachmentController controller = new AttachmentController();
            DepartmanImage = controller.CreateImage(a, DepartmanImage, Stretch.None);
            this.DepartmanCode = d.Code;
        }

        private void Grid_MouseLeftButtonDown(object sender, EventArgs e)
        {
            DepartmanWindow window = new DepartmanWindow(DepartmanCode, "load");
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void DepartmancardLoad(object sender, RoutedEventArgs e)
        {
            dep_name.Text = DepartmanName;
            dep_cover = DepartmanImage;
            border.Background = dep_cover;
        }
        
    }
}
