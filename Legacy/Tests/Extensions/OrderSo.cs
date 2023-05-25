using Core.Wait;
using GeneratedWrappers.Acumatica;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Extensions
{
    public class OrderSo : SO301000_SOOrderEntry
    {
        public OrderSo(){
            ToolBar.PrepareInvoice.WaitAction = delegate
            {
                Wait.WaitForCallbackToComplete();
                Wait.WaitForPageToLoad();
            };

        }

        public c_document_form Summary
        {
            get { return base.Document_form; }
        }

        public c_transactions_grid DocumentDetails
        {
            get { return base.Transactions_grid; }
        }

        public c_adjustments_detgrid Payments
        {
            get { return base.Adjustments_detgrid; }
        }

        public c_currentdocument_formpaymentinformation PaymentSettings
        {
            get { return base.CurrentDocument_formPaymentInformation; }
        }
        public c_quickpayment_createpaymentformview CreatePaymentForm => base.QuickPayment_CreatePaymentFormView;
    }
}
