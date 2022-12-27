using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace AcumaticaDummyProcessingCenter
{
  public class ADPCCustomerProfileEntry : PXGraph<ADPCCustomerProfileEntry, ADPCCustomerProfile>
  {

    public PXSave<ADPCCustomerProfile> Save;
    public PXCancel<ADPCCustomerProfile> Cancel;
    public PXSetup<ADPCSetup> Setup;
  
    public SelectFrom<ADPCCustomerProfile>.View CustomerProfile;
    public SelectFrom<ADPCPaymentProfile>.Where<ADPCPaymentProfile.customerProfileID.IsEqual<ADPCCustomerProfile.customerProfileID.FromCurrent>>.View PaymentProfiles;
  
    protected virtual void _(Events.RowInserting<ADPCPaymentProfile> e)
    {
        ADPCPaymentProfile row = e.Row as ADPCPaymentProfile;
        row.PaymentProfileID = Guid.NewGuid();
    }
  
  }
}