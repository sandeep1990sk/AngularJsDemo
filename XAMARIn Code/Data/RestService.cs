using myCIIEmployee.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace myCIIEmployee
{
    public class RestService : IRestService
    {
        HttpClient client;
        public RestService()
        {
            var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 1024*1024*10;
            //client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }
        public async Task<string> GetDataAsync()
        {
            string Item = "";

            var uri = new Uri(string.Format(Constants.RestUrl, "GetCurrentTime"));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Item = (string)content;
                    //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Item;
        }

        public async Task<string> IsValidUser(Login objLogin)
        {
            string strReturn = string.Empty;

            HttpResponseMessage response = null;

            var uri = new Uri(string.Format(Constants.RestUrl, "IsValidUser"));

            try
            {
                var json = JsonConvert.SerializeObject(objLogin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data;
                    Debug.WriteLine(@"TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return strReturn;
        }
        public async Task<List<Login>> ValidUserList()
        {
            List<Login> Items = new List<Login>();

            var uri = new Uri(string.Format(Constants.RestUrl, "ValidUserList"));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Login>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<LoginResult> ValidateUserEAM(LoginResult objLogin)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            string uri = string.Format(Constants.RestUrl, "Login", "ValidateUserEAM");
            var json = JsonConvert.SerializeObject(objLogin);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objLogin = JsonConvert.DeserializeObject<LoginResult>(strReturn);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return objLogin;
        }

        public async Task<RequestionHistoryList> GetRequisitionHistory(Int64 ReqsuisitionID)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;

            RequestionHistoryList objRequisitionHistory = null;

            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetHistory?ReqsuisitionID=" + ReqsuisitionID));
            try
            {
                var json = JsonConvert.SerializeObject("{ \"ReqsuisitionID\" :" + ReqsuisitionID + " } ");
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objRequisitionHistory = JsonConvert.DeserializeObject<RequestionHistoryList>(strReturn);

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return objRequisitionHistory;
        }


        public async Task<Requisition> GetPendingRequisition(Requisition objReqTotal)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetPendingRequisitionSearch"));
            try
            {
                var json = JsonConvert.SerializeObject(objReqTotal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objReqTotal = JsonConvert.DeserializeObject<Requisition>(strReturn);
                    //data = response.Content;

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return objReqTotal;
        }
        public async Task<Requisition> GetPendingRequisitionSearch(Requisition objReqTotal)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetPendingRequisitionSearch?Rtype=" + objReqTotal.RType + "&DepartmentId=null&searchText=null&EmployeeId=" + objReqTotal.EmployeeId + "&PageIndex=1&PageSize=5000&CallFor=null"));
            try
            {
                var json = JsonConvert.SerializeObject("{ \"EmpID\" :" + objReqTotal.EmployeeId + " } ");
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objReqTotal = JsonConvert.DeserializeObject<Requisition>(strReturn);

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return objReqTotal;
        }

        public async Task<Requisition> GetPendingRequisitionDetail(Requisition objReqTotal)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetPendingRequisitionDetail"));
            try
            {
                var json = JsonConvert.SerializeObject(objReqTotal);
                int size = 1000;
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (var httpclient = new HttpClient())
                {
                    httpclient.DefaultRequestHeaders.Range = new RangeHeaderValue(0, size);
                    response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string data1 = await response.Content.ReadAsStringAsync();
                        var bytes = new byte[size];
                        strReturn = (string)data1;
                        objReqTotal = JsonConvert.DeserializeObject<Requisition>(strReturn);
                        //data = response.Content;
                        Debug.WriteLine(@"TodoItem successfully saved.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return objReqTotal;
        }
        //public string EncryptData(object DataforEcryption)
        //{
        //    Byte[] data = SymmetricEncryptionUtility.EncryptData(Convert.ToString(DataforEcryption), KeyFileName);
        //    return Convert.ToBase64String(data);
        //}

        //public string DecryptData(Object DataforEcryption)
        //{
        //    Byte[] data = Convert.FromBase64String(Convert.ToString(DataforEcryption).Trim());
        //    return SymmetricEncryptionUtility.DecryptData(data, KeyFileName);
        //}
        public async Task<EventBudget> GetPendingRequisitionBudgetDetail(EventBudget objEventBudget)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetPendingRequisitionBudgetDetail?ReqId=" + objEventBudget.ReqId + "&ReqTypeId=" + objEventBudget.ReqTypeId + "&EventId=" + objEventBudget.EventId));
            try
            {
                var json = JsonConvert.SerializeObject(objEventBudget);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objEventBudget = JsonConvert.DeserializeObject<EventBudget>(strReturn);

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return objEventBudget;
        }

        public async Task<EventBudget> GetPendingReqEventsRelatedToDept(EventBudget objNotRelatedToEvent)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetPendingReqEventsRelatedToDept"));
            try
            {
                var json = JsonConvert.SerializeObject(objNotRelatedToEvent);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objNotRelatedToEvent = JsonConvert.DeserializeObject<EventBudget>(strReturn);
                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objNotRelatedToEvent;
        }
        public async Task<UpdateReqAction> UpdatePendingRequisitionDetail(UpdateReqAction objUpdateReqAction)

        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "UpdatePendingRequisitionStatus"));
            try
            {
                var json = JsonConvert.SerializeObject(objUpdateReqAction);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objUpdateReqAction = JsonConvert.DeserializeObject<UpdateReqAction>(strReturn);

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return objUpdateReqAction;
        }
        public async Task<Requisition> GetGeneralRequisition(Requisition objGetTravelReqDetail)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetGeneralRequisition"));
            try
            {
                var json = JsonConvert.SerializeObject(objGetTravelReqDetail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objGetTravelReqDetail = JsonConvert.DeserializeObject<Requisition>(strReturn);
                    //data = response.Content;

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objGetTravelReqDetail;
        }

        public async Task<Requisition> GetMediaRequisition(Requisition objGetMediaReqDetail)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetMediaRequisition"));
            try
            {
                var json = JsonConvert.SerializeObject(objGetMediaReqDetail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objGetMediaReqDetail = JsonConvert.DeserializeObject<Requisition>(strReturn);
                    //data = response.Content;

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objGetMediaReqDetail;
        }
        public async Task<Requisition> GetPrintRequisition(Requisition objPrintReqDetail)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetPrintRequisition"));
            try
            {
                var json = JsonConvert.SerializeObject(objPrintReqDetail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objPrintReqDetail = JsonConvert.DeserializeObject<Requisition>(strReturn);
                    //data = response.Content;

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objPrintReqDetail;
        }
        public async Task<Requisition> GetHotelRequisition(Requisition objHotelReqDetail)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetHotelRequisition"));
            try
            {
                var json = JsonConvert.SerializeObject(objHotelReqDetail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objHotelReqDetail = JsonConvert.DeserializeObject<Requisition>(strReturn);
                    //data = response.Content;

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objHotelReqDetail;
        }
        public async Task<Requisition> GetTransportRequisition(Requisition objTransportReqDetail)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "GetTransportRequisition"));
            try
            {
                var json = JsonConvert.SerializeObject(objTransportReqDetail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objTransportReqDetail = JsonConvert.DeserializeObject<Requisition>(strReturn);
                    //data = response.Content;

                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objTransportReqDetail;
        }
        public async Task<RequisitionApproval> SaveUpdateWorkflowRequisition(RequisitionApproval objRequisitionApproval)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            var uri = new Uri(string.Format(Constants.RestUrl, "Requsition", "SaveUpdateWorkflowRequisition"));
            try
            {
                var json = JsonConvert.SerializeObject(objRequisitionApproval);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objRequisitionApproval = JsonConvert.DeserializeObject<RequisitionApproval>(strReturn);
                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objRequisitionApproval;
        }
        public async Task<LeaveApprovalListSearch> GetPageLoadData_LeaveRequestDetails(LeaveApprovalListSearch objLeaveApprovalListSearch)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}z
            var uri = new Uri(string.Format(Constants.RestUrl, "LeaveApproval", "GetPageLoadData_LeaveRequestDetails"));
            try
            {
                var json = JsonConvert.SerializeObject(objLeaveApprovalListSearch);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objLeaveApprovalListSearch = JsonConvert.DeserializeObject<LeaveApprovalListSearch>(strReturn);
                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objLeaveApprovalListSearch;
        }

        public async Task<LeaveApprovalSubmit> Get_PageLoad_LeaveAplication(LeaveApprovalSubmit objLeaveApprovalSubmit)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}z
            var uri = new Uri(string.Format(Constants.RestUrl, "LeaveApproval", "Get_PageLoad_LeaveAplication"));
            try
            {
                var json = JsonConvert.SerializeObject(objLeaveApprovalSubmit);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    objLeaveApprovalSubmit = JsonConvert.DeserializeObject<LeaveApprovalSubmit>(strReturn);
                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return objLeaveApprovalSubmit;
        }


        public async Task<LeaveApprovalSave> Save_LeaveApplicationForm(LeaveApprovalSave LeaveApprovalSave)
        {
            string strReturn = string.Empty;
            HttpResponseMessage response = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}z
            var uri = new Uri(string.Format(Constants.RestUrl, "LeaveApproval", "Save_LeaveApplicationForm"));
            try
            {
                var json = JsonConvert.SerializeObject(LeaveApprovalSave);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string data1 = await response.Content.ReadAsStringAsync();
                    strReturn = (string)data1;
                    LeaveApprovalSave = JsonConvert.DeserializeObject<LeaveApprovalSave>(strReturn);
                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return LeaveApprovalSave;
        }
    }
}
