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

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for LicenseWindow.xaml
    /// </summary>
    public partial class LicenseWindow : Window
    {
        #region variables
        private Int64 id;
        private string function;
        List<Attachment> attachments = new List<Attachment>();
        public string Function
        {
            get { return function; }
            set { function = value; }
        }

        public Int64 ID_
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        public LicenseWindow(Int64 id, string f)
        {
            InitializeComponent();
            ID_ = id;
            Function = f;
        }

        private void Save_Click(object sender, RoutedEventArgs e)//save or update license
        {
            try
            {
                License license = new License();
                LicenseController controller = new LicenseController();
                AttachmentController attachmentController = new AttachmentController();

                license.LicenseNumber = Int64.Parse(code.Text);
                license.LicenseName = name.Text;
                license.Issuer = issuer.Text;
                license.Type = type.IsChecked == true ? true : false;
                license.Date = date.SelectedDate.ToDateTime();
                checkchange(license);
                if (Save.Content.ToString() == "ذخیره")
                {
                    for (int i = 0; i < attachments.Count; i++)
                    {
                        attachmentController.SaveAttachments(attachments[i]);
                    }
                    MessageBox.Show(controller.SaveLicense(license) == "1" ? "عملیات موفقیت آمیز بود" : "گواهی موجود هست");
                }
                else
                {
                    for (int i = 0; i < attachments.Count; i++)
                    {
                        attachmentController.SaveAttachments(attachments[i]);
                    }
                    license.ID = ID_;
                    MessageBox.Show(controller.UpdateLicense(license) == "1" ? "عملیات موفقیت آمیز بود" : "خطایی رخ داده");
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

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add(object sender, RoutedEventArgs e)//add attachment's to the license
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == true)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    try
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
                        temp.Type = "License";
                        temp.ProccessSubType = name.Text;
                        temp.Proccess = Int64.Parse(code.Text);
                        attachlbl.ToolTip += temp.Filename + ",";
                        attachlbl.Text = "مشاهده";
                        attachments.Add(temp);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void checkchange(License temp)
        {
            if (type.IsChecked == true)
            {
                temp.ExpireDate = exp_date.SelectedDate.ToDateTime();
            }
            else
            {
                temp.ExpireDate = DateTime.Now.AddYears(1);
            }
        }

        private void licenseload(object sender, RoutedEventArgs e)//load license from database
        {
            if (Function != "new")
            {
                Save.Content = "بروز رسانی";
                Save.FontSize = 15;
                attachlbl.Text = "مشاهده";

                #region license info
                try
                {
                    License license = new License();
                    LicenseController controller = new LicenseController();
                    license = controller.ShowLicense(ID_);
                    code.Text = license.LicenseNumber.ToString();
                    name.Text = license.LicenseName;
                    issuer.Text = license.Issuer;
                    PersianCalendar pc = new PersianCalendar();
                    date.Text = pc.GetYear(license.Date).ToString() + "/" + pc.GetMonth(license.Date).ToString() + "/" + pc.GetDayOfMonth(license.Date).ToString();
                    exp_date.Text = pc.GetYear(license.ExpireDate).ToString() + "/" + pc.GetMonth(license.ExpireDate).ToString() + "/" + pc.GetDayOfMonth(license.ExpireDate).ToString();
                    type.IsChecked = license.Type;
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

                #region license files
                try
                {
                    AttachmentController ac = new AttachmentController();
                    DataTable res = ac.ShowAttachments(Int64.Parse(code.Text), "License", name.Text);
                    if (res.Rows.Count != 0)
                    {
                        for (int i = 0; i < res.Rows.Count; i++)
                        {
                            Attachment attachment = new Attachment();
                            attachment.Type = "License";
                            attachment.ProccessSubType = name.Text;
                            attachment.Proccess = Int64.Parse(code.Text);
                            attachment.Location = res.Rows[i][1].ToString();
                            attachment.Filename = res.Rows[i][0].ToString();
                            attachment.FileType = res.Rows[i][2].ToString();
                            attachment.Filebuffer = (byte[])res.Rows[i][3];
                            attachments.Add(attachment);
                        }
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
                #endregion
            }
        }

        private void show(object sender, System.Windows.Input.MouseButtonEventArgs e)//show license attachment's
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
                        if (!Directory.Exists("D:/DMS/Rasa/Sorces/License"))
                        {
                            Directory.CreateDirectory("D:/DMS/Rasa/Sorces/License");
                        }
                        temp.Location = "D:/DMS/Rasa/Sorces/License" + "/" + attachments.ToString() + attachments[i].FileType.ToString();
                        temp.Filebuffer = attachments[i].Filebuffer;
                        var newfile = temp.Location.Replace(temp.FileType, DateTime.Now.DayOfYear + temp.FileType);
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

        private void type_Checked(object sender, RoutedEventArgs e)
        {
            border.Visibility = Visibility.Visible;
        }

        private void type_Unchecked(object sender, RoutedEventArgs e)
        {
            border.Visibility = Visibility.Hidden;
        }
    }
}
