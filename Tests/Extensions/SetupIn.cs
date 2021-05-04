using Api;
using GeneratedWrappers.Acumatica;
using Tests.Extensions;

namespace Core
{
    public partial class SetupIn : IN101000_INSetupMaint
    {
        public c_setup_tab GeneralSettings => Setup_tab;
        public c_scansetup_formscansetup OperationSettings => ScanSetup_formScanSetup;

        public void Add()
        {
            using (TestExecution.TestExecution.CreateGroup(Log.Log.TESTCASE, "Configure IN setup"))
            {
                OpenScreen();
                Process();
                Save();
            }
        }

        private void Process()
        {
            using (TestExecution.TestExecution.CreateGroup(Log.Log.OPERATION, "General GeneralSettings"))
            {
                #region Numbering GeneralSettings

                using (TestExecution.TestExecution.CreateGroup(Log.Log.OPERATION, "Numbering GeneralSettings"))
                {
                    GeneralSettings.ReceiptNumberingID.Select();
                    GeneralSettings.IssueNumberingID.Select();
                    //GeneralSettings.TransferNumberingID.Select();
                    GeneralSettings.AdjustmentNumberingID.Select();
                    GeneralSettings.KitAssemblyNumberingID.Select();
                    GeneralSettings.PINumberingID.Select();
                    GeneralSettings.ReplenishmentNumberingID.Select();
                }

                #endregion

                #region Posting And Retention GeneralSettings

                using (TestExecution.TestExecution.CreateGroup(Log.Log.OPERATION, "Posting And Retention GeneralSettings"))
                {
                    GeneralSettings.UpdateGL.Set();
                    GeneralSettings.SummPost.Set();
                    GeneralSettings.AutoPost.Set();
                    GeneralSettings.HoldEntry.Set();
                    GeneralSettings.RequireControlTotal.Set();
                }

                #endregion

                #region Physical Inventory GeneralSettings

                using (TestExecution.TestExecution.CreateGroup(Log.Log.OPERATION, "Physical Inventory GeneralSettings"))
                {
                    GeneralSettings.PIUseTags.Set();
                    GeneralSettings.PILastTagNumber.Type();
                    GeneralSettings.TurnoverPeriodsPerYear.Type();
                }

                #endregion

                #region Account GeneralSettings

                //using (TestExecution.TestExecution.CreateGroup(Log.Log.OPERATION, "Account GeneralSettings"))
                //{
                //    Features features = ExtMethods.ReadOne(new ExtendedBiObject<Features>(ApiConnection.ApiConnection.Destination));

                //    GeneralSettings.ARClearingAcctID.Select();
                //    GeneralSettings.INProgressAcctID.Select();

                //    if (features.Summary.Warehouse.Value)
                //    {
                //        GeneralSettings.INTransitAcctID.Select();
                //    }
                //    if (features.Summary.SubAccount.Value)
                //    {
                //        GeneralSettings.ARClearingSubID.Type();
                //        GeneralSettings.INTransitSubID.Type();
                //        GeneralSettings.INProgressSubID.Type();
                //    }
                //}

                #endregion

                #region Default GeneralSettings

                using (TestExecution.TestExecution.CreateGroup(Log.Log.OPERATION, "Default GeneralSettings"))
                {
                    GeneralSettings.DfltNonStkItemClassID.Select();
                    GeneralSettings.DfltStkItemClassID.Select();
                    GeneralSettings.ReceiptReasonCode.Select();
                    GeneralSettings.IssuesReasonCode.Select();
                    GeneralSettings.AdjustmentReasonCode.Select();
                    GeneralSettings.PIReasonCode.Select();
                }

                #endregion
            }
        }

        //public static void GuiTest()
        //{
        //    Login.PxLogin.LoginToDestinationSite();

        //    SetupIn obj = ApiFactory.Get<SetupIn>(ApiConnection.ApiConnection.Source).ReadOne();

        //    obj.Add();

        //    obj.Add();
        //}

        //public static void ApiTest()
        //{
        //    ApiFactory.Get<SetupIn>(ApiConnection.ApiConnection.Source).ReadAll(ApiConnection.ApiConnection.Destination, true);
        //}
    }
}
