using PX.Common;

namespace AcumaticaDummyCreditCardPlugin
{
    [PXLocalizable()]
    public static class ADCPMessages
    {
        #region Keys for the setting descriptions
        public const string ADPCURLDesc      = "URL for Acumatica with ADPC deloyed";
        public const string ADPCUserNameDesc = "Username for Acumatica with ADPC deloyed";
        public const string ADPCPasswordDesc = "Password for Acumatica with ADPC deloyed";
        public const string ADPCTenantDesc   = "Tenant for Acumatica with ADPC deloyed";
        #endregion

        #region Validation Messages
        public const string NoSetting   = "Setting is missing";
        public const string NoValue     = "No value set for {0} setting";
        #endregion
    }
}
