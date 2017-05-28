using myCIIEmployee.Models;
using System;

using Xamarin.Forms;

namespace myCIIEmployee.LeaveApproval
{
    public partial class PendingApprovalList : ContentPage
    {
        public string _strStatus, _strType;
        public PendingApprovalList(string Status, string type)
        {
            _strType = type;
            _strStatus = Status;
            InitializeComponent();
            if (Status == "Cancelled")
                Title = "Cancelled Applications";
            else if (Status == "Approved")
                Title = "Approved Applications";
            else if (Status == "Disapproved")
                Title = "Disapproved Applications";
            else if (Status == "Applied")
                Title = "Pending Approval";
            else if (Status == "All")
                Title = "Leave Details";

        }
        protected async override void OnAppearing()
        {
            overlay.IsVisible = true;
            base.OnAppearing();
            LeaveApprovalListSearch objLeaveApprovalListSearch = new LeaveApprovalListSearch(Convert.ToString(Application.Current.Properties["EmployeeId"]));
            objLeaveApprovalListSearch.LeaveType = null;
            objLeaveApprovalListSearch.AppliedDate = null;
            objLeaveApprovalListSearch.FromDate = null;
            objLeaveApprovalListSearch.ToDate = null;
            objLeaveApprovalListSearch.Leavefrom = null;
            objLeaveApprovalListSearch.LeaveTo = null;
            objLeaveApprovalListSearch.LeaveStatus = null;
            objLeaveApprovalListSearch.DirectReportTo = null;
            objLeaveApprovalListSearch.type = _strType;
            objLeaveApprovalListSearch.Name = null;
            objLeaveApprovalListSearch.strStatus = _strStatus;
            objLeaveApprovalListSearch.Ltype = null;
            objLeaveApprovalListSearch.Status = null;
            objLeaveApprovalListSearch.SearchBy = null;
            objLeaveApprovalListSearch.Region = null;
            objLeaveApprovalListSearch.AdminReport = null;
            objLeaveApprovalListSearch = await App.TodoManager.GetPageLoadData_LeaveRequestDetails(objLeaveApprovalListSearch);
            if (objLeaveApprovalListSearch.LeaveApprovalList.Count > 0)
                listPendingLeave.ItemsSource = objLeaveApprovalListSearch.LeaveApprovalList;
            else
            {
                await DisplayAlert("alert", "This tab contains no record", "Ok");
                App.SetupRedirection(new Views.RequisitionDashBoard());
                App.Current.MainPage = App.MasterDetailPage;
            }
            overlay.IsVisible = false;
        }

        private void listPendingLeave_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            LeaveApprovalList options = (LeaveApprovalList)e.SelectedItem;
            string EmpId = options.Employee_id;

            if (EmpId == Convert.ToString(Application.Current.Properties["EmployeeId"]))
                EmpId = "Self";
            App.SetupRedirection(new LeaveApproval.LeaveApplicationForm(_strType, options.Requisition_Id, EmpId, options.Status, options.CancelShowHide));
            App.Current.MainPage = App.MasterDetailPage;
            //}

        }

        private void listPendingLeave_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            //((Label)this.listPendingLeave.Id("")     .FindControl("Label")).Text = "active";
            //var item = (Xamarin.Forms.Label)sender;
            //var Approver1 = e.Item.Approver1;

            //string selectedFromList = listPendingLeave.   GetItemAtPosition(e.Position).ToString();

            //lbltext.Text = selectedFromList;
            //if (listPendingLeave[e.Position])
            //{ }
        }
        //private bool isRowEven;

        //private void Cell_OnAppearing(object sender, EventArgs e)
        //{

        //    if (listPendingLeave.HasUnevenRows)
        //    {

        //    }
        //    if (this.isRowEven)
        //    {
        //        var viewCell = (ViewCell)sender;
        //        if (viewCell.View != null)
        //        {
        //            viewCell.View.BackgroundColor = Color.FromHex("#E5e5e5");
        //        }
        //    }
        //    this.isRowEven = !this.isRowEven;
        //}

        //public void OnMore(object sender, EventArgs e)
        //{
        //    var mi = ((MenuItem)sender);
        //    DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        //}

        //public void OnDelete(object sender, EventArgs e)
        //{
        //    var mi = ((MenuItem)sender);
        //    DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        //}
    }
}
