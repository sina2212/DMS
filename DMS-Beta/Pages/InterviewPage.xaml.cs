using DMS_Beta.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for InterviewPage.xaml
    /// </summary>
    public partial class InterviewPage : Page
    {
        DataTable dt = new DataTable();
        private int creator;

        public int Creator
        {
            get { return creator; }
            set { creator = value; }
        }

        public InterviewPage(int emp)
        {
            InitializeComponent();
            Creator = emp;
        }
        public void NewInterview(object sender, RoutedEventArgs e)
        {
            InterviewWindow window = new InterviewWindow(0, "new", Creator);
            
            if (PFdata.Items.Count == 0)
            {
                window = new InterviewWindow(1, "new", Creator);
            }
            else
            {
                var cellInfo = new DataGridCellInfo(PFdata.Items[PFdata.Items.Count-1], PFdata.Columns[0]);
                var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
                DataGridCell cell = (DataGridCell)cellContent.Parent;
                string index = ((TextBlock)cell.Content).Text;
                window = new InterviewWindow(Int64.Parse(index) + 1, "new", Creator);
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void InterviewLoad(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionstring);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            
            try
            {
                #region Show All Interviews information except interviewers
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from interview_view";
                dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                PFdata.ItemsSource = dt.DefaultView;
                for (int j = 0; j < PFdata.Items.Count; j++)
                {
                    DataRowView rowView = (PFdata.Items[j] as DataRowView);
                    rowView.BeginEdit();
                    changedate(j, 12, rowView);
                    changedate(j, 9, rowView);
                    changedate(j, 5, rowView);
                    rowView.EndEdit();
                    PFdata.Items.Refresh();
                }
                #endregion

                for (int i = 0; i < PFdata.Items.Count; i++)
                {
                    DataRowView rowView = (PFdata.Items[i] as DataRowView);
                    int iref = int.Parse((PFdata.Items[i] as DataRowView).Row.ItemArray[0].ToString());
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select interviewer from InterviewItem where interviewref = 1";
                    dt = new DataTable();
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    dt.Load(dr2);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        rowView.BeginEdit();
                        //rowView[3] += dt.Rows[j].ToString() + "-";
                        
                        rowView.EndEdit();
                        PFdata.Items.Refresh();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Class + ". " + ex.Message);
            }
            con.Close();
        }

        private void Rowselect(object sender, System.Windows.Input.MouseButtonEventArgs e)//select a interview to show
        {
            var cellInfo = new DataGridCellInfo(PFdata.CurrentItem, PFdata.Columns[0]);
            var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
            DataGridCell cell = (DataGridCell)cellContent.Parent;
            string index = ((TextBlock)cell.Content).Text;
            InterviewWindow window = new InterviewWindow(Int64.Parse(index), "load", Creator);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void SearchInterview(object sender, RoutedEventArgs e)//serach for a specefic interview
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
                if (invsname[i].ToString().Contains(searchtxt.Text))
                {
                    InterviewWindow window = new InterviewWindow(invsid[i], "load", int.Parse(dt.Rows[0][0].ToString()));
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.ShowDialog();
                    flag = true;
                }
            }
            if (flag==false)
            {
                MessageBox.Show("مصاحبه ایی مربوط به این شخص پیدا نشد");
            }
        }
        
        private void changedate(int i, int j, DataRowView rowView)//convert datetime fo grid to shamsi
        {
            if ((PFdata.Items[i] as DataRowView).Row.ItemArray[j].ToString() != "")
            {
                PersianCalendar pc = new PersianCalendar();
                DateTime date = DateTime.Parse((PFdata.Items[i] as DataRowView).Row.ItemArray[j].ToString());
                rowView[j] = pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString() + "/" + pc.GetDayOfMonth(date).ToString();
            }
        }
    }
}
