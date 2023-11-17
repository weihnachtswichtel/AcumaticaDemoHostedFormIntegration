using Controls.Alert;
using GeneratedWrappers.Acumatica;
using Core;
using Core.Wait;
using Core.Log;

namespace Tests.Extensions
{
    public class PaymentAr : AR302000_ARPaymentEntry
    {
        public const string Payment = "PMT";
        public const string CreditMemo = "CRM";
        public const string CustomerRefund = "REF";
        public const string VoidPayment = "RPM";
        public const string Prepayment = "PPM";

        public c_loadopts_loform LoadDocuments => base.Loadopts_loform;
        public c_ccproctran_grdccproctran CcTransactions => base.Ccproctran_grdccproctran;
        public c__arpayment_currencyinfo__rf CurrencyInformation => base._ARPayment_CurrencyInfo__rf;
        public c_adjustments_detgrid DocumentsToApply => base.Adjustments_detgrid;
        public c_adjustments_lv0 DocumentsToApplyForm => base.Adjustments_lv0;
        public c_adjustments_history_detgrid2 ApplicationHistory => base.Adjustments_History_detgrid2;
        public c_soadjustments_detgrid3 OrdersToApply => base.SOAdjustments_detgrid3;
        public c_document_form Summary => base.Document_form;
        public c_currentdocument_form2 FinancialDetails => base.CurrentDocument_form2;
        public c_ccpaymentinfo_frmccpaymentinfo RecordCCPaymentForm => base.Ccpaymentinfo_frmccpaymentinfo;
        public c_ccpaymentinfo_frmccpaymentinfo1 ExternAuthorizedCCPayment => base.Ccpaymentinfo_frmccpaymentinfo1;
        public c_paymentcharges_detgrid3 FinanceCharges => base.PaymentCharges_detgrid3;
        public c_approval_gridapproval ApprovalDetails => base.Approval_gridApproval;
        public c_compliancedocuments_grdcompliancedocuments Compliance => base.ComplianceDocuments_grdComplianceDocuments;

        public PaymentAr()
        {
            #region CCProcessing Wait Actions

            ToolBar.CaptureCCPayment.WaitAction = WaitForCCOperationToComplete;
            ToolBar.AuthorizeCCPayment.WaitAction = WaitForCCOperationToComplete;
            ToolBar.VoidCCPayment.WaitAction = WaitForCCOperationToComplete;
            ToolBar.CreditCCPayment.WaitAction = WaitForCCOperationToComplete;
            ToolBar.ValidateCCPayment.WaitAction = WaitForCCOperationToComplete;
            RecordCCPaymentForm.Buttons.Save.WaitAction = WaitForCCOperationToComplete;

            #endregion

            DocumentsToApply.ToolBar.Export.WaitAction = Wait.WaitForFileDownloadComplete;
            DocumentsToApply.ToolBar.LoadInvoices.WaitAction = Wait.WaitForCallbackToComplete;

            FinancialDetails.BatchNbr.WaitAction = () => Wait.WaitForPageToLoad(true);

            ToolBar.CustomerDocuments.WaitAction = () => Wait.WaitForPageToLoad();
            ToolBar.Save.WaitAction = Alert.AlertToException;
            ToolBar.VoidCheck.WaitAction = Wait.WaitForCallbackToComplete;
            ToolBar.SaveAndAdd.WaitAction = Wait.WaitForPageToLoad;

            ApplicationHistory.Row.AdjBatchNbr.WaitAction = () => Wait.WaitForPageToLoad(true);

            DocumentsToApply.ToolBar.Export.WaitAction = Wait.WaitForFileDownloadComplete;
            ApplicationHistory.ToolBar.Export.WaitAction = Wait.WaitForFileDownloadComplete;

            QuickImport.Skip = true;
            QuickImport.Key = new[] { "Payment", "000416" };
            FileName = "Payments And Applications";
        }

        private void WaitForCCOperationToComplete()
        {
            try
            {
                Wait.WaitForLongOperationToComplete();
            }
            catch
            {
                if (CcTransactions.RowsCount() != 0)
                {
                    CcTransactions.Columns.TranNbr.SortDescending();
                    CcTransactions.SelectRow(1);
                    Log.Information("Last transaction status: " + CcTransactions.Row.TranStatus.GetValue() + ". Processing center responce: " + CcTransactions.Row.PCResponseReasonText.GetValue());
                }
                throw;
            }
        }
    }
}
