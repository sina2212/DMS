using DMS_Beta.Models;
using DMS_Beta.Styles;
using DMS_Beta.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for DepartmansPage.xaml
    /// </summary>
    public partial class DepartmansPage : Page
    {
        public DepartmansPage()
        {
            InitializeComponent();
        }

        private void Departmans_load(object sender, RoutedEventArgs e)
        {
            List<StackPanel> lp = new List<StackPanel>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select name,code,filebuffer,filelocation,filename,filetype from Departmans " +
                "left outer join Attachments on category = 'Departman' and categoryid = code";
            DataTable dt = Database.Show(cmd);
            for (int i = 0; i < (int)(dt.Rows.Count * 230 / this.WindowWidth) + 1; i++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Margin = new Thickness(0, 0, 0, 0);
                sp.Height = 280;
                sp.Width = this.Width;
                for (int j = i * ((int)this.WindowWidth / 250); j < (int)(this.WindowWidth / 250) * (i + 1); j++)
                {
                    if (j<dt.Rows.Count)
                    {
                        Departman item = new Departman();
                        item.Name = dt.Rows[j][0].ToString();
                        item.Code = int.Parse(dt.Rows[j][1].ToString());
                        Attachment attachment = new Attachment();
                        attachment.Filebuffer = (byte[])dt.Rows[j][2];
                        attachment.Filename = dt.Rows[j][4].ToString();
                        attachment.FileType = dt.Rows[j][5].ToString();
                        DepartmanCard card = new DepartmanCard(item, attachment);
                        card.Margin = new Thickness(10, 5, 0, 0);
                        sp.Children.Add(card);
                    }                }
                lp.Add(sp);
            }
            foreach (var item in lp)
            {
                listview.Children.Add(item);
            }
        }

        private void AddDepartman_(object sender, RoutedEventArgs e)
        {
            NewDepartmanWindow newDepartmanWindow = new NewDepartmanWindow();
            newDepartmanWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newDepartmanWindow.ShowDialog();
        }
    }
}
