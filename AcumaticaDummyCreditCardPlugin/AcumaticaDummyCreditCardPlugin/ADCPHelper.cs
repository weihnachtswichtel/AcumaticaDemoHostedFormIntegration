using PX.CCProcessingBase.Interfaces.V2;
using PX.Data.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcumaticaDummyCreditCardPlugin
{
    public static class ADCPHelper
    {

        public static Dictionary<string, CCCardType> MapCardType = new Dictionary<string, CCCardType>
        {
            {"V", CCCardType.Visa},
            {"M", CCCardType.MasterCard},
            {"A", CCCardType.AmericanExpress},
            {"U", CCCardType.UnionPay }
        };

        public static Dictionary<string, CCTranType> MapTranType = new Dictionary<string, CCTranType>
        {
            {"Authorization", CCTranType.AuthorizeOnly},
            {"Capture", CCTranType.AuthorizeAndCapture},
           // {"VIS", CCTranType.CaptureOnly},
            {"Void", CCTranType.Void},
            {"Refund", CCTranType.VoidOrCredit},
            {"Credit", CCTranType.Credit},
            //{"VIS", CCTranType.PriorAuthorizedCapture}
        };

        public static Dictionary<string, CCTranStatus> MapTranStatus = new Dictionary<string, CCTranStatus>
        {
            {"Approved", CCTranStatus.Approved},
            {"Declined", CCTranStatus.Declined},
            {"Error", CCTranStatus.Error},
            {"Expired", CCTranStatus.Expired},
            //{"Error", CCTranStatus.GeneralError},
            {"Held For Review", CCTranStatus.HeldForReview},
            {"Refund Settled Successfully", CCTranStatus.RefundSettledSuccessfully},
            {"Settled Successfully", CCTranStatus.SettledSuccessfully},
           // {"VIS", CCTranStatus.SettlementError},
            {"Unknown", CCTranStatus.Unknown},
            {"Voided", CCTranStatus.Voided},
        };

        public static Acumatica.ADPCGateway.PCCredentials GetPCGredentials(IEnumerable<SettingsValue> settingValues)
        {
            return new Acumatica.ADPCGateway.PCCredentials {
                Url = settingValues.First(x => x.DetailID == ADCPConstants.ADPCURL).Value,
                UserName = settingValues.First(x => x.DetailID == ADCPConstants.ADPCUserName).Value,
                Password = settingValues.First(x => x.DetailID == ADCPConstants.ADPCPassword).Value,
                Tenant = settingValues.First(x => x.DetailID == ADCPConstants.ADPCTenant).Value,
            };
        }
    }
}
