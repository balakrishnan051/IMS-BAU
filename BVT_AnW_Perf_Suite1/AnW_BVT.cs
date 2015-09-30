//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using MbUnit.Framework;
//using Framework;
//using System.Threading;
//using OpenQA.Selenium;
//using System.Diagnostics;
//using Ladbrokes_IMS_TestRepository;
//using System.IO;
//using System.Configuration;
//using System.Globalization;
//using System.Reflection;
//using AdminSuite;
//using IMS_AdminSuite;
//using Selenium;
//using Gallio;
//using System.Xml;
//using ICE.ObjectRepository.Vegas_IMS_BAU;
//using ICE.DataRepository;
//using OpenQA.Selenium.Chrome;
////using ICE.ActionRepository;
//using ICE.ObjectRepository;
//using ICE.DataRepository.Vegas_IMS_Data;


//[assembly: ParallelismLimit]
//namespace Regression_AnW_perf
//{
//     //[AssemblyFixture]
//    class AnW_BVT_Perf
//    {
       
//        [TestFixture(Order = 1), Timeout(8000)]
//         [Parallelizable]
//        public class Registration : BaseTest
//        {

//        #region Declaration
//        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
//      //  TestRepository.VegasPortal commonVegas = new VegasPortal();
//        WebCommonMethods commonWebMethods = new WebCommonMethods();
//        wActions wAction = new wActions();
//        readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);

//        Framework.Common.Common commonFramework = new Framework.Common.Common();        
//        #endregion

//        #region commonMethods



//        public Configuration TestDataInit()
//        {
//            ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
//            string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
//            constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
//            constFileName = constFileName.Replace("%20", " ");
//            ecf.ExeConfigFilename = constFileName.Replace("regression_anw_stg.dll", "TestData.config");

//            Configuration testdataConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
//            return testdataConfig;
//        }

//        public string methodname()
//        {
//            StackTrace stackTrace = new System.Diagnostics.StackTrace();
//            StackFrame frame = stackTrace.GetFrames()[2];
//            MethodBase method = frame.GetMethod();
//            string methodName = method.Name;
//            return methodName;
//        }

//        public IWebDriver browserInitialize(ISelenium iBrowser)
//        {
//            iBrowser.Start();
//            iBrowser.WindowMaximize();
//            // Pass("Browser opened successfully.");
//            AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
//            try
//            {
//                // Clear cahce and cookies before the test run begin
//                iBrowser.DeleteAllVisibleCookies();
//            }
//            catch (Exception Ex)
//            {
//                Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
//            }
//            iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
//            //iBrowser.Open(FrameGlobals.LadbrokesBase);
//            commonWebMethods.OpenURL(iBrowser, FrameGlobals.BingoURL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
//            iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

//            //bool s = iBrowser.IsElementPresent("id('column-6')");

//            //iBrowser.WindowMaximize();
//            //iBrowser.WindowFocus();
//            IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
//            commonWebMethods.WaitforPageLoad(drive);
//            drive.Manage().Window.Maximize();
//            Pass();
//            return drive;
//        }
//        public void InitializeBase(IWebDriver driver)
//        {

//            driver.Manage().Cookies.DeleteAllCookies();
//            AddTestCase("Load Vegas site", "Site to load successfully");
//            commonWebMethods.OpenURL(driver, FrameGlobals.BingoURL, "Base Site failed to load", FrameGlobals.reloadTimeOut);
//            driver.Manage().Window.Maximize();
//            Pass();

//        }
//        public string ReadxmlData(string tag, string key, string filename)
//        {

//            string _testDataFilePath = null;
//            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
//            string path = _testDataFilePath + "\\" + filename;
//            return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
//        }

//        #endregion

//        /// <summary>
//        /// Author:Anand
//        /// Register a customer through Playtech Pages
//        /// Date: 24/03/2014
//        /// </summary>
//       // [Test(Order=1)]
//        [RepeatOnFail]
//        [Timeout(2200)]        
//        public void Verify_CustomerRegistrationAnW()
//        {
//            #region DriverInitiation
//            IWebDriver driverObj;
//            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//            driverObj = browserInitialize(iBrowser);
//            #endregion

//            #region Declaration
//            Registration_Data regData = new Registration_Data();
            
//            IMS_Base baseIMS = new IMS_Base();
//            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//            #endregion

//            try
//            {
//                AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
//                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                
//                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
//                AnW.Registration_PlaytechPages(driverObj, ref regData);
//                Pass("Customer registered succesfully");
//                excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
//            "UserName: " + regData.username + ";\nPassword: " + regData.password);

                
//                /*BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
//                baseIMS.Init();
//                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.registeredUsername);
//                commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
//                BaseTest.Pass("Customer Details verified successfully in IMS");*/
//            }
//            catch (Exception e)
//            {
//                exceptionStack(e);
//                CaptureScreenshot(driverObj, "Portal");
//                Fail("URL Navigation failed");
//            }
//            finally
//            {
//                baseIMS.Quit();
//            }
//        }

   



//        /// <summary>
//        /// Author:Anand
//        /// Register a customer through Playtech Pages
//        /// Date: 01/04/2014
//        /// </summary>
//       // [Test(Order=3)]
//        [RepeatOnFail]
//        [Timeout(2200)]      
//        public void Verify_CustomerLogin()
//        {
//            #region DriverInitiation
//            IWebDriver driverObj;
//            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//            driverObj = browserInitialize(iBrowser);
//            #endregion

//            #region Declaration
//            Registration_Data regData = new Registration_Data();
          
//            IMS_Base baseIMS = new IMS_Base();
//            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//            Login_Data loginData = new Login_Data();
//            #endregion

//            try
//            {
                

//                AddTestCase("Verified the customer login to Bingo portal ", "Customer should be able to Login.");
//                loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.Accounts_Wallets),
//                    ReadxmlData("ccdata", "pwd", DataFilePath.Accounts_Wallets),
//                    ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets));
//                excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
//                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                AnW.LogoutFromPortal(driverObj);
//                BaseTest.Pass();
//            }
//            catch (Exception e)
//            {
//                exceptionStack(e);
//                CaptureScreenshot(driverObj, "Portal");
//                Fail("URL Navigation failed");
//            }
//        }

//        }//Registration Class

//       [TestFixture(Order = 2), Timeout(8000)]
//       [Parallelizable]
//        public class MyAccount:BaseTest
//        {
//              #region Declaration
//        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
//       // TestRepository.VegasPortal commonVegas = new VegasPortal();
//        WebCommonMethods commonWebMethods = new WebCommonMethods();
//        wActions wAction = new wActions();
//        readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);

//        Framework.Common.Common commonFramework = new Framework.Common.Common();        
//        #endregion

//        #region commonMethods



//        public Configuration TestDataInit()
//        {
//            ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
//            string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
//            constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
//            constFileName = constFileName.Replace("%20", " ");
//            ecf.ExeConfigFilename = constFileName.Replace("regression_anw_stg.dll", "TestData.config");

//            Configuration testdataConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
//            return testdataConfig;
//        }

//        public string methodname()
//        {
//            StackTrace stackTrace = new System.Diagnostics.StackTrace();
//            StackFrame frame = stackTrace.GetFrames()[2];
//            MethodBase method = frame.GetMethod();
//            string methodName = method.Name;
//            return methodName;
//        }

//        public IWebDriver browserInitialize(ISelenium iBrowser)
//        {
//            iBrowser.Start();
//            iBrowser.WindowMaximize();
//            // Pass("Browser opened successfully.");
//            AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
//            try
//            {
//                // Clear cahce and cookies before the test run begin
//                iBrowser.DeleteAllVisibleCookies();
//            }
//            catch (Exception Ex)
//            {
//                Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
//            }
//            iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
//            //iBrowser.Open(FrameGlobals.LadbrokesBase);
//            commonWebMethods.OpenURL(iBrowser, FrameGlobals.BingoURL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
//            iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

//            //bool s = iBrowser.IsElementPresent("id('column-6')");

//            //iBrowser.WindowMaximize();
//            //iBrowser.WindowFocus();
//            IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
//            commonWebMethods.WaitforPageLoad(drive);
//            drive.Manage().Window.Maximize();
//            Pass();
//            return drive;
//        }
//        public void InitializeBase(IWebDriver driver)
//        {

//            driver.Manage().Cookies.DeleteAllCookies();
//            AddTestCase("Load Vegas site", "Site to load successfully");
//            commonWebMethods.OpenURL(driver, FrameGlobals.BingoURL, "Base Site failed to load", FrameGlobals.reloadTimeOut);
//            driver.Manage().Window.Maximize();
//            Pass();

//        }
//        public string ReadxmlData(string tag, string key, string filename)
//        {

//            string _testDataFilePath = null;
//            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
//            string path = _testDataFilePath + "\\" + filename;
//            return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
//        }

//        #endregion
//        /// <summary>
//        /// Author:Sandeep
//        /// Modify 'My Accounts' details verification
//        /// Date:25/03/2014
//        /// </summary>
//      //  [Test(Order=3)]
//        [Timeout(2200)]
//        [RepeatOnFail]
//        public void Modify_My_Accounts()
//        {
//            #region DriverInitiation
//            IWebDriver driverObj;
//            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//            driverObj = browserInitialize(iBrowser);
//            #endregion

//            #region Declaration
//            AdminTests admin = new AdminTests();
//            AdminSuite.Common commonAdm = new AdminSuite.Common();
//            Login_Data loginData = new Login_Data();
//            MyAcct_Data acctData = new MyAcct_Data();

//            Registration_Data regData = new Registration_Data();
//            #endregion

//            AddTestCase("Modify 'My accounts' details Validation", "User should be able to modify the details and modified details should be reflected in OB and in PlayTech");
//            try
//            {

//                loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.Accounts_Wallets),
//                     ReadxmlData("ccdata", "pwd", DataFilePath.Accounts_Wallets),
//                     ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets));

//                excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
//                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);
//                AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

//                try
//                {
//                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
//                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
//                    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
//                }
//                catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

//                AnW.MyAccount_VerifyLinks(driverObj);
//                AnW.MyAccount_ChangePassword(driverObj);
//                AnW.MyAccount_ModifyDetails(driverObj);
//            }

//            catch (Exception e)
//            {

//                exceptionStack(e);
//                CaptureScreenshot(driverObj, "Portal");
//                Fail("Payment method for Credit card is failed for exception");

//            }

//        }

//        /// <summary>
//        /// Author:Sandeep
//        /// Verify Responsible gambling page
//        /// Date:07/04/2014
//        /// </summary>
//       // [Test(Order=4)]
//        [Timeout(2200)]
//        [RepeatOnFail]
//        public void Verify_Responsible_Gambling()
//        {
//            #region DriverInitiation
//            IWebDriver driverObj;
//            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//            driverObj = browserInitialize(iBrowser);
//            #endregion

//            #region Declaration
//            AdminTests admin = new AdminTests();
//            AdminSuite.Common commonAdm = new AdminSuite.Common();
//            Login_Data loginData = new Login_Data();
//            MyAcct_Data acctData = new MyAcct_Data();

//            Registration_Data regData = new Registration_Data();
//            #endregion

//            AddTestCase("Modify deposit limits in 'Responsible Gambling' Page", "User should be able to modify the deposit limit details");
//            try
//            {

//                loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.Accounts_Wallets),
//                    ReadxmlData("ccdata", "pwd", DataFilePath.Accounts_Wallets),
//                    ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets));
//                excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
//                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

//                try
//                {
//                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Login not Successfull/username button is not found", FrameGlobals.reloadTimeOut, false);
//                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
//                    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
//                }
//                catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

//                AnW.MyAccount_Responsible_Gambling(driverObj);

//            }
//            catch (Exception e)
//            {
//                exceptionStack(e);
//                CaptureScreenshot(driverObj, "Portal");
//                Fail("Responsible gambling validation failed for exception:" + e.Message);
//            }
//        }
       

//     }//MyAcctClass



//    }
//}
