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

  }

}