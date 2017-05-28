using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myCIIEmployee.Models;

using Xamarin.Forms;
using Windows.UI.Xaml.Controls;

namespace myCIIEmployee.Views
{
    public partial class RequisitionList : ContentPage
    {
        int _strReqId, _strReqCount;
        public RequisitionList(int ReqId)
        {
            InitializeComponent();
            _strReqId = ReqId;
        }
        protected async override void OnAppearing()
        {
            //listReq.IsPullToRefreshEnabled = false;
            if (_strReqId == 1)
                Title = "Print And Design Requisition";
            else if (_strReqId == 2)
                Title = "Travel Requisition";
            else if (_strReqId == 3)
                Title = "Conference / Exhibition Requisition";
            else if (_strReqId == 4)
                Title = "Hotel Requisition";
            else if (_strReqId == 5)
                Title = "Transport Requisition";
            else if (_strReqId == 6)
                Title = "IT Requisition";
            else if (_strReqId == 7)
                Title = " Audio Visual Requisition";
            else if (_strReqId == 8)
                Title = "Backdrop / Banner Requisition";
            else if (_strReqId == 9)
                Title = "General Requisition";
            else if (_strReqId == 10)
                Title = "Miscellaneous Requisition";
            else if (_strReqId == 14)
                Title = "Media Requisition";
            overlay.IsVisible = true;
            base.OnAppearing();
            Requisition objReqTotal = new Requisition(Convert.ToString(Application.Current.Properties["EmployeeId"]));
            objReqTotal.PageIndex = 1;
            objReqTotal.PageSize = 10000;
            objReqTotal.CallFor = null;
            objReqTotal.searchText = null;
            objReqTotal.DepartmentId = null;
            objReqTotal.RType = Convert.ToString(_strReqId);
            objReqTotal = await App.TodoManager.GetPendingRequisitionSearch(objReqTotal);
            listReq.ItemsSource = objReqTotal.RequisitionList_Main;

            listReq.IsPullToRefreshEnabled = true;
            _strReqCount = objReqTotal.RequisitionList_Main.Count();
            overlay.IsVisible = false;
            listReq.IsPullToRefreshEnabled = false;

        }

        private void listReq_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            RequisitionList_Main options = (RequisitionList_Main)e.SelectedItem;

            int RequId = options.Requisitionid;
            int RequTypeId = options.RequTypeId;
            if (RequTypeId == 3 || RequTypeId == 9 || RequTypeId == 6 || RequTypeId == 7 || RequTypeId == 8 || RequTypeId == 10)
            {
                App.SetupRedirection(new Views.GeneralRequisition(Convert.ToString(Application.Current.Properties["EmployeeId"]), RequId, Convert.ToInt32(options.RequTypeId), Convert.ToString(options.Departmentid), Convert.ToString(options.Eventid), _strReqCount));
                App.Current.MainPage = App.MasterDetailPage;
            }
            else if (RequTypeId == 2)
            {
                App.SetupRedirection(new Views.RequisitionView(Convert.ToString(Application.Current.Properties["EmployeeId"]), RequId, Convert.ToInt32(options.RequTypeId), Convert.ToString(options.Departmentid), Convert.ToString(options.Eventid), _strReqCount));
                App.Current.MainPage = App.MasterDetailPage;
            }
            else if (RequTypeId == 14)
            {
                App.SetupRedirection(new Views.MediaRequisition(Convert.ToString(Application.Current.Properties["EmployeeId"]), RequId, Convert.ToInt32(options.RequTypeId), Convert.ToString(options.Departmentid), Convert.ToString(options.Eventid), _strReqCount));
                App.Current.MainPage = App.MasterDetailPage;
            }
            else if (RequTypeId == 1)
            {
                App.SetupRedirection(new Views.PrintRequisition(Convert.ToString(Application.Current.Properties["EmployeeId"]), RequId, Convert.ToInt32(options.RequTypeId), Convert.ToString(options.Departmentid), Convert.ToString(options.Eventid), _strReqCount));
                App.Current.MainPage = App.MasterDetailPage;
            }
            else if (RequTypeId == 4)
            {
                App.SetupRedirection(new Views.HotelRequisition(Convert.ToString(Application.Current.Properties["EmployeeId"]), RequId, Convert.ToInt32(options.RequTypeId), Convert.ToString(options.Departmentid), Convert.ToString(options.Eventid), _strReqCount));
                App.Current.MainPage = App.MasterDetailPage;
            }
            else if (RequTypeId == 5)
            {
                App.SetupRedirection(new Views.TransportRequisition(Convert.ToString(Application.Current.Properties["EmployeeId"]), RequId, Convert.ToInt32(options.RequTypeId), Convert.ToString(options.Departmentid), Convert.ToString(options.Eventid), _strReqCount));
                App.Current.MainPage = App.MasterDetailPage;
            }
        }
    }
}
