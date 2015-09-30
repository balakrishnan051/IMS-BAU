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
using Regression_Suite_IP2;


[assembly: ParallelismLimit]
namespace BVT_BAU
{
            
       
        [TestFixture,MbUnit.Framework.Timeout(15000)]
        [Parallelizable]
        public class EP_Registration : BaseTest
        {
            
            IP2Common ip2 = new IP2Common();
            AccountAndWallets AnW = new AccountAndWallets();
            Regression_AnW_Suite2.Registration1 Reg1 = new Regression_AnW_Suite2.Registration1();
            Regression_AnW_Suite2.Registration2 Reg2 = new Regression_AnW_Suite2.Registration2();
            Regression_AnW_Suite2.Telebet telebet = new Regression_AnW_Suite2.Telebet();
            Regression_AnW_CountryRegister.GeneralTests.AllCountryRegistration AllCountry = new Regression_AnW_CountryRegister.GeneralTests.AllCountryRegistration();
            Non_IMS_Suite.Sports sports = new Non_IMS_Suite.Sports();
            Non_IMS_Suite.Exchange Exchange = new Non_IMS_Suite.Exchange();
            Regression_Suite_IP2.Security_Reg_and_Login regLogin = new Regression_Suite_IP2.Security_Reg_and_Login();

            //[TestMethod]
            [Test(Order = 1)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]           
            public void Verify_realPlay_Cust_Registration_Vegas()
            {
                Reg1.Verify_realPlay_Cust_Registration_Vegas();
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(900)]
            public void Verify_CustomerRegistration_Telebet()
            {
                telebet.Verify_CustomerRegistration_Telebet();
             
            }

            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            [Test(Order = 3)]
            public void CreateCustomer_Germany_NOK()
            {
                AllCountry.CreateCustomer_Germany_NOK();
            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_Sports_Customer_Registration()
            {
                sports.Verify_Sports_Customer_Registration();
            }


            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            [Test(Order = 7)]
            public void Verify_Sports_Customer_Systemsource_OB()
            {
                sports.Verify_Sports_Customer_Systemsource_OB();

            }

            [Test(Order = 8)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_PasswordFormat_Registration_982()
            {
                regLogin.Verify_PasswordFormat_Registration_982();

            }

          //  [Test(Order=9)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_ExistinguserCheck_Registration()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();

                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                string regUrl = ReadxmlData("regUrl", "vegas_reg", DataFilePath.IP2_Authetication);
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, regUrl);
                #endregion


                try
                {
                    AddTestCase("Register using an existing username", "Given Passwordformat should be as expected");
                    
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    #endregion

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    ip2.Registration_UsernameCheck(driverObj, ref regData);
                    
                    BaseTest.Pass();


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_ExistinguserCheck_Registration failed");

                }

            }

            [Test(Order = 9)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Exchange_Customer_Registration()
            {
                Exchange.Verify_Exchange_Customer_Registration();
            }

            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            [Test(Order = 10)]
            public void CreateCustomer_UK_CHF()
            {
                AllCountry.CreateCustomer_UK_CHF();
            }

            [Test(Order = 11)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_Registration_Invalid_Login_FindAddressCheck()
            {
                Reg2.Verify_Registration_Invalid_Login_FindAddressCheck();
            }


            

            

        }//Registration class
        
        [TestFixture,MbUnit.Framework.Timeout(15000)]
        [Parallelizable]
        public class EP_Login : BaseTest
        {
           
            IP2Common ip2 = new IP2Common();
            AccountAndWallets AnW = new AccountAndWallets();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Regression_Suite_IP2.Security_Reg_and_Login RegLogin = new Regression_Suite_IP2.Security_Reg_and_Login();
            Regression_AnW_Suite1.MyAcct1 myacct1 = new Regression_AnW_Suite1.MyAcct1();
            Regression_Suite_IP2.Security_Sports Sports = new Security_Sports();


            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_AVFailed_Cust_Login_971()
            {
                Sports.Verify_AVFailed_Cust_Login_971();
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_Casino_Login_Logout()
            {

                
               
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
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

                string s = ReadxmlData("log", "ClientPlatform_casino", DataFilePath.IP2_Authetication);

                try
                {
                    AddTestCase("Verify login is successfull in Casino", "Login should be successfully");
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    regData.username = "User";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    #region Login
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_casino", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, ReadxmlData("log", "ClientPlatform_casino", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, Generic.GetPublic_IPAddress());
                    #endregion

                    #region Logout
                    AnW.LogoutFromPortal(driverObj);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_casino", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));

                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_casino", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());
                    #endregion


                    Pass("casino login successfully");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");

                    Fail("Verify_Casino_Login - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }


            }

            
            [Test(Order = 6)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            [Parallelizable]
            public void Verify_Sports_Login_964()
            {
                Sports.Verify_Sports_Login_964();

            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_Exchange_Login_Logout_1910()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
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
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);



                    ip2.LoginFromExchange(driverObj, loginData);
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, Generic.GetPublic_IPAddress());

                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                //    ip2.sessionManagement_Login(driverObj, sites.Replace("X", ""));

                    ip2.Logout_Exchange(driverObj);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));

                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());

                  //  ip2.sessionManagement_Logout(driverObj, sites.Replace("X", ""));


                    Pass("login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");

                    Fail("Exchange login - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }



            }
            
            [Test(Order = 8)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_Games_Login_Logout_1908()
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

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    ip2.LoginGames(driverObj, loginData);
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_games", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, ReadxmlData("log", "ClientPlatform_games", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, Generic.GetPublic_IPAddress());



                    ip2.Logout_Games(driverObj);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_games", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));

                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_games", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());


                    Pass("games login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Games_Login_Logout - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }


            }


            [Test(Order = 11)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            [Parallelizable]
            public void Verify_Casino_LockedDebitCust_Login()
            {
                RegLogin.Verify_Casino_LockedCreditCust_Login();
            }


            [Test(Order = 12)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            [Parallelizable]
            public void Verify_SearchCustomer_Logout_Telebet()
            {
                RegLogin.Verify_SearchCustomer_Logout_Telebet();
            }

         


        }//login class

        [TestFixture,MbUnit.Framework.Timeout(15000)]
        [Parallelizable]
        public class EP_SelfExcl : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            //  IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            Regression_Suite_IP2.SelfExcl_Admin_Portal SelfExAdmin = new Regression_Suite_IP2.SelfExcl_Admin_Portal();
            Regression_Suite_IP2.SelfExcl_Portal_Telebet SelfExc2 = new Regression_Suite_IP2.SelfExcl_Portal_Telebet();

            [Test(Order = 1)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            [Parallelizable]
            public void Verify_SelfExcl_ValidateOB()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion

                try
                {
                    AddTestCase("Verify setting selfexclusion for a debit customer in OB", "Credit limit should be set successfully");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);                   
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

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
            
            [Test(Order = 2)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            [Parallelizable]
            public void Verify_All_Product_Credit_SelfExc_Removed_Login()
            {
                SelfExAdmin.Verify_All_Product_Credit_SelfExc_Removed_Login();
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_SelfExcl_Login_Games_1109()
            {
                SelfExAdmin.Verify_SelfExcl_Login_Games_1109();
            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_SelfExcl_Login_Sports()
            {
                SelfExc2.Verify_SelfExcl_Login_Sports();
            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_SelfExcl_Login_Casino()
            {
                SelfExc2.Verify_SelfExcl_Login_Casino();
            }
           

        }     //self ex


        [TestFixture,MbUnit.Framework.Timeout(15000)]
        [Parallelizable]
        public class EP_IMS : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            //  IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            Regression_AnW_Suite1.IMSFeatures imsFeat = new Regression_AnW_Suite1.IMSFeatures();
            Regression_AnW_Suite1.FundTransfer Funtrans = new Regression_AnW_Suite1.FundTransfer();
          
            [Test(Order = 1)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_DepositLimit_IMS()
            {

                imsFeat.Verify_DepositLimit_IMS();

            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1600)]
            public void Verify_Fundtransfer_IMS_All()
            {
                Funtrans.Verify_Fundtransfer_IMS_All();
           
            }
            
            [Test(Order = 3)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_Freeze_UnFreeze_CustomerLogin()
            {
                imsFeat.Verify_Freeze_UnFreeze_CustomerLogin();

            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_AddCorrection_IMS()
            {
                imsFeat.Verify_AddCorrection_IMS();
            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1000)]
            public void Verify_Increase_DepositLimit_IMS()
            {
                imsFeat.Verify_Increase_DepositLimit_IMS();
            }
            


        }

        [TestFixture,MbUnit.Framework.Timeout(15000)]
        [Parallelizable]
        public class EP_MyAcct : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            //  IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            SeamLessWallet sw = new SeamLessWallet();
            Regression_AnW_Suite1.MyAcct1 myAcct1 = new Regression_AnW_Suite1.MyAcct1();
            Regression_AnW_Suite1.MyAcct2 myAcct2 = new Regression_AnW_Suite1.MyAcct2();
            Non_IMS_Suite.Sports sports = new Non_IMS_Suite.Sports();

            [MbUnit.Framework.Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 1)]
            public void Verify_MyAcct_DepositLimit_Responsible_Gambling()
            {
                myAcct1.Verify_MyAcct_DepositLimit_Responsible_Gambling();
            }

            [MbUnit.Framework.Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 2)]
            public void Verify_MyAcct_PasswordChange()
            {
                myAcct1.Verify_MyAcct_PasswordChange();
            }

            [MbUnit.Framework.Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 3)]
            public void Verify_MyAcct_Contact_Preferences()
            {
                myAcct1.Verify_MyAcct_Contact_Preferences();
            }

            /// <summary>
            /// Author:Nagamanickam
            /// Verify My account pages from sports url 
            /// </summary>
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_MyAcct_VerifyLinks_Sports()
            {

                sports.Verify_MyAcct_VerifyLinks_Sports();
            }

            [MbUnit.Framework.Timeout(2200)]
            [RepeatOnFail]
            [Test(Order = 5)]
            public void Verify_WalletBal_InAccHistory()
            {
                myAcct2.Verify_WalletBal_InAccHistory();
            }

                
        }

        [TestFixture,MbUnit.Framework.Timeout(15000)]
        [Parallelizable]
        public class EP_Cashier : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            //  IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            SeamLessWallet sw = new SeamLessWallet();
            Regression_AnW_Suite1.Banking_PayMethods1 Paymenthod1 = new Regression_AnW_Suite1.Banking_PayMethods1();
            Regression_AnW_Suite1.Banking_PayMethods2 Paymenthod2 = new Regression_AnW_Suite1.Banking_PayMethods2();
            Regression_AnW_Suite2.Banking3 Banking3 = new Regression_AnW_Suite2.Banking3();
            Regression_AnW_Suite1.FundTransfer Fundtrans = new Regression_AnW_Suite1.FundTransfer();

            [Test(Order = 1)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_Master_Card_Registration_EUR()
            {
                Paymenthod1.Verify_Master_Card_Registration_EUR();
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_Netteller_Registration_USD()
            {
                Paymenthod1.Verify_Netteller_Registration_USD();
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_Paypal_Registration_GBP()
            {
                Banking3.Verify_PayPal_Registration_GBP();
            }

            
            [Test(Order = 4)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_Skrill_Registration_GBP()
            {

                Paymenthod2.Verify_Skrill_Registration_GBP();

            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_Deposit_VisaCard()
            {
                Banking3.Verify_Deposit_VisaCard();
            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_Sofort_Registration_EUR()
            {
                Paymenthod2.Verify_Sofort_Registration_EUR();
            }

            
            [Test(Order = 7)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_FirstDeposit_CreditCard()
            {
                Paymenthod2.Verify_FirstDeposit_CreditCard();
            }

            

            [MbUnit.Framework.Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 8)]
            public void Verify_Withdraw_Portal_CreditCard()
            {
                Banking3.Verify_Withdraw_Portal_CreditCard();

            }

            [MbUnit.Framework.Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 9)]
            public void Verify_CancelWIthdrawal_Portal()
            {
                Banking3.Verify_CancelWIthdrawal_Portal();

            }

            [Test(Order = 10)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1600)]
            public void Verify_Transfer_Portal_AllWallets()
            {
                Fundtrans.Verify_Transfer_Portal_AllWallets();
            }

            [RepeatOnFail]
            [Test(Order = 13)]
            [MbUnit.Framework.Timeout(2200)]
            public void Verify_Transfer_Insufficient_Fund()
            {
                Fundtrans.Verify_Transfer_Insufficient_Fund();
            }

            [Test(Order = 14)]
            [Parallelizable]
            [MbUnit.Framework.Timeout(1500)]
            [RepeatOnFail]
            public void Verify_LiveDealer_WithdrawalMoreThanBalance()
            {
                Banking3.Verify_LiveDealer_WithdrawalMoreThanBalance();
            }

            [Test(Order = 15)]
            [RepeatOnFail]
            [MbUnit.Framework.Timeout(1500)]
            public void Verify_MultiplePaymethods_Registration_GBP()
            {
                Paymenthod1.Verify_MultiplePaymethods_Registration_GBP();
            }
          


        }



        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class EP_CC_Account_Management : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            SeamLessWallet sw = new SeamLessWallet();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Regression_Suite_IP2.CC_Account_Management Ip2SourceCode = new Regression_Suite_IP2.CC_Account_Management();
            Regression_Suite_IP2.Sportsbook ip2SW = new Regression_Suite_IP2.Sportsbook();
            ChequePrinting_ManualAdj Ip2Source = new ChequePrinting_ManualAdj();

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Registration_Of_CreditCustomer_2644()
            {

                Ip2SourceCode.Verify_Registration_Of_CreditCustomer_2644();
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SetupCredit_CreditCustomer_2680()
            {
               Ip2SourceCode.Verify_SetupCredit_CreditCustomer_2680();
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_WriteOFF_CreditCustomer_2683_2682()
            {
              Ip2SourceCode.Verify_WriteOFF_CreditCustomer_2683_2682();
            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_FundTransfer_CreditCustomer_2688()
            {
             Ip2SourceCode.Verify_FundTransfer_CreditCustomer_2688();
            }

            [Timeout(1500)]
            [RepeatOnFail]
            [Test(Order = 5)]
            public void Verify_Withdraw_CreditCustomer_2657()
            {
              Ip2SourceCode.Verify_Withdraw_CreditCustomer_2657();


            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Envoy_CreditCustomer_2686()
            {
              Ip2SourceCode.Verify_Envoy_CreditCustomer_2686();
            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_CreditCustomer_CardReg_Telebet_2685()
            {
              Ip2SourceCode.Verify_CreditCustomer_CardReg_Telebet_2685();
            }

            [Test(Order = 8)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Cheque_CreditCustomer_2687()
            {
               Ip2SourceCode.Verify_Cheque_CreditCustomer_2687();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 9)]
            [Parallelizable]
            public void Verify_CreditCust_FreeBet_2689_2566_2583()
            {
               Ip2SourceCode.Verify_CreditCust_FreeBet_2689_2566_2583();

            }

            [Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(2200)]
            public void Verify_EndCredit_CreditCustomer_2671()
            {
          Ip2SourceCode.Verify_EndCredit_CreditCustomer_2671();
            }
            
            [Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(2200)]
            public void Verify_StakeGreater_thanCreditLimit_2692()
            {
               Ip2SourceCode.Verify_StakeGreater_thanCreditLimit_2692();
            }

            [Test(Order = 12)]
            [RepeatOnFail]
            [Timeout(2200)]
            public void Verify_Deposit_CreditLimitReached_2695()
            {
             Ip2SourceCode.Verify_Deposit_CreditLimitReached_2695();
        }

            [Test(Order = 12)]
            [RepeatOnFail]
            [Timeout(2200)]
            public void Verify_Cheque_Withdrawal_2771()
            {
                Ip2Source.Verify_Cheque_Withdrawal_2771();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 13)]
            [Parallelizable]
            public void Verify_AdjustToStake_2839()
            {
                Ip2Source.Verify_AdjustToStake_2839();
                
                
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 13)]
            [Parallelizable]
            public void Verify_Freebet_Promo_MyAcct_2545()
            {
                ip2SW.Verify_Freebet_Promo_MyAcct_2545();
                
                
            }

            

    }//AccountManagement class
    
    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class Sportsbook : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            SeamLessWallet sw = new SeamLessWallet();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
              Regression_Suite_IP2.Sportsbook Ip2SourceCode = new Regression_Suite_IP2.Sportsbook();

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_SingleWallet_Check_PT_SPorts_2524()
            {
        Ip2SourceCode.Verify_SingleWallet_Check_PT_SPorts_2524();
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_SingleWallet_Check_IMS_OB_2528()
            {
               Ip2SourceCode.Verify_SingleWallet_Check_IMS_OB_2528();
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_SingleWallet_Check_NonUK_2534()
            {
             Ip2SourceCode.Verify_SingleWallet_Check_NonUK_2534();
            }
                    

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 5)]
            public void Verify_Forecast_Tricast_2564()
            {
             Ip2SourceCode.Verify_Forecast_Tricast_2564();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 6)]
            public void Verify_EachWay_2562()
            {
             Ip2SourceCode.Verify_EachWay_2562();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 7)]
            public void Verify_HandiCap_2568()
            {
         Ip2SourceCode.Verify_HandiCap_2568();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 8)]
            public void Verify_BIR_2563()
            {
             Ip2SourceCode.Verify_BIR_2563();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 9)]
            public void Verify_Trixie_2559()
            {
               Ip2SourceCode.Verify_Trixie_2559();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 10)]
            public void Verify_Yankee_2560()
            {
             Ip2SourceCode.Verify_Yankee_2560();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 11)]
            public void Verify_Candian_2561()
            {
            Ip2SourceCode.Verify_Candian_2561();
            }


            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 11)]
            public void Verify_Lucky63_4147()
            {
         Ip2SourceCode.Verify_Lucky63_4147();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 6)]
            public void Verify_Double_2557()
            {
           Ip2SourceCode.Verify_Double_2557();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 6)]
            public void Verify_Single_NonUK_2574()
            {
              Ip2SourceCode.Verify_Single_NonUK_2574();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 10)]
            public void Verify_Treble_2558()
            {
             Ip2SourceCode.Verify_Treble_2558();
            }

        }//Sportsbook Seamlesswallet

         [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Telebet : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            SeamLessWallet sw = new SeamLessWallet();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
             Regression_Suite_IP2.Telebet Ip2SourceCode = new Regression_Suite_IP2.Telebet();

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 1)]
            public void Verify_BIR_Telebet_2580()
            {
               Ip2SourceCode.Verify_BIR_Telebet_2580();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 2)]
            public void Verify_Double_Telebet_2575()
            {
               Ip2SourceCode.Verify_Double_Telebet_2575();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 3)]
            public void Verify_Trexie_Telebet_2577()
            {
                Ip2SourceCode.Verify_Trexie_Telebet_2577();
             
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 4)]
            public void Verify_Accumulator_Telebet_2576()
            {
              Ip2SourceCode.Verify_Accumulator_Telebet_2576();
            }
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_SingleWallet_Telebet_2527()
            {
              Ip2SourceCode.Verify_SingleWallet_Telebet_2527();
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 4)]
            public void Verify_Yankee_Telebet_2578()
            {
              Ip2SourceCode.Verify_Yankee_Telebet_2578();
            }
         }//Telebet

}//NameSpace
