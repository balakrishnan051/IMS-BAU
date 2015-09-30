using System;
using System.Windows.Automation;
using System.Threading;

namespace Framework.Common
{
    public class WindowPopupHandler
    {
        public bool Authenticate(string windowTitle, string userName, string password)
        {
            string checkProperty = "AutomationId";
            string userNameId = "1003";
            string passwordId = "1005";
            string okButoonId = "1";
            try
            {
                AutomationElement desktopRoot = AutomationElement.RootElement;
                AutomationElement app = desktopRoot.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, windowTitle));
                if (app == null)
                    throw new AutomationException("Window '" + windowTitle + "' is not found.");

                this.WaitForCondition(app, checkProperty, userNameId, 5000);
                AutomationElement userNameTextBoxElement = GetRequiredElement(app, checkProperty, userNameId);
                var userNameTextBox = (ValuePattern)userNameTextBoxElement.GetCurrentPattern(ValuePattern.Pattern);
                    userNameTextBox.SetValue(userName);
               
                AutomationElement passwordTextBoxElement = GetRequiredElement(app, checkProperty, passwordId);
                var passwordTextBox = (ValuePattern)passwordTextBoxElement.GetCurrentPattern(ValuePattern.Pattern);

                AutomationElement okButton = GetRequiredElement(app, checkProperty, okButoonId);
                var okButtonelement = (InvokePattern)okButton.GetCurrentPattern(InvokePattern.Pattern);
                okButtonelement.Invoke();

                return true;
            }
            catch (Exception e)
            {
                throw new AutomationException("Authenticate Method Failed due to exception '" + e.Message);
            }
        }

        public string GetWindowsAlert(string windowTitle, string dialogName)
        {
            string checkProperty = "AutomationId";
            string textId = "65535";
            string okButoonId = "2";
            try
            {
                AutomationElement desktopRoot = AutomationElement.RootElement;
                AutomationElement app = desktopRoot.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, windowTitle));
                if (app == null)
                    throw new AutomationException("Window '" + windowTitle + "' is not found.");

                //getting the dialog
                app = GetRequiredElement(app, "Name", dialogName);

                this.WaitForCondition(app, checkProperty, textId, 5000);
                AutomationElement alertElement = GetRequiredElement(app, checkProperty, textId);
                string alert = alertElement.Current.Name;

                AutomationElement okButton = GetRequiredElement(app, checkProperty, okButoonId);
                InvokePattern okButtonelement = (InvokePattern)okButton.GetCurrentPattern(InvokePattern.Pattern);
                okButtonelement.Invoke();
                return alert;
            }
            catch (Exception e)
            {
                throw new AutomationException("Authenticate Method Failed due to exception '" + e.Message);
            }
        }

        public Boolean IsAlertDialogPresent(string windowTitle, string dialogName)
        {
            AutomationElement _desktopRoot = AutomationElement.RootElement;
            AutomationElement app = _desktopRoot.FindFirst(TreeScope.Children,
                new PropertyCondition(AutomationElement.NameProperty, windowTitle));
            if (app == null)
                return false;

            //getting the dialog
            app = GetRequiredElement(app, "Name", dialogName);

            if (app == null)
                return false;
            else
                return true;
        }

        private AutomationElement GetRequiredElement(AutomationElement parentElement, string criteria, string text)
        {
            foreach (AutomationElement element in parentElement.FindAll(TreeScope.Descendants, Condition.TrueCondition))
            {
                if (IsRequiredElement(element, criteria, text))
                    return element;
            }
            return null;

        }

        private Boolean IsRequiredElement(AutomationElement element, string criteria, string text)
        {
            //Check property exists and has value.
            if (!(element.Current.GetType().GetProperty(criteria) == null) && !(element.Current.GetType().GetProperty(criteria).GetValue(element.Current, null) == null))
            {
                //Check property is 'System.String' as it has a different logic to extract the value.
                if (element.Current.GetType().GetProperty(criteria).GetValue(element.Current, null).GetType().ToString().Equals("System.String")
                    || element.Current.GetType().GetProperty(criteria).GetValue(element.Current, null).GetType().ToString().Equals("System.Int32"))
                {
                    if (element.Current.GetType().GetProperty(criteria).GetValue(element.Current, null).ToString() == text)
                        return true;
                }
                else
                {
                    if ((((AutomationIdentifier)(element.Current.GetType().GetProperty(criteria).GetValue(element.Current, null))).ProgrammaticName) == text)
                        return true;
                }
            }
            return false;
        }

        private Boolean WaitForCondition(AutomationElement parentLocator, string criteria, string text, int timeout)
        {
            DateTime actualtimeouttime = DateTime.Now.AddMilliseconds(timeout);
            DateTime currTime = DateTime.Now;
            while (currTime < actualtimeouttime)
            {
                AutomationElement element = GetRequiredElement(parentLocator, criteria, text);
                if (element != null)
                {
                    return true;
                }
                Thread.Sleep(timeout/100);
                currTime = DateTime.Now;
            }
            throw new AutomationException("Element with search criteria '" + criteria + "=" + text + "' was not found even after '" + timeout + "'");
        }

    }
}
