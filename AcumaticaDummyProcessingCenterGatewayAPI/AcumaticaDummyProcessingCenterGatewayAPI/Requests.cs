using Acumatica.ADPCGateway.Api;
using Acumatica.ADPCGateway.Model;
using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.Auxiliary;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi.Model;
using Acumatica.RESTClient.RootApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AcumaticaDummyProcessingCenterGatewayAPI
{
    public class Requests
    {
        public string GetCreateCustomerProfileByCustomerCD(string url, string username, string password, string tenant, string customerCD, string customerName, string customerEmail)
        {
           // var authApi = new AuthApi(url);
            string result = string.Empty;
            int timeout = 100000;
            var Cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = Cookies;

            var Client = new HttpClient(handler);
            Client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
            result = string.Empty;
            string postBody = $"{{\"name\":\"{username}\", \"password\":\"{password}\", \"tenant\": \"{tenant}\"}}";

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = Client.PostAsync(url + "/entity/auth/login", new StringContent(postBody, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            //if (response?.IsSuccessStatusCode == true)
            //{
            //    result = "Credentials correct. ";
            //}

            response = Client.GetAsync(url + $"/entity/ADPCGateway/1/CustomerProfile?$filter=CustomerName eq '{customerCD}'").GetAwaiter().GetResult();
            var p = response.EnsureSuccessStatusCode();
            CustomerProfile cp = ((List<CustomerProfile>)ApiClientHelpers.Deserialize<List<CustomerProfile>>(response)).FirstOrDefault();
            if (cp == null)
            {
                cp = new CustomerProfile
                {
                    CustomerName = customerCD,
                    CustomerDescription = customerName,
                    Email = customerEmail
                };

                response = Client.PutAsync(url + $"/entity/ADPCGateway/1/CustomerProfile", new StringContent(ApiClientHelpers.Serialize(cp), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
                cp = (CustomerProfile)ApiClientHelpers.Deserialize<CustomerProfile>(response);
            }

            response = Client.PostAsync(url + "/entity/auth/logout", new StringContent(string.Empty, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            //    var client = new ApiClient(SiteURL);

            //try
            //{
            //    // Task.Run(() => client.Login(Username, Password, Tenant)).Wait();
            //    client.Login(Username, Password, Tenant);
            //client.Login(Username, Password, Tenant, null, null);

            //var endpoints = Task.Run(() => client.RootGet()).Result;
            //bool isADPCinstalled = endpoints?.Endpoints.Any(e => e.Name.StartsWith("ADPCGateway")) == true;
            //if (isADPCinstalled)
            //{
            //    result += "ADPC installed on the instance";
            //}
            //else
            //{
            //    result += "ADPC is NOT installed on the instance";
            //}

            //}
            //catch (Exception e)
            //{
            //    result += $"Could not connect to {SiteURL}";
            //}
            //finally
            //{
            //    Task.Run(() => client.TryLogout());
            //}
            return cp.CustomerProfileID;




            //try
            //{
            //    authApi.LogIn(username, password, tenant, null, null);
            //    var customerProfileApi = new CustomerProfileApi(authApi);
            //    result = "Credentials correct. ";

            //    var endpoints = rootAPI.RootGet();
            //    bool isADPCinstalled = endpoints?.Endpoints.Any(e => e.Name.StartsWith("ADPCGateway")) == true;
            //    if (isADPCinstalled)
            //    {
            //        result += "ADPC installed on the instance";
            //    }
            //    else
            //    {
            //        result += "ADPC is NOT installed on the instance";
            //    }

            //}
            //catch (Exception e)
            //{
            //    result += $"Could not connect to {SiteURL}";
            //}
            //finally
            //{
            //    authApi.TryLogout();
            //}
         //   return result;
        }

        public CustomerProfile GetCustomerProfileByCPID(string url, string username, string password, string tenant, string customerProfileId)
        {
            string result = string.Empty;
            int timeout = 100000;
            var Cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = Cookies;

            var Client = new HttpClient(handler);
            Client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
            result = string.Empty;
            string postBody = $"{{\"name\":\"{username}\", \"password\":\"{password}\", \"tenant\": \"{tenant}\"}}";

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = Client.PostAsync(url + "/entity/auth/login", new StringContent(postBody, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            //if (response?.IsSuccessStatusCode == true)
            //{
            //    result = "Credentials correct. ";
            //}

            response = Client.GetAsync(url + $"/entity/ADPCGateway/1/CustomerProfile/{customerProfileId}").GetAwaiter().GetResult();
            var p = response.EnsureSuccessStatusCode();
            CustomerProfile cp = ((CustomerProfile)ApiClientHelpers.Deserialize<CustomerProfile>(response));
            response = Client.PostAsync(url + "/entity/auth/logout", new StringContent(string.Empty, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            return cp;// new {CCPID = cp.CustomerProfileID, CustomerName = cp.CustomerDescription, CustomerCD = cp.CustomerName, Email = cp.Email };
        }

        public Transaction GetTransactionByID(string url, string username, string password, string tenant, string transactionID){
            string result = string.Empty;
            int timeout = 100000;
            var Cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = Cookies;

            var Client = new HttpClient(handler);
            Client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
            result = string.Empty;
            string postBody = $"{{\"name\":\"{username}\", \"password\":\"{password}\", \"tenant\": \"{tenant}\"}}";

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = Client.PostAsync(url + "/entity/auth/login", new StringContent(postBody, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            //if (response?.IsSuccessStatusCode == true)
            //{
            //    result = "Credentials correct. ";
            //}

            response = Client.GetAsync(url + $"/entity/ADPCGateway/1/Transaction?$filter=TransactionID eq '{transactionID}'").GetAwaiter().GetResult();
            var p = response.EnsureSuccessStatusCode();
            Transaction tran = ((List<Transaction>)ApiClientHelpers.Deserialize<List<Transaction>>(response)).FirstOrDefault();
            response = Client.PostAsync(url + "/entity/auth/logout", new StringContent(string.Empty, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            return tran;
            //return new {
            //    Amount = tran.TransactionAmount,
            //    AuthCode = "Capture",
            //    //CustomerId = "AACUSTOMER",
            //    //CcvVerificationStatus = CcvVerificationStatus.Match,
            //    //DocNum = transactionInfo[6] + "-" + transactionInfo[7],
            //    ExpireAfterDays = 30,
            //    PaymentId = tran.TransactionDocument,
            //    SubmitTime = tran.TransactionDate,
            //    TranID = tran.TransactionID,
            //    TranStatus = tran.TransactionStatus,
            //    //TranType = (CCTranType)Enum.Parse(typeof(CCTranType), "0"),
            //    ResponseReasonCode = 200,
            //    ResponseReasonText = "Success",
            //    CardType = "Vis",                                                        //As Card Type comes from the Processing Center
            //    //CardTypeCode = CCCardType.Visa,
            //    TranUID = tran.Tranuid
            //};
        }

        public List<Transaction> GetUnsettledTransactions(string url, string username, string password, string tenant)
        {
            string result = string.Empty;
            int timeout = 100000;
            var Cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = Cookies;

            var Client = new HttpClient(handler);
            Client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
            result = string.Empty;
            string postBody = $"{{\"name\":\"{username}\", \"password\":\"{password}\", \"tenant\": \"{tenant}\"}}";

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = Client.PostAsync(url + "/entity/auth/login", new StringContent(postBody, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            //if (response?.IsSuccessStatusCode == true)
            //{
            //    result = "Credentials correct. ";
            //}

            response = Client.GetAsync(url + $"/entity/ADPCGateway/1/Transaction?$filter=TransactionStatus ne 'Settled Successfully'").GetAwaiter().GetResult();
            var p = response.EnsureSuccessStatusCode();
            List<Transaction> trs = (List<Transaction>)ApiClientHelpers.Deserialize<List<Transaction>>(response);
            response = Client.PostAsync(url + "/entity/auth/logout", new StringContent(string.Empty, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            return trs;
        }

        public string TestConnection(string SiteURL, string Username, string Password, string Tenant = null)
        {
            int timeout = 100000;
            var Cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = Cookies;

            var Client = new HttpClient(handler);
            Client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
            string result = string.Empty;
            string postBody = $"{{\"name\":\"{Username}\", \"password\":\"{Password}\", \"tenant\": \"{Tenant}\"}}";

            Client.DefaultRequestHeaders.Accept .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = Client.PostAsync(SiteURL + "/entity/auth/login", new StringContent(postBody, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            if (response?.IsSuccessStatusCode == true) {
                result = "Credentials correct. ";
            }

            response = Client.GetAsync(SiteURL + "/entity").GetAwaiter().GetResult();
            var p = response.EnsureSuccessStatusCode();
            if (response.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("ADPCGateway")){
                result += "ADPC installed on the instance";
            }
            else
            {
                result += "ADPC is NOT installed on the instance";
            }

            response = Client.PostAsync(SiteURL + "/entity/auth/logout", new StringContent(string.Empty, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            //    var client = new ApiClient(SiteURL);

            //try
            //{
            //    // Task.Run(() => client.Login(Username, Password, Tenant)).Wait();
            //    client.Login(Username, Password, Tenant);
            //client.Login(Username, Password, Tenant, null, null);

            //var endpoints = Task.Run(() => client.RootGet()).Result;
            //bool isADPCinstalled = endpoints?.Endpoints.Any(e => e.Name.StartsWith("ADPCGateway")) == true;
            //if (isADPCinstalled)
            //{
            //    result += "ADPC installed on the instance";
            //}
            //else
            //{
            //    result += "ADPC is NOT installed on the instance";
            //}

            //}
            //catch (Exception e)
            //{
            //    result += $"Could not connect to {SiteURL}";
            //}
            //finally
            //{
            //    Task.Run(() => client.TryLogout());
            //}
            return result;
        }
    }
}