//using PX.Data;
//using PX.Objects.AR;
//using PX.Objects.AR.GraphExtensions;
//using PX.Objects.CR.Extensions;
//using PX.Objects.Extensions.PaymentTransaction;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AcumaticaDummyProcessingCenter
//{
//    public class CCOverride : PXGraphExtension<ARPaymentEntryPaymentTransaction, ARPaymentEntry>
//    {
//        //public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.customCCIntegration>(); //for 2023R1 and further
//        public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.integratedCardProcessing>(); //for 2022R2 and prior

//        public delegate IEnumerable CaptureCCPaymentDelegate(PXAdapter adapter);

//        [PXOverride]
//        public virtual IEnumerable CaptureCCPayment(PXAdapter adapter, CaptureCCPaymentDelegate baseDelegate) {
//            var ret = baseDelegate?.Invoke(adapter);
//            Base.Document.Ask("Capture", MessageButtons.OK);
//            return ret;
//        }
//    }

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

//}