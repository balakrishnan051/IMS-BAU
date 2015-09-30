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

[assembly: ParallelismLimit]
namespace Regression_AnW_CountryRegister
{
    [AssemblyFixture]
   public class GeneralTests
    {
        [FixtureTearDown]
        public void AfterRunAssembly()
        {
            BaseTest.EndOfExecution();
        }
        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class AllCountryRegistration : BaseTest
        {
            

            AccountAndWallets AnW = new AccountAndWallets();

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Germany_Euro()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Germany", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "EUR";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("€"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]

            public void CreateCustomer_Sweden_NZD()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Sweden", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "NZD";
                    regData.email = "@aditi.com";
                                             AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    //if (!bal.Contains("RUB"))
                    if (!bal.Contains("NZ$"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test(Order = 3)]
            public void CreateCustomer_Finalnd_SEK()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verified the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Finland", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "SEK";
                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("kr"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }


            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Germany_NOK()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verified the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Germany", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "NOK";
                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains(regData.currency))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Sweden_USD()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Sweden", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "USD";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency should be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("$"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }


            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Netherlands_Euro()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Netherlands", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "EUR";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("€"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
         //   [Test]
            [Parallelizable]
            public void CreateCustomer_Malaysia_SGD()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Malaysia", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "SGD";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("S$"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Portugal_EUR()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Portugal", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "EUR";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("€"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Norway_NOK()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Norway", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "NOK";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains(regData.currency))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_UK_CHF()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "CHF";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains(regData.currency))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Sweden_SEK()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Sweden", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "SEK";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("kr"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Brazil_USD()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Brazil", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "USD";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency should be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("$"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Mexico_USD()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Mexico", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "USD";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency should be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("$"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            [RepeatOnFail]
            [Timeout(1000)]
            [Test]
            [Parallelizable]
            public void CreateCustomer_Romania_EUR()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LiveDealerURL);
                #endregion
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common cmn = new Ladbrokes_IMS_TestRepository.Common();
                // Configuration testdata = TestDataInit();


                AddTestCase("Verify the customer registration from Plyatech pages.", "Customer should be created successfully");
                try
                {
                    //Updating customer details
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), "Romania", ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                    regData.currency = "EUR";
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    //creating customer
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");
                    //writing customer name in excel
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    //verifying currency of customer 
                    AddTestCase("Verified the currency in the portal home page", "Appropriate Currency shoul be displayed in home page");
                    string bal = cmn.HomePage_Balance(driverObj);
                    if (!bal.Contains("€"))
                    {
                        Fail("Currency type is not correct for the customer registered");
                    }
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(MyBrowser, "portal");
                    Fail("Customer Registration/Auto Login has failed");
                    Pass();
                }
            }

            //=--------------------------------------------------------------------------------



            //[Test]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Title_Russian()
            {
                #region Declaration
                 IMS_Base baseIMS = new IMS_Base();
                 IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
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
                Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
                string temp = Registration_Data.title;
                try
                {
                    Registration_Data.title = "Г - н";
                    AddTestCase("Russian Language", "Registration should be successfull");

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_germany_german", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));

                    //Change the language
                    wAction._ClickAndMove(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "LanguageMenu", FrameGlobals.elementTimeOut, "Header element Change language menu not found");
                    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Language_Russian", "Language Russian in menu not found", 0, false);

                    //register in Russian
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Russian_Btn", "Join button not found", 0, false);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                   // Testcomm.Createcustomer_PostMethod(ref regData, Registration_Data.title);
                    Pass("Customer registered succesfully");

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username);
                    baseIMS.IMSDriver.SwitchTo().DefaultContent();
                    BaseTest.Assert.IsTrue(baseIMS.IMSDriver.FindElement(By.Id("title")).GetAttribute("value") == Registration_Data.title, "title did not match in IMS Admin. Expected:" +
                      Registration_Data.title + " Actual:" + baseIMS.IMSDriver.FindElement(By.Id("title")).GetAttribute("value"));

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify title : scenario failed");
                }
                finally
                {
                    Registration_Data.title = temp;
                }
            }


        }//AllCountryRegistration class
    }
}
