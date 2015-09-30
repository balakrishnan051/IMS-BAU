using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using Framework;
using System.Threading;
using OpenQA.Selenium;
using TestRepository;
using AdminSuite;
using IMS_AdminSuite;
using Selenium;
using ICE.DataRepository;
using ICE.ActionRepository;
using ICE.ObjectRepository;
using ICE.DataRepository.Vegas_IMS_Data;

[assembly: ParallelismLimit]
namespace Regression_AnW_Suite2
{
    [AssemblyFixture]
    class Regression_AnW_Suite2 
    {
       

        [FixtureTearDown]
        public void AfterRunAssembly()
        {
            BaseTest.EndOfExecution();
        }

      

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Banking_PayMethods : BaseTest
        {
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
            TestRepository.Common Testcomm = new TestRepository.Common();
            //  public Dictionary<string, string> Usernames = new Dictionary<string, string>();


            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Netteller_Registration_EUR_Ecom()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion

                AddTestCase("Validation of Netteller registraion for Euro currency", "User should be able to register Netteller card successfully");
                try
                {

                    #region Prerequiste
                   // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    //wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "EcomJoin_btn", Keys.Enter, "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    #endregion

                    //Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    #region Neteller Registration
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    try
                    {
                        if (wAction._IsElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk"))
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", 0, false);
                        else
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Banking_lnk", "Banking Link not found", 0, false);

                        wAction.WaitAndMovetoPopUPWindow(driverObj, "Banking window not found", FrameGlobals.elementTimeOut);
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }
                    AccountAndWallets.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets));
                    
                    #endregion
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Netteller_Registration_EUR for exception");
                }

                
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Netteller_Registration_USD()
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

                AddTestCase("Validation of Netteller registraion for Euro currency", "User should be able to register Netteller card successfully");
                try
                {
                    #region Prerequiste
                   // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_USD", DataFilePath.Accounts_Wallets);
                   // wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                   // AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    //Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    #region Neteller Registration
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction.WaitAndMovetoPopUPWindow(driverObj, "Banking window not found", FrameGlobals.elementTimeOut);
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }
                    AccountAndWallets.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets));

                    #endregion

                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Netteller_Registration_USD for exception");
                }
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_PayPal_Registration_EUR()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
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
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                   // Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "user_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));

                    AccountAndWallets.Register_Paypal(driverObj, acctData);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    WriteUserName(regData.username);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_PayPal_Registration_EUR failed ");
                }

            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_PayPal_Registration_USD_Ecom()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,FrameGlobals.EcomURL);
                #endregion

                AddTestCase("Verify the payment deposit with pay pal method is successful", "Amount should be deposited to the selected wallet");
                try
                {
                    #region Prerequiste
                   // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_USD", DataFilePath.Accounts_Wallets);
                    //wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "EcomJoin_btn", Keys.Enter, "Join button not found", 0, false);
                   // AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                   // Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    #endregion


                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "user_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "Amt_USD", DataFilePath.Accounts_Wallets),
                         ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));

                    AccountAndWallets.Register_Paypal_Ecom(driverObj, acctData);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    WriteUserName(regData.username);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for Paypal");
                }

            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Master_Card_Registration_EUR()
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

                AddTestCase("Validation of credit card registraion for Euro Currency", "User should be able to register credit card successfully");
                try
                {


                    #region Prerequiste
                    //AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                   // wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    //Pass("Customer registered succesfully");

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion

                 
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction.WaitAndMovetoPopUPWindow(driverObj, "Banking window not found", FrameGlobals.elementTimeOut);
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }

                    AccountAndWallets.Verify_Credit_Card_Registration(driverObj, creditCardNumber,"MasterCard");
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Credit Card registration failed for exception");
                }
               
            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Master_Card_Registration_USD_Ecom()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion

                AddTestCase("Validation of credit card registraion for Euro Currency", "User should be able to register credit card successfully");
                try
                {


                    #region Prerequiste
                   // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                   // wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "EcomJoin_btn",Keys.Enter, "Join button not found", 0, false);
                   // AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                  //  Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    #endregion

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion


                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        //wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        //wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        if (wAction._IsElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk"))
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", 0, false);
                        else
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Banking_lnk", "Banking Link not found", 0, false);

                       // wAction.Click(driverObj, By.LinkText("Banking"), "Banking link not found");
                        wAction.WaitAndMovetoPopUPWindow(driverObj, "Banking window not found", FrameGlobals.elementTimeOut);
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }

                    AccountAndWallets.Verify_Credit_Card_Registration(driverObj, creditCardNumber,"MasterCard");
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Credit Card registration failed for exception");
                }

            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_MultiplePaymethods_Registration_EUR()
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


                AddTestCase("Adding two payment methods(Paysafe and Netteller) for a customer", "User should be able to add two payment methods successfully");
                try
                {

                    #region Prerequiste
                   // AddTestCase("Prerequiste : Create customer from Live Dealer Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                   // AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                   // Pass("Customer registered succesfully");

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        if (wAction._IsElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk"))
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", 0, false);
                        else
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Banking_lnk", "Banking Link not found", 0, false);

                        //wAction.Click(driverObj, By.LinkText("Banking"), "Unable to click on Banking link", 0, false);

                        commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate Banking page");
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }
                    #region Paysafe Registration
                    acctData.depositWallet = ReadxmlData("paysafedata", "DepWallet", DataFilePath.Accounts_Wallets);
                    acctData.depositAmt = ReadxmlData("paysafedata", "DepAmt", DataFilePath.Accounts_Wallets);
                    AccountAndWallets.Verify_PaySafe_Registration(driverObj, ReadxmlData("paysafedata", "Paysafe_pin1", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin2", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin3", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin4", DataFilePath.Accounts_Wallets), acctData);
                    #endregion

                    wAction.BrowserClose(driverObj);
                    System.Threading.Thread.Sleep(2000);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                    driverObj.Navigate().Refresh();
                    System.Threading.Thread.Sleep(5000);
                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        if (wAction._IsElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk"))
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", 0, false);
                        else
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Banking_lnk", "Banking Link not found", 0, false);
                        //wAction.Click(driverObj, By.LinkText("Banking"), "Unable to click on Banking link", 0, false);

                        commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate Banking page");
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }

                    #region Neteller Registration
                    AccountAndWallets.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), true);
                    #endregion

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for Multiple payment methods EUR");
                }

            }

            [Test(Order = 8)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_MultiplePaymethods_Registration_GBP()
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

                AddTestCase("Adding two payment methods(Paysafe and Netteller) for a customer", "User should be able to add two payment methods successfully");
                try
                {
                    #region Prerequiste
                   // AddTestCase("Prerequiste : Create customer from Live Dealer Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                   // wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                   // AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                   // Pass("Customer registered succesfully");

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    #endregion

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        //wAction.Click(driverObj, By.LinkText("Banking"), "Unable to click on Banking link", 0, false);

                        commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate Banking page");
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }
                    #region Creditcard Registration
                    string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                    AccountAndWallets.Verify_Credit_Card_Registration(driverObj, creditCardNumber, "MasterCard");
                    #endregion

                    wAction.BrowserClose(driverObj);
                    System.Threading.Thread.Sleep(2000);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                    driverObj.Navigate().Refresh();
                    System.Threading.Thread.Sleep(5000);

                    #region Paypal Registration

                    acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "user_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("paydata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));

                    AccountAndWallets.VerifyRegister_Paypal(driverObj, acctData);

                    #endregion

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Registration scenario failed for Multiple payment methods EUR");
                }

            }

            [Test(Order = 9)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_MasterCard_Registration_GBP()
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

                AddTestCase("Validation of credit card registraion for Euro Currency", "User should be able to register credit card successfully");
                try
                {
                    #region Prerequiste
                   // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    //regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                   // wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                   // Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion



                    string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion


                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction.WaitAndMovetoPopUPWindow(driverObj, "Banking window not found", FrameGlobals.elementTimeOut);
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }

                    AccountAndWallets.Verify_Credit_Card_Registration(driverObj, creditCardNumber, "MasterCard");
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Credit Card registration failed for exception");
                }

            }

            [Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(1500)]
            /// <summary>
            /// Author:Anand C
            /// Registration of customer pay method as MoneyBooker (Skrill) - GEN-5400
            /// Verify Deposit to Games wallet by Money Booker  as a pay method - GEN-3211
            /// Date: 11/09/2014
            /// </summary>
            public void Verify_Skrill_Registration_GBP()
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

                AddTestCase("Validation of Skrill registraion for GBP Currency", "User should be able to register Skrill successfully");
                try
                {
                    #region Prerequiste
                   
                    //AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    //regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                   // wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                   // Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    #endregion

                    /*loginData.update_Login_Data("Useruojjmahav", "Password1", ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);*/

                    #region Skrill Registration
                    acctData.Update_deposit_skrill_account(
                        ReadxmlData("skrilldata", "user_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("skrilldata", "user_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("skrilldata", "DepAmt", DataFilePath.Accounts_Wallets),
                        ReadxmlData("skrilldata", "WalletName", DataFilePath.Accounts_Wallets));

                    AccountAndWallets.VerifyRegister_Skrill(driverObj, acctData);
                    #endregion

                    #region Skrill Deposit
                    System.Threading.Thread.Sleep(2000);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                    driverObj.Navigate().Refresh();
                    System.Threading.Thread.Sleep(5000);
                    AccountAndWallets.Deposit_Cust_Skrill(driverObj, acctData);
                    System.Threading.Thread.Sleep(1000);
                    WriteUserName(regData.username);
                    #endregion

                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Skrill registration failed for exception");
                }

            }


            [Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(1500)]
            /// <summary>
            /// Author:Anand C
            /// Verify successful withdrawal into MoneyBooker (Skrill) pay method - GEN-5473            
            /// Date: 11/09/2014
            /// </summary>
            //[DependsOn("Verify_Skrill_Registration_GBP")]
            public void Verify_WIthdrawal_Skrill()
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

                #region Prerequiste
                if (!Usernames.ContainsKey("Verify_Skrill_Registration_GBP"))
                {
                    UpdateThirdStatus("Not Run");
                    Fail("Dependent Test case \"Verify_Skrill_Registration_GBP\" failed");
                    return;
                }
                #endregion

                AddTestCase("Verify successful withdrawal into MoneyBooker (Skrill) pay method", "Withdraw should be successful.");
                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);

                    loginData.update_Login_Data(Usernames["Verify_Skrill_Registration_GBP"], Registration_Data.password,
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    #region Skrill Deposit
                    acctData.Update_deposit_skrill_account(
                        ReadxmlData("skrilldata", "user_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("skrilldata", "user_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("skrilldata", "DepAmt", DataFilePath.Accounts_Wallets),
                        ReadxmlData("skrilldata", "WalletName", DataFilePath.Accounts_Wallets));
                    AccountAndWallets.Withdraw_Skrill(driverObj, acctData);
                    #endregion
                    Pass();
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_WIthdrawal_Skrill failed");
                }
            }

            [Test(Order = 12)]
            [RepeatOnFail]
            [Timeout(1500)]
            /// <summary>
            /// Author:Anand C
            /// Registration of customer pay method as EuTeller - GEN-5396            
            /// Date: 11/09/2014
            /// </summary>
            public void Verify_EuTeller_Registration_FI()
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

                AddTestCase("Validation of Skrill registraion for GBP Currency", "User should be able to register Skrill successfully");
                try
                {
                    #region Prerequiste
                    //AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Finland", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_finland", DataFilePath.Accounts_Wallets));
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    //Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    #endregion

                    /*loginData.update_Login_Data("Useruojluarat", "Password1", ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);*/

                    #region EuTeller Registration
                    acctData.depositWallet = ReadxmlData("genPaydata", "WalletName", DataFilePath.Accounts_Wallets);
                    acctData.depositAmt = ReadxmlData("genPaydata", "DepAmt", DataFilePath.Accounts_Wallets);
                    AccountAndWallets.VerifyRegister_Euteller(driverObj, acctData);
                    #endregion

                    #region EuTeller Deposit
                    System.Threading.Thread.Sleep(2000);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                    driverObj.Navigate().Refresh();
                    System.Threading.Thread.Sleep(5000);
                    AccountAndWallets.Deposit_Cust_Euteller(driverObj, acctData);
                    System.Threading.Thread.Sleep(1000);
                    #endregion

                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("EuTeller registration failed for exception");
                }

            }

            [Test(Order = 13)]
            [RepeatOnFail]
            [Timeout(1500)]
            /// <summary>
            /// Author:Naga
            /// Registration of customer pay method as Entropay - GEN-5386            
            /// Date: 15/09/2014
            /// </summary>
            public void Verify_EntroPay_Card_Registration()
            {

                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
                string creditCardNumber = null;
                #endregion
                #region DriverInitiation
                IWebDriver driverObj = null;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion


                AddTestCase("Registration of customer pay method as Entropay", "User should be able to register credit card successfully");
                try
                {
                    #region Prerequiste
                    //AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_uganda", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_uganda", DataFilePath.Accounts_Wallets));

                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                   // AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets);
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                   // Pass("Customer registered succesfully");
                    creditCardNumber = ReadxmlData("epdata", "CCard", DataFilePath.Accounts_Wallets);
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion


                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction.WaitAndMovetoPopUPWindow(driverObj, "Banking window not found", FrameGlobals.elementTimeOut);
                    
                    AccountAndWallets.Verify_Credit_Card_Registration(driverObj, creditCardNumber,"EntroPay");
                    driverObj.Close();
                    wAction.WaitAndMovetoPopUPWindow(driverObj, portalWindow, "Main window not found");
                    wAction.PageReload(driverObj);
                    AccountAndWallets.DepositTOWallet_CC(driverObj, acctData);
                  
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    //  CaptureScreenshot(baseIMS.IMSDriver, "IMS");                  
                    Fail("Credit Card registration failed for exception");
                }
                finally
                {
                    try
                    {
                        baseIMS.Init();
                        commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                        commIMS.AllowDuplicateCreditCard_FullNavigation_Newlook(baseIMS.IMSDriver, creditCardNumber.Substring(0, 6), creditCardNumber.Substring(creditCardNumber.Length - 4, 4));
                    }
                    catch (Exception e)
                    {
                        CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                        throw e;
                    }

                    finally
                    {
                        baseIMS.Quit();
                    }
                }


            }

          
            [Test(Order = 14)]
            [RepeatOnFail]
            [Timeout(1500)]
            /// <summary>
            /// Author:Anand C
            /// Registration of customer pay method as eNets - GEN-5385
            /// Date: 11/09/2014
            /// </summary>
            public void Verify_eNets_Registration_SGD()
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

                AddTestCase("Validation of eNets registraion for SGD Currency", "User should be able to register eNets successfully");
                try
                {
                    #region Prerequiste
                    //   AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Singapore", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_singapore", DataFilePath.Accounts_Wallets));
                    regData.currency = "SGD";

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    // wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //  AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    // Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    /*loginData.update_Login_Data("test_aditi_CNAZAJPH", "Lbr1234", ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);*/

                    #region eNets Registration
                    acctData.Update_deposit_skrill_account(
                        ReadxmlData("eNetsdata", "user_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("eNetsdata", "user_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("eNetsdata", "DepAmt", DataFilePath.Accounts_Wallets),
                        ReadxmlData("eNetsdata", "WalletName", DataFilePath.Accounts_Wallets));

                    AccountAndWallets.VerifyRegister_eNets(driverObj, acctData);
                    #endregion

                    #region eNets Deposit
                    System.Threading.Thread.Sleep(2000);
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                    driverObj.Navigate().Refresh();
                    System.Threading.Thread.Sleep(5000);
                    AccountAndWallets.Deposit_Cust_Skrill(driverObj, acctData);
                    System.Threading.Thread.Sleep(1000);
                    #endregion
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("eNets registration failed for exception");
                }

            }



            /// <summary>
            /// Verify customer is able to add credit/ debit as payment method in the first deposit page
            /// Naga
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 15)]
            [Parallelizable]
            public void Verify_FirstDeposit_CreditCard()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
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
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    // AnW.Registration_PlaytechPages(driverObj, ref regData,0,false, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));

                    commTest.PP_Registration(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();

                    acctData.Update_deposit_withdraw_Card(TD.createCreditCard("MasterCard").ToString(),
                     ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("ccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets)
                    ,ReadxmlData("ccdata", "CVV", DataFilePath.Accounts_Wallets));

                    wAction.WaitAndMovetoFrame(driverObj, By.Id("_cashier2iframe_WAR_cashierportlet_cashier2"));
                    BaseTest.AddTestCase("Verify customer is able to add credit/ debit as payment method in the first deposit page", "First deposit should be successful");
                    Assert.IsTrue(AnW.Verify_FirstCashier_MasterCard(driverObj, acctData), "First Deposit amount not added to the wallet");
                    BaseTest.Pass("successfully verified");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_FirstDeposit_CreditCard - failed");
                }

            }


            [Test(Order = 16)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Sofort_Registration_EUR()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,FrameGlobals.VegasURL);
                #endregion

                AddTestCase("Verify the payment deposit with Sofort method is successful", "Amount should be deposited to the selected wallet");
                try
                {

                    #region Prerequiste
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_Germany", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    // Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    acctData.Update_deposit_sofort_account(ReadxmlData("sofdata", "Acct", DataFilePath.Accounts_Wallets),
                        ReadxmlData("sofdata", "PIN", DataFilePath.Accounts_Wallets),ReadxmlData("sofdata", "code", DataFilePath.Accounts_Wallets),
                        ReadxmlData("sofdata", "depAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("sofdata", "depWallet", DataFilePath.Accounts_Wallets));



                    AccountAndWallets.Register_Sofort(driverObj, acctData);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    WriteUserName(regData.username);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sofort_Registration_EUR failed ");
                }

            }


            [RepeatOnFail]
            [Timeout(1500)]
            [Test(Order = 17)]  
            //[DependsOn(Verify_Sofort_Registration_EUR)]            
            public void Verify_Sofort_Deposit_EUR()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

              //  Usernames.Add("Verify_Sofort_Registration_EUR", "Userupbgtakx");
                try
                {

                    AddTestCase("Verify Netteller deposit is successful", "Deposit through netteller should be successful.");

                    if (!Usernames.ContainsKey("Verify_Sofort_Registration_EUR"))
                    {
                        UpdateThirdStatus("Not Run");
                        Fail("Dependent Test case \"Verify_Sofort_Registration_EUR\" Failed");
                        return;
                    }

                    loginData.update_Login_Data(Usernames["Verify_Sofort_Registration_EUR"],
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets),
                        "");

                    acctData.Update_deposit_sofort_account(ReadxmlData("sofdata", "Acct", DataFilePath.Accounts_Wallets),
                        ReadxmlData("sofdata", "PIN", DataFilePath.Accounts_Wallets), ReadxmlData("sofdata", "code", DataFilePath.Accounts_Wallets),
                        ReadxmlData("sofdata", "depAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("sofdata", "depWallet", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    AccountAndWallets.Register_Sofort(driverObj, acctData);

                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Deposit bonus scenario failed");
                }
            }

            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 18)]
            //[DependsOn(Verify_Sofort_Registration_EUR)]
            public void Verify_Withdraw_Sofort_Gaming()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion

                #region Declaration


                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //// Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify Withdraw to Envoy pay method (Sofort) is successful", "Withdraw should be successful.");

                Usernames.Add("Verify_Sofort_Registration_EUR", "Userupbjmanbc");
                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);
                    if (!Usernames.ContainsKey("Verify_Sofort_Registration_EUR"))
                    {
                        UpdateThirdStatus("Not Run");
                        Fail("Dependent Test case \"Verify_Sofort_Registration_EUR\" Failed");
                        return;
                    }

                    loginData.update_Login_Data(Usernames["Verify_Sofort_Registration_EUR"],
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets),
                        "");


                    acctData.Withdraw_Sofort(ReadxmlData("sofdata", "wPayee", DataFilePath.Accounts_Wallets),
                         ReadxmlData("sofdata", "wBankName", DataFilePath.Accounts_Wallets),
                         ReadxmlData("sofdata", "wIBAN", DataFilePath.Accounts_Wallets),
                     ReadxmlData("sofdata", "wSWIFTBIC", DataFilePath.Accounts_Wallets), 
                     ReadxmlData("sofdata", "wCountry", DataFilePath.Accounts_Wallets), 
                     ReadxmlData("sofdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("sofdata", "depWallet", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    AccountAndWallets.Withdraw_Sofort(driverObj, acctData);
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
            

            [Test(Order = 18)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_GiroPay_Registration_EUR()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                AddTestCase("Verify the payment deposit with Giropay method is successful", "Amount should be deposited to the selected wallet");
                try
                {

                    #region Prerequiste
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_Germany", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    // Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    acctData.Update_deposit_giropay_account(ReadxmlData("girodata", "SwiftBIC", DataFilePath.Accounts_Wallets),
                        ReadxmlData("girodata", "sc", DataFilePath.Accounts_Wallets),
                        ReadxmlData("girodata", "esc", DataFilePath.Accounts_Wallets), 
                        ReadxmlData("girodata", "depAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("girodata", "depWallet", DataFilePath.Accounts_Wallets));



                    AccountAndWallets.Register_Giropay(driverObj, acctData);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    WriteUserName(regData.username);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_GiroPay_Registration_EUR failed ");
                }

            }


            [Test(Order = 19)]
            [RepeatOnFail]
            [Timeout(1500)]
            //[DependsOn("Verify_GiroPay_Registration_EUR")]
            public void Verify_GiroPay_Deposit_EUR()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                AddTestCase("Verify the payment deposit with Giropay method is successful", "Amount should be deposited to the selected wallet");
                try
                {
                    
                    #region Prerequiste
                    // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_Germany", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                    //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    // Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    acctData.Update_deposit_giropay_account(ReadxmlData("girodata", "SwiftBIC", DataFilePath.Accounts_Wallets),
                        ReadxmlData("girodata", "sc", DataFilePath.Accounts_Wallets),
                        ReadxmlData("girodata", "esc", DataFilePath.Accounts_Wallets), 
                        ReadxmlData("girodata", "depAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("girodata", "depWallet", DataFilePath.Accounts_Wallets));
                  //  Usernames.Add("Verify_GiroPay_Registration_EUR", "Userupbisaeax");
                    
                    if (!Usernames.ContainsKey("Verify_GiroPay_Registration_EUR"))
                    {
                        UpdateThirdStatus("Not Run");
                        Fail("Dependent Test case \"Verify_GiroPay_Registration_EUR\" Failed");
                        return;
                    }

                    loginData.update_Login_Data(Usernames["Verify_GiroPay_Registration_EUR"],
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets),
                        "");

                    AccountAndWallets.Register_Giropay(driverObj, acctData);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    WriteUserName(regData.username);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_GiroPay_Registration_EUR failed ");
                }

            }

            [Test(Order = 18)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Ideal_Registration_EUR()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                AddTestCase("Registration of customer pay method as Ideal", "Customer should be directed to the Third party site & Payment method should be registered to the customer account & Deposited successfully");
                try
                {

                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Netherlands", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_Netherland", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                  
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    // Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    #endregion

                    acctData.Update_deposit_Ideal_account(
                        ReadxmlData("idealdata", "depAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("idealdata", "depWallet", DataFilePath.Accounts_Wallets));


                    AccountAndWallets.Register_Ideal(driverObj, acctData);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);

                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();
                    WriteUserName(regData.username);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Ideal_Registration_EUR failed ");
                }

            }


        }//Paymethods
        

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Bonus : BaseTest
        {
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
            
            /// <summary>
            /// Roopa
            /// Verify the Sign up bonus 
            /// </summary>
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SignUp_Bonus_Registration()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                TestRepository.Common commTest = new TestRepository.Common();
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
                    AddTestCase("Excecute Verify_SignUp_Bonus_Registration", "Verify_SignUp_Bonus_Registration : Pass");
                    AddTestCase("Register a customer selecting Indonesia as country and currency as AUD", "Registration should be successfull");

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_indonesia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = ReadxmlData("regdata", "currency_AUD", DataFilePath.Accounts_Wallets);

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                 
                    AddTestCase("Verify the Bonus after registration", "Bonus should be credited to the customer");

                    if (AccountAndWallets.MyAccount_NavigateToMyAccount(driverObj))
                    {
                        if (!AccountAndWallets.MyAccount_VerifyFreeBet(driverObj, ReadxmlData("bonus", "signup", DataFilePath.Accounts_Wallets), ReadxmlData("bonus", "signup_amt", DataFilePath.Accounts_Wallets)))
                        {
                            Fail("Freebet not added");
                        }
                    }
                    else
                    {
                        Fail("Navigation to My account page is failed");

                    }

                    Pass("Bonus is credited to the customer as expected");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass("Verify_SignUp_Bonus_Registration : scenario passed");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SignUp_Bonus_Registration : scenario failed");
                }
            }


            /// <summary>
            /// Verify if new customers get bonus on Register (with promo Code) ->  First Deposit if condition satisfies.
            /// Naga
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 2)]
            [Parallelizable]           
            public void Verify_SignUpDeposit_bonus_Satisfies()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                MyAcct_Data acctData = new MyAcct_Data();
                AccountAndWallets AnW = new AccountAndWallets();

                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    // AnW.Registration_PlaytechPages(driverObj, ref regData,0,false, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));

                    commTest.PP_Registration(driverObj, ref regData, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();

                    BaseTest.AddTestCase("Verify if new customers get bonus on Register (with promo Code) ->  First Deposit if condition satisfies.", "First deposit should be successful");
                    // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                  BaseTest.Assert.IsTrue(AccountAndWallets.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("bonus", "depoWallet", DataFilePath.Accounts_Wallets),
                        ReadxmlData("bonus", "bonusWallet", DataFilePath.Accounts_Wallets),
                         ReadxmlData("bonus", "depoAmnt", DataFilePath.Accounts_Wallets)),                        
                        "Bonus amount not added to: " + ReadxmlData("bonus", "bonusWallet", DataFilePath.Accounts_Wallets));


                    BaseTest.Pass("Bonus amount added & successfully verified");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SignUpDeposit_bonus_Satisfies - failed");
                }

            }
            
            /// <summary>
            /// Verify if new customers do not get bonus on Register (with promo Code) ->  First Deposit if condition not satisfies.
            /// Naga
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 3)]
            [Parallelizable]
            //Verify the Sign up Deposit type bonus 
            public void Verify_SignUpDeposit_bonus_NotSatisfies()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                MyAcct_Data acctData = new MyAcct_Data();
                AccountAndWallets AnW = new AccountAndWallets();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    // AnW.Registration_PlaytechPages(driverObj, ref regData,0,false, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));

                    commTest.PP_Registration(driverObj, ref regData, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();

                    BaseTest.AddTestCase("Verify if new customers do not get bonus on Register (with promo Code) ->  First Deposit if condition not satisfies.", "First deposit should be successful");
                    // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                 BaseTest.Assert.IsFalse(AccountAndWallets.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
                        ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("bonus", "depoWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("bonus", "bonusWallet", DataFilePath.Accounts_Wallets),
                        ReadxmlData("bonus", "depoAmnt2", DataFilePath.Accounts_Wallets)),
                        "Bonus amount got triggered even when condition not satisfied");
                    BaseTest.Pass("First Deposit successfully verified");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SignUpDeposit_bonus_NotSatisfies - failed");
                }

            }

            /// <summary>
            /// Verify if new customers gets Sports Free Bet on Register (with promo Code) -> First Deposit -> and places first Sports Bet of Odds more than 1/3
            /// Naga
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 4)]
            [Parallelizable]
            public void Verify_SportsFreeBet_bonus_satisfies_Ecom()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                MyAcct_Data acctData = new MyAcct_Data();
                AccountAndWallets AnW = new AccountAndWallets();

                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Ecom_Btn", Keys.Enter, "Join button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    // AnW.Registration_PlaytechPages(driverObj, ref regData,0,false, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));

                    commTest.PP_Registration(driverObj, ref regData, ReadxmlData("bonus", "betProm", DataFilePath.Accounts_Wallets));
                    Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();

                    BaseTest.AddTestCase("Verify if new customers get bonus on Register (with promo Code) ->  First Deposit if condition satisfies.", "First deposit should be successful");
                    // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    BaseTest.Assert.IsTrue(AccountAndWallets.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
                          ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                          ReadxmlData("bonus", "depoWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("bonus", "depoWallet", DataFilePath.Accounts_Wallets),
                           ReadxmlData("bonus", "betDepAmt", DataFilePath.Accounts_Wallets)),
                          "Depositted amount not added to: " + ReadxmlData("bonus", "depoWallet", DataFilePath.Accounts_Wallets));
                    Pass();
                    driverObj.Close();
                    driverObj.SwitchTo().Window(portalWindow);
                    //switch to main window
                    string eventName = ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets);
                    string oddValue = ReadxmlData("bonus", "selectionOdd", DataFilePath.Accounts_Wallets);
                    string stake = ReadxmlData("bonus", "stake", DataFilePath.Accounts_Wallets);
                    string bonusname = ReadxmlData("bonus", "betBonus", DataFilePath.Accounts_Wallets);
                    string bonusvalue = ReadxmlData("bonus", "bet_value", DataFilePath.Accounts_Wallets);

                    AccountAndWallets.SearchEvent(driverObj, eventName);
                    AccountAndWallets.AddToBetSlipPlaceBet_selection(driverObj, oddValue, stake);

                    AddTestCase("Verify whether freebet is triggered?", "Freebet should be triggered");

                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "Close_Receipt", "close receipt button not found", 0, false);
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//label[contains(text(),'£" + bonusvalue + " - " + bonusname + "')]")), "Freebet not triggered after placebet");
                    Pass();

                    Pass("Bonus amount added & successfully verified");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SportsFreeBet_bonus_satisfies_Ecom - failed");
                }

            }

            /// <summary>
            /// Verify if new customers gets Sports Free Bet on Register (with promo Code) -> First Deposit -> and places first Sports Bet of Odds less than 1/3
            /// Naga
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 5)]
            [Parallelizable]
            public void Verify_SportsFreeBet_bonus_notsatisfies_Ecom()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                MyAcct_Data acctData = new MyAcct_Data();
                AccountAndWallets AnW = new AccountAndWallets();

                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion

                try
                {
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Ecom_Btn", Keys.Enter, "Join button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    // AnW.Registration_PlaytechPages(driverObj, ref regData,0,false, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));

                    commTest.PP_Registration(driverObj, ref regData, ReadxmlData("bonus", "betProm", DataFilePath.Accounts_Wallets));
                    Pass("Customer registered succesfully");


                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    AddTestCase("Username:" + regData.username + " Password:" + Registration_Data.password, "");
                    Pass();

                    BaseTest.AddTestCase("Verify if new customers get bonus on Register (with promo Code) ->  First Deposit if condition satisfies.", "First deposit should be successful");
                    // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    BaseTest.Assert.IsTrue(AccountAndWallets.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
                          ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                          ReadxmlData("bonus", "depoWallet", DataFilePath.Accounts_Wallets),
                          ReadxmlData("bonus", "depoWallet", DataFilePath.Accounts_Wallets),
                           ReadxmlData("bonus", "betDepAmt", DataFilePath.Accounts_Wallets)),
                          "Depositted amount not added to: " + ReadxmlData("bonus", "depoWallet", DataFilePath.Accounts_Wallets));
                    Pass();

                    driverObj.Close();
                    driverObj.SwitchTo().Window(portalWindow);
                    //switch to main window
                    string eventName = ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets);
                    string oddValue = ReadxmlData("bonus", "selectionOdd_Negative", DataFilePath.Accounts_Wallets);
                    string stake = ReadxmlData("bonus", "stake_Negative", DataFilePath.Accounts_Wallets);
                    string bonusname = ReadxmlData("bonus", "betBonus", DataFilePath.Accounts_Wallets);
                    string bonusvalue = ReadxmlData("bonus", "bet_value", DataFilePath.Accounts_Wallets);

                    AccountAndWallets.SearchEvent(driverObj, eventName);
                    AccountAndWallets.AddToBetSlipPlaceBet_selection(driverObj, oddValue, stake);

                    AddTestCase("Verify whether freebet is triggered?", "Freebet should not be triggered");

                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "Close_Receipt", "close receipt button not found", 0, false);
                    BaseTest.Assert.IsFalse(wAction.IsElementPresent(driverObj, By.XPath("//label[contains(text(),'£" + bonusvalue + " - " + bonusname + "')]")), "Freebet has triggered after placebet");
                    Pass();

                    Pass("Bonus amount added & successfully verified");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SportsFreeBet_bonus_notsatisfies_Ecom - failed");
                }

            }

            [RepeatOnFail]
            [Timeout(1500)]
            [Test(Order = 6)]
            [Parallelizable]
            //Verify the Deposit type bonus /promotion 
            public void Verify_Deposit_Netteller_Bonus()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("bonus", "depoAmnt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("bonus", "bonusWallet", DataFilePath.Accounts_Wallets), ReadxmlData("bonus", "bonusWallet", DataFilePath.Accounts_Wallets),null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                AccountAndWallets.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("bonus", "depoProm", DataFilePath.Accounts_Wallets));

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

        
      [TestFixture, Timeout(8000)]
        [Parallelizable]
        public class FundTransfer : BaseTest
        {
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();

          
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1600)]
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets),null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    AccountAndWallets.AllWallets_Transfer(driverObj, acctData, ReadxmlData("depdata", "TransWalletDropDown", DataFilePath.Accounts_Wallets),
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
        
          
            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1600)]
            public void Verify_Fundtransfer_IMS_All()
            {
          
                #region declaration
                IMS_Base imsAdmin = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Registration_Data regData = new Registration_Data();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                #endregion

                AddTestCase("Verifying fund transfer through IMS admin", "Fund should be transferred successfully");
                try
                {
                    imsAdmin.Init();
                    commIMS.SearchCustomer_Newlook(imsAdmin.IMSDriver, ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets));

                    List<string> wallet = ReadxmlData("depdata", "TransWalletDropDown", DataFilePath.Accounts_Wallets).ToString().Split(';').ToList<string>();

                    for (int fromWalletInd = 0; fromWalletInd < wallet.Count(); fromWalletInd++)
                        for (int toWalletInd = 0; toWalletInd < wallet.Count(); toWalletInd++)
                            if (wallet[fromWalletInd].ToString() == wallet[toWalletInd].ToString())
                                continue;
                            else
                                commIMS.FundTransfer(imsAdmin.IMSDriver, wallet[fromWalletInd], wallet[toWalletInd]);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    // CaptureScreenshot(MyBrowser, "Portal");
                    CaptureScreenshot(imsAdmin.IMSDriver, "IMS");
                    Fail("Affiliate Customer Registration/Validation has failed");
                    Pass();
                }
                finally { imsAdmin.Quit(); }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1600)]
            public void Verify_Fundtransfer_IMS_All_History()
            {

                #region declaration
                IMS_Base imsAdmin = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Registration_Data regData = new Registration_Data();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                #endregion

                AddTestCase("Verifying fund transfer through IMS admin", "Fund should be transferred successfully");
                try
                {
                    imsAdmin.Init();
                    commIMS.SearchCustomer_Newlook(imsAdmin.IMSDriver, ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets));

                    List<string> wallet = ReadxmlData("depdata", "TransWalletDropDown", DataFilePath.Accounts_Wallets).ToString().Split(';').ToList<string>();

                    commIMS.FundTransfer_History(imsAdmin.IMSDriver, ReadxmlData("depdata", "AuditFromWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "AuditToWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "AuditAmt", DataFilePath.Accounts_Wallets));

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    // CaptureScreenshot(MyBrowser, "Portal");
                    CaptureScreenshot(imsAdmin.IMSDriver, "IMS");
                    Fail("Affiliate Customer Registration/Validation has failed");
                    Pass();
                }
                finally { imsAdmin.Quit(); }
            }

            /// <summary>
            ///  Author :Naga
            ///  GEN-5411
            ///  Verify the wallet transfer functionality across wallets to Verify transfer amount lesser than non withdrawable amount 
            /// </summary>
            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 3)]            
            public void Verify_Transfer_Insufficient_Fund()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion

                #region Declaration


                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Registration_Data regData = new Registration_Data();

                #endregion
                AddTestCase("Verify the wallet transfer functionality across wallets to Verify transfer amount lesser than non withdrawable amount ", "Insufficient fund error should be displayed");
                try
                {
                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets),null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    AccountAndWallets.Wallet_Transfer_Balance_Insuff(driverObj, acctData, ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "FromWallet", DataFilePath.Accounts_Wallets));

                    Pass();

                }
                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Transfer_Insufficient_Fund failed");

                }

            }
           
          
          [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1600)]
            public void Verify_Transfer_Portal_Ecom()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,FrameGlobals.EcomURL);
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

                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                         ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                          ReadxmlData("depdata", "FromWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets),null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    AccountAndWallets.Wallet_Transfer_Single_Ecom(driverObj, acctData);

                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Transfer_Portal_Ecom is failed for an exception");

                }
            }
            

        }//Fund Transfer class


       [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class IMSFeatures : BaseTest
        {
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
            TestRepository.Common Testcomm = new TestRepository.Common();
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]           
            public void Verify_Freeze_CustomerStatusInOB()
            {
        

                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm= new AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify the Freezed user in IMS is in Suspended state in OB", "Freezed user should be in Suspend status");
                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);
                    baseIMS.Init();

                    loginData.update_Login_Data(ReadxmlData("lgndata", "User", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgndata", "Pass", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets));
                    loginData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
                    loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, loginData.password);
                    BaseTest.Pass("Customer created successfully in IMS");
                    commIMS.FreezeTheCustomer(baseIMS.IMSDriver);
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + Registration_Data.password, "");
                    BaseTest.Pass(); 
                    AddTestCase("Verify the Freezed user status is Suspendedin OB", "Freezed user should be in Suspend status");
                    adminBase.Init();
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                   
                    string Status = wAction.GetSelectedDropdownOptionText(adminBase.MyBrowser, By.Name("Status"));
                    BaseTest.Assert.IsTrue(Status.Contains("Suspend"), "Status in OB for freezed customer is :" + Status);
                    Pass();

                    Pass();
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                 
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Freezed customer verification in OB is failed for an exception");

                }
                finally
                {
                    adminBase.Quit();
                    baseIMS.Quit();
                }

            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_UnFreeze_CustomerStatusInOB()
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

                AddTestCase("Verify the UnFreezed user in IMS is in Active state in OB", "UnFreezed user should be in Active status");
                try
                {
                    //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);
                    baseIMS.Init();

                    loginData.update_Login_Data(ReadxmlData("lgndata", "User", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgndata", "Pass", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets));
                    loginData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
                    loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, loginData.password);
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
                    BaseTest.Pass();
                    BaseTest.Pass("Customer created successfully in IMS");
                    commIMS.FreezeTheCustomer(baseIMS.IMSDriver);
                    baseIMS.Quit();
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.DisableFreeze(baseIMS.IMSDriver);

                    Thread.Sleep(TimeSpan.FromMinutes(1));

                    AddTestCase("Verify the UnFreezed user status is Active OB", "Freezed user should be in Active status");
                    adminBase.Init();
                    adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                    string Status = wAction.GetSelectedDropdownOptionText(adminBase.MyBrowser, By.Name("Status"));
                    BaseTest.Assert.IsTrue(Status.Contains("Active"), "Status in OB for freezed customer is :" + Status);
                    Pass();
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Freezed customer verification in OB is failed for an exception");
                }
                finally
                {
                    adminBase.Quit();
                    baseIMS.Quit();
                }

            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Credit_Card_Registration_IMS()
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
                AddTestCase("Validation of credit card registraion", "User should be able to register credit card successfully");
                try
                {
                    #region Prerequiste
                    AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    baseIMS.Init();
                    string pass = ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets);
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, "User", pass);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + pass);
                    AddTestCase("Username:" + regData.username + " Password:" + pass, "");
                    Pass();
                    string creditCardNumber = TD.createCreditCard("Visa").ToString();
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion
                    commIMS.AddCreditCard(baseIMS.IMSDriver, creditCardNumber);
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS, "IMS");
                    Fail("Credit Card registration failed for exception");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 4)]
            [Timeout(1800)]
            [RepeatOnFail]
            public void Verify_Cust_ChangePassword_IMS()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                IMS_AdminSuite.IMS_Base IMSBase = new IMS_Base();
                IMS_AdminSuite.Common IMSComm = new IMS_AdminSuite.Common();
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                #endregion


                //Configuration testdata = TestDataInit();
                try
                {
                    Login_Data loginData = new Login_Data();
                   // AddTestCase("Change the password", "Customer should be created successfully with Selfexcluded status");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                   // wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Join button in home page", 0, false);
                   // AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                   WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + "Password2");
                  

                    AccountAndWallets.LogoutFromPortal(driverObj);
                    IMSBase.Init();
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, regData.username);
                    IMSComm.ResetPassword(IMSBase.IMSDriver, "Password2");
                    IMSBase.Quit();
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    loginData.update_Login_Data(regData.username, "Password2", "Tester");
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    Pass("Customer password changeded successfully");
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


            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_DepositLimit_IMS()
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


                AddTestCase("Validation of Deposit Limit set in IMS", "User should be able to set deposit limit successfully");
                try
                {


                    #region Prerequiste
                   // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                    baseIMS.Init();
                    string pass = ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets);
                   // regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, "User", pass);
                   // Pass("Customer registered succesfully");

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                 
                    
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                   // AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion
                     WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " +  pass);
                    AddTestCase("Username:" + regData.username + " Password:" + pass, "");
                    Pass();
                   
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, true);
                    commIMS.SetDepositLimit(baseIMS.IMSDriver,"500");
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Deposit Limit failed for exception");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 6)]
            [Timeout(1200)]
            [RepeatOnFail]
            public void Verify_Cust_Affiliate_Registration()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser);
                #endregion
                #region declaration              
                IMS_Base imsAdmin = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();         
                Registration_Data regData = new Registration_Data();                
                AdminSuite.Common adminComm = new AdminSuite.Common();
                #endregion

                AddTestCase("Affiliate Customer Registration - Verify the Affiliate customer Registration and check the affiliate details in OB and IMS Admin", "Affiliate Customer Registration should be successful and Affiliate ID details should be updated in OB and IMS Admin.");
                try
                {

                    AddTestCase("Open Affiliate URL and enter the details", "Affiliate ID details should be updated successfully");
                    driverObj.Manage().Cookies.AddCookie(new Cookie("AFF_DATA", Registration_Data.affliateData, ".ladbrokes.com", "/", DateTime.Now.AddDays(1)));

                    Pass();
                    AddTestCase("Register the Affiliate customer in portal", "The customer should be registered successfully");
                    //Thread.Sleep(3000);
                    Console.WriteLine(driverObj.Manage().Cookies.GetCookieNamed("AFF_DATA"));
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                  AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer has registered in successfully");

                    AddTestCase("Verify affiliate details in IMS", "Affiliate details should match with registered details");
                    imsAdmin.Init();
                    commIMS.SearchCustomer_Newlook(imsAdmin.IMSDriver, regData.registeredUsername);
                    commTest.VerifyAffiliateDetailinIMS(imsAdmin.IMSDriver, regData);
                    Pass("Affiliate details and other registration details matched");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "Portal");
                    CaptureScreenshot(imsAdmin.IMSDriver, "IMS");
                    Fail("Affiliate Customer Registration/Validation has failed");
                    Pass();
                }
                finally { imsAdmin.Quit(); }
            }

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 7)]
            public void Verify_Increase_DepositLimit_IMS()
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
                    //  wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                    //  AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    // Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData, "mr", "200");
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion


                    #endregion

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    AccountAndWallets.MyAccount_Responsible_Gambling(driverObj, "500");

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                    commIMS.VerifyPendingDepositLimitInIms(baseIMS.IMSDriver, "500");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Responsible gambling validation failed for exception:" + e.Message);
                }
                finally { baseIMS.IMSDriver.Quit(); }
            }

            [Test(Order = 8)]
            [RepeatOnFail]
            [Timeout(1600)]
            public void Verify_Withdraw_IMS()
            {
         
                #region declaration
                IMS_Base imsAdmin = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                Registration_Data regData = new Registration_Data();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                #endregion
                #region DriverInitiation
               IWebDriver driverObj;
               ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
               driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
               #endregion

                AddTestCase("Verifying fund transfer through IMS admin", "Fund should be transferred successfully");
                try
                {
                    Login_Data loginData = new Login_Data();
                    loginData.update_Login_Data(ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets),
                  ReadxmlData("depdata", "Pass", DataFilePath.Accounts_Wallets),
                  ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    AccountAndWallets.LogoutFromPortal(driverObj);

                    imsAdmin.Init();
                    AddTestCase("Customer : " + ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets), ""); Pass();
                    commIMS.SearchCustomer_Newlook(imsAdmin.IMSDriver, ReadxmlData("depdata", "User", DataFilePath.Accounts_Wallets));
                    commIMS.WithdrawNetellerFromIMS(imsAdmin.IMSDriver, int.Parse(ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets)), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets));

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    // CaptureScreenshot(MyBrowser, "Portal");
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(imsAdmin.IMSDriver, "IMS");
                    Fail("Verify_Withdraw_IMS has failed");
                    Pass();
                }
                finally { imsAdmin.Quit(); }
            }

            [Test(Order = 9)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Credit_Card_AllowDuplicate_IMS()
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

                AddTestCase("Verify Credit card Allow duplicate functionality", "Allow duplicate should allow same credit card to add in different customer");
                try
                {

                    #region Prerequiste
                    baseIMS.Init();
                    string pass = ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets);
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, "User", pass);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + pass);

                    AddTestCase("Username:" + regData.username + " Password:" + pass, "");
                    Pass();
                    string creditCardNumber = TD.createCreditCard("Visa").ToString();
                    Console.WriteLine("Credit Card:" + creditCardNumber);
                    #endregion
                    commIMS.AddCreditCard(baseIMS.IMSDriver, creditCardNumber);
                    //commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                    commIMS.AllowDuplicateCreditCard_Newlook(baseIMS.IMSDriver, creditCardNumber.Substring(0, 6), creditCardNumber.Substring(creditCardNumber.Length - 4, 4));

                    wAction.Click(baseIMS.IMSDriver, By.LinkText(regData.username),"Username link not found in Credit card page");
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, "User", pass);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + pass);
                    AddTestCase("Username:" + regData.username + " Password:" + pass, "");
                    Pass();

                    commIMS.AddCreditCard(baseIMS.IMSDriver, creditCardNumber);
                    Pass();
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Credit Card Allow duplicate failed for exception");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            /// <summary>
            /// Author:Anand C
            /// Register a customer through IMS
            /// Date: 13/08/2014
            /// </summary>
            [Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_AddCorrection_IMS()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                TestRepository.Common commTest = new TestRepository.Common();
                AdminSuite.Common commOB = new AdminSuite.Common();
                String balance = "0";
                #endregion

                try
                {
                    #region Create Customer from Playtech pages
                   // AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                  //  wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                  //  AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                  //  Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    balance = AccountAndWallets.FetchBalance_Portal_Homepage(driverObj);
                    AccountAndWallets.LogoutFromPortal(driverObj);
                    #endregion


                    #region Adding correction in IMS
                    BaseTest.AddTestCase("Adding correction amount to Customer in IMS", "Correction amount to added to Customer in IMS");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                    commIMS.AddCorrection(baseIMS.IMSDriver, "13");
                    BaseTest.Pass("Correction amount to added to Customer in IMS");
                    #endregion

                    Thread.Sleep(TimeSpan.FromSeconds(40));
                    #region Verify the balance in portal
                    BaseTest.AddTestCase("Verify the Correction amount added to Customer in Portal", "Verified Correction amount added to Customer in Portal");
                    loginData.update_Login_Data(regData.username, Registration_Data.password, regData.fname);
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    String newBalance = AccountAndWallets.FetchBalance_Portal_Homepage(driverObj);
                    if (Convert.ToDouble(newBalance) != Convert.ToDouble(balance) + 13)
                    {
                        BaseTest.Fail("Correction amount is not reflected in portal as expected");
                    }
                    BaseTest.Pass();
                    #endregion
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_AddCorrection_IMS - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

           /// <summary>
            /// Verify the firstname+lastname put on firstname field in IMS credit card page
            /// Naga
           /// </summary>
            [Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_FnameLname_CreditCard_IMS()
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

                AddTestCase("Verify the firstname+lastname put on firstname field in IMS credit card page", "Fname and Lname should be same");
                try
                {
                    #region Prerequiste
                    baseIMS.Init();
                    loginData.update_Login_Data(ReadxmlData("ccdata", "User", DataFilePath.Accounts_Wallets),
                    ReadxmlData("ccdata", "Pass", DataFilePath.Accounts_Wallets),
                     ReadxmlData("ccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    acctData.Update_deposit_withdraw_Card(ReadxmlData("ccdata", "CCard", DataFilePath.Accounts_Wallets),
                      ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                       ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                       ReadxmlData("ccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets),
                        ReadxmlData("ccdata", "CVV", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
                    Pass();                  
                    Console.WriteLine("Credit Card:" + acctData.card);
                    #endregion

                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                   
                    string fname = wAction.GetAttribute(baseIMS.IMSDriver, By.Id("firstname"), "value", "First name field not found");
                    string lname = wAction.GetAttribute(baseIMS.IMSDriver, By.Id("lastname"), "value", "Last name field not found");

                    commIMS.SearchCC_WithinCustomer(baseIMS.IMSDriver, acctData.card.Substring(0, 6), acctData.card.Substring(acctData.card.Length - 4, 4));

                    wAction.WaitAndMovetoFrame(baseIMS.IMSDriver, "main-content");
                    string CCfname = wAction.GetAttribute(baseIMS.IMSDriver, By.Id("firstname"), "value", "First name field not found");
                    string CClname = wAction.GetAttribute(baseIMS.IMSDriver, By.Id("lastname"), "value", "Last name field not found");

                    BaseTest.Assert.IsTrue(CCfname == fname, "Firstname in Credit card is not matching the customer " + CCfname + "<>" + fname);
                    BaseTest.Assert.IsTrue(CClname == lname, "Lastname in Credit card is not matching the customer " + CCfname + "<>" + fname);
                  }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_FnameLname_CreditCard_IMS failed for exception");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            /// <summary>
            /// Verify the firstname+lastname put on firstname field in IMS credit card page
            /// Naga
            /// </summary>
            [Test(Order = 12)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_EventLog_Email_IMS()
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

                AddTestCase("Verify the Event Logs when Email address is changed by Admin ", "Event log check should pass");
                try
                {

                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    Testcomm.Createcustomer_PostMethod(ref regData);                    
                    baseIMS.Init();
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);                   
                      #endregion

                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                    wAction.Clear(baseIMS.IMSDriver, By.Id("email"), "Email text box not loaded/found", 0, false);
                    wAction.Type(baseIMS.IMSDriver, By.Id("email"), "new@playtech.com", "Email text box not loaded/found");
                    wAction.Click(baseIMS.IMSDriver, By.Id("update"), "update button not loaded/found");
                    wAction.WaitAndAccepAlert(baseIMS.IMSDriver);
                    System.Threading.Thread.Sleep(8000);
                    wAction.Click(baseIMS.IMSDriver, By.LinkText("username"), "username event log link not loaded/found");

                    wAction.WaitAndMovetoFrame(baseIMS.IMSDriver, "main-content");
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(baseIMS.IMSDriver,By.XPath("//tr[td[a[contains(text(),'SandeepKR1')]] and td[contains(text(),'user_modified') ]]")), "No log found for email modificaton");
                }

                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_EventLog_Email_IMS failed for exception");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }
         

        }//IMS class

      

       [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class MyAccount : BaseTest
        {
            TestRepository.AccountAndWallets AccountAndWallets = new AccountAndWallets();
            TestRepository.Common Testcomm = new TestRepository.Common();

            /// <summary>
            /// Author:Nagamanickam
            /// </summary>
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_MyAcct_VerifyLinks_Poker()
            {

                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
               // TestRepository.Common commTest = new TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,FrameGlobals.PokerURL);
                #endregion

                try
                {

                    AddTestCase("Verify All links in MY Acount page is present", "All the MY Acct links should be present in the page");

                    //loginData.update_Login_Data("testaditisuhaileur",
                    //    "123456",
                    //    ReadxmlData("depdata", "CCFname", DataFilePath.ProductMigration));



                    loginData.update_Login_Data(ReadxmlData("lgnsdata", "User", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgnsdata", "Pass", DataFilePath.Accounts_Wallets),
                        ReadxmlData("lgnsdata", "Fname", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);

                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    AccountAndWallets.MyAccount_VerifyLinks(driverObj);
                    BaseTest.Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("My Acct Link Verification scenario failed");
                }
            }

         
            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 2)]
            public void Verify_MyAcct_DepositLimit_Responsible_Gambling()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,FrameGlobals.LiveDealerURL);
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
                  //  wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                  //  AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                  //  Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    #endregion

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    AccountAndWallets.MyAccount_Responsible_Gambling(driverObj);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
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

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 3)]
            public void Verify_MyAcct_PasswordChange()
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
                    
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    //wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    // AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion


                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
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
                    AccountAndWallets.LogoutFromPortal(driverObj);
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    loginData.update_Login_Data(regData.username, "Newpassword1", "fname");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
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

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 4)]
            public void Verify_MyAcct_NonUK_CountryCheck()
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

                AddTestCase("Verify that Address lookup on post code for UK Customer in MyAccount page is available.", "User should be able see UK in country list");
                try
                {
                    AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    AccountAndWallets.MyAccount_CheckNonUKcustomer_Country(driverObj);
                    Pass();
                 
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Country check for UK customer failed for exception:" + e.Message);
                }
            }

            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 5)]
            public void Verify_MyAcct_Contact_Preferences()
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
                  //  AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                //    Pass("Customer registered succesfully");
                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    #endregion

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    AccountAndWallets.MyAccount_ModifyDetails(driverObj);
                    AccountAndWallets.MyAccount_Edit_ContactPref_DirectMail(driverObj);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                    BaseTest.AddTestCase("Verify contact preference updated in IMS", "Contact preferences should be updated");
                    wAction.Click(baseIMS.IMSDriver, By.Id("imgsec_contact"), "Contact preference link not found");
                    System.Threading.Thread.Sleep(2000);
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(baseIMS.IMSDriver, By.XPath("//td[@id='contact_preferences']//input[not(@checked) and @id='communicationoptouts[3][2]']")), "Contact preferences is not updated in IMS");
                    BaseTest.Pass();
                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Country check for UK customer failed for exception:" + e.Message);
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            /// <summary>
            /// Naga
            ///GEN-3434
            ///GEN-3433
            ///GEN-3432
            /// </summary>
            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 6)]
            public void Verify_MyAcct_NonEditableFields()
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

                AddTestCase("Verify that Fname,Title and Surname are not editable in My accts page.", "The Fields should not be editable");
                try
                {
                    loginData.update_Login_Data(ReadxmlData("lgndata", "User", DataFilePath.Accounts_Wallets),
                      ReadxmlData("lgndata", "Pass", DataFilePath.Accounts_Wallets),
                      ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    try
                    {
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }

                    AccountAndWallets.MyAccount_ValidateDiabledFields(driverObj);
                    AccountAndWallets.MyAccount_EditPostCode(driverObj, "ha29sr", "Worple Way");
                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Field editable validation failed for exception:" + e.Message);
                }
            }

            /// <summary>
            /// Naga
            /// </summary>
            [Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 7)]
            public void Verify_WalletBal_InAccHistory()
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

                AddTestCase("Verifying whether balance in each wallet displayed in Account history and Banking page are equal", "Wallet balance should be displayed as expected");
                try
                {
                    loginData.update_Login_Data(ReadxmlData("vccdata", "User", DataFilePath.Accounts_Wallets),
                      ReadxmlData("vccdata", "Pass", DataFilePath.Accounts_Wallets),
                      ReadxmlData("vccdata", "CCFname", DataFilePath.Accounts_Wallets));

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    AccountAndWallets.LoginFromPortal(driverObj, loginData);
                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "myAcct_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction.WaitAndMovetoPopUPWindow(driverObj, "My Account window not found", FrameGlobals.elementTimeOut);
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }

                    //BaseTest.AddTestCase("Comparing wallet balances in Account history and Banking page", "Wallet balances displayed should be equal");
                    AccountAndWallets.ComparingWalletBalances(driverObj);
                    //BaseTest.Pass();

                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_WalletBal_InAccHistory failed for exception:" + e.Message);
                }

            }


            /// <summary>
            /// Author:Naga
            /// Verify the Sports bet history under Account history 
            /// Date: 24/03/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 8)]
            public void Verify_SportsBet_History()
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
                    AddTestCase("Verify the Sports bet history under Account history ", "Bet history should appear in my accounts page.");



                    loginData.update_Login_Data(ReadxmlData("betdata", "User", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("betdata", "Pass", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("betdata", "Fname", DataFilePath.Accounts_Wallets_stg));
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    AccountAndWallets.LoginFromEcom(driverObj, loginData);

                    AccountAndWallets.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets));
                    AccountAndWallets.AddToBetSlipPlaceBet(driverObj, 10);

                    try
                    {
                        wAction.Click(driverObj, By.XPath("//a[@class='account-link' and text()='My Account']"), "Welcome text not found", 0, false);

                        //wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
                        //wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "myAcct_lnk", "Deposit Link not found", FrameGlobals.reloadTimeOut, false);
                        wAction.WaitAndMovetoPopUPWindow(driverObj, "My Account window not found", FrameGlobals.elementTimeOut);
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }

                    AccountAndWallets.Verify_Sports_BetHistory(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets));
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SportsBet_History scenario failed");
                }

            }


            [Timeout(2200)]
            [RepeatOnFail]
           // [Test(Order = 9)]
            public void Verify_MyAcct_ViewOdds_Ecom()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                // Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify that contact preferences edited in portal is updated in IMS.", "Contact preferences should be updated successfully");
                try
                {
                    //AddTestCase("Create customer from Bingo Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    //wAction._Type(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "EcomJoin_btn", Keys.Enter, "Join button not found/Not clickable", 0, false);
                    //AccountAndWallets.Registration_PlaytechPages(driverObj, ref regData);
                    //Pass("Customer registered succesfully");

                    #region NewCustTestData
                    Testcomm.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = Registration_Data.password;
                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    #endregion

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + Registration_Data.password);
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    AccountAndWallets.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets));
                    string sel1 = ReadxmlData("eventID", "sel1", DataFilePath.Accounts_Wallets);

                   
                    //try
                    //{
                    //    List<IWebElement> ele = driverObj.FindElements(By.XPath("//a[contains(@class,'betbutton')]")).ToList();
                    //    ele[0].GetAttribute("value");
                    //}
                    //catch (Exception e) { BaseTest.Fail("No selection found for the event"); }
                    double odds = Math.Round(FrameGlobals.FractionToDouble(sel1), 2) + 1;
                    sel1 = Convert.ToString(odds);

                    AccountAndWallets.VerifySelection(driverObj, sel1);
                    try
                    {
                        wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "UserImg", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                       
                    }
                    catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'My Accounts' Page" + e.Message.ToString()); }
                    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                    AccountAndWallets.MyAccount_ModifyPreferences(driverObj, true, "Fraction", true);
                    driverObj.Close();
                    wAction.WaitAndMovetoPopUPWindow(driverObj, portalWindow, "Unable to switch to main window");
                    AccountAndWallets.LogoutFromPortal(driverObj);
                    loginData.update_Login_Data(regData.registeredUsername, Registration_Data.password, "Tester");
                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    AccountAndWallets.VerifySelection(driverObj, ReadxmlData("eventID", "sel1", DataFilePath.Accounts_Wallets));
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_MyAcct_ViewOdds_Ecom failed for exception:" + e.Message);
                }
            }

            /// Author:Naga
            /// Register a customer through Playtech Pages
            /// Date: 24/03/2014
            /// </summary>
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 10)]
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


                    loginData.update_Login_Data(ReadxmlData("autodata", "User", DataFilePath.Accounts_Wallets_stg),
                                            ReadxmlData("autodata", "Pass", DataFilePath.Accounts_Wallets_stg),
                                            "");
                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    AccountAndWallets.LoginFromEcom(driverObj, loginData);
                    AccountAndWallets.Ecom_MyAcct_AutoTopUP(driverObj);
                    AccountAndWallets.LogoutFromPortal_Ecom(driverObj);
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


        }//MyAccount class

     

      

    }//RegressionSuite_AnW class
}//NameSpace
