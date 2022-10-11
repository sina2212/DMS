using DMS_Beta.Controllers;
using DMS_Beta.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DMS_Beta.Styles
{
    /// <summary>
    /// Interaction logic for ChooseEmployee.xaml
    /// </summary>
    public partial class ChooseEmployee : UserControl
    {
        #region variables
        private int depcode;
        public int DepCode
        {
            get { return depcode; }
            set { depcode = value; }
        }

        List<ChoosingEmployee> ces = new List<ChoosingEmployee>();
        public List<ChoosingEmployee> ces2 { get; set; }
        public event EventHandler AddList;
        #endregion

        public ChooseEmployee(int index)
        {
            InitializeComponent();
            DepCode = index;
        }

        private void Close_(object sender, RoutedEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void ChooseEmployeeLoad(object sender, RoutedEventArgs e)
        {
            DepartmanController controller = new DepartmanController();
            List<Employee> notEmployed = new List<Employee>();
            notEmployed = controller.ShowDepartmannotEmployee(DepCode);
            addtocontroller(notEmployed);
        }

        private void ChooseEmployes(object sender, RoutedEventArgs e)
        {
            AddList(sender, e);
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        #region function
        private void addtocontroller(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                ChoosingEmployee ce = new ChoosingEmployee(employees[i].RealName, employees[i].EmpCode);
                ce.Margin = new Thickness(0);
                childstack.Children.Add(ce);
                ce.Add += new EventHandler(chooseemployes);
                ce.Remove += new EventHandler(chooseemployes);
                ces.Add(ce);
            }
        }
        private void chooseemployes(object sender, EventArgs e)
        {
            ces2 = new List<ChoosingEmployee>();
            for (int k = 0; k < ces.Count; k++)
            {
                if (ces[k].alreadyadded == true)
                {
                    ces2.Add(ces[k]);
                }
            }
        }
        #endregion
    }
}
