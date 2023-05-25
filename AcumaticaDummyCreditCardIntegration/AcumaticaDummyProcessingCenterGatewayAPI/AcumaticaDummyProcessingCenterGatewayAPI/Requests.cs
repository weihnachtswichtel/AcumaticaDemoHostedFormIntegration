namespace AcumaticaDummyProcessingCenterGatewayAPI
{
    public static class Requests
    {
        public static void TestConnection(string SiteURL, string Username, string Password, string Tenant = null)
        {
            var authApi = new AuthApi(SiteURL);
            string result;
            try
            {
                var configuration = authApi.LogIn(username, password, tenant, null, null);
                authApi.TryLogout();
                result = "Connection to the instance successful";
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
    }
}