using System;
using System.Collections.Generic;
using PX.CCProcessingBase.Interfaces.V2;

namespace AcumaticaDummyCreditCardPlugin
{
    [PX.CCProcessingBase.Attributes.PXDisplayTypeName("Cookieless Processing Plug-in")]
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
            var settings = new List<SettingsDetail>();


            settings.Add( new SettingsDetail
                {
                    DetailID = "CLID",
                    Descr = "CLDescr",
                    DefaultValue = "CLDefaultValue"
                });
            return settings;
        }

        public void TestCredentials(IEnumerable<SettingsValue> settingValues)
        {
            throw new NotImplementedException();
        }

        public string ValidateSettings(SettingsValue setting)
        {
            return string.Empty;
        }
    }
}