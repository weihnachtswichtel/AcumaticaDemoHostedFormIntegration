using Api;
using Controls.Editors.Selector;
using Core.ApiConnection;
using Core.Verifications;
using Core.WebService;
using GeneratedWrappers.Acumatica;
using System;
using System.Linq;

namespace Core
{
    public partial class FinancialPeriod : GL201000_MasterFinPeriodMaint
    {
        public c_fiscalyear_form Summary => base.FiscalYear_form;
        public c_periods_grid Details => base.Periods_grid;
        public c_generateparams_genparams FinPeriodGenerateParameters => base.GenerateParams_GenParams;
        public c_savedialog_saveprm UpdateFinancialYear => base.SaveDialog_SavePrm;

        public FinancialPeriod()
        {
            ToolBar.Open.WaitAction = Wait.Wait.WaitForPageToLoad;
            ToolBar.Close.WaitAction = Wait.Wait.WaitForPageToLoad;
            ToolBar.Lock.WaitAction = Wait.Wait.WaitForPageToLoad;
            ToolBar.Deactivate.WaitAction = Wait.Wait.WaitForPageToLoad;
            ToolBar.Reopen.WaitAction = Wait.Wait.WaitForPageToLoad;
            ToolBar.Unlock.WaitAction = Wait.Wait.WaitForPageToLoad;

            ToolBar.GenerateYears.WaitAction = null;
            FinPeriodGenerateParameters.Buttons.Ok.WaitAction = Wait.Wait.WaitForLongOperationToComplete;

            Details.ToolBar.Export.WaitAction = Wait.Wait.WaitForFileDownloadComplete;
        }

        public void DeleteAllYears()
        {
            using (TestExecution.TestExecution.CreateGroup(Log.Log.TESTCASE, "Delete all financial years"))
            {
                foreach (string year in GetYearsAvailableInSelector())
                {
                    Summary.Year.Select(year);
                    base.Delete();
                    RefreshScreen();
                }

                RefreshScreen();
                GetYearsAvailableInSelector().VerifyCount(0);
            }
        }

        public EnumerableStringVerifiableValue GetYearsAvailableInSelector()
        {
            Summary.Year.Open();
            return Summary.Year.Grid.Columns.DynamicControl<SelectorColumnFilter>("Financial Year").GetValues();
        }

        //public void ActivatePeriodsTillNow()
        //{
        //    ActivatePeriodsTill(DateTime.Today.Year);
        //}

        //public void ActivatePeriodsTill(int Year)
        //{
        //    GeneratePeriodsTill(Year);

        //    using (TestExecution.TestExecution.CreateTestStepGroup("Activate Periods Till " + Year))
        //    {
        //        Last();
        //        if (Details.Columns.Status.GetValues().Any(s => s.StartsWith("I")))
        //            new ManageFinancialPeriodGl().ActivatePeriods(Year);
        //        else Log.Log.Information("No Inactive periods was found, periods activation skipped");
        //    }
        //}

        public void GeneratePeriodsTill(int Year)
        {
            using (TestExecution.TestExecution.CreateTestStepGroup("Generate Periods Till " + Year))
            {
                OpenScreen();
                base.GenerateYears();
                FinPeriodGenerateParameters.ToYear.Type(Year);
                FinPeriodGenerateParameters.Ok();
            }
        }

        //public static void ApiTest()
        //{
        //    var source = ApiFactory.Get<FinancialPeriod>(ApiConnection.ApiConnection.Source);

        //    using (TestExecution.TestExecution.CreateGroup(Log.Log.OPERATION, "Financial Period"))
        //    {
        //        ApiConnection.ApiConnection.Destination.SetBusinessDate(new DateTime(2006, 2, 1));

        //        Content content = source.ApiHelper.GetUntypedContent(source.Source);

        //        var autoFill = content.Actions.First(x => x.Name.ToLower().Contains("autofill"));

        //        for (int i = 0; i < 10; i++)
        //        {
        //            source.ApiHelper.Submit(ApiConnection.ApiConnection.Destination, new[] { content.Insert() });
        //            source.ApiHelper.Submit(ApiConnection.ApiConnection.Destination, new Command[] { autoFill });
        //            source.ApiHelper.Submit(ApiConnection.ApiConnection.Destination, new[] { content.Save() });
        //        }

        //        string[][] exportResults = source.ApiHelper.Export(source.Source);
        //        ImportResult[] importResults = source.ApiHelper.Import(ApiConnection.ApiConnection.Destination, exportResults);
        //    }
        //}
    }
}
