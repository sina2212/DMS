using DMS_Beta.Controllers;
using DMS_Beta.Models;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for UserPassWindow.xaml
    /// </summary>
    public partial class UserPassWindow : Window
    {
        #region variables
        private Int64 id;
        private string func;

        public string Function
        {
            get { return func; }
            set { func = value; }
        }
        public Int64 ID
        {
            get { return id; }
            set { id = value; }
        }

        #endregion

        public UserPassWindow(Int64 id, string f)
        {
            InitializeComponent();
            ID = id;
            Function = f;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            UsernamePassword userpass = new UsernamePassword();
            UserPassController userpasscontroller = new UserPassController();

            try
            {
                userpass.ID = this.ID;
                userpass.Username = username.Text;
                userpass.Password = password.Text;
                userpass.Title = TypeTextChanged(combo2.Text);
                userpass.TitleID = Int64.Parse(code.Text);
                if (Save.Content.ToString() == "ذخیره")
                {
                    MessageBox.Show(userpasscontroller.SaveUsernamePassword(userpass) == "1" ? "عملیات انجام شد" : "خطایی رخ داده است");
                }
                else
                {
                    MessageBox.Show(userpasscontroller.UpdateUserPass(userpass) == "1" ? "عملیات انجام شد" : "خطایی رخ داده است");
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
        private string TypeTextChanged(string txt)
        {
            string type = "";
            if (txt == "یوزرنیم پسورد کارکنان")
            {
                type = "EmpAuth";
            }
            else if (txt == "آموزش کارکنان")
            {
                type = "Education";
            }
            else if (txt == "گواهی نامه")
            {
                type = "license";
            }
            else if (txt == "سایت")
            {
                type = "site";
            }
            if (txt == "EmpAuth")
            {
                type = "یوزرنیم پسورد کارکنان";
            }
            else if (txt == "Education")
            {
                type = "آموزش کارکنان";
            }
            else if (txt == "license")
            {
                type = " گواهی نام";
            }
            else if (txt == "site")
            {
                type = "سایت";
            }
            return type;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if (Function != "new")
            {
                Save.Content = "بروز رسانی";
                Save.FontSize = 15;
                UserPassController controller = new UserPassController();
                UsernamePassword res = controller.Showuserpass(ID);
                res.ID = ID;
                username.Text = res.Username;
                password.Text = res.Password;
                code.Text = res.TitleID.ToString();
                combo2.Text = res.Title;
            }
        }
    }
}
