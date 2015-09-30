using System;
using Selenium;
using System.Threading;
using System.Globalization;
using OpenQA.Selenium;
using System.Net;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Framework.Common
{
    public class Common
    {
        public ISelenium GetDriverByTestCaseName(Dictionary<string, ISelenium> driverList, string testcaseName)
        {
            #region tobeDeleted
            //ISelenium localInstanceOfDriver = null;

            //foreach (KeyValuePair<string, ISelenium> pair in driverList)
            //{
            //    if (pair.Key == testcaseName)
            //    {
            //        localInstanceOfDriver = pair.Value;
            //        break;
            //    }

            //}

            //return localInstanceOfDriver;
            #endregion

            if (driverList.ContainsKey(testcaseName))
                return driverList[testcaseName];
            else
            {
                throw new Exception(testcaseName + " not found in the list");
                return null;
            }

        }

        #region AN_Methods
        public bool WaitUntilElementPresent(ISelenium myBrowser, string locator, string timeout)
        {
            DateTime endTime = DateTime.Now.AddMilliseconds(Convert.ToDouble(timeout,CultureInfo.CurrentCulture));
            while (endTime > DateTime.Now)
            {
                if (myBrowser.IsElementPresent(locator))
                    return true;
                else
                    Thread.Sleep(Convert.ToInt32(Convert.ToDouble(timeout, CultureInfo.InvariantCulture) / 10.0, CultureInfo.InvariantCulture));
            }
            throw new AutomationException("Element with search critera '" + locator + "' was not found.");
        }

        public bool WaitUntilElementEditable(ISelenium myBrowser, string locator, string timeout)
        {
            DateTime endTime = DateTime.Now.AddMilliseconds(Convert.ToDouble(timeout,CultureInfo.CurrentCulture));
            while (endTime > DateTime.Now)
            {
                if (myBrowser.IsEditable(locator))
                    return true;
                else
                    Thread.Sleep(Convert.ToInt32(Convert.ToDouble(timeout, CultureInfo.CurrentCulture) / 10.0,CultureInfo.CurrentCulture));
            }
            throw new AutomationException("Element with search critera '" + locator + "' is not editable.");
        }

        public bool WaitUntilElementPresentAndReturnTrue(ISelenium myBrowser, string locator, string timeout)
        {
            DateTime endTime = DateTime.Now.AddMilliseconds(Convert.ToDouble(timeout, CultureInfo.CurrentCulture));
            while (endTime > DateTime.Now)
            {
                if (myBrowser.IsElementPresent(locator))
                    return true;
                else
                    Thread.Sleep(Convert.ToInt32(Convert.ToDouble(timeout, CultureInfo.InvariantCulture)/10.0,
                                                 CultureInfo.InvariantCulture));
            }
             return false;
        }

        public bool WaitUntilElementOrWindowsAlertPresent(ISelenium myBrowser, string locator, string windowTitle, string dialogName, string timeout)
        {
            var windowPopupHandler = new Framework.Common.WindowPopupHandler();
            DateTime endTime = DateTime.Now.AddMilliseconds(Convert.ToDouble(timeout,CultureInfo.CurrentCulture));
            while (endTime > DateTime.Now)
            {
                if (myBrowser.IsElementPresent(locator))
                    return true;
                else if (windowPopupHandler.IsAlertDialogPresent(windowTitle, dialogName))
                    return false;
                else
                    Thread.Sleep(Convert.ToInt32(Convert.ToDouble(timeout, CultureInfo.InvariantCulture) / 10.0, CultureInfo.InvariantCulture));
            }
            
            throw new AutomationException("Element was not found.");
        }

        /// <summary>
        /// To wait untill all elements are loaded in the page
        /// </summary>
        ///  Author: Yogesh
        /// <param name="myBrowser">Selenium Browser</param>
        /// <returns>None</returns>
        /// Created Date: 23-Dec-2011
        /// Modified By: 
        /// Modified Date: 
        /// Modification Comments:
        public void WaitUntilAllElementsLoad(ISelenium myBrowser)
        {
            try
            {
                IWebDriver driver = ((WebDriverBackedSelenium)myBrowser).UnderlyingWebDriver;
                var ts = new TimeSpan(0, 0, 60);
                driver.Manage().Timeouts().ImplicitlyWait(ts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

     
         /// <summary>
        /// Get Desired element attribute.
        /// Eg. GetElementAttribute(browserInstance, "//div[@id='myFavourites']", "title")
        /// </summary>
        /// <param name="browser">Browser Instance</param>
        /// <param name="xPath">Element xpath</param>
        /// <param name="attributeName">Attribute Name</param>
        /// <returns>Returns attirbute value</returns>
        public string GetElementAttribute(ISelenium browser, string xPath, string attributeName)
        {
            string attributeValue = string.Empty;

            IWebDriver driver = ((WebDriverBackedSelenium)browser).UnderlyingWebDriver;

            if (browser.IsElementPresent(xPath))
            {
                attributeValue = driver.FindElement(By.XPath(xPath)).GetAttribute(attributeName);
            }
            else
            {
                throw new AutomationException("Element " + xPath + " is not found");
            }
            return attributeValue;
        }
        /// <summary>
        /// Get the css property of particular element.
        /// </summary>
        /// <param name="browser">Browser Instance</param>
        /// <param name="xPathElement">Xpath location of element</param>
        /// <param name="propertyName">CssPropertyName</param>
        /// <returns>Returns Css value</returns>
        /// Ex. GetCssProperty(MyBrowser, "//div[@id='textid']","backgroud-color");
        public string GetCssPropertyValue(ISelenium browser, string xPathElement, string propertyName)
        {
            try
            {
                IWebDriver driver = ((WebDriverBackedSelenium)browser).UnderlyingWebDriver;
                string cssValue = driver.FindElement(By.XPath(xPathElement)).GetCssValue(propertyName);
                return cssValue;
            }
            catch (Exception ex)
            {
                throw new AutomationException(ex.ToString());
            }
        }

        /// <summary>
        /// Method to enter text to Desired editbox
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// Date:25th-March-2013
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <param name="strText"> string text</param>
        /// <returns>Boolean value(True/False)</returns>
        public bool EnterTextToEditBox(ISelenium objMySelenium, string strLocator, string strText)
        {
            bool bStatus = true;
            IWebDriver objMyWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            TimeSpan ts = new TimeSpan(0, 0, 60);
            objMyWebDriver.Manage().Timeouts().ImplicitlyWait(ts);
            try
            {
                if (objMyWebDriver.FindElement(By.XPath(strLocator)).Displayed)
                    if (objMySelenium.IsEditable(strLocator))
                    {
                        objMyWebDriver.FindElement(By.XPath(strLocator)).Click();
                        Thread.Sleep(2000);
                        objMyWebDriver.FindElement(By.XPath(strLocator)).SendKeys(strText);
                    }
                    else
                        bStatus = false;
                else
                    bStatus = false;
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

        /// <summary>
        /// Method to click the Desired Button
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// Date:26th-March-2013
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <returns>Boolean value(True/False)</returns>
        public bool ClickButton(ISelenium objMySelenium, string strLocator)
        {
            bool bStatus = true;
            IWebDriver objMyWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            try
            {
                if (objMyWebDriver.FindElement(By.XPath(strLocator)).Displayed)
                    objMyWebDriver.FindElement(By.XPath(strLocator)).Click();
                else
                    bStatus = false;

            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

        /// <summary>
        /// Method to click the Desired Link
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// Date:27th-March-2013
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <returns>Boolean value(True/False)</returns>
        public bool ClickLink(ISelenium objMySelenium, string strLocator)
        {
            bool bStatus = true;
            IWebDriver objMyWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            try
            {
                if (objMyWebDriver.FindElement(By.XPath(strLocator)).Displayed)
                    objMyWebDriver.FindElement(By.XPath(strLocator)).Click();
                else
                    bStatus = false;
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

        /// <summary>
        /// Method to fire the Desired Link/Button
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// Date:27th-March-2013
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="strLocator"> Locator of the Control/Element(Xpath)</param>
        /// <returns>Boolean value(True/False)</returns>

        public bool ClickFireEvent(ISelenium objMySelenium, string strLocator)
        {
            bool bStatus = true;
            IWebDriver objMyWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            try
            {
                if (objMyWebDriver.FindElement(By.XPath(strLocator)).Displayed)
                    objMySelenium.FireEvent(strLocator, "onclick");
                else
                    bStatus = false;
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }


        /// <summary>
        /// Method to get the text from the desired Element/Control
        /// </summary>
        ///Authout : Vamsi Krishna Boyapati
        ///Date : 25th-March-2013
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="strLocator"> Locator of the Control/Element(Xpath)</param>
        /// <param name="strExpText"> String Expected text</param>
        /// <returns>Boolean value(True/False)</returns>
        public bool ValidatingTextOnControl(ISelenium objMySelenium, string strLocator, string strExpText)
        {
            bool bStatus = true;
            string strTemp = null;
            IWebDriver objMyWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            try
            {

                if (objMyWebDriver.FindElement(By.XPath(strLocator)).Displayed)
                {
                    strTemp = objMyWebDriver.FindElement(By.XPath(strLocator)).Text;
                    strTemp = strTemp.ToUpper().Trim().Replace(" ", "");
                    if (!(strTemp.Contains(strExpText.ToUpper().Trim().Replace(" ", ""))))
                        bStatus = false;
                }
                else
                    bStatus = false;
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

        /// <summary>
        /// Method to Wait until Either Desired/Specific element is present or specified time is elapsed
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// Date:26th-March-2013
        /// <param name="objMySelenium">Selenium RC Instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <param name="varTimeOut">Time to Wait</param>

        public void WiatForElementToPresent(ISelenium objMySelenium, string strLocator, int varTimeOut)
        {
            try
            {
                DateTime varDateTime;
                DateTime varElapseTime = DateTime.Now.AddSeconds(varTimeOut);
                do
                {
                    varDateTime = DateTime.Now;
                    if (objMySelenium.IsElementPresent(strLocator))
                        break;
                    else
                        Thread.Sleep(2000);
                } while (varDateTime <= varElapseTime);
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
        }

        /// <summary>
        /// Method to hanle Sync
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// Date:26th-March-2013
        /// <param name="objMySelenium">Selenium Rc instace</param>
        public void PageSync(ISelenium objMySelenium)
        {
            //IWebDriver objMyWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            TimeSpan ts = new TimeSpan(0, 0, 90);
            WaitUntilAllElementsLoad(objMySelenium);
            objMySelenium.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut.ToString());
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Method to select item from ComboBox/DropDown
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// <param name="objMySelenium">RC instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <param name="itemToSelect">String to select</param>
        /// <returns>Boolean value(True/False)</returns>
        public bool SelectItemFromDropDwon(ISelenium objMySelenium, string strLocator, string itemToSelect)
        {
            bool bStatus = false;
            try
            {
                string[] arrItems = objMySelenium.GetSelectOptions(strLocator);
                foreach (string strTemp in arrItems)
                    if (strTemp.ToUpper().Trim().Contains(itemToSelect.Trim().ToUpper()))
                    {
                        objMySelenium.Select(strLocator, strTemp);
                        bStatus = true;
                        break;
                    }
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

        /// <summary>
        /// Method to check whether Toggle Button(CheckBox/Radio) is checked or not
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// <param name="objMySelenium">Selenium RC Instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <returns>Boolean value(True/False)</returns>

        public bool GetCheckBoxStatus(ISelenium objMySelenium, string strLocator)
        {
            bool bStatus = false;
            try
            {
                if (objMySelenium.IsElementPresent(strLocator))
                    bStatus = objMySelenium.IsChecked(strLocator);
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

        /// <summary>
        /// Method to Check the CheckBox
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// <param name="objMySelenium">Selenium RC Instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <returns>Boolean value(True/False)</returns>
        public bool SelectCheckBox(ISelenium objMySelenium, string strLocator)
        {
            bool bStatus = false;
            try
            {
                if (objMySelenium.IsElementPresent(strLocator))
                {
                    objMySelenium.Check(strLocator);
                    bStatus = true;
                }
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

        /// <summary>
        /// Method to Check the Radio Button
        /// </summary>
        /// Authour: Vamsi krishna Boyapati
        /// <param name="objMySelenium">Selenium RC Instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <returns>Boolean value(True/False)</returns>
        public bool SelectRadioButton(ISelenium objMySelenium, string strLocator)
        {
            bool bStatus = false;
            try
            {
                if (objMySelenium.IsElementPresent(strLocator))
                {
                    objMySelenium.Check(strLocator);
                    bStatus = true;
                }
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

        /// <summary>
        /// Return the Desired element from the collection 
        /// </summary>
        /// Authour: Vamsi Krishna Boyapati
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="strLocator">Locator of the Control/Element(Xpath)</param>
        /// <param name="strElementName">Expected Element Name</param>
        /// <returns>WebElement</returns>

        public IWebElement GetDesiredElement(ISelenium objMySelenium, string strLocator, string strElementName)
        {
            IWebElement getElement = null;
            IWebDriver objWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            try
            {
                ReadOnlyCollection<IWebElement> objCollections = objWebDriver.FindElements(By.XPath(strLocator));
                foreach (IWebElement tempEle in objCollections)
                {
                    string strTemp = tempEle.Text.Trim().ToUpper();
                    if (strTemp.Contains(strElementName.Trim().ToUpper()))
                    {
                        getElement = tempEle;
                        break;
                    }
                }
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return getElement;
        }

        /// <summary>
        /// Check the desired string is present in the Dropdown list
        /// </summary>
        /// Authour: Vamsi Krishna Boyapati
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="locator">Locator of the Control/Element(Xpath)</param>
        /// <param name="expectedItem">Expected string </param>
        /// <returns>Boolean value(True/False)</returns>

        public bool CheckItemPresentInDropDownList(ISelenium objMySelenium, string locator, string expectedItem)
        {
            bool bStatus = false;
            try
            {
                string[] itemArray = objMySelenium.GetSelectOptions(locator);
                for (int i = 0; i < itemArray.Length; i++)
                    if (itemArray[i] == expectedItem)
                        bStatus = true;
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }

     
        /// <summary>
        /// Will get the Attribute value of an element using WebDriver
        /// </summary>
        /// Authour: Vamsi Krishna Boyapati
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="locator">Locator of the Control/Element(Xpath)</param>
        /// <param name="nameAttribute">Attribute Name </param>
        /// <returns>String value</returns>

        public string GetAttribute(ISelenium objMySelenium, string locator, string nameAttribute)
        {
            string strAttValue = null;
            IWebDriver objWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            try
            {
                if (!objWebDriver.FindElement(By.XPath(locator)).Displayed)
                    MbUnit.Framework.Assert.Fail("Desired Element is not present");
                strAttValue = objWebDriver.FindElement(By.XPath(locator)).GetAttribute(nameAttribute);
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return strAttValue;
        }

        /// <summary>
        /// To check the Brokes links functionality
        /// </summary>
        /// Authour: Vamsi Krishna Boyapati
        /// <param name="objMySelenium">Selenium RC instance</param>
        /// <param name="URL">App URL that needs to be validate</param>
        /// <returns>Integer value</returns>

        public int ValidateBrokesLinksFunctionality(ISelenium objMySelenium, string appUrl)
        {
            int responseCode = 0;
            IWebDriver objWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
            try
            {
                HttpWebRequest strRequest = (HttpWebRequest)WebRequest.Create(appUrl);
                strRequest.AllowAutoRedirect = false;
                HttpWebResponse strResponse = (HttpWebResponse)strRequest.GetResponse();
                responseCode = (int)strResponse.StatusCode;
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return responseCode;
        }

        /// <summary> Extracts Number from a string
        /// Author : Vamsi Krishna Boyapati
        /// </summary>
        /// <param name="source">Any String</param>
        /// <returns>Extracted Number From the passed String</returns>

        public static void ExtractNumberFromString(ref string source)
        {
            int i = 0;
            string extractNumFromString = "";
            try
            {
                char[] characterCollection = source.ToCharArray();
                for (i = 0; i < characterCollection.Length; i++)
                {
                    if (Char.IsDigit(characterCollection[i]) || Char.IsPunctuation(characterCollection[i]))
                    {
                        extractNumFromString = extractNumFromString + characterCollection[i];
                    }
                }
                source = extractNumFromString;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <Summary>Returns the desired webelement if the control has ID
        /// Author : Vamsi Krishna Boyapati
        /// </Summary>
        /// <param name="objMySelenium">Selenium RC Instance</param>
        /// <Param name="locator">ID of the control</returns>

        public IWebElement GetElementByID(ISelenium objMySelenium, string controlID)
        {
            IWebDriver objWebDriver = null;
            IWebElement desiredWebElement = null;
            try
            {
                objWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
                desiredWebElement = objWebDriver.FindElement(By.Id(controlID));
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return desiredWebElement;
        }

        /// <Summary>Check whether desired element is prent in Application or not
        /// Author : Vamsi Krishna Boyapati
        /// Date  : 23rd-April-2013
        /// </Summary>
        /// <param name="objMySelenium">Selenium RC Instance</param>
        /// <Param name="locator">Locator of the Element</returns>
        public bool ElementPresent(ISelenium objMySelenium, string locator)
        {
            IWebDriver objWebDriver = null;
            bool bStatus = false;
            try
            {
                objWebDriver = ((WebDriverBackedSelenium)objMySelenium).UnderlyingWebDriver;
                bStatus = objWebDriver.FindElement(By.XPath(locator)).Displayed;
            }
            catch (AutomationException EX)
            {
                Console.WriteLine(EX.Message);
            }
            return bStatus;
        }
        #endregion

    }
}
    

