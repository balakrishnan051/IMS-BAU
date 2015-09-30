using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using Framework;
using System.Threading;
using OpenQA.Selenium;
using System.Diagnostics;
using TestRepository;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Reflection;

using IMS_AdminSuite;
using Selenium;
using Gallio;
using System.Xml;
using ICE.ObjectRepository.Vegas_IMS_BAU;
using ICE.DataRepository;
using OpenQA.Selenium.Chrome;
using ICE.ActionRepository;
using ICE.ObjectRepository;
using ICE.DataRepository.Vegas_IMS_Data;
using AdminSuite;


[assembly: ParallelismLimit]
namespace Regression_AnW_stg
{
    [AssemblyFixture]
    class AnW_BVT_Stg 
    {
        [FixtureTearDown]
        public void AfterRunAssembly()
        {
            BaseTest.EndOfExecution();
        }    
     

        [TestFixture(Order = 1), Timeout(8000)]
        [Parallelizable]
        public class Registration : BaseTest
        {

            #region Declaration
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
            //TestRepository.VegasPortal commonVegas = new VegasPortal();
            WebCommonMethods commonWebMethods = new WebCommonMethods();
            wActions wAction = new wActions();
            readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
            Framework.Common.Common commonFramework = new Framework.Common.Common();
           
            #endregion

            #region commonMethods



            public Configuration TestDataInit()
            {
                ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
                string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
                constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
                constFileName = constFileName.Replace("%20", " ");
                ecf.ExeConfigFilename = constFileName.Replace("regression_anw_stg.dll", "TestData.config");

                Configuration testdataConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                return testdataConfig;
            }

            public string methodname()
            {
                StackTrace stackTrace = new System.Diagnostics.StackTrace();
                StackFrame frame = stackTrace.GetFrames()[2];
                MethodBase method = frame.GetMethod();
                string methodName = method.Name;
                return methodName;
            }

            public IWebDriver browserInitialize(ISelenium iBrowser)
            {
                iBrowser.Start();
                iBrowser.WindowMaximize();
                // Pass("Browser opened successfully.");
                AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
                try
                {
                    // Clear cahce and cookies before the test run begin
                    iBrowser.DeleteAllVisibleCookies();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
                }
                iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
                //iBrowser.Open(FrameGlobals.LadbrokesBase);
                commonWebMethods.OpenURL(iBrowser, FrameGlobals.BingoURL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
                iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

                //bool s = iBrowser.IsElementPresent("id('column-6')");

                //iBrowser.WindowMaximize();
                //iBrowser.WindowFocus();
                IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                commonWebMethods.WaitforPageLoad(drive);
                drive.Manage().Window.Maximize();
                Pass();
                return drive;
            }
            public IWebDriver browserInitialize(ISelenium iBrowser,string URL)
            {
                iBrowser.Start();
                iBrowser.WindowMaximize();
                // Pass("Browser opened successfully.");
                AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
                try
                {
                    // Clear cahce and cookies before the test run begin
                    iBrowser.DeleteAllVisibleCookies();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
                }
                iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
                //iBrowser.Open(FrameGlobals.LadbrokesBase);
                commonWebMethods.OpenURL(iBrowser, URL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
                iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

                //bool s = iBrowser.IsElementPresent("id('column-6')");

                //iBrowser.WindowMaximize();
                //iBrowser.WindowFocus();
                IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                commonWebMethods.WaitforPageLoad(drive);
                drive.Manage().Window.Maximize();
                Pass();
                return drive;
            }
            public void InitializeBase(IWebDriver driver)
            {

                driver.Manage().Cookies.DeleteAllCookies();
                AddTestCase("Load Vegas site", "Site to load successfully");
                commonWebMethods.OpenURL(driver, FrameGlobals.BingoURL, "Base Site failed to load", FrameGlobals.reloadTimeOut);
                driver.Manage().Window.Maximize();
                Pass();

            }
            public string ReadxmlData(string tag, string key, string filename)
            {

                string _testDataFilePath = null;
                if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
                string path = _testDataFilePath + "\\" + filename;
                return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
            }

            #endregion


            /// <summary>
            /// Author:Anand
            /// Register a customer through Playtech Pages
            /// Date: 24/03/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 1)]
            public void Verify_CustomerRegistrationAnW_Bingo()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
               // Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets_stg));
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);                 
                    Pass("Customer registered succesfully");
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
            "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    /*BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.registeredUsername);
                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
                    BaseTest.Pass("Customer Details verified successfully in IMS");*/
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("URL Navigation failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }
                   

            /// <summary>
            /// Author:Anand
            /// Register a customer in Sports portal through Playtech Page
            /// Date: 10/04/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]           
            [Test(Order = 2)]
            public void Verify_CustomerRegistrationAnW_Sports()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
               // Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets_stg));
                    //driverObj.Navigate().GoToUrl(FrameGlobals.LadbrokesSports);
                    //Thread.Sleep(5000);
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "sports_open_account", "Open Account button not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "sports_open_account",Keys.Enter, "Open Account button not found", 0, false);
                    AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    Pass("Customer registered succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("URL Navigation failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            /// <summary>
            /// Author:Anand
            /// Register a customer through Playtech Pages
            /// Date: 24/03/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 3)]
            public void Verify_CustomerRegistrationAnW_Ecomm()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,FrameGlobals.EcomURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
               // Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets_stg));
                    wAction._Type(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "EcomJoin_btn",Keys.Enter, "Join button not found/Not clickable", 0, false);
                  
                    AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                          "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    /*BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.registeredUsername);
                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
                    BaseTest.Pass("Customer Details verified successfully in IMS");*/
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("URL Navigation failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }
            
            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through IMS
            /// Date: 24/03/2014
            /// </summary>
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(2200)]
            public void Verify_CustomerRegistrationAnW_IMS()
            {
                #region DriverInitiation
                // IWebDriver driverObj;
                //ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                // driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from IMS pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    baseIMS.Init();
                    regData.username=commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, Registration_Data.password);
                   // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                        "UserName: " + regData.username + ";\nPassword: " +  Registration_Data.password);

                    AddTestCase("Customer created: " + regData.username,"");
                    Pass();

                 
                    Pass("Customer registered succesfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");                   
                    Fail("Create customer from IMS pages- failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through IMS
            /// Date: 24/03/2014
            /// </summary>
            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(2200)]
            public void Verify_Registration_Of_CreditCustomer_IMS()
            {
              
                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from IMS pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    baseIMS.Init();
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, Registration_Data.password,"Credit");
                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                        "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();


                    Pass("Customer registered succesfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Create customer from IMS pages- failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through Telebet
            /// Date: 24/03/2014
            /// </summary>
            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(2200)]
            public void Verify_CustomerRegistration_Telebet()
            {

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Telebet pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    baseIMS.Init();

                    commIMS.CreateNewCustomer_Telebet(baseIMS.IMSDriver, ref regData);

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                        "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Customer created: " + regData.username, ""); Pass();
                    Pass("Customer registered succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Create customer from IMS pages- failed");
                }
                finally
                {
                    baseIMS.IMSDriver.Quit();
                }
            }

            /// <summary>
            /// Author:Anand
            /// Register a customer through Playtech Pages
            /// Date: 01/04/2014
            /// </summary>

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 6)]
            public void Verify_CustomerLogin_Bingo()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
               // Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                try
                {

                    AddTestCase("Verified the customer login to Bingo portal ", "Customer should be able to Login.");
                    loginData.update_Login_Data(ReadxmlData("lgndata", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("lgndata", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets_stg));
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    AccountAndWallets.LogoutFromPortal(driverObj);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("URL Navigation failed");
                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 7)]
            public void Verify_CustomerLogin_Sports()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
               //// Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, "http://uat-sports.ladbrokes.com/en-gb/");
                #endregion

                try
                {

                    AddTestCase("Verified the customer login to Sports portal ", "Customer should be able to Login.");
                    loginData.update_Login_Data(ReadxmlData("lgnsdata", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("lgnsdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("lgnsdata", "Fname", DataFilePath.Accounts_Wallets_stg));
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AccountAndWallets.Login_Logout_Sports(driverObj, loginData);
                    BaseTest.Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("URL Navigation failed");
                }
            }

        }//class Registration

        [TestFixture(Order = 2), Timeout(8000)]
        [Parallelizable]
        public class MyAccount:BaseTest
        {

            #region Declaration
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
           // TestRepository.VegasPortal commonVegas = new VegasPortal();
            WebCommonMethods commonWebMethods = new WebCommonMethods();
            wActions wAction = new wActions();
            readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);

            Framework.Common.Common commonFramework = new Framework.Common.Common();            
            #endregion

            #region commonMethods



            public Configuration TestDataInit()
            {
                ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
                string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
                constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
                constFileName = constFileName.Replace("%20", " ");
                ecf.ExeConfigFilename = constFileName.Replace("regression_anw_stg.dll", "TestData.config");

                Configuration testdataConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                return testdataConfig;
            }

            public string methodname()
            {
                StackTrace stackTrace = new System.Diagnostics.StackTrace();
                StackFrame frame = stackTrace.GetFrames()[2];
                MethodBase method = frame.GetMethod();
                string methodName = method.Name;
                return methodName;
            }

            public IWebDriver browserInitialize(ISelenium iBrowser)
            {
                iBrowser.Start();
                iBrowser.WindowMaximize();
                // Pass("Browser opened successfully.");
                AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
                try
                {
                    // Clear cahce and cookies before the test run begin
                    iBrowser.DeleteAllVisibleCookies();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
                }
                iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
                //iBrowser.Open(FrameGlobals.LadbrokesBase);
                commonWebMethods.OpenURL(iBrowser, FrameGlobals.BingoURL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
                iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

                //bool s = iBrowser.IsElementPresent("id('column-6')");

                //iBrowser.WindowMaximize();
                //iBrowser.WindowFocus();
                IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                commonWebMethods.WaitforPageLoad(drive);
                drive.Manage().Window.Maximize();
                Pass();
                return drive;
            }
            public IWebDriver browserInitialize(ISelenium iBrowser, string URL)
            {
                iBrowser.Start();
                iBrowser.WindowMaximize();
                // Pass("Browser opened successfully.");
                AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
                try
                {
                    // Clear cahce and cookies before the test run begin
                    iBrowser.DeleteAllVisibleCookies();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
                }
                iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
                //iBrowser.Open(FrameGlobals.LadbrokesBase);
                commonWebMethods.OpenURL(iBrowser, URL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
                iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

                //bool s = iBrowser.IsElementPresent("id('column-6')");

                //iBrowser.WindowMaximize();
                //iBrowser.WindowFocus();
                IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                commonWebMethods.WaitforPageLoad(drive);
                drive.Manage().Window.Maximize();
                Pass();
                return drive;
            }
            public void InitializeBase(IWebDriver driver)
            {

                driver.Manage().Cookies.DeleteAllCookies();
                AddTestCase("Load Vegas site", "Site to load successfully");
                commonWebMethods.OpenURL(driver, FrameGlobals.BingoURL, "Base Site failed to load", FrameGlobals.reloadTimeOut);
                driver.Manage().Window.Maximize();
                Pass();

            }
            public string ReadxmlData(string tag, string key, string filename)
            {

                string _testDataFilePath = null;
                if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
                string path = _testDataFilePath + "\\" + filename;
                return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
            }

            #endregion

            /// <summary>
            /// Author:Sandeep
            /// Modify 'My Accounts' details verification
            /// Date:25/03/2014
            /// </summary>

            [Timeout(2200)]
            [RepeatOnFail]
           // [Parallelizable]
            [Test(Order = 1)]
            public void Modify_My_Accounts()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration               
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Modify 'My accounts' details Validation", "User should be able to modify the details and modified details should be reflected in OB and in PlayTech");
                try
                {

                    loginData.update_Login_Data(ReadxmlData("ccdata", "User", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("ccdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets_stg));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    AccountAndWallets.MyAccount_VerifyLinks(driverObj);
                    AccountAndWallets.MyAccount_ChangePassword(driverObj);
                    AccountAndWallets.MyAccount_ModifyDetails(driverObj);
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Payment method for Credit card is failed for exception");

                }

            }

            /// <summary>
            /// Author:Sandeep
            /// Verify Responsible gambling page
            /// Date:07/04/2014
            /// </summary>
            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 2)]
            public void Verify_Responsible_Gambling()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_AdminSuite.IMS_Base IMSBase = new IMS_Base();
                IMS_AdminSuite.Common IMSComm = new IMS_AdminSuite.Common(); 
               // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Modify deposit limits in 'Responsible Gambling' Page", "User should be able to modify the deposit limit details");
                try
                {

                    loginData.update_Login_Data(ReadxmlData("rgdata", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("rgdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("rgdata", "Fname", DataFilePath.Accounts_Wallets_stg));
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    string setLimit =AccountAndWallets.MyAccount_Responsible_Gambling(driverObj);

                    IMS_Base ims = new IMS_Base();
                    ims.Init();
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, ReadxmlData("rgdata", "User", DataFilePath.Accounts_Wallets_stg));
                    IMSComm.VerifyDepositLimitInIms(IMSBase.IMSDriver, "");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Responsible gambling validation failed for exception:" + e.Message);
                }
            }

            /// <summary>
            /// Author:Anand
            /// Verify Password change in my account page
            /// Date:01/05/2014
            /// </summary>
            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 2)]
            public void Verify_PasswordChange()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Password Change is successfull for the customer", "User should be able to modify the deposit limit details");
                try
                {
                    AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets_stg));
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
            "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    AccountAndWallets.MyAccount_ChangePassword_Func(driverObj, Registration_Data.password);
                    driverObj.Close();
                    driverObj.SwitchTo().Window(portalWindow);
                    driverObj.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                    driverObj.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                    AccountAndWallets.LogoutFromPortal(driverObj);
                    loginData.update_Login_Data(regData.username, "Newpassword1", "fname");
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    Pass("Customer password changed succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Password change failed for exception:" + e.Message);
                }
            }

            /// <summary>
            /// Author:Naga
            /// Register a customer through Playtech Pages
            /// Date: 24/03/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 7)]
            public void Verify_Disable_Auto_TopUP()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
               // Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Verify that Disabiling the AutoTop up option, placing a Bet throws an error", "Customer should not be able to place bet through Auto Top Up");


                    loginData.update_Login_Data(ReadxmlData("lgndata", "User", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("lgndata", "Pass", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets_stg));
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    AccountAndWallets.Ecom_MyAcct_AutoTopUP(driverObj);
                    AccountAndWallets.LogoutFromPortal(driverObj);
                    System.Threading.Thread.Sleep(10000);
                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    AccountAndWallets.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets));
                    AccountAndWallets.AddToBetSlipInsuff(driverObj, 10);
                    Pass("Customer registered succesfully");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Disabled - Auto Top Up scenario failed");
                }

            }


            /// <summary>
            /// Author:Anusha
            /// Auto top up - Positive 
            /// Date: 12/06/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 7)]
            public void Verify_Enable_Auto_TopUP()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Verify that Enabling the AutoTop up option", "Customer should not be able to place bet through Auto Top Up");


                    loginData.update_Login_Data(ReadxmlData("lgndata", "User", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("lgndata", "Pass", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets_stg));
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    AccountAndWallets.Ecom_MyAcct_AutoTopUP_Enable(driverObj);
                    AccountAndWallets.LogoutFromPortal(driverObj);
                    System.Threading.Thread.Sleep(10000);
                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    AccountAndWallets.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets));
                    AccountAndWallets.AddToBetSlipPlaceBet(driverObj, 10);
                    Pass("Customer registered succesfully");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Disabled - Auto Top Up scenario failed");
                }

            }

        }//MyAccount Class

        
        [TestFixture(Order = 3), Timeout(8000)]
        [Parallelizable]
        public class IMSScenarios : BaseTest
        {
            #region Declaration
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
            // TestRepository.VegasPortal commonVegas = new VegasPortal();
            WebCommonMethods commonWebMethods = new WebCommonMethods();
            wActions wAction = new wActions();
            readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);

            Framework.Common.Common commonFramework = new Framework.Common.Common();
            #endregion

            #region commonMethods



            public Configuration TestDataInit()
            {
                ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
                string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
                constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
                constFileName = constFileName.Replace("%20", " ");
                ecf.ExeConfigFilename = constFileName.Replace("regression_anw_stg.dll", "TestData.config");

                Configuration testdataConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                return testdataConfig;
            }

            public string methodname()
            {
                StackTrace stackTrace = new System.Diagnostics.StackTrace();
                StackFrame frame = stackTrace.GetFrames()[2];
                MethodBase method = frame.GetMethod();
                string methodName = method.Name;
                return methodName;
            }

            public IWebDriver browserInitialize(ISelenium iBrowser)
            {
                iBrowser.Start();
                iBrowser.WindowMaximize();
                // Pass("Browser opened successfully.");
                AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
                try
                {
                    // Clear cahce and cookies before the test run begin
                    iBrowser.DeleteAllVisibleCookies();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
                }
                iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
                //iBrowser.Open(FrameGlobals.LadbrokesBase);
                commonWebMethods.OpenURL(iBrowser, FrameGlobals.BingoURL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
                iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

                //bool s = iBrowser.IsElementPresent("id('column-6')");

                //iBrowser.WindowMaximize();
                //iBrowser.WindowFocus();
                IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                commonWebMethods.WaitforPageLoad(drive);
                drive.Manage().Window.Maximize();
                Pass();
                return drive;
            }
            public IWebDriver browserInitialize(ISelenium iBrowser, string URL)
            {
                iBrowser.Start();
                iBrowser.WindowMaximize();
                // Pass("Browser opened successfully.");
                AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
                try
                {
                    // Clear cahce and cookies before the test run begin
                    iBrowser.DeleteAllVisibleCookies();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
                }
                iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
                //iBrowser.Open(FrameGlobals.LadbrokesBase);
                commonWebMethods.OpenURL(iBrowser, URL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
                iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

                //bool s = iBrowser.IsElementPresent("id('column-6')");

                //iBrowser.WindowMaximize();
                //iBrowser.WindowFocus();
                IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                commonWebMethods.WaitforPageLoad(drive);
                drive.Manage().Window.Maximize();
                Pass();
                return drive;
            }
            public void InitializeBase(IWebDriver driver)
            {

                driver.Manage().Cookies.DeleteAllCookies();
                AddTestCase("Load Vegas site", "Site to load successfully");
                commonWebMethods.OpenURL(driver, FrameGlobals.BingoURL, "Base Site failed to load", FrameGlobals.reloadTimeOut);
                driver.Manage().Window.Maximize();
                Pass();

            }
            public string ReadxmlData(string tag, string key, string filename)
            {

                string _testDataFilePath = null;
                if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
                string path = _testDataFilePath + "\\" + filename;
                return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
            }

            #endregion 

            /// <summary>
            /// Author:Anusha
            /// Verify that Self Excluded customers can login to Vegas portal
            /// Date:12/06/2014
            /// </summary>
            [Test]
            [Timeout(2200)]
            [Parallelizable]
            [RepeatOnFail]
            public void Verify_Cust_Login_SelfExcluded()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,"http://vegas-stg.ladbrokes.com/en/");
                #endregion


                IMS_AdminSuite.IMS_Base IMSBase = new IMS_Base();
                IMS_AdminSuite.Common IMSComm = new IMS_AdminSuite.Common();

                AddTestCase("Verify that Self Excluded customers should not login to Vegas portal", "Should not allow the customer to login to Vegas portal");
                //    string browser = "Portal";
                try
                {
                    Registration_Data regData = new Registration_Data();
                    Login_Data loginData = new Login_Data();
                    //Configuration testdata = TestDataInit();
                    try
                    {


                        AddTestCase("Create SelfExcluded customer in OB", "Customer should be created successfully with Selfexcluded status");

                        IMSBase.Init();
                        regData.registeredUsername = IMSComm.CreateNewCustomer(IMSBase.IMSDriver, regData.username, Registration_Data.password);
                        IMSComm.SelfExclude_Customer(IMSBase.IMSDriver, regData.registeredUsername);

                        loginData.update_Login_Data(regData.registeredUsername, loginData.password, "Tester");
                        //loginData.update_Login_Data("abcd", "123456", "Tester");
                        Pass("Customer created successfully");
                    }
                    catch (Exception e) { Assert.Fail("Customer creation failed"); }


                    if (AccountAndWallets.LoginFromPortal_InvalidCust(driverObj, loginData, "Self exclusion error msg did not appeared"))
                        Pass("Customer loggin Failed : Self Excluded Cust not allowed : Error Msg displayed");
                    else
                        throw new Exception();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(IMSBase.IMSDriver, "IMS");
                    Fail("Customer Loggin attempt failed : Error Msg not displayed");
                }

                finally
                {
                    IMSBase.Quit();
                }


            }

            /// <summary>
            /// Author:Rashmi
            /// Verify that whether customer is able to play after suspending the account in Admin
            /// Date:19/05/2013
            /// </summary>
            [Test]
            [Timeout(2200)]
            [Parallelizable]
            [RepeatOnFail]
            public void Verify_Cust_Freeze_and_Unfreeze()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                //driverObj = browserInitialize(iBrowser);
                #endregion
                try
                {
                    BaseTest.AddTestCase("Verify that whether customer is able to play after suspending the account in Admin", "Proper message should be displayed and customer should not able to play the game");
                    baseIMS.Init();
                    loginData.update_Login_Data(ReadxmlData("lgndata", "User", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgndata", "Pass", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets));
                    loginData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
                    loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, loginData.password);
                    BaseTest.Pass("Customer created successfully in IMS");

                    commIMS.FreezeTheCustomer(baseIMS.IMSDriver);
                    baseIMS.Quit();

                    driverObj = browserInitialize(iBrowser);
                    AddTestCase("Verified the customer login, after freeze", "Customer should not be able to Login.and message should be displayed");
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AccountAndWallets.LoginFromPortal_InvalidCust(driverObj, loginData, "User suspended error msg did not appeared");
                    BaseTest.Pass();
                    //driverObj.Close();

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.DisableFreeze(baseIMS.IMSDriver);
                    System.Threading.Thread.Sleep(10000);
                    //baseIMS.Quit();

                    driverObj.Navigate().Refresh();
                    driverObj.Navigate().Refresh();
                    System.Threading.Thread.Sleep(30000);
                    AddTestCase("Verified the customer login, after unfreeze", "Customer should be able to Login.and message should be displayed");
                    //excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    // "UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    //driverObj.Navigate().Refresh();
                    //System.Threading.Thread.Sleep(5000);
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    BaseTest.Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    //CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS, "IMS");
                    Fail("URL Navigation failed");
                    //Pass();
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

        }//IMS class

        [TestFixture(Order = 4), Timeout(8000)]
        [Parallelizable]
        public class Banking:BaseTest
        {
            #region Declaration
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
            //TestRepository.VegasPortal commonVegas = new VegasPortal();
            WebCommonMethods commonWebMethods = new WebCommonMethods();
            wActions wAction = new wActions();
            readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
            Framework.Common.Common commonFramework = new Framework.Common.Common();            
            #endregion

            #region commonMethods



            public Configuration TestDataInit()
            {
                ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
                string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
                constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
                constFileName = constFileName.Replace("%20", " ");
                ecf.ExeConfigFilename = constFileName.Replace("regression_anw_stg.dll", "TestData.config");

                Configuration testdataConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                return testdataConfig;
            }

            public string methodname()
            {
                StackTrace stackTrace = new System.Diagnostics.StackTrace();
                StackFrame frame = stackTrace.GetFrames()[2];
                MethodBase method = frame.GetMethod();
                string methodName = method.Name;
                return methodName;
            }

            public IWebDriver browserInitialize(ISelenium iBrowser)
            {
                iBrowser.Start();
                iBrowser.WindowMaximize();
                // Pass("Browser opened successfully.");
                AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
                try
                {
                    // Clear cahce and cookies before the test run begin
                    iBrowser.DeleteAllVisibleCookies();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
                }
                iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
                //iBrowser.Open(FrameGlobals.LadbrokesBase);
                commonWebMethods.OpenURL(iBrowser, FrameGlobals.BingoURL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
                iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

                //bool s = iBrowser.IsElementPresent("id('column-6')");

                //iBrowser.WindowMaximize();
                //iBrowser.WindowFocus();
                IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                commonWebMethods.WaitforPageLoad(drive);
                drive.Manage().Window.Maximize();
                Pass();
                return drive;
            }
            public void InitializeBase(IWebDriver driver)
            {

                driver.Manage().Cookies.DeleteAllCookies();
                AddTestCase("Load Vegas site", "Site to load successfully");
                commonWebMethods.OpenURL(driver, FrameGlobals.BingoURL, "Base Site failed to load", FrameGlobals.reloadTimeOut);
                driver.Manage().Window.Maximize();
                Pass();

            }
            public string ReadxmlData(string tag, string key, string filename)
            {

                string _testDataFilePath = null;
                if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
                string path = _testDataFilePath + "\\" + filename;
                return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
            }

            #endregion

            /// <summary>
            /// Author:Nagamanickam
            /// Verify payment method through credit card is successful
            /// Date:25/03/2014
            /// </summary>

            /// <summary>
            /// Author:Nagamanickam
            /// Verify payment method through credit card is successful
            /// Date:25/03/2014
            /// </summary>

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 1)]
            //[Parallelizable]
            public void Verify_Deposit_Portal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");
                try
                {
                   // acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets_stg));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg));


                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    AccountAndWallets.DepositTOWallet_Netteller(driverObj, acctData);

                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");                   
                    Fail("Deposit for netteller is failed for exception");

                }

            }

            /// <summary>
            /// Author:Nagamanickam
            /// Verify Withdraw netteller
            /// Date:25/03/2014
            /// </summary>

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 2)]
            // [Parallelizable]
            public void Verify_Withdraw_Portal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               //// Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify Withdraw is successful", "Withdraw should successful.");

                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets_stg));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                     ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    AccountAndWallets.Withdraw_Netteller(driverObj, acctData);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    
                    Fail("Withdraw for netteller is failed for exception");

                }

            }


            /// <summary>
            /// Author:Nagamanickam
            /// Verify canceling netteller withdraw
            /// Date:25/03/2014
            /// </summary>

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 3)]
            // [Parallelizable]
            public void Verify_CancelWIthdrawal_Portal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               //// Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify Cancel deposit is successful", "Cancel withdraw should successful.");
                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets_stg));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    AccountAndWallets.Cancel_Withdraw_Netteller(driverObj, acctData);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                   
                    Fail("Cancel Withdraw for netteller is failed for exception");

                }

            }

            /// <summary>
            /// Author:Nagamanickam
            /// Verify canceling netteller withdraw
            /// Date:25/03/2014
            /// </summary>

            [Timeout(2200)]
            [RepeatOnFail]
           // [Test(Order = 4)]
            // [Parallelizable]
            public void Verify_Transfer_Portal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify Transfer is successful", "Transfer should be successful.");
                try
                {
                    // acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets_stg));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets_stg));

                    acctData.update_Wallets_Name(ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "FromWallet", DataFilePath.Accounts_Wallets_stg));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    AccountAndWallets.Transfer_Withdraw_Netteller(driverObj, acctData);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    
                    Fail("Cancel Withdraw for netteller is failed for exception");

                }

            }
            
            /// <summary>
            /// Author:Sandeep
            /// Modify 'My Accounts' details verification
            /// Date:25/03/2014
            /// </summary>

            [Timeout(2200)]
            [Parallelizable]
            [RepeatOnFail]
            [Test]
            public void Verify_Credit_Card_Registration()
            {
                #region DriverInitiation
                IWebDriver driverObj = null;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
                
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
                #endregion

                //  string creditCardNumber = ReadxmlData("Visadata", "VISA_CCard", DataFilePath.Accounts_Wallets_stg);

                string creditCardNumber = TD.createCreditCard("Visa").ToString();
                Console.WriteLine("Credit Card:" + creditCardNumber);
                // string creditCardNumber = "673518275612873";

                AddTestCase("Validation of credit card registraion", "User should be able to register credit card successfully");
                try
                {
                    baseIMS.Init();

                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, Registration_Data.password, "Credit");

                    //commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, ReadxmlData("Visadata", "credit_card_user", DataFilePath.Accounts_Wallets_stg));

                    //commIMS.AllowDuplicateCreditCard_Newlook(baseIMS.IMSDriver, creditCardNumber.Substring(0, 6), creditCardNumber.Substring(creditCardNumber.Length - 4));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                        "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    //   regData.username="Useruogmlajf";
                    //   Registration_Data.password = "Password1";

                    loginData.update_Login_Data(regData.username, Registration_Data.password, ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets_stg));

                    //loginData.update_Login_Data("test_aditi_april16", "Ladbrokes1", ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets_stg));
                    System.Threading.Thread.Sleep(15000);
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }

                    AccountAndWallets.Verify_Credit_Card_Registration(driverObj, creditCardNumber);
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS, "IMS");
                    //baseIMS.Quit();
                    Fail("Credit Card registration failed for exception");
                }
                finally
                {
                    // commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, ReadxmlData("Visadata", "credit_card_user", DataFilePath.Accounts_Wallets_stg));
                    // commIMS.ReverseDuplicateCreditCard_Newlook(baseIMS.IMSDriver, creditCardNumber.Substring(0, 6), creditCardNumber.Substring(creditCardNumber.Length - 4));
                    baseIMS.Quit();
                }
            }

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 4)]            
            public void Verify_Transfer_Portal_AllWallets()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify Transfer is successful", "Transfer should be successful.");
                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets_stg));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    AccountAndWallets.AllWallets_Transfer(driverObj, acctData, ReadxmlData("depdata", "TransWalletDropDown", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "TransWalletTable", DataFilePath.Accounts_Wallets_stg));

                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Cancel Withdraw for netteller is failed for exception");

                }

            }

            /// <summary>
            /// Author:Sandeep
            /// Verify cashier first deposit page
            /// Date:24/04/2014
            /// </summary>

            [Timeout(2200)]
            [RepeatOnFail]
            [Parallelizable]
            [Test(Order = 6)]
            public void Verify_Cashier_First_Deposit_E2E()
            {
                #region DriverInitiation
                IWebDriver driverObj = null;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                TestRepository.Common commonRepo = new TestRepository.Common();
                #endregion

                AddTestCase("Validation Cashier first deposit page E2E flow using neteller payment method", "User should be able to Register, Deposit, Transfer, withdraw, cancel withdraw from cashier first deposit page");
                try
                {
                    AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets_stg));

                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");

                    #region Customer registration
                    string username = commonRepo.PP_Registration(driverObj, ref regData);
                    System.Threading.Thread.Sleep(5000);
                    wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
                    if (wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.name, "header_username"))
                        Pass();
                    else
                        Fail("Customer Registration failed");
                    driverObj.SwitchTo().DefaultContent();
                    Pass("Customer registered succesfully");
                    #endregion

                    #region Neteller Registration
                    AccountAndWallets.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets_stg), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg));
                    #endregion

                    #region withdrawal
                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                     ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg));
                    Framework.BaseTest.Assert.IsTrue(commonRepo.CommonWithdraw_Netteller_PT(driverObj, acctData, "10", false), "Withdrawal failed, ammount not deducted after withdrawal");

                    wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit tab is not found", FrameGlobals.reloadTimeOut, false);
                    driverObj.SwitchTo().DefaultContent();
                    #endregion

                    #region Cancel withdraw

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg));
                    Framework.BaseTest.Assert.IsTrue(commonRepo.CommonCancelWithdraw_Netteller_PT(driverObj, acctData, "10", false), "Cancel withdrawal failed");

                    //wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
                    ////wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit tab is not found", FrameGlobals.reloadTimeOut, false);
                    driverObj.SwitchTo().DefaultContent();
                    #endregion

                    #region Tranfer
                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_stg),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets_stg));

                    acctData.update_Wallets_Name(ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "FromWallet", DataFilePath.Accounts_Wallets_stg));

                    commonRepo.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, "10");
                    #endregion

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    //CaptureScreenshot(baseIMS, "IMS");
                    //baseIMS.Quit();
                    Fail("Cashier first deposit page verification failed for exception");
                }
            }

            /// <summary>
            /// Author:Anand
            /// Verify Deposit limit is successful
            /// Date:25/03/2014
            /// </summary>

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 1)]
            //[Parallelizable]
            public void Verify_DepositLimit_Portal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                
               
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
               // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify deposit limit functioning correctly", "Deposit limit is not functioning correctly");
                try
                {
                    loginData.update_Login_Data(ReadxmlData("depLimt", "User", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depLimt", "Pass", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depLimt", "Fname", DataFilePath.Accounts_Wallets_stg));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    AccountAndWallets.DepositMax_Netteller(driverObj, ReadxmlData("depLimt", "Amnt", DataFilePath.Accounts_Wallets_stg), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets_stg));
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Deposit limit is not functioning correctly");
                }

            }

        }//Banking Class

        //[TestFixture(Order = 4), Timeout(8000)]
        //public class Mobile_LoginRegistration : BaseTest
        //{
        //    #region Declaration
        //    TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
        //    TestRepository.AccountAndWallets_Mobile AccountAndWallets_Mobile = new AccountAndWallets_Mobile();
        //    TestRepository.VegasPortal commonVegas = new VegasPortal();
        //    WebCommonMethods commonWebMethods = new WebCommonMethods();
        //    wActions wAction = new wActions();
        //    readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);

        //    Framework.Common.Common commonFramework = new Framework.Common.Common();

        //    #endregion

        //    #region commonMethods


        //    public Configuration TestDataInit()
        //    {
        //        ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
        //        string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
        //        constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
        //        constFileName = constFileName.Replace("%20", " ");
        //        ecf.ExeConfigFilename = constFileName.Replace("regression_anw_mobile.dll", "TestData.config");

        //        Configuration testdataConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
        //        return testdataConfig;
        //    }

        //    public string methodname()
        //    {
        //        StackTrace stackTrace = new System.Diagnostics.StackTrace();
        //        StackFrame frame = stackTrace.GetFrames()[2];
        //        MethodBase method = frame.GetMethod();
        //        string methodName = method.Name;
        //        return methodName;
        //    }

        //    public IWebDriver browserInitialize(ISelenium iBrowser)
        //    {
        //        iBrowser.Start();
        //        iBrowser.WindowMaximize();
        //        // Pass("Browser opened successfully.");
        //        AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
        //        try
        //        {
        //            // Clear cahce and cookies before the test run begin
        //            iBrowser.DeleteAllVisibleCookies();
        //        }
        //        catch (Exception Ex)
        //        {
        //            Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
        //        }
        //        iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
        //        //iBrowser.Open(FrameGlobals.LadbrokesBase);
        //        commonWebMethods.OpenURL(iBrowser, FrameGlobals.LadbrokesMobileLobby, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
        //        iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

        //        //bool s = iBrowser.IsElementPresent("id('column-6')");

        //        //iBrowser.WindowMaximize();
        //        //iBrowser.WindowFocus();
        //        IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
        //        commonWebMethods.WaitforPageLoad(drive);
        //        drive.Manage().Window.Maximize();
        //        Pass();
        //        return drive;
        //    }
        //    public IWebDriver browserInitialize(ISelenium iBrowser, string URL)
        //    {
        //        iBrowser.Start();
        //        iBrowser.WindowMaximize();
        //        // Pass("Browser opened successfully.");
        //        AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
        //        try
        //        {
        //            // Clear cahce and cookies before the test run begin
        //            iBrowser.DeleteAllVisibleCookies();
        //        }
        //        catch (Exception Ex)
        //        {
        //            Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
        //        }
        //        iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
        //        //iBrowser.Open(FrameGlobals.LadbrokesBase);
        //        commonWebMethods.OpenURL(iBrowser, URL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
        //        iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

        //        //bool s = iBrowser.IsElementPresent("id('column-6')");

        //        //iBrowser.WindowMaximize();
        //        //iBrowser.WindowFocus();
        //        IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
        //        commonWebMethods.WaitforPageLoad(drive);
        //        drive.Manage().Window.Maximize();
        //        Pass();
        //        return drive;
        //    }
        //    public void InitializeBase(IWebDriver driver)
        //    {

        //        driver.Manage().Cookies.DeleteAllCookies();
        //        AddTestCase("Load Vegas site", "Site to load successfully");
        //        commonWebMethods.OpenURL(driver, FrameGlobals.LadbrokesBase, "Base Site failed to load", FrameGlobals.reloadTimeOut);
        //        driver.Manage().Window.Maximize();
        //        Pass();

        //    }
        //    public string ReadxmlData(string tag, string key, string filename)
        //    {

        //        string _testDataFilePath = null;
        //        if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
        //        string path = _testDataFilePath + filename;
        //        return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
        //    }

        //    #endregion


        //    /// <summary>
        //    /// Author:Anand
        //    /// Register a customer through Mobile Pages
        //    /// Date: 23/04/2014
        //    /// </summary>
        //    [RepeatOnFail]
        //    [Timeout(2200)]
        //    [Test(Order = 1)]
        //    public void Verify_CustomerRegistrationAnW_Mobile()
        //    {

           

        //        #region DriverInitiation
        //        IWebDriver driverObj;
        //        ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
        //        driverObj = browserInitialize(iBrowser, FrameGlobals.LadbrokesMobileLobby);
        //        #endregion

        //        #region Declaration
        //        Registration_Data regData = new Registration_Data();               
        //        IMS_Base baseIMS = new IMS_Base();
        //        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        //        TestRepository.Common commTest = new TestRepository.Common();
        //        #endregion

        //        try
        //        {
        //            AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
        //            regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets_mobile), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets_mobile), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets_mobile), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets_mobile));
        //            AccountAndWallets_Mobile.Registration_Mobile(driverObj, ref regData);
        //            Pass("Customer registered succesfully");
        //            excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
        //    "UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

        //            /*BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
        //            baseIMS.Init();
        //            commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.registeredUsername);
        //            commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
        //            BaseTest.Pass("Customer Details verified successfully in IMS");*/
        //        }
        //        catch (Exception e)
        //        {
        //            exceptionStack(e);
        //            CaptureScreenshot(driverObj, "Portal");
        //            Fail("Registration in Mobile failed");
        //        }
        //        finally
        //        {
        //            baseIMS.Quit();
        //        }
        //    }

           

        //    /// <summary>
        //    /// Author:Anand
        //    /// Register a customer through Playtech Pages
        //    /// Date: 24/03/2014
        //    /// </summary>
        //    [RepeatOnFail]
        //    [Timeout(2200)]
        //    [Test(Order = 2)]
        //    public void Verify_CustomerLoginAnW_Mobile()
        //    {

        //        #region DriverInitiation
        //        IWebDriver driverObj;
        //        ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
        //        driverObj = browserInitialize(iBrowser, FrameGlobals.LadbrokesMobileLobby);
        //        #endregion

        //        #region Declaration
        //        Registration_Data regData = new Registration_Data();         
        //        IMS_Base baseIMS = new IMS_Base();
        //        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        //        TestRepository.Common commTest = new TestRepository.Common();
        //        Login_Data loginData = new Login_Data();
        //        #endregion

        //        try
        //        {
        //            AddTestCase("Verified the customer login to Mobile Games lobby ", "Customer should be able to Login.");
        //            loginData.update_Login_Data(ReadxmlData("lgndata", "User", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("lgndata", "Pass", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets_mobile));
        //            excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
        //               "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

        //            AccountAndWallets_Mobile.LoginFromMobile(driverObj, loginData);
        //         //   AccountAndWallets_Mobile.LogoutFromMobile(driverObj);
        //            BaseTest.Pass();
        //        }
        //        catch (Exception e)
        //        {
        //            exceptionStack(e);
        //            CaptureScreenshot(driverObj, "Portal");
        //            Fail("Mobile login failed");
        //        }
        //    }

            
         


        //}//Mobile Reg Class


        //[TestFixture(Order = 5), Timeout(8000)]
        //public class Mobile_Banking : BaseTest
        //{
        //    #region Declaration
        //    TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
        //    TestRepository.AccountAndWallets_Mobile AccountAndWallets_Mobile = new AccountAndWallets_Mobile();
        //    TestRepository.VegasPortal commonVegas = new VegasPortal();
        //    WebCommonMethods commonWebMethods = new WebCommonMethods();
        //    wActions wAction = new wActions();
        //    readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);

        //    Framework.Common.Common commonFramework = new Framework.Common.Common();

        //    #endregion

        //    #region commonMethods


        //    public Configuration TestDataInit()
        //    {
        //        ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
        //        string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
        //        constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
        //        constFileName = constFileName.Replace("%20", " ");
        //        ecf.ExeConfigFilename = constFileName.Replace("regression_anw_mobile.dll", "TestData.config");

        //        Configuration testdataConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
        //        return testdataConfig;
        //    }

        //    public string methodname()
        //    {
        //        StackTrace stackTrace = new System.Diagnostics.StackTrace();
        //        StackFrame frame = stackTrace.GetFrames()[2];
        //        MethodBase method = frame.GetMethod();
        //        string methodName = method.Name;
        //        return methodName;
        //    }

        //    //public IWebDriver browserInitialize(ISelenium iBrowser)
        //    //{
        //    //    iBrowser.Start();
        //    //    iBrowser.WindowMaximize();
        //    //    // Pass("Browser opened successfully.");
        //    //    AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
        //    //    try
        //    //    {
        //    //        // Clear cahce and cookies before the test run begin
        //    //        iBrowser.DeleteAllVisibleCookies();
        //    //    }
        //    //    catch (Exception Ex)
        //    //    {
        //    //        Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
        //    //    }
        //    //    iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
        //    //    //iBrowser.Open(FrameGlobals.LadbrokesBase);
        //    //    commonWebMethods.OpenURL(iBrowser, FrameGlobals.LadbrokesMobileLobby, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
        //    //    iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

        //    //    //bool s = iBrowser.IsElementPresent("id('column-6')");

        //    //    //iBrowser.WindowMaximize();
        //    //    //iBrowser.WindowFocus();
        //    //    IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
        //    //    commonWebMethods.WaitforPageLoad(drive);
        //    //    drive.Manage().Window.Maximize();
        //    //    Pass();
        //    //    return drive;
        //    //}
        //    public IWebDriver browserInitialize(ISelenium iBrowser, string URL)
        //    {
        //        iBrowser.Start();
        //        iBrowser.WindowMaximize();
        //        // Pass("Browser opened successfully.");
        //        AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
        //        try
        //        {
        //            // Clear cahce and cookies before the test run begin
        //            iBrowser.DeleteAllVisibleCookies();
        //        }
        //        catch (Exception Ex)
        //        {
        //            Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
        //        }
        //        iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
        //        //iBrowser.Open(FrameGlobals.LadbrokesBase);
        //        commonWebMethods.OpenURL(iBrowser, URL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
        //        iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

        //        //bool s = iBrowser.IsElementPresent("id('column-6')");

        //        //iBrowser.WindowMaximize();
        //        //iBrowser.WindowFocus();
        //        IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
        //        commonWebMethods.WaitforPageLoad(drive);
        //        drive.Manage().Window.Maximize();
        //        Pass();
        //        return drive;
        //    }
        //    public void InitializeBase(IWebDriver driver)
        //    {

        //        driver.Manage().Cookies.DeleteAllCookies();
        //        AddTestCase("Load Vegas site", "Site to load successfully");
        //        commonWebMethods.OpenURL(driver, FrameGlobals.LadbrokesBase, "Base Site failed to load", FrameGlobals.reloadTimeOut);
        //        driver.Manage().Window.Maximize();
        //        Pass();

        //    }
        //    public string ReadxmlData(string tag, string key, string filename)
        //    {

        //        string _testDataFilePath = null;
        //        if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
        //        string path = _testDataFilePath + filename;
        //        return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
        //    }

        //    #endregion


       
        //    /// <summary>
        //    /// Author:Naga            
        //    /// Date: 02/04/2014
        //    /// </summary>
        //    [RepeatOnFail]
        //    [Timeout(2200)]
        //    [Test(Order = 1)]
        //    public void Verify_Deposit_Mobile()
        //    {

              
        //        #region DriverInitiation
        //        IWebDriver driverObj;
        //        ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
        //        driverObj = browserInitialize(iBrowser, FrameGlobals.LadbrokesMobileLobby);
        //        #endregion

        //        #region Declaration
        //        Registration_Data regData = new Registration_Data();
        //        IMS_Base baseIMS = new IMS_Base();
        //        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        //        TestRepository.Common commTest = new TestRepository.Common();
        //        Login_Data loginData = new Login_Data();
        //        MyAcct_Data acctData = new MyAcct_Data();
        //        #endregion

        //        try
        //        {
        //            AddTestCase("Verified the amount is deposited into the customer account ", "Amount should be deposited successfully.");
        //            loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_mobile),
        //                null);
        //            excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
        //               "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

        //            acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_mobile));


        //            AccountAndWallets_Mobile.LoginFromMobile(driverObj, loginData);
        //            //   AccountAndWallets_Mobile.LogoutFromMobile(driverObj);
        //            AccountAndWallets_Mobile.Deposit_Mobile(driverObj, acctData);


        //            BaseTest.Pass();
        //        }
        //        catch (Exception e)
        //        {
        //            exceptionStack(e);
        //            CaptureScreenshot(driverObj, "Portal");
        //            Fail("Mobile deposit failed");
        //        }
        //    }

        //    /// <summary>
        //    /// Author:Naga            
        //    /// Date: 02/04/2014
        //    /// </summary>
        //    [RepeatOnFail]
        //    [Timeout(2200)]
        //    [Test(Order = 2)]
        //    public void Verify_Withdraw_Mobile()
        //    {


        //        #region DriverInitiation
        //        IWebDriver driverObj;
        //        ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
        //        driverObj = browserInitialize(iBrowser, FrameGlobals.LadbrokesMobileLobby);
        //        #endregion

        //        #region Declaration
        //        Registration_Data regData = new Registration_Data();
        //        IMS_Base baseIMS = new IMS_Base();
        //        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        //        TestRepository.Common commTest = new TestRepository.Common();
        //        Login_Data loginData = new Login_Data();
        //        MyAcct_Data acctData = new MyAcct_Data();
        //        #endregion

        //        try
        //        {


        //            AddTestCase("Verified the amount is deposited into the customer account ", "Amount should be deposited successfully.");
        //            loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_mobile),
        //                null);
        //            excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
        //               "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

        //            acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_mobile));


        //            AccountAndWallets_Mobile.LoginFromMobile(driverObj, loginData);
        //            //   AccountAndWallets_Mobile.LogoutFromMobile(driverObj);
        //            AccountAndWallets_Mobile.Withdraw_Mobile(driverObj, acctData);


        //            BaseTest.Pass();
        //        }
        //        catch (Exception e)
        //        {
        //            exceptionStack(e);
        //            CaptureScreenshot(driverObj, "Portal");
        //            Fail("Mobile Withdraw failed");
        //        }
        //    }


        ////Verify unsuccessful Withdraw more than the available balance
        //    /// <summary>
        //    /// Author:Roopa            
        //    /// Date: 05/05/2014
        //    /// </summary>
        //    [RepeatOnFail]
        //    [Timeout(2200)]
        //    [Test(Order = 2)]
        //    public void Verify_Withdraw_Mobile_InsufficientBalance()
        //    {


        //        #region DriverInitiation
        //        IWebDriver driverObj;
        //        ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
        //        driverObj = browserInitialize(iBrowser, FrameGlobals.LadbrokesMobileLobby);
        //        #endregion

        //        #region Declaration
        //        Registration_Data regData = new Registration_Data();
        //        IMS_Base baseIMS = new IMS_Base();
        //        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        //        TestRepository.Common commTest = new TestRepository.Common();
        //        Login_Data loginData = new Login_Data();
        //        MyAcct_Data acctData = new MyAcct_Data();
        //        #endregion

        //        try
        //        {


        //            AddTestCase("Verified the amount is deposited into the customer account ", "Amount should be deposited successfully.");
        //            loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_mobile),
        //                null);
        //            excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
        //               "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

        //            acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_mobile));

        //            AccountAndWallets_Mobile.LoginFromMobile(driverObj, loginData);
        //            //   AccountAndWallets_Mobile.LogoutFromMobile(driverObj);
        //            AccountAndWallets_Mobile.Withdraw_Mobile_InsufficientBalance(driverObj, acctData);


        //            BaseTest.Pass();
        //        }
        //        catch (Exception e)
        //        {
        //            exceptionStack(e);
        //            CaptureScreenshot(driverObj, "Portal");
        //            Fail("Mobile Withdraw failed");
        //        }
        //    }

        //    /// <summary>
        //    /// Author:Roopa            
        //    /// Date: 06/05/2014
        //    /// </summary>
        //    [RepeatOnFail]
        //    [Timeout(2200)]
        //    [Test(Order = 2)]
        //    public void Verify_Withdraw_Mobile_InvalidData()
        //    {

        //        #region DriverInitiation
        //        IWebDriver driverObj;
        //        ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
        //        driverObj = browserInitialize(iBrowser, FrameGlobals.LadbrokesMobileLobby);
        //        #endregion

        //        #region Declaration
        //        Registration_Data regData = new Registration_Data();
        //        IMS_Base baseIMS = new IMS_Base();
        //        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        //        TestRepository.Common commTest = new TestRepository.Common();
        //        Login_Data loginData = new Login_Data();
        //        MyAcct_Data acctData = new MyAcct_Data();
        //        #endregion

        //        try
        //        {


        //            AddTestCase("Verified the amount is deposited into the customer account ", "Amount should be deposited successfully.");
        //            loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets_mobile),
        //                null);
        //            excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
        //               "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

        //            acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_mobile),
        //                ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets_mobile));

        //            AccountAndWallets_Mobile.LoginFromMobile(driverObj, loginData);
        //            //   AccountAndWallets_Mobile.LogoutFromMobile(driverObj);
        //            AccountAndWallets_Mobile.Withdraw_Mobile_InsufficientBalance(driverObj, acctData);


        //            BaseTest.Pass();
        //        }
        //        catch (Exception e)
        //        {
        //            exceptionStack(e);
        //            CaptureScreenshot(driverObj, "Portal");
        //            Fail("Mobile Withdraw failed");
        //        }
        //    }



        //}//Mobile Reg Class
        
    }
}
