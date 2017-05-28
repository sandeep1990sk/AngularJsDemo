using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myCIIEmployee.Models;


using Xamarin.Forms;
using FormsPopup;

namespace myCIIEmployee.Views
{
    public partial class HotelRequisition : ContentPage
    {

        private readonly SwitchCell _closeOnAnyTap;
        private readonly SwitchCell _preventShowing;
        private readonly SwitchCell _displayTappedSection;
        private readonly SwitchCell _showingAnimation;
        private readonly Popup _popup1;
        public Editor _actionEditor;
        public Button actionButton;
        public Label lblServiceDeptId;
        public string IsServiceDept;
        int _strRequisitionId, _strRequisitionTypeId, _strReqCount;
        string _EmployeeId, _EventId, _Departmentid, _strButtonText;

        public HotelRequisition(string EmployeeId, int RequisitionId, int RequisitionTypeId, string Departmentid, string EventId, int ReqCount)
        {
            InitializeComponent();
            _strRequisitionId = RequisitionId;
            _strRequisitionTypeId = RequisitionTypeId;
            _Departmentid = Departmentid;
            _EventId = EventId;
            _EmployeeId = EmployeeId;
            Title = "Hotel Requisition";
            _strReqCount = ReqCount;
            #region "popup"
            _closeOnAnyTap = new SwitchCell { Text = "Close on any tap", On = false };
            _preventShowing = new SwitchCell { Text = "Prevent popup from showing" };
            _displayTappedSection = new SwitchCell { Text = "Display the tapped section's name" };
            _showingAnimation = new SwitchCell { Text = "Turn on 'Showing' animation", On = true };


            var closeButton = new Button
            {
                Text = "Close",
                TextColor = Color.FromHex("#FFF"),
                BackgroundColor = Color.FromHex("#ed583b"),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 130,
                Margin = new Thickness(10, 0, 10, 10),
            };

            actionButton = new Button
            {
                Text = "Ok",
                TextColor = Color.FromHex("#FFF"),
                BackgroundColor = Color.FromHex("#2196f2"),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 130,
                Margin = new Thickness(10, 0, 0, 10),
            };

            _actionEditor = new Editor
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Gray
            };

            _popup1 = new Popup
            {
                XPositionRequest = 0.5,
                YPositionRequest = 0.5,
                ContentWidthRequest = 0.8,
                ContentHeightRequest = 0.5,
                BackgroundColor = new Color(0, 0, 0, 0.5),
                Header = new ContentView
                {
                    Padding = 10,
                    BackgroundColor = Color.FromHex("#3399FF"),
                    Content = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        TextColor = Color.White,
                        Text = "Remarks"
                    }
                },

                Body = new ContentView
                {
                    Padding = 10,
                    BackgroundColor = Color.White,
                    Content = _actionEditor
                },

                Footer = new ContentView
                {
                    BackgroundColor = Color.White,

                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        //  HorizontalOptions = LayoutOptions.EndAndExpand,
                        Children = { actionButton, closeButton }
                    }
                }
            };


            new PopupPageInitializer(this) { _popup1 };

            _popup1.Tapped += Popup1_Tapped;
            _popup1.Showing += Popup1_Showing;
            actionButton.Clicked += ActionButton_Clicked;
            closeButton.Clicked += CloseButton_Clicked;
            #endregion
        }

        private async void ActionButton_Clicked(object sender, EventArgs e)
        {
            if ((_actionEditor.Text == "") || (_actionEditor.Text == null))
                await DisplayAlert("Alert", "Please enter reason", "OK");
            else
            {
                RequisitionApproval objRequisitionApproval = new RequisitionApproval();
                string strActionEditor = _actionEditor.Text;
                await _popup1.HideAsync(async p =>
                {
                    await p.FadeTo(0, 250, Easing.Linear);
                    p.Opacity = 1;
                });
                if (_strButtonText.ToLower() == "accept")
                {
                    overlay.IsVisible = true;
                    objRequisitionApproval.TransactionType = "approve";
                    objRequisitionApproval.RequestID = _strRequisitionId;
                    objRequisitionApproval.RequTypeId = _strRequisitionTypeId;
                    objRequisitionApproval.EmployeeID = _EmployeeId;
                    objRequisitionApproval.Remark = _actionEditor.Text;
                    objRequisitionApproval.DepartmentID = _Departmentid;
                    objRequisitionApproval.SerDeptID = lblServiceDeptId.Text;
                    objRequisitionApproval.Purpose = "";
                    if (_EventId == null)
                        objRequisitionApproval.EventRelated = "0";
                    else
                        objRequisitionApproval.EventRelated = "1";
                    objRequisitionApproval.EventID = _EventId;
                    objRequisitionApproval.RequisitionInsertUpdate = "";
                    objRequisitionApproval.IsSpecialApp = "";
                    objRequisitionApproval.BudgetYear = 0;
                    objRequisitionApproval.EventReason = "";
                    objRequisitionApproval.Attachment = null;
                    objRequisitionApproval.strIsSerDept = IsServiceDept;
                    objRequisitionApproval.stremployeeName = App.Name;
                    objRequisitionApproval = await App.TodoManager.SaveUpdateWorkflowRequisition(objRequisitionApproval);
                    await DisplayAlert("Alert", "You have accepted the requisition.", "OK");
                    if (_strReqCount == 1)
                    {
                        App.SetupRedirection(new Views.RequisitionDashBoard());
                        App.Current.MainPage = App.MasterDetailPage;
                    }
                    else
                    {
                        App.SetupRedirection(new Views.RequisitionList(_strRequisitionTypeId));
                        App.Current.MainPage = App.MasterDetailPage;
                    }
                    overlay.IsVisible = false;
                }
                else if (_strButtonText.ToLower() == "reject")
                {
                    overlay.IsVisible = true;
                    objRequisitionApproval.TransactionType = "Reject";
                    objRequisitionApproval.RequestID = _strRequisitionId;
                    objRequisitionApproval.RequTypeId = _strRequisitionTypeId;
                    objRequisitionApproval.EmployeeID = _EmployeeId;
                    objRequisitionApproval.Remark = _actionEditor.Text;
                    objRequisitionApproval.DepartmentID = _Departmentid;
                    objRequisitionApproval.SerDeptID = lblServiceDeptId.Text;//"D000000106";
                    objRequisitionApproval.Purpose = "";
                    if (_EventId == null)
                        objRequisitionApproval.EventRelated = "0";
                    else
                        objRequisitionApproval.EventRelated = "1";
                    objRequisitionApproval.EventID = _EventId;
                    objRequisitionApproval.RequisitionInsertUpdate = "";
                    objRequisitionApproval.IsSpecialApp = "";
                    objRequisitionApproval.BudgetYear = 0;
                    objRequisitionApproval.EventReason = "";
                    objRequisitionApproval.Attachment = null;
                    objRequisitionApproval.strIsSerDept = IsServiceDept;
                    objRequisitionApproval.stremployeeName = App.Name;

                    objRequisitionApproval = await App.TodoManager.SaveUpdateWorkflowRequisition(objRequisitionApproval);
                    await DisplayAlert("Alert", "You have rejected the requisition", "OK");
                    if (_strReqCount == 1)
                    {
                        App.SetupRedirection(new Views.RequisitionDashBoard());
                        App.Current.MainPage = App.MasterDetailPage;
                    }
                    else
                    {
                        App.SetupRedirection(new Views.RequisitionList(_strRequisitionTypeId));
                        App.Current.MainPage = App.MasterDetailPage;
                    }
                    overlay.IsVisible = false;

                }
                else if (_strButtonText.ToLower() == "revert")
                {
                    overlay.IsVisible = true;
                    objRequisitionApproval.TransactionType = "Revert";
                    objRequisitionApproval.RequestID = _strRequisitionId;
                    objRequisitionApproval.RequTypeId = _strRequisitionTypeId;
                    objRequisitionApproval.EmployeeID = _EmployeeId;
                    objRequisitionApproval.Remark = _actionEditor.Text;
                    objRequisitionApproval.DepartmentID = _Departmentid;
                    objRequisitionApproval.SerDeptID = lblServiceDeptId.Text;//"D000000106";
                    objRequisitionApproval.Purpose = "";
                    if (_EventId == null)
                        objRequisitionApproval.EventRelated = "0";
                    else
                        objRequisitionApproval.EventRelated = "1";
                    objRequisitionApproval.EventID = _EventId;
                    objRequisitionApproval.RequisitionInsertUpdate = "";
                    objRequisitionApproval.IsSpecialApp = "";
                    objRequisitionApproval.BudgetYear = 0;
                    objRequisitionApproval.EventReason = "";
                    objRequisitionApproval.Attachment = null;
                    objRequisitionApproval.strIsSerDept = IsServiceDept;
                    objRequisitionApproval.stremployeeName = App.Name;
                    objRequisitionApproval = await App.TodoManager.SaveUpdateWorkflowRequisition(objRequisitionApproval);
                    await DisplayAlert("Alert", "You have reverted the requisition", "OK");
                    if (_strReqCount == 1)
                    {
                        App.SetupRedirection(new Views.RequisitionDashBoard());
                        App.Current.MainPage = App.MasterDetailPage;
                    }
                    else
                    {
                        App.SetupRedirection(new Views.RequisitionList(_strRequisitionTypeId));
                        App.Current.MainPage = App.MasterDetailPage;
                    }
                }
                overlay.IsVisible = false;
            }
        }
        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await _popup1.HideAsync(async p =>
            {
                await p.FadeTo(0, 250, Easing.Linear);
                p.Opacity = 1;
            });
        }
        private void Popup1_Showing(object sender, PopupShowingEventArgs e)
        {
            if (_preventShowing.On)
            {
                e.Cancel = true;
            }
        }

        private void Popup1_Tapped(object sender, PopupTappedEventArgs e)
        {
            if (_closeOnAnyTap.On)
            {
                e.Popup.Hide();
            }

            if (_displayTappedSection.On)
            {
                DisplayAlert("Information", $"{e.Section} tapped.", "OK");
            }
        }

        protected async void btnAccept_Clicked(object sender, EventArgs e)
        {
            actionButton.Text = "Accept";

            _strButtonText = "Accept";
            if (_showingAnimation.On)
            {
                double original;
                await _popup1.ShowAsync(async p =>
                {
                    original = p.Scale;

                    await Task.WhenAll
                    (
                        p.SectionContainer.RelScaleTo(0.05, 100, Easing.CubicOut),
                        p.SectionContainer.RelScaleTo(-0.05, 105, Easing.CubicOut)
                    ).ContinueWith(c =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            p.SectionContainer.Scale = original;
                        });
                    });
                });
            }
            else
                _popup1.Show();
        }
        protected async void btnReject_Clicked(object sender, EventArgs e)
        {
            actionButton.Text = "Reject";
            _strButtonText = "Reject";
            if (_showingAnimation.On)
            {
                double original;
                await _popup1.ShowAsync(async p =>
                {
                    original = p.Scale;

                    await Task.WhenAll
                    (
                        p.SectionContainer.RelScaleTo(0.05, 100, Easing.CubicOut),
                        p.SectionContainer.RelScaleTo(-0.05, 105, Easing.CubicOut)
                    ).ContinueWith(c =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            p.SectionContainer.Scale = original;
                        });
                    });
                });
            }
            else
                _popup1.Show();

        }
        protected async void btnRevert_Clicked(object sender, EventArgs e)
        {
            actionButton.Text = "Revert";
            _strButtonText = "Revert";
            if (_showingAnimation.On)
            {
                double original;
                await _popup1.ShowAsync(async p =>
                {
                    original = p.Scale;

                    await Task.WhenAll
                    (
                        p.SectionContainer.RelScaleTo(0.05, 100, Easing.CubicOut),
                        p.SectionContainer.RelScaleTo(-0.05, 105, Easing.CubicOut)
                    ).ContinueWith(c =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            p.SectionContainer.Scale = original;
                        });
                    });
                });
            }
            else
                _popup1.Show();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            overlay.IsVisible = true;
            Requisition objReqTotal = new Requisition(_EmployeeId, _strRequisitionId, _strRequisitionTypeId);
            objReqTotal = await App.TodoManager.GetHotelRequisition(objReqTotal);
            lblServiceDeptId = new Label
            {
                Text = objReqTotal.GetMainDetails[0].serDeptID
            };

            if (objReqTotal.GetMainDetails[0].EventRelated == "0")
            {
                EventBudget objNotRelatedToEvent = new EventBudget(_strRequisitionId, _strRequisitionTypeId, _Departmentid, Convert.ToInt32(objReqTotal.GetMainDetails[0].BudgetYear));
                objNotRelatedToEvent = await App.TodoManager.GetPendingReqEventsRelatedToDept(objNotRelatedToEvent);
                lblBP.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objNotRelatedToEvent.Budgetprovision));
                lblBU.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objNotRelatedToEvent.BudgetUtilised));
                lblSFP.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objNotRelatedToEvent.Submittedforprocessing));
                lblBA.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objNotRelatedToEvent.BalanceAvailable));
                lblTR.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objNotRelatedToEvent.ThisRequisition));
                lblBATR.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objNotRelatedToEvent.BalanceAfterThisRequisition));
            }
            else
            {
                EventBudget objEventBudget = new EventBudget(_strRequisitionId, _strRequisitionTypeId, _Departmentid, _EventId);
                objEventBudget = await App.TodoManager.GetPendingRequisitionBudgetDetail(objEventBudget);
                lblBP.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objEventBudget.Budgetprovision));
                lblBU.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objEventBudget.BudgetUtilised));
                lblSFP.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objEventBudget.Submittedforprocessing));
                lblBA.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objEventBudget.BalanceAvailable));
                lblTR.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objEventBudget.ThisRequisition));
                lblBATR.Text = String.Format("{0:0,0.00}", Convert.ToDouble(objEventBudget.BalanceAfterThisRequisition));
            }
            BindTravellerDetails(objReqTotal.getTravellerDetails);
            //listBookingDetails.ItemsSource = objReqTotal.getTravellerDetails;
            lblDeptName.Text = objReqTotal.GetMainDetails[0].DeptName;
            lblevtName.Text = objReqTotal.GetMainDetails[0].Title;
            lblCID.Text = objReqTotal.GetHotelRequisition[0].Date_Checkin;
            lblTime.Text = objReqTotal.GetHotelRequisition[0].Time_Checkin;
            lblpickupFrom.Text = objReqTotal.GetHotelRequisition[0].PickupFrom_Checkin;

            lblCOD.Text = objReqTotal.GetHotelRequisition[0].Date_Checkout;
            lblTimeCOD.Text = objReqTotal.GetHotelRequisition[0].Time_Checkout;
            lblpickupFromCOD.Text = objReqTotal.GetHotelRequisition[0].PickupFrom_Checkout;

            lblpurposetest.Text = objReqTotal.GetMainDetails[0].Purpose;
            lblESDValue.Text = objReqTotal.GetMainDetails[0].SerDeptRegion + ", " + objReqTotal.GetMainDetails[0].ServDeptName;
            IsServiceDept = Convert.ToString(objReqTotal.IsServiceDept);

            #region "History Section"

            stackRequisitionHistory.Children.Add(await App.TodoManager.BindRequisitionHistory(Convert.ToInt64(_strRequisitionId)));

            #endregion

            overlay.IsVisible = false;

        }

        void BindTravellerDetails(List<GetTravellerDetails> lstTravellerDetails)
        {
            int indx = -1;
            foreach (var GeneralDetails in lstTravellerDetails)
            {
                indx++;
                StackLayout stackMain = new StackLayout();
                stackMain.Orientation = StackOrientation.Vertical;
                stackMain.HorizontalOptions = LayoutOptions.FillAndExpand;
                stackMain.VerticalOptions = LayoutOptions.StartAndExpand;
                stackMain.Margin = 0;
                stackMain.Spacing = 0;
                stackMain.Padding = 0;

                StackLayout stackBorder = new StackLayout();
                stackBorder.HeightRequest = 1;
                stackBorder.BackgroundColor = Color.Gray;
                stackBorder.Margin = new Thickness(0, 0, 0, 5);
                if (lstTravellerDetails.Count > 1 && indx != 0)
                    stackMain.Children.Add(stackBorder);


                StackLayout stackRow1 = new StackLayout();
                stackRow1.Orientation = StackOrientation.Horizontal;
                stackRow1.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lblTravellerName = new Label();
                lblTravellerName.FontSize = 16;
                lblTravellerName.FontFamily = "Roboto";
                lblTravellerName.Margin = 0;
                lblTravellerName.TextColor = Color.FromHex("#000");
                lblTravellerName.Text = GeneralDetails.TravellerName;
                stackRow1.Children.Add(lblTravellerName);

                stackMain.Children.Add(stackRow1);

                StackLayout stackRow2 = new StackLayout();
                stackRow2.Orientation = StackOrientation.Horizontal;

                Label lblBookingFor = new Label();
                lblBookingFor.FontSize = 14;
                lblBookingFor.FontFamily = "Roboto";
                lblBookingFor.Margin = 0;
                lblBookingFor.TextColor = Color.FromHex("#333");
                lblBookingFor.Text = "Booking for : " + Convert.ToString(GeneralDetails.BookingFor);
                lblBookingFor.HorizontalOptions = LayoutOptions.StartAndExpand;
                stackRow2.Children.Add(lblBookingFor);
                Label lblCategoryOfRoom = new Label();
                lblCategoryOfRoom.FontSize = 14;
                lblCategoryOfRoom.FontFamily = "Roboto";
                lblCategoryOfRoom.Margin = 0;
                lblCategoryOfRoom.TextColor = Color.FromHex("#333");
                lblCategoryOfRoom.Text = "Room : " + Convert.ToString(GeneralDetails.CategoryOfRoom);
                lblCategoryOfRoom.HorizontalOptions = LayoutOptions.EndAndExpand;
                stackRow2.Children.Add(lblCategoryOfRoom);
                stackMain.Children.Add(stackRow2);
                stackBookingDetails.Children.Add(stackMain);
            }
        }
    }
}
