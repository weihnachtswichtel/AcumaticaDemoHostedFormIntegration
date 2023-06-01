using System.Collections.Generic;
using System;
using PX.CCProcessingBase.Interfaces.V2;
using AcumaticaDummyProcessingCenterGatewayAPI;
using System.Linq;

namespace AcumaticaDummyCreditCardPlugin
{
    [PX.CCProcessingBase.Attributes.PXDisplayTypeName("Acumatica Dummy Credit Card Plug-in")]
    public class ADCPProcessingPlugIn : ICCProcessingPlugin
    {
        public T CreateProcessor<T>(IEnumerable<SettingsValue> settingValues) where T : class
        {
            if (typeof(T) == typeof(ICCProfileProcessor))
            {
                return new ADCPProfileProcessor(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCHostedFormProcessor))
            {
                return new ADCPHostedFormProcessor(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCTransactionProcessor))
            {
                return new ADCPTransactionProcessor(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCHostedPaymentFormProcessor))
            {
                return new ADCPHostedPaymentFormProcessor(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCHostedPaymentFormResponseParser))
            {
                return new ADCPHostedPaymentFormResponseParser(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCTransactionGetter))
            {
                return new ADCPTransactionGetter(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCProfileCreator))
            {
                return new ADCPProfileCreator(settingValues) as T;
            }
            return null;
        }

        public IEnumerable<SettingsDetail> ExportSettings()
        {
            var settings = new List<SettingsDetail>
            {
                new SettingsDetail
                {
                    DetailID     = ADCPConstants.ADPCURL,
                    Descr        = ADCPMessages.ADPCURLDesc,
                    DefaultValue = ADCPConstants.DefaultADPCURL
                },
                new SettingsDetail
                {
                    DetailID     = ADCPConstants.ADPCUserName,
                    Descr        = ADCPMessages.ADPCUserNameDesc,
                    DefaultValue = ADCPConstants.DefaultADPCUserName
                },
                new SettingsDetail
                {
                    DetailID             = ADCPConstants.ADPCPassword,
                    Descr                = ADCPMessages.ADPCPasswordDesc,
                    DefaultValue         = ADCPConstants.DefaultADPCPassword,
                    IsEncryptionRequired = true
                },
                new SettingsDetail
                {
                    DetailID     = ADCPConstants.ADPCTenant,
                    Descr        = ADCPMessages.ADPCTenantDesc,
                    DefaultValue = ADCPConstants.DefaultADPCTenant
                }
            };
            return settings;
        }

        public void TestCredentials(IEnumerable<SettingsValue> settingValues)
        {
            string url      = settingValues.First(x => x.DetailID == ADCPConstants.ADPCURL).Value;
            string username = settingValues.First(x => x.DetailID == ADCPConstants.ADPCUserName).Value;
            string password = settingValues.First(x => x.DetailID == ADCPConstants.ADPCPassword).Value;
            string tenant   = settingValues.First(x => x.DetailID == ADCPConstants.ADPCTenant).Value;

            Requests req = new Requests();
            string result = req.TestConnection(url, username, password, tenant);

            throw new Exception(result);
        }

        public string ValidateSettings(SettingsValue setting)
        {
            string result = string.Empty;
            if (setting == null)
            {
                return ADCPMessages.NoSetting;
            }
            switch (setting.DetailID)
            {
                case ADCPConstants.ADPCURL:
                    result = string.IsNullOrEmpty(setting.Value) ? string.Format(ADCPMessages.NoValue, setting.DetailID) : string.Empty ;
                    break;
                case ADCPConstants.ADPCUserName:
                    result = string.IsNullOrEmpty(setting.Value) ? string.Format(ADCPMessages.NoValue, setting.DetailID) : string.Empty;
                    break;
                case ADCPConstants.ADPCPassword:
                    result = string.IsNullOrEmpty(setting.Value) ? string.Format(ADCPMessages.NoValue, setting.DetailID) : string.Empty;
                    break;
            }
            return result;
        }
    }
}