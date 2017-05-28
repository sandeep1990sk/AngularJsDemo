using myCIIEmployee.Models;
using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace myCIIEmployee.Views
{
    public partial class StaffList : ContentPage
    {
        public StaffList()
        {
            InitializeComponent();
            Title = "CII Staff Directory";
        }

        protected async override void OnAppearing()
        {
            overlay.IsVisible = true;
            base.OnAppearing();
            listEmployee.ItemsSource = GetAllStaff();
            overlay.IsVisible = false;
        }

        IEnumerable<Employee> GetAllStaff()
        {

            Employee objEmployee = new Employee();
            List<Employee> lstEmployee = new List<Employee>();

            for (int indx = 0; indx < 10; indx++)
            {
                objEmployee = new Employee();
                objEmployee.EmailId = "EmailId_" + indx;
                objEmployee.EmployeeId = "EmployeeId_" + indx;
                objEmployee.ImageURL = "ImageURL_" + indx;
                objEmployee.Mobile = "Mobile_" + indx;
                objEmployee.Name = "Name_" + indx;

                lstEmployee.Add(objEmployee);
            }

            return lstEmployee;
        }

        private void listEmployee_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {



        }

        private void TapGestureRecognizer_Call(object sender, EventArgs e)
        {
            dynamic obj = e;

            DisplayAlert("Clicked", Convert.ToString("Call : " + obj.Parameter), "Ok");


        }

        private void TapGestureRecognizer_Message(object sender, EventArgs e)
        {
            dynamic obj = e;

            DisplayAlert("Clicked", Convert.ToString("Message : " + obj.Parameter), "Ok");


        }

    }
}
