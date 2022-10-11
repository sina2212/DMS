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
using System.Windows.Input;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for LetterWindow.xaml
    /// </summary>
    public partial class LetterWindow : Window
    {
        #region Variables
        List<Attachment> letters = new List<Attachment>();
        private Int64 letternumber;
        private string function;

        public string Function
        {
            get { return function; }
            set { function = value; }
        }
        public Int64 LetterNumber
        {
            get { return letternumber; }
            set { letternumber = value; }
        }
        #endregion

        public LetterWindow(Int64 number, string f)
        {
            InitializeComponent();
            LetterNumber = number;
            Function = f;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Letter letter = new Letter();
            LetterController letterController = new LetterController();

            try
            {
                letter.LetterNumber = Int64.Parse(code.Text);
                letter.Subject = subject.Text;
                letter.Date = date.SelectedDate.ToDateTime();
                letter.LetterType = combo2.Text == "ارسالی" ? true : false;
                letter.SendOrRecieveType = typeofrecivesend.Text;
                letter.OrganName = organization.Text;
                letter.RecieverorSender = senderreciever.Text;
                letter.Answer = 0;
                foreach (Attachment item in letters)
                {
                    AttachmentController ac = new AttachmentController();
                    item.Proccess = Int64.Parse(code.Text);
                    ac.SaveAttachments(item);
                }
                if (Save.Content.ToString() == "ذخیره")//save
                {
                    MessageBox.Show(letterController.SaveLetter(letter) == "1" ? "عملیات با موفقیت انجام شد" : "نامه جواب پیدا نشد");
                }
                else//update
                {
                    MessageBox.Show(letterController.UpdateLetter(letter) == "1" ? "عملیات با موفقیت انجام شد" : "خطایی رخ داده");
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

        private void attachmentshow(object sender, MouseButtonEventArgs e)
        {
            if (letters.Count == 0)
            {
                MessageBox.Show("فایلی پیدا نشد");
            }
            else
            {
                try
                {
                    for (int i = 0; i < letters.Count; i++)
                    {
                        Attachment temp = new Attachment();
                        temp.Filename = letters[i].Filename;
                        temp.FileType = letters[i].FileType;
                        if (!Directory.Exists("D:/DMS/Rasa/Sorces/Letters"))
                        {
                            Directory.CreateDirectory("D:/DMS/Rasa/Sorces/Letters");
                        }
                        temp.Location = "D:/DMS/Rasa/Sorces/Letters" + "/" + LetterNumber.ToString() + letters[i].FileType.ToString();
                        temp.Filebuffer = letters[i].Filebuffer;
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

        private void Add(object sender, RoutedEventArgs e)
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
                    temp.Type = "Letter";
                    temp.ProccessSubType = "letter";
                    letters.Add(temp);
                    attachlbl.ToolTip += temp.Filename + ",";
                    attachlbl.Text += "مشاهده";
                }
            }
        }

        private void letterload(object sender, RoutedEventArgs e)
        {
            if (Function == "new")
            {
                code.Text = LetterNumber.ToString();
            }
            else
            {
                try
                {
                    Save.Content = "بروز رسانی";
                    Save.FontSize = 15;
                    attachlbl.Text = "مشاهده";

                    #region letters info
                    LetterController controller = new LetterController();
                    Letter result = controller.ShowLetter(LetterNumber);
                    code.Text = result.LetterNumber.ToString();
                    subject.Text = result.Subject;
                    typeofrecivesend.Text = result.SendOrRecieveType;
                    combo2.SelectedIndex = result.LetterType == true ? 0 : 1;
                    senderreciever.Text = result.RecieverorSender;
                    organization.Text = result.OrganName;
                    PersianCalendar pc = new PersianCalendar();
                    date.Text = pc.GetYear(result.Date).ToString() + "/" + pc.GetMonth(result.Date).ToString() + "/" + pc.GetDayOfMonth(result.Date).ToString();
                    #endregion

                    #region attached files
                    AttachmentController ac = new AttachmentController();
                    DataTable res = ac.ShowAttachments(Int64.Parse(code.Text), "Letter", "letter");
                    if (res.Rows.Count != 0)
                    {
                        for (int i = 0; i < res.Rows.Count; i++)
                        {
                            Attachment attachment = new Attachment();
                            attachment.Type = "Letter";
                            attachment.ProccessSubType = "letter";
                            attachment.Proccess = Int64.Parse(code.Text);
                            attachment.Location = res.Rows[i][1].ToString();
                            attachment.Filename = res.Rows[i][0].ToString();
                            attachment.FileType = res.Rows[i][2].ToString();
                            attachment.Filebuffer = (byte[])res.Rows[i][3];
                            letters.Add(attachment);
                        }
                    }
                    #endregion
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

        private void changetype(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (combo2.SelectedIndex == 0)
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(organization, "سازمان گیرنده");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(typeofrecivesend, "نحوه ارسال");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(senderreciever, "نام گیرنده");
                date.ToolTip = "تاریخ ارسال";
            }
            else
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(organization, "سازمان فرستنده");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(typeofrecivesend, "نحوه دریافت");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(senderreciever, "نام فرستنده");
                date.ToolTip = "تاریخ درسافت";
            }
        }
    }
}
