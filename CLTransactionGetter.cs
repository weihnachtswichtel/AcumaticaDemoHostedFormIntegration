using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
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
            if (transactionId.StartsWith("TRAN")){
                //This had to be worked around later. The TranID cannot hold the length of transactionId, but it TranID is used for Validation - it has no data in it.
                string tempTranID = PXContext.Session["TempTranID"] as string;
                if (!string.IsNullOrEmpty(tempTranID)){
                    transactionId = tempTranID;
                }
            }

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
                //TranID = transactionId,
                TranStatus = (CCTranStatus)Int32.Parse(transactionInfo[13]),
                TranType = (CCTranType)Enum.Parse(typeof(CCTranType), transactionInfo[3]),
                ResponseReasonCode = 200,
                ResponseReasonText = "Success",
                CardType = cardType,                                                        //As Card Type comes from the Processing Center
 //               CardTypeCode = CLHelper.MapCardType[cardType],                              //As Acumatica Internal enum  
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
            //This should be implemented differently in real life scenario. This just to overcome the situation that there is no processing center that could be requested
            string mobile = PXContext.Session["MobileDummyFix"] as string;
            if (string.IsNullOrEmpty(mobile))
            {
                return new List<TransactionData>();
            }
            else {
                string[] values = mobile.Split(new string[]{Environment.NewLine}, StringSplitOptions.None);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (string value in values){
                    string[] kvp = value.Split(',', 2);
                    dict.Add(kvp[0].Replace("[", string.Empty), kvp[1].Replace("]", string.Empty));
                }
                
                Random random = new Random();
                string dymmyPaymentProfileId = String.Format("{0}-{1}-{2}", "VISdummyToken", (DateTimeOffset.Now.ToUnixTimeMilliseconds() + 31536000000).ToString(),  random.Next(1234, 9999).ToString());
                string tranId = String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", dymmyPaymentProfileId,
                                                                                dict["Type"].Replace(" ", string.Empty),
                                                                                dict["Amount"].Replace(" ", string.Empty),
                                                                                dict["CPID"].Replace(" ", string.Empty),
                                                                                dict["DocType"].Replace(" ", string.Empty),
                                                                                dict["DocRefNbr"].Replace(" ", string.Empty),
                                                                                dict["TranUID"].Replace(" ", string.Empty),
                                                                                "0"); //Hardcoded transtatus to simplify Mobile demo.
                PXContext.Session.SetString("TempTranID", tranId);
                return new List<TransactionData> {
                    new TransactionData{
                        Amount = Convert.ToDecimal(dict["Amount"]),
                        DocNum = dict["DocType"].Replace(" ", string.Empty)+dict["DocRefNbr"].Replace(" ", string.Empty),
                        TranID = tranId,
                        CustomerId = dict["CPID"].Replace(" ", string.Empty),
                        PaymentId = dymmyPaymentProfileId,
                     }
                };

            }

        }
    }
}
