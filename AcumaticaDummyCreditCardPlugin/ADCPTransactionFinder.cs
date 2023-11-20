using Acumatica.ADPCGateway;
using Acumatica.ADPCGateway.Model;
using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcumaticaDummyCreditCardPlugin
{
    public class ADCPTransactionFinder : ICCTransactionFinder
    {
        private IEnumerable<SettingsValue> settingValues;

        public ADCPTransactionFinder(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }

        public TransactionData FindTransaction(TransactionSearchParams prms)
        {
            Transaction tran = Requests.FindTransactionByGUID(ADCPHelper.GetPCGredentials(settingValues), prms?.TransactionGuid);

            TransactionData td = new TransactionData
            {
                Amount = (decimal)tran.TransactionAmount.Value,
                AuthCode = "123",
                //CustomerId = "AACUSTOMER",
                CcvVerificationStatus = CcvVerificationStatus.Match,
                DocNum = tran.TransactionDocument,
                ExpireAfterDays = (tran.TransactionExpirationDate.Value - tran.TransactionDate.Value)?.Days,
                PaymentId = tran.PaymentProfileID.ToString(),
                SubmitTime = (DateTime)tran.TransactionDate.Value,
                TranID = tran.TransactionID,
                TranStatus = ADCPHelper.MapTranStatus[tran.TransactionStatus],
                TranType = ADCPHelper.MapTranType[tran.TransactionType],
                ResponseReasonCode = 200,
                ResponseReasonText = "Success",
                CardType = tran.PaymentProfileIDCardType,                                                        //As Card Type comes from the Processing Center
                CardTypeCode = ADCPHelper.MapCardType[tran.PaymentProfileIDCardType],                            //As Acumatica Internal enum                            
                TranUID = tran.Tranuid                                                                           //Setting TranUid returned from Processing Center
            };

            return td;
        }
    }
}