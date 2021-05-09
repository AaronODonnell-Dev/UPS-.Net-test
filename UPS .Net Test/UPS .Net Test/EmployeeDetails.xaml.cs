using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UPS.Net_Test
{
    /// <summary>
    /// Interaction logic for EmployeeDetails.xaml
    /// </summary>
    public partial class EmployeeDetails : Window
    {

        public MainWindow main = new MainWindow();
        public UserInfo Employees = new UserInfo();
        RESTClient client = new RESTClient();

        public List<UserInfo> tempEmployees = new List<UserInfo>();
        public EmployeeDetails()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(tbxName != null && tbxGender != null && tbxStatus != null && tbxEmail != null)
            {
                UserInfo newEmp = new UserInfo();

                foreach (var emp in tempEmployees)
                {
                    if(emp.name == tbxName.Text || emp.email == tbxEmail.Text)
                    {
                        MessageBox.Show("This Employee already exits");
                    }
                    else if(emp.status != "Active" || emp.status != "Inactive")
                    {
                        if(Regex.IsMatch(tbxEmail.Text, @"^[\sa-zA-z0-9.@]+$"))
                        {
                            newEmp.name = tbxName.Text;
                            newEmp.gender = tbxGender.Text;
                            newEmp.status = tbxStatus.Text;
                            newEmp.email = tbxEmail.Text;

                            this.Hide();
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid email, should contain only letters, numbers, @ and .");
                        }
                    }
                }

                if(newEmp != null)
                {
                    tempEmployees.Add(newEmp);
                    main.Employees = tempEmployees;
                    main.lbx_Employees.ItemsSource = main.Employees;
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (tbxName != null && tbxGender != null && tbxStatus != null && tbxEmail != null)
            {
                foreach (var emp in tempEmployees)
                {
                    if(emp.name == tbxName.Text)
                    {
                        if(Regex.IsMatch(tbxEmail.Text, @"^[\sa-zA-z0-9.@]+$"))
                        {
                            tempEmployees.Remove(emp);

                            emp.name = tbxName.Text;
                            emp.gender = tbxGender.Text;
                            emp.status = tbxStatus.Text;
                            emp.email = tbxEmail.Text;

                            tempEmployees.Add(emp);
                            main.Employees = tempEmployees;
                            main.lbx_Employees.ItemsSource = main.Employees;

                            this.Hide();
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid email, should contain only letters, numbers, @ and .");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee not on file, please click 'Add' button instead");
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (tbxName != null && tbxGender != null && tbxStatus != null && tbxEmail != null)
            {
                foreach (var emp in tempEmployees)
                {
                    if (emp.name == tbxName.Text)
                    {
                        tempEmployees.Remove(emp);
                        main.Employees = tempEmployees;
                        main.lbx_Employees.ItemsSource = main.Employees;

                        this.Hide();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Employee not on file, canot be removed");
                    }
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbxName.Text = "";
            tbxGender.Text = "";
            tbxStatus.Text = "";
            tbxEmail.Text = "";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            main.Show();
        }
    }
}
