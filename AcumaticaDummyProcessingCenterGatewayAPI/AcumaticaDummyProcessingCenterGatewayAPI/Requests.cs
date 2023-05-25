using Acumatica.Auth.Api;
using System;

namespace AcumaticaDummyProcessingCenterGatewayAPI
{
    public static class Requests
    {
        public static string TestConnection(string SiteURL, string Username, string Password, string Tenant = null)
        {
            var authApi = new AuthApi(SiteURL);
            string result;
            try
            {
                var configuration = authApi.LogIn(Username, Password, Tenant, null, null);
                authApi.TryLogout();
                result = "Connection to the instance is successful";
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
    }
}