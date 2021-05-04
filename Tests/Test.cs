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



namespace Tests
{
    public class Test : Check
    {
        //Not yet implemented or mandatory required for SalesDemo
        //protected readonly SetupAr SetupAr = new SetupAr();
        //private readonly SetupIn SetupIn = new SetupIn();
        //private readonly SetupSo SetupSo = new SetupSo();
        //private readonly OrderType OrderType = new OrderType();
        //private readonly FinancialPeriod FinancialPeriod = new FinancialPeriod();

        protected readonly PaymentMethod PaymentMethod = new PaymentMethod();
        protected readonly ProcessingCenter ProcessingCenter = new ProcessingCenter();
        protected readonly CustomerPaymentMethod CustomerPaymentMethod = new CustomerPaymentMethod();
        protected readonly HostedFormHandler AddPayment = new HostedFormHandler();
        protected readonly string CustomerID = "AACUSTOMER";
        protected readonly string ProcessingCenterID = "TESTPC";
        protected readonly string PaymentMethodID = "TESTPM";


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
                #region Step 1. Acumatica Configuration

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
                    //        ProcessingCenter.TestCredentials();
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

                    ////Delete?
                    //PaymentMethod.CashAccounts.Adjust();
                    //PaymentMethod.CashAccounts.Refresh();
                    PaymentMethod.Save();

                    //New row in Cash accounts
                    PaymentMethod.CashAccounts.New();
                    PaymentMethod.CashAccounts.Row.CashAccountID.Select("10600");
                    PaymentMethod.CashAccounts.Row.UseForAR.SetTrue();
                    PaymentMethod.CashAccounts.Row.ARIsDefault.SetTrue();
                    //PaymentMethod.CashAccounts.Row.ARIsDefaultForRefund.SetTrue();
                    //PaymentMethod.CashAccounts.Row.ARAutoNextNbr.SetFalse();
                    //PaymentMethod.CashAccounts.Row.ARLastRefNbr.Type("");

                    //Settings in AR
                    PaymentMethod.PaymentSettings.ARIsProcessingRequired.SetTrue();
                    PaymentMethod.PaymentSettings.IsAccountNumberRequired.SetTrue();
                    //PaymentMethod.PaymentSettings.ARVoidOnDepositAccount.SetFalse();
                    //PaymentMethod.PaymentSettings.ARHasBillingInfo.SetFalse();

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
            using (TestExecution.CreateTestCaseGroup("Credit card vaulting via Customer Payment Method Screen")){

                #region Step 1. Vaulting card via HostedForm on CPM Screen
                using (TestExecution.CreateTestStepGroup("Vaulting card"))
                { 
                    CustomerPaymentMethod.OpenScreen();
                    CustomerPaymentMethod.Insert();
                    CustomerPaymentMethod.RefreshScreen(); //AC-183004 remove after 2020r205 has been merged to 2020r1
                    CustomerPaymentMethod.Summary.BAccountID.Select(CustomerID);
                    CustomerPaymentMethod.Summary.PaymentMethodID.Select(PaymentMethodID);
                    //CustomerPaymentMethod.Summary.CCProcessingCenterID.Select("Test PC");
                    CustomerPaymentMethod.Summary.CashAccountID.Select("10600");
                    CustomerPaymentMethod.Details.CreateCCPaymentMethodHF();
                    AddPayment.Add();
                    CustomerPaymentMethod.Save();
                }
                #endregion
            }

            #endregion            
        }  
    }
}