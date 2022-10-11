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
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for InterviewWindow.xaml
    /// </summary>
    public partial class InterviewWindow : Window
    {
        #region variables
        private Int64 interviewid;
        private string function;
        private int code_;
        int attachesindex = 0;
        List<InterviewItem> interviewers = new List<InterviewItem>();
        InterviewerCard ic;

        public int Code
        {
            get { return code_; }
            set { code_ = value; }
        }
        public string Function
        {
            get { return function; }
            set { function = value; }
        }
        public Int64 INterviewID
        {
            get { return interviewid; }
            set { interviewid = value; }
        }
        List<Attachment> attachments = new List<Attachment>();
        #endregion

        public InterviewWindow(Int64 id, string f, int emp)
        {
            InitializeComponent();
            Function = f;
            INterviewID = id;
            Code = emp;
        }

        public void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void combo3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combo3.SelectedIndex == 2)
            {
                description.Visibility = Visibility.Visible;
            }
            else
            {
                description.Visibility = Visibility.Hidden;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)//save and update interview
        {
            Interview interview = new Interview();
            Person person = new Person();
            PhoneModel phone = new PhoneModel();
            InterviewController ic_ = new InterviewController();
            
            if (Save.Content.ToString() == "ذخیره")
            {
                string res = "";
                try
                {
                    res += setpersonparameters(person);

                    setinterviewparameters(interview);

                    res += setphoneparameters(phone, (int)INterviewID);

                    setfileattachments(attachments, attachesindex);
                    
                    if (res.Contains("0") == false)
                    {
                        MessageBox.Show(ic_.SaveInterview(interview, nationalcode.Text, Code) != "0" ? "عملیات با موفقیت انجام شد" : "خطایی رخ داده");
                        Save.Content = "بروزرسانی";
                        Save.FontSize = 15;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Class.ToString() + ". " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else//update
            {
                string res = "";
                try
                {
                    res += setpersonparameters(person);

                    res += setphoneparameters(phone, (int)INterviewID);

                    setinterviewparameters(interview);
                    
                    if (attachments.Count > attachesindex)
                    {
                        setfileattachments(attachments, attachesindex);
                    }

                    if (res.Contains("0") == false)
                    {
                        MessageBox.Show(ic_.UpdateInterview(interview, Code, (int)INterviewID) == "1" ? "عملیات موفقیت آمیز بود" : "خطایی رخ داده");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Class.ToString() + ". " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void interviewload(object sender, RoutedEventArgs e)//load interview from database
        {
            if (Function == "new")
            {
                code.Content += INterviewID.ToString();
                Save.Content = "ذخیره";
                InterviewItem.Content = "ذخیره";
            }
            else
            {
                nationalcode.IsReadOnly = true;
                Save.Content = "بروز رسانی";
                Save.FontSize = 15;
                InterviewItem.Content = "بروز رسانی";
                InterviewItem.FontSize = 15;

                #region Interview info
                try
                {
                    InterviewController ic = new InterviewController();
                    DataTable res = ic.ShowInterview(INterviewID);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(res);
                    code.Content += ds.Tables[0].Rows[0][0].ToString();
                    combo3.Text = ds.Tables[0].Rows[0][1].ToString();
                    position.Text = ds.Tables[0].Rows[0][2].ToString();
                    DateTime dt = DateTime.Parse(ds.Tables[0].Rows[0][3].ToString());
                    PersianCalendar pc = new PersianCalendar();
                    date.Text = pc.GetYear(dt).ToString() + "/" + pc.GetMonth(dt).ToString() + "/" + pc.GetDayOfMonth(dt).ToString();
                    fullname.Text = ds.Tables[0].Rows[0][10].ToString();
                    education.Text = ds.Tables[0].Rows[0][11].ToString();
                    nationalcode.Text = ds.Tables[0].Rows[0][12].ToString();
                    phonenumber.Text = ds.Tables[0].Rows[0][13].ToString();
                    if (ds.Tables[0].Rows[0][4].ToString() == "True")
                    {
                        trial.SetCurrentValue(CheckBox.IsCheckedProperty, true);
                        dt = new DateTime();
                        dt = DateTime.Parse(ds.Tables[0].Rows[0][5].ToString());
                        trainingdate.Text = pc.GetYear(dt).ToString() + "/" + pc.GetMonth(dt).ToString() + "/" + pc.GetDayOfMonth(dt).ToString();
                        result1.Text = ds.Tables[0].Rows[0][6].ToString();
                    }
                    if (ds.Tables[0].Rows[0][7].ToString() == "True")
                    {
                        educational.SetCurrentValue(CheckBox.IsCheckedProperty, true);
                        dt = new DateTime();
                        dt = DateTime.Parse(ds.Tables[0].Rows[0][8].ToString());
                        educationdate.Text = pc.GetYear(dt).ToString() + "/" + pc.GetMonth(dt).ToString() + "/" + pc.GetDayOfMonth(dt).ToString();
                        result2.Text = ds.Tables[0].Rows[0][9].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Class.ToString() + ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                #endregion
                
                #region Interview Files
                try
                {
                    AttachmentController ac = new AttachmentController();
                    DataTable res = ac.ShowAttachments(INterviewID, "Interview", "Resume");
                    if (res.Rows.Count != 0)
                    {
                        for (int i = 0; i < res.Rows.Count; i++)
                        {
                            Attachment attachment = new Attachment();
                            attachment.Type = "Interview";
                            attachment.ProccessSubType = "Resume";
                            attachment.Proccess = INterviewID;
                            attachment.Location = res.Rows[i][1].ToString();
                            attachment.Filename = res.Rows[i][0].ToString();
                            attachment.FileType = res.Rows[i][2].ToString();
                            attachment.Filebuffer = (byte[])res.Rows[i][3];
                            attachments.Add(attachment);
                        }
                    }
                    attachesindex = res.Rows.Count;
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

                #region interviewers
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select* from InterviewItem where interviewref = " + INterviewID.ToString();
                DataTable tabel = Database.Show(cmd);
                for (int i = 0; i < tabel.Rows.Count; i++)
                {
                    Models.InterviewItem interviewer = new InterviewItem();
                    interviewer.Name = tabel.Rows[i][1].ToString();
                    interviewer.Result = tabel.Rows[i][3].ToString();
                    interviewer.Comment = tabel.Rows[i][4].ToString();
                    interviewerstable.Items.Add(interviewer);
                }
                #endregion
                
            }
        }

        private void showattachments(object sender, MouseButtonEventArgs e)//show interview attachments
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
                        if (!Directory.Exists("D:/DMS/Rasa/Sorces/Interview/Resumes"))
                        {
                            Directory.CreateDirectory("D:/DMS/Rasa/Sorces/Interview/Resumes");
                        }
                        temp.Location = "D:/DMS/Rasa/Sorces/Interview/Resumes" + "/" + INterviewID.ToString() + attachments[i].FileType.ToString();
                        temp.Filebuffer = (byte[])attachments[i].Filebuffer;
                        var newfile = temp.Location.Replace(temp.FileType, DateTime.Now.DayOfYear + temp.FileType);
                        File.WriteAllBytes(newfile, temp.Filebuffer);
                        Process.Start(newfile);
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
        }

        private void Add(object sender, RoutedEventArgs e)//attach file's into the resume
        {
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
                        stream.Read(buffer, 0, buffer.Length);
                    }
                    FileInfo fileInfo = new FileInfo(file);
                    Attachment temp = new Attachment();
                    temp.Location = fileInfo.FullName;
                    temp.Filename = fileInfo.Name;
                    temp.FileType = fileInfo.Extension;
                    temp.Filebuffer = buffer;
                    temp.Type = "Interview";
                    temp.ProccessSubType = "Resume";
                    attachments.Add(temp);
                }
            }
        }

        private void SaveInterviewer(object sender, RoutedEventArgs e)//save interviewers
        {
            ic = new InterviewerCard();
            showusercontroller(ic, this.Height / 17, this.Width / 10);
            ic.Add += new EventHandler(setinterviewers);
        }
        
        private void SaveInterviewItem(object sender, RoutedEventArgs e)//save interview item
        {
            Interview interview = new Interview();
            InterviewController ic = new InterviewController();
            #region interviewers
            if (interviewers.Count > 0)
            {
                for (int i = 0; i < interviewers.Count; i++)
                {
                    InterviewItemController iwc = new InterviewItemController();
                    try
                    {
                        MessageBox.Show(iwc.SaveInterviewer(interviewers[i]) == "1" ? "ثبت شد" : "خطایی رخ داده");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Class.ToString() + ". " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            } 
            #endregion
            #region educational
            if (educational.IsChecked == true)
            {
                interview.I_Code = INterviewID;
                interview.Educational = true;
                interview.Educationdate = educationdate.SelectedDate.ToDateTime();
                interview.EducationResult = result2.Text;
                ic.SaveEducational(interview, Code);
            }
            #endregion
            #region training
            if (true)
            {
                interview.I_Code = INterviewID;
                interview.Training = true;
                interview.TrainingDate = trainingdate.SelectedDate.ToDateTime();
                interview.TrainingResult = result1.Text;
                ic.SaveTrial(interview, Code);
            } 
            #endregion
        }

        private void checkchange1(object sender, RoutedEventArgs e)
        {
            trainingdate.IsEnabled = true;
            result1.IsEnabled = true;
        }

        private void checkchange2(object sender, RoutedEventArgs e)
        {
            educationdate.IsEnabled = true;
            result2.IsEnabled = true;
        }

        private void layoutupdate(object sender, EventArgs e)
        {
            if (childstack.Children.Count > 0)
            {
                InterviewItem.IsEnabled = false;
            }
            else
            {
                InterviewItem.IsEnabled = true;
            }
        }

        #region Function's
        private void showusercontroller(UserControl userControl, double top, double left)//show user controller
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
        private void setinterviewers(object sender, EventArgs e)//add interviewers to the list
        {
            InterviewItem interviewer = new InterviewItem();
            interviewer.InterViewref = INterviewID;
            interviewer.Name = ic.Interviewer;
            interviewer.Result = ic.result;
            interviewer.Comment = ic.comment;
            interviewers.Add(interviewer);
            interviewerstable.Items.Add(interviewer);
        }
        private void setinterviewparameters(Interview temp)//Get Interview information by this function. used for save and update
        {
            temp.I_Code = Int64.Parse(code.Content.ToString());
            temp.RealName = fullname.Text;
            temp.Education = education.Text;
            temp.Position = position.Text;
            PersianDateTime dt = PersianDateTime.Parse(date.Text);
            temp.Date = date.SelectedDate.ToDateTime();
            temp.Introduction = combo3.Text + (description.Visibility == Visibility.Visible ? ":" : "") + descriptiontxt.Text;
        }
        private string setpersonparameters(Person temp)//Get Person information by this function. used for save and update
        {
            temp.RealName = fullname.Text;
            temp.Education = education.Text;
            temp.NationalNumber = nationalcode.Text;
            PersonController p_c = new PersonController();
            string res = p_c.SavePerson(temp);
            return res;
        }
        public string setphoneparameters(PhoneModel temp, int refrense)//Get Phone information by this function. used for save and update
        {
            PhoneController pc = new PhoneController();
            temp.Person_ref = refrense;
            temp.PhoneNumber = phonenumber.Text;
            temp.Title = "Interview";
            temp.Subtitle = "interview";
            return pc.SavePhone(temp);
        }
        public void setfileattachments(List<Attachment>lists, int index)//Get interview attachments
        {
            for (int i = index; i < lists.Count; i++)
            {
                AttachmentController ac = new AttachmentController();
                attachments[i].Proccess = INterviewID;
                ac.SaveAttachments(attachments[i]);
            }
        }
        private void loaddatagridview()
        {
            
        }
        #endregion
        
    }
}
