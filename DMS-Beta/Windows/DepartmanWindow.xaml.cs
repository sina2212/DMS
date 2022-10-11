using DMS_Beta.Controllers;
using DMS_Beta.Models;
using DMS_Beta.Styles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DMS_Beta.Windows
{
    /// <summary>
    /// Interaction logic for DepartmanWindow.xaml
    /// </summary>
    public partial class DepartmanWindow : Window
    {
        #region Variables
        private int code;
        private string function;
        public string Function
        {
            get { return function; }
            set { function = value; }
        }
        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        ChooseEmployee ce;
        #endregion

        public DepartmanWindow(int depcode, string f)
        {
            InitializeComponent();
            Code = depcode;
            Function = f;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Function == "load")
            {
                var message = (tabControl.SelectedItem as TabItem).Header.ToString();
                DepartmanController controller = new DepartmanController();

                if (message == "مشخصات واحد")
                {
                    List<Employee> dt = new List<Employee>();
                    dt = controller.ShowDepartmanEmployee(Code);
                    listview.Children.Clear();
                    List<StackPanel> lp = new List<StackPanel>();
                    for (int i = 0; i < (int)(dt.Count * 110 / this.Width) + 1; i++)
                    {
                        StackPanel sp = new StackPanel();
                        sp.Orientation = Orientation.Horizontal;
                        sp.Margin = new Thickness(0, 0, 0, 0);
                        sp.Height = 110;
                        sp.Width = this.Width;
                        for (int j = i * ((int)this.Width / 110); j < (int)(this.Width / 110) * (i + 1); j++)
                        {
                            if (j < dt.Count)
                            {
                                if (dt[j].JobPosition == "مسئول")
                                {
                                    unitheader.Text = dt[j].RealName;
                                }
                                else
                                {
                                    Attachment attachment = new Attachment();
                                    attachment.Filebuffer = (byte[])dt[j].Avatar.Filebuffer;
                                    EmpCard pc = new EmpCard(dt[j].RealName, attachment);
                                    pc.Margin = new Thickness(20, 5, 0, 0);
                                    sp.Children.Add(pc);
                                }
                            }
                        }
                        lp.Add(sp);
                    }
                    foreach (var item in lp)
                    {
                        listview.Children.Add(item);
                    }
                }
                else
                {
                    DataTable table = new DataTable();
                    table = controller.showdepartmanAttachments(Code);
                    PFdata.ItemsSource = table.DefaultView;
                }
            }
        }

        private void ChoseEmployee(object sender, MouseButtonEventArgs e)
        {
            ce = new ChooseEmployee(Code);
            showusercontroller(ce, 70, 120);
            ce.AddList += new EventHandler(getchoosenemp);
        }

        private void SaveDepartman(object sender, RoutedEventArgs e)
        {
            string str = "";
            DepartmanController controller = new DepartmanController();
            if (ce == null || ce.ces2 == null || ce.ces2.Count == 0)
            {
                try
                {
                    str = controller.ToEmploy(0, Code, unitheader.Text);
                }
                catch (SqlException ex)
                {
                    str = ex.Class.ToString() + ": " + ex.Message;
                }
                catch (Exception ex)
                {
                    str = ex.Message;
                }
                if (str != "2")
                {
                    MessageBox.Show("کارمندان بخش بروز شدن" + "\n" + "مسئول واحدی با این نام پیدا نشد");
                }
                else if (str == "2")
                {
                    MessageBox.Show("عملیات با موفقیت انجام شد");
                }
                else
                {
                    MessageBox.Show(str);
                }
            }
            else if (unitheader.Text == null || unitheader.Text ==" ")
            {
                MessageBox.Show("فیلد خالی زل پر کنین");
            }
            else
            {
                for (int i = 0; i < ce.ces2.Count; i++)
                {
                    try
                    {
                        str = controller.ToEmploy(ce.ces2[i].empcode, Code, unitheader.Text);
                    }
                    catch (SqlException ex)
                    {
                        str = ex.Class.ToString() + ": " + ex.Message;
                    }
                    catch (Exception ex)
                    {
                        str = ex.Message;
                    }
                }
                if (str != "2")
                {
                    MessageBox.Show("کارمندان بخش بروز شدن" + "\n" + "مسئول واحدی با این نام پیدا نشد");
                }
                else if(str == "2")
                {
                    MessageBox.Show("عملیات با موفقیت انجام شد");
                }
                else
                {
                    MessageBox.Show(str);
                }
            }
        }

        #region Function
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
        public void getchoosenemp(object sender, EventArgs e)
        {
            List<StackPanel> lsp = new List<StackPanel>();
            for (int i = 0; i < (int)(ce.ces2.Count * 110 / this.Width) + 1; i++)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Margin = new Thickness(0, 0, 0, 0);
                sp.Height = 110;
                sp.Width = this.Width;
                for (int j = i * ((int)this.Width / 110); j < (int)(this.Width / 110) * (i + 1); j++)
                {
                    if (j < ce.ces2.Count)
                    {
                        AttachmentController attachmentController = new AttachmentController();
                        DataTable dt = new DataTable();
                        dt = attachmentController.ShowAttachments(ce.ces2[j].empcode, "Employee", "cover");
                        Attachment attachment = new Attachment();
                        attachment.Filebuffer = (byte[])dt.Rows[0][3];
                        EmpCard empCard = new EmpCard(ce.ces2[j].empname, attachment);
                        empCard.Margin = new Thickness(20, 5, 0, 0);
                        sp.Children.Add(empCard);
                    }
                }
                lsp.Add(sp);
            }
            for (int i = 0; i < lsp.Count; i++)
            {
                listview.Children.Add(lsp[i]);
            }
        }
        #endregion
    }
}
