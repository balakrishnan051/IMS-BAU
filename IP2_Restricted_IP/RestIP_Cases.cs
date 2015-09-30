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


namespace IP2_Restricted_IP
{
  
    


        [TestFixture, Timeout(15000)]
        //[Parallelizable]
        public class Login : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();          
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Netteller_Registration_1098()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                //Configuration testdata = TestDataInit();
                Registration_Data regData = new Registration_Data();
            
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
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                   // regData.currency = ReadxmlData("regdata", "currency_USD", DataFilePath.IP2_Authetication);
                    #region NewCustTestData
                    commTest.Createcustomer_PostMethod(ref regData);
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
                    AnW.Verify_Neteller_Registration(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication), ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication));

                    #endregion
                    WriteUserName(regData.username);
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
            [Timeout(1000)]
            [Parallelizable]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Verify_Audit_Dep_ActivityReport_1098()
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

                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP from login overlay from different product homepages", "Restricted login error did not appear");
                try
                {


                    #region user
                  //  Usernames["Verify_Netteller_Registration_1098"] = "Userupfadmyf";
                    loginData.username = Usernames["Verify_Netteller_Registration_1098"];
                    loginData.password = regData.password;
                    #endregion


                    AddTestCase("Try for Poker", "Error message success");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginFromPortal_RestrictedIP(driverObj, loginData);
                    Pass();

                    

                    baseIMS.Init();
                    commIMS.CheckLog_LoginFailedCustomer_ByIP(baseIMS.IMSDriver, loginData.username, Generic.GetPublic_IPAddress(), "Login denied by ASE");

                    baseIMS.Quit();

                    Pass("pass");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Audit_Dep_ActivityReport_1098 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void RestrictLoginIP_Prerequisite()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                try
                {
                    baseIMS.Init();

                    if (FrameGlobals.projectName == "IP2")
                    commIMS.Restrict_IP_Rules(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test,Generic.GetPublic_IPAddress());
                    else
                        commIMS.Restrict_IP_Rules(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage,Generic.GetPublic_IPAddress());
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 99)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Disable_RestLoginIP_Clear()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                try
                {
                    baseIMS.Init();
                    if (FrameGlobals.projectName == "IP2")
                    commIMS.Restrict_IP_Rule_Disable(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Restrict_IP_Rule_Disable(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

             [Test(Order = 11)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            [Parallelizable]
            public void Verify_Restrict_Login_Games_1062()
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
             
                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP for Games Online Portal", "Restricted login error did not appear");
                try
                {

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginGames_ResIP(driverObj, loginData);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                   // commIMS.RestrictedIP_LoginMsg(baseIMS.IMSDriver);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);

                    baseIMS.Quit();

                    Pass("pass");


                    

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Games_1109 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            [DependsOn("RestrictLoginIP_Prerequisite")]
             public void Verify_Restrict_Login_PT_1056_1067()
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

                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP from login overlay from different product homepages", "Restricted login error did not appear");
                try
                {


                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                 
                    
                    AddTestCase("Try for Poker", "Error message success");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginFromPortal_RestrictedIP(driverObj, loginData);
                    Pass();

                    AddTestCase("Try for Casino", "Error message success");
                    wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Casino Url not opened", 40);
                    ip2.LoginFromPortal_RestrictedIP(driverObj, loginData);
                    Pass();

                    AddTestCase("Try for Vegas", "Error message success");
                    wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Vegas Url not opened", 40);
                    ip2.LoginFromPortal_RestrictedIP(driverObj, loginData);
                    Pass();

                    AddTestCase("Try for Bingo", "Error message success");
                    wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Bingo Url not opened", 40);
                    ip2.LoginFromPortal_RestrictedIP(driverObj, loginData);
                    Pass();

                    AddTestCase("Try for LD", "Error message success");
                    wAction.OpenURL(driverObj, FrameGlobals.LiveDealerURL, "Bingo Url not opened", 40);
                    ip2.LoginFromPortal_RestrictedIP(driverObj, loginData);
                    Pass();
           

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);

                    baseIMS.Quit();

                    Pass("pass");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Restrict_Login_PT_1066 : scenario failed");
                }
            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Verify_Restrict_Login_Sports_1058()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
                #endregion

                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP for Classic sports Online Portal", "Restricted login error did not appear");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                 

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.Login_Restricted_Sports(driverObj, loginData);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);

                    baseIMS.Quit();
                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Games_1109 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Verify_Sports_PlaceBet_RestrictLogin_1059()
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

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    //wAction.Click(driverObj, By.LinkText("Close"));

                    wAction.Click(driverObj, By.XPath(Sportsbook_Control.Football_lnk_XP), "Footbal link not loaded", FrameGlobals.reloadTimeOut, false);

                    AnW.AddRandomSelectionTBetSlip_CheckBet(driverObj);
                    //bet now button should b displayed

                    ip2.RestrictIPlogin_fromBetslip(driverObj, loginData);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);

                    baseIMS.Quit();
                 

                    Pass("Sports login successfull");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Sports_PlaceBet_InvalidLogin - Failed");
                }


            }

            [Test(Order = 6)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Verify_Restrict_Login_Overlay_1066()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                Login_Data loginDataI = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion

                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP from login overlay from different product homepages", "Restricted login error did not appear");
                try
                {

                    loginDataI.update_Login_Data("test",
                                     "test");
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginDataI, "Login prompt not loaded");

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    AddTestCase("Try for Poker", "Error message success");
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginFromPortal_Overlay_RestrictedIP(driverObj, loginData);
                    Pass();

                 
                    AddTestCase("Try for Casino", "Error message success");
                    wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Casino Url not opened", 40);
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginDataI, "Login prompt not loaded");
                    ip2.LoginFromPortal_Overlay_RestrictedIP(driverObj, loginData);
                    Pass();

         
                    AddTestCase("Try for Vegas", "Error message success");
                    wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Vegas Url not opened", 40);
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginDataI, "Login prompt not loaded");
                    ip2.LoginFromPortal_Overlay_RestrictedIP(driverObj, loginData);
                    Pass();

                    
                    AddTestCase("Try for Bingo", "Error message success");
                    wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Bingo Url not opened", 40);
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginDataI, "Login prompt not loaded");
                    ip2.LoginFromPortal_Overlay_RestrictedIP(driverObj, loginData);
                    Pass();

                    
                    AddTestCase("Try for LD", "Error message success");
                    wAction.OpenURL(driverObj, FrameGlobals.LiveDealerURL, "LD Url not opened", 40);
                    AnW.LoginFromPortal_InvalidCust(driverObj, loginDataI, "Login prompt not loaded");
                    ip2.LoginFromPortal_Overlay_RestrictedIP(driverObj, loginData);
                    Pass();
                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Restrict_Login_PT_1066 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 7)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Verify_Restrict_Login_Backgammon_1911()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.BackgammonURL);
                #endregion

                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP on backgammon portal", "Restricted login error did not appear");
                try
                {

                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginBackgammon_ResIP(driverObj, loginData);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);

                    baseIMS.Quit();

                    Pass("pass");




                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Games_1109 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 8)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Verify_Restrict_Login_Ecom_1060()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion

                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP for ecom Online Portal", "Restricted login error did not appear");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "RestIP_Lgn", DataFilePath.IP2_Authetication));

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);

                    baseIMS.Quit();
                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Games_1109 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }


            [Test(Order = 9)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Verify_Restrict_Login_Ecom_Betslip_1061()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.EcomURL);
                #endregion

                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP for ecom Online Portal", "Restricted login error did not appear");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                   // ip2.LoginFromEcom_Security(driverObj, loginData, ReadxmlData("err", "RestIP_Lgn", DataFilePath.IP2_Authetication));
                    AnW.SearchEvent(driverObj, ReadxmlData("eventdata", "eventID", DataFilePath.IP2_Authetication));
                    ip2.login_fromBetslip_Ecom_Restrict(driverObj, loginData, ReadxmlData("err", "RestIP_Lgn", DataFilePath.IP2_Authetication));

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);

                    baseIMS.Quit();
                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Games_1109 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 10)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictLoginIP_Prerequisite")]
            public void Verify_Restrict_Login_Exchange_1063()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.ExchangeURL);
                #endregion

                AddTestCase("RE-07/10/11/12/13  Player's login should not be allowed from a restricted IP for exchange Online Portal", "Restricted login error did not appear");
                try
                {
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginFromExchange_Security(driverObj, loginData, ReadxmlData("err", "RestIP_Lgn", DataFilePath.IP2_Authetication));
                    
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.LoginByIP_Lnk_Stage);

                    baseIMS.Quit();
                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_SelfExcl_Login_Games_1109 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }
        }

        [TestFixture, Timeout(15000)]
     //   [Parallelizable]
        public class Registration : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                     
            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void RestrictRegIP_Prerequisite_1088()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                try
                {
                    baseIMS.Init();
                    commIMS.Restrict_IP_Register(baseIMS.IMSDriver);
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 99)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictRegIP_Prerequisite_1088")]
            public void Disable_RestRegIP_Clear()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                try
                {
                    baseIMS.Init();
                    
                    commIMS.Restrict_IP_Reg_Disable(baseIMS.IMSDriver);

                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictRegIP_Prerequisite_1088")]
            public void Verify_Restrict_Register_1054()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                AddTestCase("RE-05 Player registration should not be allowed from a restricted IP for online portal", "Restricted registration error did not appear");
                try
                {
                    string reset =regData.username;
                    #region PT
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    // AnW.Registration_PlaytechPages(driverObj, ref regData,0,false, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));

                    commTest.PP_Registration(driverObj, ref regData);
                   
                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    Assert.IsFalse(wAction.WaitUntilElementPresent(driverObj, By.Id(Reg_Control.CashierFrame_ID)),"First deposit page is getting displayed");
                    wAction.BrowserClose(driverObj);
                    driverObj.SwitchTo().Window(portalWindow);

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                    if (FrameGlobals.projectName == "IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.RegByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.RegByIP_Lnk_Stage);

                    baseIMS.Quit();
                    Pass("Customer registered succesfully");
                    #endregion


                    #region Sports
                    AddTestCase("Create customer from Sports pages", "Customer should be created.");
                    wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "sports page not opened", 50);
                    regData.username = reset;
                    System.Threading.Thread.Sleep(10);
                    wAction.WaitforPageLoad(driverObj);
                    wAction.Click(driverObj, By.XPath("id('login')/a[text()='Open account']"), "Open Account button not found", 0, false);
                    portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    // AnW.Registration_PlaytechPages(driverObj, ref regData,0,false, ReadxmlData("bonus", "regProm", DataFilePath.Accounts_Wallets));

                    commTest.PP_Registration(driverObj, ref regData);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                    System.Threading.Thread.Sleep(10);
                    Assert.IsFalse(wAction.IsElementPresent(driverObj, By.Id(Reg_Control.CashierFrame_ID)), "First deposit page is getting displayed");
                     // Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//div[text()='Please login to view this content']")), "First deposit page is getting displayed");


                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);

                    if(FrameGlobals.projectName=="IP2")
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.RegByIP_Lnk_Test);
                    else
                        commIMS.Fraud_EventDetails(baseIMS.IMSDriver, IMS_Control_Rules.RegByIP_Lnk_Stage);
                   
                    baseIMS.Quit();
                    Pass("Customer registered succesfully");
                    #endregion

                    Pass("pass");

                    WriteUserName(regData.username);


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Restrict_Register_PT_1054 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }


            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("Verify_Restrict_Register_1054")]
            public void Verify_Report_Audit_1089()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                IMS_Base baseIMS = new IMS_Base();
                
                #endregion
                try
                {

                    AddTestCase("RE-19 Compliance team should be able to see an audit of any changes to configure country of registration as a restricted territory", "Customer should be displayed in the log");

                   // Usernames["Verify_Restrict_Register_1054"] = "Userupfablbgx";
                    loginData.update_Login_Data(Usernames["Verify_Restrict_Register_1054"],
                                             ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    baseIMS.Init();

                    if (FrameGlobals.projectName == "IP2")
                        commIMS.ComplianceReportView_RestIP(baseIMS.IMSDriver, ReadxmlData("restData", "Report_RestCountry", DataFilePath.IP2_Authetication), IMS_Control_Rules.RegByIP_Lnk_Test, loginData.username);
                    else
                    commIMS.ComplianceReportView_RestIP(baseIMS.IMSDriver, "LB Fraud cases", IMS_Control_Rules.RegByIP_Lnk_Stage, loginData.username);


                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    Pass();





                }
                 catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "Portal");
                    Fail("Verify_Report_Audit_1089 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }


            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("Verify_Restrict_Register_1054")]
            public void Verify_Restricted_acct_Suspended_1101_1103()
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
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion
                AddTestCase("All Products - Message to be displayed when customer logs in with invalid username and invalid password from online", "Invalid login error did not appear");
                try
                {
                    
                    AddTestCase("RE-06/09 Player Account should be frozen when registered online from a restricted territory IP", "Invalid login error did not appear");
                    loginData.update_Login_Data(Usernames["Verify_Restrict_Register_1054"],
                                             ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    ip2.LoginFromPortal_Suspended_Restricted_Cust(driverObj, loginData);

                    Pass("Verify_Restricted_acct_Suspended_1101 : scenario passed");

     



                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_Restricted_acct_Suspended_1101 : scenario failed");
                }
            }


        }
      
        [TestFixture, Timeout(15000)]
      //  [Parallelizable]
        public class Regulated_Countries : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]        
            public void Verify_Regulated_Register_1052()
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

                AddTestCase("04a Warning message to be displayed when CSA agent chooses Regulated country at the time of Telebet Registration ", "Regulated country registration warning did not appear");
                try
                {
                    string reset = regData.username;
                    #region PT
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_Bel", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    commTest.PP_Registration_RegulatedCountry(driverObj, ref regData);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();
                    
                    wAction.BrowserClose(driverObj);
                    driverObj.SwitchTo().Window(portalWindow);

                   baseIMS.Init();
                   commIMS.SearchCustomer_NotFound(baseIMS.IMSDriver, regData.username);
                   baseIMS.Quit();
                    Pass("Customer registered succesfully");
                    #endregion
                    
                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Restrict_Register_PT_1054 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Regulated_Register_Telebet_1050()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                #region DriverInitiation
                //IWebDriver driverObj;
                //ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                //driverObj = browserInitialize(iBrowser, "http://account-test.ladbrokes.com/en/registration-telebet");

               IWebDriver driverObj = new FirefoxDriver();
                wAction.OpenURL(driverObj, "http://account-test.ladbrokes.com/en/registration-telebet", "Telebet page did not open");
                #endregion
                
                AddTestCase("04a Warning message to be displayed when CSA agent chooses Regulated country at the time of Telebet Registration ", "Regulated country registration warning did not appear");
                try
                {
                    string reset = regData.username;
                    #region PT
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_Bel", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                     
                    commIMS.TeleBet_RegulatedCntry_Registration(driverObj, ref regData);


                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                  
                    baseIMS.Init();
                    commIMS.SearchCustomer_NotFound(baseIMS.IMSDriver, regData.username,false);
                    baseIMS.Quit();
                    Pass("Customer registered succesfully");
                    #endregion

                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_Regulated_Register_Telebet_1050 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_Regulated_Country_URLRedirect_1051()
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

                AddTestCase("04a Warning message to be displayed when CSA agent chooses Regulated country at the time of Telebet Registration ", "Regulated country registration warning did not appear");
                try
                {
                    string reset = regData.username;
                   
                    AddTestCase("RE-04a Customer should be able to redirect to respecitve URL on choosing regulated country from online registration", "URL should be redirected");
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_Bel", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    wAction.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");



                    wAction._SelectDropdownOption(iBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);
                    if (FrameGlobals.projectName == "IP2")
                        wAction.SelectDropdownOption(iBrowser, By.Id("depositLimit"), "5000", "Limit not found", 0, false);
                    else
                        wAction._SelectDropdownOption(iBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                        wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcc_Btn", Keys.Space, "Submit not found", 0, false);
                    else
                        wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcc_Btn", "Submit button not found", 0, false);

                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    wAction.Click(driverObj, By.XPath("//a[text()='REGISTER ON BELGIUM SITE']"), "REGISTER ON BELGIUM SITE pop did not appear", 0, false);

                    driverObj.SwitchTo().Window(portalWindow);

                    Thread.Sleep(TimeSpan.FromSeconds(10));
                    wAction.WaitforPageLoad(driverObj);

                    Assert.IsTrue(driverObj.Url.ToString().Contains(".ladbrokes.be/en/"), "Url did not redirect to .ladbrokes.be/en/");

                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");

                    Fail("Verify_Regulated_Country_URLRedirect_1051 : scenario failed");
                }
              
            }
        }

        [TestFixture, Timeout(15000)]
        //[Parallelizable]
        public class Cashier : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            
      
       

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_RestrictDeposit_ByIP_1092()
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

            

    AddTestCase("RE-20 Customers should not be able to deposit whose IP is restricted to allow only login and withdraw online", "Deposit should be restricted");
                try
                {

                    baseIMS.Init();
                    if(FrameGlobals.projectName=="IP2")
                    commIMS.Restrict_BannedCountry_Enable(baseIMS.IMSDriver, IMS_Control_Rules.DepositByIP_Lnk_Test);
                    else
                        commIMS.Restrict_BannedCountry_Enable(baseIMS.IMSDriver, IMS_Control_Rules.DepositByIP_Lnk_Stage);
                    baseIMS.Quit();

                    loginData.update_Login_Data(ReadxmlData("rdepdata", "user", DataFilePath.IP2_Authetication),
                                           ReadxmlData("rdepdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("rdepdata", "dAmt", DataFilePath.IP2_Authetication),
                         ReadxmlData("rdepdata", "wAmt", DataFilePath.IP2_Authetication),
                          ReadxmlData("rdepdata", "depWallet", DataFilePath.IP2_Authetication), ReadxmlData("rdepdata", "depWallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    ip2.DepositRestrict(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), CashierPage.restricted_BannedCountry_Dep_Msg);

          


                    Pass("pass");




                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_RestrictDeposit_ByIP_1092 : scenario failed");
                }
                finally
                {
                    baseIMS.Init();
                    if (FrameGlobals.projectName == "IP2")
                    commIMS.Restrict_IP_Rule_Disable(baseIMS.IMSDriver, IMS_Control_Rules.DepositByIP_Lnk_Test);
                    else
                        commIMS.Restrict_IP_Rule_Disable(baseIMS.IMSDriver, IMS_Control_Rules.DepositByIP_Lnk_Stage);
                    baseIMS.Quit();
                }

            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void RestrictBannedDepositIP_Prerequisite()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                try
                {
                    baseIMS.Init();
                    if (FrameGlobals.projectName == "IP2")
                    commIMS.Restrict_BannedCountry_Enable(baseIMS.IMSDriver, IMS_Control_Rules.DepositByBannedCountry_Lnk_Test);
                    else
                        commIMS.Restrict_BannedCountry_Enable(baseIMS.IMSDriver, IMS_Control_Rules.DepositByBannedCountry_Lnk_Stage);
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictBannedDepositIP_Prerequisite")]
            public void Disable_BannedDeposit_Clear()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                try
                {
                    baseIMS.Init();
                    if(FrameGlobals.projectName=="IP2")
                    commIMS.Restrict_IP_Rule_Disable(baseIMS.IMSDriver, IMS_Control_Rules.DepositByBannedCountry_Lnk_Test);
                    else
                    commIMS.Restrict_IP_Rule_Disable(baseIMS.IMSDriver, IMS_Control_Rules.DepositByBannedCountry_Lnk_Stage);
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictBannedDepositIP_Prerequisite")]
            public void Verify_BannedDeposit_ByCountry_Portal_1078()
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

                AddTestCase("RE-14 Customers whose country of registration that is soon to be banned are allowed only login and withdraw online", "Deposit should be restricted");
                try
                {


                    loginData.update_Login_Data(ReadxmlData("bdepdata", "user", DataFilePath.IP2_Authetication),
                                           ReadxmlData("bdepdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("bdepdata", "damt", DataFilePath.IP2_Authetication),
                         ReadxmlData("bdepdata", "wamt", DataFilePath.IP2_Authetication),
                          ReadxmlData("bdepdata", "wallet", DataFilePath.IP2_Authetication), ReadxmlData("bdepdata", "wallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    ip2.DepositRestrict(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication),CashierPage.restricted_BannedCountry_Dep_Msg);
                    
                    Pass("pass");




                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
               Fail("Verify_RestrictDeposit_ByIP_1092 : scenario failed");
                }
               

            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictBannedDepositIP_Prerequisite")]
            public void Verify_BannedDeposit_ByCountry_Telebet_1080()
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

                AddTestCase("RE-14 Customers whose country of registration that is soon to be banned are allowed only login and withdraw on Telebet", "Deposit should be restricted");
                try
                {


                    loginData.update_Login_Data(ReadxmlData("bdepdata", "user", DataFilePath.IP2_Authetication),
                                           ReadxmlData("bdepdata", "pwd", DataFilePath.IP2_Authetication));


                    acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_pwd", DataFilePath.IP2_Authetication),
                         ReadxmlData("bdepdata", "damt", DataFilePath.IP2_Authetication),
                         ReadxmlData("bdepdata", "wamt", DataFilePath.IP2_Authetication),
                          ReadxmlData("bdepdata", "wallet", DataFilePath.IP2_Authetication), ReadxmlData("bdepdata", "wallet", DataFilePath.IP2_Authetication), null);

                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);


                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();

                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
             
                    AddTestCase("Deposit to customer " + loginData.username, ""); Pass();
                    commTB2.DepositRestrictTelebet(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), CashierPage.restricted_BannedCountry_Dep_Msg);
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
           
         
        }

        [TestFixture, Timeout(15000)]
        //   [Parallelizable]
        public class PokerLogin : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();



            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void RestrictPokerByIP_Prerequisite()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                try
                {
                    baseIMS.Init();
                    commIMS.Restrict_Country(baseIMS.IMSDriver, ReadxmlData("restData", "pokerRule", DataFilePath.IP2_Authetication), ReadxmlData("restData", "pokerCountry", DataFilePath.IP2_Authetication));
                }
                finally
                {
                    baseIMS.Quit();
                }
            }

          

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictPokerByIP_Prerequisite")]
            public void Verify_BlockedNetwork_Login_972()
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
                    AddTestCase("SEC-01 Customer whose details are added as bad data (serial/personal details) should not be able to login", "Login should not be successfully");
                    #region Prerequiste
                    regData.update_Registration_Data("UserBad", ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    #region NewCustTestData
                    regData.email = "t@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    #endregion
                    Pass("Customer registered succesfully");
                    #endregion
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password;
                    

                    AnW.LoginFromPortal_SecurityError_Prompt(driverObj, loginData, "Unknown username or password");

                    wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Casino page not loaded", 60);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    
                    
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username,false);
                    commIMS.Fraud_EventDetails(baseIMS.IMSDriver, ReadxmlData("restData", "pokerRule", DataFilePath.IP2_Authetication));           
                    Pass("Poker login failed");
                    WriteUserName(loginData.username);

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_BlockedNetwork_Login_972 - failed");
                }
                finally
                {

                    baseIMS.Quit();
                }
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1000)]
            [DependsOn("RestrictPokerByIP_Prerequisite")]
            public void Disable_RestPokerByIP_Clear()
            {
                #region Declaration
                IMS_Base baseIMS = new IMS_Base();
                #endregion
                try
                {
                    baseIMS.Init();

                    commIMS.Restrict_IP_Disable(baseIMS.IMSDriver, ReadxmlData("restData", "pokerRule", DataFilePath.IP2_Authetication));

                }
                finally
                {
                    baseIMS.Quit();
                }
            }


            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1000)]
            [Parallelizable]
            [DependsOn("Disable_RestPokerByIP_Clear")]
            public void Verify_ReLogin_UnBlockedCust_973()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
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
                    loginData.update_Login_Data(Usernames["Verify_BlockedNetwork_Login_972"],
                                             ReadxmlData("lgndata", "pwd", DataFilePath.IP2_Authetication), "");

                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.DisableFreeze(baseIMS.IMSDriver);
                    baseIMS.Quit();
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    Pass("casino login successfully");

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "portal");
                    Fail("Verify_ReLogin_UnBlockedCust_973 - failed");
                }
                finally
                {

                    baseIMS.Quit();
                }
            }

        }

        [TestFixture, Timeout(15000)]
        //  [Parallelizable]
        public class Banned_Countries : BaseTest
        {
            Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
            IP2Common ip2 = new IP2Common();
            Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
            Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1000)]
            public void Verify_BannedCountry_Register_1087()
            {
                #region Declaration
                Registration_Data regData = new Registration_Data();
                Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
                Login_Data loginData = new Login_Data();
                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                string temp = Registration_Data.depLimit;
                IWebDriver driverObj2 = new FirefoxDriver();
                #endregion
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
                #endregion

                AddTestCase("RE-17 Ensure IMS Admin can remove country from banned countries and future Registrations are valid", "Banned country should work as expected");
                try
                {

                    #region SetBannedCountry
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_Banned", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));

                    baseIMS.Init();
                    commIMS.BannedCountrySet(baseIMS.IMSDriver, regData.country_code);
                    #endregion




                    #region PT_BannedApplied
                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                    commTest.PP_Registration_BannedCountry(driverObj, ref regData);

                    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                    AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                    Pass();

                    wAction.BrowserClose(driverObj);
                    driverObj.SwitchTo().Window(portalWindow);


                    Pass("Customer registered succesfully");
                    #endregion

                    #region PT_BannedRemoved
                    commIMS.BannedCountryRemove_HalfNavigation(baseIMS.IMSDriver, regData.country_code);

                    AddTestCase("Create customer from Playtech pages", "Customer should be created.");

                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
                    Pass("Customer registered succesfully");

                    regData.username = "User";
                    wAction.OpenURL(driverObj2, ReadxmlData("Url", "telebet_Reg_url", DataFilePath.IP2_Authetication), "Telebet page did not open");
                    Registration_Data.depLimit = "5000";
                    commIMS.TeleBet_Registration(driverObj2, ref regData);
                    

                    #endregion

                   
                   
                    Pass("pass");


                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    Fail("Verify_BannedCountry_Register_1087 : scenario failed");
                }
                finally
                {
                    baseIMS.Quit();
                    Registration_Data.depLimit = temp;
                    driverObj2.Quit();
                }
            }


        }
}//NameSpace
