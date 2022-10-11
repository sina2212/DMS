using DMS_Beta.Controllers;
using DMS_Beta.Models;
using DMS_Beta.Styles;
using MD.PersianDateTime;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for HirePage.xaml
    /// </summary>
    public partial class HirePage : Page
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
        List<Attachment> contracts = new List<Attachment>();
        Attachment cover;
        #endregion

        public HirePage()
        {
            InitializeComponent();
        }

        private void Choosecover(object sender, MouseButtonEventArgs e)//chose cover for the employee
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            cover = new Attachment();
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);
                byte[] buffer;
                using (Stream stream = File.OpenRead(fileInfo.FullName))
                {
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                }
                cover.Type = "Employee";
                cover.ProccessSubType = "cover";
                cover.Location = fileInfo.FullName;
                cover.Filename = fileInfo.Name;
                cover.FileType = fileInfo.Extension;
                cover.Filebuffer = buffer;
                avatar = new ImageBrush();
                avatar.Stretch = Stretch.UniformToFill;
                avatar.ImageSource = new BitmapImage(new Uri(fileInfo.FullName));
                border.Background = avatar;
            }
            else
            {
                cover.Location = "D:\\Projects\\DMS-Beta\\DMS-Beta\\Resorces\\Background\\backgroud_0018.jpg";
                cover.Filename = "backgroud_0018.jpg";
                cover.FileType = ".jpg";
            }
        }

        private void AddPhone(object sender, RoutedEventArgs e)//set mobile phones
        {
            pc = new PhoneCard();
            showusercontroller(pc, this.WindowHeight / 17, this.WindowWidth / 6);
            pc.Add += new EventHandler(getphone);
        }

        private void AddPhone2(object sender, RoutedEventArgs e)//set emergency phones
        {
            pc = new PhoneCard();
            showusercontroller(pc, this.WindowHeight / 4, this.WindowWidth / 3);
            pc.Add += new EventHandler(getphone_emergency);
        }

        private void AddAddress(object sender, RoutedEventArgs e)//set address
        {
            ac = new AddressCard();
            showusercontroller(ac, this.WindowWidth / 13, this.WindowHeight / 6);
            ac.Add += new EventHandler(getaddress);
        }

        #region attach personel file to the employee
        private void AttachFile1(object sender, RoutedEventArgs e)//education
        {
            educations = new List<Attachment>();
            educations = ChooseFile("Employee", "Educations", lbl1);
        }
        private void AttachFile2(object sender, RoutedEventArgs e)//resume
        {
            resumes = new List<Attachment>();
            resumes = ChooseFile("Employee", "Resumes", lbl2);
        }
        private void AttachFile3(object sender, RoutedEventArgs e)//certificates
        {
            certificates = new List<Attachment>();
            certificates = ChooseFile("Employee", "Certificates", lbl3);
        }
        private void AttachFile4(object sender, RoutedEventArgs e)//contract
        {

        }
        private void AttachFile5(object sender, RoutedEventArgs e)//licenses
        {
            licenses = new List<Attachment>();
            licenses = ChooseFile("Employee", "Licenses", lbl5);
        }
        #endregion

        #region show attached file rfom local or from database
        private void show1(object sender, MouseButtonEventArgs e)//show education's attachments
        {
            showfiles(educations);
        }
        private void show2(object sender, MouseButtonEventArgs e)//show interview attachments
        {
            showfiles(resumes);
        }
        private void show3(object sender, MouseButtonEventArgs e)//show the certificate's attachments
        {
            showfiles(certificates);
        }
        private void show4(object sender, MouseButtonEventArgs e)//show the contract's attachments
        {
            showfiles(contracts);
        }
        private void show5(object sender, MouseButtonEventArgs e)//show license's attachments
        {
            showfiles(licenses);
        }
        #endregion

        private void SaveEmployee(object sender, RoutedEventArgs e)//save employee to database
        {
            Employee emp = new Employee();
            PhoneModel phone = new PhoneModel();
            AddressModel address = new AddressModel();
            try
            {
                emp.RealName = fullname.Text;
                emp.Gender = gender.Text == "مرد" ? false : true;
                emp.Status = single.Text == "مجرد" ? true : false;
                emp.Child = int.Parse(child.Text);
                emp.BirthDate = birthdate.SelectedDate.ToDateTime();
                emp.EmpCode = int.Parse(code.Text);
                emp.BeginEmployment = beginhire.SelectedDate.ToDateTime();
                emp.Contract = Int64.Parse(contract.Text);
                phone.Title = "Phone";
                phone.Subtitle = "Company";
                phone.PhoneNumber = voip.Text;
                phones.Add(phone);
                emp.EndEmployment = enddate.SelectedDate.ToDateTime();//contract end date
                emp.JobPosition = position.Text;
                emp.DepName = dep_name.Text;
                emp.JobTitle = jobtitle.Text;
                emp.Education = education.Text;

                EmployeeController emp_controller = new EmployeeController();
                MessageBox.Show(emp_controller.SaveEmployee(emp) == "1" ? "عملیات انجام شد" : "خطایی رخ داد");

                #region attachments
                SaveToAttachmentsTabel(educations);
                SaveToAttachmentsTabel(resumes);
                SaveToAttachmentsTabel(certificates);
                SaveToAttachmentsTabel(licenses);
                AttachmentController ac = new AttachmentController();
                cover.Proccess = Int64.Parse(code.Text);
                //ac.SaveAttachments(cover);
                #endregion
                
                #region phones
                foreach (var item in phones)
                {
                    item.Person_ref = int.Parse(code.Text);
                    PhoneController phoneController = new PhoneController();
                    phoneController.SavePhone(item);
                }
                #endregion
                
                #region address
                foreach (var item in addresses)
                {
                    item.Person_ref = int.Parse(code.Text);
                    item.Type = "Home";
                    AddressController addressController = new AddressController();
                    addressController.SaveAddress(item);
                }
                #endregion
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Class.ToString() + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        #region Function's
        private void SaveToAttachmentsTabel(List<Attachment> attachments)
        {
            foreach (Attachment item in attachments)
            {
                item.Proccess = Int64.Parse(code.Text);
                AttachmentController ac = new AttachmentController();
                //ac.SaveAttachments(item);
            }
        }
        private List<Attachment> ChooseFile(string p_type, string p_subtype, TextBlock label)
        {
            List<Attachment> attachments = new List<Attachment>();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == true)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    Attachment temp = new Attachment();
                    byte[] buffer;
                    using (Stream stream = File.OpenRead(file))
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                    }
                    temp.Location = fileInfo.FullName;
                    temp.Filename = fileInfo.Name;
                    temp.FileType = fileInfo.Extension;
                    temp.Filebuffer = buffer;
                    temp.Type = p_type;
                    temp.ProccessSubType = p_subtype;
                    //temp.Proccess = Int64.Parse(textBox1.Text);
                    attachments.Add(temp);
                    label.ToolTip += temp.Filename + ",";
                }
            }
            return attachments;
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
        private void getphone(object sender, EventArgs e)
        {
            PhoneModel p = new PhoneModel();
            p.Title = "Mobile";
            p.Subtitle = pc.sub;
            p.PhoneNumber = pc.phone;
            mobile_.Items.Add(pc.sub + ":" + pc.phone);
            phones.Add(p);
        }
        private void getphone_emergency(object sender, EventArgs e)
        {
            PhoneModel p = new PhoneModel();
            p.Title = "Emergency";
            p.Subtitle = pc.sub;
            p.PhoneNumber = pc.phone;
            emergency_.Items.Add(pc.sub + ":" + pc.phone);
            phones.Add(p);
        }
        private void getaddress(object sender, EventArgs e)
        {
            AddressModel A = new AddressModel();
            A.State = ac.state;
            A.City = ac.city;
            A.FullAddress = ac.address;
            address_.Items.Add(ac.state + "-" + ac.city + "-" + ac.address);
            addresses.Add(A);
        }
        private void showfiles(List<Attachment> attachments)//show attachments
        {
            if (attachments.Count == 0)
            {
                MessageBox.Show("فایلی موجود نیست");
            }
            else
            {
                for (int i = 0; i < attachments.Count; i++)
                {
                    ProcessStartInfo psi = new ProcessStartInfo() { Arguments = attachments[i].Location.ToString(), FileName = "explorer.exe" };
                    if (File.Exists(attachments[i].Location.ToString()) == true)
                    {
                        Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("Can not find this file or the file direcory had been changed\n" + attachments[i].Location.ToString());
                    }
                }
            }
        }
        #endregion

        private void HirepageLoad(object sender, RoutedEventArgs e)
        {
            #region load departmans
            DepartmanController departman = new DepartmanController();
            DataTable dt = new DataTable();
            dt = departman.ShowDepartmans();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dep_name.Items.Add(dt.Rows[i][1].ToString());
            }
            #endregion
        }
    }
}
