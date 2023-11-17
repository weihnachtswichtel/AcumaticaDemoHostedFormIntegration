using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;

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