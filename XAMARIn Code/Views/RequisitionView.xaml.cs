using System;
using myCIIEmployee.Models;
using Xamarin.Forms;
using FormsPopup;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace myCIIEmployee.Views
{
    public partial class RequisitionView : ContentPage
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
        public RequisitionView(string EmployeeId, int RequisitionId, int RequisitionTypeId, string Departmentid, string EventId, int ReqCount)
        {
            InitializeComponent();
            _strRequisitionId = RequisitionId;
            _strRequisitionTypeId = RequisitionTypeId;
            _EmployeeId = EmployeeId;
            _Departmentid = Departmentid;
            _EventId = EventId;
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
                Margin = new Thickness(10, 0, 10, 10),
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
        protected async override void OnAppearing()
        {
            decimal moneyvalue = 1921.39m;
            string html = String.Format("Order Total: {0:C}", moneyvalue);
            base.OnAppearing();
            overlay.IsVisible = true;
            Title = "Travel Requisition";
            Requisition objReqTotal = new Requisition(_EmployeeId, _strRequisitionId, _strRequisitionTypeId);
            objReqTotal = await App.TodoManager.GetPendingRequisitionDetail(objReqTotal);
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

            BindAllTravellers(objReqTotal.getTravellerDetails);
            BindAllReservationDetails(objReqTotal.GetReservationDetails);

            #region "History Section"

            stackRequisitionHistory.Children.Add(await App.TodoManager.BindRequisitionHistory(Convert.ToInt64(_strRequisitionId)));

            #endregion

            lblDeptName.Text = objReqTotal.GetMainDetails[0].DeptName;
            lblevtName.Text = objReqTotal.GetMainDetails[0].Title;
            lblpurposetest.Text = objReqTotal.GetMainDetails[0].Purpose;
            lblESDValue.Text = objReqTotal.GetMainDetails[0].SerDeptRegion + ", " + objReqTotal.GetMainDetails[0].ServDeptName;
            IsServiceDept = Convert.ToString(objReqTotal.IsServiceDept);
            overlay.IsVisible = false;

        }

        async void BindRequisitionHistory(Int64 ReqsuisitionID)
        {
            RequestionHistoryList lstRequisitionHistory = await App.TodoManager.GetRequisitionHistory(ReqsuisitionID);

            //foreach (var reservationDetails in lstReservationDetails)
            //{
            //    StackLayout stackMain = new StackLayout();
            //    stackMain.Orientation = StackOrientation.Vertical;
            //    stackMain.Margin = new Thickness(20, 10, 20, 20);

            //    StackLayout stack1 = new StackLayout();
            //    stack1.Orientation = StackOrientation.Horizontal;
            //    stack1.HorizontalOptions = LayoutOptions.StartAndExpand;

            //    Label lbl1 = new Label();
            //    lbl1.Text = reservationDetails.FromJourney + " --> " + reservationDetails.ToJourney + " (" + reservationDetails.Flight_TrainNo + ")";
            //    lbl1.FontSize = 16;
            //    lbl1.FontFamily = "Roboto";
            //    lbl1.TextColor = Color.Black;

            //    stack1.Children.Add(lbl1);
            //    stackMain.Children.Add(stack1);

            //    StackLayout stack2 = new StackLayout();
            //    stack2.Orientation = StackOrientation.Horizontal;
            //    stack2.HorizontalOptions = LayoutOptions.StartAndExpand;

            //    Label lbl2 = new Label();
            //    lbl2.Text = reservationDetails.RequiredOn + "  " + reservationDetails.DepartureTime + "  " + reservationDetails.ArrivalTime;
            //    lbl2.FontSize = 14;
            //    lbl2.FontFamily = "Roboto";
            //    lbl2.TextColor = Color.Gray;

            //    stack2.Children.Add(lbl2);
            //    stackMain.Children.Add(stack2);

            //    stackReservation.Children.Add(stackMain);
            //}

        }

        void BindAllReservationDetails(List<GetReservationDetails> lstReservationDetails)
        {
            foreach (var reservationDetails in lstReservationDetails)
            {
                StackLayout stackMain = new StackLayout();
                stackMain.Orientation = StackOrientation.Vertical;
                stackMain.Margin = new Thickness(20, 10, 20, 20);

                StackLayout stack1 = new StackLayout();
                stack1.Orientation = StackOrientation.Horizontal;
                stack1.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lbl1 = new Label();
                lbl1.Text = reservationDetails.FromJourney + " --> " + reservationDetails.ToJourney + " (" + reservationDetails.Flight_TrainNo + ")";
                lbl1.FontSize = 16;
                lbl1.FontFamily = "Roboto";
                lbl1.TextColor = Color.Black;

                stack1.Children.Add(lbl1);
                stackMain.Children.Add(stack1);

                StackLayout stack2 = new StackLayout();
                stack2.Orientation = StackOrientation.Horizontal;
                stack2.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lbl2 = new Label();
                lbl2.Text = reservationDetails.RequiredOn + "  " + reservationDetails.DepartureTime + "  " + reservationDetails.ArrivalTime;
                lbl2.FontSize = 14;
                lbl2.FontFamily = "Roboto";
                lbl2.TextColor = Color.Gray;

                stack2.Children.Add(lbl2);
                stackMain.Children.Add(stack2);

                stackReservation.Children.Add(stackMain);

            }

        }

        void BindAllTravellers(List<GetTravellerDetails> lstTravellerDetails)
        {
            foreach (var travellerDetails in lstTravellerDetails)
            {
                StackLayout stackMain = new StackLayout();
                stackMain.Orientation = StackOrientation.Vertical;
                stackMain.Margin = new Thickness(20, 10, 20, 20);

                StackLayout stack1 = new StackLayout();
                stack1.Orientation = StackOrientation.Horizontal;
                stack1.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lbl1 = new Label();
                lbl1.Text = travellerDetails.TravellerName;
                lbl1.FontSize = 16;
                lbl1.FontFamily = "Roboto";
                lbl1.TextColor = Color.Black;

                stack1.Children.Add(lbl1);
                stackMain.Children.Add(stack1);

                stackTravellers.Children.Add(stackMain);

            }

        }

    }
}
