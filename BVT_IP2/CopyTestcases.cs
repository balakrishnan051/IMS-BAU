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
//using System;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Firefox;

//[assembly: ParallelismLimit]
//namespace BVT_IP2
//{

//        [TestFixture, Timeout(15000)]
//        [Parallelizable]
//        public class _Copy_30minBVT_IP2 : BaseTest
//        {
//            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
//            wActions wActions = new wActions();
//            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
//            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
//            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
//            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();            
//            AdminSuite.Common adminComm = new AdminSuite.Common();
//            IP2Common ip2 = new IP2Common();

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Registration_DebitCustomer_IMS_Ip2_BVT()
//            {
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
//                AdminSuite.Common adminComm = new AdminSuite.Common();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                #endregion

//                try
//                {
//                    AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    baseIMS.Init();
//                    //  regData.username = "testCDTIP1";
//                    regData.email = "@aditi.com";
//                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, null, null, regData.email);
//                    //Pass();
//                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);

//                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
//                    AddTestCase("Customer created: " + regData.username, "");
//                    Pass();

//                    adminBase.Init();
//                    // Thread.Sleep(TimeSpan.FromMinutes(1));
//                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
//                    adminBase.Quit();

//                    Pass("Credit limit set successfully");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    CaptureScreenshot(adminBase.MyBrowser, "OB");
//                    Fail("Create customer from IMS pages- failed");
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                    adminBase.Cleanup();
//                }
//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Registration_Of_CreditCustomer_IMS_Ip2_BVT()
//            {
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
//                AdminSuite.Common adminComm = new AdminSuite.Common();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                #endregion

//                try
//                {
//                    AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    baseIMS.Init();
//                    //  regData.username = "testCDTIP1";
//                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit");
//                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);

//                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
//                    AddTestCase("Customer created: " + regData.username, "");
//                    Pass();
//                    baseIMS.Quit();

//                    adminBase.Init();
//                     //Thread.Sleep(TimeSpan.FromMinutes(1));
//                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
//                    //adminComm.ManualAdjustment("100", adminBase.MyBrowser);
//                    adminComm.SetCreditLimit(adminBase.MyBrowser);
//                    adminBase.Quit();

//                    Pass("Credit limit set successfully");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    CaptureScreenshot(adminBase.MyBrowser, "OB");
//                    Fail("Create customer from IMS pages- failed");
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                    adminBase.Cleanup();
//                }
//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Registration_Germany_IMS_Ip2_BVT()
//            {
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
//                AdminSuite.Common adminComm = new AdminSuite.Common();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                #endregion

//                try
//                {
//                    AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    baseIMS.Init();
//                    //  regData.username = "testCDTIP1";
//                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Germany");
//                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);
//                   // Pass();
//                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
//                    AddTestCase("Customer created: " + regData.username, "");
//                    Pass();

//                    adminBase.Init();
//                    // Thread.Sleep(TimeSpan.FromMinutes(1));
//                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
//                    //adminComm.ManualAdjustment("100", adminBase.MyBrowser);                    
//                    adminBase.Quit();

//                    Pass("Credit limit set successfully");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    CaptureScreenshot(adminBase.MyBrowser, "OB");
//                    Fail("Create customer from IMS pages- failed");
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                    adminBase.Cleanup();
//                }
//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Sports_Login_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
//                #endregion

//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                // Configuration testdata = TestDataInit();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();               
//                #endregion

              
//                try
//                {
//                    AddTestCase("Verify login is successfull in Sports", "Login should be successfully");
//                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
//                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
//                    AnW.Login_Logout_Sports(driverObj, loginData);
//                    Pass("Sports login successfully");
                   


//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "portal");
//                    Fail("Verify_Sports_Login_IP2_BVT - failed");
//                }
                
//            }
//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Casino_Login_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
//                #endregion

//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                // Configuration testdata = TestDataInit();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                #endregion

               
//                try
//                {
//                    AddTestCase("Verify login is successfull in Casino", "Login should be successfully");
//                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
//                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                    Pass("casino login successfully");
                    
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "portal");
//                    Fail("Verify_Casino_Login_IP2_BVT - failed");
//                }

//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Casino_LockedCust_Login_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
//                #endregion

//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                // Configuration testdata = TestDataInit();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                #endregion


//                try
//                {
//                    AddTestCase("Verify login is not successfull for locked customer in Casino", "Login should not be successfully");
//                    #region Prerequiste
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    #region NewCustTestData
//                    Testcomm.Createcustomer_PostMethod(ref regData);
//                    loginData.username = regData.username;
//                    loginData.password = regData.password;
//                    #endregion
//                       Pass("Customer registered succesfully");
//                    #endregion
//                       WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
//                       loginData.password = regData.password + "8";

//                       for (int i = 0; i < 3; i++)
//                       {
//                           AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");
//                           wAction.PageReload(driverObj);
//                       }

//                       loginData.password = regData.password;

//                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");
//                    Pass("casino login successfully");

//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "portal");
//                    Fail("Verify_Casino_LockedCust_Login_IP2_BVT - failed");
//                }

//            }

//            /// <summary>
//            /// Author:Nagamanickam
//            /// Register a customer through IMS
//            /// Date: 24/03/2014
//            /// </summary>
//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            public void Verify_Casino_Registration_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
//                #endregion

//                #region Declaration
//                Registration_Data regData = new Registration_Data();

//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                AdminTests Ob = new AdminTests();
//                AdminSuite.Common comAdmin = new AdminSuite.Common();
//                #endregion
//                try
//                {
//                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    regData.email = "@aditi.com";
//                         AnW.OpenRegistrationPage(driverObj);
//                    AnW.Registration_PlaytechPages(driverObj, ref regData);
//                    Pass("Customer registered succesfully");
//                    wAction.BrowserQuit(driverObj);

//                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
//                    baseIMS.Init();
//                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
//                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
//                    BaseTest.Pass("Customer Details verified successfully in IMS");
//                    baseIMS.Quit();

//                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
//                    Ob.Init();
//                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
//                    commTest.VerifyCustDetailsInOB(Ob.MyBrowser, regData);
//                    BaseTest.Pass("Customer Details verified successfully in OB");
//                    Ob.Cleanup();
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    CaptureScreenshot(Ob.MyBrowser, "OB");
//                    Fail("Verify_RegBtn_Cust_Registration_LiveDealer failed");
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                    Ob.Cleanup();
//                }
//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            public void Verify_Sports_Customer_Registration_Ip2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
//                #endregion

//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                AdminTests Ob = new AdminTests();
//                AdminSuite.Common comAdmin = new AdminSuite.Common();
//                #endregion

//                try
//                {
//                    AddTestCase("Create customer from classic sports", "Customer should be created.");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    wAction.Click(driverObj, By.XPath("id('login')/a[text()='Open account']"), "Open Account button not found", 0, false);
//                    regData.email = "@aditi.com";
//                    //       AnW.OpenRegistrationPage(driverObj);
//                    AnW.Registration_PlaytechPages(driverObj, ref regData);
//                    Pass("Customer registered succesfully");

//                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
//                    baseIMS.Init();
//                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
//                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
//                    BaseTest.Pass("Customer Details verified successfully in IMS");
//                    baseIMS.Quit();

//                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
//                    Ob.Init();
//                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
//                    commTest.VerifyCustDetailsInOB(Ob.MyBrowser, regData);
//                    BaseTest.Pass("Customer Details verified successfully in OB");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    CaptureScreenshot(Ob.MyBrowser, "OB");
//                    Fail("Verify_Sports_Customer_Registration failed");
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                    Ob.Cleanup();
//                }
//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_SelfExcl_DebitCustomer_Ip2_BVT()
//            {
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
//                AdminSuite.Common adminComm = new AdminSuite.Common();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                #endregion

//                try
//                {
//                    AddTestCase("Verify setting selfexclusion for a debit customer in OB", "Credit limit should be set successfully");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    baseIMS.Init();
                    
//                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password);
//                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

//                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
//                    AddTestCase("Customer created: " + regData.username, "");
//                    Pass();
//                    baseIMS.Quit();

//                    adminBase.Init();
//                    Thread.Sleep(TimeSpan.FromMinutes(1));
//                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
//                    // string s = wAction.GetSelectedDropdownOptionText(adminBase.MyBrowser, By.Name("Status"), "Status field not found");
//                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(adminBase.MyBrowser, By.XPath("//b[text()='PT_SELFEX,LOGIN']")), "Self exclusion is not synced with OB");
//                    adminBase.Quit();
//                    WriteUserName(regData.username);
//                    Pass();
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    CaptureScreenshot(adminBase.MyBrowser, "OB");
//                    Fail("Verify_SelfExcl_DebitCustomer_Ip2_BVT- failed");
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                    adminBase.Cleanup();
//                }
//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_SearchCustomer_Telebet()
//            {

//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
//                #endregion
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
//                AdminSuite.Common adminComm = new AdminSuite.Common();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                #endregion

//                try
//                {
//                    AddTestCase("Deposit from Telebet pages", "Deposit should be successful");
//                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
//                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication));


//                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
//                        baseTB2.Init(driverObj);
//                    else
//                        baseTB2.Init();

//                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
//                    WriteCommentToMailer("UserName: " + loginData.username);
//                    AddTestCase("Searched Customer in Telebet:  " + loginData.username, ""); Pass();
//                    Pass("Fund deposited succesfully");





//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
//                    Fail("Verify_SearchCustomer_Telebet - failed");
//                }
//                finally
//                {

//                    baseTB2.Quit();
//                }
//            }


//            /// <summary>
//            /// Naga
//            /// GEN-4423
//            /// </summary>
//          //  [Test(Order = 1)]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_RegBtn_Cust_Registration_Bingo_Ip2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
//                #endregion

//                #region Declaration
//                Registration_Data regData = new Registration_Data();

//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                AdminTests Ob = new AdminTests();
//                AdminSuite.Common comAdmin = new AdminSuite.Common();
//                #endregion
//                try
//                {
//                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

//                         AnW.OpenRegistrationPage(driverObj);
//                    AnW.Registration_PlaytechPages(driverObj, ref regData);
//                    Pass("Customer registered succesfully");
//                    wAction.BrowserQuit(driverObj);


//                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
//                    baseIMS.Init();
//                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
//                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
//                    baseIMS.Quit();
//                    BaseTest.Pass("Customer Details verified successfully in IMS");

//                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
//                    Ob.Init();
//                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
//                    commTest.VerifyCustDetailsInOB(Ob.MyBrowser, regData);
//                    Ob.Cleanup();
//                    BaseTest.Pass("Customer Details verified successfully in OB");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    CaptureScreenshot(Ob.MyBrowser, "OB");
//                    Fail("Verify_RegBtn_Cust_Registration_Bingo failed");
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                    Ob.Cleanup();
//                }
//            }


//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            public void Verify_Freeze_UnFreeze_CustomerStatusInOB_Ip2_BVT()
//            {
//                #region Declaration
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
//                AdminSuite.Common adminComm = new AdminSuite.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                // Configuration testdata = TestDataInit();
//                Registration_Data regData = new Registration_Data();
//                #endregion

//                AddTestCase("Verify the UnFreezed user in IMS is in Active state in OB", "UnFreezed user should be in Active status");
//                try
//                {
                 
//                    baseIMS.Init();

//                   // loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Test),
//                     //   ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Test),
//                       // ReadxmlData("lgndata", "Fname", DataFilePath.IP2_Test));
//                    loginData.password = ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication);
//                    loginData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
//                    loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, loginData.password);
//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
//                    BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
//                    BaseTest.Pass();
//                    BaseTest.Pass("Customer created successfully in IMS");
//                    commIMS.FreezeTheCustomer(baseIMS.IMSDriver);
//                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
//                    commIMS.DisableFreeze(baseIMS.IMSDriver);
//                    baseIMS.Quit();
//                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    
//                    AddTestCase("Verify the UnFreezed user status is Active OB", "Freezed user should be in Active status");
//                    adminBase.Init();
//                    adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
//                    string Status = wAction.GetSelectedDropdownOptionText(adminBase.MyBrowser, By.Name("Status"));
//                    BaseTest.Assert.IsTrue(Status.Contains("Active"), "Status in OB for freezed customer is :" + Status);
//                    Pass();
//                    Pass();
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    CaptureScreenshot(adminBase.MyBrowser, "OB");
//                    Fail("Freezed customer verification in OB is failed for an exception");
//                }
//                finally
//                {
//                    adminBase.Quit();
//                    baseIMS.Quit();
//                }

//            }



//          //  [Test(Order = 3)]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_PayPal_Deposit_GBP_Ip2_BVT()
//            {
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                #endregion
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser,FrameGlobals.VegasURL);
//                #endregion

//                AddTestCase("Verify the payment deposit with pay pal method is successful", "Amount should be deposited to the selected wallet");
//                try
//                {

//                    #region Prerequiste
//                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    #region NewCustTestData
//                    Testcomm.Createcustomer_PostMethod(ref regData);
//                    loginData.username = regData.username;
//                    loginData.password = regData.password;
//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                    #endregion
//                    //       AnW.OpenRegistrationPage(driverObj);
//                    // AnW.Registration_PlaytechPages(driverObj, ref regData);
//                    Pass("Customer registered succesfully");

//                    #endregion

//                    acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.IP2_Authetication),
//                        ReadxmlData("paydata", "user_pwd", DataFilePath.IP2_Authetication),
//                        ReadxmlData("paydata", "CCAmt", DataFilePath.IP2_Authetication),
//                         ReadxmlData("paydata", "depWallet", DataFilePath.IP2_Authetication));

//                    AnW.Register_Paypal(driverObj, acctData);
//                    WriteUserName(regData.username);
//                    BaseTest.Pass();
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Registration scenario failed for Paypal");
//                }

//            }

//        //    [Test(Order = 4)]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Deposit_VisaCard_Casino_Ip2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
//                #endregion

//                AddTestCase("Verify the Banking Links to deposit to Gaming wallet using credit card", "Amount should be deposited to the selected wallet");
//                MyAcct_Data acctData = new MyAcct_Data();

//                try
//                {
//                    Login_Data loginData = new Login_Data();

//                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.IP2_Authetication),
//                  ReadxmlData("vccdata", "pwd", DataFilePath.IP2_Authetication),
//                  ReadxmlData("vccdata", "CCFname", DataFilePath.IP2_Authetication));

//                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.IP2_Authetication),
//                        ReadxmlData("vccdata", "CCAmt", DataFilePath.IP2_Authetication),
//                         ReadxmlData("vccdata", "CCAmt", DataFilePath.IP2_Authetication),
//                         ReadxmlData("vccdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "withWallet", DataFilePath.IP2_Authetication),
//                          ReadxmlData("vccdata", "CVV", DataFilePath.IP2_Authetication));

//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                    AnW.DepositTOWallet_CC(driverObj, acctData);

//                    WriteUserName(loginData.username);

//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Deposit has failed");
//                    //Pass();
//                }
//            }

//            //[Test(Order = 5)]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Withdraw_Portal_CreditCard_Ip2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser);
//                #endregion

//                #region Declaration


//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                //// Configuration testdata = TestDataInit();
//                Registration_Data regData = new Registration_Data();
//                #endregion

//                AddTestCase("Verify Withdraw is successful", "Withdraw should successful.");

//                try
//                {
//                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

//                    loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.IP2_Authetication),
//                        ReadxmlData("ccdata", "pwd", DataFilePath.IP2_Authetication),
//                        ReadxmlData("ccdata", "CCFname", DataFilePath.IP2_Authetication));

//                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.IP2_Authetication),
//                            ReadxmlData("vccdata", "CCAmt", DataFilePath.IP2_Authetication),
//                             ReadxmlData("vccdata", "CCAmt", DataFilePath.IP2_Authetication),
//                             ReadxmlData("vccdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "withWallet", DataFilePath.IP2_Authetication),
//                              ReadxmlData("vccdata", "CVV", DataFilePath.IP2_Authetication));

//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                    AnW.Withdraw_CC(driverObj, acctData, ReadxmlData("depdata", "TransWalletDropDown", DataFilePath.IP2_Authetication));
//                    Pass();
//                }

//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Withdraw for credit card is failed for exception");

//                }

//            }
          
//            [Test(Order = 6)]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_CustomerRegistration_Telebet_Ip2_BVT()
//            {

//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
//                AdminSuite.Common adminComm = new AdminSuite.Common();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                #endregion
//                #region DriverInitiation
//                IWebDriver driverObj = new FirefoxDriver();
//                wAction.OpenURL(driverObj, "http://account-test.ladbrokes.com/en/registration-telebet", "Telebet page did not open");
//                #endregion


//                try
//                {
//                    AddTestCase("Create customer from Telebet pages", "Customer should be created.");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                 //   baseIMS.Init();

//                   // commIMS.CreateNewCustomer_Telebet(baseIMS.IMSDriver, ref regData);
//                    string temp = Registration_Data.depLimit; 
//                    Registration_Data.depLimit = "5000";
//                    commIMS.TeleBet_Registration(driverObj, ref regData);
//                    Registration_Data.depLimit = temp;

//                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
//                    BaseTest.AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
//                    BaseTest.Pass();

//                    adminBase.Init();
//                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
//                    adminBase.Quit();

//                    Pass("Customer registered succesfully");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(baseIMS.IMSDriver, "Telebet");
//                    CaptureScreenshot(adminBase.MyBrowser, "OB");
//                    Fail("Create customer from IMS pages- failed");
//                }
//                finally
//                {
//                    //baseIMS.Quit();
//                    adminBase.Cleanup();
//                }
//            }
      

//            //[Test(Order = 7)]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Deposit_Telebet_Ip2_BVT()
//            {

//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
//                #endregion
//                #region Declaration
//                Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
//                Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                #endregion

//                try
//                {
//                    AddTestCase("Deposit from Telebet pages", "Deposit should be successful");
//                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.IP2_Authetication),
//                ReadxmlData("vccdata", "pwd", DataFilePath.IP2_Authetication),
//                ReadxmlData("vccdata", "CCFname", DataFilePath.IP2_Authetication));

//                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.IP2_Authetication),
//                        ReadxmlData("vccdata", "TCCAmt", DataFilePath.IP2_Authetication),
//                         ReadxmlData("vccdata", "TCCAmt", DataFilePath.IP2_Authetication),
//                         ReadxmlData("vccdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication),
//                          ReadxmlData("vccdata", "CVV", DataFilePath.IP2_Authetication));

//                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
//                        baseTB2.Init(driverObj);
//                    else
//                        baseTB2.Init();

//                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
//                    WriteCommentToMailer("UserName: " + loginData.username);
//                    AddTestCase("Deposit to customer " + loginData.username, ""); Pass();
//                    commTB2.CreditCardDepositTB2(baseTB2.IMSDriver, acctData);
//                    WriteUserName(loginData.username);
//                    Pass("Fund deposited succesfully");

//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
//                    Fail("Verify_Deposit_Telebet - failed");
//                }
//                finally
//                {

//                    baseTB2.Quit();
//                }
//            }
//            //[Test(Order = 8)]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Withdraw_Limit_Cancel_Telebet_Ip2_BVT()
//            {

//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
//                #endregion
//                #region Declaration
//                Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
//                Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                Ladbrokes_IMS_TestRepository.Common common = new Ladbrokes_IMS_TestRepository.Common();

//                loginData.update_Login_Data(ReadxmlData("Teldata", "user", DataFilePath.IP2_Authetication),
//            ReadxmlData("Teldata", "pwd", DataFilePath.IP2_Authetication),
//           " ");

//                acctData.Update_deposit_withdraw_Card(ReadxmlData("Teldata", "account_pwd", DataFilePath.IP2_Authetication),
//                    ReadxmlData("Teldata", "wAmt", DataFilePath.IP2_Authetication),
//                     ReadxmlData("Teldata", "wAmt", DataFilePath.IP2_Authetication),
//                     ReadxmlData("Teldata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("Teldata", "withWallet", DataFilePath.IP2_Authetication), null);

//                #endregion

//                try
//                {

//                    AddTestCase("Withdraw related scenarios in Telebet pages", "Withdraw related function should be successful");

//                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
//                        baseTB2.Init(driverObj);
//                    else
//                        baseTB2.Init();

                  

//                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
//                    WriteCommentToMailer("UserName: " + loginData.username);
//                    AddTestCase("Withdraw check in customer " + loginData.username, ""); Pass();
//                    //commTB2.WithdrawRelated(baseTB2.IMSDriver, acctData, min, max);
//                    baseTB2.IMSDriver.FindElement(By.Id("deposit_tab_list_item")).Click();
//                    System.Threading.Thread.Sleep(3000);
//                    wAction.WaitAndMovetoFrame(baseTB2.IMSDriver, By.Id("acctIframe"));
//                    common.CommonWithdraw_Netteller_PT(baseTB2.IMSDriver, acctData, acctData.depositAmt, false);
//                    wAction.WaitAndMovetoFrame(baseTB2.IMSDriver, By.Id("acctIframe"));
//                    common.CommonCancelWithdraw_Netteller_PT(baseTB2.IMSDriver, acctData, acctData.depositAmt, false, true);

//                    Pass("Withdraw Chk succesfull");

//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);

//                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
//                    Fail("Verify_Withdraw_Limit_Cancel_Telebet - failed");
//                }
//                finally
//                {

//                    baseTB2.Quit();
//                }

//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_Sports_PlaceBet_Ip2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
//                #endregion

//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                // Configuration testdata = TestDataInit();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                #endregion

//                try
//                {
//                    AddTestCase("Verify the sports bets history From My account", "Sports history should display all bet details");

//                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.IP2_Authetication),
//                                            ReadxmlData("vccdata", "pwd", DataFilePath.IP2_Authetication),
//                                            "");
//                    //  excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                    AnW.Login_Logout_Sports(driverObj, loginData);

//                    //tempCode
//                 //  wAction.PageReload(driverObj);

//                    wAction.Click(driverObj, By.LinkText("Close"));

//                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

//                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);
//                    //bet now button should b displayed
//                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "place_bet_button");

//                    //string s = wAction.GetText(driverObj, By.XPath("//div[2]"));
//                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//div[contains(text(),'Your bets have been placed successfully')]")), "Bet not placed");

//                    Pass("Sports history verified successfully");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Verify_Sports_Bet_History - Failed");
//                }

//            }

//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_All_Product_Login_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
//                #endregion

//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                // Configuration testdata = TestDataInit();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                #endregion


//                try
//                {
//                    AddTestCase("Verify login is successfull in Casino", "Login should be successfully");
//                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
//                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                    Pass("casino login successfully");

                    
//                    AddTestCase("Verify login is successfull in Bingo", "Login should be successfully");
//                    //driverObj.Manage().Cookies.DeleteAllCookies();
//                    AnW.LogoutFromPortal(driverObj);
//                    Thread.Sleep(TimeSpan.FromSeconds(5));
//                    wAction.WaitforPageLoad(driverObj);
//                    wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Bingo page not loaded", 60);
//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                    Pass("bingo login successfully");

                    
//                    AddTestCase("Verify login is successfull in Poker", "Login should be successfully");
//                    AnW.LogoutFromPortal(driverObj);
//                    Thread.Sleep(TimeSpan.FromSeconds(5));
//                    wAction.WaitforPageLoad(driverObj);
//                    wAction.OpenURL(driverObj, FrameGlobals.PokerURL, "Poker page not loaded", 60);
//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                    Pass("Poker login successfully");

                    
//                    AddTestCase("Verify login is successfull in Vegas", "Login should be successfully");
//                    AnW.LogoutFromPortal(driverObj);
//                    Thread.Sleep(TimeSpan.FromSeconds(5));
//                    wAction.WaitforPageLoad(driverObj);
//                    wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Vegas page not loaded", 60);
//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                    Pass("Vegas login successfully");



//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "portal");
//                    Fail("Verify_AllPT_Login - failed");
//                }

//            }

//            /// <summary>
//            /// Verify customer is able to add credit/ debit as payment method in the first deposit page
//            /// Naga
//            /// </summary>
//            [Test]
//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Parallelizable]
//            public void Verify_FirstDeposit_CreditCard_IP2_BVT()
//            {
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                MyAcct_Data acctData = new MyAcct_Data();
//                TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
//                AccountAndWallets AnW = new AccountAndWallets();

//                #endregion
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
//                #endregion

//                try
//                {
//                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

//                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
//                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
//                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
//                    // AnW.Registration_PlaytechPages(driverObj, ref regData,0,false, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));

//                    commTest.PP_Registration(driverObj, ref regData);
//                    Pass("Customer registered succesfully");
//                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
//                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
//                    Pass();

//                    //acctData.Update_deposit_withdraw_Card(TD.createCreditCard("Visa").ToString(),
//                    // ReadxmlData("ccdata", "CCAmt", DataFilePath.IP2_Test),
//                    //  ReadxmlData("ccdata", "CCAmt", DataFilePath.IP2_Test),
//                    //  ReadxmlData("ccdata", "depWallet", DataFilePath.IP2_Test), ReadxmlData("ccdata", "depWallet", DataFilePath.IP2_Test)
//                    //, ReadxmlData("ccdata", "CVV", DataFilePath.IP2_Test));

                
//                    BaseTest.AddTestCase("Verify customer is able to add credit/ debit as payment method in the first deposit page", "First deposit should be successful");
//                    //Assert.IsTrue(ip2.Verify_FirstCashier_Visa(driverObj, acctData), "First Deposit amount not added to the wallet");
//                    Assert.IsTrue(ip2.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
//                             ReadxmlData("netdata", "VegasWallet", DataFilePath.IP2_Authetication),
//                         ReadxmlData("netdata", "dAmt", DataFilePath.IP2_Authetication)), "First Deposit amount not added to the wallet");
//                    BaseTest.Pass("successfully verified");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Verify_FirstDeposit_CreditCard - failed");
//                }

//            }

//            [RepeatOnFail]
//            [Timeout(1000)]
//            [Test]
//            //  [Parallelizable]
//            public void Verify_realPlay_Cust_Registration_Vegas_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
//                #endregion
//                Registration_Data regData = new Registration_Data();
//                // Configuration testdata = TestDataInit();

//                AddTestCase("Verify the customer registration or login from Real Play window.", "Should allow the customer to register or login from Play for Real Money option.");
//                try
//                {
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                    AnW.OpenRealPlay(driverObj);
//                    System.Threading.Thread.Sleep(2000);

//                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "reg_invalid_Prpmt_lgn", "Registration button not found in login pop up", 0, false);
//                    //commonWebMethods.Click(driverObj, By.XPath("//a[@title='Registration']"), "Registration prompt not found", FrameGlobals.reloadTimeOut, false);
//                    // vegasPortal.Registration(driverObj, ref regData);
//                    AnW.Registration_PlaytechPages(driverObj, ref regData);
//                    Pass("Customer has logged in successfully");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(MyBrowser, "portal");
//                    Fail("Verify_realPlay_Cust_Registration_Vegas has failed");
//                }
//            }

//            [Timeout(2200)]
//            [RepeatOnFail]
//            [Test]
//            [Parallelizable]
//            public void Verify_MyAcct_Contact_Preferences_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser,FrameGlobals.VegasURL);
//                #endregion

//                #region Declaration
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
//                AdminSuite.Common adminComm = new AdminSuite.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                // Configuration testdata = TestDataInit();
//                Registration_Data regData = new Registration_Data();
//                #endregion

//                AddTestCase("Verify that contact preferences edited in portal is updated in IMS.", "Contact preferences should be updated successfully");
//                try
//                {
//                    //   AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                   
//                    #region NewCustTestData
//                    Testcomm.Createcustomer_PostMethod(ref regData);
//                    loginData.username = regData.username;
//                    loginData.password = regData.password;
//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                    #endregion

//                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

//                    try
//                    {
//                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
//                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
//                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
//                    }
//                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

//                    AnW.MyAccount_ModifyDetails(driverObj);
//                    AnW.MyAccount_Edit_ContactPref_DirectMail(driverObj);

//                    baseIMS.Init();
//                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
//                    BaseTest.AddTestCase("Verify contact preference updated in IMS", "Contact preferences should be updated");
//                    wAction.Click(baseIMS.IMSDriver, By.Id("imgsec_contact"), "Contact preference link not found");
//                    System.Threading.Thread.Sleep(2000);
//                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(baseIMS.IMSDriver, By.XPath("//td[@id='contact_preferences']//input[not(@checked) and @id='communicationoptouts[3][2]']")), "Contact preferences is not updated in IMS");
//                    BaseTest.Pass();
//                    Pass();

//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    Fail("Country check for UK customer failed for exception:" + e.Message);
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                }
//            }


//            [Timeout(2200)]
//            [RepeatOnFail]
//            [Test]
//            public void Verify_MyAcct_DepositLimit_Responsible_Gambling_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                #endregion

//                #region Declaration


//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                // Configuration testdata = TestDataInit();
//                Registration_Data regData = new Registration_Data();
//                #endregion

//                AddTestCase("Modify deposit limits in 'Responsible Gambling' Page", "User should be able to modify the deposit limit details");
//                try
//                {

//                    #region Prerequiste
//                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
//                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
//                    //       AnW.OpenRegistrationPage(driverObj);
//                    //  AnW.Registration_PlaytechPages(driverObj, ref regData);
//                    //  Pass("Customer registered succesfully");
//                    #region NewCustTestData
//                    Testcomm.Createcustomer_PostMethod(ref regData);
//                    loginData.username = regData.username;
//                    loginData.password = regData.password;
//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                    #endregion

//                    #endregion

//                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

//                    try
//                    {
//                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
//                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
//                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
//                    }
//                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

//                    AnW.MyAccount_Responsible_Gambling(driverObj);

//                    baseIMS.Init();
//                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
//                    commIMS.VerifyDepositLimitInIms(baseIMS.IMSDriver, "500");
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
//                    Fail("Responsible gambling validation failed for exception:" + e.Message);
//                }
//                finally
//                {
//                    baseIMS.Quit();
//                }
//            }


//            [RepeatOnFail]
//            [Timeout(1500)]
//            [Test(Order = 15)]
//            public void Verify_Deposit_SingleWallet_IP2_BVT()
//            {
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                #endregion

//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
//                #endregion

//                try
//                {

//                    AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");

//                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
//                        ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));
                        

//                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
//                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
//                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
//                          ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), null);

//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                    AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets));

//                    BaseTest.Pass();
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Deposit bonus scenario failed");
//                }
//            }

//            [RepeatOnFail]
//            [Timeout(1500)]
//            [Test(Order = 16)]
//            // [Parallelizable]
//            public void Verify_Withdraw_SingleWallet_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
//                #endregion

//                #region Declaration


//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                //// Configuration testdata = TestDataInit();
//                Registration_Data regData = new Registration_Data();
//                #endregion

//                AddTestCase("Verify Withdraw is successful", "Withdraw should successful.");

//                try
//                {
//                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

//                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
//                         ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


//                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
//                         ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
//                         ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
//                          ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), null);

//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                    AnW.Withdraw_Netteller(driverObj, acctData);
//                    Pass();
//                    WriteUserName(loginData.username);
//                }

//                catch (Exception e)
//                {

//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Withdraw for netteller is failed for exception");

//                }

            
//            }

//            [RepeatOnFail]
//            [Timeout(1500)]
//            [Test(Order = 17)]
//            public void Verify_Deposit_Games_IP2_BVT()
//            {
//                #region Declaration
//                Registration_Data regData = new Registration_Data();
//                IMS_Base baseIMS = new IMS_Base();
//                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                #endregion

//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
//                #endregion

//                try
//                {

//                    AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");

//                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
//                        ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


//                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
//                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
//                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
//                          ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), null);

//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                    AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets));

//                    BaseTest.Pass();
//                }
//                catch (Exception e)
//                {
//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Deposit bonus scenario failed");
//                }
//            }

//            [RepeatOnFail]
//            [Timeout(1500)]
//            [Test(Order = 18)]
//            // [Parallelizable]
//            public void Verify_Withdraw_Games_IP2_BVT()
//            {
//                #region DriverInitiation
//                IWebDriver driverObj;
//                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
//                #endregion

//                #region Declaration


//                Login_Data loginData = new Login_Data();
//                MyAcct_Data acctData = new MyAcct_Data();
//                //// Configuration testdata = TestDataInit();
//                Registration_Data regData = new Registration_Data();
//                #endregion

//                AddTestCase("Verify Withdraw is successful", "Withdraw should successful.");

//                try
//                {
//                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

//                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
//                         ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


//                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
//                       ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
//                       ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
//                        ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), null);

//                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                    AnW.Withdraw_Netteller(driverObj, acctData);
//                    Pass();
//                    WriteUserName(loginData.username);
//                }

//                catch (Exception e)
//                {

//                    exceptionStack(e);
//                    CaptureScreenshot(driverObj, "Portal");
//                    Fail("Withdraw for netteller is failed for exception");

//                }


//            }


//               [Test(Order = 19)]
//               [RepeatOnFail]
//               [Timeout(1600)]
//             public void Verify_Transfer_SingleToGames_IP2_BVT()
//               {
//                   #region DriverInitiation
//                   IWebDriver driverObj;
//                   ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                   driverObj = browserInitialize(iBrowser,FrameGlobals.PokerURL);
//                   #endregion

//                   #region Declaration
//                   Login_Data loginData = new Login_Data();
//                   MyAcct_Data acctData = new MyAcct_Data();
//                   // Configuration testdata = TestDataInit();
//                   Registration_Data regData = new Registration_Data();
//                   #endregion

//                   AddTestCase("Verify Transfer is successful", "Transfer should be successful.");
//                   try
//                   {
//                       //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);
//                       loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
//                                            ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


//                       acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
//                            ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
//                            ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
//                             ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), null);

            
//                       WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                       AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                       ip2.Wallet_Transfer_Single(driverObj, acctData);
                      
//                       Pass();
//                   }

//                   catch (Exception e)
//                   {

//                       exceptionStack(e);
//                       CaptureScreenshot(driverObj, "Portal");
//                       Fail("Fund transfer among all wallets is failed for an exception");

//                   }
//               }

//               [Test(Order = 20)]
//               [RepeatOnFail]
//               [Timeout(1600)]
//               public void Verify_Transfer_SingleToExchange_IP2_BVT()
//               {
//                   #region DriverInitiation
//                   IWebDriver driverObj;
//                   ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                   driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
//                   #endregion

//                   #region Declaration
//                   Login_Data loginData = new Login_Data();
//                   MyAcct_Data acctData = new MyAcct_Data();
//                   // Configuration testdata = TestDataInit();
//                   Registration_Data regData = new Registration_Data();
//                   #endregion

//                   AddTestCase("Verify Transfer is successful", "Transfer should be successful.");
//                   try
//                   {
                      
//                       loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
//                                            ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


//                       acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
//                            ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
//                            ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
//                             ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "eXWallet", DataFilePath.IP2_Authetication), null);


//                       WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                       AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                       ip2.Wallet_Transfer_Single(driverObj, acctData);

//                       Pass();
//                   }

//                   catch (Exception e)
//                   {

//                       exceptionStack(e);
//                       CaptureScreenshot(driverObj, "Portal");
//                       Fail("Fund transfer among all wallets is failed for an exception");

//                   }
//               }
             
            
            
//               /// <summary>
//               ///  Author :Naga
//               ///  Verify if customer is able to deposit more than set deposit limit
//               ///  PNG-163
//               /// </summary>
//               [Test]
//               [RepeatOnFail]
//               [Timeout(1500)]
//               [Parallelizable]
//               public void Verify_DepositLimit_Portal_IP2_BVT()
//               {
//                   #region DriverInitiation
//                   IWebDriver driverObj;
//                   ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                   driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
//                   #endregion

//                   #region Declaration


//                   Login_Data loginData = new Login_Data();
//                   MyAcct_Data acctData = new MyAcct_Data();
//                   // Configuration testdata = TestDataInit();
//                   Registration_Data regData = new Registration_Data();
//                   #endregion

//                   AddTestCase("Verify deposit limit functioning correctly", "Deposit limit is not functioning correctly");
//                   try
//                   {
//                       loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.IP2_Authetication),
//                           ReadxmlData("depLimt", "pwd", DataFilePath.IP2_Authetication));

//                       excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
//                          "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                       AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                       AnW.DepositMax_Netteller(driverObj, ReadxmlData("depLimt", "dAmt", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication));
//                       Pass();
//                   }
//                   catch (Exception e)
//                   {
//                       exceptionStack(e);
//                       CaptureScreenshot(driverObj, "Portal");
//                       Fail("Verify_DepositLimit_Portal is not functioning correctly");
//                   }
//               }

//               [Test]
//               [RepeatOnFail]
//               [Timeout(1500)]
//               [Parallelizable]
//               public void Verify_Netteller_Registration_IP2_BVT()
//               {
//                   #region Declaration
//                   IMS_Base baseIMS = new IMS_Base();
//                   IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                   Login_Data loginData = new Login_Data();
//                   MyAcct_Data acctData = new MyAcct_Data();
//                   //Configuration testdata = TestDataInit();
//                   Registration_Data regData = new Registration_Data();
//                   TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
//                   #endregion
//                   #region DriverInitiation
//                   IWebDriver driverObj = null;
//                   ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                   driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
//                   #endregion

//                   AddTestCase("Validation of Netteller registraion for Euro currency", "User should be able to register Netteller card successfully");
//                   try
//                   {
//                       #region Prerequiste
//                       // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
//                       regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
//                      // regData.currency = ReadxmlData("regdata", "currency_USD", DataFilePath.IP2_Test);



//                       #region NewCustTestData
//                       Testcomm.Createcustomer_PostMethod(ref regData);
//                       loginData.username = regData.username;
//                       loginData.password = regData.password;
//                       AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                       #endregion

//                       //Pass("Customer registered succesfully");

//                       WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
//                       AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
//                       Pass();
//                       #endregion

//                       #region Neteller Registration
//                       String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                      
//                           wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
//                           wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
//                           wAction.WaitAndMovetoPopUPWindow(driverObj, "Banking window not found", FrameGlobals.elementTimeOut);
                      
                       
//                       AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "VegasWallet", DataFilePath.IP2_Authetication));

//                       #endregion

//                       Pass();
//                   }
//                   catch (Exception e)
//                   {
//                       exceptionStack(e);
//                       CaptureScreenshot(driverObj, "Portal");
//                       Fail("Verify_Netteller_Registration for exception");
//                   }
//               }

//               [Test]
//               [Parallelizable]
//               [Timeout(1500)]
//               [RepeatOnFail]
//               public void Verify_LiveDealer_WithdrawalMoreThanBalance_IP2_BVT()
//               {
//                   #region DriverInitiation
//                   IWebDriver driverObj;
//                   ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                   driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
//                   #endregion
//                   #region Declaration
//                   AdminSuite.Common commonAdm = new AdminSuite.Common();
//                   Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
//                   Login_Data loginData = new Login_Data();
//                   MyAcct_Data acctData = new MyAcct_Data();

//                   Registration_Data regData = new Registration_Data();
//                   #endregion

//                   AddTestCase("Verify the Banking / My Account Links to deposit or Withdraw amount from different wallets and check the deposit limit.", "Amount should be deposited to the selected wallet and should not allow to deposit more than deposit limit and Should allow to withdraw amount and withdrawn amount should be updated in the selected wallet.");

//                   try
//                   {
//                       loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.IP2_Authetication),
//                          ReadxmlData("depLimt", "pwd", DataFilePath.IP2_Authetication));
                       
//                       WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
//                       AnW.LoginFromPortal(driverObj, loginData,iBrowser);
//                       string balAmount = cmn.HomePage_Balance(driverObj);
//                       cmn.EnterAmount_ToWithdrawMoreThanBalance(driverObj, ReadxmlData("netdata", "VegasWallet", DataFilePath.IP2_Authetication));
//                   }
//                   catch (Exception e)
//                   {
//                       exceptionStack(e);
//                       CaptureScreenshot(driverObj, "Portal");
//                       Fail("Verify_LiveDealer_WithdrawalMoreThanBalance failed");

//                   }
//               }


//               /// <summary>
//               ///  Author :Naga
//               ///  GEN-5411
//               ///  Verify the wallet transfer functionality across wallets to Verify transfer amount lesser than non withdrawable amount 
//               /// </summary>
//               [Timeout(2200)]
//               [RepeatOnFail]
//               [Test]
//               [Parallelizable]
//               public void Verify_Transfer_Insufficient_Fund_IP2_BVT()
//               {
//                   #region DriverInitiation
//                   IWebDriver driverObj;
//                   ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                   driverObj = browserInitialize(iBrowser,FrameGlobals.PokerURL);
//                   #endregion

//                   #region Declaration
//                   Login_Data loginData = new Login_Data();
//                   MyAcct_Data acctData = new MyAcct_Data();
//                   Registration_Data regData = new Registration_Data();

//                   #endregion
//                   AddTestCase("Verify the wallet transfer functionality across wallets to Verify transfer amount lesser than non withdrawable amount ", "Insufficient fund error should be displayed");
//                   try
//                   {
//                       loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.IP2_Authetication),
//                  ReadxmlData("depLimt", "pwd", DataFilePath.IP2_Authetication));
                       
           

//                       acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
//                            ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
//                            ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
//                             ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), null);

//                       WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

//                       AnW.LoginFromPortal(driverObj, loginData,iBrowser);

//                       AnW.Wallet_Transfer_Balance_Insuff(driverObj, acctData, ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication),
//                           ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication));

//                       Pass();

//                   }
//                   catch (Exception e)
//                   {

//                       exceptionStack(e);
//                       CaptureScreenshot(driverObj, "Portal");
//                       Fail("Verify_Transfer_Insufficient_Fund failed");

//                   }

//               }

//               [Test]
//               [RepeatOnFail]
//               [Timeout(1000)]
//               [Parallelizable]
//               public void Verify_Max_MinPassword_IP2_BVT()
//               {
//                   string regUrl = ReadxmlData("regUrl", "vegas_reg", DataFilePath.IP2_Authetication);
//                   #region DriverInitiation
//                   IWebDriver driverObj;
//                   ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
//                   driverObj = browserInitialize(iBrowser, regUrl );
//                   #endregion
//                   #region Declaration
//                   Registration_Data regData = new Registration_Data();

//                   IMS_Base baseIMS = new IMS_Base();
//                   IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
//                   Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
//                   AdminTests Ob = new AdminTests();
//                   AdminSuite.Common comAdmin = new AdminSuite.Common();
//                   #endregion

//                   try
//                   {
//                       AddTestCase("Create customer from Playtech pages", "Customer should be created.");
//                       regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                     
//                       regData.password = "Lbr12345";


//                       for (int minMax = 0; (regData.password.Length) <= 32; minMax++)
//                       {
//                           BaseTest.AddTestCase("Verifying password length: " + regData.password.Length, "Registration should happen with the password");
//                           commTest.PP_Registration(driverObj, ref regData);
//                           wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));

//                           BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.SelectPayment_Lbl_XP)), "First deposit page not found");
//                           BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//span[contains(text(),'" + regData.username + "')]")), "First deposit page not found");
//                           if (minMax == 10) minMax = 0;
//                           regData.password = regData.password + minMax;
                         
//                           BaseTest.Pass();
//                       }


//                   }
//                   catch(Exception e)
//                   {
//                       exceptionStack(e);
//                       CaptureScreenshot(driverObj, "Portal");
//                       Fail("Verify_Max_MinPassword_IP2_BVT failed");

//                   }

//               }
                
               


//        }//30minBVT class
//    }

