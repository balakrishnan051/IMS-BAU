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

namespace Ladbrokes_IMS_TestRepository
{
    public class AccountAndWallets
    {
        WebCommonMethods commonWebMethods = new WebCommonMethods();
        Common RepositoryCommon = new Common();
        wActions wAction = new wActions();
        BaseTest baseTest = new BaseTest();
        /// <summary>
        /// Author:Nagamanickam
        /// Login from mobile
        /// </summary>
        /// <param name="driverObj">webdriver instance</param>
        /// /// <param name="loginData">login details</param>
        public void LoginFromMobile(IWebDriver driverObj, Login_Data loginData)
        {
            try
            {
                BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
                BaseTest.Pass();
                driverObj.Manage().Window.Maximize();
                BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
                commonWebMethods.Click(driverObj, By.Id("btn_menu"), "Menu not found");
                // commonWebMethods.Click(driverObj, By.XPath(Login_Control.loginMenuLink_Xpath), "Login menu not found");
                System.Threading.Thread.Sleep(5000);
                commonWebMethods.Click(driverObj, By.Id("login"), "Login menu not found");

            retype:
                commonWebMethods.Type(driverObj, By.Id("username"), loginData.username, "Username Txt box not found,{Error In :loginData.username}", 0, false);
                commonWebMethods.Type(driverObj, By.Id("password"), loginData.password, "Password Txt box not found,{Error In :loginData.password}", 0, false);
                if (commonWebMethods.GetAttribute(driverObj, By.Id("username"), "value") == string.Empty)
                    goto retype;

                commonWebMethods.Click(driverObj, By.Id("login_btn"), "Login Button not found", 0, false);
                commonWebMethods.WaitUntilElementDisappears(driverObj, By.XPath(Login_Control.loginSpin_Xpath));
                BaseTest.Assert.IsTrue(commonWebMethods.IsElementPresent(driverObj, By.Id("btn_logout")), "Customer not logged in");
                commonWebMethods.WaitforPageLoad(driverObj);
                BaseTest.Pass();
            }
            catch (Exception e) { BaseTest.Assert.Fail("Customer Login Failed : " + e.Message.ToString()); }

        }

        public void MyAccount_EditPostCode(IWebDriver driverObj, string postcode, string address)
        {
            ISelenium browser = new WebDriverBackedSelenium(driverObj, "https://www.google.co.in/");
            browser.Start();
            BaseTest.AddTestCase("Edit postcode in my accounts page", "Address should be generated successfully for the given postal code");
            commonWebMethods.Clear(driverObj, By.Id("postCode"), "Postal code text box not found", 0, false);
            commonWebMethods.Type(driverObj, By.Id("postCode"), postcode, "Post code text box not found", 0, false);
            // commonWebMethods.Click(driverObj, By.Id("fillFields"), "Find address Button not found", 0, false);
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.GetAttribute(driverObj, By.Id("postCode"), "value").ToString().Contains(postcode), "Postal code is not entered");
            BaseTest.Pass();
        }
        public void MyAccount_ValidateDiabledFields(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Verify Title field in My Account->Personal Details page is non-editable", "Title field in My Account->Personal Details page should be non-editable");
            wAction.Click(driverObj, By.Id(MyAcctPage.myAcct_tab_Id), "Personal Details link not found", FrameGlobals.reloadTimeOut, false);
            BaseTest.Assert.IsFalse(wAction.IsElementEnabled(driverObj, By.Id("title")), "Title is enabled/Editable");
            BaseTest.Pass();

            BaseTest.AddTestCase("Verify First name field in My Account->Personal Details page is non-editable", "First name field in My Account->Personal Details page should be non-editable");
            BaseTest.Assert.IsFalse(wAction.IsElementEnabled(driverObj, By.Id("firstName")), "Fname is enabled/Editable");
            BaseTest.Pass();

            BaseTest.AddTestCase("Verify SurName field in My Account->Personal Details page is non-editable", "SurName field in My Account->Personal Details page should be non-editable");
            BaseTest.Assert.IsFalse(wAction.IsElementEnabled(driverObj, By.Id("lastName")), "Lname is enabled/Editable");
            BaseTest.Pass();



        }

        /// <summary>
        /// Author:Nagamanickam
        /// Login from portal
        /// </summary>
        /// <param name="driverObj">webdriver instance</param>
        /// /// <param name="loginData">login details</param>
        public void LoginFromPortal(IWebDriver driverObj, Login_Data loginData, ISelenium mybro)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
            string s = loginData.fname;

        retype:
            commonWebMethods.Clear(driverObj, By.Name("username"), "Username text box not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(mybro, By.Name("username"), loginData.username);

            //driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            commonWebMethods.Clear(driverObj, By.Name("password"), "Password text box not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(mybro, By.Name("password"), loginData.password);

            //driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            //  wAction.Type(mybro, By.XPath(Login_Control.loginBtn_Xpath),Keys.Enter, "Login Button not found");    

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();
            commonWebMethods.WaitforPageLoad(driverObj);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

        clickAgain:

            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox_PT");
            wAction.WaitforPageLoad(driverObj);
            if (wAction._IsElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox_PT"))
                goto clickAgain;


            wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Xpath));
            System.Threading.Thread.Sleep(2000);
            
            clickAgain2:
            wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath));
            wAction.WaitforPageLoad(driverObj);
            if (wAction.IsElementPresent(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath)))
                goto clickAgain2;

            

            wAction.WaitforPageLoad(driverObj);
            /*if (!commonWebMethods.IsElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)))
                commonWebMethods.PageReload(driverObj);
            */
            BaseTest.Assert.IsTrue(commonWebMethods.WaitUntilElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)), "Customer unable to login");
            //BaseTest.Assert.IsTrue(commonWebMethods.GetText(driverObj, By.XPath(Login_Control.userDisplay_Xpath), "User login failed,{Error In: Login_Control.userDisplay_Xpath}").ToString().Contains(loginData.fname), "Customer login failed");
            commonWebMethods.WaitforPageLoad(driverObj);
            BaseTest.Pass();

        }



        #region ToBeDeleted
        ///// <summary>
        ///// Author:Nagamanickam
        ///// Transfer fund between wallets
        ///// </summary>
        ///// <param name="driverObj">broswer</param>
        ///// <param name="acctData">acct details</param>
        //public void DepositTOWallet(IWebDriver driverObj, MyAcct_Data acctData)
        //{

        //    BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");
        //   // commonWebMethods.OpenURL(driverObj, FrameGlobals.LadbrokesIMSdirect, "My Account page not loaded", FrameGlobals.reloadTimeOut + 20);

        //    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

        //    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "myAcct_lnk","My Account Link not found", FrameGlobals.reloadTimeOut, false);
        //    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());

        //    double beforeVal = RepositoryCommon.CommonDeposit(driverObj, acctData, acctData.depositAmt);
        //    if (!commonWebMethods.GetText(driverObj, By.Id(MyAcct_Control.depsoitSuccessMsg_ID), "Confirmation message not found", false).Contains("Deposit successful"))
        //    {
        //        driverObj.Close();
        //        BaseTest.Fail("Success Message not found, Actual: " + commonWebMethods.GetText(driverObj, By.Id(MyAcct_Control.depsoitSuccessMsg_ID), "Confirmation message not found", false));
        //    }
        //    else
        //    {
        //        try
        //        {

        //            RepositoryCommon.Validate_Deposit_wallet(driverObj, acctData.depositWallet, beforeVal, double.Parse(acctData.depositAmt));
        //            BaseTest.Pass();
        //        }
        //        catch (Exception e) { BaseTest.Assert.Fail("Deposit value mismached (or) Wallet value has some invalid value : " + e.Message.ToString()); }
        //        finally
        //        {

        //            commonWebMethods.BrowserClose(driverObj);
        //            driverObj.SwitchTo().Window(portalWindow);
        //        }
        //    }

        //}
        #endregion



        public void DepositTOWallet_Netteller(IWebDriver driverObj, MyAcct_Data acctData, string netAcc = null, string promoCode = null)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(CommonDeposit_Netteller_PT(driverObj, acctData, acctData.depositAmt, portalWindow, netAcc, promoCode), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.Close();
            driverObj.SwitchTo().Window(portalWindow);


        }
        public void CheckDepLimit_Netteller(IWebDriver driverObj, MyAcct_Data acctData)
        {
            try
            {
                BaseTest.AddTestCase("Verify Netteller deposit", "Amount should be deposited successfully");
                //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                string depLimitTxt = wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "depLimit_txt", "Deposit limit text not found", false);
                string[] upperLimit = depLimitTxt.Split('-');
                string temp = upperLimit[1];


                string bval = temp.Replace(",", string.Empty).Trim();
                Regex re = new Regex(@"\d+");
                Match b4Value = re.Match(bval);
                double beforeVal = double.Parse(b4Value.Value);

                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
                wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                bool flag = wAction._WaitUntilElementPresent(driverObj, ORFile.Poker_Banking, wActions.locatorType.xpath, "DepsoitSuccess");

                if (!flag)
                    BaseTest.Fail("No Success message found");

                BaseTest.Pass();

                driverObj.Navigate().Refresh();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                BaseTest.AddTestCase("Verify deposit limit is reduced after successful deposit", "Deposit limit should be updated based on the amount deposited");
                //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                depLimitTxt = wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "depLimit_txt", "Deposit limit text not found", false);
                upperLimit = depLimitTxt.Split('-');
                temp = upperLimit[1];
                string aval = temp.Replace(",", string.Empty).Trim();
                re = new Regex(@"\d+");
                Match afterValue = re.Match(aval);

                double afterVal = double.Parse(afterValue.Value);


                BaseTest.AddTestCase(" Transaction Amount:" + acctData.depositAmt + " => Deposit limit Before deposit:" + beforeVal + ", Deposit limit after deposit:" + afterVal, "Deposit limit verification has failed");
                BaseTest.Pass();

                //driverObj.SwitchTo().DefaultContent();

                if (afterVal == beforeVal - double.Parse(acctData.depositAmt))
                    BaseTest.Pass();
                else
                    BaseTest.Fail("Deposit limit not updated after successful deposit");
            }
            catch (Exception e) { BaseTest.Assert.Fail("Verifying deposit limit failed : " + e.Message.ToString()); }

        }


        public double GetWalletBalance_Withdrawable(IWebDriver driverObj, string wallet)
        {


            string depositWalletPath = "//tr[td[contains(text(),'" + wallet + "')]]/td[3]";
            BaseTest.AddTestCase("Get Balance of wallet: " + wallet, "Wallet Amount should be present");

            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();


            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Deposit_Coin_lnk", "Deposit Coin Link not found", FrameGlobals.reloadTimeOut, false);
            wAction.WaitAndMovetoPopUPWindow(driverObj, "Cashier - Pop Up window not found");

            // commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), wallet + "Wallet value not found", false).ToString().Trim());

            string bval = temp.Replace(",", string.Empty).Trim();
            Regex re = new Regex(@"\d+");
            Match b4Value = re.Match(bval);

            double FinalVal = double.Parse(b4Value.Value);
            driverObj.Close();
            driverObj.SwitchTo().Window(portalWindow);

            BaseTest.Pass();
            return FinalVal;


        }
        public bool CommonDeposit_Netteller_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window = null, string netAcc = null, string promoCode = null)
        {



            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            //   string depositWalletPath = "id('ssec_ftbal')/td/table/tbody/tr/td/table/tbody/tr[contains(string(),'" + acctData.depositWallet + "')]/td[2]";


            //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), "Cashier Frame not found", FrameGlobals.elementTimeOut);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            double beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

            BaseTest.AddTestCase("Verify that Payment method is added to the customer", "The customer should have the payment option added to it");
            String nettellerImg = "//td[contains(text(),'" + netAcc + "')]";
            wAction.Click(driverObj, By.XPath(nettellerImg), "Netteller Image not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
            if (promoCode != null)
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "promoCode", promoCode, "promoCode not found");

            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
          BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Poker_Banking, wActions.locatorType.xpath, "DepsoitSuccess"),"Deposit success msg did not appear");
            wAction.Click(driverObj, By.XPath(CashierPage.Close_DepSuccessPrompt_XP));

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());

            BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
            BaseTest.Pass();
            // Console.WriteLine("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal);

            //wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
            driverObj.SwitchTo().DefaultContent();

            if (promoCode != null)
                beforeVal = beforeVal + Convert.ToDouble(acctData.depositAmt);

            if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                return true;
            else
                return false;

            return true;
        }

        public bool Verify_FirstCashier_Neteller(IWebDriver driverObj, string neteller_id, string neteller_pwd, string depositWallet, string bonusWallet, string amount, bool multiple_paymethod = false, bool autoBonus = false)
        {
            //   wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "neteller_payment_tab", "Neteller paymyemt method is not found", 10, false);

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
            commonWebMethods.Click(driverObj, By.XPath(Login_Control.modelWindow_OK_Xpath), null, 0, false);
            commonWebMethods.WaitforPageLoad(driverObj);
            if (wAction.IsElementPresent(driverObj, By.XPath(Login_Control.modelWindow_Generic_Xpath)))
                goto clickAgain;

            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 5);
            BaseTest.AddTestCase("Verify Neteller payment method registration and first deposit bonus", "Neteller registration should be successful");
            //wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", 8);

            string bonusWalletPath = "//tr[td[contains(text(),'" + bonusWallet.ToLower().Substring(0, 1).ToUpper() + bonusWallet.ToLower().Substring(1) + "')]]/td[2]";

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw tab is not found", FrameGlobals.reloadTimeOut, false);


            double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(bonusWalletPath), bonusWallet + " Wallet value not found", false).ToString().Replace('£', ' ').Trim());

            if (autoBonus)
                AfterVal = AfterVal / 2;

            if (AfterVal == Convert.ToDouble(amount))
                return true;
            else
                return false;

        }

        public bool Verify_FirstCashier_MasterCard(IWebDriver driverObj, MyAcct_Data acctData)
        {
            //   wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
            acctData.cardCSC = "123";
            string typeImag = "Master_tab";
            wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));
            
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, typeImag, typeImag + " Not found in the deposit page", 0, false);
            System.Threading.Thread.Sleep(3000);
          wAction.WaitAndMovetoFrame(driverObj, Cashier_Control_SW.redirectIframe);
            System.Threading.Thread.Sleep(3000);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "credit_number", acctData.card, "Credit card number field is not found/Not loaded", 0, false);

            wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "expiry_month", "4", "Expiry month select box is not found", 0, false);

            wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "expiry_year", "4", "Expiry Year select box is not found", 0, false);

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "continue_btn", "Continue button is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(3000);

            driverObj.SwitchTo().DefaultContent();
            if (driverObj.Url.Contains("telebet"))
                wAction.WaitAndMovetoFrame(driverObj, By.Id("acctIframe"));
            else
            {
                wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "MasterCard_img", "MasterCard_img not found");
            }
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found/Card is already registered to another account :"+ acctData.card);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "creditcard_cvv", "CVV_txt not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "creditcard_cvv", acctData.cardCSC, "CVV_txt not found");
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));


            driverObj.SwitchTo().DefaultContent();
            wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
            if (driverObj.Url.Contains("telebet"))
                wAction.WaitAndMovetoFrame(driverObj, By.Id("acctIframe"));
            else
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

        public void VerifyCashier(IWebDriver driverObj)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);

            string value = wAction._GetAttribute(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", "value", "sourceWallet_cmb not found");

            driverObj.Close();
        }

        public void DepositTOWallet_CCSports(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");

            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            BaseTest.AddTestCase("Verify Deposit link is available and clicking on Deposit link takes to the new Banking window", "The deposit page should be present and opens a new window");
            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Sports_banking_lnk", "Banking Link not found", FrameGlobals.reloadTimeOut, false);

            driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
            BaseTest.Pass();

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonDeposit_CreditCard_PT(driverObj, acctData, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void DepositTOWallet_CC(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonDeposit_CreditCard_PT(driverObj, acctData, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void DepositTOWallet_CC_Exchange(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonDeposit_CreditCard_PT(driverObj, acctData, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void DepositTOWalletMaster_GamesPage(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);
            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonDeposit_CreditCard_PT(driverObj, acctData, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void Validate_RegionCode_RegitraionPage(IWebDriver driverObj, string countryName, string countryCode, int windowIndex = 0)
        {
            try
            {
                BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
                String portalWindow;
                if (windowIndex != 0)
                {
                    portalWindow = driverObj.WindowHandles.ToArray()[windowIndex - 1].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[windowIndex].ToString(), "Unable to locate registration page");
                }
                else
                {
                    portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                }
                BaseTest.Pass("Registration page opened");

                BaseTest.AddTestCase("Verify region code for Telephone and Mobile according to the country selected", "Region code for Telephone and Mobile verified Successfully");
                BaseTest.Assert.IsTrue(RepositoryCommon.RegionCode_RegPage(driverObj, countryName, countryCode), "Region code is not changing based on country");

            }
            catch (Exception e) { BaseTest.Assert.Fail("Region code is not changing based on country : " + e.Message.ToString()); }
        }


        public void Register_Paypal_Sports(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to register Paypal account", "Amount should be deposited to the selected wallet");

            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            BaseTest.AddTestCase("Verify Deposit link is available and clicking on Deposit link takes to the new Banking window", "The deposit page should be present and opens a new window");

            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Sports_banking_lnk", "Banking Link not found", FrameGlobals.reloadTimeOut, false);

            wAction.WaitAndMovetoPopUPWindow(driverObj, "Pop Up window not found");
            BaseTest.Pass();

            Framework.BaseTest.Assert.IsTrue(Add_Paypal(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }
        public void Register_Paypal(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to register Paypal account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(Add_Paypal_BalanceValid(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void Register_Sofort(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to register sofort account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(Add_Sofort_BalanceValid(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void Register_Giropay(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to register Giropay account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(Add_Giropay_BalanceValid(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }
        public void Register_Ideal(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to register Ideal account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(Add_iDeal_BalanceValid(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void VerifyRegister_Paypal(IWebDriver driverObj, MyAcct_Data acctData, bool multiple_paymethod = false)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to register Paypal account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(Add_Paypal(driverObj, acctData, acctData.depositAmt, portalWindow, true), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void VerifyRegister_Skrill(IWebDriver driverObj, MyAcct_Data acctData, bool multiple_paymethod = false)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to register Skrill account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(Add_Skrill(driverObj, acctData, acctData.depositAmt, portalWindow, false), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void VerifyRegister_eNets(IWebDriver driverObj, MyAcct_Data acctData, bool multiple_paymethod = false)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to register eNets account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(Add_Skrill(driverObj, acctData, acctData.depositAmt, portalWindow, false, true), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public bool Add_Skrill(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window, bool multiple_paymethod = false, bool eNets = false)
        {
            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


            ////commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());

            double beforeVal = StringCommonMethods.ReadDoublefromString(temp);
            


            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

            if (multiple_paymethod == true)
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "add_new_paymethod", "Add new paymyemt method option is not found", FrameGlobals.reloadTimeOut, false);


            if (eNets)
                wAction.Click(driverObj, By.XPath("//li[contains(@data-type,'SkrillENT')]"), "eNets payment method not found in the list");
            else
                wAction.Click(driverObj, By.XPath(CashierPage.Skrill_tab_Xp), "Skrill payment method not found in the list");



            BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            
            wAction.WaitAndMovetoFrame(driverObj, By.Name(Cashier_Control_SW.redirectIframe));

            wAction.Click(driverObj, By.LinkText("I already have a Skrill account"), "I already have a Skrill account link not found", 0, false);

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_email_text", "skrill_email_text not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_email_text", acctData.skrillUser, "paypal_email_text not found");

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_pwd_text", "skrill_pwd_text not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_pwd_text", acctData.skrillPwd, "paypal_pwd_text not found");

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_submit_btn", "skrill_submit_btn not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            if (wAction.IsElementPresent(driverObj, By.Id("redirectContinue")))
                wAction.Click(driverObj, By.Id("redirectContinue"));
            else
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_paynow_btn", "skrill_paynow_btn not found", 0, false);
            
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(CashierPage.Skrill_success_Xp)), "Success msg not found");
            
            driverObj.SwitchTo().DefaultContent();
            wAction.PageReload(driverObj);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath(CashierPage.Skrill_img_Xp)),"Skrill not added");
            BaseTest.Pass();

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
            double AfterVal = StringCommonMethods.ReadDoublefromString(temp);

            BaseTest.Assert.IsTrue(AfterVal == beforeVal + double.Parse(amount), "Skrill amount not added");


            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            driverObj.Close();
            return true;
        }

        public bool Deposit_Skrill(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window)
        {

            wAction.WaitforPageLoad(driverObj);

            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


            // commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());

            string bval = temp.Replace(",", string.Empty).Trim();
            Regex re = new Regex(@"\d+");
            Match b4Value = re.Match(bval);

            double beforeVal = double.Parse(b4Value.Value);


            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

           // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Skrill_img", "Skrill payment method not /Not Registered");
            //wAction.Click(driverObj, By.XPath("//li[@data-type='PayPal']"), "Paypal payment method not found in the list");

            BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");

            String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            wAction.WaitAndMovetoFrame(driverObj, By.Name(Cashier_Control_SW.redirectIframe));

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_email_text", "skrill_email_text not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_email_text", acctData.skrillUser, "paypal_email_text not found");

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_pwd_text", "skrill_pwd_text not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_pwd_text", acctData.skrillPwd, "paypal_pwd_text not found");

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_submit_btn", "skrill_submit_btn not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "skrill_paynow_btn", "skrill_paynow_btn not found", 0, false);            
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(CashierPage.Skrill_success_Xp)), "Success msg not found");            
            driverObj.SwitchTo().DefaultContent();
            
            wAction.PageReload(driverObj);

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));


            BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath(CashierPage.Skrill_img_Xp)), "Skrill not added");
            BaseTest.Pass();

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
            double AfterVal = StringCommonMethods.ReadDoublefromString(temp);

            BaseTest.Assert.IsTrue(AfterVal == beforeVal + double.Parse(amount), "Skrill amount not added");


            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            driverObj.Close();
            return true;
        }

        /// <summary>
        /// Author:Anand C
        /// Withdraw  fund into wallets - Skrill
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void Withdraw_Skrill(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonWithdraw_Skrill_PT(driverObj, acctData, acctData.depositAmt), "Amount not deducted after withdraw");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);


        }

        public void Deposit_Cust_Skrill(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet using Skrill", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(Deposit_Skrill(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void VerifyRegister_Euteller(IWebDriver driverObj, MyAcct_Data acctData, bool multiple_paymethod = false)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to register Euteller account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(Add_Euteller(driverObj, acctData, acctData.depositAmt, portalWindow, "1234"), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public bool Add_Euteller(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window, string password, bool multiple_paymethod = false)
        {
            try
            {

                string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
                DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


                //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());

                string bval = temp.Replace(",", string.Empty).Trim();
                Regex re = new Regex(@"\d+");
                Match b4Value = re.Match(bval);

                double beforeVal = double.Parse(b4Value.Value);


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                if (multiple_paymethod == true)
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "add_new_paymethod", "Add new paymyemt method option is not found", FrameGlobals.reloadTimeOut, false);

                wAction.Click(driverObj, By.XPath("//li[contains(@data-type,'EUTELLER')]"), "Euteller payment method not found in the list");

                BaseTest.AddTestCase("Verify that Registration of Euteller is successful to the customer", "The customer should have the Euteller payment option added to it");

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

                wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");

                String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[2].ToString());
                wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Euteller registration window not found", FrameGlobals.elementTimeOut);

                wAction.Click(driverObj, By.XPath("//input[@value='Proceed']"), "Proceed button not found in third party page for Euteller");

                wAction.Click(driverObj, By.XPath("//td[contains(.,'Nordea')]"), "Nordea link is not found in third party page for Euteller");

                if (verifyAlert(driverObj))
                {
                    wAction.Click(driverObj, By.Id("maturityCheck"), "Nordea link is not found in third party page for Euteller");
                    wAction.Click(driverObj, By.XPath("//td[contains(.,'Nordea')]"), "Nordea link is not found in third party page for Euteller");
                }

                wAction.Click(driverObj, By.XPath("//input[@name='Ok']"), "OK button is not found in third party page for Euteller");

                wAction.Type(driverObj, By.XPath("//input[@type='password']"), password, "Unable to find the password filed in third party page for Euteller");

                wAction.Click(driverObj, By.XPath("//input[@value='Vahvista']"), "Unable to find the Vahvista filed in third party page for Euteller");

                wAction.Click(driverObj, By.XPath("//input[@value='Palaa myyjän palveluun']"), "Unable to find the Vahvista filed in third party page for Euteller");

                wAction.Click(driverObj, By.XPath("//a[contains(.,'Close window')]"), "Unable to find the Close window link in third party page for Euteller");

                BaseTest.Pass();

                driverObj.SwitchTo().Window(depositWindow);
                //  wAction.PageReload(driverObj);
                //  wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                //wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction.WaitforPageLoad(driverObj);


                //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Euteller_img"))
                    return true;
                else
                    return false;
            }
            catch (Exception e) { BaseTest.Fail("Registration Failed:" + e.Message.ToString()); }
            finally
            {
                driverObj.Close();
            }
            return true;
        }

        public void Deposit_Cust_Euteller(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet using Euteller", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);


            Framework.BaseTest.Assert.IsTrue(Deposit_Euteller(driverObj, acctData, acctData.depositAmt, "1234", portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public bool Deposit_Euteller(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window, string password, bool multiple_paymethod = false)
        {
           
                wAction.WaitforPageLoad(driverObj);

                string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
                DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


                //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());

                string bval = temp.Replace(",", string.Empty).Trim();
                Regex re = new Regex(@"\d+");
                Match b4Value = re.Match(bval);

                double beforeVal = double.Parse(b4Value.Value);


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                BaseTest.AddTestCase("Verify that Deposit of money from Euteller payment method is successful to the customer", "The customer should be able to deposit the money from EUteller");

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

                wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");

                String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[2].ToString());
                wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Euteller registration window not found", FrameGlobals.elementTimeOut);

                wAction.Click(driverObj, By.XPath("//input[@value='Proceed']"), "Proceed button not found in third party page for Euteller", 0, false);

                wAction.Click(driverObj, By.XPath("//td[contains(.,'Nordea')]"), "Nordea link is not found in third party page for Euteller", 0, false);

                if (verifyAlert(driverObj))
                {
                    wAction.Click(driverObj, By.Id("maturityCheck"), "Nordea link is not found in third party page for Euteller");
                    wAction.Click(driverObj, By.XPath("//td[contains(.,'Nordea')]"), "Nordea link is not found in third party page for Euteller");
                }
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction.Click(driverObj, By.XPath("//input[@name='Ok']"), "OK button is not found in third party page for Euteller");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction.Type(driverObj, By.XPath("//input[@type='password']"), password, "Unable to find the password filed in third party page for Euteller");

                wAction.Click(driverObj, By.XPath("//input[@value='Vahvista']"), "Unable to find the Vahvista filed in third party page for Euteller");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction.Click(driverObj, By.XPath("//input[@value='Palaa myyjän palveluun']"), "Unable to find the Vahvista filed in third party page for Euteller");

                wAction.Click(driverObj, By.XPath("//a[contains(.,'Close window')]"), "Unable to find the Close window link in third party page for Euteller");

                BaseTest.Pass();

                driverObj.SwitchTo().Window(depositWindow);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('€', ' ').Trim());

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                if (!(beforeVal + 5 == AfterVal))
                {
                    BaseTest.Fail("Deposit amount is not correct");
                }
                BaseTest.Pass();
                driverObj.SwitchTo().DefaultContent();
                return true;

      
        }

        public bool verifyAlert(IWebDriver driverObj)
        {
            try
            {
                driverObj.SwitchTo().Alert().Accept();
                return true;
            }
            catch (NoAlertPresentException ex)
            {
                return false;
            }

        }

        public void Register_Paypal_Ecom(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to register Paypal account", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(Add_Paypal(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void Deposit_Cust_Paypal(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet using Paypal", "Amount should be deposited to the selected wallet");

            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(Deposit_Paypal(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public bool Add_Paypal(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window, bool multiple_paymethod = false)
        {
           

                string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
                DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


                //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());

                string bval = temp.Replace(",", string.Empty).Trim();
                Regex re = new Regex(@"\d+");
                Match b4Value = re.Match(bval);

                double beforeVal = double.Parse(b4Value.Value);


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                if (multiple_paymethod == true)
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "add_new_paymethod", "Add new paymyemt method option is not found", FrameGlobals.reloadTimeOut, false);

                wAction.Click(driverObj, By.XPath("//li[@data-type='PayPal']"), "Paypal payment method not found in the list");

                BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

                wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");

                String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[2].ToString());
                wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Paypal registration window not found", FrameGlobals.elementTimeOut);

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_email_text", "paypal_email_text not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_email_text", acctData.paypalUser, "paypal_email_text not found");

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_pwd_text", "paypal_pwd_text not found");
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_pwd_text", acctData.paypalPwd, "paypal_pwd_text not found");

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_submit_btn", "paypal_submit_btn not found");

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_paynow_btn", "paypal_paynow_btn not found", 0, false);

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

                bool flag = wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "PP_DepsoitSuccess");

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "close_window_link", "close_window_link not found");

                if (!flag)
                    BaseTest.Fail("No Success message found");

                BaseTest.Pass();

                driverObj.SwitchTo().Window(depositWindow);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "PayPal_img"))
                    return true;
                else
                    return false;
         
        
        }
        public bool Add_Paypal_BalanceValid(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window, bool multiple_paymethod = false)
        {
                string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
                DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
                double beforeVal = StringCommonMethods.ReadDoublefromString(temp);


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                if (multiple_paymethod == true)
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "add_new_paymethod", "Add new paymyemt method option is not found", FrameGlobals.reloadTimeOut, false);

                wAction.Click(driverObj, By.XPath(CashierPage.Paypal_tab_Xp), "Paypal payment method not found in the list", 0, false);

                BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

                wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found", 0, false);

                String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
           
                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_email_text", "paypal_email_text not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_email_text", acctData.paypalUser, "paypal_email_text not found");

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_pwd_text", "paypal_pwd_text not found");
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_pwd_text", acctData.paypalPwd, "paypal_pwd_text not found");

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_submit_btn", "paypal_submit_btn not found");

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));

                if (wAction.IsElementPresent(driverObj, By.Id("continue")))
                    wAction.Click(driverObj, By.Id("continue"), "Unable to click on pgmask");
                else
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_paynow_btn", "paypal_paynow_btn not found", 0, false);

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

                bool flag = wAction.WaitUntilElementPresent(driverObj,By.XPath(CashierPage.Skrill_success_Xp));

              //  wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "close_window_link", "close_window_link not found");

                if (!flag)
                    BaseTest.Fail("No Success message found");

                BaseTest.Pass();

                  wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                 
                
                if (wAction.WaitUntilElementPresent(driverObj,By.XPath(CashierPage.Paypal_img_Xp)))
                {
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                    temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
                    double AfterVal = StringCommonMethods.ReadDoublefromString(temp);

                    BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                    BaseTest.Pass();
                    driverObj.SwitchTo().DefaultContent();

                    if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
          
        }
        public bool Add_Sofort_BalanceValid(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window, bool multiple_paymethod = false)
        {

            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


            ////commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
            double beforeVal = StringCommonMethods.ReadDoublefromString(temp);



            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
            if (multiple_paymethod == true)
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "add_new_paymethod", "Add new paymyemt method option is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(6000);

            if (wAction.IsElementPresent(driverObj, By.XPath(CashierPage.Sofort_PayImage)))
                wAction.Click(driverObj, By.XPath(CashierPage.Sofort_PayImage));
            else
              if (wAction.IsElementPresent(driverObj, By.XPath(CashierPage.Sofort_tab)))
                wAction.Click(driverObj, By.XPath(CashierPage.Sofort_tab), "Sofort image/tab not found", 0, false);
              else
                  while (wAction.IsElementPresent(driverObj, By.Id("payments-next")))
                      if (!wAction.IsElementPresent(driverObj, By.XPath(CashierPage.Sofort_tab)))
                      {
                          driverObj.FindElement(By.Id("payments-next")).Click();

                      }
                      else
                      {
                          wAction.Click(driverObj, By.XPath(CashierPage.Sofort_tab));
                          break;
                      }
            

            BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");


            String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Sofort registration window not found", FrameGlobals.elementTimeOut);




            //     wAction.Click(driverObj, By.Id(CashierPage.Sofort_confirm_Id), "Confirm button not found", 0, false);

            wAction.Type(driverObj, By.Id(CashierPage.Sofort_Sortcode_Id), acctData.SortCode, "Sort code button not found", 0, false);
            wAction.Click(driverObj, By.XPath(CashierPage.Sofort_Next_Btn), "Next button not found");
            wAction.Type(driverObj, By.Id(CashierPage.Sofort_Acct_Number_Id), acctData.acctID, "Account ID button not found", 0, false);
            wAction.Type(driverObj, By.Id(CashierPage.Sofort_PIN_Number_Id), acctData.PIN, "PIN button not found", 0, false);
            wAction.Click(driverObj, By.XPath(CashierPage.Sofort_Next_Btn), "Next button not found");
            wAction.Click(driverObj, By.Id(CashierPage.Sofort_radiobtn_Id), "Radio button not found", 0, false);
            wAction.Click(driverObj, By.XPath(CashierPage.Sofort_Next_Btn), "Next button not found");
            wAction.Type(driverObj, By.Id(CashierPage.Sofort_Tan_Id), acctData.acctID, "TAN ID button not found", 0, false);
            wAction.Click(driverObj, By.XPath(CashierPage.Sofort_Next_Btn), "Next button not found");


            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
       
            BaseTest.Pass();

            driverObj.SwitchTo().Window(depositWindow);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            if (wAction.WaitUntilElementPresent(driverObj, By.XPath(CashierPage.Sofort_PayImage)))
            {
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
                double AfterVal = StringCommonMethods.ReadDoublefromString(temp);

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                BaseTest.Pass();
                driverObj.SwitchTo().DefaultContent();

                if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public bool Add_Giropay_BalanceValid(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window, bool multiple_paymethod = false)
        {

            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


            //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
            double beforeVal = StringCommonMethods.ReadDoublefromString(temp);



            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
            if (multiple_paymethod == true)
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "add_new_paymethod", "Add new paymyemt method option is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            wAction.WaitforPageLoad(driverObj);

            if (!wAction.IsElementPresent(driverObj, By.Id(CashierPage.GiroPay_PayImage)))
            {
                wAction.Click(driverObj, By.XPath(CashierPage.Giropay_tab), "Giropay image/tab not found", 0, false);
            }
            else
                wAction.Click(driverObj, By.Id(CashierPage.GiroPay_PayImage));


            BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");


            String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Giropay registration window not found", FrameGlobals.elementTimeOut);



            wAction.Type(driverObj, By.Id(CashierPage.Giropay_CustName_ID), "Test", "Customer name textbox not found", 0, false);
            wAction.Type(driverObj, By.Id(CashierPage.Giropay_SwiftCode_ID), acctData.SwiftBIC, "SwiftBIC box not found");
            wAction.Click(driverObj, By.Id(CashierPage.Giropay_Confirm_ID), "Confirm button not found");

            wAction.Type(driverObj, By.Name(CashierPage.Giropay_Sc_Name), acctData.sc, "SC box not found", 0, false);
            wAction.Type(driverObj, By.Name(CashierPage.Giropay_ExSc_Name), acctData.esc, "ESC box not found");
            wAction.Click(driverObj, By.XPath(CashierPage.Giropay_submit_ID), "Submit button not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "close_window_link", "close_window_link not found");

            BaseTest.Pass();

            driverObj.SwitchTo().Window(depositWindow);

            //  wAction.PageReload(driverObj);
            //  wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

            //wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));


            //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
            if (wAction.WaitUntilElementPresent(driverObj, By.Id(CashierPage.GiroPay_PayImage)))
            {
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
                double AfterVal = StringCommonMethods.ReadDoublefromString(temp);

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                BaseTest.Pass();
                driverObj.SwitchTo().DefaultContent();

                if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public bool Add_iDeal_BalanceValid(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window, bool multiple_paymethod = false)
        {

            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


            //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
            double beforeVal = StringCommonMethods.ReadDoublefromString(temp);



            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
            if (multiple_paymethod == true)
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "add_new_paymethod", "Add new paymyemt method option is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            wAction.WaitforPageLoad(driverObj);

            if (!wAction.IsElementPresent(driverObj, By.XPath(CashierPage.iDeal_PayImage)))
            {
                wAction.Click(driverObj, By.XPath(CashierPage.iDeal_tab), "Ideal image/tab not found", 0, false);
            }
            else
                wAction.Click(driverObj, By.XPath(CashierPage.iDeal_PayImage));


            BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");


            String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Ideal registration window not found", FrameGlobals.elementTimeOut);




            wAction.SelectDropdownOption(driverObj, By.Id(CashierPage.iDeal_BnkName_ID), "1", "Bank name dropdown not found/empty", 0, false);
            wAction.Click(driverObj, By.Id(CashierPage.iDeal_Proceed_ID), "Proceed button not found");

            wAction.Click(driverObj, By.Name(CashierPage.iDeal_Submit_Name), "Go button not found", 0, false);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "close_window_link", "close_window_link not found");

            BaseTest.Pass();

            driverObj.SwitchTo().Window(depositWindow);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));


            if (wAction.WaitUntilElementPresent(driverObj, By.XPath(CashierPage.iDeal_PayImage)))
            {

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
                double AfterVal = StringCommonMethods.ReadDoublefromString(temp);

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                BaseTest.Pass();
                driverObj.SwitchTo().DefaultContent();

                if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public bool Deposit_Paypal(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window)
        {
            try
            {

                string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
                DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);


                // commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());

                double beforeVal = StringCommonMethods.ReadDoublefromString(temp);

                //string bval = temp.Replace(",", string.Empty).Trim();
                //Regex re = new Regex(@"\d+");
                //Match b4Value = re.Match(bval);

                // double beforeVal = double.Parse(b4Value.Value);


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "PayPal_img", "Paypal payment method not /Not Registered");
                //wAction.Click(driverObj, By.XPath("//li[@data-type='PayPal']"), "Paypal payment method not found in the list");

                BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

                wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");

                String depositWindow = driverObj.WindowHandles.ToArray()[1].ToString();
                // driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[2].ToString());
                wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Paypal registration window not found", FrameGlobals.elementTimeOut);

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_email_text", "paypal_email_text not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_email_text", acctData.paypalUser, "paypal_email_text not found");

                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_pwd_text", "paypal_pwd_text not found");
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_pwd_text", acctData.paypalPwd, "paypal_pwd_text not found");

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_submit_btn", "paypal_submit_btn not found");

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_paynow_btn", "paypal_paynow_btn not found", 0, false);

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

                bool flag = wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "PP_DepsoitSuccess");

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "close_window_link", "close_window_link not found");

                if (!flag)
                    BaseTest.Fail("No Success message found");

                BaseTest.Pass();

                driverObj.SwitchTo().Window(depositWindow);
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
                double AfterVal = StringCommonMethods.ReadDoublefromString(temp);

                // double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                BaseTest.Pass();

                driverObj.SwitchTo().DefaultContent();

                if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                    return true;
                else
                    return false;
            }
            catch (Exception e) { BaseTest.Fail("Deposit Failed:" + e.Message.ToString()); }
            finally
            {
                driverObj.Close();
            }
            return true;
        }

        /// <summary>
        /// Author:Anand
        /// Register the customer through Playtech pages
        /// </summary>
        /// <param name="driverObj">webdriver instance</param>
        /// /// <param name="loginData">login details</param>
        public void Registration_PlaytechPages(IWebDriver driverObj, ref Registration_Data regData, int windowIndex = 0, bool checkFindAddress = false, string Bonus = null)
        {


            try
            {
                BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
                String portalWindow;
                if (windowIndex != 0)
                {
                    portalWindow = driverObj.WindowHandles.ToArray()[windowIndex - 1].ToString();
                    commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[windowIndex].ToString(), "Unable to locate registration page");
                }
                else
                {
                    portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                    wAction.WaitAndMovetoPopUPWindow(driverObj, "Unable to locate registration page");
                }
                BaseTest.Pass("Registration page opened");

                BaseTest.AddTestCase("Enter Registration details for customer", "Registration details should be entered and received success msg Successfully");
                regData.username = RepositoryCommon.PP_Registration(driverObj, ref regData, Bonus, checkFindAddress, false);

                wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 35);
                wAction.WaitUntilElementPresent(driverObj, By.Id(CashierPage.CashierUser_element_name));

                driverObj.Close();
                driverObj.SwitchTo().Window(portalWindow);
                //  temp:        
                bool login = false;
                //Verify customer creation


                string Urlcode;
                if (driverObj.Url.Contains("exchange"))
                    Urlcode = "exchange";
                else if (driverObj.Url.Contains("mobenga"))
                    Urlcode = "sports";
                else
                    Urlcode = driverObj.Url.Substring(0, driverObj.Url.IndexOf("ladbrokes.com"));

                if (Urlcode.Contains("sports"))
                {
                    //wAction.Click(driverObj,
                    login = wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.LoggedinMenu_XP));
                }

                else if (Urlcode.Contains("games"))
                {
                    wAction.PageReload(driverObj);
                    wAction.WaitforPageLoad(driverObj);
                    System.Threading.Thread.Sleep(5000);

                    login = wAction.WaitUntilElementPresent(driverObj, By.XPath(Games_Control.depositLink));
                    System.Threading.Thread.Sleep(3000);
                    wAction.Click(driverObj, By.XPath("//*[text()='No thanks']"));
                    //login = wAction._IsElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Ecom_welcome_txt");
                }
                else if (Urlcode.Contains("exchange"))
                {
                    wAction.PageReload(driverObj);
                    wAction.WaitforPageLoad(driverObj);
                    System.Threading.Thread.Sleep(2000);
                    login = (wAction.WaitUntilElementPresent(driverObj, By.LinkText(Ecomm_Control.exchange_Logout)));

                }
                else if (Gallio.Framework.TestContext.CurrentContext.Test.Name == "Verify_CashierPopUP_LoggedOut")
                {
                    driverObj.Navigate().Refresh();
                    System.Threading.Thread.Sleep(2000);

                    //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                    login = wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.name, "Username_dep_page");
                }

                else
                    login = wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName");

                BaseTest.WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);

                BaseTest.AddTestCase("Username:" + regData.username + " Password:" + regData.password, "");
                BaseTest.Pass();

                if (!login)
                    BaseTest.Fail("Auto Login for Registered username:" + regData.username + " has failed");

                else
                    BaseTest.Pass("Customer is Registered successfully username:" + regData.username);


            }
            catch (Exception e) { BaseTest.Assert.Fail("Customer Login Failed : " + e.Message.ToString()); }
        }

        public void Registration_PlaytechPages_BonusCode(IWebDriver driverObj, ref Registration_Data regData, string Bonus, bool invalid = false)
        {



            BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");

            BaseTest.Pass("Registration page opened");

            BaseTest.AddTestCase("Enter Registration details for customer with Invalid Bonus code", "Registration details should be entered and received success msg Successfully");
            string username = RepositoryCommon.PP_Registration(driverObj, ref regData, Bonus, invalid);


            if (username == "Success")
                BaseTest.Fail("Invalid Bonus error message not appearing");

            else
                BaseTest.Pass("Success");



        }

        public void Registration_PlaytechPages_FieldValidation(IWebDriver driverObj, ref Registration_Data regData)
        {


            try
            {
                BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
                String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");

                BaseTest.Pass("Registration page opened");

                BaseTest.AddTestCase("Enter Registration details for customer", "Registration details should be entered and received success msg Successfully");
                string username = RepositoryCommon.PP_Registration_FV(driverObj, ref regData);
                wAction.WaitforPageLoad(driverObj);


                driverObj.Close();
                driverObj.SwitchTo().Window(portalWindow);


                bool login = false;
                login = wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName");


                if (!login)
                    BaseTest.Fail("Auto Login for Registered username:" + username + " has failed");

                else
                    BaseTest.Pass("Customer is Registered successfully username:" + username);


            }
            catch (Exception e) { BaseTest.Assert.Fail("Customer Login Failed : " + e.Message.ToString()); }
        }

        public void Registration_PlaytechPages_Postal(IWebDriver driverObj, ref Registration_Data regData)
        {


            string username = null;
            BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");

            BaseTest.Pass("Registration page opened");

            BaseTest.AddTestCase("Enter Registration details for customer", "Registration details should be entered and received success msg Successfully");

            if (regData.country_code == "Germany")
                username = RepositoryCommon.PP_Registration_Germany(driverObj, ref regData);
            else if (regData.country_code == "Ireland")
                username = RepositoryCommon.PP_Registration_IreLand(driverObj, ref regData);

            wAction.WaitforPageLoad(driverObj);


            driverObj.Close();
            driverObj.SwitchTo().Window(portalWindow);


            bool login = false;
            login = wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName");


            if (!login)
                BaseTest.Fail("Auto Login for Registered username:" + username + " has failed");

            else
                BaseTest.Pass("Customer is Registered successfully username:" + username);



        }

        public void Registration_PlaytechPages_PaypalQuickReg(IWebDriver driverObj, ref Registration_Data regData, MyAcct_Data depData)
        {


            try
            {
                ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
                myBrowser.Start();
                BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
                String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");
                BaseTest.Pass("Registration page opened");
                //    bool flag = wAction.IsElementPresent(myBrowser, By.XPath("//form[@id='quick-registration-form']//select[@name='currency']"));
                BaseTest.AddTestCase("Select USD (Non default currency) and deposit limit as 1000 for weekly in Quick paypal reg", "Selected data should be set accordingly");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                wAction.Click(driverObj, By.LinkText("Show Paypal quick registration form"), "Show Paypal quick registration form link not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                wAction.SelectDropdownOption(myBrowser, By.XPath("//form[@id='quick-registration-form']//select[@name='currency']"), "USD", "Currency drop down not found or USD value not found in the list", 0, false);
                wAction.Click(myBrowser, By.XPath("//form[@id='quick-registration-form']//input[@data-select='weekly']"), "Weekly deposit limit radio not found");
                wAction.SelectDropdownOption(myBrowser, By.XPath("//form[@id='quick-registration-form']//select[@name='depositLimit']"), "1000", "Deposit limit drop down not found / value 1000 not found in the list");
                wAction.Type(driverObj, By.XPath("id('quick-registration-form')//input[@name='amount']"), depData.depositAmt, "Deposit amount box not found in quick reg");
                wAction.Click(driverObj, By.XPath("id('quick-registration-form')//button"), "Sumbit button not found");
                BaseTest.Pass();

                PaypalQuickRegistration(driverObj, depData);


                BaseTest.AddTestCase("Enter Registration details for customer", "Registration details should be entered and received success msg Successfully");
                string username = RepositoryCommon.PP_Registration_QuickReg(driverObj, ref regData, depData);
                wAction.WaitforPageLoad(driverObj);


                //driverObj.Close();
                driverObj.SwitchTo().Window(portalWindow);


                bool login = false;
                login = wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "log_button_UserName");


                if (!login)
                    BaseTest.Fail("Auto Login for Registered username:" + username + " has failed");

                else
                    BaseTest.Pass("Customer is Registered successfully username:" + username);


            }
            catch (Exception e) { BaseTest.Assert.Fail("Customer Login Failed : " + e.Message.ToString()); }
        }

        public void PaypalQuickRegistration(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Register paypal from paypal Window", "Registration Window Should be opened Successfully and registration should be successful");
            // wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Paypal registration window not found", FrameGlobals.elementTimeOut);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(40));
            wAction.Click(driverObj, By.Id("loadLogin"), null, 0, false);

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_email_text", "paypal_email_text not found", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_email_text", acctData.paypalUser, "paypal_email_text not found");

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_pwd_text", "paypal_pwd_text not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_pwd_text", acctData.paypalPwd, "paypal_pwd_text not found");

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_submit_btn", "paypal_submit_btn not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paypal_paynow_btn", "paypal_paynow_btn not found", 0, false);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));


            BaseTest.Pass();


        }
        /// <summary>
        /// Author:Sandeep
        /// Verify the different links for "My Account" on my account page. 
        /// </summary>
        /// <param name="driverObj">broswer</param>        
        public void MyAccount_VerifyLinks(IWebDriver driverObj)
        {
            //String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

          
                wAction.Click(driverObj, By.Id(MyAcctPage.myAcct_tab_Id), "Personal Details link not found", FrameGlobals.reloadTimeOut, false);

                //BaseTest.AddTestCase("Verified Personal Details links in 'My Account' page", "'My accounts' page should contain Personal details links");
                BaseTest.AddTestCase("Verified below links in 'My Account' page <br>Personal Details <br>Change Password <br> Responsible Gambling <br> Account History", "'My accounts' page should contain Personal details links");
                if (!wAction.IsElementPresent(driverObj, By.LinkText("Personal Details")))
                {
                    BaseTest.Fail("Personal Details link is not avaliable");
                }


                if (!wAction.IsElementPresent(driverObj, By.LinkText("Change Password")))
                {
                    BaseTest.Fail("Change Password link is not avaliable");
                }

                if (!wAction.IsElementPresent(driverObj, By.LinkText("Responsible Gambling")))
                {
                    BaseTest.Fail("Responsible Gambling link is not avaliable");
                }

                if (!wAction.IsElementPresent(driverObj, By.LinkText("Free Bets")))
                {
                    BaseTest.Fail("Free Bets link is not avaliable");
                }

              
                BaseTest.Pass();
         }

        public void MyDetails_VerifyLinks(IWebDriver driverObj)
        {

            BaseTest.AddTestCase("Verified below links in 'My Details' page <br>Messages <br>Change Password <br> Alerts<br> Odds on!", "'My Details' page should contain Personal details links");

            wAction.WaitAndMovetoFrame(driverObj, By.Name("acctmidnav"));
            if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "msg_lnk"))
            {
                BaseTest.Fail("Messages Details link is not avaliable");
            }


            if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "Alert_lnk"))
            {
                BaseTest.Fail("Alerts link is not avaliable");
            }

            if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "Odds_lnk"))
            {
                BaseTest.Fail("Odds link is not avaliable");
            }

            BaseTest.Pass();

        }


        /// <summary>
        /// Author:Sandeep
        /// Verify Fields in Change Password tab in "My Account" page. 
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void MyAccount_ChangePassword(IWebDriver driverObj)
        {
            //String portalWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            try
            {
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "change_Pwd_lnk", "Change Password link is not found", FrameGlobals.reloadTimeOut, false);

                try
                {
                    BaseTest.AddTestCase("Verified below fields in Change password tab, in 'My Account' page <br>Old Password text box <br>New Password text box <br>Retype new Password text box <br> Submit button", "Change password page field validation failed");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox"))
                    {
                        BaseTest.Fail("Old Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify New Password text box in Change Password tab in 'My Account' page", "New Password text box is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox"))
                    {
                        BaseTest.Fail("New Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify Retype new Password text box in Change Password tab in 'My Account' page", "Retype new Password text box is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox"))
                    {
                        BaseTest.Fail("Retype new Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify Submit button in 'My Account' page", "Submit button is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn"))
                    {
                        BaseTest.Fail("Submit button is not avaliable");
                    }
                    BaseTest.Pass();
                }
                catch (Exception e) { BaseTest.Assert.Fail("Error while validating change password page" + e.Message.ToString()); }
            }
            catch (Exception e) { BaseTest.Assert.Fail("Error during opening Change password tab in 'My Accounts' page" + e.Message.ToString()); }
            finally
            {
                //commonWebMethods.BrowserClose(driverObj);
                //driverObj.SwitchTo().Window(portalWindow);

            }
        }


        public void MyAccount_CheckNonUKcustomer_Country(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Check UK is avaliable in country drop down", "UK should be available");

           // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "personal_details_lnk", "Personal Details link not found", FrameGlobals.reloadTimeOut, false);
            // wAction.SelectDropdownOption(driverObj, By.XPath("id('country')"), "United Kingdom", "United Kingdom not found in the drop down for non UK customer");
           // wAction.PageReload(driverObj);
            BaseTest.Assert.IsFalse(wAction.IsElementPresent(driverObj, By.Id("fillFields")), "Find address button is displayed for non UK customer");
            //  BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//option[text()='United Kingdom']")), "United Kingdom not found in the drop down for non UK customer");

            BaseTest.Pass();
        }

        public void MyAccount_Edit_ContactPref_DirectMail(IWebDriver driverObj)
        {
            ISelenium browser = new WebDriverBackedSelenium(driverObj, "https://www.google.co.in/");
            browser.Start();
            BaseTest.AddTestCase("Edit contact preference- Direct mail", "Contact preference should be updated successfully");
       //     wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "personal_details_lnk", "Personal Details link not found", FrameGlobals.reloadTimeOut, false);
            //BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//span[@class='input-checkbox-styled']//input[@id='accountNotificationDirectMail']")), "Failed to edit contact preferences in portal");
            //browser.IsElementPresent("//span[@class='input-checkbox-styled']//input[@id='accountNotificationDirectMail']");
            //browser.Highlight("//span[@class='input-checkbox-styled']//input[@id='accountNotificationDirectMail']");
            // browser.Click("id='promotionalNotificationDirectMail']");
            // browser.Click("//button[@id='_updatemydetails_WAR_accountportlet_updateMyDetailsSubmit']");
            //browser.IsElementPresent("//div[@class='portlet-msg portlet-msg-info']");
            bool flag = wAction._IsElementPresent(browser, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "directmail_chk");

            wAction._Click(browser, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "directmail_chk", "Direct mail check box not found", 0, false);
            System.Threading.Thread.Sleep(2000);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "submit_btn", "Submit button not found", 0, false);
            System.Threading.Thread.Sleep(2000);
            BaseTest.Assert.IsTrue(browser.IsElementPresent("//div[@class='portlet-msg portlet-msg-info']"), "Failed to edit contact preferences in portal");
            BaseTest.Pass();
        }

        /// <summary>
        /// Author:Sandeep
        /// Verify Fields in Change Password tab in "My Account" page. 
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void MyAccount_ModifyDetails(IWebDriver driverObj, ref Dictionary<string, string> modDetails)
        {
            //String portalWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            Random rand = new Random();
            string postcode = "HA" + rand.Next(11, 99).ToString() + "SR";
          //  wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "personal_details_lnk", "Personal Details link not found", FrameGlobals.reloadTimeOut, false);

            //Test data
            modDetails.Add("address", "Test address " + rand.Next(11111, 99999));
            modDetails.Add("phone", "+4478678678" + rand.Next(11111, 99999));
            modDetails.Add("mobile", "+4478678678" + rand.Next(11111, 99999));

            try
            {

                BaseTest.AddTestCase("Modify details in 'My Account' page is complete and verified", "Field modification in 'My Accounts' page failed");
                try
                {
                    //Clear and modify post code text field
                    String temp;
                    wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
                    temp = wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "postcode_textBox", "", false);
                    temp = wAction._GetAttribute(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "postcode_textBox", "value", "", false);
                    wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "postcode_textBox", "Post code text box was not found", FrameGlobals.reloadTimeOut, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "postcode_textBox", postcode, "Post code text box not found", FrameGlobals.reloadTimeOut, false);
                    BaseTest.AddTestCase("Modified postcode for customer from: " + temp + " To " + postcode, "Field modification in 'My Accounts' page failed");
                    BaseTest.Pass();
                    //Clear and modify address text field
                    temp = wAction._GetAttribute(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "address_textBox", "value", "", false);
                    wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "address_textBox", "Address text box was not found", FrameGlobals.reloadTimeOut, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "address_textBox", modDetails["address"], "Address text box not found", FrameGlobals.reloadTimeOut, false);
                    BaseTest.AddTestCase("Modified Adrress line1 for customer from: " + temp + " To " + modDetails["address"], "Field modification in 'My Accounts' page failed");
                    BaseTest.Pass();
                    //Clear and modify telephone text field
                    temp = wAction._GetAttribute(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "telephone_textBox", "value", "", false);
                    wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "telephone_textBox", "Telephone text box was not found", FrameGlobals.reloadTimeOut, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "telephone_textBox", modDetails["phone"], "Telephone text box not found", FrameGlobals.reloadTimeOut, false);
                    BaseTest.AddTestCase("Modified phone number for customer from: " + temp + " To " + modDetails["phone"], "Field modification in 'My Accounts' page failed");
                    BaseTest.Pass();

                    //Clear and modify mobile text field
                    temp = wAction._GetAttribute(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "mobile_textBox", "value", "", false);
                    wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "mobile_textBox", "Mobile text box was not found", FrameGlobals.reloadTimeOut, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "mobile_textBox", modDetails["mobile"], "Mobile text box not found", FrameGlobals.reloadTimeOut, false);
                    BaseTest.AddTestCase("Modified mobile number for customer from: " + temp + " To " + modDetails["mobile"], "Field modification in 'My Accounts' page failed");
                    BaseTest.Pass();
                    System.Threading.Thread.Sleep(2000);
                    //Click on Submit button
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "submit_personal_detail_btn", "Submit button not found", 0, false);

                    if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text", "Confirmation text block not found", false) == "Your details have been changed")
                    {
                        BaseTest.Pass();

                    }
                    else
                    {
                        BaseTest.Fail("Modified fields are not changed");
                    }
                }
                catch (Exception e) { BaseTest.Assert.Fail("Error while modifying personal details" + e.Message.ToString()); }
            }
            catch (Exception e) { BaseTest.Assert.Fail("Error while modifying personal details" + e.Message.ToString()); }
            finally
            {
                //commonWebMethods.BrowserClose(driverObj);
                //driverObj.SwitchTo().Window(portalWindow);
            }

        }

        #region TBD
        /// <summary>
        /// Author:Nagamanickam
        /// Depsoit  fund into wallets - netteller
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        //public void DepositTOWallet_Netteller1(IWebDriver driverObj, MyAcct_Data acctData)
        //{

        //    BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");
        //    // commonWebMethods.OpenURL(driverObj, FrameGlobals.LadbrokesIMSdirect, "My Account page not loaded", FrameGlobals.reloadTimeOut + 20);

        //    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

        //    BaseTest.AddTestCase("Verify Deposit link is available and clicking on Deposit link takes to the new Banking window", "The deposit page should be present and opens a new window");
        //    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
        //    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", 0, false);
        //    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
        //    BaseTest.Pass();

        //    Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonDeposit_Netteller_PT(driverObj, acctData, acctData.depositAmt,portalWindow), "Amount not added after deposit");
        //    BaseTest.Pass();
        //    driverObj.SwitchTo().Window(portalWindow);


        //}
        #endregion


        /// <summary>
        /// Author:Anusha
        /// Deposit - Deposit limit unset
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void Deposit_LimitUnset(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");
            // commonWebMethods.OpenURL(driverObj, FrameGlobals.LadbrokesIMSdirect, "My Account page not loaded", FrameGlobals.reloadTimeOut + 20);

            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.Common_Deposit_LimitUnset(driverObj, acctData), "Amount not added after deposit");
            BaseTest.Pass();
        }

        /// <summary>
        /// Author:Nagamanickam
        /// Withdraw  fund into wallets - netteller
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void Withdraw_Netteller(IWebDriver driverObj, MyAcct_Data acctData, string site = null)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
            try
            {
                //Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not deducted after withdraw");
                Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt, false, false, "Games"), "Amount not deducted after withdraw");
                BaseTest.Pass();
            }
            finally
            {
                driverObj.SwitchTo().Window(portalWindow);
            }

        }

        public void Withdraw_Sofort(IWebDriver driverObj, MyAcct_Data acctData, string site = null)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonWithdraw_Sofort_PT(driverObj, acctData, acctData.depositAmt), "Amount not deducted after withdraw");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);


        }
        //public void Withdraw_Netteller_Sports(IWebDriver driverObj, MyAcct_Data acctData)
        //{

        //    BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
        //    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

        //    BaseTest.AddTestCase("Verify Deposit link is available and clicking on Deposit link takes to the new Banking window", "The deposit page should be present and opens a new window");
        //    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Sports_banking_lnk", "Banking Link not found", FrameGlobals.reloadTimeOut, false);
        //    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
        //    BaseTest.Pass();

        //    Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not deducted after withdraw");
        //    BaseTest.Pass();
        //    driverObj.SwitchTo().Window(portalWindow);


        //}

        public void Withdraw_TextBoXValidation(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.Withdraw_TextBoXValidation(driverObj, acctData, acctData.depositAmt), "Text box validation Failed");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);


        }

        public void Withdraw_AnyPaymethod(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonWithdraw_PT(driverObj, acctData, true), "Amount not deducted after withdraw");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);


        }

        /// <summary>
        /// Author:Anu
        /// Withdraw  fund from wallets - Credit card
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void Withdraw_CC(IWebDriver driverObj, MyAcct_Data acctData, String wallets)
        {
            //List<string> wallet = wallets.ToString().Split(';').ToList<string>();

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);
            //for (int toWalletInd = 0; toWalletInd < wallet.Count(); toWalletInd++)
            //{
            //    acctData.withdrawWallet = wallet[toWalletInd].ToString();
            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonWithdraw_CreditCard_PT(driverObj, acctData, acctData.depositAmt, false), "Amount not deducted after withdraw");

            // } 
            driverObj.SwitchTo().Window(portalWindow);


        }

        public void Withdraw_CC_Ecom(IWebDriver driverObj, MyAcct_Data acctData)
        {
            // List<string> wallet = wallets.ToString().Split(';').ToList<string>();

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);
            //for (int toWalletInd = 0; toWalletInd < wallet.Count(); toWalletInd++)
            //{
            //    acctData.withdrawWallet = wallet[toWalletInd].ToString();
            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonWithdraw_CreditCard_PT(driverObj, acctData, acctData.depositAmt, false), "Amount not deducted after withdraw");

            //}
            driverObj.SwitchTo().Window(portalWindow);


        }

        /// <summary>
        /// Author:Nagamanickam
        /// Cancel withdraw fund into wallets - netteller
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void Cancel_Withdraw_Netteller(IWebDriver driverObj, MyAcct_Data acctData, string site = null)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Cancel the Withdraw amount from wallet", "Withdrawed Amount should be Cancelled from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);
            try
            {
                Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonCancelWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added after cancelling the withdraw");
                BaseTest.Pass();
            }
            finally
            {
                driverObj.SwitchTo().Window(portalWindow);
            }


        }
      

        /// <summary>
        /// Author:Nagamanickam
        /// Cancel withdraw fund into wallets - netteller
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void Transfer_Withdraw_Netteller(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.depositWallet + " wallet to  " + acctData.withdrawWallet, "Transfer Amount should be success from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
            driverObj.SwitchTo().DefaultContent();

            string temp = acctData.depositWallet;
            acctData.depositWallet = acctData.withdrawWallet;
            acctData.withdrawWallet = temp;

            temp = acctData.wallet1;
            acctData.wallet1 = acctData.wallet2;
            acctData.wallet2 = temp;

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");

            driverObj.Close();
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);


        }

        /// <summary>
        /// Author:Anand
        /// LOgout from Portal
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void LogoutFromPortal(IWebDriver driverObj)
        {

            BaseTest.AddTestCase("Logout form Portal", "Customer should be able to logout");
            wAction.PageReload(driverObj);
            System.Threading.Thread.Sleep(2000);
            wAction.Click(driverObj, By.XPath(Cashier_Control_SW.TransactionNotification_OK_Dialog));
            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", null, 0, false);
            // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Username_btn", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
            //  wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "Logout", "Logout Link is not found", FrameGlobals.reloadTimeOut, false);
            wAction.Click(driverObj, By.LinkText("Logout"), "Logout link not found", FrameGlobals.reloadTimeOut, false);
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name("username")), "Customer might not have logged off successfully please check");
            BaseTest.Pass();

        }

        public void LogoutFromPortal_Ecom(IWebDriver driverObj)
        {

            BaseTest.AddTestCase("Logout form Portal", "Customer should be able to logout");
            // wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Ecom_welcome_txt", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
            // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Username_btn", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "Logout", "Logout Link is not found", FrameGlobals.reloadTimeOut, false);

            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("loginusername")), "Customer might not have logged off successfully please check");
            BaseTest.Pass();

        }




        /// <summary>
        /// Author:Anand
        /// Fetch the currency
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void getCurrency(IWebDriver driverObj)
        {

            BaseTest.AddTestCase("Logout form Portal", "Customer should be able to logout");
            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
            // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Username_btn", "Header element username button is not found", FrameGlobals.reloadTimeOut, false);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "Logout", "Logout Link is not found", FrameGlobals.reloadTimeOut, false);

            //Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonCancelWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added after cancelling the withdraw");
            BaseTest.Pass();

        }

        /// <summary>
        /// Author:Sandeep
        /// Verify deposit limit in "My Account" page. 
        /// </summary>
        /// <param name="driverObj">broswer</param>        
        public string MyAccount_Responsible_Gambling(IWebDriver driverObj, string limit = "None", string currency = "GBP")
        {
            //BaseTest.AddTestCase("set deposit limit in my acct", "deposit limit should be set");
            System.Threading.Thread.Sleep(5000);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            System.Threading.Thread.Sleep(2000);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "responsible_gambling_lnk", "Responsible Gambling link not found", FrameGlobals.reloadTimeOut, false);
            System.Threading.Thread.Sleep(3000);
            //   wAction.Click(driverObj, By.Name("deposit-period"), "deposit period radio button not found", 0, false);

            wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "resp_gambling_submit");
            SelectElement sele = new SelectElement(driverObj.FindElement(By.Id("limitDepositDaily")));

            string temp1 = sele.SelectedOption.Text;

            if (temp1 == currency + " 500")
            {
                sele.SelectByText(currency + " 1000");
                BaseTest.AddTestCase("Modified daily deposit limit to GBP 1000 in Responsible Gambling page", "Mobifying deposit limit details in Responsible Gambling page failed");
            }
            else
            {
                if (limit != "None")
                {
                    sele.SelectByText(currency + " " + limit);
                    BaseTest.AddTestCase("Modified daily deposit limit to " + currency + " " + limit + " in Responsible Gambling page", "Mobifying deposit limit details in Responsible Gambling page failed");
                }
                else
                {
                    sele.SelectByText(currency + " 500");
                    BaseTest.AddTestCase("Modified daily deposit limit to " + currency + " 500 in Responsible Gambling page", "Mobifying deposit limit details in Responsible Gambling page failed");
                }
            }


            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "resp_gambling_submit", "Submit button in responsible gambling page is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(8000);
            string deplimit = sele.SelectedOption.Text;

            if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "resp_gamb_confimation", "Confirmation text block not found", false) == "Details changed")
            {
                BaseTest.Pass();
                return deplimit;
            }
            else
            {
                BaseTest.Fail("Modify deposit limit are not changed");

            }


            return null;
        }

        /// <summary>
        /// Author:Sandeep
        /// Login/Logout to Sports page
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void Login_Sports(IWebDriver driverObj, Login_Data loginData)
        {



            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            BaseTest.AddTestCase("Login to Sports portal usign valid credential: " + loginData.username, "Customer should be able to Login");
            wAction.WaitUntilElementPresent(driverObj, By.Id(Sportsbook_Control.username_Id));
            wAction.Clear(driverObj, By.Id(Sportsbook_Control.username_Id), "Header element username textbox is not found", FrameGlobals.reloadTimeOut, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.username_Id), loginData.username, "Header element username textbox is not found", 0, false);
            wAction.Click(driverObj, By.Id(Sportsbook_Control.pwd_Id), "Header element Password textbox is not found", 0, false);
            wAction.Type(driverObj, By.Id(Sportsbook_Control.pwd_Id), loginData.password, "Header element Password textbox is not found", 0, false);
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.login_btn_XP), "Login not found", 0, true);

            System.Threading.Thread.Sleep(5000);

            if (FrameGlobals.projectName == "IP2") wAction.Click(driverObj, By.LinkText("Close"));


            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.Deposit_XP)), "Login did not happen/Deposit link missing");
            wAction.Click(driverObj, By.XPath("//a/div[text()='X']"));
            BaseTest.Pass();


        }
        public double GetBalance_Sports(IWebDriver driverObj)
        {



            BaseTest.AddTestCase("Get Balance from SPorts Header ", "Balance should be displayed");
            if (wAction.WaitUntilElementPresent(driverObj, By.ClassName("balance-text")))
                goto Skip;
          
                wAction.WaitforPageLoad(driverObj);
                System.Threading.Thread.Sleep(2000);

                wAction.Click(driverObj, By.Id("user_balance_hide_show"), "Balance header not found");
                System.Threading.Thread.Sleep(3000);
            
            Skip:
            double val = StringCommonMethods.ReadDoublefromString(wAction.GetText(driverObj, By.ClassName("balance-text"), "Balance not found", false).Replace("GBP", "").Trim());

            BaseTest.Pass();
            return val;

        }



        /// <summary>
        /// Author:Sandeep
        /// Register VISA credit card to customer
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void Verify_Credit_Card_Registration(IWebDriver driverObj, string cardNumber, string type = "Visa")
        {
            string cvvCode = cardNumber.Substring(cardNumber.Length - 3);
            string typeImag = "";
            BaseTest.AddTestCase("Register " + type + " in cashier page", "Customer should be able to register " + type + " in cashier page");
            BaseTest.AddTestCase("Enter following details in 'Add new account' page: <br></br> 1. Credit card number <br></br> 2. Expiry month and year <br></br> 3. CVV code ", "Customer should be able to Enter <br></br> 1. Credit card <br></br> 2. Expiry Month/Year <br></br> 3. CVV code");


            if (type == "Visa")
                typeImag = "visa_tab";

            else if (type == "EntroPay")
                typeImag = "Entro_tab";

            else
                typeImag = "Master_tab";


            if (typeImag != "Master_tab")
            {
                wAction.WaitUntilElementPresent(driverObj, By.Id("payments-next"));
                while (wAction.IsElementPresent(driverObj, By.Id("payments-next")))
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, typeImag))
                    {
                        driverObj.FindElement(By.Id("payments-next")).Click();

                    }
                    else
                        break;

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, typeImag, typeImag + " Not found in the deposit page");
                //driverObj.SwitchTo().DefaultContent();
            }
        //    wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.name, "credit_card_frame", null, FrameGlobals.elementTimeOut);
            
            wAction.WaitAndMovetoFrame(driverObj, By.Name(Cashier_Control_SW.redirectIframe));
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "credit_number", cardNumber, "Credit card number field is not found/Not loaded", 0, false);

            wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "expiry_month", "4", "Expiry month select box is not found", 0, false);

            wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "expiry_year", "4", "Expiry Year select box is not found", 0, false);

            //wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "cvv_code", cvvCode, "CVV code textbox is not found", FrameGlobals.reloadTimeOut, false);

            BaseTest.Pass();
            //}
            //catch (Exception e) { BaseTest.Assert.Fail("Error while entering credit card details" + e.Message.ToString()); }
            System.Threading.Thread.Sleep(3000);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "continue_btn", "Continue button is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(3000);

            driverObj.SwitchTo().DefaultContent();

            //  wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", null,3);

            if (typeImag == "visa_tab")
            {
                if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "visa_pm"))
                {
                    BaseTest.Pass();
                }
            }
            else if (typeImag == "Master_tab")
            {
                if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "MasterCard_img"))
                {
                    BaseTest.Pass();
                }
            }
            else if (typeImag == "Entro_tab")
            {
                if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "EntroPay_img"))
                {
                    BaseTest.Pass();
                }
            }
            else
                BaseTest.Fail("Credit card registraiton failed");


        }

        public void Verify_Credit_Card_Registration_Used_CC(IWebDriver driverObj, string cardNumber, string type = "Visa")
        {
            string cvvCode = cardNumber.Substring(cardNumber.Length - 3);
            string typeImag = "";
            BaseTest.AddTestCase("Register " + type + " in cashier page", "Customer should be able to register " + type + " in cashier page");
            BaseTest.AddTestCase("Enter following details in 'Add new account' page: <br></br> 1. Credit card number <br></br> 2. Expiry month and year <br></br> 3. CVV code ", "Customer should be able to Enter <br></br> 1. Credit card <br></br> 2. Expiry Month/Year <br></br> 3. CVV code");

          

            if (type == "Visa")
                typeImag = "visa_tab";

            else if (type == "EntroPay")
                typeImag = "Entro_tab";

            else
                typeImag = "Master_tab";


            if (typeImag != "Master_tab")
            {
                wAction.WaitUntilElementPresent(driverObj, By.Id("payments-next"));
                while (wAction.IsElementPresent(driverObj, By.Id("payments-next")))
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, typeImag))
                    {
                        driverObj.FindElement(By.Id("payments-next")).Click();

                    }
                    else
                        break;

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, typeImag, typeImag + " Not found in the deposit page");
                //driverObj.SwitchTo().DefaultContent();
            }
        //    wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.name, "credit_card_frame", null, FrameGlobals.elementTimeOut);
            wAction.WaitAndMovetoFrame(driverObj, Cashier_Control_SW.redirectIframe);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "credit_number", cardNumber, "Credit card number field is not found/Not loaded", 0, false);

            wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "expiry_month", "4", "Expiry month select box is not found", 0, false);

            wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "expiry_year", "4", "Expiry Year select box is not found", 0, false);

            //wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "cvv_code", cvvCode, "CVV code textbox is not found", FrameGlobals.reloadTimeOut, false);

            BaseTest.Pass();
            //}
            //catch (Exception e) { BaseTest.Assert.Fail("Error while entering credit card details" + e.Message.ToString()); }
            System.Threading.Thread.Sleep(3000);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "continue_btn", "Continue button is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(3000);
            String bodyText = driverObj.FindElement(By.TagName(CashierPage.Used_CC_Text_name)).Text;
            String ExpectedText = "Account is not allowed";
            int result = 0;

            result = string.Compare(bodyText, ExpectedText);
            if (result == 0)
                BaseTest.Pass();
            else
                BaseTest.Fail("Account not allowed error message not found");
            return;


            driverObj.SwitchTo().DefaultContent();

            //  wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", null,3);
            bool item = false;

            if (!(typeImag == "visa_tab"))
            {
                if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "visa_pm"))
                {
                    item = true;
                }
            }
            else if (!(typeImag == "Master_tab"))
            {
                if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "MasterCard_img"))
                {
                    item = true;
                }
            }

            if (item == false)
                BaseTest.Fail("Credit card registraiton failed");


        }


        //public void DepositTOWallet_Paypal(IWebDriver driverObj, MyAcct_Data acctData)
        //{
        //    BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");

        //    String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

        //    BaseTest.AddTestCase("Verify Deposit link is available and clicking on Deposit link takes to the new Banking window", "The deposit page should be present and opens a new window");
        //    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);
        //    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", 0, false);
        //    driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
        //    BaseTest.Pass();

        //    Framework.BaseTest.Assert.IsTrue(Deposit_Paypal(driverObj, acctData, acctData.depositAmt, portalWindow), "Amount not added after deposit");
        //    BaseTest.Pass();
        //    driverObj.SwitchTo().Window(portalWindow);
        //}

        /// <summary>
        /// Author:Nagamanickam
        /// Transfer fund between wallets
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// /// <param name="loginData">My Acct details</param>
        public void AllWallets_Transfer(IWebDriver driverObj, MyAcct_Data acctData, string wallets, string table)
        {
            List<string> wallet = wallets.ToString().Split(';').ToList<string>();
            List<string> walletTbl = table.ToString().Split(';').ToList<string>();

            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.depositWallet + " wallet to  " + acctData.withdrawWallet, "Transfer Amount should be success from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);

            for (int fromWalletInd = 0; fromWalletInd < wallet.Count(); fromWalletInd++)
                for (int toWalletInd = 0; toWalletInd < wallet.Count(); toWalletInd++)
                    if (wallet[fromWalletInd].ToString() == wallet[toWalletInd].ToString())
                        continue;

                    else
                    {
                        BaseTest.AddTestCase("Validating transfer from wallet:" + wallet[fromWalletInd].ToString() + " to Towallet: " + wallet[toWalletInd].ToString(), "Transfer Should be successfull");
                        acctData.depositWallet = wallet[fromWalletInd];
                        acctData.withdrawWallet = wallet[toWalletInd];
                        acctData.wallet1 = walletTbl[fromWalletInd];
                        acctData.wallet2 = walletTbl[toWalletInd];

                        Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
                        BaseTest.Pass();
                        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                        driverObj.SwitchTo().DefaultContent();

                    }
            driverObj.Close();
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        public void GamesToGaming_GamingToGames_Transfer(IWebDriver driverObj, MyAcct_Data acctData, string wallets, string table)
        {
            // List<string> wallet = wallets.ToString().Split(';').ToList<string>();
            // List<string> walletTbl = table.ToString().Split(';').ToList<string>();

            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.depositWallet + " wallet to  " + acctData.withdrawWallet, "Transfer Amount should be success from the selected wallet");
            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            acctData.wallet1 = acctData.depositWallet;
            acctData.wallet2 = acctData.withdrawWallet;

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
            BaseTest.Pass();

            acctData.wallet1 = acctData.depositWallet;
            acctData.wallet2 = acctData.withdrawWallet;
            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
            BaseTest.Pass();

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
            driverObj.SwitchTo().DefaultContent();


            driverObj.Close();
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        /// <summary>
        /// Author:Anusha
        /// Transfer insufficient fund between wallet
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// /// <param name="loginData">My Acct details</param>
        public void Wallet_Transfer_Balance_Insuff(IWebDriver driverObj, MyAcct_Data acctData, string fromWallet, string toWallet)
        {

            //BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + fromWallet + " wallet to  " + toWallet+"", "Transfer Amount should be success from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);


            BaseTest.AddTestCase("Validating insufficient funds error message when the customer tries to transfer more than available balance from wallet: " + fromWallet + " to Towallet: " + toWallet, "Insufficient funds error message should appear");
            acctData.depositWallet = fromWallet;
            acctData.withdrawWallet = toWallet;
            acctData.wallet1 = fromWallet;
            acctData.wallet2 = toWallet;

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransfer_Netteller_PT_InsuffBal(driverObj, acctData), "Amount not added/deducted after transfer");
            BaseTest.Pass();
        }
        public void Wallet_Transfer_Single_Ecom(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.depositWallet + " wallet to  " + acctData.withdrawWallet + "", "Transfer Amount should be success from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);


            acctData.wallet1 = acctData.depositWallet;
            acctData.wallet2 = acctData.withdrawWallet;

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
            BaseTest.Pass();
        }


        public void Wallet_Transfer_SportsGames_Sports(IWebDriver driverObj, MyAcct_Data acctData)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.depositWallet + " wallet to  " + acctData.withdrawWallet + "", "Transfer Amount should be success from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);


            acctData.wallet1 = acctData.depositWallet;
            acctData.wallet2 = acctData.withdrawWallet;

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
            BaseTest.Pass();

            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.withdrawWallet + " wallet to  " + acctData.depositWallet + "", "Transfer Amount should be success from the selected wallet");
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

            acctData.wallet1 = acctData.withdrawWallet;
            acctData.wallet2 = acctData.depositWallet;

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
            BaseTest.Pass();
        }

        // <summary>
        /// Author:Sandeep
        /// Register VISA credit card to customer
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void Verify_Neteller_Registration(IWebDriver driverObj, string neteller_id, string neteller_pwd, string depositWallet, bool multiple_paymethod = false)
        {
            driverObj.Manage().Window.Maximize();
            string WalletPath = "//tr[td[contains(text(),'" + depositWallet + "')]]/td[2]";
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            string temp = wAction.GetText(driverObj, By.XPath(WalletPath), depositWallet + "Wallet value not found", false).ToString();
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);


                if (multiple_paymethod == true)
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "add_new_paymethod", "Add new paymyemt method option is not found", FrameGlobals.reloadTimeOut, false);


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "neteller_payment_tab", "Neteller paymyemt method is not found", 0, false);

                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "neteller_accountId", neteller_id, "Neteller account id textbox is not found", 0, false);

                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", neteller_pwd, "Neteller password textbox is not found", FrameGlobals.reloadTimeOut, false);

                if (wAction._GetAttribute(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "value") == string.Empty)
                {
                    wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Neteller password textbox is not found");
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "50");
                }

                wAction.SelectDropdownOption(driverObj, By.Id(CashierPage.WalletDropdown_Id), depositWallet);

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "Deposit button is not found", 0, false);

                System.Threading.Thread.Sleep(3000);
                BaseTest.AddTestCase("Verify Neteller payment method registration and first deposit", "Neteller registration should be successful");
                wAction.PageReload(driverObj);
                driverObj.SwitchTo().DefaultContent();

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                string temp2 = wAction.GetText(driverObj, By.XPath(WalletPath), depositWallet + "Wallet value not found", false).ToString();
       
            double AfterVal=0; 
                if (StringCommonMethods.ReadDoublefromString(temp2) != -1)
                AfterVal = StringCommonMethods.ReadDoublefromString(temp2);
                else
                    BaseTest.Fail("Wallet value is null/Blank");

                BaseTest.Assert.IsTrue((AfterVal == StringCommonMethods.ReadDoublefromString(temp) + 50), "Balance not added: Expected : 50 & Actual:"+ AfterVal);

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);


                if (wAction.WaitUntilElementPresent(driverObj, By.XPath("//td[contains(text(),'" + neteller_id + "')]")))
                    BaseTest.Pass();
                else
                    BaseTest.Fail("Neteller Registration/deposit failed");

        }
        public void Verify_Webmoney_Registration(IWebDriver driverObj, MyAcct_Data acctData)
        {
            ISelenium browser = new WebDriverBackedSelenium(driverObj, "https://www.google.co.in/");
            browser.Start();
            try
            {

                BaseTest.AddTestCase("Register Webmoney to the customer", "Customer should be deposited with Webmoney");
                //wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
                //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), null, FrameGlobals.elementTimeOut);
                string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                double beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace("USD", "").Trim());


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "webmoney_tab"))
                {
                    driverObj.FindElement(By.Id("payments-next")).Click();

                }

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "webmoney_tab", "Webmoney paymemt method is not found", FrameGlobals.reloadTimeOut, false);
                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Unable to type on Amount text box", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Unable to type on Amount text box");
                wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "Source wallet select box not found");

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "Unable to click on Deposit Button", FrameGlobals.reloadTimeOut, false);
                System.Threading.Thread.Sleep(3000);

                wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Qiwi Pop Window not found");


                //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("RedirectTarget"), null, FrameGlobals.elementTimeOut);
                //wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "phone_txt", phone, "Pin 1 text box not found", 0, false);
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Proceed_btn", "Unable to click on Proceed_btn");
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Go_btn", "Go button pay button is not found", FrameGlobals.reloadTimeOut, false);

                wAction.BrowserClose(driverObj);
                wAction.WaitAndMovetoPopUPWindow(driverObj, "Unable to switch back to Banking Window");
                //  driverObj.SwitchTo().DefaultContent();


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace("USD", "").Trim());

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                BaseTest.Pass();


                // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);


                if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                    BaseTest.Pass();
                else
                    throw new Exception("qiwi registration/deposit failed");

                //wAction.Click(driverObj, By.LinkText("Go back"));
                // driverObj.SwitchTo().DefaultContent();
            }
            catch (Exception e) { BaseTest.Assert.Fail("Error while Webmoney registration" + e.Message.ToString()); }
        }
        // <summary>
        /// Author:Anusha
        /// Register Paysafe card to customer
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void Verify_PaySafe_Registration(IWebDriver driverObj, string pin1, string pin2, string pin3, string pin4, MyAcct_Data acctData)
        {
            ISelenium browser = new WebDriverBackedSelenium(driverObj, "https://www.google.co.in/");
            browser.Start();
            try
            {

                BaseTest.AddTestCase("Register PaySafe to the customer", "Customer should be deposited with PaySafe");
                //wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
                //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), null, FrameGlobals.elementTimeOut);
                string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                double beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('€', ' ').Trim());


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                System.Threading.Thread.Sleep(3000);
                while (wAction.IsElementPresent(driverObj, By.Id("payments-next")))
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "paysafe_tab"))
                    {
                        driverObj.FindElement(By.Id("payments-next")).Click();

                    }
                    else
                        break;

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "paysafe_tab", "Paysafe paymemt method is not found", FrameGlobals.reloadTimeOut, false);




                //wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "source_wallet_select", depositWallet, "Source wallet select box not found", FrameGlobals.reloadTimeOut, false);
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paysafe_dep_btn", "Paysafe paymeemt method is not found", FrameGlobals.reloadTimeOut, false);
                System.Threading.Thread.Sleep(3000);
                // commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), null, FrameGlobals.elementTimeOut);
                commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("RedirectTarget"), null, FrameGlobals.elementTimeOut);
                System.Threading.Thread.Sleep(3000);

                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paysafe_pin1", pin1, "Pin 1 text box not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paysafe_pin2", pin2, "Pin 2 text box not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paysafe_pin3", pin3, "Pin 3 text box not found", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paysafe_pin4", pin4, "Pin 4 text box not found", 0, false);

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "patsafe_acc_check", "Paysafe pay button is not found", FrameGlobals.reloadTimeOut, false);
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "paysafe_pay_btn", "Paysafe pay button is not found", FrameGlobals.reloadTimeOut, false);

                // System.Threading.Thread.Sleep(10000);

                // commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("RedirectTarget"), null, FrameGlobals.elementTimeOut);
                //if (commonWebMethods.IsElementPresent(driverObj,By.XPath("//div[@class='message msg-ok']")))
                //{
                //    BaseTest.Pass();
                //}
                //else
                //{
                //    BaseTest.Fail("Pay safe registration failed");
                //}
                // wAction.Click(driverObj, By.LinkText("Go back"),"Go Back Link not found",0,false);
                driverObj.SwitchTo().DefaultContent();
                wAction.Click(driverObj, By.XPath("//a[span[text()='close']]"));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('€', ' ').Trim());

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                BaseTest.Pass();


                // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                driverObj.SwitchTo().DefaultContent();

                if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                    BaseTest.Pass();
                else
                    throw new Exception("Pay Safe registration/deposit failed");

                //wAction.Click(driverObj, By.LinkText("Go back"));
                // driverObj.SwitchTo().DefaultContent();
            }
            catch (Exception e) { BaseTest.Assert.Fail("Error while Paysafe registration" + e.Message.ToString()); }
        }
        public void Verify_BetCard_Registration(IWebDriver driverObj, MyAcct_Data acctData, string betcard)
        {
            //ISelenium browser = new WebDriverBackedSelenium(driverObj, "https://www.google.co.in/");
            //browser.Start();
            //try
            //{

            BaseTest.AddTestCase("Register Bet Card to the customer", "Customer should be deposited with Bet Card");
            //wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
            //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), null, FrameGlobals.elementTimeOut);
            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            double beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace("USD", "").Trim());


            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", FrameGlobals.reloadTimeOut, false);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "betCard_tab", "Bet card paymemt method is not found", 0, false);

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Voucher_txt", "Unable to type on Voucher text box", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Voucher_txt", betcard, "Unable to type on Voucher text box");

            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Unable to type on Amount text box", 0, false);
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Unable to type on Amount text box");
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "Source wallet select box not found");
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "Deposit button is not found", 0, false);

            System.Threading.Thread.Sleep(3000);


            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace("USD", "").Trim());

            BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
            BaseTest.Pass();


            // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
            driverObj.SwitchTo().DefaultContent();

            if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                BaseTest.Pass();
            else
                throw new Exception("Pay Safe registration/deposit failed");

            //wAction.Click(driverObj, By.LinkText("Go back"));
            // driverObj.SwitchTo().DefaultContent();
            //}
            //catch (Exception e) { BaseTest.Assert.Fail("Error while Paysafe registration" + e.Message.ToString()); }
        }

        public void Verify_QiWi_Registration(IWebDriver driverObj, string phone, MyAcct_Data acctData)
        {
            ISelenium browser = new WebDriverBackedSelenium(driverObj, "https://www.google.co.in/");
            browser.Start();
            try
            {

                BaseTest.AddTestCase("Register Qiwi to the customer", "Customer should be deposited with qiwi");
                //wAction._WaitAndMovetoFrame(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "deposit_tab_iframe", "Cashier frame is not found", FrameGlobals.reloadTimeOut);
                //  wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                double beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace("USD", "").Trim());


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "qiwi_tab"))
                {
                    driverObj.FindElement(By.Id("payments-next")).Click();

                }

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "qiwi_tab", "Qiwi paymemt method is not found", FrameGlobals.reloadTimeOut, false);
                wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Unable to type on Amount text box", 0, false);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Unable to type on Amount text box");
                wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "Source wallet select box not found");

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "Unable to click on Deposit Button", FrameGlobals.reloadTimeOut, false);
                System.Threading.Thread.Sleep(3000);

                wAction.WaitAndMovetoPopUPWindow_WithIndex(driverObj, 2, "Qiwi Pop Window not found");


                //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("RedirectTarget"), null, FrameGlobals.elementTimeOut);
                wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "phone_txt", phone, "Pin 1 text box not found", 0, false);
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Proceed_btn", "Unable to click on Proceed_btn");
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Go_btn", "Go button pay button is not found", FrameGlobals.reloadTimeOut, false);

                wAction.BrowserClose(driverObj);
                wAction.WaitAndMovetoPopUPWindow(driverObj, "Unable to switch back to Banking Window");
                //  driverObj.SwitchTo().DefaultContent();


                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace("USD", "").Trim());

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                BaseTest.Pass();


                // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);


                if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                    BaseTest.Pass();
                else
                    throw new Exception("qiwi registration/deposit failed");

                //wAction.Click(driverObj, By.LinkText("Go back"));
                // driverObj.SwitchTo().DefaultContent();
            }
            catch (Exception e) { BaseTest.Assert.Fail("Error while qiwi registration" + e.Message.ToString()); }
        }

        public void MyAccount_ChangePassword_Func(IWebDriver driverObj, String password)
        {
            //String portalWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            try
            {
                System.Threading.Thread.Sleep(5000);
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "change_Pwd_lnk", "Change Password link is not found", FrameGlobals.reloadTimeOut, false);

                try
                {
                    BaseTest.AddTestCase("Verified below fields in Change password tab, in 'My Account' page <br>Old Password text box <br>New Password text box <br>Retype new Password text box <br> Submit button", "Change password page field validation failed");
                    if (!wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox"))
                    {
                        BaseTest.Fail("Old Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify New Password text box in Change Password tab in 'My Account' page", "New Password text box is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox"))
                    {
                        BaseTest.Fail("New Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify Retype new Password text box in Change Password tab in 'My Account' page", "Retype new Password text box is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox"))
                    {
                        BaseTest.Fail("Retype new Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify Submit button in 'My Account' page", "Submit button is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn"))
                    {
                        BaseTest.Fail("Submit button is not avaliable");
                    }

                    BaseTest.Pass();
                    BaseTest.AddTestCase("Changing the password using Invalid old password and verifying the error message", "Invalid password error should be displayed");
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", "hfbjh123gf", "Unable to find element", 0, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Newpassword1", "Unable to find element", 0, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Newpassword1", "Unable to find element", 0, false);
                    System.Threading.Thread.Sleep(3000);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");

                    if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "invalid_pwd_error", "Confirmation text block not found", false).Contains("Invalid password"))
                    {
                        BaseTest.Pass();
                    }
                    else
                    {
                        BaseTest.Fail("Password changed successfully using invalid old password/ Invalid password error message is incorrect");
                    }
                    System.Threading.Thread.Sleep(3000);

                    BaseTest.AddTestCase("Changing the password using valid old password", "Password should be changed");
                    wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", "Unable to find element", 0, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element", 0, false);
                    wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Unable to find element", 0, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Newpassword1", "Unable to find element", 0, false);
                    wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Unable to find element", 0, false);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Newpassword1", "Unable to find element", 0, false);
                    System.Threading.Thread.Sleep(3000);
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn", Keys.Enter, "Submit button not found/not clicked");

                    if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text", "Confirmation text block not found", false).Contains("Password change was successful"))
                    {
                        BaseTest.Pass();
                    }
                    else
                    {
                        BaseTest.Fail("Unable to change password");
                    }
                    System.Threading.Thread.Sleep(3000);
                }
                catch (Exception e) { BaseTest.Assert.Fail("Error while validating change password page" + e.Message.ToString()); }
            }
            catch (Exception e) { BaseTest.Assert.Fail("Error during opening Change password tab in 'My Accounts' page" + e.Message.ToString()); }
        }

        /// <summary>
        /// Author:Anand
        /// Verify Fields in Change Password tab in "My Account" page. 
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void MyAccount_ChangePassword_Func_Old(IWebDriver driverObj, String password, bool flag = false)
        {
            //String portalWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            try
            {
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "change_Pwd_lnk", "Change Password link is not found", FrameGlobals.reloadTimeOut, false);

                try
                {
                    BaseTest.AddTestCase("Verified below fields in Change password tab, in 'My Account' page <br>Old Password text box <br>New Password text box <br>Retype new Password text box <br> Submit button", "Change password page field validation failed");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox"))
                    {
                        BaseTest.Fail("Old Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify New Password text box in Change Password tab in 'My Account' page", "New Password text box is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox"))
                    {
                        BaseTest.Fail("New Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify Retype new Password text box in Change Password tab in 'My Account' page", "Retype new Password text box is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox"))
                    {
                        BaseTest.Fail("Retype new Password text box is not avaliable");
                    }


                    //BaseTest.AddTestCase("Verify Submit button in 'My Account' page", "Submit button is not found");
                    if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn"))
                    {
                        BaseTest.Fail("Submit button is not avaliable");
                    }

                    BaseTest.Pass();
                    BaseTest.AddTestCase("Changing the password", "Password should be changed");
                    wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "old_pwd_textBox", password, "Unable to find element", 0, false);
                    if (!flag)
                    {
                        wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Newpassword1", "Unable to find element", 0, false);
                        wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Newpassword1", "Unable to find element", 0, false);
                    }
                    else
                    {
                        wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_pwd_textBox", "Password1", "Unable to find element", 0, false);
                        wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "new_Pwd_verify_textBox", "Password1", "Unable to find element", 0, false);
                    }

                    System.Threading.Thread.Sleep(3000);
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "change_pwd_btn");

                    if (flag)
                    {
                        if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text", "Confirmation text block not found", false).Contains("Password has already been used recently"))
                        {
                            BaseTest.Pass();
                        }
                        else
                        {
                            BaseTest.Fail("Unable to change password");
                        }
                        return;
                    }
                    if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "confirmation_text", "Confirmation text block not found", false).Contains("Password change was successful"))
                    {
                        BaseTest.Pass();
                    }
                    else
                    {
                        BaseTest.Fail("Unable to change password");
                    }
                    System.Threading.Thread.Sleep(3000);
                }
                catch (Exception e) { BaseTest.Assert.Fail("Error while validating change password page" + e.Message.ToString()); }
            }
            catch (Exception e) { BaseTest.Assert.Fail("Error during opening Change password tab in 'My Accounts' page" + e.Message.ToString()); }
        }

        /// <summary>
        /// Author:Anand
        /// Depsoit max fund into wallets - netteller
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void DepositMax_Netteller(IWebDriver driverObj, String maxAmnt, String netSID)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to deposit amount into wallet", "Amount should be deposited to the selected wallet");
            // commonWebMethods.OpenURL(driverObj, FrameGlobals.LadbrokesIMSdirect, "My Account page not loaded", FrameGlobals.reloadTimeOut + 20);
            String portalWindow = OpenCashier(driverObj, false);

            //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", netSID, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", maxAmnt, "Amount_txt not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
            if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "amntExcedMsg"))
            {
                BaseTest.Fail("No error message is displayed for Exceded amount");
            }

            if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "amntExcedMsg", "No error message is displayed for Exceded amount", false).Contains("The amount exceeds allowed limits"))
            {
                BaseTest.Pass();
            }

            driverObj.SwitchTo().Window(portalWindow);

        }


        public void SearchEvent(IWebDriver driverObj, String eventName)
        {

            BaseTest.AddTestCase("Search the given event : " + eventName, "Event details should be displayed");
            //try
            //{
            //    //Search Event
            wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "SearchOption", "Search Option not found", FrameGlobals.reloadTimeOut, false);
            wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.id, "SearchBox", eventName + Keys.Enter, "Search Box not found", 0, false);

            //Click on Searched Event Detail.
            wAction.Click(driverObj, By.XPath("//span[normalize-space()='" + eventName + "']"), "No Result Found for the Event: +" + eventName, 0, false);
            driverObj.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 10, 0));
            // BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//div[@class='market-group']")), "Event details page not opened");
            BaseTest.Pass("done");
            //}catch(Exception e){BaseTest

        }
        public void AddToBetSlip(IWebDriver driverObj, int amount)
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
            wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "StakeAmt_text", amount.ToString(), "StakeAmt_text Box not found", 0, false);
            BaseTest.Pass("done");

        }

        public void VerifySelection(IWebDriver driverObj, string selection)
        {
            bool flag = false;
            BaseTest.AddTestCase("Verify if the selection is present", "Selection is present in the event detail page");
            //Select bet
            //System.Threading.Thread.Sleep(4000);
            try
            {
                List<IWebElement> selections = driverObj.FindElements(By.XPath("//a[contains(@class,'betbutton')]")).ToList();
                for (int i = 0; i < selections.Count; i++)
                {
                    if (selections[i].Text.Contains(selection))
                    {
                        flag = true;
                        break;
                    }
                }

            }
            catch (Exception e) { BaseTest.Fail("No selection found for the event"); flag = false; }
            if (flag) { BaseTest.Pass("done"); }
            else { BaseTest.Fail("Selection: " + selection + " is not present in the event details page"); }

        }

        public void AddToBetSlipInsuff(IWebDriver driverObj, int amount, String Stake = null)
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
            wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "StakeAmt_text", amount.ToString(), "StakeAmt_text Box not found", 0, false);


            //PlaceBet
            wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "PlaceBet_Btn", "PlaceBet_Button not found", 0, false);

            System.Threading.Thread.Sleep(5000);
            BaseTest.Assert.IsTrue(wAction._IsElementPresent(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "Insuffund_error"), "Event details page not opened");
            BaseTest.Pass("done");

        }

        public void AddToBetSlipPlaceBet(IWebDriver driverObj, int amount)
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
            wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "StakeAmt_text", amount.ToString(), "StakeAmt_text Box not found", 0, false);


            //PlaceBet
            wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "PlaceBet_Btn", "PlaceBet_Button not found", 0, false);

            System.Threading.Thread.Sleep(7000);
            BaseTest.Assert.IsTrue(wAction._IsElementPresent(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "Betplace_success"), "Placing bet failed");
            BaseTest.Pass("done");

        }

        public void AddToBetSlipPlaceBet_selection(IWebDriver driverObj, string selection, string Stake)
        {

            BaseTest.AddTestCase("Add to betslip for the given bet for the event", "Selection should get added to the bet slip");
            //Select bet
            System.Threading.Thread.Sleep(4000);
            try
            {
                List<IWebElement> ele = driverObj.FindElements(By.XPath("//a[contains(@class,'betbutton')]")).ToList();
                foreach (IWebElement selections in ele)
                    if (selections.Text.Contains(selection))
                    {
                        selections.Click();
                        break;
                    }
            }
            catch (Exception e) { BaseTest.Fail("No selection found for the event"); }

            System.Threading.Thread.Sleep(4000);
            //wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "FreeBet_Collapse", "FreeBet_Collapse not found", 0, false);

            //Enter Stake
            wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "StakeAmt_text", Stake, "StakeAmt_text Box not found", 0, false);


            //PlaceBet
            wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "PlaceBet_Btn", "PlaceBet_Button not found", 0, false);

            System.Threading.Thread.Sleep(5000);
            BaseTest.Assert.IsTrue(wAction._IsElementPresent(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "Betplace_success"), "Placing bet failed");
            BaseTest.Pass("done");

        }


        /// <summary>
        /// Author:Naga        
        /// </summary>
        /// <param name="driverObj">broswer</param>        
        public void Ecom_MyAcct_AutoTopUP(IWebDriver driverObj)
        {
            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            try
            {

                wAction.Click(driverObj, By.LinkText("My Account"), "My Account link not found", FrameGlobals.reloadTimeOut, false);



                // wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox_PT");
                BaseTest.AddTestCase("Disable AutoTop up checkbox", "Auto top up checkbox should be diabled");
                wAction.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "My Account pop up window not opened", FrameGlobals.elementTimeOut);
                driverObj.Manage().Window.Maximize();
                System.Threading.Thread.Sleep(8000);
                wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));


                wAction.Click(driverObj, By.LinkText("Preferences"), "Preferences tab not found", 0, false);

                wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));


                wAction.WaitUntilElementPresent(driverObj, By.XPath("//span[input[@id='bettingAutoTopUp']]"));


            DisableNow:
                bool isEnabled = wAction.IsElementPresent(driverObj, By.XPath("//span[input[@id='bettingAutoTopUp' and @class='checked']]"));
                if (isEnabled)
                {
                    wAction.Click(driverObj, By.XPath("//span[contains(text(),'Enable Auto Top-up')]"), "Unable to click on Auto top up check box");
                    wAction.Click(driverObj, By.Id("betting-pref-save"), "Save Preference button not found", 0, false);
                    wAction.WaitforPageLoad(driverObj);
                    System.Threading.Thread.Sleep(3000);
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.Id("betting-pref-success")), "Preference saving Failed");
                }
                else
                {
                    wAction.Click(driverObj, By.XPath("//span[contains(text(),'Enable Auto Top-up')]"), "Unable to click on Auto top up check box");
                    goto DisableNow;
                }
                BaseTest.Pass("Done");





            }
            finally
            {
                driverObj.Close();
                wAction.WaitAndMovetoPopUPWindow(driverObj, portalWindow, "Unable to switch to main window");
            }
        }

        /// <summary>
        /// Author:Anusha
        /// Login from portal
        /// </summary>
        /// <param name="driverObj">webdriver instance</param>
        /// <param name="failMsg"> failure msg</param>
        /// <param name="loginData">login details</param>

        public void LoginFromPortal_SecurityError_Prompt(IWebDriver driverObj, Login_Data loginData, string errorMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            commonWebMethods.WaitforPageLoad(driverObj);
            commonWebMethods.WaitUntilElementPresent(driverObj, By.Name("username"));

        retype:
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_PT_XP)).Contains(errorMsg), "invalid error msg not found");
            BaseTest.Pass();
        }
        public void LoginFromPortal_SecurityError(IWebDriver driverObj, Login_Data loginData, string errorMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            commonWebMethods.WaitforPageLoad(driverObj);
            commonWebMethods.WaitUntilElementPresent(driverObj, By.Name("username"));

        retype:
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();


            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.ClosedCust_PT_XP),"Error msg dialog does not appear",false).Contains(errorMsg), "invalid error msg not found");
            BaseTest.Pass();
        }

        public void LoginFromPortal_InvalidCust(IWebDriver driverObj, Login_Data loginData, string failMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            commonWebMethods.WaitforPageLoad(driverObj);
            commonWebMethods.WaitUntilElementPresent(driverObj, By.Name("username"));

        retype:
            wAction.Clear(driverObj, By.Name("username"), "username not found", 0, false);
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();
            //IWebElement ele = driverObj.FindElement(By.XPath("//div[contains(@class,'login-popup')]"));

            // wAction.Click(driverObj, By.XPath(Cashier_Control_SW.TransactionNotification_OK_Dialog));
            BaseTest.Assert.IsTrue(commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath("//div[contains(@class,'login-popup')]")), failMsg);

            //commonWebMethods.WaitforPageLoad(driverObj);
            //  if(FrameGlobals.projectName=="IP2")
            string errorMsg = baseTest.ReadxmlData("err", "PT_Invalid_Lgn", DataFilePath.IP2_Authetication);

            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_PT_XP),"login prompt did not open",false).Contains(errorMsg), "invalid error msg not found");
            BaseTest.Pass();
        }

        public void LoginFromPortal_FrozenCust(IWebDriver driverObj, Login_Data loginData, string failMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            commonWebMethods.WaitforPageLoad(driverObj);
            commonWebMethods.WaitUntilElementPresent(driverObj, By.Name("username"));

        retype:
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();
            //IWebElement ele = driverObj.FindElement(By.XPath("//div[contains(@class,'login-popup')]"));

            // wAction.Click(driverObj, By.XPath(Cashier_Control_SW.TransactionNotification_OK_Dialog));
            BaseTest.Assert.IsTrue(commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath("//div[contains(@class,'login-popup')]")), failMsg);

            //commonWebMethods.WaitforPageLoad(driverObj);
            //  if(FrameGlobals.projectName=="IP2")
            string errorMsg = baseTest.ReadxmlData("err", "PT_Frozen_Lgn", DataFilePath.IP2_Authetication);

            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.invalidLoginErr_PT_XP)).Contains(errorMsg), "invalid error msg not found");
            BaseTest.Pass();
        }
        public void LoginFromPortal_SelfExCust(IWebDriver driverObj, Login_Data loginData, string failMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            commonWebMethods.WaitforPageLoad(driverObj);
            commonWebMethods.WaitUntilElementPresent(driverObj, By.Name("username"));

        retype:
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();
            //IWebElement ele = driverObj.FindElement(By.XPath("//div[contains(@class,'login-popup')]"));

            // wAction.Click(driverObj, By.XPath(Cashier_Control_SW.TransactionNotification_OK_Dialog));
            // BaseTest.Assert.IsTrue(commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath("//div[contains(@class,'login-popup')]")), failMsg);

            //commonWebMethods.WaitforPageLoad(driverObj);
            //  if (FrameGlobals.projectName == "IP2")
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.SelExCust_PT_XP)), "incorrect login error message");
            BaseTest.Pass();
        }
        public void LoginFromPortal_ClosedCust(IWebDriver driverObj, Login_Data loginData, string failMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            commonWebMethods.WaitforPageLoad(driverObj);
            commonWebMethods.WaitUntilElementPresent(driverObj, By.Name("username"));

        retype:
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();

            //  BaseTest.Assert.IsTrue(commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath("//div[contains(@class,'login-popup')]")), failMsg);
            string errorMsg = baseTest.ReadxmlData("err", "PT_Closed_Lgn", DataFilePath.IP2_Authetication);

            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.ClosedCust_PT_XP)).Contains(errorMsg), "incorrect login error message");
            BaseTest.Pass();

        }
        public void LoginFromPortal_AVFailedCust(IWebDriver driverObj, Login_Data loginData, string failMsg)
        {

            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
            driverObj.Manage().Window.Maximize();
            string s = loginData.fname;
            commonWebMethods.WaitforPageLoad(driverObj);
            commonWebMethods.WaitUntilElementPresent(driverObj, By.Name("username"));

        retype:
            driverObj.FindElement(By.Name("username")).SendKeys(loginData.username);
            driverObj.FindElement(By.Name("password")).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.Name("username"), "value") == string.Empty)
                goto retype;

            driverObj.FindElement(By.XPath(Login_Control.loginBtn_Xpath)).Click();

            //  BaseTest.Assert.IsTrue(commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath("//div[contains(@class,'login-popup')]")), failMsg);
            string errorMsg = baseTest.ReadxmlData("err", "PT_AVFailed_Lgn", DataFilePath.IP2_Authetication);

            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.ClosedCust_PT_XP)).Contains(errorMsg), "incorrect login error message");
            BaseTest.Pass();

        }

        public void LoginPrompt(IWebDriver driverObj, Login_Data loginData)
        {
            BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
            BaseTest.Pass();
        retype:
            // driverObj.FindElement(By.XPath(Login_Control.promptUserName_Xpath)).Click();
            // commonWebMethods.Clear(driverObj, By.XPath(Login_Control.promptUserName_Xpath), "Username text box not found ,{Error In :Login_Control.promptUserName_Xpath}", 0, false);
            driverObj.FindElement(By.XPath(Login_Control.promptUserName_Xpath)).SendKeys(loginData.username);
            commonWebMethods.Clear(driverObj, By.XPath(Login_Control.promptPassword_Xpath), "Password text box not found,{Error In :Login_Control.promptPassword_Xpath}", 0, false);
            driverObj.FindElement(By.XPath(Login_Control.promptPassword_Xpath)).SendKeys(loginData.password);
            if (commonWebMethods.GetAttribute(driverObj, By.XPath(Login_Control.promptUserName_Xpath), "value") == string.Empty)
                goto retype;

            commonWebMethods.Click(driverObj, By.XPath(Login_Control.promptLoginBtn_Xpath), "Click on Login button failed", 0, false);
            commonWebMethods.WaitforPageLoad(driverObj);
            commonWebMethods.Click(driverObj, By.XPath(Login_Control.modelWindow_Xpath), null, 15);
            commonWebMethods.WaitforPageLoad(driverObj);


            if (driverObj.Url.Contains("my-account"))
            {
                BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "responsible_gambling_lnk"), "Visible_gambling_lnk link not found");

            }
            else
            {
                if (!commonWebMethods.IsElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)))
                    driverObj.Navigate().Refresh();

                BaseTest.Assert.IsTrue(commonWebMethods.WaitUntilElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)), "Customer unable to login");
                //BaseTest.Assert.IsTrue(commonWebMethods.GetText(driverObj, By.XPath(Login_Control.userDisplay_Xpath), "User login failed,{Error In :Login_Control.userDisplay_Xpath}").ToString().Contains(loginData.fname), "Customer login failed");
                wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Xpath));

                System.Threading.Thread.Sleep(3000);
            clickAgain:

                wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox_PT");
                wAction.WaitforPageLoad(driverObj);
                if (wAction._IsElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox_PT"))
                    goto clickAgain;


                wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Xpath));
                System.Threading.Thread.Sleep(2000);

            clickAgain2:
                wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath));
                wAction.WaitforPageLoad(driverObj);
                if (wAction.IsElementPresent(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath)))
                    goto clickAgain2;

                commonWebMethods.WaitforPageLoad(driverObj);

            }
        }

        /// <summary>
        /// Author:Anusha      
        /// </summary>
        /// <param name="driverObj">broswer</param>        
        public void Ecom_MyAcct_AutoTopUP_Enable(IWebDriver driverObj)
        {
            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            try
            {

                wAction.Click(driverObj, By.LinkText("My Account"), "My Account link not found", FrameGlobals.reloadTimeOut, false);
                BaseTest.AddTestCase("Disable AutoTop up checkbox", "Auto top up checkbox should be diabled");
                wAction.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "My Account pop up window not opened", FrameGlobals.elementTimeOut);

                wAction.Click(driverObj, By.LinkText("Preferences"), "Preferences tab not found", 0, false);

                wAction.WaitUntilElementPresent(driverObj, By.XPath("//span[input[@id='bettingAutoTopUp']]"));


                bool s = wAction.IsElementPresent(driverObj, By.XPath("//span[input[@id='bettingAutoTopUp' and @class='checked']]"));
                if (!wAction.IsElementPresent(driverObj, By.XPath("//span[input[@id='bettingAutoTopUp' and @class='checked']]")))
                {
                    wAction.Click(driverObj, By.XPath("//span[contains(text(),'Enable Auto Top-up')]"), "Unable to click on Auto top up check box");
                    wAction.Click(driverObj, By.Id("betting-pref-save"), "Save Preference button not found", 0, false);
                    wAction.WaitforPageLoad(driverObj);
                    System.Threading.Thread.Sleep(3000);
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.Id("betting-pref-success")), "Preference saving Failed");
                }

                BaseTest.Pass("Done");





            }
            catch (Exception e) { BaseTest.Assert.Fail("Error in 'My Accounts' Preference page" + e.Message.ToString()); }
            finally
            {
                driverObj.Close();
                wAction.WaitAndMovetoPopUPWindow(driverObj, portalWindow, "Unable to switch to main window");
            }
        }

        /// <summary>
        /// Author:Nagamanickam
        /// Login from portal
        /// </summary>
        /// <param name="driverObj">webdriver instance</param>
        /// /// <param name="loginData">login details</param>
        public void LoginFromEcom(IWebDriver driverObj, Login_Data loginData)
        {
            try
            {
                BaseTest.AddTestCase("Username:" + loginData.username + " Password:" + loginData.password, "");
                BaseTest.Pass();
                driverObj.Manage().Window.Maximize();
                BaseTest.AddTestCase("Login portal with Valid user details : " + loginData.username, "Login should be successfull");
                string s = loginData.fname;

            retype:
                wAction.Clear(driverObj, By.Id(Ecomm_Control.Username_ID), "Username text box not found", FrameGlobals.reloadTimeOut, false);
                driverObj.FindElement(By.Id(Ecomm_Control.Username_ID)).SendKeys(loginData.username);
                wAction.Clear(driverObj, By.Id(Ecomm_Control.Pwd_ID), "Password text box not found", FrameGlobals.reloadTimeOut, false);
                driverObj.FindElement(By.Id(Ecomm_Control.Pwd_ID)).SendKeys(loginData.password);
                if (wAction.GetAttribute(driverObj, By.Id(Ecomm_Control.Pwd_ID), "value") == string.Empty)
                    goto retype;

                driverObj.FindElement(By.Id("submit-header")).Click();
                commonWebMethods.WaitforPageLoad(driverObj);


                // commonWebMethods.Click(driverObj, By.XPath(Login_Control.modelWindow_Generic_Xpath), null, 15);

                //commonWebMethods.Click(driverObj, By.XPath(Login_Control.modelWindow_Xpath), null, 15);
                //  commonWebMethods.WaitforPageLoad(driverObj);
                /*if (!commonWebMethods.IsElementPresent(driverObj, By.Id(Portal_Control.Customer_Menu_Id)))
                    commonWebMethods.PageReload(driverObj);
                */
                System.Threading.Thread.Sleep(10000);
                wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox");
                //wAction.Click(driverObj, By.XPath("//a/div[text()='X']"));
                BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Ecom_welcome_txt"), "Customer unable to login");
                //BaseTest.Assert.IsTrue(commonWebMethods.GetText(driverObj, By.XPath(Login_Control.userDisplay_Xpath), "User login failed,{Error In: Login_Control.userDisplay_Xpath}").ToString().Contains(loginData.fname), "Customer login failed");
                commonWebMethods.WaitforPageLoad(driverObj);
                BaseTest.Pass();
            }
            catch (Exception e) { BaseTest.Assert.Fail("Customer Login Failed : " + e.Message.ToString()); }

        }


        public void OpenRealPlay(IWebDriver driverObj)
        {
            System.Threading.Thread.Sleep(3000);
            driverObj.FindElement(By.XPath("//a[text()='Super Heroes']")).Click();
            driverObj.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 60, 0));
            driverObj.SwitchTo().DefaultContent();
            driverObj.Manage().Window.Maximize();
            commonWebMethods.MouseOver(driverObj, By.XPath(Game_Control.RealPlayGame_Xpath), By.XPath(Game_Control.PayBtn_RealPlay_Xpath), WebCommonMethods.MouseOperations.MouseOver, FrameGlobals.reloadTimeOut, "MouseOver on the Game Failed, {Error In :Game_Control.multiWheelGame_Xpath}");
            commonWebMethods.Click(driverObj, By.XPath(Game_Control.PayBtn_RealPlay_Xpath), "Real play button for the game not found ,{Error In :Game_Control.multiWheelRealPlay_Xpath}");

        }

        /// <summary>
        /// Author:Anand
        /// 
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void Verify_WithdrawalBlock(IWebDriver driverObj)
        {
            try
            {
                BaseTest.AddTestCase("Verifying withdrawal in portal", "withdrawaleblock should be applied");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(60));
                //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                if (!driverObj.FindElement(By.XPath("//div[@id='payment-heading']//span")).Text.Contains("SELECT WITHDRAW METHOD"))
                {
                    BaseTest.Fail("Withdrawal block not applied to customer");
                }
                BaseTest.Pass();

            }
            catch (Exception e) { BaseTest.Assert.Fail("Failed to open 'Banking' Page" + e.Message.ToString()); }
        }

        public void ComparingWalletBalances(IWebDriver driverObj)
        {
            System.Threading.Thread.Sleep(3000);
          
                commonWebMethods.Click(driverObj, By.XPath(MyAcctPage.MyAcct_History_XP), "Account History link not found", 0, false);
                System.Threading.Thread.Sleep(3000);
                BaseTest.AddTestCase("Get balance amt from each wallet in My Account page", "Balance should be retrived successfully");

                string gamesBeforeVal = wAction.GetText(driverObj, By.XPath("//tr[contains(string(),'Games')]/td[2]/div"), "Games value not found", false);
                string gamingBeforeVal = wAction.GetText(driverObj, By.XPath("//tr[contains(string(),'Main Wallet')]/td[2]/div"), "Main Wallet value not found", false);
                string exchangeBeforeVal = wAction.GetText(driverObj, By.XPath("//tr[contains(string(),'Exchange')]/td[2]/div"), "Exchange value not found", false);

                BaseTest.Pass();
                driverObj.Close();
                driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[0].ToString());
                driverObj.Navigate().Refresh();
                System.Threading.Thread.Sleep(5000);

                String portalWindow = OpenCashier(driverObj, false);

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                System.Threading.Thread.Sleep(3000);

                BaseTest.AddTestCase("Get balance amt from each wallet in Banking page", "Balance should be retrived successfully");

                string gamesAfterVal2 = wAction.GetText(driverObj, By.XPath("//tr[td[contains(text(),'Games')]]/td[2]"), "Games value not found", false);
                string gamingAfterVal3 = wAction.GetText(driverObj, By.XPath("//tr[td[contains(text(),'Main Wallet')]]/td[2]"), "Main Wallet value not found", false);
                string exchangeAfterVal4 = wAction.GetText(driverObj, By.XPath("//tr[td[contains(text(),'Exchange')]]/td[2]"), "Exchange value not found", false);
                BaseTest.Pass();

                BaseTest.AddTestCase("Verify Games wallet balance:" + gamesBeforeVal, "Games wallet balance in Account history and Transfer should equal");
                BaseTest.Assert.IsTrue(gamesBeforeVal.Trim().Replace(" ", "") == gamesAfterVal2.Trim(), "Games wallet balance in Account history and Transfer page are not equal");
                BaseTest.Pass();

                BaseTest.AddTestCase("Verify Gaming wallet balance:" + gamingBeforeVal, "Main Wallet wallet balance in Account history and Transfer should be equal");
                BaseTest.Assert.IsTrue(gamingBeforeVal.Trim().Replace(" ", "") == gamingAfterVal3.Trim(), "Main Wallet wallet balance in Account history and Transfer page are not equal");
                BaseTest.Pass();

                BaseTest.AddTestCase("Verify Exchange wallet balance:" + exchangeBeforeVal, "Exchange wallet balance in Account history and Transfer should be equal");
                BaseTest.Assert.IsTrue(exchangeBeforeVal.Trim().Replace(" ", "") == exchangeAfterVal4.Trim(), "Exchange wallet balance in Account history and Transfer page are not equal");
                BaseTest.Pass();
            
        }

        public void Verify_Sports_BetHistory(IWebDriver driverObj, string eventname)
        {
            BaseTest.AddTestCase("Verify account history for bet placed", "Entry should be displayed");
      
                
                driverObj.Manage().Window.Maximize();
                commonWebMethods.Click(driverObj, By.XPath(MyAcctPage.MyAcct_History_XP), "Account History link not found", 0, false);

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));


                wAction.WaitUntilElementPresent(driverObj, By.LinkText("Refresh balance viewer"));
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));


                commonWebMethods.Click(driverObj, By.XPath(MyAcctPage.SportsBetHistory_XP), "Account sports bet History link not found", 0, false);
                System.Threading.Thread.Sleep(3000);

                BaseTest.Assert.IsTrue(commonWebMethods.GetText(driverObj, By.XPath(MyAcctPage.EventNameInHistory_XP), "Sports bet entry not found", false).Contains(eventname), "Entry not found for bet placed");
                BaseTest.Pass();
          
        }

        public void MyAccount_ModifyPreferences(IWebDriver driverObj, bool EnableAutoTopUp, string odds, bool SaveChanges)
        {

            ISelenium sel = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
            sel.Start();
            BaseTest.AddTestCase("Modify preferences of customer in Myaccount page", "Modified preferences of customer in Myaccount page");
            System.Threading.Thread.Sleep(5000);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));

            wAction.Click(driverObj, By.Id("betting-preferences"), "Unable to locate Preference tab in MyAccount page");
            wAction.WaitforPageLoad(driverObj);


            bool flag = wAction.IsElementPresent(sel, By.Id("bettingViewOdds"));
            wAction.SelectDropdownOption(sel, By.Id("bettingViewOdds"), odds, "Unable to locate Odd selector, dropdown");

            if (SaveChanges)
            {
                wAction.Click(sel, By.Id("betting-pref-save"), "Unable to locate Save button in preference tab");
                wAction.WaitforPageLoad(driverObj);
                System.Threading.Thread.Sleep(5000);
                if (!wAction.GetText(sel, By.Id("betting-preference-portlet"), "No success message is found after changing the preferences").Contains("Preferences were saved successfully"))
                { BaseTest.Fail("No success message is found after changing the preferences"); }
            }
            else
            {
                wAction.Click(driverObj, By.Id("betting-pref-cancel"), "Unable to locate Cancel button in preference tab");
                wAction.WaitforPageLoad(driverObj);
                IWebElement selVal = new SelectElement(driverObj.FindElement(By.Id("bettingViewOdds"))).SelectedOption;
                if (!selVal.Text.Contains("Decimal")) { BaseTest.Fail("Cancel button is not functioning as expected"); }
            }
            BaseTest.Pass();
        }

        /// <summary>
        /// Author:Anand C
        /// Withdraw  fund into wallets - netteller
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public void VerifyWithdraw_Limit_Portal(IWebDriver driverObj, string minVal, string maxVal)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Withdraw amount from wallet", "Amount should be withdrawed from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);
            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.VerifyWithdraw_Limit(driverObj, minVal, maxVal), "Amount not deducted after withdraw");
            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);
        }

        /// <summary>
        /// Author:Anand C
        /// Withdraw  fund into wallets - netteller
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">acct details</param>
        public string FetchBalance_Portal_Homepage(IWebDriver driverObj)
        {
            string balance = "0.00";
        
                    wAction.Click(driverObj,By.XPath("//span[contains(text(),'Show')]"));
                    System.Threading.Thread.Sleep(1000);
                    balance = wAction.GetText(driverObj, By.Id("balance"), "Unable to locate balance in portal Home Page");
                    double bal = StringCommonMethods.ReadDoublefromString(balance);
                    balance = bal.ToString();
                
            
            return balance;
        }





        public bool MyAccount_VerifyFreeBet(IWebDriver driverObj, string bonusname, string freeBetAmount)
        {
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "free_bets_lnk", "Free bet link not found", FrameGlobals.reloadTimeOut, false);
            //Validate the amount 
            string amountStr = "//tr[td[contains(text(),'" + freeBetAmount + "')] and td[contains(text(),'" + bonusname + "')]]";

            //  bool flag = wAction.IsElementPresent(driverObj, By.XPath(amountStr));
            return wAction.IsElementPresent(driverObj, By.XPath(amountStr));
        }

        public void WalletTransfer(IWebDriver driverObj, MyAcct_Data acctData, string fromWallet, string toWallet)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.depositWallet + " wallet to  " + acctData.withdrawWallet, "Transfer Amount should be success from the selected wallet");
            String portalWindow = OpenCashier(driverObj, false);

            BaseTest.AddTestCase("Validating transfer from wallet:" + fromWallet.ToString() + " to Towallet: " + toWallet, "Transfer Should be successfull");
            acctData.depositWallet = fromWallet;
            acctData.withdrawWallet = toWallet;
            acctData.wallet1 = fromWallet;
            acctData.wallet2 = toWallet;

            Framework.BaseTest.Assert.IsTrue(RepositoryCommon.CommonTransferWithdraw_Netteller_PT(driverObj, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");

        }
        public string AddRandomSelectionTBetSlip_CheckBet(IWebDriver driverObj, string amount = "1")
        {
            string eventN = null;
            BaseTest.AddTestCase("Add a random selection and placebet", "Placebet should be successfull");
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            if (!commTest.AddRandomEvent_Bestlip_sports(driverObj))
            {
                BaseTest.Fail("Selection not added to betslip");
            }
            else
            {

                wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "single_stake_box", amount, "Stake box not found", 0, false);
                eventN = wAction.GetText(driverObj, By.XPath(Sportsbook_Control.BetSlip_SingleEventName));
                wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "check_btn", "Check button not found inside betslip", 0, false);
                System.Threading.Thread.Sleep(4000);

            }
            BaseTest.Pass();
            return eventN;
        }
        public string AddRandomSelectionTBetSlip_NoCheckBet(IWebDriver driverObj, string amount = "1")
        {
            string eventN = null;
            BaseTest.AddTestCase("Add a random selection and placebet", "Placebet should be successfull");
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            if (!commTest.AddRandomEvent_Bestlip_sports(driverObj))
            {
                BaseTest.Fail("Selection not added to betslip");
            }
            else
            {

                wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "single_stake_box", amount, "Stake box not found", 0, false);
                eventN = wAction.GetText(driverObj, By.XPath(Sportsbook_Control.BetSlip_SingleEventName));
                //  wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "check_btn", "Check button not found inside betslip", 0, false);
                //  System.Threading.Thread.Sleep(4000);

            }
            BaseTest.Pass();
            return eventN;
        }

        public void Search_CheckBet_Sports(IWebDriver driverObj, string eventName, string amount = "1")
        {

            BaseTest.AddTestCase("Add a random selection and placebet", "Placebet should be successfull");
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

            if (!commTest.AddRandomEvent_Bestlip_sports(driverObj))
            {
                BaseTest.Fail("Selection not added to betslip");
            }
            else
            {
                wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "single_stake_box", amount, "Stake box not found", 0, false);
                wAction.WaitforPageLoad(driverObj);
                wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "check_btn", "Check button not found inside betslip", 0, false);
                System.Threading.Thread.Sleep(4000);

            }
            BaseTest.Pass();
        }
        /// <summary>
        /// Author:Roopa        
        /// </summary>
        /// <param name="driverObj">broswer</param>        
        public void Ecom_MyAcct_Enable_AutoTopUP(IWebDriver driverObj)
        {
            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            wAction.Click(driverObj, By.LinkText("My Account"), "My Account link not found", FrameGlobals.reloadTimeOut, false);
            BaseTest.AddTestCase("Enable AutoTop up checkbox", "Auto top up checkbox should be enabled");
            wAction.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "My Account pop up window not opened", FrameGlobals.elementTimeOut);
            driverObj.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(20));
            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Login_Banner_Msgbox_PT");


            wAction.Click(driverObj, By.LinkText("Preferences"), "Preferences tab not found", 0, false);
            wAction.WaitUntilElementPresent(driverObj, By.XPath("//span[input[@id='bettingAutoTopUp']]"));

            bool isEnabled = wAction.IsElementPresent(driverObj, By.XPath("//span[input[@id='bettingAutoTopUp' and @class='checked']]"));
            if (!isEnabled)
            {
                wAction.Click(driverObj, By.XPath("//span[contains(text(),'Enable Auto Top-up')]"), "Unable to click on Auto top up check box");
                wAction.Click(driverObj, By.Id("betting-pref-save"), "Save Preference button not found", 0, false);
                wAction.WaitforPageLoad(driverObj);
                System.Threading.Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.Id("betting-pref-success")), "Preference saving Failed");
            }

            BaseTest.Pass("Done");
            wAction.BrowserClose(driverObj);
            wAction.WaitAndMovetoPopUPWindow(driverObj, portalWindow, "Main Window not found");
        }



        public bool Verify_Deposit_NonAv(IWebDriver driverObj, MyAcct_Data acctData)
        {
            String portalWindow = OpenCashier(driverObj, false);

            //    return RepositoryCommon.CheckDepositForNon_AVCustomer(driverObj, acctData, portalWindow);
            return RepositoryCommon.CommonDeposit_Netteller_Non_AV(driverObj, acctData);

        }
        /// <summary>
        /// Author : Roopa
        /// </summary>

        public bool WalletTransferAV(IWebDriver driverObj, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verify the Banking / My Account Links to transfer the amount from " + acctData.depositWallet + " wallet to  " + acctData.withdrawWallet, "Transfer Amount should be success from the selected wallet");
            String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

            OpenCashier(driverObj, false);

            BaseTest.AddTestCase("Validating transfer from wallet:" + acctData.depositWallet.ToString() + " to Towallet: " + acctData.withdrawWallet, "Transfer Should be successfull");
            // acctData.depositWallet = fromWallet;
            // acctData.withdrawWallet = toWallet;
            acctData.wallet1 = acctData.depositWallet;
            acctData.wallet2 = acctData.withdrawWallet;

            return RepositoryCommon.FundTransfer_Non_AV(driverObj, acctData, acctData.depositAmt);

        }

        //=================================================================================================
        //new 2015 jun
        //=================================================================================================

        public enum EventType
        {
            Single,
            Double,
            Forecast,
            Tricast,
            EachWay,
            Handicap,
            BIR,
            Trixie,
            Yankee,
            Candian,
            Lucky63,
            Treble,
            Accumulator
        }

        public string AddSelections_toBetslip(IWebDriver driverObj, string eventID, string eventClass, EventType type, string amount = null, int numberofSelections = 1, bool checkbet = false, int betslipSelections = 0, string amount2 = null)
        {

            BaseTest.AddTestCase("Add given selection to betslip from this event", "Add betslip should be successfull");
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();


            if (type == EventType.BIR)
                wAction.OpenURL(driverObj, FrameGlobals.SportsURL + "BetInPlay?EvId=" + eventID, "Page failed to load:" + FrameGlobals.SportsURL + "BetInPlay?EvId=" + eventID, 40);
            else if (eventClass.Contains("football"))
                wAction.OpenURL(driverObj, FrameGlobals.SportsURL + eventClass + "-e" + eventID, "Page failed to load:" + FrameGlobals.SportsURL + "football/english/" + eventClass + "-e" + eventID, 40);

            else
                wAction.OpenURL(driverObj, FrameGlobals.SportsURL + "Navigation?byocList=" + eventClass + "&evListFilter=" + eventID, "Page failed to load: " + FrameGlobals.SportsURL + "Navigation?byocList=" + eventClass + "&evListFilter" + eventID, 40);


            System.Threading.Thread.Sleep(8000);
            List<IWebElement> odds;

            odds = wAction.ReturnWebElements(driverObj, By.XPath("//td[contains(@class,'odds')]//a"), "No selections found", 0, false);

            int count = 1;
            int retry = 0;
            if (odds.Count > 0)
            {
                foreach (IWebElement odd in odds)
                    if (odd.Text.ToString() != string.Empty)
                    {
                    above:
                        wAction.ClickByWebElement(odd, "Unable to click");
                        System.Threading.Thread.Sleep(5000);
                        if (!wAction.IsElementPresent(driverObj, wAction.returnLocatorObject(ORFile.Betslip, wActions.locatorType.id, "check_btn")))
                            System.Threading.Thread.Sleep(6000);
                        if (!wAction.IsElementPresent(driverObj, wAction.returnLocatorObject(ORFile.Betslip, wActions.locatorType.id, "check_btn")))
                            if (retry++ < 2)
                                goto above;

                        if (count++ >= numberofSelections)
                            break;
                    }
                wAction.IsElementPresent(driverObj, wAction.returnLocatorObject(ORFile.Betslip, wActions.locatorType.id, "check_btn"));
            }
            else
                BaseTest.Assert.Fail("No selections present in this event");

            string betslipCount = wAction._GetText(driverObj, ORFile.Betslip, wActions.locatorType.id, "BetSlip_count", "Betslip count text not found", false);

            if (betslipSelections == 0)
            {
                if (!betslipCount.Contains(numberofSelections.ToString()))
                    BaseTest.Assert.Fail("Given number of events not added");
            }
            else
                if (!betslipCount.Contains(betslipSelections.ToString()))
                    BaseTest.Assert.Fail("Given number of events not added");

            if (amount != null)
            {
                if (type == EventType.Single || type == EventType.BIR)
                    wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "single_stake_box", amount, "Stake box not found", 0, false);

                else if (type == EventType.Handicap)
                {
                    wAction.Click(driverObj, By.PartialLinkText("More Info"));
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//div[contains(text(),'Asian Line Handicap')]")), "Handicap bet not displayed in betslip");
                    wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "single_stake_box", amount, "Stake box not found", 0, false);

                }
                else if (type == EventType.Trixie)
                {
                    if (betslipSelections == 3)
                    {
                        wAction.Clear(driverObj, By.XPath("//div[contains(string(),'Trix')]/div/div/input[@type='text']"), "Trixie stake box not found");
                        wAction.Type(driverObj, By.XPath("//div[contains(string(),'Trix')]/div/div/input[@type='text']"), amount, "Trixie stake box not found");
                        wAction.Clear(driverObj, By.XPath("//div[contains(string(),'Patent')]/div/div/input[@type='text']"), "Trixie stake box not found");
                        wAction.Type(driverObj, By.XPath("//div[contains(string(),'Patent')]/div/div/input[@type='text']"), amount2, "Trixie stake box not found");

                    }

                }
                else if (type == EventType.Yankee)
                {
                    if (betslipSelections == 4)
                    {
                        wAction.Clear(driverObj, By.XPath("//div[contains(string(),'Yankee')]/div/div/input[@type='text']"), "Yankee stake box not found");
                        wAction.Type(driverObj, By.XPath("//div[contains(string(),'Yankee')]/div/div/input[@type='text']"), amount, "Yankee stake box not found");

                        wAction.Clear(driverObj, By.XPath("//div[contains(string(),'Lucky')]/div/div/input[@type='text']"), "Lucky15 stake box not found");
                        wAction.Type(driverObj, By.XPath("//div[contains(string(),'Lucky')]/div/div/input[@type='text']"), amount2, "Lucky stake box not found");
                    }

                }
                else if (type == EventType.Candian)
                {
                    if (betslipSelections == 5)
                    {
                        wAction.Clear(driverObj, By.XPath("//div[contains(string(),'Canadian')]/div/div/input[@type='text']"), "Candian stake box not found");
                        wAction.Type(driverObj, By.XPath("//div[contains(string(),'Canadian')]/div/div/input[@type='text']"), amount, "Candian stake box not found");

                        wAction.Clear(driverObj, By.XPath("//div[contains(string(),'Lucky')]/div/div/input[@type='text']"), "Lucky31 stake box not found");
                        wAction.Type(driverObj, By.XPath("//div[contains(string(),'Lucky')]/div/div/input[@type='text']"), amount2, "Lucky31 stake box not found");
                    }

                }
                else if (type == EventType.Lucky63)
                {
                    if (betslipSelections == 6)
                    {
                        wAction.Clear(driverObj, By.XPath("//div[contains(string(),'Heinz')]/div/div/input[@type='text']"), "Heinz stake box not found");
                        wAction.Type(driverObj, By.XPath("//div[contains(string(),'Heinz')]/div/div/input[@type='text']"), amount, "Heinz stake box not found");

                        wAction.Clear(driverObj, By.XPath("//div[contains(string(),'Lucky')]/div/div/input[@type='text']"), "Lucky63 stake box not found");
                        wAction.Type(driverObj, By.XPath("//div[contains(string(),'Lucky')]/div/div/input[@type='text']"), amount2, "Lucky63 stake box not found");
                    }

                }

                else if (type == EventType.EachWay)
                {
                    wAction.Click(driverObj, By.Id("ew0"), "Eachway option not found");
                    wAction._Type(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "single_stake_box", amount, "Stake box not found", 0, false);

                }


                else if (type == EventType.Forecast)
                {
                    wAction.Click(driverObj, By.XPath("//div/div[label[text()='Forecast']]/input"), "Forecast check box not found", 0, false);
                    System.Threading.Thread.Sleep(2000);
                    wAction.Clear(driverObj, By.XPath("//div[div[label[text()='Forecast']]]/div[input[@type='text']]/input[@type='text']"), "Forecast stake box not found");
                    wAction.Type(driverObj, By.XPath("//div[div[label[text()='Forecast']]]/div[input[@type='text']]/input[@type='text']"), amount, "Forecast stake box not found");
                }


                if (checkbet)
                {
                    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "check_btn", "Check button not found inside betslip", 0, false);
                    System.Threading.Thread.Sleep(4000);
                }
            }
            string eventN = wAction.GetText(driverObj, By.XPath(Sportsbook_Control.BetSlip_SingleEventName));

            BaseTest.Pass();
            return eventN;
        }
        public void AddSelections_toBetslip_Nelson(IWebDriver driverObj, string eventID, string eventName, string eventClass, EventType type, string amount = null, int numberofSelections = 1, int betslipSelections = 0, string amount2 = null)
        {

            BaseTest.AddTestCase("Add given selection to betslip from this event", "Add betslip should be successfull");
            Ladbrokes_IMS_TestRepository.Common commTest = new Ladbrokes_IMS_TestRepository.Common();

           // if (eventClass.Contains("football") || eventClass.Contains("horse"))
                wAction.OpenURL(driverObj, FrameGlobals.SportsURL + eventClass + "/" + eventName + "/" + eventID + "/", "Page failed to load:" + FrameGlobals.SportsURL + "football/english/" + eventClass + "-e" + eventID, 40);

           //else
           //    wAction.OpenURL(driverObj, FrameGlobals.SportsURL + "BetInPlay?EvId=" + eventID, "Page failed to load:" + FrameGlobals.SportsURL + "BetInPlay?EvId=" + eventID, 40);



            System.Threading.Thread.Sleep(2);
            List<IWebElement> odds;
            if (type == EventType.Forecast)
            {
                wAction.Click(driverObj, By.XPath(Sportsbook_Control.betslip.Forcast_Tab), "Forecast tab not found", 0, false);
                List<IWebElement> forecast = wAction.ReturnWebElements(driverObj, By.XPath(Sportsbook_Control.betslip.Forcast_Selections_XP), "Forecast selections not found", 0, false);
                List<IWebElement> tricast = wAction.ReturnWebElements(driverObj, By.XPath(Sportsbook_Control.betslip.Tricast_Selections_XP), "Tricast selections not found", 0, false);
                forecast[1].Click(); 
                System.Threading.Thread.Sleep(1000); 
                forecast[0].Click();
                System.Threading.Thread.Sleep(1000); 
                tricast[1].Click();
                System.Threading.Thread.Sleep(1000); 
                tricast[2].Click();
                System.Threading.Thread.Sleep(1000); 
                tricast[0].Click();
                System.Threading.Thread.Sleep(1000);
                BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Sportsbook_Control.betslip.ForecastAndTricast_Notification_XP), "Selection message not found").Contains("1 Reverse Forecast bet and 6 Combination Tricast bet"), "1 Reverse Forecast bet and 6 Combination Tricast bet message not found");
                wAction.Click(driverObj, By.XPath(Sportsbook_Control.betslip.AddtoBetslip),"Unable to find add to betslip button");
                BaseTest.Assert.IsTrue( wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.betslip.Placebet_XP)),"Betslip not having selections");
            }
            else
            {
                odds = wAction.ReturnWebElements(driverObj, By.XPath(Sportsbook_Control.oddList), "No selections found", 0, false);

                int count = 1;
                int retry = 0;
                if (odds.Count > 0)
                {
                    foreach (IWebElement odd in odds)
                        if (odd.Text.ToString() != string.Empty)
                        {
                        above:
                            wAction.ClickByWebElement(odd, "Unable to click");
                            System.Threading.Thread.Sleep(5000);
                            if (!wAction.IsElementPresent(driverObj, By.XPath("//input[@placeholder='Stake']")))
                                System.Threading.Thread.Sleep(6000);
                            if (!wAction.IsElementPresent(driverObj, By.XPath("//input[@placeholder='Stake']")))
                                if (retry++ < 2)
                                    goto above;

                            if (count++ >= numberofSelections)
                                break;
                        }
                    wAction.IsElementPresent(driverObj, By.XPath(Sportsbook_Control.betslip.Placebet_XP));
                }
                else
                    BaseTest.Assert.Fail("No selections present in this event");
            }

            string betslipCount = wAction.GetText(driverObj, By.Id("betslip-indicator"), "Betslip count text not found", false);

            if (betslipSelections == 0)
            {
                if (!betslipCount.Contains(numberofSelections.ToString()))
                    BaseTest.Assert.Fail("Given number of events not added");
            }
            else
                if (!betslipCount.Contains(betslipSelections.ToString()))
                    BaseTest.Assert.Fail("Given number of events not added");

            if (amount != null)
            {
                if (type == EventType.Single || type == EventType.BIR)
                    wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Single_Stake_XP), amount, "Stake box not found", 0, false);

                else if (type == EventType.Handicap)
                {
                   
                    BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//div[contains(text(),'Asian Line Handicap')]")), "Handicap bet not displayed in betslip");
                    wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Single_Stake_XP), amount, "Stake box not found", 0, false);

                }
                else if (type == EventType.Treble)
                {
                    if (betslipSelections == 4)
                    {
                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Treble_Stake_XP), "Treble stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Treble_Stake_XP), amount, "Treble stake box not found");

                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Accumulator_Stake_XP), "Accumulator stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Accumulator_Stake_XP), amount2, "Accumulator stake box not found");
                    }
                }
                else if (type == EventType.Double)
                {
                    if (betslipSelections == 2)
                    {
                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Double_Stake_XP), "Doubles stake box not found",0,false);
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Double_Stake_XP), amount, "Doubles stake box not found");
                        
                    }
                }
                else if (type == EventType.Trixie)
                {
                    if (betslipSelections == 3)
                    {
                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Trixie_Stake_XP), "Trixie stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Trixie_Stake_XP), amount, "Trixie stake box not found");
                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Patent_Stake_XP), "Patent stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Patent_Stake_XP), amount2, "Patent stake box not found");

                    }

                }
                else if (type == EventType.Yankee)
                {
                    if (betslipSelections == 4)
                    {
                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Yankee_Stake_XP), "Yankee stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Yankee_Stake_XP), amount, "Yankee stake box not found");

                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Lucky_Stake_XP), "Lucky15 stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Lucky_Stake_XP), amount2, "Lucky stake box not found");
                    }

                }
                else if (type == EventType.Candian)
                {
                    if (betslipSelections == 5)
                    {
                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Canadian_Stake_XP), "Candian stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Canadian_Stake_XP), amount, "Candian stake box not found");

                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Lucky_Stake_XP), "Lucky31 stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Lucky_Stake_XP), amount2, "Lucky31 stake box not found");
                    }

                }
                else if (type == EventType.Lucky63)
                {
                    if (betslipSelections == 6)
                    {
                        
                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Heinz_Stake_XP), "Heinz stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Heinz_Stake_XP), amount, "Heinz stake box not found");

                        wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Lucky_Stake_XP), "Lucky63 stake box not found");
                        wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Lucky_Stake_XP), amount2, "Lucky63 stake box not found");
                    }

                }

                else if (type == EventType.EachWay)
                {
                    wAction.Click(driverObj, By.Id("betslip-eachway-single-0"), "Eachway option not found");
                    wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Single_Stake_XP), amount, "Stake box not found", 0, false);

                }


                else if (type == EventType.Forecast)
                {
                    wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Forecast_Stake_XP), "Forecast stake box not found");
                    wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Forecast_Stake_XP), amount, "Forecast stake box not found");
                    wAction.Clear(driverObj, By.XPath(Sportsbook_Control.betslip.Tricast_Stake_XP), "Tricast stake box not found");
                    wAction.Type(driverObj, By.XPath(Sportsbook_Control.betslip.Tricast_Stake_XP), amount2, "Tricast stake box not found");
        
                }


                //if (checkbet)
                //{
                //    wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.id, "check_btn", "Check button not found inside betslip", 0, false);
                //    System.Threading.Thread.Sleep(4000);
                //}
            }


            BaseTest.Pass();

        }

        public void placeBet_Nelson(IWebDriver driverObj, EventType type = EventType.Single)
        {
            BaseTest.AddTestCase("Placebet and verify success msg", "Successfully placed bets?");
            System.Threading.Thread.Sleep(3000);
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.betslip.Placebet_XP));

            if (type == EventType.BIR)
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait)), "Betslip wait for BIR did not appear");
            //   wAction.WaitUntilElementDisappears(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait));
            
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                wAction.WaitUntilElementPresent(driverObj, By.Id("receiptDoneButton"));
            string msg = wAction.GetText(driverObj, By.XPath("id('betslip-container')//span[@class='top-message']"), "Betslip success msg not found", false);



            BaseTest.Assert.IsTrue(msg.ToLower().Contains("bet placed successfully") || msg.ToLower().Contains("placed successfully"), "Bet not placed");
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj,By.XPath("//span[@class='receipt-receiptno-label']")),"Receipt not found");


            BaseTest.Pass();
        }
        public void placeBet(IWebDriver driverObj, EventType type = EventType.Single)
        {
            BaseTest.AddTestCase("Placebet and verify success msg", "Successfully placed bets?");
            System.Threading.Thread.Sleep(3000);
            wAction._Click(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "place_bet_button");

            if (type == EventType.BIR)
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait)), "Betslip wait for BIR did not appear");
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            string msg = wAction.GetText(driverObj, By.XPath("id('lbr_betslip')/fieldset/div[2]"), "Betslip success msg not found", false);
            BaseTest.Assert.IsTrue(msg.Contains("bet has now been placed") || msg.Contains("placed successfully"), "Bet not placed");


            BaseTest.Pass();
        }
        public void placeBet_Fail(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Placebet and verify error msg", "Bet should not be placed");
            wAction.Click(driverObj, By.XPath(Sportsbook_Control.betslip.Placebet_XP));

            wAction.WaitUntilElementDisappears(driverObj, By.XPath(Sportsbook_Control.BetSlip_wait));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            string msg = wAction.GetText(driverObj, By.XPath("//li[@class='bet-error']"),"No Error msg found");
            BaseTest.Assert.IsTrue(msg.Contains("Please deposit additional funds to place this bet"), "Bet not placed");
            
            BaseTest.Pass();
        }


        public string OpenRegistrationPage(IWebDriver driverObj, bool switchback = false)
        {
            return RepositoryCommon.OpenRegistrationPage(driverObj,switchback);
        }
        public string OpenCashier(IWebDriver driverObj, bool switchBack = false)
        {

            return RepositoryCommon.OpenCashier(driverObj, switchBack);
        }
        public string OpenMyAcct(IWebDriver driverObj, bool switchback = false)
        {
            return RepositoryCommon.OpenMyAcct(driverObj, switchback);
        }


        //****************************IMS suite*************************
        public void Registration_Page_sourceId(IWebDriver driverObj, ref Registration_Data regData, string sourceId)
        {


          
                BaseTest.AddTestCase("Open Registration Window", "Registration Window Should be opened Successfully");
                String portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();

                commonWebMethods.WaitAndMovetoPopUPWindow(driverObj, driverObj.WindowHandles.ToArray()[1].ToString(), "Unable to locate registration page");

                BaseTest.Pass("Registration page opened");
                commonWebMethods.WaitforPageLoad(driverObj);
                System.Threading.Thread.Sleep(5000);

                BaseTest.Assert.IsTrue(driverObj.Url.Contains(sourceId), "Source Id is not displayed in the URL");
          
        }
        public void CasinoTabIncludedInOtherProducts(IWebDriver driverObj, string locatorName, int category)
        {
            BaseTest.AddTestCase("Verify casino tab included in " + locatorName + " products", "Casino link found successfully");

            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, locatorName, "Products link " + locatorName + " is not found", FrameGlobals.reloadTimeOut, false);

            System.Threading.Thread.Sleep(1000);

            if (category == 2) //////li[@class=' Casino']
            {
                wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "casino_tab_lnk", "Casino Link not found in " + locatorName, FrameGlobals.reloadTimeOut, false);
            }
            else if (category == 1) ////a[@title='Casino']
            {
                wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "casino_tab_in_sports", "Casino Link not found in " + locatorName, FrameGlobals.reloadTimeOut, false);
            }
            else if (category == 3) //////span[contains(text(),'Casino']
            {
                wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "casino_tab_in_lottos", "Casino Link not found " + locatorName, FrameGlobals.reloadTimeOut, false);
            }
            System.Threading.Thread.Sleep(1000);

            BaseTest.Pass("Poker link found");
        }


        public void Verify_HeaderLink(IWebDriver driverObj, string product="PT")
        {

            BaseTest.AddTestCase("Verify header items", "All informations are displayed as expected");
            if (product == "PT")
            {
                BaseTest.AddTestCase("Verify Help Link", "Help link should be present and submenu are displayed as expected");
                wAction.ClickAndMove(driverObj, By.XPath("//a[@title='Help']"), 30, "Help link not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.LinkText("Responsible Gambling")), "Responsible Gambling link not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.LinkText("Help Centre")), "Help Centre link not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.LinkText("Rules")), "Rules link not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.LinkText("Banking")), "Banking link not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.LinkText("Shop Locator")), "Shop Locator link not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.LinkText("Restricted Territories")), "Restricted Territories link not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//a[text()='Terms and Conditions']")), "Terms and Conditions link not found");
                BaseTest.Pass();
                wAction.PageReload(driverObj);
                wAction.WaitforPageLoad(driverObj);
                BaseTest.AddTestCase("Verify Contact us Link", "Contact Us link should be present and submenu are displayed as expected");
                wAction.ClickAndMove(driverObj, By.XPath("//a[@title='Contact us']"), 15, "Contact link not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//div[contains(text(),'Contact Us')]")), "Contact US text not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//*[text()='Sportsbook customer support']")), "Sportsbook customer support not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//*[text()='Gaming customer support']")), "Gaming customer support not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//*[text()='Exchange customer support']")), "Exchange customer support not found");
                BaseTest.Pass();
                wAction.PageReload(driverObj);
                wAction.WaitforPageLoad(driverObj);
                System.Threading.Thread.Sleep(2000);
                BaseTest.AddTestCase("Verify date Link", "Date link should be present as expected");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//span[@data-timeformat='HH:mm:ss _&&_X']")), "Date not found or format incorrect (ex:12:27:24 GMT+5.5)");
                BaseTest.Pass();
                BaseTest.AddTestCase("Verify remember me Link", "Remember Me link should be present as expected");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.Name("remember-me")), "remember-me not found or format incorrect (ex:12:27:24 GMT+5.5)");
                BaseTest.Pass();
            }
            BaseTest.Pass();
        }
    }
}
