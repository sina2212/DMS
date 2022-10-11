using DMS_Beta.Controllers;
using DMS_Beta.Models;
using DMS_Beta.Styles;
using MD.PersianDateTime;
using Microsoft.Win32;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for PersonalfileWindow.xaml
    /// </summary>
    public partial class PersonalfileWindow : Window
    {
        #region variables
        PhoneCard pc;
        AddressCard ac;
        List<PhoneModel> phones = new List<PhoneModel>();
        List<AddressModel> addresses = new List<AddressModel>();
        List<Attachment> educations = new List<Attachment>();
        List<Attachment> resumes = new List<Attachment>();
        List<Attachment> certificates = new List<Attachment>();
        List<Attachment> licenses = new List<Attachment>();
        List<Attachment> cover = new List<Attachment>();
        
        private int employeeid;
        public int EmployeeID
        {
            get { return employeeid; }
            set { employeeid = value; }
        }

        #endregion

        public PersonalfileWindow(int empid)
        {
            InitializeComponent();
            EmployeeID = empid;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPhone(object sender, RoutedEventArgs e)
        {
            pc = new PhoneCard();
            showusercontroller(pc, this.Height / 17, this.Width / 6);
            pc.Add += new EventHandler(getphone);
        }

        private void AddPhone2(object sender, RoutedEventArgs e)
        {
            pc = new PhoneCard();
            showusercontroller(pc, this.Width / 15, this.Width / 7);
            pc.Add += new EventHandler(getphone_emergency);
        }

        private void AddAddress(object sender, RoutedEventArgs e)
        {
            ac = new AddressCard();
            showusercontroller(ac, this.Width / 13, this.Width / 6);
            ac.Add += new EventHandler(getaddress);
        }

        private void Choosecover(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = new TextBlock();
            cover = new List<Attachment>();
            cover = ChooseFile("Employee", "cover", block, false);
        }

        private void personalfileload(object sender, RoutedEventArgs e)
        {
            EmployeeController E_controller = new EmployeeController();
            AddressController Ad_controller = new AddressController();
            DataTable dt = new DataTable();
            DataSet set = new DataSet();

            #region loadcomboboxes
            DepartmanController departman = new DepartmanController();
            dt = new DataTable();
            dt = departman.ShowDepartmans();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dep_.Items.Add(dt.Rows[i][1].ToString());
            }
            #endregion

            #region Employee Avatar
            try
            {
                AttachmentController attachmentController = new AttachmentController();
                dt = new DataTable();
                dt = attachmentController.ShowAttachments(EmployeeID, "Employee", "cover");
                Attachment attachment = new Attachment();
                attachment.Filebuffer = (byte[])dt.Rows[dt.Rows.Count - 1][3];
                avatar = attachmentController.CreateImage(attachment, avatar, Stretch.UniformToFill);
                border.Background = avatar;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Class.ToString() + "," + ex.Message.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            #region Employee information
            try
            {
                dt = new DataTable();
                dt = E_controller.ShowEmployee(EmployeeID);
                set.Tables.Add(dt);
                fullname.Text = set.Tables[0].Rows[0][0].ToString();
                single.Text = (bool)set.Tables[0].Rows[0][1] == false ? "متاهل" : "مجرد";
                child.Text = set.Tables[0].Rows[0][2].ToString();
                b_date.Text = changedate(DateTime.Parse(set.Tables[0].Rows[0][4].ToString()));
                code.Text = set.Tables[0].Rows[0][5].ToString();
                education.Text = set.Tables[0].Rows[0][3].ToString();
                position.Text = set.Tables[0].Rows[0][7].ToString();
                dep_.Text = set.Tables[0].Rows[0][6].ToString();
                exp_date.Text = changedate(DateTime.Parse(set.Tables[0].Rows[0][8].ToString()));
                begindate.Text = changedate(DateTime.Parse(set.Tables[0].Rows[0][11].ToString()));
                enddate.Text = changedate(DateTime.Parse(set.Tables[0].Rows[0][12].ToString()));
                title.Text = set.Tables[0].Rows[0][9].ToString();
                how.Text = set.Tables[0].Rows[0][10].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            #region phone
            showphones("Mobile", mobile_);
            showphones("Emergency", emergency_);
            showphones("Phone", company);
            #endregion

            #region address
            dt = new DataTable();
            set = new DataSet();
            try
            {
                dt = Ad_controller.ShowAddress(int.Parse(code.Text));
                set.Tables.Add(dt);
                for (int i = 0; i < set.Tables.Count; i++)
                {
                    for (int j = 0; j < set.Tables[i].Rows.Count; j++)
                    {
                        address_.Items.Add(set.Tables[i].Rows[j][0].ToString() + "-" + set.Tables[i].Rows[j][1].ToString() + "-" + set.Tables[i].Rows[j][2].ToString() + "-" + set.Tables[i].Rows[j][3].ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.State.ToString() + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee();
            PhoneModel phone = new PhoneModel();
            AddressModel address = new AddressModel();
            EmployeeController emp_controller = new EmployeeController();

            #region Employee information
            try
            {
                emp.RealName = fullname.Text;
                emp.Status = single.Text == "مجرد" ? true : false;
                emp.Child = int.Parse(child.Text);
                emp.BirthDate = b_date.SelectedDate.ToDateTime();
                emp.EmpCode = int.Parse(code.Text);
                emp.BeginEmployment = begindate.SelectedDate.ToDateTime();
                emp.EndEmployment = enddate.SelectedDate.ToDateTime();//contract end
                emp.JobTitle = title.Text;
                emp.JobPosition = position.Text;
                emp.Education = education.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            /*contract*/

            #region attachments
            SaveToAttachmentsTabel(educations);
            SaveToAttachmentsTabel(resumes);
            SaveToAttachmentsTabel(certificates);
            SaveToAttachmentsTabel(licenses);
            SaveToAttachmentsTabel(cover);
            #endregion

            #region phones
            foreach (var item in phones)
            {
                try
                {
                    item.Person_ref = int.Parse(code.Text);
                    PhoneController phoneController = new PhoneController();
                    phoneController.SavePhone(item);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.State.ToString() + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            #endregion

            #region address
            foreach (var item in addresses)
            {
                try
                {
                    item.Person_ref = int.Parse(code.Text);
                    item.Type = "Home";
                    AddressController addressController = new AddressController();
                    addressController.SaveAddress(item);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.State.ToString() + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            #endregion

            try
            {
                MessageBox.Show(emp_controller.UpdateEmployee(emp) == "1" ? "عملیات با موفقیت انجام شد" : "خطایی رخ داد");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.State.ToString() + ex.Message);
            }
        }

        #region attach personel file to thr employee
        private void AttachFile1(object sender, RoutedEventArgs e)//education
        {
            educations = new List<Attachment>();
            educations = ChooseFile("Employee", "Educations", lbl1, true);
        }
        private void AttachFile2(object sender, RoutedEventArgs e)//resume
        {
            resumes = new List<Attachment>();
            resumes = ChooseFile("Employee", "Resumes", lbl2, true);
        }
        private void AttachFile3(object sender, RoutedEventArgs e)//certificates
        {
            certificates = new List<Attachment>();
            certificates = ChooseFile("Employee", "Certificates", lbl3, true);
        }
        private void AttachFile4(object sender, RoutedEventArgs e)//contract
        {

        }
        private void AttachFile5(object sender, RoutedEventArgs e)//licenses
        {
            licenses = new List<Attachment>();
            licenses = ChooseFile("Employee", "Licenses", lbl5, true);
        }
        #endregion

        #region show attached file rfom local or from database
        private void show1(object sender, MouseButtonEventArgs e)
        {
            showfiles("Employee", "Certificates", EmployeeID, lbl3);
        }
        private void show2(object sender, MouseButtonEventArgs e)
        {
            showfiles("Employee", "Educations", EmployeeID, lbl1);
        }
        private void show3(object sender, MouseButtonEventArgs e)
        {
            showfiles("Employee", "Resumes", EmployeeID, lbl2);
        }
        private void show4(object sender, MouseButtonEventArgs e)
        {
            showfiles("Employee", "Licenses", EmployeeID, lbl2);
        }
        private void show5(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Function's
        private string changedate(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            string result = pc.GetYear(dateTime).ToString() +"/"+ pc.GetMonth(dateTime).ToString() + "/" + pc.GetDayOfMonth(dateTime).ToString();
            return result;
        }

        private void SaveToAttachmentsTabel(List<Attachment> attachments)//add to attachments
        {
            if (attachments.Count != 0)
            {
                foreach (Attachment item in attachments)
                {
                    try
                    {
                        item.Proccess = Int64.Parse(code.Text);
                        AttachmentController ac = new AttachmentController();
                        ac.SaveAttachments(item);
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

        private void showfiles(string proccesstype, string proccesssubtype, Int64 id, TextBlock lbl)//select attachments
        {
            try
            {
                AttachmentController ac = new AttachmentController();
                DataTable res = ac.ShowAttachments(id, proccesstype, proccesssubtype);
                if (res.Rows.Count < 1)
                {
                    MessageBox.Show("فایلی پیدا نشد");
                }
                else
                {
                    for (int i = 0; i < res.Rows.Count; i++)
                    {
                        Attachment temp = new Attachment();
                        temp.Filename = res.Rows[i][0].ToString();
                        temp.FileType = res.Rows[i][2].ToString();
                        if (!Directory.Exists("D:/DMS/Rasa/Sorces/" + proccesstype))
                        {
                            Directory.CreateDirectory("D:/DMS/Rasa/Sorces/" + proccesstype);
                        }
                        temp.Location = "D:/DMS/Rasa/Sorces/" + proccesstype + "/" + temp.Filename;
                        temp.Filebuffer = (byte[])res.Rows[i][3];
                        var newfile = temp.Location.Replace(temp.FileType, DateTime.Now.DayOfYear + temp.FileType);
                        File.WriteAllBytes(newfile, temp.Filebuffer);
                        Process.Start(newfile);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.State + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void showphones(string title, ComboBox combo)//show phones
        {
            DataTable res = new DataTable();
            PhoneController controller = new PhoneController();
            try
            {
                res = controller.ShowPhoneNumbers(int.Parse(code.Text), title);
                for (int j = 0; j < res.Rows.Count; j++)
                {
                    combo.Items.Add(res.Rows[j][2].ToString() + ": " + res.Rows[j][0].ToString());
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.State + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Attachment> ChooseFile(string p_type, string p_subtype, TextBlock label, bool select)//choose files
        {
            List<Attachment> attachments = new List<Attachment>();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == true)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    byte[] buffer;
                    using (Stream stream = File.OpenRead(file))
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, file.Length);
                    }
                    FileInfo fileInfo = new FileInfo(file);
                    Attachment temp = new Attachment();
                    temp.Location = fileInfo.FullName;
                    temp.Filename = fileInfo.Name;
                    temp.FileType = fileInfo.Extension;
                    temp.Filebuffer = buffer;
                    temp.Type = p_type;
                    temp.ProccessSubType = p_subtype;
                    //temp.Proccess = Int64.Parse(code.Text);
                    attachments.Add(temp);
                    label.ToolTip += temp.Filename + ",";
                }
            }
            return attachments;
        }

        private void getphone(object sender, EventArgs e)//get mobile phone number
        {
            PhoneModel p = new PhoneModel();
            p.Title = "Mobile";
            p.Subtitle = pc.sub;
            p.PhoneNumber = pc.phone;
            mobile_.Items.Add(pc.sub + ":" + pc.phone);
            phones.Add(p);
        }

        private void getphone_company(object sender, EventArgs e)//get mobile phone number
        {
            PhoneModel p = new PhoneModel();
            p.Title = "Phone";
            p.Subtitle = pc.sub;
            p.PhoneNumber = pc.phone;
            company.Items.Add(pc.sub + ":" + pc.phone);
            phones.Add(p);
        }

        private void getphone_emergency(object sender, EventArgs e)//get emergency phone number
        {
            PhoneModel p = new PhoneModel();
            p.Title = "Emergency";
            p.Subtitle = pc.sub;
            p.PhoneNumber = pc.phone;
            emergency_.Items.Add(pc.sub + ":" + pc.phone);
            phones.Add(p);
        }

        private void getaddress(object sender, EventArgs e)//get address
        {
            AddressModel A = new AddressModel();
            A.State = ac.state;
            A.City = ac.city;
            A.FullAddress = ac.address;
            address_.Items.Add(ac.state + "-" + ac.city + "-" + ac.address);
            addresses.Add(A);
        }

        public void showusercontroller(UserControl userControl, double top, double left)
        {
            if (childstack.Children.Count > 0)
            {
                childstack.Children.RemoveAt(0);
            }
            userControl.VerticalAlignment = VerticalAlignment.Top;
            userControl.HorizontalAlignment = HorizontalAlignment.Left;
            userControl.Margin = new Thickness(left, top, 0, 0);
            childstack.Children.Add(userControl);
        }
        #endregion

    }
}
