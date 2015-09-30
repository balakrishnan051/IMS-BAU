using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Data;
using Framework;
using Selenium;
using Gallio;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;

using OpenQA.Selenium.Support.UI;
using ICE.ObjectRepository.Vegas_IMS_BAU;
using ICE.ObjectRepository;
//using ICE.Vegas_IMS_Data;
//using ICE.ActionRepository;
using ICE.DataRepository.Vegas_IMS_Data;
using IMS_AdminSuite;
using ICE.DataRepository;
using System.Threading;

namespace Ladbrokes_IMS_TestRepository
{
    public class IP2Common
    {

        Common RepositoryCommon = new Common();
        wActions wAction = new wActions();
        BaseTest baseTest = new BaseTest();
        SeamLessWallet sw = new SeamLessWallet();
        AccountAndWallets AnW = new AccountAndWallets();

        public void Login_SecurityError_Sports(IWebDriver driverObj, Login_Data loginData, string errorMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            BaseTest.AddTestCase("Login to Sports portal usign valid credential: " + loginData.username, "Customer should be able to Login");


            wAction.Clear(driverObj, By.Id(Sportsbook_Control.username_Id), "Header element username textbox is not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.username_Id), loginData.username, "Header element username textbox is not found", 0, false);
            wAction.Click(driverObj, By.Id(Sportsbook_Control.pwd_Id), "Header element Password textbox is not found", 0, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.pwd_Id), loginData.password, "Header element Password textbox is not found", 0, false);
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.login_btn_XP), "Login not found", 0, true);
               



            if (wAction.GetText(driverObj, By.XPath(Sportsbook_Control.InvalidLoginError_XP), "Invalid Login Error not found", false).Contains(errorMsg))
            {
                BaseTest.Pass();

            }
            else
                BaseTest.Fail("Sports login error did not appear");

        }

        public void Login_Invalid_Sports(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            BaseTest.AddTestCase("Login to Sports portal usign valid credential: " + loginData.username, "Customer should be able to Login");


            wAction.Clear(driverObj, By.Id(Sportsbook_Control.username_Id), "Header element username textbox is not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.username_Id), loginData.username, "Header element username textbox is not found", 0, false);
            wAction.Click(driverObj, By.Id(Sportsbook_Control.pwd_Id), "Header element Password textbox is not found", 0, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.pwd_Id), loginData.password, "Header element Password textbox is not found", 0, false);
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.login_btn_XP), "Login not found", 0, true);
               
            string errorMsg = baseTest.ReadxmlData("err", "Sports_Invalid_Lgn", DataFilePath.IP2_Authetication);


            if (wAction.GetText(driverObj, By.XPath(Sportsbook_Control.InvalidLoginError_XP), "Sports Login Error not found", false).Contains(errorMsg))
            {
                BaseTest.Pass();

            }
            else
                BaseTest.Fail("Sports login error did not appear");

        }
        public void Login_Restricted_Sports(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            BaseTest.AddTestCase("Login to Sports portal usign valid credential: " + loginData.username, "Customer should be able to Login");


            wAction.Clear(driverObj, By.Id(Sportsbook_Control.username_Id), "Header element username textbox is not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.username_Id), loginData.username, "Header element username textbox is not found", 0, false);
            wAction.Click(driverObj, By.Id(Sportsbook_Control.pwd_Id), "Header element Password textbox is not found", 0, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.pwd_Id), loginData.password, "Header element Password textbox is not found", 0, false);
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.login_btn_XP), "Login not found", 0, true);
            if (wAction.WaitUntilElementPresent(driverObj, By.XPath("id('login_error')/h3['Ladbrokes does not allow bets/activity from restricted territories.']")))
            {
                BaseTest.Pass();

            }
            else
                BaseTest.Fail("Sports login Restricted IP error did not appear");

        }

        public void Login_SelfEx_Sports(IWebDriver driverObj, Login_Data loginData,string error)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            BaseTest.AddTestCase("Login to Sports portal usign valid credential: " + loginData.username, "Customer should be able to Login");


            wAction.Clear(driverObj, By.Id(Sportsbook_Control.username_Id), "Header element username textbox is not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.username_Id), loginData.username, "Header element username textbox is not found", 0, false);
            wAction.Click(driverObj, By.Id(Sportsbook_Control.pwd_Id), "Header element Password textbox is not found", 0, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.pwd_Id), loginData.password, "Header element Password textbox is not found", 0, false);
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.login_btn_XP), "Login not found", 0, true);
            //   string s =wAction.GetText(driverObj, By.XPath(Sportsbook_Control.InvalidLoginErrorPrompt_XP)); 
            Thread.Sleep(3000);
            if (wAction.GetText(driverObj, By.XPath(Sportsbook_Control.InvalidLoginError_XP), "Login error prompt not found", false).Contains(error))
            {
                BaseTest.Pass();

            }
            else
                BaseTest.Fail("Sports login frozen error did not appear");

        }
        public void Verify_Neteller_Registration_Basic(IWebDriver driverObj, string neteller_id, string neteller_pwd, string depositWallet, string amt)
        {

            BaseTest.AddTestCase("Register the customer to netteller account", "Netteller should be added successfully");


            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            wAction.Click(driverObj, By.LinkText("NETeller"), "Neteller payment method is not found", 0, false);

            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "neteller_accountId", neteller_id, "Neteller account id textbox is not found", 0, false);

            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", neteller_pwd, "Neteller password textbox is not found", FrameGlobals.reloadTimeOut, false);

            wAction.Type(driverObj, By.Id("textAmount"), amt, "Neteller payment method is not found", 0, false);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //  wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "source_wallet_select", depositWallet, "Source wallet select box not found", FrameGlobals.reloadTimeOut, false);

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "Deposit button is not found", 0, false);

            System.Threading.Thread.Sleep(3000);

            driverObj.SwitchTo().DefaultContent();
            // wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Generic_Xpath));


            if (wAction.WaitUntilElementPresent(driverObj, By.XPath("//h1[contains(text(),'Your deposit succeeded')]")))
                BaseTest.Pass();
            else
                BaseTest.Fail("Neteller Registration/deposit failed");

        }
        public bool Verify_FirstCashier_Visa(IWebDriver driverObj, MyAcct_Data acctData)
        {
            //   wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
            acctData.cardCSC = "123";
            string typeImag = "visa_tab";

            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, typeImag, typeImag + " Not found in the deposit page");
            wAction.WaitAndMovetoFrame(driverObj, Cashier_Control_SW.redirectIframe);
            //wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.name, "credit_card_frame", null, FrameGlobals.elementTimeOut);

            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "credit_number", acctData.card, "Credit card number field is not found/Not loaded", 0, false);

            wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "expiry_month", "4", "Expiry month select box is not found", 0, false);

            wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "expiry_year", "4", "Expiry Year select box is not found", 0, false);

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "continue_btn", "Continue button is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(3000);

            driverObj.SwitchTo().DefaultContent();
            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Visa_img", "MasterCard_img not found");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "creditcard_cvv", "CVV_txt not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "creditcard_cvv", acctData.cardCSC, "CVV_txt not found");
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));


            driverObj.SwitchTo().DefaultContent();
            wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));
            //  bool s = wAction.IsElementPresent(driverObj, By.Id("playtechModalMessages"));
            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
            //wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", 8);

            //string bonusWalletPath = "//tr[td[contains(text(),'" + bonusWallet.ToLower().Substring(0, 1).ToUpper() + bonusWallet.ToLower().Substring(1) + "')]]/td[2]";
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "goback_lnk");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            driverObj.SwitchTo().DefaultContent();
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw tab is not found", FrameGlobals.reloadTimeOut, false);

            double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + " Wallet value not found", false).ToString().Replace('£', ' ').Trim());

            if (AfterVal == Convert.ToDouble(acctData.depositAmt))
                return true;
            else
                return false;

        }
        public bool Verify_FirstCashier_Neteller(IWebDriver driverObj, string neteller_id, string neteller_pwd, string depositWallet, string amount)
        {
            //   wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);

            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "neteller_payment_tab", null, 10, false);

            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "neteller_accountId", neteller_id, "Neteller account id textbox is not found", 0, false);

            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", neteller_pwd, "Neteller password textbox is not found", FrameGlobals.reloadTimeOut, false);

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_text_amount", "Amount textbox is not found", FrameGlobals.reloadTimeOut, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_text_amount", amount, "Amount textbox is not found", FrameGlobals.reloadTimeOut, false);

            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "source_wallet_select", depositWallet, "Source wallet select box not found", FrameGlobals.reloadTimeOut, false);

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "Deposit button is not found", 0, false);

            System.Threading.Thread.Sleep(3000);

            driverObj.SwitchTo().DefaultContent();
            wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Generic_Xpath), null, 0, false);
             clickAgain:
            
                        wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_OK_Xpath));
                        wAction.WaitforPageLoad(driverObj);
                        if (wAction.IsElementPresent(driverObj, By.XPath(Login_Control.modelWindow_OK_Xpath)))
                            goto clickAgain;

             clickAgain2:
                        wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath));
                        wAction.WaitforPageLoad(driverObj);
                        if (wAction.IsElementPresent(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath)))
                            goto clickAgain2;
             clickAgain3:
                        wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Xpath));
                        wAction.WaitforPageLoad(driverObj);
                        if (wAction.IsElementPresent(driverObj, By.XPath(Login_Control.modelWindow_Xpath)))
                            goto clickAgain3;

            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 5);
            BaseTest.AddTestCase("Verify Neteller payment method registration and first deposit bonus", "Neteller registration should be successful");
           // string bonusWalletPath = "//tr[td[contains(text(),'" + depositWallet.ToLower().Substring(0, 1).ToUpper() + depositWallet.ToLower().Substring(1) + "')]]/td[2]";
            string bonusWalletPath = "//tr[td[contains(text(),'" + depositWallet+"')]]/td[2]";
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw tab is not found", FrameGlobals.reloadTimeOut, false);

            driverObj.SwitchTo().DefaultContent();
            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 5);
           
            double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(bonusWalletPath), depositWallet + " Wallet value not found", false).ToString().Replace('£', ' ').Trim());
            

            if (AfterVal == Convert.ToDouble(amount))
                return true;
            else
                return false;

        }

        public void login_fromBetslip(IWebDriver driverObj, Login_Data lgnData)
        {
            BaseTest.AddTestCase("Login from Betslip", "Login should be successfull");
            wAction.Type(driverObj, By.Id(Sportsbook_Control.Bet_UserName_Txt_ID), lgnData.username, "Username textbox not found in Betslip", 0, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.Bet_Pwd_Txt_ID), lgnData.password, "Password textbox not found in Betslip");
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.Bet_Login_Btn_XP), "Login Button not found in Betslip");
            if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "welcome_txt"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Sports login failed");

        }
        public void login_fromBetslip_Ecom(IWebDriver driverObj, Login_Data loginData)
        {
            BaseTest.AddTestCase("Add to betslip for the given bet for the event", "Selection should get added to the bet slip");
            //Select bet
            System.Threading.Thread.Sleep(4000);
            try
            {
                List<IWebElement> ele = driverObj.FindElements(By.XPath("//a[contains(@class,'betbutton')]")).ToList();
                ele[0].Click();
            }
            catch (Exception e) { BaseTest.Fail("No selection found for the event"); }

            System.Threading.Thread.Sleep(4000);
            //wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "FreeBet_Collapse", "FreeBet_Collapse not found", 0, false);

            //Enter Stake
            wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "StakeAmt_text", "1", "StakeAmt_text Box not found", 0, false);

            BaseTest.Pass();

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Ecomm_Control.BetSlip_Username_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.BetSlip_Username_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Ecomm_Control.BetSlip_Pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.BetSlip_Pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Ecomm_Control.BetSlip_Pwd_ID), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.Id(Ecomm_Control.BetSlip_Submit_ID)).Click();
            wAction.WaitforPageLoad(driverObj);



            System.Threading.Thread.Sleep(10000);

            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox");
            //wAction.Click(driverObj, By.XPath("//a/div[text()='X']"));
            BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Ecom_welcome_txt"), "Customer unable to login");
            //BaseTest.Assert.IsTrue(commonWebMethods.GetText(driverObj, By.XPath(Login_Control.userDisplay_Xpath), "User login failed,{Error In: Login_Control.userDisplay_Xpath}").ToString().Contains(loginData.fname), "Customer login failed");
            wAction.WaitforPageLoad(driverObj);
            BaseTest.Pass();

        }
        public void login_fromBetslip_Ecom_Restrict(IWebDriver driverObj, Login_Data loginData, string err)
        {
            BaseTest.AddTestCase("Add to betslip for the given bet for the event", "Selection should get added to the bet slip");
            //Select bet
            System.Threading.Thread.Sleep(4000);

            try
            {
                List<IWebElement> ele = driverObj.FindElements(By.XPath("//a[contains(@class,'betbutton')]")).ToList();
                ele[0].Click();
            }
            catch (Exception e) { BaseTest.Fail("No selection found for the event"); }

            System.Threading.Thread.Sleep(4000);
            //wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "FreeBet_Collapse", "FreeBet_Collapse not found", 0, false);

            //Enter Stake
            wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "StakeAmt_text", "1", "StakeAmt_text Box not found", 0, false);

            BaseTest.Pass();

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Ecomm_Control.BetSlip_Username_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.BetSlip_Username_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Ecomm_Control.BetSlip_Pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.BetSlip_Pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Ecomm_Control.BetSlip_Pwd_ID), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.Id(Ecomm_Control.BetSlip_Submit_ID)).Click();
            wAction.WaitforPageLoad(driverObj);

            System.Threading.Thread.Sleep(10000);
            // wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox");


            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath("//a[@id='F_Betslip-Submit' and contains(@class,'disable')]")), "Customer has logged in");

            // BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath("id('betslip-scroll')//p"),"No error message found",false).Contains(err), "Customer unable to login");

            wAction.WaitforPageLoad(driverObj);
            BaseTest.Pass();

        }
        public void Invalidlogin_fromBetslip(IWebDriver driverObj, Login_Data lgnData)
        {
            BaseTest.AddTestCase("Login from Betslip", "Login should be successfull");
            wAction.Type(driverObj, By.Id(Sportsbook_Control.Bet_UserName_Txt_ID), lgnData.username, "Username textbox not found in Betslip", 0, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.Bet_Pwd_Txt_ID), lgnData.password, "Password textbox not found in Betslip");
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.Bet_Login_Btn_XP), "Login Button not found in Betslip");

            if (wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.InvalidLoginError_XP)))
                BaseTest.Pass();
            else
                BaseTest.Fail("Sports login failed");



        }
        public void RestrictIPlogin_fromBetslip(IWebDriver driverObj, Login_Data lgnData)
        {
            BaseTest.AddTestCase("Login from Betslip", "Login should be successfull");
            wAction.Type(driverObj, By.Id(Sportsbook_Control.Bet_UserName_Txt_ID), lgnData.username, "Username textbox not found in Betslip", 0, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.Bet_Pwd_Txt_ID), lgnData.password, "Password textbox not found in Betslip");
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.Bet_Login_Btn_XP), "Login Button not found in Betslip");

            if (wAction.WaitUntilElementPresent(driverObj, By.XPath("id('login_error')/h3['Ladbrokes does not allow bets/activity from restricted territories.']")))
            {
                BaseTest.Pass();

            }
            else
                BaseTest.Fail("Sports login Restricted IP error did not appear");



        }

        public void Logout_Sports(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Logout from sportsbook", "logout should be successfull");
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.LoggedinMenu_XP), "Menu not found", FrameGlobals.reloadTimeOut, false);
            System.Threading.Thread.Sleep(3000);
            wAction.Click(driverObj, By.Id("logoutSubmit"), "Logout button not found");
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Sportsbook_Control.username_Id)), "Customer still not logged out!");
            BaseTest.Pass();

        }
        public void Logout_Games(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Logout from games", "logout should be successfull");

            wAction.Click(driverObj, By.XPath(Games_Control.userMenu), "Menu not found", FrameGlobals.reloadTimeOut, false);
            System.Threading.Thread.Sleep(3000);
            wAction.Click(driverObj, By.LinkText("Logout"), "Logout button not found");
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("tbUsername")), "Logout not working");

            BaseTest.Pass();

        }

        public void Wallet_Transfer_Single(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.depositWallet + " wallet to  " + acctData.withdrawWallet + "", "Transfer Amount should be success from the selected wallet");
            String portalWindow = AnW.OpenCashier(driverObj);


            acctData.wallet1 = acctData.depositWallet;
            acctData.wallet2 = acctData.withdrawWallet;

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
            BaseTest.Pass();
        }

        public void Withdraw_DeleteCookies(IWebDriver driverObj, MyAcct_Data acctData, string site = null)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = AnW.OpenCashier(driverObj);

            string withWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";
            double beforeVal = 0;
            if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
            else
                wAction._Click_Javascript(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            String temp = wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();

            if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                beforeVal = StringCommonMethods.ReadDoublefromString(temp);
            else
                BaseTest.Fail("Wallet value is null/Blank");

            BaseTest.AddTestCase("Verify that the amount in the " + acctData.withdrawWallet + " wallet is less than the desired amount" + acctData.depositAmt, "Amount should be more than the desired amount");
            if (beforeVal < double.Parse(acctData.depositAmt))
                BaseTest.Fail("Insufficient balance in the wallet to withdraw");
            else
                BaseTest.Pass();

            wAction.SelectDropdownOption_ByPartialText(driverObj, By.Id(CashierPage.Sofort_withdraw_To_ID), "NETeller", "Netteller option not found", 0, false);


            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", acctData.depositAmt, "Amount_txt not found");
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "destinationWallet_cmb", acctData.withdrawWallet, "destinationWallet_cmb not found");

            driverObj.Manage().Cookies.DeleteAllCookies();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");
            // wAction.Click(driverObj, By.Name("genericWithdraw.confirmation.yes"), "Deposit not successfull", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine("Validation: " + wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Confirmation_Dlg"));

            //BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Login_Control.promptWindow_Xpath)), "Login prompt is not found");
            BaseTest.Pass();
        }

        public void DepositRestrict(IWebDriver driverObj, MyAcct_Data acctData, string netAcc, string error, string site = null)
        {

            BaseTest.AddTestCase("Verify the customer is able to perform withdraw and not deposit", "Deposit should not be successfull");
            
            AnW.OpenCashier(driverObj);
            AnW.Withdraw_Netteller(driverObj, acctData, site);

            #region Deposit
            driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

            BaseTest.AddTestCase("Verify that Payment method is added to the customer but cannot deposit", "The customer should have the payment option added to it but gets error msg during deposit");
            String nettellerImg = "//table[contains(@class,'data')]//tbody[contains(@id,'accounts')]//td[contains(text(),'" + netAcc + "')]";
            wAction.Click(driverObj, By.XPath(nettellerImg), "Netteller Image not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");

            string errmsg = wAction.GetText(driverObj, By.XPath("id('globalErrors')//span"), "Restrict message not found", false);
            string error2 = "Ladbrokes do not accept deposits from this territory";
            BaseTest.Assert.IsTrue(errmsg.Contains(error) || errmsg.Contains(error2), "Restrict login error message is incorrect");

            BaseTest.Pass();
            #endregion

            BaseTest.Pass();
        }

        public void MyAccount_ChangePassword_Format(IWebDriver driverObj, String password)
        {

            System.Threading.Thread.Sleep(5000);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "change_Pwd_lnk", "Change Password link is not found", FrameGlobals.reloadTimeOut, false);

            BaseTest.AddTestCase("In the 'New Password' field, input a password less than 8 characters that includes at least one alphanumeric, one uppercase and one lowercase and click outside the Password field",
                "Password not allowed and player is prompted that the password has to have at least 8 characters");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "NewPwd1", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "NewPwd1", "Unable to find element", 0, false);
            System.Threading.Thread.Sleep(3000);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");

            if (wAction.IsElementPresent(driverObj, By.XPath(MyAcctPage.PwdFormatMeter_XP)) && !wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Password changed successfully using invalid old password");

            BaseTest.AddTestCase("Input a password greater than 8 characters that includes only alphabets, one uppercase and one lowercase and click outside the Password field",
           "Password not allowed and player is prompted that the password has to have at least 8 characters");

            wAction.PageReload(driverObj);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "NewPwdtest", "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "NewPwdtest", "Unable to find element");
            System.Threading.Thread.Sleep(3000);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");

            if (wAction.IsElementPresent(driverObj, By.XPath(MyAcctPage.PwdFormatMeter_XP)) && !wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Password changed successfully using invalid old password");

            BaseTest.AddTestCase("Input a password greater than 8 characters that includes only digits, and click outside the Password field",
                 "Password not allowed and player is prompted that the password has to have at least 8 characters");

            wAction.PageReload(driverObj);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "1234567890", "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "1234567890", "Unable to find element");
            System.Threading.Thread.Sleep(3000);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");

            if (wAction.IsElementPresent(driverObj, By.XPath(MyAcctPage.PwdFormatMeter_XP)) && !wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Password changed successfully using invalid old password");


            BaseTest.AddTestCase("Input a password equal to 8 characters that includes at least one alphanumeric, one uppercase or one lowercase letter and click outside the Password field",
                     "Password allowed");

            string tempPwd = "Lb123456";
            wAction.PageReload(driverObj);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", tempPwd, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", tempPwd, "Unable to find element");
            System.Threading.Thread.Sleep(3000);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");
            if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text", "Confirmation text block not found", false).Contains("Password change was successful"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Unable to change password");

            BaseTest.AddTestCase("Input a password greater than 8 characters that includes at least one alphanumeric, one uppercase or one lowercase letter and click outside the Password field",
                    "Password allowed");
            password = tempPwd;
            tempPwd = "Lb1234567";
            wAction.PageReload(driverObj);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", tempPwd, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", tempPwd, "Unable to find element");
            System.Threading.Thread.Sleep(3000);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");
            if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text", "Confirmation text block not found", false).Contains("Password change was successful"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Unable to change password");


            BaseTest.AddTestCase("Input a password greater than 8 characters that includes at least one letter and 1 digit all in uppercase and click outside the Password field",
                    "Password allowed");
            password = tempPwd;
            tempPwd = "LB1234567";
            wAction.PageReload(driverObj);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", tempPwd, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", tempPwd, "Unable to find element");
            System.Threading.Thread.Sleep(3000);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");
            if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text", "Confirmation text block not found", false).Contains("Password change was successful"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Unable to change password");
            BaseTest.AddTestCase("Input a password greater than 8 characters that includes at least one letter and 1 digit all in lowercase and click outside the Password field",
                            "Password allowed");
            password = tempPwd;
            tempPwd = "lb1234569";
            wAction.PageReload(driverObj);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", tempPwd, "Unable to find element");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Unable to find element", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", tempPwd, "Unable to find element");
            System.Threading.Thread.Sleep(3000);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");
            if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text", "Confirmation text block not found", false).Contains("Password change was successful"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Unable to change password");


        }
        public string Registration_PasswordCheck(IWebDriver driverObj, ref Registration_Data regData, string PasswordMeter = null, string finalErr = null)
        {
            var random = new Random();
            ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
            myBrowser.Start();
            myBrowser.WindowMaximize();
            System.Threading.Thread.Sleep(2000);

            string fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            string lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            string username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            string email = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + regData.email;


            string uname = "Failed";

            int accept = 3;

        title:
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);

        fname:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", fname, "First name not found", 0, false);

        lname:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", lname, "Last name not found", 0, false);

        dob:
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);

        email:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", email, "Email  Not Found", 0, false);

        tele:

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);

            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);

        address:
            if (regData.country_code == "United Kingdom")
            {

                wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "post_txt not found", 0, false);

                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", regData.uk_postcode, "Post Code Not Found", 0, false);

                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "findAddr_Btn", "Find Address button not found", 0, false);
                System.Threading.Thread.Sleep(2000);
                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "findAddr_Btn", "Find Address button not found", 0, false);


                if (!wAction.IsElementPresent(driverObj, By.Id("house")))
                {
                    wAction.Click(driverObj, By.Id("postCodeManually"), "Manual Address link not found");
                    wAction.Type(driverObj, By.Id("house"), regData.houseNo, "House number box not found");
                    wAction.Type(driverObj, By.Id("address"), regData.uk_addr_street_1, "address box not found");
                    wAction.Type(driverObj, By.Id("city"), regData.city, "City box not found");
                }
            }
            else
            {
                wAction.Type(driverObj, By.Id("postCode"), Registration_Data.other_postcode, "post box not found");
                wAction.Type(driverObj, By.Id("address"), regData.uk_addr_street_1, "address box not found");
                wAction.Type(driverObj, By.Id("city"), regData.city, "City box not found");
            }

        uname:

            regData.username = username;

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", regData.username, "User name  Not Found", 0, false);

        pass:

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);
            if (PasswordMeter != null)
            {
                string msg = wAction.GetText(driverObj, By.XPath("//div[@class='comment']")).ToString();
                BaseTest.Assert.IsTrue(msg.ToUpper().Contains(PasswordMeter.ToUpper()), "Pasword Meter shows Actual:" + msg + " ;Expected:" + PasswordMeter);
            }

        pconfirm:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", "pwdverify_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", regData.password, "Password  Not Found", 0, false);


        security:
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_cmb", Registration_Data.securityQues, "Title not found", 0, false);

        answer:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", "answer_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", regData.answer, "House  Not Found", 0, false);

        BettingCurrency:
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "currency_ddn", regData.currency, "currency not found", 0, false);

        deplimit:
            if (FrameGlobals.projectName == "IP2")
                wAction.SelectDropdownOption(myBrowser, By.Id("depositLimit"), Registration_Data.depLimit, "Limit not found", 0, false);
            else
                wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code", "promo_code not found", 0, false);

            Thread.Sleep(TimeSpan.FromSeconds(2));

        checkbox:
            if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
            {
                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "terms_check", Keys.Space, "Accept Check Box Not Found", 0, false);

            }

            else
                wAction._Click(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "terms_check", "Accept Check Box Not Found", 0, false);


            Thread.Sleep(TimeSpan.FromSeconds(2));

            if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcc_Btn", Keys.Space, "Submit not found", 0, false);
            else
                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcc_Btn", "Submit button not found", 0, false);




            if (finalErr == null)
            {

                System.Threading.Thread.Sleep(2000);
            ClkAgain:
                wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
                if (wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.ModelDialogOK)))

                    goto ClkAgain;


                wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 40);
                System.Threading.Thread.Sleep(5000);
                string isReg = wAction.GetText(driverObj, By.Name("header_username"), "No username header found post reg", false);
                BaseTest.Assert.IsTrue(isReg.Contains(regData.username), "Customer not registered - Username not found cashier page");
            }
            else
            {
                string isReg = wAction.GetText(driverObj, By.XPath("id('tooltip')/p/span"), "No tool tip error found", false);
                if (finalErr.Contains("\\r"))
                    finalErr = finalErr.Replace("\\r", "\r");
                if (finalErr.Contains("\\n"))
                    finalErr = finalErr.Replace("\\n", "\n");

              
                BaseTest.Assert.IsTrue(isReg.Contains(finalErr), "Tool tip Error mismatched ; Actual:" + isReg + " Expected:" + finalErr);

            }

            return regData.username;



        }


        public string Registration_UsernameCheck(IWebDriver driverObj, ref Registration_Data regData)
        {
            var random = new Random();
            ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
            myBrowser.Start();
            myBrowser.WindowMaximize();
            System.Threading.Thread.Sleep(2000);

            string fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            string lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            string email = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + regData.email;


            int accept = 3;

        title:
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);

        fname:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", fname, "First name not found", 0, false);

        lname:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", lname, "Last name not found", 0, false);

        dob:
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);

        email:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", email, "Email  Not Found", 0, false);

        tele:

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);

            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);

        address:
            if (regData.country_code == "United Kingdom")
            {

                wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "post_txt not found", 0, false);

                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", regData.uk_postcode, "Post Code Not Found", 0, false);

                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "findAddr_Btn", "Find Address button not found", 0, false);
                System.Threading.Thread.Sleep(2000);
                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "findAddr_Btn", "Find Address button not found", 0, false);


                if (!wAction.IsElementPresent(driverObj, By.Id("house")))
                {
                    wAction.Click(driverObj, By.Id("postCodeManually"), "Manual Address link not found");
                    wAction.Type(driverObj, By.Id("house"), regData.houseNo, "House number box not found");
                    wAction.Type(driverObj, By.Id("address"), regData.uk_addr_street_1, "address box not found");
                    wAction.Type(driverObj, By.Id("city"), regData.city, "City box not found");
                }
            }
            else
            {
                wAction.Type(driverObj, By.Id("postCode"), Registration_Data.other_postcode, "post box not found");
                wAction.Type(driverObj, By.Id("address"), regData.uk_addr_street_1, "address box not found");
                wAction.Type(driverObj, By.Id("city"), regData.city, "City box not found");
            }

        uname:

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", regData.username, "User name  Not Found", 0, false);

        pass:

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);
           
            //username suggestion box
            BaseTest.Assert.IsTrue( wAction.IsElementPresent(driverObj, By.Id("usernameSuggestionsTmpl")),"Suggestion box did not appear for existing user");


        pconfirm:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", "pwdverify_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", regData.password, "Password  Not Found", 0, false);


        security:
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_cmb", Registration_Data.securityQues, "Title not found", 0, false);

        answer:
            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", "answer_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", regData.answer, "House  Not Found", 0, false);

        BettingCurrency:
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "currency_ddn", regData.currency, "currency not found", 0, false);

        deplimit:
            if (FrameGlobals.projectName == "IP2")
                wAction.SelectDropdownOption(myBrowser, By.Id("depositLimit"), Registration_Data.depLimit, "Limit not found", 0, false);
            else
                wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code", "promo_code not found", 0, false);

            Thread.Sleep(TimeSpan.FromSeconds(2));

        checkbox:
            if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
            {
                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "terms_check", Keys.Space, "Accept Check Box Not Found", 0, false);

            }

            else
                wAction._Click(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "terms_check", "Accept Check Box Not Found", 0, false);


            Thread.Sleep(TimeSpan.FromSeconds(2));

            if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcc_Btn", Keys.Space, "Submit not found", 0, false);
            else
                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcc_Btn", "Submit button not found", 0, false);


                        BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//p[text()='Username not available']")), "Username not available message did not appear");


            return regData.username;



        }



        public void LoginGames(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

            ISelenium bro = new WebDriverBackedSelenium(driverObj, "http://google.com");
            bro.Start();

            wAction.Click(driverObj, By.XPath("//*[text()='No thanks']"));
        retype:
            wAction.Clear(bro, By.Id(Games_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(bro, By.Id(Games_Control.uname_ID), loginData.username);
            wAction.Type(bro, By.Id(Games_Control.pwd_ID), loginData.password, "Password field not found/not editable");
            if (wAction.GetAttribute(driverObj, By.Id(Games_Control.uname_ID), "value") == string.Empty)
                goto retype;
            wAction.Click(bro, By.XPath(Games_Control.login_XP), "Unable to click on login");



            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Games_Control.depositLink)), "Login did not happen/Deposit link missing");
            
            wAction.Click(driverObj,By.ClassName("lgt-close-bt"));
            BaseTest.Pass();

        }
        public void LoginGames_SecurityError(IWebDriver driverObj, Login_Data loginData, string errorMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

            ISelenium bro = new WebDriverBackedSelenium(driverObj, "http://google.com");
            bro.Start();

        retype:
            wAction.Clear(bro, By.Id(Games_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(bro, By.Id(Games_Control.uname_ID), loginData.username);
            wAction.Type(bro, By.Id(Games_Control.pwd_ID), loginData.password, "Password field not found/not editable");
            if (wAction.GetAttribute(driverObj, By.Id(Games_Control.uname_ID), "value") == string.Empty)
                goto retype;
            wAction.Click(bro, By.XPath(Games_Control.login_XP), "Unable to click on login");



            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_Games_XP), "Sports Login Error not found", false).
                Contains(errorMsg)
             , "Customer login error not incorrect");
            BaseTest.Pass();

        }
        public void LoginGames_Invalid(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

            ISelenium bro = new WebDriverBackedSelenium(driverObj, "http://google.com");
            bro.Start();
        retype:
            wAction.Clear(bro, By.Id(Games_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(bro, By.Id(Games_Control.uname_ID), loginData.username);
            wAction.Type(bro, By.Id(Games_Control.pwd_ID), loginData.password, "Password field not found/not editable");
            if (wAction.GetAttribute(driverObj, By.Id(Games_Control.uname_ID), "value") == string.Empty)
                goto retype;
            wAction.Click(bro, By.XPath(Games_Control.login_XP), "Unable to click on login");

            string errorMsg = baseTest.ReadxmlData("err", "Games_Invalid_Lgn", DataFilePath.IP2_Authetication);


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_Games_XP), "Sports Login Error not found", false).
                Contains(errorMsg)
             , "Customer login error not incorrect");
            BaseTest.Pass();

        }
        public void LoginGames_SelEX(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

            ISelenium bro = new WebDriverBackedSelenium(driverObj, "http://google.com");
            bro.Start();

        retype:
            wAction.Clear(bro, By.Id(Games_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
        wAction.Type(bro, By.Id(Games_Control.uname_ID), loginData.username);
        wAction.Type(bro, By.Id(Games_Control.pwd_ID), loginData.password, "Password field not found/not editable");
        if (wAction.GetAttribute(driverObj, By.Id(Games_Control.uname_ID), "value") == string.Empty)
                goto retype;            
            wAction.Click(bro, By.XPath(Games_Control.login_XP), "Unable to click on login");


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_Games_XP), "Sports Login prompt not found", false).
                Contains("Your account is currently excluded. If you believe this is incorrect, please e-mail selfexclusion@ladbrokes.com")
             , "Customer login error not incorrect");
            BaseTest.Pass();

        }
        public void LoginGames_ResIP(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

            ISelenium bro = new WebDriverBackedSelenium(driverObj, "http://google.com");
            bro.Start();

        retype:
            wAction.Clear(bro, By.Id(Games_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(bro, By.Id(Games_Control.uname_ID), loginData.username);
            wAction.Type(bro, By.Id(Games_Control.pwd_ID), loginData.password, "Password field not found/not editable");
            if (wAction.GetAttribute(driverObj, By.Id(Games_Control.uname_ID), "value") == string.Empty)
                goto retype;
            wAction.Click(bro, By.XPath(Games_Control.login_XP), "Unable to click on login");

            System.Threading.Thread.Sleep(5000);
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_Games_XP), "games Login prompt not found", false).
                Contains("Ladbrokes does not allow bets/activity from restricted territories")
             , "Customer login error not correct");
            BaseTest.Pass();

        }
        public void LoginBackgammon_ResIP(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

            ISelenium bro = new WebDriverBackedSelenium(driverObj, "http://google.com");
            bro.Start();

        retype:
            wAction.Clear(bro, By.Id("tbUsername"), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(bro, By.Id("tbUsername"), loginData.username);
            wAction.Clear(bro, By.Id("password-clear"), "Password text box not found", 0, false);
            wAction.Type(bro, By.Id("tbPassword"), loginData.password, "Password field not found/not editable");

            if (wAction.GetAttribute(driverObj, By.Id("tbUsername"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath("//span[contains(text(),'Login')]")).Click();

            System.Threading.Thread.Sleep(5000);
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.Id(Reg_Control.invalidLoginErr_backgmon_XP), "games Login prompt not found", false).
                Contains("Ladbrokes does not allow bets/activity from restricted territories")
             , "Customer login error not correct");
            BaseTest.Pass();

        }
        public void LoginFromPortal_Overlay_RestrictedIP(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            wAction.WaitforPageLoad(driverObj);


        retype:
            wAction.Clear(driverObj, By.XPath("//div[contains(@class,'login-popup')]//input[@id='username']"), "username not found", 0, false);
            driverObj.FindElement(By.XPath("//div[contains(@class,'login-popup')]//input[@id='username']")).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.XPath("//div[contains(@class,'login-popup')]//input[@id='password']"), "username not found", 0, false);
            driverObj.FindElement(By.XPath("//div[contains(@class,'login-popup')]//input[@id='password']")).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.XPath("//div[contains(@class,'login-popup')]//input[@id='username']"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath("//div[contains(@class,'login-popup')]//button[text()='Login']")).Click();

            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLogin_ModelBox_PT_XP), "error pop up did not appear", false).
                Contains("Ladbrokes does not allow bets/activity from restricted territories. For further information please refer to our Restricted Territories"), "incorrect login error message");
            BaseTest.Pass();
        }
        public void LoginFromPortal_RestrictedIP(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            wAction.WaitforPageLoad(driverObj);


        retype:
            wAction.Clear(driverObj, By.Name("username"), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Name("password"), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLogin_ModelBox_PT_XP), "error pop up did not appear", false).
                Contains("Ladbrokes does not allow bets/activity from restricted territories. For further information please refer to our Restricted Territories"), "incorrect login error message");
            BaseTest.Pass();
        }


        public void LoginFromPortal_Suspended_Restricted_Cust(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            wAction.WaitforPageLoad(driverObj);
            wAction.WaitUntilElementPresent(driverObj, By.Name("username"));

        retype:
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();
            //IWebElement ele = driverObj.FindElement(By.XPath("//div[contains(@class,'login-popup')]"));

            // wAction.Click(driverObj, By.XPath(Cashier_Control_SW.TransactionNotification_OK_Dialog));
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath("//div[contains(@class,'login-popup')]")), "Login prompt did not loaded");

            //commonWebMethods.WaitforPageLoad(driverObj);
            //  if (FrameGlobals.projectName == "IP2")
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_PT_XP)).Contains("This account is suspended for security reasons. Please contact Customer Support at support@vegas.ladbrokes.com for further assistance. We apologize for any inconvenience caused"), "incorrect login error message");
            BaseTest.Pass();
        }
        public void LoginFromEcom_Security(IWebDriver driverObj, Login_Data loginData, string errorMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();

            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with self ex user details : " + loginData.username, "Login should not be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Ecomm_Control.Username_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.Username_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Ecomm_Control.Pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.Pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Ecomm_Control.Pwd_ID), "value") == string.Empty)
                goto retype;


            wAction.Click(driverObj, By.Id(Ecomm_Control.LoginBtn_ID), "Submit button not found");


            wAction.WaitforPageLoad(driverObj);


            System.Threading.Thread.Sleep(2000);
            // wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox");
            // BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.invalidLoginErr_Ecom_XP)), "invalid error message not found");


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_Ecom_XP), "Invalid Login Error not found", false).
             Contains(errorMsg)
         , "Customer login error not incorrect");

            BaseTest.Pass();

        }

        public void LoginFromExchange_Security(IWebDriver driverObj, Login_Data loginData, string errorMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();

            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with self ex user details : " + loginData.username, "Login should not be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Ecomm_Control.Username_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.Username_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Ecomm_Control.Pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.Pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Ecomm_Control.Pwd_ID), "value") == string.Empty)
                goto retype;


            wAction.Click(driverObj, By.XPath("//a[text()='Login']"), "Login button not found");

            wAction.WaitforPageLoad(driverObj);
            System.Threading.Thread.Sleep(2000);


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_Ecom_XP), "Invalid Login Error not found", false).
             Contains(errorMsg)
         , "Customer login error not incorrect");

            BaseTest.Pass();

        }

        public void LoginFromExchange(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();

            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with valid user details : " + loginData.username, "Login should not be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Ecomm_Control.Username_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.Username_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Ecomm_Control.Pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.Pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Ecomm_Control.Pwd_ID), "value") == string.Empty)
                goto retype;


            wAction.Click(driverObj, By.XPath("//*[text()='Login']"), "Login button not found");

            wAction.WaitforPageLoad(driverObj);

            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.LinkText(Ecomm_Control.exchange_Logout)), "Customer login not successfull");

            BaseTest.Pass();

        }

        public void LoginFromEcom_Invalid(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();

            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with self ex user details : " + loginData.username, "Login should not be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Ecomm_Control.Username_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.Username_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Ecomm_Control.Pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Ecomm_Control.Pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Ecomm_Control.Pwd_ID), "value") == string.Empty)
                goto retype;

            wAction.Click(driverObj, By.Id(Ecomm_Control.LoginBtn_ID), "Submit button not found");


            wAction.WaitforPageLoad(driverObj);


            System.Threading.Thread.Sleep(2000);
            // wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox");
            // BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.invalidLoginErr_Ecom_XP)), "invalid error message not found");

            string errorMsg = baseTest.ReadxmlData("err", "Ecom_Invalid_Lgn", DataFilePath.IP2_Authetication);
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_Ecom_XP), "Invalid Login Error not found", false).
             Contains(errorMsg)
         , "Customer login error not incorrect");

            BaseTest.Pass();

        }

        public void LoginFromLottos_SecurityError(IWebDriver driverObj, Login_Data loginData, string errorMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();

            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with self ex user details : " + loginData.username, "Login should not be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Lottos_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Lottos_Control.uname_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Lottos_Control.pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Lottos_Control.pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Lottos_Control.uname_ID), "value") == string.Empty)
                goto retype;

            wAction.Click(driverObj, By.XPath(Lottos_Control.login_XP), "Submit button not found");
            wAction.WaitforPageLoad(driverObj);
            System.Threading.Thread.Sleep(2000);
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.Id(Reg_Control.invalidLoginErr_Lottos_ID), "Invalid Login Error not found", false).
                Contains(errorMsg)
            , "Customer login error not incorrect");

            BaseTest.Pass();

        }
        public void LoginFromLottos(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();

            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with self ex user details : " + loginData.username, "Login should not be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Lottos_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Lottos_Control.uname_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Lottos_Control.pwdClr_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Lottos_Control.pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Lottos_Control.uname_ID), "value") == string.Empty)
                goto retype;

            wAction.Click(driverObj, By.XPath(Lottos_Control.login_XP), "Submit button not found");


            wAction.WaitforPageLoad(driverObj);


            System.Threading.Thread.Sleep(2000);
            wAction.Click(driverObj, By.XPath(Lottos_Control.login_XP), "Login button not found");
            wAction.PageReload(driverObj);
            System.Threading.Thread.Sleep(10000);
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.LinkText(Lottos_Control.logout_lnk)), "Customer not logged in");
            //, "Customer login error not incorrect");

            BaseTest.Pass();

        }
        public void Logout_Lottos(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Logout from Lottos", "logout should be successfull");
            wAction.Click(driverObj, By.LinkText(Lottos_Control.logout_lnk), "Logout button not found", FrameGlobals.reloadTimeOut, false);
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Lottos_Control.login_XP)), "Logout not working");

            BaseTest.Pass();

        }
        public void Logout_Exchange(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Logout from Exchange", "logout should be successfull");
            wAction.Click(driverObj, By.LinkText(Ecomm_Control.exchange_Logout), "Logout button not found", FrameGlobals.reloadTimeOut, false);
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Ecomm_Control.Username_ID)), "Logout not working");

            BaseTest.Pass();

        }
        public void LoginFromLottos_Invalid(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();

            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with self ex user details : " + loginData.username, "Login should not be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Lottos_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Lottos_Control.uname_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Lottos_Control.pwdClr_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Lottos_Control.pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Lottos_Control.uname_ID), "value") == string.Empty)
                goto retype;

            wAction.Click(driverObj, By.XPath(Lottos_Control.login_XP), "Submit button not found");


            wAction.WaitforPageLoad(driverObj);


            System.Threading.Thread.Sleep(2000);
            wAction.Click(driverObj, By.XPath(Lottos_Control.login_XP), "Login button not found");
            string errorMsg = baseTest.ReadxmlData("err", "Lottos_Invalid_Lgn", DataFilePath.IP2_Authetication);


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.Id(Reg_Control.invalidLoginErr_Lottos_ID), "Invalid Login Error not found", false).
                Contains(errorMsg)
            , "Customer login error not incorrect");

            BaseTest.Pass();

        }
        public void LoginFromLottos_SelfEx(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();

            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with self ex user details : " + loginData.username, "Login should not be successfull");
            string s = loginData.fname;

        retype:
            wAction.Clear(driverObj, By.Id(Lottos_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Lottos_Control.uname_ID)).SendKeys(loginData.username);
            wAction.Clear(driverObj, By.Id(Lottos_Control.pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            driverObj.FindElement(By.Id(Lottos_Control.pwd_ID)).SendKeys(loginData.password);
            if (wAction.GetAttribute(driverObj, By.Id(Lottos_Control.uname_ID), "value") == string.Empty)
                goto retype;

            wAction.Click(driverObj, By.XPath(Lottos_Control.login_XP), "Submit button not found");


            wAction.WaitforPageLoad(driverObj);


            System.Threading.Thread.Sleep(2000);
      //      wAction.Click(driverObj, By.XPath(Lottos_Control.login_XP), "Login button not found");


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.Id(Reg_Control.invalidLoginErr_Lottos_ID), "Sports Login Error not found", false).
            Contains("Your account is currently excluded. If you believe this is incorrect, please e-mail selfexclusion@ladbrokes.com")
            , "Customer login error not incorrect");

            BaseTest.Pass();

        }
        public void quickDep_Ecom(IWebDriver driverObj, string amount)
        {

            BaseTest.AddTestCase("Add to betslip for the given bet for the event", "Selection should get added to the bet slip");
            //Select bet
            System.Threading.Thread.Sleep(4000);

            List<IWebElement> ele = wAction.ReturnWebElements(driverObj, By.XPath("//a[contains(@class,'betbutton')]"), "Selections not found", 0, false);
            ele[0].Click();

            System.Threading.Thread.Sleep(4000);

            //Enter Stake
            wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "StakeAmt_text", amount.ToString(), "StakeAmt_text Box not found", 0, false);

            //PlaceBet
            wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "PlaceBet_Btn", "PlaceBet_Button not found", 0, false);

            wAction.Click(driverObj, By.XPath(Ecomm_Control.DepositBtn_Betslip_XP), "Deposit button not found", 0, false);
            System.Threading.Thread.Sleep(7000);
            driverObj.SwitchTo().DefaultContent();
            wAction.WaitAndMovetoFrame(driverObj, "0", null, 60);

            // bool flag = wAction.IsElementPresent(driverObj, By.Id(Ecomm_Control.qDep_Cvv_ID));
            wAction.Type(driverObj, By.Id(Ecomm_Control.qDep_Cvv_ID), "123", "CVV box not found", 0, false);
            wAction.Click(driverObj, By.Id(Ecomm_Control.qDep_DepBtn_ID), "deposit button not found", 0, false);

            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Ecomm_Control.qDep_successmsg_XP)), "deposit success msg not found");
            //  wAction.Click(driverObj, By.Id(Ecomm_Control.qDep_close_ID), "close button not found");

            //PlaceBet
            //  wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "PlaceBet_Btn", "PlaceBet_Button not found", 0, false);

            //   System.Threading.Thread.Sleep(7000);
            //   BaseTest.Assert.IsTrue(wAction._IsElementPresent(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "Betplace_success"), "Placing bet failed");
            BaseTest.Pass("done");
        }
        public void quickDep_Sports(IWebDriver driverObj, string amount)
        {

            BaseTest.AddTestCase("Deposit fund using quick deposit", "quick dep should be successfull");
            driverObj.SwitchTo().DefaultContent();
            wAction.WaitAndMovetoFrame(driverObj, "PtMiniCashier", null, 60);
            wAction.Clear(driverObj, By.Id("textAmount"), "amont box not found", 0, false);
            wAction.Type(driverObj, By.Id("textAmount"), amount, "amount box not found");
            wAction.Clear(driverObj, By.Id(Ecomm_Control.qDep_Cvv_ID), "CVV box not found", 0, false);
            wAction.Type(driverObj, By.Id(Ecomm_Control.qDep_Cvv_ID), "123", "CVV box not found");
            wAction.Click(driverObj, By.Id(Ecomm_Control.qDep_DepBtn_ID), "deposit button not found");
            // wAction.Click(driverObj, By.XPath("//span[@class='close']"));

            // BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Ecomm_Control.qDep_successmsg_XP)), "deposit success msg not found");
            driverObj.SwitchTo().DefaultContent();

            BaseTest.Pass("done");
        }

        public void LoginFromBackgammon(IWebDriver driverObj, Login_Data loginData)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login to backgammon with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

            ISelenium bro = new WebDriverBackedSelenium(driverObj, "http://google.com");
            bro.Start();

        retype:
            wAction.Clear(bro, By.Id(Games_Control.uname_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
        wAction.Type(bro, By.Id(Games_Control.uname_ID), loginData.username);
        wAction.Clear(bro, By.Id(Games_Control.pwd_ID), "Password text box not found", 0, false);
        wAction.Type(bro, By.Id(Games_Control.pwd_ID), loginData.password, "Password field not found/not editable");

        if (wAction.GetAttribute(driverObj, By.Id(Games_Control.pwd_ID), "value") == string.Empty)
                goto retype;
        wAction.Click(driverObj, By.XPath(Lottos_Control.login_XP), "Login button not found");
            

            System.Threading.Thread.Sleep(5000);


            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Games_Control.depositLink)), "BackGammon not logged in");

            BaseTest.Pass();

        }
        public void Logout_Backgammon(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Logout from Backgammon", "logout should be successfull");
            wAction.Click(driverObj, By.XPath("id('fmLogin')//section[@class='user']"), "User menu not found");
            wAction.Click(driverObj, By.LinkText("Logout"), "Logout button not found", FrameGlobals.reloadTimeOut, false);
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("tbUsername")), "Logout did not happaned");

            BaseTest.Pass();

        }



        //=======================================================================================
        //    Single Wallet
        //=====================================================================================



        public void Verify_SingleWallet_Cashier(IWebDriver driverObj, string ListOfWallets, string SingleWallet, double Balance)
        {

            BaseTest.AddTestCase("Verify Cashier for Single Wallet", "Single wallet should be displayed in all places");
            String portalWindow = AnW.OpenCashier(driverObj);

            //driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
            wAction.WaitforPageLoad(driverObj);
            string SingleWalletPath = "//tr[td[contains(text(),'" + SingleWallet + "')]]/td[2]";
          

            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));

            if(wAction.IsElementPresent(driverObj, By.XPath("id('sourceWallet')/option")))
            {
            List<IWebElement> depDropDown = wAction.ReturnWebElements(driverObj, By.XPath("id('sourceWallet')/option"));
            foreach (IWebElement options in depDropDown)
                if (ListOfWallets.Contains(options.Text.Trim()))
                    continue;
                else
                    BaseTest.Fail("Invalid wallet name found:" + options.Text.Trim());
            }


            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            List<IWebElement> listOfWall = wAction.ReturnWebElements(driverObj, By.XPath("//tr/td[1]"), "Wallet balance sheet not loaded");
            foreach (IWebElement options in listOfWall)
                BaseTest.Assert.IsTrue(ListOfWallets.Contains(options.Text.Trim()), "Invalid wallet name found:" + options.Text.Trim());


            string temp = wAction.GetText(driverObj, By.XPath(SingleWalletPath), SingleWallet + " Wallet value not found", false);
            double val = 0;
            if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                val = StringCommonMethods.ReadDoublefromString(temp);

            BaseTest.Assert.IsTrue((val == Balance), "Single wallet balance not matching , Expected:" + Balance + " ;Actual:" + val);



            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
           listOfWall = wAction.ReturnWebElements(driverObj, By.XPath("//tr/td[1]"), "Wallet drop down not loaded in deposit page");
          
            foreach (IWebElement options in listOfWall)
                BaseTest.Assert.IsTrue(ListOfWallets.Contains(options.Text.Trim()), "Invalid wallet name found:" + options.Text.Trim());

            temp = wAction.GetText(driverObj, By.XPath(SingleWalletPath), SingleWallet + " Wallet value not found", false);
            val = 0;
            if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                val = StringCommonMethods.ReadDoublefromString(temp);

            BaseTest.Assert.IsTrue((val == Balance), "Single wallet balance not matching , Expected:" + Balance + " ;Actual:" + val);

            driverObj.Close();
            driverObj.SwitchTo().Window(portalWindow);
            BaseTest.Pass();

        }
        public void Verify_SingleWallet_MyAcct(IWebDriver driverObj, string ListOfWallets, string SingleWallet, double Balance)
        {

            BaseTest.AddTestCase("Verify My Acct for Single Wallet", "Single wallet should be displayed in all places");
            string portal = AnW.OpenMyAcct(driverObj);
            string SingleWalletPath = "//tr[td[div[contains(text(),'" + SingleWallet + "')]]]/td[2]";


            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction.Click(driverObj, By.XPath(MyAcctPage.MyAcct_History_XP), "Account History link not found", 0, false);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
          
             System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
             List<IWebElement> listOfWall = wAction.ReturnWebElements(driverObj, By.XPath("id('account-history-table')//tr/td[1]"), "History table loaded in my acct page");

            foreach (IWebElement options in listOfWall)
                BaseTest.Assert.IsTrue(ListOfWallets.Contains(options.Text.Trim()), "Invalid wallet name found:" + options.Text.Trim());

            string temp = wAction.GetText(driverObj, By.XPath(SingleWalletPath), SingleWallet + " Wallet value not found", false);
            double val = 0;
            if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                val = StringCommonMethods.ReadDoublefromString(temp);

            BaseTest.Assert.IsTrue((val == Balance), "Single wallet balance not matching , Expected:" + Balance + " ;Actual:" + val);

            driverObj.Close();
            driverObj.SwitchTo().Window(portal);
            BaseTest.Pass();

        }

        public void sessionManagement_Logout(IWebDriver driverObj, string sites)
        {
            wAction.ExecJavaScript(driverObj, "window.open('','_blank');");
            string main = driverObj.CurrentWindowHandle;
            driverObj.SwitchTo().Window(driverObj.WindowHandles[1]);
            if (sites.Contains("S"))
            {
                BaseTest.AddTestCase("Verify session for sports", "User should be logged in");
               // wAction.ExecJavaScript(driverObj, "window.open('" + FrameGlobals.SportsURL + "','_blank');");
                wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Sports load failed", 60);              
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Sportsbook_Control.username_Id)), "Customer still not logged out!");
                BaseTest.Pass();
            }
            if (sites.Contains("P"))
            {
                BaseTest.AddTestCase("Verify session for Vegas", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Vegas load failed", 60);
                Thread.Sleep(3000);
                
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name(Portal_Control.username_name)), "Customer might not have logged off successfully please check");
                BaseTest.Pass();

                BaseTest.AddTestCase("Verify session for Casino", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Casino load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name(Portal_Control.username_name)), "Customer might not have logged off successfully please check");
                BaseTest.Pass();
                BaseTest.AddTestCase("Verify session for Bingo", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Bingo load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name(Portal_Control.username_name)), "Customer might not have logged off successfully please check");
                BaseTest.Pass();
            }
            if (sites.Contains("G"))
            {

                BaseTest.AddTestCase("Verify session for Games", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "Ecomm load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("tbUsername")), "Logout not working");
                BaseTest.Pass();
            }
            if (sites.Contains("B"))
            {

                BaseTest.AddTestCase("Verify session for backgammon", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.BackgammonURL, "Backgammon load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("tbUsername")), "Logout did not happaned");
                BaseTest.Pass();
            }
            if (sites.Contains("X"))
            {

                BaseTest.AddTestCase("Verify session for Exchange", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.ExchangeURL, "ExchangeURL load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Ecomm_Control.Username_ID)), "Logout not working");
                BaseTest.Pass();
            }
            if (sites.Contains("E"))
            {

                BaseTest.AddTestCase("Verify session for ecom", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "Ecomm load failed", 60);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("loginusername")), "Customer might not have logged off successfully please check");
                BaseTest.Pass();
            }
            driverObj.Close();
            driverObj.SwitchTo().Window(main);
        }
        public void sessionManagement_Login(IWebDriver driverObj, string sites)
        {
            wAction.ExecJavaScript(driverObj, "window.open('','_blank');");
            string main = driverObj.CurrentWindowHandle;
            driverObj.SwitchTo().Window(driverObj.WindowHandles[1]);
            if (sites.Contains("S"))
            {
                BaseTest.AddTestCase("Verify session for sports", "User should be logged in");
                // wAction.ExecJavaScript(driverObj, "window.open('" + FrameGlobals.SportsURL + "','_blank');");
                wAction.OpenURL(driverObj, FrameGlobals.SportsURL, "Sports load failed", 60);
                Thread.Sleep(3000);
                wAction.Click(driverObj, By.XPath(Sportsbook_Control.LoggedinMenu_XP), "Sports user menu not found", 0, false);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.Logout_XP)), "Sports not logged in");
                BaseTest.Pass();
            }
            if (sites.Contains("P"))
            {
                BaseTest.AddTestCase("Verify session for Vegas", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.VegasURL, "Vegas load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)), "Vegas not logged in");
                BaseTest.Pass();

                BaseTest.AddTestCase("Verify session for Casino", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Casino load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)), "Casino not logged in");
                BaseTest.Pass();
                BaseTest.AddTestCase("Verify session for Bingo", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Bingo load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)), "Bingo not logged in");
                BaseTest.Pass();
            }
            if (sites.Contains("G"))
            {

                BaseTest.AddTestCase("Verify session for Games", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.GamesURL, "Ecomm load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Games_Control.depositLink)), "Games Login did not happen");
                
                BaseTest.Pass();
            }
            if (sites.Contains("B"))
            {

                BaseTest.AddTestCase("Verify session for backgammon", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.BackgammonURL, "Backgammon load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Games_Control.depositLink)), "BackGammon not logged in");
                BaseTest.Pass();
            }
           
            driverObj.Close();
            driverObj.SwitchTo().Window(main);
        }

    }
}