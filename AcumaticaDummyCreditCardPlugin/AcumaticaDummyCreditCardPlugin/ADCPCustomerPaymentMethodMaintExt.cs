using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;
using System.Collections;
using static PX.Objects.AR.CustomerPaymentMethodMaint;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPCustomerPaymentMethodMaintExt : PXGraphExtension<PaymentProfileHostedForm, CustomerPaymentMethodMaint>
    {
        public delegate IEnumerable SyncCCPaymentMethodsDelegate(PXAdapter adapter);

        public static bool IsActive() =>  PXAccess.FeatureInstalled<FeaturesSet.customCCIntegration>();

        [PXOverride]
        public IEnumerable SyncCCPaymentMethods(PXAdapter adapter, SyncCCPaymentMethodsDelegate baseMethod)
        {
            var request = System.Web.HttpContext.Current.Request;
            PXContext.Session.SetString("CLResponseToken", request.Form.Get("__TRANID"));

            return baseMethod(adapter);
        }

    }
}
