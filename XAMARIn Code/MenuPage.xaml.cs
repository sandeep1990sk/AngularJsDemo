using ImageCircle.Forms.Plugin.Abstractions;
using System;

using Xamarin.Forms;

namespace myCIIEmployee
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            ScrollView scroll = new ScrollView();
            Content = scroll;
            StackLayout stackGray = new StackLayout() { BackgroundColor = Color.FromHex("#09f") };
            stackGray.Orientation = StackOrientation.Vertical;
            stackGray.HorizontalOptions = LayoutOptions.FillAndExpand;
            stackGray.HeightRequest = 170;
            Image imgBack = new Image { Source = "Pattern.jpg", Aspect = Aspect.Fill, HeightRequest = 170, WidthRequest = 400 };

            AbsoluteLayout absMain = new AbsoluteLayout { };
            absMain.Children.Add(imgBack);
            AbsoluteLayout absInner = new AbsoluteLayout
            {

            };

            StackLayout stackInner = new StackLayout() { };
            stackGray.Orientation = StackOrientation.Vertical;
            stackGray.HorizontalOptions = LayoutOptions.FillAndExpand;
            CircleImage imgEmpPic = new CircleImage()
            {
                Source = "http://mycii.in/" + App.photograph,
                Margin = new Thickness(20, 20, 0, 0),
                WidthRequest = 80,
                HeightRequest = 80,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BorderColor = Color.FromHex("#FFF"),
                BorderThickness = 1,
                Aspect = Aspect.AspectFit

            };
            Label lblName = new Label() { Text = App.Name, FontSize = 18, TextColor = Color.FromHex("#FFF"), Margin = new Thickness(20, 0, 0, 0), HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.Center };
            Label lblDesignation = new Label() { Text = App.Designation, FontSize = 16, TextColor = Color.FromHex("#FFF"), Margin = new Thickness(20, 0, 0, 0), HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.Center };
            stackInner.Children.Add(imgEmpPic);
            stackInner.Children.Add(lblName);
            stackInner.Children.Add(lblDesignation);
            absInner.Children.Add(stackInner);
            absMain.Children.Add(absInner);
            stackGray.Children.Add(absMain);
            StackLayout stackMain = new StackLayout() { BackgroundColor = Color.FromHex("#FFF"), Margin = new Thickness(20, 10, 0, 0) };
            stackMain.Orientation = StackOrientation.Vertical;
            stackMain.HorizontalOptions = LayoutOptions.StartAndExpand;
            stackMain.VerticalOptions = LayoutOptions.StartAndExpand;

            //#region "Home Dashboard"

            //var gestureRecognizerHomeDashboard = new TapGestureRecognizer();
            //gestureRecognizerHomeDashboard.Tapped += (s, e) =>
            //{
            //    App.SetupRedirection(new PendingApproval());
            //    App.Current.MainPage = App.MasterDetailPage;
            //};
            //StackLayout stackMainReq = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Margin = 0, Padding = 0, Spacing = 0 };
            //StackLayout stackHD = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 0, Padding = 0, Spacing = 0 };
            //stackMainReq.GestureRecognizers.Add(gestureRecognizerHomeDashboard);
            //stackHD.GestureRecognizers.Add(gestureRecognizerHomeDashboard);
            //Label lblHD = new Label() { FontSize = 16, Text = "Home", HorizontalOptions = LayoutOptions.Start, TextColor = Color.Black, BackgroundColor = Color.FromHex("#FFF"), Margin = new Thickness(10, 6, 0, 0) };
            //Image imgHD = new Image()
            //{
            //    HorizontalOptions = LayoutOptions.Start,
            //    Source = "home.png"
            //};
            //stackHD.Children.Add(imgHD);
            //stackHD.Children.Add(lblHD);
            //stackMain.Children.Add(stackHD);
            //stackMain.Children.Add(stackMainReq);
            //StackLayout stackBorderHD = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 500, Margin = 0, Padding = 0, Spacing = 0 };
            //stackMain.Children.Add(stackBorderHD);

            //#endregion

            #region "Requistion"

            var gestureRecognizerHome = new TapGestureRecognizer();
            gestureRecognizerHome.Tapped += (s, e) =>
                {
                    App.SetupRedirection(new Views.RequisitionDashBoard());
                    App.Current.MainPage = App.MasterDetailPage;
                };

            StackLayout stackMainHomeDB = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Margin = 0, Padding = 0, Spacing = 0 };
            StackLayout stack1 = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 0, Padding = 0, Spacing = 0 };
            stackMainHomeDB.GestureRecognizers.Add(gestureRecognizerHome);
            stack1.GestureRecognizers.Add(gestureRecognizerHome);
            Label lblHome = new Label() { FontSize = 16, Text = "Requisition", HorizontalOptions = LayoutOptions.Start, TextColor = Color.Black, BackgroundColor = Color.FromHex("#FFF"), Margin = new Thickness(10, 6, 0, 0) };
            Image imgHome = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "Req.png"
            };
            stack1.Children.Add(imgHome);
            stack1.Children.Add(lblHome);
            stackMain.Children.Add(stack1);
            stackMain.Children.Add(stackMainHomeDB);
            StackLayout stackBorderReq = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 500, Margin = 0, Padding = 0, Spacing = 0 };
            stackMain.Children.Add(stackBorderReq);

            #endregion

            #region "MY HRIS"
            StackLayout stackMyHRIS = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Margin = 0, Padding = 0, Spacing = 0 };
            StackLayout stackHRIS = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Horizontal, Margin = 0, Padding = 0, Spacing = 0 };
            Label lblMyHRIS = new Label() { FontSize = 16, Text = "My HRIS", HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.Black, Margin = new Thickness(10, 6, 0, 0) };
            Image imgHRiS = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "MyHR.png"
            };

            Image imgHRiSplus = new Image()
            {
                HorizontalOptions = LayoutOptions.End,
                Source = "open.png",
                Margin = new Thickness(0, 0, 20, 0)
            };
            stackHRIS.Children.Add(imgHRiS);
            stackHRIS.Children.Add(lblMyHRIS);
            stackHRIS.Children.Add(imgHRiSplus);
            stackMain.Children.Add(stackHRIS);
            stackMain.Children.Add(stackMyHRIS);

            StackLayout stackBorderHRIS = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 500, Margin = 0, Padding = 0, Spacing = 0 };
            stackMain.Children.Add(stackBorderHRIS);
            #endregion

            #region "Sub Menu of HRIS"

            StackLayout SMyHris = new StackLayout() { Margin = new Thickness(40, 0, 0, 0), Padding = 0, Spacing = 0 };
            SMyHris.IsVisible = false;
            SMyHris.Orientation = StackOrientation.Vertical;

            #region "Form View of Leave Approval"
            var gestureRecognizerLAform = new TapGestureRecognizer();
            gestureRecognizerLAform.Tapped += (s, e) =>
                    {
                        App.SetupRedirection(new LeaveApproval.LeaveApplicationForm("", 0, "Self", "", ""));//"Self", 0, "", "", ""));
                        App.Current.MainPage = App.MasterDetailPage;
                    };
            StackLayout stackBorderLeaveApp1 = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 100, Margin = new Thickness(-40, 5, 20, 5), Padding = 0, Spacing = 0 };
            SMyHris.Children.Add(stackBorderLeaveApp1);
            StackLayout stackLev = new StackLayout() { Orientation = StackOrientation.Horizontal };
            stackLev.GestureRecognizers.Add(gestureRecognizerLAform);

            Image imgLeave = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "LeaveApplication.png"
            };
            stackLev.Children.Add(imgLeave);

            Label lblLAform = new Label() { FontSize = 16, Text = "Leave Application", HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.Black, Margin = new Thickness(10, 0, 0, 0), BackgroundColor = Color.FromHex("#FFF") };

            stackLev.Children.Add(lblLAform);
            stackMain.Children.Add(stackLev);
            SMyHris.Children.Add(stackLev);
            StackLayout stackBorderLeaveApp = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 100, Margin = new Thickness(-40, 5, 20, 5), Padding = 0, Spacing = 0 };
            SMyHris.Children.Add(stackBorderLeaveApp);

            #endregion

            #region "Pending Applications"
            var gestureRecognizerStaff = new TapGestureRecognizer();
            gestureRecognizerStaff.Tapped += (s, e) =>
                        {
                            App.SetupRedirection(new LeaveApproval.PendingApprovalList("Applied", "hris"));
                            App.Current.MainPage = App.MasterDetailPage;
                        };


            StackLayout stackPendingAppl = new StackLayout() { Orientation = StackOrientation.Horizontal };
            stackPendingAppl.GestureRecognizers.Add(gestureRecognizerStaff);

            Image imgPendingApplications = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "PendingLeave.png"
            };
            stackPendingAppl.Children.Add(imgPendingApplications);

            Label lblStaff = new Label() { FontSize = 16, Text = "Pending Application", HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.Black, Margin = new Thickness(10, 6, 0, 0), BackgroundColor = Color.FromHex("#FFF") };

            stackPendingAppl.Children.Add(lblStaff);
            stackMain.Children.Add(stackPendingAppl);

            SMyHris.Children.Add(stackPendingAppl);
            StackLayout stackBorderPending = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 100, Margin = new Thickness(-40, 5, 20, 5), Padding = 0, Spacing = 0 };
            SMyHris.Children.Add(stackBorderPending);
            #endregion

            #region "Approved Leave Application"
            var gestureRecognizerAppLeaveAppli = new TapGestureRecognizer();
            gestureRecognizerAppLeaveAppli.Tapped += (s, e) =>
                        {
                            App.SetupRedirection(new LeaveApproval.PendingApprovalList("Approved", "hris"));
                            App.Current.MainPage = App.MasterDetailPage;
                        };


            StackLayout stackApprovedLeaveApplication = new StackLayout() { Orientation = StackOrientation.Horizontal };
            stackApprovedLeaveApplication.GestureRecognizers.Add(gestureRecognizerAppLeaveAppli);

            Image imgApprovedLeaveApplication = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "ApprovedLeave.png"
            };
            stackApprovedLeaveApplication.Children.Add(imgApprovedLeaveApplication);

            Label lblApprovedLeaveApplication = new Label() { FontSize = 16, Text = "Approved Application", HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.Black, Margin = new Thickness(10, 6, 0, 0), BackgroundColor = Color.FromHex("#FFF") };
            stackApprovedLeaveApplication.Children.Add(lblApprovedLeaveApplication);
            stackMain.Children.Add(stackApprovedLeaveApplication);
            SMyHris.Children.Add(stackApprovedLeaveApplication);
            StackLayout stackBorderApproved = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 100, Margin = new Thickness(-40, 5, 20, 5), Padding = 0, Spacing = 0 };
            SMyHris.Children.Add(stackBorderApproved);
            #endregion

            #region "DisApprovedLeaveApplication"
            var gestureDisApprovedLeaveApplication = new TapGestureRecognizer();
            gestureDisApprovedLeaveApplication.Tapped += (s, e) =>
                        {
                            App.SetupRedirection(new LeaveApproval.PendingApprovalList("Disapproved", "hris"));
                            App.Current.MainPage = App.MasterDetailPage;
                        };



            StackLayout stackDisApprovedLeaveApplication = new StackLayout() { Orientation = StackOrientation.Horizontal };
            stackDisApprovedLeaveApplication.GestureRecognizers.Add(gestureDisApprovedLeaveApplication);

            Image imgDisApprovedLeaveApplication = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "Disapproved.png"
            };
            stackDisApprovedLeaveApplication.Children.Add(imgDisApprovedLeaveApplication);

            Label lblDisApprovedLeaveApplication = new Label() { FontSize = 16, Text = "Disapproved Application", HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.Black, Margin = new Thickness(10, 6, 0, 0), BackgroundColor = Color.FromHex("#FFF") };
            stackDisApprovedLeaveApplication.Children.Add(lblDisApprovedLeaveApplication);
            stackMain.Children.Add(stackDisApprovedLeaveApplication);
            SMyHris.Children.Add(stackDisApprovedLeaveApplication);
            StackLayout stackBorderDisApproved = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 100, Margin = new Thickness(-40, 5, 20, 5), Padding = 0, Spacing = 0 };
            SMyHris.Children.Add(stackBorderDisApproved);
            #endregion

            #region "CancelledLeaveApplication"
            var gestureCancelledLeaveApplication = new TapGestureRecognizer();
            gestureCancelledLeaveApplication.Tapped += (s, e) =>
                        {
                            App.SetupRedirection(new LeaveApproval.PendingApprovalList("Cancelled", "hris"));
                            App.Current.MainPage = App.MasterDetailPage;
                        };


            StackLayout stackCancelledLeaveApplication = new StackLayout() { Orientation = StackOrientation.Horizontal };
            stackCancelledLeaveApplication.GestureRecognizers.Add(gestureCancelledLeaveApplication);

            Image imgCancelledLeaveApplication = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "CancelledLeave.png"
            };
            stackCancelledLeaveApplication.Children.Add(imgCancelledLeaveApplication);

            Label lblCancelledLeaveApplication = new Label() { FontSize = 16, Text = "Cancelled Application", HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.Black, Margin = new Thickness(10, 6, 0, 0), BackgroundColor = Color.FromHex("#FFF") };
            stackCancelledLeaveApplication.Children.Add(lblCancelledLeaveApplication);
            stackMain.Children.Add(stackCancelledLeaveApplication);
            SMyHris.Children.Add(stackCancelledLeaveApplication);
            StackLayout stackBorderCancelled = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 100, Margin = new Thickness(-40, 5, 20, 5), Padding = 0, Spacing = 0 };
            SMyHris.Children.Add(stackBorderCancelled);

            #endregion

            #region "AllLeaveApplication"
            var gestureAllLeaveApplication = new TapGestureRecognizer();
            gestureAllLeaveApplication.Tapped += (s, e) =>
                        {
                            App.SetupRedirection(new LeaveApproval.PendingApprovalList("All", "emp"));
                            App.Current.MainPage = App.MasterDetailPage;
                        };

            StackLayout stackAllLeaveApplication = new StackLayout() { Orientation = StackOrientation.Horizontal };
            stackAllLeaveApplication.GestureRecognizers.Add(gestureAllLeaveApplication);

            Image imgAllLeaveApplication = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "LeaveDetails.png"
            };
            stackAllLeaveApplication.Children.Add(imgAllLeaveApplication);

            Label lblAllLeaveApplication = new Label() { FontSize = 16, Text = "Leave Details", HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.Black, Margin = new Thickness(10, 6, 0, 0), BackgroundColor = Color.FromHex("#FFF") };
            stackAllLeaveApplication.Children.Add(lblAllLeaveApplication);
            stackMain.Children.Add(stackAllLeaveApplication);
            SMyHris.Children.Add(stackAllLeaveApplication);
            //StackLayout stackBorderAll = new StackLayout() { BackgroundColor = Color.FromHex("#E5E5E5"), HeightRequest = 1, Orientation = StackOrientation.Horizontal, WidthRequest = 100, Margin = new Thickness(-40, 5, 20, 5), Padding = 0, Spacing = 0 };
            //SMyHris.Children.Add(stackBorderCancelled);

            #endregion

            var gestureRecognizermyHRIS = new TapGestureRecognizer();
            gestureRecognizermyHRIS.Tapped += (s, e) =>
                        {
                            var source = imgHRiSplus.Source as FileImageSource;
                            if (source.File == "open.png")
                            {
                                imgHRiSplus.Source = "close.png";
                                SMyHris.FadeTo(1, 500);
                                SMyHris.IsVisible = true;
                            }
                            else
                            {
                                imgHRiSplus.Source = "open.png";
                                SMyHris.FadeTo(0, 500);
                                SMyHris.IsVisible = false;
                            }
                        };
            stackHRIS.GestureRecognizers.Add(gestureRecognizermyHRIS);
            stackMyHRIS.Children.Add(SMyHris);
            stackMain.Children.Add(stackMyHRIS);

            #endregion

            #region "Logout"

            var gestureRecognizer = new TapGestureRecognizer();
            gestureRecognizer.Tapped += (s, e) =>
                        {
                            App.Database.Logout();
                            Application.Current.Properties.Remove("EmployeeId");
                            App.Current.MainPage = new MainPage();
                        };

            StackLayout stackMainLogout = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand, Margin = 0, Padding = 0, Spacing = 0 };
            StackLayout stackLogout = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 0, Padding = 0, Spacing = 0 };
            stackMainLogout.GestureRecognizers.Add(gestureRecognizer);
            stackLogout.GestureRecognizers.Add(gestureRecognizer);
            Label lblLogout = new Label() { FontSize = 16, Text = "Logout", HorizontalOptions = LayoutOptions.Start, TextColor = Color.Black, BackgroundColor = Color.FromHex("#FFF"), Margin = new Thickness(10, 6, 0, 0) };
            Image imgLogout = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
                Source = "Logout.png"
            };
            stackLogout.Children.Add(imgLogout);
            stackLogout.Children.Add(lblLogout);
            stackMain.Children.Add(stackLogout);
            stackMain.Children.Add(stackMainLogout);

            #endregion

            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform<int>(20, 0, 0), 0, 0),
                Children = { stackGray, stackMain }
            };
            Content.BackgroundColor = Color.FromHex("#FFF");
            Title = "Menu";
            BackgroundColor = Color.Gray.WithLuminosity(0.9);
            Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null;
        }
        void btnPendingRequisitionClicked(object sender, EventArgs e)
        {
            App.SetupRedirection(new Views.RequisitionDashBoard());
            App.Current.MainPage = App.MasterDetailPage;
        }
    }
}


