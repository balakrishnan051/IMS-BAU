using System.Linq;
using MbUnit.Framework;
using Framework;
using System.Threading;
using OpenQA.Selenium;
using System.Diagnostics;
using Ladbrokes_IMS_TestRepository;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using AdminSuite;
using IMS_AdminSuite;
using Selenium;
using Gallio;
using System.Xml;
using ICE.ObjectRepository.Vegas_IMS_BAU;
using ICE.DataRepository;
using OpenQA.Selenium.Chrome;
//using ICE.ActionRepository;
using ICE.ObjectRepository;
using ICE.DataRepository.Vegas_IMS_Data;
using System;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

using Ladbrokes_IMS_TestRepository.VirtualScript;

[assembly: ParallelismLimit]
namespace BVT_IP2
{

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Registration : BaseTest
        {
            #region Declaration
          
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            wActions wActions = new wActions();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();            
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IP2Common ip2 = new IP2Common();
            #endregion

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]            
            public void Verify_Casino_Registration_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion
                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com";
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
                    Fail("Verify_RegBtn_Cust_Registration_LiveDealer failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_Customer_Registration_Ip2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
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
                    AddTestCase("Create customer from classic sports", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    wAction.Click(driverObj, By.XPath("id('login')/a[text()='Open account']"), "Open Account button not found", 0, false);
                    regData.email = "@aditi.com";
                    //       AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

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
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_Sports_Customer_Registration failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }
            
            [Test]
            [RepeatOnFail]
            [Timeout(1000)]            
            public void Verify_Games_Registration_Ip2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.GamesURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Games pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                          AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");


                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
                    BaseTest.Pass("Customer Details verified successfully in IMS");

                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
                    Ob.Init();
                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
                    commTest.VerifyCustDetailsInOB(Ob.MyBrowser, regData);
                    BaseTest.Pass("Customer Details verified successfully in OB");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_RegBtn_Cust_Registration_Games failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_CustomerRegistration_Telebet_Ip2_BVT()
            {

                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                string temp = Registration_Data.depLimit;
                #endregion
                #region DriverInitiation
                IWebDriver driverObj = new FirefoxDriver();
                wAction.OpenURL(driverObj, ReadxmlData("Url", "telebet_Reg_url", DataFilePath.IP2_Authetication), "Telebet page did not open");
                #endregion


                try
                {
                    AddTestCase("Create customer from Telebet pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    //   baseIMS.Init();

                    // commIMS.CreateNewCustomer_Telebet(baseIMS.IMSDriver, ref regData);
                    
                    Registration_Data.depLimit = "5000";
                    commIMS.TeleBet_Registration(driverObj, ref regData);
                    Registration_Data.depLimit = temp;

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    BaseTest.AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    BaseTest.Pass();

                    adminBase.Init();
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    adminBase.Quit();

                    Pass("Customer registered succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "Telebet");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Create customer from IMS pages- failed");
                }
                finally
                {
                    //baseIMS.Quit();
                    adminBase.Cleanup();
                }
            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]            
            public void Verify_FirstDeposit_IP2_BVT()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                MyAcct_Data acctData = new MyAcct_Data();
                TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
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
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                   
                    commTest.PP_Registration(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    

                    BaseTest.AddTestCase("Verify customer is able to add credit/ debit as payment method in the first deposit page", "First deposit should be successful");
                    
                    Assert.IsTrue(ip2.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                             ReadxmlData("netdata", "VegasWallet", DataFilePath.IP2_Authetication),
                         ReadxmlData("netdata", "dAmt", DataFilePath.IP2_Authetication)), "First Deposit amount not added to the wallet");
                    BaseTest.Pass("successfully verified");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_FirstDeposit_CreditCard - failed");
                }

            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Ecom_Customer_Registration_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
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
                    AddTestCase("Create customer from Ecom sports", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Ecom_Btn", Keys.Enter, "Join button not found", 0, false);

                    //       AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                    commTest.VerifyCustDetailinIMS_newLook(baseIMS.IMSDriver, regData);
                    BaseTest.Pass("Customer Details verified successfully in IMS");

                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
                    Ob.Init();
                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
                    commTest.VerifyCustDetailsInOB(Ob.MyBrowser, regData);
                    BaseTest.Pass("Customer Details verified successfully in OB");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_Ecom_Sports_Customer_Registration failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

        }//Reg class

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class BVT_SportsBet : BaseTest
        {
            #region Declaration
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            wActions wActions = new wActions();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IP2Common ip2 = new IP2Common();
            #endregion
            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_Registration_AfterPlaceBet()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

                #endregion

                try
                {
                    AddTestCase("Customer clicks on any selection to place a bet on and do a successful registration", "Customer able to register successfully after adding selection to betslip.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    Assert.IsTrue(commTest.AddRandomEvent_Bestlip_sports(driverObj), "Selection not added to betslip");
                    wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "single_stake_box", "1", "Stake box not found", 0, false);
                    wAction.WaitforPageLoad(driverObj);
                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "check_btn", "Check button not found inside betslip", 0, false);
                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "openAccount_betslip_btn", "Open account button not found inside betslip", 0, false);

                    AnW.Registration_PlaytechPages(driverObj, ref regData);

                    Pass("Customer registered succesfully after adding a selection to Betslip");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_Registration_AfterPlaceBet failed");
                }

            }
            
            [RepeatOnFail]
            [Timeout(2200)]
            [Test]
            public void Verify_Sports_PlaceBet_History_Ip2_BVT()
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
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Verify the Sports bet history under Account history ", "Bet history should appear in my accounts page.");



                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.IP2_Authetication),
                                           ReadxmlData("vccdata", "pwd", DataFilePath.IP2_Authetication),
                                           "");
                    //  excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);
                    // //div[@id='betslipSingle0']//div[contains(string(),'Event')]/following-sibling::div[@class='rightcol']

                    string eventName = wAction._GetText(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "eventName_checkBetPage", "Event name not found", false);
                    //bet now button should b displayed
                    AnW.placeBet(driverObj); 

                    wAction.Click(driverObj, By.LinkText("My Account"), "My Account link not found", FrameGlobals.reloadTimeOut, false);
                    wAction.WaitAndMovetoPopUPWindow(driverObj, "My Account window not found", FrameGlobals.elementTimeOut);

                    AnW.Verify_Sports_BetHistory(driverObj, eventName);
                    Pass("Sports history verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SportsBet_History scenario failed");
                }

            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test]
            public void Verify_Sports_QuickDep_Ip2_BVT()
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
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Verify the Sports bet history under Account history ", "Bet history should appear in my accounts page.");



                    loginData.update_Login_Data(ReadxmlData("qbetdata", "user", DataFilePath.IP2_Authetication),
                                           ReadxmlData("qbetdata", "pwd", DataFilePath.IP2_Authetication));

                    string amount = ReadxmlData("qbetdata", "dAmt", DataFilePath.IP2_Authetication);
                    //  excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj,amount);
                    // //div[@id='betslipSingle0']//div[contains(string(),'Event')]/following-sibling::div[@class='rightcol']

                    string eventName = wAction._GetText(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "eventName_checkBetPage", "Event name not found", false);
                    //bet now button should b displayed
                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "place_bet_button");
                    wAction.WaitUntilElementDisappears(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait));


                    ip2.quickDep_Sports(driverObj, ReadxmlData("qbetdata", "dAmt", DataFilePath.IP2_Authetication));

                    Pass("Sports history verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_QuickDep_Ip2_BVT scenario failed");
                }

            }

        }
        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class IMSAdmin : BaseTest
        {
            #region Declaration
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            wActions wActions = new wActions();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IP2Common ip2 = new IP2Common();
            #endregion

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Registration_DebitCustomer_IMS_Ip2_BVT()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();
                    //  regData.username = "testCDTIP1";
                    regData.email = "@aditi.com";
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, null, null, regData.email);
                    //Pass();
                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();

                    adminBase.Init();
                    // Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    adminBase.Quit();

                    Pass("Credit limit set successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Create customer from IMS pages- failed");
                }
                finally
                {
                    baseIMS.Quit();
                    adminBase.Cleanup();
                }
            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Registration_Germany_IMS_Ip2_BVT()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();
                    //  regData.username = "testCDTIP1";
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Germany");
                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);
                    // Pass();
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();

                    adminBase.Init();
                    // Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    //adminComm.ManualAdjustment("100", adminBase.MyBrowser);                    
                    adminBase.Quit();

                    Pass("Credit limit set successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Create customer from IMS pages- failed");
                }
                finally
                {
                    baseIMS.Quit();
                    adminBase.Cleanup();
                }
            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Registration_Of_CreditCustomer_IMS_Ip2_BVT()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();
                    //  regData.username = "testCDTIP1";
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit");
                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    adminBase.Init();
                    //Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    //adminComm.ManualAdjustment("100", adminBase.MyBrowser);
                    //adminComm.SetCreditLimit(adminBase.MyBrowser);
                    adminBase.Quit();

                    Pass("Credit limit set successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Create customer from IMS pages- failed");
                }
                finally
                {
                    baseIMS.Quit();
                    adminBase.Cleanup();
                }
            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Freeze_UnFreeze_CustomerStatus_Ip2_BVT()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                AddTestCase("Verify the UnFreezed user in IMS is in Active state in OB", "UnFreezed user should be in Active status");
                try
                {

                    baseIMS.Init();

         
                    loginData.password = ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication);
                    loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, loginData.password);
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
                    BaseTest.Pass();
                    BaseTest.Pass("Customer created successfully in IMS");
                    commIMS.FreezeTheCustomer(baseIMS.IMSDriver);

                    AnW.LoginFromPortal_FrozenCust(driverObj, loginData,"Frozen customer error mismatch/error did not appear");

                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.DisableFreeze(baseIMS.IMSDriver);
                    baseIMS.Quit();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    wAction.PageReload(driverObj);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Freeze_UnFreeze_CustomerStatus_Ip2_BVT failed for an exception");
                }
                finally
                {
                    
                    baseIMS.Quit();
                }

            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_DebitCustomer_Ip2_BVT()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data lngData = new Login_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                try
                {
                    AddTestCase("Verify setting selfexclusion for a debit customer in OB", "Credit limit should be set successfully");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();
                    lngData.username = regData.username;
                    lngData.password = regData.password;

                    AnW.LoginFromPortal_SelfExCust(driverObj, lngData,"Self excluded customer is logging in");

                    adminBase.Init();
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(adminBase.MyBrowser, By.XPath("//b[text()='PT_SELFEX']")), "Self exclusion is not synced with OB");
                    adminBase.Quit();
                    WriteUserName(regData.username);
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Verify_SelfExcl_DebitCustomer_Ip2_BVT- failed");
                }
                finally
                {
                    baseIMS.Quit();
                    adminBase.Cleanup();
                }
            }
        }//IMSAdmin class


        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class BVT_Login : BaseTest
        {
            #region Declaration
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            wActions wActions = new wActions();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IP2Common ip2 = new IP2Common();
            #endregion

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_PT_Login_Logout_IP2_BVT()
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
                    AddTestCase("Verify login is successfull in Casino", "Login should be successfully");
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.LogoutFromPortal(driverObj);
                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_Casino_Login_IP2_BVT - failed");
                }

            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Sports_Login_IP2_BVT()
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
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion


                try
                {
                    AddTestCase("Verify login is successfull in Sports", "Login should be successfully");
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.Login_Sports(driverObj, loginData);
                    
                    Pass("Sports login successfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_Sports_Login_IP2_BVT - failed");
                }

            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Ecom_Login_Logout()
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
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                AddTestCase("Verify login is successfull in Ecom", "Login should be successfully");
                try
                {
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                      ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);



                    AnW.LoginFromEcom(driverObj, loginData);
                    AnW.LogoutFromPortal_Ecom(driverObj);
                    Pass("Ecom login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_Ecomm_Login - failed");
                }

            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Games_Login_Logout()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.GamesURL);
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

                AddTestCase("Verify login is successfull in games", "Login should be successfully");
                try
                {
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                      ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    ip2.LoginGames(driverObj, loginData);
                    //   AnW.LoginFromEcom(driverObj, loginData);
                    ip2.Logout_Games(driverObj);
                    Pass("games login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_Games_Login_Logout - failed");
                }

            }

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SeemlessLogin_BVT()
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
                    AddTestCase("Verify seemless login is successfull in all product", "Login should be successfully");
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    AddTestCase("Verify seemless login for Bingo", "Login should be successfully");
                     wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Unable to load bingo url", 45);
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)), "Customer not logged in when navigating to Bingo");
                    Pass();

                    AddTestCase("Verify seemless login for Sports", "Login should be successfully");
                    wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Unable to load sports url", 45);
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath("id('fmLogin')//a[text()='Log out']")), "Customer not logged in when navigating to Sports");
                    Pass();

                    AddTestCase("Verify seemless login for Games", "Login should be successfully");
                    wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "Unable to load Games url", 45);
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.LinkText("Logout")), "Customer not logged in when navigating to Games");
                    Pass();

                    AddTestCase("Verify seemless login for Ecom", "Login should be successfully");
                    wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "Unable to load Games url", 45);
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.LinkText("Logout")), "Customer not logged in when navigating to Ecom");
                    Pass();


                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_SeemlessLogin_BVT - failed");
                }

            }

        }//login class

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class BVT_Cashier : BaseTest
        {
            #region Declaration
            SeamLessWallet sw = new SeamLessWallet();
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            wActions wActions = new wActions();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IP2Common ip2 = new IP2Common();
            #endregion

            [RepeatOnFail]
            [Timeout(1500)]
            [Test]
            public void Verify_Deposit_SingleWallet_IP2_BVT()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion

                try
                {

                    AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
                        ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
                          ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication));

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
            [Timeout(1500)]
            [Test]
            public void Verify_Withdraw_SingleWallet_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
                          ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData);
                    Pass();
                    WriteUserName(loginData.username);
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Withdraw for netteller is failed for exception");

                }


            }

            [RepeatOnFail]
            [Timeout(1500)]
            [Test]
            public void Verify_Deposit_Games_IP2_BVT()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion

                try
                {

                    AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
                        ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
                          ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication));

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
            [Timeout(1500)]
            [Test]
            public void Verify_Withdraw_Games_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                       ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
                       ReadxmlData("depdata", "dAmt", DataFilePath.IP2_Authetication),
                        ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData);
                    Pass();
                    WriteUserName(loginData.username);
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Withdraw for netteller is failed for exception");

                }


            }

            [Test]
            [RepeatOnFail]
            [Timeout(1500)]
             public void Verify_NonCard_Registration_BVT()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj = null;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                AddTestCase("Validation of Netteller registraion", "User should be able to register Netteller card successfully");
                try
                {

                    #region Prerequiste
                    AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion
                    #endregion
                  
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                  

                    #region Neteller Registration
                    String portalWindow = AnW.OpenCashier(driverObj);
                    AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "VegasWallet", DataFilePath.IP2_Authetication));
                    driverObj.Close();
                    driverObj.SwitchTo().Window(portalWindow);
                    Pass();
                    #endregion
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Netteller_Registration for exception");
                }

                
            }

            [RepeatOnFail]
            [Test]
            [Parallelizable]
            public void Verify_Transfer_Insufficient_Fund_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion

                #region Declaration
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Registration_Data regData = new Registration_Data();

                #endregion
                AddTestCase("Verify the wallet transfer functionality across wallets to Verify transfer amount lesser than non withdrawable amount ", "Insufficient fund error should be displayed");
                try
                {
                    loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.IP2_Authetication),
               ReadxmlData("depLimt", "pwd", DataFilePath.IP2_Authetication));



                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
                          ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    AnW.Wallet_Transfer_Balance_Insuff(driverObj, acctData, ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication),
                        ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication));

                    Pass();

                }
                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Transfer_Insufficient_Fund failed");

                }

            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            public void Verify_Deposit_VisaCard_Ip2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                AddTestCase("Verify the Banking Links to deposit to Gaming wallet using credit card", "Amount should be deposited to the selected wallet");
                MyAcct_Data acctData = new MyAcct_Data();

                try
                {
                    Login_Data loginData = new Login_Data();

                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.IP2_Authetication),
                  ReadxmlData("vccdata", "pwd", DataFilePath.IP2_Authetication));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "card", DataFilePath.IP2_Authetication),
                        ReadxmlData("vccdata", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("vccdata", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("vccdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("vccdata", "depWallet", DataFilePath.IP2_Authetication),
                          ReadxmlData("vccdata", "CVV", DataFilePath.IP2_Authetication));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    AnW.DepositTOWallet_CC(driverObj, acctData);

                    WriteUserName(loginData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Deposit has failed");
                    //Pass();
                }
            }
        }//cashier

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class BVT_Limit : BaseTest
        {
            #region Declaration
            SeamLessWallet sw = new SeamLessWallet();
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            wActions wActions = new wActions();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IP2Common ip2 = new IP2Common();
            #endregion


            [Test]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_DepositLimit_Portal_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
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
                    loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.IP2_Authetication),
                        ReadxmlData("depLimt", "pwd", DataFilePath.IP2_Authetication));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.DepositMax_Netteller(driverObj, ReadxmlData("depLimt", "dAmt", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication));
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_DepositLimit_Portal is not functioning correctly");
                }
            }


            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_LimitCheck_Registration_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion
                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    
                    Pass("Customer registered succesfully");
                    wAction.BrowserQuit(driverObj);

                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commTest.VerifyDepLimit_newLook(baseIMS.IMSDriver);
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                    baseIMS.Quit();

                   
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_RegBtn_Cust_Registration_LiveDealer failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            [Test]
            [Timeout(1000)]
            [RepeatOnFail]       
            public void Verify_MyAcct_DepositLimit_Responsible_Gambling_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                #endregion

                #region Declaration
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Modify deposit limits in 'Responsible Gambling' Page", "User should be able to modify the deposit limit details");
                try
                {

                    #region Prerequiste
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion

                    #endregion

                    String portalWindow =     AnW.OpenMyAcct(driverObj);
                    AnW.MyAccount_Responsible_Gambling(driverObj);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.VerifyDepositLimitInIms(baseIMS.IMSDriver, "500");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Responsible gambling validation failed for exception:" + e.Message);
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test]
            [Timeout(1000)]
            [RepeatOnFail]
            public void Verify_LiveDealer_WithdrawalMoreThanBalance_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                #region Declaration
                AdminSuite.Common commonAdm = new AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();

                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify the Banking / My Account Links to deposit or Withdraw amount from different wallets and check the deposit limit.", "Amount should be deposited to the selected wallet and should not allow to deposit more than deposit limit and Should allow to withdraw amount and withdrawn amount should be updated in the selected wallet.");

                try
                {
                    loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.IP2_Authetication), ReadxmlData("depLimt", "pwd", DataFilePath.IP2_Authetication));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    string balAmount = cmn.HomePage_Balance(driverObj);
                    cmn.EnterAmount_ToWithdrawMoreThanBalance(driverObj, ReadxmlData("depLimt", "depWallet", DataFilePath.IP2_Authetication));
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_LiveDealer_WithdrawalMoreThanBalance failed");

                }
            }

            [Timeout(2200)]
            [RepeatOnFail]
            [Test]
            public void Verify_MyAcct_Contact_Preferences_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify that contact preferences edited in portal is updated in IMS.", "Contact preferences should be updated successfully");
                try
                {
                    //   AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    //  wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    //  AnW.Registration_PlaytechPages(driverObj, ref regData);
                    //    Pass("Customer registered succesfully");
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion

                    String portalWindow =    AnW.OpenMyAcct(driverObj);
                    Dictionary<string, string> modDetails = new Dictionary<string, string>();
                    AnW.MyAccount_ModifyDetails(driverObj, ref modDetails);
                    AnW.MyAccount_Edit_ContactPref_DirectMail(driverObj);
                    driverObj.Quit();


                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                    Testcomm.VerifyCustDetailinIMS_Modified(baseIMS.IMSDriver, modDetails);



                    BaseTest.AddTestCase("Verify contact preference updated in IMS", "Contact preferences should be updated");
                    wAction.Click(baseIMS.IMSDriver, By.Id("imgsec_contact"), "Contact preference link not found");
                    System.Threading.Thread.Sleep(2000);
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(baseIMS.IMSDriver, By.XPath(IMS_Control_PlayerDetails.DirectMailValidation_Checkbox)), "Contact preferences is not updated in IMS");
                    baseIMS.Quit();
                    BaseTest.Pass();

                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
                    adminBase.Init();
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    Testcomm.VerifyCustDetailsInOB_Modified(adminBase.MyBrowser, modDetails);
                    BaseTest.Pass("Customer Details verified successfully in OB");

                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Country check for UK customer failed for exception:" + e.Message);
                }
                finally
                {
                    baseIMS.Quit();
                    adminBase.Cleanup();
                }
            }

         
        }//limit

        [TestFixture, Timeout(15000)]
        public class Games : BaseTest
        {
            #region Declaration
            Virtual_AnW virtual_AnW = new Virtual_AnW();
            
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            wActions wActions = new wActions();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IP2Common ip2 = new IP2Common();
            #endregion

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            public void Verify_realPlay_Cust_Registration_Vegas_IP2_BVT()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                Registration_Data regData = new Registration_Data();


                AddTestCase("Verify the customer registration or login from Real Play window.", "Should allow the customer to register or login from Play for Real Money option.");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    virtual_AnW.RealPlayCust(driverObj, regData); 
                    Pass("Customer has logged in successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Verify_realPlay_Cust_Registration_Vegas has failed");
                }
            }
        }//games

        [TestFixture, Timeout(15000)]
        public class BVT_Telebet : BaseTest
        {
            #region Declaration
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            wActions wActions = new wActions();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IP2Common ip2 = new IP2Common();
            #endregion

            [Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SearchCustomer_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Deposit from Telebet pages", "Deposit should be successful");
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication));


                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();

                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Searched Customer in Telebet:  " + loginData.username, ""); Pass();
                    Pass("Fund deposited succesfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_SearchCustomer_Telebet - failed");
                }
                finally
                {
                    baseTB2.Quit();
                }
            }

        }//telebet
    }

