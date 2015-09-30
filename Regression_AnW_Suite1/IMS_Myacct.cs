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


namespace Regression_AnW_Suite1
{


    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class IMSFeatures : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
        Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
       // [Test(Order = 1)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_Freeze_CustomerStatusInOB()
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

            AddTestCase("Verify the Freezed user in IMS is in Suspended state in OB", "Freezed user should be in Suspend status");
            try
            {
                //acctData.Update_deposit_withdraw_Card(testdata.AppSettings.Settings["CDCC1"].Value, testdata.AppSettings.Settings["MaxAmount"].Value, testdata.AppSettings.Settings["CDDepAmount"].Value, testdata.AppSettings.Settings["CDDepWallet"].Value, testdata.AppSettings.Settings["CDWithWallet"].Value);
                baseIMS.Init();

                loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                    ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets));
                loginData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
                loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, loginData.password);
                // BaseTest.Pass("Customer created successfully in IMS");
                commIMS.FreezeTheCustomer(baseIMS.IMSDriver);
                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
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

      //  [Test(Order = 2)]
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

                loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                    ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets));
                loginData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
                loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, loginData.password);
                //   BaseTest.Pass("Customer created su Iccessfully inMS");

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
                BaseTest.Pass();

                commIMS.FreezeTheCustomer(baseIMS.IMSDriver);
                //baseIMS.Quit();
                //baseIMS.Init();
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                commIMS.DisableFreeze(baseIMS.IMSDriver);
                baseIMS.Quit();
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

        [Test(Order = 1)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_Freeze_UnFreeze_CustomerLogin()
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
            driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
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

                AnW.LoginFromPortal_FrozenCust(driverObj, loginData, "Frozen customer error mismatch/error did not appear");

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


       // [Test(Order = 3)]
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
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
              
                #region NewCustTestData
                regData.email = "t@aditi.com";
                Testcomm.Createcustomer_PostMethod(ref regData);
                #endregion
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                // Pass("Customer registered succesfully");
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                string creditCardNumber = TD.createCreditCard("Visa").ToString();
                Console.WriteLine("Credit Card:" + creditCardNumber);
                #endregion
                commIMS.AddCreditCard(baseIMS.IMSDriver, creditCardNumber);
                Pass();
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
            driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
            #endregion

            #region Declaration
            IMS_AdminSuite.IMS_Base IMSBase = new IMS_Base();
            IMS_AdminSuite.Common IMSComm = new IMS_AdminSuite.Common();
            Registration_Data regData = new Registration_Data();
            // Configuration testdata = TestDataInit();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            #endregion


            //Configuration testdata = TestDataInit();
            try
            {
                Login_Data loginData = new Login_Data();
                // AddTestCase("Change the password", "Customer should be created successfully with Selfexcluded status");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                // wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Join button in home page", 0, false);
                // AnW.Registration_PlaytechPages(driverObj, ref regData);
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + "Password2");


                AnW.LogoutFromPortal(driverObj);
                IMSBase.Init();
                IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, regData.username);
                IMSComm.ResetPassword(IMSBase.IMSDriver, "Password2");
                IMSBase.Quit();
                Thread.Sleep(TimeSpan.FromMinutes(1));
                loginData.update_Login_Data(regData.username, "Password2", "Tester");
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
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
                // regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, "user", pass);
                // Pass("Customer registered succesfully");

                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));


                #region NewCustTestData
                regData.email = "abc@abc.com";
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                // AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + pass);
                AddTestCase("Username:" + regData.username + " Password:" + pass, "");
                Pass();

                #endregion
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                commIMS.SetDepositLimit(baseIMS.IMSDriver, "500");
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

       // [Test(Order = 6)]
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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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
                AnW.OpenRegistrationPage(driverObj);
                AnW.Registration_PlaytechPages(driverObj, ref regData);
                Pass("Customer has registered in successfully");

                AddTestCase("Verify affiliate details in IMS", "Affiliate details should match with registered details");
                imsAdmin.Init();
                commIMS.SearchCustomer_Newlook(imsAdmin.IMSDriver, regData.registeredUsername,false);
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
                //       AnW.OpenRegistrationPage(driverObj);
                //  AnW.Registration_PlaytechPages(driverObj, ref regData);
                // Pass("Customer registered succesfully");
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData, "mr", "200");
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion


                #endregion

                String portalWindow =  AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, "500");

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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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
                loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
              ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
              ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                AnW.LogoutFromPortal(driverObj);

                imsAdmin.Init();
                AddTestCase("Customer : " + ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets), ""); Pass();
                commIMS.SearchCustomer_Newlook(imsAdmin.IMSDriver, ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets));
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

                #region NewCustTestData
                regData.email = "t@aditi.com";
                Testcomm.Createcustomer_PostMethod(ref regData, "mr", "200");
                #endregion

                 commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver,regData.username,false);
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + pass);

                AddTestCase("Username:" + regData.username + " Password:" + pass, "");
                Pass();
                string creditCardNumber = TD.createCreditCard("Visa").ToString();
                Console.WriteLine("Credit Card:" + creditCardNumber);
                #endregion
                commIMS.AddCreditCard(baseIMS.IMSDriver, creditCardNumber);
                //commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                
                commIMS.AllowDuplicateCreditCard_Newlook(baseIMS.IMSDriver, creditCardNumber.Substring(0, 6), creditCardNumber.Substring(creditCardNumber.Length - 4, 4));

                wAction.Click(baseIMS.IMSDriver, By.LinkText(regData.username), "Username link not found in Credit card page");
                #region NewCustTestData
                regData.email = "t@aditi.com";
                regData.username = "User";
                Testcomm.Createcustomer_PostMethod(ref regData, "mr", "200");
                #endregion

                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);

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
            driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
            #endregion

            #region Declaration
            Registration_Data regData = new Registration_Data();
            Login_Data loginData = new Login_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            AdminSuite.Common commOB = new AdminSuite.Common();
            String balance = "0";
            #endregion

            try
            {
                #region Create Customer from Playtech pages
                // AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                //       AnW.OpenRegistrationPage(driverObj);
                //  AnW.Registration_PlaytechPages(driverObj, ref regData);
                //  Pass("Customer registered succesfully");
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                balance = AnW.FetchBalance_Portal_Homepage(driverObj);
                AnW.LogoutFromPortal(driverObj);
                #endregion


                #region Adding correction in IMS
                BaseTest.AddTestCase("Adding correction amount to Customer in IMS", "Correction amount to added to Customer in IMS");
                baseIMS.Init();
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.Accounts_Wallets), ReadxmlData("mc", "TransactionType_Add", DataFilePath.Accounts_Wallets));
           
                
                BaseTest.Pass("Correction amount to added to Customer in IMS");
                #endregion

                Thread.Sleep(TimeSpan.FromSeconds(40));
                #region Verify the balance in portal
                BaseTest.AddTestCase("Verify the Correction amount added to Customer in Portal", "Verified Correction amount added to Customer in Portal");
                loginData.update_Login_Data(regData.username, regData.password, regData.fname);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                String newBalance = AnW.FetchBalance_Portal_Homepage(driverObj);
                if (Convert.ToDouble(newBalance) != StringCommonMethods.ReadDoublefromString(balance) + 10)
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
                loginData.update_Login_Data(ReadxmlData("ccdata", "user", DataFilePath.Accounts_Wallets),
                ReadxmlData("ccdata", "pwd", DataFilePath.Accounts_Wallets),
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
                string CCfname = wAction.GetAttribute(baseIMS.IMSDriver, By.Name("firstname"), "value", "First name field not found");
                string CClname = wAction.GetAttribute(baseIMS.IMSDriver, By.Name("lastname"), "value", "Last name field not found");

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
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                #endregion

                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                wAction.Clear(baseIMS.IMSDriver, By.Id("email"), "Email text box not loaded/found", 0, false);
                wAction.Type(baseIMS.IMSDriver, By.Id("email"), "new@playtech.com", "Email text box not loaded/found");
                wAction.Click(baseIMS.IMSDriver, By.Id("update"), "update button not loaded/found");
                wAction.WaitAndAccepAlert(baseIMS.IMSDriver);
                System.Threading.Thread.Sleep(8000);
                wAction.Click(baseIMS.IMSDriver, By.LinkText("username"), "username event log link not loaded/found");

                wAction.WaitAndMovetoFrame(baseIMS.IMSDriver, "main-content");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(baseIMS.IMSDriver, By.XPath("//tr[td[a[contains(text(),'SandeepKR1')]] and td[contains(text(),'user_modified') ]]")), "No log found for email modificaton");
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
    public class MyAcct1 : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
        Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
        SeamLessWallet sw = new SeamLessWallet();


        #region Myacct

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
            // Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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

                AddTestCase("Verify All links in MY Acount page is present", "All the MY Acct links should be present in the page");

                loginData.update_Login_Data(ReadxmlData("lgnsdata", "user", DataFilePath.Accounts_Wallets),
                    ReadxmlData("lgnsdata", "pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("lgnsdata", "Fname", DataFilePath.Accounts_Wallets));

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
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


        [Timeout(2200)]
        [RepeatOnFail]
        [Test(Order = 2)]
        public void Verify_MyAcct_DepositLimit_Responsible_Gambling()
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
                //       AnW.OpenRegistrationPage(driverObj);
                //  AnW.Registration_PlaytechPages(driverObj, ref regData);
                //  Pass("Customer registered succesfully");
                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                #endregion

                String portalWindow =  AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj);

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
                // AnW.Registration_PlaytechPages(driverObj, ref regData);

                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion


                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                String portalWindow =     AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_ChangePassword_Func(driverObj, regData.password);
                driverObj.Close();
                driverObj.SwitchTo().Window(portalWindow);
                driverObj.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                AnW.LogoutFromPortal(driverObj);
                Thread.Sleep(TimeSpan.FromMinutes(1));
                loginData.update_Login_Data(regData.username, "Newpassword1", "fname");
                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);

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
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Russia", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets), "DE");
                AnW.OpenRegistrationPage(driverObj);
                AnW.Registration_PlaytechPages(driverObj, ref regData);
                //#region NewCustTestData
                //regData.email = "t@aditi.com";
                //Testcomm.Createcustomer_PostMethod(ref regData);
                //loginData.username = regData.username;
                //loginData.password = regData.password;
                //AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                //#endregion

                Pass("Customer registered succesfully");
                String portalWindow =     AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_CheckNonUKcustomer_Country(driverObj);
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
                 regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_Russia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                            #region NewCustTestData
                regData.email = "t@aditi.com";
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                #endregion

                String portalWindow = AnW.OpenMyAcct(driverObj);
                Dictionary<string, string> modDetails = new Dictionary<string, string>();
                AnW.MyAccount_ModifyDetails(driverObj,ref modDetails);
                AnW.MyAccount_Edit_ContactPref_DirectMail(driverObj);
                driverObj.Quit();


                baseIMS.Init();
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                Testcomm.VerifyCustDetailinIMS_Modified(baseIMS.IMSDriver, modDetails);



                BaseTest.AddTestCase("Verify contact preference updated in IMS", "Contact preferences should be updated");
                wAction.Click(baseIMS.IMSDriver, By.Id("imgsec_contact"), "Contact preference link not found");
                System.Threading.Thread.Sleep(2000);
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(baseIMS.IMSDriver, By.XPath(IMS_Control_PlayerDetails.DirectMailValidation_Checkbox)), "Direct Mail - Contact preferences is not updated in IMS");
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


        #endregion

    }//IMS class


    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class MyAcct2 : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();
        Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
        SeamLessWallet sw = new SeamLessWallet();


        #region Myacct

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
                loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.Accounts_Wallets),
                  ReadxmlData("lgndata", "pwd", DataFilePath.Accounts_Wallets),
                  ReadxmlData("lgndata", "Fname", DataFilePath.Accounts_Wallets));

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_ValidateDiabledFields(driverObj);
                AnW.MyAccount_EditPostCode(driverObj, "ha29sr", "Worple Way");
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
                loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                        ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                AnW.OpenMyAcct(driverObj);
              
               AnW.ComparingWalletBalances(driverObj);
               
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



                loginData.update_Login_Data(ReadxmlData("betdata", "user", DataFilePath.Accounts_Wallets_stg),
                                        ReadxmlData("betdata", "pwd", DataFilePath.Accounts_Wallets_stg),
                                        ReadxmlData("betdata", "Fname", DataFilePath.Accounts_Wallets_stg));
                excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                   "UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                string amt = ReadxmlData("event", "eAmt", DataFilePath.IP2_SeamlessWallet);

                
                AnW.Login_Sports(driverObj, loginData);
                AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);
                AnW.placeBet_Nelson(driverObj);
                 AnW.OpenMyAcct(driverObj);              
                AnW.Verify_Sports_BetHistory(driverObj, eventName);
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
        //[Test(Order = 9)]
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
                //AnW.Registration_PlaytechPages(driverObj, ref regData);
                //Pass("Customer registered succesfully");

                #region NewCustTestData
                Testcomm.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                AnW.LoginFromEcom(driverObj, loginData);
                #endregion

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                AnW.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets));
                string sel1 = ReadxmlData("eventID", "sel1", DataFilePath.Accounts_Wallets);


                //try
                //{
                //    List<IWebElement> ele = driverObj.FindElements(By.XPath("//a[contains(@class,'betbutton')]")).ToList();
                //    ele[0].GetAttribute("value");
                //}
                //catch (Exception e) { BaseTest.Fail("No selection found for the event"); }
                double odds = Math.Round(FrameGlobals.FractionToDouble(sel1), 2) + 1;
                sel1 = Convert.ToString(odds);

                AnW.VerifySelection(driverObj, sel1);
                AnW.OpenMyAcct(driverObj); 
                driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                AnW.MyAccount_ModifyPreferences(driverObj, true, "Fraction", true);
                driverObj.Close();
                wAction.WaitAndMovetoPopUPWindow(driverObj, portalWindow, "Unable to switch to main window");
                AnW.LogoutFromPortal(driverObj);
                loginData.update_Login_Data(regData.registeredUsername, regData.password, "Tester");
                AnW.LoginFromEcom(driverObj, loginData);
                AnW.VerifySelection(driverObj, ReadxmlData("eventID", "sel1", DataFilePath.Accounts_Wallets));
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
        //[Test(Order = 9)]
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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion

            try
            {
                AddTestCase("Verify that Disabiling the AutoTop up option, placing a Bet throws an error", "Customer should not be able to place bet through Auto Top Up");


                loginData.update_Login_Data(ReadxmlData("autodata", "user", DataFilePath.Accounts_Wallets_stg),
                                        ReadxmlData("autodata", "pwd", DataFilePath.Accounts_Wallets_stg),
                                        "");
                excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                   "UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                AnW.LoginFromEcom(driverObj, loginData);
                AnW.Ecom_MyAcct_AutoTopUP(driverObj);
                AnW.LogoutFromPortal_Ecom(driverObj);
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromEcom(driverObj, loginData);
                AnW.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.Accounts_Wallets));
                AnW.AddToBetSlipInsuff(driverObj, 10);
                Pass("Customer registered succesfully");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Disabled - Auto Top Up scenario failed");
            }

        }
        #endregion

    }//IMS class









}