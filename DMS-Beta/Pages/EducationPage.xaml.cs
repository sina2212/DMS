using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Data;
using DMS_Beta.Windows;
using System.Data.SqlClient;
using System.ComponentModel;
using DMS_Beta.Models;
using System.Collections.Generic;
using System.Linq;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for EducationPage.xaml
    /// </summary>
    public partial class EducationPage : Page
    {
        public EducationPage()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void newEducation_Click(object sender, RoutedEventArgs e)
        {
            EducationWindow window = new EducationWindow(0, "new");
            if (PFdata.Items.Count == 0)
            {
                window = new EducationWindow(1, "new");
            }
            else
            {
                var cellInfo = new DataGridCellInfo(PFdata.Items[PFdata.Items.Count - 1], PFdata.Columns[0]);
                var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
                DataGridCell cell = (DataGridCell)cellContent.Parent;
                string index = ((TextBlock)cell.Content).Text;
                window = new EducationWindow(Int64.Parse(index) + 1, "new");
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void EducationLoad(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionstring);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from knowledge_view";
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            PFdata.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void Rowselect(object sender, MouseButtonEventArgs e)
        {
            var cellInfo = new DataGridCellInfo(PFdata.CurrentItem, PFdata.Columns[0]);
            var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
            DataGridCell cell = (DataGridCell)cellContent.Parent;
            string index = ((TextBlock)cell.Content).Text;
            EducationWindow window = new EducationWindow(Int64.Parse(index),"load");
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void SearchEducation(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            List<Int64> edsid = new List<long>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                edsid.Add(Int64.Parse(dt.Rows[i][0].ToString()));
            }
            for (int i = 0; i < edsid.Count; i++)
            {
                if (edsid[i].ToString() == searchtxt.Text)
                {
                    EducationWindow window = new EducationWindow(edsid[i], "load");
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.ShowDialog();
                }
            }
            if (flag == false)
            {
                MessageBox.Show("مصاحبه ایی مربوط به این شخص پیدا نشد");
            }
        }
    }
}
