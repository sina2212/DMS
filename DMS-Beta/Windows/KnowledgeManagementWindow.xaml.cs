using DMS_Beta.Controllers;
using DMS_Beta.Models;
using MD.PersianDateTime;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for KnowledgeManagementWindow.xaml
    /// </summary>
    public partial class KnowledgeManagementWindow : Window
    {
        #region variables
        private UInt64 kmid;
        private string function;

        public string Function
        {
            get { return function; }
            set { function = value; }
        }
        public UInt64 KMid
        {
            get { return kmid; }
            set { kmid = value; }
        }
        #endregion
        
        public KnowledgeManagementWindow(UInt64 id, string f)
        {
            InitializeComponent();
            KMid = id;
            Function = f;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            KnowledgeManagement management = new KnowledgeManagement();
            KnowledgeManagementController KMcontroller = new KnowledgeManagementController();
            try
            {
                management.Senario = scenario.Text;
                management.Client = owner.Text;
                management.KeyWord = word.Text;
                management.Date = date.SelectedDate.ToDateTime();
                management.Employer = int.Parse(code.Text);
                if (Save.Content.ToString() == "ذخیره")
                {
                    MessageBox.Show(KMcontroller.SaveKnowledgeManagement(management) == "1" ? "عملیات موفق آمیز بود" : "کارمند مربوطه پیدا نشد");
                }
                else
                {
                    management.KMID = KMid;
                    MessageBox.Show(KMcontroller.UpdateKnowledgeManagement(management) == "1" ? "عملیات موفق آمیز بود" : "کارمند مربوطه پیدا نشد");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Class.ToString() + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KMload(object sender, RoutedEventArgs e)
        {
            if (Function != "new")
            {
                Save.Content = "بروز رسانی";
                KnowledgeManagementController kmcontroller = new KnowledgeManagementController();
                DataTable res = kmcontroller.ShowKm(KMid);

                scenario.Text = res.Rows[0][1].ToString();
                word.Text = res.Rows[0][2].ToString();
                DateTime date_ = DateTime.Parse(res.Rows[0][3].ToString());
                PersianCalendar pc = new PersianCalendar();
                date.Text = pc.GetYear(date_).ToString() + "/" + pc.GetMonth(date_).ToString() + "/" + pc.GetDayOfMonth(date_).ToString();
                owner.Text = res.Rows[0][4].ToString();
                code.Text = res.Rows[0][6].ToString() + "( " + res.Rows[0][0].ToString() + " )";
                this.Title = res.Rows[0][5].ToString();
            }
        }
    }
}
