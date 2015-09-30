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
using System.Globalization;

namespace Ladbrokes_IMS_TestRepository
{
    public class SeamLessWallet
    {

        Common RepositoryCommon = new Common();
        wActions wAction = new wActions();
        BaseTest baseTest = new BaseTest();
        AccountAndWallets AnW = new AccountAndWallets();
       


        //=========================================================
        public void Cancel_Withdraw(IWebDriver driverObj, MyAcct_Data acctData, bool closeWindow = true, bool inWithdrawPage = false)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to Cancel the Withdraw amount from wallet", "Withdrawed Amount should be Cancelled from the selected wallet");
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";
            string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
            driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());

            if (!inWithdrawPage)
            {
                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
            }
            string temp = wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
            
            double beforeVal = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                 wAction.Click(driverObj,By.XPath( Cashier_Control_SW.CancelWithdraw), "CancelWithdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

                 BaseTest.AddTestCase("Check if any Withdraw transaction availabel to cancel", "Minimum one withdraw request should be present");
               //  wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Cancel_btn",Keys.Enter, "No Withdraw request available / Cancel button not found", 0, false);
                 List<IWebElement> ele = wAction.ReturnWebElements(driverObj, By.XPath("//button[contains(text(),'Cancel')]"), "No Pending withdrawals", 0, false);
                 ele[0].Click();
                 BaseTest.Pass();
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "CancelConfirmation_Dlg", "CancelConfirmation dialog not found", 0, false);
                 wAction.Click(driverObj, By.LinkText("Go back"), null, 0, false);

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                 
                // double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString().Replace('£', ' ').Trim());
                 temp = wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 double AfterVal = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");
          
            
            BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before Cancellation:" + beforeVal + ", Amount after Cancellation:" + AfterVal, "Amount should be calculated accordingly");
            BaseTest.Pass();


            BaseTest.Assert.IsTrue(AfterVal == beforeVal + double.Parse(acctData.depositAmt), "Cancelled withdraw amount not added back");
             
                 if (closeWindow)
                 driverObj.Close();
                
               

            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);


        }
       public void Withdraw_Netteller(IWebDriver driverObj, MyAcct_Data acctData,bool closeWindow=true)
        {

            BaseTest.AddTestCase("Verify the Banking / My Account Links to the Withdraw amount from wallet", "Withdrawed Amount should be Cancelled from the selected wallet");
         
            string withWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";
            string portalWindow = driverObj.WindowHandles.ToArray()[0].ToString();
            driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
  

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
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferFrom_cmb", acctData.withdrawWallet, "destinationWallet_cmb not found");
            

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Success_Dlg", "Successfull message did not appear", 0, false);
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Confirmation_Dlg", "Confirmation_Dlg button not found");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction.Click(driverObj, By.LinkText("Refresh Balances"));
            wAction.WaitforPageLoad(driverObj);
            double AfterVal = 0;
            temp = wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
            if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                AfterVal = StringCommonMethods.ReadDoublefromString(temp);
            else
                BaseTest.Fail("Wallet value is null/Blank");


            driverObj.SwitchTo().DefaultContent();

            if (closeWindow)
                driverObj.Close();
                 BaseTest.AddTestCase("Wallet:" + acctData.withdrawWallet + " Transaction Amount:" + acctData.withdrawWallet + " => Amount Before Cancellation:" + beforeVal + ", Amount after Cancellation:" + AfterVal, "Amount should be calculated accordingly");
                 BaseTest.Pass();
                 BaseTest.Assert.IsTrue(AfterVal == beforeVal - double.Parse(acctData.depositAmt), "Cancelled withdraw amount not added back");
           
                 if (closeWindow)
                 driverObj.Close();
                

            BaseTest.Pass();
            driverObj.SwitchTo().Window(portalWindow);


        }
       public void ValidateWalletBalance_AcctHistory(IWebDriver driverObj, string SingleWallet, string amt)
       {
           BaseTest.AddTestCase("Verify balance after the transaction", "Balance should be updated as expected");
           string portal = driverObj.WindowHandles.ToArray()[0].ToString();

           driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
           wAction.WaitforPageLoad(driverObj);
           string SingleWalletPath = "//tr[td[div[contains(text(),'" + SingleWallet + "')]]]/td[2]";


           wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
           wAction.Click(driverObj, By.XPath(MyAcctPage.MyAcct_History_XP), "Account History link not found", 0, false);
           wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));

           string temp = wAction.GetText(driverObj, By.XPath(SingleWalletPath), SingleWallet + " Wallet value not found", false);
           double val = 0;
           if (StringCommonMethods.ReadDoublefromString(temp) != -1)
               val = StringCommonMethods.ReadDoublefromString(temp);

           double Balance = double.Parse(amt);
           BaseTest.Assert.IsTrue((val == Balance), "Single wallet balance not matching , Expected:" + Balance + " ;Actual:" + val);

           driverObj.Close();
           driverObj.SwitchTo().Window(portal);
           BaseTest.Pass();
       }
       public void AddFreeBet(IWebDriver driverObj,string Promo)
       {
           string portal = driverObj.WindowHandles.ToArray()[0].ToString();
           BaseTest.AddTestCase("Add Freebet to the customer", "Freebet should be added");
           driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
           wAction.WaitforPageLoad(driverObj);
           
           wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
           wAction.Click(driverObj, By.LinkText(MyAcctPage.MyAcct_Free_Bets_lnk), "Freebet link not found");
           wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
           wAction.Clear(driverObj, By.Name(MyAcctPage.Freebet_promotionCode_name), "Promo code box not found", 0, false);
           
           wAction.Type(driverObj, By.Name(MyAcctPage.Freebet_promotionCode_name), Promo,"Promo code box not found");
           wAction.Click(driverObj, By.XPath(MyAcctPage.Freebet_Submit_XP), "Submit btn not found");
           BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath(MyAcctPage.Freebet_Success_XP)), "Success message not found");


           driverObj.Close();
           driverObj.SwitchTo().Window(portal);
           BaseTest.Pass();
       }

       public void ValidateFreeBet(IWebDriver driverObj, string BetName, string Promo)
       {
           string portal = driverObj.WindowHandles.ToArray()[0].ToString();
           BaseTest.AddTestCase("Validate Freebet in the customer", "Freebet should be added");
           driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
           wAction.WaitforPageLoad(driverObj);

           wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
           wAction.Click(driverObj, By.LinkText(MyAcctPage.MyAcct_Free_Bets_lnk), "Freebet link not found");
           wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));


           List<IWebElement> FreebetRow = wAction.ReturnWebElements(driverObj, By.XPath(MyAcctPage.Freebet_reflection_XP), "No freebet row found", 0, false);
           string name = FreebetRow[0].Text; string date = FreebetRow[1].Text;
           
           BaseTest.Assert.IsTrue(name == BetName, "Betname not matching");
           DateTime dateValue;
           bool flag= DateTime.TryParse(date,out dateValue);
           BaseTest.Assert.IsTrue(flag
                              , "Expiry date not in expected format/blank");

           driverObj.Close();
           driverObj.SwitchTo().Window(portal);
           BaseTest.Pass();
       }


        //==========================================================
        public void Verify_SingleWallet_Cashier(IWebDriver driverObj, string ListOfWallets, string SingleWallet, double Balance)
        {

            BaseTest.AddTestCase("Verify Cashier for Single Wallet", "Single wallet should be displayed in all places");
            String portal = AnW.OpenCashier(driverObj);
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
            driverObj.SwitchTo().Window(portal);
            BaseTest.Pass();

        }
        public void Verify_SingleWallet_MyAcct(IWebDriver driverObj, string ListOfWallets, string SingleWallet, double Balance)
        {

            BaseTest.AddTestCase("Verify My Acct for Single Wallet", "Single wallet should be displayed in all places");
            if (!driverObj.Url.ToLower().Contains("sports"))
            wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", noWait: false);

            //wAction.Click(driverObj, By.XPath("//a[text()='My Account']"), "Welcome text not found", 0, false);
            wAction.Click(driverObj, By.LinkText("My Account"), "My Account link not found");
            string portal = driverObj.WindowHandles.ToArray()[0].ToString();

            driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
            wAction.WaitforPageLoad(driverObj);
            string SingleWalletPath = "//tr[td[span[contains(text(),'" + SingleWallet + "')]]]/td[2]/span";


            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
            wAction.Click(driverObj, By.XPath(MyAcctPage.MyAcct_History_XP), "Account History link not found", 0, false);
            wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
          
             System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
           List<IWebElement> listOfWall = wAction.ReturnWebElements(driverObj, By.XPath("//table[@id='account-history-table']//td[1]/span"), "History table loaded in my acct page");

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
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name("username")), "Customer might not have logged off successfully please check");
                BaseTest.Pass();

                BaseTest.AddTestCase("Verify session for Casino", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.CasinoURL, "Casino load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name("username")), "Customer might not have logged off successfully please check");
                BaseTest.Pass();
                BaseTest.AddTestCase("Verify session for Bingo", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.BingoURL, "Bingo load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Name("username")), "Customer might not have logged off successfully please check");
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
                wAction.Click(driverObj, By.XPath(Sportsbook_Control.LoggedinMenu_XP), "Sports user menu not found",0,false);
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
               BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath(Games_Control.depositLink)), "Games not logged in");
                BaseTest.Pass();
            }
            if (sites.Contains("B"))
            {

                BaseTest.AddTestCase("Verify session for backgammon", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.BackgammonURL, "Backgammon load failed", 60);
                Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.LinkText("Logout")), "BackGammon not logged in");
                BaseTest.Pass();
            }
            if (sites.Contains("E"))
            {

                BaseTest.AddTestCase("Verify session for ecom", "User should be logged in");
                wAction.OpenURL(driverObj, FrameGlobals.EcomURL, "Ecomm load failed", 60);
                BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "Logout"), "Ecomm not logged in");
                BaseTest.Pass();
            }
            driverObj.Close();
            driverObj.SwitchTo().Window(main);
        }

        
    }
}