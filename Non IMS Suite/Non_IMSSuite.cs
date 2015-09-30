using System;
using System.Text;
using System.Collections.Generic;
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
//using System.Xml;
using ICE.ObjectRepository.Vegas_IMS_BAU;
using ICE.DataRepository;
using OpenQA.Selenium.Chrome;
//using ICE.ActionRepository;
using ICE.ObjectRepository;
using ICE.DataRepository.Vegas_IMS_Data;
using System.Web;
using OpenQA.Selenium.Interactions;

[assembly: ParallelismLimit]
namespace Non_IMS_Suite
{
    
       

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Exchange : BaseTest
        {
            
            AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            /// <summary>
            /// Author:Roopa
            /// Register customer with valid data in mandatory and optional fields from classic sports
            /// Date: 17/10/2014
            /// </summary>
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Exchange_Customer_Registration()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
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
                    AddTestCase("Create customer from Exchange", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    AnW.OpenRegistrationPage(driverObj);
                    //wAction.Click(driverObj, By.XPath(Ecomm_Control.exchange_Join), "Open Account button not found", 0, false);
                    //wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Ecom_Btn", Keys.Enter, "Open Account button not found", 0, false);

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
                    Fail("Verify_Exchange_Customer_Registration failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }


            /// <summary>
            /// Author:Roopa
            /// Verify the sync for Source,Source_ID,Client Type,Channel,Client Platform for the customer registered from Sports portal 
            /// Date: 17/10/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(1000)]
            [Test(Order = 2)]
            public void Verify_Exchange_Customer_Systemsource()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
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
                    AddTestCase("Create customer from classic sports", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets_stg), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets_stg));
                    AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in OB Admin", "Systemsource and Systemsource ID should be present as expected in OB");
                    baseOB.Init();
                    commOB.SearchCustomer(regData.username, baseOB.MyBrowser);
                    commOB.VerifySystemSourceAndID("OBE", null, baseOB.MyBrowser);
                    Pass("Verified customer flag in OB");
                    BaseTest.AddTestCase("Verified Systemsource and Systemsource ID of the customer in the IMS Admin", "Systemsource and Systemsource ID should be present as expected in IMS Admin");
                  
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                    commTest.VerifyCustSourceDetailinIMS_Exchange(baseIMS.IMSDriver);
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Verify_Exchange_Customer_Systemsource failed");
                }
                finally
                {
                    baseIMS.Quit();
                    baseOB.Cleanup();
                }
            }



            /// <summary>
            /// Author:Roopa
            /// Verify Deposit to Exchange wallet by Visa as a pay method
            /// Date: 29/10/2014
            /// </summary>
            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Exchange_Deposit_VisaCard()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion


                AddTestCase("Verify the Banking Links to deposit to Gaming wallet using credit card", "Amount should be deposited to the selected wallet");
                MyAcct_Data acctData = new MyAcct_Data();

                try
                {
                    Login_Data loginData = new Login_Data();

                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.Accounts_Wallets),
                  ReadxmlData("vccdata", "pwd", DataFilePath.Accounts_Wallets),
                  ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "depWallet_ex", DataFilePath.Accounts_Wallets),   ReadxmlData("vccdata", "depWallet_ex", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                  //  AnW.LoginFromEcom(driverObj, loginData);
                    ip2.LoginFromExchange(driverObj, loginData);
                    AnW.DepositTOWallet_CC_Exchange(driverObj, acctData);

                    WriteUserName(loginData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Exchange_Deposit_VisaCard  failed");
                    //Pass();
                }
            }


            /// <summary>
            /// Author:Roopa
            /// Successful Withdraw money from Exchange
            /// Date: 29/10/2014
            /// </summary>
            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 4)]
            public void Verify_Exchange_Withdraw_Exchange()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
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

                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.Accounts_Wallets),
             ReadxmlData("vccdata", "pwd", DataFilePath.Accounts_Wallets),
             ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "depWallet_ex", DataFilePath.Accounts_Wallets), ReadxmlData("vccdata", "depWallet_ex", DataFilePath.Accounts_Wallets),
                          ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //  AnW.LoginFromEcom(driverObj, loginData);
                    ip2.LoginFromExchange(driverObj, loginData);

                    AnW.Withdraw_CC_Ecom(driverObj, acctData);
                    WriteUserName(loginData.username);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");

                    Fail("Verify_Exchange_Withdraw_Exchange failed");

                }

            }

            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 5)]
            public void Verify_CancelWIthdrawal_Portal_Exchange()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
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
                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.Accounts_Wallets),
                           ReadxmlData("vccdata", "pwd", DataFilePath.Accounts_Wallets),
                           ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("vccdata", "depWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //  AnW.LoginFromEcom(driverObj, loginData);
                    ip2.LoginFromExchange(driverObj, loginData);

                    AnW.Cancel_Withdraw_Netteller(driverObj, acctData,"Ecom");
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
            /// Author:Roopa
            /// Verify the wallet transfer functionality across wallets: Exchange-Games and Games-Exchange
            /// Date: 4/11/2014
            /// </summary>
            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 6)]
            public void Verify_Exchange_FundTransfer_ExchangeToGames()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion

                #region Declaration

                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //// Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify the wallet transfer functionality across wallets: Exchange to Games", "Fund should be transfered successfully from Exchange to Games");

                try
                {
                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     "Exchange", "Exchange",null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //  AnW.LoginFromEcom(driverObj, loginData);
                    ip2.LoginFromExchange(driverObj, loginData);

                    AnW.WalletTransfer(driverObj, acctData, ReadxmlData("avdata", "depWallet_ex", DataFilePath.Accounts_Wallets), ReadxmlData("avdata", "depWallet_gme", DataFilePath.Accounts_Wallets));
                    Pass("Fund transfered successfully from Exchange to Games");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Exchange");
                    Fail("Verify_Exchange_FundTransfer_ExchangeToGames : Failed");
                }
            }

            /// <summary>
            /// Author:Roopa
            /// Verify the wallet transfer functionality across wallets: Exchange-Sports and Sports-Exchange
            /// Date: 4/11/2014
            /// </summary>
            [Timeout(1500)]
            [RepeatOnFail]
           // [Test(Order = 7)]
            public void Verify_Exchange_FundTransfer_ExchangeToSports()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion

                #region Declaration

                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //// Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify the wallet transfer functionality across wallets: Exchange to Sports", "Fund should be transfered successfully from Exchange to Sports");

                try
                {
                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     "Exchange", "Exchange",null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //  AnW.LoginFromEcom(driverObj, loginData);
                    ip2.LoginFromExchange(driverObj, loginData);

                    AnW.WalletTransfer(driverObj, acctData, "Exchange", "Sportsbook");
                    Pass("Fund transfered successfully from Exchange to Sports");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Exchange");
                    Fail("Verify_Exchange_FundTransfer_ExchangeToSports : Failed");
                }
            }

            /// <summary>
            /// Author:Roopa
            /// Verify the wallet transfer functionality across wallets: Exchange-Sports and Sports-Exchange
            /// Date: 4/11/2014
            /// </summary>
            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 8)]
            public void Verify_Exchange_FundTransfer_ExchangeToGaming()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion

                #region Declaration

                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //// Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify the wallet transfer functionality across wallets: Exchange to Gaming", "Fund should be transfered successfully from Exchange to Gaming");

                try
                {
                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     "Exchange", "Exchange",null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //  AnW.LoginFromEcom(driverObj, loginData);
                    ip2.LoginFromExchange(driverObj, loginData);

                    AnW.WalletTransfer(driverObj, acctData, ReadxmlData("avdata", "depWallet_ex", DataFilePath.Accounts_Wallets), ReadxmlData("avdata", "depWallet_gm", DataFilePath.Accounts_Wallets));
                    Pass("Fund transfered successfully from Exchange to Gaming");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Exchange");
                    Fail("Verify_Exchange_FundTransfer_ExchangeToGaming : Failed");
                }
            }

            /// <summary>
            /// Author:Roopa
            /// Verify Cashier First Deposit page From Exchange
            /// Date: 5/11/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 9)]
            [Parallelizable]
            public void Verify_Exchange_FirstDeposit_CreditCard()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion
                string creditCardNumber=ReadxmlData("dupdata", "CreditCd", DataFilePath.Accounts_Wallets);
                bool check = false;
                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    AnW.OpenRegistrationPage(driverObj);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");

                    BaseTest.AddTestCase("Enter Registration details for customer", "Registration details should be entered and received success msg Successfully");

                    //       AnW.OpenRegistrationPage(driverObj);
                    commTest.PP_Registration(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                   // acctData.Update_deposit_withdraw_Card(TD.createCreditCard("MasterCard").ToString(),
                    acctData.Update_deposit_withdraw_Card(creditCardNumber,

                    ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                    ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                    ReadxmlData("ccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), ReadxmlData("ccdata", "CVV", DataFilePath.Accounts_Wallets));
                    check = true;
                    BaseTest.AddTestCase("Verify customer is able to add credit/ debit as payment method in the first deposit page", "First deposit should be successful");
                    Assert.IsTrue(AnW.Verify_FirstCashier_MasterCard(driverObj, acctData), "First Deposit amount not added to the wallet");
                    BaseTest.Pass("successfully verified");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Exchange_FirstDeposit_CreditCard - failed");
                }
                finally
                {
                    if (check)
                    {
                        try
                        {
                            baseIMS.Init();
                            commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                            commIMS.AllowDuplicateCreditCard_FullNavigation_Newlook(baseIMS.IMSDriver, creditCardNumber.Substring(0, 6), creditCardNumber.Substring(creditCardNumber.Length - 4, 4));
                        }
                        catch (Exception e)
                        {
                            CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                            Console.WriteLine(e.Message.ToString());
                            // throw e;
                        }

                        finally
                        {
                            baseIMS.Quit();
                        }
                    }
                }
            }


            /// <summary>
            /// Author:Roopa
            /// Verify if the player's AV status is not passed then deposit fails to Exchange wallet
            /// Date: 6/11/2014
            /// </summary>
            [Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Exchange_Deposit_non_AV()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion
                AddTestCase("Verify if the player's AV status is not passed then deposit fails to Exchange wallet", "Amount should not be deposited to the Exchange wallet");
                MyAcct_Data acctData = new MyAcct_Data();

                try
                {
                    Login_Data loginData = new Login_Data();

                    loginData.update_Login_Data(ReadxmlData("avdata", "user", DataFilePath.Accounts_Wallets),
                  ReadxmlData("avdata", "pwd", DataFilePath.Accounts_Wallets),
                  ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("avdata", "depWallet_ex", DataFilePath.Accounts_Wallets), ReadxmlData("avdata", "depWallet_ex", DataFilePath.Accounts_Wallets),null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //  AnW.LoginFromEcom(driverObj, loginData);
                    ip2.LoginFromExchange(driverObj, loginData);

                    Assert.IsTrue(AnW.Verify_Deposit_NonAv(driverObj, acctData), "");

                    WriteUserName(loginData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Exchange_Deposit_non_AV  failed");
                }
            }


            /// <summary>
            /// Author:Roopa
            /// Verify the wallet transfer functionality across wallets: Exchange-Games and Games-Exchange
            /// Date: 4/11/2014
            /// </summary>
            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 11)]
            public void Verify_Exchange_FundTransfer_Non_AV()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion

                #region Declaration

                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //// Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify the wallet transfer functionality across wallets: Exchange to Games", "Fund should be transfered successfully from Exchange to Games");

                try
                {
                    loginData.update_Login_Data(ReadxmlData("avdata", "user", DataFilePath.Accounts_Wallets),
                 ReadxmlData("avdata", "pwd", DataFilePath.Accounts_Wallets),
                 ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("avdata", "depWallet_gm", DataFilePath.Accounts_Wallets), ReadxmlData("avdata", "depWallet_ex", DataFilePath.Accounts_Wallets),null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //  AnW.LoginFromEcom(driverObj, loginData);
                    ip2.LoginFromExchange(driverObj, loginData);

                    Assert.IsTrue(AnW.WalletTransferAV(driverObj, acctData), "Transfer was successful for customer with AV status as failed");
                    Pass("Fund transfer fails from  Gaming to Exchange as expected ");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Exchange");
                    Fail("Verify_Exchange_FundTransfer_Non_AV : Failed");
                }
            }



        }

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Sports : BaseTest
        {
            
            Ladbrokes_IMS_TestRepository.SeamLessWallet sw = new SeamLessWallet();
            AccountAndWallets AnW = new AccountAndWallets();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            /// <summary>
            /// Author:Roopa
            /// Verify the sync for Source,Source_ID,Client Type,Channel,Client Platform for the customer registered from Sports portal 
            /// Date: 17/10/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(1000)]
            [Test(Order = 1)]
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
                    AddTestCase("Create customer from classic sports", "Customer should be created.");
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
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Verify_Sports_Customer_Systemsource_OB failed");
                }
                finally
                {
                    baseOB.Quit();
                }
            }

            /// <summary>
            /// Author:Roopa
            /// Customers will have an SBK_ACCESS Customer Flag 1 set for their accounts
            /// for non UK country on registration through online channel"
            /// Date: 17/10/2014
            /// </summary>
            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_Customer_NonUK_SBKFlag()
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
                    AddTestCase("Create customer from classic Sports", "Customer should be created.");
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                          AnW.OpenRegistrationPage(driverObj);

                    //        AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

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
                    Fail("Verify_Sports_Customer_NonUK_SBKFlag - failed");
                }
                finally
                {
                    baseOB.Quit();
                }
            }

            //Registration
            /// <summary>
            /// Author:Roopa
            /// Register customer with valid data in mandatory and optional fields from classic sports
            /// Date: 17/10/2014
            /// </summary>
            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_Customer_Registration()
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
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                          AnW.OpenRegistrationPage(driverObj);

                    
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
                    Fail("Verify_Sports_Customer_Registration failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            /// <summary>
            /// Author:Roopa
            /// Register customer with valid data in  mandatory and optional fields from eccom sports 
            /// Date: 20/10/2014
            /// </summary>
         //   [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Ecom_Customer_Registration()
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
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    AnW.OpenRegistrationPage(driverObj); 
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


            /// <summary>
            /// Author:Roopa
            /// Verify Banking link Sports (Classic/Ecom)
            /// Date: 21/10/2014
            /// </summary>
          //  [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_Banking_Link()
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
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {

                    loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.Accounts_Wallets),
                       ReadxmlData("ccdata", "pwd", DataFilePath.Accounts_Wallets),
                       ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets));


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AddTestCase("Verify banking link for ecom sports", "Banking page displayed with all paymethods");

                    AnW.Login_Sports(driverObj, loginData);
                    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Sports_banking_lnk", "Banking Link not found", FrameGlobals.reloadTimeOut, false);
                    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    //Verify whether all the available payment methods are displayed



                    Pass("Banking page displayed properly with all paymethods");
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_Sports_Banking_Link failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            /// <summary>
            /// Author:Nagamanickam
            /// Verify My account pages from sports url 
            /// </summary>
            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_MyAcct_VerifyLinks_Sports()
            {

                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                // Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                try
                {

                    AddTestCase("Verify All links in MY Acount page is present", "All the MY Acct links should be present in the page");

                   loginData.update_Login_Data(ReadxmlData("lgnsdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgnsdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgnsdata", "Fname", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);

                    AnW.OpenMyAcct(driverObj);
                    
                    AnW.MyAccount_VerifyLinks(driverObj);


                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("My Acct Link Verification scenario failed");
                }
            }

            /// <summary>
            /// Author:Nagamanickam
            /// Verify My Details pages from sports url 
            /// </summary>
          // [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_MyDetails_VerifyLinks_Sports()
            {

                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                // Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                try
                {

                    AddTestCase("Verify All links in MY Details page is present", "All the MY Acct links should be present in the page");

                    loginData.update_Login_Data(ReadxmlData("lgnsdata", "user", DataFilePath.Accounts_Wallets),
                         ReadxmlData("lgnsdata", "pwd", DataFilePath.Accounts_Wallets),
                         ReadxmlData("lgnsdata", "Fname", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);

                    try
                    {
                        //  wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "mydetails_lnk", "My Details Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    wAction.Click(driverObj, By.XPath(Cashier_Control_SW.TransactionNotification_OK_Dialog));
                    AnW.MyDetails_VerifyLinks(driverObj);


                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("My Acct Link Verification scenario failed");
                }
            }


            [Test(Order = 8)]
            [RepeatOnFail]
            [Timeout(1600)]
            public void Verify_Transfer_Portal_Sports()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration


                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify Transfer is successful in Ecom", "Transfer should be successful.");
                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "Wallet_Sports", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "Wallet_Games", DataFilePath.Accounts_Wallets),null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.Login_Sports(driverObj, loginData);
                    AnW.Wallet_Transfer_SportsGames_Sports(driverObj, acctData);

                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Fund transfer among all wallets is failed for an exception");

                }
            }


            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 9)]
            // [Parallelizable]
            public void Verify_Withdraw_Netteller_Sports()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData,"Sports");
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


            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 10)]
            
            public void Verify_CancelWIthdrawal_Portal_Sports()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets_stg));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets_stg),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets_stg),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets_stg), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    AnW.OpenCashier(driverObj);
                    BaseTest.Assert.IsTrue(commTest.CommonCancelWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt ),"Amount not added after cancelling the withdraw");
                    
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
            /// Author:Roopa
            /// Verify Deposit to sports wallet by Visa as a pay method
            /// Date: 30/10/2014
            /// </summary>
            [Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Sports_Deposit_VisaCard()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion


                AddTestCase("Verify the Banking Links to deposit to Gaming wallet using credit card", "Amount should be deposited to the selected wallet");
                MyAcct_Data acctData = new MyAcct_Data();

                try
                {
                    Login_Data loginData = new Login_Data();

                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.Accounts_Wallets),
                  ReadxmlData("vccdata", "pwd", DataFilePath.Accounts_Wallets),
                  ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "Wallet_Sports", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "Wallet_Sports", DataFilePath.Accounts_Wallets), ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.Login_Sports(driverObj, loginData);
                    AnW.OpenCashier(driverObj);
                    AnW.DepositTOWallet_CC(driverObj, acctData);
                  //  AnW.DepositTOWallet_CCSports(driverObj, acctData);
                    WriteUserName(loginData.username);
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_Deposit_VisaCard failed");
                    //Pass();
                }
            }

            [Test(Order = 12)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_CreditCustomer_Mastercard()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                AddTestCase("Validation of credit card registraion for Credit customer", "User should be able to register credit card successfully");
                try
                {


                    #region Prerequiste
                    //AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    baseIMS.Init();
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit");
                    baseIMS.Quit();
                    //      AnW.OpenRegistrationPage(driverObj);
                    //AnW.Registration_PlaytechPages(driverObj, ref regData);
                    //Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                    string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion

                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.Login_Sports(driverObj, loginData);
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    AnW.OpenMyAcct(driverObj);
                    AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                    wAction.BrowserClose(driverObj);
                    driverObj.SwitchTo().Window(portalWindow);

                    AnW.OpenCashier(driverObj);
                        wAction.WaitAndMovetoPopUPWindow(driverObj, "Banking window not found", FrameGlobals.elementTimeOut);
                        
                    AnW.Verify_Credit_Card_Registration(driverObj, creditCardNumber, "MasterCard");
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_CreditCustomer_Mastercard - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }
        
            /// <summary>
            /// Author:Roopa
            /// Customer  clicks on any selection to place a bet on and do a successful registration
            /// Date: 31/10/2014
            /// </summary>
         //   [Test(Order = 13)]
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
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

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

            /// <summary>
            /// Author:Naga
            /// Verify Deposit to sports wallet by PayPal  as a pay method
            /// Date: 31/10/2014
            /// </summary>
            [Test(Order = 14)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Sports_PayPal_Deposit()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                AddTestCase("Verify the payment deposit with pay pal method is successful", "Amount should be deposited to the selected wallet");
                try
                {

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.Login_Sports(driverObj, loginData);
                    #endregion
                    acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                       ReadxmlData("paydata", "user_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));

                    //AnW.OpenCashier(driverObj);
                    AnW.Register_Paypal(driverObj, acctData);                    
                    WriteUserName(regData.username);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_PayPal_Deposit failed");
                }

            }

            /// <summary>
            /// Author:Roopa
            /// Prefix (Region code) for Telephone and Mobile should populate according to the Country selected
            /// Date: 30/10/2014
            /// </summary>
            [Test(Order = 15)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_Registration_RegionCode()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion

                try
                {
                    AddTestCase("Create customer from classic sports", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                          AnW.OpenRegistrationPage(driverObj);

                    //       AnW.OpenRegistrationPage(driverObj);
                    AnW.Validate_RegionCode_RegitraionPage(driverObj, ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany_code", DataFilePath.Accounts_Wallets));
                    AnW.Validate_RegionCode_RegitraionPage(driverObj, ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK_code", DataFilePath.Accounts_Wallets));

                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_Registration_RegionCode failed");
                }
            }

            /// <summary>
            /// Author:Roopa
            ///Verify selections are removed from Bet Slip
            /// Date: 3/11/2014
            /// </summary>
            [Test(Order = 16)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_SelectionRemovedAfterLogOut()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                Login_Data loginData = new Login_Data();
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

                #endregion
                IP2Common Ip2 = new IP2Common();
                try
                {
                    AddTestCase("Verify selections are removed from Bet Slip after logout", "Selections are successfully removed form the betslip after log out");

                    loginData.update_Login_Data(ReadxmlData("lgnsdata", "user", DataFilePath.Accounts_Wallets),
                       ReadxmlData("lgnsdata", "pwd", DataFilePath.Accounts_Wallets),
                       ReadxmlData("lgnsdata", "Fname", DataFilePath.Accounts_Wallets));
                string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                string amt = ReadxmlData("event", "eAmt", DataFilePath.IP2_SeamlessWallet);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.Login_Sports(driverObj, loginData);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Single);
                    Ip2.Logout_Sports(driverObj);
                    string betslipCount = wAction.GetText(driverObj, By.Id("betslip-indicator"), "Betslip count text not found", false);
                    Assert.IsTrue((betslipCount.Equals("1")), "Selection removed from the betslip after Log out");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Sports");
                    Fail("Verify_Sports_SelectionRemovedAfterLogOut failed");
                }

            }

            /// Author:Roopa
            /// Verify Auto top up funds transfer when flag is enabled 
            /// Date: 24/03/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
           // [Test(Order = 17)]
            public void Verify_sports_Enable_Auto_TopUP()
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
                    AddTestCase("Verify Auto top up funds transfer when flag is enabled", "Customer should  be able to place bet through Auto Top Up");


                    loginData.update_Login_Data(ReadxmlData("autodata", "user", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("autodata", "pwd", DataFilePath.Accounts_Wallets_stg),
                                            "");
                   // excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    AnW.Login_Sports(driverObj, loginData);
                    AnW.Ecom_MyAcct_Enable_AutoTopUP(driverObj);

                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);
                    AnW.placeBet(driverObj); 
                    Pass("Place bet for Auto top up verified succesfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_sports_Enable_Auto_TopUP - Auto Top Up scenario failed");
                }

            }

            /// Author:Roopa
            /// Verify placing a bet when Auto top up flag is off
            /// Date: 24/03/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
           // [Test(Order = 18)]
            public void Verify_sports_Disable_Auto_TopUP()
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
                    AddTestCase("Verify that Disabiling the AutoTop up option, placing a Bet throws an error", "Customer should not be able to place bet through Auto Top Up");


                    loginData.update_Login_Data(ReadxmlData("autodata", "user", DataFilePath.Accounts_Wallets_stg),
                                             ReadxmlData("autodata", "pwd", DataFilePath.Accounts_Wallets_stg),
                                             "");
                  //  excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);

                    AnW.Ecom_MyAcct_AutoTopUP(driverObj);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);

                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "place_bet_button", "Place bet  button is not found", 0, false);
                    wAction.WaitUntilElementDisappears(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait));


                    //bet now button should b displayed
                    Assert.IsFalse(wAction._IsElementPresent(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "betReceiptPath"), "Bet now button is displayed for Auto top up disabled");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_sports_Disable_Auto_TopUP - failed");
                }

            }

            /// Author:Roopa
            /// Verify Cashier First Deposit page From Sports
            /// Date: 5/11/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 19)]
            [Parallelizable]
            public void Verify_Sports_FirstDeposit_Netteller()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IP2Common ip2 = new IP2Common();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                MyAcct_Data acctData = new MyAcct_Data();
                TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
                AccountAndWallets AnW = new AccountAndWallets();

                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
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
                    Fail("Verify_Sports_FirstDeposit_CreditCard - failed");
                }

            }
           

            /// Author:Roopa
            /// Verify the sports bets history From My account --> Account history  
            /// Date: 10/11/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
         //   [Test(Order = 20)]
            public void Verify_Sports_Bet_History()
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
                    AddTestCase("Verify the sports bets history From My account", "Sports history should display all bet details");

                    loginData.update_Login_Data(ReadxmlData("vccdata", "user", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("vccdata", "pwd", DataFilePath.Accounts_Wallets_stg),
                                            "");
                  //  excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                   string eventName = AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);
                    // //div[@id='betslipSingle0']//div[contains(string(),'Event')]/following-sibling::div[@class='rightcol']

                //    string eventName = wAction._GetText(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "eventName_checkBetPage", "Event name not found", false);
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
                    Fail("Verify_Sports_Bet_History - Failed");
                }

            }
          
            [RepeatOnFail]
            [Timeout(2200)]
         //   [Test(Order = 20)]
            public void Verify_Sports_Bet_Quick_Deposit2()
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
                Ladbrokes_IMS_TestRepository.AccountAndWallets accwal = new Ladbrokes_IMS_TestRepository.AccountAndWallets();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                bool flag = false;
                #endregion

                try
                {
                    AddTestCase("Verify the sports bets history From My account", "Sports history should display all bet details");

                    loginData.update_Login_Data(ReadxmlData("qdepdata", "user", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("qdepdata", "pwd", DataFilePath.Accounts_Wallets_stg), "");

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                         ReadxmlData("qdepdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("qdepdata", "wAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("qdepdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("qdepdata", "ToWallet", DataFilePath.Accounts_Wallets), null);

                    //  excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj,acctData.depositAmt);
                    // //div[@id='betslipSingle0']//div[contains(string(),'Event')]/following-sibling::div[@class='rightcol']

                    //string eventName = wAction._GetText(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "eventName_checkBetPage", "Event name not found", false);
                    //bet now button should b displayed
                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "place_bet_button");
                    wAction.WaitAndMovetoFrame(driverObj, By.Name("PtMiniCashier"), null, 10);
                    //Clicking the open cashier button
                    wAction.Click(driverObj, By.Name("quickDeposit.openFullCashier"), "Open cashier link not found", 0, true);
                    wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 1, "Window not found");

                    //Calling common method netteller
                    BaseTest.Assert.IsTrue(accwal.CommonDeposit_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added");
                    flag = true;

                    wAction.BrowserClose(driverObj);
                    wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 0, "Main window not found");
                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "check_btn", "Check button not found inside betslip", 0, false);
                    AnW.placeBet(driverObj); 
                    // BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//div[contains(text(),'Your bets have been placed successfully')]")), "Bet not placed");
                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    try
                    {
                        if (flag == true)
                        {
                            AnW.OpenCashier(driverObj);
                            accwal.Withdraw_Netteller(driverObj, acctData, "Sports");

                        }
                    }
                    catch (Exception)
                    {
                    }

                    Fail("Bet has not been placed");
                }
            }


            [RepeatOnFail]
            [Timeout(2200)]
         //   [Test(Order = 20)]
            public void Verify_Sports_Bet_Quick_Deposit()
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
                IP2Common ip2 = new IP2Common();
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Verify the Sports bet history under Account history ", "Bet history should appear in my accounts page.");



                    loginData.update_Login_Data(ReadxmlData("qdepdata", "user", DataFilePath.Accounts_Wallets_stg),
                                               ReadxmlData("qdepdata", "pwd", DataFilePath.Accounts_Wallets_stg), "");

                        
                    string amount =ReadxmlData("qdepdata", "wAmt", DataFilePath.Accounts_Wallets);
                    //  excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj, amount);
                    // //div[@id='betslipSingle0']//div[contains(string(),'Event')]/following-sibling::div[@class='rightcol']

                    string eventName = wAction._GetText(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "eventName_checkBetPage", "Event name not found", false);
                    //bet now button should b displayed
                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "place_bet_button");
                    wAction.WaitUntilElementDisappears(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait));


                    ip2.quickDep_Sports(driverObj,amount );
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSuccess1_xp)) || wAction.IsElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSuccess2_xp)), "Bet not placed");
                 
                    Pass("Sports history verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_QuickDep_Ip2_BVT scenario failed");
                }

            }
            
            /// <summary>
            /// Author:Roopa
            /// Verify the display of Odds based on the selection in “View Odds as” drop down in the top of LHN
            /// Date: 10/11/2014
            /// </summary>
            [Test(Order = 21)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_ViewOdds()
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
                    AddTestCase("Verify that on changing the display format from View Odds", "Odd display format changed successfully.");
                    string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "eAmt", DataFilePath.IP2_SeamlessWallet);


                    
                    string new_EventOddFormat = null;

                    AddTestCase("Verify that on changing the display format from View Odds Fraction->Decimal", "Odd display format changed successfully.");
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.SettingsMenu_XP), "View Odds drop down not found", FrameGlobals.reloadTimeOut, false);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Decimal_XP), "Decimal odd Link not found",0,false);
                    wAction.WaitforPageLoad(driverObj);

                    List<IWebElement>  odds = wAction.ReturnWebElements(driverObj, By.XPath(Sportsbook_Control.oddList), "No selections found", 0, false);
                    int index = 0;
                    foreach (IWebElement odd in odds)
                        if (odd.Text != " " && odd.Text != "SUSP")
                            break;
                        else
                            index++;

                    new_EventOddFormat = odds[index].Text;
                    Assert.IsTrue(new_EventOddFormat.Contains("."), "Odds not changed to decimal");
                  
                    Pass();

                    AddTestCase("Verify that on changing the display format from View Odds Decimal->Fractional", "Odd display format changed successfully.");
                   if(!wAction.IsElementPresent(driverObj,By.XPath(Sportsbook_Control.Fractional_XP)))
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.SettingsMenu_XP), "View Odds drop down not found", FrameGlobals.reloadTimeOut, false);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Fractional_XP), "Fractional odd Link not found",0,false); wAction.WaitforPageLoad(driverObj);
                    
                    new_EventOddFormat = odds[index].Text;
                    Assert.IsTrue(new_EventOddFormat.Contains("/"), "Odds not changed to fractional yet");
                    
                    Pass();                 

                    Pass("Odds display changed successfully based on Odd View selection");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_ViewOdds failed");
                }
            }





        }//Sports class

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Games : BaseTest
        {
            
            IP2Common ip2 = new IP2Common();
            AccountAndWallets AnW = new AccountAndWallets();
            SeamLessWallet sw = new SeamLessWallet();


            /// <summary>
            /// Author:Nagamanickam
            /// Register a customer through games
            /// Date: 24/03/2014
            /// </summary>
            /// 
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_RegBtn_Cust_Registration_Games()
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
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.email = "user@gmail.com";
                          AnW.OpenRegistrationPage(driverObj);
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
                    Fail("Verify_RegBtn_Cust_Registration_Games failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1600)]
            public void Verify_Transfer_Portal_GamesToGaming_GamingToGames()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.GamesURL);
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "Wallet_Games", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                   
                    ip2.LoginGames(driverObj, loginData);
                    AnW.OpenCashier(driverObj);
                    AnW.GamesToGaming_GamingToGames_Transfer(driverObj, acctData, ReadxmlData("depdata", "TransWalletDropDown", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "TransWalletTable", DataFilePath.Accounts_Wallets));

                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Fund transfer among all wallets is failed for an exception");

                }
            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1600)]
            public void Verify_Withdraw_Except_GamesWallet()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.GamesURL);
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    ip2.LoginGames(driverObj, loginData);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData, "Games");
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
            /// <summary>
            /// Author:Nagamanickam
            /// Launching of PT cashier after login to the portal
            /// </summary>
            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Cashier_Deposit_Games()
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
                    AddTestCase("Verify Launching of PT cashier after login to the portal", "Launching PT cashier page should be successfull");
                    MyAcct_Data acctData = new MyAcct_Data();

                    Login_Data loginData = new Login_Data();
                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                                          ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                                          ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), null);


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    ip2.LoginGames(driverObj, loginData);
                    
                    AnW.DepositTOWallet_Netteller(driverObj, acctData);
                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Cashier_Deposit_Games failed");
                }

            }

            /// <summary>
            /// Author:Roopa
            /// Verify the sync for Source,Source_ID,Client Type,Channel,Client Platform for the customer registered from Games portal UK Customer 
            /// Date: 16/10/2014
            /// </summary>
            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_RegBtn_Cust_Registration_Games_Source_UK()
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
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                          AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                    commTest.VerifyCustSourceDetailinIMS_newLook(baseIMS.IMSDriver);
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_RegBtn_Cust_Registration_Games_Source_UK failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Cleanup();
                }
            }

            /// <summary>
            /// Author:Roopa
            /// Verify the Games website in different languages supported
            /// Date: 20/10/2014
            /// </summary>
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Games_Language()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.GamesURL);
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
                    AddTestCase("Verify the Games website in different languages supported", "Games website should be working fine for different languages");

                    wAction.ClickAndMove(driverObj, By.XPath(Games_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Games_Control.Deutsch_XP), "Language Deutsch in menu not found", 0, false);

                    Thread.Sleep(4000);
                    String failMsg = null;
                    if (!driverObj.Url.Contains("de"))
                    {
                        failMsg = "Casino not loaded properly on selecting Deutsch language";
                        isPass = false;
                    }
                    commonWebMethods.WaitforPageLoad(driverObj);
                    driverObj.Navigate().GoToUrl(FrameGlobals.GamesURL);
                    commonWebMethods.WaitforPageLoad(driverObj);
                    Thread.Sleep(4000);

                    //Change the language
                    wAction.ClickAndMove(driverObj, By.XPath(Games_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Games_Control.espanol_XP), "Language Espanol in menu not found", 0, false);

                    Thread.Sleep(4000);

                    if (!driverObj.Url.Contains("es"))
                    {
                        failMsg = failMsg + "Games not loaded properly on selecting Espanol language";
                        isPass = false;
                    }
                    commonWebMethods.WaitforPageLoad(driverObj);
                    driverObj.Navigate().GoToUrl(FrameGlobals.GamesURL);
                    commonWebMethods.WaitforPageLoad(driverObj);
                    Thread.Sleep(4000);
                    //Change the language
                    
                    wAction.ClickAndMove(driverObj, By.XPath(Games_Control.Language_XP), 10, "Language Change not found");
                    wAction.Click(driverObj, By.XPath(Games_Control.irish_XP), "Language Svenska in menu not found", 0, false);
                    Thread.Sleep(4000);
                    if (!driverObj.Url.Contains("ie"))
                    {
                        failMsg = failMsg + "Games not loaded properly on selecting Irish language";
                        isPass = false;
                    }

                    Assert.IsTrue(isPass, failMsg);
                    BaseTest.Pass("Games website in different languages supported verified successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Games_Language failed");
                }
            }

            /// <summary>
            /// Author:Roopa
            /// Verify the sync for Source,Source_ID,Client Type,Channel,Client Platform for the customer registered from Games portal non-UK Customer 
            /// Date: 20/10/2014
            /// </summary>
            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_RegBtn_Cust_Registration_Games_Source_non_UK()
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
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                          AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    
                    Pass("Customer registered succesfully");

                    BaseTest.AddTestCase("Verified the Customers details page in the IMS Admin", "Registered customers details should be present in the IMS Admin");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                    commTest.VerifyCustSourceDetailinIMS_newLook(baseIMS.IMSDriver);
                    BaseTest.Pass("Customer Details verified successfully in IMS");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_RegBtn_Cust_Registration_Games_Source_non_UK - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }


          

        }//games class


    
}//NameSpace
