using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using System.Linq;
using System.Collections;

namespace AcumaticaDummyProcessingCenter
{
    public class ADPCTransactionEntry : PXGraph<ADPCTransactionEntry, ADPCTransaction>
    {

        public PXSave<ADPCTransaction> Save;
        public PXCancel<ADPCTransaction> Cancel;
        public PXSetup<ADPCSetup> Setup;


        public PXSelect<ADPCTransaction> Transaction;
        public PXSelect<ADPCPaymentProfile> PaymentProfile;
        public SelectFrom<ADPCTransactionHistory>.Where<ADPCTransactionHistory.transactionID.IsEqual<ADPCTransaction.transactionID.FromCurrent>>.OrderBy<ADPCTransactionHistory.changeDate.Desc>.View TransactionHistory;

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

            base.Persist();

        }
    }
}