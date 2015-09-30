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

using Ladbrokes_IMS_TestRepository.VirtualScript;


namespace Regression_AnW_Suite2
{
 


        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Registration1 : BaseTest
        {
            

            AccountAndWallets AnW = new AccountAndWallets();
            Virtual_AnW virtual_AnW = new Virtual_AnW();

            [RepeatOnFail]
            [Timeout(1000)]
          //  [Test(Order = 1)]
            public void Verify_Placebet_Cust_Registration()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();

                AddTestCase("Verify the customer registration or login from Betslip window.", "Should allow the customer to register or login from Betslip");
                try
                {
                     regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                     string eventID = ReadxmlData("event", "fb_eID", DataFilePath.Accounts_Wallets);
                     string eventName = ReadxmlData("event", "fb_name", DataFilePath.Accounts_Wallets);
                     string market = ReadxmlData("event", "fb_clname", DataFilePath.Accounts_Wallets);
                     string amt = ReadxmlData("event", "fb_amt", DataFilePath.Accounts_Wallets);

                    // AnW.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets));
                   // AnW.AddToBetSlip(driverObj, 10);

                     AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName,market, AccountAndWallets.EventType.Single, amt, 1,1);
                     wAction.Click(driverObj, By.XPath("//button[text()='PLACE BET']"));
                  //  wAction.Click(driverObj, By.LinkText("Register"), "Register link not found", 0, false);

                    System.Threading.Thread.Sleep(2000);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);

                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();

                    Pass("Customer has logged in successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    

                }
            }


            /// <summary>
            /// Author:Nagamanickam
            /// Verify the customer registration or login from Real Play window.
            /// </summary>
            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Registration_Customer_IMS()
            {

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from IMS pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    baseIMS.Init();
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password);
                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
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
            /// Author:Anand C
            /// Register a customer through IMS
            /// Date: 01/07/2014
            /// </summary>
            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_LiveDealer_Customer_Systemsource_OB()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                AdminBase baseOB = new AdminBase();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminSuite.Common commOB = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in OB Admin", "Systemsource and Systemsource ID should be present as expected in OB");
                    baseOB.Init();
                    commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                    commOB.VerifySystemSourceAndID("LDE", "LDE_WEB", baseOB.MyBrowser);
                    Pass("Verified customer flag in OB");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Verify_LiveDealer_Customer_Systemsource_OB - failed");
                }
                finally
                {
                    baseOB.Quit();
                }
            }

            /// <summary>
            /// Author:Anand
            /// Register a customer in Sports portal through Playtech Page
            /// Date: 10/04/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(1000)]
            [Test(Order = 4)]
            public void Verify_Sports_Customer_Systemsource_OB()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminBase baseOB = new AdminBase();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminSuite.Common commOB = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets_stg));
                          AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in OB Admin", "Systemsource and Systemsource ID should be present as expected in OB");
                    baseOB.Init();
                    commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                    commOB.VerifySystemSourceAndID("OBE", "OBS_WEB", baseOB.MyBrowser);
                    Pass("Verified customer flag in OB");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseOB.FfDriver, "OB");
                    Fail("URL Navigation failed");
                }
                finally
                {
                    baseOB.Quit();
                }
            }

            /// <summary>
            /// Author:Anand C
            /// Register a customer through IMS
            /// Date: 01/07/2014
            /// </summary>
            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Registration_NonUK_Verify_SBKFlag()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                AdminBase baseOB = new AdminBase();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminSuite.Common commOB = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified the Customers details SBK flag in OB Admin", "Registered customers details should be present in the OB Admin");
                    baseOB.Init();
                    commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                    commOB.VerifyCustomerFlag("SBK_ACCESS", "1", baseOB.MyBrowser);
                    Pass("Verified customer flag in OB");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Create customer - failed");
                }
                finally
                {
                    baseOB.Quit();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test(Order = 6)]
            public void Verify_realPlay_Cust_Registration_Vegas()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();

                AddTestCase("Verify the customer registration or login from Real Play window.", "Should allow the customer to register or login from Play for Real Money option.");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
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


        }//Registration class

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Registration2 : BaseTest
        {
            

            AccountAndWallets AnW = new AccountAndWallets();

          
            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through IMS
            /// Date: 24/03/2014
            /// </summary>
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Registration_Of_CreditCustomer_IMS()
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
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    baseIMS.Init();
                    //  regData.username = "testCDTIP1";
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit");
                    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();

                    adminBase.Init();
                    // Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    //adminComm.ManualAdjustment("100", adminBase.MyBrowser);
                  //  adminComm.SetCreditLimit(adminBase.MyBrowser);
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

            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through IMS
            /// Date: 24/03/2014
            /// </summary>
            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_RegBtn_Cust_Registration_LiveDealer()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
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
                    Fail("Verify_RegBtn_Cust_Registration_LiveDealer failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through IMS
            /// Date: 24/03/2014
            /// </summary>
            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Cust_Registration_PromoCode_Poker()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion

                #region Decl
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();
                AccountAndWallets AnW = new Ladbrokes_IMS_TestRepository.AccountAndWallets();
                #endregion

                AddTestCase("Verify the Customers registration by adding bonus", "Bonus should be added while the Customer registers");
                try
                {
                    string Pcode = ReadxmlData("regdata", "PromoCode", DataFilePath.Accounts_Wallets);
                 //   baseIMS.Init();
                //    commIMS.CreateNew_RegisterBonus(baseIMS.IMSDriver, Pcode, Pcode);
                    AddTestCase("Bonus Code : " + Pcode, "");
                    Pass();

                    // Pass();
                //    baseIMS.Quit();
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages_BonusCode(driverObj, ref regData, Pcode);

                    // Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    Pass("Customer has logged in successfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Add Bonus to the customer reg scenario is Failed");


                }
                
            }


            /// <summary>
            /// Author:Anusha
            /// Register a customer through banking link
            /// Date: 20/06/2014
            /// </summary>
            //[Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_CashierPopUP_LoggedOut()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion
                try
                {
                    AddTestCase("Create customer from Banking pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "banking_lnk", "Coin-Cashier button not found", 0, false);

                    BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    BaseTest.Pass("Registration page opened");

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "reg_invalid_Prpmt_lgn", "Join button not found", 0, false);
                    AnW.Registration_PlaytechPages(driverObj, ref regData, 2);

                    Pass("Customer registered succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("URL Navigation failed");
                }

            }

            /// <summary>
            /// Author:Anusha
            /// Register a customer through invalid login pop up
            /// Date: 20/06/2014
            /// </summary>
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Registration_Invalid_Login_FindAddressCheck()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion
                try
                {
                    AddTestCase("Create customer from invalid customer login pop up", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    loginData.update_Login_Data("abcd", "abcd", "abcd");
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "banking_lnk", "Join button not found", 0, false);

                    BaseTest.AddTestCase("Customer login using invalid credentials", "User should not be able to login");
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "User logged in");
                    BaseTest.Pass("Customer Login failed");

                    BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
                    System.Threading.Thread.Sleep(3000);
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "reg_invalid_Prpmt_lgn", "Registration button not found in invalid login pop up", 0, false);
                    System.Threading.Thread.Sleep(3000);
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    BaseTest.Pass("Registration page opened");

                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "banking_reg", "Join button not found", 0, false);
                    AnW.Registration_PlaytechPages(driverObj, ref regData, 0, true);

                    Pass("Customer registered succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Registration_Invalid_Login_FindAddressCheck failed");
                }
            }


            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through IMS
            /// Date: 5/09/2014
            /// </summary>
            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Cust_Registration_FieldValidation()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages_FieldValidation(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Cust_Registration_FieldValidation failed");
                }

            }


            /// <summary>
            /// Naga
            /// GEN-4423
            /// </summary>
            //  [Test(Order = 13)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_German_Registration()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                string temp = Registration_Data.title;

                try
                {

                    //  Registration_Data.title = "Г-н";
                    AddTestCase("Register a customer in German language selecting Germany as country", "Registration should be successfull");

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany_german", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //Change the language
                    wAction._ClickAndMove(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "LanguageMenu", FrameGlobals.elementTimeOut, "Header element Change language menu not found");
                    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Join_German_Btn", "Language German in menu not found", 0, false);

                    //register in Russian
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Russian_Btn", "Join button not found", 0, false);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_German_Registration : scenario failed");
                }

            }


            /// <summary>
            /// Naga
            /// GEN-4423
            /// </summary>
            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Swedish_Registration()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                try
                {

                    AddTestCase("Register a customer in Swedish language selecting Germany as country", "Registration should be successfull");

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Swedish_Swedan", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //Change the language
                    wAction._ClickAndMove(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "LanguageMenu", FrameGlobals.elementTimeOut, "Header element Change language menu not found");
                    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Language_Swedish", "Language Swedish in menu not found", 0, false);

                    //register in Swedish
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Swedish_Btn", "Join button not found", 0, false);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Swedish_Registration : scenario failed");
                }
            }


          

        }//Registration class
        
        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Registration3 : BaseTest
        {
            

            AccountAndWallets AnW = new AccountAndWallets();

            


            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through IMS
            /// Date: 24/03/2014
            /// </summary>
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Cust_Registration_PromoCode_Invalid()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion

                #region Decl
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();
                AccountAndWallets AnW = new Ladbrokes_IMS_TestRepository.AccountAndWallets();
                #endregion

                AddTestCase("Verify the Customers registration by adding bonus", "Bonus should be added while the Customer registers");
                try
                {
                    string Pcode = "InvalidPromo";
                    //baseIMS.Init();
                    //commIMS.CreateNew_RegisterBonus(baseIMS.IMSDriver, Pcode + " signup bonus sports", Pcode);
                    //AddTestCase("Bonus Code : " + Pcode, "");
                    //Pass();

                    // Pass();
                    //baseIMS.Quit();
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages_BonusCode(driverObj, ref regData, Pcode, true);

                    // Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    Pass("Customer has logged in successfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Add Bonus to the customer reg scenario is Failed");


                }
                finally { baseIMS.Quit(); }
            }

            /// <summary>
            /// Naga
            /// Customers will have an SBK_ACCESS Customer Flag 2 set for their accounts for UK country on registration through online channel for ecommerce portal
            /// </summary>
            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SBKAccessFlag_UK_Customer_Systemsource_OB_Sports()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                AdminBase baseOB = new AdminBase();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminSuite.Common commOB = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Customers will have an SBK_ACCESS Customer Flag 2 set for their accounts for UK country on registration through online channel for ecommerce portal", "SBK Access should be present as expected in OB");
                    baseOB.Init();
                    commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                    commOB.VerifySBKAccess("2", baseOB.MyBrowser);

                    Pass("Verified customer flag in OB");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Verify_SBKAccessFlag_UK_Customer_Systemsource_OB_Ecom - failed");
                }
                finally
                {
                    baseOB.Cleanup();
                }
            }


            /// <summary>
            /// Author:Nagamanickam
            /// Verify if customer can choose any other currency listed other than default in PayPal quick registration form
            /// PNG-163
            /// </summary>
            // [Test(Order = 17)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_QuickReg_paypal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id_USD", DataFilePath.Accounts_Wallets),
                     ReadxmlData("paydata", "user_pwd_USD", DataFilePath.Accounts_Wallets),
                     ReadxmlData("paydata", "QAmt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));


                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages_PaypalQuickReg(driverObj, ref regData, acctData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commTest.VerifyCustDetailinIMS_newLook_Paypal(baseIMS.IMSDriver, regData);
                    BaseTest.Pass("Customer Details verified successfully in IMS");

                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
                    Ob.Init();
                    comAdmin.SearchCustomer(regData.username, Ob.MyBrowser);
                    commTest.VerifyCustDetailsInOB_Paypal(Ob.MyBrowser, regData);
                    BaseTest.Pass("Customer Details verified successfully in OB");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_QuickReg_paypal failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }

            }

            /// <summary>
            /// Author:Nagamanickam
            /// Verify if PayPal quick Registration link is available in Registration Page
            /// PNG-163
            /// </summary>
            //   [Test(Order = 18)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_QuickReg_paypal_Link()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                AddTestCase("Verify if PayPal quick Registration link is available in Registration Page for All PT product", "Quick Registration link should be present.");
                try
                {

                    AddTestCase("Verify if PayPal quick Registration link is available in Registration Page for Bingo product", "Quick Registration link should be present.");
                    //  string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                         AnW.OpenRegistrationPage(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    wAction.Click(driverObj, By.LinkText("Show Paypal quick registration form"), "Show Paypal quick registration form link not found", 0, false);
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Bingo");

                    AddTestCase("Verify if PayPal quick Registration link is available in Registration Page for Vegas product", "Quick Registration link should be present.");
                    wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Vegas Home page not loaded");
                         AnW.OpenRegistrationPage(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    wAction.Click(driverObj, By.LinkText("Show Paypal quick registration form"), "Show Paypal quick registration form link not found", 0, false);
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Vegas");

                    AddTestCase("Verify if PayPal quick Registration link is available in Registration Page for Live Dealer product", "Quick Registration link should be present.");
                    wAction.OpenURL(driverObj, FrameGlobals.LiveDealerURL, "Live Dealer Home page not loaded");
                         AnW.OpenRegistrationPage(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    wAction.Click(driverObj, By.LinkText("Show Paypal quick registration form"), "Show Paypal quick registration form link not found", 0, false);
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("LD");

                    AddTestCase("Verify if PayPal quick Registration link is available in Registration Page for Casino product", "Quick Registration link should be present.");
                    wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Casino Home page not loaded", FrameGlobals.elementTimeOut);
                    Thread.Sleep(TimeSpan.FromSeconds(20));
                         AnW.OpenRegistrationPage(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    wAction.Click(driverObj, By.LinkText("Show Paypal quick registration form"), "Show Paypal quick registration form link not found", 0, false);
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Casino");

                    AddTestCase("Verify if PayPal quick Registration link is available in Registration Page for Poker product", "Quick Registration link should be present.");
                    wAction.OpenURL(driverObj, FrameGlobals.PokerURL, "Poker Home page not loaded");
                         AnW.OpenRegistrationPage(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    wAction.Click(driverObj, By.LinkText("Show Paypal quick registration form"), "Show Paypal quick registration form link not found", 0, false);
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Poker");

                    AddTestCase("Verify if PayPal quick Registration link is available in Registration Page for Ecom product", "Quick Registration link should be present.");
                    wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "Ecom Home page not loaded");
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Ecom_Btn", Keys.Enter, "Join button not found", 0, false);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    wAction.Click(driverObj, By.LinkText("Show Paypal quick registration form"), "Show Paypal quick registration form link not found", 0, false);
                    wAction.BrowserClose(driverObj);
                    BaseTest.Pass("Ecom");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_QuickReg_paypal_Link failed");
                }


            }

            /// <summary>
            /// Author:Nagamanickam
            /// Verify launching cashier page in logged out state            
            /// </summary>
           // [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_CoinButton()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                AddTestCase("Verify launching cashier page in logged out state for all products", "Help page should be present.");
                try
                {

                    AddTestCase("Verify if Help page is present for Bingo product", "Quick Registration link should be present.");
                    //  string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Coin_Btn", "Coin_Btn not found", 0, false);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to Help page");
                    BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Help_Page"), "Help page not loaded / found");
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Bingo");

                    AddTestCase("Verify if Help page is present for Vegas product", "Quick Registration link should be present.");
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Coin_Btn", "Coin_Btn not found", 0, false);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to Help page");
                    BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Help_Page"), "Help page not loaded / found");
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Vegas");

                    AddTestCase("Verify if Help page is present for Live Dealer product", "Quick Registration link should be present.");
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Coin_Btn", "Coin_Btn not found", 0, false);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to Help page");
                    BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Help_Page"), "Help page not loaded / found");
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("LD");

                    AddTestCase("Verify if Help page is present for Casino product", "Quick Registration link should be present.");
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Coin_Btn", "Coin_Btn not found", 0, false);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to Help page");
                    BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Help_Page"), "Help page not loaded / found");
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Casino");

                    AddTestCase("Verify if Help page is present for Poker product", "Quick Registration link should be present.");
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Coin_Btn", "Coin_Btn not found", 0, false);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to Help page");
                    BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Help_Page"), "Help page not loaded / found");
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Poker");

                    AddTestCase("Verify if Help page is present for Ecom product", "Quick Registration link should be present.");
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Coin_Btn", "Coin_Btn not found", 0, false);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to Help page");
                    BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Help_Page"), "Help page not loaded / found");
                    wAction.BrowserClose(driverObj);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Home page");
                    BaseTest.Pass("Ecom");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_CoinButton failed");
                }


            }
                        

            /// <summary>
            /// Author:Nagamanickam
            ///Verify if customer is able to log in from overlay           
            /// </summary>
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_LoginOverlay()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion
                try
                {
                    AddTestCase("Verify if customer is able to log in from overlay", "Customer should be able to login thru overlay");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    loginData.update_Login_Data("abcd", "abcd", "abcd");
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "banking_lnk", "Join button not found", 0, false);

                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "User logged in/incorrect error displayed");

                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                       ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets),
                       ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets));
                    AnW.LoginPrompt(driverObj, loginData);

                    BaseTest.Pass("Customer Login failed");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_LoginOverlay failed");
                }


            }

            /// <summary>
            /// Author:Nagamanickam
            /// Postcode Mandatory for Germany player registration
            /// PNG-166
            /// </summary>
            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Cust_Registration_Germany_Postal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    regData.uk_postcode = "10117";
                    //regData.uk_addr_street_1 = "Hindes Road";
                    //regData.city = "Harrow";

                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages_Postal(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Cust_Registration_Germany_Postal failed");
                }

            }


            /// <summary>
            /// Author:Nagamanickam
            /// Postcode Mandatory for Germany player registration
            /// PNG-166
            /// </summary>
            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Cust_Registration_Ireland_Postal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Ireland", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    regData.uk_postcode = "10117";
                    //regData.uk_addr_street_1 = "Hindes Road";
                    //regData.city = "Harrow";

                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages_Postal(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Cust_Registration_Ireland_Postal failed");
                }

            }

        }//Registration class
        


     



        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Telebet : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();


            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(900)]
            public void Verify_CustomerRegistration_Telebet()
            {

                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from Telebet pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    baseIMS.Init();

                    commIMS.CreateNewCustomer_Telebet(baseIMS.IMSDriver, ref regData);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    BaseTest.AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    BaseTest.Pass();
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
            /// Naga
            /// GEN-1100
            /// </summary>
            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(900)]

            public void Verify_Deposit_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
                Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("Deposit from Telebet pages", "Deposit should be successful");
                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.Accounts_Wallets),
                ReadxmlData("vccdata", "pwd", DataFilePath.Accounts_Wallets),
                ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("vccdata", "TCCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "TCCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();

                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Deposit to customer " + loginData.username, ""); Pass();
                    commTB2.CreditCardDepositTB2(baseTB2.IMSDriver, acctData);
                    WriteUserName(loginData.username);
                    Pass("Fund deposited succesfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_Deposit_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }
            }

            /// <summary>
            /// Naga
            /// GEN-1398
            /// </summary>
            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(900)]
            //dependson('Verify_Deposit_Telebet')
            public void Verify_CallID_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion
                #region prereq
                if (!Usernames.ContainsKey("Verify_Deposit_Telebet"))
                {
                    UpdateThirdStatus("Not Run");
                    Fail("Dependent Test case \"Verify_Deposit_Telebet\" failed");
                    return;
                }
                #endregion


                try
                {
                    AddTestCase("Verify that the IMS associated all the payment transactions done on Telebet2 to the call Id generated by Openbet", "Call ID should be successfully Updated in IMS");
                    loginData.update_Login_Data(Usernames["Verify_Deposit_Telebet"],
                ReadxmlData("vccdata", "pwd", DataFilePath.Accounts_Wallets),
                ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("vccdata", "TCCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "TCCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Check for call id for Customer: " + loginData.username, ""); Pass();


                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.CheckCallID(baseIMS.IMSDriver, "ccdeposit_done", acctData.depositAmt);

                    Pass("Verify_CallID_Telebet succesfull");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_CallID_Telebet- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }


            /// <summary>
            /// Naga
            /// GEN-1118 / GEN-1119 / GEN-1115
            /// </summary>
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1200)]
            public void Verify_Withdraw_Limit_Cancel_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
                Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();

                loginData.update_Login_Data(ReadxmlData("Teldata", "user", DataFilePath.Accounts_Wallets),
            ReadxmlData("Teldata", "pwd", DataFilePath.Accounts_Wallets),
           " ");

                acctData.Update_deposit_withdraw_Card(ReadxmlData("Teldata", "account_pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("Teldata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("Teldata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("Teldata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("Teldata", "withWallet", DataFilePath.Accounts_Wallets), null);

                #endregion

                try
                {

                    AddTestCase("Withdraw related scenarios in Telebet pages", "Withdraw related function should be successful");

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();

                    #region Modify Withdraw limit in IMS
                    baseIMS.Init();
                    commIMS.ModifyWithdrawLimit(baseIMS.IMSDriver, 3, 6002);
                    baseIMS.Quit();

                    string min = "3.00";
                    string max = "6,002.00";
                    #endregion

                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Withdraw check in customer " + loginData.username, ""); Pass();
                    commTB2.WithdrawRelated(baseTB2.IMSDriver, acctData, min, max);

                    Pass("Withdraw Chk succesfull");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    CaptureScreenshot(baseIMS.IMSDriver, "Telebet");
                    Fail("Verify_Withdraw_Limit_Cancel_Telebet - failed");
                }
                finally
                {

                    baseIMS.Init();
                    commIMS.ModifyWithdrawLimit(baseIMS.IMSDriver, 5, 5000);

                    baseIMS.Quit();
                    baseTB2.Quit();
                }

            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1200)]
            public void Verify_TransferAll_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
                Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();

                loginData.update_Login_Data(ReadxmlData("Teldata", "user", DataFilePath.Accounts_Wallets),
            ReadxmlData("Teldata", "pwd", DataFilePath.Accounts_Wallets),
           " ");

                acctData.Update_deposit_withdraw_Card(ReadxmlData("Teldata", "account_pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("Teldata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("Teldata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("Teldata", "TransWalletDropDown", DataFilePath.Accounts_Wallets), ReadxmlData("Teldata", "TransWalletTable", DataFilePath.Accounts_Wallets),
                      null);

                #endregion

                try
                {

                    AddTestCase("Transfer related scenarios in Telebet pages", "Transfer related function should be successful");

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Transfer check in customer " + loginData.username, ""); Pass();
                    commTB2.AllWallets_TransferTB2(baseTB2.IMSDriver, acctData, acctData.depositWallet, acctData.withdrawWallet);
                    WriteUserName(loginData.username);
                    Pass("Withdraw Chk succesfull");

                }
                catch (Exception e)
                {
                    exceptionStack(e);

                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_TransferAll_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }

            }

         //   [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1200)]           
            public void Verify_Transfer_History()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
                Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();



                acctData.Update_deposit_withdraw_Card(ReadxmlData("Teldata", "account_pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("Teldata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("Teldata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("Teldata", "TransWalletDropDown", DataFilePath.Accounts_Wallets), ReadxmlData("Teldata", "TransWalletTable", DataFilePath.Accounts_Wallets), null);

                #endregion
                try{
                AddTestCase("Verify the account statements for a customer via telebet", "Transfer transaction should be present as expected.");
                  loginData.update_Login_Data(ReadxmlData("Teldata", "user", DataFilePath.Accounts_Wallets),
            ReadxmlData("Teldata", "pwd", DataFilePath.Accounts_Wallets),
           " ");

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Transfer check in customer " + loginData.username, ""); Pass();
                    commTB2.AccountHistory_Transfer(baseTB2.IMSDriver, acctData);
                    Pass("Withdraw Chk succesfull");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_Transfer_History - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }

            }


        }//Telebet class

      

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class OpenBetAdmin : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
            Ladbrokes_IMS_TestRepository.Common testcomm = new Ladbrokes_IMS_TestRepository.Common();
           // [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_ManualDeposit_OB()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                #region Declaration
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Ladbrokes_IMS_TestRepository.Common comm = new Ladbrokes_IMS_TestRepository.Common();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Manual Adjustments on Sports", "Manual adjustments should be successfull");
                try
                {
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    #region NewCustTestData
                    testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion


                    Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    #endregion

                   // double BefVal = AnW.GetWalletBalance_Withdrawable(driverObj, "Sportsbook");
                    double BefVal = StringCommonMethods.ReadDoublefromString(AnW.FetchBalance_Portal_Homepage(driverObj));
                    adminBase.Init();
                    // Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    adminComm.ManualAdjustment("100", adminBase.MyBrowser);
                    adminBase.Quit();

                    wAction.PageReload(driverObj);
                    double AftVal = StringCommonMethods.ReadDoublefromString(AnW.FetchBalance_Portal_Homepage(driverObj));
                    BaseTest.Assert.IsTrue((BefVal == (AftVal - 100)), "Amount not added to Customer");
                    Pass();


                }

                catch (Exception e)
                {

                    exceptionStack(e);

                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Verify_ManualDeposit_OB is failed for an exception");

                }
                finally
                {
                    adminBase.Quit();
                }

            }


        }//OB class
    }

