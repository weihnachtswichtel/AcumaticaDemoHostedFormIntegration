using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string[] transactionInfo = transactionId.Split('-');
            int? expDate = null;
            if (transactionInfo[3] == "AuthorizeOnly") { expDate = 1; }
            string cardType = transactionInfo[0].Length > 3 ? transactionInfo[0].Substring(0, 3) : "OTH";
            TransactionData td = new TransactionData
            {
                Amount = Decimal.Parse(transactionInfo[4]),
                AuthCode = transactionInfo[3] == "AuthorizeOnly" ? "AUTH" + transactionInfo[1] : null,
                CustomerId = transactionInfo[5],
                CcvVerificationStatus = CcvVerificationStatus.Match,
                DocNum = transactionInfo[6] + "-" + transactionInfo[7],
                ExpireAfterDays = expDate,
                PaymentId = string.Format("{0}-{1}-{2}", transactionInfo[0], transactionInfo[1], transactionInfo[2]),
                SubmitTime = DateTime.UtcNow,
                TranID = "TRAN" + transactionInfo[1],
                TranStatus = (CCTranStatus)Int32.Parse(transactionInfo[13]),
                TranType = (CCTranType)Enum.Parse(typeof(CCTranType), transactionInfo[3]),
                ResponseReasonCode = 200,
                ResponseReasonText = "Success",
                CardType = cardType,                                                        //As Card Type comes from the Processing Center
                CardTypeCode = ADCPHelper.MapCardType[cardType],                              //As Acumatica Internal enum  
                TranUID = Guid.Parse(string.Format("{0}-{1}-{2}-{3}-{4}", transactionInfo[8], transactionInfo[9], transactionInfo[10], transactionInfo[11], transactionInfo[12])) //Setting TranUid returned from Processing Center
            };                                                                                                                                                                    //Unlucky initial choice of delimiter for this project - Guid had to be glued together here

            return td;
        }

        public IEnumerable<TransactionData> GetTransactionsByCustomer(string customerProfileId, TransactionSearchParams searchParams = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TransactionData> GetUnsettledTransactions(TransactionSearchParams searchParams = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
