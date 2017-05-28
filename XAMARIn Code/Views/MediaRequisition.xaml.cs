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
    public partial class MediaRequisition : ContentPage
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
        public MediaRequisition(string EmployeeId, int RequisitionId, int RequisitionTypeId, string Departmentid, string EventId, int ReqCount)
        {
            InitializeComponent();
            _strRequisitionId = RequisitionId;
            _strRequisitionTypeId = RequisitionTypeId;
            _EmployeeId = EmployeeId;
            _Departmentid = Departmentid;
            _EventId = EventId;
            _strReqCount = ReqCount;
            Title = "Media Requisition";
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
                        Margin = new Thickness(10, 0, 10, 0),
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
                    if (_EventId == null || _EventId == "")
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
                    if (_EventId == null || _EventId == "")
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
                    if (_EventId == null || _EventId == "")
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
            objReqTotal = await App.TodoManager.GetMediaRequisition(objReqTotal);
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
            BindGeneralDetails(objReqTotal.TravelReqDetail);
            lblDeptName.Text = objReqTotal.GetMainDetails[0].DeptName;
            lblevtName.Text = objReqTotal.GetMainDetails[0].Title;
            lblpurposetest.Text = objReqTotal.GetMainDetails[0].Purpose;
            lblESDValue.Text = objReqTotal.GetMainDetails[0].SerDeptRegion + ", " + objReqTotal.GetMainDetails[0].ServDeptName;
            IsServiceDept = Convert.ToString(objReqTotal.IsServiceDept);
            overlay.IsVisible = false;

            #region "History Section"

            stackRequisitionHistory.Children.Add(await App.TodoManager.BindRequisitionHistory(Convert.ToInt64(_strRequisitionId)));

            #endregion


        }

        void BindGeneralDetails(List<GetTravelReqDetail> lstGeneralDetails)
        {
            int indx = -1;
            foreach (var GeneralDetails in lstGeneralDetails)
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
                if (lstGeneralDetails.Count > 1 && indx != 0)
                    stackMain.Children.Add(stackBorder);


                StackLayout stackRow1 = new StackLayout();
                stackRow1.Orientation = StackOrientation.Horizontal;
                stackRow1.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lblItemName = new Label();
                lblItemName.FontSize = 16;
                lblItemName.FontFamily = "Roboto";
                lblItemName.Margin = 0;
                lblItemName.TextColor = Color.FromHex("#000");
                lblItemName.Text = GeneralDetails.ItemName;
                stackRow1.Children.Add(lblItemName);

                stackMain.Children.Add(stackRow1);

                StackLayout stackRow2 = new StackLayout();
                stackRow2.Orientation = StackOrientation.Horizontal;
                stackRow2.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lblRate = new Label();
                lblRate.FontSize = 14;
                lblRate.FontFamily = "Roboto";
                lblRate.Margin = 0;
                lblRate.TextColor = Color.FromHex("#333");
                lblRate.Text = "Rate : " + Convert.ToString(GeneralDetails.Rate);
                stackRow2.Children.Add(lblRate);

                stackMain.Children.Add(stackRow2);


                StackLayout stackRow3 = new StackLayout();
                stackRow3.Orientation = StackOrientation.Horizontal;
                stackRow3.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lblQuantity = new Label();
                lblQuantity.FontSize = 14;
                lblQuantity.FontFamily = "Roboto";
                lblQuantity.Margin = 0;
                lblQuantity.TextColor = Color.FromHex("#333");
                lblQuantity.Text = "Quantity : " + Convert.ToString(GeneralDetails.Quantity);
                stackRow3.Children.Add(lblQuantity);

                stackMain.Children.Add(stackRow3);

                StackLayout stackRow4 = new StackLayout();
                stackRow4.Orientation = StackOrientation.Horizontal;
                stackRow4.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lblAmount = new Label();
                lblAmount.FontSize = 14;
                lblAmount.FontFamily = "Roboto";
                lblAmount.Margin = 0;
                lblAmount.TextColor = Color.FromHex("#333");
                lblAmount.Text = "Amount : " + Convert.ToString(GeneralDetails.Amount);
                stackRow4.Children.Add(lblAmount);

                stackMain.Children.Add(stackRow4);

                StackLayout stackRow5 = new StackLayout();
                stackRow5.Orientation = StackOrientation.Horizontal;
                stackRow5.HorizontalOptions = LayoutOptions.StartAndExpand;

                Label lblRequiredOn = new Label();
                lblRequiredOn.FontSize = 14;
                lblRequiredOn.FontFamily = "Roboto";
                lblRequiredOn.Margin = 0;
                lblRequiredOn.TextColor = Color.FromHex("#333");
                lblRequiredOn.Text = "Required On : " + Convert.ToString(GeneralDetails.Requiredon);
                stackRow5.Children.Add(lblRequiredOn);

                stackMain.Children.Add(stackRow5);

                stackGeneralDeatils.Children.Add(stackMain);

            }

        }
    }
}
