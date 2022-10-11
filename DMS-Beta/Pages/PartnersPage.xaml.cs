using DMS_Beta.Models;
using DMS_Beta.Styles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Pages
{
    /// <summary>
    /// Interaction logic for PartnersPage.xaml
    /// </summary>
    public partial class PartnersPage : Page
    {
        public PartnersPage()
        {
            InitializeComponent();
        }
        public void PartnersPage_Load(object sender ,EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ShowPartners";
            DataTable dt = Database.Show(cmd);
            List<StackPanel> lp = new List<StackPanel>();
            for (int i = 0; i < (int)(dt.Rows.Count * 160/this.WindowWidth)+1; i++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Margin = new Thickness(0, 0, 0, 0);
                sp.Height = 260;
                sp.Width = this.Width;
                sp.Width = this.Width;
                for (int j = i * ((int)this.WindowWidth/160); j < (int)(this.WindowWidth/160)*(i+1); j++)
                {
                    if (j<dt.Rows.Count)
                    {
                        #region Employee information
                        Employee item = new Employee();
                        item.RealName = dt.Rows[j][0].ToString();
                        item.DepName = dt.Rows[j][1].ToString();
                        item.EmpCode = int.Parse(dt.Rows[j][2].ToString());
                        #endregion

                        #region employee cover image
                        Attachment attachment = new Attachment();
                        attachment.Filebuffer = (byte[])dt.Rows[j][5];
                        item.Avatar = attachment;
                        #endregion

                        PartnersCard pc = new PartnersCard(item);
                        pc.Margin = new Thickness(5, 5, 0, 0);
                        sp.Children.Add(pc);
                    }
                }
                lp.Add(sp);
            }
            foreach (var item in lp)
            {
                listview.Children.Add(item);
            }
        }
    }
}
