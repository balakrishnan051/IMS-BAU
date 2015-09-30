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


namespace Regression_Suite_IP2
{
  

      
      
        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Security_Reg_and_Login : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();          
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            
            //IMS_Base baseIMS = new IMS_Base();

            /// <summary>
            /// Naga
            /// Message to be displayed when customer logs in with invalid username and invalid password from online
            /// </summary>
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Invalid_LoginError_BothInvalid_DebitCust()
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
                driverObj = browserInitialize(iBrowser,FrameGlobals.SportsURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                  
                    loginData.update_Login_Data("Test",
                                            "Test", "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                   
                    
                    #region InvalidLogin
                       string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                       if (sites.Contains("S"))
                       {
                           AddTestCase("Sports - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           ip2.Login_SecurityError_Sports(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass();
                       }

                       if (sites.Contains("P"))
                       {
                           AddTestCase("PT - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Sports not opened", 60);
                           AnW.LoginFromPortal_SecurityError_Prompt(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass();
                       }
                       if (sites.Contains("G"))
                       {

                           AddTestCase("GamesUrl - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "games page not opened");
                           ip2.LoginGames_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass("games");
                       }
                       if (sites.Contains("L"))
                       {
                           AddTestCase("LottosURL - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           wAction.OpenURL(driverObj, FrameGlobals.LottosURL, "lottos page not opened");
                           ip2.LoginFromLottos_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass("lottos");
                       }
                       if (sites.Contains("E"))
                       {
                           AddTestCase("Ecom - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "ecom page not opened");
                           ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass("ecom");
                       }
                    #endregion


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Invalid_LoginError : scenario failed");
                }
            }
         
            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_All_PT_Login_964_1545()
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
                    AnW.LoginFromPortal(driverObj, loginData, iBrowser);
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


                    AddTestCase("Verify login is successfull in Poker", "Login should be successfully");
                    #region NewCustTestData
                    regData.email = "t@aditi.com"; regData.username = "User";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    wAction.WaitforPageLoad(driverObj);
                    wAction.OpenURL(driverObj, FrameGlobals.PokerURL, "Poker page not loaded", 60);

                    #region Login
                    AnW.LoginFromPortal(driverObj, loginData, iBrowser);                   
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                     commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_poker", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                      commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, ReadxmlData("log", "ClientPlatform_poker", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, Generic.GetPublic_IPAddress());
                    #endregion

                    #region Logout
                    AnW.LogoutFromPortal(driverObj);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                     commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_poker", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));

                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                     commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_poker", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());
                    #endregion

                    Pass("Poker login successfully");

                    AddTestCase("Verify login is successfull in Vegas", "Login should be successfully");               
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    #region NewCustTestData
                    regData.email = "t@aditi.com"; regData.username = "User";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    wAction.WaitforPageLoad(driverObj);
                    wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Vegas page not loaded", 60);

                    #region Login
                    AnW.LoginFromPortal(driverObj, loginData, iBrowser);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                     commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_vegas", DataFilePath.IP2_Authetication));
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
                    Pass("Vegas login successfully");


                    AddTestCase("Verify login is successfull in Bingo", "Login should be successfully");
                    #region NewCustTestData
                    regData.email = "t@aditi.com"; regData.username = "User";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    wAction.WaitforPageLoad(driverObj);
                    wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Bingo page not loaded", 60);

                    #region Login
                    AnW.LoginFromPortal(driverObj, loginData, iBrowser);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                     commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_bingo", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                      commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, ReadxmlData("log", "ClientPlatform_bingo", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, Generic.GetPublic_IPAddress());
                    #endregion

                    #region Logout
                    AnW.LogoutFromPortal(driverObj);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_bingo", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));

                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_bingo", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());
                    #endregion
                    Pass("bingo login successfully");
                  

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");

                    Fail("Verify_AllPT_Login - failed");
                }
                finally
                {
                   baseIMS.Quit();
                }


            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SearchCustomer_Logout_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration               
                Login_Data loginData = new Login_Data();              
                #endregion

                try
                {
                    AddTestCase("Search customer from Telebet pages", "Search should be successful");
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
                    AddTestCase("Logout customer from Telebet pages", "Logout should be successful");
                    commTB2.LogoutCustomer(baseTB2.IMSDriver);
                    Pass();


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

          

            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Casino_LockedDebitCust_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("Verify login is not successfull for locked customer in Casino", "Login should not be successfully");
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com";
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password + "RL";
                    #region getLoginAttempt
                    baseIMS.Init();
                    Login_Data.loginAttempt = commIMS.GetLoginFailAttempt(baseIMS.IMSDriver);

                    #endregion



                    for (int i = 0; i < (Login_Data.loginAttempt); i++)
                    {
                        AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");
                        wAction.PageReload(driverObj);
                    }

                    loginData.password = regData.password;
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");


                    AddTestCase("Search customer in IMS and check for locked error", "Customer not locked in IMS");
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username,false);
                    Assert.IsTrue(wAction.WaitUntilElementPresent(baseIMS.IMSDriver, By.XPath("id('sectionsContainer')/div[contains(text(),'Account is temporarily locked')]")), "Temp locked message not found in IMS");
                    Pass();
                    baseIMS.Quit();
                    WriteUserName(loginData.username);
                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            [DependsOn("Verify_Casino_LockedCreditCust_Login")]
            public void Verify_Search_LockedCustomer_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();
                #endregion

                try
                {
                    AddTestCase("SEC-06 Verify if Telebet agent is able to retrieve locked customer", "The customer should not get searched");

                   // Usernames["Verify_Casino_LockedCreditCust_Login"] = "UserUPFHSNX";


                    regData.username = Usernames["Verify_Casino_LockedCreditCust_Login"];
                //    regData.username = "Userupcxsvbd ";
                 
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                

                    

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, regData.username);
                    WriteCommentToMailer("UserName: " + regData.username);

                    AddTestCase("Searched Customer in Telebet:  " + regData.username, ""); Pass();
                    Pass("Error success");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_Search_SelfEXCustomer_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }
            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            [DependsOn("Verify_Casino_LockedDebitCust_Login")]
            public void Verify_Reset_FailedAttempts()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion

                try
                {
                    AddTestCase("SEC-08a IMS admin should be able to remove temporary lock on customers account and customer is able to login with valid password", "The customer should be allowed to login after reset");

                    loginData.username = Usernames["Verify_Casino_LockedDebitCust_Login"];
                    loginData.password = regData.password;
                    //    regData.username = "Userupcxsvbd ";
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + loginData.username, ""); Pass();

                    #region resetLoginattempt
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Reset_FailedLoginAttempt(baseIMS.IMSDriver);

                    #endregion
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    WriteUserName(loginData.username);
                    Pass("reset success");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Reset_FailedAttempts - failed");
                }
                finally
                {

                    baseIMS.Quit();
                }
            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            [DependsOn("Verify_Reset_FailedAttempts")]
            public void Verify_Sports_PlaceBet_Locked_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                AdminBase baseOB = new AdminBase();
                IMS_Base baseIMS = new IMS_Base();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Verify that user logs in successfully from Betslip when temporary lock is removed for the account", "Sports login should be successfull");

                  //  Usernames["Verify_Reset_FailedAttempts"]= "Userupfhltl";
                    loginData.username = Usernames["Verify_Reset_FailedAttempts"];
                    loginData.password = regData.password;


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //Set stake in OB
                    baseOB.Init();
                    adminComm.SearchCustomer(loginData.username, baseOB.MyBrowser);
                    adminComm.SetStakeLimit(baseOB.MyBrowser);
                    baseOB.Quit();


                   // wAction.Click(driverObj, By.LinkText("Close"));
               
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);

                    //bet now button should b displayed
                    ip2.login_fromBetslip(driverObj, loginData);

                    Pass("Sports login successfull");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseOB.MyBrowser, "OB");
                    Fail("Verify_Sports_PlaceBet_Login - Failed");
                }
                finally
                {
                    baseOB.Quit();
                }

            }


            [Test(Order =8 )]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Casino_LockedCreditCust_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("Verify customer gets locked if tried with invalid password for max times", "Login should not be successfully");
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();
                    loginData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", null, "@aditi.com");
                    Login_Data.loginAttempt = commIMS.GetLoginFailAttempt(baseIMS.IMSDriver);
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password + "8";

                    for (int i = 0; i < (Login_Data.loginAttempt); i++)
                    {
                        AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");
                        wAction.PageReload(driverObj);
                    }

                    loginData.password = regData.password;

                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");

                    AddTestCase("Search customer in IMS and check for locked error", "Customer not locked in IMS");
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username,false);
                    Assert.IsTrue(wAction.WaitUntilElementPresent(baseIMS.IMSDriver, By.XPath("id('sectionsContainer')/div[contains(text(),'Account is temporarily locked')]")), "Temp locked message not found in IMS");
                    Pass();

                    WriteUserName(loginData.username);
                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }


            [Test(Order = 9)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Casino_LastAttempt_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("Verify login is not successfull for locked customer in Casino", "Login should not be successfully");
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    #region NewCustTestData
                    regData.email = "@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password + "RL";
                    #region getLoginAttempt
                    baseIMS.Init();
                    Login_Data.loginAttempt = commIMS.GetLoginFailAttempt(baseIMS.IMSDriver);

                    #endregion



                    for (int i = 0; i < (Login_Data.loginAttempt)-1; i++)
                    {
                        AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");
                        wAction.PageReload(driverObj);
                    }

                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData, iBrowser);


                  //  AddTestCase("Search customer in IMS and check for locked error", "Customer not locked in IMS");
                  //  commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                  //  Assert.IsTrue(wAction.WaitUntilElementPresent(baseIMS.IMSDriver, By.XPath("id('sectionsContainer')/div[contains(text(),'Account is temporarily locked')]")), "Temp locked message not found in IMS");
                //    Pass();
                    baseIMS.Quit();

                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Vegas_Lock_DebitCust_CookiesClear_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("Verify customer gets locked if tried with invalid password for max times", "Login should not be successfully");
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    #region NewCustTestData
                    regData.email = "@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password + "RL";
                    #region getLoginAttempt
                    baseIMS.Init();
                    Login_Data.loginAttempt = commIMS.GetLoginFailAttempt(baseIMS.IMSDriver);

                    #endregion



                    for (int i = 0; i < (Login_Data.loginAttempt); i++)
                    {
                        AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");
                        driverObj.Manage().Cookies.DeleteAllCookies();
                        wAction.PageReload(driverObj);
                    }

                    loginData.password = regData.password;

                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");

                    AddTestCase("Search customer in IMS and check for locked error", "Customer not locked in IMS");
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username,false);
                    Assert.IsTrue(wAction.WaitUntilElementPresent(baseIMS.IMSDriver, By.XPath("id('sectionsContainer')/div[contains(text(),'Account is temporarily locked')]")), "Temp locked message not found in IMS");
                    Pass();


                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }



            //[Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(1000)]            
            public void Verify_Bad_data_Cust_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("SEC-01 Customer whose details are added as bad data (serial/personal details) should not be able to login", "Login should not be successfully");
                    #region Prerequiste
                    regData.update_Registration_Data("UserBad", ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
              
                    #region NewCustTestData
                    regData.email = "@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password;
                    #region setBadadata
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.SetBadData(baseIMS.IMSDriver);
                    wAction.PageReload(baseIMS.IMSDriver);
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(baseIMS.IMSDriver, By.Name("unblockserial")), "The customer is not blocked after adding bad data");
                    #endregion



                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");




                    Pass("casino login failed");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {

                    baseIMS.Quit();
                }
            }

            [Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(1000)]
            ///*************************
            //Rule Name  ******	Player has bad profile (Compulsive gambler) ******
            ///**************************
            public void Verify_Bad_data_Cust_Login_977()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("SEC-01 Customer whose details are added as bad data (serial/personal details) should not be able to login", "Login should not be successfully");
                    #region Prerequiste
                    regData.update_Registration_Data("UserBad", ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    #region NewCustTestData
                    regData.email = "@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password;
                    #region setBadadata
                    baseIMS.Init();
                    commIMS.SetBadData_RiskManagement(baseIMS.IMSDriver, loginData.username);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    #endregion



                    AnW.LoginFromPortal_SecurityError_Prompt(driverObj, loginData, "Unknown username or password");

                    wAction.PageReload(baseIMS.IMSDriver);

                    commIMS.Fraud_EventDetails(baseIMS.IMSDriver, "Player has bad profile");
                  


                    Pass("casino login failed");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {

                    baseIMS.Quit();
                }
            }

            [Test(Order = 12)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Configure_LockedDebitCust_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("Verify login is not successfull for locked customer in Casino", "Login should not be successfully");
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com";
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password + "RL";
                    #region getLoginAttempt
                    baseIMS.Init();
                    Login_Data.loginAttempt = commIMS.GetLoginFailAttempt(baseIMS.IMSDriver);
                    commIMS.SetLoginFailAttempt(baseIMS.IMSDriver, Login_Data.loginAttempt + 1);

                    #endregion



                    for (int i = 0; i < (Login_Data.loginAttempt) + 1; i++)
                    {
                        AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");
                        wAction.PageReload(driverObj);
                    }

                    loginData.password = regData.password;
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Login Error did not appear");


                    AddTestCase("Search customer in IMS and check for locked error", "Customer not locked in IMS");
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username,false);
                    Assert.IsTrue(wAction.WaitUntilElementPresent(baseIMS.IMSDriver, By.XPath("id('sectionsContainer')/div[contains(text(),'Account is temporarily locked')]")), "Temp locked message not found in IMS");
                    Pass();


                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {
                    commIMS.SetLoginFailAttempt(baseIMS.IMSDriver, Login_Data.loginAttempt);
                    baseIMS.Quit();
                }
            }
             
            
            [Test(Order = 13)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_PT_Registration_Autologin()
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
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();
                #endregion
                try
                {
                    AddTestCase("Create customer from Vegas pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com";
                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    

                    AnW.LogoutFromPortal(driverObj);
                    wAction.PageReload(driverObj);
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    AddTestCase("Create customer from Poker pages", "Customer should be created.");
                    wAction.OpenURL(driverObj, FrameGlobals.PokerURL, "Poker page not loaded");
                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");



                    AnW.LogoutFromPortal(driverObj);
                    wAction.PageReload(driverObj);
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    AddTestCase("Create customer from Casino pages", "Customer should be created.");
                    wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Casino page not loaded");
                         AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    iBrowser.Stop();
                    
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_PT_Registration_Autologin failed");
                }

            
            }


            [Test(Order = 14)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Invalid_LoginError_IMSLog()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion
                AddTestCase("Check whether customer logs in failure msg in IMS", "IMS failure login message is displayed in IMS");
                try
                {
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email="@aditi.com";
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password + "x";
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion


                    AddTestCase("Casino - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginData, "Customer logged in");
                    Pass();

                    baseIMS.Init();

                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username,false);
                    commIMS.CheckLog_InvalidLoginMsg(baseIMS.IMSDriver);

                    Pass("Verify_Invalid_LoginError_UserPwd_IMSLog : scenario passed");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Invalid_LoginError_IMSLog : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }

            

            [Test(Order = 16)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SessionKill_Login_1818()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("SEC-01 Session time out on manually killing the session on IMS", "Login should not be killed");
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com";
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                  
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                   
                    //PT page
                    AnW.LoginFromPortal(driverObj, loginData, iBrowser);
                    AddTestCase("Search customer in IMS and kill session", "Session should be killed in IMS");                    
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    wAction.Click(baseIMS.IMSDriver, By.Id("killplayer"), "Close Session button nt found", 0, false);
                    wAction.WaitAndAccepAlert(baseIMS.IMSDriver);
                    Assert.IsTrue(wAction.GetText(baseIMS.IMSDriver, By.Id("message-info-1"), "message not found", false).Contains("Kill player command issued"), "session kill message mot found in IMS");
                    Pass();
                   // baseIMS.Quit();

                    wAction.PageReload(driverObj);
                   BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name("username")),"User not logged out");                   
                   
                    //sports page
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com"; regData.username = "";
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion

                   

                    wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Sports page not loaded", 60);    
                    AnW.Login_Sports(driverObj, loginData);
                   wAction.Click(driverObj, By.LinkText("Close"));
                    AddTestCase("Search customer in IMS and kill session", "Session should be killed in IMS");
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    wAction.Click(baseIMS.IMSDriver, By.Id("killplayer"), "Close Session button nt found", 0, false);
                    wAction.WaitAndAccepAlert(baseIMS.IMSDriver);
                    Assert.IsTrue(wAction.GetText(baseIMS.IMSDriver, By.Id("message-info-1"), "message not found", false).Contains("Kill player command issued"), "session kill message mot found in IMS");
                    Pass();
                
                    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Sports_banking_lnk", "Banking Link not found", FrameGlobals.reloadTimeOut, false);
                
                    string portal = driverObj.CurrentWindowHandle.ToString();
                    wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 1, "Banking window not opened");

                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("loginPopupForm")), "User not logged out");
                    wAction.BrowserClose(driverObj);
                     wAction.WaitAndMovetoPopUPWindow(driverObj, portal, "Home page not available");
                    Pass("login kill successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_SessionKill_Login_2272 - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 16)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SessionKill_Login_2272()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.GamesURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("SEC-01 Session time out on manually killing the session on IMS", "Login should not be killed");
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com";
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion                 
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    //games page

                    ip2.LoginGames(driverObj, loginData);
                    AddTestCase("Search customer in IMS and kill session", "Session should be killed in IMS");
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    wAction.Click(baseIMS.IMSDriver, By.Id("killplayer"), "Close Session button nt found", 0, false);
                    wAction.WaitAndAccepAlert(baseIMS.IMSDriver);
                    Assert.IsTrue(wAction.GetText(baseIMS.IMSDriver, By.Id("message-info-1"), "message not found", false).Contains("Kill player command issued"), "session kill message mot found in IMS");
                    Pass();
                    baseIMS.Quit();

                    wAction.PageReload(driverObj);
                    //temp-del
                    Thread.Sleep(5000);
                    wAction.Click(driverObj, By.Id("fancybox-close"));

                 Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("tbUsername")),"Session did not expire");



                    //ecom page                    
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.email = "@aditi.com"; regData.username = "";
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion

                    wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "Ecom page not loaded", 60);
                    AnW.LoginFromEcom(driverObj, loginData);
                    AddTestCase("Search customer in IMS and kill session", "Session should be killed in IMS");
                     baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    wAction.Click(baseIMS.IMSDriver, By.Id("killplayer"), "Close Session button nt found", 0, false);
                    wAction.WaitAndAccepAlert(baseIMS.IMSDriver);
                    Assert.IsTrue(wAction.GetText(baseIMS.IMSDriver, By.Id("message-info-1"), "message not found", false).Contains("Kill player command issued"), "session kill message mot found in IMS");
                    Pass();
                    baseIMS.Quit();

                    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Banking_lnk", "ecom Banking Link not found");
                    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());

                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("loginPopupForm")), "User not logged out");
                    Pass("login kill successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_SessionKill_Login_2272 - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 17)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
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

          


            [Test(Order = 18)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_InvalidLogin_CreditCustomer_996()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                Login_Data lgnData = new Login_Data();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion
                try
                {
                    AddTestCase("Verify setting selfexclusion for a debit customer in OB", "Credit limit should be set successfully");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                  
                    lgnData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", null, "@aditi.com");
                    lgnData.password = "testinvalid1";
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    AnW.LoginFromPortal_InvalidCust(driverObj, lgnData,"Invalid error message is wrong");


      
                    Pass();
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");

                    Fail("Verify_InvalidLogin_CreditCustomer_996- failed");
                }
                finally
                {
                    baseIMS.Quit();
                
                }
            }

          //  [Test(Order = 19)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Lottos_Login_1910()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LottosURL);
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



                    ip2.LoginFromLottos(driverObj, loginData);
                    AnW.LogoutFromPortal_Ecom(driverObj);
                    Pass("login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Lottos login - failed");
                }

            }
           
            [Test(Order = 19)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
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
                    ip2.sessionManagement_Login(driverObj, sites.Replace("X", ""));                    

                    ip2.Logout_Exchange(driverObj);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));

                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());

                    ip2.sessionManagement_Logout(driverObj, sites.Replace("X", ""));                    


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

            [Test(Order = 20)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SessionManagement_Games_2293()
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

            
                try
                {
                    AddTestCase("Login to Games and verify session management accross all products", "Session should be maintained");

                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                                   ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    ip2.LoginGames(driverObj, loginData);

                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                    ip2.sessionManagement_Login(driverObj, sites.Replace("G", ""));

                    ip2.Logout_Games(driverObj);
                    ip2.sessionManagement_Logout(driverObj, sites.Replace("G", ""));

                    Pass();
                  

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_SessionManagement - failed");
                }

            }


            [Test(Order = 21)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SessionManagement_Ecom_2274()
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


                try
                {
                    AddTestCase("Login to ecom and verify session management accross all products", "Session should be maintained");

                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                                   ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromEcom(driverObj, loginData);
                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                    ip2.sessionManagement_Login(driverObj, sites.Replace("E", ""));

                    AnW.LogoutFromPortal_Ecom(driverObj);
                    ip2.sessionManagement_Logout(driverObj, sites.Replace("E", ""));   
                    Pass();


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_SessionManagement - failed");
                }

            }

            [Test(Order = 22)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SessionManagement_Ecom_PlaceBet_2270()
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


                try
                {
                    AddTestCase("Login to ecom and verify session management accross all products", "Session should be maintained");

                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),
                                                   ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.IP2_Authetication));
                   
                    ip2.login_fromBetslip_Ecom(driverObj, loginData);
                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                  
                 
                        AddTestCase("Verify session for Vegas", "User should be logged in");
                        wAction.ExecJavaScript(driverObj, "window.open('" + FrameGlobals.VegasURL + "','_blank');");
                        driverObj.SwitchTo().Window(driverObj.WindowHandles[1]);
                        Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn"), "Vegas not logged in");
                        Pass();
                 
                    if (sites.Contains("S"))
                    {
                        AddTestCase("Verify session for sports", "User should be logged in");
                        wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "sports load failed", 60);
                        wAction.Click(driverObj, By.XPath(Sportsbook_Control.LoggedinMenu_XP), "Sports user menu not found", 0, false);
                        Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.Logout_XP)), "Sports not logged in");
                        Pass();
                    }
                    if (sites.Contains("G"))
                    {

                        AddTestCase("Verify session for Games", "User should be logged in");
                        wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "Games load failed", 60);

                        Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "Logout"), "Ecomm not logged in");
                        Pass();
                    }
                    if (sites.Contains("B"))
                    {

                        AddTestCase("Verify session for backgammon", "User should be logged in");
                        wAction.OpenURL(driverObj, FrameGlobals.BackgammonURL, "Backgammon load failed", 60);
                        Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.LinkText("Logout")), "BackGammon not logged in");
                        Pass();
                    }

                    driverObj.SwitchTo().Window(driverObj.WindowHandles[0]);
                    AnW.LogoutFromPortal_Ecom(driverObj);

                    driverObj.SwitchTo().Window(driverObj.WindowHandles[1]);
                    wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Vegas load failed", 30);
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name("username")), "Customer might not have logged off successfully please check");
                    wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "sports load failed", 30);
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Sportsbook_Control.username_Id)), "Customer still not logged out!");
                  

                    Pass();


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_SessionManagement - failed");
                }

            }

            [Test(Order = 23)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_PasswordFormat_Registration_982()
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
                    AddTestCase("SEC-02 Allowed Password format on online Player Registration page", "Given Passwordformat should be as expected");

                    AddTestCase("Input a password equal to 8 characters that includes at least one alphanumeric, one uppercase or one lowercase letter and click outside the Password field",
                        "Password allowed");

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    regData.username = "User";
                    regData.password = "Lbr12345";
                    
                    ip2.Registration_PasswordCheck(driverObj, ref regData, "FAIR");
                    BaseTest.Pass();

                    AddTestCase("Input a password less than 8 characters that includes at least one alphanumeric, one uppercase or lowercase letter and click outside the Password field",
                        "Password not allowed and player is prompted that the password has to have at least 8 characters");
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, regUrl, "Reg page not opened", 30);
                    regData.username = "User";
                    regData.password = "Lbr1234";
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    ip2.Registration_PasswordCheck(driverObj, ref regData, "NOT VALID", ReadxmlData("tooltip", "reg_pass", DataFilePath.IP2_Authetication));
                    BaseTest.Pass();

                    AddTestCase("Input a password greater than 8 characters that includes only alphabets, one uppercase or one lowercase letter and click outside the Password field",
                        "Password not allowed and player is prompted that the password has to have at least one number in it");
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, regUrl, "Reg page not opened", 30);
                    regData.username = "User";
                    regData.password = "LbrEight";
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    ip2.Registration_PasswordCheck(driverObj, ref regData, "NOT VALID", ReadxmlData("tooltip", "reg_pass2", DataFilePath.IP2_Authetication));
                    BaseTest.Pass();

                    AddTestCase("Input a password greater than 8 characters that includes only digits",
                        "Password not allowed and player is prompted that the password has to have at least one alphabet in it");
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, regUrl, "Reg page not opened", 30);
                    regData.username = "User";
                    regData.password = "12345678";
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    ip2.Registration_PasswordCheck(driverObj, ref regData, "NOT VALID", ReadxmlData("tooltip", "reg_pass2", DataFilePath.IP2_Authetication));
                    BaseTest.Pass();

                    AddTestCase("Input a password greater than 8 characters that includes at least one letter and 1 digit all in lowercase and click outside the Password field",
                        "Password allowed");
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, regUrl, "Reg page not opened", 30);
                    regData.username = "User";
                    regData.password = "lbr456789";
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    ip2.Registration_PasswordCheck(driverObj, ref regData, "WEAK");
                    BaseTest.Pass();

                    AddTestCase("Input a password greater than 8 characters that includes at least one letter and 1 digit all in uppercase and click outside the Password field",
                        "Password allowed");
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, regUrl, "Reg page not opened", 30);
                    regData.username = "User";
                    regData.password = "LBR456789";
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    ip2.Registration_PasswordCheck(driverObj, ref regData, "WEAK");
                    BaseTest.Pass();

                    AddTestCase("Input a password greater than 8 characters that includes at least one alphanumeric, one uppercase or one lowercase letter and click outside the Password field",
                        "Password allowed");
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, regUrl, "Reg page not opened", 30);
                    regData.username = "User";
                    regData.password = "Lbr456789";
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    ip2.Registration_PasswordCheck(driverObj, ref regData, "FAIR");
                    BaseTest.Pass();
                   
                    BaseTest.Pass();
                    

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_PasswordFormat_Registration_982 failed");

                }

            }

            [Test(Order = 24)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Backgammon_Login_Logout_1046()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BackgammonURL);
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

                AddTestCase("Verify login is successfull in BackGammon", "Login should be successfully");
                try
                {

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    ip2.LoginFromBackgammon(driverObj, loginData);
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_games", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, ReadxmlData("log", "ClientPlatform_games", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, Generic.GetPublic_IPAddress());

                    

                    ip2.Logout_Backgammon(driverObj);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_games", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));

                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_games", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());

                   
                    Pass("BackGame login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Backgammon_Login_Logout_1908 - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }


            }

            [Test(Order = 25)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SessionManagement_Backgammon_1047()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BackgammonURL);
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

                AddTestCase("Verify session management is successfull in BackGammon", "Login should be successfully");
                try
                {

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    ip2.LoginFromBackgammon(driverObj, loginData);
                   
                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                    ip2.sessionManagement_Login(driverObj, sites.Replace("B", ""));


                    ip2.Logout_Backgammon(driverObj);
                  
                    ip2.sessionManagement_Logout(driverObj, sites.Replace("B", ""));

                    Pass("BackGame login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_SessionManagement - failed");
                }

            }
        }



        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Security_MyAcct_Cashier : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            SeamLessWallet sw = new SeamLessWallet();
            //IMS_Base baseIMS = new IMS_Base();

         
            [Timeout(1200)]
            [RepeatOnFail]
            [Test(Order = 1)]
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
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_Russia", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData, iBrowser);
                    #endregion

                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                    
                        //wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
                        //wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "myAcct_lnk", "My Account Link not found", FrameGlobals.reloadTimeOut, false);
                        //driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());

                    AnW.OpenMyAcct(driverObj);
                    Dictionary<string, string> modDetails = new Dictionary<string, string>();
                    AnW.MyAccount_ModifyDetails(driverObj, ref modDetails);
                    AnW.MyAccount_Edit_ContactPref_DirectMail(driverObj);
                    driverObj.Quit();


                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                    commTest.VerifyCustDetailinIMS_Modified(baseIMS.IMSDriver, modDetails);



                    BaseTest.AddTestCase("Verify contact preference updated in IMS", "Contact preferences should be updated");
                    wAction.Click(baseIMS.IMSDriver, By.Id("imgsec_contact"), "Contact preference link not found");
                    System.Threading.Thread.Sleep(2000);
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(baseIMS.IMSDriver, By.XPath(IMS_Control_PlayerDetails.DirectMailValidation_Checkbox)), "Contact preferences is not updated in IMS");
                    baseIMS.Quit();
                    BaseTest.Pass();

                    BaseTest.AddTestCase("Verified the Customers details page in the OB Admin", "Registered customers details should be present in the OB Admin");
                    adminBase.Init();
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    commTest.VerifyCustDetailsInOB_Modified(adminBase.MyBrowser, modDetails);
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

          
             [Timeout(2200)]
            [RepeatOnFail]
             [Test(Order = 2)]
            public void Verify_MyAcct_PasswordChange_FormatCheck_DebitCust()
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

                AddTestCase("SEC-02 Allowed Password format on online Player My Account page of a Debit customer", "User should be able to modify the deposit limit details");
                try
                {

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                   
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    #endregion

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                       AnW.OpenMyAcct(driverObj);    
                    ip2.MyAccount_ChangePassword_Format(driverObj, regData.password);
                                      

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
             [Test(Order = 2)]
             public void Verify_MyAcct_PasswordChange_FormatCheck_CreditCust()
             {
                 #region DriverInitiation
                 IWebDriver driverObj;
                 ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                 driverObj = browserInitialize(iBrowser);
                 #endregion

                 #region Declaration
                 Login_Data loginData = new Login_Data();
                 MyAcct_Data acctData = new MyAcct_Data();
                 IMS_Base baseIMS = new IMS_Base();
                 Registration_Data regData = new Registration_Data();
                 #endregion

                 AddTestCase("SEC-02 Allowed Password format on online Player My Account page of a credit customer", "User should be able to modify the deposit limit details");
                 try
                 {

                     

                     #region Prerequiste
                     regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                     baseIMS.Init();
                     regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit",null,"@aditi.com");
                     baseIMS.Quit();
                     #endregion
                     loginData.username = regData.username;
                     loginData.password = regData.password;

                     WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                     String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                     AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                        AnW.OpenMyAcct(driverObj);
                     ip2.MyAccount_ChangePassword_Format(driverObj, regData.password);


                     Pass("Customer password changed succesfully");
                 }
                 catch (Exception e)
                 {
                     exceptionStack(e);
                     CaptureScreenshot(driverObj, "Portal");
                     CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                     Fail("Password change failed for exception:" + e.Message);
                 }
                 finally
                 {
                     baseIMS.Quit();
                 }

             }


            [RepeatOnFail]
            [Timeout(1500)]
            [Test(Order = 3)]
            public void Verify_Withdraw_DeleteCookies()
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

                AddTestCase("SEC-18 Verify Step-Up authentication for withdrawal", "Should not be allowed to withdraw");

                try
                {


                    loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("depdata", "wAmt", DataFilePath.IP2_Authetication),
                          ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    ip2.Withdraw_DeleteCookies(driverObj, acctData);
                    
                    Pass();
                    //WriteUserName(loginData.username);
                }

                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Withdraw_DeleteCookies exception");

                }


            }


            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            //[Parallelizable]
            public void Verify_SearchLoyalityCust_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Search loyality customer from Telebet pages", "Search should be successful");
                    loginData.update_Login_Data(ReadxmlData("ldata", "user", DataFilePath.IP2_Authetication), null);


                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();

                    commTB2.SearchValidLoyalityCustomer(baseTB2.IMSDriver, loginData.username);
                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Searched Customer in Telebet:  " + loginData.username, ""); Pass();
                    Pass("search succesfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_SearchLoyalityCust_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }
            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            //[Parallelizable]
            public void Verify_SearchInvalidLoyalityCust_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Search loyality customer from Telebet pages", "Search should be successful");
                    loginData.update_Login_Data("99999", null);


                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();

                    commTB2.SearchInValidCustomer(baseTB2.IMSDriver, loginData.username);
                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Searched Customer in Telebet:  " + loginData.username, ""); Pass();
                    Pass("search succesfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_SearchLoyalityCust_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }
            }


            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Search_Closed_Customer_Telebet_967()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion

                try
                {
                    AddTestCase("SEC-01 Telebet agent should not be able to retrieve details of Closed/AV Failed Debit/Credit customer", "The customer should not get searched");

                    #region ReadClosedCustomer
                    string closedCust = ReadxmlData("clgndata", "user", DataFilePath.IP2_Authetication);

                    WriteCommentToMailer("UserName: " + closedCust + ";\nPassword: " + regData.password);

                    AddTestCase("Closed customer is: " + closedCust, "");
                    Pass();
                    #endregion

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchClosedCustomer(baseTB2.IMSDriver, closedCust);



                    Pass("Error success");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_Search_Credit_Closed_AVFailed_Customer_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }
            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_HTTPS_Telebet_1026()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                #endregion

                try
                {
                    AddTestCase("Search loyality customer from Telebet pages", "Search should be successful");
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication), null);


                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();

                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    commTB2.ValidateHTTPSTelebet(baseTB2.IMSDriver);

                    WriteCommentToMailer("UserName: " + loginData.username);
                    AddTestCase("Searched Customer in Telebet:  " + loginData.username, ""); Pass();
                    Pass("search succesfully");



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_HTTPS_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }
            }

            [Timeout(1200)]
            [RepeatOnFail]
            [Test(Order = 8)]
            public void Verify_HTTPS_WEBPortal_1024()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,FrameGlobals.PokerURL);
                #endregion

                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
                AdminSuite.Common adminComm = new AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Registration_Data regData = new Registration_Data();
                #endregion

                AddTestCase("Verify that contact preferences edited in portal is updated in IMS.", "Contact preferences should be updated successfully");
                try
                {
                   
                    loginData.update_Login_Data(ReadxmlData("lgndata", "user", DataFilePath.IP2_Authetication),ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication));

                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                  string portalWindow=  AnW.OpenCashier(driverObj);
                    BaseTest.Assert.IsTrue(driverObj.Url.Contains("https"), "URL is not HTTPS when accessing My ACct page:" + driverObj.Url);
                    driverObj.Close();
                    driverObj.SwitchTo().Window(portalWindow);
                    driverObj.Navigate().Refresh();


                    AnW.OpenMyAcct(driverObj);
                        BaseTest.Assert.IsTrue(driverObj.Url.Contains("https"), "URL is not HTTPS when accessing My ACct page:" + driverObj.Url);

                 
                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_HTTPS_WEBPortal for exception:" + e.Message);
                }
               
            }
        }
        
        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class Security_Sports : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            //IMS_Base baseIMS = new IMS_Base();
                     

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Invalid_LoginError_UserInvalid_DebitCust()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion
                try
                {

                   
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = "invalid";
                    #region InvalidLogin
                       string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                       if (sites.Contains("S"))
                       {
                           #region NewCustTestData
                           regData.username = "User";
                           regData.email = "t@aditi.com";
                           commTest.Createcustomer_PostMethod(ref regData);
                           loginData.username = regData.username;
                           
                           #endregion
                           AddTestCase("Sports - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           ip2.Login_SecurityError_Sports(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass();
                       }
                       if (sites.Contains("P"))
                       {
                           #region NewCustTestData
                           regData.username = "User";
                           regData.email = "t@aditi.com";
                           commTest.Createcustomer_PostMethod(ref regData);
                           loginData.username = regData.username;
                           
                           #endregion
                           AddTestCase("PT - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Sports not opened", 60);
                           AnW.LoginFromPortal_SecurityError_Prompt(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass();

                       }
                       if (sites.Contains("G"))
                       {
                           #region NewCustTestData
                           regData.username = "User";
                           regData.email = "t@aditi.com";
                           commTest.Createcustomer_PostMethod(ref regData);
                           loginData.username = regData.username;
                        
                           #endregion
                           AddTestCase("GamesUrl - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "games page not opened");
                           ip2.LoginGames_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass("games");
                       }
                       if (sites.Contains("L"))
                       {
                           #region NewCustTestData
                           regData.username = "User";
                           regData.email = "t@aditi.com";
                           commTest.Createcustomer_PostMethod(ref regData);
                           loginData.username = regData.username;
                           #endregion
                           AddTestCase("LottosURL - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           wAction.OpenURL(driverObj, FrameGlobals.LottosURL, "lottos page not opened");
                           ip2.LoginFromLottos_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass("lottos");
                       }
                       if (sites.Contains("E"))
                       {
                           #region NewCustTestData
                           regData.username = "User";
                           regData.email = "t@aditi.com";
                           commTest.Createcustomer_PostMethod(ref regData);
                           loginData.username = regData.username;
                         
                           #endregion
                           AddTestCase("Ecom - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "ecom page not opened");
                           ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass("ecom");
                       }
                    #endregion
               


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Invalid_LoginError : scenario failed");
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Invalid_LoginError_UserPwd_DebitCust()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    
                    loginData.update_Login_Data("Test",
                                            ReadxmlData("ilgndata", "pwd", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                   
                    #region InvalidLogin
                       string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                       if (sites.Contains("S"))
                       {
                           AddTestCase("Sports - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           ip2.Login_SecurityError_Sports(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass();
                       }

                    if (sites.Contains("P"))
                       {
                    AddTestCase("PT - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                    wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Sports not opened", 60);
                    AnW.LoginFromPortal_SecurityError_Prompt(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                    Pass();
                    }

                        if (sites.Contains("G"))
                       {
                    AddTestCase("GamesUrl - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                    wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "games page not opened");
                    ip2.LoginGames_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                    Pass("games");
                        }
                      
                            if (sites.Contains("L"))
                       {
                    AddTestCase("LottosURL - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                    wAction.OpenURL(driverObj, FrameGlobals.LottosURL, "lottos page not opened");
                    ip2.LoginFromLottos_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                    Pass("lottos");
                            }

                            if (sites.Contains("E"))
                            {
                                AddTestCase("Ecom - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                                wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "ecom page not opened");
                                ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                                Pass("ecom");
                           } 
                    #endregion

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Invalid_LoginError : scenario failed");
                }
            }
                    

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Sports_Customer_Registration_Autologin()
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
                    //wAction.Click(driverObj, By.XPath("id('login')/a[text()='Open account']"), "Open Account button not found", 0, false);
                    AnW.OpenRegistrationPage(driverObj);
                    regData.email = "@aditi.com";
                    //       AnW.OpenRegistrationPage(driverObj);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(Ob.MyBrowser, "OB");
                    Fail("Verify_Sports_Customer_Registration_Autologin failed");
                }

            }

            

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Frozen_Cust_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("SEC-04 Message to be displayed when frozen customer logs in with valid username and valid password from online", "Login should not be successfully");
                    //goto skip;

                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();
                    regData.email = "@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.FreezeTheCustomer(baseIMS.IMSDriver);
                    #endregion

                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password;

                    AnW.LoginFromPortal_SecurityError_Prompt(driverObj, loginData, ReadxmlData("err", "PT_Frozen_Lgn", DataFilePath.IP2_Authetication));


                    #region frozencust
                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                    if (sites.Contains("S"))
                    {
                        AddTestCase("Sports - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Sports not opened", 60);
                        ip2.Login_SecurityError_Sports(driverObj, loginData, ReadxmlData("err", "PT_Frozen_Lgn", DataFilePath.IP2_Authetication));
                        Pass();

                    }
                    if (sites.Contains("G"))
                    {
                        AddTestCase("GamesUrl - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "games page not opened");
                        ip2.LoginGames_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Frozen_Lgn", DataFilePath.IP2_Authetication));
                        Pass("games");
                    }
                    if (sites.Contains("L"))
                    {
                        AddTestCase("LottosURL - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.LottosURL, "lottos page not opened");
                        ip2.LoginFromLottos_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Frozen_Lgn", DataFilePath.IP2_Authetication));
                        Pass("lottos");
                    }
                    if (sites.Contains("E"))
                    {

                        AddTestCase("Ecom - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "ecom page not opened");
                        ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "PT_Frozen_Lgn", DataFilePath.IP2_Authetication));
                        Pass("ecom");
                    }
                    #endregion

                    commIMS.CheckLog_LoginFailedCustomer(baseIMS.IMSDriver, loginData.username, ReadxmlData("err", "IMS_Frozen_Log", DataFilePath.IP2_Authetication));
                    WriteUserName(loginData.username);
                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Sports_Login_964()
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

                AddTestCase("Verify login is successfull in Sports", "Login should be successfully");
                try
                {

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.Login_Sports(driverObj, loginData);


                    Pass("Sports login successfully");





                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    
                    Fail("Verify_Sports_Login - failed");
                }
               


            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Sports_Logout_1544()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                AddTestCase("Verify Login & Logout is successfull in Sports", "Logout should be successfully");
                try
                {
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    
                    
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                   
                  
                    AnW.Login_Sports(driverObj, loginData);

                    wAction.Click(driverObj, By.LinkText("Close"));
                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                    ip2.sessionManagement_Login(driverObj, sites.Replace("S", ""));   



                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, Generic.GetPublic_IPAddress());

                    ip2.Logout_Sports(driverObj);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                   
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());

                    ip2.sessionManagement_Logout(driverObj, sites.Replace("S", ""));   

                    Pass("Sports login successfully");
                    
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Sports_Logout - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }

           // [Test(Order = 15)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Ecom_Login_Logout_1719()
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
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);



                    AnW.LoginFromEcom(driverObj, loginData);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogin_XP, Generic.GetPublic_IPAddress());

                    AnW.LogoutFromPortal_Ecom(driverObj);
                   
                   
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.ClientPlatform_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LoginTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));
                    commIMS.Validate_SessionLogin(baseIMS.IMSDriver, IMS_Control_PlayerDetails.LogoutTime_XP, DateTime.Now.ToString("yyyy-MM-dd"));

                    commIMS.Open_CheckLog(baseIMS.IMSDriver);
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, ReadxmlData("log", "ClientPlatform_Sports", DataFilePath.IP2_Authetication));
                    commIMS.Validate_CheckLog(baseIMS.IMSDriver, IMS_Control_CasinoLog.SessionLogout_XP, Generic.GetPublic_IPAddress());

                  
                    Pass("Ecom login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Ecom_Login_Logout - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }

            /// <summary>
            /// Naga
            /// Message to be displayed when customer logs in with invalid username and invalid password from online
            /// </summary>
            [Test(Order = 9)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_ClosedCust_Login()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    
                    loginData.update_Login_Data(ReadxmlData("clgndata", "user", DataFilePath.IP2_Authetication),
                                            ReadxmlData("clgndata", "pwd", DataFilePath.IP2_Authetication), "");


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    #region ClosedLogin
                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                    if (sites.Contains("S"))
                    {
                        AddTestCase("Sports - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        ip2.Login_SecurityError_Sports(driverObj, loginData, ReadxmlData("err", "Sports_Closed_Lgn", DataFilePath.IP2_Authetication));
                        Pass();
                    }

                    if (sites.Contains("P"))
                    {
                        AddTestCase("PT - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Sports not opened", 60);
                        AnW.LoginFromPortal_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Closed_Lgn", DataFilePath.IP2_Authetication));
                        Pass();
                    }

                    if (sites.Contains("G"))
                    {
                        AddTestCase("GamesUrl - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "games page not opened");
                        ip2.LoginGames_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Closed_Lgn", DataFilePath.IP2_Authetication));
                        Pass("games");
                    }
                    if (sites.Contains("L"))
                    {
                        AddTestCase("LottosURL - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.LottosURL, "lottos page not opened");
                        ip2.LoginFromLottos_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Closed_Lgn", DataFilePath.IP2_Authetication));
                        Pass("lottos");
                    }
                    if (sites.Contains("E"))
                    {
                        AddTestCase("Ecom - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "ecom page not opened");
                        ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "PT_Closed_Lgn", DataFilePath.IP2_Authetication));
                        Pass("ecom");
                    }
                    #endregion


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_ClosedCust_Login : scenario failed");
                }
            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_All_Product_SessionManagement()
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

                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                    ip2.sessionManagement_Login(driverObj, sites.Replace("P", ""));

                    AnW.LogoutFromPortal(driverObj);

                    ip2.sessionManagement_Logout(driverObj, sites.Replace("P", ""));
                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_SeemlessLogin_BVT - failed");
                }

            }


            [Test(Order = 8)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_AVFailed_Cust_Login_971()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                #region Declaration
                Registration_Data regData = new Registration_Data();
                // Configuration testdata = TestDataInit();           
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();
                #endregion

                try
                {
                    AddTestCase("SEC-01 AV status fail frozen Debit/Credit customer should not be able to login to online Ladbrokes products", "Login should not be successfully");
                    //goto skip;

                    #region ClosedLogin
                    string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                    if (sites.Contains("S"))
                    {
                        #region Prerequiste
                        regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                        baseIMS.Init();
                        regData.username = "User";
                        regData.email = "@aditi.com";
                        commTest.Createcustomer_PostMethod(ref regData);
                        commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                        commIMS.AVstatusSet(baseIMS.IMSDriver, "Failed");
                        loginData.username = regData.username;
                        loginData.password = regData.password;
                        WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                        loginData.password = regData.password;
                        baseIMS.Quit();
                        #endregion
                        AddTestCase("Sports - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        ip2.Login_SecurityError_Sports(driverObj, loginData, ReadxmlData("err", "Sports_AVFailed_Lgn", DataFilePath.IP2_Authetication));
                        Pass();
                    }

                    if (sites.Contains("P"))
                    {

                        #region Prerequiste
                        regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                        baseIMS.Init();
                        regData.username = "User";
                        regData.email = "@aditi.com";
                        commTest.Createcustomer_PostMethod(ref regData);
                        commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                        commIMS.AVstatusSet(baseIMS.IMSDriver, "Failed");
                        loginData.username = regData.username;
                        loginData.password = regData.password;
                        WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                        loginData.password = regData.password;
                        baseIMS.Quit();
                        #endregion
                        AddTestCase("PT - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Bingo not opened", 60);
                        AnW.LoginFromPortal_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_AVFailed_Lgn", DataFilePath.IP2_Authetication));
                        Pass();

                    }
                    if (sites.Contains("G"))
                    {

                        #region Prerequiste
                        regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                        baseIMS.Init();
                        regData.username = "User";
                        regData.email = "@aditi.com";
                        commTest.Createcustomer_PostMethod(ref regData);
                        commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                        commIMS.AVstatusSet(baseIMS.IMSDriver, "Failed");
                        loginData.username = regData.username;
                        loginData.password = regData.password;
                        WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                        loginData.password = regData.password;
                        baseIMS.Quit();
                        #endregion
                        AddTestCase("GamesUrl - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "games page not opened");
                        ip2.LoginGames_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_AVFailed_Lgn", DataFilePath.IP2_Authetication));
                        Pass("games");
                    }
                    if (sites.Contains("L"))
                    {

                        #region Prerequiste
                        regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                        baseIMS.Init();
                        regData.username = "User";
                        regData.email = "@aditi.com";
                        commTest.Createcustomer_PostMethod(ref regData);
                        commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                        commIMS.AVstatusSet(baseIMS.IMSDriver, "Failed");
                        loginData.username = regData.username;
                        loginData.password = regData.password;
                        WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                        loginData.password = regData.password;
                        baseIMS.Quit();
                        #endregion
                        AddTestCase("LottosURL - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                        wAction.OpenURL(driverObj, FrameGlobals.LottosURL, "lottos page not opened");
                        ip2.LoginFromLottos_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_AVFailed_Lgn", DataFilePath.IP2_Authetication));
                        Pass("lottos");

                    }
                 
                    #endregion

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.CheckLog_LoginFailedCustomer(baseIMS.IMSDriver, loginData.username, ReadxmlData("err", "IMS_AVFailed_Log", DataFilePath.IP2_Authetication));
                    Pass("casino login successfully");
                    WriteUserName(loginData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_Casino_LockedDebitCust_Login - failed");
                }
                finally
                {
                    baseIMS.Quit();
                }

            }


            [Test(Order = 9)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]     
            public void Verify_Search_AVFailedCustomer_Telebet_967()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion

                try
                {
                    AddTestCase("SEC-01 Telebet agent should not be able to retrieve details of Closed/AV Failed Debit/Credit customer", "The customer should not get searched");
                  
                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();
                    regData.username = "User";
                    regData.email = "@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.AVstatusSet(baseIMS.IMSDriver, "Failed");
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password;
                    baseIMS.Quit();
                    #endregion
                   

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.Search_Customer_GeneralError(baseTB2.IMSDriver, regData.username,ReadxmlData("err", "PT_AVFailed_Lgn", DataFilePath.IP2_Authetication));
                    WriteCommentToMailer("UserName: " + regData.username);

                    AddTestCase("Searched Customer in Telebet:  " + regData.username, ""); Pass();
                    Pass("Error success");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_Search_SelfEXCustomer_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }
            }


            [Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Sports_PlaceBet_Login()
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

                    loginData.update_Login_Data(ReadxmlData("lgnsdata", "user", DataFilePath.IP2_Authetication),
                                            ReadxmlData("lgnsdata", "pwd", DataFilePath.IP2_Authetication),
                                            "");
                    string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "eAmt", DataFilePath.IP2_SeamlessWallet);


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);

                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.betslip.Placebet_XP));
                    Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath(Sportsbook_Control.betslip.NotLoggedInError_XP)), "Not found err: To place a bet you must log in");



                    Pass("Sports login successfull");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_PlaceBet_Login - Failed");
                }

            }

            [Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Sports_PlaceBet_InvalidLogin()
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

                    loginData.update_Login_Data("InvalidUser",
                                            "InvalidPwd",
                                            "");
                    //  excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //tempCode
                    //  wAction.PageReload(driverObj);

                    wAction.Click(driverObj, By.LinkText("Close"));

                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);
                    //bet now button should b displayed

                    ip2.Invalidlogin_fromBetslip(driverObj, loginData);

                    Pass("Sports login successfull");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_PlaceBet_InvalidLogin - Failed");
                }

            }

            [RepeatOnFail]
            [Timeout(2200)]
        //    [Test(Order = 12)]
            public void Verify_Sports_Bet_Quick_Deposit_1380()
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

                    loginData.update_Login_Data(ReadxmlData("qbetdata", "user", DataFilePath.IP2_Authetication),
                                            ReadxmlData("qbetdata", "pwd", DataFilePath.IP2_Authetication));


                    
                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("qbetdata", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("qbetdata", "dAmt", DataFilePath.IP2_Authetication),
                          ReadxmlData("qbetdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("qbetdata", "depWallet", DataFilePath.IP2_Authetication), null);

                    
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    wAction.Click(driverObj, By.LinkText("Close"));

                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    //AnW.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.IP2_Authetication));
                    //AnW.AddToBetSlipPlaceBet(driverObj,int.Parse(acctData.depositAmt));
                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj, acctData.depositAmt);
                    
                    
                    //bet now button should b displayed
           
                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "place_bet_button");
                    wAction.WaitUntilElementDisappears(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait));
                    
                 
                    //Calling common method netteller
                 
                    ip2.quickDep_Sports(driverObj, acctData.depositAmt);
                    flag = true;
                    driverObj.SwitchTo().DefaultContent();
                
                   // wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "check_btn", "Check button not found inside betslip", 0, false);
                  //  wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "place_bet_button");
                    System.Threading.Thread.Sleep(10000);
                  //  BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSuccess2_xp)), "Bet not placed");

                    wAction.Click(driverObj, By.ClassName("close"));
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSuccess1_xp)) || wAction.IsElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSuccess2_xp)), "Bet not placed");
                   
                   
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
                            Ladbrokes_IMS_TestRepository.Common RepositoryCommon = new Ladbrokes_IMS_TestRepository.Common();
                            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Sports_banking_lnk", "Banking Link not found", FrameGlobals.reloadTimeOut, false);
             
                            driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                            RepositoryCommon.CommonWithdraw_CreditCard_PT(driverObj, acctData, acctData.depositAmt, false);

                        }
                    }
                    catch (Exception)
                    {
                    }

                    Fail("Bet has not been placed");
                }
            }

            [Test(Order = 13)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_LoyalityCust_LoginError_DebitCust_978()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion
               
                try
                {
                    AddTestCase("Sports - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                    loginData.update_Login_Data(ReadxmlData("ldata", "user", DataFilePath.IP2_Authetication),
                                            ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    #region InvalidLogin
                       string sites = ReadxmlData("availableProd", "sites", DataFilePath.IP2_Authetication);
                       if (sites.Contains("S"))
                       {
                           AddTestCase("Sports - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                           ip2.Login_SecurityError_Sports(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                           Pass();

                       }
                    if (sites.Contains("P"))
                       {
                    AddTestCase("PT - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                    wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Sports not opened", 60);
                    AnW.LoginFromPortal_SecurityError_Prompt(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                    Pass();
                    }

                        if (sites.Contains("G"))
                       {
                    AddTestCase("GamesUrl - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                    wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "games page not opened");
                    ip2.LoginGames_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                    Pass("games");
                        }
                            if (sites.Contains("L"))
                       {
                    AddTestCase("LottosURL - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                    wAction.OpenURL(driverObj, FrameGlobals.LottosURL, "lottos page not opened");
                    ip2.LoginFromLottos_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                    Pass("lottos");
                            }
                            if (sites.Contains("E"))
                            {
                                AddTestCase("Ecom - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                                wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "ecom page not opened");
                                ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication));
                                Pass("ecom");
                            }
                    #endregion
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Invalid_LoginError : scenario failed");
                }
            }
            
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 14)]
            public void Verify_Ecom_QuickDeposit_1381()
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
                    AddTestCase("Verify the Ecom quick deposit in betslip", "Bet should be placed successfully");



                    loginData.update_Login_Data(ReadxmlData("qbetdata", "user", DataFilePath.IP2_Authetication),
                                            ReadxmlData("qbetdata", "pwd", DataFilePath.IP2_Authetication));

                    excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
                       "UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    AnW.LoginFromEcom(driverObj, loginData);

                    AnW.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.IP2_Authetication));
                    ip2.quickDep_Ecom(driverObj, ReadxmlData("qbetdata", "dAmt", DataFilePath.IP2_Authetication));
                    
                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Ecom_QuickDeposit_1381 scenario failed");
                }

            }

            [Test(Order = 15)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_Sports_PlaceBet_StepUp_1021()
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
                    AddTestCase("SEC-18 Verify Step-Up authentication when placing sports bet", "Bet should not be placed");

                    loginData.update_Login_Data(ReadxmlData("lgnsdata", "user", DataFilePath.IP2_Authetication),
                                            ReadxmlData("lgnsdata", "pwd", DataFilePath.IP2_Authetication));                                           
                    

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.Login_Sports(driverObj, loginData);
                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);
                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);
                    driverObj.Manage().Cookies.DeleteAllCookies();

                    AnW.placeBet(driverObj); 
                    driverObj.Navigate().Refresh();
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Sportsbook_Control.username_Id)), "Customer still not logged out!");
                    Pass("Sports login successfull");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_PlaceBet_StepUp_1021 - Failed");
                }

            }

        }

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class SelfExcl_Portal_Telebet : BaseTest
        {
            SeamLessWallet sw = new SeamLessWallet();
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
          //  IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();          

            
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_Login_Sports()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();

                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    AddTestCase("Sports - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    loginData.update_Login_Data(regData.username,
                                            regData.password, "");

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.Login_SecurityError_Sports(driverObj, loginData, ReadxmlData("err", "SelfEx_Lgn", DataFilePath.IP2_Stage));
//                    ip2.Login_SelfEx_Sports(driverObj, loginData,ReadxmlData("err","SelfEx_Lgn",DataFilePath.IP2_Stage));
                  

                    Pass("Verify_SelfExcl_Login_Sports : scenario passed");

                  


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Sports : scenario failed");
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_Login_Casino()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.CasinoURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    AddTestCase("Sports - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                   // regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", null, "@aditi.com","fraud");
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username,false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    loginData.update_Login_Data(regData.username,
                                            regData.password, "");

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal_SelfExCust(driverObj, loginData, "No Login prompt error is displayed");
                    Pass();


                    WriteUserName(regData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Invalid_LoginError : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

           // [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_Login_Bingo()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    AddTestCase("Sports - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                    // regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", null, "@aditi.com","fraud");
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    loginData.update_Login_Data(regData.username,
                                            regData.password, "");

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal_SelfExCust(driverObj, loginData, "No Login prompt error is displayed");
                    Pass();


                    WriteUserName(regData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Invalid_LoginError : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }
           
            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            [DependsOn("Verify_SelfExcl_Login_Casino")]
            public void Verify_All_Product_SelfExc_Removed_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
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
                    AddTestCase("Verify login is successfull in Bingo after self ex is removed", "Login should be successfully");
                    loginData.update_Login_Data(Usernames["Verify_SelfExcl_Login_Casino"],
                                            ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);



                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Remove_SelfExclusion_Customer(baseIMS.IMSDriver);
                    baseIMS.Quit();


                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    Pass("Bingo login successfully");


                    //AddTestCase("Verify login is successfull in Ecom", "Login should be successfully");                  
                    AnW.LogoutFromPortal(driverObj);
                    //Thread.Sleep(TimeSpan.FromSeconds(5));
                    //wAction.WaitforPageLoad(driverObj);
                    //wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "Bingo page not loaded", 60);
                    //AnW.LoginFromEcom(driverObj, loginData);
                    //Pass("Ecom login successfully");

                    AddTestCase("Verify login is successfull in Sports", "Login should be successfully");
                   // AnW.LogoutFromPortal_Ecom(driverObj);
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Sports page not loaded", 60);                 
                    AnW.Login_Sports(driverObj, loginData);
                    //wAction.Click(driverObj, By.LinkText("Close"));
                    Pass("LD login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_AllPT_Login - failed");
                }

            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_Login_Ecom()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();

                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    AddTestCase("Ecom - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, ""); Pass();

                    baseIMS.Quit();

                    loginData.update_Login_Data(regData.username,
                                            regData.password, "");

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "SelfEx_Lgn", DataFilePath.IP2_Authetication));          

                    Pass("Verify_SelfExcl_Login_Ecom : scenario passed");




                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Ecom : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_TelebetCustomer_1114_1148()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj = new FirefoxDriver();
                wAction.OpenURL(driverObj, ReadxmlData("Url", "telebet_Reg_url", DataFilePath.IP2_Authetication), "Telebet page did not open");
                #endregion


                try
                {
                    AddTestCase("Verify setting selfexclusion for a telebet customer in OB", "Credit limit should be set successfully");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    string temp = Registration_Data.depLimit;
                    regData.email = "sx@gmail.com";
                    Registration_Data.depLimit = "5000";
                    commIMS.TeleBet_Registration(driverObj, ref regData);
                    Registration_Data.depLimit = temp;

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    double custID = StringCommonMethods.ReadIntegerfromString(baseIMS.IMSDriver.Url.ToString());
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    commIMS.ComplianceReportView(baseIMS.IMSDriver, "CURL - Player selfexclusions", custID);


                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    adminBase.Init();
                  
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
                    Fail("Verify_SelfExcl_TelebetCustomer - failed");
                }
                finally
                {
                    baseIMS.Quit();
                    driverObj.Quit();

                }
            }


            
            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("Verify_SelfExcl_TelebetCustomer_1114_1148")]
            public void Verify_Search_SelfEx_Removed_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion

                try
                {
                    AddTestCase("SEL-12 Customer should be able to login from Telebet channel once self-exclusion is removed", "The customer should not get searched");

                    //Usernames["Verify_SelfExcl_TelebetCustomer"] = "UserUPEABOIU";
                    regData.username = Usernames["Verify_SelfExcl_TelebetCustomer"];

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.Remove_SelfExclusion_Customer(baseIMS.IMSDriver);
                    baseIMS.Quit();


                    //if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                    //else
                    //    baseTB2.Init();

                    commTB2.SearchCustomer(baseTB2.IMSDriver, regData.username);
                    WriteCommentToMailer("UserName: " + regData.username);

                    AddTestCase("Searched Customer in Telebet:  " + regData.username, ""); Pass();
                    Pass("Error success");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_Search_SelfEx_Removed_Telebet - failed");
                }
                finally
                {
                    baseTB2.Quit();
                    baseIMS.Quit();
                }
            }

         


            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Search_SelfEXCustomer_Telebet()
            {

                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();
                IMS_Base baseIMS = new IMS_Base();

                #endregion

                try
                {
                    AddTestCase("Search the self excluded customer from Telebet pages and check error message", "Deposit should be successful");
                    baseIMS.Init();

                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", null, "@aditi.com", "fraud");
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    Thread.Sleep(TimeSpan.FromSeconds(50));

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchSelfExCustomer(baseTB2.IMSDriver, regData.username);
                    WriteCommentToMailer("UserName: " + regData.username);

                    AddTestCase("Searched Customer in Telebet:  " + regData.username, ""); Pass();
                    Pass("Error success");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                    Fail("Verify_Search_SelfEXCustomer_Telebet - failed");
                }
                finally
                {

                    baseTB2.Quit();
                }
            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Withdraw_SelfEx_Approval_IMS()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser,FrameGlobals.VegasURL);
                #endregion
                #region Declaration


                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Registration_Data regData = new Registration_Data();
                IMS_AdminSuite.IMS_Base IMSBase = new IMS_Base();
                IMS_AdminSuite.Common IMSComm = new IMS_AdminSuite.Common();
                #endregion

                bool checkpoint = false;
              
                AddTestCase("Verify non approved WD request are displayed in the transaction history for self excluded custoomer", "Non approved WD requests should be approved even after self exclusion");
                try
                {
                    //AddTestCase("Verify Netteller withdraw", "Withdraw should be successful");

                    loginData.update_Login_Data(ReadxmlData("swdata", "user", DataFilePath.IP2_Authetication),
                       ReadxmlData("swdata", "pwd", DataFilePath.IP2_Authetication));
         

                    acctData.Update_deposit_withdraw_Card(null,
                         ReadxmlData("swdata", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("swdata", "dAmt", DataFilePath.IP2_Authetication),
                     ReadxmlData("swdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("swdata", "depWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //Login to portal and withdraw 
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData);

                    //self excl customer from IMS
                    IMSBase.Init();
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    commIMS.SelfExclude_Customer(IMSBase.IMSDriver, loginData.username);
                    checkpoint = true;
                    //Approve WD request
                    IMSComm.Approve_Withdraw_Request(IMSBase.IMSDriver);
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    IMSComm.Approve_WithdrawRequest(IMSBase.IMSDriver, loginData.username);
                    Pass();
                }
                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(IMSBase.IMSDriver, "IMS");
                    Fail("Verify_Withdraw_SelfEx_Approval_IMS failed");
                }
                finally
                {
                    
                    IMSBase.Quit();
                    if (checkpoint)
                    {
                        try
                        {
                            IMSBase.Init();
                            IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                            IMSComm.Remove_SelfExclusion_Customer(IMSBase.IMSDriver);
                        }
                        finally
                        {
                            IMSBase.Quit();
                        }
                    }
                }
            }

            [Test(Order = 8)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Withdraw_SelfEx_Approval_IMS_NonGBP()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                Registration_Data regData = new Registration_Data();
                IMS_AdminSuite.IMS_Base IMSBase = new IMS_Base();
                IMS_AdminSuite.Common IMSComm = new IMS_AdminSuite.Common();
                #endregion
              
                bool checkpoint = false;

                AddTestCase("Verify non approved WD request are displayed in the transaction history for self excluded custoomer", "Non approved WD requests should be approved even after self exclusion");
                try
                {
                     

                    //AddTestCase("Verify Netteller withdraw", "Withdraw should be successful");

                    loginData.update_Login_Data(ReadxmlData("sw2data", "user", DataFilePath.IP2_Authetication),
                       ReadxmlData("sw2data", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(null,
                         ReadxmlData("sw2data", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("sw2data", "dAmt", DataFilePath.IP2_Authetication),
                     ReadxmlData("swdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("swdata", "depWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //Login to portal and withdraw 
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData,"Vegas");

                    //self excl customer from IMS
                    IMSBase.Init();
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    commIMS.SelfExclude_Customer(IMSBase.IMSDriver, loginData.username);
                    checkpoint = true;
                    //Approve WD request
                    IMSComm.Approve_Withdraw_Request(IMSBase.IMSDriver);
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    IMSComm.Approve_WithdrawRequest(IMSBase.IMSDriver, loginData.username);
               
                    Pass();
                   
                }
                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(IMSBase.IMSDriver, "IMS");
                    Fail("Verify_Withdraw_SelfEx_Approval_IMS failed");
                }
                finally
                {

                    IMSBase.Quit();
                    if (checkpoint)
                    {
                        try
                        {
                            IMSBase.Init();
                            IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                            IMSComm.Remove_SelfExclusion_Customer(IMSBase.IMSDriver);
                        }
                        finally
                        {
                            IMSBase.Quit();
                        }
                    }
                }
            }

            [Test(Order = 9)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Withdraw_SelfEx_Approval_IMS_Games_1150()
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
                IMS_AdminSuite.IMS_Base IMSBase = new IMS_Base();
                IMS_AdminSuite.Common IMSComm = new IMS_AdminSuite.Common();
                #endregion

                bool checkpoint = false;

                AddTestCase("SEL-17 CSA agent should be able to process withdrawal request for self excluded customer from games wallet", "Non approved WD requests should be approved even after self exclusion");
                try
                {
                    //AddTestCase("Verify Netteller withdraw", "Withdraw should be successful");

                    loginData.update_Login_Data(ReadxmlData("swdata", "user", DataFilePath.IP2_Authetication),
                       ReadxmlData("swdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(null,
                         ReadxmlData("swdata", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("swdata", "dAmt", DataFilePath.IP2_Authetication),
                     ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "GamesWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //Login to portal and withdraw 
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    AnW.OpenCashier(driverObj);
                    AnW.Withdraw_Netteller(driverObj, acctData);

                    //self excl customer from IMS
                    IMSBase.Init();
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    commIMS.SelfExclude_Customer(IMSBase.IMSDriver, loginData.username);
                    checkpoint = true;
                    //Approve WD request
                    IMSComm.Approve_Withdraw_Request(IMSBase.IMSDriver);
                    IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                    IMSComm.Approve_WithdrawRequest(IMSBase.IMSDriver, loginData.username);
                    Pass();
                }
                catch (Exception e)
                {

                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(IMSBase.IMSDriver, "IMS");
                    Fail("Verify_Withdraw_SelfEx_Approval_IMS failed");
                }
                finally
                {

                    IMSBase.Quit();
                    if (checkpoint)
                    {
                        try
                        {
                            IMSBase.Init();
                            IMSComm.SearchCustomer_Newlook(IMSBase.IMSDriver, loginData.username);
                            IMSComm.Remove_SelfExclusion_Customer(IMSBase.IMSDriver);
                        }
                        finally
                        {
                            IMSBase.Quit();
                        }
                    }
                }
            }


            [Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_Login_Lottos_1112()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.LottosURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    AddTestCase("SEL-01 Login of a self excluded player into lottos portal", "Invalid login error did not appear");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                    // regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", null, "@aditi.com","fraud");
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    loginData.update_Login_Data(regData.username,
                                            regData.password, "");

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginFromLottos_SelfEx(driverObj, loginData);
                    Pass("Verify_SelfExcl_Login_Lottos : scenario passed");


                    WriteUserName(regData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Lottos : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_Login_Exchange_1136()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                MyAcct_Data acctData = new MyAcct_Data();

                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion
                AddTestCase("SEL-12 Customer should be able to login from Exchange product once self exclusion is removed", "Invalid login error did not appear");
                try
                {
                    AddTestCase("Ecom - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, ""); Pass();

                    baseIMS.Quit();

                    loginData.update_Login_Data(regData.username,
                                            regData.password, "");

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "SelfEx_Lgn", DataFilePath.IP2_Authetication));

                    Pass("Verify_SelfExcl_Login_Ecom : scenario passed");




                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Ecom : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }
        }

        [TestFixture, Timeout(15000)]
        [Parallelizable]
        public class SelfExcl_Admin_Portal : BaseTest
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


            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SelfExcl_DebitCustomer()
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
                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password,null,null,"@aditi.com");
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    adminBase.Init();
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                    // string s = wAction.GetSelectedDropdownOptionText(adminBase.MyBrowser, By.Name("Status"), "Status field not found");
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
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_SelfExcl_CreditCustomer()
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

                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit",null,"@aditi.com");
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


            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_All_Product_Credit_SelfExc_Removed_Login()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
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
                    AddTestCase("Verify login is successfull in Bingo after self ex is removed", "Login should be successfully");
                  

                    baseIMS.Init();

                    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", null, "@aditi.com");
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);
                    commIMS.Remove_SelfExclusion_Customer(baseIMS.IMSDriver);
                    baseIMS.Quit();

                    loginData.update_Login_Data(regData.username,
                                          ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication), "");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    Pass("Bingo login successfully");
                      AnW.LogoutFromPortal(driverObj);
                    AddTestCase("Verify login is successfull in Sports", "Login should be successfully");
                 
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Sports page not loaded", 60);
                    AnW.Login_Sports(driverObj, loginData);
                 
                    Pass("LD login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    Fail("Verify_AllPT_Login - failed");
                }

            }


            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_All_Product_FullCust_SelfExc_Removed_Login_1138_1107()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
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

                bool flag = false;
                try
                {
                    AddTestCase("Verify login is successfull in Bingo after self ex is removed", "Login should be successfully");


                    baseIMS.Init();

                    loginData.update_Login_Data(ReadxmlData("fulldata", "sx_user", DataFilePath.IP2_Authetication),
                              ReadxmlData("fulldata", "sx_pwd", DataFilePath.IP2_Authetication));

                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, loginData.username);

                    flag = true;
                    commIMS.CheckLog_SelExclMsg(baseIMS.IMSDriver);

                    adminBase.Init();
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(adminBase.MyBrowser, By.XPath("//b[text()='PT_SELFEX']")), "Self exclusion is not synced with OB");
                    adminBase.Quit();


                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.Remove_SelfExclusion_Customer(baseIMS.IMSDriver);
                    flag = false;
                    baseIMS.Quit();

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);



                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    // Pass("Bingo login successfully");


                    //AddTestCase("Verify login is successfull in Ecom", "Login should be successfully");                  
                    AnW.LogoutFromPortal(driverObj);
                    //Thread.Sleep(TimeSpan.FromSeconds(5));
                    //wAction.WaitforPageLoad(driverObj);
                    //wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "Bingo page not loaded", 60);
                    //AnW.LoginFromEcom(driverObj, loginData);
                    //Pass("Ecom login successfully");

                    AddTestCase("Verify login is successfull in Sports", "Login should be successfully");
                    // AnW.LogoutFromPortal_Ecom(driverObj);
                    driverObj.Manage().Cookies.DeleteAllCookies();
                    wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Sports page not loaded", 60);
                    AnW.Login_Sports(driverObj, loginData);
                    //wAction.Click(driverObj, By.LinkText("Close"));
                    Pass("LD login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(adminBase.MyBrowser, "OB");
                    Fail("Verify_AllPT_Login - failed");
                }
                finally
                {
                    if (flag)
                    {
                        baseIMS.Init();
                        commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                        commIMS.Remove_SelfExclusion_Customer(baseIMS.IMSDriver);
                    }
                    baseIMS.Quit();
                    adminBase.Quit();
                }
            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_SelfExcl_Login_Games_1109()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.GamesURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    AddTestCase("Sports - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    baseIMS.Init();

                
                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    commIMS.SelfExclude_Customer(baseIMS.IMSDriver, regData.username);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Customer created: " + regData.username, "");
                    Pass();
                    baseIMS.Quit();

                    loginData.update_Login_Data(regData.username,
                                            regData.password, "");

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginGames_SelEX(driverObj, loginData);
                    Pass("Verify_SelfExcl_Login_Games_1109 : scenario passed");


                    WriteUserName(regData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Games_1109 : scenario failed");
                }
            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            public void Verify_RetailCust_Login_1143()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
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

                bool flag = false;
                try
                {
                    AddTestCase("SEL-13 Compliance team should be able to get Custom report of failed login attempts of self excluded customer whose channel of registration is 'Retail'", "Login should be successfully");


                    baseIMS.Init();

                    loginData.update_Login_Data(ReadxmlData("retaildata", "sx_user", DataFilePath.IP2_Authetication),
                              ReadxmlData("retaildata", "sx_pwd", DataFilePath.IP2_Authetication));

                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    double custID = StringCommonMethods.ReadDoublefromString(baseIMS.IMSDriver.Url.ToString());

                    commIMS.DisableFreeze(baseIMS.IMSDriver);
                 
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal_SelfExCust(driverObj, loginData, "No Login prompt error is displayed");

                    Thread.Sleep(TimeSpan.FromSeconds(30));
                    commIMS.ComplianceReportView(baseIMS.IMSDriver, "CURL - Failed login attempts", custID);

                    Pass("LD login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_RetailCust_Login_1143 - failed");
                }
                finally
                {
                  
                    baseIMS.Quit();
                  
                }
            }
         
        }     
     

      

    }//RegressionSuite_AnW class
//}//NameSpace
