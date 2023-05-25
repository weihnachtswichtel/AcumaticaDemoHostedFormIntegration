using System;
using PX.Data;
using PX.Data.BQL.Fluent;


namespace AcumaticaDummyProcessingCenter
{
  public class ADPCSetupMaint : PXGraph<ADPCSetupMaint>
  {

    public PXSave<ADPCSetup> Save;
    public PXCancel<ADPCSetup> Cancel;

    public SelectFrom<ADPCSetup>.View Setup;
  
  }
}