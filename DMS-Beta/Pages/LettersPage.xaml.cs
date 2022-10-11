using DMS_Beta.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for LettersPage.xaml
    /// </summary>
    public partial class LettersPage : Page
    {
        DataTable dt = new DataTable();
        public LettersPage()
        {
            InitializeComponent();
        }

        private void newLetter_Click(object sender, RoutedEventArgs e)
        {
            LetterWindow window = new LetterWindow(0, "new");
            if (PFdata.Items.Count == 0)
            {
                window = new LetterWindow(1, "new");
            }
            else
            {
                var cellInfo = new DataGridCellInfo(PFdata.Items[PFdata.Items.Count - 1], PFdata.Columns[0]);
                var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
                DataGridCell cell = (DataGridCell)cellContent.Parent;
                string index = ((TextBlock)cell.Content).Text;
                window = new LetterWindow(Int64.Parse(index) + 1, "new");
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void LettersPageLoad(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionstring);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from letters_view";
            dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            PFdata.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void RowSelect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cellInfo = new DataGridCellInfo(PFdata.Items[PFdata.Items.Count - 1], PFdata.Columns[0]);
            var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
            DataGridCell cell = (DataGridCell)cellContent.Parent;
            string index = ((TextBlock)cell.Content).Text;
            LetterWindow window = new LetterWindow(Int64.Parse(index), "load");
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void SearchLetter(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            List<Int64> invsid = new List<Int64>();
            List<string> invsname = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                invsid.Add(Int64.Parse(dt.Rows[i][0].ToString()));
                invsname.Add(dt.Rows[i][1].ToString());
            }
            for (int i = 0; i < invsid.Count; i++)
            {
                if (invsname[i].ToString() == searchtxt.Text)
                {
                    LetterWindow window = new LetterWindow(invsid[i], "load");
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.ShowDialog();
                    flag = true;
                }
            }
            if (flag == false)
            {
                MessageBox.Show("مصاحبه ایی مربوط به این شخص پیدا نشد");
            }
        }
    }
}
