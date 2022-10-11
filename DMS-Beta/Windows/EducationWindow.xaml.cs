using DMS_Beta.Controllers;
using DMS_Beta.Models;
using MD.PersianDateTime;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for EducationWindow.xaml
    /// </summary>
    public partial class EducationWindow : Window
    {
        #region variables
        private Int64 educationid;
        private string function;
        List<Attachment> documents = new List<Attachment>();
        List<Attachment> licenses = new List<Attachment>();
        public Int64 EducationID
        {
            get { return educationid; }
            set { educationid = value; }
        }
        public string Function
        {
            get { return function; }
            set { function = value; }
        }
        #endregion
        
        public EducationWindow(Int64 id, string f)
        {
            InitializeComponent();
            EducationID = id;
            Function = f;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)//save or update education
        {
            Education education = new Education();
            UsernamePassword userpass = new UsernamePassword();
            EducationController controller = new EducationController();

            try
            {
                #region education variables
                education.ID = EducationID;
                education.CourseName = course.Text;
                education.EmployeeId = int.Parse(emp_code.Text);
                education.EmployeName = fullname.Text;
                education.DepartmanName = combo1.SelectedItem.ToString();
                education.BeginDate = begindate.SelectedDate.ToDateTime();
                education.EndDate = enddate.SelectedDate.ToDateTime();
                education.Status = (string)((TextBlock)combo2.SelectedItem).Text;
                education.Institute = institute.Text;
                education.Cost = UInt64.Parse(cost.Text);
                education.Goal = goal.Text;
                #endregion
                #region education Authentication
                userpass.Username = username.Text;
                userpass.Password = password.Text;
                #endregion
                #region Attachments
                SaveToAttachmentsTabel(documents);
                SaveToAttachmentsTabel(licenses);
                #endregion
                if (Save.Content.ToString() == "ذخیره")
                {
                    MessageBox.Show(controller.SaveKnowledge(education, userpass) == "1" ? "عملیات با موفقیت انجام شد" : "خطایی رخ داد");
                }
                else
                {
                    MessageBox.Show(controller.UpdateKnowledge(education, userpass) == "1" ? "عملیات با موفقیت انجام شد" : "خطایی رخ داد");
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

        private void EducationLoad(object sender, RoutedEventArgs e)//get values from database when education select from parent window
        {
            DataTable dt = new DataTable();
            if (Function == "new")
            {
                Save.Content = "ذخیره";
            }
            else
            {
                Save.Content = "بروز رسانی";
                Save.FontSize = 15;

                #region Knowlege info
                try
                {
                    Education res = new Education();
                    UsernamePassword userpass = new UsernamePassword();
                    EducationController controller = new EducationController();
                    dt = controller.ShowKnowledge(EducationID);
                    course.Text = dt.Rows[0][0].ToString();
                    combo1.SelectedItem = dt.Rows[0][1].ToString();
                    institute.Text = dt.Rows[0][2].ToString();
                    DateTime date = DateTime.Parse(dt.Rows[0][3].ToString());
                    PersianCalendar pc = new PersianCalendar();
                    begindate.Text = pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString() + "/" + pc.GetDayOfMonth(date).ToString();
                    date = DateTime.Parse(dt.Rows[0][4].ToString());
                    enddate.Text = pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString() + "/" + pc.GetDayOfMonth(date).ToString();
                    goal.Text = dt.Rows[0][5].ToString();
                    cost.Text = dt.Rows[0][6].ToString();
                    combo2.Text = dt.Rows[0][7].ToString();
                    emp_code.Text = dt.Rows[0][8].ToString();
                    fullname.Text = dt.Rows[0][9].ToString();
                    username.Text = dt.Rows[0][10].ToString();
                    password.Text = dt.Rows[0][11].ToString();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Class.ToString() + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion

                #region attahments
                licenses = listfiles("Knowledge", "Licenses", EducationID);
                documents = listfiles("Knowledge", "Documents", EducationID);
                #endregion
            }

            #region show load departmans
            DepartmanController departman = new DepartmanController();
            dt = departman.ShowDepartmans();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                combo1.Items.Add(dt.Rows[i][1].ToString());
            }
            #endregion

        }

        private void Add1(object sender, RoutedEventArgs e)//education document's
        {
            documents = ChooseFile("Knowledge", "Documents", documentslbl);
        }

        private void Add2(object sender, RoutedEventArgs e)//education license
        {
            licenses = ChooseFile("Knowledge", "Licenses", licenselbl);
        }

        private void show1(object sender, System.Windows.Input.MouseButtonEventArgs e)//show education document's
        {
            showfiles(documents);
        }

        private void show2(object sender, System.Windows.Input.MouseButtonEventArgs e)//show education license
        {
            showfiles(licenses);
        }

        #region function's
        private void SaveToAttachmentsTabel(List<Attachment> attachments)//save attachments
        {
            foreach (Attachment item in attachments)
            {
                try
                {
                    item.Proccess = EducationID;
                    AttachmentController ac = new AttachmentController();
                    ac.SaveAttachments(item);
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
        }

        private void showfiles(List<Attachment> attachments)//show attachments
        {
            if (attachments.Count == 0)
            {
                MessageBox.Show("فایلی موجود نیست");
            }
            else
            {
                try
                {
                    for (int i = 0; i < attachments.Count; i++)
                    {
                        Attachment temp = new Attachment();
                        temp.Filename = attachments[i].Filename;
                        temp.FileType = attachments[i].FileType;
                        if (!Directory.Exists("D:/DMS/Rasa/Sorces/Education"))
                        {
                            Directory.CreateDirectory("D:/DMS/Rasa/Sorces/Education");
                        }
                        temp.Location = "D:/DMS/Rasa/Sorces/Education" + "/" + attachments[i].Proccess.ToString() + attachments[i].FileType.ToString();
                        temp.Filebuffer = attachments[i].Filebuffer;
                        var newfile = temp.Location.Replace(temp.FileType, DateTime.Now.Day + temp.FileType);
                        File.WriteAllBytes(newfile, temp.Filebuffer);
                        Process.Start(newfile);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private List<Attachment> listfiles(string proccesstype, string proccesssubtype, Int64 id)//get files from database
        {
            List<Attachment> attachments = new List<Attachment>();
            try
            {
                AttachmentController ac = new AttachmentController();
                DataTable res = ac.ShowAttachments(id, proccesstype, proccesssubtype);
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    Attachment attachment = new Attachment();
                    attachment.Type = proccesstype;
                    attachment.ProccessSubType = proccesssubtype;
                    attachment.Proccess = id;
                    attachment.Location = res.Rows[i][1].ToString();
                    attachment.Filename = res.Rows[i][0].ToString();
                    attachment.FileType = res.Rows[i][2].ToString();
                    attachment.Filebuffer = (byte[])res.Rows[i][3];
                    attachments.Add(attachment);
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
            return attachments;
        }

        private List<Attachment> ChooseFile(string p_type, string p_subtype, TextBlock label)//choose files
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
                    attachments.Add(temp);
                    label.ToolTip += temp.Filename + ",";
                    label.Text = "مشاهده";
                }
            }
            return attachments;
        }
        #endregion
    }
}
