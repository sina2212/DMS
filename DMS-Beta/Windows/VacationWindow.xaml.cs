using DMS_Beta.Controllers;
using DMS_Beta.Models;
using MD.PersianDateTime;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for VacationWindow.xaml
    /// </summary>
    public partial class VacationWindow : Window
    {
        #region Variables
        private UInt64 id;
        private string function;

        public string Function
        {
            get { return function; }
            set { function = value; }
        }

        public UInt64 V_ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        public VacationWindow(UInt64 id, string f)
        {
            InitializeComponent();
            V_ID = id;
            Function = f;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Vacation vacation = new Vacation();
                vacation.RealName = fullname.Text;
                vacation.Duration = int.Parse(duration.Text);
                vacation.Departman = combo1.SelectedItem.ToString();
                PersianDateTime pdt = PersianDateTime.Parse(date.Text);
                vacation.Date_ = pdt.ToDateTime();
                vacation.From = fromdate.SelectedDate.ToDateTime();
                vacation.Todate = todate.SelectedDate.ToDateTime();
                vacation.Status = status.SelectedItem.ToString() == "تایید شد" ? true : false;

                VacationController controller = new VacationController();
                if (Save.Content.ToString() == "ذخیره")
                {
                    MessageBox.Show(controller.Save(vacation) == "1" ? "عملیات انجام شد" : "خطایی رخ داده است");
                }
                else
                {
                    MessageBox.Show(controller.Save(vacation) == "1" ? "عملیات انجام شد" : "خطایی رخ داده است");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Class.ToString() + ex.Message.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
