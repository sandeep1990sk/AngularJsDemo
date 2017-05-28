using myCIIEmployee.Models;
using System;
using System.Web;
using Windows.System.Profile;
using Xamarin.Forms;

namespace myCIIEmployee
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        void CreateSession(LoginResult objLoginResult)
        {
            LogedInUser objLogedInUser = new LogedInUser();
            objLogedInUser.EmployeeId = objLoginResult.EmpId;
            objLogedInUser.Photograph = objLoginResult.photograph;
            objLogedInUser.Designation = objLoginResult.designation;
            objLogedInUser.Name = objLoginResult.Name;
            App.Database.SaveItem(objLogedInUser);
            App.EmployeeId = objLoginResult.EmpId;
            App.photograph = objLoginResult.photograph;
            App.Designation = objLoginResult.designation;
            App.Name = objLoginResult.Name;

        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                overlay.IsVisible = true;

                LoginResult objLoginResult = new LoginResult();
                objLoginResult.username = EmailId.Text;
                objLoginResult.Password = Password.Text;
                objLoginResult = await App.TodoManager.ValidateUserEAM(objLoginResult);
                if (objLoginResult.Message != null && Convert.ToString(objLoginResult.Message).ToLower() == "success")
                {
                    CreateSession(objLoginResult);

                    Application.Current.Properties["EmployeeId"] = objLoginResult.EmpId;
                    await Application.Current.SavePropertiesAsync();
                    App.EmployeeId = objLoginResult.EmpId;
                    App.photograph = objLoginResult.photograph;
                    App.Designation = objLoginResult.designation;
                    App.Name = objLoginResult.Name;
                    //App.SetupRedirection(new  PendingApproval());
                    App.SetupRedirection(new Views.RequisitionDashBoard());

                    App.Current.MainPage = App.MasterDetailPage;
                }
                else
                    await DisplayAlert("Alert", "Username and password does not match.", "", "Re-Try");
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alert", ex.ToString(), "", "Re-Try");
            }
            finally
            {
                overlay.IsVisible = false;

            }

        }
    }
}
