using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp;
using System.Text;
using System.Threading.Tasks;
using Acumatica.ADPCGateway.Model;
using PX.Objects.CA;
using Acumatica.ADPCGateway;

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
            Transaction tran = Requests.GetTransactionByID(ADCPHelper.GetPCGredentials(settingValues), transactionId);

            TransactionData td = new TransactionData
            {
                Amount = (decimal)tran.TransactionAmount.Value,
                AuthCode = tran.AuthorizationNbr,
                // CustomerId = "AACUSTOMER",
                //   CcvVerificationStatus = CcvVerificationStatus.Match,
                PaymentId = tran.PaymentProfileID.ToString(),
                DocNum = tran.TransactionDocument,
                ExpireAfterDays = 30,
                SubmitTime = (DateTime)tran.TransactionDate.Value,
                TranID = tran.TransactionID,
                TranStatus = ADCPHelper.MapTranStatus[tran.TransactionStatus],
                TranType = ADCPHelper.MapTranType[tran.TransactionType],
                ResponseReasonCode = 200,
                ResponseReasonText = tran.TransactionStatus,
                CardType = tran.PaymentProfileIDCardType,                                                        //As Card Type comes from the Processing Center
                CardTypeCode = ADCPHelper.MapCardType[tran.PaymentProfileIDCardType],                              //As Acumatica Internal enum  
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
                //CustomerId = "AACUSTOMER",
                CcvVerificationStatus = CcvVerificationStatus.Match,
                //DocNum = transactionInfo[6] + "-" + transactionInfo[7],
                ExpireAfterDays = 30,
                PaymentId = rnd.Next(100000, 999999).ToString(),
                SubmitTime = DateTime.UtcNow,
                TranID = "123123123",
                TranStatus = CCTranStatus.Approved,
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
            List<Transaction> trs = Requests.GetUnsettledTransactions(ADCPHelper.GetPCGredentials(settingValues));

            List<TransactionData> tdList = new List<TransactionData>();


            Transaction tran = trs.OrderByDescending(t => t.TransactionID.Value).FirstOrDefault();

            TransactionData td = new TransactionData
            {
                Amount = (decimal)tran.TransactionAmount.Value,
                AuthCode = "123",
                //CustomerId = "AACUSTOMER",
                CcvVerificationStatus = CcvVerificationStatus.Match,
                DocNum = tran.TransactionDocument,
                ExpireAfterDays = 30,
                PaymentId = tran.PaymentProfileID.ToString(),
                SubmitTime = (DateTime)tran.TransactionDate.Value,
                TranID = tran.TransactionID,
                TranStatus = ADCPHelper.MapTranStatus[tran.TransactionStatus],
                TranType = ADCPHelper.MapTranType[tran.TransactionType],
                ResponseReasonCode = 200,
                ResponseReasonText = "Success",
                CardType = tran.PaymentProfileIDCardType,                                                        //As Card Type comes from the Processing Center
                CardTypeCode = ADCPHelper.MapCardType[tran.PaymentProfileIDCardType],                              //As Acumatica Internal enum                             //As Acumatica Internal enum  
                                                                                                                   //  TranUID = Guid.Parse(string.Format("{0}-{1}-{2}-{3}-{4}", transactionInfo[8], transactionInfo[9], transactionInfo[10], transactionInfo[11], transactionInfo[12])) //Setting TranUid returned from Processing Center
                TranUID = tran.Tranuid
            };                                                                                                                                                                    //Unlucky initial choice of delimiter for this project - Guid had to be glued together here
            tdList.Add(td);

            return tdList;
        }
    }
}
