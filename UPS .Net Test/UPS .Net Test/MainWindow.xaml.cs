using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UPS.Net_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RESTClient client = new RESTClient();
        public List<UserInfo> Employees = new List<UserInfo>();
        public MainWindow()
        {
            InitializeComponent();

            Employees = client.GetInfo();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbx_Employees.ItemsSource = Employees;
            tbxSearch.Text = "";
            tbxName.Text = "";
            tbxGender.Text = "";
            tbxStatus.Text = "";
            tbxEmail.Text = "";

        }

        private void lbx_Employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserInfo Emp = lbx_Employees.SelectedItem as UserInfo;

            if(Emp != null)
            {
                tbxName.Text = Emp.name;
                tbxGender.Text = Emp.gender;
                tbxStatus.Text = Emp.status;
                tbxEmail.Text = Emp.email;
            }
            else
            {
                tbxName.Text = "";
                tbxGender.Text = "";
                tbxStatus.Text = "";
                tbxEmail.Text = "";
            }
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<UserInfo> filteredList = new List<UserInfo>();

            string txtinput = tbxSearch.Text;
            string upper = txtinput.ToUpper();
            string lower = txtinput.ToLower();

            foreach (var user in Employees)
            {
                if(user.name.StartsWith(lower) || user.name.StartsWith(upper) || user.name.StartsWith(txtinput))
                {
                    filteredList.Add(user);
                }
            }

            lbx_Employees.ItemsSource = filteredList;
        }

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            client.PostInfo(Employees);
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            employeeDetails.Show();

            employeeDetails.tbxName.Text = tbxName.Text;
            employeeDetails.tbxGender.Text = tbxGender.Text;
            employeeDetails.tbxStatus.Text = tbxStatus.Text;
            employeeDetails.tbxEmail.Text = tbxEmail.Text;

            employeeDetails.tempEmployees = Employees;

            this.Hide();
        }
    }
}
