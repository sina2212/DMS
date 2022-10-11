using DMS_Beta.Controllers;
using DMS_Beta.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for PartnerWindow.xaml
    /// </summary>
    public partial class PartnerWindow : Window
    {
        #region varables
        private int empcode;
        
        public int EmpCode
        {
            get { return empcode; }
            set { empcode = value; }
        } 
        #endregion

        public PartnerWindow(Employee employee)
        {
            InitializeComponent();
            EmpCode = employee.EmpCode;
        }
        public void Close(object sender, EventArgs e)
        {
            this.Close();
        }
        public void PartnerWindowLoad(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            EmployeeController emp_ctrl = new EmployeeController();
            PhoneController p_ctrl = new PhoneController();
            AttachmentController a_ctrl = new AttachmentController();
            try
            {
                DataTable dt = emp_ctrl.ShowEmployee(EmpCode);
                name.Text += dt.Rows[0][0].ToString();
                code.Text += dt.Rows[0][5].ToString();
                departman_.Text += dt.Rows[0][6].ToString();
                dt = p_ctrl.ShowPhoneNumbers(EmpCode, "Phone");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    voip.Text += dt.Rows[i][0].ToString() + " ,";
                }
                dt = p_ctrl.ShowPhoneNumbers(EmpCode, "Mobile");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    mobile.Text += dt.Rows[i][0].ToString() + " ,";
                }
                dt = a_ctrl.ShowAttachments(EmpCode, "Employee", "cover");
                emp.Avatar = new Attachment();
                emp.Avatar.Filebuffer = (byte[])dt.Rows[0][3];
                cover = a_ctrl.CreateImage(emp.Avatar, cover, Stretch.UniformToFill);
                border.Background = cover;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Class + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
