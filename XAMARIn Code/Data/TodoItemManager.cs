using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myCIIEmployee.Models;
using Xamarin.Forms;

namespace myCIIEmployee
{
    public class TodoItemManager
    {
        IRestService restService;

        public TodoItemManager(IRestService service)
        {
            restService = service;
        }

        public Task<string> MainGetDataAsync()
        {
            return restService.GetDataAsync();
        }

        public Task<List<Login>> ValidUserList()
        {
            return restService.ValidUserList();
        }
        public Task<string> IsValidUser(Login objLogin)
        {
            return restService.IsValidUser(objLogin);
        }
        public Task<LoginResult> ValidateUserEAM(LoginResult objLogin)
        {
            return restService.ValidateUserEAM(objLogin);
        }
        public Task<Requisition> GetPendingRequisition(Requisition objReqTotal)
        {
            return restService.GetPendingRequisition(objReqTotal);
        }
        public Task<Requisition> GetPendingRequisitionDetail(Requisition objReqTotal)
        {
            return restService.GetPendingRequisitionDetail(objReqTotal);
        }

        public Task<Requisition> GetPendingRequisitionSearch(Requisition objReqTotal)
        {
            return restService.GetPendingRequisitionSearch(objReqTotal);
        }

        public Task<EventBudget> GetPendingRequisitionBudgetDetail(EventBudget objEventBudget)
        {
            return restService.GetPendingRequisitionBudgetDetail(objEventBudget);
        }
        public Task<EventBudget> GetPendingReqEventsRelatedToDept(EventBudget objNotRelatedToEvent)
        {
            return restService.GetPendingReqEventsRelatedToDept(objNotRelatedToEvent);
        }
        public Task<UpdateReqAction> UpdatePendingRequisitionDetail(UpdateReqAction objUpdateReqAction)
        {
            return restService.UpdatePendingRequisitionDetail(objUpdateReqAction);
        }
        public Task<Requisition> GetGeneralRequisition(Requisition objGetTravelReqDetail)
        {
            return restService.GetGeneralRequisition(objGetTravelReqDetail);
        }
        public Task<Requisition> GetMediaRequisition(Requisition objGetMediaReqDetail)
        {
            return restService.GetMediaRequisition(objGetMediaReqDetail);
        }
        public Task<Requisition> GetPrintRequisition(Requisition objPrintReqDetail)
        {
            return restService.GetPrintRequisition(objPrintReqDetail);
        }
        public Task<Requisition> GetHotelRequisition(Requisition objHotelReqDetail)
        {
            return restService.GetHotelRequisition(objHotelReqDetail);
        }
        public Task<Requisition> GetTransportRequisition(Requisition objTransportReqDetail)
        {
            return restService.GetTransportRequisition(objTransportReqDetail);
        }
        public Task<RequisitionApproval> SaveUpdateWorkflowRequisition(RequisitionApproval objRequisitionApproval)
        {
            return restService.SaveUpdateWorkflowRequisition(objRequisitionApproval);
        }

        public Task<RequestionHistoryList> GetRequisitionHistory(Int64 ReqsuisitionID)
        {
            return restService.GetRequisitionHistory(ReqsuisitionID);
        }

        public async Task<StackLayout> BindRequisitionHistory(Int64 ReqsuisitionID)
        {
            RequestionHistoryList lstRequisitionHistory = await GetRequisitionHistory(ReqsuisitionID);

            StackLayout stackFinal = new StackLayout();

            foreach (var history in lstRequisitionHistory.RequestionHistory)
            {
                StackLayout stackRow = new StackLayout();
                stackRow.Spacing = 0;
                stackRow.Padding = 0;
                stackRow.Orientation = StackOrientation.Vertical;
                stackRow.BackgroundColor = Color.FromHex("#FDFBC6");
                stackRow.Margin = new Thickness(0, 0, 0, 10);

                StackLayout stackContent1 = new StackLayout();
                stackContent1.Orientation = StackOrientation.Horizontal;
                stackContent1.Margin = new Thickness(20, 10, 20, 0);

                Label lblActionOn = new Label();
                lblActionOn.FontSize = 14;
                lblActionOn.FontFamily = "Roboto";
                lblActionOn.TextColor = Color.FromHex("#000");
                lblActionOn.HorizontalOptions = LayoutOptions.StartAndExpand;
                //lblActionOn.Text = Convert.ToDateTime(history.ActionOn).ToString("dd MMM yyyy hh:mm  tt") ;
                lblActionOn.Text = history.ActionOn;

                stackContent1.Children.Add(lblActionOn);

                Label lblAction = new Label();
                lblAction.FontSize = 14;
                lblAction.FontFamily = "Roboto";
                lblAction.TextColor = Color.FromHex("#000");
                lblAction.HorizontalOptions = LayoutOptions.EndAndExpand;
                lblAction.FontAttributes = FontAttributes.Bold;
                lblAction.Text = ActionNamedAs(history.Action);

                stackContent1.Children.Add(lblAction);

                stackRow.Children.Add(stackContent1);


                StackLayout stackContent2 = new StackLayout();
                stackContent2.Orientation = StackOrientation.Horizontal;
                stackContent2.Margin = new Thickness(20, 10, 20, 0);

                Label lblActionBy = new Label();
                lblActionBy.FontSize = 14;
                lblActionBy.FontFamily = "Roboto";
                lblActionBy.TextColor = Color.FromHex("#000");
                lblActionBy.HorizontalOptions = LayoutOptions.StartAndExpand;
                lblActionBy.Text = history.ActionBy;

                stackContent2.Children.Add(lblActionBy);

                Label lblSentTo = new Label();
                lblSentTo.FontSize = 14;
                lblSentTo.FontFamily = "Roboto";
                lblSentTo.TextColor = Color.FromHex("#000");
                lblSentTo.HorizontalOptions = LayoutOptions.EndAndExpand;
                lblSentTo.Text = history.SendTo;

                stackContent2.Children.Add(lblSentTo);

                stackRow.Children.Add(stackContent2);



                StackLayout stackContent3 = new StackLayout();
                stackContent3.Orientation = StackOrientation.Horizontal;
                stackContent3.Margin = new Thickness(20, 10, 20, 10);

                if (!String.IsNullOrEmpty(history.Remarks))
                {
                    Label lblRemarks = new Label();
                    lblRemarks.FontSize = 14;
                    lblRemarks.FontFamily = "Roboto";
                    lblRemarks.TextColor = Color.FromHex("#000");
                    lblRemarks.HorizontalOptions = LayoutOptions.StartAndExpand;
                    lblRemarks.Text = history.Remarks;

                    stackContent3.Children.Add(lblRemarks);
                }
                stackRow.Children.Add(stackContent3);

                stackFinal.Children.Add(stackRow);

            }
            return stackFinal;

        }

        public string ActionNamedAs(string strvalue)
        {
            string retValue = string.Empty;
            switch (strvalue.Trim().ToLower())
            {
                case "revert":
                    retValue = "Reverted";
                    break;
                case "reject":
                    retValue = "Rejected";
                    break;
                case "submit":
                    retValue = "Submited";
                    break;
                case "approve":
                    retValue = "Approved";
                    break;
                case "resubmit":
                    retValue = "Re-Submited";
                    break;
                case "po generate":
                    retValue = "PO Generated";
                    break;
                case "prrecall":
                    retValue = "PR-Recalled";
                    break;
                default:
                    retValue = strvalue;
                    break;
            }
            return retValue;
        }


        public Task<LeaveApprovalListSearch> GetPageLoadData_LeaveRequestDetails(LeaveApprovalListSearch objLeaveApprovalListSearch)
        {
            return restService.GetPageLoadData_LeaveRequestDetails(objLeaveApprovalListSearch);
        }
        public Task<LeaveApprovalSubmit> Get_PageLoad_LeaveAplication(LeaveApprovalSubmit objLeaveApprovalSubmit)
        {
            return restService.Get_PageLoad_LeaveAplication(objLeaveApprovalSubmit);
        }

        public Task<LeaveApprovalSave> Save_LeaveApplicationForm(LeaveApprovalSave objLeaveApprovalSave)
        {
            return restService.Save_LeaveApplicationForm(objLeaveApprovalSave);
        }
    }
}
