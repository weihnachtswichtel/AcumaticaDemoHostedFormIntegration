//using PX.Data;
//using PX.Objects.AR;
//using PX.Objects.AR.GraphExtensions;
//using PX.Objects.CR.Extensions;
//using PX.Objects.Extensions.PaymentTransaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.Extensions.PaymentTransaction;

namespace AcumaticaDummyProcessingCenter
{
    public class ARPaymentEntryPaymentTransactionExt : PXGraphExtension<ARPaymentEntryPaymentTransaction, ARPaymentEntry>
    {
        public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.customCCIntegration>(); //for 2023R1 and further
       // public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.integratedCardProcessing>(); //for 2022R2 and prior



        //public delegate IEnumerable RecordCCPaymentDelegate(PXAdapter adapter);

        //[PXOverride]
        //public virtual IEnumerable RecordCCPayment(PXAdapter adapter, RecordCCPaymentDelegate baseDelegate)
        //{
        //    //InputPaymentInfo info = Base1.ccPaymentInfo.Current;
        //    //info.PCTranNumber = "TRAN00000000023";
        //    //Base1.ccPaymentInfo.Cache.Insert(info);
        //    ////Base1.ccPaymentInfo.Cache.Current = info;
        //    //Base.Document.View.Answer = WebDialogResult.OK;
        //    //return baseDelegate?.Invoke(adapter);
        //   // try
        //   // {
        //   //     var ret = baseDelegate?.Invoke(adapter);
        //    //}
        //    //catch (PXDialogRequiredException ex)
        //    //{ throw ex; }
        //    //Base1.ccPaymentInfo.Cache.Current = info;
        //    //throw;
        //    //}
        //   // return adapter.Get();//baseDelegate?.Invoke(adapter);
        //}
    }

    //    public class KPPaymentTransactionInvExt : PXGraphExtension<ARPaymentEntryImportTransaction, ARPaymentEntryPaymentTransaction, ARPaymentEntry>
    //    {
    //        public static bool IsActive() => true;

    //        public delegate IEnumerable CaptureCCPaymentDelegate(PXAdapter adapter);
    //        [PXOverride]
    //        public virtual IEnumerable CaptureCCPayment(PXAdapter adapter, CaptureCCPaymentDelegate baseMethod)
    //        {

    //            var invokeBaseMethod = baseMethod(adapter);

    //            //Custom logic here

    //            return invokeBaseMethod;
    //        }

    //        public delegate IEnumerable SyncPaymentTransactionDelegate(PXAdapter adapter);

    //        [PXOverride]
    //        public virtual IEnumerable SyncPaymentTransaction(PXAdapter adapter, SyncPaymentTransactionDelegate baseDelegate)
    //        {
    //            var ret = baseDelegate?.Invoke(adapter);


    //            return ret;
    //        }
    //    }

}