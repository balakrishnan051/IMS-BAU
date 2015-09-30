using System.Diagnostics;
using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Framework;
using System;
//using ICE.ActionRepository;
using System.Globalization;
using ICE.ObjectRepository;
using ICE.DataRepository.Vegas_IMS_Data;
using OpenQA.Selenium.Interactions;
using ICE.DataRepository;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;
using Ladbrokes_IMS_TestRepository;

namespace Telebet_Suite
{
    public class Common
    {
        WebCommonMethods commonWebMethods = new WebCommonMethods();
        wActions wAction = new wActions();
        Ladbrokes_IMS_TestRepository.Common common = new Ladbrokes_IMS_TestRepository.Common();

        public enum BetEventType
        {
            Single,
            Double,
            Forecast,
            Tricast,
            Trebele,
            EachWay,
            Handicap,
            BIR,
            Trixie,
            Yankee,
            Candian,
            Lucky63
        }


        // ICE.DataRepository.Vegas_IMS_Data.Registration_Data registration_data = new ICE.DataRepository.Vegas_IMS_Data.Registration_Data();

        public void SearchCustomer(IWebDriver tb2Driver, string userName)
        {
            BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).Clear();
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            wAction.Click(tb2Driver, By.XPath("id('modalButtons')/a"));
            BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(tb2Driver,ICE.ObjectRepository.ORFile.Telebet,wActions.locatorType.id,"customername"),"Customer search failed");
            BaseTest.Pass();

        }
        public void LogoutCustomer(IWebDriver tb2Driver)
        {
            BaseTest.AddTestCase("Logout customer in Telebet", "Customer should be logged out successfully");
            wAction.Click(tb2Driver, By.Id("cancel_call_tab_list_item"), "Cancel button not found",0,false);
            wAction.Click(tb2Driver, By.Id("okBtn"), "confirmation button not found",0,false);
            
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementDisappears(tb2Driver, By.Id("accNoValueTxtBox")), "Customer logout failed");
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.Id(Telebet_Control.searchAcct_ID)), "Search accout box did not appear");
            BaseTest.Pass();

        }

        public void SearchSelfExCustomer(IWebDriver tb2Driver, string userName)
        {
            BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).Clear();
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver,By.XPath("id('popText')/p[contains(text(),'Your account is currently excluded. If you believe this is incorrect, please e-mail')]"))
                , "Customer search failed");
            BaseTest.Pass();

        }
        public void SearchLockedCustomer(IWebDriver tb2Driver, string userName)
        {
            BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).Clear();
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath(Telebet_Control.LockedCustMsg_XP))
                , "Customer search failed");
            BaseTest.Pass();

        }
        public void SearchFrozenCustomer(IWebDriver tb2Driver, string userName)
        {
            BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).Clear();
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath(Telebet_Control.FrozenCustMsg_XP))
                , "Customer search failed");
            BaseTest.Pass();

        }
        public void Search_Customer_GeneralError(IWebDriver tb2Driver, string userName,string err)
        {
            BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).Clear();
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath("//p[contains(text(),'"+err+"')]"))
                , "Customer search failed");
            BaseTest.Pass();

        }
        public void SearchValidLoyalityCustomer(IWebDriver tb2Driver, string userName)
        {
            BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).Clear();
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.GetText(tb2Driver, By.Id("popText")).Contains("Loyalty Customer") 
                , "Customer search failed");
            BaseTest.Pass();

        }
        public void SearchInValidCustomer(IWebDriver tb2Driver, string userName)
        {
            BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be show proper error successfully");
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).Clear();
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath(Telebet_Control.NoCustFoundMsg_XP))
                , "Customer search failed");
            BaseTest.Pass();

        }
        public void SearchClosedCustomer(IWebDriver tb2Driver, string userName)
        {
            BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).Clear();
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath("//p[text()='Player is closed "+userName+"']"))
                , "Customer search failed");
            
            BaseTest.Pass();

        }
        public void CreditCardDepositTB2(IWebDriver tb2Driver , MyAcct_Data acctData)
        {
            String portalWindow = tb2Driver.WindowHandles.ToArray()[0].ToString();
            //BaseTest.AddTestCase("Searching customer " + userName + " in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id("deposit_tab_list_item")).Click();
            //tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(userName);
            //tb2Driver.FindElement(By.Id(Telebet_Control.searchAcct_ID)).SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            //BaseTest.Assert.IsTrue(wAction._IsElementPresent(tb2Driver, ICE.ObjectRepository.ORFile.Telebet, wActions.locatorType.id, "acctMenuDep"), "Failed to navigate to Deposit page");
            //BaseTest.Pass();
            //tb2Driver.SwitchTo().Frame("acctIframe");
            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));
            
            
           BaseTest.Assert.IsTrue(common.CommonDeposit_CreditCard_PT(tb2Driver, acctData,portalWindow),"Amount not added/Error in deposit");
            

        }
        public void ValidateHTTPSTelebet(IWebDriver tb2Driver)
        {

            BaseTest.AddTestCase("Customer should be taken to HTTPS page on accessing any page from Telebet having sensitive data", "Sensitive page should not be having HTTPS");
            AccountAndWallets anw = new AccountAndWallets();

            String portalWindow = tb2Driver.WindowHandles.ToArray()[0].ToString();
            wAction.Click(tb2Driver, By.Id("deposit_tab_list_item"), "Deposit link not found");
            
            System.Threading.Thread.Sleep(3000);
            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));
            BaseTest.Assert.IsTrue(wAction._IsElementPresent(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab"),"Cashier page not loaded");
            BaseTest.Assert.IsTrue(tb2Driver.Url.Contains("https"), "URL is not HTTPS when accessing deposit page:" + tb2Driver.Url);



            tb2Driver.SwitchTo().DefaultContent();
            wAction.Click(tb2Driver, By.LinkText("Account Overview"), "Account Overview link not found");
            BaseTest.Assert.IsTrue(tb2Driver.Url.Contains("https"), "URL is not HTTPS when accessing My ACct page:" + tb2Driver.Url);


            BaseTest.Pass();
        }

        public void DepositRestrictTelebet(IWebDriver tb2Driver, MyAcct_Data acctData, string netAcc, string error, string site = null)
        {

            BaseTest.AddTestCase("Verify the customer is able to perform withdraw and not deposit", "Deposit should not be successfull");
            AccountAndWallets anw = new AccountAndWallets();

            String portalWindow = tb2Driver.WindowHandles.ToArray()[0].ToString();
            wAction.Click(tb2Driver, By.Id("deposit_tab_list_item"), "Deposit link not found");
            System.Threading.Thread.Sleep(3000);
            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));


            Framework.BaseTest.Assert.IsTrue(common.CommonWithdraw_Netteller_PT(tb2Driver, acctData, acctData.depositAmt, false, false, "Games"), "Amount not deducted after withdraw");
            
            
          

            #region Deposit
           // tb2Driver.SwitchTo().Window(tb2Driver.WindowHandles.ToArray()[1].ToString());
            wAction._Click(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

            BaseTest.AddTestCase("Verify that Payment method is added to the customer but cannot deposit", "The customer should have the payment option added to it but gets error msg during deposit");

            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));
            String nettellerImg = "//table[contains(@class,'data')]//tbody[contains(@id,'accounts')]//td[contains(text(),'" + netAcc + "')]";
            wAction.Click(tb2Driver, By.XPath(nettellerImg), "Netteller Image not found", 0, false);
            wAction._Type(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
            wAction._Clear(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
            wAction._Type(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");

            wAction._SelectDropdownOption_ByPartialText(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
            BaseTest.Assert.IsTrue(wAction.GetText(tb2Driver, By.XPath("id('globalErrors')//span"), "Restrict message not found", false).Contains(error), "Restrict login error message is incorrect");

            BaseTest.Pass();
            #endregion

            BaseTest.Pass();
        }

        public void AllWallets_TransferTB2(IWebDriver tb2Driver, MyAcct_Data acctData, string wallets, string table)
        {
            List<string> wallet = wallets.ToString().Split(';').ToList<string>();
            List<string> walletTbl = table.ToString().Split(';').ToList<string>();
            tb2Driver.FindElement(By.Id("deposit_tab_list_item")).Click();
            System.Threading.Thread.Sleep(3000);
            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));
          

           
            String portalWindow = tb2Driver.WindowHandles.ToArray()[0].ToString();

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

                        Framework.BaseTest.Assert.IsTrue(common.CommonTransferWithdraw_Netteller_PT(tb2Driver, acctData, acctData.depositAmt), "Amount not added/deducted after transfer");
                        BaseTest.Pass();
                        wAction._Click(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                       // tb2Driver.SwitchTo().DefaultContent();

                    }
            
           
            
        }
        public void WithdrawRelated(IWebDriver tb2Driver, MyAcct_Data acctData,string min,string max)
        {
          //  String portalWindow = tb2Driver.WindowHandles.ToArray()[0].ToString();
            tb2Driver.FindElement(By.Id("deposit_tab_list_item")).Click();
            System.Threading.Thread.Sleep(3000);
            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));
            BaseTest.Assert.IsTrue(common.VerifyWithdraw_Limit(tb2Driver, min, max), "Amount not added/Error in deposit");
          //  common.CommonWithdrawMoreThanBalance(tb2Driver, acctData.depositWallet,true);
            wAction._Click(tb2Driver, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
            common.CommonWithdraw_Netteller_PT(tb2Driver, acctData, acctData.depositAmt, false);
            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));
            common.CommonCancelWithdraw_Netteller_PT(tb2Driver, acctData, acctData.depositAmt, false,true);

        }
        public void SubCommonWithdrawMoreThanBalance(IWebDriver driverObj, string wallet)
        {
            double depositAmount = 0; string withdrawAmountTextBox_Id = "amount";
            string depositWalletPath = "//tr[td[contains(text(),'" + wallet + "')]]/td[2]";
            string balAmount = wAction.GetText(driverObj, By.XPath(depositWalletPath), wallet + "Wallet value not found", false).ToString().Trim();

            if (balAmount.Contains("£"))
                double.TryParse(balAmount.Replace("£", ""), out depositAmount);
            else if (balAmount.Contains("$"))
                double.TryParse(balAmount.Replace("$", ""), out depositAmount);
            else
                double.TryParse(balAmount, out depositAmount);

            // double.TryParse(balAmount, out depositAmount);

            depositAmount = depositAmount + 10;

            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "destinationWallet_cmb", wallet, "destinationWallet_cmb not found");
            driverObj.FindElement(By.Id(withdrawAmountTextBox_Id)).Clear();
            commonWebMethods.Type(driverObj, By.Id(withdrawAmountTextBox_Id), depositAmount.ToString(), "Deposit Amount text box not found, {Error in:depositAmountTextBox_Id}");
            
            if (!wAction.GetText(driverObj, By.XPath("//span[contains(@name,'playtech.cashier.generic.deposit.error')]")).Contains("The amount exceeds allowed limits"))
            {
                BaseTest.Fail("No correct error message is displayed on entring the amount more than maximum withdrawal amount");

            }
           
        }
        public void AccountHistory_Transfer(IWebDriver tb2Driver, MyAcct_Data acctData)
        {
            BaseTest.AddTestCase("Verifying acct history page in Telebet", "All transfer history should be recorded");
            String portalWindow = tb2Driver.WindowHandles.ToArray()[0].ToString();
            wAction.Click(tb2Driver, By.Id("deposit_tab_list_item"), "Deposit button not found in Customer page", FrameGlobals.reloadTimeOut, false);
            wAction.Click(tb2Driver, By.Id("acctMenuCustStmt"), "Acct statement Link not found in Customer page", 0, false);
            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));

            wAction.SelectDropdownOption(tb2Driver, By.Id("hist_state_time"),"Today", "History Time dropdown not found / Value = Today not present in the list", 0, false);

            wAction.Click(tb2Driver, By.Id("histSubmitButton"), "View statement Button not found in History page");

            if (acctData.depositWallet.Contains("Sports") && acctData.depositWallet.Contains("Games"))
            {
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath("//div[string()='Transfer from Games wallet to Sports wallet' and contains(@class,'accHistTrans')]"))
                , "Transaction for Transfer from Games wallet to Sports wallet not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath("//div[string()='Transfer from Sports wallet to Games wallet' and contains(@class,'accHistTrans')]"))
                  , "Transaction for Transfer from Sports wallet to Games wallet not found");
            }

            if (acctData.depositWallet.Contains("Games") && acctData.depositWallet.Contains("Vegas"))
            {
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath("//div[string()='Transfer from Games wallet to Vegas wallet' and contains(@class,'accHistTrans')]"))
               , "Transaction for Transfer from Games wallet to Vegas wallet not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath("//div[string()='Transfer from Vegas wallet to Games wallet' and contains(@class,'accHistTrans')]"))
        , "Transaction for Transfer from Vegas wallet to Games wallet not found");

            }

            if (acctData.depositWallet.Contains("Sports") && acctData.depositWallet.Contains("Vegas"))
            {
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath("//div[string()='Transfer from Sports wallet to Vegas wallet' and contains(@class,'accHistTrans')]"))
            , "Transaction for Transfer from Sports wallet to Vegas wallet not found");
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(tb2Driver, By.XPath("//div[string()='Transfer from Vegas wallet to Sports wallet' and contains(@class,'accHistTrans')]"))
                , "Transaction for Transfer from Vegas wallet to Sports wallet not found");
            }

          
        
           


           
           
           
            BaseTest.Pass();
        }
        public string GetBalance(IWebDriver tb2Driver)
        {
            BaseTest.AddTestCase("Read Header Balance in Telebet", "Balance should be present");
            string val = wAction.GetText(tb2Driver, By.Id("accBalValue"), "Balance header not found", false);
            BaseTest.Pass();
            return val;
            

           
        }
  
        public void Verify_SingleWallet_Cashier(IWebDriver driverObj, string ListOfWallets, string SingleWallet, double Balance)
        {

            BaseTest.AddTestCase("Verify Cashier for Single Wallet", "Single wallet should be displayed in all places");
            wAction.Click(driverObj, By.Id("deposit_tab_list_item"), "Deposit button not found in Customer page", FrameGlobals.reloadTimeOut, false);
          
           
          //  driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
            wAction.WaitforPageLoad(driverObj);
            string SingleWalletPath = "//tr[td[contains(text(),'" + SingleWallet + "')]]/td[2]";

            wAction.WaitAndMovetoFrame(driverObj, By.Id("acctIframe"));

            if (wAction.IsElementPresent(driverObj, By.XPath("id('sourceWallet')/option")))
            {
                List<IWebElement> depDropDown = wAction.ReturnWebElements(driverObj, By.XPath("id('sourceWallet')/option"));
                foreach (IWebElement options in depDropDown)
                    if (ListOfWallets.Contains(options.Text.Trim()))
                        continue;
                    else
                        BaseTest.Fail("Invalid wallet name found:" + options.Text.Trim());
            }


            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
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

            
            
            BaseTest.Pass();

        }

        public void Place_SingleBet(IWebDriver tb2Driver,string amount)
        {
            BaseTest.AddTestCase("Place a Bet successfully in Telebet", "Bet should be placed successfully");
            wAction.Click(tb2Driver,By.XPath(Telebet_Control.FootBall_XP),"Footbal link not found",0,false);
            System.Threading.Thread.Sleep(2000);
            List<IWebElement> expand = wAction.ReturnWebElements(tb2Driver,By.XPath(Telebet_Control.EventExpand_Colapse_XP),"Events not found in bet page",0,false);
            foreach(IWebElement xp in expand)
                xp.Click();

            System.Threading.Thread.Sleep(2000);
             List<IWebElement> Selections = wAction.ReturnWebElements(tb2Driver,By.XPath(Telebet_Control.RandomSelection_XP),"Selections not found in bet page");
            
            for(int i=1 ; i<10;i++)
            {
                Selections[i].Click();
                  System.Threading.Thread.Sleep(3000);
                if(wAction.IsElementPresent( tb2Driver,By.Id(Telebet_Control.stakeAmt_ID)))
                    break;
            }


            wAction.Type(tb2Driver, By.Id(Telebet_Control.stakeAmt_ID), amount);
             wAction.Click(tb2Driver,By.Id(Telebet_Control.CheckBet_ID),"CheckBet btn not found");
            System.Threading.Thread.Sleep(2000);
           
             wAction.Click(tb2Driver,By.Id(Telebet_Control.PlaceBet_ID),"Placebet Btn not found");
             System.Threading.Thread.Sleep(1000);
             wAction.Click(tb2Driver, By.Id(Telebet_Control.okButton_ID));
             System.Threading.Thread.Sleep(5000);
             if (!wAction.GetText(tb2Driver, By.XPath(Telebet_Control.Receipt_XP), "Betreceipt did not appear").Contains("Your bet has been placed"))
             {
                 wAction.Click(tb2Driver, By.XPath("//*[text()='Override All']"));
                 wAction.Click(tb2Driver, By.ClassName("popupSubmit"));
             }
            BaseTest.Assert.IsTrue(wAction.GetText(tb2Driver, By.XPath(Telebet_Control.Receipt_XP), "Betreceipt did not appear").Contains("Your bet has been placed"), "'Your bet has been placed' msg not found");
             wAction.Click(tb2Driver, By.Id(Telebet_Control.okButton_ID));

            BaseTest.Pass();           



        }

        public void Place_SpecificBet(IWebDriver tb2Driver, string amount, string eventName, BetEventType ET, string marketPlace = null, string amount2 = null, int numberOfSelection = 1, int selectedCount = 1,bool placeBet=true)
        {
            BaseTest.AddTestCase("Place a Bet successfully in Telebet", "Bet should be placed successfully");

            if (ET == BetEventType.BIR)
            {
                wAction.Click(tb2Driver, By.LinkText("Bet In Play"), "BIR link not found", 0, false);
                System.Threading.Thread.Sleep(2000);

                wAction.Click(tb2Driver, By.XPath("//div[text()='Football']"), "Football link not found", 0, false);
                wAction.Click(tb2Driver, By.LinkText(eventName), eventName + " link not found", 0, false);
                System.Threading.Thread.Sleep(2000);
            }
            else if (ET == BetEventType.Double || ET == BetEventType.Yankee)
            {
                if(!wAction.IsElementPresent(tb2Driver, By.LinkText(marketPlace)))
                wAction.Click(tb2Driver, By.XPath("//div[a[text()='Horse Racing']]/div[@class='menuArrow']"), "Horse Racing link not found", 0, false);
                wAction.Click(tb2Driver, By.LinkText(marketPlace), marketPlace +"link not found");
                wAction.Click(tb2Driver, By.XPath("//div[@class='marketExpand']/div[contains(text(),'" + eventName + "')]"), eventName + " not found", 0, false);
                               
            }
            else if (ET == BetEventType.Trixie || ET == BetEventType.Trebele)
            {
                if (!wAction.IsElementPresent(tb2Driver, By.LinkText(marketPlace)))
                {
                    wAction.Click(tb2Driver, By.XPath("//div[a[text()='Football']]/div[contains(@class,'menuArrow')]"), "Football link not found", 0, false);
                    System.Threading.Thread.Sleep(3000);
                    wAction.Click(tb2Driver, By.XPath("//div[a[text()='English']]/div[contains(@class,'menuArrow')]"), "English link not found");
                }
                wAction.Click(tb2Driver, By.PartialLinkText(marketPlace), marketPlace + "link not found");
                wAction.Click(tb2Driver, By.XPath("//div[@class='displayrowExpand']//div[contains(text(),'" + eventName + "')]"), eventName + " not found", 0, false);
       
            }
            List<IWebElement> Selections =null;

               if (ET == BetEventType.BIR)
                    Selections = wAction.ReturnWebElements(tb2Driver, By.XPath("//div[contains(@class,'marketPrice')]"), "Selections not found in bet page");
               else if (ET == BetEventType.Trixie || ET == BetEventType.Trebele)
                   Selections = wAction.ReturnWebElements(tb2Driver, By.XPath("//div[@class='displayrowExpand' and div[div[contains(text(),'" + eventName + "')]]]//div[contains(@class,'marketPrice')]"), "Selections not found in bet page");
              
            else
        Selections = wAction.ReturnWebElements(tb2Driver, By.XPath("//div[@class='marketRow' and div[div[contains(text(),'"+eventName+"')]]]//div[contains(@class,'marketPrice')]"), "Selections not found in bet page");

            for (int i = 1; i <= numberOfSelection; i++)
            {
                //IJavaScriptExecutor executor = (IJavaScriptExecutor)tb2Driver;
                //    executor.ExecuteScript("arguments[0].click();", Selections[i]);

                
                Selections[i].Click();
                System.Threading.Thread.Sleep(3000);
                wAction.IsElementPresent(tb2Driver, By.Id(Telebet_Control.stakeAmt_ID));
                   
            }

            List<IWebElement> count = wAction.ReturnWebElements(tb2Driver, By.XPath("//div[@class='betSlipOutcome']"), "betlsip not found", 0, false);
            System.Threading.Thread.Sleep(3000);
            if (count.Count != selectedCount)
                BaseTest.Fail(selectedCount+ " selections not added");

            if(ET==BetEventType.BIR)
                    wAction.Type(tb2Driver, By.Id(Telebet_Control.stakeAmt_ID), amount);
            else if (ET == BetEventType.Double)
            {
                if (selectedCount == 2)
                {
                    wAction.Click(tb2Driver, By.XPath("//div[contains(@class,'betStakeButton betTypeButton') and contains(text(),'DBL')]"), "DBL link not found");
                    System.Threading.Thread.Sleep(1000);
                    wAction.Type(tb2Driver, By.XPath("//div[div[text()='SGL']]//input[contains(@id,'win_stake')]"), amount, "Single stake box not found");
                    wAction.Type(tb2Driver, By.XPath("//div[div[text()='DBL']]//input[contains(@id,'win_stake')]"), amount2, "Double stake box not found");
                }
            }
            else if (ET == BetEventType.Trixie)
            {
                if (selectedCount == 3)
                {
                    wAction.Clear(tb2Driver, By.Id("betTypeInput"), "Bet Type selection dropdown not found");
                    wAction.Type(tb2Driver, By.Id("betTypeInput"),"TRX", "Bet Type selection dropdown not found");
                    wAction.Click(tb2Driver, By.Id("BetTypeTRX"), "Trixie selection dropdown not found");
                    wAction.Clear(tb2Driver, By.Id("betTypeInput"),  "Bet Type selection dropdown not found");
                    wAction.Type(tb2Driver, By.Id("betTypeInput"), "PAT", "Bet Type selection dropdown not found");
                    wAction.Click(tb2Driver, By.Id("BetTypePAT"), "Patent selection dropdown not found");

                    System.Threading.Thread.Sleep(1000);
                    wAction.Click(tb2Driver, By.XPath("//div[div[text()='SGL']]//div[contains(@class,'betTypeRemove')]"), "Single stake box not found");
                    wAction.Clear(tb2Driver, By.XPath("//div[div[text()='TRX']]//input[contains(@id,'win_stake')]"),  "Single stake box not found");
                    wAction.Type(tb2Driver, By.XPath("//div[div[text()='TRX']]//input[contains(@id,'win_stake')]"), amount, "Single stake box not found");
                    wAction.Clear(tb2Driver, By.XPath("//div[div[text()='PAT']]//input[contains(@id,'win_stake')]"),  "Double stake box not found");
                    wAction.Type(tb2Driver, By.XPath("//div[div[text()='PAT']]//input[contains(@id,'win_stake')]"), amount2, "Double stake box not found");
                }
            }
            else if (ET == BetEventType.Trebele)
            {
                if (selectedCount == 4)
                {


                    wAction.Click(tb2Driver, By.XPath("//div[contains(@class,'betStakeButton betTypeButton') and contains(text(),'TBL')]"), "TBL link not found");
                    wAction.Click(tb2Driver, By.XPath("//div[contains(@class,'betStakeButton betTypeButton') and contains(text(),'ACCA')]"), "ACCA link not found");

                    System.Threading.Thread.Sleep(1000);
                    wAction.Click(tb2Driver, By.XPath("//div[div[text()='SGL']]//div[contains(@class,'betTypeRemove')]"), "Single stake box not found");
                    wAction.Clear(tb2Driver, By.XPath("//div[div[text()='TBL']]//input[contains(@id,'win_stake')]"), "TBL stake box not found");
                    wAction.Type(tb2Driver, By.XPath("//div[div[text()='TBL']]//input[contains(@id,'win_stake')]"), amount, "TBL stake box not found");
                    wAction.Clear(tb2Driver, By.XPath("//div[div[text()='ACC4']]//input[contains(@id,'win_stake')]"), "ACC4 stake box not found");
                    wAction.Type(tb2Driver, By.XPath("//div[div[text()='ACC4']]//input[contains(@id,'win_stake')]"), amount2, "ACC4 stake box not found");
                }
            }
            else if (ET == BetEventType.Yankee)
            {
                if (selectedCount == 4)
                {
                    wAction.Clear(tb2Driver, By.Id("betTypeInput"), "Bet Type selection dropdown not found");
                    wAction.Type(tb2Driver, By.Id("betTypeInput"), "YAN", "Bet Type yankee selection dropdown not found");
                    wAction.Click(tb2Driver, By.Id("BetTypeYAN"), "YAN selection dropdown not found");
                    wAction.Clear(tb2Driver, By.Id("betTypeInput"), "Bet Type selection dropdown not found");
                    wAction.Type(tb2Driver, By.Id("betTypeInput"), "L15", "Bet Type lucky15 selection dropdown not found");
                    wAction.Click(tb2Driver, By.Id("BetTypeL15"), "L15 selection dropdown not found");

                    System.Threading.Thread.Sleep(1000);
                    wAction.Click(tb2Driver, By.XPath("//div[div[text()='SGL']]//div[contains(@class,'betTypeRemove')]"), "single stake box not found");
                    wAction.Clear(tb2Driver, By.XPath("//div[div[text()='YAN']]//input[contains(@id,'win_stake')]"), "yankee stake box not found");
                    wAction.Type(tb2Driver, By.XPath("//div[div[text()='YAN']]//input[contains(@id,'win_stake')]"), amount, "Single stake box not found");
                    wAction.Clear(tb2Driver, By.XPath("//div[div[text()='L15']]//input[contains(@id,'win_stake')]"), "lucky15 stake box not found");
                    wAction.Type(tb2Driver, By.XPath("//div[div[text()='L15']]//input[contains(@id,'win_stake')]"), amount2, "lucky15 stake box not found");
                }
            }


            if (placeBet)
            {
                wAction.Click(tb2Driver, By.Id(Telebet_Control.CheckBet_ID), "CheckBet btn not found");
                System.Threading.Thread.Sleep(2000);

                wAction.Click(tb2Driver, By.Id(Telebet_Control.PlaceBet_ID), "Placebet Btn not found");
                System.Threading.Thread.Sleep(1000);
                wAction.Click(tb2Driver, By.Id(Telebet_Control.okButton_ID));
                System.Threading.Thread.Sleep(4000);
                wAction.Click(tb2Driver, By.XPath("//div[@class='popupSubmit' and text()='Place Bet']"));
                System.Threading.Thread.Sleep(2000);
                if (!wAction.GetText(tb2Driver, By.XPath(Telebet_Control.Receipt_XP), "Betreceipt did not appear").Contains("Your bet has been placed"))
                {
                    wAction.Click(tb2Driver, By.XPath("//*[text()='Override All']"));
                    wAction.Click(tb2Driver, By.ClassName("popupSubmit"));
                }
                BaseTest.Assert.IsTrue(wAction.GetText(tb2Driver, By.XPath(Telebet_Control.Receipt_XP), "Betreceipt did not appear").Contains("Your bet has been placed"), "'Your bet has been placed' msg not found");
                wAction.Click(tb2Driver, By.Id(Telebet_Control.okButton_ID));
            }
            BaseTest.Pass();



        }



        //===========================SW===================================================

        public void OpenDepositPage(ref IWebDriver tb2Driver)
        {
            String portalWindow = tb2Driver.WindowHandles.ToArray()[0].ToString();
            BaseTest.AddTestCase("Open Deposit page for the customer in Telebet", "Customer should be searched successfully");
            tb2Driver.FindElement(By.Id("deposit_tab_list_item")).Click();
            System.Threading.Thread.Sleep(3000);
            wAction.WaitAndMovetoFrame(tb2Driver, By.Id("acctIframe"));

        }
    }
}
