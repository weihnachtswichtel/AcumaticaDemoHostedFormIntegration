using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using System.Linq;
using System.Collections;
using PX.Objects.SO;

namespace AcumaticaDummyProcessingCenter
{
    public class ADPCTransactionEntry : PXGraph<ADPCTransactionEntry, ADPCTransaction>
    {

        public PXSetup<ADPCSetup> Setup;


        public PXSelect<ADPCTransaction> Transaction;
        public PXSelect<ADPCPaymentProfile> PaymentProfile;
        public SelectFrom<ADPCTransactionHistory>.Where<ADPCTransactionHistory.transactionID.IsEqual<ADPCTransaction.transactionID.FromCurrent>>.OrderBy<ADPCTransactionHistory.changeDate.Desc>.View TransactionHistory;

        protected virtual void _(Events.FieldUpdated<ADPCTransaction, ADPCTransaction.paymentProfileID> e) {
            if (e != null && string.IsNullOrEmpty(e.Row.CustomerProfileID))
            {
                e.Cache.SetDefaultExt<ADPCTransaction.customerProfileID>(e.Row);
            }
        }

        public override void Persist()
        {
            var latestHistoryRecord = TransactionHistory.SelectSingle();

            if (latestHistoryRecord == null || latestHistoryRecord.TransactionStatus != Transaction.Current.TransactionStatus || latestHistoryRecord.TransactionType != Transaction.Current.TransactionType)
            {
                ADPCTransactionHistory hist = TransactionHistory.Insert();
                hist.TransactionType = Transaction.Current.TransactionType;
                hist.TransactionStatus = Transaction.Current.TransactionStatus;
                hist.ChangeDate = DateTime.Now;
                TransactionHistory.Insert(hist);
            }

            if(Transaction.Current.TransactionType == "A")
            {
                if (Transaction.Current.TransactionExpirationDate == null) {
                    Transaction.Current.TransactionExpirationDate = DateTime.UtcNow.AddDays(20);
                }
                if (string.IsNullOrEmpty(Transaction.Current.AuthorizationNbr)){
                    Transaction.Current.AuthorizationNbr = "123456";
                }
            }

            base.Persist();

        }
    }
}