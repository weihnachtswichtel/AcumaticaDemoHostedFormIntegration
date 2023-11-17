using Controls.Alert;
using Controls.Button;
using Controls.DefaultControl;
using Controls.Input;
using Controls.Select;
using Core.Config;
using Core.Core.Browser;
using Core.Log;
using Core.Wait;
using OpenQA.Selenium;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Tests.Extensions
{
    public class HostedFormHandler
    {
        public Input Token;
        public Button SendTokenBack;

        public HostedFormHandler()
        {

            Token = new Input("outRequest", null, null, null);
            SendTokenBack = new Button("sendBack", null, null); // { WaitAction = SaveWaitAction };
        }

         public void Add()
        {
            Token.Type("DummyGWresponse");
            SendTokenBack.Click();
        }

        protected void SaveWaitAction()
        {
            Wait.WaitForCondition(() =>
            {
                try
                {
                    Wait.WaitForPageToLoad();
                    return !SendTokenBack.IsVisible();
                }
                catch (UnhandledAlertException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Log.Information(e.ToString());
                    return false;
                }
                finally
                {
                    Alert.AlertToException();
                    Wait.WaitForPageToLoad();
                }
            }, Wait.LongTimeOut);
        }

        public void Maximize()
        {
            const string scriptMaximizePanel = "var elem = window.document.activePanel && window.document.activePanel.element.querySelector('.control-Maximize'); elem && elem.click();";
            Browser.JavaScriptExecutor.ExecuteScript(scriptMaximizePanel);
            Wait.WaitForCallbackToComplete();
            Log.Screenshot();
        }

        public void Close()
        {
            const string scriptMaximizePanel = "var elem = window.document.activePanel && window.document.activePanel.element.querySelector('.control-Close'); elem && elem.click();";
            Browser.JavaScriptExecutor.ExecuteScript(scriptMaximizePanel);
            Wait.WaitForCallbackToComplete();
            Log.Screenshot();
        }
      
    }
}
