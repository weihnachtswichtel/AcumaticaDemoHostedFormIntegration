using Acumatica.Auth.Api;
using Acumatica.RESTClient.RootApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AcumaticaDummyProcessingCenterGatewayAPI
{
    public class Requests
    {
        public string TestConnection(string SiteURL, string Username, string Password, string Tenant = null)
        {
            var tasks = new List<Task>();

            var authApi = new AuthApi(SiteURL);
            string result = string.Empty;
            try
            {
                authApi.LogIn(Username, Password, Tenant, null, null);
                var rootAPI = new RootApi(authApi.ApiClient);
                result = "Credentials correct. ";

                var endpoints = rootAPI.RootGet();
                bool isADPCinstalled = endpoints?.Endpoints.Any(e => e.Name.StartsWith("ADPCGateway")) == true;
                if (isADPCinstalled)
                {
                    result += "ADPC installed on the instance";
                }
                else
                {
                    result += "ADPC is NOT installed on the instance";
                }

            }
            catch (Exception e)
            {
                result += $"Could not connect to {SiteURL}";
            }
            finally
            {
                authApi.TryLogout();
            }
            return result;
        }
    }
}