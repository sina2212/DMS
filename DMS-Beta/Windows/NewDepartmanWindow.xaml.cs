
using DMS_Beta.Controllers;
using DMS_Beta.Models;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for NewDepartmanWindow.xaml
    /// </summary>
    public partial class NewDepartmanWindow : Window
    {
        string imagename = "";
        public NewDepartmanWindow()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Savedep(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Departmanvalue
                Departman departman = new Departman();
                departman.Name = depname.Text;
                departman.Code = int.Parse(depcode.Text);
                #endregion

                #region departman icon value
                byte[] file;
                using (Stream stream = File.OpenRead(imagename))
                {
                    file = new byte[stream.Length];
                    stream.Read(file, 0, file.Length);
                }
                Attachment icon = new Attachment();
                icon.Filename = new FileInfo(imagename).Name;
                icon.FileType = new FileInfo(imagename).Extension;
                icon.Filebuffer = file;
                icon.Location = new FileInfo(imagename).FullName;
                icon.Type = "Departman";
                icon.ProccessSubType = "icon";
                icon.Proccess = departman.Code;
                #endregion

                DepartmanController depcontroller = new DepartmanController();
                MessageBox.Show(depcontroller.Save(departman) == "1" ? "عملیات انجام شد" : "خطایی رخ داده");
                AttachmentController attController = new AttachmentController();
                attController.SaveAttachments(icon,0);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Class.ToString() + ":" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void choosefile(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                imagename = ofd.FileName;
                dep_cover.ImageSource = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }
}
