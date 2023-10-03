using System;
using System.Collections;
using System.Linq.Expressions;
using PX.CCProcessing.Fortis.V2;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.EP;
using PX.Objects.Extensions.PaymentTransaction;
using static PX.Objects.TX.CSTaxCalcType;

namespace AcumaticaDummyProcessingCenter
{
  public class ADPCSetupMaint : PXGraph<ADPCSetupMaint>
  {

    public PXSave<ADPCSetup> Save;
    public PXCancel<ADPCSetup> Cancel;

    public SelectFrom<ADPCSetup>.View Setup;

    public SelectFrom<Customer>.View customer;
    public PXAction<ADPCSetup> ImportCardPayments;

    //[PXButton, PXUIField(DisplayName = "Import Cards")]
    //public virtual IEnumerable importCardPayments(PXAdapter adapter) {
    //        //     PXLongOperation.StartOperation(this, () =>
    //        //     {

    //        //input paramaters
    //        string CustomerCD = "AACUSTOMER";
    //        string ProcessingCenterID = "ADCP";
    //        string PaymentMethodID = "ADCP";
    //        string TransactionID = "1232123123";
    //        decimal? Amount = 10;

    //        //Implementation

    //        //Creating ARPayment
    //        ARPaymentEntry pe = PXGraph.CreateInstance<ARPaymentEntry>();
    //        Customer cust = customer.Search<Customer.acctCD>(CustomerCD);
    //        ARPayment payment = new ARPayment();
    //        payment.CustomerID = cust.BAccountID;
    //        payment.CuryOrigDocAmt = Amount;
    //        payment.PaymentMethodID = PaymentMethodID;
    //        pe.Document.Current = pe.Document.Insert(payment);
    //        pe.Document.Current.PMInstanceID = PaymentTranExtConstants.NewPaymentProfile;
    //        pe.Document.Current.ProcessingCenterID = ProcessingCenterID;
    //        pe.Document.Current.NewCard = true;
    //        pe.Document.Insert(pe.Document.Current);
    //        pe.Save.Press();

    //        //Recording transaction
    //        //var ext = pe.GetExtension<ARPaymentEntryPaymentTransaction>();
    //        var ext = pe.GetExtension<MyARPaymentEntryPaymentTransaction>();


    //        ext.InputPmtInfo.Current.PCTranNumber = TransactionID;
    //        ext.RecordCCPayment(ARPaymentEntry.CreateAdapterWithDummyView(pe, pe.Document.Current));


    //        return adapter.Get();
    //}

    }

    //public class MyARPaymentEntryPaymentTransaction : PaymentTransactionAcceptFormGraph<ARPaymentEntry, ARPayment>{
    //    public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.integratedCardProcessing>();

    //    protected override PaymentTransactionDetailMapping GetPaymentTransactionMapping()
    //    {
    //        return new PaymentTransactionDetailMapping(typeof(CCProcTran));
    //    }

    //    protected override PaymentMapping GetPaymentMapping()
    //    {
    //        return new PaymentMapping(typeof(ARPayment));
    //    }

    //    protected override ExternalTransactionDetailMapping GetExternalTransactionMapping()
    //    {
    //        return new ExternalTransactionDetailMapping(typeof(ExternalTransaction));
    //    }

    //    [PXUIField(DisplayName = "Record Card Payment", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
    //    [PXProcessButton]
    //    public override IEnumerable RecordCCPayment(PXAdapter adapter)
    //    {

    //        if (this.Base.Document.Current != null &&
    //        this.Base.Document.Current.IsCCPayment == true)
    //        {
    //           ARPayment payment = base.Base.Document.Current;
    //           SelectedProcessingCenter = payment.ProcessingCenterID;
    //           SelectedBAccount = payment.CustomerID;
    //           SelectedPaymentMethod = payment.PaymentMethodID;
    //           return base.RecordCCPayment(adapter);
    //        }
    //        InputPmtInfo.View.Clear();
    //        InputPmtInfo.Cache.Clear();
    //        return adapter.Get();
    //    }

    //    protected override ARPayment SetCurrentDocument(ARPaymentEntry graph, ARPayment doc)
    //    {
    //       PXSelectJoin<ARPayment, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>>, Where<ARPayment.docType, Equal<Optional<ARPayment.docType>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>> document = graph.Document;
    //       document.Current = document.Search<ARPayment.refNbr>(doc.RefNbr, new object[1] { doc.DocType });
    //       return document.Current;
    //    }

    //    protected override PaymentTransactionGraph<ARPaymentEntry, ARPayment> GetPaymentTransactionExt(ARPaymentEntry graph)
    //    {
    //        return graph.GetExtension<ARPaymentEntryPaymentTransaction>();
    //    }

   // }

}