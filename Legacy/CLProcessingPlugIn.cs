using System;
using System.Collections.Generic;
using PX.CCProcessingBase.Interfaces.V2;

namespace CookielessHostedForm
{
    [PX.CCProcessingBase.Attributes.PXDisplayTypeName("Cookieless Processing Plug-in")]
    public class CLProcessingPlugIn : ICCProcessingPlugin
    {
        public T CreateProcessor<T>(IEnumerable<SettingsValue> settingValues) where T : class
        {
            if (typeof(T) == typeof(ICCProfileProcessor))
            {
                return new CLProfileProcessor(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCHostedFormProcessor))
            {
                return new CLHostedFormProcessor(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCTransactionProcessor))
            {
                return new CLTransactionProcessor(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCHostedPaymentFormProcessor))
            {
                return new CLHostedPaymentFormProcessor(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCHostedPaymentFormResponseParser))
            {
                return new CLHostedPaymentFormResponseParser(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCTransactionGetter))
            {
                return new CLTransactionGetter(settingValues) as T;
            }
            if (typeof(T) == typeof(ICCProfileCreator))
            {
                return new CLProfileCreator(settingValues) as T;
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