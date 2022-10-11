using DMS_Beta.Windows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for UserPass_LicensesPage.xaml
    /// </summary>
    public partial class UserPass_LicensesPage : Page
    {
        public UserPass_LicensesPage()
        {
            InitializeComponent();
        }

        private void newUserPass_Click(object sender, RoutedEventArgs e)
        {
            UserPassWindow window = new UserPassWindow(0, "new");
            if (LSdata.Items.Count == 0)
            {
                window = new UserPassWindow(1, "new");
            }
            else
            {
                var cellInfo = new DataGridCellInfo(LSdata.Items[LSdata.Items.Count - 1], LSdata.Columns[0]);
                var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
                DataGridCell cell = (DataGridCell)cellContent.Parent;
                string index = ((TextBlock)cell.Content).Text;
                window = new UserPassWindow(Int64.Parse(index) + 1, "new");
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void newLicense_Click(object sender, RoutedEventArgs e)
        {
            LicenseWindow window = new LicenseWindow(0, "new");
            if (LSdata.Items.Count == 0)
            {
                window = new LicenseWindow(1, "new");
            }
            else
            {
                var cellInfo = new DataGridCellInfo(LSdata.Items[LSdata.Items.Count - 1], LSdata.Columns[0]);
                var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
                DataGridCell cell = (DataGridCell)cellContent.Parent;
                string index = ((TextBlock)cell.Content).Text;
                window = new LicenseWindow(Int64.Parse(index) + 1, "new");
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void UpserPassLicenseLoad(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionstring);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from license_view";
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            LSdata.ItemsSource = dt.DefaultView;

            cmd.CommandText = "select * from userpass_view";
            dt = new DataTable();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            UPdata.ItemsSource = dt.DefaultView;

            con.Close();
        }

        private void SelectUPDatarow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cellInfo = new DataGridCellInfo(UPdata.CurrentItem, UPdata.Columns[0]);
            var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
            DataGridCell cell = (DataGridCell)cellContent.Parent;
            string index = ((TextBlock)cell.Content).Text;
            UserPassWindow window = new UserPassWindow(Int64.Parse(index), "load");
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void LSdataSelectrow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cellInfo = new DataGridCellInfo(LSdata.CurrentItem, LSdata.Columns[0]);
            var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
            DataGridCell cell = (DataGridCell)cellContent.Parent;
            string index = ((TextBlock)cell.Content).Text;
            LicenseWindow window = new LicenseWindow(Int64.Parse(index), "load");
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }
    }
}
