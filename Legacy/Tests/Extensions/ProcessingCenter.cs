using Controls.Editors.DropDown;
using Controls.SmartPanel;
using Core.Config;
using GeneratedWrappers.Acumatica;
using System.Linq;

namespace Core
{
    public class ProcessingCenter : CA205000_CCProcessingCenterMaint
    {
        /// <summary>
        /// Default testing endpoint (Tokenized)
        /// </summary>
        public const string APITEST_URL = "https://apitest.authorize.net/soap/v1/Service.asmx";

        /// <summary>
        /// Default testing endpoint (basic)
        /// </summary>
        public const string APITEST_URL_BASIC = "https://test.authorize.net/gateway/transact.dll";

        /// <summary>
        /// PX.CCProcessing.V2.AuthnetProcessingPlugin - the newest plugin type
        /// </summary>
        public const string AuthnetProcessingPluginDisplayName = "Authorize.Net API plug-in";
        /// <summary>
        /// PX.CCProcessing.AuthorizeNetTokenizedProcessing - tokenizes plugin
        /// </summary>
        public const string AuthorizeNetTokenizedProcessingDisplayName = "Authorize.Net CIM plug-in";
        /// <summary>
        /// PX.CCProcessing.AuthorizeNetProcessing - oldest plugin type, does not require https from user to acumatica
        /// </summary>
        public const string AuthorizeNetProcessingDisplayName = "Authorize.Net AIM plug-in";

        public c_processingcenter_form Summary => base.ProcessingCenter_form;
        public c_currentprocessingcenter_tab ConnectionPreferences => base.CurrentProcessingCenter_tab;
        public c_details_grddetails Details => base.Details_grdDetails;
        public c_paymentmethods_grdpaymentmethods PaymentMethods => base.PaymentMethods_grdPaymentMethods;

        public ProcessingCenter()
        {
            ToolBar.TestCredentials.WaitAction = () =>
            {
                Wait.Wait.WaitForCondition(MessageBox.Buttons.Ok.IsVisible, Wait.Wait.LongTimeOut);
                MessageBox.Ok();
            };

            QuickImport.Key = new[] { "AUTHNETTOK" };

            QuickImport.DisabledLines = new[]
            {
                "CreditCardProcessingCenter->AllowDirectInput",
                "PlugInParameters->Value"
            };

            QuickImport.ExcludeFields = new[]
            {
                "CreditCardProcessingCenter->AllowDirectInput",
                "PlugInParameters->Description",
                "PlugInParameters->Value",
                "PaymentMethods->Default",
            };

            QuickImport.SkipContainers = new[]
            {
                "PaymentMethods"
            };

            ToolBar.UpdateExpirationDate.WaitAction = () => Wait.Wait.WaitForPageToLoad(true);

            PaymentMethods.ToolBar.Export.WaitAction = Wait.Wait.WaitForFileDownloadComplete;
            Details.ToolBar.Export.WaitAction = Wait.Wait.WaitForFileDownloadComplete;
        }

        /// <summary>
        /// Creates processing center configuration with V2 mode
        /// </summary>
        /// <param name="ID">ID of PC</param>
        /// <param name="name">Name (description) of PC</param>
        /// <param name="cashAccount">Cash Account for processing</param>
        /// <param name="CredsID">id of credentials to extract from the WELL KNOWN place</param>
        public void AddV2(string ID, string name, string cashAccount, string CredsID = "basic")
        {
            OpenScreen();
            Insert();
            Summary.ProcessingCenterID.Type(ID);
            Summary.Name.Type(name);
            Summary.CashAccountID.Select(cashAccount);
            Summary.IsActive.SetTrue();
            Summary.ProcessingTypeName.Select(AuthnetProcessingPluginDisplayName);
            if (MessageBox.Buttons.Ok.IsVisible()) MessageBox.Ok();
            Details.RowsCount().VerifyEquals(5);
            Details.SelectRow(Details.Columns.DetailID, "MERCNAME");
            Details.Row.Value.Type(SecurityConfig.GetAccount(CredsID).LoginID);
            Details.SelectRow(Details.Columns.DetailID, "TESTMODE");
            Details.Row.Value.Type(1);
            Details.SelectRow(Details.Columns.DetailID, "TRANKEY");
            Details.Row.Value.Type(SecurityConfig.GetAccount(CredsID).Trankey);
            Details.SelectRow(Details.Columns.DetailID, "VALIDATION");
            Details.Row.Value.DynamicControl<DropDown>().Select("Test Mode");
            Save();
            TestCredentials();
        }
        /// <summary>
        /// Creates processing center configuration with V2 mode
        /// </summary>
        /// <param name="ID">ID of PC</param>
        /// <param name="name">Name (description) of PC</param>
        /// <param name="cashAccount">Cash Account for processing</param>
        /// <param name="CredsID">id of credentials to extract from the WELL KNOWN place</param>
        public void ChangeV2(string ID, string name, string cashAccount, string CredsID = "basic")
        {
            OpenScreen();
            Insert();
            Summary.ProcessingCenterID.Type(ID);
            Summary.Name.Type(name);
            Summary.CashAccountID.Select(cashAccount);
            Summary.IsActive.SetTrue();
            Summary.ProcessingTypeName.Select(AuthnetProcessingPluginDisplayName);
            MessageBox.Ok();
            Details.RowsCount().VerifyEquals(5);
            Details.SelectRow(Details.Columns.DetailID, "MERCNAME");
            Details.Row.Value.Type(SecurityConfig.GetAccount(CredsID).LoginID);
            Details.SelectRow(Details.Columns.DetailID, "TESTMODE");
            Details.Row.Value.Type(1);
            Details.SelectRow(Details.Columns.DetailID, "TRANKEY");
            Details.Row.Value.Type(SecurityConfig.GetAccount(CredsID).Trankey);
            Details.SelectRow(Details.Columns.DetailID, "VALIDATION");
            Details.Row.Value.DynamicControl<DropDown>().Select("Test Mode");
            Save();
            TestCredentials();
        }

        /// <summary>
        /// Add trankey and merchname from security config
        /// </summary>
        /// <param name="accountID">account id for security config</param>
        public void AddAuthorizationSettings(string accountID)
        {
            string[] loginNames = new[] { "LOGINID", "MERCNAME" };
            string loginName = Details.Columns.DetailID.GetValues().Intersect(loginNames).Single();

            Details.SelectRow(Details.Columns.DetailID, "TRANKEY");
            Details.Row.Value.Type(SecurityConfig.GetAccount(accountID).Trankey);
            Details.SelectRow(Details.Columns.DetailID, loginName);
            Details.Row.Value.Type(SecurityConfig.GetAccount(accountID).LoginID);
            Save();
        }

        public void AddAuthorizationSettings()
        {
            string[] loginNames = new[] { "LOGINID", "MERCNAME" };
            string loginName = Details.Columns.DetailID.GetValues().Intersect(loginNames).Single();

            Details.SelectRow(Details.Columns.DetailID, "TRANKEY");
            Details.Row.Value.Type(SecurityConfig.GetAccount("tokenized").Trankey);
            Details.SelectRow(Details.Columns.DetailID, loginName);
            Details.Row.Value.Type(SecurityConfig.GetAccount("tokenized").Mercname);
            Save();
        }
    }
}
