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




    //[TestFixture, Timeout(15000)]
    [Parallelizable]
    public class Bonus : BaseTest
    {
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();

        /// <summary>
        /// Roopa
        /// Verify the Sign up bonus 
        /// </summary>
      //  [Test(Order = 1)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Verify_SignUp_Bonus_Registration()
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
            driverObj = browserInitialize(iBrowser);
            #endregion

            try
            {
                AddTestCase("Excecute Verify_SignUp_Bonus_Registration", "Verify_SignUp_Bonus_Registration : Pass");
                AddTestCase("Register a customer selecting Indonesia as country and currency as AUD", "Registration should be successfull");

                regData.update_Registration_Data(ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "country_indonesia", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                regData.currency = ReadxmlData("regdata", "currency_AUD", DataFilePath.Accounts_Wallets);

                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Find Address button not found", 0, false);
                AnW.Registration_PlaytechPages(driverObj, ref regData);
                Pass("Customer registered succesfully");

                AddTestCase("Verify the Bonus after registration", "Bonus should be credited to the customer");

                if (AnW.OpenMyAcct(driverObj,false)!=null)
                {
                    if (!AnW.MyAccount_VerifyFreeBet(driverObj, ReadxmlData("bonus", "signup", DataFilePath.Accounts_Wallets), ReadxmlData("bonus", "signup_amt", DataFilePath.Accounts_Wallets)))
                    {
                        Fail("Freebet not added");
                    }
                }
                else
                {
                    Fail("Navigation to My account page is failed");

                }

                Pass("Bonus is credited to the customer as expected");

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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
                wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 10);
                Pass("Customer registered succesfully");
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 10);

                BaseTest.AddTestCase("Verify if new customers get bonus on Register (with promo Code) ->  First Deposit if condition satisfies.", "First deposit should be successful");
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                BaseTest.Assert.IsTrue(AnW.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
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



        [RepeatOnFail]
        [Timeout(2200)]
        //[Test(Order = 2)]
        [Parallelizable]
        public void Verify_SignUpDeposit_Autobonus_Satisfies()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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

                commTest.PP_Registration(driverObj, ref regData);
                Pass("Customer registered succesfully");
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();
                wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 10);
                BaseTest.AddTestCase("Verify if new customers get bonus on Register without promo Code ->  First Deposit if condition satisfies.", "First deposit should be successful");
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                BaseTest.Assert.IsTrue(AnW.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
                      ReadxmlData("netdata", "account_pwd", DataFilePath.Accounts_Wallets),
                      ReadxmlData("bonus", "First_deposit_depoWallet", DataFilePath.Accounts_Wallets),
                      ReadxmlData("bonus", "First_deposit_depoWallet", DataFilePath.Accounts_Wallets),
                       ReadxmlData("bonus", "First_deposit_bonus", DataFilePath.Accounts_Wallets), false, true),
                      "Bonus amount not added to: " + ReadxmlData("bonus", "bonusWallet", DataFilePath.Accounts_Wallets));


                BaseTest.Pass("Bonus amount added & successfully verified");
            }
            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Verify_SignUpDeposit_Autobonus_Satisfies - failed");
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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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
                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();

                BaseTest.AddTestCase("Verify if new customers do not get bonus on Register (with promo Code) ->  First Deposit if condition not satisfies.", "First deposit should be successful");
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                BaseTest.Assert.IsFalse(AnW.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
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
      //  [Test(Order = 4)]
        [Parallelizable]
        public void Verify_SportsFreeBet_bonus_satisfies_Ecom()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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

                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();

                BaseTest.AddTestCase("Verify if new customers get bonus on Register (with promo Code) ->  First Deposit if condition satisfies.", "First deposit should be successful");
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                BaseTest.Assert.IsTrue(AnW.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
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

                AnW.SearchEvent(driverObj, eventName);
                AnW.AddToBetSlipPlaceBet_selection(driverObj, oddValue, stake);

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
      //  [Test(Order = 5)]
        [Parallelizable]
        public void Verify_SportsFreeBet_bonus_notsatisfies_Ecom()
        {
            #region Declaration
            Registration_Data regData = new Registration_Data();
            IMS_Base baseIMS = new IMS_Base();
            IMS_AdminSuite.Common commIMS = new IMS_AdminSuite.Common();
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
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


                WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
                AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                Pass();

                BaseTest.AddTestCase("Verify if new customers get bonus on Register (with promo Code) ->  First Deposit if condition satisfies.", "First deposit should be successful");
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                BaseTest.Assert.IsTrue(AnW.Verify_FirstCashier_Neteller(driverObj, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets),
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

                AnW.SearchEvent(driverObj, eventName);
                AnW.AddToBetSlipPlaceBet_selection(driverObj, oddValue, stake);

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
                     ReadxmlData("bonus", "depoAmnt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("bonus", "bonusWallet", DataFilePath.Accounts_Wallets), ReadxmlData("bonus", "bonusWallet", DataFilePath.Accounts_Wallets), null);

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);
                AnW.DepositTOWallet_Netteller(driverObj, acctData, ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), ReadxmlData("bonus", "depoProm", DataFilePath.Accounts_Wallets));

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
        Ladbrokes_IMS_TestRepository.AccountAndWallets AnW= new AccountAndWallets();


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

                loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                    ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                    ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "wAmt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), null);

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                AnW.AllWallets_Transfer(driverObj, acctData, ReadxmlData("depdata", "TransWalletDropDown", DataFilePath.Accounts_Wallets),
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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Registration_Data regData = new Registration_Data();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion

            AddTestCase("Verifying fund transfer through IMS admin", "Fund should be transferred successfully");
            try
            {
                imsAdmin.Init();
                commIMS.SearchCustomer_Newlook(imsAdmin.IMSDriver, ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets));

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
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();
            Registration_Data regData = new Registration_Data();
            AdminSuite.Common adminComm = new AdminSuite.Common();
            #endregion

            AddTestCase("Verifying fund transfer through IMS admin", "Fund should be transferred successfully");
            try
            {
                imsAdmin.Init();
                commIMS.SearchCustomer_Newlook(imsAdmin.IMSDriver, ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets));

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
                loginData.update_Login_Data(ReadxmlData("depdata", "user", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "pwd", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCFname", DataFilePath.Accounts_Wallets));

                acctData.Update_deposit_withdraw_Card(ReadxmlData("depdata", "CCard", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                     ReadxmlData("depdata", "CCAmt", DataFilePath.Accounts_Wallets),
                      ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), null);

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);

                AnW.LoginFromPortal(driverObj, loginData,iBrowser);

                AnW.Wallet_Transfer_Balance_Insuff(driverObj, acctData, ReadxmlData("depdata", "FromWallet", DataFilePath.Accounts_Wallets),
                 ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets));

                Pass();

            }
            catch (Exception e)
            {

                exceptionStack(e);
                CaptureScreenshot(driverObj, "Portal");
                Fail("Verify_Transfer_Insufficient_Fund failed");

            }

        }


      //  [Test(Order = 4)]
        [RepeatOnFail]
        [Timeout(1600)]
        public void Verify_Transfer_Portal_Ecom()
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
                      ReadxmlData("depdata", "FromWallet", DataFilePath.Accounts_Wallets), ReadxmlData("depdata", "ToWallet", DataFilePath.Accounts_Wallets), null);

                WriteCommentToMailer("UserName: " + loginData.username + ";\nPassword: " + loginData.password);
                AnW.LoginFromEcom(driverObj, loginData);
                AnW.Wallet_Transfer_Single_Ecom(driverObj, acctData);

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



}
      


