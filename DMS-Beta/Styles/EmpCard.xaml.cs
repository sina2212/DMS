using DMS_Beta.Controllers;
using DMS_Beta.Models;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Styles
{
    /// <summary>
    /// Interaction logic for EmpCard.xaml
    /// </summary>
    public partial class EmpCard : UserControl
    {
        #region Variables
        private ImageBrush image;
        private string emp_name;
        public string Emp_Name
        {
            get { return emp_name; }
            set { emp_name = value; }
        }
        public ImageBrush Cover
        {
            get { return image; }
            set { image = value; }
        }
        #endregion

        public EmpCard(string name, Attachment a)
        {
            InitializeComponent();
            this.Cover = new ImageBrush();
            Cover.Stretch = Stretch.UniformToFill;
            AttachmentController controller = new AttachmentController();
            Cover = controller.CreateImage(a, Cover, Stretch.UniformToFill);
            this.Emp_Name = name;
        }

        private void CardLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            name.Text = Emp_Name;
            emp_cover = Cover;
            border.Background = emp_cover;
        }
    }
}
