using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcumaticaDummyCreditCardPlugin
{
    public static class ADCPConstants
    {
        #region Keys for the setting details
        //Not more than 10 characters
        public const string ADPCURL      = "URL";
        public const string ADPCUserName = "Username";
        public const string ADPCPassword = "Password";
        public const string ADPCTenant   = "Tenant";
        #endregion

        #region Default Settings
        public const string DefaultADPCURL      = "https://localhost/AcumaticaSite";
        public const string DefaultADPCUserName = "admin";
        public const string DefaultADPCPassword = "123";
        public const string DefaultADPCTenant   = "Company";
        #endregion
    }
}
