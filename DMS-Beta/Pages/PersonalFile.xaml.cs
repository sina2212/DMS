using DMS_Beta.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for PersonalFile.xaml
    /// </summary>
    public partial class PersonalFile : Page
    {
        public PersonalFile()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void PersonalFileLoad(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionstring);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from employee_view";
            dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            PFdata.ItemsSource = dt.DefaultView;
            con.Close();
            
        }

        private void rowselect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cellInfo1 = new DataGridCellInfo(PFdata.CurrentItem, PFdata.Columns[7]);
            var cellContent1 = cellInfo1.Column.GetCellContent(cellInfo1.Item);
            DataGridCell cell1 = (DataGridCell)cellContent1.Parent;
            string index1 = ((TextBlock)cell1.Content).Text;
            PersonalfileWindow window = new PersonalfileWindow(int.Parse(index1));
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void SearchPersonal(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            List<int> invsid = new List<int>();
            List<string> invsname = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                invsid.Add(int.Parse(dt.Rows[i][8].ToString()));
                invsname.Add(dt.Rows[i][1].ToString());
            }
            for (int i = 0; i < invsid.Count; i++)
            {
                if (invsname[i].ToString() == searchtxt.Text)
                {
                    PersonalfileWindow window = new PersonalfileWindow(invsid[i]);
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.ShowDialog();
                    flag = true;
                }
            }
            if (flag == false)
            {
                MessageBox.Show("کارمندی مربوط پیدا نشد");
            }
        }
    }
}
