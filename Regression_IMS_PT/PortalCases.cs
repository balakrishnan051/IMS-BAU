using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using Framework;
using System.Threading;
using OpenQA.Selenium;
using Ladbrokes_IMS_TestRepository;
using AdminSuite;
using IMS_AdminSuite;
using Selenium;
using ICE.DataRepository;
//using ICE.ActionRepository;
using ICE.ObjectRepository;
using ICE.DataRepository.Vegas_IMS_Data;
using OpenQA.Selenium.Firefox;


namespace Regression_Suite_Portal
{

    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class IMS_Poker : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
        IP2Common ip2 = new IP2Common();
        Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
        Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();


        [RepeatOnFail]
        [Timeout(1000)]
        [Test(Order = 1)]
        public void Verify_Poker_Customer_Systemsource()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
            #endregion

            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminBase baseOB = new AdminBase();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            AdminSuite.Common commOB = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            #endregion

            try
            {
                

                AddTestCase("Create customer from casino", "Customer should be created.");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                AnW.OpenRegistrationPage(driverObj);
                AnW.Registration_PlaytechPages(driverObj, ref regData);
                Pass("Customer registered succesfully");

                BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in OB Admin", "Systemsource and Systemsource ID should be present as expected in OB");
                baseOB.Init();
                commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                commOB.VerifySystemSource("PPK", baseOB.MyBrowser);
                Pass("Verified customer flag in OB");
                BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in the IMS Admin", "Systemsource and Systemsource ID should be present as expected in IMS Admin");
                baseIMS.Init();
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                commTest.VerifyCustSourceDetailinIMS(baseIMS.IMSDriver, "PPK", "PPK_WEB");
                BaseTest.Pass("Customer Details verified successfully in IMS");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(baseOB.MyBrowser, "OB");
                Fail("Verify_Poker_Customer_Systemsource failed");
            }
            finally
            {
                baseIMS.Quit();
                baseOB.Cleanup();
            }
        }

        [Test(Order = 2)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_Poker_Single_Sign()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
            #endregion

            #region Declaration

            bool isPass = true;
            Registration_Data regData = new Registration_Data();
            Login_Data loginData = new Login_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            AdminTests Ob = new AdminTests();
            AdminSuite.Common comAdmin = new AdminSuite.Common();
            #endregion

            try
            {



                AddTestCase("Verify the Poker website for single sign on", "Cashier and my acct pages should be auto logged in");
                loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                             ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets));
                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);


                AnW.OpenMyAcct(driverObj);
                Assert.IsTrue(wAction.GetText(driverObj, By.XPath(MyAcctPage.myAcctHeader_uname_Xp), "My Acct header customer name not found",false).Contains(loginData.username), "My Acct content not signed in / username mismatch :" + loginData.username);
                driverObj.Close(); wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 0, "Main window not found");
                AnW.OpenCashier(driverObj);
               
                Assert.IsTrue(wAction.GetText(driverObj, By.Name(CashierPage.CashierUser_element_name), "Cashier header customer name not found", false).Contains(loginData.username), "Cashier content not signed in / username mismatch :" + loginData.username);

                BaseTest.Pass("Languages supported verified successfully");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Verify_Poker_Single_Sign failed");
            }
        }

        [Test(Order = 3)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_ChangeLanguage_MyAcct_VerifyLinks()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
            #endregion

            #region Declaration
            bool isPass = true;
            Registration_Data regData = new Registration_Data();
            Login_Data loginData = new Login_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            AdminTests Ob = new AdminTests();
            AdminSuite.Common comAdmin = new AdminSuite.Common();
            #endregion

            try
            {
                AddTestCase("Verify the LD website in different languages supported", "LD website should be working fine for different languages");
                loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                             ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets), "");
                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);


                //Change the language
                wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                wAction.Click(driverObj, By.XPath(Portal_Control.Deutsch_XP), "Language Deutsch in menu not found", 0, false);
                AnW.OpenMyAcct(driverObj);
                Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//*[text()='Kontakteinstellungen']")), "My acct content not loaded in german");
                BaseTest.Pass("Languages supported verified successfully");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Verify_ChangeLanguage_MyAcct_VerifyLinks failed");
            }
        }


        [RepeatOnFail]
        [Timeout(1500)]
        [Test(Order = 5)]
        [Parallelizable]
        public void Verify_Deposit_Netteller_Poker()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            MyAcct_Data acctData = new MyAcct_Data();
            #endregion

            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
            #endregion

            try
            {

                AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");

                loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                    ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), null);

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets));

                BaseTest.Pass();
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Deposit bonus scenario failed");
            }
        }
    }
      
      
        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class IMS_Casino : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();          
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Casino_Customer_Registration()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    wAction.BrowserQuit(driverObj);

                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                    baseIMS.Quit();

                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
                    Ob.Init();
                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
                    commTest.VerifyCustDetailsInOB(Ob.MyBrowser, regData);
                    BaseTest.Pass("Customer Details verified successfully in OB");
                    Ob.Cleanup();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_Casino_Customer_Registration failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            [Test(Order = 2)]
            [Timeout(1200)]
            [RepeatOnFail]
            public void Verify_Casino_Cust_RegistrationPage_SourceId()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                

                AddTestCase("Verify the customer registration and source id.", "Should allow the customer to register or login from Play for Real Money option.");
                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_Page_sourceId(driverObj, ref regData, "PCS_WEB");
                    Pass("Customer has logged in successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();

                }
            }
            /// <summary>
            /// Author:Roopa
            /// Verify the Casino website in different languages supported
            /// Date: 17/11/2014
            /// </summary>
            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Casino_Language_Supported()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration

                bool isPass = true;
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    


                    AddTestCase("Verify the Casino website in different languages supported", "Casino website should be working fine for different languages");
                    Thread.Sleep(4000);

                    //Change the language
                    wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Portal_Control.Deutsch_XP), "Language Deutsch in menu not found", 0, false);
                    
                    Thread.Sleep(4000);
                    String failMsg = null;
                    if (!driverObj.Url.Contains("de"))
                    {
                        failMsg = "Casino not loaded properly on selecting Deutsch language";
                        isPass = false;
                    }
                    commonWebMethods.WaitforPageLoad(driverObj);
                    driverObj.Navigate().GoToUrl(FrameGlobals.CasinoURL);
                    commonWebMethods.WaitforPageLoad(driverObj);
                    Thread.Sleep(4000);

                    //Change the language
                    wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Portal_Control.espanol_XP), "Language Espanol in menu not found", 0, false);
                    
                   Thread.Sleep(4000);

                    if (!driverObj.Url.Contains("es"))
                    {
                        failMsg = failMsg + "Casino not loaded properly on selecting Espanol language";
                        isPass = false;
                    }
                    commonWebMethods.WaitforPageLoad(driverObj);
                    driverObj.Navigate().GoToUrl(FrameGlobals.CasinoURL);
                    commonWebMethods.WaitforPageLoad(driverObj);
                    Thread.Sleep(4000);
                    //Change the language
                    
                    wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Portal_Control.svenska_XP), "Language Svenska in menu not found", 0, false);
                    Thread.Sleep(4000);
                    if (!driverObj.Url.Contains("sv"))
                    {
                        failMsg = failMsg + "Casino not loaded properly on selecting Irish language";
                        isPass = false;
                    }

                    Assert.IsTrue(isPass, failMsg);
                    BaseTest.Pass("Casino website in different languages supported verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Casino_Language_Supported failed");
                }
            }


            /// <summary>
            /// Author:Roopa
            /// Launching of PT cashier after login to the portal
            /// Verify the user can deposit funds to Gaming wallet 
            /// </summary>
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Cashier_Deposit_Gaming()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    

                    AddTestCase("Verify Launching of PT cashier after login to the portal", "Launching PT cashier page should be successfull");
                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.Accounts_Wallets),
               ReadxmlData("vccdata", "pwd", DataFilePath.Accounts_Wallets),
               ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);

                    AnW.DepositTOWallet_CC(driverObj, acctData);

                    WriteUserName(loginData.username);


                    Pass();

                }
                
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Cashier_Deposit_Games failed");
                }

            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Casino_Header_VerifyLinks()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration

                bool isPass = true;
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Verify the header information in LD site", "LD website should be working fine for all header details");
                    AnW.Verify_HeaderLink(driverObj);
                    BaseTest.Pass("Casino website in different languages supported verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Casino_Header_VerifyLinks failed");
                }
            }
          


            /// <summary>
            /// Author:Roopa
            /// Verify the sync for Source,Source_ID,Client Type,Channel,Client Platform for the customer registered from Casino 
            /// Date: 17/10/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(1000)]
            [Test(Order = 6)]
            public void Verify_Casino_Customer_Systemsource()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminBase baseOB = new AdminBase();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminSuite.Common commOB = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                #endregion

                try
                {
                   

                    AddTestCase("Create customer from casino", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in OB Admin", "Systemsource and Systemsource ID should be present as expected in OB");
                    baseOB.Init();
                    commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                    commOB.VerifySystemSource("PCS", baseOB.MyBrowser);
                    Pass("Verified customer flag in OB");
                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in the IMS Admin", "Systemsource and Systemsource ID should be present as expected in IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                    commTest.VerifyCustSourceDetailinIMS(baseIMS.IMSDriver,"PCS", "PCS_WEB");
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Verify_Casino_Customer_Systemsource failed");
                }
                finally
                {
                    baseIMS.Quit();
                    baseOB.Cleanup();
                }
            }


            /// <summary>
            /// Author:Roopa
            /// Verify that Comp point  respective  Account &  Balance is displayed in the casino portal after logging in
            /// Date: 21/10/2014
            /// </summary>
            [Test(Order = 7)]
            [RepeatOnFail]
            [Parallelizable]
            public void Verify_Casino_AccountAndBalance()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                //  driverObj = browserInitialize(iBrowser);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();

                #endregion

                AddTestCase("Verfiy the balance info from  the user name dropename", "Home page should be opened successfully.");
                try
                {

                 

                    
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //***//casinoPortal.LoginFromPortal(driverObj, loginData);
                   
                    int bal = 0;

                    Assert.IsTrue(StringCommonMethods.ReadIntegerfromString(AnW.FetchBalance_Portal_Homepage(driverObj)) == bal, "Balance amount not found");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Customer care support scenario failed");
                }
            }


            /// <summary>
            /// Author:Nagamanickam
            /// Verify customer can play a demo game in logged out state.
            /// </summary>
           //    [Test]
            [Timeout(2200)]
            //  [Parallelizable]
            [RepeatOnFail]
            public void Verify_demoPlay_UserLoggedOut()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                //  driverObj = browserInitialize(iBrowser);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                AddTestCase("Verify customer can play a demo game in logged out state.", "Player should be able to play demo game.");
                try
                {

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    Thread.Sleep(20000);
                    AddTestCase("Open Demo Play game", "Game Window should be opened successfully");
                    //***//vegasPortal.OpenDemoPlay(driverObj);

                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Demo Play game window did not open");
                    Pass();
                    Thread.Sleep(60000);
                    Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.LinkText("QUICK DEPOSIT")), "Game page not loaded/Quick Deposit button not found");

                    commonWebMethods.BrowserClose(driverObj);
                    driverObj.SwitchTo().Window(portalWindow);
                    Pass("Player can play demo game");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Player cannot play demo game");
                    Pass();
                }
            }

            [Timeout(2200)]
           // [Test]
            [Parallelizable]
            public void Verify_Casino_Tab_In_All_Tabs()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                //driverObj = browserInitialize(iBrowser);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                AdminTests admin = new AdminTests();
                AdminSuite.Common commonAdm = new AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify all the links to the Casino website will be included in all tab menus", "Casino tab is present in all other tabs");
                try
                {
                    wAction.Click(driverObj, By.XPath(Portal_Control.Vegas_Tab_XP), "Vegas Tab not found", 0, false);
                    wAction.Click(driverObj, By.XPath(Portal_Control.Casino_Tab_XP), "Casino Tab not found",0, false);
                    wAction.Click(driverObj, By.XPath(Portal_Control.Poker_Tab_XP), "Poker Tab not found",0, false);
                    wAction.Click(driverObj, By.XPath(Portal_Control.Casino_Tab_XP), "Casino Tab not found", 0, false);

                    wAction.Click(driverObj, By.XPath(Portal_Control.Bingo_Tab_XP), "Bingo Tab not found", 0, false);
                    wAction.Click(driverObj, By.XPath(Portal_Control.Casino_Tab_XP), "Casino Tab not found", 0, false);
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("URL Navigation failed");
                }
            }

        }

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class IMS_LiveDealer : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_LiveDealer_Customer_Registration()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    wAction.BrowserQuit(driverObj);

                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                    baseIMS.Quit();

                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
                    Ob.Init();
                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
                    commTest.VerifyCustDetailsInOB(Ob.MyBrowser, regData);
                    BaseTest.Pass("Customer Details verified successfully in OB");
                    Ob.Cleanup();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_LiveDealer_Customer_Registration failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

           

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_LiveDealer_Header_VerifyLinks()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                #region Declaration

                bool isPass = true;
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {



                    AddTestCase("Verify the header information in LD site", "LD website should be working fine for all header details");
                    AnW.Verify_HeaderLink(driverObj);
                    BaseTest.Pass("Casino website in different languages supported verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_LiveDealer_Header_VerifyLinks failed");
                }
            }
          
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_LiveDealer_Single_Sign()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                #region Declaration

                bool isPass = true;
                Registration_Data regData = new Registration_Data();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {



                    AddTestCase("Verify the LD website for single sign on", "Cashier and my acct pages should be auto logged in");
                    //Thread.Sleep(4000);
                    //loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                    //           ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets), "");
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                                 ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);


                    AnW.OpenMyAcct(driverObj);
                    Assert.IsTrue(wAction.GetText(driverObj, By.XPath(MyAcctPage.myAcctHeader_uname_Xp), "My Acct header customer name not found",false).Contains(loginData.username), "My Acct content not signed in / username mismatch :" + loginData.username);
                    driverObj.Close(); wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 0, "Main window not found");
                    AnW.OpenCashier(driverObj);
                   
                    Assert.IsTrue(wAction.GetText(driverObj, By.Name(CashierPage.CashierUser_element_name), "Cashier header customer name not found", false).Contains(loginData.username), "Cashier content not signed in / username mismatch :" + loginData.username);

                    BaseTest.Pass("Languages supported verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_ChangeLanguage_MyAcct_VerifyLinks failed");
                }
            }

            [RepeatOnFail]
            [Timeout(1500)]
            [Test(Order = 5)]
            [Parallelizable]
            public void Verify_Deposit_Banking_LD()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                try
                {

                    AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                    AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets));

                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Deposit bonus scenario failed");
                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 19)]
            [Parallelizable]
            public void Verify_LiveDealer_FirstDeposit()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IP2Common ip2 = new IP2Common();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                MyAcct_Data acctData = new MyAcct_Data();
              
                AccountAndWallets AnW = new AccountAndWallets();

                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    AnW.OpenRegistrationPage(driverObj);

                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    BaseTest.AddTestCase("Enter Registration details for customer", "Registration details should be entered and received success msg Successfully");
                    commTest.PP_Registration(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    BaseTest.AddTestCase("Verify customer is able to add credit/ debit as payment method in the first deposit page", "First deposit should be successful");
                    Assert.IsTrue(ip2.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                             ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication)), "First Deposit amount not added to the wallet");

                    BaseTest.Pass("successfully verified");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Sports");
                    Fail("Verify_LiveDealer_FirstDeposit - failed");
                }

            }

        }

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class IMS_Bingo : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            [RepeatOnFail]
            [Timeout(1000)]
            [Test(Order = 1)]
            public void Verify_Bingo_Customer_Systemsource()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminBase baseOB = new AdminBase();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminSuite.Common commOB = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                #endregion

                try
                {


                    AddTestCase("Create customer from Vegas", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in OB Admin", "Systemsource and Systemsource ID should be present as expected in OB");
                    baseOB.Init();
                    commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                    commOB.VerifySystemSource("BNG", baseOB.MyBrowser);
                    Pass("Verified customer flag in OB");
                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in the IMS Admin", "Systemsource and Systemsource ID should be present as expected in IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commTest.VerifyCustSourceDetailinIMS(baseIMS.IMSDriver,"BNG", "BIN_WEB");
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Verify_Bingo_Customer_Systemsource failed");
                }
                finally
                {
                    baseIMS.Quit();
                    baseOB.Cleanup();
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Bingo_Language_Supported()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion

                #region Declaration

                bool isPass = true;
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {



                    AddTestCase("Verify the Vegas website in different languages supported", "Casino website should be working fine for different languages");
                    string failMsg="";
                    //Change the language
                    wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Portal_Control.irish_XP), "Language Irish in menu not found", 0, false);

                    Thread.Sleep(4000);

                    if (!driverObj.Url.Contains("ie"))
                    {
                        failMsg = failMsg + "Vegas not loaded properly on selecting Irish language";
                        isPass = false;
                    }
                    commonWebMethods.WaitforPageLoad(driverObj);
                    driverObj.Navigate().GoToUrl(FrameGlobals.BingoURL);
                    commonWebMethods.WaitforPageLoad(driverObj);
                    Thread.Sleep(4000);
                    //Change the language

                    wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Portal_Control.svenska_XP), "Language Svenska in menu not found", 0, false);
                    Thread.Sleep(4000);
                    if (!driverObj.Url.Contains("sv"))
                    {
                        failMsg = failMsg + "Bingo not loaded properly on selecting Irish language";
                        isPass = false;
                    }

                    Assert.IsTrue(isPass, failMsg);
                    BaseTest.Pass("Bingo website in different languages supported verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Bingo_Language_Supported failed");
                }
            }

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Bingo_Customer_Registration()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    wAction.BrowserQuit(driverObj);

                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                    baseIMS.Quit();

                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
                    Ob.Init();
                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
                    commTest.VerifyCustDetailsInOB(Ob.MyBrowser, regData);
                    BaseTest.Pass("Customer Details verified successfully in OB");
                    Ob.Cleanup();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_Bingo_Customer_Registration failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

        }

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class IMS_Vegas : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            [RepeatOnFail]
            [Timeout(1000)]
            [Test(Order = 1)]
            public void Verify_Vegas_Customer_Systemsource()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminBase baseOB = new AdminBase();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminSuite.Common commOB = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                #endregion

                try
                {


                    AddTestCase("Create customer from Vegas", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in OB Admin", "Systemsource and Systemsource ID should be present as expected in OB");
                    baseOB.Init();
                    commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                    commOB.VerifySystemSource("VGS", baseOB.MyBrowser);
                    Pass("Verified customer flag in OB");
                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in the IMS Admin", "Systemsource and Systemsource ID should be present as expected in IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commTest.VerifyCustSourceDetailinIMS(baseIMS.IMSDriver,"VGS", "VGS_WEB");
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Verify_Vegas_Customer_Systemsource failed");
                }
                finally
                {
                    baseIMS.Quit();
                    baseOB.Cleanup();
                }
            }

            [Test(Order = 2)]
            [Timeout(1200)]
            [RepeatOnFail]
            public void Verify_Vegas_Cust_RegistrationPage_SourceId()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                Registration_Data regData = new Registration_Data();


                AddTestCase("Verify the customer registration and source id.", "Should allow the customer to register or login from Play for Real Money option.");
                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_Page_sourceId(driverObj, ref regData, "VGS_WEB");
                    Pass("Customer has logged in successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();

                }
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Vegas_Language_Supported()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration

                bool isPass = true;
                Registration_Data regData = new Registration_Data();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {



                    AddTestCase("Verify the Vegas website in different languages supported", "Casino website should be working fine for different languages");
                    Thread.Sleep(4000);
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                              ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);

                    //Change the language
                    wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Portal_Control.Deutsch_XP), "Language Deutsch in menu not found", 0, false);

                    Thread.Sleep(4000);
                    String failMsg = null;
                    if (!driverObj.Url.Contains("de"))
                    {
                        failMsg = "Vegas not loaded properly on selecting Deutsch language";
                        isPass = false;
                    }
                    commonWebMethods.WaitforPageLoad(driverObj);
                    driverObj.Navigate().GoToUrl(FrameGlobals.VegasURL);
                    commonWebMethods.WaitforPageLoad(driverObj);
                    Thread.Sleep(4000);

                    //Change the language
                    wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Portal_Control.espanol_XP), "Language Espanol in menu not found", 0, false);

                    Thread.Sleep(4000);

                    if (!driverObj.Url.Contains("es"))
                    {
                        failMsg = failMsg + "Vegas not loaded properly on selecting Espanol language";
                        isPass = false;
                    }
                    commonWebMethods.WaitforPageLoad(driverObj);
                    driverObj.Navigate().GoToUrl(FrameGlobals.VegasURL);
                    commonWebMethods.WaitforPageLoad(driverObj);
                    Thread.Sleep(4000);
                    //Change the language

                    wAction.ClickAndMove(driverObj, By.XPath(Portal_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Portal_Control.svenska_XP), "Language Svenska in menu not found", 0, false);
                    Thread.Sleep(4000);
                    if (!driverObj.Url.Contains("sv"))
                    {
                        failMsg = failMsg + "Vegas not loaded properly on selecting Irish language";
                        isPass = false;
                    }

                    Assert.IsTrue(isPass, failMsg);
                    BaseTest.Pass("Casino website in different languages supported verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Vegas_Language_Supported failed");
                }
            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_Vegas_Login_Logout()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion


                try
                {


                    AddTestCase("Verify login is successfull in Vegas", "Login should be successfully");
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    #region NewCustTestData
                    regData.email = "t@aditi.com"; regData.username = "User";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    #region Login
                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                    #endregion

                    #region Logout
                    AnW.LogoutFromPortal(driverObj);
                    #endregion
                    Pass("Vegas login successfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");

                    Fail("Verify_Vegas_Login_Logout - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }


            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Vegas_Single_Sign()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration

                bool isPass = true;
                Registration_Data regData = new Registration_Data();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {



                    AddTestCase("Verify the vegas website for single sign on", "Cashier and my acct pages should be auto logged in");
                    //Thread.Sleep(4000);
                    //loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                    //           ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets), "");
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                                 ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);


                    AnW.OpenMyAcct(driverObj);
                    Assert.IsTrue(wAction.GetText(driverObj, By.XPath(MyAcctPage.myAcctHeader_uname_Xp), "My Acct header customer name not found",false).Contains(loginData.username), "My Acct content not signed in / username mismatch :" + loginData.username);
                    driverObj.Close(); wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 0, "Main window not found");
                    AnW.OpenCashier(driverObj);
                    
                    Assert.IsTrue(wAction.GetText(driverObj, By.Name(CashierPage.CashierUser_element_name), "Cashier header customer name not found",false).Contains(loginData.username), "Cashier content not signed in / username mismatch :" + loginData.username);

                    BaseTest.Pass("Languages supported verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Vegas_Single_Sign failed");
                }
            }

            [RepeatOnFail]
            [Timeout(1500)]
            [Test(Order = 6)]
            [Parallelizable]
            public void Verify_Deposit_Banking_Vegas()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                try
                {

                    AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                    AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets));

                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Deposit bonus scenario failed");
                }
            }

            [Timeout(2200)]
            [Test]
            [Parallelizable]
            public void Verify_Vegas_Tab_In_All_Tabs()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                //driverObj = browserInitialize(iBrowser);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                AdminTests admin = new AdminTests();
                AdminSuite.Common commonAdm = new AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify all the links to the Vegas website will be included in all tab menus", "Vegas tab is present in all other tabs");
                try
                {
                    wAction.Click(driverObj, By.XPath(Portal_Control.Casino_Tab_XP), "Casino Tab not found", 0, false);
                    wAction.Click(driverObj, By.XPath(Portal_Control.Vegas_Tab_XP), "Vegas Tab not found", 0, false);
                    wAction.Click(driverObj, By.XPath(Portal_Control.Poker_Tab_XP), "Poker Tab not found", 0, false);
                    wAction.Click(driverObj, By.XPath(Portal_Control.Vegas_Tab_XP), "Vegas Tab not found", 0, false);

                    wAction.Click(driverObj, By.XPath(Portal_Control.Bingo_Tab_XP), "Bingo Tab not found", 0, false);
                    wAction.Click(driverObj, By.XPath(Portal_Control.Vegas_Tab_XP), "Vegas Tab not found", 0, false);
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("URL Navigation failed");
                }
            }

        }
    }//RegressionSuite_AnW class
//}//NameSpace
