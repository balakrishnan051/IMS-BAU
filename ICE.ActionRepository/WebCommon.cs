using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using MbUnit.Framework;
using System.Threading;
using System.Globalization;
using OpenQA.Selenium.Interactions;
using Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
//using System.Net;
using System.IO;
using Framework;
using ICE.ObjectRepository;
using System.Xml.Linq;
using System.Net;

namespace ICE.ActionRepository
{

    public class wActions : WebCommonMethods
    {


        public enum locatorType
        {
            [Description("xpath")]
            xpath = 0,
            [Description("id")]
            id = 1,
            [Description("class")]
            _class = 2,
            [Description("link")]
            link = 3,
            [Description("name")]
            name = 4,
            [Description("plink")]
            partialLink = 5,
            [Description("tag")]
            tag = 6,
            [Description("cssselector")]
            css = 7
        }
        public static string ReadByKey(string path, string TagName, string _attributeVal)
        {
            try
            {
                XDocument xDoc = XDocument.Load(path);
                var pairs = XDocument.Parse(xDoc.ToString())
                          .Descendants(TagName)
                          .Select(x => new
                          {
                              Key = x.Attribute("key").Value,
                              Value = x.Attribute("value").Value

                          })
                         .Where(addr => (string)addr.Key.ToString() == _attributeVal)

                          .ToDictionary(item => item.Key, item2 => item2.Value);

                string val = null;
                val = pairs[_attributeVal];
                return val.Trim();
            }
            catch (Exception e) { BaseTest.Fail("Issue while reading :" + _attributeVal + " from XML OR files" + " \n Details:" + e.Message.ToString()); }
            return null;

        }

        public By returnLocatorObject(string locatorFile, locatorType type, string name)
        {
            switch (type)
            {
                case locatorType.xpath:
                    return By.XPath(ReadByKey(locatorFile, "xpath", name));
                case locatorType.css:
                    return By.CssSelector(ReadByKey(locatorFile, "cssselector", name));
                case locatorType.id:
                    return By.Id(ReadByKey(locatorFile, "id", name));
                case locatorType.link:
                    return By.LinkText(ReadByKey(locatorFile, "link", name));
                case locatorType._class:
                    return By.ClassName(ReadByKey(locatorFile, "class", name));
                case locatorType.partialLink:
                    return By.PartialLinkText(ReadByKey(locatorFile, "plink", name));
                case locatorType.name:
                    return By.Name(ReadByKey(locatorFile, "name", name));
                case locatorType.tag:
                    return By.TagName(ReadByKey(locatorFile, "tag", name));
                default:
                    return null;
            }
        }

        #region Wrappers
        public void _Click(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            Click(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }
        public void _Click_JavaScript(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            Click_JavaScript(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }

        public void _Click_Javascript(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            Click_JavaScript(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }

        public void _ClickAll(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            ClickAll(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }



        public void _Click(ISelenium driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {

            Click(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }

        public void _Type(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string input, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            Type(driver, returnLocatorObject(locatorFile, type, locatorName), input, failmsg, timeOutSec, noWait);

        }
        public void _Type(ISelenium driver, string locatorFile, locatorType type, string locatorName, string input, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            Type(driver, returnLocatorObject(locatorFile, type, locatorName), input, failmsg, timeOutSec, noWait);

        }

        public void _Clear(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            Clear(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }
        public void _Clear(ISelenium driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            Clear(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }

        public IWebElement _returnWebElement(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            return ReturnWebElement(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }
        public IWebElement _returnWebElement(ISelenium driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            return ReturnWebElement(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }
        public List<IWebElement> _returnWebElements(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            return ReturnWebElements(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }
        public List<IWebElement> _returnWebElements(ISelenium driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            return ReturnWebElements(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec, noWait);

        }

        public string _GetAttribute(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string attributeName, string failmsg = null, bool noWait = true)
        {


            return GetAttribute(driver, returnLocatorObject(locatorFile, type, locatorName), attributeName, failmsg, noWait);

        }
        public string _GetAttribute(ISelenium driver, string locatorFile, locatorType type, string locatorName, string attributeName, string failmsg = null, bool noWait = true)
        {


            return GetAttribute(driver, returnLocatorObject(locatorFile, type, locatorName), attributeName, failmsg, noWait);

        }

        public string _GetText(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, bool noWait = true)
        {


            return GetText(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, noWait);

        }
        public string _GetText(ISelenium driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, bool noWait = true)
        {


            return GetText(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, noWait);


        }

        public void _SelectDropdownOption(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string visibleText_or_Index, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            SelectDropdownOption(driver, returnLocatorObject(locatorFile, type, locatorName), visibleText_or_Index, failmsg, timeOutSec, noWait);

        }
        public void _SelectDropdownOption(ISelenium driver, string locatorFile, locatorType type, string locatorName, string visibleText_or_Index, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            SelectDropdownOption(driver, returnLocatorObject(locatorFile, type, locatorName), visibleText_or_Index, failmsg, timeOutSec, noWait);

        }
        public void _SelectDropdownOption_ByValue(dynamic driver, string locatorFile, locatorType type, string locatorName, string value, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            SelectDropdownOption_ByValue(driver, returnLocatorObject(locatorFile, type, locatorName), value, failmsg, timeOutSec, noWait);

        }

        public double _GetDropdownSelectedValue(dynamic driver, string locatorFile, locatorType type, string locatorName, string value, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {
            return GetDropDownValue(driver, returnLocatorObject(locatorFile, type, locatorName), value, failmsg, timeOutSec, noWait);

        }
        public void _SelectDropdownOption_ByPartialText(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string partialText, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            SelectDropdownOption_ByPartialText(driver, returnLocatorObject(locatorFile, type, locatorName), partialText, failmsg, timeOutSec, noWait);

        }
        public void _SelectDropdownOption_ByPartialText(ISelenium driver, string locatorFile, locatorType type, string locatorName, string partialText, string failmsg = null, int timeOutSec = 0, bool noWait = true)
        {


            SelectDropdownOption_ByPartialText(driver, returnLocatorObject(locatorFile, type, locatorName), partialText, failmsg, timeOutSec, noWait);

        }
        public string _GetSelectedCombobox_Attribute(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string attributeName, string failmsg = null, bool noWait = true)
        {


            return GetSelectedCombobox_Attribute(driver, returnLocatorObject(locatorFile, type, locatorName), attributeName, failmsg, noWait);

        }
        public string _GetSelectedCombobox_Attribute(ISelenium driver, string locatorFile, locatorType type, string locatorName, string attributeName, string failmsg = null, bool noWait = true)
        {


            return GetSelectedCombobox_Attribute(driver, returnLocatorObject(locatorFile, type, locatorName), attributeName, failmsg, noWait);

        }

        public void _FireEvent(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string action, string failmsg = null, bool noWait = true)
        {


            FireEvent(driver, returnLocatorObject(locatorFile, type, locatorName), action, failmsg, noWait);

        }
        public void _FireEvent(ISelenium driver, string locatorFile, locatorType type, string locatorName, string action, string failmsg = null, bool noWait = true)
        {


            FireEvent(driver, returnLocatorObject(locatorFile, type, locatorName), action, failmsg, noWait);

        }

        public void _MouseOver(IWebDriver driver, string locatorFile, locatorType type, string locatorName, locatorType ToLocatorType, string ToLocatorName, MouseOperations action, string failmsg = null, int timeOutInSec = 0)
        {
            MouseOver(driver, returnLocatorObject(locatorFile, type, locatorName), returnLocatorObject(locatorFile, ToLocatorType, ToLocatorName), action, timeOutInSec, failmsg);

        }
        public void _MouseOver(ISelenium driver, string locatorFile, locatorType type, string locatorName, locatorType ToLocatorType, string ToLocatorName, MouseOperations action, string failmsg = null, int timeOutInSec = 0)
        {

            MouseOver(driver, returnLocatorObject(locatorFile, type, locatorName), returnLocatorObject(locatorFile, ToLocatorType, ToLocatorName), action, timeOutInSec, failmsg);

        }

        public void _ClickAndMove(IWebDriver driverObj, string locatorFile, locatorType type, string locatorName, int timeOutInSec, string failMsg)
        {
            ClickAndMove(driverObj, returnLocatorObject(locatorFile, type, locatorName), timeOutInSec, failMsg);
        }

        public void _WaitAndMovetoFrame(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0)
        {


            WaitAndMovetoFrame(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec);

        }
        public void _WaitAndMovetoFrame(ISelenium driver, string locatorFile, locatorType type, string locatorName, string failmsg = null, int timeOutSec = 0)
        {


            WaitAndMovetoFrame(driver, returnLocatorObject(locatorFile, type, locatorName), failmsg, timeOutSec);

        }
        public bool _IsElementPresent(IWebDriver driver, string locatorFile, locatorType type, string locatorName)
        {

            return IsElementPresent(driver, returnLocatorObject(locatorFile, type, locatorName));
        }
        public bool _IsElementPresent(ISelenium driver, string locatorFile, locatorType type, string locatorName)
        {

            return IsElementPresent(driver, returnLocatorObject(locatorFile, type, locatorName));
        }

        public bool _WaitUntilElementPresent(IWebDriver driver, string locatorFile, locatorType type, string locatorName)
        {

            return WaitUntilElementPresent(driver, returnLocatorObject(locatorFile, type, locatorName));
        }
        public bool _WaitUntilElementPresent(ISelenium driver, string locatorFile, locatorType type, string locatorName)
        {

            return WaitUntilElementPresent(driver, returnLocatorObject(locatorFile, type, locatorName));
        }

        public bool _WaitUntilElementDisappears(IWebDriver driver, string locatorFile, locatorType type, string locatorName)
        {

            return WaitUntilElementDisappears(driver, returnLocatorObject(locatorFile, type, locatorName));
        }
        public bool _WaitUntilElementDisappears(ISelenium driver, string locatorFile, locatorType type, string locatorName)
        {

            return WaitUntilElementDisappears(driver, returnLocatorObject(locatorFile, type, locatorName));
        }

        public bool _WaitUntilElementValue_Matches_a_String(IWebDriver driver, string locatorFile, locatorType type, string locatorName, string stringTobeMatched, int timeOutSec)
        {

            return WaitUntilElementValue_Matches_a_String(driver, returnLocatorObject(locatorFile, type, locatorName), stringTobeMatched, timeOutSec);
        }
        public bool _WaitUntilElementValue_Matches_a_String(ISelenium driver, string locatorFile, locatorType type, string locatorName, string stringTobeMatched, int timeOutSec)
        {

            return WaitUntilElementValue_Matches_a_String(driver, returnLocatorObject(locatorFile, type, locatorName), stringTobeMatched, timeOutSec);
        }

        #endregion
    }
    public class WebCommonMethods
    {
        int implicitMinWait = 5;
        public enum MouseOperations
        {
            Click,
            MouseOver,
            MouseDown,
            MouseUp
        }

        /// <summary>
        /// Author:Naga
        /// Returns the selenium equivalent Locator syntax
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public string returnSeleniumLocator(By locator)
        {
            string[] loc = locator.ToString().Split(':');
            switch (loc[0])
            {
                case "By.XPath":
                    return loc[1].Trim();
                case "By.Id":
                    return ("id=" + loc[1].Trim()).Trim();
                case "By.PartialLinkText":
                    return ("//a[contains(text(),'" + loc[1] + "')]").Trim();
                case "By.Name":
                    return ("name=" + loc[1].Trim()).Trim();
                case "By.ClassName":
                    return ("class=" + loc[1].Trim()).Trim();
                case "By.LinkText":
                    return ("link=" + loc[1].Trim()).Trim();
                case "By.CssSelector":
                    return ("css=" + loc[1].Trim()).Trim();
                default:
                    return string.Empty;
            }

        }



        #region DriverActions


        /// <summary>
        /// Naga
        /// Open an URL using selenium
        /// </summary>
        /// <param name="webObj">Webdriver</param>
        /// <param name="URL">URL to Navigate</param>
        /// <param name="failMsg">Fail Message to print, Null if no expection required</param>
        /// <param name="timeOut">Time limit to redo the action</param>
        public void OpenURL(IWebDriver webObj, string URL, string failMsg, int timeOut = 0)
        {
            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    {
                        failMsg = "Object is not Webdriver";
                        throw new Exception();
                    }
                    IWebDriver drive = webObj;
                    ManageTimeOut(drive, implicitMinWait);
                    drive.Navigate().GoToUrl(URL);
                    WaitforPageLoad(drive);

                    //if (!IsElementPresent(webObj, By.XPath("//html/body")))
                    //    throw new Exception();

                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    continue;
                }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        /// <summary>
        /// Naga
        /// Open an URL using Webdriver
        /// </summary>
        /// <param name="webObj">Selenium</param>
        /// <param name="URL">URL to Navigate</param>
        /// <param name="failMsg">Fail Message to print, Null if no expection required</param>
        /// <param name="timeOut">Time limit to redo the action</param>
        public void OpenURL(ISelenium webObj, string URL, string failMsg, int timeOut = 0)
        {
            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    {
                        failMsg = "Object is not WebDriverBackedSelenium";
                        throw new Exception();
                    }
                    webObj.SetTimeout(FrameGlobals.PageLoadTimeOut);
                    webObj.Open(URL);
                    WaitforPageLoad(webObj);

                    //if (!IsElementPresent(webObj, By.XPath("//html/body")))
                    //   throw new Exception();

                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    continue;
                }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);

        }



        /// <summary>
        /// Naga
        /// Click an object
        /// </summary>
        /// <param name="webObj">Backed selenium/Webdriver Object</param>
        /// <param name="Locator">Any valid locators (BY object)</param>
        /// <param name="failMsg">Fail Message</param>
        /// <param name="timeOut">Time Out to redo the action</param>
        /// <param name="noWait">Wait for element present, default no</param>
        public void Click(ISelenium webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);

            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();




                    webObj.Click(returnSeleniumLocator(Locator));



                    WaitforPageLoad(webObj);
                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    if (e.Message.ToString().Contains("Other element would receive the click"))
                    {
                        returnValue = true;
                        break;
                    }
                    else if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        public void Click(IWebDriver webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();

                    ManageTimeOut(webObj, implicitMinWait);


                    webObj.FindElement(Locator).Click();


                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    if (e.Message.ToString().Contains("Other element would receive the click"))
                    {
                        returnValue = true;
                        break;
                    }
                    else if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }

        public void Click_JavaScript(IWebDriver webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    


                    IJavaScriptExecutor executor = (IJavaScriptExecutor)webObj;
                    executor.ExecuteScript("arguments[0].click();", ReturnWebElement(webObj, Locator, failMsg, timeOut, noWait));

                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    if (e.Message.ToString().Contains("Other element would receive the click"))
                    {
                        returnValue = true;
                        break;
                    }
                    else if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }

        public void ClickAll(IWebDriver webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true,int limitclick=0)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();

                    ManageTimeOut(webObj, implicitMinWait);

                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> listOfElements = webObj.FindElements(Locator);
                 int iter = 1;
                    foreach (IWebElement ele in listOfElements)
                    {
                        if (limitclick != 0)
                            if (iter < limitclick)
                            {
                                ele.FindElement(Locator).Click();
                                System.Threading.Thread.Sleep(2000);
                            }
                            else
                                break;
                    }

                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    if (e.Message.ToString().Contains("Other element would receive the click"))
                    {
                        returnValue = true;
                        break;
                    }
                    else if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }



        /// <summary>
        /// Naga
        /// Clear the textbox or object
        /// </summary>
        /// <param name="webObj">Backed selenium/Webdriver Object</param>
        /// <param name="Locator">Locator</param>
        /// <param name="failMsg">Fail message to report</param>
        /// <param name="timeOut">Time out to redo the action</param>
        /// <param name="noWait">wait until element present</param>
        public void Clear(IWebDriver webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);

            WaitforPageLoad(webObj);

            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();

                    ManageTimeOut(webObj, implicitMinWait);


                    webObj.FindElement(Locator).Clear();


                    WaitforPageLoad(webObj);
                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    if (e.Message.ToString().Contains("Other element would receive"))
                    {
                        returnValue = true;
                        break;
                    }

                    else if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        public void Clear(ISelenium webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);

            WaitforPageLoad(webObj);

            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();

                    IWebDriver driver = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
                    driver.FindElement(Locator).Clear();


                    WaitforPageLoad(webObj);
                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    if (e.Message.ToString().Contains("Other element would receive"))
                    {
                        returnValue = true;
                        break;
                    }

                    else if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }



        /// <summary>
        /// Naga
        /// Type in a text box or object
        /// </summary>
        /// <param name="webObj">Backed selenium/Webdriver Object</param>
        /// <param name="Locator">Locator</param>
        /// <param name="input">Input to type</param>
        /// <param name="failMsg">Failmessage to report</param>
        /// <param name="timeOut">Timeout to redo the action if fails</param>
        /// <param name="noWait">wait until element present or not</param>
        public void Type(IWebDriver webObj, By Locator, string input, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            WaitforPageLoad(webObj);
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            do
            {
                varDateTime = DateTime.Now;

                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();

                    webObj.FindElement(Locator).SendKeys(input);

                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        public void Type(ISelenium webObj, By Locator, string input, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            WaitforPageLoad(webObj);
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            do
            {
                varDateTime = DateTime.Now;

                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();

                    webObj.Type(returnSeleniumLocator(Locator), input);


                    returnValue = true;
                    break;
                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }



        /// <summary>
        /// Naga
        /// Returns a web element based on the given Locator
        /// </summary>
        /// <param name="webObj">Backed selenium/Webdriver Object</param>
        /// <param name="Locator">Locator</param>
        /// <param name="failMsg">FailMessage</param>
        /// <param name="timeOut">Timeout to redo the action when fails</param>
        /// <param name="noWait">wait for element or not</param>
        /// <returns>web element</returns>
        public IWebElement ReturnWebElement(IWebDriver webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {


            IWebElement returnElement = null;
            IWebDriver driver;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;

                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();


                    returnElement = webObj.FindElement(Locator);
                    break;
                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        if (failMsg != null)
                            BaseTest.Assert.Fail(failMsg);
                        else
                            PageReload(webObj);
                }

            } while (varDateTime <= varElapseTime);

            return returnElement;
        }
        public IWebElement ReturnWebElement(ISelenium webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {


            IWebElement returnElement = null;
            IWebDriver driver;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;

                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();


                    driver = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
                    returnElement = driver.FindElement(Locator);


                    break;
                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        if (failMsg != null)
                            BaseTest.Assert.Fail(failMsg);
                        else
                            PageReload(webObj);
                }

            } while (varDateTime <= varElapseTime);

            return returnElement;
        }

        public List<IWebElement> ReturnWebElements(IWebDriver webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {


            List<IWebElement> returnElement = new List<IWebElement>();
            IWebDriver driver;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;

                try
                {
                    if (!noWait)
                        WaitUntilElementsPresent(webObj, Locator);

                    if (!IsElementsPresent(webObj, Locator))
                        throw new Exception();


                    returnElement = webObj.FindElements(Locator).ToList();
                    if (returnElement.Count == 0)
                        throw new Exception("No Elements found for: " + Locator);

                    break;
                }
                catch (Exception e)
                {
                    if (varDateTime >= varElapseTime)
                        if (failMsg != null)
                            BaseTest.Assert.Fail(failMsg + " " + e);
                        else
                            PageReload(webObj);
                }

            } while (varDateTime <= varElapseTime);

            return returnElement;
        }
        public List<IWebElement> ReturnWebElements(ISelenium webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {


            List<IWebElement> returnElement = new List<IWebElement>();
            IWebDriver driver;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;

                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();


                    driver = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
                    returnElement = driver.FindElements(Locator).ToList();
                    if (returnElement.Count == 0)
                        throw new Exception("No Elements found for: "+Locator);


                    break;
                }
                catch (Exception e)
                {
                    if (varDateTime >= varElapseTime)
                        if (failMsg != null)
                            BaseTest.Assert.Fail(failMsg+" "+e);
                        else
                            PageReload(webObj);
                }

            } while (varDateTime <= varElapseTime);

            return returnElement;
        }

        /// <summary>
        /// Naga
        /// </summary>
        /// <param name="webObj"></param>
        /// <param name="Locator"></param>
        /// <param name="nameAttribute"></param>
        /// <param name="failMsg"></param>
        /// <param name="noWait"></param>
        /// <returns></returns>
        public string GetAttribute(IWebDriver webObj, By Locator, string nameAttribute, string failMsg = null, bool noWait = true)
        {
            WaitforPageLoad(webObj);
            string strAttValue = null;
            try
            {
                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (failMsg != null)
                    if (!IsElementPresent(webObj, Locator))
                        BaseTest.Assert.Fail(failMsg);



                webObj.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                strAttValue = webObj.FindElement(Locator).GetAttribute(nameAttribute);

            }
            catch (Exception)
            {
                if (failMsg != null)
                    BaseTest.Assert.Fail(failMsg);
            }
            return strAttValue;
        }
        public string GetAttribute(ISelenium webObj, By Locator, string nameAttribute, string failMsg = null, bool noWait = true)
        {
            WaitforPageLoad(webObj);
            string strAttValue = null;
            try
            {
                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (failMsg != null)
                    if (!IsElementPresent(webObj, Locator))
                        BaseTest.Assert.Fail(failMsg);

                IWebDriver driver = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
                strAttValue = driver.FindElement(Locator).GetAttribute(nameAttribute);


            }
            catch (Exception)
            {
                if (failMsg != null)
                    BaseTest.Assert.Fail(failMsg);
            }
            return strAttValue;
        }


        public string GetTitle(IWebDriver webObj, string failMsg)
        {
            string returnValue = string.Empty;

            try
            {

                WaitforPageLoad(webObj);


                returnValue = webObj.Title.ToString();

            }
            catch (Exception)
            {
                BaseTest.Assert.Fail(failMsg);
            }

            return returnValue;

        }
        public string GetTitle(ISelenium webObj, string failMsg)
        {
            string returnValue = string.Empty;

            try
            {

                WaitforPageLoad(webObj);


                returnValue = webObj.GetTitle();


            }
            catch (Exception)
            {
                BaseTest.Assert.Fail(failMsg);
            }

            return returnValue;

        }

        public string GetToolTipText(dynamic webObj, By toMouseOverElement, By toolTipElement, int timeOutInSec, string failMsg)
        {
            string toolTipText = string.Empty;
            IWebElement ele = ReturnWebElement(webObj, toMouseOverElement, "Mouse Over Failed:" + failMsg, FrameGlobals.elementTimeOut, false);
            new Actions(webObj).MoveToElement(ele).Perform();
            ILocatable hoverItem = (ILocatable)ele;
            IMouse mouse = ((IHasInputDevices)webObj).Mouse;

            mouse.MouseMove(hoverItem.Coordinates);

            DateTime dt = DateTime.Now.AddSeconds(timeOutInSec);
            while (dt > DateTime.Now)
            {
                if (IsElementPresent(webObj, toolTipElement))
                {
                    toolTipText = GetAttribute(webObj, toolTipElement, "title", failMsg, false);
                    break;
                }
                else
                {
                    continue;
                }
            }
            return toolTipText;
        }

        public void SelectDropdownOption(IWebDriver webObj, By Locator, string visibleText_or_Index, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);

            IWebDriver driverObj;
            if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                driverObj = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
            else
                driverObj = webObj;
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(driverObj, Locator);

                    if (!IsElementPresent(driverObj, Locator))
                        throw new Exception();
                    SelectElement dropDown = new SelectElement(driverObj.FindElement(Locator));
                    int index;

                    if (int.TryParse(visibleText_or_Index, out index))
                        dropDown.SelectByIndex(index);
                    else
                        dropDown.SelectByText(visibleText_or_Index);

                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(driverObj);

                }
            } while (varDateTime <= varElapseTime);

            if(failMsg!=null)
            BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        public void SelectDropdownOption(ISelenium webObj, By Locator, string visibleText_or_Index, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();

                    int index;

                    if (int.TryParse(visibleText_or_Index, out index))
                        webObj.Select(returnSeleniumLocator(Locator), index.ToString());
                    else
                        webObj.Select(returnSeleniumLocator(Locator), visibleText_or_Index);

                    returnValue = true;
                    break;
                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            BaseTest.Assert.IsTrue(returnValue, failMsg);

        }


        public void SelectDropdownOption_ByValue(dynamic webObj, By Locator, string OptionValue, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            string returnValue1 = null;
            WaitforPageLoad(webObj);

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            IWebDriver driverObj;
            if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                driverObj = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
            else
                driverObj = webObj;
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(driverObj, Locator);

                    if (!IsElementPresent(driverObj, Locator))
                        throw new Exception();
                    SelectElement dropDown = new SelectElement(driverObj.FindElement(Locator));


                    IWebElement Selects = driverObj.FindElement(Locator);
                    List<IWebElement> drop = Selects.FindElements(By.TagName("option")).ToList();
                    int index = 0;
                    foreach (IWebElement options in drop)
                    {
                        if (options.GetAttribute("value").Contains(OptionValue))
                        {
                            dropDown.SelectByIndex(index);
                            returnValue = true;
                            break;
                        }
                        else
                            index++;

                        //  break;
                    }
                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(driverObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }

        public double GetDropDownValue(dynamic webObj, By Locator, string OptionValue, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            WaitforPageLoad(webObj);
            string returnValue = null;
            IWebDriver driverObj;

            if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                driverObj = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
            else
                driverObj = webObj;

            SelectElement dropDown = new SelectElement(driverObj.FindElement(Locator));

            IWebElement Selects = driverObj.FindElement(Locator);
            List<IWebElement> drop = Selects.FindElements(By.TagName("option")).ToList();
            int index = 0;
            foreach (IWebElement options in drop)
                if (options.GetAttribute("value").Contains(OptionValue))
                {
                    dropDown.SelectByIndex(index);
                    returnValue = options.Text.ToString().Trim();
                    string[] separators = { "(", ")", " " };
                    string[] temp = returnValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    double maxVal = 0;
                    double.TryParse(temp[temp.Length - 1], out maxVal);
                    return maxVal;
                    break;
                }
            return -1;
        }

        public void SelectDropdownOption_ByPartialText(IWebDriver driverObj, By Locator, string OptionValue, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(driverObj);

            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(driverObj, Locator);

                    if (!IsElementPresent(driverObj, Locator))
                        throw new Exception();
                   
                    SelectElement dropDown = new SelectElement(driverObj.FindElement(Locator));


                    IWebElement Selects = driverObj.FindElement(Locator);
                    List<IWebElement> drop = Selects.FindElements(By.TagName("option")).ToList();
                    int index = 0;
                    foreach (IWebElement options in drop)
                        if (options.Text.Contains(OptionValue))
                        {
                            dropDown.SelectByIndex(index);
                            returnValue = true;
                            break;
                        }
                        else
                            index++;



                    break;
                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(driverObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        public void SelectDropdownOption_ByPartialText(ISelenium webObj, By Locator, string OptionValue, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);

            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();

                    if (webObj.GetSelectedLabel(returnSeleniumLocator(Locator)).Contains(OptionValue))
                        returnValue = true;


                    break;
                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }


        public string GetSelectedDropdownOptionText(IWebDriver driverObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime; string value = "";
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(driverObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(driverObj, Locator);

                    if (!IsElementPresent(driverObj, Locator))
                        throw new Exception();
                    SelectElement dropDown = new SelectElement(driverObj.FindElement(Locator));


                    IWebElement Selects = driverObj.FindElement(Locator);
                    List<IWebElement> drop = Selects.FindElements(By.TagName("option")).ToList();
                    int index = 0;
                    foreach (IWebElement options in drop)
                        if (options.Selected)
                        {
                            value = options.Text;
                            returnValue = true;
                            break;
                        }
                        else
                            index++;



                    break;
                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(driverObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);
            return value;
        }
        public string GetSelectedDropdownOptionText(ISelenium webObj, By Locator, string failMsg = null, int timeOut = 0, bool noWait = true)
        {
            bool returnValue = false;
            DateTime varDateTime; string value = "";
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            WaitforPageLoad(webObj);

            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!noWait)
                        WaitUntilElementPresent(webObj, Locator);

                    if (!IsElementPresent(webObj, Locator))
                        throw new Exception();


                    value = webObj.GetSelectedLabel(returnSeleniumLocator(Locator));
                    returnValue = true;
                    break;


                }
                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(webObj);

                }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);
            return value;
        }


        public void ExecJavaScript(IWebDriver webObj, string script, string failMsg = null, int TimeOutSec = 0)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(TimeOutSec);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)webObj;
                    js.ExecuteScript(script);
                    returnValue = true;
                    break;
                }
                catch (Exception e) { }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);


        }


        public string GetText(IWebDriver webObj, By Locator, string failMsg = null, bool noWait = true)
        {
            string returnValue = string.Empty;
            WaitforPageLoad(webObj);
            try
            {

                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (!IsElementPresent(webObj, Locator))
                    throw new Exception();

                returnValue = webObj.FindElement(Locator).Text;

            }
            catch (Exception e)
            {
                if (failMsg != null)
                    BaseTest.Assert.Fail(failMsg + "  : " + e.Message);
            }

            return returnValue;

        }
        public string GetText(ISelenium webObj, By Locator, string failMsg = null, bool noWait = true)
        {
            string returnValue = string.Empty;
            WaitforPageLoad(webObj);
            try
            {

                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (!IsElementPresent(webObj, Locator))
                    throw new Exception();

                returnValue = webObj.GetText(returnSeleniumLocator(Locator));

            }
            catch (Exception e)
            {
                if (failMsg != null)
                    BaseTest.Assert.Fail(failMsg + "  : " + e.Message);
            }

            return returnValue;

        }

        public string GetSelectedCombobox_Attribute(IWebDriver webObj, By Locator, string nameAttribute, string failMsg = null, bool noWait = true)
        {
            WaitforPageLoad(webObj);
            SelectElement comboBox = new SelectElement(webObj.FindElement(Locator));

            string strAttValue = null;
            try
            {
                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (failMsg != null)
                    if (!IsElementPresent(webObj, Locator))
                        BaseTest.Assert.Fail(failMsg);

                strAttValue = comboBox.SelectedOption.GetAttribute(nameAttribute);
            }
            catch (Exception)
            {
                if (failMsg != null)
                    BaseTest.Assert.Fail(failMsg);
            }
            return strAttValue;
        }
        public string GetSelectedCombobox_Attribute(ISelenium webObj, By Locator, string nameAttribute, string failMsg = null, bool noWait = true)
        {
            WaitforPageLoad(webObj);


            string strAttValue = null;
            try
            {
                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (failMsg != null)
                    if (!IsElementPresent(webObj, Locator))
                        BaseTest.Assert.Fail(failMsg);

                strAttValue = webObj.GetSelectedValue(returnSeleniumLocator(Locator));
            }
            catch (Exception)
            {
                if (failMsg != null)
                    BaseTest.Assert.Fail(failMsg);
            }
            return strAttValue;
        }


        public void ClickByWebElement(IWebElement element, string failMsg)
        {
            try
            {
                element.Click();
            }
            catch (Exception e)
            {
                if (e.Message.ToString().Contains("Other element would receive the click"))
                {
                  
                    return;
                }
                else
                    BaseTest.Assert.Fail(failMsg);

            
            }
        }
        public void TypeByWebElement(IWebElement element, string input, string failMsg)
        {
            try
            {
                element.SendKeys(input);
            }
            catch (Exception)
            {
                BaseTest.Assert.Fail(failMsg);
            }
        }


        public void FireEvent(IWebDriver webObj, By Locator, string action, string failMsg = null, bool noWait = true)
        {
            bool returnValue = false;
            WaitforPageLoad(webObj);
            try
            {
                ISelenium rcBroswer;
                rcBroswer = new WebDriverBackedSelenium(webObj, "http://www.google.com");
                rcBroswer.Start();


                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (!IsElementPresent(webObj, Locator))
                    throw new Exception();


                rcBroswer.FireEvent(returnSeleniumLocator(Locator), action);
                //WaitforPageLoad(webObj);
                returnValue = true;
            }
            catch (Exception e) { }
            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        public void FireEvent(ISelenium rcBroswer, By Locator, string action, string failMsg = null, bool noWait = true)
        {
            bool returnValue = false;
            WaitforPageLoad(rcBroswer);
            try
            {

                if (!noWait)
                    WaitUntilElementPresent(rcBroswer, Locator);

                if (!IsElementPresent(rcBroswer, Locator))
                    throw new Exception();


                rcBroswer.FireEvent(returnSeleniumLocator(Locator), action);
                //WaitforPageLoad(webObj);
                returnValue = true;
            }
            catch (Exception e) { }
            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);

        }

        public void MouseOver(ISelenium webObj, By toMouseOverElement, By Locator, MouseOperations operation, int timeOutInSec, string failMsg)
        {
            WaitforPageLoad(webObj);
            IWebDriver driverObj;
            driverObj = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
            DateTime dt = DateTime.Now.AddSeconds(timeOutInSec);

            bool isElementFound = false;

            WaitUntilElementPresent(webObj, toMouseOverElement);
            while (dt > DateTime.Now)
            {
                IWebElement ele = driverObj.FindElement(toMouseOverElement);
                new Actions(driverObj).MoveToElement(ele).Perform();
                ILocatable hoverItem = (ILocatable)ele;
                IMouse mouse = ((IHasInputDevices)driverObj).Mouse;


                switch (operation)
                {
                    case MouseOperations.Click:
                        mouse.Click(hoverItem.Coordinates);
                        break;
                    case MouseOperations.MouseOver:
                        mouse.MouseMove(hoverItem.Coordinates);
                        break;
                    case MouseOperations.MouseDown:
                        mouse.MouseDown(hoverItem.Coordinates);
                        break;
                    case MouseOperations.MouseUp:
                        mouse.MouseUp(hoverItem.Coordinates);
                        break;

                }


                if (IsElementPresent(driverObj, Locator))
                {
                    isElementFound = true;
                    break;
                }
                else
                {
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)driverObj;
                    jse.ExecuteScript("window.scrollBy(0,250)", "");

                }
            }
            BaseTest.Assert.IsTrue(isElementFound, failMsg);
        }
        public void MouseOver(IWebDriver driverObj, By toMouseOverElement, By Locator, MouseOperations operation, int timeOutInSec, string failMsg)
        {
            WaitforPageLoad(driverObj);
            bool isElementFound = false;
            DateTime dt = DateTime.Now.AddSeconds(timeOutInSec);

            WaitUntilElementPresent(driverObj, toMouseOverElement);

            while (dt > DateTime.Now)
            {
                IWebElement ele = driverObj.FindElement(toMouseOverElement);
                new Actions(driverObj).MoveToElement(ele).Perform();
                ILocatable hoverItem = (ILocatable)ele;
                IMouse mouse = ((IHasInputDevices)driverObj).Mouse;


                switch (operation)
                {
                    case MouseOperations.Click:
                        mouse.Click(hoverItem.Coordinates);
                        break;
                    case MouseOperations.MouseOver:
                        mouse.MouseMove(hoverItem.Coordinates);
                        break;
                    case MouseOperations.MouseDown:
                        mouse.MouseDown(hoverItem.Coordinates);
                        break;
                    case MouseOperations.MouseUp:
                        mouse.MouseUp(hoverItem.Coordinates);
                        break;

                }

                if (FrameGlobals.BrowserToLoad == Framework.BrowserTypes.InternetExplorer)
                {
                    FireEvent(driverObj, toMouseOverElement, "mouseover");


                }

                Thread.Sleep(3000);
                if (IsElementPresent(driverObj, Locator))
                {
                    isElementFound = true;
                    break;
                }
                else
                {
                 
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)driverObj;
                   jse.ExecuteScript("window.scrollBy(0,20)", "");

                }
            }
            BaseTest.Assert.IsTrue(isElementFound, failMsg);
        }


        public void ClickAndMove(IWebDriver driverObj, By toMouseMoveElement, int timeOutInSec, string failMsg=null)
        {
            WaitforPageLoad(driverObj);
            bool isElementFound = false;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            DateTime varDateTime = DateTime.Now;
            WaitUntilElementPresent(driverObj, toMouseMoveElement);


            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    IWebElement ele = driverObj.FindElement(toMouseMoveElement);
                    //build and perform the mouse move over the menu with Advanced User Interactions API
                    new Actions(driverObj).MoveToElement(ele).Click().Perform();
                    isElementFound = true;

                    break;
                }

                catch (Exception)
                {
                    if (varDateTime >= varElapseTime)
                        break;
                    else
                        PageReload(driverObj);

                }
            } while (varDateTime <= varElapseTime);

            BaseTest.Assert.IsTrue(isElementFound, failMsg);
        }


        public void WaitAndAccepAlert(IWebDriver webObj, string failMsg = null, int timeOutInSec = 0)
        {

            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    //if (int.TryParse(frameID, out iFrameID))
                    //    if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    //        webObj.SelectFrame("index=" + iFrameID.ToString());
                    //  else
                   // webObj.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    webObj.SwitchTo().Alert().Accept();

                    returnValue = true;
                    break;
                }
                catch (Exception) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);
        }


        public void WaitAndMovetoFrame(IWebDriver webObj, string frameID, string failMsg=null, int timeOutInSec = 0)
        {

            bool returnValue = false; int iFrameID;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    if (int.TryParse(frameID, out iFrameID))
                        selectFrame(webObj, iFrameID);
                    else
                        selectFrame(webObj, frameID);

                    returnValue = true;
                    break;
                }
                catch (Exception e) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public void WaitAndMovetoFrame(ISelenium webObj, string frameID, string failMsg=null, int timeOutInSec = 0)
        {

            bool returnValue = false; int iFrameID;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    if (int.TryParse(frameID, out iFrameID))
                        webObj.SelectFrame("index=" + iFrameID.ToString());

                    else
                        webObj.SelectFrame("name=" + frameID.ToString());

                    returnValue = true;
                    break;
                }
                catch (Exception) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public void WaitAndMovetoFrame(IWebDriver webObj, IWebElement element, string failMsg=null, int timeOutInSec = 0)
        {

            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {


                    //if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    webObj.SwitchTo().Frame(element);


                    returnValue = true;
                    break;
                }
                catch (Exception) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }

        public void WaitAndMovetoFrame(IWebDriver webObj, By locator, string failMsg = null, int timeOutInSec = 5)
        {

            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    webObj.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(8));
                    //if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    webObj.SwitchTo().Frame(webObj.FindElement(locator));


                    returnValue = true;
                    break;
                }
                catch (Exception e) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public void WaitAndMovetoFrame(ISelenium webObj, By locator, string failMsg = null, int timeOutInSec = 0)
        {

            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            WaitforPageLoad(webObj);
            do
            {
                varDateTime = DateTime.Now;
                try
                {


                    //if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    webObj.SelectFrame(returnSeleniumLocator(locator));

                    returnValue = true;
                    break;
                }
                catch (Exception) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            if (failMsg != null)
                BaseTest.Assert.IsTrue(returnValue, failMsg);
        }

        public void WaitAndMovetoPopUPWindow(IWebDriver webObj, string windowsHandle, string failMsg, int timeOutInSec = 0, string partialWindowTitle = null)
        {

            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    webObj.SwitchTo().Window(windowsHandle);
                    if (partialWindowTitle != null)
                        if (webObj.Title.Contains(partialWindowTitle))
                            returnValue = true;
                        else
                            returnValue = false;
                    else
                        returnValue = true;
                    break;

                }
                catch (Exception) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public void WaitAndMovetoPopUPWindow(ISelenium webObj, string windowsHandle, string failMsg, int timeOutInSec = 0, string partialWindowTitle = null)
        {

            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            do
            {
                varDateTime = DateTime.Now;
                try
                {


                    webObj.SelectWindow(windowsHandle);
                    if (partialWindowTitle != null)
                        if (webObj.GetTitle().Contains(partialWindowTitle))
                            returnValue = true;
                        else
                            returnValue = false;
                    else
                        returnValue = true;
                    break;

                }
                catch (Exception) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public void WaitAndMovetoPopUPWindow(IWebDriver webObj, string failMsg, int timeOutInSec = 0, string partialWindowTitle = null)
        {

            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    webObj.SwitchTo().Window(webObj.WindowHandles[1].ToString());
                    if (partialWindowTitle != null)
                        if (webObj.Title.Contains(partialWindowTitle))
                            returnValue = true;
                        else
                            returnValue = false;
                    else
                        returnValue = true;
                    break;

                }
                catch (Exception) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public void WaitAndMovetoPopUPWindow_WithIndex(IWebDriver webObj,int windowIndex, string failMsg, int timeOutInSec = 0, string partialWindowTitle = null)
        {

            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutInSec);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    webObj.SwitchTo().Window(webObj.WindowHandles[windowIndex].ToString());
                    if (partialWindowTitle != null)
                        if (webObj.Title.Contains(partialWindowTitle))
                            returnValue = true;
                        else
                            returnValue = false;
                    else
                        returnValue = true;
                    break;

                }
                catch (Exception) { Thread.Sleep(FrameGlobals.generalTimeOut); }
            } while (varDateTime <= varElapseTime);

            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }

        public void ScrollWindow(IWebDriver webObj, int scrollLength = 250)
        {
            WaitforPageLoad(webObj);
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)webObj;
                jse.ExecuteScript("window.scrollBy(0," + scrollLength + ")", "");
            }
            catch (Exception e) { BaseTest.Fail("Unable to Scroll the window, Reason:" + e.Message.ToString()); }
        }
        public void ScrollWindow(ISelenium webObj, int scrollLength = 250)
        {
            WaitforPageLoad(webObj);
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)webObj;
                jse.ExecuteScript("window.scrollBy(0," + scrollLength + ")", "");
            }
            catch (Exception e) { BaseTest.Fail("Unable to Scroll the window, Reason:" + e.Message.ToString()); }
        }

        #endregion

        #region WebDriverUtilities
        public Boolean IsAttributePresent(ISelenium webObj, By Locator, string nameAttribute, string failMsg = null, bool noWait = true)
        {
            bool strAttValue = false;
            try
            {
                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (failMsg != null)
                    if (!IsElementPresent(webObj, Locator))
                        BaseTest.Assert.Fail(failMsg);



                IWebDriver driver = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
                driver.FindElement(Locator).GetAttribute(nameAttribute);


                strAttValue = true;
            }
            catch (Exception)
            {
                strAttValue = false;
            }
            return strAttValue;
        }
        public Boolean IsAttributePresent(IWebDriver webObj, By Locator, string nameAttribute, string failMsg = null, bool noWait = true)
        {
            bool strAttValue = false;
            try
            {
                if (!noWait)
                    WaitUntilElementPresent(webObj, Locator);

                if (failMsg != null)
                    if (!IsElementPresent(webObj, Locator))
                        BaseTest.Assert.Fail(failMsg);

                webObj.FindElement(Locator).GetAttribute(nameAttribute);
                strAttValue = true;
            }
            catch (Exception)
            {
                strAttValue = false;
            }
            return strAttValue;
        }


        public Boolean IsElementPresent(ISelenium webObj, By locator)
        {

            bool returnValue = false;

            try
            {


                ISelenium broswer = webObj;
                returnValue = broswer.IsElementPresent(returnSeleniumLocator(locator));

            }
            catch (Exception) { returnValue = false; }


            return returnValue;

        }
        public Boolean IsElementPresent(IWebDriver webObj, By locator)
        {

            bool returnValue = false;

            try
            {

                ManageTimeOut(webObj, implicitMinWait);
                returnValue = webObj.FindElement(locator).Displayed;

            }
            catch (Exception) { returnValue = false; }
            finally
            {

                ManageTimeOut(webObj, FrameGlobals.elementTimeOut);

            }

            return returnValue;

        }
        public Boolean IsElementsPresent(IWebDriver webObj, By locator)
        {

            bool returnValue = true;

            try
            {

                ManageTimeOut(webObj, implicitMinWait);
                List<IWebElement> test = webObj.FindElements(locator).ToList();

            }
            catch (Exception) { returnValue = false; }
            finally
            {

                ManageTimeOut(webObj, FrameGlobals.elementTimeOut);

            }

            return returnValue;

        }


        public Boolean IsElementEnabled(IWebDriver webObj, By locator)
        {

            bool returnValue = false;

            try
            {

                WaitforPageLoad(webObj);
                returnValue = webObj.FindElement(locator).Enabled;

            }
            catch (Exception) { returnValue = false; }

            return returnValue;

        }
        public Boolean IsElementVisible(ISelenium webObj, By locator)
        {

            bool returnValue = false;

            try
            {

                WaitforPageLoad(webObj);
                returnValue = webObj.IsVisible(returnSeleniumLocator(locator));

            }
            catch (Exception) { returnValue = false; }

            return returnValue;

        }
        public Boolean IsElementEditable(ISelenium webObj, By locator)
        {

            bool returnValue = false;

            try
            {

                WaitforPageLoad(webObj);
                returnValue = webObj.IsEditable(returnSeleniumLocator(locator));

            }
            catch (Exception) { returnValue = false; }

            return returnValue;

        }

        public void WaitforPageLoad(IWebDriver webObj)
        {
            try
            {
                IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(webObj, TimeSpan.FromSeconds(30.00));
                wait.Until(driver1 => ((IJavaScriptExecutor)webObj).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (Exception) { }
            //ManageTimeOut(webObj, double.Parse(FrameGlobals.PageLoadTimeOut) / 1000);

        }
        public void WaitforPageLoad(ISelenium webObj)
        {
            webObj.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);
        }


        public void ManageTimeOut(IWebDriver driver, double timeOutSec)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOutSec));

        }
        public void selectFrame(IWebDriver drive, string frame)
        {
            drive.SwitchTo().Frame(frame);
        }
        public void selectFrame(IWebDriver drive, int frame)
        {
            drive.SwitchTo().Frame(frame);
        }


        public Boolean WaitUntilElementPresent(IWebDriver webObj, By locator)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.elementTimeOut);
            try
            {
                WaitforPageLoad(webObj);
                do
                {
                    varDateTime = DateTime.Now;
                    if (IsElementPresent(webObj, locator))
                    {
                        returnValue = true;
                        break;
                    }
                    else
                        Thread.Sleep(FrameGlobals.generalTimeOut);
                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }
        public Boolean WaitUntilElementsPresent(IWebDriver webObj, By locator)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.elementTimeOut);
            try
            {
                WaitforPageLoad(webObj);
                do
                {
                    varDateTime = DateTime.Now;
                    if (IsElementsPresent(webObj, locator))
                    {
                        returnValue = true;
                        break;
                    }
                    else
                        Thread.Sleep(FrameGlobals.generalTimeOut);
                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }
        public Boolean WaitUntilElementPresent(ISelenium webObj, By locator)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.elementTimeOut);
            try
            {
                do
                {
                    varDateTime = DateTime.Now;
                    if (IsElementPresent(webObj, locator))
                    {
                        returnValue = true;
                        break;
                    }
                    else
                        Thread.Sleep(FrameGlobals.generalTimeOut);
                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }


        public Boolean WaitUntilElementDisappears(IWebDriver webObj, By locator)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.elementTimeOut);
            try
            {
                do
                {
                    varDateTime = DateTime.Now;
                    if (!IsElementPresent(webObj, locator))
                    {
                        returnValue = true;
                        break;
                    }
                    else
                        Thread.Sleep(FrameGlobals.generalTimeOut);
                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }
        public Boolean WaitUntilElementDisappears(ISelenium webObj, By locator)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.elementTimeOut);
            try
            {
                do
                {
                    varDateTime = DateTime.Now;
                    if (!IsElementPresent(webObj, locator))
                    {
                        returnValue = true;
                        break;
                    }
                    else
                        Thread.Sleep(FrameGlobals.generalTimeOut);
                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }


        public Boolean WaitUntilElementValue_Matches_a_String(IWebDriver webObj, By locator, string match, int timeOutSec)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutSec);
            try
            {
                do
                {
                    varDateTime = DateTime.Now;
                    if (IsElementPresent(webObj, locator))
                    {
                        if (GetAttribute(webObj, locator, "value").ToString().Contains(match))
                        {
                            returnValue = true;
                            break;
                        }
                    }
                    else
                        Assert.Fail("Submit button not found");

                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }
        public Boolean WaitUntilElementValue_Matches_a_String(ISelenium webObj, By locator, string match, int timeOutSec)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutSec);
            try
            {
                do
                {
                    varDateTime = DateTime.Now;
                    if (IsElementPresent(webObj, locator))
                    {
                        if (GetAttribute(webObj, locator, "value").ToString().Contains(match))
                        {
                            returnValue = true;
                            break;
                        }
                    }
                    else
                        Assert.Fail("Submit button not found");

                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }


        public Boolean WaitUntilTitlePartialMatch(IWebDriver webObj, string PartialtitleName, int timeOutSec)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutSec);
            try
            {
                do
                {
                    varDateTime = DateTime.Now;
                    if (webObj.Title.ToString().Contains(PartialtitleName))
                    {
                        returnValue = true;
                        break;
                    }
                    else
                        Thread.Sleep(FrameGlobals.generalTimeOut);

                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }
        public Boolean WaitUntilTitlePartialMatch(ISelenium webObj, string PartialtitleName, int timeOutSec)
        {
            bool returnValue = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOutSec);
            try
            {
                do
                {
                    varDateTime = DateTime.Now;
                    if (webObj.GetTitle().ToString().Contains(PartialtitleName))
                    {
                        returnValue = true;
                        break;
                    }
                    else
                        Thread.Sleep(FrameGlobals.generalTimeOut);

                } while (varDateTime <= varElapseTime);
            }
            catch (Exception) { return returnValue; }
            return returnValue;
        }


        public void PageReload(IWebDriver webObj)
        {

            try
            {

                IWebDriver driver = webObj;
                ManageTimeOut(driver, implicitMinWait);
                driver.Navigate().Refresh();
                WaitforPageLoad(driver);

            }
            catch (Exception e) { }
            finally { ManageTimeOut(webObj, FrameGlobals.elementTimeOut); }
        }
        public void PageReload(ISelenium webObj)
        {

            try
            {

                webObj.Refresh();

            }
            catch (Exception e) { }

        }


        public void BrowserQuit(dynamic webObj)
        {
            if (webObj != null)
                if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    webObj.Close();
                else
                {
                    webObj.Quit();
                    webObj.Dispose();
                }
        }
        public void BrowserClose(dynamic webObj)
        {
            if (webObj != null)
                webObj.Close();
        }

        public void test()
        {
        }
        #endregion




        #region HTTPService
        public StringBuilder HTTPRequestXMLPost(string URL, string xml)
        {
            StringBuilder responseValue = new StringBuilder();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(URL));
                request.Method = "POST";
                request.ContentType = "text/xml; encoding='utf-8'";
                request.Accept = "text/xml; encoding='utf-8'";



                byte[] bytes = Encoding.UTF8.GetBytes(xml);
                request.ContentLength = bytes.Length;
                using (Stream putStream = request.GetRequestStream())
                {
                    putStream.Write(bytes, 0, bytes.Length);
                }


                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseValue.AppendLine(reader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                BaseTest.Fail("Error in Service Request: " + e.Message.ToString());
                return null;
            }

            return responseValue;

        }

        #endregion



    }
}
