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
    public class StatementClearDown : BaseTest
    
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
        IP2Common ip2 = new IP2Common();
        SeamLessWallet sw = new SeamLessWallet();
        Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
        Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_Elite_Envoy_Zero_2961()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;


                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "EliteDaily");
                
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser,"Elite");
                //adminBase.Quit();
                commIMS.AddEnvoy(baseIMS.IMSDriver, wallet, "40");               
                 baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch(Exception e)
                 {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_Elite_Cheque_Winner_2960()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;
                string eventID = ReadxmlData("event", "Forecast_eID", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "Forecast_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = ReadxmlData("event", "fAmt", DataFilePath.IP2_SeamlessWallet);


                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "EliteDaily");

       
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));

                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.Login_Sports(driverObj, loginData);
                wAction.Click(driverObj, By.LinkText("Close"));
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);
                
                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();
                 commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, "200");
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
                commIMS.SetAccountSweep(baseIMS.IMSDriver);
                baseIMS.Quit();


                try
                {
                    wAction.PageReload(driverObj);
                    wAction.Click(driverObj, By.LinkText("Close"));
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);
                  //  AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventClass, AccountAndWallets.EventType.Single, amt, 1, true);
                    //bet now button should b displayed
                    AnW.placeBet_Nelson(driverObj);
                }
                catch (Exception) { }



                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_Elite_Cheque_Looser_2966()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "10", "EliteDaily");
                
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "100");
                System.Threading.Thread.Sleep(10000);
               

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();


                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, "10");
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
                baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "40");
                Console.WriteLine(commTB2.GetBalance(baseTB2.IMSDriver));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_PulledElite_Envoy_Zero_2965()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "PulledEliteDaily", true);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();

                commIMS.AddEnvoy(baseIMS.IMSDriver, wallet, "40");
                baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_PulledElite_Cheque_Winner_2982()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "PulledEliteDaily", true);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();

                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, "200");
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
             
             baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_PulledHVC_Envoy_Looser_2984()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "10", "HVCEliteDaily", true);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
            

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "HVC");
                //adminBase.Quit();
                commIMS.AddEnvoy(baseIMS.IMSDriver, wallet, "10");
                  baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "40");
                Console.WriteLine(commTB2.GetBalance(baseTB2.IMSDriver));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_Elite_Cheque_Retain_Winner_2992()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;
                string eventID = ReadxmlData("event", "Forecast_eID", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "Forecast_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = ReadxmlData("event", "fAmt", DataFilePath.IP2_SeamlessWallet);

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "EliteDaily", true);
              
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
             
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);

                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();

            
                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, "200");
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
                commIMS.SetAccountSweep(baseIMS.IMSDriver);
                   baseIMS.Quit();

                              


                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

    }


    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class StatementClearDown_New : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
        IP2Common ip2 = new IP2Common();
        SeamLessWallet sw = new SeamLessWallet();
        Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
        Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_CreditTest_Envoy_Zero_2961()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;


                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "CreditTest");

                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser,"Elite");
                //adminBase.Quit();
                commIMS.AddEnvoy(baseIMS.IMSDriver, wallet, "40");
                commIMS.SetAccountSweep(baseIMS.IMSDriver);
                baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_CreditTest_Cheque_Winner_2960()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;
                string eventID = ReadxmlData("event", "Forecast_eID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "Forecast_name", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "Forecast_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = ReadxmlData("event", "fAmt", DataFilePath.IP2_SeamlessWallet);


                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "CreditTest");


                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));

                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
              
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);

                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();
                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, "200");
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
                commIMS.SetAccountSweep(baseIMS.IMSDriver);
                baseIMS.Quit();


                try
                {
                    wAction.PageReload(driverObj);
                    wAction.Click(driverObj, By.LinkText("Close"));
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);
                    //bet now button should b displayed
                    AnW.placeBet_Nelson(driverObj);
                }
                catch (Exception) { }



                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_CreditTest_Cheque_Looser_2966()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "10", "CreditTest");

                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "100");
                System.Threading.Thread.Sleep(10000);


                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();


                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, "10");
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
                baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "40");
                Console.WriteLine(commTB2.GetBalance(baseTB2.IMSDriver));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_Pulled_Envoy_Zero_2965()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "Pulled", true);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();

                commIMS.AddEnvoy(baseIMS.IMSDriver, wallet, "40");
                baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_Pulled_Cheque_Winner_2982()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "100", "Pulled", true);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);
                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();

                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, "200");
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);

                baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver)));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void CreditCust_Pulled_Envoy_Looser_2984()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver, true, "10", "Pulled", true);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "50");
                System.Threading.Thread.Sleep(10000);


                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "HVC");
                //adminBase.Quit();
                commIMS.AddEnvoy(baseIMS.IMSDriver, wallet, "10");
                baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "40");
                Console.WriteLine(commTB2.GetBalance(baseTB2.IMSDriver));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test]
        [Parallelizable]
        public void DebitTest_Cheque()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);

            #endregion
            string wallet = ReadxmlData("cleardata", "depWallet", DataFilePath.IP2_SeamlessWallet);
            try
            {

                baseIMS.Init();
               // regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");

                baseIMS.Init();
                #region Prerequiste
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication), "GB");
                regData.email = "@aditi.com";
                commTest.Createcustomer_PostMethod(ref regData);
                #endregion

                loginData.username = regData.username; loginData.password = regData.password;

                commIMS.AddStatementClearanceOptions(baseIMS.IMSDriver,false,custom10:"DebitTest");
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));
               // commIMS.AddCreditLimit(baseIMS.IMSDriver, "100");
                System.Threading.Thread.Sleep(10000);


                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                //string portalWin = AnW.OpenMyAcct(driverObj);
                //AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                //driverObj.Close(); driverObj.SwitchTo().Window(portalWin);


                //adminBase.Init();
                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //adminComm.SetStakeLimit(adminBase.MyBrowser);
                //adminComm.SetSegments(adminBase.MyBrowser, "Elite");
                //adminBase.Quit();


                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, "10");
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
                baseIMS.Quit();

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();


                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                commTB2.Place_SingleBet(baseTB2.IMSDriver, "10");
                Console.WriteLine(commTB2.GetBalance(baseTB2.IMSDriver));
                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(adminBase.MyBrowser, "IMS");
                Fail("CreditCust_Envoy_Looser_2961 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

    }


    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class CC_Account_Management : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
        IP2Common ip2 = new IP2Common();
        SeamLessWallet sw = new SeamLessWallet();
        Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
        Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

     


        [Test(Order = 1)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_Registration_Of_CreditCustomer_2644()
        {


            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
            #endregion
            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion

            try
            {
                AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();
                //  regData.username = "testCDTIP1";
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit");
                // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();


                loginData.update_Login_Data(regData.username,
                                       regData.password);
                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);



                adminBase.Init();
                adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
                adminBase.Quit();

                //   wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Sports page not loaded", 60);
                //  AnW.Login_Logout_Sports(driverObj, loginData);

                Pass("Credit limit set successfully");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
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

        [Test(Order = 2)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_SetupCredit_CreditCustomer_2680()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
            #endregion
            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion

            try
            {
                AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
                string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = "20";
                string singleWallet = ReadxmlData("eWallet", "depWallet", DataFilePath.IP2_SeamlessWallet);



                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();

                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();
                loginData.update_Login_Data(regData.username,
                                     regData.password);



                Thread.Sleep(TimeSpan.FromSeconds(20));
                commIMS.AddCreditLimit(baseIMS.IMSDriver, amt);
                commIMS.AddThresholdLimit(baseIMS.IMSDriver, "10");

               
                
                if (FrameGlobals.projectName == "IP2")
                {
                    adminBase.Init();
                    adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                    adminComm.SetStakeLimit(adminBase.MyBrowser);
                    adminBase.Quit();
                }


                AnW.Login_Sports(driverObj, loginData);
                if (FrameGlobals.projectName == "IP2") wAction.Click(driverObj, By.LinkText("Close"));

                //temp
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);                
               // commIMS.AddEnvoy(baseIMS.IMSDriver, singleWallet, amt);               
               
                AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);
                AnW.placeBet_Nelson(driverObj);

                commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt)).ToString());


                //  BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSuccess1_xp)) || wAction.IsElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSuccess2_xp)), "Bet not placed");
                Pass("Credit limit set successfully");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_SetupCredit_CreditCustomer_2680- failed");
            }
            finally
            {
                baseIMS.Quit();

            }
        }

        [Test(Order = 3)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_WriteOFF_CreditCustomer_2683_2682()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
            #endregion
            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion

            try
            {
                AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();

                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();
                loginData.username = regData.username; loginData.password = regData.password;
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);

                string amt = ReadxmlData("woWallet", "depAmt", DataFilePath.IP2_SeamlessWallet);
                string singleWallet = ReadxmlData("woWallet", "depWallet", DataFilePath.IP2_SeamlessWallet);


                commIMS.AddNetteller(baseIMS.IMSDriver, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), singleWallet, amt);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, amt, ReadxmlData("mc", "withdrawBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_With", DataFilePath.IP2_SeamlessWallet));



                BaseTest.Assert.IsTrue(commIMS.ValidateWalletBalance(baseIMS.IMSDriver, singleWallet, "0")
                    , singleWallet + " Balance in Fund Transfer not matching with portal");

                commIMS.Closecustomer(baseIMS.IMSDriver);
                baseIMS.Quit();
                AnW.LogoutFromPortal(driverObj);
                AddTestCase("PT - Message to be displayed when customer logs in with Frozen Customer", "Invalid login error did not appear");
                AnW.LoginFromPortal_SecurityError(driverObj, loginData, ReadxmlData("err", "PT_Closed_Lgn", DataFilePath.IP2_Authetication));
                Pass();

                Pass();
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_WriteOFF_CreditCustomer_2683- failed");
            }
            finally
            {
                baseIMS.Quit();

            }
        }

        [Test(Order = 4)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_FundTransfer_CreditCustomer_2688()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = ((WebDriverBackedSelenium)(iBrowser)).UnderlyingWebDriver;
            driverObj.Close();
            #endregion
            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion

            try
            {
                AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();

                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();

                string amt = ReadxmlData("woWallet", "depAmt", DataFilePath.IP2_SeamlessWallet);
                string singleWallet = ReadxmlData("woWallet", "depWallet", DataFilePath.IP2_SeamlessWallet);


                commIMS.AddFundTransfer(baseIMS.IMSDriver, amt);




                BaseTest.Assert.IsTrue(commIMS.ValidateWalletBalance(baseIMS.IMSDriver, singleWallet, amt)
                    , singleWallet + " Balance in Fund Transfer not matching with portal");



                Pass();
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_FundTransfer_CreditCustomer_2688- failed");
            }
            finally
            {
                baseIMS.Quit();

            }
        }

        [Timeout(1500)]
        [RepeatOnFail]
        [Test(Order = 5)]
        public void Verify_Withdraw_CreditCustomer_2657()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
            #endregion

            #region Declaration
            Login_Data loginData = new Login_Data();
            MyAcct_Data acctData = new MyAcct_Data();
            Registration_Data regData = new Registration_Data();
            #endregion

            AddTestCase("CC-05 Withdrawal of a credit customer with positive balance", "Withdraw should successful.");

            try
            {

                loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.IP2_SeamlessWallet),
                    ReadxmlData("depdata", "pwd", DataFilePath.IP2_SeamlessWallet));

                acctData.Update_deposit_withdraw_Card(ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication),
                     ReadxmlData("depdata", "wAmt", DataFilePath.IP2_SeamlessWallet),
                     ReadxmlData("depdata", "wAmt", DataFilePath.IP2_SeamlessWallet),
                 ReadxmlData("depdata", "depWallet", DataFilePath.IP2_SeamlessWallet), ReadxmlData("depdata", "depWallet", DataFilePath.IP2_SeamlessWallet), null);

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                AnW.LoginFromPortal(driverObj, loginData, iBrowser);

                AnW.OpenCashier(driverObj);
                sw.Withdraw_Netteller(driverObj, acctData, false);
                commTest.CommonWithdrawMoreThanBalance(driverObj, acctData.withdrawWallet, true, true);
                sw.Cancel_Withdraw(driverObj, acctData, false, true);

                Pass();

            }

            catch (Exception e)
            {

                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Withdraw for netteller is failed for exception");

            }
            

        }

        [Test(Order = 6)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_Envoy_CreditCustomer_2686()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.PokerURL);
            #endregion

            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion
            try
            {
                string amt = ReadxmlData("eWallet", "depAmt", DataFilePath.IP2_SeamlessWallet);
                string singleWallet = ReadxmlData("eWallet", "depWallet", DataFilePath.IP2_SeamlessWallet);

                
                AddTestCase("Verify setting credit limit for a customer in OB", "Credit limit should be set successfully");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();

                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();
                loginData.username = regData.username; loginData.password = regData.password;


                AnW.LoginFromPortal(driverObj, loginData, iBrowser);
                
                string portalWin =    AnW.OpenMyAcct(driverObj);
                
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);

             
                commIMS.AddEnvoy(baseIMS.IMSDriver, singleWallet, amt);

                BaseTest.Assert.IsTrue(commIMS.ValidateWalletBalance(baseIMS.IMSDriver, singleWallet, amt), singleWallet + " Balance in Fund Transfer not matching with portal");
                
                portalWin =    AnW.OpenMyAcct(driverObj);
                sw.ValidateWalletBalance_AcctHistory(driverObj, singleWallet, amt);
               
                Pass();
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_Envoy_CreditCustomer_2686- failed");
            }
            finally
            {
                baseIMS.Quit();

            }
        }
        
        [Test(Order = 7)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_CreditCustomer_CardReg_Telebet_2685()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, ReadxmlData("Url", "MyAcct_url", DataFilePath.IP2_SeamlessWallet));
            #endregion
            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            MyAcct_Data acctData = new MyAcct_Data();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            bool check = false; string creditCardNumber="";
            #endregion

            try
            {
                AddTestCase("CC-24 Card registration, Deposit and withdraw are sucessfull in telebet", "Deposit should be done successfully");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();
                regData.email = "abc@abc.com";
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: regData.email);
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();

                loginData.username = regData.username; loginData.password = regData.password;
          
                AnW.LoginPrompt(driverObj, loginData);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);


                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                    baseTB2.Init(driverObj);
                else
                    baseTB2.Init();

                acctData.Update_deposit_withdraw_Card(ReadxmlData("mcdata", "card", DataFilePath.IP2_SeamlessWallet),
                    ReadxmlData("mcdata", "wamt", DataFilePath.IP2_SeamlessWallet),
                     ReadxmlData("mcdata", "damt", DataFilePath.IP2_SeamlessWallet),
                     ReadxmlData("mcdata", "depWallet", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mcdata", "depWallet", DataFilePath.IP2_SeamlessWallet),
                      ReadxmlData("mcdata", "CVV", DataFilePath.IP2_SeamlessWallet));
                creditCardNumber = acctData.card;

                commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                AddTestCase("Deposit to customer " + loginData.username, ""); Pass();

                commTB2.OpenDepositPage(ref baseTB2.IMSDriver);
                check = true;
                //baseTB2.IMSDriver.SwitchTo().DefaultContent();
                Assert.IsTrue(AnW.Verify_FirstCashier_MasterCard(baseTB2.IMSDriver, acctData), "First Deposit amount not added to the wallet");
                baseTB2.IMSDriver.SwitchTo().DefaultContent();
                Assert.IsTrue(commTest.CommonWithdraw_CreditCard_PT(baseTB2.IMSDriver, acctData, acctData.max_amt, false), "Amount not deducted after withdraw");
                Pass();
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(baseTB2.IMSDriver, "Telebet");
                CaptureScreenshot(driverObj, "My Acct");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_CreditCustomer_Transactions_Telebet_2685- failed");
            }

            finally
            {
                if (check)
                {
                    try
                    {
                       // baseIMS.Init();
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
                        baseTB2.Quit();
                    }

                   
                }
            }
        }

        [Test(Order = 8)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_Cheque_CreditCustomer_2687()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser,FrameGlobals.VegasURL);
            #endregion

            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion
            try
            {
                AddTestCase("CC-26 Process cheque deposits on Credit and Debit Customers", "BD should be added successfully");
                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();
                
                
               #region DebitCust
                //regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                //    regData.email = "@aditi.com";
                //    #region NewCustTestData
                //    commTest.Createcustomer_PostMethod(ref regData);
                //    loginData.username = regData.username;
                //    loginData.password = regData.password;
                //    #endregion
                //    Pass("Customer registered succesfully");

                //    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                //    AddTestCase("Customer created: " + regData.username, "");
                //    Pass();
                
                #endregion


                #region CreditCust
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();

                loginData.username = regData.username; loginData.password = regData.password;
                
                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); 
                driverObj.SwitchTo().Window(portalWin);

                #endregion

          
                string amt = ReadxmlData("eWallet", "depAmt", DataFilePath.IP2_SeamlessWallet);
                string singleWallet = ReadxmlData("eWallet", "depWallet", DataFilePath.IP2_SeamlessWallet);
                
                commIMS.AddBankDraft(baseIMS.IMSDriver, singleWallet, amt);
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);


                BaseTest.Assert.IsTrue(commIMS.ValidateWalletBalance(baseIMS.IMSDriver, singleWallet, amt)
                    , singleWallet + " Balance in Fund Transfer not matching after adding fund");

                wAction.PageReload(driverObj);
                AnW.OpenMyAcct(driverObj);
                sw.ValidateWalletBalance_AcctHistory(driverObj, singleWallet, amt);

                Pass();
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(driverObj, "portal");

                Fail("Verify_Cheque_CreditCustomer_2687- failed");
            }
            finally
            {
                baseIMS.Quit();

            }
        }
       
        [RepeatOnFail]
        [Timeout(2200)]
        [Test(Order = 9)]
        [Parallelizable]
        public void Verify_CreditCust_FreeBet_2689_2566_2583()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
            #endregion

            try
            {
                AddTestCase("CC-29 Specific Freebet to a Credit Cusomer", "Winning amount should be added back to the wallet");
                string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);

                string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = "10";


                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
               

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();

                System.Threading.Thread.Sleep(20000);
                loginData.username = regData.username; loginData.password = regData.password;
                adminBase.Init();
                adminComm.SearchCustomer(loginData.username,adminBase.MyBrowser);
               // adminComm.SetStakeLimit(adminBase.MyBrowser);
                System.Threading.Thread.Sleep(3000);
                adminComm.Add_RewardAdhoctoken(adminBase.MyBrowser, regData.username, amt);
                adminBase.Cleanup();


                AnW.Login_Sports(driverObj, loginData);
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close();
                driverObj.SwitchTo().Window(portalWin);

               
                AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);


                BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.ClassName("information")).Contains("£10.00"), "Freebet not triggered");

             //   wAction.Click(driverObj, By.XPath(Sportsbook_Control.freebet_checkbox_xp), "freebet checkbox disabled");


                wAction.Click(driverObj, By.Name("freebet"), "freebet checkbox disabled");
                System.Threading.Thread.Sleep(3000);

                AnW.placeBet_Nelson(driverObj); 
                driverObj.Quit();

                //adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
             //string winning = adminComm.SettleBetForCustomer(adminBase.MyBrowser, "10.00 value token used", "25.00", "0", "1", "0", "0");

              string winning =  commIMS.SettleABet(baseIMS.IMSDriver, "25.00");
               

                
                string singleWallet = ReadxmlData("eWallet", "depWallet", DataFilePath.IP2_SeamlessWallet);
                BaseTest.Assert.IsTrue(commIMS.ValidateWalletBalance(baseIMS.IMSDriver,singleWallet,winning),
                     singleWallet + " Balance in Fund Transfer not matching after settling bet");
               
             
                Pass("Bonus amount added & successfully verified");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(driverObj, "portal");
                CaptureScreenshot(adminBase.MyBrowser, "OB");
                Fail("Verify_CreditCust_FreeBet_2689- failed");
            }
            finally
            {
                baseIMS.Quit();
                adminBase.Cleanup();
            }

        }

        [Test(Order = 10)]
        [RepeatOnFail]
        [Timeout(2200)]
        public void Verify_EndCredit_CreditCustomer_2671()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
            #endregion
            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion

            try
            {

                AddTestCase("CC-10 Removal of Credit limit", "Credit limit should be end successfully");
                string SingleWallet = ReadxmlData("Wallet", "SingleWallet", DataFilePath.IP2_SeamlessWallet);
                string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = "20";


                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                Thread.Sleep(TimeSpan.FromSeconds(20));
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();
                loginData.update_Login_Data(regData.username,
                                     regData.password);
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "100");

                AnW.Login_Sports(driverObj, loginData);
                if (FrameGlobals.projectName == "IP2") wAction.Click(driverObj, By.LinkText("Close"));
                string portalWin =    AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);

                //    commIMS.AddEnvoy(baseIMS.IMSDriver, SingleWallet, "10");
          
                
                
              
                if (FrameGlobals.projectName == "IP2")
                {
                    adminBase.Init();
                    adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                    adminComm.SetStakeLimit(adminBase.MyBrowser);
                    adminBase.Quit();
                }

                wAction.PageReload(driverObj);
                Thread.Sleep(TimeSpan.FromSeconds(3));
                AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);
                AnW.placeBet_Nelson(driverObj);
                commIMS.EndCreditLimit(baseIMS.IMSDriver);
                baseIMS.Quit();
                ip2.Logout_Sports(driverObj);
                AnW.Login_Sports(driverObj, loginData);
              //  double beforeBalance = AnW.GetBalance_Sports(driverObj);


                AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);
                AnW.placeBet_Fail(driverObj);
                
                //bet now button should b displayed

               
               // double afterBalance = AnW.GetBalance_Sports(driverObj);
               // double expectedValue = (beforeBalance - (double.Parse(amt) ));

               // BaseTest.Assert.IsTrue(afterBalance == expectedValue, "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);

                
                
                Pass("Credit limit end successfully");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_EndCredit_CreditCustomer_2671- failed");
            }
            finally
            {
                baseIMS.Quit();

            }
        }
        [Test(Order = 11)]
        [RepeatOnFail]
        [Timeout(2200)]
        public void Verify_StakeGreater_thanCreditLimit_2692()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
            #endregion
            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion

            try
            {

                AddTestCase("SP-05/SP-06 Bet placements of stake greater than Free bet amount from online", "Bet should only be placed within credit limit");
                string SingleWallet = ReadxmlData("Wallet", "SingleWallet", DataFilePath.IP2_SeamlessWallet);
                string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = "11";


                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                Thread.Sleep(TimeSpan.FromSeconds(20));
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();
                loginData.update_Login_Data(regData.username,
                                     regData.password);

             
                AnW.Login_Sports(driverObj, loginData);
                commIMS.AddCreditLimit(baseIMS.IMSDriver, "10");

               // string portalWin = AnW.OpenMyAcct(driverObj);
               // AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
               // driverObj.Close(); driverObj.SwitchTo().Window(portalWin);               
                
               //// commIMS.AddEnvoy(baseIMS.IMSDriver, SingleWallet, "10");
               

                //if (FrameGlobals.projectName == "IP2")
                //{
                //    adminBase.Init();
                //    adminComm.SearchCustomer(loginData.username, adminBase.MyBrowser);
                //    adminComm.SetStakeLimit(adminBase.MyBrowser);
                //    adminBase.Quit();
                //}


                AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);
                AnW.placeBet_Fail(driverObj);
                
                commIMS.ValidateCreditLimit(baseIMS.IMSDriver, "10");

                //amt = "20";
                          
                Pass("Credit limit end successfully");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_StakeGreater_thanCreditLimit_2567- failed");
            }
            finally
            {
                baseIMS.Quit();

            }
        }

        [Test(Order = 11)]
        [RepeatOnFail]
        [Timeout(2200)]
        public void Verify_Deposit_CreditLimitReached_2695()
        {
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
            #endregion
            #region Declaration
            Registration_Data regData = new Registration_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Login_Data loginData = new Login_Data();
            #endregion

            try
            {

                AddTestCase("CC-32 Deposit of funds after the credit limit is reached or breached", "deposit successful when credit limit reached");
                string SingleWallet = ReadxmlData("Wallet", "SingleWallet", DataFilePath.IP2_SeamlessWallet);
                string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = "10";


                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                Thread.Sleep(TimeSpan.FromSeconds(20));
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Customer created: " + regData.username, "");
                Pass();
                loginData.update_Login_Data(regData.username,
                                     regData.password);


                AnW.Login_Sports(driverObj, loginData);
                

                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);

                commIMS.AddCreditLimit(baseIMS.IMSDriver, "10");


                AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);
                AnW.placeBet_Nelson(driverObj);

                commIMS.ValidateCreditLimit(baseIMS.IMSDriver, "0");
                commIMS.AddEnvoy(baseIMS.IMSDriver, SingleWallet, "20");


                BaseTest.Assert.IsTrue(commIMS.ValidateWalletBalance(baseIMS.IMSDriver, SingleWallet, "10"),
                     SingleWallet + " Balance in Fund Transfer not matching after settling bet");


                Pass("Credit limit end successfully");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_Deposit_CreditLimitReached_2695- failed");
            }
            finally
            {
                baseIMS.Quit();

            }
        }

    }

    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class ChequePrinting_ManualAdj : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW = new AccountAndWallets();
        IP2Common ip2 = new IP2Common();
        SeamLessWallet sw = new SeamLessWallet();
        Telebet_Suite.Tele_Base baseTB2 = new Telebet_Suite.Tele_Base();
        Telebet_Suite.Common commTB2 = new Telebet_Suite.Common();
        IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
        Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

        [RepeatOnFail]
        [Timeout(2200)]
        [Test(Order = 1)]
        [Parallelizable]
        public void Verify_Cheque_Withdrawal_2771()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);

            #endregion
          
            
            try
            {

                baseIMS.Init();
                string wallet = ReadxmlData("BDdata", "depWallet", DataFilePath.IP2_SeamlessWallet);
                
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;
                
                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);
                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, ReadxmlData("BDdata", "depAmt", DataFilePath.IP2_SeamlessWallet));
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
                AnW.LogoutFromPortal(driverObj);
        
                wAction.PageReload(baseIMS.IMSDriver);
                commIMS.WithdrawBDFromIMS(baseIMS.IMSDriver, ReadxmlData("BDdata", "wAmt", DataFilePath.IP2_SeamlessWallet), wallet, regData.username);
                commIMS.Approve_Withdraw_Request(baseIMS.IMSDriver);
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                commIMS.Approve_WithdrawRequest_Payments(baseIMS.IMSDriver, loginData.username);

                baseIMS.Quit();

            Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");                
                Fail("Verify_Cheque_Withdrawal_2771 - Failed");
            }
            finally
            {
                baseIMS.Quit();
                
            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
       // [Test(Order = 1)]
        [Parallelizable]
        public void Verify_Cheque_Withdraw_Cancel_2781()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);

            #endregion


            try
            {

                baseIMS.Init();
                string wallet = ReadxmlData("BDdata", "depWallet", DataFilePath.IP2_SeamlessWallet);
                string amt = ReadxmlData("BDdata", "depAmt", DataFilePath.IP2_SeamlessWallet);
                regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit", email: "credit@aditi.com");
                loginData.username = regData.username; loginData.password = regData.password;

                AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close(); driverObj.SwitchTo().Window(portalWin);
                commIMS.AddBankDraft(baseIMS.IMSDriver, wallet, amt);
                commIMS.Approve_DepositRequest(baseIMS.IMSDriver, regData.username);
                AnW.LogoutFromPortal(driverObj);

                wAction.PageReload(baseIMS.IMSDriver);
                commIMS.WithdrawBDFromIMS(baseIMS.IMSDriver, ReadxmlData("BDdata", "wAmt", DataFilePath.IP2_SeamlessWallet), wallet, regData.username);
                commIMS.Approve_Withdraw_Request(baseIMS.IMSDriver,true);
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(baseIMS.IMSDriver, By.XPath(IMS_Control_PlayerDetails.DeclinedWD_Request_XP)), "Withdraw request not cancelled");
                baseIMS.Quit();

                Pass();

            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                Fail("Verify_Cheque_Withdraw_Cancel_2781 - Failed");
            }
            finally
            {
                baseIMS.Quit();

            }

        }

        [RepeatOnFail]
        [Timeout(2200)]
        [Test(Order = 2)]
        [Parallelizable]
        public void Verify_AdjustToStake_2839()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            MyAcct_Data acctData = new MyAcct_Data();
            AccountAndWallets AnW = new AccountAndWallets();
            Login_Data loginData = new Login_Data();
            AdminSuite.AdminBase adminBase = new AdminSuite.AdminBase();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion
            #region DriverInitiation
            IWebDriver driverObj;
            ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            driverObj = browserInitialize(iBrowser, FrameGlobals.SportsURL);
            #endregion

            try
            {
                AddTestCase("CC-29 Specific Freebet to a Credit Cusomer", "Winning amount should be added back to the wallet");
                string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                string amt = "10";


                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                baseIMS.Init();
                regData.email = "@aditi.com"; regData.username = "";
                #region NewCustTestData
                commTest.Createcustomer_PostMethod(ref regData);
                loginData.username = regData.username;
                loginData.password = regData.password;
                #endregion

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, regData.username, false);
                commIMS.AddCorrection_New(baseIMS.IMSDriver, "10", ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), ReadxmlData("mc", "TransactionType_Add", DataFilePath.IP2_SeamlessWallet));


                System.Threading.Thread.Sleep(5000);
                loginData.username = regData.username; loginData.password = regData.password;
   
                AnW.Login_Sports(driverObj, loginData);
                string portalWin = AnW.OpenMyAcct(driverObj);
                AnW.MyAccount_Responsible_Gambling(driverObj, Registration_Data.depLimit);
                driverObj.Close();
                driverObj.SwitchTo().Window(portalWin);


                AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Single, amt, 1);
                AnW.placeBet_Nelson(driverObj);
                driverObj.Quit();

                
                string winning = commIMS.SettleABet(baseIMS.IMSDriver, "25.00",AdjustStake:"10");
                winning = "35.00";


                string singleWallet = ReadxmlData("eWallet", "depWallet", DataFilePath.IP2_SeamlessWallet);
                BaseTest.Assert.IsTrue(commIMS.ValidateWalletBalance(baseIMS.IMSDriver, singleWallet, winning),
                     singleWallet + " Balance in Fund Transfer not matching after settling bet");


                Pass("Bonus amount added & successfully verified");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                CaptureScreenshot(driverObj, "portal");
                
                Fail("Verify_CreditCust_FreeBet_2689- failed");
            }
            finally
            {
                baseIMS.Quit();
                
            }

        }

    }
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

            [Test(Order = 1)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_SingleWallet_Check_PT_SPorts_2524()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                AddTestCase("SP-01 Single wallet Balance displayed to customer in Sportsbook classic/Sportsbeta/Virtual Sports/ PT Portals/", "Sportswallet and Gamming walet should be merged to one");
                MyAcct_Data acctData = new MyAcct_Data();

                try
                {
                    Login_Data loginData = new Login_Data();

                    loginData.update_Login_Data(ReadxmlData("lgnBdata", "user", DataFilePath.IP2_SeamlessWallet),
                    ReadxmlData("lgnBdata", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string ListOfWallets = ReadxmlData("Allwallets", "wallets", DataFilePath.IP2_SeamlessWallet);
                    string SingleWallet = ReadxmlData("Wallet", "SingleWallet", DataFilePath.IP2_SeamlessWallet);
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AddTestCase("Single wallet Balance displayed to customer in PT Portals", "Sportswallet and Gamming walet should be merged to one");
               
                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                    string bals = commTest.HomePage_Balance(driverObj);
                    double bal = StringCommonMethods.ReadDoublefromString(bals);
                    ip2.Verify_SingleWallet_Cashier(driverObj, ListOfWallets, SingleWallet, bal);
                    driverObj.Navigate().Refresh();
                    ip2.Verify_SingleWallet_MyAcct(driverObj, ListOfWallets, SingleWallet, bal);
                    Pass();

                    AddTestCase("Single wallet Balance displayed to customer in Sports", "Sportswallet and Gamming walet should be merged to one");
                    wAction.OpenURL(driverObj,FrameGlobals.SportsURL,"Sports page not loaded",45);
                    wAction.Click(driverObj, By.LinkText("Close"),noWait:false);

                    ip2.Verify_SingleWallet_Cashier(driverObj, ListOfWallets, SingleWallet, bal);
                    driverObj.Navigate().Refresh();
                    wAction.Click(driverObj, By.LinkText("Close"), noWait: false);
                    ip2.Verify_SingleWallet_MyAcct(driverObj, ListOfWallets, SingleWallet, bal);
                    Pass();
                    Pass();

                    

                    

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SingleWallet_Check_2524 has failed");
                    
                }
            }

            [Test(Order = 2)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_SingleWallet_Check_IMS_OB_2528()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.VegasURL);
                #endregion

                MyAcct_Data acctData = new MyAcct_Data();
                IMS_Base baseIMS = new IMS_Base();
                AdminTests Ob = new AdminTests();
                AdminSuite.Common comAdmin = new AdminSuite.Common();

                try
                {
                    Login_Data loginData = new Login_Data();

                    loginData.update_Login_Data(ReadxmlData("lgnBdata", "user", DataFilePath.IP2_SeamlessWallet),
                    ReadxmlData("lgnBdata", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string ListOfWallets = ReadxmlData("Allwallets", "wallets", DataFilePath.IP2_SeamlessWallet);
                    string SingleWallet = ReadxmlData("Wallet", "SingleWallet", DataFilePath.IP2_SeamlessWallet);
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);



                    AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                    string bals = commTest.HomePage_Balance(driverObj);
                    double bal = StringCommonMethods.ReadDoublefromString(bals);

                    AddTestCase("SP-01 Single wallet Balance displayed in IMS", "Sportswallet and Gaming walet should be merged to one");
            
                   
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    wAction._Click(baseIMS.IMSDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "fundTransfer_lnk", "Fund Transfer link not found", 0, false);
                    System.Threading.Thread.Sleep(3000);
                    Pass();

                    BaseTest.Assert.IsTrue(StringCommonMethods.ReadDoublefromString(wAction.GetText(baseIMS.IMSDriver, By.XPath("id('bexgrid_table')//tr[td[contains(text(),'" + SingleWallet + "')]]/td[3]"))) == bal, SingleWallet + " Balance in Fund Transfer not matching with portal");
                    List<IWebElement> listOfWall = wAction.ReturnWebElements(baseIMS.IMSDriver, By.XPath("id('bexgrid')//td[1]"), "Wallet list not found in transfer section");
                    foreach (IWebElement options in listOfWall)
                        BaseTest.Assert.IsTrue(ListOfWallets.Contains(options.Text.Trim()), "Invalid wallet name found:" + options.Text.Trim());

                    AddTestCase("SP-01 Single wallet Balance displayed in OB", "Sportswallet and Gaming walet should be merged to one");
                    baseIMS.Quit();
                    Ob.Init();
                    comAdmin.SearchCustomer(loginData.username, Ob.MyBrowser);
                    System.Threading.Thread.Sleep(2000);
                    BaseTest.Assert.IsTrue(StringCommonMethods.ReadDoublefromString(wAction.GetText(Ob.MyBrowser, By.Id("admin.cust.details.account_balance"))) == bal, SingleWallet + " Balance in customer Detail page not matching with portal");
                    BaseTest.Assert.IsTrue(StringCommonMethods.ReadDoublefromString(wAction.GetText(Ob.MyBrowser, By.Id("admin.cust.details.acct_balance_wtd"))) == bal, SingleWallet + " Withdrawable Balance in customer Detail page not matching with portal");
               
                   
                          

                    Pass();

                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SingleWallet_Check_IMS_2528 has failed");

                }
                finally
                {
                    baseIMS.Quit();
                    Ob.Quit();
                }
            }

            [Test(Order = 3)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_SingleWallet_Check_NonUK_2534()
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

                AddTestCase("SP-01 Single wallet Balance displayed to customer whose country of registration is other than UK and currency other than GBP", "Sportswallet and Gamming walet should be merged to one");


                try
                {


                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_germany", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication), "DE");
                    regData.currency = "EUR";
                    regData.email = "@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    #endregion

                    loginData.username = regData.username;
                    loginData.password = regData.password;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                    loginData.password = regData.password;

                    string amt = ReadxmlData("woWallet", "depAmt", DataFilePath.IP2_SeamlessWallet);

                    string ListOfWallets = ReadxmlData("Allwallets", "wallets", DataFilePath.IP2_SeamlessWallet);
                    string singleWallet = ReadxmlData("Wallet", "SingleWallet", DataFilePath.IP2_SeamlessWallet);
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AddTestCase("Single wallet Balance displayed to customer in PT Portals", "Sportswallet and Gamming walet should be merged to one");

                    AnW.LoginFromPortal(driverObj, loginData, MyBrowser);
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username, false);
                    commIMS.AddNetteller(baseIMS.IMSDriver, ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), singleWallet, amt);
                    baseIMS.Quit();

                    string bals = commTest.HomePage_Balance(driverObj);
                    double bal = StringCommonMethods.ReadDoublefromString(bals);



                    ip2.Verify_SingleWallet_Cashier(driverObj, ListOfWallets, singleWallet, bal);
                    driverObj.Navigate().Refresh();
                    ip2.Verify_SingleWallet_MyAcct(driverObj, ListOfWallets, singleWallet, bal);
                    Pass();


                    Pass();





                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(driverObj, "Portal");
                    Fail("Verify_SingleWallet_Check_NonUK_2534 has failed");

                }
                finally
                {
                    baseIMS.Quit();
                }
            }
                    

            [Test(Order = 5)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_Freebet_Promo_MyAcct_2545()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, FrameGlobals.BingoURL);
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                Registration_Data regData = new Registration_Data();

                #endregion

                AddTestCase("SP-03 Customer should be able to avail Free bet on entering a Promo Code", "Promo code should be accepted");


                try
                {

                    #region Prerequiste
                    regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication), "GB");
                    regData.email = "@aditi.com";
                    commTest.Createcustomer_PostMethod(ref regData);
                    #endregion

                    loginData.username = regData.username;
                    loginData.password = regData.password;
                     WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                     AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                     AnW.OpenMyAcct(driverObj);
                     sw.AddFreeBet(driverObj, 
                         ReadxmlData("freebet", "fPromo_Myacct", DataFilePath.IP2_SeamlessWallet));

                goto temp;
                     System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
                     wAction.PageReload(driverObj);
                     AnW.LogoutFromPortal(driverObj);
                     AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                     AnW.OpenMyAcct(driverObj);
                     sw.ValidateFreeBet(driverObj, ReadxmlData("freebet", "fofferName_Myacct", DataFilePath.IP2_SeamlessWallet),
                          ReadxmlData("freebet", "fPromo_Myacct", DataFilePath.IP2_SeamlessWallet));
                 temp:

                   
                    Pass();





                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Portal");
                    Fail("Verify_Freebet_Promo_MyAcct_2545 has failed");

                }
                
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 6)]
            public void Verify_Forecast_Tricast_2564()
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
                    AddTestCase("SP-05/SP-06 Forecast/Tricast bet placements online should use single wallet balance and also be tracked in IMS.", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "Forecast_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "Forecast_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "Forecast_clID", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "forecast_Amt", DataFilePath.IP2_SeamlessWallet);
                    string amt2 = ReadxmlData("event", "tricast_Amt", DataFilePath.IP2_SeamlessWallet);

                  //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
//                    wAction.Click(driverObj, By.LinkText("Close"));
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);

                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass,  AccountAndWallets.EventType.Forecast,amt, 2,2,amt2);


                    //bet now button should b displayed

                    AnW.placeBet_Nelson(driverObj);

                    wAction.PageReload(driverObj);
                    //validate balance
                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj),2);
                    double expectedValue = Math.Round((beforeBalance- (double.Parse(amt)*2+double.Parse(amt2)*6)),2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                 //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, ((double.Parse(amt)*2).ToString()));
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, ((double.Parse(amt2) * 6).ToString()));
                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Forecast_Tricast_2564- failed");
                }
                finally
                {
                    baseIMS.Quit();
                  
                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 6)]
            public void Verify_EachWay_2562()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Forecast/Tricast bet placements online should use single wallet balance and also be tracked in IMS.", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "eAmt", DataFilePath.IP2_SeamlessWallet);


                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    

                    double beforeBalance =   AnW.GetBalance_Sports(driverObj);

                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.EachWay, amt, 1);


                    //bet now button should b displayed

                    AnW.placeBet_Nelson(driverObj);

                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj),2);
                    double expectedValue = Math.Round((beforeBalance - (double.Parse(amt) * 2)),2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt) * 2).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Forecast_Tricast_2564- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 7)]
            public void Verify_HandiCap_2568()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Forecast/Tricast bet placements online should use single wallet balance and also be tracked in IMS.", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "handicap_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "handicap_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "handi_clname", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "hAmt", DataFilePath.IP2_SeamlessWallet);


                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    

                    double beforeBalance = AnW.GetBalance_Sports(driverObj);

                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Handicap, amt, 1);


                    //bet now button should b displayed

                    AnW.placeBet_Nelson(driverObj);

                    wAction.PageReload(driverObj);
                    //wAction.Click(driverObj, By.LinkText("Close"));

                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj),2);
                    double expectedValue = Math.Round((beforeBalance - (double.Parse(amt))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt)).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_HandiCap_2568 - failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 8)]
            public void Verify_BIR_2563()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 BIR bet placements online should use single wallet balance and also be tracked in IMS. (Bet placement to be tested on different markets and event class)", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "BIR_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "BIR_name", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "bAmt", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "BIR_clname", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    

                    double beforeBalance = AnW.GetBalance_Sports(driverObj);

                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.BIR, amt, 1);


                    //bet now button should b displayed

                    AnW.placeBet_Nelson(driverObj,AccountAndWallets.EventType.BIR);

                    wAction.PageReload(driverObj); wAction.Click(driverObj, By.LinkText("Close"));
                    double afterBalance = AnW.GetBalance_Sports(driverObj);
                    double expectedValue = (beforeBalance - (double.Parse(amt)));

                    BaseTest.Assert.IsTrue(Math.Round(afterBalance, 2).Equals( Math.Round(expectedValue, 2)), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt)).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Forecast_Tricast_2564- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 9)]
            public void Verify_Trixie_2559()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Trixie/Patent bet placements online should use single wallet balance and also be tracked in IMS. (Bet placement to be tested on different markets and event class)", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "tAmt", DataFilePath.IP2_SeamlessWallet);
                    string amt2 = ReadxmlData("event", "pat_Amt", DataFilePath.IP2_SeamlessWallet);
                  
                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Trixie, amt, 1,betslipSelections:1);

                    eventID = ReadxmlData("event", "handicap_eID", DataFilePath.IP2_SeamlessWallet);
                     eventName = ReadxmlData("event", "handicap_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "handi_clname", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Trixie, amt, 1, betslipSelections: 2);

                    eventID = ReadxmlData("event", "Forecast_eID", DataFilePath.IP2_SeamlessWallet);
                    eventName = ReadxmlData("event", "Forecast_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "Forecast_clID", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Trixie, amt, 1, betslipSelections: 3,amount2:amt2);


                    AnW.placeBet_Nelson(driverObj);

                    wAction.PageReload(driverObj);
                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj),2);
                    double expectedValue = Math.Round((beforeBalance - ((double.Parse(amt) * 4)+(double.Parse(amt2) * 7))),2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt) * 4).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt2) * 7).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Trixie_2559- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 10)]
            public void Verify_Yankee_2560()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Yankee/Lucky15 bet placements online should use single wallet balance and also be tracked in IMS. (Bet placement to be tested on different markets and event class)", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                    string Y_amt = ReadxmlData("event", "yAmt", DataFilePath.IP2_SeamlessWallet);
                    string L_amt = ReadxmlData("event", "lAmt", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Yankee, Y_amt, 1, betslipSelections: 1);

                    eventID = ReadxmlData("event", "luck_eID", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "luck_clname", DataFilePath.IP2_SeamlessWallet);
                     eventName = ReadxmlData("event", "luck_name", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Yankee, Y_amt, 1, betslipSelections: 2);

                    eventID = ReadxmlData("event", "Forecast_eID", DataFilePath.IP2_SeamlessWallet);
                     eventName = ReadxmlData("event", "Forecast_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "Forecast_clID", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Yankee, Y_amt, 1, betslipSelections: 3);

                    eventID = ReadxmlData("event", "yank_eID", DataFilePath.IP2_SeamlessWallet);
                     eventName = ReadxmlData("event", "yank_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "yank_clname", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Yankee, Y_amt, 1, betslipSelections: 4, amount2:L_amt);


                    AnW.placeBet_Nelson(driverObj);

                    wAction.PageReload(driverObj);
                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj), 2);
                    double expectedValue = Math.Round((beforeBalance - ((double.Parse(Y_amt) * 11) + (double.Parse(L_amt) * 15))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(Y_amt) * 11).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(L_amt) * 15).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Yankee_2560- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 11)]
            public void Verify_Candian_2561()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Candian/Lucky31/Heinz/Lucky63 bet placements online should use single wallet balance and also be tracked in IMS", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                    string c_amt = ReadxmlData("event", "cAmt", DataFilePath.IP2_SeamlessWallet);
                    string L_amt = ReadxmlData("event", "l31Amt", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Candian, c_amt, 1, betslipSelections: 1);

                    eventID = ReadxmlData("event", "luck_eID", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "luck_clname", DataFilePath.IP2_SeamlessWallet);
                     eventName = ReadxmlData("event", "luck_name", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Candian, c_amt, 1, betslipSelections: 2);

                    eventID = ReadxmlData("event", "Forecast_eID", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "Forecast_clID", DataFilePath.IP2_SeamlessWallet);
                     eventName = ReadxmlData("event", "Forecast_name", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Candian, c_amt, 1, betslipSelections: 3);

                    eventID = ReadxmlData("event", "yank_eID", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "yank_clname", DataFilePath.IP2_SeamlessWallet);
                     eventName = ReadxmlData("event", "yank_name", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Candian, c_amt, 1, betslipSelections: 4, amount2: L_amt);


                    eventID = ReadxmlData("event", "Can_eID", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "Can_clname", DataFilePath.IP2_SeamlessWallet);
                    eventName = ReadxmlData("event", "Can_name", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName, eventClass, AccountAndWallets.EventType.Candian, c_amt, 1, betslipSelections: 5, amount2: L_amt);


                    AnW.placeBet_Nelson(driverObj);

                    wAction.PageReload(driverObj);
                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj), 2);
                    double expectedValue = Math.Round((beforeBalance - ((double.Parse(c_amt) * 26) + (double.Parse(L_amt) * 31))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(c_amt) * 26).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(L_amt) * 31).ToString());

                    Pass("Bet has been placed successfully");
                }
               catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Candian_2561- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }


            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 11)]
            public void Verify_Lucky63_4147()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Candian/Lucky31/Heinz/Lucky63 bet placements online should use single wallet balance and also be tracked in IMS", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "EW_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName1 = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                    string c_amt = ReadxmlData("event", "He_Amt", DataFilePath.IP2_SeamlessWallet);
                    string L_amt = ReadxmlData("event", "l63_Amt", DataFilePath.IP2_SeamlessWallet);

                    string eventID2 = ReadxmlData("event", "luck_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName2 = ReadxmlData("event", "luck_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "luck_clname", DataFilePath.IP2_SeamlessWallet);
                    string eventID3 = ReadxmlData("event", "Forecast_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName3 = ReadxmlData("event", "Forecast_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "Forecast_clID", DataFilePath.IP2_SeamlessWallet);
                    string eventID4 = ReadxmlData("event", "yank_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName4 = ReadxmlData("event", "yank_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "yank_clname", DataFilePath.IP2_SeamlessWallet);
                    string eventID5 = ReadxmlData("event", "Can_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName5 = ReadxmlData("event", "Can_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "Can_clname", DataFilePath.IP2_SeamlessWallet);

                    string eventID6 = ReadxmlData("event", "l63_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName6 = ReadxmlData("event", "l63_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "l63_clname", DataFilePath.IP2_SeamlessWallet);



                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
              
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID,eventName1, eventClass, AccountAndWallets.EventType.Lucky63, c_amt, 1, betslipSelections: 1);

                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID2,eventName2, eventClass, AccountAndWallets.EventType.Lucky63, c_amt, 1, betslipSelections: 2);

                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID3, eventName3, eventClass, AccountAndWallets.EventType.Lucky63, c_amt, 1, betslipSelections: 3);

                      AnW.AddSelections_toBetslip_Nelson(driverObj, eventID4, eventName4, eventClass, AccountAndWallets.EventType.Lucky63, c_amt, 1, betslipSelections: 4);


                     AnW.AddSelections_toBetslip_Nelson(driverObj, eventID5, eventName5, eventClass, AccountAndWallets.EventType.Lucky63, c_amt, 1, betslipSelections: 5);


       
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID6, eventName6, eventClass, AccountAndWallets.EventType.Lucky63, c_amt, 1, betslipSelections: 6, amount2: L_amt);


                    AnW.placeBet_Nelson(driverObj);

                    wAction.PageReload(driverObj);
                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj), 2);
                    double expectedValue = Math.Round((beforeBalance - ((double.Parse(c_amt) * 57) + (double.Parse(L_amt) * 63))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(c_amt) * 57).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(L_amt) * 63).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Heinz_4147- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 6)]
            public void Verify_Double_2557()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Single/Double bet placements online should use single wallet balance and also be tracked in IMS. (Bet placement to be tested on different markets and event class)", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID1 = ReadxmlData("event", "yank_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventID2 = ReadxmlData("event", "luck_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName1 = ReadxmlData("event", "yank_name", DataFilePath.IP2_SeamlessWallet);
                    string eventName2 = ReadxmlData("event", "luck_name", DataFilePath.IP2_SeamlessWallet);

                    string eventClass = ReadxmlData("event", "yank_clname", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "Double_Amt", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);


                    double beforeBalance = AnW.GetBalance_Sports(driverObj);


                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID1,eventName1, eventClass, AccountAndWallets.EventType.Double, amt, 1,1);


                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID2, eventName2, eventClass, AccountAndWallets.EventType.Double, amt, 1, 2);
                    //bet now button should b displayed

                    AnW.placeBet_Nelson(driverObj);

                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj), 2);
                    double expectedValue = Math.Round((beforeBalance - (double.Parse(amt) )), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt)).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Double_2557- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 6)]
            public void Verify_Single_NonUK_2574()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Online Bet placements for customer with germany as Registration country and Currency as EUR/SEK to consider Single wallet balance", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user_EUR", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd_EUR", DataFilePath.IP2_SeamlessWallet));
                    string eventID1 = ReadxmlData("event", "yank_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName1 = ReadxmlData("event", "yank_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "yank_clname", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "Single_Amt", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);

                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID1, eventName1, eventClass, AccountAndWallets.EventType.Single, amt, 1, 1);

                    //bet now button should b displayed
                    AnW.placeBet_Nelson(driverObj);

                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj), 2);
                    double expectedValue = Math.Round((beforeBalance - (double.Parse(amt))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);
                    
                    //fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt)).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Single_NonUK_2557- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 10)]
            public void Verify_Treble_2558()
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
                #endregion

                try
                {
                    AddTestCase("SP-05/SP-06 Trixie/Patent bet placements online should use single wallet balance and also be tracked in IMS. (Bet placement to be tested on different markets and event class)", "Sports events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventID = ReadxmlData("event", "BIR_eID", DataFilePath.IP2_SeamlessWallet);
                    string eventName = ReadxmlData("event", "BIR_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "BIR_clname", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "treb_Amt", DataFilePath.IP2_SeamlessWallet);
                    string amt2 = ReadxmlData("event", "Acc_Amt", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AnW.Login_Sports(driverObj, loginData);
                    double beforeBalance = AnW.GetBalance_Sports(driverObj);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Treble, amt, 1, betslipSelections: 1);

                    eventID = ReadxmlData("event", "pat_eID", DataFilePath.IP2_SeamlessWallet);
                    eventName = ReadxmlData("event", "pat_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "pat_clname", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Treble, amt, 1, betslipSelections: 2);

                    eventID = ReadxmlData("event", "tri_eID", DataFilePath.IP2_SeamlessWallet);
                    eventName = ReadxmlData("event", "tri_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "tri_clname", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Treble, amt, 1, betslipSelections: 3);


                    eventID = ReadxmlData("event", "Acc_eID", DataFilePath.IP2_SeamlessWallet);
                    eventName = ReadxmlData("event", "Acc_name", DataFilePath.IP2_SeamlessWallet);
                    eventClass = ReadxmlData("event", "Acc_clname", DataFilePath.IP2_SeamlessWallet);
                    AnW.AddSelections_toBetslip_Nelson(driverObj, eventID, eventName, eventClass, AccountAndWallets.EventType.Treble, amt, 1, betslipSelections: 4, amount2: amt2);


                    AnW.placeBet_Nelson(driverObj);



                    wAction.PageReload(driverObj);
                    double afterBalance = Math.Round(AnW.GetBalance_Sports(driverObj), 2);

                    double expectedValue = Math.Round((beforeBalance - ((double.Parse(amt) * 4) + (double.Parse(amt2)))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);

                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt) * 4).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt2)).ToString());

                    Pass("Bet has been placed successfully");
           
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Treble_2558- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

        }

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
            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 1)]
            public void Verify_BIR_Telebet_2580()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, "http://www.google.com");
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
                #endregion

                try
                {
                    AddTestCase("SP-05/06 BIR bet placements should use single wallet balance and also be tracked in IMS when placed from Telebt2 application", "Telebet events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "Tele_user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "Tele_pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventName = ReadxmlData("event", "BIR_name", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "bAmt", DataFilePath.IP2_SeamlessWallet);


                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    double beforeBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.BIR);





                    double afterBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    double expectedValue = (beforeBalance - (double.Parse(amt)));

                    BaseTest.Assert.IsTrue(Math.Round(afterBalance, 2).Equals(Math.Round(expectedValue, 2)), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt)).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_BIR_Telebet_2580- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 2)]
            public void Verify_Double_Telebet_2575()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, "http://www.google.com");
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
                #endregion

                try
                {
                    AddTestCase("SP-05/06 Single/Double bet placements should use single wallet balance and also be tracked in IMS when placed from telebet2 application", "Telebet events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "Tele_user", DataFilePath.IP2_SeamlessWallet),
                                            ReadxmlData("event", "Tele_pwd", DataFilePath.IP2_SeamlessWallet));


                    string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "eAmt", DataFilePath.IP2_SeamlessWallet);
                    string amt2 = ReadxmlData("event", "eAmt2", DataFilePath.IP2_SeamlessWallet);
                    string markt = ReadxmlData("event", "Ew_markt", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    double beforeBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.Double, markt, amt2, 1, 1, false);
                    eventName = ReadxmlData("event", "Forecast_name", DataFilePath.IP2_SeamlessWallet);
                    wAction.PageReload(baseTB2.IMSDriver);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.Double, markt, amt2, 1, 2);


                    double afterBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    double expectedValue = (beforeBalance - ((double.Parse(amt) * 2) + (double.Parse(amt2))));

                    BaseTest.Assert.IsTrue(Math.Round(afterBalance, 2).Equals(Math.Round(expectedValue, 2)), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt)).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt2)).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Double_Telebet_2575- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 3)]
            public void Verify_Trexie_Telebet_2577()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, "http://www.google.com");
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
                #endregion

                try
                {
                    AddTestCase("SP-05/06 Single/Double bet placements should use single wallet balance and also be tracked in IMS when placed from telebet2 application", "Telebet events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "Tele_user", DataFilePath.IP2_SeamlessWallet),
                                           ReadxmlData("event", "Tele_pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventName = ReadxmlData("event", "BIR_name", DataFilePath.IP2_SeamlessWallet);
                    string amt = ReadxmlData("event", "tAmt", DataFilePath.IP2_SeamlessWallet);
                    string amt2 = ReadxmlData("event", "pat_Amt", DataFilePath.IP2_SeamlessWallet);
                    string markt = ReadxmlData("event", "tri_markt", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    double beforeBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.BIR, placeBet: false);
                    wAction.PageReload(baseTB2.IMSDriver);

                    eventName = ReadxmlData("event", "pat_name", DataFilePath.IP2_SeamlessWallet);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.Trixie, markt, amt2, 1, 2, false);
                    eventName = ReadxmlData("event", "tri_name", DataFilePath.IP2_SeamlessWallet);
                    wAction.PageReload(baseTB2.IMSDriver);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.Trixie, markt, amt2, 1, 3);


                    double afterBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    double expectedValue = Math.Round((beforeBalance - ((double.Parse(amt) * 4) + (double.Parse(amt2) * 7))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt) * 4).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt2) * 7).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Trexie_Telebet_2577- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 4)]
            public void Verify_Accumulator_Telebet_2576()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, "http://www.google.com");
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
                #endregion

                try
                {
                    AddTestCase("SP-05/06 Accumaltor bet placements should use single wallet balance and also be tracked in IMS when placed from telebet2 application", "Telebet events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "Tele_user", DataFilePath.IP2_SeamlessWallet),
                                           ReadxmlData("event", "Tele_pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventName = ReadxmlData("event", "BIR_name", DataFilePath.IP2_SeamlessWallet);
                   string amt = ReadxmlData("event", "treb_Amt", DataFilePath.IP2_SeamlessWallet);
                   string amt2 = ReadxmlData("event", "Acc_Amt", DataFilePath.IP2_SeamlessWallet);
                   string markt = ReadxmlData("event", "tri_markt", DataFilePath.IP2_SeamlessWallet);

                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    double beforeBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));


                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.BIR, placeBet: false);

                    eventName = ReadxmlData("event", "tri_name", DataFilePath.IP2_SeamlessWallet);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.Trebele, markt, amt2, 1, 2,false);

                    wAction.PageReload(baseTB2.IMSDriver);

                    eventName = ReadxmlData("event", "pat_name", DataFilePath.IP2_SeamlessWallet);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.Trebele, markt, amt2, 1, 3, false);
                    eventName = ReadxmlData("event", "Acc_name", DataFilePath.IP2_SeamlessWallet);
                    wAction.PageReload(baseTB2.IMSDriver);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, amt, eventName, Telebet_Suite.Common.BetEventType.Trebele, markt, amt2, 1, 4);


                    double afterBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    double expectedValue = Math.Round((beforeBalance - ((double.Parse(amt) * 4) + (double.Parse(amt2)))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);

                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt) * 4).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(amt2)).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Accumulator_Telebet_2576- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }
            [Test(Order = 4)]
            [RepeatOnFail]
            [Timeout(1500)]
            public void Verify_SingleWallet_Telebet_2527()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
                #endregion
                #region Declaration
                Login_Data loginData = new Login_Data();
                #endregion

                AddTestCase("SP-01 Single wallet Balance display in telebet", "Sportswallet and Gaming wallet should be merged to one");


                try
                {


                    loginData.update_Login_Data(ReadxmlData("lgnBdata", "user", DataFilePath.IP2_SeamlessWallet),
                    ReadxmlData("lgnBdata", "pwd", DataFilePath.IP2_SeamlessWallet));

                    string ListOfWallets = ReadxmlData("Allwallets", "wallets", DataFilePath.IP2_SeamlessWallet);
                    string SingleWallet = ReadxmlData("Wallet", "SingleWallet", DataFilePath.IP2_SeamlessWallet);
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    AddTestCase("Single wallet Balance displayed to customer in PT Portals", "Sportswallet and Gamming walet should be merged to one");




                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    double bal = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));

                    commTB2.Verify_SingleWallet_Cashier(baseTB2.IMSDriver, ListOfWallets, SingleWallet, bal);
                    baseTB2.IMSDriver.SwitchTo().DefaultContent();
                    wAction.Click(baseTB2.IMSDriver, By.XPath(Telebet_Control.BackToHome_XP), "Back to Telebet link not found");
                    commTB2.Place_SingleBet(baseTB2.IMSDriver, "1");
                    double bal2 = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    BaseTest.Assert.IsTrue(bal == bal2 + 1, "Balance not deducted from single wallet");

                    Pass();





                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseTB2.IMSDriver, "Portal");
                    Fail("Verify_SingleWallet_Telebet_2527 has failed");

                }
                finally
                {
                    baseTB2.IMSDriver.Quit();
                }
            }

            [RepeatOnFail]
            [Timeout(2200)]
            [Test(Order = 4)]
            public void Verify_Yankee_Telebet_2578()
            {
                #region DriverInitiation
                IWebDriver driverObj;
                ISelenium iBrowser = commonFramework.GetDriverByTestCaseName(SeleniumContainer, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                driverObj = browserInitialize(iBrowser, "http://www.google.com");
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
                #endregion

                try
                {
                    AddTestCase("SP-05/06 Yankee/Lucky15 bet placements should use single wallet balance and also be tracked in IMS when placed from Telebt2 application", "Telebet events should be placed successfully");

                    loginData.update_Login_Data(ReadxmlData("event", "Tele_user", DataFilePath.IP2_SeamlessWallet),
                                           ReadxmlData("event", "Tele_pwd", DataFilePath.IP2_SeamlessWallet));
                    string eventName = ReadxmlData("event", "EW_name", DataFilePath.IP2_SeamlessWallet);
                    string eventClass = ReadxmlData("event", "EW_clID", DataFilePath.IP2_SeamlessWallet);
                    string Y_amt = ReadxmlData("event", "yAmt", DataFilePath.IP2_SeamlessWallet);
                    string L_amt = ReadxmlData("event", "lAmt", DataFilePath.IP2_SeamlessWallet);
                    string markt = ReadxmlData("event", "Ew_markt", DataFilePath.IP2_SeamlessWallet);


                    //  goto fin;
                    WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                        baseTB2.Init(driverObj);
                    else
                        baseTB2.Init();


                    commTB2.SearchCustomer(baseTB2.IMSDriver, loginData.username);
                    double beforeBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));


                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, Y_amt, eventName, Telebet_Suite.Common.BetEventType.Yankee, markt, L_amt, 1, 1, false);

                    eventName = ReadxmlData("event", "yank_name", DataFilePath.IP2_SeamlessWallet);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, Y_amt, eventName, Telebet_Suite.Common.BetEventType.Yankee, markt, L_amt, 1, 2, false);

                    wAction.PageReload(baseTB2.IMSDriver);

                    eventName = ReadxmlData("event", "Can_name", DataFilePath.IP2_SeamlessWallet);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, Y_amt, eventName, Telebet_Suite.Common.BetEventType.Yankee, markt, L_amt, 1, 3, false);
                    eventName = ReadxmlData("event", "luck_name", DataFilePath.IP2_SeamlessWallet);
                    wAction.PageReload(baseTB2.IMSDriver);
                    commTB2.Place_SpecificBet(baseTB2.IMSDriver, Y_amt, eventName, Telebet_Suite.Common.BetEventType.Yankee, markt, L_amt, 1, 4);


                    double afterBalance = StringCommonMethods.ReadDoublefromString(commTB2.GetBalance(baseTB2.IMSDriver));
                    double expectedValue = Math.Round((beforeBalance - ((double.Parse(Y_amt) * 11) + (double.Parse(L_amt) * 15))), 2);

                    BaseTest.Assert.IsTrue(afterBalance.Equals(expectedValue), "Bet placement did not impact single wallet, Expected:" + expectedValue + " ,Actual:" + afterBalance);



                    //   fin:
                    baseIMS.Init();
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(Y_amt) * 11).ToString());
                    commIMS.SearchCustomer_Newlook(baseIMS.IMSDriver, loginData.username);
                    commIMS.VerifyWalletHistoryForBet(baseIMS.IMSDriver, (double.Parse(L_amt) * 15).ToString());

                    Pass("Bet has been placed successfully");
                }
                catch (Exception e)
                {
                    exceptionStack(e);
                    CaptureScreenshot(baseIMS.IMSDriver, "IMS");
                    CaptureScreenshot(driverObj, "portal");

                    Fail("Verify_Accumulator_Telebet_2576- failed");
                }
                finally
                {
                    baseIMS.Quit();

                }
            }

        }
    }//Regression_Suite_IP2

