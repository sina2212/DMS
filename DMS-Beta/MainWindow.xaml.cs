using System;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DMS_Beta.Controllers;
using DMS_Beta.Models;
using DMS_Beta.Pages;

namespace DMS_Beta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable input = new DataTable();
        Int64 logkey;
        public MainWindow(DataTable data)
        {
            InitializeComponent();
            input = data;
        }
        public void Choose(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
        public void SignOut(object sender, EventArgs e)
        {
            EmployeeController controller = new EmployeeController();
            var dialogResult = MessageBox.Show("آیا میخواهید خارج شوید", "خروج", MessageBoxButton.YesNo);
            if (dialogResult.ToString() == "Yes")
            {
                string res = controller.Logout(logkey);
                if (res == "1")
                {
                    Application.Current.Shutdown();
                }
            }
        }
        public void Partners(object sender, EventArgs e)
        {
            mainframe.Content = null;
            PartnersPage page = new PartnersPage();
            mainframe.Content = page;
        }
        public void Hire(object sender, EventArgs e)
        {
            mainframe.Content = null;
            HirePage hp = new HirePage();
            mainframe.Content = hp;
        }
        public void Human_resource_processes(object sender, EventArgs e)
        {
        }
        public void Fire(object sender, EventArgs e)
        {
        }
        public void Resignation(object sender, EventArgs e)
        {
        }
        public void Vacations(object sender, EventArgs e)
        {
            mainframe.Content = null;
            VacationsPage vp = new VacationsPage();
            mainframe.Content = vp;
        }
        public void Training_process(object sender, EventArgs e)
        {
        }
        public void PersonalFile_(object sender, EventArgs e)
        {
            mainframe.Content = null;
            PersonalFile pf_page = new PersonalFile();
            mainframe.Content = pf_page;
        }
        public void Interviews(object sender, EventArgs e)
        {
            mainframe.Content = null;
            InterviewPage ip = new InterviewPage(int.Parse(input.Rows[0][0].ToString()));
            mainframe.Content = ip;
        }
        public void Education(object sender, EventArgs e)
        {
            mainframe.Content = null;
            EducationPage ep = new EducationPage();
            mainframe.Content = ep;
        }
        private void Knowledge_Management(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            KnowledgeManagementPage kmp = new KnowledgeManagementPage();
            mainframe.Content = kmp;
        }
        private void Notifications(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            NotificationsPage np = new NotificationsPage();
            mainframe.Content = np;
        }
        private void Letters(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            LettersPage lp = new LettersPage();
            mainframe.Content = lp;
        }
        private void UserPass_Licenses(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            UserPass_LicensesPage upl = new UserPass_LicensesPage();
            mainframe.Content = upl;
        }
        private void Directors_license(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            Director_License_Page dlp = new Director_License_Page();
            mainframe.Content = dlp;
        }
        private void Reulation_Construction(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            Regulation_construction_Page rcp = new Regulation_construction_Page();
            mainframe.Content = rcp;
        }
        private void mainload(object sender, RoutedEventArgs e)
        {
            textBlock.Text = input.Rows[0][2].ToString();
            logkey = Int64.Parse(input.Rows[0][7].ToString());
            AttachmentController controller = new AttachmentController();
            Attachment avatar = new Attachment();
            if (input.Rows[0][5].ToString() != "")
            {
                avatar.Filebuffer = (byte[])input.Rows[0][5];
                cover = controller.CreateImage(avatar, cover, Stretch.UniformToFill);
                border.Background = cover;
            }
            else
            {
                cover = new ImageBrush();
                cover.Stretch = Stretch.Fill;
                cover.ImageSource = new ImageSourceConverter().ConvertFromString("D:/DMS/Rasa/Sorces/Resorces/Icons/Icon awesome-chart-bar.png") as ImageSource;
                border.Background = cover;
            }
        }

        private void Departmans(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            DepartmansPage dp = new DepartmansPage();
            mainframe.Content = dp;
        }
    }
}
