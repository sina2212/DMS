using DMS_Beta.Windows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for VacationsPage.xaml
    /// </summary>
    public partial class VacationsPage : Page
    {
        public VacationsPage()
        {
            InitializeComponent();
        }

        private void newVacation_Click(object sender, RoutedEventArgs e)
        {
            VacationWindow window = new VacationWindow(0, "new");
            if (Vdata.Items.Count == 0)
            {
                window = new VacationWindow(1, "new");
            }
            else
            {
                var cellInfo = new DataGridCellInfo(Vdata.Items[Vdata.Items.Count - 1], Vdata.Columns[0]);
                var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
                DataGridCell cell = (DataGridCell)cellContent.Parent;
                string index = ((TextBlock)cell.Content).Text;
                window = new VacationWindow(UInt64.Parse(index) + 1, "new");
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void VactionLoad(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionstring);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from vacation_view";
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            Vdata.ItemsSource = dt.DefaultView;
            con.Close();
        }
        
    }
}
