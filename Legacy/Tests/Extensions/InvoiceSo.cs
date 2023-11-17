using GeneratedWrappers.Acumatica;

namespace Tests.Extensions
{
    public class InvoiceSo : SO303000_SOInvoiceEntry 
   {
        public c_adjustments_detgrid Applications => base.Adjustments_detgrid;
    }
}
