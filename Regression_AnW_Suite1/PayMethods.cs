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
using ICE.ObjectRepository.Vegas_IMS_BAU;


namespace Regression_AnW_Suite1
{

    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class Banking_PayMethods1 : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
        Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();



        [Test(Order = 1)]
        [RepeatOnFail]
        [Timeout(1500)]
        public void Verify_Netteller_Registration_EUR()
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
                regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                //wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "EcomJoin_btn", Keys.Enter, "Join button not found", 0, false);
                //AnW.Registration_PlaytechPages(driverObj, ref regData);

                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                //Pass("Customer registered succesfully");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                #endregion

                #region Neteller Registration
                String portalWindow = AnW.OpenCashier(driverObj);
                AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets));

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
                //      AnW.OpenRegistrationPage(driverObj);
                // AnW.Registration_PlaytechPages(driverObj, ref regData);
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                //Pass("Customer registered succesfully");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                #endregion

                #region Neteller Registration
                String portalWindow = AnW.OpenCashier(driverObj); 
                AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets));

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
                regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                //AnW.Registration_PlaytechPages(driverObj, ref regData);
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                // Pass("Customer registered succesfully");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                #endregion

                acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                    ReadxmlData("paydata", "user_pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("paydata", "CCAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));

                AnW.Register_Paypal(driverObj, acctData);
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
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
        public void Verify_PayPal_Registration_USD()
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
            driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
            #endregion

            AddTestCase("Verify the payment deposit with pay pal method is successful", "Amount should be deposited to the selected wallet");
            try
            {
                #region Prerequiste
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                regData.currency = ReadxmlData("regdata", "currency_USD", DataFilePath.Accounts_Wallets);
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

                acctData.Update_deposit_paypal_account(ReadxmlData("paydata", "user_id", DataFilePath.Accounts_Wallets),
                    ReadxmlData("paydata", "user_pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("paydata", "Amt_USD", DataFilePath.Accounts_Wallets),
                     ReadxmlData("paydata", "depWallet", DataFilePath.Accounts_Wallets));

                AnW.Register_Paypal(driverObj, acctData);
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
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
                //      AnW.OpenRegistrationPage(driverObj);
                //AnW.Registration_PlaytechPages(driverObj, ref regData);
                //Pass("Customer registered succesfully");

                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                Console.WriteLine("Credit Card:" + creditCardNumber);
                #endregion


                String portalWindow = AnW.OpenCashier(driverObj);
                AnW.Verify_Credit_Card_Registration(driverObj, creditCardNumber, "MasterCard");
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
        public void Verify_Master_Card_Registration_USD()
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
            driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
            #endregion

            AddTestCase("Validation of credit card registraion for USD Currency", "User should be able to register credit card successfully");
            try
            {


                #region Prerequiste
                // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                // wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "EcomJoin_btn",Keys.Enter, "Join button not found", 0, false);
                // AnW.Registration_PlaytechPages(driverObj, ref regData);
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
                string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                Console.WriteLine("Credit Card:" + creditCardNumber);
                #endregion

                String portalWindow = AnW.OpenCashier(driverObj);
                AnW.Verify_Credit_Card_Registration(driverObj, creditCardNumber, "MasterCard");
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
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
              
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
                #region Paysafe Registration
                acctData.depositWallet = ReadxmlData("paysafedata", "DepWallet", DataFilePath.Accounts_Wallets);
                acctData.depositAmt = ReadxmlData("paysafedata", "DepAmt", DataFilePath.Accounts_Wallets);
                AnW.Verify_PaySafe_Registration(driverObj, ReadxmlData("paysafedata", "Paysafe_pin1", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin2", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin3", DataFilePath.Accounts_Wallets), ReadxmlData("paysafedata", "Paysafe_pin4", DataFilePath.Accounts_Wallets), acctData);
                #endregion

                wAction.BrowserClose(driverObj);
                System.Threading.Thread.Sleep(2000);
                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                driverObj.Navigate().Refresh();
                System.Threading.Thread.Sleep(5000);
                wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath));
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
                //      AnW.OpenRegistrationPage(driverObj);
                // AnW.Registration_PlaytechPages(driverObj, ref regData);
                // Pass("Customer registered succesfully");

                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                #endregion

                String portalWindow = AnW.OpenCashier(driverObj);
                #region Creditcard Registration
                string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                AnW.Verify_Credit_Card_Registration(driverObj, creditCardNumber, "MasterCard");
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

                AnW.VerifyRegister_Paypal(driverObj, acctData);

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
            driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
            #endregion

            AddTestCase("Validation of credit card registraion for Euro Currency", "User should be able to register credit card successfully");
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
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion



                string creditCardNumber = TD.createCreditCard("MasterCard").ToString();
                Console.WriteLine("Credit Card:" + creditCardNumber);
                #endregion


                String portalWindow = AnW.OpenCashier(driverObj);

                AnW.Verify_Credit_Card_Registration(driverObj, creditCardNumber, "MasterCard");
            }

            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Credit Card registration failed for exception");
            }

        }




    }//Paymethods

    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class Banking_PayMethods2 : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
        Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();



        [Test(Order = 1)]
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
                regData.email = "ims@aditi.com";
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                //regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                //      AnW.OpenRegistrationPage(driverObj);
                //AnW.Registration_PlaytechPages(driverObj, ref regData);
                // Pass("Customer registered succesfully");
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                #endregion

                /*loginData.update_Login_Data("Useruojjmahav", "Password1", ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);*/

                #region Skrill Registration
                acctData.Update_deposit_skrill_account(
                    ReadxmlData("skrilldata", "user_id", DataFilePath.Accounts_Wallets),
                    ReadxmlData("skrilldata", "user_pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("skrilldata", "DepAmt", DataFilePath.Accounts_Wallets),
                    ReadxmlData("skrilldata", "WalletName", DataFilePath.Accounts_Wallets));

                AnW.VerifyRegister_Skrill(driverObj, acctData);
                #endregion

                #region Skrill Deposit
                System.Threading.Thread.Sleep(2000);
                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                driverObj.Navigate().Refresh();
                System.Threading.Thread.Sleep(5000);
                AnW.Deposit_Cust_Skrill(driverObj, acctData);
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


        [Test(Order = 2)]
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

       //     Usernames["Verify_Skrill_Registration_GBP"] = "Userupeaeneam";

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

                loginData.update_Login_Data(Usernames["Verify_Skrill_Registration_GBP"], ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets),
                    ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                
                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                #region Skrill Deposit
                acctData.Update_deposit_skrill_account(
                    ReadxmlData("skrilldata", "user_id", DataFilePath.Accounts_Wallets),
                    ReadxmlData("skrilldata", "user_pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("skrilldata", "wAmt", DataFilePath.Accounts_Wallets),
                    ReadxmlData("skrilldata", "WalletName", DataFilePath.Accounts_Wallets));
                AnW.Withdraw_Skrill(driverObj, acctData);
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

     //   [Test(Order = 3)]
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
            driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
            #endregion

            AddTestCase("Validation of Skrill registraion for GBP Currency", "User should be able to register Skrill successfully");
            try
            {
                #region Prerequiste
                regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Finland", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                #endregion

                /*loginData.update_Login_Data("Useruojluarat", "Password1", ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);*/

                #region EuTeller Registration
                acctData.depositWallet = ReadxmlData("genPaydata", "WalletName", DataFilePath.Accounts_Wallets);
                acctData.depositAmt = ReadxmlData("genPaydata", "DepAmt", DataFilePath.Accounts_Wallets);
                
                AnW.VerifyRegister_Euteller(driverObj, acctData);
                #endregion

                #region EuTeller Deposit
                System.Threading.Thread.Sleep(2000);
                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                driverObj.Navigate().Refresh();
                System.Threading.Thread.Sleep(5000);
                AnW.Deposit_Cust_Euteller(driverObj, acctData);
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

        [Test(Order = 4)]
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
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_uganda", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_uganda", DataFilePath.Accounts_Wallets));
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion
                acctData.Update_deposit_withdraw_Card(ReadxmlData("vccdata", "CCard", DataFilePath.Accounts_Wallets),
                  ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                   ReadxmlData("vccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                   ReadxmlData("vccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets),
                    ReadxmlData("vccdata", "CVV", DataFilePath.Accounts_Wallets));

                
                // Pass("Customer registered succesfully");
                creditCardNumber = ReadxmlData("epdata", "CCard", DataFilePath.Accounts_Wallets);
                Console.WriteLine("Credit Card:" + creditCardNumber);
                #endregion


                String portalWindow = AnW.OpenCashier(driverObj);
                AnW.Verify_Credit_Card_Registration(driverObj, creditCardNumber, "EntroPay");
                driverObj.Close();
                wAction.WaitAndMovetoPopUPWindow(driverObj, portalWindow, "Main window not found");
                wAction.PageReload(driverObj);
                AnW.DepositTOWallet_CC(driverObj, acctData);

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
                    Console.WriteLine(e.Message.ToString());
                    // throw e;
                }

                finally
                {
                    baseIMS.Quit();
                }
            }


        }


        //[Test(Order = 14)]
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
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                //      AnW.OpenRegistrationPage(driverObj);
                //  AnW.Registration_PlaytechPages(driverObj, ref regData);
                // Pass("Customer registered succesfully");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                #endregion

                /*loginData.update_Login_Data("test_aditi_CNAZAJPH", "Lbr12345678", ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);*/

                #region eNets Registration
                acctData.Update_deposit_skrill_account(
                    ReadxmlData("eNetsdata", "user_id", DataFilePath.Accounts_Wallets),
                    ReadxmlData("eNetsdata", "user_pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("eNetsdata", "DepAmt", DataFilePath.Accounts_Wallets),
                    ReadxmlData("eNetsdata", "WalletName", DataFilePath.Accounts_Wallets));

                AnW.VerifyRegister_eNets(driverObj, acctData);
                #endregion

                #region eNets Deposit
                System.Threading.Thread.Sleep(2000);
                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[0].ToString(), "Unable to locate Banking page");
                driverObj.Navigate().Refresh();
                System.Threading.Thread.Sleep(5000);
                AnW.Deposit_Cust_Skrill(driverObj, acctData);
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
        [Test(Order = 5)]
        [Parallelizable]
        public void Verify_FirstDeposit_CreditCard()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            TestDataCreation.TestData_Generator TD = new TestDataCreation.TestData_Generator();
            AccountAndWallets AnW = new AccountAndWallets();
            string creditCardNumber = null;
            bool check = false;

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
                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                
                commTest.PP_Registration(driverObj, ref regData);
                Pass("Customer registered succesfully");
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();

                creditCardNumber = ReadxmlData("dupdata", "CreditCd", DataFilePath.Accounts_Wallets);

                acctData.Update_deposit_withdraw_Card(ReadxmlData("dupdata", "CreditCd", DataFilePath.Accounts_Wallets),
                 ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                  ReadxmlData("ccdata", "CCAmt", DataFilePath.Accounts_Wallets),
                  ReadxmlData("ccdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "withWallet", DataFilePath.Accounts_Wallets)
                , ReadxmlData("ccdata", "CVV", DataFilePath.Accounts_Wallets));

          
                BaseTest.AddTestCase("Verify customer is able to add credit/ debit as payment method in the first deposit page", "First deposit should be successful");
                check = true;

                Assert.IsTrue(AnW.Verify_FirstCashier_MasterCard(driverObj, acctData), "First Deposit amount not added to the wallet");
                BaseTest.Pass("successfully verified");

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Verify_FirstDeposit_CreditCard - failed");
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


        [Test(Order = 6)]
        [RepeatOnFail]
        [Timeout(1500)]
        public void Verify_Sofort_Registration_EUR()
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

            AddTestCase("Verify the payment deposit with Sofort method is successful", "Amount should be deposited to the selected wallet");
            try
            {

                #region Prerequiste
                // AddTestCase("Prerequiste : Create customer from Playtech pages", "Prerequiste Failed: Customer should be created.");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "countryCode_Germany", DataFilePath.Accounts_Wallets));
                regData.currency = ReadxmlData("regdata", "currency_euro", DataFilePath.Accounts_Wallets);
                //wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                //AnW.Registration_PlaytechPages(driverObj, ref regData);
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                // Pass("Customer registered succesfully");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                #endregion

                acctData.Update_deposit_sofort_account(ReadxmlData("sofdata", "Acct", DataFilePath.Accounts_Wallets),
                    ReadxmlData("sofdata", "PIN", DataFilePath.Accounts_Wallets), ReadxmlData("sofdata", "code", DataFilePath.Accounts_Wallets),
                    ReadxmlData("sofdata", "depAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("sofdata", "depWallet", DataFilePath.Accounts_Wallets));



                AnW.Register_Sofort(driverObj, acctData);
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
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
       // [Test(Order = 7)]
        //[DependsOn(Verify_Sofort_Registration_EUR)]            
        public void Verify_Sofort_Deposit_EUR()
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
                    ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                acctData.Update_deposit_sofort_account(ReadxmlData("sofdata", "Acct", DataFilePath.Accounts_Wallets),
                    ReadxmlData("sofdata", "PIN", DataFilePath.Accounts_Wallets), ReadxmlData("sofdata", "code", DataFilePath.Accounts_Wallets),
                    ReadxmlData("sofdata", "depAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("sofdata", "depWallet", DataFilePath.Accounts_Wallets));

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                AnW.Register_Sofort(driverObj, acctData);

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
        [Test(Order = 8)]
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

           // Usernames.Add("Verify_Sofort_Registration_EUR", "Userupgztaat");
            try
            {
             //   Usernames["Verify_Sofort_Registration_EUR"] = "Userupitozf";
                //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);
                if (!Usernames.ContainsKey("Verify_Sofort_Registration_EUR"))
                {
                    UpdateThirdStatus("Not Run");
                    Fail("Dependent Test case \"Verify_Sofort_Registration_EUR\" Failed");
                    return;
                }

                loginData.update_Login_Data(Usernames["Verify_Sofort_Registration_EUR"],
                     ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));


                acctData.Withdraw_Sofort(ReadxmlData("sofdata", "wPayee", DataFilePath.Accounts_Wallets),
                     ReadxmlData("sofdata", "wBankName", DataFilePath.Accounts_Wallets),
                     ReadxmlData("sofdata", "wIBAN", DataFilePath.Accounts_Wallets),
                 ReadxmlData("sofdata", "wSWIFTBIC", DataFilePath.Accounts_Wallets),
                 ReadxmlData("sofdata", "wCountry", DataFilePath.Accounts_Wallets),
                 ReadxmlData("sofdata", "wAmt", DataFilePath.Accounts_Wallets),
                 ReadxmlData("sofdata", "depWallet", DataFilePath.Accounts_Wallets));

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                AnW.Withdraw_Sofort(driverObj, acctData);
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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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
                //AnW.Registration_PlaytechPages(driverObj, ref regData);
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                // Pass("Customer registered succesfully");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                #endregion

                acctData.Update_deposit_giropay_account(ReadxmlData("girodata", "SwiftBIC", DataFilePath.Accounts_Wallets),
                    ReadxmlData("girodata", "sc", DataFilePath.Accounts_Wallets),
                    ReadxmlData("girodata", "esc", DataFilePath.Accounts_Wallets),
                    ReadxmlData("girodata", "depAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("girodata", "depWallet", DataFilePath.Accounts_Wallets));



                AnW.Register_Giropay(driverObj, acctData);
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
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


        [Test(Order = 9)]
        [RepeatOnFail]
        [Timeout(1500)]
        //[DependsOn("Verify_GiroPay_Registration_EUR")]
        public void Verify_GiroPay_Deposit_EUR()
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

            AddTestCase("Verify the payment deposit with Giropay method is successful", "Amount should be deposited to the selected wallet");
            try
            {

             

                acctData.Update_deposit_giropay_account(ReadxmlData("girodata", "SwiftBIC", DataFilePath.Accounts_Wallets),
                    ReadxmlData("girodata", "sc", DataFilePath.Accounts_Wallets),
                    ReadxmlData("girodata", "esc", DataFilePath.Accounts_Wallets),
                    ReadxmlData("girodata", "depAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("girodata", "depWallet", DataFilePath.Accounts_Wallets));
                //Usernames.Add("Verify_GiroPay_Registration_EUR", "Userupejhbgap");

                if (!Usernames.ContainsKey("Verify_GiroPay_Registration_EUR"))
                {
                    UpdateThirdStatus("Not Run");
                    Fail("Dependent Test case \"Verify_GiroPay_Registration_EUR\" Failed");
                    return;
                }

                loginData.update_Login_Data(Usernames["Verify_GiroPay_Registration_EUR"],
                    ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                AnW.Register_Giropay(driverObj, acctData);
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                // Pass("Customer registered succesfully");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                #endregion

                acctData.Update_deposit_Ideal_account(
                    ReadxmlData("idealdata", "depAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("idealdata", "depWallet", DataFilePath.Accounts_Wallets));


                AnW.Register_Ideal(driverObj, acctData);
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
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


}

  

