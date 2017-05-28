using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myCIIEmployee.Models;

namespace myCIIEmployee
{
    public interface IRestService
    {
        Task<String> GetDataAsync();
        Task<string> IsValidUser(Login objLogin);

        Task<List<Login>> ValidUserList();

        Task<LoginResult> ValidateUserEAM(LoginResult objLogin);
        Task<Requisition> GetPendingRequisition(Requisition objReqTotal);
        Task<Requisition> GetPendingRequisitionDetail(Requisition objReqTotal);
        Task<RequestionHistoryList> GetRequisitionHistory(Int64 ReqsuisitionID);

        Task<Requisition> GetPendingRequisitionSearch(Requisition objReqTotal);
        Task<EventBudget> GetPendingRequisitionBudgetDetail(EventBudget objEventBudget);
        Task<EventBudget> GetPendingReqEventsRelatedToDept(EventBudget objNotRelatedToEvent);


        Task<UpdateReqAction> UpdatePendingRequisitionDetail(UpdateReqAction objUpdateReqAction);
        Task<Requisition> GetGeneralRequisition(Requisition objGetTravelReqDetail);
        Task<Requisition> GetMediaRequisition(Requisition objGetMediaReqDetail);
        Task<Requisition> GetPrintRequisition(Requisition objPrintReqDetail);
        Task<Requisition> GetHotelRequisition(Requisition objHotelReqDetail);
        Task<Requisition> GetTransportRequisition(Requisition objTransportReqDetail);

        Task<RequisitionApproval> SaveUpdateWorkflowRequisition(RequisitionApproval objRequisitionApproval);
        Task<LeaveApprovalListSearch> GetPageLoadData_LeaveRequestDetails(LeaveApprovalListSearch objLeaveApprovalListSearch);
        Task<LeaveApprovalSubmit> Get_PageLoad_LeaveAplication(LeaveApprovalSubmit objLeaveApprovalSubmit);

        Task<LeaveApprovalSave> Save_LeaveApplicationForm(LeaveApprovalSave LeaveApprovalSave);
    }
}
