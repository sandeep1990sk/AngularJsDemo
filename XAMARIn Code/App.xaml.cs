
using Xamarin.Forms;
using myCIIEmployee;
using System;
using System.Net;

namespace myCIIEmployee
{
    public partial class App : Application
    {
        static TodoItemDatabase database;

        public static TodoItemManager TodoManager { get; set; }
        public static string EmployeeId { get; set; }
        public static string Name { get; set; }
        public static string Designation { get; set; }
        public static string photograph { get; set; }


        public static MasterDetailPage MasterDetailPage;

        public static void SetupRedirection(Page objPage)
        {
            if (objPage != null)
            {
                MasterDetailPage = new MasterDetailPage
                {
                    Master = new MenuPage(),
                    Detail = new NavigationPage(objPage),
                };
            }
        }

        void CreateSession(LogedInUser objLogedInUser)
        {
            App.EmployeeId = objLogedInUser.EmployeeId;
            App.photograph = objLogedInUser.Photograph;
            App.Designation = objLogedInUser.Designation;
            App.Name = objLogedInUser.Name;

        }

        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoItemDatabase();
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            TodoManager = new TodoItemManager(new RestService());

            if (Application.Current.Properties.Values.Contains("EmployeeId") && Convert.ToString(Application.Current.Properties["EmployeeId"]) != "")
            {
                App.SetupRedirection(new Views.RequisitionDashBoard());
                MainPage = MasterDetailPage;
            }
            else
            {
                LogedInUser objLogedInUser = App.Database.IsUserLogedIn();
                if (objLogedInUser != null)
                {
                    CreateSession(objLogedInUser);
                    Application.Current.Properties["EmployeeId"] = objLogedInUser.EmployeeId;
                    App.SetupRedirection(new Views.RequisitionDashBoard());
                    MainPage = MasterDetailPage;
                }
                else
                {
                    MainPage = new MainPage();
                    //Application.Current.Properties["EmployeeId"] = "E000000001";
                    //App.SetupRedirection(new Views.RequisitionDashBoard());
                    //MainPage = MasterDetailPage;
                }
            }

            //MainPage = new Views.DemoGridPage();

            //  bool result = chk_con();
            //if (Application.Current.Properties.Values.Contains("EmployeeId") && Convert.ToString(Application.Current.Properties["EmployeeId"]) != "")
            //{
            //    App.SetupRedirection(new Views.RequisitionDashBoard());
            //    MainPage = MasterDetailPage;
            //}
            //else
            //    MainPage = new MainPage();

            //MainPage = new Views.StaffList();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            LogedInUser objLogedInUser = App.Database.IsUserLogedIn();
            if (objLogedInUser != null)
            {
                CreateSession(objLogedInUser);
                Application.Current.Properties["EmployeeId"] = objLogedInUser.EmployeeId;
                //App.SetupRedirection(new PendingApproval());
                App.SetupRedirection(new Views.RequisitionDashBoard());
                MainPage = MasterDetailPage;
            }
            else
            {
                MainPage = new MainPage();
            }
            // Handle when your app resumes

        }

    }
}
