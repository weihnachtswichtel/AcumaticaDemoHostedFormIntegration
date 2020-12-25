using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookielessHostedForm
{
    public class CLTransactionGetter : ICCTransactionGetter
    {
        private IEnumerable<SettingsValue> settingValues;

        public CLTransactionGetter(IEnumerable<SettingsValue> settingValues)
        {
            this.settingValues = settingValues;
        }
        public TransactionData GetTransaction(string transactionId)
        {
            string[] transactionInfo = transactionId.Split('-');


            TransactionData td = new TransactionData
            {
                Amount = Decimal.Parse(transactionInfo[4]),
                AuthCode = "AUTH" + transactionInfo[1],
                CustomerId = transactionInfo[5],
                CcvVerificationStatus = CcvVerificationStatus.Match,
                DocNum = transactionInfo[6]+ "-"+ transactionInfo[7],
                ExpireAfterDays = 1,
                PaymentId = string.Format("{0}-{1}-{2}", transactionInfo[0], transactionInfo[1], transactionInfo[2]),
                SubmitTime = DateTime.UtcNow,
                TranID = "TRAN" + transactionInfo[1],
                TranStatus = CCTranStatus.Approved,
                TranType = transactionInfo[3].Contains("Auth") ? CCTranType.AuthorizeOnly : CCTranType.AuthorizeAndCapture,
                ResponseReasonCode = 200,
                ResponseReasonText = "Success"
            };

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
