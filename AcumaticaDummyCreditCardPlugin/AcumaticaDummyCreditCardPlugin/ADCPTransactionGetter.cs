using AcumaticaDummyProcessingCenterGatewayAPI;
using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp;
using System.Text;
using System.Threading.Tasks;
using Acumatica.ADPCGateway.Model;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPTransactionGetter : ICCTransactionGetter
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPTransactionGetter(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }
        public TransactionData GetTransaction(string transactionId)
        {
            string url = settingValues.First(x => x.DetailID == ADCPConstants.ADPCURL).Value;
            string username = settingValues.First(x => x.DetailID == ADCPConstants.ADPCUserName).Value;
            string password = settingValues.First(x => x.DetailID == ADCPConstants.ADPCPassword).Value;
            string tenant = settingValues.First(x => x.DetailID == ADCPConstants.ADPCTenant).Value;

            Requests req = new Requests();
            Transaction tran = req.GetTransactionByID(url, username, password, tenant, transactionId);

            //string[] transactionInfo = transactionId.Split('-');
            //int? expDate = null;
            //if (transactionInfo[3] == "AuthorizeOnly") { expDate = 1; }
            //string cardType = transactionInfo[0].Length > 3 ? transactionInfo[0].Substring(0, 3) : "OTH";
            Random rnd = new Random();
            TransactionData td = new TransactionData
            {
                Amount = (decimal)tran.TransactionAmount.Value,
                AuthCode = "Capture",
                // CustomerId = "AACUSTOMER",
                //   CcvVerificationStatus = CcvVerificationStatus.Match,
                PaymentId = tran.TransactionDocument,
                ExpireAfterDays = 30,
                SubmitTime = (DateTime)tran.TransactionDate.Value,
                TranID = tran.TransactionID,
                TranStatus = CCTranStatus.Approved,// tran.TransactionStatus.Value,
                TranType = (CCTranType)Enum.Parse(typeof(CCTranType), "0"),
                ResponseReasonCode = 200,
                ResponseReasonText = "Success",
                CardType = "Vis",                                                        //As Card Type comes from the Processing Center
                CardTypeCode = CCCardType.Visa,                              //As Acumatica Internal enum  
                TranUID = tran.Tranuid //Setting TranUid returned from Processing Center
            };                                                                                                                                                                    //Unlucky initial choice of delimiter for this project - Guid had to be glued together here

            return td;
        }

        public IEnumerable<TransactionData> GetTransactionsByCustomer(string customerProfileId, TransactionSearchParams searchParams = null)
        {
            Random rnd = new Random();
            TransactionData td = new TransactionData
            {
                Amount = 10,
                AuthCode = "Capture",
                CustomerId = "AACUSTOMER",
                CcvVerificationStatus = CcvVerificationStatus.Match,
                //DocNum = transactionInfo[6] + "-" + transactionInfo[7],
                ExpireAfterDays = 30,
                PaymentId = rnd.Next(100000, 999999).ToString(),
                SubmitTime = DateTime.UtcNow,
                TranID = "123123123",
                TranStatus = (CCTranStatus)Int32.Parse("0"),
                TranType = (CCTranType)Enum.Parse(typeof(CCTranType), "0"),
                ResponseReasonCode = 200,
                ResponseReasonText = "Success",
                CardType = "Vis",                                                        //As Card Type comes from the Processing Center
                CardTypeCode = CCCardType.Visa,                              //As Acumatica Internal enum  
                                                                             //  TranUID = Guid.Parse(string.Format("{0}-{1}-{2}-{3}-{4}", transactionInfo[8], transactionInfo[9], transactionInfo[10], transactionInfo[11], transactionInfo[12])) //Setting TranUid returned from Processing Center
            };                                                                                                                                                                    //Unlucky initial choice of delimiter for this project - Guid had to be glued together here
            List<TransactionData> tdList = new List<TransactionData>(); 
            tdList.Add(td);
            return tdList;
        }

        public IEnumerable<TransactionData> GetUnsettledTransactions(TransactionSearchParams searchParams = null)
        {
            Random rnd = new Random();
            TransactionData td = new TransactionData
            {
                Amount = 10,
                AuthCode = "Capture",
                CustomerId = "AACUSTOMER",
                CcvVerificationStatus = CcvVerificationStatus.Match,
                //DocNum = transactionInfo[6] + "-" + transactionInfo[7],
                ExpireAfterDays = 30,
                PaymentId = rnd.Next(100000, 999999).ToString(),
                SubmitTime = DateTime.UtcNow,
                TranID = "123123123",
                TranStatus = (CCTranStatus)Int32.Parse("0"),
                TranType = (CCTranType)Enum.Parse(typeof(CCTranType), "0"),
                ResponseReasonCode = 200,
                ResponseReasonText = "Success",
                CardType = "Vis",                                                        //As Card Type comes from the Processing Center
                CardTypeCode = CCCardType.Visa,                              //As Acumatica Internal enum  
                                                                             //  TranUID = Guid.Parse(string.Format("{0}-{1}-{2}-{3}-{4}", transactionInfo[8], transactionInfo[9], transactionInfo[10], transactionInfo[11], transactionInfo[12])) //Setting TranUid returned from Processing Center
            };                                                                                                                                                                    //Unlucky initial choice of delimiter for this project - Guid had to be glued together here

            List<TransactionData> tdList = new List<TransactionData>();
            tdList.Add(td);
            return tdList;
        }
    }
}
