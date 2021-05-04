using GeneratedWrappers.Acumatica;

namespace Tests.Extensions
{
    public partial class CustomerPaymentMethod : AR303010_CustomerPaymentMethodMaint
    {
        public c_customerpaymentmethod_form Summary => base.CustomerPaymentMethod_form;
        public c_details_grid Details => base.Details_grid;
        public c_billcontact_billcontact BillContact => base.BillContact_BillContact;
        public c_billaddress_billaddress BillAddress => base.BillAddress_BillAddress;
        public c_currentcpm_tab Billinfochecks => base.CurrentCPM_tab;

        public HostedFormHandler AddPayment { get; set; } = new HostedFormHandler();

        public CustomerPaymentMethod()
        {
            QuickImport.Skip = true;
        }
    }
}
