using System;
using System.Collections.Generic;
using System.Globalization;

namespace myCIIEmployee.Models
{
    public class Login
    {
        public string EmailId { set; get; }
        public string Password { set; get; }
    }
    public class LoginResult
    {
        public string username { set; get; }
        public string EmpId { set; get; }
        public string Password { set; get; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string photograph { get; set; }
        public string designation { get; set; }
    }

    #region "Requisition"
    public class RequisitionList
    {

        public string Rtype { get; set; }
        public string DepartmentId { get; set; }
        public string searchText { get; set; }
        public string EmployeeId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string CallFor { get; set; }
    }


    public class Requisition
    {
        public string CallFor { get; set; }
        public string RType { get; set; }
        public string DepartmentId { get; set; }
        public string searchText { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public Requisition()
        { }
        public Requisition(string EmployeeId)
        {
            this.EmployeeId = EmployeeId;

        }
        public Requisition(string EmployeeId, int RequisitionId, int ReqTypeId)
        {
            this.EmpID = EmployeeId;
            this.ReqID = RequisitionId;
            this.ReqType = ReqTypeId;
        }
        public List<RequisitionList> Req { get; set; }
        public List<DeparmentRList> DeptReqList { get; set; }
        public List<RequisitionList_Main> RequisitionList_Main { get; set; }
        public List<RType> ReqList { get; set; }
        //Detail List 
        public List<GetReqDetail> GetReqDetail { get; set; }
        public List<GetExpenseMaster> GetExpenseMaster { get; set; }
        public List<GetTravellerDetails> getTravellerDetails { get; set; }
        public List<GetReservationDetails> GetReservationDetails { get; set; }
        public List<GetRegionName> GetRegionName { get; set; }

        public List<GetMainDetails> GetMainDetails { get; set; }

        public List<GetEmployee> GetEmployee { get; set; }
        public List<GetPODetail> GetPODetail { get; set; }
        public List<GetTravelReqDetail> TravelReqDetail { get; set; }
        public List<TravelCatergoryType> TravelCatergoryType { get; set; }
        public List<GetPrintReqDetail> GetPrintReqDetail { get; set; }
        public List<GetHotelRequisition> GetHotelRequisition { get; set; }
        public List<GetPOTransportDetail> GetPOTransportDetail { get; set; }
        public List<GetTransportDetails> GetTransportDetails { get; set; }
        public Int64 Total { get; set; }
        public int IsServiceDept { get; set; }
        public string CreatedBy { get; set; }
        public int IsSavedOrSubmit { get; set; }
        public string PendingItems { get; set; }
        public string ReqStatus { get; set; }
        public string EmployeeId { get; private set; }
        public int ReqID { get; set; }
        public string EmpID { get; set; }
        public int ReqType { get; set; }

        public List<UpdateReqAction> UpdateReqAction { get; set; }
        public List<UpdateReqActionMailBody> UpdateReqActionMailBody { get; set; }

    }
    public class RequisitionList_Main
    {
        public Int64 ReqCount { get; set; }
        public int Requisitionid { get; set; }
        public int RequTypeId { get; set; }
        public string RequisitionType { get; set; }
        public string Sent_To_Multiple { get; set; }
        public string reqdate { get; set; }
        public string Empname { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string RelatedTo { get; set; }
        public string Departmentid { get; set; }
        public string PRNO { get; set; }
        public string Amount { get; set; }
        public string EventTitle { get; set; }
        public string Eventid { get; set; }
        public string ReqIcon { get; set; }
        public string BriefDet { get; set; }
    }

    //for Requisition Type and Name
    public class RType
    {
        public int ReqTypeId { get; set; }
        public string ReqName { get; set; }
    }

    public class DeparmentRList
    {
        public string Departmentid { get; set; }
        public string Name { get; set; }

    }

    //Requisition Detail

    public class RequisitionList_Detail
    {
        public int ReqID { get; set; }
        public string EmpID { get; set; }
        public int ReqType { get; set; }
    }
    public class GetReqDetail
    {
        //public int TravReqID { get; set; }
        //public bool IsActive { get; set; }
        //public int Requisitionid { get; set; }
        //public int ItemNo { get; set; }
        //public int Rate { get; set; }
        //public int Quantity { get; set; }
        //public string BookingFor { get; set; }
        //public string SpecialInstruction { get; set; }
        //public string BookingReqIn { get; set; }
        //public string PurOrderID { get; set; }
        //public string EarlyBookingReason { get; set; }
        public Int64 TravReqID { get; set; }
        public bool IsActive { get; set; }
        public Int64 Requisitionid { get; set; }
        public string ItemNo { get; set; }
        public double Rate { get; set; }
        public Int64 Quantity { get; set; }
        public string BookingFor { get; set; }
        public string SpecialInstruction { get; set; }
        public string BookingReqIn { get; set; }
        public string PurOrderID { get; set; }
        public string EarlyBookingReason { get; set; }

    }
    public class GetExpenseMaster
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class GetTravellerDetails
    {
        public Int64 TDID { get; set; }
        public Int64 RequisitionID { get; set; }
        public string BookingFor { get; set; }
        public string TravellerID { get; set; }
        public string TravellerName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Seat { get; set; }
        public string Meal { get; set; }
        public string FrequentFlierNo { get; set; }
        public string CategoryOfRoom { get; set; }
        public string AlternateNo { get; set; }
    }
    public class GetReservationDetails
    {
        public int TRDID { get; set; }
        public int RequisitionID { get; set; }
        public string FromJourney { get; set; }
        public string ToJourney { get; set; }
        public string RequiredOn { get; set; }
        public string Flight_TrainNo { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public float Amount { get; set; }
    }
    public class GetRegionName
    {
        public string regionid { get; set; }
        public string regionname { get; set; }

    }
    public class GetMainDetails
    {
        public string CreatedBy { get; set; }
        public int RequisitionID { get; set; }
        public string RequisitionType { get; set; }
        public string EventRelated { get; set; }
        public string Departmentid { get; set; }
        public string DeptName { get; set; }
        public string Region { get; set; }
        public string Eventid { get; set; }
        public string Title { get; set; }
        public string WorkFlow_id { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
        public string workorderno { get; set; }
        public string serDeptID { get; set; }
        public string ServDeptName { get; set; }
        public string SerDeptRegion { get; set; }
        public string BudgetYear { get; set; }
        public string EventReason { get; set; }
        public string ReasonAttachment { get; set; }

    }
    public class GetEmployee
    {
        public string EmployeeID { get; set; }
        public string Name { get; set; }

    }
    public class GetPODetail
    {
        public int ID { get; set; }
        public int RequisitionId { get; set; }
        public string PoDate { get; set; }
        public string PurOrderId { get; set; }
        public string Amount { get; set; }
        public string VenderID { get; set; }
        public string Items { get; set; }
        public string VenderName { get; set; }
        public string IsCancel { get; set; }
        public string CancellationCharges { get; set; }
        public string IsBillRaised { get; set; }
    }

    public class EventBudget
    {
        public EventBudget()
        {
        }

        public EventBudget(int RequisitionId, int ReqTypeId, string DepartmentId, int BudgetYear)
        {
            this.ReqId = RequisitionId;
            this.ReqTypeId = ReqTypeId;
            this.DepartmentID = DepartmentId;
            this.BudgetYear = Convert.ToString(BudgetYear);
        }
        public EventBudget(int RequisitionId, int ReqTypeId, string DepartmentId, string EventId)
        {
            this.ReqId = RequisitionId;
            this.ReqTypeId = ReqTypeId;
            this.EventId = EventId;
            this.DepartmentID = DepartmentId;
        }
        public string DepartmentID { get; set; }
        public string BudgetYear { get; set; }
        public int ReqId { get; set; }
        public int ReqTypeId { get; set; }
        public string EventId { get; set; }
        public string Budgetprovision { get; set; }
        public string Submittedforprocessing { get; set; }
        public string BudgetUtilised { get; set; }
        public string BalanceAvailable { get; set; }
        public string ThisRequisition { get; set; }
        public string BalanceAfterThisRequisition { get; set; }
    }
    public class UpdateReqAction
    {
        public int ReqId { get; set; }
        public int ReqTypeId { get; set; }
        public string ActionBy { get; set; }
        public string ActionOn { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int AutoId { get; set; }
        public string ActionFrom { get; set; }
    }
    #endregion

    #region "General requisition"
    public class GetTravelReqDetail
    {
        public bool IsEdit { get; set; }
        public string PurOrderID { get; set; }
        public int GRID { get; set; }
        public string CategoryID { get; set; }
        public string ItemNo { get; set; }
        public float Rate { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public string SpecialInstruction { get; set; }
        public string Requiredon { get; set; }
        public string ItemName { get; set; }
        public string Amount { get; set; }
    }
    public class TravelCatergoryType
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    #endregion

    #region "Print requisition"
    public class GetPrintReqDetail
    {
        public bool IsEdit { get; set; }
        public string PurOrderID { get; set; }
        public int PrintReqID { get; set; }
        public int Requisitionid { get; set; }
        public string CategoryID { get; set; }
        public string ItemNo { get; set; }
        public float Rate { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public string SpecialInstruction { get; set; }
        public string Requiredon { get; set; }
        public string JobType { get; set; }

        public string DocColor { get; set; }
        public string Size { get; set; }
        public string Pages { get; set; }

        public string IncomSource { get; set; }
        public string Remarks { get; set; }
        public string IsNew { get; set; }
        public string Attachfiles { get; set; }
        public string ItemName { get; set; }
        public string Amount { get; set; }

    }
    #endregion

    #region "Hotel Requisition"
    public class GetHotelRequisition
    {
        public string PurOrderID { get; set; }
        public int HotelReqID { get; set; }
        public int Requisitionid { get; set; }
        public string Time_Checkout { get; set; }
        public string Date_Checkout { get; set; }
        public string ItemNo { get; set; }
        public float Rate { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public string FlightTo_Checkin { get; set; }
        public string FlightFrom_Checkin { get; set; }
        public string FlightNo_Checkin { get; set; }
        public string PickupFrom_Checkin { get; set; }
        public bool PickupRequired_Checkin { get; set; }
        public string Time_Checkin { get; set; }
        public string Date_Checkin { get; set; }
        public string City { get; set; }
        public bool DropToRequired_Checkout { get; set; }
        public string PickupFrom_Checkout { get; set; }
        public string FlightNo_Checkout { get; set; }
        public string FlightFrom_Checkout { get; set; }
        public string FlightTo_Checkout { get; set; }
    }
    #endregion

    #region "Transport Requisition"
    public class GetPOTransportDetail
    {
        public int TranReqID { get; set; }
        public bool IsActive { get; set; }
        public int Requisitionid { get; set; }
        public int ItemNo { get; set; }
        public string Rate { get; set; }
        public int Quantity { get; set; }
        public string BookingFor { get; set; }
        public string PurOrderID { get; set; }
        public string SpecialInstruction { get; set; }
        public string ItemName { get; set; }
    }
    public class GetTransportDetails
    {
        public int RequisitionID { get; set; }
        public int TRPDID { get; set; }
        public string PicDate { get; set; }
        public string PicTime { get; set; }
        public string PickAddress { get; set; }
        public string Half_FullDay { get; set; }
        public string VehicleType { get; set; }

    }
    #endregion

    public class UpdateReqActionMailBody
    {
        public string IsValidMail { get; set; }
        public string Name { get; set; }
        public string MailBody { get; set; }
        public string Email { get; set; }
    }
    public class Employee
    {
        public string EmailId { get; set; }
        public string EmployeeId { get; set; }
        public string ImageURL { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
    }
    #region "Approval chain"
    public class ReqAttachment
    {
        public string RequestId { get; set; }
        public string Attachment { get; set; }
    }
    public class RequisitionApproval
    {
        public List<ReqApprovalEmpDetails> ReqApprovalEmpDetails { get; set; }
        public List<ReqApprovalReqDetails> ReqApprovalReqDetails { get; set; }
        public List<ReqApprovalActionDet> ReqApprovalActionDet { get; set; }
        public List<EventBudget> EventBudget { get; set; }
        public List<ReqAttachment> ReqAttachment { get; set; }

        public Int64 RequestID { get; set; }
        public string EmployeeID { get; set; }
        public string DepartmentID { get; set; }
        public string TransactionType { get; set; }
        public string SerDeptID { get; set; }
        public string Purpose { get; set; }
        public string EventRelated { get; set; }
        public string EventID { get; set; }
        public string Remark { get; set; }
        public int RequTypeId { get; set; }
        public string RequisitionInsertUpdate { get; set; }
        public string IsSpecialApp { get; set; }
        public Int64 BudgetYear { get; set; }
        public string EventReason { get; set; }
        public string Attachment { get; set; }
        public string stremployeeName { get; set; }
        public string strIsSerDept { get; set; }
    }

    public class ReqApprovalEmpDetails
    {
        public string Employee_id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string DesignationName { get; set; }
    }

    public class ReqApprovalReqDetails
    {
        public string SendToEmpType { get; set; }
        public string RequisitionType { get; set; }
        public int Requisitionid { get; set; }
        public string SerDeptName { get; set; }
        public string WorkOrderNo { get; set; }
        public string PRNo { get; set; }
        public string EventReason { get; set; }
    }

    public class ReqApprovalActionDet
    {
        public string Actionby { get; set; }
        public string Actiondate { get; set; }
        public string Action { get; set; }
        public string Remarks { get; set; }
        public string Sentto { get; set; }
    }
    #endregion

    #region "Requisition History "


    public class RequestionHistoryList
    {
        public List<RequestionHistoryDetail> RequestionHistory { get; set; }
    }

    public class RequestionHistoryDetail
    {
        public int WFHID { get; set; }
        public string ActionBy { get; set; }
        public string ActionOn { get; set; }
        public string Action { get; set; }
        public string Remarks { get; set; }
        public string SendTo { get; set; }
    }


    #endregion

    #region "Leave Approval"
    public class LeavesubDetails_Edit
    {
        public int ReqID { get; set; }
        public Int32 LeaveTypeID { get; set; }
        public string LeaveType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Days { get; set; }


    }
    public class LeaveApprovalList
    {
        public Int64 leave_id { get; set; }
        public Int64 Requisition_Id { get; set; }
        public string WorkFlowID { get; set; }
        public string Sent_To_Multiple { get; set; }
        public string Employee_id { get; set; }
        public string leaveType { get; set; }
        public string CreateByName { get; set; }
        public string Purpose { get; set; }
        public string Resion { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CancelShowHide { get; set; }
        public string Status_DateTime { get; set; }
        public string Reqdate { get; set; }
        public string ToDate { get; set; }
        public string leaveFromTo { get; set; }
        public string leaveFrom { get; set; }
        public string leaveTo { get; set; }
        public string statusDateTime { get; set; }
        public bool Read_Unread { get; set; }
        public decimal Days { get; set; }
        public string EmployeeType { get; set; }
        public string Approver1 { get; set; }
        public string Approver2 { get; set; }
        public Int64 PendingDays { get; set; }
    }

    public class LeaveApprovalListSearch : IDisposable
    {
        public LeaveApprovalListSearch(string EmployeeId)
        {
            this.EmpId = EmployeeId;

        }
        public List<LeaveApprovalList> LeaveApprovalList { get; set; }
        public List<LeaveTypeCategory> LeaveTypeCategory { get; set; }
        public string LeaveType { get; set; }
        public string EmpId { get; set; }
        public string AppliedDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Leavefrom { get; set; }
        public string LeaveTo { get; set; }
        public string LeaveStatus { get; set; }
        public string Name { get; set; }
        public string DirectReportTo { get; set; }
        public string Status { get; set; }
        public string Ltype { get; set; }
        public string AdminReport { get; set; }
        public string strStatus { get; set; }
        public string SearchBy { get; set; }
        public string type { get; set; }
        public string Departmentid { get; set; }
        public string Region { get; set; }
        public void Dispose()
        {

        }
    }
    public class LeaveTypeCategory
    {
        public Int64 leaveId { get; set; }
        public string leaveType { get; set; }


    }
    public class LeaveApprovalSubmit : IDisposable
    {
        //public List<LeaveApprovalList> LeaveApprovalList { get; set; }
        public List<LeaveSubApprovalList> LeaveSubApprovalList { get; set; }
        public List<LeaveSubTypeCategory> LeaveSubTypeCategory { get; set; }
        public List<LeaveSubCategory> LeaveSubCategory { get; set; }
        public List<LeaveSubDetailsList> LeaveSubDetailsList { get; set; }
        public List<LeavesubDetails_Edit> LeavesubDetails_Edit { get; set; }
        public Int64 ReqId { get; set; }
        public string EmployeeID { get; set; }
        public Int64 ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string leaveId { get; set; }
        public string LeaveType { get; set; }
        public double OpeningBalance { get; set; }
        public decimal Accrued { get; set; }
        public decimal Total { get; set; }
        public decimal Availed { get; set; }
        public decimal ClosingBalance { get; set; }
        public decimal AppliedNotApproved { get; set; }
        public int TDays { get; set; }
        public string DREmployeeID { get; set; }
        public string FullName { get; set; }
        public string ReportingManager { get; set; }
        public string Gender { get; set; }
        public string Confirmed { get; set; }
        public string JoinTime { get; set; }
        public string CategoryId { get; set; }
        public string ContractEndDate { get; set; }
        public string ActualLeaveType { get; set; }
        public string LeaveTypeID { get; set; }
        public string L_Type { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ReasonForLeave { get; set; }
        public string WorkFlowID { get; set; }
        public string Days { get; set; }
        public string ReqDate { get; set; }
        public string Hod { get; set; }
        public string Approver1 { get; set; }
        public string Approver12 { get; set; }
        public string Departmentid { get; set; }
        public string Status_DateTime { get; set; }
        public string ContactCity { get; set; }
        public string ContactPin { get; set; }
        public string ToCCMail { get; set; }
        public string IsMedicalCertificate { get; set; }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }
    }
    public class LeaveSubDetailsList
    {
        public string Name { get; set; }
        public string leaveId { get; set; }
        public string ActualLeaveType { get; set; }
        public string LeaveTypeID { get; set; }
        public string L_Type { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ReasonForLeave { get; set; }
        public string WorkFlowID { get; set; }
        public string Days { get; set; }


        public string ReqDate
        {
            get; set;

        }
        public string ReportingManager { get; set; }
        public string Gender { get; set; }
        public string LeaveType { get; set; }
        public string Hod { get; set; }
        public string Approver1 { get; set; }
        public string Approver12 { get; set; }
        public string Departmentid { get; set; }
        public string DepartmentName { get; set; }
        public string ContactCity { get; set; }
        public string ContactPin { get; set; }
        public string ToCCMail { get; set; }
        public string IsMedicalCertificate { get; set; }
        public string Status_DateTime { get; set; }
    }
    public class LeaveSubApprovalList
    {
        public int ID { get; set; }
        public string Type { get; set; }
    }
    public class LeaveSubTypeCategory
    {
        public string LeaveType { get; set; }
        public double OpeningBalance { get; set; }
        public double Accrued { get; set; }
        public double Total { get; set; }
        public double Availed { get; set; }
        public double ClosingBalance { get; set; }
        public double AppliedNotApproved { get; set; }
        public int TDays { get; set; }
    }
    public class LeaveSubCategory
    {
        public string Confirmed { get; set; }
        public Int32 JoinTime { get; set; }
        public string CategoryId { get; set; }
        public string ContractEndDate { get; set; }
    }

    public class LeaveApprovalSave : IDisposable
    {
        public string AlertMessage { get; set; }
        public string ApprovalStatus { get; set; }
        public int LeaveID { get; set; }
        public string EmployeeID { get; set; }
        public string LeaveType { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string ReasonLeave { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhoneNum { get; set; }
        public string ApproveBy { get; set; }
        public string CancelledBy { get; set; }
        public string ReasonCancel { get; set; }
        public string TransactionType { get; set; }
        public string WFlowID { get; set; }
        public int ReqID { get; set; }
        public string Days { get; set; }
        public string ContactCity { get; set; }
        public int ContactPin { get; set; }
        public string ToCCMail { get; set; }
        public string IsMedicalCertificate { get; set; }
        public string SendDateTime { get; set; }
        public string AppliedBy { get; set; }
        public string SendToMulti { get; set; }
        public string ISLWP { get; set; }
        public string LWPDays { get; set; }
        public string halfStatus { get; set; }
        public string ActLeaveType { get; set; }
        public int fIsSL { get; set; }
        public int fIsPL { get; set; }
        public int fIsCL { get; set; }
        public string ActionFrom { get; set; }
        public string DirectReportTo { get; set; }
        public string AdminReport { get; set; }
        public string Status { get; set; }
        public string ApprovedDate { get; set; }
        public string DeptId { get; set; }
        public void Dispose()
        {
        }
    }
    #endregion
}
