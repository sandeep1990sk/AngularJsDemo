using FormsPopup;
//using Java.Util;
using myCIIEmployee.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace myCIIEmployee.LeaveApproval
{
    public partial class LeaveApplicationForm : ContentPage
    {

        #region "Global Variables"
        public Dictionary<string, int> _leaveType = new Dictionary<string, int>();
        private readonly Popup _popup1;
        public Label _actionEditor;
        public Label _actionEditor1;
        public Button actionButton;
        public Picker pickerhalfDay;

        private readonly Popup _popupforDisapproved;
        public Label _actionEditorforDisapproved;
        public Button actionButtonforDisapproved;

        private readonly SwitchCell _closeOnAnyTap;
        private readonly SwitchCell _preventShowing;
        private readonly SwitchCell _displayTappedSection;
        private readonly SwitchCell _showingAnimation;
        public string StrMessage = string.Empty;

        public DateTime ReqDate = new DateTime();
        public DateTime RequestLastDate = new DateTime();
        public DateTime FromDate = new DateTime();
        public DateTime ToDate = new DateTime();
        public DateTime StartDate = new DateTime();
        public DateTime LastDate = new DateTime();

        public string LeaveId = "0";
        public string hdnStatusPrev = null, hdnTypeFor = null;
        public int IsPL, SLLeaveBalance;
        public double exterLeave, LevAvailedCL;
        public double TotalDaysCL, BalCL, ClosingCL, LevCLApplied;
        public Editor _actionEditorforDisapproved1;
        public String _strGender = null, _strEmployeeConfirmed = null, _strHalfDay = null, hdnNoOfDays = null, hdnFinalLWP = null, _strSelf, _strStatus, _StrEmpId, _StrCancelShowHide;
        public string halfStatus = string.Empty;
        public string hdnEmployeeConfirmed, hdnCategoryId, hdnWorkFlowID, hdnGender, hdnDirectReport, hdnFinalLeave = null, hdnCallFor;
        public int hdnLeaveId, hdnJoinTime, hdnRequisitionId, _hdnActualLeaveType, hdnTotalDays, _strrequisitionid, hdnSLTotalDays, hdnCLTotalDays, LeaveID;
        public double hdnOpeningBalance, IsCL, LevOpeningBaLCL, ClosingPLorLL, LevPLApplied, TotalDaysPLOrLL, LevOpeningBaLPLOrLL, LevAvailedPLOrLL, BalPLorLL, hdnAvailed, hdnBalancePL, hdnAccruded, hdnPLAppliedNotApproved, hdnIsPL = 0.0, hdnSLOpeningBalance, hdnCLAppliedNotApproved, hdnIsCL = 0.0;
        public double hdnSLAvailed, hdnBalanceSL, hdnSLAccruded, hdnSLClosingBalElibility, hdnCLOpeningBalance, hdnSLAppliedNotApproved, hdnCLAvailed, hdnBalanceCL, hdnCLAccruded;
        #endregion

        public LeaveApplicationForm(string strSelf, Int64 RequisitionId, string EmpId, string Status, string CancelShowHide)
        {
            _StrEmpId = EmpId;
            _strStatus = Status;
            _StrCancelShowHide = CancelShowHide;
            _strSelf = strSelf;
            _strrequisitionid = Convert.ToInt32(RequisitionId);


            InitializeComponent();

            #region "Pageload"
            if (_strrequisitionid != 0)
                LeaveId = Convert.ToString(_strrequisitionid);
            if (_strStatus != null && _strStatus != "")
                hdnStatusPrev = _strStatus;

            #region "Hide And Show Section of Button"
            if (_strSelf != null && _strSelf == "emp")
                hdnTypeFor = "emp";
            if (_strSelf != null && _strSelf == "hris")
                hdnTypeFor = "hris";
            else
                hdnTypeFor = "emp";

            if (_strrequisitionid != 0)
            {
                if (_strSelf != null && _strSelf == "emp")
                {
                    if (!string.IsNullOrEmpty(_strStatus) && _strStatus.ToLower() == "saved")
                    {
                        btnSave.IsVisible = true;
                        btnApply.IsVisible = true;
                        btnApprove.IsVisible = false;
                        if (_StrCancelShowHide.ToLower() == "y")
                        {
                            btnReject.IsVisible = true;
                            btnReject.Text = "Cancel";
                        }
                        else
                        {
                            btnReject.IsVisible = false;
                        }
                    }
                    else if (!string.IsNullOrEmpty(_strStatus) && _strStatus.ToLower() == "cancelled")
                    {
                        btnSave.IsVisible = false;
                        btnReject.IsVisible = false;
                        btnApply.IsVisible = false;
                        btnApprove.IsVisible = false;
                    }
                    else
                    {

                        btnSave.IsVisible = false;
                        if (_StrCancelShowHide.ToLower() == "y")
                        {
                            btnReject.IsVisible = true;
                            btnReject.Text = "Cancel";
                        }
                        else
                        {
                            btnReject.IsVisible = false;
                        }
                        btnApply.IsVisible = false;
                        btnApprove.IsVisible = false;
                    }
                }

                else
                {
                    if (!string.IsNullOrEmpty(_strStatus) && _strStatus.ToLower() == "applied" || _strStatus.ToLower() == "rework")
                    {
                        btnSave.IsVisible = false;
                        btnReject.Text = "Disapprove";
                        btnApply.IsVisible = false;
                        btnApprove.IsVisible = true;
                        btnReject.IsVisible = true;
                    }
                    else
                    {
                        btnSave.IsVisible = false;
                        if (_StrCancelShowHide.ToLower() == "y")
                        {
                            btnReject.IsVisible = true;
                            btnReject.Text = "Cancel";
                        }
                        else
                        {
                            btnReject.Text = "Disapprove";
                            btnReject.IsVisible = false;
                        }

                        btnApply.IsVisible = false;
                        btnApprove.IsVisible = false;
                    }
                }
            }
            else
            {
                // Div_Status.Style.Add("display", "none");
                if (_StrEmpId.ToLower() == "self")
                {
                    btnSave.IsVisible = true;
                }
                else
                {
                    btnSave.IsVisible = false;
                }

                btnApply.IsVisible = true;
                btnReject.IsVisible = false;
                btnApprove.IsVisible = false;
            }
            #endregion

            #endregion
            Dictionary<string, int> nameToColor = new Dictionary<string, int>
        {
                { "Please select", -1},
            { "I will be leaving in the second half of the first day of my leave", 0},
                { "I will be joining back in the second half of the last day of the leave", 1 },
            { "Both",2 }
        };

            Dictionary<string, int> IsCertificate = new Dictionary<string, int>
        {
                { "Please select", -1},
            { "Yes", 1},
                { "No", 0 }
        };
            foreach (string medCer in IsCertificate.Keys)
            {
                pickerMedCer.Items.Add(medCer);
            }
            Title = "Apply Leave";
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

            _actionEditor = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Gray,
                Text = "Please specify on which day are you taking a half day :"

            };

            _actionEditor1 = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Gray,
                Text = "I am taking a half day on"

            };

            pickerhalfDay = new Picker
            {
                Title = "Select Day",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.FromHex("#000")
            };

            foreach (string colorName in nameToColor.Keys)
            {
                pickerhalfDay.Items.Add(colorName);
            }
            _popup1 = new Popup
            {
                XPositionRequest = 0.1,
                YPositionRequest = 0.1,
                ContentWidthRequest = 1.0,
                ContentHeightRequest = 0.4,
                BackgroundColor = new Color(0, 0, 0, 0.5),
                Header = new ContentView
                {
                    Padding = 5,
                    BackgroundColor = Color.FromHex("#3399FF"),
                    Content = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        TextColor = Color.White,
                        Text = "Half Day"
                    }
                },

                Body = new ContentView
                {
                    Padding = 10,
                    BackgroundColor = Color.White,
                    //Content = _actionEditor,
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Margin = new Thickness(10, 0, 10, 0),
                        //  HorizontalOptions = LayoutOptions.EndAndExpand,
                        Children = { _actionEditor, _actionEditor1, pickerhalfDay }
                    }

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

            #region "Popup for disapprove"
            _closeOnAnyTap = new SwitchCell { Text = "Close on any tap", On = false };
            _preventShowing = new SwitchCell { Text = "Prevent popup from showing" };
            _displayTappedSection = new SwitchCell { Text = "Display the tapped section's name" };
            _showingAnimation = new SwitchCell { Text = "Turn on 'Showing' animation", On = true };

            var closeButtonforDisapproved = new Button
            {
                Text = "Close",
                TextColor = Color.FromHex("#FFF"),
                BackgroundColor = Color.FromHex("#ed583b"),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 130,
                Margin = new Thickness(10, 0, 10, 10),
            };

            actionButtonforDisapproved = new Button
            {
                Text = "Ok",
                TextColor = Color.FromHex("#FFF"),
                BackgroundColor = Color.FromHex("#2196f2"),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                WidthRequest = 130,
                Margin = new Thickness(10, 0, 10, 10),
            };
            _actionEditorforDisapproved = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Gray,
                Text = "Remarks"
            };
            _actionEditorforDisapproved1 = new Editor
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.FromHex("#FFF")
            };
            _popupforDisapproved = new Popup
            {
                XPositionRequest = 0.1,
                YPositionRequest = 0.1,
                ContentWidthRequest = 1.0,
                ContentHeightRequest = 0.4,
                BackgroundColor = new Color(0, 0, 0, 0.5),
                Header = new ContentView
                {
                    Padding = 5,
                    BackgroundColor = Color.FromHex("#3399FF"),
                    Content = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        TextColor = Color.White,
                        Text = "Disapprove"
                    }
                },

                Body = new ContentView
                {
                    Padding = 10,
                    BackgroundColor = Color.White,
                    //Content = _actionEditor,
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Margin = new Thickness(10, 0, 10, 0),
                        //  HorizontalOptions = LayoutOptions.EndAndExpand,
                        // Children = { _actionEditorforDisapproved, _actionEditor1, pickerhalfDay }
                        Children = { _actionEditorforDisapproved, _actionEditorforDisapproved1 }
                    }
                },
                Footer = new ContentView
                {
                    BackgroundColor = Color.White,

                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness(10, 0, 10, 0),
                        Children = { actionButtonforDisapproved, closeButtonforDisapproved }
                    }
                }
            };

            //new PopupPageInitializer(this) { _popupforDisapproved };
            _popupforDisapproved.Tapped += _popupforDisapproved_Tapped;
            _popupforDisapproved.Showing += _popupforDisapproved_Showing;
            actionButtonforDisapproved.Clicked += actionButtonforDisapproved_Clicked;
            closeButtonforDisapproved.Clicked += closeButtonforDisapproved_Clicked;
            #endregion
        }
        protected async override void OnAppearing()
        {
            overlay.IsVisible = true;
            base.OnAppearing();
            stackButton.IsVisible = false;
            if (_strStatus != "")
            {
                stackButton.IsVisible = true;
                stackFirstTab.IsVisible = false;
                stackNextTab.IsVisible = true;
            }
            LeaveApprovalSubmit objLeaveApprovalSubmit = new LeaveApprovalSubmit();
            objLeaveApprovalSubmit.ReqId = _strrequisitionid;
            if (_StrEmpId.ToLower() == "self")
                objLeaveApprovalSubmit.EmployeeID = Convert.ToString(Application.Current.Properties["EmployeeId"]);
            else
                objLeaveApprovalSubmit.EmployeeID = _StrEmpId;
            objLeaveApprovalSubmit = await App.TodoManager.Get_PageLoad_LeaveAplication(objLeaveApprovalSubmit);
            _strGender = objLeaveApprovalSubmit.Gender;

            if (objLeaveApprovalSubmit.LeaveSubCategory != null)
            {
                _strEmployeeConfirmed = objLeaveApprovalSubmit.LeaveSubCategory[0].Confirmed;
                hdnJoinTime = objLeaveApprovalSubmit.LeaveSubCategory[0].JoinTime;
                hdnEmployeeConfirmed = objLeaveApprovalSubmit.LeaveSubCategory[0].Confirmed;
                hdnCategoryId = objLeaveApprovalSubmit.LeaveSubCategory[0].CategoryId;

                if (!string.IsNullOrEmpty(hdnCategoryId))
                {
                    if (Convert.ToInt32(hdnCategoryId) > 2 && Convert.ToInt32(objLeaveApprovalSubmit.ReqId) == 0)
                    {
                        if (objLeaveApprovalSubmit.LeaveSubCategory[0].ContractEndDate.ToUpper() == "Y")
                        {
                            btnSave.IsVisible = false;
                            btnApply.IsVisible = false;
                            await DisplayAlert("alert", "You do not have any leave balance kindly contact HR for any query", "Ok");
                        }
                    }
                }
            }
            if (objLeaveApprovalSubmit.LeaveSubApprovalList != null)
            {
                int indx = 0;
                foreach (var item in objLeaveApprovalSubmit.LeaveSubApprovalList)
                {
                    _leaveType.Add(item.Type, item.ID);
                    entLeaveType.Items.Add(item.Type);
                    if (objLeaveApprovalSubmit.LeavesubDetails_Edit != null)
                    {
                        if (objLeaveApprovalSubmit.LeavesubDetails_Edit.Count > 0 && Convert.ToInt32(objLeaveApprovalSubmit.LeavesubDetails_Edit[0].LeaveTypeID) == Convert.ToInt32(item.ID))
                            entLeaveType.SelectedIndex = indx;
                    }
                    indx++;
                }
            }
            listLeaveStatus.ItemsSource = objLeaveApprovalSubmit.LeaveSubTypeCategory;
            if (objLeaveApprovalSubmit.LeavesubDetails_Edit != null)
            {
                int count = objLeaveApprovalSubmit.LeavesubDetails_Edit.Count;
                if (count > 1)
                    count = 2;
                for (int j = 0; j < count; j++)
                {
                    if (j != 0)
                    {
                        //Bind grid LeaveType from to days. new 
                    }
                    else
                    {
                        //entLeaveType.Items.Add(objLeaveApprovalSubmit.LeavesubDetails_Edit[0].LeaveType);
                        DPDateFrom.Date = DateTime.ParseExact(objLeaveApprovalSubmit.LeavesubDetails_Edit[0].FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (objLeaveApprovalSubmit.LeavesubDetails_Edit.Count > 1)
                            DPDateTo.Date = Convert.ToDateTime(Convert.ToInt32(objLeaveApprovalSubmit.LeavesubDetails_Edit.Count) - 1);/*  .ToDate.);*/
                        else
                            DPDateTo.Date = DateTime.ParseExact(objLeaveApprovalSubmit.LeavesubDetails_Edit[0].ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        hdnLeaveId = objLeaveApprovalSubmit.LeavesubDetails_Edit[0].LeaveTypeID;
                        lblDays.Text = objLeaveApprovalSubmit.LeavesubDetails_Edit[0].Days;
                        EditReason.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].ReasonForLeave;
                        EditContactAddress.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].ContactAddress;
                        EntContactNumber.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].ContactPhoneNumber;
                        hdnRequisitionId = objLeaveApprovalSubmit.LeavesubDetails_Edit[0].ReqID;
                        hdnWorkFlowID = objLeaveApprovalSubmit.LeaveSubDetailsList[0].WorkFlowID;
                        slDays.IsVisible = true;
                        lblDays.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].Days;
                        DPDate.Date = DateTime.ParseExact(objLeaveApprovalSubmit.LeaveSubDetailsList[0].ReqDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        entApprover.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].ReportingManager;
                        EntCMail.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].ToCCMail;
                        EntCity.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].ContactCity;
                        if (objLeaveApprovalSubmit.LeaveSubDetailsList[0].ContactPin != "0")
                            EntPin.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].ContactPin;
                        else
                            EntPin.Text = "";
                        slDays.IsVisible = true;
                        if (objLeaveApprovalSubmit.LeaveSubDetailsList.Count > 1)
                        {
                            lblDays.Text = Convert.ToString(Convert.ToDecimal(Convert.ToString(objLeaveApprovalSubmit.LeaveSubDetailsList[j].Days)) + Convert.ToDecimal(Convert.ToString(objLeaveApprovalSubmit.LeaveSubDetailsList[j + 1].Days)));
                        }
                        else
                        {
                            lblDays.Text = objLeaveApprovalSubmit.LeaveSubDetailsList[0].Days;
                        }
                        hdnGender = objLeaveApprovalSubmit.LeaveSubDetailsList[0].Gender;

                        if (objLeaveApprovalSubmit.LeaveSubDetailsList[0].LeaveType.ToLower() == "SL")
                        {
                            div_MedicalCerti.IsVisible = true;
                            if (objLeaveApprovalSubmit.LeaveSubDetailsList[0].IsMedicalCertificate != null)
                            {
                                try
                                {
                                    pickerMedCer.SelectedIndex = Convert.ToInt32(objLeaveApprovalSubmit.LeaveSubDetailsList[0].IsMedicalCertificate);
                                }
                                catch
                                { }
                            }
                        }

                    }

                }
            }
            else
            {
                //Div_Days.Style.Add("display", "none");
                div_MedicalCerti.IsVisible = false;
                //DPDate.Date = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", DPDate.Date));
                //lblDate.Text = System.DateTime.Now.ToString("dddd,dd mmmm yyyy");
                slDays.IsVisible = false;
                div_MedicalCerti.IsVisible = false;
            }
            if (objLeaveApprovalSubmit != null)
            {
                entApprover.Text = Convert.ToString(objLeaveApprovalSubmit.ReportingManager);
                hdnGender = Convert.ToString(objLeaveApprovalSubmit.Gender);
                hdnDirectReport = Convert.ToString(objLeaveApprovalSubmit.DREmployeeID);
            }
            if (objLeaveApprovalSubmit.LeaveSubTypeCategory != null)
            {
                for (int k = 0; k < objLeaveApprovalSubmit.LeaveSubTypeCategory.Count; k++)
                {
                    if (k != objLeaveApprovalSubmit.LeaveSubTypeCategory.Count - 1)
                    {
                        //if (Convert.ToString(dtCalc.Rows[j]["Leave Type"]).ToLower() == "privilege leave" || Convert.ToString(dtCalc.Rows[j]["Leave Type"]).ToLower() == "leave")
                        if (objLeaveApprovalSubmit.LeaveSubTypeCategory[k].LeaveType.ToLower() == "privilege leave" || objLeaveApprovalSubmit.LeaveSubTypeCategory[k].LeaveType.ToLower() == "leave")
                        {
                            hdnOpeningBalance = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].OpeningBalance;
                            hdnTotalDays = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].TDays;
                            hdnAvailed = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].Availed;
                            hdnBalancePL = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].ClosingBalance;
                            hdnAccruded = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].Accrued;
                            hdnPLAppliedNotApproved = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].AppliedNotApproved;
                        }
                        else if (objLeaveApprovalSubmit.LeaveSubTypeCategory[k].LeaveType.ToLower() == "sick leave")
                        {
                            hdnSLOpeningBalance = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].OpeningBalance;
                            hdnSLTotalDays = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].TDays;
                            hdnSLAvailed = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].Availed;
                            hdnBalanceSL = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].ClosingBalance;
                            hdnSLAccruded = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].Accrued;
                            hdnSLAppliedNotApproved = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].AppliedNotApproved;
                            if (hdnEmployeeConfirmed == "N")
                                hdnSLClosingBalElibility = float.Parse(Convert.ToString(objLeaveApprovalSubmit.LeaveSubTypeCategory[k].OpeningBalance)) + float.Parse(Convert.ToString(objLeaveApprovalSubmit.LeaveSubTypeCategory[k].Accrued));
                            else
                                hdnSLClosingBalElibility = float.Parse(Convert.ToString(objLeaveApprovalSubmit.LeaveSubTypeCategory[k].OpeningBalance)) + float.Parse(Convert.ToString(objLeaveApprovalSubmit.LeaveSubTypeCategory[k].TDays));
                        }
                        else if (objLeaveApprovalSubmit.LeaveSubTypeCategory[k].LeaveType.ToLower() == "Opening Balance")
                        {
                            hdnCLOpeningBalance = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].OpeningBalance;
                            hdnCLTotalDays = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].TDays;
                            hdnCLAvailed = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].Availed;
                            hdnBalanceCL = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].ClosingBalance;
                            hdnCLAccruded = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].Accrued;
                            hdnCLAppliedNotApproved = objLeaveApprovalSubmit.LeaveSubTypeCategory[k].AppliedNotApproved;
                        }
                    }
                    else
                    { }
                }
            }
            DPDateFrom.DateSelected += DPDateFrom_DateSelected;
            DPDateTo.DateSelected += DPDateFrom_DateSelected;
            overlay.IsVisible = false;




        }

        #region "Popup for Half Day and Disprrove remarks"
        private async void actionButtonforDisapproved_Clicked(object sender, EventArgs e)
        {
            overlay.IsVisible = true;
            #region "Disapproval Case"
            LeaveApprovalSave objLeaveApprovalSave = new LeaveApprovalSave();
            string strCallEmailFor = string.Empty;
            try
            {
                string strTransact = string.Empty;
                objLeaveApprovalSave.Status = _strStatus;
                if (btnReject.Text == "Cancel")
                    if (_strStatus.ToLower() == "approved")
                        strTransact = "Rework";
                    else
                        strTransact = "Cancelled";
                else
                    if (_strStatus.ToLower() == "rework")
                    strTransact = "Approved";
                else
                    strTransact = "Disapproved";

                objLeaveApprovalSave.TransactionType = strTransact;
                strCallEmailFor = btnReject.Text;
                hdnCallFor = strTransact;

                objLeaveApprovalSave.LeaveID = hdnLeaveId;
                objLeaveApprovalSave.EmployeeID = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                objLeaveApprovalSave.LeaveType = Convert.ToString(_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]]);
                objLeaveApprovalSave.From = String.Format("{0:dd/MM/yyyy}", DPDateFrom.Date);
                objLeaveApprovalSave.To = String.Format("{0:dd/MM/yyyy}", DPDateTo.Date);
                objLeaveApprovalSave.ReasonLeave = EditReason.Text.Trim();
                objLeaveApprovalSave.ContactAddress = EditContactAddress.Text.Trim();
                objLeaveApprovalSave.ContactPhoneNum = EntContactNumber.Text.Trim();
                objLeaveApprovalSave.ApproveBy = null;
                objLeaveApprovalSave.ReasonCancel = _actionEditorforDisapproved1.Text;//popup textbox txtmodalReason
                objLeaveApprovalSave.CancelledBy = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                objLeaveApprovalSave.ReqID = hdnRequisitionId;
                objLeaveApprovalSave.WFlowID = hdnWorkFlowID;
                objLeaveApprovalSave.Days = "0";
                objLeaveApprovalSave.AppliedBy = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                objLeaveApprovalSave.SendDateTime = DateTime.Now.ToString("dd/MM/yyyy");
                objLeaveApprovalSave.halfStatus = halfStatus;
                objLeaveApprovalSave.SendToMulti = hdnDirectReport;
                objLeaveApprovalSave = await App.TodoManager.Save_LeaveApplicationForm(objLeaveApprovalSave);
                if (objLeaveApprovalSave.AlertMessage != "" || objLeaveApprovalSave.AlertMessage != null)
                {
                    await DisplayAlert("alert", objLeaveApprovalSave.AlertMessage, "Ok");
                    App.SetupRedirection(new LeaveApproval.PendingApprovalList("Disapproved", "hris"));
                    App.Current.MainPage = App.MasterDetailPage;
                }
                _popupforDisapproved.Hide();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //objLeaveApprovalSave = null;
                //strCallEmailFor = null;
            }
            #endregion
            overlay.IsVisible = false;
        }
        private async void closeButtonforDisapproved_Clicked(object sender, EventArgs e)
        {
            await _popupforDisapproved.HideAsync(async p =>
            {
                await p.FadeTo(0, 250, Easing.Linear);
                p.Opacity = 1;
            });
        }
        private void _popupforDisapproved_Showing(object sender, PopupShowingEventArgs e)
        {
            if (_preventShowing.On)
            {
                e.Cancel = true;
            }
        }
        private void _popupforDisapproved_Tapped(object sender, PopupTappedEventArgs e)
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
        private void ActionButton_Clicked(object sender, EventArgs e)
        {
            _strHalfDay = Convert.ToString(pickerhalfDay.SelectedIndex);
            CalculateDays();
            float TotalDays;
            double SubtractDaysForleave = 0.0;
            float ActualDay = Convert.ToInt32(lblDays.Text);
            if (_strHalfDay == "0")
            {
                SubtractDaysForleave = 0.5;
                halfStatus = "F";
            }
            else if (_strHalfDay == "1")
            {
                SubtractDaysForleave = 0.5;
                halfStatus = "L";
            }
            else if (_strHalfDay == "2")
            {
                SubtractDaysForleave = 1;
                halfStatus = "B";
            }
            TotalDays = float.Parse(Convert.ToString(ActualDay)) - float.Parse(Convert.ToString(SubtractDaysForleave));
            hdnNoOfDays = Convert.ToString(TotalDays);
            _popup1.Hide();
        }
        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await _popup1.HideAsync(async p =>
            {
                await p.FadeTo(0, 250, Easing.Linear);
                p.Opacity = 1;
            });
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
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
        #endregion
        protected void SetParameters()
        {
            var ChangeDayFormat = String.Format("{0:dd/MM/yyyy}", DPDate.Date);
            DateTime WhenStart = Convert.ToDateTime("01 / 01 / 2013");
            DateTime WhenEnd = Convert.ToDateTime("12 / 31 / 2013");
            DateTime ReqLastDate = Convert.ToDateTime("01 / 13 / 2013");
            int dateChangeDayFormat = DPDate.Date.Day;
            int MonthChangeDayFormat = DPDate.Date.Month;
            int YearChangeDayFormat = DPDate.Date.Year;
            ReqDate = new DateTime(YearChangeDayFormat, (MonthChangeDayFormat - 1), dateChangeDayFormat);

            int dateFrom = DPDateFrom.Date.Day;
            int MonthFrom = DPDateFrom.Date.Month;
            int YearFrom = DPDateFrom.Date.Year;
            FromDate = new DateTime(YearFrom, MonthFrom == 1 ? 1 : (MonthFrom - 1), dateFrom);

            int dateTo = DPDateTo.Date.Day;
            int MonthTo = DPDateTo.Date.Month;
            int YearTo = DPDateTo.Date.Year;
            ToDate = new DateTime(YearTo, (MonthTo - 1), dateTo);

            int dateWS = WhenStart.Date.Day;
            int MonthWS = WhenStart.Date.Month;
            int YearWS = WhenStart.Date.Year;

            StartDate = new DateTime(YearWS, MonthWS == 1 ? 1 : (MonthWS - 1), dateWS);
            int dateWL = WhenEnd.Date.Day;
            int MonthWL = WhenEnd.Date.Month;
            int YearWL = WhenEnd.Date.Year;
            LastDate = new DateTime(YearWL, MonthWL == 12 ? 12 : (MonthWL - 1), dateWL);
            int dateRLD = ReqLastDate.Date.Day;
            int MonthRLD = ReqLastDate.Date.Month;
            int YearRLD = ReqLastDate.Date.Year;
            RequestLastDate = new DateTime(YearRLD, MonthRLD == 1 ? 1 : (MonthRLD - 1), dateRLD);
        }
        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            #region "Validation "

            SetParameters();
            if (entLeaveType.SelectedIndex == -1 || EditReason.Text == null || lblFrom.Text == null || EditReason.Text == null)
            {
                #region "Validation "
                if (entLeaveType.SelectedIndex == -1)
                    StrMessage += "Please select leave type.\n";
                if (EditReason.Text == null)
                    StrMessage += "Please enter reason.\n";
                if (lblFrom.Text == null)
                    StrMessage += "Please enter from.\n";
                if (EntCMail.Text != "" && EntCMail.Text != null)
                {
                    bool isEmail = Regex.IsMatch(EntCMail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    if (!isEmail)
                    {
                        StrMessage += "CC Email is not valid \n ";
                    }
                }
                #endregion
            }
            else if (BlockCode("Save", ReqDate, RequestLastDate, FromDate, ToDate, StartDate, LastDate))
            {
                StrMessage += "You can not apply leave for 2013 \n";
            }
            else
            {
                #region "Multiple cases for leave type"
                if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 8 && _strGender == "M")
                    StrMessage += "Your are not allowed for leave \n";
                else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 9 && _strGender == "F")
                    StrMessage += "Your are not allowed for leave \n";
                else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 5)
                {
                    int dateFrom = DPDateFrom.Date.Day;
                    int MonthFrom = DPDateFrom.Date.Month;
                    int YearFrom = DPDateFrom.Date.Year;

                    int dateChangeDay = DPDate.Date.Day;
                    int MonthFromChangeDay = DPDate.Date.Month;
                    int YearFromChangeDay = DPDate.Date.Year;

                    DateTime oldDate = new DateTime(YearFromChangeDay, MonthFromChangeDay, dateChangeDay);
                    DateTime newDate = new DateTime(YearFrom, MonthFrom, dateFrom);
                    TimeSpan ts = newDate - oldDate;
                    int differenceInDays = ts.Days;
                    if (differenceInDays < 15)
                    {
                        await DisplayAlert("alert", "Please intimite before 15 days to apply for PL", "Ok");
                    }
                }
                else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 6)
                {
                    string strFrom = string.Empty;
                    string strTo = string.Empty;
                    strFrom = String.Format("{0:dd/MM/yyyy}", DPDate.Date);
                    strTo = String.Format("{0:dd/MM/yyyy}", DPDateTo.Date);
                    if (DateTime.Parse(strTo).Date > DateTime.Parse(strFrom).Date)
                    {
                        StrMessage += "You can not apply SL for future date\n";
                    }
                }
                else
                {
                    if (EditReason.Text == null)
                        StrMessage += "Please enter reason.\n";
                    if (lblFrom.Text == null)
                        StrMessage += "Please enter from.\n";
                    if (EditReason.Text == null)
                        StrMessage += "Please enter reason for leave.\n";

                    if (lblFrom.Text == null)
                        StrMessage += "Please select To date \n ";
                    //if (txtCCMail != "")
                    //{
                    //    if (validate(txtCCMail) == false)
                    //    {
                    //        StrMessage += ' CC Email is not valid \n ';
                    //    }
                    //}
                }
                #endregion
            }
            if (StrMessage != "" && StrMessage != null)
            {
                await DisplayAlert("Alert", StrMessage, "OK");
                StrMessage = null;
                //return;
            }
            else
            {
                #region "Validate Leave Type And Save"

                double LevOpeningBaL, TotalDays;
                double LevApplied, LevAvailed, LevAccruded;
                double LevBaLClosing = 0, LeaBalClosing, LevSLApplied, Valtemp;
                double ShowLeave, ShowLWP;
                string Bal, LevBaL = null, Availed, LeaveType = null, LevNBal = "";
                int NoOfDays = Convert.ToInt32(lblDays.Text);
                if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 5 || _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 6 || _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 7 || _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 10)
                {
                    if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 5)
                    {
                        #region "Leave Type ==5"
                        LeaveType = "PL";
                        _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                        LevOpeningBaL = hdnOpeningBalance;//from Pageload
                        TotalDays = hdnTotalDays;//from Pageload
                        LevAvailed = hdnAvailed;
                        LevAccruded = hdnAccruded;
                        if (_strEmployeeConfirmed == "N")
                            LevNBal = Convert.ToString(LevOpeningBaL + LevAccruded);
                        else
                            LevNBal = Convert.ToString(LevOpeningBaL + TotalDays);
                        /// Applied With Availed on 6 Dec 2012
                        LevApplied = hdnPLAppliedNotApproved;//from pageload
                        LevAvailed = LevAvailed + LevApplied;
                        /// Applied With Availed on 6 Dec 2012
                        if (LevAvailed == 0)
                            LevBaL = "" + LevAvailed;
                        else
                            LevBaL = Convert.ToString(LevAvailed);
                        LevBaLClosing = hdnBalancePL;
                        #endregion
                    }
                    else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 6)
                    {
                        #region "Leave Type ==6"
                        LeaveType = "SL";
                        string MsgLeaveType, MsgExecutiveNotCofirmed, MsgNonExecutiveNotCofirmed, MsgExecutiveCofirmed, MsgNonExecutiveCofirmed, MsgLwpExecutive, MsgLwpNonExecutive;
                        var year = (new DateTime()).Date;
                        //  Prompt for SL when apply these apply will come 
                        MsgExecutiveNotCofirmed = "You are applying for SL more than your available balance (opening balance + accrued till date). If approved, the system will deduct your complete (opening balance + accrued till date) and SL  over and above will be adjusted from PL and rest will be LWP.";
                        MsgNonExecutiveNotCofirmed = "You are applying for SL more than your available balance (opening balance + accrued till date). If approved, the system will deduct your complete (opening balance + accrued till date) and SL  over and above will be adjusted from PL,CL and rest will be LWP.";
                        MsgLwpExecutive = "You do not have any SL and PL balance. Your Sick leave application will be consider as LWP";
                        MsgLwpNonExecutive = "You do not have any SL,PL and CL balance. Your Sick leave application will be consider as LWP";
                        MsgExecutiveCofirmed = "You are applying for SL more than your available balance (accrued + carried forward). If approved, the system will deduct your complete accrued balance for ' + year.getFullYear() + ' + carry forward balance and SL over and above will be adjusted from PL and rest will be LWP.";
                        MsgNonExecutiveCofirmed = "You are applying for SL more than your available balance (accrued + carried forward). If approved, the system will deduct your complete accrued balance for ' + year.getFullYear() + ' + carry forward balance and SL over and above will be adjusted from PL,CL and rest will be LWP.";
                        //  Prompt for SL on 04 Dec 2012
                        _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                        if (NoOfDays > 3)
                            await DisplayAlert("Alert", "For SL more than 3 days, you are advised to submit the Medical Certificate to HR.", "OK");
                        LevOpeningBaL = hdnSLOpeningBalance;
                        Availed = Convert.ToString(hdnSLAvailed);
                        LeaBalClosing = hdnBalanceSL;// recheck
                        LevSLApplied = hdnSLAppliedNotApproved;
                        if (_strEmployeeConfirmed == "N")
                            TotalDays = hdnSLAccruded;
                        else
                            TotalDays = hdnSLTotalDays;
                        Bal = Convert.ToString(LevOpeningBaL + TotalDays);
                        string CategoryId = hdnCategoryId;
                        if (Bal != "" && Availed != "")
                        {
                            string year1 = DateTime.Now.Year.ToString();
                            Valtemp = Convert.ToDouble(Math.Abs(Convert.ToDouble(Bal))) - (float.Parse(Availed)) + Convert.ToDouble(Math.Abs(LevSLApplied));
                            /// Eligilble For PL
                            if (Convert.ToDouble(Valtemp) < NoOfDays)
                            {
                                /// MsgLeaveType = 'SL';

                                SLLeaveBalance = Convert.ToInt32(Valtemp);
                                hdnFinalLeave = Convert.ToString(SLLeaveBalance); // recheck
                                double exterLeave = NoOfDays - Valtemp;
                                LevOpeningBaLPLOrLL = hdnOpeningBalance;
                                LevAvailedPLOrLL = hdnAvailed;
                                LevPLApplied = hdnPLAppliedNotApproved;
                                if (_strEmployeeConfirmed == "N")
                                    TotalDaysPLOrLL = hdnAccruded;
                                else
                                    TotalDaysPLOrLL = hdnTotalDays;
                                LevPLApplied = hdnPLAppliedNotApproved;
                                LevAvailedPLOrLL = LevAvailedPLOrLL + LevPLApplied;
                                //////////////// Added on 12 Dec 2012 for PLApplied Will Count in Availd
                                BalPLorLL = LevOpeningBaLPLOrLL + TotalDaysPLOrLL;
                                ClosingPLorLL = BalPLorLL - LevAvailedPLOrLL;
                                if (Convert.ToDouble(ClosingPLorLL) < Convert.ToDouble(exterLeave))
                                {
                                    if (Convert.ToDouble(ClosingPLorLL) <= 0)
                                    {
                                        IsPL = 0;
                                        hdnIsPL = IsPL;
                                        //////// Added code on 06 Nove 2012
                                        if (CategoryId == "2")
                                        {
                                            LevOpeningBaLCL = hdnCLOpeningBalance;
                                            LevAvailedCL = hdnCLAvailed;
                                            if (_strEmployeeConfirmed == "N")
                                                TotalDaysCL = hdnCLAccruded;
                                            else
                                                TotalDaysCL = hdnCLTotalDays;
                                            LevCLApplied = hdnCLAppliedNotApproved;
                                            LevAvailedCL = Convert.ToDouble(LevAvailedCL) + Convert.ToDouble(LevCLApplied);
                                            //////////////// Added on 12 Dec 2012 for SLApplied Will Count in Availd
                                            BalCL = Convert.ToDouble(LevOpeningBaLCL) + Convert.ToDouble(TotalDaysCL);
                                            ClosingCL = Convert.ToDouble(BalCL) - Convert.ToDouble(LevAvailedCL);
                                            if (Convert.ToDouble(ClosingCL) < Convert.ToDouble(exterLeave))
                                            {
                                                if (Convert.ToDouble(ClosingCL) <= 0)
                                                {
                                                    ShowLWP = Convert.ToDouble(exterLeave);
                                                    hdnFinalLWP = Convert.ToString(ShowLWP);
                                                    if (SLLeaveBalance != 0)
                                                    {
                                                        if (_strEmployeeConfirmed == "N")
                                                        {
                                                            StrMessage += MsgNonExecutiveNotCofirmed;
                                                        }
                                                        else
                                                        {
                                                            StrMessage += MsgNonExecutiveCofirmed;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        StrMessage += MsgLwpNonExecutive;
                                                    }
                                                }
                                                else
                                                {
                                                    IsCL = ClosingCL;
                                                    ShowLWP = Convert.ToDouble(exterLeave) - Convert.ToDouble(IsCL);
                                                    hdnIsCL = IsCL;
                                                    hdnFinalLWP = Convert.ToString(ShowLWP);
                                                    //MsgLeaveType += ' PL,CL and rest will be LWP.';
                                                    if (_strEmployeeConfirmed == "N")
                                                    {
                                                        StrMessage += MsgNonExecutiveNotCofirmed;
                                                    }
                                                    else
                                                    {
                                                        StrMessage += MsgNonExecutiveCofirmed;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                IsCL = Convert.ToDouble(exterLeave);
                                                hdnIsCL = IsCL;
                                                if (_strEmployeeConfirmed == "N")
                                                {
                                                    StrMessage += MsgNonExecutiveNotCofirmed;
                                                }
                                                else
                                                {
                                                    StrMessage += MsgNonExecutiveCofirmed;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ShowLWP = Convert.ToDouble(exterLeave);
                                            hdnFinalLWP = Convert.ToString(ShowLWP);
                                            if (SLLeaveBalance != 0)
                                            {
                                                if (_strEmployeeConfirmed == "N")
                                                {
                                                    StrMessage += MsgExecutiveNotCofirmed;
                                                }
                                                else
                                                {
                                                    StrMessage += MsgExecutiveCofirmed;
                                                }
                                            }
                                            else
                                            {
                                                StrMessage += MsgLwpExecutive;
                                            }
                                        }
                                        /// End here Code ob 06 Nov 2012
                                    }
                                    else
                                    {
                                        IsPL = Convert.ToInt32(ClosingPLorLL);
                                        hdnIsPL = IsPL;
                                        exterLeave = Convert.ToDouble(exterLeave) - Convert.ToDouble(IsPL);
                                        if (CategoryId == "2")
                                        {
                                            LevOpeningBaLCL = hdnCLOpeningBalance;
                                            LevAvailedCL = hdnCLAvailed;
                                            if (_strEmployeeConfirmed == "N")
                                                TotalDaysCL = hdnCLAccruded;
                                            else
                                                TotalDaysCL = hdnCLTotalDays;
                                            LevCLApplied = hdnCLAppliedNotApproved;
                                            LevAvailedCL = Convert.ToDouble(LevAvailedCL) + Convert.ToDouble(LevCLApplied);
                                            BalCL = Convert.ToDouble(LevOpeningBaLCL) + Convert.ToDouble(TotalDaysCL);
                                            ClosingCL = Convert.ToDouble(BalCL) - Convert.ToDouble(LevAvailedCL);
                                            if (Convert.ToDouble(ClosingCL) < Convert.ToDouble(exterLeave))
                                            {
                                                if (Convert.ToDouble(ClosingCL) <= 0)
                                                {
                                                    ShowLWP = Convert.ToDouble(exterLeave);
                                                    hdnFinalLWP = Convert.ToString(ShowLWP);
                                                    if (IsPL != 0 || SLLeaveBalance != 0 || IsCL != 0)
                                                    {
                                                        if (_strEmployeeConfirmed == "N")
                                                        {
                                                            StrMessage += MsgNonExecutiveNotCofirmed;
                                                        }
                                                        else
                                                        {
                                                            StrMessage += MsgNonExecutiveCofirmed;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        StrMessage += MsgLwpNonExecutive;
                                                    }
                                                }
                                                else
                                                {
                                                    IsCL = ClosingCL;
                                                    ShowLWP = Convert.ToDouble(exterLeave) - Convert.ToDouble(IsCL);
                                                    hdnIsCL = IsCL;
                                                    hdnFinalLWP = Convert.ToString(ShowLWP);
                                                    if (IsPL != 0 || SLLeaveBalance != 0 || IsCL != 0)
                                                    {
                                                        if (_strEmployeeConfirmed == "N")
                                                        {
                                                            StrMessage += MsgNonExecutiveNotCofirmed;
                                                        }
                                                        else
                                                        {
                                                            StrMessage += MsgNonExecutiveCofirmed;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        StrMessage += MsgLwpNonExecutive;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                IsCL = Convert.ToDouble(exterLeave);
                                                hdnIsCL = IsCL;
                                                if (_strEmployeeConfirmed == "N")
                                                {
                                                    StrMessage += MsgNonExecutiveNotCofirmed;
                                                }
                                                else
                                                {
                                                    StrMessage += MsgNonExecutiveCofirmed;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ShowLWP = Convert.ToDouble(exterLeave);
                                            hdnFinalLWP = Convert.ToString(ShowLWP);
                                            if (IsPL != 0 || SLLeaveBalance != 0)
                                            {
                                                if (_strEmployeeConfirmed == "N")
                                                {
                                                    StrMessage += MsgExecutiveNotCofirmed;
                                                }
                                                else
                                                {
                                                    StrMessage += MsgExecutiveCofirmed;
                                                }
                                            }
                                            else
                                            {
                                                StrMessage += MsgLwpExecutive;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 7)
                    {
                        #region "LeaveType==7"
                        LeaveType = "CL";
                        _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                        LevOpeningBaL = hdnCLOpeningBalance;
                        TotalDays = hdnCLTotalDays;
                        LevAvailed = hdnCLAvailed;
                        LevAccruded = hdnCLAccruded;
                        if (_strEmployeeConfirmed == "N")
                            LevNBal = Convert.ToString(Convert.ToDouble(LevOpeningBaL) + Convert.ToDouble(LevAccruded));
                        else
                            LevNBal = Convert.ToString(Convert.ToDouble(LevOpeningBaL) + Convert.ToDouble(TotalDays));
                        // Added on 06 Dec 2012 for CLApplied Will Count in Availd
                        LevCLApplied = Convert.ToDouble(hdnCLAppliedNotApproved);
                        LevAvailed = Convert.ToDouble(LevAvailed) + Convert.ToDouble(LevCLApplied);
                        if (LevAvailed == 0)
                            LevBaL = "" + LevAvailed;
                        else
                            LevBaL = Convert.ToString(LevAvailed);
                        LevBaLClosing = hdnBalanceCL;
                        #endregion
                    }
                    else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 10)
                    {
                        #region "Leavetype==10"
                        LeaveType = "LL";
                        _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]]; ;
                        LevOpeningBaL = hdnOpeningBalance;
                        TotalDays = hdnTotalDays;
                        LevAvailed = hdnAvailed;
                        LevNBal = Convert.ToString(Convert.ToDouble(LevOpeningBaL) + Convert.ToDouble(TotalDays));
                        LevApplied = hdnPLAppliedNotApproved;
                        LevAvailed = Convert.ToDouble(LevAvailed) + Convert.ToDouble(LevApplied);
                        if (LevAvailed == 0)
                            LevBaL = "" + LevAvailed;
                        else
                            LevBaL = Convert.ToString(LevAvailed);
                        LevBaLClosing = hdnBalancePL;
                        #endregion
                    }


                    if (LevBaL != "" && LevNBal != "")
                    {
                        Valtemp = Convert.ToDouble(LevNBal) - float.Parse(LevBaL);
                        if (Convert.ToDouble(Valtemp) <= float.Parse("0"))
                        {
                            hdnFinalLeave = Convert.ToString(NoOfDays);
                            _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] = 11;
                            int elementRef = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                            if (elementRef == 11)
                            {
                                //entLeaveType.SelectedIndex
                            }
                            //for (var i = 0; i < elementRef.options.length; i++)
                            //{
                            //    if (elementRef.options[i].value == "11")
                            //    {
                            //        elementRef.options[i].selected = true;
                            //    }
                            //}
                            StrMessage += "You do not have any " + LeaveType + " balance. Your leave application will be consider as LWP";
                        }
                        else
                        {
                            if (Convert.ToDouble(NoOfDays) > Valtemp)
                            {
                                ShowLeave = Valtemp;
                                ShowLWP = Convert.ToDouble(NoOfDays) - Convert.ToDouble(Valtemp);
                            }
                            else
                            {
                                ShowLeave = NoOfDays;
                                ShowLWP = 0;
                            }
                            //if (ShowLeave)
                            if (hdnEmployeeConfirmed == "N" && LeaveType != "LL")
                            {
                                hdnFinalLeave = Convert.ToString(ShowLeave);
                                if (LeaveType == "PL")
                                    StrMessage += "Under probation, " + LeaveType + " can be availled only in case of emergency and is to be approved by the Reporting Manager as well as HOD";
                                if (ShowLWP != 0)
                                {
                                    if (LeaveType == "PL" || LeaveType == "CL" || LeaveType == "LL")
                                    {
                                        StrMessage += "You are applying for " + LeaveType + " more than your available balance (opening balance + accrued till date). If approved, the system will deduct your complete (opening balance + accrued till date) and " + LeaveType + " over and above will be treated as LWP.";
                                        hdnFinalLWP = Convert.ToString(ShowLWP);
                                    }
                                }
                            }
                            else if (hdnEmployeeConfirmed == "Y")
                            {
                                hdnFinalLeave = Convert.ToString(ShowLeave);
                                string year = DateTime.Now.Year.ToString();
                                if (ShowLWP != 0)
                                {
                                    if (LeaveType == "PL" || LeaveType == "CL" || LeaveType == "LL")
                                        hdnFinalLWP = Convert.ToString(ShowLWP);
                                }
                                else
                                {
                                    if ((int)LevBaLClosing < NoOfDays)
                                        StrMessage += "You are applying for " + LeaveType + " more than your available balance (accrued + carried forward). If approved, the system will deduct your complete accrued balance for " + year + " carry forward balance leaving you with a negative balance.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    int JobPeriod = 0;
                    JobPeriod = hdnJoinTime;
                    NoOfDays = Convert.ToInt32(lblDays.Text);
                    if (hdnEmployeeConfirmed == "N")
                    {
                        if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 8)
                        {
                            if (JobPeriod <= 4 && NoOfDays > 90)
                            {
                                await DisplayAlert("alert", "ML can not be applied for more than 90 days", "Ok");
                            }
                            else
                            {
                                if (NoOfDays > 90)
                                {
                                    await DisplayAlert("alert", "ML can not be applied for more than 90 days", "Ok");
                                }
                                else
                                {
                                    StrMessage += "Your ML will be considered as LWP";
                                    hdnFinalLeave = Convert.ToString(NoOfDays);
                                    _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                                    var elementRef = _hdnActualLeaveType;
                                    if (_hdnActualLeaveType == 11)
                                    {
                                    }
                                    //for (var i = 0; i < entLeaveType.length; i++)
                                    //{
                                    //    if (entLeaveType.options[i].value == "11")
                                    //    {
                                    //        entLeaveType.options[i].selected = true;
                                    //    }
                                    //}
                                }
                            }
                        }
                        else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 9)
                        {
                            if (NoOfDays > 14)
                            {
                                await DisplayAlert("alert", "PTL can not be applied for more than 14 days", "Ok");
                            }
                            else
                            {
                                hdnFinalLeave = Convert.ToString(NoOfDays);
                            }
                        }
                    }
                    else
                    {
                        if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 8)
                        {
                            if (NoOfDays > 90)
                            {
                                await DisplayAlert("alert", "ML can not be applied for more than 90 days", "Ok");
                            }
                            else
                            {
                                hdnFinalLeave = Convert.ToString(NoOfDays);
                            }
                        }
                        if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 9)
                        {
                            if (NoOfDays > 14)
                            {
                                StrMessage += "PTL can not be applied for more than 14 days";
                            }
                            else
                            {
                                hdnFinalLeave = Convert.ToString(NoOfDays);
                            }
                        }
                    }
                }
                if (StrMessage != "" && StrMessage != null)
                {
                    await DisplayAlert("alert", StrMessage, "Ok");
                    return;
                }
                else
                {
                    overlay.IsVisible = true;
                    #region "Saving Data"
                    LeaveApprovalSave objLeaveApprovalSave = new LeaveApprovalSave();
                    objLeaveApprovalSave.LeaveID = 0;// LeaveID;
                    objLeaveApprovalSave.EmployeeID = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(_hdnActualLeaveType)))
                    {
                        objLeaveApprovalSave.ActLeaveType = Convert.ToString(_hdnActualLeaveType);
                    }
                    else
                    {
                        objLeaveApprovalSave.ActLeaveType = "0";
                    }
                    objLeaveApprovalSave.LeaveType = Convert.ToString(_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]]);
                    //if(entLeaveType.se)
                    objLeaveApprovalSave.From = String.Format("{0:dd/MM/yyyy}", DPDateFrom.Date);// Convert.ToString(DPDateFrom.Date);
                    objLeaveApprovalSave.To = String.Format("{0:dd/MM/yyyy}", DPDateTo.Date);//Convert.ToString(DPDateTo.Date);
                    objLeaveApprovalSave.ReasonLeave = EditReason.Text;
                    objLeaveApprovalSave.ContactAddress = EditContactAddress.Text;
                    objLeaveApprovalSave.ContactPhoneNum = EntContactNumber.Text;
                    objLeaveApprovalSave.ApproveBy = null;
                    objLeaveApprovalSave.CancelledBy = null;
                    objLeaveApprovalSave.ReasonCancel = null;
                    objLeaveApprovalSave.TransactionType = "Saved";
                    objLeaveApprovalSave.WFlowID = null;
                    objLeaveApprovalSave.ReqID = 0;
                    if (lblDays.Text.Trim() != "")
                        objLeaveApprovalSave.Days = lblDays.Text.Trim();
                    else
                        if (lblDays.Text.Trim() != null && lblDays.Text.Trim() != "")
                        objLeaveApprovalSave.Days = lblDays.Text;

                    objLeaveApprovalSave.ContactCity = EntCity.Text;
                    if (EntPin.Text != "" || EntPin.Text != null)
                        objLeaveApprovalSave.ContactPin = Convert.ToInt32(EntPin.Text);
                    if (EntCMail.Text != "" || EntCMail.Text != null)
                    {
                        objLeaveApprovalSave.ToCCMail = EntCMail.Text.Trim();
                    }
                    else
                    {
                        objLeaveApprovalSave.ToCCMail = null;
                    }
                    if (Convert.ToString(pickerMedCer.SelectedIndex) == "-1")
                        objLeaveApprovalSave.IsMedicalCertificate = null;
                    else
                        objLeaveApprovalSave.IsMedicalCertificate = Convert.ToString(pickerMedCer.SelectedIndex);
                    objLeaveApprovalSave.SendDateTime = DateTime.Now.ToString();
                    objLeaveApprovalSave.AppliedBy = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                    objLeaveApprovalSave.SendToMulti = hdnDirectReport;
                    if (hdnFinalLWP != null)
                    {
                        objLeaveApprovalSave.ISLWP = "1";
                        objLeaveApprovalSave.LWPDays = hdnFinalLWP;
                    }
                    objLeaveApprovalSave.halfStatus = halfStatus;

                    if (lblLeaveType.Text == "6")
                        objLeaveApprovalSave.fIsSL = Convert.ToInt32(hdnFinalLWP);
                    else
                        objLeaveApprovalSave.fIsSL = 0;
                    if (hdnIsPL != null)
                        objLeaveApprovalSave.fIsPL = Convert.ToInt32(hdnIsPL);
                    else
                        objLeaveApprovalSave.fIsPL = 0;
                    if (hdnIsCL != null)
                        objLeaveApprovalSave.fIsCL = Convert.ToInt32(hdnIsCL);
                    else
                        objLeaveApprovalSave.fIsCL = 0;

                    objLeaveApprovalSave.ActionFrom = null;
                    objLeaveApprovalSave.DirectReportTo = null;
                    objLeaveApprovalSave.AdminReport = null;
                    objLeaveApprovalSave.Status = "Saved";
                    objLeaveApprovalSave.ApprovedDate = null;
                    objLeaveApprovalSave.DeptId = null;
                    objLeaveApprovalSave = await App.TodoManager.Save_LeaveApplicationForm(objLeaveApprovalSave);

                    if (objLeaveApprovalSave.AlertMessage == "" || objLeaveApprovalSave.AlertMessage == null)
                    {
                        await DisplayAlert("alert", "Your leave has been saved.", "Ok");
                        App.SetupRedirection(new LeaveApproval.PendingApprovalList("All", "emp"));
                        App.Current.MainPage = App.MasterDetailPage;
                    }
                    #endregion
                    overlay.IsVisible = false;
                }
                #endregion
            }
            #endregion

        }
        private async void btnReject_Clicked(object sender, EventArgs e)
        {
            LeaveApprovalSave objLeaveApprovalSave = new LeaveApprovalSave();
            string strCallEmailFor = string.Empty;
            if (_showingAnimation.On)
            {
                double original;
                await _popupforDisapproved.ShowAsync(async p =>
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
                _popupforDisapproved.Show();
        }

        private async void btnApprove_Clicked(object sender, EventArgs e)
        {
            overlay.IsVisible = true;

            #region "Approval Process"
            LeaveApprovalSave objLeaveApprovalSave = new LeaveApprovalSave();
            try
            {
                objLeaveApprovalSave.Status = _strStatus.Trim();
                if (_strStatus.ToLower() == "rework")
                {
                    objLeaveApprovalSave.TransactionType = "Cancelled";
                    hdnCallFor = "Cancelled";
                }
                else
                {
                    objLeaveApprovalSave.TransactionType = "Approved";
                    hdnCallFor = "Approved";
                }
                objLeaveApprovalSave.LeaveID = hdnLeaveId;
                objLeaveApprovalSave.EmployeeID = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                objLeaveApprovalSave.LeaveType = Convert.ToString(_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]]);
                objLeaveApprovalSave.From = String.Format("{0:dd/MM/yyyy}", DPDateFrom.Date);
                objLeaveApprovalSave.To = String.Format("{0:dd/MM/yyyy}", DPDateTo.Date);
                objLeaveApprovalSave.ReasonLeave = EditReason.Text.Trim();
                objLeaveApprovalSave.ContactAddress = EditContactAddress.Text.Trim();
                objLeaveApprovalSave.ContactPhoneNum = EntContactNumber.Text.Trim();
                objLeaveApprovalSave.ApproveBy = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                objLeaveApprovalSave.CancelledBy = null;
                objLeaveApprovalSave.ReasonCancel = null;
                objLeaveApprovalSave.ReqID = hdnRequisitionId;
                objLeaveApprovalSave.WFlowID = hdnWorkFlowID;
                if (lblDays.Text != "0")
                    objLeaveApprovalSave.Days = lblDays.Text.Trim();
                else
                    objLeaveApprovalSave.Days = "0";
                objLeaveApprovalSave.AppliedBy = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                objLeaveApprovalSave.SendDateTime = DateTime.Now.ToString("dd/MM/yyyy");
                objLeaveApprovalSave.CancelledBy = null;
                objLeaveApprovalSave.SendToMulti = hdnDirectReport;
                objLeaveApprovalSave.halfStatus = halfStatus;
                objLeaveApprovalSave = await App.TodoManager.Save_LeaveApplicationForm(objLeaveApprovalSave);
                if (objLeaveApprovalSave.AlertMessage != "" || objLeaveApprovalSave.AlertMessage != null)
                {
                    await DisplayAlert("alert", objLeaveApprovalSave.AlertMessage, "Ok");
                    App.SetupRedirection(new LeaveApproval.PendingApprovalList("Approved", "hris"));
                    App.Current.MainPage = App.MasterDetailPage;
                }
            }
            catch
            {

            }
            finally
            { }
            #endregion
            overlay.IsVisible = false;


        }

        public bool BlockCode(string ButtonName, DateTime ReqDate, DateTime RequestLastDate, DateTime FromDate, DateTime ToDate, DateTime StartDate, DateTime LastDate)
        {
            SetParameters();
            if ((ButtonName == "Submit") && (Convert.ToInt32(ReqDate.Date) < Convert.ToInt32(RequestLastDate.Date)))
            {
                if ((Convert.ToInt32(FromDate.Date) >= Convert.ToInt32(StartDate.Date) && Convert.ToInt32(FromDate.Date) <= Convert.ToInt32(LastDate)) || (Convert.ToInt32(ToDate.Date) >= Convert.ToInt32(StartDate.Date) && Convert.ToInt32(ToDate.Date) <= Convert.ToInt32(LastDate.Date)))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        private async void btnApply_Clicked(object sender, EventArgs e)
        {
            #region "Validation "

            SetParameters();
            if (entLeaveType.SelectedIndex == -1 || EditReason.Text == null || lblFrom.Text == null || EditReason.Text == null)
            {
                #region "Validation "
                if (entLeaveType.SelectedIndex == -1)
                    StrMessage += "Please select leave type.\n";
                if (EditReason.Text == null)
                    StrMessage += "Please enter reason.\n";
                if (lblFrom.Text == null)
                    StrMessage += "Please enter from.\n";
                if (EntCMail.Text != "")
                {
                    bool isEmail = Regex.IsMatch(EntCMail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    if (!isEmail)
                    {
                        StrMessage += "CC Email is not valid \n ";
                    }
                }
                #endregion
            }
            else if (BlockCode("Save", ReqDate, RequestLastDate, FromDate, ToDate, StartDate, LastDate))
            {
                StrMessage += "You can not apply leave for 2013 \n";
            }
            else
            {
                #region "Multiple cases for leave type"
                if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 8 && _strGender == "M")
                    StrMessage += "Your are not allowed for leave \n";
                else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 9 && _strGender == "F")
                    StrMessage += "Your are not allowed for leave \n";
                else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 5)
                {
                    int dateFrom = DPDateFrom.Date.Day;
                    int MonthFrom = DPDateFrom.Date.Month;
                    int YearFrom = DPDateFrom.Date.Year;

                    int dateChangeDay = DPDate.Date.Day;
                    int MonthFromChangeDay = DPDate.Date.Month;
                    int YearFromChangeDay = DPDate.Date.Year;

                    DateTime oldDate = new DateTime(YearFromChangeDay, MonthFromChangeDay, dateChangeDay);
                    DateTime newDate = new DateTime(YearFrom, MonthFrom, dateFrom);
                    TimeSpan ts = newDate - oldDate;
                    int differenceInDays = ts.Days;
                    if (differenceInDays < 15)
                    {
                        await DisplayAlert("alert", "Please intimite before 15 days to apply for PL", "Ok");
                    }

                }

                else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 6)
                {
                    string strFrom = string.Empty;
                    string strTo = string.Empty;
                    strFrom = String.Format("{0:dd/MM/yyyy}", DPDate.Date);
                    strTo = String.Format("{0:dd/MM/yyyy}", DPDateTo.Date);
                    if (DateTime.Parse(strTo).Date > DateTime.Parse(strFrom).Date)
                    {
                        StrMessage += "You can not apply SL for future date\n";
                    }
                }
                else
                {
                    if (EditReason.Text == null)
                        StrMessage += "Please enter reason.\n";
                    if (lblFrom.Text == null)
                        StrMessage += "Please enter from.\n";
                    if (EditReason.Text == null)
                        StrMessage += "Please enter reason for leave.\n";

                    if (lblFrom.Text == null)
                        StrMessage += "Please select To date \n ";
                    //if (txtCCMail != "")
                    //{
                    //    if (validate(txtCCMail) == false)
                    //    {
                    //        StrMessage += ' CC Email is not valid \n ';
                    //    }
                    //}
                }
                #endregion
            }
            if (StrMessage != "" && StrMessage != null)
            {
                await DisplayAlert("Alert", StrMessage, "OK");
                StrMessage = null;
                //return;
            }
            else
            {
                #region "Validate Leave Type And Save"

                double LevOpeningBaL, TotalDays;
                double LevApplied, LevAvailed, LevAccruded;
                double LevBaLClosing = 0, LeaBalClosing, LevSLApplied, Valtemp;
                double ShowLeave, ShowLWP;
                string Bal, LevBaL = null, Availed, LeaveType = null, LevNBal = "";
                int NoOfDays = Convert.ToInt32(lblDays.Text);
                if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 5 || _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 6 || _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 7 || _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 10)
                {
                    if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 5)
                    {
                        #region "Leave Type ==5"
                        LeaveType = "PL";
                        _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                        LevOpeningBaL = hdnOpeningBalance;//from Pageload
                        TotalDays = hdnTotalDays;//from Pageload
                        LevAvailed = hdnAvailed;
                        LevAccruded = hdnAccruded;
                        if (_strEmployeeConfirmed == "N")
                            LevNBal = Convert.ToString(LevOpeningBaL + LevAccruded);
                        else
                            LevNBal = Convert.ToString(LevOpeningBaL + TotalDays);
                        /// Applied With Availed on 6 Dec 2012
                        LevApplied = hdnPLAppliedNotApproved;//from pageload
                        LevAvailed = LevAvailed + LevApplied;
                        /// Applied With Availed on 6 Dec 2012
                        if (LevAvailed == 0)
                            LevBaL = "" + LevAvailed;
                        else
                            LevBaL = Convert.ToString(LevAvailed);
                        LevBaLClosing = hdnBalancePL;
                        #endregion
                    }
                    else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 6)
                    {
                        #region "Leave Type ==6"
                        LeaveType = "SL";
                        string MsgLeaveType, MsgExecutiveNotCofirmed, MsgNonExecutiveNotCofirmed, MsgExecutiveCofirmed, MsgNonExecutiveCofirmed, MsgLwpExecutive, MsgLwpNonExecutive;
                        var year = (new DateTime()).Date;
                        //  Prompt for SL when apply these apply will come 
                        MsgExecutiveNotCofirmed = "You are applying for SL more than your available balance (opening balance + accrued till date). If approved, the system will deduct your complete (opening balance + accrued till date) and SL  over and above will be adjusted from PL and rest will be LWP.";
                        MsgNonExecutiveNotCofirmed = "You are applying for SL more than your available balance (opening balance + accrued till date). If approved, the system will deduct your complete (opening balance + accrued till date) and SL  over and above will be adjusted from PL,CL and rest will be LWP.";
                        MsgLwpExecutive = "You do not have any SL and PL balance. Your Sick leave application will be consider as LWP";
                        MsgLwpNonExecutive = "You do not have any SL,PL and CL balance. Your Sick leave application will be consider as LWP";
                        MsgExecutiveCofirmed = "You are applying for SL more than your available balance (accrued + carried forward). If approved, the system will deduct your complete accrued balance for ' + year.getFullYear() + ' + carry forward balance and SL over and above will be adjusted from PL and rest will be LWP.";
                        MsgNonExecutiveCofirmed = "You are applying for SL more than your available balance (accrued + carried forward). If approved, the system will deduct your complete accrued balance for ' + year.getFullYear() + ' + carry forward balance and SL over and above will be adjusted from PL,CL and rest will be LWP.";
                        //  Prompt for SL on 04 Dec 2012
                        _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                        if (NoOfDays > 3)
                            await DisplayAlert("Alert", "For SL more than 3 days, you are advised to submit the Medical Certificate to HR.", "OK");
                        LevOpeningBaL = hdnSLOpeningBalance;
                        Availed = Convert.ToString(hdnSLAvailed);
                        LeaBalClosing = hdnBalanceSL;// recheck
                        LevSLApplied = hdnSLAppliedNotApproved;
                        if (_strEmployeeConfirmed == "N")
                            TotalDays = hdnSLAccruded;
                        else
                            TotalDays = hdnSLTotalDays;
                        Bal = Convert.ToString(LevOpeningBaL + TotalDays);
                        string CategoryId = hdnCategoryId;
                        if (Bal != "" && Availed != "")
                        {
                            string year1 = DateTime.Now.Year.ToString();
                            Valtemp = Convert.ToDouble(Math.Abs(Convert.ToDouble(Bal))) - (float.Parse(Availed)) + Convert.ToDouble(Math.Abs(LevSLApplied));
                            /// Eligilble For PL
                            if (Convert.ToDouble(Valtemp) < NoOfDays)
                            {
                                /// MsgLeaveType = 'SL';
                                SLLeaveBalance = Convert.ToInt32(Valtemp);
                                hdnFinalLeave = Convert.ToString(SLLeaveBalance); // recheck
                                double exterLeave = NoOfDays - Valtemp;
                                LevOpeningBaLPLOrLL = hdnOpeningBalance;
                                LevAvailedPLOrLL = hdnAvailed;
                                LevPLApplied = hdnPLAppliedNotApproved;
                                if (_strEmployeeConfirmed == "N")
                                    TotalDaysPLOrLL = hdnAccruded;
                                else
                                    TotalDaysPLOrLL = hdnTotalDays;
                                LevPLApplied = hdnPLAppliedNotApproved;
                                LevAvailedPLOrLL = LevAvailedPLOrLL + LevPLApplied;
                                //////////////// Added on 12 Dec 2012 for PLApplied Will Count in Availd
                                BalPLorLL = LevOpeningBaLPLOrLL + TotalDaysPLOrLL;
                                ClosingPLorLL = BalPLorLL - LevAvailedPLOrLL;
                                if (Convert.ToDouble(ClosingPLorLL) < Convert.ToDouble(exterLeave))
                                {
                                    if (Convert.ToDouble(ClosingPLorLL) <= 0)
                                    {
                                        IsPL = 0;
                                        hdnIsPL = IsPL;
                                        //////// Added code on 06 Nove 2012
                                        if (CategoryId == "2")
                                        {
                                            LevOpeningBaLCL = hdnCLOpeningBalance;
                                            LevAvailedCL = hdnCLAvailed;
                                            if (_strEmployeeConfirmed == "N")
                                                TotalDaysCL = hdnCLAccruded;
                                            else
                                                TotalDaysCL = hdnCLTotalDays;
                                            LevCLApplied = hdnCLAppliedNotApproved;
                                            LevAvailedCL = Convert.ToDouble(LevAvailedCL) + Convert.ToDouble(LevCLApplied);
                                            //////////////// Added on 12 Dec 2012 for SLApplied Will Count in Availd
                                            BalCL = Convert.ToDouble(LevOpeningBaLCL) + Convert.ToDouble(TotalDaysCL);
                                            ClosingCL = Convert.ToDouble(BalCL) - Convert.ToDouble(LevAvailedCL);
                                            if (Convert.ToDouble(ClosingCL) < Convert.ToDouble(exterLeave))
                                            {
                                                if (Convert.ToDouble(ClosingCL) <= 0)
                                                {
                                                    ShowLWP = Convert.ToDouble(exterLeave);
                                                    hdnFinalLWP = Convert.ToString(ShowLWP);
                                                    if (SLLeaveBalance != 0)
                                                    {
                                                        if (_strEmployeeConfirmed == "N")
                                                        {
                                                            StrMessage += MsgNonExecutiveNotCofirmed;
                                                        }
                                                        else
                                                        {
                                                            StrMessage += MsgNonExecutiveCofirmed;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        StrMessage += MsgLwpNonExecutive;
                                                    }
                                                }
                                                else
                                                {
                                                    IsCL = ClosingCL;
                                                    ShowLWP = Convert.ToDouble(exterLeave) - Convert.ToDouble(IsCL);
                                                    hdnIsCL = IsCL;
                                                    hdnFinalLWP = Convert.ToString(ShowLWP);
                                                    //MsgLeaveType += ' PL,CL and rest will be LWP.';
                                                    if (_strEmployeeConfirmed == "N")
                                                    {
                                                        StrMessage += MsgNonExecutiveNotCofirmed;
                                                    }
                                                    else
                                                    {
                                                        StrMessage += MsgNonExecutiveCofirmed;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                IsCL = Convert.ToDouble(exterLeave);
                                                hdnIsCL = IsCL;
                                                if (_strEmployeeConfirmed == "N")
                                                {
                                                    StrMessage += MsgNonExecutiveNotCofirmed;
                                                }
                                                else
                                                {
                                                    StrMessage += MsgNonExecutiveCofirmed;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ShowLWP = Convert.ToDouble(exterLeave);
                                            hdnFinalLWP = Convert.ToString(ShowLWP);
                                            if (SLLeaveBalance != 0)
                                            {
                                                if (_strEmployeeConfirmed == "N")
                                                {
                                                    StrMessage += MsgExecutiveNotCofirmed;
                                                }
                                                else
                                                {
                                                    StrMessage += MsgExecutiveCofirmed;
                                                }
                                            }
                                            else
                                            {
                                                StrMessage += MsgLwpExecutive;
                                            }
                                        }
                                        /// End here Code ob 06 Nov 2012
                                    }
                                    else
                                    {
                                        IsPL = Convert.ToInt32(ClosingPLorLL);
                                        hdnIsPL = IsPL;
                                        exterLeave = Convert.ToDouble(exterLeave) - Convert.ToDouble(IsPL);
                                        if (CategoryId == "2")
                                        {
                                            LevOpeningBaLCL = hdnCLOpeningBalance;
                                            LevAvailedCL = hdnCLAvailed;
                                            if (_strEmployeeConfirmed == "N")
                                                TotalDaysCL = hdnCLAccruded;
                                            else
                                                TotalDaysCL = hdnCLTotalDays;
                                            LevCLApplied = hdnCLAppliedNotApproved;
                                            LevAvailedCL = Convert.ToDouble(LevAvailedCL) + Convert.ToDouble(LevCLApplied);
                                            BalCL = Convert.ToDouble(LevOpeningBaLCL) + Convert.ToDouble(TotalDaysCL);
                                            ClosingCL = Convert.ToDouble(BalCL) - Convert.ToDouble(LevAvailedCL);
                                            if (Convert.ToDouble(ClosingCL) < Convert.ToDouble(exterLeave))
                                            {
                                                if (Convert.ToDouble(ClosingCL) <= 0)
                                                {
                                                    ShowLWP = Convert.ToDouble(exterLeave);
                                                    hdnFinalLWP = Convert.ToString(ShowLWP);
                                                    if (IsPL != 0 || SLLeaveBalance != 0 || IsCL != 0)
                                                    {
                                                        if (_strEmployeeConfirmed == "N")
                                                        {
                                                            StrMessage += MsgNonExecutiveNotCofirmed;
                                                        }
                                                        else
                                                        {
                                                            StrMessage += MsgNonExecutiveCofirmed;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        StrMessage += MsgLwpNonExecutive;
                                                    }
                                                }
                                                else
                                                {
                                                    IsCL = ClosingCL;
                                                    ShowLWP = Convert.ToDouble(exterLeave) - Convert.ToDouble(IsCL);
                                                    hdnIsCL = IsCL;
                                                    hdnFinalLWP = Convert.ToString(ShowLWP);
                                                    if (IsPL != 0 || SLLeaveBalance != 0 || IsCL != 0)
                                                    {
                                                        if (_strEmployeeConfirmed == "N")
                                                        {
                                                            StrMessage += MsgNonExecutiveNotCofirmed;
                                                        }
                                                        else
                                                        {
                                                            StrMessage += MsgNonExecutiveCofirmed;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        StrMessage += MsgLwpNonExecutive;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                IsCL = Convert.ToDouble(exterLeave);
                                                hdnIsCL = IsCL;
                                                if (_strEmployeeConfirmed == "N")
                                                {
                                                    StrMessage += MsgNonExecutiveNotCofirmed;
                                                }
                                                else
                                                {
                                                    StrMessage += MsgNonExecutiveCofirmed;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ShowLWP = Convert.ToDouble(exterLeave);
                                            hdnFinalLWP = Convert.ToString(ShowLWP);
                                            if (IsPL != 0 || SLLeaveBalance != 0)
                                            {
                                                if (_strEmployeeConfirmed == "N")
                                                {
                                                    StrMessage += MsgExecutiveNotCofirmed;
                                                }
                                                else
                                                {
                                                    StrMessage += MsgExecutiveCofirmed;
                                                }
                                            }
                                            else
                                            {
                                                StrMessage += MsgLwpExecutive;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 7)
                    {
                        #region "LeaveType==7"
                        LeaveType = "CL";
                        _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                        LevOpeningBaL = hdnCLOpeningBalance;
                        TotalDays = hdnCLTotalDays;
                        LevAvailed = hdnCLAvailed;
                        LevAccruded = hdnCLAccruded;
                        if (_strEmployeeConfirmed == "N")
                            LevNBal = Convert.ToString(Convert.ToDouble(LevOpeningBaL) + Convert.ToDouble(LevAccruded));
                        else
                            LevNBal = Convert.ToString(Convert.ToDouble(LevOpeningBaL) + Convert.ToDouble(TotalDays));
                        // Added on 06 Dec 2012 for CLApplied Will Count in Availd
                        LevCLApplied = Convert.ToDouble(hdnCLAppliedNotApproved);
                        LevAvailed = Convert.ToDouble(LevAvailed) + Convert.ToDouble(LevCLApplied);
                        if (LevAvailed == 0)
                            LevBaL = "" + LevAvailed;
                        else
                            LevBaL = Convert.ToString(LevAvailed);
                        LevBaLClosing = hdnBalanceCL;
                        #endregion
                    }
                    else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 10)
                    {
                        #region "Leavetype==10"
                        LeaveType = "LL";
                        _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]]; ;
                        LevOpeningBaL = hdnOpeningBalance;
                        TotalDays = hdnTotalDays;
                        LevAvailed = hdnAvailed;
                        LevNBal = Convert.ToString(Convert.ToDouble(LevOpeningBaL) + Convert.ToDouble(TotalDays));
                        LevApplied = hdnPLAppliedNotApproved;
                        LevAvailed = Convert.ToDouble(LevAvailed) + Convert.ToDouble(LevApplied);
                        if (LevAvailed == 0)
                            LevBaL = "" + LevAvailed;
                        else
                            LevBaL = Convert.ToString(LevAvailed);
                        LevBaLClosing = hdnBalancePL;
                        #endregion
                    }


                    if (LevBaL != "" && LevNBal != "")
                    {
                        Valtemp = Convert.ToDouble(LevNBal) - float.Parse(LevBaL);
                        if (Convert.ToDouble(Valtemp) <= float.Parse("0"))
                        {
                            hdnFinalLeave = Convert.ToString(NoOfDays);
                            _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] = 11;
                            int elementRef = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                            if (elementRef == 11)
                            {
                                //entLeaveType.SelectedIndex
                            }
                            //for (var i = 0; i < elementRef.options.length; i++)
                            //{
                            //    if (elementRef.options[i].value == "11")
                            //    {
                            //        elementRef.options[i].selected = true;
                            //    }
                            //}
                            StrMessage += "You do not have any " + LeaveType + " balance. Your leave application will be consider as LWP";
                        }
                        else
                        {
                            if (Convert.ToDouble(NoOfDays) > Valtemp)
                            {
                                ShowLeave = Valtemp;
                                ShowLWP = Convert.ToDouble(NoOfDays) - Convert.ToDouble(Valtemp);
                            }
                            else
                            {
                                ShowLeave = NoOfDays;
                                ShowLWP = 0;
                            }
                            //if (ShowLeave)
                            if (hdnEmployeeConfirmed == "N" && LeaveType != "LL")
                            {
                                hdnFinalLeave = Convert.ToString(ShowLeave);
                                if (LeaveType == "PL")
                                    StrMessage += "Under probation, " + LeaveType + " can be availled only in case of emergency and is to be approved by the Reporting Manager as well as HOD";
                                if (ShowLWP != 0)
                                {
                                    if (LeaveType == "PL" || LeaveType == "CL" || LeaveType == "LL")
                                    {
                                        StrMessage += "You are applying for " + LeaveType + " more than your available balance (opening balance + accrued till date). If approved, the system will deduct your complete (opening balance + accrued till date) and " + LeaveType + " over and above will be treated as LWP.";
                                        hdnFinalLWP = Convert.ToString(ShowLWP);
                                    }
                                }
                            }
                            else if (hdnEmployeeConfirmed == "Y")
                            {
                                hdnFinalLeave = Convert.ToString(ShowLeave);
                                string year = DateTime.Now.Year.ToString();
                                if (ShowLWP != 0)
                                {
                                    if (LeaveType == "PL" || LeaveType == "CL" || LeaveType == "LL")
                                        hdnFinalLWP = Convert.ToString(ShowLWP);
                                }
                                else
                                {
                                    if ((int)LevBaLClosing < NoOfDays)
                                        StrMessage += "You are applying for " + LeaveType + " more than your available balance (accrued + carried forward). If approved, the system will deduct your complete accrued balance for " + year + " carry forward balance leaving you with a negative balance.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    int JobPeriod = 0;
                    JobPeriod = hdnJoinTime;
                    NoOfDays = Convert.ToInt32(lblDays.Text);
                    if (hdnEmployeeConfirmed == "N")
                    {
                        if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 8)
                        {
                            if (JobPeriod <= 4 && NoOfDays > 90)
                            {
                                await DisplayAlert("alert", "ML can not be applied for more than 90 days", "Ok");
                            }
                            else
                            {
                                if (NoOfDays > 90)
                                {
                                    await DisplayAlert("alert", "ML can not be applied for more than 90 days", "Ok");
                                }
                                else
                                {
                                    StrMessage += "Your ML will be considered as LWP";
                                    hdnFinalLeave = Convert.ToString(NoOfDays);
                                    _hdnActualLeaveType = _leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]];
                                    var elementRef = _hdnActualLeaveType;
                                    if (_hdnActualLeaveType == 11)
                                    {
                                    }
                                    //for (var i = 0; i < entLeaveType.length; i++)
                                    //{
                                    //    if (entLeaveType.options[i].value == "11")
                                    //    {
                                    //        entLeaveType.options[i].selected = true;
                                    //    }
                                    //}
                                }
                            }
                        }
                        else if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 9)
                        {
                            if (NoOfDays > 14)
                            {
                                await DisplayAlert("alert", "PTL can not be applied for more than 14 days", "Ok");
                            }
                            else
                            {
                                hdnFinalLeave = Convert.ToString(NoOfDays);
                            }
                        }
                    }
                    else
                    {
                        if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 8)
                        {
                            if (NoOfDays > 90)
                            {
                                await DisplayAlert("alert", "ML can not be applied for more than 90 days", "Ok");
                            }
                            else
                            {
                                hdnFinalLeave = Convert.ToString(NoOfDays);
                            }
                        }
                        if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 9)
                        {
                            if (NoOfDays > 14)
                            {
                                StrMessage += "PTL can not be applied for more than 14 days";
                            }
                            else
                            {
                                hdnFinalLeave = Convert.ToString(NoOfDays);
                            }
                        }
                    }
                }
                if (StrMessage != "" && StrMessage != null)
                {
                    await DisplayAlert("alert", StrMessage, "Ok");
                    return;
                }
                else
                {
                    overlay.IsVisible = true;
                    #region "Submit Case"

                    LeaveApprovalSave objLeaveApprovalSave = new LeaveApprovalSave();
                    string IsLWP = "0";
                    try
                    {
                        objLeaveApprovalSave.LeaveID = LeaveID;
                        if (_hdnActualLeaveType != 0)
                        {
                            objLeaveApprovalSave.ActLeaveType = Convert.ToString(_hdnActualLeaveType);
                        }
                        else
                        {
                            objLeaveApprovalSave.ActLeaveType = "0";
                        }
                        objLeaveApprovalSave.LeaveType = Convert.ToString(_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]]);
                        objLeaveApprovalSave.From = String.Format("{0:dd/MM/yyyy}", DPDateFrom.Date);
                        objLeaveApprovalSave.To = String.Format("{0:dd/MM/yyyy}", DPDateTo.Date);
                        objLeaveApprovalSave.ReasonLeave = EditReason.Text;
                        if (EditContactAddress.Text != "" || EditContactAddress.Text != null || EditContactAddress.Text != (null))
                        {
                            objLeaveApprovalSave.ContactAddress = EditContactAddress.Text;
                        }
                        if (EntContactNumber.Text != "" || EntContactNumber.Text != null || EntContactNumber.Text != (null))
                        {
                            objLeaveApprovalSave.ContactPhoneNum = EntContactNumber.Text;
                        }
                        objLeaveApprovalSave.halfStatus = halfStatus;
                        objLeaveApprovalSave.ApproveBy = null;
                        objLeaveApprovalSave.CancelledBy = null;
                        objLeaveApprovalSave.ReasonCancel = null;
                        if (_strSelf.ToLower() == "self")
                        {
                            objLeaveApprovalSave.EmployeeID = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                            objLeaveApprovalSave.TransactionType = "Applied";
                        }
                        else
                        {
                            objLeaveApprovalSave.EmployeeID = _strSelf;
                            objLeaveApprovalSave.TransactionType = "Applied";
                        }
                        objLeaveApprovalSave.AppliedBy = Convert.ToString(Application.Current.Properties["EmployeeId"]);
                        objLeaveApprovalSave.SendDateTime = DateTime.Now.ToString("dd/MM/yyyy");
                        objLeaveApprovalSave.ApproveBy = entApprover.Text.Trim();
                        if (EntCMail.Text != "" || EntCMail.Text != null || EntCMail.Text != (null))
                        {
                            objLeaveApprovalSave.ToCCMail = EntCMail.Text.Trim();
                        }
                        if (EntCity.Text != "")
                        {
                            objLeaveApprovalSave.ContactCity = EntCity.Text.Trim();
                        }
                        if (EntPin.Text != "" || EntPin.Text != null)
                        {
                            objLeaveApprovalSave.ContactPin = Convert.ToInt32(EntPin.Text);
                        }
                        //if (hdnNoOfDays != "" || hdnNoOfDays!=null)
                        //{
                        //hdnNoOfDays.Trim();
                        //}
                        //else if (lblDays.Text != null || lblDays.Text != "")
                        //{
                        objLeaveApprovalSave.Days = lblDays.Text;
                        //}
                        if (_leaveType[entLeaveType.Items[entLeaveType.SelectedIndex]] == 6)
                        {
                            objLeaveApprovalSave.fIsSL = Convert.ToInt32(hdnFinalLeave.Trim());
                        }
                        else
                        {
                            objLeaveApprovalSave.fIsSL = 0;
                        }
                        if ((int)hdnIsPL != 0 || Convert.ToString(hdnIsPL) != "")
                        {
                            objLeaveApprovalSave.fIsPL = Convert.ToInt32(hdnIsPL);
                        }
                        else
                        {
                            objLeaveApprovalSave.fIsPL = 0;
                        }
                        if ((int)hdnIsCL != 0)
                        {
                            objLeaveApprovalSave.fIsCL = Convert.ToInt32(hdnIsCL);
                        }
                        else
                        {
                            objLeaveApprovalSave.fIsCL = 0;
                        }
                        if (Convert.ToString(pickerMedCer.SelectedIndex) == "-1")
                            objLeaveApprovalSave.IsMedicalCertificate = null;
                        else
                            objLeaveApprovalSave.IsMedicalCertificate = Convert.ToString(pickerMedCer.SelectedIndex);
                        objLeaveApprovalSave.ReqID = LeaveID;

                        if (_strSelf.ToLower() == "self")
                            hdnCallFor = "Applied";
                        else
                            hdnCallFor = "Approved";

                        if (!string.IsNullOrEmpty(hdnFinalLWP))
                        {
                            IsLWP = "1";
                            objLeaveApprovalSave.ISLWP = IsLWP;
                            objLeaveApprovalSave.LWPDays = hdnFinalLWP;
                        }
                        objLeaveApprovalSave = await App.TodoManager.Save_LeaveApplicationForm(objLeaveApprovalSave);
                        if (objLeaveApprovalSave.AlertMessage != "" || objLeaveApprovalSave.AlertMessage != null)
                        {
                            await DisplayAlert("alert", objLeaveApprovalSave.AlertMessage, "Ok");
                            App.SetupRedirection(new LeaveApproval.PendingApprovalList("Applied", "hris"));
                            App.Current.MainPage = App.MasterDetailPage;

                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                    }
                    #endregion
                    overlay.IsVisible = false;

                }
                #endregion
            }
            #endregion
        }

        #region "Picker Selected Index Changed"
        private void DPDateFrom_DateSelected(object sender, DateChangedEventArgs e)
        {
            CalculateDays();
        }
        public async void CalculateDays()
        {
            string strFrom = string.Empty;
            string strTo = string.Empty;
            strFrom = String.Format("{0:dd/MM/yyyy}", DPDateFrom.Date);
            strTo = String.Format("{0:dd/MM/yyyy}", DPDateTo.Date);
            if (strFrom != "")
            {
                DateTime fromdate1 = Convert.ToDateTime(DPDateFrom.Date);
                DateTime todate1 = Convert.ToDateTime(DPDateTo.Date);
                if (fromdate1 <= todate1)
                {
                    slDays.IsVisible = true;
                    TimeSpan daycount = todate1.Subtract(fromdate1);
                    int dacount1 = Convert.ToInt32(daycount.Days);
                    lblDays.Text = Convert.ToString(dacount1);
                }
                else
                {
                    slDays.IsVisible = false;
                    await DisplayAlert("Alert", "From Date Must be Less Than To Date", "OK");
                }
            }
        }
        private void entLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ddlValue;
            string lType = entLeaveType.Items[entLeaveType.SelectedIndex];
            ddlValue = _leaveType[lType];
            if (entLeaveType.SelectedIndex != -1)
            {
                // For Seak Leave code is 6
                if (ddlValue == 6)
                    div_MedicalCerti.IsVisible = true;
                else
                    div_MedicalCerti.IsVisible = false;

            }
            else
            {
                div_MedicalCerti.IsVisible = false;
            }
            //if (ddlValue == 8)
            //    PageMethods.CheckLeaveMessagePerLeaveType(ddlValue, CallSuccessCheckLeave, CallFailed);
            //if (ddlValue == "9")
            //    PageMethods.CheckLeaveMessageLeaveType(ddlValue, CallSuccessCheckLeave, CallFailed);


        }
        #endregion


        protected void btnApplyForLeave_Clicked(object sender, EventArgs e)
        {
            stackNextTab.IsVisible = true;
            stackFirstTab.IsVisible = false;
            stackButton.IsVisible = true;
        }

        protected void btnBack_Clicked(object sender, EventArgs e)
        {
            stackFirstTab.IsVisible = true;
            stackButton.IsVisible = false;
            stackNextTab.IsVisible = false;
        }

    }
}
