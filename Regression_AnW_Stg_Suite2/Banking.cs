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


namespace Regression_AnW_Suite2
{
 


        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Banking1 : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            SeamLessWallet sw = new SeamLessWallet();

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1500)]
            /// <summary>
            /// Author:Anand
            /// Setting up Withdrawal limit successfully in IMS admin
            /// Registration of customer pay method as Neteller - GEN-5404
            /// Date: 20/06/2014
            /// </summary>
            public void Verify_Netteller_Registration_WithdrawLimit()
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
                    // regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion

                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    // AnW.Registration_PlaytechPages(driverObj, ref regData);
                    //Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    #endregion

                    #region Neteller Registration
                    String portalWindow = AnW.OpenCashier(driverObj);
                    AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets));
                    driverObj.Close();
                    driverObj.SwitchTo().Window(portalWindow);
                    //if (driverObj.FindElement(By.XPath(Login_Control.modelWindow_Generic_Xpath)).Displayed)
                    //{
                    //    commonWebMethods.Click(driverObj, By.XPath(Login_Control.modelWindow_Generic_Xpath), null, 0, false);
                    //}
                    wAction.PageReload(driverObj);
                    AnW.LogoutFromPortal(driverObj);
                    #endregion

                    #region Modify Withdraw limit in IMS
                    baseIMS.Init();
                    commIMS.ModifyWithdrawLimit(baseIMS.IMSDriver, 3, 5002);
                    baseIMS.Quit();
                    #endregion

                    #region Verify Withdraw limit in portal
                    loginData.update_Login_Data(regData.username, regData.password, regData.fname);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.VerifyWithdraw_Limit_Portal(driverObj, "3.00", "5,002.00");
                    #endregion

                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Netteller_Registration_WithdrawLimit for exception");
                }

                finally
                {
                    baseIMS.Init();
                    commIMS.ModifyWithdrawLimit(baseIMS.IMSDriver, 5, 5000);
                    baseIMS.Quit();
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Credit_Card_Registration()
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


                AddTestCase("Validation of credit card registraion", "User should be able to register credit card successfully");
                try
                {
                    #region Prerequiste
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AnW.Registration_PlaytechPages(driverObj, ref regData);
                    //Pass("Customer registered succesfully");

                    string creditCardNumber = TD.createCreditCard("Visa").ToString();
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion


                    String portalWindow = AnW.OpenCashier(driverObj);
                    AnW.Verify_Credit_Card_Registration(driverObj, creditCardNumber);
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    //  CaptureScreenshot(baseIMS.IMSDriver, "IMS");                  
                    Fail("Credit Card registration failed for exception");
                }

            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Credit_Card_Registration_Used_CC()
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


                AddTestCase("Validation of credit card registraion", "User should be able to register credit card successfully");
                try
                {
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    #region NewCustTestData

                    regData.email="new@aditi.com";
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion

                    baseIMS.Init();

                    string creditCardNumber = ReadxmlData("ccdata", "CCard", DataFilePath.Accounts_Wallets);
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion


                    String portalWindow = AnW.OpenCashier(driverObj);
                    AnW.Verify_Credit_Card_Registration_Used_CC(driverObj, creditCardNumber, "MasterCard");
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    //  CaptureScreenshot(baseIMS.IMSDriver, "IMS");                  
                    Fail("Credit Card registration failed for exception");
                }

            }
                   
            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Cash_Registration()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                AddTestCase("Verify the payment deposit with Cash method is successful", "Amount should be deposited to the selected wallet");
                try
                {

                    #region Prerequiste

                    //
                    //string pass = ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets);
                    //regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, "user", pass);

                    //WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + pass);
                    //AddTestCase("Username:" + regData.username + " Password:" + pass, "");
                    //Pass();
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + loginData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + loginData.password, "");
                    baseIMS.Init();
                    #endregion

                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.AddCash(baseIMS.IMSDriver, ReadxmlData("cashdata", "WalletName", DataFilePath.Accounts_Wallets), "50");
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Registration scenario failed for Cash");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_BankDraft_Registration()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                AddTestCase("Verify the payment deposit with Cash method is successful", "Amount should be deposited to the selected wallet");
                try
                {

                    #region Prerequiste

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + loginData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + loginData.password, "");
                    baseIMS.Init();
                    #endregion
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.AddBankDraft(baseIMS.IMSDriver, ReadxmlData("bddata", "WalletName", DataFilePath.Accounts_Wallets), "50");
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Registration scenario failed for Cash");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }

      



    



        }//Banking1 class

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Banking2 : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();


            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_PaySafe_Registration()
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


                AddTestCase("Validation of Paysafe registraion", "User should be able to register Paysafe card successfully");
                try
                {

                    #region Prerequiste
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //      AnW.OpenRegistrationPage(driverObj);
                    regData.currency = "EUR";
                    // AnW.Registration_PlaytechPages(driverObj, ref regData);
                    //  Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion

                    #endregion

                    String portalWindow = AnW.OpenCashier(driverObj);
                    #region Paysafe Registration
                    acctData.depositWallet = ReadxmlData("paysafedata", "DepWallet", DataFilePath.Accounts_Wallets);
                    acctData.depositAmt = ReadxmlData("paysafedata", "DepAmt", DataFilePath.Accounts_Wallets);
                    AnW.Verify_PaySafe_Registration(driverObj, ReadxmlData("paysafedata", "Paysafe_pin1", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin2", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin3", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin4", DataFilePath.Accounts_Wallets), acctData);
                    #endregion
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for PaySafe");
                }

            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_WesterUnion_Registration()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                AddTestCase("Verify the payment deposit with Cash method is successful", "Amount should be deposited to the selected wallet");
                try
                {

                    #region Prerequiste

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + loginData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + loginData.password, "");
                    baseIMS.Init();
                    #endregion
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.AddWesternUnion(baseIMS.IMSDriver, ReadxmlData("wudata", "WalletName", DataFilePath.Accounts_Wallets), "50");
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Registration scenario failed for Cash");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }

          //  [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Qiwi_Registration_Sports()
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


                AddTestCase("Validation of Qiwi registraion", "User should be able to register Qiwi card successfully");
                try
                {

                    #region Prerequiste
                    //AddTestCase("Prerequiste : Create customer from Sports Playtech page", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), "RU");
                    //       AnW.OpenRegistrationPage(driverObj);
                    //AnW.Registration_PlaytechPages(driverObj, ref regData);
                    regData.currency = ReadxmlData("regdata", "currency_USD", DataFilePath.Accounts_Wallets);
                    //  AnW.Registration_PlaytechPages(driverObj, ref regData);
                    //  Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.Login_Sports(driverObj, loginData);
                    #endregion


                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                    #endregion

                    String portalWindow = AnW.OpenCashier(driverObj);
                    #region Qiwi_Registration
                    acctData.depositWallet = ReadxmlData("qiwidata", "DepWallet", DataFilePath.Accounts_Wallets);
                    acctData.depositAmt = ReadxmlData("qiwidata", "DepAmt", DataFilePath.Accounts_Wallets);
                    AnW.Verify_QiWi_Registration(driverObj, ReadxmlData("qiwidata", "phone", DataFilePath.Accounts_Wallets), acctData);
                    #endregion
                    WriteUserName(regData.username);
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for Qiwi");
                }

            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Betcard_Registration()
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
                IWebDriver driverObj = null;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion

                try
                {
                    #region PrerequisteCustomer
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //      AnW.OpenRegistrationPage(driverObj);
                    regData.currency = "USD";
                    // AnW.Registration_PlaytechPages(driverObj, ref regData);
                    // Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion

                    #endregion
                    #region PrerequisteBetCard
                    AddTestCase("Create Bet card from OB", "New Bet card should be created successfully");
                    adminBase.Init();
                    string betcard = adminComm.Generate_Bet_Card(adminBase.MyBrowser);
                    Pass();
                    adminBase.Quit();
                    #endregion

                    AddTestCase("Bet Card :" + betcard, "successfully"); Pass();
                    String portalWindow = AnW.OpenCashier(driverObj);
                    #region BetCard Registration
                    acctData.depositWallet = ReadxmlData("bcdata", "DepWallet", DataFilePath.Accounts_Wallets);
                    acctData.depositAmt = ReadxmlData("bcdata", "DepAmt", DataFilePath.Accounts_Wallets);
                    AnW.Verify_BetCard_Registration(driverObj, acctData, betcard);
                    #endregion
                    WriteUserName(regData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(adminBase.MyBrowser, "Admin");
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for Bet card");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 6)]
            // [DependsOn("Verify_Betcard_Registration")]
            public void Verify_Withdraw_BetCard_Games()
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

                AddTestCase("Verify Withdraw is successful into betcard", "Withdraw should successful.");

                try
                {

                    if (!Usernames.ContainsKey("Verify_Betcard_Registration"))
                    {
                        UpdateThirdStatus("Not Run");
                        Fail("Dependent Test case \"Verify_Betcard_Registration\" failed");
                        return;
                    }

                    loginData.update_Login_Data(Usernames["Verify_Betcard_Registration"],
                        ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets),
                        "");
                  
                    acctData.Update_deposit_withdraw_Card("8964545646",
                         ReadxmlData("bcdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("bcdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("bcdata", "DepWallet", DataFilePath.Accounts_Wallets), ReadxmlData("bcdata", "DepWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    AnW.Withdraw_AnyPaymethod(driverObj, acctData);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");

                    Fail("Verify_Withdraw_BetCard_Games failed for exception");

                }

            }


           // [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Webmoney_Registration()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                AddTestCase("Validation of Webmoney registraion", "User should be able to register Webmoney card successfully");
                try
                {
                    #region Prerequiste
                    //AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //      AnW.OpenRegistrationPage(driverObj);
                    regData.currency = ReadxmlData("regdata", "currency_USD", DataFilePath.Accounts_Wallets);
                    // AnW.Registration_PlaytechPages(driverObj, ref regData);
                    // Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion


                    #endregion

                    String portalWindow = AnW.OpenCashier(driverObj);
                    #region Webmoney_Registration
                    acctData.depositWallet = ReadxmlData("qiwidata", "DepWallet", DataFilePath.Accounts_Wallets);
                    acctData.depositAmt = ReadxmlData("qiwidata", "DepAmt", DataFilePath.Accounts_Wallets);
                    AnW.Verify_Webmoney_Registration(driverObj, acctData);
                    #endregion
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for Web money");
                }
            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_MultiplePaymethods_Registration_USD()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                MyAcct_Data acctData = new MyAcct_Data();
                //Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj = null;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion


                AddTestCase("Adding two payment methods(Netteller,Webmoney) for a customer", "User should be able to add two payment methods successfully");
                try
                {

                    #region Prerequiste
                    // AddTestCase("Prerequiste : Create customer from Live Dealer Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //      AnW.OpenRegistrationPage(driverObj);
                    regData.currency = ReadxmlData("regdata", "currency_USD", DataFilePath.Accounts_Wallets);
                    // AnW.Registration_PlaytechPages(driverObj, ref regData);
                    // Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion

                    #endregion


                
                    #region Betcard_Registration
                            #region PrerequisteBetCard
                            AddTestCase("Create Bet card from OB", "New Bet card should be created successfully");
                            adminBase.Init();
                            string betcard = adminComm.Generate_Bet_Card(adminBase.MyBrowser);
                            Pass();
                            adminBase.Quit();
                            String portalWindow = AnW.OpenCashier(driverObj);
                            acctData.depositWallet = ReadxmlData("bcdata", "DepWallet", DataFilePath.Accounts_Wallets);
                            acctData.depositAmt = ReadxmlData("bcdata", "DepAmt", DataFilePath.Accounts_Wallets);
                            AnW.Verify_BetCard_Registration(driverObj, acctData, betcard);
                            #endregion

                    #endregion
                                wAction.BrowserClose(driverObj);
                                System.Threading.Thread.Sleep(2000);
                                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                                driverObj.Navigate().Refresh();
                                System.Threading.Thread.Sleep(5000);
                    AnW.OpenCashier(driverObj);
                    #region Neteller Registration
                    AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), true);
                    #endregion

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for Multiple payment methods");
                }

            }



        }//Banking2 class
      
    
    
        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Banking3 : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
            //  public Dictionary<string, string> Usernames = new Dictionary<string, string>();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
            SeamLessWallet sw = new SeamLessWallet();

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Deposit_MasterCard_ExchangeWallet()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion


                AddTestCase("Verify the Banking Links to deposit to Exchange wallet using credit card", "Amount should be deposited to the selected wallet");
                MyAcct_Data acctData = new MyAcct_Data();

                try
                {
                    Login_Data loginData = new Login_Data();

                    loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.Accounts_Wallets),
                  ReadxmlData("ccdata", "pwd", DataFilePath.Accounts_Wallets),
                  ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("ccdata", "CCard", DataFilePath.Accounts_Wallets),
                        ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("ccdata", "depWallet_ex", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("ccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);

                    AnW.DepositTOWallet_CC(driverObj, acctData);


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Deposit has failed");
                    //Pass();
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Deposit_VisaCard()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
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
                         ReadxmlData("vccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

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

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_PayPal_Registration_GBP()
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
                driverObj = browserInitialize(iBrowser);
                #endregion

                AddTestCase("Verify the payment deposit with pay pal method is successful", "Amount should be deposited to the selected wallet");
                try
                {

                    #region Prerequiste
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion
                    //       AnW.OpenRegistrationPage(driverObj);
                    // AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    #endregion

                    acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "user_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));
                    WriteUserName(regData.username);
                    AnW.Register_Paypal(driverObj, acctData);
                   
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for Paypal");
                }

            }

            [Test(Order = 4)]
            [Timeout(2200)]
            //  [Parallelizable]
            [RepeatOnFail]
            //[DependsOn(Verify_PayPal_Registration_GBP)]
            public void Verify_Paypal_Deposit()
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
                driverObj = browserInitialize(iBrowser);
                #endregion

                AddTestCase("Verify the payment deposit with pay pal method is successful", "Amount should be deposited to the selected wallet");
                try
                {

                    #region Prerequiste
                    //AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    //regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AnW.Registration_PlaytechPages(driverObj, ref regData);
                    //Pass("Customer registered succesfully");

                    // WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    // AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    // Pass();
                    #endregion

                    // Usernames.Add("Verify_PayPal_Registration","Useruohypawz");

                    if (!Usernames.ContainsKey("Verify_PayPal_Registration_GBP"))
                    {
                        UpdateThirdStatus("Not Run");
                        Fail("Dependent Test case \"Verify_PayPal_Registration\" failed");
                        return;
                    }


                    loginData.update_Login_Data(Usernames["Verify_PayPal_Registration_GBP"],
                                 ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets),
                                  ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));
                    acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                            ReadxmlData("paydata", "user_pwd", DataFilePath.Accounts_Wallets),
                            ReadxmlData("paydata", "CCAmt", DataFilePath.Accounts_Wallets),
                             ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    AnW.Deposit_Cust_Paypal(driverObj, acctData);




                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Paypal Deposit scenario failed");
                    //Pass();
                }

            }

            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 5)]
            // [DependsOn("Verify_PayPal_Registration_GBP")]
            public void Verify_Withdraw_Paypal_Sports()
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

                if (!Usernames.ContainsKey("Verify_PayPal_Registration_GBP"))
                {
                    UpdateThirdStatus("Not Run");
                    Fail("Dependent Test case \"Verify_PayPal_Registration_GBP\" failed");
                    return;
                }

                AddTestCase("Verify Withdraw is successful", "Withdraw should successful.");

                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(Usernames["Verify_PayPal_Registration_GBP"],
                        ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                         ReadxmlData("paydata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("paydata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    AnW.Withdraw_AnyPaymethod(driverObj, acctData);

                    Pass();

                    WriteUserName(loginData.username);
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Withdraw_Paypal_Sports failed");

                }

            }




            [RepeatOnFail]
            [Timeout(1500)]
            [Test(Order = 6)]
            [Parallelizable]
            //Verify the Deposit type bonus /promotion 
            public void Verify_Deposit_Netteller()
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

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
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


            /// <summary>
            /// Verify the deposit limit is forced to set at the time of deposit when existing customer tries to deposit
            /// Author: Anusha
            /// </summary>
            [RepeatOnFail]
            [Timeout(1500)]
            [Test(Order = 7)]
            [Parallelizable]
            public void Verify_DepositLimit_Error()
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

                    AddTestCase("Verify customer is forced to set deposit limit", "Customer should be prompted with a error message to set deposit deposit limit");

                    baseIMS.Init();
                    AddTestCase("Created customer in IMS admin without setting deposit limit", "Customer should be created successfully");
                    loginData.username = null;
                    loginData.password = ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets);

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), null);
                   
                    loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, loginData.password);
                 

                    Thread.Sleep(10000);

                    acctData.cardCSC = ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets);
                    //WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    BaseTest.Pass("Customer created successfully in IMS");

                    AddTestCase("Logged in using the newly created customer", "Customer should be logged in successfully");
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    BaseTest.Pass();
                    AnW.Deposit_LimitUnset(driverObj, acctData);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Deposit scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 8)]
            // [Parallelizable]
            public void Verify_Withdraw_Netteller_Gaming()
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), null);

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


            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 9)]
            public void Verify_Withdraw_Portal_CreditCard()
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

                    loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("ccdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("ccdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("ccdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("ccdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("ccdata", "wWallet", DataFilePath.Accounts_Wallets), ReadxmlData("ccdata", "wWallet", DataFilePath.Accounts_Wallets),
                      ReadxmlData("ccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    AnW.Withdraw_CC(driverObj, acctData, ReadxmlData("depdata", "TransWalletDropDown", DataFilePath.Accounts_Wallets));
                    WriteUserName(loginData.username);
                    Pass();

                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Withdraw for credit card is failed for exception");

                }

            }


            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 10)]            
            public void Verify_CancelWIthdrawal_Portal()
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

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData);
                    AnW.Cancel_Withdraw_Netteller(driverObj, acctData);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");

                    Fail("Cancel Withdraw for netteller is failed for exception");

                }

            }

            [Test(Order = 11)]
            [Parallelizable]
            [Timeout(1500)]
            [RepeatOnFail]
            public void Verify_LiveDealer_Withdrawal_Limit()
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
                    loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.Accounts_Wallets), ReadxmlData("depLimt", "pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depLimt", "Fname", DataFilePath.Accounts_Wallets));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    cmn.EnterAmount_Withdraw(driverObj);
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Payment method for Credit card is failed for exception");
                }
            }

            [Test(Order = 12)]
            [Parallelizable]
            [Timeout(1500)]
            [RepeatOnFail]
            public void Verify_LiveDealer_WithdrawalMoreThanBalance()
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
                    loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.Accounts_Wallets), ReadxmlData("depLimt", "pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depLimt", "Fname", DataFilePath.Accounts_Wallets));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    string balAmount = cmn.HomePage_Balance(driverObj);
                    cmn.EnterAmount_ToWithdrawMoreThanBalance(driverObj, ReadxmlData("depLimt", "depWallet", DataFilePath.Accounts_Wallets));
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_LiveDealer_WithdrawalMoreThanBalance failed");

                }
            }

          


            /// <summary>
            ///  Verify if the approved request from non approved WD request are displayed in the transaction history 
            ///  Verify if the non approved withdraw request can be approved from IMS admin 
            ///  Author :Anusha
            /// </summary>
            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 13)]
            // [Parallelizable]
           //  [DependsOn("Verify_Withdraw_Netteller_Gaming")]
            public void Verify_Withdraw_Approval_IMS()
            {
            

                #region Declaration


                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Registration_Data regData = new Registration_Data();
                IMS_AdminSuite.IMS_Base IMSBase = new IMS_Base();
                IMS_AdminSuite.Common IMSComm = new IMS_AdminSuite.Common();
                #endregion

              //  Usernames["Verify_Withdraw_Netteller_Gaming"] = "Userupeadrahan";
                //if (!Usernames.ContainsKey("Verify_Withdraw_Netteller_Gaming"))
                //{
                //    UpdateThirdStatus("Not Run");
                //    Fail("Dependent Test case \"Verify_Withdraw_Netteller_Gaming\" failed");
                //    return;
                //}

                loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                    ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                 ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets), null);

                AddTestCase("Verify non approved WD request are displayed in the transaction history ", "Non approved WD requests should be displayed in transaction history");
                try
                {
                    //AddTestCase("Verify Netteller withdraw", "Withdraw should be successful");

                    loginData.update_Login_Data(loginData.username,
                        loginData.password,
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));
                    //loginData.username = "Useruohsrazae";
                    //loginData.password = "Password1";

                    //acctData.Update_deposit_withdraw_Card(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                    //     ReadxmlData("paydata", "CCAmt", DataFilePath.Accounts_Wallets),
                    //     ReadxmlData("paydata", "CCAmt", DataFilePath.Accounts_Wallets),
                    // ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    IMSBase.Init();
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    //IMSComm.Approve_Withdraw_Request(IMSBase.IMSDriver);
                    //IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    //IMSComm.Verify_Withdraw_Approval(IMSBase.IMSDriver);
                   
                    IMSComm.Approve_Withdraw_Request(IMSBase.IMSDriver);
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    IMSComm.Approve_WithdrawRequest(IMSBase.IMSDriver, loginData.username);

                    Pass();
                }
                catch (Exception e)
                {

                    exceptionStack(e);
                  
                    CaptureScreenshot(IMSBase.IMSDriver, "IMS");
                    Fail("Verify_Withdraw_Approval_IMS failed");
                }
                finally
                {
                    IMSBase.Quit();
                }
            }



            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 14)]
            public void Verify_Withdraw_Exchange()
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     "Exchange", "Exchange", null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");

                    Fail("Withdraw from Exchange is failed for exception");

                }

            }

            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 15)]
            public void Verify_Withdraw_Games()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     "Games", "Games", null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Withdraw from Games is failed for exception");

                }

            }

         



        }//Banking3 class

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Banking4 : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
            //  public Dictionary<string, string> Usernames = new Dictionary<string, string>();
            Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();


            /// <summary>
            ///  Deposit Limit reduced in portal banking screen after the successful deposit
            ///  Author :Anusha
            /// </summary>
            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 12)]
            // [Parallelizable]
            public void Verify_Reduced_DepLimit()
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
                try
                {
                    AddTestCase("Verify deposit Limit reduced in portal banking screen after the successful deposit", "Deposit limit should be reduced based on the amount deposited");

                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    //AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets));
                    String portalWindow = AnW.OpenCashier(driverObj);
                    AnW.CheckDepLimit_Netteller(driverObj, acctData);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Deposit limit verification failed");

                }

            }


          


            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 17)]
            // [Parallelizable]
            public void Verify_Withdraw_TextBox_Validation()
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

                AddTestCase("Verify unsuccessful withdrawal of funds by entering invalid data & blank fields on withdraw page", "Validation should be successful.");

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

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    AnW.Withdraw_TextBoXValidation(driverObj, acctData);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Check incorrect input message on Withdraw is failed for exception");

                }

            }

            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 18)]
            public void Verify_Withdraw_Portal_CreditCard_Nelson()
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

                AddTestCase("Verify Withdraw is successful from Nelson thru CC", "Withdraw should successful.");

                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("ccdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("ccdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("ccdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("ccdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("ccdata", "depWallet_ex", DataFilePath.Accounts_Wallets), ReadxmlData("ccdata", "depWallet_ex", DataFilePath.Accounts_Wallets),
                      ReadxmlData("ccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_CC(driverObj, acctData,acctData.withdrawWallet);
                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Withdraw_Portal_CreditCard_Ecom failed for exception");

                }

            }

            /// <summary>
            ///  Author :Anand C
            ///  Verify successful withdrawal of funds when the withdrawal block is applied to an account - GEN-1295
            /// </summary>
            [Test(Order = 19)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_WithdrawBlock_IMS()
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
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    //regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    //      AnW.OpenRegistrationPage(driverObj);
                    //AnW.Registration_PlaytechPages(driverObj, ref regData);
                    // Pass("Customer registered succesfully");
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion



                    string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion


                    String portalWindow = AnW.OpenCashier(driverObj);
               
                        #region Neteller Registration
                        AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets));
                        #endregion
                       BaseTest.AddTestCase("Apply withdrawaleblock for customer in IMS Admin", "Should be able to apply withdrawaleblock");
                    baseIMS.Init();
                    //regData.username = "Useruohxmapbe";
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.DisableWithdrawPayMethod(baseIMS.IMSDriver, "NETeller");
                    BaseTest.Pass("Withdrawaleblock applied for customer in IMS successfully");
                    AnW.Verify_WithdrawalBlock(driverObj);
                    BaseTest.Pass();
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Unable to add Withdraw block");
                }
                finally { baseIMS.Quit(); }

            }

            /// <summary>
            ///  Author :Anand C
            ///  Verify withdrawal from a self excluded customer account - GEN-3152
            /// </summary>
            [Test(Order = 20)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_WithdrawFromSelfExcluded_IMS()
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
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    //       AnW.OpenRegistrationPage(driverObj);
                    //   AnW.Registration_PlaytechPages(driverObj, ref regData);
                    //  Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion


                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                    #endregion

                   String portalWindow = AnW.OpenCashier(driverObj);
                        #region Neteller Registration
                        AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets));
                        driverObj.Close();
                        driverObj.SwitchTo().Window(portalWindow);
                        wAction.PageReload(driverObj);
                        AnW.LogoutFromPortal(driverObj);
                        #endregion
                    BaseTest.AddTestCase("Self exclude a customer in IMS Admin", "Should be able to self exclude a customer in IMS");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);
                    commIMS.WithdrawNetellerFromIMS(baseIMS.IMSDriver, 10, ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets));

                    BaseTest.Pass();
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    baseIMS.Quit();
                    Fail("Verify_WithdrawFromSelfExcluded_IMS failed");
                }

                finally { baseIMS.Quit(); }
            }


            /// <summary>
            ///  Author :Naga
            ///  Verify if customer is able to deposit more than set deposit limit
            ///  PNG-163
            /// </summary>
            [Test(Order = 21)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_DepositLimit_Portal()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
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
                    loginData.update_Login_Data(ReadxmlData("depLimt", "user", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depLimt", "pwd", DataFilePath.Accounts_Wallets_stg),
                        ReadxmlData("depLimt", "Fname", DataFilePath.Accounts_Wallets_stg));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.DepositMax_Netteller(driverObj, ReadxmlData("depLimt", "Amnt", DataFilePath.Accounts_Wallets_stg), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets_stg));
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_DepositLimit_Portal is not functioning correctly");
                }
            }


            /// <summary>
            /// Roopa
            /// Verify the Payment request details in the event log
            /// </summary>
            [Test(Order = 22)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_PaymentRequest_Eventlog_IMS()
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
                AddTestCase("Verify Payment request details in the event log", "Payment request details is not displayed correctly");

                try
                {
                    #region prereq
                    if (!Usernames.ContainsKey("Verify_Deposit_VisaCard"))
                    {
                        UpdateThirdStatus("Not Run");
                        Fail("Dependent Test case \"Verify_Deposit_VisaCard\" failed");
                        return;
                    }
                    #endregion
                    baseIMS.Init();
                    AddTestCase("Username:" + Usernames["Verify_Deposit_VisaCard"], "");
                    Pass();
                    commIMS.Verify_CustomerDepositStatus_IMS(baseIMS.IMSDriver, Usernames["Verify_Deposit_VisaCard"]);
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_PaymentRequest_Eventlog_IMS : Failed");

                }
                finally
                {
                    baseIMS.Quit();
                }
                Pass("Verify_PaymentRequest_Eventlog_IMS : Pass");

            }


        }//Banking3 class
    }

