using Controls.Dashboard;
using Core;
using Core.Config;
using Core.Log;
using Core.Login;
using Core.TestExecution;
using Tests.Extensions;
using System;
using Controls.Editors.DropDown;
using GeneratedWrappers.Acumatica;
using Core.Wait;

namespace Tests
{
    public class Test : Check
    {
        //Not yet implemented or mandatory required for SalesDemo
        //protected readonly SetupAr SetupAr = new SetupAr();
        //private readonly OrderType OrderType = new OrderType();
        //private readonly FinancialPeriod FinancialPeriod = new FinancialPeriod();

        private readonly SetupIn SetupIn = new SetupIn();
        private readonly SetupSo SetupSo = new SetupSo();
        protected readonly InvoiceSo InvoiceSo = new InvoiceSo();
        protected readonly OrderSo OrderSo = new OrderSo();
        protected readonly PaymentAr PaymentAr = new PaymentAr();
        protected readonly PaymentMethod PaymentMethod = new PaymentMethod();
        protected readonly ProcessingCenter ProcessingCenter = new ProcessingCenter();
        protected readonly CustomerPaymentMethod CustomerPaymentMethod = new CustomerPaymentMethod();
        protected readonly HostedFormHandler AddPayment = new HostedFormHandler();
        protected readonly string CustomerID = "AACUSTOMER";
        protected readonly string ProcessingCenterID = "TESTPC";
        protected readonly string PaymentMethodID = "TESTPM";
        protected string PCTranNumber;
        protected string AuthNumber;
        protected string RefNumber;


        public override void BeforeExecute()
        {
            PxLogin.LoginToDestinationSite();

            #region clean up

            //TO DO. Not yet very clear how to remove all the CMP created during the previous run.
            //using (TestExecution.CreateTestCaseGroup("Clear up ap the site"))
            //    try
            //    {
            //        ProcessingCenter.OpenScreen();
            //        ProcessingCenter.Summary.ProcessingCenterID.Select(ProcessingCenterID);
            //        ProcessingCenter.Delete();
            //        //CustomerPaymentMethod.OpenScreen();
            //        //CustomerPaymentMethod.Summary.BAccountID.Select("AACUSTOMER");
            //        //CustomerPaymentMethod.Summary.PaymentMethodID.Select("TestPM");
            //        //CustomerPaymentMethod.Delete();
            //        PaymentMethod.OpenScreen();
            //        PaymentMethod.Summary.PaymentMethodID.Select(PaymentMethodID);
            //        PaymentMethod.Delete();
            //    }
            //    catch { }

            #endregion
        }


        public override void Execute()
        {

            #region Test Case 1. Processing Center configuration
            using (TestExecution.CreateTestCaseGroup("Processing Center configuration"))
            {
                // #region Step 1. Acumatica Configuration

                //SetupIn.OpenScreen();
                //SetupIn.GeneralSettings.HoldEntry.SetFalse();
                //SetupIn.Save();


                //SetupSo.OpenScreen();
                //SetupSo.GeneralSettings.HoldShipments.SetFalse();
                //SetupSo.GeneralSettings.RequireShipmentTotal.SetFalse();
                //SetupSo.Save();
                /*
                    using (TestExecution.CreateTestStepGroup("Acumatica Configuration"))
                    {
                        //SetupAr.OpenScreen();
                        //SetupAr.SelectBranch("Products Wholesale");
                        //SetupAr.GeneralSettings.HoldEntry.SetFalse();
                        //SetupAr.Save();

                        //SetupIn.OpenScreen();
                        //SetupIn.GeneralSettings.HoldEntry.SetFalse();
                        //SetupIn.Save();

                        //OrderType.OpenScreen();
                        //OrderType.Summary.OrderType.Select("SO");
                        //OrderType.GeneralSettings.HoldEntry.SetFalse();
                        //OrderType.GeneralSettings.RequireControlTotal.SetFalse();
                        //OrderType.Save();
                        //OrderType.Summary.OrderType.Select("SA");
                        //OrderType.GeneralSettings.HoldEntry.SetFalse();
                        //OrderType.GeneralSettings.RequireControlTotal.SetFalse();
                        //OrderType.Save();
                        //OrderType.Summary.OrderType.Select("IN", "Order Type");
                        //OrderType.Summary.Active.SetTrue();
                        //OrderType.GeneralSettings.FreightAcctID.Select("");
                        //OrderType.GeneralSettings.FreightSubID.Type("");
                        //OrderType.GeneralSettings.DiscountAcctID.Select("");
                        //OrderType.GeneralSettings.DiscountSubID.Type("");
                        //OrderType.Save();

                        //SetupSo.OpenScreen();
                        //SetupSo.GeneralSettings.HoldShipments.SetFalse();
                        //SetupSo.GeneralSettings.RequireShipmentTotal.SetFalse();
                        //SetupSo.Save();


                        //FinancialPeriod.ActivatePeriodsTillNow();

                    }

                    #endregion
                */
                    #region Step 2. Processing Center Configuration
                    using (TestExecution.CreateTestStepGroup("Configure Processing Center"))
                    {
                        ProcessingCenter.OpenScreen();
                        ProcessingCenter.Insert();
                        ProcessingCenter.Summary.ProcessingCenterID.Type(ProcessingCenterID);
                        ProcessingCenter.Summary.Name.Type("Test Processing Center");
                        ProcessingCenter.Summary.CashAccountID.Type("10600");
                        ProcessingCenter.Summary.IsActive.SetTrue();
                        ProcessingCenter.Summary.ProcessingTypeName.Select("Cookieless Processing Plug-in");
                        ProcessingCenter.ConnectionPreferences.OpenTranTimeout.Type(300);
                        ProcessingCenter.ConnectionPreferences.OpenTranTimeout.GetValue().VerifyEquals(60);
                        ProcessingCenter.Details.SelectRow(ProcessingCenter.Details.Columns.DetailID, "CLID");
                        ProcessingCenter.Details.Row.Value.Type("Test Detail");
                        // ProcessingCenter.TestCredentials();   ToDo
                        ProcessingCenter.Save();
                    }

                    #endregion

                    #region Step 3. Payment Method Configuration
                    using (TestExecution.CreateTestStepGroup("Configure payment method " + PaymentMethodID))
                    {
                        //New summary
                        PaymentMethod.OpenScreen();
                        PaymentMethod.Insert();
                        PaymentMethod.Summary.PaymentMethodID.Type(PaymentMethodID);
                        PaymentMethod.Summary.IsActive.SetTrue();
                        PaymentMethod.Summary.PaymentType.Select("CCD");
                        PaymentMethod.Summary.Descr.Type("Test Payment Method");
                        PaymentMethod.Summary.UseForAP.SetFalse();
                        PaymentMethod.Summary.UseForAR.SetTrue();
                        PaymentMethod.Summary.UseForCA.SetTrue();

                        PaymentMethod.Save();

                        //New row in Cash accounts
                        PaymentMethod.CashAccounts.New();
                        PaymentMethod.CashAccounts.Row.CashAccountID.Select("10600");
                        PaymentMethod.CashAccounts.Row.UseForAR.SetTrue();
                        PaymentMethod.CashAccounts.Row.ARIsDefault.SetTrue();
                

                        //Settings in AR
                        PaymentMethod.PaymentSettings.ARIsProcessingRequired.SetTrue();
                        PaymentMethod.PaymentSettings.IsAccountNumberRequired.SetTrue();


                        PaymentMethod.ProcessingCenters.New();
                        PaymentMethod.ProcessingCenters.Row.ProcessingCenterID.Select(ProcessingCenterID);
                        PaymentMethod.ProcessingCenters.Row.IsActive.SetTrue();
                        PaymentMethod.ProcessingCenters.Row.IsDefault.SetTrue();

                        PaymentMethod.Save();
                    }
                    #endregion
            }
            #endregion

            #region Test Case 2. Vaulting Card
            using (TestExecution.CreateTestCaseGroup("Credit card vaulting via Customer Payment Method Screen"))
            {

                #region Step 1. Vaulting card via HostedForm on CPM Screen
                using (TestExecution.CreateTestStepGroup("Vaulting card"))
                {
                    CustomerPaymentMethod.OpenScreen();
                    CustomerPaymentMethod.Insert();
                    CustomerPaymentMethod.RefreshScreen(); 
                    CustomerPaymentMethod.Summary.BAccountID.Select(CustomerID);
                    CustomerPaymentMethod.Summary.PaymentMethodID.Select(PaymentMethodID);
                    CustomerPaymentMethod.Summary.CashAccountID.Select("10600");
                    CustomerPaymentMethod.Details.CreateCCPaymentMethodHF();
                    AddPayment.Add();
                    CustomerPaymentMethod.Save();
                }
                #endregion
            }

            #endregion            

            #region Test Case 3.  Payment Authorize from new card, Void
            using (TestExecution.CreateTestCaseGroup("Payment Authorize from new card, Void"))
            {
                #region Step 1. Authorize from new Card
                using (TestExecution.CreateTestStepGroup("Create CC Payment and Authorize"))
                {
                    PaymentAr.OpenScreen();
                    PaymentAr.Insert();
                    PaymentAr.Summary.CustomerID.Select(CustomerID);
                    PaymentAr.Summary.PaymentMethodID.Select(PaymentMethodID);
                    PaymentAr.Summary.NewCard.SetTrue();
                    PaymentAr.Summary.CuryOrigDocAmt.Type(100m);
                    PaymentAr.Save();
                    PaymentAr.AuthorizeCCPayment();
                    AddPayment.Add();
                    PaymentAr.Summary.Status.GetValue().VerifyEquals("Pending Processing");
                    PaymentAr.Summary.CCPaymentStateDescr.GetValue().VerifyEquals("Pre-Authorized");
                }
                #endregion

                #region Step 2. Verify External transaction

                using (TestExecution.CreateTestStepGroup("Verify External transaction"))
                {
                    PaymentAr.CcTransactions.RowsCount().VerifyEquals(1);
                    PaymentAr.CcTransactions.SelectRow(1);
                    PaymentAr.CcTransactions.Row.TranType.GetValue().VerifyEquals("Authorize Only");
                    PaymentAr.CcTransactions.Row.TranStatus.GetValue().VerifyEquals("Approved");
                    PaymentAr.CcTransactions.Row.ProcStatus.GetValue().VerifyEquals("Completed");
                    PaymentAr.CcTransactions.Row.Amount.GetValue().VerifyEquals("100.0000");

                    PCTranNumber = PaymentAr.CcTransactions.Row.PCTranNumber.GetValue();
                    AuthNumber = PaymentAr.CcTransactions.Row.AuthNumber.GetValue();
                    RefNumber = PaymentAr.Summary.RefNbr.GetValue();

                    PaymentAr.Summary.ExtRefNbr.GetValue().VerifyEquals(PCTranNumber);
                }
                #endregion

                #region Step 3. Void

                using (TestExecution.CreateTestStepGroup("Void"))
                {
                    PaymentAr.OpenScreen();
                    PaymentAr.Summary.RefNbr.Select(RefNumber);
                    PaymentAr.VoidCCPayment();
                }
                #endregion

                #region Step 4. Verify External transaction after void

                using (TestExecution.CreateTestStepGroup("Verify External transaction"))
                {
                    PaymentAr.Summary.Status.GetValue().VerifyEquals("Pending Processing");
                    PaymentAr.CcTransactions.RowsCount().VerifyEquals(2);
                    PaymentAr.CcTransactions.SelectRow(1);
                    PaymentAr.CcTransactions.Row.TranType.GetValue().VerifyEquals("Void");
                    PaymentAr.CcTransactions.Row.TranStatus.GetValue().VerifyEquals("Approved");
                    PaymentAr.CcTransactions.Row.ProcStatus.GetValue().VerifyEquals("Completed");
                    PaymentAr.CcTransactions.Row.Amount.GetValue().VerifyEquals("100.0000");
                    AuthNumber = PaymentAr.CcTransactions.Row.AuthNumber.GetValue();
                    RefNumber = PaymentAr.Summary.RefNbr.GetValue();
                    PaymentAr.CcTransactions.SelectRow(2);
                    PaymentAr.CcTransactions.Row.Amount.GetValue().VerifyEquals(100m);
                    PaymentAr.Summary.CuryOrigDocAmt.Type(100m);
                    PaymentAr.Save();
                    PaymentAr.Summary.ExtRefNbr.GetValue().VerifyEquals(PCTranNumber);
                }
                #endregion


                #region Step 5. Authorize CC Payment

                using (TestExecution.CreateTestStepGroup("Authorize CC Payment"))
                {
                    PaymentAr.OpenScreen();
                    PaymentAr.Summary.RefNbr.Select(RefNumber);
                    PaymentAr.AuthorizeCCPayment();
                }

                #endregion

                #region Step 6. Verify External transaction

                using (TestExecution.CreateTestStepGroup("Verify External transaction"))
                {
                    PaymentAr.Summary.Status.GetValue().VerifyEquals("Pending Processing");
                    PaymentAr.Summary.CCPaymentStateDescr.GetValue().VerifyEquals("Pre-Authorized");
                    PaymentAr.CcTransactions.RowsCount().VerifyEquals(3);
                    PaymentAr.CcTransactions.SelectRow(1);
                    PaymentAr.CcTransactions.Row.TranType.GetValue().VerifyEquals("Authorize Only");
                    PaymentAr.CcTransactions.Row.TranStatus.GetValue().VerifyEquals("Approved");
                    PaymentAr.CcTransactions.Row.ProcStatus.GetValue().VerifyEquals("Completed");
                    PaymentAr.CcTransactions.Row.Amount.GetValue().VerifyEquals("100.0000");

                    PCTranNumber = PaymentAr.CcTransactions.Row.PCTranNumber.GetValue();
                    AuthNumber = PaymentAr.CcTransactions.Row.AuthNumber.GetValue();
                    RefNumber = PaymentAr.Summary.RefNbr.GetValue();

                    PaymentAr.Summary.ExtRefNbr.GetValue().VerifyEquals(PCTranNumber);
                }

                #endregion

                #region Step 7. Capture CC Payment

                using (TestExecution.CreateTestStepGroup("Capture CC Payment"))
                {
                    PaymentAr.OpenScreen();
                    PaymentAr.Summary.RefNbr.Select(RefNumber);
                    PaymentAr.CaptureCCPayment();
                }

                #endregion

                #region Step 8. Verify External transaction

                using (TestExecution.CreateTestStepGroup("Verify External transaction"))
                {
                    PaymentAr.Summary.Status.GetValue().VerifyEquals("Open");
                    Wait.WaitForCondition(() =>
                    {
                        PaymentAr.RefreshScreen();
                        return PaymentAr.Summary.CCPaymentStateDescr.GetValue() == "Captured";
                    }, Wait.LongTimeOut);
                    PaymentAr.Summary.CCPaymentStateDescr.GetValue().VerifyEquals("Captured");
                    PaymentAr.CcTransactions.RowsCount().VerifyEquals(4);
                    PaymentAr.CcTransactions.SelectRow(1);
                    PaymentAr.CcTransactions.Row.TranType.GetValue().VerifyEquals("Capture Authorized");
                    PaymentAr.CcTransactions.Row.TranStatus.GetValue().VerifyEquals("Approved");
                    PaymentAr.CcTransactions.Row.ProcStatus.GetValue().VerifyEquals("Completed");
                    PaymentAr.CcTransactions.Row.Amount.GetValue().VerifyEquals("100.0000");
                    PCTranNumber = PaymentAr.CcTransactions.Row.PCTranNumber.GetValue();
                    AuthNumber = PaymentAr.CcTransactions.Row.AuthNumber.GetValue();
                    RefNumber = PaymentAr.Summary.RefNbr.GetValue();

                    PaymentAr.Summary.ExtRefNbr.GetValue().VerifyEquals(PCTranNumber);
                }
                #endregion
            }
            #endregion


            #region Test case 4. Processing from vaulted cards

            using (TestExecution.CreateTestCaseGroup
                ("Processing using previosulsy Vaulted Card"))
            {
                //#region Step 1. Accept payment from new card set false

                //using (TestExecution.CreateTestStepGroup("Accept payment from new card set false"))
                //{
                //    ProcessingCenter.OpenScreen();
                //    ProcessingCenter.Summary.ProcessingCenterID.Select(PaymentMethodID);
                //    ProcessingCenter.Summary.UseAcceptPaymentForm.SetTrue();
                //    ProcessingCenter.Save();
                //}

                //#endregion

                #region Step 2. Create sales order

                using (TestExecution.CreateTestStepGroup("Create sales order"))
                {
                    OrderSo.OpenScreen();
                    OrderSo.Insert();
                    OrderSo.Summary.CustomerID.Select(CustomerID);
                    OrderSo.DocumentDetails.New();
                    OrderSo.DocumentDetails.Row.InventoryID.Select("ACCOMODATE");  //Should be non stock
                    OrderSo.DocumentDetails.Row.OrderQty.Type(1);
                    OrderSo.DocumentDetails.Row.CuryUnitPrice.Type(100);
                    OrderSo.PaymentSettings.PaymentMethodID.Select(PaymentMethodID);
                    OrderSo.Save();

                    OrderSo.Payments.CreateDocumentPayment();
                    OrderSo.CreatePaymentForm.CreatePaymentAuthorizeButton();
                    Wait.WaitForLongOperationToComplete();
                    OrderSo.Payments.VoidDocumentPayment();
                    Wait.WaitForLongOperationToComplete();
                    OrderSo.Payments.CreateDocumentPayment();
                    OrderSo.CreatePaymentForm.CreatePaymentAuthorizeButton();
                    Wait.WaitForLongOperationToComplete();
                    OrderSo.PrepareInvoice();
                    InvoiceSo.Applications.RowsCount().VerifyEquals(1);
                    InvoiceSo.Applications.CaptureDocumentPayment();
                    Wait.WaitForLongOperationToComplete();
                    InvoiceSo.Release();
                    Wait.WaitForLongOperationToComplete();
                    InvoiceSo.Applications.SelectRow(1);
                    InvoiceSo.Applications.Row.CuryAdjdAmt.GetValue().VerifyEquals(100);
                    InvoiceSo.Applications.Row.AdjgRefNbr.ClickLink();
                    PaymentAr.Summary.Status.GetValue().VerifyEquals("Closed");
                    PaymentAr.Summary.CCPaymentStateDescr.GetValue().VerifyEquals("Captured");
                    PaymentAr.CcTransactions.RowsCount().VerifyEquals(2);
                    PaymentAr.CloseWindow();
                }
                #endregion
            }
            #endregion
        }
    }
}