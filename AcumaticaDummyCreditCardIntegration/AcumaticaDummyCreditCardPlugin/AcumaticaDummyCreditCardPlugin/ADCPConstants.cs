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
        public const string ADPCURL      = "ADPCURL";
        public const string ADPCUserName = "ADPCUserName";
        public const string ADPCPassword = "ADPCPassword";
        public const string ADPCTenant   = "ADPCTenant";
        #endregion

        #region Default Settings
        public const string DefaultADPCURL      = "https://localhost/AcumaticaSite";
        public const string DefaultADPCUserName = "admin";
        public const string DefaultADPCPassword = "123";
        public const string DefaultADPCTenant   = "Company";
        #endregion
    }
}
