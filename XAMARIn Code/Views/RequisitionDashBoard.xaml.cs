using myCIIEmployee.Models;
using System;
using System.Linq;

using Xamarin.Forms;

namespace myCIIEmployee.Views
{
    public partial class RequisitionDashBoard : ContentPage
    {
        public RequisitionDashBoard()
        {
            InitializeComponent();
            Title = "Requisition Dashboard";
        }
        //protected void btnRefresh_Clicked(object sender, EventArgs e)
        //{
        //    OnAppearing();
        //}


        protected async override void OnAppearing()
        {
            overlay.IsVisible = true;
            base.OnAppearing();
            Requisition objReqTotal = new Requisition(Convert.ToString(Application.Current.Properties["EmployeeId"]));
            objReqTotal.PageIndex = 1;
            objReqTotal.PageSize = 10000;
            objReqTotal.CallFor = null;
            objReqTotal.searchText = null;
            objReqTotal.DepartmentId = null;
            objReqTotal.RType = null;
            objReqTotal = await App.TodoManager.GetPendingRequisitionSearch(objReqTotal);
            if (objReqTotal.RequisitionList_Main != null)
            {
                for (int i = 0; i < objReqTotal.RequisitionList_Main.Count; i++)
                {
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 1)
                    {
                        int intCountPrint = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount); //objReqTotal.RequisitionList_Main.Count(x => x.RequTypeId == 1);
                        SetUpBadge(intCountPrint, ref lblPDCount, ref imgPD, ref stackPD);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 2)
                    {
                        int intCountTravel = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountTravel, ref lblTravelReqCount, ref imgTravel, ref stackTravel);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 3)
                    {
                        int intCountConference = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountConference, ref lblCECount, ref imgCE, ref stackConference);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 4)
                    {
                        int intCountHotel = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountHotel, ref lblHotelReqCount, ref imgHotel, ref stackHotel);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 5)
                    {
                        int intCountTransport = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountTransport, ref lblTransportReqCount, ref imgTransport, ref stackTransport);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 6)
                    {
                        int intCountIT = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountIT, ref lblITReqCount, ref imgIT, ref stackIT);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 7)
                    {
                        int intCountAudioVisual = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountAudioVisual, ref lblAVReqCount, ref imgAV, ref stackAudioVisual);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 8)
                    {
                        int intCountBackdrop = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountBackdrop, ref lblBBReqCount, ref imgBB, ref stackBackdrop);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 9)
                    {
                        int intCountGeneral = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountGeneral, ref lblGenReqCount, ref imgGen, ref stackGeneral);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 10)
                    {
                        int intCountMiscellaneous = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountMiscellaneous, ref lblMiscReqCount, ref imgMisc, ref stackMiscellaneous);
                    }
                    if (objReqTotal.RequisitionList_Main[i].RequTypeId == 14)
                    {
                        int intCountMedia = Convert.ToInt32(objReqTotal.RequisitionList_Main[i].ReqCount);
                        SetUpBadge(intCountMedia, ref lblMediaReqCount, ref imgMedia, ref stackMedia);
                    }
                }
            }
            overlay.IsVisible = false;
        }

        void SetUpBadge(int badgesCount, ref Label lblBadge, ref Image imgBadge, ref StackLayout StackBadge)
        {
            //if(badgesCount > 5)
            //badgesCount = 12;
            //else if (badgesCount == 1)
            //    badgesCount = 121;

            if (badgesCount > 0)
            {
                lblBadge.Text = Convert.ToString(badgesCount);
                lblBadge.FontFamily = "Robot";
                lblBadge.FontAttributes = FontAttributes.Bold;

                if (badgesCount <= 9)
                {
                    imgBadge.Source = "badges.png";
                    lblBadge.Margin = new Thickness(Device.OnPlatform(8, 5, 8), Device.OnPlatform(3, 0, 2), 0, 0);
                }
                else if (badgesCount <= 99)
                {
                    imgBadge.Source = "badges1.png";
                    lblBadge.Margin = new Thickness(Device.OnPlatform(8, 3, 8), Device.OnPlatform(3, -1, 2), 0, 0);
                }
                else if (badgesCount <= 999)
                {
                    imgBadge.Source = "badges3.png";
                    imgBadge.Margin = new Thickness(-7, 0, 0, 0);
                    lblBadge.Margin = new Thickness(Device.OnPlatform(8, -3, 8), Device.OnPlatform(3, 0, 2), 0, 0);
                }
                else if (badgesCount <= 9999)
                {
                    imgBadge.Source = "badges2.png";
                    imgBadge.Margin = new Thickness(-15, 0, 0, 0);
                    lblBadge.Margin = new Thickness(Device.OnPlatform(8, -10, 8), Device.OnPlatform(3, 0, 2), 0, 0);
                }
                else
                {
                    imgBadge.Source = "badges5.png";
                    imgBadge.Margin = new Thickness(-20, 0, 0, 0);
                    lblBadge.Margin = new Thickness(Device.OnPlatform(8, -14, 8), Device.OnPlatform(3, 1, 2), 0, 0);
                }
            }
            else
            {
                imgBadge.IsVisible = false;
                lblBadge.BackgroundColor = Color.Transparent;
                StackBadge.InputTransparent = true;
                //StackBadge.Opacity = 0.9;
            }
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("EmployeeId");
            App.Current.MainPage = new MainPage();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(1));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(1));

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(2));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(2));
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(3));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(3));
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(6));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(6));

        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(4));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(4));

        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(5));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(5));

        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(7));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(7));

        }

        private void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(8));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(8));

        }

        private void TapGestureRecognizer_Tapped_8(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(9));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(9));

        }

        private void TapGestureRecognizer_Tapped_9(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(10));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(10));

        }

        private void TapGestureRecognizer_Tapped_10(object sender, EventArgs e)
        {
            App.SetupRedirection(new RequisitionList(14));
            App.Current.MainPage = App.MasterDetailPage;
            //Navigation.PushModalAsync(new RequisitionList(14));

        }


        //private void PullToRefresh_Refreshing(object sender, EventArgs e)
        //{
        //    OnAppearing();
        //    PullToRefresh.IsRefreshing = false;
        //}
    }
}
