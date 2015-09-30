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
using System.Collections.Generic;

namespace IMS_AdminSuite
{

    public class Common
    {
        WebCommonMethods commonWebMethods = new WebCommonMethods();
        wActions wAction = new wActions();
        
        // ICE.DataRepository.Vegas_IMS_Data.Registration_Data registration_data = new ICE.DataRepository.Vegas_IMS_Data.Registration_Data();

       


        public void SearchCustomer(IWebDriver imsDriver, string userName)
        {
            imsDriver.FindElement(By.LinkText("Manage users")).Click();
            imsDriver.FindElement(By.LinkText("User database")).Click();

            imsDriver.SwitchTo().DefaultContent();

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    imsDriver.SwitchTo().Frame("main");
                    break;
                }
                catch (Exception) { }
            } while (varDateTime <= varElapseTime);

            commonWebMethods.Type(imsDriver, By.Id("username"), userName, "Username not found in Login Page", 0, false);
            imsDriver.FindElement(By.Id("submit")).Click();
            commonWebMethods.Click(imsDriver, By.LinkText(userName), "Customer not found in Login Page", 0, false);
        }
        public void CreateNewBonus(IWebDriver imsDriver, string BonusName, string amount)
        {

            BaseTest.AddTestCase("Create a new Bonus in database", "Bonus should be added to the database");
            try
            {
                //imsDriver.Navigate().GoToUrl("https://admin-stg.ladbrokes.com/ims/bexcore/welcome");
                //wAction.WaitAndMovetoFrame(imsDriver, "top", "Top Frame not found", FrameGlobals.elementTimeOut);
                //   wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "tryout_newlook_lnk", "Could not find try out new look link", 0, false);

                wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "templatedb", "Template Database not found", 0, false);

                wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.link, "bonusdb_lnk", "Bonus Database link not found", 0, false);


                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

                wAction._WaitUntilElementPresent(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "newbonustype_cmb");


                wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "casinocode_cmb", "ladbrokesvegasstg", "Casino Code combo not found", 0, false);

                wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "newbonustype_cmb", "Casino manual bonus", "New bonus drop down not found", 0, false);

                wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "createnew_btn", "Create new button not found", 0, false);
                imsDriver.SwitchTo().DefaultContent();

                //commonWebMethods.WaitAndMovetoFrame(imsDriver, "main-content", "No Frame Present in Add bonus page", FrameGlobals.elementTimeOut);
                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

                //commonWebMethods.Clear(imsDriver, By.Id("name"), "Bonus name text box not found", 0, false);
                //commonWebMethods.Type(imsDriver, By.Id("name"), BonusName, "Bonus name text box not found", 0, false);
                wAction._Clear(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "bonusname_txt", "Bonus name text box not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "bonusname_txt", BonusName, "Bonus name text box not found", 0, false);

                //commonWebMethods.Clear(imsDriver, By.Id("maxbonus"), "Max bonus textbox not found", 0, false);
                //commonWebMethods.Type(imsDriver, By.Id("maxbonus"), amount, "Max bonus textbox not found", 0, false);
                wAction._Clear(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "maxbonus_txt", "Max bonus textbox not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "maxbonus_txt", amount, "Max bonus textbox not found", 0, false);

                //commonWebMethods.SelectDropdownOption(imsDriver, By.Id("defaultproduct"), "casino", "Casino product combobox not found", 0, false);
                wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "defaultproduct_cmb", "casino", "Casino product combobox not found", 0, false);

                //commonWebMethods.Click(imsDriver, By.Id("fwtoggle"), "Fiexd Wager checkbox not found");
                wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "fwtoggle_chk", "Fiexd Wager checkbox not found", 0, false);
                //commonWebMethods.Type(imsDriver, By.Id("fixedwageringrequirementamt"), "10", "Fixed wager text box not found");
                wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "fixedwageringrequirementamt_txt", "10", "Fixed wager text box not found", 0, false);

                DateTime dateFormat = DateTime.Now;
                string dateTime = (dateFormat.ToString("yyyy-MM-dd HH:mm"));
                //commonWebMethods.Clear(imsDriver, By.Id("startdate"), "Startdate not found", 0, false);
                //commonWebMethods.Type(imsDriver, By.Id("startdate"), dateTime, "Startdate not found", 0, false);
                wAction._Clear(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "startdate_txt", "Startdate not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "startdate_txt", dateTime, "Startdate not found", 0, false);

                dateFormat = DateTime.Now.AddYears(1);
                dateTime = (dateFormat.ToString("yyyy-MM-dd HH:mm"));
                //commonWebMethods.Clear(imsDriver, By.Id("enddate"), "End date not found", 0, false);
                //commonWebMethods.Type(imsDriver, By.Id("enddate"), dateTime, "End date not found", 0, false);
                wAction._Clear(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "enddate_txt", "End date not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "enddate_txt", dateTime, "End date not found", 0, false);

                //commonWebMethods.SelectDropdownOption(imsDriver, By.Id("gameweightsconfigurationcode"), "test", "Casino product combobox not found", 0, false);
                wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "gameweightsconfigurationcode_cmb", "test", "Casino product combobox not found", 0, false);

                //imsDriver.FindElement(By.Id("action[create]")).Click();
                wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "action_btn", "Button not found", 0, false);
                imsDriver.SwitchTo().Alert().Accept();
                //if (commonWebMethods.IsElementPresent(imsDriver, By.Id("message-error-1")))
                if (wAction._IsElementPresent(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "errormsg_lbl"))
                {
                    if (commonWebMethods.GetText(imsDriver, By.Id("message-error-1")).Contains("Bonus name must be unique"))
                        BaseTest.Pass();
                }
                //else if (commonWebMethods.IsElementPresent(imsDriver, By.Id("message-info-1")))
                else if (wAction._IsElementPresent(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "msginfo_lbl"))
                {
                    if (commonWebMethods.GetText(imsDriver, By.Id("message-info-1")).Contains("Bonus created"))
                        BaseTest.Pass();
                }
                else
                    BaseTest.Fail("Bonus does not exist and not created too");

            }
            catch (Exception) { BaseTest.Fail("Add bonus to the database failed"); }
            finally
            {
                //commonWebMethods.BrowserQuit(imsDriver);
                wAction.BrowserQuit(imsDriver);
            }

        }
        public void CreateNew_RegisterBonus(IWebDriver imsDriver, string BonusName, string PromoName)
        {

            BaseTest.AddTestCase("Create a new register Bonus in database", "Bonus should be added to the database");

            //imsDriver.Navigate().GoToUrl("https://admin-stg.ladbrokes.com/ims/bexcore/welcome");
            //wAction.WaitAndMovetoFrame(imsDriver, "top", "Top Frame not found", FrameGlobals.elementTimeOut);
            //   wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "tryout_newlook_lnk", "Could not find try out new look link", 0, false);

            wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "templatedb", "Template Database not found", 0, false);

            wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.link, "bonusdb_lnk", "Bonus Database link not found", 0, false);


            wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

            wAction._WaitUntilElementPresent(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "newbonustype_cmb");


            wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "casinocode_cmb", "ladbrokesvegasstg", "Casino Code combo not found", 0, false);

            wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "newbonustype_cmb", "Casino automatic bonus", "New bonus drop down not found/Caino Automatic Bonus not found in the drop down list", 0, false);



            //Temp code:

            wAction.Type(imsDriver, By.Id("name"), BonusName, "Bonus name field not found", 0, false);
            wAction.Click(imsDriver, By.Id("inactive"), "Inactive checkbox not found");
            wAction.Click(imsDriver, By.Id("show"), "Search button not found");

            if (wAction.IsElementPresent(imsDriver, By.XPath("//table[@class='result no-wrap']//tr[td[a[contains(text(),'" + BonusName + "')]]]/td[contains(text(),'Active')]")))
            {
                BaseTest.Pass();
                return;
            }
            else if (wAction.IsElementPresent(imsDriver, By.XPath("//table[@class='result no-wrap']//tr[td[a[contains(text(),'" + BonusName + "')]]]/td[contains(text(),'Inactive')]")))
            {
                wAction.Click(imsDriver, By.XPath(" //table[@class='result no-wrap']//tr[td[a[contains(text(),'" + BonusName + "')]]]/td[2]"), "No result found", 0, false);
                wAction.Click(imsDriver, By.Id("action[activate]"), "Activate button not found");

                if (commonWebMethods.GetText(imsDriver, By.Id("message-info-1")).Contains("Bonus activated"))
                    BaseTest.Pass();
                else
                    BaseTest.Fail("Bonus not Activated");
                return;

            }




            wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "createnew_btn", "Create new button not found", 0);
            imsDriver.SwitchTo().DefaultContent();

            //commonWebMethods.WaitAndMovetoFrame(imsDriver, "main-content", "No Frame Present in Add bonus page", FrameGlobals.elementTimeOut);
            wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

            //commonWebMethods.Clear(imsDriver, By.Id("name"), "Bonus name text box not found", 0, false);
            //commonWebMethods.Type(imsDriver, By.Id("name"), BonusName, "Bonus name text box not found", 0, false);
            wAction.Clear(imsDriver, By.Id("name"), "Bonus name text box not found", 0);
            wAction.Type(imsDriver, By.Id("name"), BonusName, "Bonus name text box not found", 0);

            wAction.Clear(imsDriver, By.Id("priority"), "Priority textbox not found", 0);
            wAction.Type(imsDriver, By.Id("priority"), "1", "Priority textbox not found", 0);

            wAction.SelectDropdownOption(imsDriver, By.Id("defaultproduct"), "casino", "Casino product combobox not found", 0);


            //   wAction.Click(imsDriver, By.Id("completiontype"), "Completiontype checkbox not found");

            wAction.Clear(imsDriver, By.Id("conditionname"), "Fixed wager textbox not found", 0);
            wAction.Type(imsDriver, By.Id("conditionname"), PromoName, "Fixed wager text box not found");


            DateTime dateFormat = DateTime.Now;
            string dateTime = (dateFormat.ToString("yyyy-MM-dd HH:mm"));

            wAction._Clear(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "startdate_txt", "Startdate not found", 0);
            wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "startdate_txt", dateTime, "Startdate not found", 0);

            dateFormat = DateTime.Now.AddYears(1);
            dateTime = (dateFormat.ToString("yyyy-MM-dd HH:mm"));

            wAction._Clear(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "enddate_txt", "End date not found", 0);
            wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "enddate_txt", dateTime, "End date not found", 0);

            wAction.SelectDropdownOption(imsDriver, By.Id("bonustrigger"), "Signup", "bonustrigger combobox not found", 0);
            wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "gameweightsconfigurationcode_cmb", "test", "Casino product combobox not found", 0, false);

            //imsDriver.FindElement(By.Id("action[create]")).Click();
            wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "action_btn", "Button not found", 0);
            imsDriver.SwitchTo().Alert().Accept();


            //if (commonWebMethods.IsElementPresent(imsDriver, By.Id("message-error-1")))
            if (wAction._IsElementPresent(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "errormsg_lbl"))
            {
                if (commonWebMethods.GetText(imsDriver, By.Id("message-error-1")).Contains("Bonus name must be unique"))
                    BaseTest.Pass();
                return;
            }
            //else if (commonWebMethods.IsElementPresent(imsDriver, By.Id("message-info-1")))
            else if (wAction._IsElementPresent(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "msginfo_lbl"))
            {
                if (commonWebMethods.GetText(imsDriver, By.Id("message-info-1")).Contains("Bonus created"))
                    BaseTest.Pass();
            }
            else
                BaseTest.Fail("Bonus does not exist and not created too");

            BaseTest.AddTestCase("Promo Code to be created and Bonus to be Activated", "Promo Code should be created and Bonus should be Activated");
            wAction.Click(imsDriver, By.Id("newamount"), "New Condition button not found");
            wAction.SelectDropdownOption(imsDriver, By.Id("currencycode"), "GBP", "currencycode combobox not found");
            wAction.Type(imsDriver, By.Id("bonusamount"), "15", "bonusamount text box not found");
            wAction.Click(imsDriver, By.XPath("//input[@id='completiontype' and @value='immediately']"), "Completiontype checkbox not found");
            wAction.Click(imsDriver, By.Id("submit"), "submit button not found");

            imsDriver.SwitchTo().Alert().Accept();
            wAction.Click(imsDriver, By.Id("action[activate]"), "Activate button not found");

            if (commonWebMethods.GetText(imsDriver, By.Id("message-info-1")).Contains("Bonus activated"))
                BaseTest.Pass();
            else
                BaseTest.Fail("Bonus not Activated");



        }
        public void SelfExclude_Customer(IWebDriver imsDriver, string userName, string period = "6 months")
        {

            BaseTest.AddTestCase("Self Exclude customer", "Self Exclusion successfull");
            //SearchCustomer(imsDriver, userName);
            //SearchCustomer_Newlook(imsDriver, userName);
            wAction.Click(imsDriver, By.Id("selfExclusion_navigation"), "Self exclusion navigation button not found", 0, false);
            wAction._SelectDropdownOption_ByPartialText(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.xpath, "SE_Period_cmb", period, "Selfexclusion period dropdown not found", 0, false);
            commonWebMethods.Click(imsDriver, By.PartialLinkText("Self exclusion"), "Self Exclusion not found/clickable", 0, false);
            commonWebMethods.SelectDropdownOption(imsDriver, By.XPath(IMS_Control_PlayerDetails.selfExType_Dropdown_XP), "Manual", "Self excl type select not found");
            
            commonWebMethods.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.self_Save_XP), "Save Exlusion not found/clickable", 0, false);
            commonWebMethods.WaitUntilElementPresent(imsDriver, By.XPath("//div[contains(text(),'Self exclusion successfully saved')]"));
           BaseTest.Assert.IsTrue(commonWebMethods.WaitUntilElementPresent(imsDriver, By.Id("selfExclusionGrid_row_0")),"Self exclusion for products not added");
            BaseTest.Pass();
        }
        public void Remove_SelfExclusion_Customer(IWebDriver imsDriver)
        {

            BaseTest.AddTestCase("Remove Self Exclusion from customer", "Self Exclusion removal successfull");
            
            wAction.Click(imsDriver, By.Id("selfExclusion_navigation"), "Self exclusion navigation button not found", 0, false);

            for (int i = 0; i < 4; i++)
            {
                List<IWebElement> deleteSelfExList = wAction.ReturnWebElements(imsDriver, By.XPath(IMS_Control_PlayerDetails.selfEx_Delete_XP), "self exclusion not found for the account", 0, false);
                wAction.ClickByWebElement(deleteSelfExList[0], "Unable to click on Delete button");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            }



                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                BaseTest.Assert.IsFalse(commonWebMethods.IsElementPresent(imsDriver, By.Id("selfExclusionGrid_row_0")), "Self exclusion for products not added");
            BaseTest.Pass();
        }
        public string CreateNewCustomer(IWebDriver imsDriver, string strUserName, string strPassword, string CustType = null,string country=null,string email=null,string fname=null)
        {
            ISelenium myBrowser = new WebDriverBackedSelenium(imsDriver, "https://www.google.co.in/");
            myBrowser.Start();
            BaseTest.AddTestCase("Create a new Customer: " + strUserName + "in IMS", "New Customer should be created in IMS");

            string username = "";
           
            string guidPwd = Guid.NewGuid().ToString().Substring(0, 3);


            string strSignificantDate;
            string day = DateTime.Today.Day.ToString(CultureInfo.InvariantCulture);
            string month = DateTime.Today.Month.ToString(CultureInfo.InvariantCulture);
            string year = "1988";
            //DateTime.Today.Year + 1;

            // Getting DDMMYYYY format
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            if (month.Length == 1)
            {
                month = "0" + month;
            }

            strSignificantDate = day + month + year;

            string challenge1 = "Te";
            string guidChlg1 = Guid.NewGuid().ToString().Substring(0, 3);
            challenge1 = guidChlg1 + strUserName;

            string firstName = "Tester" + StringCommonMethods.GenerateAlphabeticGUID();
            string guidFrstNme = Guid.NewGuid().ToString().Substring(0, 2);
            //firstName = firstName + guidFrstNme;

            string challenge2;
            challenge2 = guidChlg1 + challenge1;

            string lastName = "Clark";
            lastName = lastName + StringCommonMethods.GenerateAlphabeticGUID();

            string houseNumTxt = "9";
            houseNumTxt = houseNumTxt + StringCommonMethods.GenerateAlphabeticGUID(); ;

            string address1 = "MTP road";
            address1 = address1 + " " + StringCommonMethods.GenerateAlphabeticGUID(); ;

            var random = new Random();
            string dialCode = random.Next(11, 99).ToString(CultureInfo.InvariantCulture);
            string phoneNumber = Convert.ToInt64(random.Next(999999999, 999999999)).ToString(CultureInfo.InvariantCulture);
            string faxNumber = Convert.ToInt64(random.Next(999999999, 999999999)).ToString(CultureInfo.InvariantCulture);
            System.Threading.Thread.Sleep(100);

            string zipCode = Registration_Data.uk_postcd;

            if(email==null)
             email = "test@playtech.com";
            else
            email = StringCommonMethods.GenerateAlphabeticGUID() + email;

           

                wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "playermgmt_lnk", "Player Management Link not found", 0, false);
                System.Threading.Thread.Sleep(2000);
                imsDriver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(2000);
                wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.partialLink, "RealPlayerDb_lnk", "Real player database Link not found", 0, false);


                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "createplayer_btn", "Create Player Button not found", 0, false);

                imsDriver.SwitchTo().DefaultContent();
                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

                // Entering Required details for Registration
            casino:

                string casinodropdown = (FrameGlobals.projectName != "IP2" ? "ladbrokesvegasstg" : "ladbrokesvegastest");

                 
                //wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "casino_cmb", "1", "casino Option not found", 0, false);
            wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "casino_cmb", casinodropdown, "ladbrokesvegasstg Option not found", 0, false);
           
                
            skin:
            //    wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "skin_cmb", "ladbrokesvegasstg", "ladbrokesvegasstg Option not found", 0, false);
            wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "skin_cmb", casinodropdown, "ladbrokesvegasstg Option not found", 0, false);
                //DOB:
                wAction._SelectDropdownOption_ByValue(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "birthdateyear_cmb", year, "Year Drop down Option not found", 0, false);
                wAction._SelectDropdownOption_ByValue(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "birthdatemonth_cmb", month, "Month Dropdown Option not found", 0, false);
                wAction._SelectDropdownOption_ByValue(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "birthdateday_cmb", day, "Day Dropdown Option not found", 0, false);


            firstname:
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "firstname_txt", "firstname not found", 0, false);
              if(fname==null)
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "firstname_txt", firstName, "firstname not found", 0, false);
              else
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "firstname_txt", fname, "firstname not found", 0, false);
            lastname:
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "lastname_txt", "lastname not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "lastname_txt", lastName, "lastname not found", 0, false);

                wAction._SelectDropdownOption_ByValue(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "AgeVerification_DropDown", "passed", "Age Verification Dropdown Option not found", 0, false);
                if (CustType == "Credit")
                    wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "CreditCust_Chk", "Credit Customer check box not found/Unable to click", 0, false);

            country:
                if(country==null)
                     country = "United Kingdom";
                wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "countrycode_cmb", country, "United Kingdom Option not found", 0, false);

            address:
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "address_txt", "address not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "address_txt", address1, "address not found", 0, false);

            phone:
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "phone_txt", "phone not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "phone_txt", phoneNumber, "phone not found", 0, false);

                //if (FrameGlobals.projectName == "IP2")
                //{
                //    DateTime dateFormat = DateTime.Now;
                //    string dateTime = (dateFormat.ToString("yyMMddHHmmss"));
                //    wAction.Clear(imsDriver, By.Id("authenticationphone"), "authenticationphone not found", 0, false);
                //    wAction.Type(imsDriver, By.Id("authenticationphone"), dateTime, "authenticationphone not found");

                //    wAction.Clear(imsDriver, By.Id("referreradvertiser"), "referreradvertiser not found", 0, false);
                //    wAction.Type(imsDriver, By.Id("referreradvertiser"), "referrer", "referreradvertiser not found");
                //}
            zip:
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "zip_txt", "zip not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "zip_txt", zipCode, "zip not found", 0, false);

            city:
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "city_txt", "city not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "city_txt", address1, "city not found", 0, false);

            email:
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "email_txt", "email not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "email_txt", email, "email not found", 0, false);

            language:
                wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "languagecode_cmb", "English", "English Option not found", 0, false);

            password:
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "password_txt", "password not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "password_txt", strPassword, "password not found", 0, false);

            username:

                 username = strUserName + StringCommonMethods.GenerateAlphabeticGUID();
               
                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "username_txt", "username not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "username_txt", username, "username not found", 0, false);

            currency:
                wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "currencycode_cmb", "GBP", "GBP Option not found", 0, false);

            if (wAction.IsElementPresent(imsDriver, By.Id("clienttype")))
                wAction.SelectDropdownOption(imsDriver, By.Id("clienttype"), "1");

            if (wAction.IsElementPresent(imsDriver, By.Id("clientplatform")))
                wAction.SelectDropdownOption(imsDriver, By.Id("clientplatform"), "1");

            channel:
                wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "clientchannelcode_cmb", "Internet", "Internet Option not found", 0, false);


                if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "createnewplayer_btn"))
                {
                    IWebElement element = imsDriver.FindElement(By.Id("createnewplayer"));
                    new Actions(imsDriver).MoveToElement(element).Build().Perform();
                    //IJavaScriptExecutor executor = (IJavaScriptExecutor)imsDriver;// This is exactly opening the page 
                    //executor.ExecuteScript("arguments[0].click();", imsDriver.FindElement(By.Id("createnewplayer")));//
                    wAction.Click_JavaScript(imsDriver, By.Id("createnewplayer"),"Create player not found",0,false);
                    imsDriver.SwitchTo().Alert().Accept();

                }

                imsDriver.SwitchTo().DefaultContent();
                if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "registereduser_lbl"))
                {
                    if (CustType == "Credit")
                    {
                        if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "CreditCust_Header"))
                            BaseTest.Pass("User Registered Successfully");
                    }
                    else
                        BaseTest.Pass("User Registered Successfully");

                    
                }
                else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "errormsg_lbl"))
                {
                    if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "firstnameerror_msg"))
                    {
                        goto firstname;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "lastnameerror_msg"))
                    {
                        goto lastname;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "addresserror_msg"))
                    {
                        goto address;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "phoneerror_msg"))
                    {
                        goto phone;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "ziperror_msg"))
                    {
                        goto zip;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "cityerror_msg"))
                    {
                        goto city;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "emailerror_msg"))
                    {
                        goto email;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "passworderror_msg"))
                    {
                        goto password;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "usernameerror_msg"))
                    {
                        goto username;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "countrycodeerror_msg"))
                    {
                        goto country;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "skinerror_msg"))
                    {
                        goto skin;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "casinoerror_msg"))
                    {
                        goto casino;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "languagecodeerror_msg"))
                    {
                        goto language;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "currencycodeerror_msg"))
                    {
                        goto currency;
                    }
                    else if (wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "clientchannelcodeerror_msg"))
                    {
                        goto channel;
                    }
                }

                BaseTest.Pass();

         
            return username;
        }
        public void SearchCustomer_Newlook(IWebDriver imsDriver, string userName,bool internalCust = true)
        {
            BaseTest.AddTestCase("Search customer in IMS", "Customer search should work as expected");
          
                //wAction.WaitAndMovetoFrame(imsDriver, "top", "Top Frame not found", FrameGlobals.elementTimeOut);
                // wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.xpath, "tryout_newlook_lnk", "Could not find try out new look link", 0, false);

                imsDriver.SwitchTo().DefaultContent();
                wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "playermgmt_lnk", "Player Management Link not found", 8, false);
                //imsDriver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(2000);
                wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.partialLink, "RealPlayerDb_lnk", "Real player database Link not found", 8, false);

                //imsDriver.SwitchTo().DefaultContent();

                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

                wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "username_txt", "userName not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "username_txt", userName, "userName not found");
               if(internalCust)
                wAction.Type(imsDriver, By.Id("internalaccount"), Keys.Space, "Internal customer checkbox not found");
                wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "signup_time_period", "All", "Signup time period select box not found");
                wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "displayuser_btn", "Display user Button not found", 0, false);
                commonWebMethods.Click(imsDriver, By.LinkText(userName), userName + ": not found in the database", 10, false);

                wAction._WaitUntilElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "signupinfo");

                BaseTest.Assert.IsTrue(wAction._IsElementPresent(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "signupinfo"), "User not found");
                BaseTest.Pass();
                imsDriver.SwitchTo().DefaultContent();

           
        }
        public void SearchCustomer_NotFound(IWebDriver imsDriver, string userName, bool internalCust = true)
        {
            BaseTest.AddTestCase("Search customer in IMS", "Customer search should work as expected");

            imsDriver.SwitchTo().DefaultContent();
            wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "playermgmt_lnk", "Player Management Link not found", 8, false);
        
            System.Threading.Thread.Sleep(2000);
            wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.partialLink, "RealPlayerDb_lnk", "Real player database Link not found", 8, false);

            wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

            wAction._Clear(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "username_txt", "userName not found", 0, false);
            wAction._Type(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "username_txt", userName, "userName not found");
            if (internalCust)
                wAction.Type(imsDriver, By.Id("internalaccount"), Keys.Space, "Internal customer checkbox not found");
            wAction._SelectDropdownOption(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "signup_time_period", "All", "Signup time period select box not found");
            wAction._Click(imsDriver, ORFile.IMSCreateCustomer, wActions.locatorType.id, "displayuser_btn", "Display user Button not found", 0, false);
           
            
            BaseTest.Assert.IsFalse(wAction.IsElementPresent(imsDriver,By.LinkText(userName)), "User found in IMS");
            BaseTest.Pass();
            


        }
        public void AVstatusSet(IWebDriver imsDriver, string status="Passed")
        {
            BaseTest.AddTestCase("Set AV status as:"+status, "AV status should be saved successful");
            wAction.SelectDropdownOption(imsDriver, By.Id("ageverification"), status, "AV dropdown/value not found", 0, false);
            wAction.Click(imsDriver, By.Id("update"), "update info button not found");
            wAction.WaitAndAccepAlert(imsDriver);
           // imsDriver.SwitchTo().Alert().Dismiss();
            wAction.PageReload(imsDriver);
        }
        public void RestrictedIP_LoginMsg(IWebDriver imsDriver)
        {
            BaseTest.AddTestCase("Check for Fraud events in the given customer","Restricted IP event should be added");
            wAction.Click(imsDriver, By.Id("arfraud_navigation"), "update info button not found");
            if(FrameGlobals.projectName=="IP2")
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("id('arfraud')//td[a[contains(text(),'"+IMS_Control_Rules.LoginByIP_Lnk_Test+"')]]")), "IP event not found");
            else
                BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("id('arfraud')//td[a[contains(text(),'" + IMS_Control_Rules.LoginByIP_Lnk_Stage + "')]]")), "IP event not found");
        }
        public void Fraud_EventDetails(IWebDriver imsDriver,string fevent)
        {
            BaseTest.AddTestCase("Check for Fraud events in the given customer", "Fraud event should be added");
            wAction.Click(imsDriver, By.Id("arfraud_navigation"), "update info button not found");

            BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("id('arfraud')//td[a[contains(text(),'" + fevent + "')]]")), fevent+" event not found");
           }
           
        public void FundTransfer(IWebDriver imsDriver, string fromWallet,string toWallet,string Amt="1")
        {
            BaseTest.AddTestCase("Transfer fund from " + fromWallet + " to "+toWallet+" through IMS admin. ", "Fund transfer should be successful");
              wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "fundTransfer_lnk", "Fund Transfer link not found", 0, false);
                System.Threading.Thread.Sleep(2000);
                wAction._SelectDropdownOption_ByPartialText(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "fromWallet_cmb", fromWallet, "From wallet not found", 0, false);
                System.Threading.Thread.Sleep(8000);
                wAction._SelectDropdownOption_ByPartialText(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "toWallet_cmb", toWallet, "From wallet not found", 0, false);
                wAction._Clear(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "amount_txt", "Amount text not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "amount_txt", Amt);
                wAction._SelectDropdownOption_ByPartialText(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "clientChannel_cmb", "Internet", "From wallet not found", 0, false);
                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "makeTransfer_btn", "Make Transfer button not found");
                System.Threading.Thread.Sleep(5000);
                BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.xpath, "success_msg"), "Success message not displayed");
                BaseTest.Pass();

          
        }

        public void FundTransfer_History(IWebDriver imsDriver, string fromWallet, string toWallet, string Amt)
        {
            BaseTest.AddTestCase("Transfer fund from " + fromWallet + " to " + toWallet + " through IMS admin. ", "Fund transfer should be successful");
            wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "fundTransfer_lnk", "Fund Transfer link not found", 0, false);
            System.Threading.Thread.Sleep(2000);
            wAction._SelectDropdownOption_ByPartialText(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "fromWallet_cmb", fromWallet, "From wallet not found", 0, false);
            System.Threading.Thread.Sleep(8000);
            wAction._SelectDropdownOption_ByPartialText(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "toWallet_cmb", toWallet, "From wallet not found", 0, false);
            wAction._Clear(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "amount_txt", "Amount text not found", 0, false);
           // imsDriver.FindElement(By.Id(CashierPage.FundTransfer_Amt_path)).SendKeys(CashierPage.FundTransfer_amt);
            wAction.Type(imsDriver, By.Id(CashierPage.FundTransfer_Transfer_Amt_path_ID), Amt, "not found", 0, true);
           // wAction._Type(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "amount_txt", Amt);
            wAction._SelectDropdownOption_ByPartialText(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "clientChannel_cmb", "Internet", "From wallet not found", 0, false);
            wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "makeTransfer_btn", "Make Transfer button not found");
            System.Threading.Thread.Sleep(5000);
            BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.xpath, "success_msg"), "Success message not displayed");


            //IWebDriver webElement = imsDriver.FindElement(By.XPath(CashierPage.FundTransfer_From_ID));
            //Actions act = new Actions(driverObj);
            wAction.ClickAndMove(imsDriver, By.Id(CashierPage.FundTransfer_From_ID), 5, "Transfer date range not found");
            wAction.Click(imsDriver, By.ClassName(CashierPage.FundTransfer_From_Today), "Today Not found", 0, true);

            wAction.SelectDropdownOption_ByPartialText(imsDriver, By.XPath("id('fundTransferHistoryZone')//div[contains(text(),'Transfer From')]/select"), fromWallet);
            wAction.SelectDropdownOption_ByPartialText(imsDriver, By.XPath("id('fundTransferHistoryZone')//div[contains(text(),'Transfer To')]/select"), toWallet);

            wAction.Click(imsDriver, By.Id(CashierPage.FundTransfer_History_Submit_ID), "Submit button not found", 0, true);
           //wAction.SelectDropdownOption_ByPartialText(imsDriver,By.Id(CashierPage.FundTransfer_Transfer_From_ID),
            //String amt= imsDriver.FindElement(By.XPath(CashierPage.FundTransfer_Amt_path)).ToString();

            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("//table[@id='transferHistoryList_table']/tbody/tr[td[contains(text(),'" +Amt+ "')] and td[a[contains(text(),'Accepted')]]]")), "Transaction history not found");
            BaseTest.Pass();
           
            

        }
        public void AddBonus(IWebDriver imsDriver, string BonusName, string amount)
        {

            BaseTest.AddTestCase("Add Bonus to customer", "Bonus should be added to the customer");
          
                //commonWebMethods.Click(imsDriver, By.XPath(Controls.AdminBase_Controls.addBonus), "Add Bonus Button not found", 0, false);
                wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.xpath, "addbonus_btn", "Add Bonus Button not found", 0, false);
                //commonWebMethods.WaitAndMovetoFrame(imsDriver, "main-content", "No Frame Present in Add bonus page", FrameGlobals.elementTimeOut);
                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                //commonWebMethods.WaitUntilElementPresent(imsDriver, By.Id(Controls.AdminBase_Controls.bonusDropDownID));
                wAction._WaitUntilElementPresent(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "bonusdropdown_txt");
                //commonWebMethods.Type(imsDriver, By.Id(Controls.AdminBase_Controls.bonusDropDownID), BonusName, "Username text not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "bonusdropdown_txt", BonusName, "Username text not found", 0, false);
                //commonWebMethods.Type(imsDriver, By.Id("amount"), amount, "Submit button not found");
                wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "amount_txt", amount, "Amount text not found", 0, false);
                // commonWebMethods.Click(imsDriver, By.Id("submit"), "submit not found", 0, false);
                //imsDriver.FindElement(By.Id("submit")).Click();
                wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "submit_btn", "Submit not found", 0, false);
                imsDriver.SwitchTo().Alert().Accept();
                BaseTest.Pass();

            

        }
        public void SearchCreditCard_Newlook(IWebDriver imsDriver, string cc1, string cc2)
        {
            BaseTest.AddTestCase("Search customer in IMS", "Customer search should work as expected");
            try
            {
                //wAction.WaitAndMovetoFrame(imsDriver, "top", "Top Frame not found", FrameGlobals.elementTimeOut);
                //wAction._Click(imsDriver, ORFile.IMSCommon, wActions.locatorType.xpath, "tryout_newlook_lnk", "Could not find try out new look link", 0, false);
                wAction._Click(imsDriver, ORFile.IMSCommon, wActions.locatorType.id, "playermgmt_lnk", "Player Management Link not found", 0, false);
                imsDriver.SwitchTo().DefaultContent();
                wAction._Click(imsDriver, ORFile.IMSCommon, wActions.locatorType.partialLink, "RealPlayerDb_lnk", "Real player database Link not found", 0, false);

                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                wAction._Click(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "ExpandPersonal", "Expand Personal user link not found", 0, false);
                wAction._SelectDropdownOption_ByValue(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "SignUpTimePeriod_cmb", "all", "Expand Personal user link not found", 0, false);
                wAction._Clear(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "CC_txt", "userName not found", 0, false);
                wAction._Type(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "CC_txt", cc1 + "%" + cc2, "userName not found", 0, false);
                wAction._Click(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "displayuser_btn", "Display user Button not found", 0, false);
                wAction._Click(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.xpath, "CustWithCC", "no record found in the database", 0, false);



                BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.xpath, "signupinfo"), "User not found");
                BaseTest.Pass();

            }
            catch (Exception) { BaseTest.Fail("Search card failed"); }
        }
        public void RemoveCreditCard_Newlook(IWebDriver imsDriver, string cc1, string cc2)
        {

            string RemCC_xPath = "id('creditcards')/table/tbody/tr/td/table/tbody/tr[td[contains(text(),'Ok')] and td[a[contains(text(),'" + cc1 + "') and contains(text(),'" + cc2 + "') ]]] /td/a";

            BaseTest.AddTestCase("Remove Credit card from a customer in IMS", "Credit card should be removed from Customer");
            try
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                wAction.Click(imsDriver, By.XPath(RemCC_xPath), "Given Credit card Not Active/Credit card not found", 0, false);

                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                BaseTest.Assert.IsTrue(wAction._IsElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.xpath, "CreditCardDetails_Page"), "Credit Card details page not loaded");
                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "FreezeCC_btn", "Freeze credit card button not found");
                imsDriver.SwitchTo().Alert().Accept();
                imsDriver.SwitchTo().DefaultContent();
                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                BaseTest.Assert.IsTrue(wAction._IsElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "UnFreezeCC_btn"), "Credit card did not get removed / Unfreeze button not visible");
                //wAction.WaitAndAccepAlert(imsDriver);

                BaseTest.Pass();

            }
            catch (Exception e) { BaseTest.Fail("Remove credit card for the customer failed"); }
        }
        public void Approve_Withdraw_Request(IWebDriver imsDriver,bool cancel=false)
        {
            BaseTest.AddTestCase("Approve customer withdraw request in IMS", "Withdraw requests should be approved");
                 wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "wd_requests_lnk", "Non approved requests link not found", 8, false);
                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.xpath, "recentReq_lnk", "No pending requests found", 8, false);
                System.Threading.Thread.Sleep(3000);
                wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "process_btn", "Process button not found", 8, false);
                System.Threading.Thread.Sleep(5000);
                if (cancel)
                {
                    wAction.SelectDropdownOption(imsDriver, By.Name("handling[0]"), "Decline","Decline value /dropdown not found");
                    wAction.SelectDropdownOption(imsDriver, By.Name("reason__[0]"), "1");
                }
                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "makePmnt_btn", "Make payment button not found", 8, false);
                System.Threading.Thread.Sleep(3000);
                
                    wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "confirmPmnt_btn", "Confirm payment button not found", 8, false);
                    imsDriver.SwitchTo().Alert().Accept();
                
                System.Threading.Thread.Sleep(3000);
            if(!cancel)
                BaseTest.Assert.IsTrue(wAction._IsElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "pay_success_msg"), "Payment not successful");
                BaseTest.Pass();
        }
        public void Verify_Withdraw_Approval(IWebDriver imsDriver)
        {
            BaseTest.AddTestCase("Verify approved withdraw request status in IMS", "Status should be set to waiting");
           
                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "transactions_lnk", "Transactions link not found", 8, false);
                System.Threading.Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction._GetText(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.xpath, "status_lnk", "Transaction status not found", false).Contains("waiting"), "Payment not successful");
                BaseTest.Pass();
         
        }

        public void Approve_WithdrawRequest(IWebDriver imsDriver,string uname)
        {
            BaseTest.AddTestCase("Approve the withdraw request from Transactions", "Status should be set to Approved");
          
                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "transactions_lnk", "Transactions link not found", 8, false);
                System.Threading.Thread.Sleep(3000);
                string timeStamp = wAction.GetText(imsDriver, By.XPath(IMS_Control_PlayerDetails.WaitingWD_Request_XP));
 
               wAction.Click(imsDriver,By.XPath(IMS_Control_PlayerDetails.WaitingWD_Request_XP),"No withdraw request pending",0,false);

               wAction.WaitAndMovetoFrame(imsDriver, IMS_Control_PlayerDetails.Main_Frame_ID, null, FrameGlobals.elementTimeOut);
               wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.approveBtn_id), "Approve button not loaded");
               wAction.Type(imsDriver, By.Id(IMS_Control_PlayerDetails.transText_id), "1234", "Trans ID box not found");
               wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.confirmBtn_XP), "Confirm button not loaded",0,false);
               wAction.WaitAndAccepAlert(imsDriver);
               System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
               wAction.Click(imsDriver, By.LinkText(uname));

               wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "transactions_lnk", "Transactions link not found", 0, false);
               BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[td[  a[text()='"+timeStamp+"'] ] and td[normalize-space()='withdraw']]/td[7]"), "Approved Transaction status not found", false).Contains("approved"), "Payment not successful");
                BaseTest.Pass();
           
        }
        public void Approve_WithdrawRequest_Payments(IWebDriver imsDriver, string uname,bool internalCust=false)
        {
            BaseTest.AddTestCase("Approve the withdraw request from Transactions", "Status should be set to Approved");
            wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "transactions_lnk", "Transactions link not found", 8, false);
            System.Threading.Thread.Sleep(3000);
            string timeStamp = wAction.GetText(imsDriver, By.XPath(IMS_Control_PlayerDetails.WaitingWD_Request_XP));
 

            wAction.Click(imsDriver, By.Id("payments_link"), "payments_link not found", 8, false);
            System.Threading.Thread.Sleep(3000);


            wAction.Click(imsDriver, By.LinkText("Waiting payments"), "Waiting payments link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, IMS_Control_PlayerDetails.Main_Frame_ID, null,FrameGlobals.elementTimeOut);
            
            wAction.Type(imsDriver, By.Id("username"), uname, "uname box not found");

            wAction.Click(imsDriver, By.Id("submit"), "Submit button not loaded", 0, false);
            wAction.Click(imsDriver, By.Name("mark[1]"), "No Waiting withdraw found", 0, false);
            wAction.Click(imsDriver,By.Id("continue"),"continue button not found");
            wAction.Click(imsDriver, By.Id("addresschange"), "addresschange button not loaded", 0, false);
            wAction.WaitAndAccepAlert(imsDriver);
            wAction.Click(imsDriver, By.Id("process"), "Set Issued button not loaded", 0, false);
            wAction.WaitAndAccepAlert(imsDriver);
            wAction.Click(imsDriver, By.Id("issued"), "Continue button not loaded", 0, false);
            wAction.Type(imsDriver, By.Name("bcnumber[1]"), "123456789", "Check# text box not found" ,0, false);
            wAction.Type(imsDriver, By.Name("exttranid[1]"), "123456789", "transID# text box not found");
            wAction.Click(imsDriver, By.Id("issued"), "Continue button not loaded");
            wAction.Click(imsDriver, By.Id("checknumbers"), "checknumbers button not loaded", 0, false);
            wAction.WaitAndAccepAlert(imsDriver);
            SearchCustomer_Newlook(imsDriver, uname, internalCust);

            wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "transactions_lnk", "Transactions link not found", 0, false);
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[td[  a[text()='" + timeStamp + "'] ] and td[normalize-space()='withdraw']]/td[7]"), "Approved Transaction status not found", false).Contains("approved"), "Payment not successful");
            BaseTest.Pass();

        }
        public void Approve_DepositRequest(IWebDriver imsDriver, string uname)
        {
            BaseTest.AddTestCase("Approve the withdraw request from Transactions", "Status should be set to Approved");

            wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "transactions_lnk", "Transactions link not found", 8, false);
            System.Threading.Thread.Sleep(3000);
            string timeStamp = wAction.GetText(imsDriver, By.XPath(IMS_Control_PlayerDetails.WaitingDep_Request_XP),"No withdraw request pending", false);

            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.WaitingDep_Request_XP));

            wAction.WaitAndMovetoFrame(imsDriver, IMS_Control_PlayerDetails.Main_Frame_ID, null, FrameGlobals.elementTimeOut);
            wAction.SelectDropdownOption(imsDriver, By.Id("casinosupportedbonustype"), "casino", "bonus type drop down not found/Casino not found in list");

            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.approveBtn_id), "Approve button not loaded");

            wAction.WaitAndAccepAlert(imsDriver);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction.Click(imsDriver, By.LinkText(uname));

            wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "transactions_lnk", "Transactions link not found", 0, false);
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("//tr[td[normalize-space()='deposit']][1]/td[7]"), "Approved Transaction status not found", false).Contains("approved"), "Payment not successful");
            BaseTest.Pass();

        }

        #region commented
        /*public void SearchCustomer_Newlook(IWebDriver imsDriver, string userName)
        {
            BaseTest.AddTestCase("Search customer in IMS", "Customer search should work as expected");
            try{
                //IWebElement topFrame = imsDriver.FindElement(By.XPath("\\frame[@name='top']"));
                //imsDriver.SwitchTo().Frame("top");
            //commonWebMethods.Click(imsDriver, By.PartialLinkText(Controls.AdminBase_Controls.newLookLink),"New Look link not found",0,false);
                imsDriver.Navigate().GoToUrl("https://admin-stg.ladbrokes.com/ims/bexcore/welcome");
            //imsDriver.SwitchTo().Frame("_yuiResizeMonitor");
                imsDriver.SwitchTo().DefaultContent();
            commonWebMethods.Click(imsDriver, By.Id(Controls.AdminBase_Controls.playerDatabaseID),"Player Database not found",0,false);
            commonWebMethods.Click(imsDriver, By.LinkText(Controls.AdminBase_Controls.rpDatabaseLink),"Database link not found",0,false);
            commonWebMethods.WaitAndMovetoFrame(imsDriver, "main-content", "Frame not found", FrameGlobals.elementTimeOut);
            commonWebMethods.Type(imsDriver, By.Id("username"),userName,"Username text not found",0,false);
            commonWebMethods.Click(imsDriver, By.Id("submit"),"Submit button not found");
            commonWebMethods.Click(imsDriver, By.LinkText(userName),userName+": not found in the database",0,false);
            commonWebMethods.WaitUntilElementPresent(imsDriver, By.XPath(Controls.AdminBase_Controls.signupInfo));
            BaseTest.Assert.IsTrue(commonWebMethods.IsElementPresent(imsDriver,By.XPath(Controls.AdminBase_Controls.signupInfo)),"User not found");
            BaseTest.Pass();

            }
            catch (Exception) { BaseTest.Fail("Add bonus to the customer failed"); }
        }*/

        /*public void AddBonus(IWebDriver imsDriver, string BonusName,string amount)
        {

            BaseTest.AddTestCase("Add Bonus to customer", "Bonus should be added to the customer");
            try
            {
                commonWebMethods.Click(imsDriver, By.XPath(Controls.AdminBase_Controls.addBonus), "Add Bonus Button not found", 0, false);
                //wAction._Click(imsDriver,ORFile.IMSBonus,wActions.locatorType.xpath,"addbonus_btn","Add Bonus Button not found",0,false)
                commonWebMethods.WaitAndMovetoFrame(imsDriver, "main-content", "No Frame Present in Add bonus page",FrameGlobals.elementTimeOut);
                
                commonWebMethods.WaitUntilElementPresent(imsDriver, By.Id(Controls.AdminBase_Controls.bonusDropDownID));
                commonWebMethods.Type(imsDriver, By.Id(Controls.AdminBase_Controls.bonusDropDownID), BonusName, "Username text not found", 0, false);
                commonWebMethods.Type(imsDriver, By.Id("amount"), amount, "Submit button not found");
               // commonWebMethods.Click(imsDriver, By.Id("submit"), "submit not found", 0, false);
                imsDriver.FindElement(By.Id("submit")).Click();
                imsDriver.SwitchTo().Alert().Accept();
                BaseTest.Pass();

            }
            catch (Exception) { BaseTest.Fail("Add bonus to the customer failed"); }
          
        }*/

        /*public void CreateNewBonus(IWebDriver imsDriver, string BonusName, string amount)
        {

            BaseTest.AddTestCase("Create a new Bonus in database", "Bonus should be added to the database");
            try
            {
                imsDriver.Navigate().GoToUrl("https://admin-stg.ladbrokes.com/ims/bexcore/welcome");
                commonWebMethods.Click(imsDriver, By.Id(Controls.AdminBase_Controls.templateDatabaseID), "Template Database not found", 0, false);
                commonWebMethods.Click(imsDriver, By.LinkText(Controls.AdminBase_Controls.bonusDatabaseLink), "Bonus Database link not found", 0, false);

                commonWebMethods.WaitAndMovetoFrame(imsDriver, "main-content", "No Frame Present in Search bonus page", FrameGlobals.elementTimeOut);
                commonWebMethods.WaitUntilElementPresent(imsDriver, By.Id("newbonustype"));

                commonWebMethods.SelectDropdownOption(imsDriver, By.Id("casinocode"), "ladbrokesvegasstg", "Casino Code combo not found", 0, false);
                commonWebMethods.SelectDropdownOption(imsDriver, By.Id("newbonustype"), "Casino manual bonus", "New bonus drop down not found", 0, false);
                commonWebMethods.Click(imsDriver, By.Id("createnew"), "Create new button not found");
                imsDriver.SwitchTo().DefaultContent();

                commonWebMethods.WaitAndMovetoFrame(imsDriver, "main-content", "No Frame Present in Add bonus page", FrameGlobals.elementTimeOut);
                commonWebMethods.Clear(imsDriver, By.Id("name"), "Bonus name text box not found", 0, false);
                commonWebMethods.Type(imsDriver, By.Id("name"), BonusName, "Bonus name text box not found", 0, false);
                commonWebMethods.Clear(imsDriver, By.Id("maxbonus"), "Max bonus textbox not found", 0, false);
                commonWebMethods.Type(imsDriver, By.Id("maxbonus"), amount, "Max bonus textbox not found", 0, false);
                commonWebMethods.SelectDropdownOption(imsDriver, By.Id("defaultproduct"), "casino", "Casino product combobox not found", 0, false);
                commonWebMethods.Click(imsDriver, By.Id("fwtoggle"), "Fiexd Wager checkbox not found");
                commonWebMethods.Type(imsDriver, By.Id("fixedwageringrequirementamt"), "10", "Fixed wager text box not found");

                DateTime dateFormat = DateTime.Now;
                string dateTime = (dateFormat.ToString("yyyy-MM-dd HH:mm"));
                commonWebMethods.Clear(imsDriver, By.Id("startdate"), "Startdate not found", 0, false);
                commonWebMethods.Type(imsDriver, By.Id("startdate"), dateTime, "Startdate not found", 0, false);

                dateFormat = DateTime.Now.AddYears(1);
                dateTime = (dateFormat.ToString("yyyy-MM-dd HH:mm"));
                commonWebMethods.Clear(imsDriver, By.Id("enddate"), "End date not found", 0, false);
                commonWebMethods.Type(imsDriver, By.Id("enddate"), dateTime, "End date not found", 0, false);

                commonWebMethods.SelectDropdownOption(imsDriver, By.Id("gameweightsconfigurationcode"), "test", "Casino product combobox not found", 0, false);

                imsDriver.FindElement(By.Id("action[create]")).Click();
                imsDriver.SwitchTo().Alert().Accept();
                if (commonWebMethods.IsElementPresent(imsDriver, By.Id("message-error-1")))
                {
                    if (commonWebMethods.GetText(imsDriver, By.Id("message-error-1")).Contains("Bonus name must be unique"))
                        BaseTest.Pass();
                }
                else if (commonWebMethods.IsElementPresent(imsDriver, By.Id("message-info-1")))
                {
                    if (commonWebMethods.GetText(imsDriver, By.Id("message-info-1")).Contains("Bonus created"))
                        BaseTest.Pass();
                }
                else
                    BaseTest.Fail("Bonus does not exist and not created too");

            }
            catch (Exception) { BaseTest.Fail("Add bonus to the database failed"); }
            finally { commonWebMethods.BrowserQuit(imsDriver); }

        }*/
        #endregion
        public void TeleBet_Registration(IWebDriver driverObj, ref Registration_Data regData)
        {

            BaseTest.AddTestCase("Enter details for creating Telebet customer","Customer did not get created");
           
            wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOKBtn_XP));
            var random = new Random();
            ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
            myBrowser.Start();
            myBrowser.WindowMaximize();
            System.Threading.Thread.Sleep(2000);
        
            regData.fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            regData.lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            regData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            regData.email = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + "@gmil.com";
          
            
                    wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);

                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", regData.fname, "First name not found", 0, false);

                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", regData.lname, "Last name not found", 0, false);

                    wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
                    wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
                    wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", "1950", "DOB_year not found", 0, false);

                    wAction._Type(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Keys.Tab);
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                    wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOKBtn_XP));
                    

                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", regData.email, "Email  Not Found", 0, false);

                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found", 0, false);

                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);

                    wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);

                    if (regData.country_code == "United Kingdom")
                    {

                        wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "post_txt not found", 0, false);

                        wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", regData.uk_postcode, "Post Code Not Found", 0, false);

                        wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "findAddr_Btn", "Find Address button not found", 0, false);
                    }
                    else
                    {
                        if (wAction._IsElementPresent(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "houseNo_txt"))
                        {
                            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "houseNo_txt", "houseNo_txt not found");
                            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "houseNo_txt", Registration_Data.other_postcode, "houseNo_txt  Not Found");

                        }
                        wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "addr_txt", "addr_txt not found", 0, false);
                        wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "addr_txt", Registration_Data.uk_addr_street_2, "addr_txt  Not Found", 0, false);
                        wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "city_txt", "city_txt not found", 0, false);
                        wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "city_txt", regData.city, "city_txt  Not Found", 0, false);
                    }


                   

                    //uname = regData.username;

                    //wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);

                    wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOKBtn_XP));
                    System.Threading.Thread.Sleep(2000);
                    wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOKBtn_XP));
                    wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Generate_Btn", "Generate button Not Found/Not Clickable", 0, false);
                    regData.username = wAction._GetAttribute(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "value", "User name  Not Found", false);



                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);


                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", "pwdverify_txt not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", regData.password, "Password  Not Found", 0, false);



                    wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_cmb", Registration_Data.securityQues, "Title not found", 0, false);

                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", "answer_txt not found", 0, false);
                    wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", regData.answer, "House  Not Found", 0, false);




                    
                    wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 3, false);
                    wAction._Type(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Keys.Tab);
                    myBrowser.WindowMaximize();
                    wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOK_XP));
                    wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOK_XP));

                    myBrowser.Click("//input[@name='deposit-period' and @data-select='weekly']");
                    //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
                    wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOK_XP));

                    wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 3, false);
                    
                    wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code");



                    // wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code");

                    //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                    wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOK_XP));
                    
            wAction.Click(driverObj, By.XPath("id('submitContainer')/button"), "Registration Button not found/Clickable", 0, false);
                  
            
                    //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                    wAction.Click(myBrowser, By.XPath(Telebet_Control.PopUpClose_DialogOK_XP));
                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.Id("success-telebet-popup")), "Registration Success message not found");

                    driverObj.Close();


               BaseTest.Pass();


            

        }
        public void TeleBet_RegulatedCntry_Registration(IWebDriver driverObj, ref Registration_Data regData)
        {

            BaseTest.AddTestCase("Enter details for creating Telebet customer", "Customer did not get created");

            wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOKBtn_XP));
            var random = new Random();
            ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
            myBrowser.Start();
            myBrowser.WindowMaximize();
            System.Threading.Thread.Sleep(2000);
           

            regData.fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            regData.lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            regData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            regData.email = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + "@gmil.com";
           

            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", regData.fname, "First name not found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", regData.lname, "Last name not found", 0, false);

            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);

            wAction._Type(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Keys.Tab);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOKBtn_XP));


            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", regData.email, "Email  Not Found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);

            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);
            System.Threading.Thread.Sleep(2000);
            //BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.regulatedCountry_Msg_XP)).Contains("If your country of residence is " + regData.country_code + " then please register on the " + regData.country_code + " website"), "Warning message did not appear/message incorrect");
            wAction.Click(driverObj, By.LinkText("Cancel"));


            if (regData.country_code == "United Kingdom")
            {

                wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "post_txt not found", 0, false);

                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", regData.uk_postcode, "Post Code Not Found", 0, false);

                wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "findAddr_Btn", "Find Address button not found", 0, false);
            }
            else
            {
             //   wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "houseNo_txt", "houseNo_txt not found", 0, false);
              //  wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "houseNo_txt", Registration_Data.other_postcode, "houseNo_txt  Not Found", 0, false);
                wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "addr_txt", "addr_txt not found", 0, false);
                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "addr_txt", Registration_Data.uk_addr_street_2, "addr_txt  Not Found", 0, false);
                wAction.Click(driverObj, By.LinkText("Cancel"));
                wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "city_txt", "city_txt not found", 0, false);
                wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "city_txt", regData.city, "city_txt  Not Found", 0, false);
            }




            //uname = regData.username;

            //wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Generate_Btn", "Generate button Not Found/Not Clickable", 0, false);
            regData.username = wAction._GetAttribute(driverObj, ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "value", "User name  Not Found", false);



            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);


            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", "pwdverify_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", regData.password, "Password  Not Found", 0, false);



            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_cmb", Registration_Data.securityQues, "Title not found", 0, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", "answer_txt not found", 0, false);
            wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", regData.answer, "House  Not Found", 0, false);





            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 3, false);
            wAction._Type(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Keys.Tab);
            wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOK_XP));
            wAction.Click(driverObj, By.LinkText("OK"));
         //   myBrowser.Click("//input[@name='deposit-period' and @data-select='weekly']");
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));

            wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 3, false);

            wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code");



            // wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code");

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            wAction.Click(driverObj, By.XPath(Telebet_Control.PopUpClose_DialogOK_XP));

            wAction.Click(driverObj, By.XPath("id('submitContainer')/button"), "Registration Button not found/Clickable", 0, false);
            System.Threading.Thread.Sleep(3000);
            if (wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.regulatedCountry_Msg_XP)))
                goto skip;
            wAction.Click(driverObj, By.LinkText("OK"));
            System.Threading.Thread.Sleep(1000);
            skip:
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.regulatedCountry_Msg_XP)).Contains("If your country of residence is " + regData.country_code + " then please register on the " + regData.country_code + " website"), "Warning message did not appear/message incorrect");
          

            
            driverObj.Close();
            BaseTest.Pass();




        }
        public void CreateNewCustomer_Telebet(IWebDriver imsDriver, ref Registration_Data regData)
        {

            BaseTest.AddTestCase("Open Telebet pages", "Telebet pages should be opened successfully.");

            wAction._Click(imsDriver, ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "header_logo_lnk", "Header logo link is not found",5 , false);

            //wAction._Click(imsDriver, ORFile.Accounts_Wallets_Registration, wActions.locatorType.link, "add_customer_lnk", "'Add new customer' link is not found", 5, false);

            imsDriver.FindElement(By.LinkText("Add New Customer")).Click();

            System.Threading.Thread.Sleep(10000);

            string main = imsDriver.WindowHandles[0].ToString();
            string popup = imsDriver.WindowHandles[1].ToString();
            imsDriver.SwitchTo().Window(popup);
            
            TeleBet_Registration(imsDriver, ref regData);
            BaseTest.Pass();
            return;

        }

        public void SearchCC_WithinCustomer(IWebDriver imsDriver, string cc1, string cc2)
        {
            string RemCC_xPath = "id('creditcards')//a[contains(text(),'" + cc1 + "') and id('creditcards')//a[contains(text(),'" + cc2 + "')]]";
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.Creditcard_Tab_id), "creditcards_navigation link not found");            
            wAction.Click(imsDriver, By.XPath(RemCC_xPath), "Given Credit card Not Active/Credit card not found", 0, false);
            //imsDriver.SwitchTo().DefaultContent();
            //wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

        }

        public void Reset_FailedLoginAttempt(IWebDriver imsDriver)
        {

            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.reset_Login_attempt_ID), "Reset login button not found");
            wAction.WaitAndAccepAlert(imsDriver);
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_PlayerDetails.LockRemovedMsg_XP)),"Customer unlocked message not found");
            

        }

        public void AllowDuplicateCreditCard_Newlook(IWebDriver imsDriver, string cc1, string cc2)
        {

            string RemCC_xPath = "id('creditcards')//a[contains(text(),'" + cc1 + "') and id('creditcards')//a[contains(text(),'" + cc2 + "')]]";

            BaseTest.AddTestCase("Allow dupilcate credit card", "Dupilcation should be successful");
            try
            {
              
                IWebElement element = imsDriver.FindElement(By.Name("duplicate"));
                element.Click();
                 BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.Name("reverseduplicate")), "Credit card did not get removed / Reverse duplicate button not visible");
                imsDriver.SwitchTo().DefaultContent();

                BaseTest.Pass();

            }
            catch (Exception e) { BaseTest.Fail("Remove credit card for the customer failed"); }
        }
        public void AllowDuplicateCreditCard_FullNavigation_Newlook(IWebDriver imsDriver, string cc1, string cc2)
        {

            string RemCC_xPath = "id('creditcards')//a[contains(text(),'" + cc1 + "') and id('creditcards')//a[contains(text(),'" + cc2 + "')]]";

            BaseTest.AddTestCase("Allow dupilcate credit card", "Dupilcation should be successful");
            try
            {
                //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

                imsDriver.FindElement(By.Id(IMS_Control_PlayerDetails.Creditcard_Tab_id)).Click();

                wAction.Click(imsDriver, By.XPath(RemCC_xPath), "Given Credit card Not Active/Credit card not found", 0, false);

                imsDriver.SwitchTo().DefaultContent();

                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                BaseTest.Assert.IsTrue(wAction._IsElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.xpath, "CreditCardDetails_Page"), "Credit Card details page not loaded");

                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "allow_duplicate_btn", "Allow duplicate button not found",8,false);

                wAction.WaitAndAccepAlert(imsDriver);
                
               BaseTest.Assert.IsTrue(wAction._IsElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "reverse_duplicate_btn"), "Credit card did not get removed / Reverse duplicate button not visible");
               
                imsDriver.SwitchTo().DefaultContent();

                BaseTest.Pass();

            }
            catch (Exception e) { BaseTest.Fail("Remove credit card for the customer failed"); }
        }

        public void ReverseDuplicateCreditCard_Newlook(IWebDriver imsDriver, string cc1, string cc2)
        {

            string RemCC_xPath = "id('creditcards')//a[contains(text(),'" + cc1 + "') and id('creditcards')//a[contains(text(),'" + cc2 + "')]]";

            BaseTest.AddTestCase("Remove Credit card from a customer in IMS", "Credit card should be removed from Customer");
            try
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

                imsDriver.FindElement(By.Id(IMS_Control_PlayerDetails.Creditcard_Tab_id)).Click();

                wAction.Click(imsDriver, By.XPath(RemCC_xPath), "Given Credit card Not Active/Credit card not found", 4, false);

                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                //BaseTest.Assert.IsTrue(wAction._IsElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.xpath, "CreditCardDetails_Page"), "Credit Card details page not loaded");

                IWebElement element = imsDriver.FindElement(By.Id("reverseduplicate"));
                new Actions(imsDriver).MoveToElement(element).Build().Perform();
                //IJavaScriptExecutor executor = (IJavaScriptExecutor)imsDriver;// This is exactly opening the page 
                //executor.ExecuteScript("arguments[0].click();", imsDriver.FindElement(By.Id("reverseduplicate")));//
                wAction.Click_JavaScript(imsDriver, By.Id("reverseduplicate"), "reverseduplicate not found");

                //wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "reverse_duplicate_btn", "Allow duplicate button not found");
                imsDriver.SwitchTo().Alert().Accept();
                imsDriver.SwitchTo().DefaultContent();
                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                System.Threading.Thread.Sleep(3000);
                BaseTest.Assert.IsTrue(wAction._IsElementPresent(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "allow_duplicate_btn"), "Credit card did not get removed / Reverse duplicate button not visible");
                //wAction.WaitAndAccepAlert(imsDriver);

                BaseTest.Pass();

            }
            catch (Exception e) { BaseTest.Fail("Remove credit card for the customer failed"); }
        }

        public void FreezeTheCustomer(IWebDriver imsDriver)
        {
            BaseTest.AddTestCase("Freeze the customer in IMS", "Customer should be freezed");
            try
            {
                bool flag = wAction._IsElementPresent(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "freeze_btn");

                if (flag)
                {
                    wAction._Click(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "freeze_btn", "freeze button not found in the user details", 0, false);
                    String portalWindow = imsDriver.WindowHandles[0].ToString();
                    string popup = imsDriver.WindowHandles[1].ToString();
                    imsDriver.SwitchTo().Window(popup);
                    wAction._SelectDropdownOption(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "freeze_reason", "Test Freeze", "Freezing reason dropdown not found", 3, false);
                    if (FrameGlobals.projectName != "IP2")
                    {
                        wAction._SelectDropdownOption(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "mesg_type", "Status Change", "Freezing type dropdown not found", 3, false);
                        wAction._Type(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "mesg_subject", "account-freezed", "Message subject text not found", 0, false);
                        wAction._Type(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "message", "account freezed", "Message text not found", 0, false);
                    }
                    wAction._Click(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "popup_freeze_btn", "Frreze button is not found", 5, false);
                   // imsDriver.SwitchTo().Alert().Accept();
                    wAction.WaitAndAccepAlert(imsDriver);
                    System.Threading.Thread.Sleep(6000);
                   // wAction.WaitforPageLoad(imsDriver);
                  //  imsDriver.SwitchTo().DefaultContent();

                    BaseTest.Pass("Customer is freezed");
                    wAction.WaitAndMovetoPopUPWindow_WithIndex(imsDriver, 0,"Main window not found");
                }
                else
                {
                    BaseTest.Pass("Customer is already in freezed state");
                }
            }
            catch (Exception) { BaseTest.Fail("Not able to freeze the customer"); }
        }
        public void DisableFreeze(IWebDriver imsDriver)
        {
                BaseTest.AddTestCase("Unfreeze the freezed customer in IMS", "Customer should be unfreezed successfully");
               

                if (wAction._IsElementPresent(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.id, "freeze_btn"))
                {
                    BaseTest.Pass("Customer is already in unfreezed state");
                }
                else if (wAction._IsElementPresent(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.xpath, "unfreeze_btn"))
                {
                    if (FrameGlobals.projectName != "IP2")
                        wAction._Click(imsDriver, ORFile.IMSSearchDatabase, wActions.locatorType.xpath, "unfreeze_btn", "Unfreeze button not found in the user details", 0, false);
                    else
                        wAction.Click(imsDriver, By.Id("unfreeze"), "Customer is already in unfreezed state");

                    System.Threading.Thread.Sleep(1000);
                    imsDriver.SwitchTo().Alert().Accept();
                    System.Threading.Thread.Sleep(5000);
                    BaseTest.Pass("Customer unfreezed");
                }
            
        }
        public void ResetPassword(IWebDriver imsDriver, string newPassword)
        {

            BaseTest.AddTestCase("Change password for customer in IMS", "Password should be changed for customer");
            try
            {
                //wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "changepwd", "changepwd button not found in the user details", 0, false);
                String portalWindow = imsDriver.WindowHandles[0].ToString();
                wAction.WaitAndMovetoPopUPWindow(imsDriver, "Change password Window not loaded", FrameGlobals.elementTimeOut);
                //string popup = imsDriver.WindowHandles[1].ToString();

               // imsDriver.SwitchTo().Window(popup);
                wAction._Type(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "password1", newPassword, "Message subject text not found", 0, false);
                wAction.Click(imsDriver, By.Id("disablepasswordchange"));
                wAction.Click(imsDriver, By.Id("disablesendemail"));
                
                wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "changepwd", "changepwd button not found in the user details", 0, false);
                string msg = imsDriver.FindElement(By.XPath("//div[@class='messages']")).Text;
                if (!msg.Contains("Password has been changed successfully"))
                {
                    BaseTest.Fail("Unable to change the password in IMS");
                }
                System.Threading.Thread.Sleep(1000);
                imsDriver.Close();
                imsDriver.SwitchTo().Window(portalWindow);
                BaseTest.Pass();
            }
            catch (Exception e) { BaseTest.Fail("Unable to change the password in IMS"); }
        }
        public string AddCreditCard(IWebDriver imsDriver, string card)
        {
            BaseTest.AddTestCase("Add Credit Card to the customer in IMS", "Customer should be having a card registered");
            try
            {
                Console.WriteLine("Credit Card Number :" + card);
                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.Creditcard_Tab_id), "Credit Card Navigation button not found", 0, false);

                String portalWindow = imsDriver.WindowHandles[0].ToString();

                wAction.Click(imsDriver, By.XPath("//input[@id='button' and @value='Add new credit card']"), "Add new Credit Card button not found");

                string popup = imsDriver.WindowHandles[1].ToString();

                wAction.WaitAndMovetoPopUPWindow(imsDriver, popup, "Credit card Pop up window not found", FrameGlobals.generalTimeOut);

                wAction.Type(imsDriver, By.Id("cc_card_number"), card, "Credit Card text not found", 0, false);
                wAction.SelectDropdownOption(imsDriver, By.Id("cc_exp_month"), "5", "Exp month dropdown not found");
                wAction.SelectDropdownOption_ByValue(imsDriver, By.Id("cc_exp_year"), "22", "Exp Year dropdown not found");
                wAction.Click(imsDriver, By.Id("continueButton"), "continueButton button not found");

                System.Threading.Thread.Sleep(5000);
                if (!wAction.GetText(imsDriver, By.XPath("//body"), "Success message not found").Contains("created"))
                    BaseTest.Fail("Success message not found");

                wAction.Click(imsDriver, By.Id("redirect"), "redirect button not found");
                imsDriver.SwitchTo().Window(portalWindow);
                BaseTest.Pass("Card Added:" + card);


            }
            catch (Exception)
            {
                BaseTest.Fail(card + " Card cannot be added to the customer");
            }
            return card;
        }
        public void SetDepositLimit(IWebDriver imsDriver, string amount)
        {
            BaseTest.AddTestCase("Add Credit Card to the customer in IMS", "Customer should be having a card registered");
            try
            {
                ISelenium myBrowser = new WebDriverBackedSelenium(imsDriver, "https://www.google.co.in/");
                myBrowser.Start();

                DateTime dateFormat = DateTime.Now;
                string dateTime = (dateFormat.AddDays(1).ToString("yyyy-MM-dd"));
                wAction.Click(imsDriver, By.Id("depositlimits_navigation"), "depositlimits_navigation not found");
                System.Threading.Thread.Sleep(2000);
                wAction.Clear(imsDriver, By.Id("depositlimitamount[single]"), "Deposit limit amount text not found", 0, false);
                wAction.Type(imsDriver, By.Id("depositlimitamount[single]"), amount);
                wAction.Clear(imsDriver, By.Id("depositlimitstartdate[single]"), "Start date text not found");
                wAction.Type(imsDriver, By.Id("depositlimitstartdate[single]"), dateTime, "Deposit Limit start text not found", 0, false);

                wAction.Click(imsDriver, By.Id("setdepositlimits"), "Set deposit limit button not found");
                System.Threading.Thread.Sleep(1000);
                imsDriver.SwitchTo().Alert().Accept();
                System.Threading.Thread.Sleep(3000);
                if (!myBrowser.IsTextPresent("Pending deposit limits"))
                    BaseTest.Fail("Success message not found");

                BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver,By.XPath("id('depositlimits')//tr[td[text()='"+amount+"'] and td[text()='Day']]")),"Pending limit not present");

                BaseTest.Pass("Deposit limit has been set successfully");


            }
         catch (Exception)
            {
                BaseTest.Fail(" Card cannot be added to the customer");
            }
        }
        public void VerifyDepositLimitInIms(IWebDriver imsDriver, string amount)
        {
            BaseTest.AddTestCase("Verify deposit limit in IMS", "Deposit limit should be set successfully");
            try
            {
                wAction.Click(imsDriver, By.Id("depositlimits_navigation"), "depositlimits Navigation button not found", 0, false);
                if (wAction.GetAttribute(imsDriver, By.Id("depositlimitamount[single]"), "value", "Deposit limit is not found", false).Contains(amount))
                {
                    BaseTest.Pass();
                }
                else
                {
                    BaseTest.Fail("Deposit limit is not set correctly");
                }
            }
            catch (Exception)
            {
                BaseTest.Fail(" Setting deposit limit through responsible gambling option failed");
            }
        }
        public void VerifyPendingDepositLimitInIms(IWebDriver imsDriver, string amount)
        {
            BaseTest.AddTestCase("Verify Pending deposit limit in IMS", "Pending Deposit limit should be available");
            try
            {
                wAction.Click(imsDriver, By.Id("depositlimits_navigation"), "depositlimits Navigation button not found", 0, false);
                if (imsDriver.FindElement(By.XPath("//tr[td[contains(text(),'Pending deposit limits')]]/following-sibling::tr[2]/td[2]")).Text.Contains(amount))
                {
                    BaseTest.Pass();
                }
                else
                {
                    BaseTest.Fail("Deposit limit is not set correctly");
                }
            }
            catch (Exception)
            {
                BaseTest.Fail(" Pending Deposit limit table is not available");
            }
        }
        public void AddNetteller(IWebDriver imsDriver, string cardnumber, string wallet, string amount)
        {
            BaseTest.AddTestCase("Add Netteller Card to the customer in IMS", "Customer should be having a card registered");
           
                wAction.Click(imsDriver, By.Id("transactions_navigation"), "transactions Navigation button not found", 0, false);
                String portalWindow = imsDriver.WindowHandles[0].ToString();

                wAction.SelectDropdownOption(imsDriver, By.Id("newdeposittrans"), "Add new NETeller", "Deposit transaction drop down not found", 0, false);

                wAction.Click(imsDriver, By.XPath("//input[@type='button' and @value='Deposit']"), "Add new deposit button not found");
                wAction.WaitAndMovetoFrame(imsDriver, "main-content");
                wAction.Click(imsDriver, By.Id("addaccount"), "Add Account button not found");


                wAction.WaitAndMovetoPopUPWindow(imsDriver, "Netteller Pop up window not found", FrameGlobals.generalTimeOut);
                wAction.Type(imsDriver, By.Id("pmaccountinfo:net_account"), cardnumber, "Credit Card text not found", 0, false);
                wAction.Click(imsDriver, By.Id("pmaccountinfo:submitInsert"), "Submit insert not found");

                BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("//span[contains(text(),'success')]")), "Netteller not added : Success message not found");
                imsDriver.Close();
                imsDriver.SwitchTo().Window(portalWindow);

                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:accountselector"), cardnumber, cardnumber + " number not found in Account selector dropdown");
                wAction.Type(imsDriver, By.Id("paymentrequest:tranid"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Trans ID text not found");
                wAction.Type(imsDriver, By.Id("paymentrequest:amount"), amount, "Trans ID text not found");
                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:ftwalletname"), wallet, wallet + " not found in wallet list dropdown");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:bonustype"), "casino", "Casino not found in bonus type dropdown");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:clientchannel"), "Internet", "Internet not found in Channel type dropdown");
                System.Threading.Thread.Sleep(2000);
                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit button not found");
            if( wAction.IsElementPresent(imsDriver, By.XPath("//span[contains(text(),'System error')]")))
                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit button not found");
               
            if (!wAction.IsElementPresent(imsDriver, By.XPath("//td[contains(text(),'NETeller')]")))
                if(!wAction.IsElementPresent(imsDriver,By.XPath("//span[contains(text(),'successful')]")))
                    BaseTest.Fail("Success message not found");

                BaseTest.Pass("netteller Card Added:" + cardnumber);


        }
        public void AddFundTransfer(IWebDriver imsDriver, string amount)
        {
            BaseTest.AddTestCase("Add FundTransfer to the customer in IMS", "FundTransfer added successfully");

            wAction.Click(imsDriver, By.Id("transactions_navigation"), "transactions Navigation button not found", 0, false);
            

            wAction.SelectDropdownOption(imsDriver, By.Id("newdeposittrans"), "Add new FundTransfer", "Deposit transaction drop down not found/Fundtransfer not found in list", 0, false);

            wAction.Click(imsDriver, By.XPath("//input[@type='button' and @value='Deposit']"), "Add new deposit button not found");
            wAction.WaitAndMovetoFrame(imsDriver, "main-content");

            wAction.Type(imsDriver, By.Id("tranid"), "123456", "tranid textbox not found", 0, false);
            wAction.Type(imsDriver, By.Id("comments"), "test", "comments textbox not found");
            wAction.Type(imsDriver, By.Id("amount"), amount, "amount textbox not found");
            wAction.SelectDropdownOption(imsDriver, By.Id("casinosupportedbonustype"), "casino", "bonus type drop down not found/Casino not found in list");


            wAction.Click(imsDriver, By.Id("submit"), "Submit btn not found");
            wAction.WaitAndAccepAlert(imsDriver);
            //wAction.WaitUntilElementDisappears(imsDriver, By.Id("tranid"));
            wAction.Click(imsDriver, By.Id("transactions_navigation"), "transactions Navigation button not found", 0, false);


            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[5]")).Contains("FundTransfer"), "No fundtransfer log added to the customer");
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[6]")).Contains(amount), "Submitted amount not matching");
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[7]")).Contains("approved"), "fundtransfer request not in appoved status");
            BaseTest.Pass();
        }
        public void AddEnvoy(IWebDriver imsDriver,string wallet, string amount)
        {
            BaseTest.AddTestCase("Add Envoy to the customer in IMS", "FundTransfer added successfully");

            wAction.Click(imsDriver, By.Id("transactions_navigation"), "transactions Navigation button not found", 0, false);


            wAction.SelectDropdownOption(imsDriver, By.Id("newdeposittrans"), "Add new Envoy", "Deposit transaction drop down not found/Fundtransfer not found in list", 0, false);

            wAction.Click(imsDriver, By.XPath("//input[@type='button' and @value='Deposit']"), "Add new deposit button not found");
            wAction.WaitAndMovetoFrame(imsDriver, "main-content");

            wAction.Type(imsDriver, By.Id("paymentrequest:tranid"), "123456", "tranid textbox not found", 0, false);
            wAction.Type(imsDriver, By.Id("paymentrequest:comments"), "test", "comments textbox not found");
            wAction.Type(imsDriver, By.Id("paymentrequest:amount"), amount, "amount textbox not found");
            wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:bonustype"), "casino", "bonus type drop down not found/Casino not found in list");
            wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:clientchannel"), "Internet", "Internet not found in Channel type dropdown");
            wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:ftwalletname"), wallet, "Wallet not found in Channel type dropdown");


            System.Threading.Thread.Sleep(2000);
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit btn not found");
            wAction.WaitAndAccepAlert(imsDriver);
            System.Threading.Thread.Sleep(2000);
            
            if (wAction.IsElementPresent(imsDriver, By.XPath("//span[contains(text(),'System error')]")))
            {
                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit button not found");
                bool fl = wAction.IsElementPresent(imsDriver, By.XPath("//span[contains(text(),'Deposit was successful')]"));
                //wAction.Click(imsDriver,By.PartialLinkText(
            }

            
            wAction.Click(imsDriver, By.Id("transactions_navigation"), "transactions Navigation button not found", 0, false);


            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[5]"),null,false).Contains("Envoy"), "No fundtransfer log added to the customer");
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[6]")).Contains(amount), "Submitted amount not matching");
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[7]")).Contains("approved"), "fundtransfer request not in appoved status");
            BaseTest.Pass();
        }
        public void AddCash(IWebDriver imsDriver, string wallet, string amount)
        {
            BaseTest.AddTestCase("Add Direct to the customer in IMS", "Customer should be having a balance added manually");
            try
            {

                wAction.Click(imsDriver, By.Id("transactions_navigation"), "transactions Navigation button not found", 0, false);
                String portalWindow = imsDriver.WindowHandles[0].ToString();

                wAction.SelectDropdownOption(imsDriver, By.Id("newdeposittrans"), "Add new Cash", "Deposit transaction drop down not found/Add new Cash Not found in the list", 0, false);

                wAction.Click(imsDriver, By.XPath("//input[@type='button' and @value='Deposit']"), "Add new deposit button not found");
                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

                wAction.Type(imsDriver, By.Id("paymentrequest:tranid"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Trans ID text not found");
                wAction.Type(imsDriver, By.Id("paymentrequest:amount"), amount, "Trans ID text not found");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:ftwalletname"), wallet, wallet + " not found in wallet list dropdown");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:bonustype"), "casino", "Casino not found in bonus type dropdown");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:clientchannel"), "Internet", "Internet not found in Channel type dropdown");

              
                System.Threading.Thread.Sleep(2000);
                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit btn not found");
              

                if (wAction.IsElementPresent(imsDriver, By.XPath("//span[contains(text(),'System error')]")))
                {
                    wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit button not found");
                }

          

                wAction.WaitforPageLoad(imsDriver);
                BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[5]"), null, false).Contains("Cash"), "No Cash log added to the customer");
                BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[6]")).Contains(amount), "Submitted amount not matching");
                BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('transactions')//tr[2]/td[7]")).Contains("approved"), "Cash request not in appoved status");
       

                BaseTest.Pass();


            }
            catch (Exception)
            {
                BaseTest.Fail("Manual Adjustment cannot be added to the customer");
            }

        }
        public void AddBankDraft(IWebDriver imsDriver, string wallet, string amount)
        {
            BaseTest.AddTestCase("Add Direct to the customer in IMS", "Customer should be having a balance added manually");
            try
            {

                wAction.Click(imsDriver, By.Id("transactions_navigation"), "transactions Navigation button not found", 0, false);
                String portalWindow = imsDriver.WindowHandles[0].ToString();

                wAction.SelectDropdownOption(imsDriver, By.Id("newdeposittrans"), "Add new BD", "Deposit transaction drop down not found/Add new Cash Not found in the list", 0, false);

                wAction.Click(imsDriver, By.XPath("//input[@type='button' and @value='Deposit']"), "Add new deposit button not found");
                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);

                wAction.Type(imsDriver, By.Id("paymentrequest:bankaccountname"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Bank Acct Name text not found");
                wAction.Type(imsDriver, By.Id("paymentrequest:bankaddress"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Bank Address text not found");
                wAction.Type(imsDriver, By.Id("paymentrequest:bankstate"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Bank State text not found");
                wAction.Type(imsDriver, By.Id("paymentrequest:bankcity"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Bank City text not found");
                wAction.Type(imsDriver, By.Id("paymentrequest:bankzip"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Bank Zip text not found");
                wAction.Type(imsDriver, By.Id("paymentrequest:bankaccountno"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Bank Zip text not found");

                wAction.Type(imsDriver, By.Id("paymentrequest:amount"), amount, "Trans ID text not found");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:bankcountry"), "United Kingdom", "United Kingdom not found in wallet list dropdown");
                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:ftwalletname"), wallet, wallet + " not found in wallet list dropdown");
                
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:clientchannel"), "Internet", "Internet not found in Channel type dropdown");

                System.Threading.Thread.Sleep(2000);
                
                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit btn not found");
          

                if (wAction.IsElementPresent(imsDriver, By.XPath("//span[contains(text(),'System error')]")))
                {
                    wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit button not found");
                }

          


               // wAction.WaitforPageLoad(imsDriver);
                if (!wAction.WaitUntilElementPresent(imsDriver, By.XPath("id('transactions')//table[@class='result']/tbody/tr[2]/td[5]")))
                {
                    string trans = wAction.GetAttribute(imsDriver, By.XPath("id('transactions')//table[@class='result']/tbody/tr[2]/td[5]"), "value");
                    string status = wAction.GetAttribute(imsDriver, By.XPath("id('transactions')//table[@class='result']/tbody/tr[2]/td[7]"), "value");
                    if (status.Contains("approved") && trans.Contains("BD"))
                        BaseTest.Fail("BD transaction not approved");
                }

                BaseTest.Pass();


            }
            catch (Exception)
            {
                BaseTest.Fail("BD registration cannot be added to the customer");
            }

        }
        public void AddWesternUnion(IWebDriver imsDriver, string wallet, string amount)
        {
            BaseTest.AddTestCase("Add Netteller Card to the customer in IMS", "Customer should be having a card registered");
            try
            {
                string unionName = "Test";
                wAction.Click(imsDriver, By.Id("transactions_navigation"), "transactions Navigation button not found", 0, false);
                String portalWindow = imsDriver.WindowHandles[0].ToString();

                wAction.SelectDropdownOption(imsDriver, By.Id("newdeposittrans"), "Add new Western Union", "Deposit transaction drop down not found/Western Union not found in the list", 0, false);

                wAction.Click(imsDriver, By.XPath("//input[@type='button' and @value='Deposit']"), "Add new deposit button not found");
                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                wAction.Click(imsDriver, By.Id("addaccount"), "Add Account button not found");


                wAction.WaitAndMovetoPopUPWindow(imsDriver, "Netteller Pop up window not found", FrameGlobals.generalTimeOut);
                wAction.Type(imsDriver, By.Id("pmaccountinfo:field1"), unionName, "Account name text not found", 0, false);
                wAction.Type(imsDriver, By.Id("pmaccountinfo:field2"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Account INfo text not found");
                wAction.Click(imsDriver, By.Id("pmaccountinfo:submitInsert"), "Submit insert not found");

                BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("//span[contains(text(),'success')]")), "Western union not added : Success message not found");
                imsDriver.Close();
                imsDriver.SwitchTo().Window(portalWindow);

                wAction.WaitAndMovetoFrame(imsDriver, "main-content", "Main content frame not found", FrameGlobals.elementTimeOut);
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:accountselector"), unionName, unionName + " number not found in Account selector dropdown");
                wAction.Type(imsDriver, By.Id("paymentrequest:tranid"), StringCommonMethods.GenerateAlphabeticGUID().ToLower(), "Trans ID text not found");
                wAction.Type(imsDriver, By.Id("paymentrequest:amount"), amount, "Trans ID text not found");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:ftwalletname"), wallet, wallet + " not found in wallet list dropdown");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:bonustype"), "casino", "Casino not found in bonus type dropdown");
                wAction.SelectDropdownOption(imsDriver, By.Id("paymentrequest:clientchannel"), "Internet", "Internet not found in Channel type dropdown");

                System.Threading.Thread.Sleep(2000);
                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit btn not found");
          

                if (wAction.IsElementPresent(imsDriver, By.XPath("//span[contains(text(),'System error')]")))
                {
                    wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID), "Submit button not found");
                }

          

                if (!wAction.WaitUntilElementPresent(imsDriver, By.XPath("id('transactions')//table[@class='result']/tbody/tr[2]/td[5]")))
                {
                    string trans = wAction.GetAttribute(imsDriver, By.XPath("id('transactions')//table[@class='result']/tbody/tr[2]/td[5]"), "value");
                   string status =  wAction.GetAttribute(imsDriver, By.XPath("id('transactions')//table[@class='result']/tbody/tr[2]/td[7]"),"value");
                   if (status.Contains("approved") && trans.Contains("Western Union"))
                    BaseTest.Fail("Western Union transaction not approved");
                }
               

                BaseTest.Pass();


            }
            catch (Exception)
            {
                BaseTest.Fail("Western Union cannot be added to the customer");
            }
        }
        public void DisableWithdrawPayMethod(IWebDriver imsDriver, string payMethod)
        {            
            try
            {
                BaseTest.AddTestCase("Enable Withdrawblock in IMS", "Withdrawblock is disabled as expected");
                wAction.Click(imsDriver, By.Id("allowedPaymentMethods_navigation"), "Allow Payment Method Navigation button not found", 0, false);
                var selectElement = new SelectElement(imsDriver.FindElement(By.Id("withdrawPayments-avail")));
                selectElement.SelectByText(payMethod);
                wAction.Click(imsDriver, By.Id("withdrawPayments-select"), "Select payment method button not found", 0, false);
                wAction.Click(imsDriver, By.Id("saveWithdraws"), "saveWithdraws button not found", 0, false);
                wAction.WaitforPageLoad(imsDriver);
                BaseTest.Pass();
            }
            catch (Exception)
            {
                BaseTest.Fail("Western Union cannot be added to the customer");
            }
        }
        public void WithdrawNetellerFromIMS(IWebDriver imsDriver, int amount, string wallet)
        {
            try
            {
                BaseTest.AddTestCase("Enable Withdrawblock in IMS", "Withdrawblock is disabled as expected");
                wAction.Click(imsDriver, By.Id("transactions_navigation"), "Transaction Navigation button not found", 0, false);
              

                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("newwithdrawtrans"), "Add new NETeller");
                wAction.Click(imsDriver, By.Id("newwithdrawtrans"));
                wAction.Click(imsDriver, By.XPath("//input[@value='Withdraw']"), "Withdraw method button not found", 0, false);
                wAction.WaitforPageLoad(imsDriver);
                wAction.WaitAndMovetoFrame(imsDriver, By.Id("main-content"), "Unable to select the main-content frame");
                
                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:accountselector"), "1");
                wAction.Type(imsDriver, By.Id("paymentrequest:amount"), amount.ToString(), "Amount field not found in withdrawals", 0, false);
                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:ftwalletname"), wallet);
                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:clientchannel"), "Internet");

                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID));
                wAction.WaitforPageLoad(imsDriver);
                System.Threading.Thread.Sleep(7000);
                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("transactions_navigation")).Displayed, "Unable to withdraw money from Neteller in IMS");
                imsDriver.FindElement(By.Id("wdrequests_navigation")).Click();
                System.Threading.Thread.Sleep(2000);
                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.XPath("//div[@id='wdrequests_section']//table[@class='result']//tr[2]")).Text.Contains(amount.ToString()), "Unable to withdraw money from Neteller in IMS");

                BaseTest.Pass();
            }
            catch (Exception)
            {
                BaseTest.Fail("Unable to withdraw money from IMS");
            }
        }
        public void WithdrawBDFromIMS(IWebDriver imsDriver, string amount, string wallet,string uname)
        {
                BaseTest.AddTestCase("Enable Withdrawblock in IMS", "Withdrawblock is disabled as expected");
                wAction.Click(imsDriver, By.Id("transactions_navigation"), "Transaction Navigation button not found", 0, false);


                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("newwithdrawtrans"), "Add new BD");
                wAction.Click(imsDriver, By.Id("newwithdrawtrans"));
                wAction.Click(imsDriver, By.XPath("//input[@value='Withdraw']"), "Withdraw method button not found", 0, false);
                wAction.WaitforPageLoad(imsDriver);
                wAction.WaitAndMovetoFrame(imsDriver, By.Id("main-content"), "Unable to select the main-content frame");

               // wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:accountselector"), "1");
                wAction.Type(imsDriver, By.Id("paymentrequest:amount"), amount.ToString(), "Amount field not found in withdrawals", 0, false);
                wAction.Type(imsDriver, By.Id("paymentrequest:bankstate"), "Test", "Client State field not found in withdrawals");
                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:ftwalletname"), wallet);
                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("paymentrequest:clientchannel"), "Internet");

                wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.transaction.SubmitPaymentID));
                System.Threading.Thread.Sleep(2000);
        //  BaseTest.Assert.IsTrue( wAction.WaitUntilElementPresent(imsDriver, By.XPath("//span[contains(text(),'successful!')]")),"Withdraw success msg not found");
          wAction.Click(imsDriver, By.LinkText(uname), "Username link not found");
                wAction.WaitforPageLoad(imsDriver);
                System.Threading.Thread.Sleep(1000);
                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("transactions_navigation")).Displayed, "Unable to withdraw money from BD in IMS");
                imsDriver.FindElement(By.Id("wdrequests_navigation")).Click();
                System.Threading.Thread.Sleep(2000);
                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.XPath("//div[@id='wdrequests_section']//table[@class='result']//tr[2]")).Text.Contains(amount.ToString()), "Unable to withdraw money from BD in IMS");

                BaseTest.Pass();
          
        }
        public void ModifyWithdrawLimit(IWebDriver imsDriver, int miniLimit, int maxLimit)
        {
            wAction.WaitforPageLoad(imsDriver); 
               System.Threading.Thread.Sleep(5000);

               BaseTest.AddTestCase("Modify withdraw limit in IMS", "Modified Withdraw limit successfully");
                wAction.Click(imsDriver, By.Id(IMS_Control_Product.Product_Tab_ID), "product settings link not found", 0, false);
                wAction.WaitforPageLoad(imsDriver);
                System.Threading.Thread.Sleep(2000);
                wAction.Click(imsDriver, By.LinkText("Casinos"), "Casinos link is not available in Products page, IMS");
                wAction.WaitforPageLoad(imsDriver);
                string casinodropdown = (FrameGlobals.projectName != "IP2" ? "ladbrokesvegasstg" : "ladbrokesvegastest");

                wAction.Click(imsDriver, By.XPath("//a[contains(text(),'" + casinodropdown + "')]"), "Config link for Ladbrokes vegas is not found, IMS");
                wAction.WaitforPageLoad(imsDriver);
                System.Threading.Thread.Sleep(7000);
                
             //   imsDriver.SwitchTo().Frame("main-content");
                wAction.Click(imsDriver, By.Id("casinotabs-span-casinoTab_payments"), "Payments tab is not found in config page, IMS");
                wAction.WaitforPageLoad(imsDriver);
                System.Threading.Thread.Sleep(3000);
                wAction.WaitAndMovetoFrame(imsDriver, By.Id("main-content"));
                wAction.Click(imsDriver, By.Id("a-method-NETeller"), "product settings link not found", 0, false);
                System.Threading.Thread.Sleep(5000);
                wAction.Clear(imsDriver, By.Id("PaymentMethodsForm:pm:NETeller:GBP:general:withdrawmin"), "WithdrawMin is not found, IMS",0,false);
                wAction.Clear(imsDriver, By.Id("PaymentMethodsForm:pm:NETeller:GBP:general:withdrawmax"), "withdrawmax is not found, IMS");
                
                wAction.Type(imsDriver, By.Id("PaymentMethodsForm:pm:NETeller:GBP:general:withdrawmin"), miniLimit.ToString(), "Unable to find Withdraw minimum text box");
                wAction.Type(imsDriver, By.Id("PaymentMethodsForm:pm:NETeller:GBP:general:withdrawmax"), maxLimit.ToString(), "Unable to find Withdraw Maximum text box");
                wAction.Click(imsDriver, By.Id("PaymentMethodsForm:update"), "Unable to locate Update button in Payments tab, config page in IMS");
                wAction.WaitforPageLoad(imsDriver);
                System.Threading.Thread.Sleep(3000);
                if (!wAction.GetText(imsDriver, By.ClassName("messages"), "Unable to update the Payment method").Contains("Payment method options changed"))
                {
                    BaseTest.Fail("Unable to modigy withdraw limit in IMS");
                }
                imsDriver.SwitchTo().DefaultContent();
                BaseTest.Pass();
            
        }                
        public void AddCorrection(IWebDriver imsDriver, string correctionAmount)
        {
            
                BaseTest.AddTestCase("Add Correction to the customer in IMS", "Added correction to Customer");
                wAction.Click(imsDriver, By.Id("balances_navigation"), "Balances Navigation button not found", 0, false);
                wAction.Click(imsDriver, By.XPath("//button[contains(text(),'Add Correction')]"), "Unable to locate Add Correction button in IMS");
                System.Threading.Thread.Sleep(3000);

                wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("businessCaseSelect"), "generic_deposit", "Correction drop down box is not found");
                System.Threading.Thread.Sleep(3000);
            
                wAction.Type(imsDriver, By.XPath("//form[@id='editorForm']//input[@id='description']"), "Testing", "Unable to locate text box to enter Description");
                wAction.Type(imsDriver, By.XPath("//form[@id='editorForm']//input[@id='amount']"), correctionAmount, "Unable to locate text box to enter amount");

                wAction.Click(imsDriver, By.Id("saveValue"), "Unable to save the Add Correction");
                wAction.WaitforPageLoad(imsDriver);
                BaseTest.Pass();
           
        }
        public void AddCorrection_New(IWebDriver imsDriver, string correctionAmount,string category,string transactionType)
        {

            BaseTest.AddTestCase("Add Correction to the customer in IMS", "Added correction to Customer");
            wAction.Click(imsDriver, By.Id("balances_navigation"),null, 0, false);
            wAction.Click(imsDriver, By.XPath("//button[contains(text(),'Add Correction')]"), "Unable to locate Add Correction button in IMS");
            System.Threading.Thread.Sleep(3000);

            wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("businessCaseSelect"), category, "Correction drop down box is not found");
            System.Threading.Thread.Sleep(3000);

            wAction.Clear(imsDriver, By.XPath("id('balanceCorrectionForm')//input[@id='amount']"), "Unable to locate text box to enter amount");
            wAction.Type(imsDriver, By.XPath("id('balanceCorrectionForm')//input[@id='amount']"), correctionAmount);
            wAction.Clear(imsDriver, By.Id("description"),  "Unable to locate text box to enter Description");
            wAction.Type(imsDriver, By.Id("description"), "Testing");

            wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("clientType"), "Sports", "clientType drop down box is not found");
            wAction.SelectDropdownOption_ByPartialText(imsDriver, By.Id("clientPlatform"), "Web", "clientPlatform drop down box is not found");

            wAction.Click(imsDriver, By.Id("saveValue"), "Unable to save the Add Correction");
            wAction.WaitforPageLoad(imsDriver);
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath("//tr[td[contains(text(),'" + transactionType + "')]]")), "Balance correction failed");
            BaseTest.Pass();

        }
        public void AddThresholdLimit(IWebDriver imsDriver, string thresAmount)
        {

            BaseTest.AddTestCase("Add threshold limit to the customer in IMS", "Added threshold to Customer");
            wAction.Click(imsDriver, By.Id("statementreportenabled"), "include in statementreportenabled checkbox not found", 0, false);
            System.Threading.Thread.Sleep(3000);

            wAction.Type(imsDriver, By.Id("stmtminpaymthreshold"), thresAmount, "stmtminpaymthreshold unable to locate text box to enter amount");
            wAction.Click(imsDriver, By.Id("update"), "Unable to save the changes");
            wAction.WaitAndAccepAlert(imsDriver);
            wAction.WaitforPageLoad(imsDriver);
            System.Threading.Thread.Sleep(3000);
            wAction.PageReload(imsDriver);
            
            BaseTest.Assert.IsTrue(wAction.GetAttribute(imsDriver, By.Id("stmtminpaymthreshold"), "value").Contains(thresAmount), "Data not saved successfully");         
            BaseTest.Pass();

        }
        public void AddCreditLimit(IWebDriver imsDriver, string Amount)
        {

            BaseTest.AddTestCase("Add Credit limit to the customer in IMS", "Added credit limit to Customer");
            if(!wAction.IsElementPresent(imsDriver,By.Id("playerCreditContracts_navigation")))
            {
                wAction.Click(imsDriver, By.Id("editSectionsDisplay"), "editSectionsDisplay not found");
                System.Threading.Thread.Sleep(2000);
                wAction.SelectDropdownOption_ByValue(imsDriver,By.Id("sectionEditing-avail"),"playerCreditContracts","playerCreditContracts not found in the list");

                wAction.Click(imsDriver, By.Id("sectionEditing-select"), "sectionEditing-select not found");
                
                wAction.Click(imsDriver, By.Id("saveSections"), "editSectionsDisplay not found");
                System.Threading.Thread.Sleep(3000);
                
            }
                
            wAction.Click(imsDriver, By.Id("playerCreditContracts_navigation"), "playerCreditContracts_navigation not found",0,false);
            System.Threading.Thread.Sleep(3000);
            wAction.Clear(imsDriver, By.Id("creditLimit"),  "creditLimit text box not found to enter amount");
            wAction.Type(imsDriver, By.Id("creditLimit"), Amount, "creditLimit text box not found to enter amount");
            wAction.Click(imsDriver, By.Id("createNewContract"), "Unable to save the changes");
            
            wAction.WaitforPageLoad(imsDriver);
            System.Threading.Thread.Sleep(6000);
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('contractsEntityList_row_0')/td[4]")).Contains("Active"), "Credit limit not saved successfully/status not active");
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('contractsEntityList_row_0')/td[5]")).Contains(Amount), "Credit limit not saved successfully/amount mismatched");
            BaseTest.Pass();

        }
        public void EndCreditLimit(IWebDriver imsDriver)
        {

            BaseTest.AddTestCase("End Credit limit to the customer in IMS", "Ended credit limit to Customer");
            wAction.Click(imsDriver, By.Id("playerCreditContracts_navigation"), "playerCreditContracts_navigation not found", 0, false);
            System.Threading.Thread.Sleep(3000);


            wAction.Click(imsDriver, By.Id("endActiveContract"), "Unable to save the changes");
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementDisappears(imsDriver, By.Id("endActiveContract")), "Credit limit not ended successfully");
            BaseTest.Pass();

        }
        public void ValidateCreditLimit(IWebDriver imsDriver, string Amount)
        {

            BaseTest.AddTestCase("Verify Credit limit to the customer in IMS", "credit limit to Customer should be: "+ Amount);
            if (!wAction.IsElementPresent(imsDriver, By.Id("playerCreditContracts_navigation")))
            {
                wAction.Click(imsDriver, By.Id("editSectionsDisplay"), "editSectionsDisplay not found");
                System.Threading.Thread.Sleep(2000);
                wAction.SelectDropdownOption_ByValue(imsDriver, By.Id("sectionEditing-avail"), "playerCreditContracts", "playerCreditContracts not found in the list");

                wAction.Click(imsDriver, By.Id("sectionEditing-select"), "sectionEditing-select not found");

                wAction.Click(imsDriver, By.Id("saveSections"), "editSectionsDisplay not found");
                System.Threading.Thread.Sleep(3000);

            }

            wAction.Click(imsDriver, By.Id("playerCreditContracts_navigation"), "playerCreditContracts_navigation not found", 0, false);
            System.Threading.Thread.Sleep(3000);
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("id('contractsEntityList_row_0')/td[5]")).Contains(Amount), "Credit limit not saved successfully/amount mismatched");
            BaseTest.Pass();

        }

        public void CheckCallID(IWebDriver imsDriver,string eventName, string amount)
        {
                      

            BaseTest.AddTestCase("Check for CallID for the Event:" + eventName, "Call ID should be present");


            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.eventlog_byUsername_XP), "Event log link not found", FrameGlobals.reloadTimeOut, false);
                wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
                wAction.SelectDropdownOption(imsDriver, By.Id(IMS_Control_CasinoLog.TimePeriod_cmb_ID), "Today", "Event log link not found", FrameGlobals.reloadTimeOut, false);
                wAction.Type(imsDriver, By.Id(IMS_Control_CasinoLog.eventName_cmb_ID), eventName, "Event Name text box not found");
                wAction.Click(imsDriver, By.Id(IMS_Control_CasinoLog.Show_Btn_ID), "show log button not found");           

                BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("//td[contains(string(),'callId') and contains(string(),'amount "+amount+"')]")), "Call ID not generated for the transaction");
                imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();

 
        }

        public void CheckLog_InvalidLoginMsg(IWebDriver imsDriver)
        {


            BaseTest.AddTestCase("Check for error log after failure", "Log should be present");
            

            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.eventlog_byUsername_XP), "Event log link not found", FrameGlobals.reloadTimeOut, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
            wAction.SelectDropdownOption(imsDriver, By.Id(IMS_Control_CasinoLog.TimePeriod_cmb_ID), "Today", "Event log link not found", FrameGlobals.reloadTimeOut, false);
            wAction.Click(imsDriver, By.Id(IMS_Control_CasinoLog.Show_Btn_ID), "show log button not found");

            BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath(IMS_Control_CasinoLog.Login_err_msg_XP)), "Call ID not generated for the transaction");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }
        public void CheckLog_SelExclMsg(IWebDriver imsDriver)
        {


            BaseTest.AddTestCase("Check for self exclusion log after set", "Log should be present");


            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.eventlog_byUsername_XP), "Event log link not found", FrameGlobals.reloadTimeOut, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
            wAction.SelectDropdownOption(imsDriver, By.Id(IMS_Control_CasinoLog.TimePeriod_cmb_ID), "Today", "Event log link not found", FrameGlobals.reloadTimeOut, false);
            wAction.Click(imsDriver, By.Id(IMS_Control_CasinoLog.Show_Btn_ID), "show log button not found");

            List<IWebElement> logs = wAction.ReturnWebElements(imsDriver, By.XPath(IMS_Control_CasinoLog.SelfExclerr_msg_XP), "Log not found", 0, false);

            //BaseTest.Assert.IsTrue(logs.Count == 4, "Self exclusion not applied on 4 products");
            foreach (IWebElement log in logs)
                if (!(log.Text.Contains("Casino") || log.Text.Contains("Sportsbook") || log.Text.Contains("Bingo") || log.Text.Contains("Poker")))
                    BaseTest.Fail("failure message contains invalid product");


            //BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath(IMS_Control_CasinoLog.Login_err_msg_XP)), "Call ID not generated for the transaction");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }

        public void CheckLog_LoginFailedCustomer(IWebDriver imsDriver, string username,string msg)
        {


            BaseTest.AddTestCase("Check for Account frozen message", "Message should be present");
            imsDriver.SwitchTo().DefaultContent();

            wAction.Click(imsDriver, By.Id(IMS_Control_Report.Monitoring_Rpt_ID), "Monitoring & Reporting not found", FrameGlobals.reloadTimeOut, false);
            wAction.Click(imsDriver, By.LinkText("Event log"), "Event Log link not found");
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));

            wAction.Type(imsDriver, By.Id("username"), username, "Username text box not found");
            wAction.SelectDropdownOption(imsDriver, By.Id("user"), "player", "User Type dropdown not found");
            wAction.Click(imsDriver, By.Id("show"), "show log button not found");

           // string errorMsg = baseTest.ReadxmlData("err", "Ecom_Invalid_Lgn", DataFilePath.IP2_Authetication);
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("//td[contains(text(),'"+msg+"')]")), msg+ " message not found");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();




        }
        public void CheckLog_LoginFailedCustomer_ByIP(IWebDriver imsDriver,string userName, string IpAddress, string msg)
        {


            BaseTest.AddTestCase("Check log for specific event "+msg , "Message should be present");
            imsDriver.SwitchTo().DefaultContent();
           
            wAction.Click(imsDriver, By.Id(IMS_Control_Report.Monitoring_Rpt_ID), "Monitoring & Reporting not found", FrameGlobals.reloadTimeOut, false);
            wAction.Click(imsDriver, By.LinkText("Event log"), "Event Log link not found");
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));

            wAction.Type(imsDriver, By.Id("remoteip"), IpAddress, "Username text box not found");
            wAction.SelectDropdownOption(imsDriver, By.Id("user"), "player", "User Type dropdown not found");
            wAction.Click(imsDriver, By.Id("show"), "show log button not found");

            // string errorMsg = baseTest.ReadxmlData("err", "Ecom_Invalid_Lgn", DataFilePath.IP2_Authetication);
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath("//tr[td[a[text()='"+userName+"']] and td[text()='playerlogin_failed']]/td[7]"),"No Log found for the user",false).Contains(msg),msg + " message not found");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();




        }

        public bool Verify_CustomerDepositStatus_IMS(IWebDriver imsDriver, string userName)
        {

            wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "monitorreport_btn", "Monitoring and Reporting link not found", 0, false);
            wAction.WaitforPageLoad(imsDriver);
            //Click on event log
            wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.xpath, "eventLog_link", "Event log link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));

            //Select time period from the drop down as today

            wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "period_cmb", "Today", "Period combo not found", 0, false);

            //Select the User type as Player 
            wAction._SelectDropdownOption(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "user_cmb", "player", "User type combo not found", 0, false);


            //Enter the user name - "test_aditi_CNCNAPATC"
            wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "username_txt", userName, "User name text box not found", 0, false);

            //Enter the event name as = > 'rp_deposit_done'
            wAction._Type(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "eventname_txt", "rp_deposit_done", " Event name text box not found", 0, false);

            //Click on show event
            wAction._Click(imsDriver, ORFile.IMSBonus, wActions.locatorType.id, "showEvent_btn", "Show log button not found", 0, false);


            string depositDonePath = "//table[contains(@class,'result')]//td[contains(translate(.,'E','e'),'external deposit')]";

            if (wAction.IsElementPresent(imsDriver, By.XPath(depositDonePath)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Open_CheckLog(IWebDriver imsDriver)
        {


            BaseTest.AddTestCase("Open Log for user", "Log should be present");
            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.eventlog_byUsername_XP), "Event log link not found", FrameGlobals.reloadTimeOut, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
            wAction.SelectDropdownOption(imsDriver, By.Id(IMS_Control_CasinoLog.TimePeriod_cmb_ID), "Today", "Event log link not found", FrameGlobals.reloadTimeOut, false);
            wAction.Click(imsDriver, By.Id(IMS_Control_CasinoLog.Show_Btn_ID), "show log button not found");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();
            
        }

        public void Validate_CheckLog(IWebDriver imsDriver,string LogRowXpath, string LogMsg)
        {


            BaseTest.AddTestCase("Validate given log for the user:"+LogMsg, "Log should be present");
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
            wAction.Click(imsDriver, By.Id(IMS_Control_CasinoLog.Show_Btn_ID), "show log button not found");
              string actual=null;bool flag= false;
            List<IWebElement> logs = wAction.ReturnWebElements(imsDriver, By.XPath(LogRowXpath));
            foreach (IWebElement ele in logs)
            {
                actual = ele.Text;
                if (actual.Contains(LogMsg)) {flag = true; break;}

            }
            BaseTest.Assert.IsTrue(flag, "Expected: " + LogMsg + " actual: " + actual);
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }

        public void Validate_SessionLogin(IWebDriver imsDriver, string LogRowXpath, string LogMsg)
        {


            BaseTest.AddTestCase("Validate given session log for the user:"+LogMsg, "Session Log should be present");
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.Login_Tab_id), "Login Session link not found",0,false);
             string actual =wAction.GetText(imsDriver, By.XPath(LogRowXpath));
            BaseTest.Assert.IsTrue(actual.Contains(LogMsg), "Expected: "+ LogMsg +" actual: "+actual);
            BaseTest.Pass();


        }

        public void BannedCountrySet(IWebDriver imsDriver, string countryName)
        {
            wAction.WaitforPageLoad(imsDriver);
            System.Threading.Thread.Sleep(5000);

            BaseTest.AddTestCase("Modify Banned country and set "+ countryName, countryName+" should be set successfully");
            wAction.Click(imsDriver, By.Id(IMS_Control_Product.Product_Tab_ID), "product settings link not found", 0, false);
            wAction.WaitforPageLoad(imsDriver);
            System.Threading.Thread.Sleep(2000);
            wAction.Click(imsDriver, By.LinkText("Casinos"), "Casinos link is not available in Products page, IMS");
            wAction.WaitforPageLoad(imsDriver);
            string casinodropdown = (FrameGlobals.projectName != "IP2" ? "ladbrokesvegasstg" : "ladbrokesvegastest");

            wAction.Click(imsDriver, By.XPath("//a[contains(text(),'" + casinodropdown + "')]"), "Config link for Ladbrokes vegas is not found, IMS");
            wAction.WaitforPageLoad(imsDriver);
            System.Threading.Thread.Sleep(7000);
            wAction.Click(imsDriver, By.LinkText(IMS_Control_Product.Signupcountries_Lnk), "Signup countries link for Ladbrokes vegas is not found, IMS");
            
            System.Threading.Thread.Sleep(10000);
            int esc = 0; 
            
            rep:
            imsDriver.SwitchTo().DefaultContent();
            wAction.WaitAndMovetoFrame(imsDriver, By.Id("host"), null, 60);
            if (!wAction.IsElementPresent(imsDriver, By.Id(IMS_Control_Product.AllowedCountry_ID)) && esc++ < 3)
                goto rep;


            wAction.SelectDropdownOption(imsDriver, By.Id(IMS_Control_Product.AllowedCountry_ID),countryName, "AllowedCountry_ID is not found in page",0,false);
            wAction.Click(imsDriver, By.Id(IMS_Control_Product.RightArrow_ID), "RightArrow_ID not found");
            wAction.SelectDropdownOption(imsDriver, By.Id(IMS_Control_Product.BannedCountry_ID), countryName, countryName+ " not found in the Banned list");
            wAction.Click(imsDriver, By.Id(IMS_Control_Product.UpdateBannedList_ID), "UpdateBannedList_ID not found");
            wAction.WaitAndAccepAlert(imsDriver);
            BaseTest.Pass();

        }
        public void BannedCountryRemove_HalfNavigation(IWebDriver imsDriver, string countryName)
        {
           
            BaseTest.AddTestCase("Remove banned country "+countryName, "country removed from banned list");
           
            imsDriver.SwitchTo().DefaultContent();
            wAction.WaitAndMovetoFrame(imsDriver, By.Id("host"));
          
            wAction.SelectDropdownOption(imsDriver, By.Id(IMS_Control_Product.BannedCountry_ID), countryName, countryName + " not found in the Banned list");
            wAction.Click(imsDriver, By.Id(IMS_Control_Product.LeftArrow_ID), "LeftArrow_ID not found");
            wAction.SelectDropdownOption(imsDriver, By.Id(IMS_Control_Product.AllowedCountry_ID), countryName, "AllowedCountry_ID is not found in page");
            
            wAction.Click(imsDriver, By.Id(IMS_Control_Product.UpdateBannedList_ID), "UpdateBannedList_ID not found");
            wAction.WaitAndAccepAlert(imsDriver);
            BaseTest.Pass();

        }                




        //====================================IP2=====================================
        public int GetLoginFailAttempt(IWebDriver imsDriver)
        {
            BaseTest.AddTestCase("Search for the login attempt value", "Login attempt page/value not found");
            wAction.Click(imsDriver, By.XPath("//span[text()='Products']"), "Products tab not found", 90, false);
            wAction.Click(imsDriver, By.LinkText("Casinos"), "Casino link not found", 0,false);
            string casinodropdown = (FrameGlobals.projectName != "IP2" ? "ladbrokesvegasstg" : "ladbrokesvegastest");
            wAction.Click(imsDriver, By.LinkText(casinodropdown), casinodropdown+" link not found", 0, false);
            wAction.Click(imsDriver, By.LinkText("Authentication configuration"), "Authentication configuration link not found", 0, false);
         
            System.Threading.Thread.Sleep(6000);
            imsDriver.SwitchTo().DefaultContent();
            
            wAction.WaitAndMovetoFrame(imsDriver, By.Id("casino-content"), null, 30);


            IJavaScriptExecutor jse = (IJavaScriptExecutor)imsDriver;
            jse.ExecuteScript("window.scrollBy(0,350)", "");
           
            string val = wAction.GetSelectedDropdownOptionText(imsDriver, By.Id("loginservice[retriecount]"), "Fail attempt drop down not found",0,false);
            BaseTest.Pass();
            imsDriver.SwitchTo().DefaultContent();
            return int.Parse(val);

        }
        public void SetLoginFailAttempt(IWebDriver imsDriver,int limit)
        {
            BaseTest.AddTestCase("Search for the login attempt value", "Login attempt page/value not found");
            wAction.Click(imsDriver, By.XPath("//span[text()='Products']"), "Products tab not found", 90, false);
            wAction.Click(imsDriver, By.LinkText("Casinos"), "Casino link not found", 0, false);
            
            string casinodropdown = (FrameGlobals.projectName != "IP2" ? "ladbrokesvegasstg" : "ladbrokesvegastest");

            wAction.Click(imsDriver, By.LinkText(casinodropdown), casinodropdown + " link not found", 0, false);
            wAction.Click(imsDriver, By.LinkText("Authentication configuration"), "Authentication configuration link not found", 0, false);
            
            System.Threading.Thread.Sleep(6000);
            imsDriver.SwitchTo().DefaultContent();
            wAction.WaitAndMovetoFrame(imsDriver, By.Id("main-content"));

            IJavaScriptExecutor jse = (IJavaScriptExecutor)imsDriver;
            jse.ExecuteScript("window.scrollBy(0,350)", "");

            wAction.SelectDropdownOption_ByValue(imsDriver, By.Id("loginservice[retriecount]"),limit.ToString(), "Fail attempt drop down not found", 0, false);
            wAction.Click(imsDriver, By.Id("update"), "update button not found");
            wAction.WaitUntilElementPresent(imsDriver, By.XPath("//div[text()='Authentication parameters updated.']"));
            BaseTest.Pass();
            imsDriver.SwitchTo().DefaultContent();
            

        }

        public void SetBadData(IWebDriver imsDriver)
        {
            BaseTest.AddTestCase("Set bad details for the given customer", "Bad data details should be set successfully");

            wAction.Click(imsDriver, By.Id("addbaduserdata"), "addbaduserdata button not found", 0, false);

            wAction.WaitAndMovetoPopUPWindow_WithIndex(imsDriver, 1,"Bad data window did not open");
            wAction.Click(imsDriver, By.Id("cbCurrentReason_6"), "Bad Advertisor checkbox not found", 0, false);
            wAction.Click(imsDriver, By.Id("cbSerial"), "Serial checkbox not found");
            wAction.Click(imsDriver, By.Id("cbEmail"), "Email checkbox not found");
            wAction.Type(imsDriver, By.Id("badDataComment"),"test", "Bada data comment box not found");
            wAction.Click(imsDriver, By.Id("cbSelectable"), "Operators checkbox not found");
            wAction.Click(imsDriver, By.Id("cbSelectable_4"), "companies checkbox not found");
            wAction.Click(imsDriver, By.Id("cbSelectable_7"), "casinos checkbox not found");


            System.Threading.Thread.Sleep(1000);
            wAction.Click(imsDriver, By.Id("save"), "save button not found");

         //   imsDriver.SwitchTo().DefaultContent();
            

           BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath("//div[text()='Bad data details successfully added.']")),"Bad data set failed");

            BaseTest.Pass();
            wAction.BrowserClose(imsDriver);
            wAction.WaitAndMovetoPopUPWindow_WithIndex(imsDriver, 0, "Cannot switch back to player management window");
         //   imsDriver.SwitchTo().DefaultContent();


        }
        public void SetBadData_RiskManagement(IWebDriver imsDriver,string uname)
        {
            BaseTest.AddTestCase("Set bad details for the given customer", "Bad data details should be set successfully");

            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.riskMgmtLink_ID), "RiskManagement link not found", 0, false);


            wAction.Click(imsDriver, By.LinkText("Bad data"), "Bad data link not found", 0, false);


            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.BadData_AddNew_XP), "Add new button not found");
            string casinodropdown = (FrameGlobals.projectName != "IP2" ? "ladbrokesvegasstg" : "ladbrokesvegastest");

          
      
            wAction.Type(imsDriver, By.Id(IMS_Control_Rules.BadData_Uname_ID),uname, "UserName box not found");
            wAction.Type(imsDriver, By.Id(IMS_Control_Rules.BadData_comments_ID), uname, "UserName box not found");
            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.BadData_LoginBan_ID), "Login Ban checkbox not found");
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.BadData_Ladbrokes_XP), "Ladbrokes checkbox not found");
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.BadData_BadDetails_XP), "Bad Details checkbox not found");
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.BadData_CumpolsiveGambler_XP), "Bad cumpolsive gambler checkbox not found");
            wAction.Click(imsDriver, By.Id("imgcompanies"));
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.BadData_LadbrokesPLC_XP), "Ladbrokes PLC checkbox not found");
            wAction.Click(imsDriver, By.Id("imgcasinos"));  
            wAction.Click(imsDriver, By.XPath("//td[normalize-space()='" + casinodropdown + "']/input"), casinodropdown+" checkbox not found");
        
            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.BadData_AddBtn_ID), "BadData_AddBtn_Create button not found");
            
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_Rules.BadData_BadDataSearchPage_XP)), "Bad data search page load failed");

            BaseTest.Pass();
            

        }
        public void Restrict_IP_Rules(IWebDriver imsDriver,string rule,string IP)
        {
            BaseTest.AddTestCase("Set IP lock for the given Ip : "+rule, "IP block details should be set successfully");


            
            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.riskMgmtLink_ID), "risk mgmt link not found", 0, false);


            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.automationRules_XP), "Automation rules link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID),null,30);
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Search_XP), "Search button not found");
            wAction.Click(imsDriver, By.LinkText(rule), "Restrict login by IP not found", 0, false);

            wAction.WaitforPageLoad(imsDriver);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            if (wAction.IsElementPresent(imsDriver, By.XPath(IMS_Control_Rules.disable_XP)))
                wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.disable_XP));

                wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.enable_XP), "Enable button not found");
                wAction.WaitAndAccepAlert(imsDriver);
                wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Edit_XP), "Edit button not found");
                wAction.WaitAndAccepAlert(imsDriver);
                wAction.Clear(imsDriver, By.Id(IMS_Control_Rules.Condition_ID), "Condition box not found");
                wAction.Type(imsDriver, By.Id(IMS_Control_Rules.Condition_ID), "Player.CurrentIP='" + IP + "'", "Condition box not found");
                wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.save_XP), "Edit button not found");
                wAction.WaitAndAccepAlert(imsDriver);
                System.Threading.Thread.Sleep(1000);
            
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_Rules.saveMsg_XP)), "Save Msg not displayed");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();
            

        }

        public void Restrict_IP_Register(IWebDriver imsDriver)
        {
            BaseTest.AddTestCase("Set IP lock for the given Ip", "IP block details should be set successfully");



            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.riskMgmtLink_ID), "risk mgmt link not found", 0, false);


            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.automationRules_XP), "Automation rules link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID), null, 30);
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Search_XP), "Search button not found");
           
            if (FrameGlobals.projectName == "IP2")
                wAction.Click(imsDriver, By.LinkText(IMS_Control_Rules.RegByIP_Lnk_Test), "Restrict login by IP not found", 0, false);
            else
                wAction.Click(imsDriver, By.LinkText(IMS_Control_Rules.RegByIP_Lnk_Stage), "Restrict login by IP not found", 0, false);

            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.enable_XP), "Enable button not found");
            wAction.WaitAndAccepAlert(imsDriver);
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Edit_XP), "Edit button not found");
            wAction.WaitAndAccepAlert(imsDriver);
            wAction.Clear(imsDriver, By.Id(IMS_Control_Rules.Condition_ID), "Condition box not found");
            wAction.Type(imsDriver, By.Id(IMS_Control_Rules.Condition_ID), "player.signupipcountry IN (\"IN\")", "Condition box not found");
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.save_XP), "Edit button not found");
            wAction.WaitAndAccepAlert(imsDriver);
            System.Threading.Thread.Sleep(1000);

            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_Rules.saveMsg_XP)), "Save Msg not displayed");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }
        public void Restrict_IP_Disable(IWebDriver imsDriver,string rule)
        {
            BaseTest.AddTestCase("Disable IP lock for the given Ip", "IP block details should be set successfully");

            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.riskMgmtLink_ID), "risk mgmt link not found", 0, false);


            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.automationRules_XP), "Automation rules link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID), null, 30);
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Search_XP), "Search button not found");
          
                wAction.Click(imsDriver, By.LinkText(rule), rule+" not found", 0, false);

            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.disable_XP), "Disable button not found");
            wAction.WaitAndAccepAlert(imsDriver);


            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_Rules.DisableMsg_XP)), "Disable Msg not displayed");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }


        public void Restrict_Country(IWebDriver imsDriver,string rule,string country)
        {
            BaseTest.AddTestCase("Set IP lock for the given Ip", "IP block details should be set successfully");



            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.riskMgmtLink_ID), "risk mgmt link not found", 0, false);


            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.automationRules_XP), "Automation rules link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID), null, 30);
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Search_XP), "Search button not found");

     
                wAction.Click(imsDriver, By.LinkText(rule), rule+" link not found", 0, false);

                if (!wAction.IsElementPresent(imsDriver, By.XPath(IMS_Control_Rules.disable_XP)))
                {

                    wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.enable_XP), "Enable button not found");
                    wAction.WaitAndAccepAlert(imsDriver);
                    wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Edit_XP), "Edit button not found");
                    wAction.WaitAndAccepAlert(imsDriver);
                    wAction.Clear(imsDriver, By.Id(IMS_Control_Rules.Condition_ID), "Condition box not found");
                    wAction.Type(imsDriver, By.Id(IMS_Control_Rules.Condition_ID), "player.signupipcountry IN (\"" + country + "\")", "Condition box not found");
                    wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.save_XP), "Edit button not found");
                    wAction.WaitAndAccepAlert(imsDriver);
                    System.Threading.Thread.Sleep(1000);

                    BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_Rules.saveMsg_XP)), "Save Msg not displayed");
                }
                imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }
        public void Restrict_IP_Rule_Disable(IWebDriver imsDriver,string rule)
        {
            BaseTest.AddTestCase("Disable IP lock for the given Ip", "IP block details should be set successfully");

            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.riskMgmtLink_ID), "risk mgmt link not found", 0, false);


            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.automationRules_XP), "Automation rules link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID), null, 30);
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Search_XP), "Search button not found");
            wAction.Click(imsDriver, By.LinkText(rule), "Restrict login by IP not found", 0, false);
            if (!wAction.IsElementPresent(imsDriver, By.XPath(IMS_Control_Rules.enable_XP)))
            {
                if (!wAction.IsElementPresent(imsDriver, By.XPath(IMS_Control_Rules.DisableMsg_XP)))
                {

                    wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.disable_XP), "Disable button not found");
                    wAction.WaitAndAccepAlert(imsDriver);

                }

                BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_Rules.DisableMsg_XP)), "Disable Msg not displayed");

            }
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }
        public void Restrict_IP_Reg_Disable(IWebDriver imsDriver)
        {
            BaseTest.AddTestCase("Disable IP lock for the given Ip", "IP block details should be set successfully");

            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.riskMgmtLink_ID), "risk mgmt link not found", 0, false);


            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.automationRules_XP), "Automation rules link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID), null, 30);
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Search_XP), "Search button not found");
            if (FrameGlobals.projectName == "IP2")
            wAction.Click(imsDriver, By.LinkText(IMS_Control_Rules.RegByIP_Lnk_Test), "Restrict login by IP not found", 0, false);
            else
            wAction.Click(imsDriver, By.LinkText(IMS_Control_Rules.RegByIP_Lnk_Stage), "Restrict login by IP not found", 0, false);

            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.disable_XP), "Disable button not found");
            wAction.WaitAndAccepAlert(imsDriver);


            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_Rules.DisableMsg_XP)), "Disable Msg not displayed");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }
        public void Restrict_BannedCountry_Enable(IWebDriver imsDriver,string rule)
        {
            BaseTest.AddTestCase("Enable country lock for the given country : "+ rule, "Country block details should be set successfully");

            wAction.Click(imsDriver, By.Id(IMS_Control_Rules.riskMgmtLink_ID), "risk mgmt link not found", 0, false);


            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.automationRules_XP), "Automation rules link not found", 0, false);
            wAction.WaitAndMovetoFrame(imsDriver, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID), null, 30);
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.Search_XP), "Search button not found");
            wAction.Click(imsDriver, By.LinkText(rule), rule + " not found", 0, false);
            if(!wAction.IsElementPresent(imsDriver, By.XPath(IMS_Control_Rules.disable_XP)))
            {
            wAction.Click(imsDriver, By.XPath(IMS_Control_Rules.enable_XP), "Enable button not found");
            wAction.WaitAndAccepAlert(imsDriver);
            }

            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_Rules.disable_XP)), "Rule not enabled");
            imsDriver.SwitchTo().DefaultContent();
            BaseTest.Pass();


        }

        public void Closecustomer(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Close the customer", "The given customer should be closed");
          wAction.Click(driverObj,By.Id("close"),"Close Button not found",0,false);
          wAction.WaitAndAccepAlert(driverObj);
          System.Threading.Thread.Sleep(5000);
          BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath("//div[text()='Account closed']")), "Account not closed yet!");

        }
        public void ComplianceReportView(IWebDriver driverObj, string reportName, double custID)
        {
            BaseTest.AddTestCase("Get report of " + reportName + " ;for " + custID, "Reports should be available");
            wAction.Click(driverObj, By.Id(IMS_Control_Report.Monitoring_Rpt_ID), "Monitoring and Report link not found",0,false);
            wAction.Click(driverObj, By.LinkText(IMS_Control_Report.Rpt_Viewer_Lnk), "Report view link not found", 0, false);
            wAction.WaitAndMovetoFrame(driverObj, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
            wAction.SelectDropdownOption(driverObj, By.Id(IMS_Control_Report.Rpt_Grp_ID), "CURL", "Report Category drop down not found", 0, false);
           
            wAction.SelectDropdownOption(driverObj, By.Id(IMS_Control_Report.Rpt_code_ID), reportName, "Report code drop down not found");

            
                string casinodropdown = (FrameGlobals.projectName != "IP2" ? "ladbrokesvegasstg" : "ladbrokesvegastest");
                wAction.SelectDropdownOption(driverObj, By.Id(IMS_Control_Report.Casino_ID), casinodropdown);
            

            wAction.Click(driverObj, By.Name(IMS_Control_Report.submit_Name), "Submit button not found");
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.XPath("//td[contains(text(),'"+(int)(custID)+"')]")), "Given customer not found in the report");
            BaseTest.Pass();


        }

        public void ComplianceReportView_RestIP(IWebDriver driverObj, string reportName, string ruleName , string username)
        {
            BaseTest.AddTestCase("Get report of " + reportName + " ;for " + ruleName, "Reports should be available");
           
            wAction.Click(driverObj, By.Id(IMS_Control_Report.Monitoring_Rpt_ID), "Monitoring and Report link not found", 0, false);
            wAction.Click(driverObj, By.LinkText(IMS_Control_Report.Rpt_Viewer_Lnk), "Report view link not found", 0, false);
            wAction.WaitAndMovetoFrame(driverObj, By.Id(IMS_Control_PlayerDetails.Main_Frame_ID));
           //wAction.SelectDropdownOption(driverObj, By.Id(IMS_Control_Report.Rpt_Grp_ID), "CURL", "Report Category drop down not found", 0, false);

            wAction.SelectDropdownOption(driverObj, By.Id(IMS_Control_Report.Rpt_code_ID), reportName, "Report code drop down not found");
            


            string casinodropdown = (FrameGlobals.projectName != "IP2" ? "ladbrokesvegasstg" : "ladbrokesvegastest");
                wAction.SelectDropdownOption(driverObj, By.Id(IMS_Control_Report.Casino_ID), casinodropdown);
               

            string date = wAction.GetAttribute(driverObj, By.Id(IMS_Control_Report.enddate_ID), "value", "End date text box not found",false);
            DateTime dt2 = Convert.ToDateTime(date);
            dt2 = dt2.AddDays(1);
            date = dt2.ToString("yyyy-MM-dd hh:mm");

            wAction.Clear(driverObj, By.Id(IMS_Control_Report.enddate_ID));
            wAction.Type(driverObj, By.Id(IMS_Control_Report.enddate_ID), date);
            wAction.Clear(driverObj, By.Id(IMS_Control_Report.suedate_ID));
            wAction.Type(driverObj, By.Id(IMS_Control_Report.suedate_ID), date);
            wAction.SelectDropdownOption(driverObj, By.Id(IMS_Control_Report.ruleName_ID), ruleName, "Rule name drop down not found");




            wAction.Click(driverObj, By.Name(IMS_Control_Report.submit_Name), "Submit button not found");
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(driverObj, By.XPath("//tr[td[@value='"+username+"']]")), "Given username: "+username+" not found in the report");
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath("//tr[td[@value='" + username + "']]/td[5]")).Contains(ruleName), "Rule name not found for: " + username + " in the report");
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath("//tr[td[@value='" + username + "']]/td[6]")).Contains("United Kingdom"), "Country name not found for: " + username + " in the report");
            BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath("//tr[td[@value='" + username + "']]/td[10]")).Contains("web"), "Client Platform is not web for: " + username + " in the report");

            BaseTest.Pass();


        }

        public string SettleABet(IWebDriver imsDriver, string winnings,string refund = "0",string win="1",string lose="0",string voids="0",string AdjustStake=null)
        {
            BaseTest.AddTestCase("Settle a bet and return the winnings", "Bet settle should be successfully");

            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.walletTransaction_id), "walletTransaction Navigation button not found", 0, false);
            System.Threading.Thread.Sleep(3000);
            wAction.Type(imsDriver, By.Id(IMS_Control_PlayerDetails.eventDate_id),"Today", "Event date dropdown not found");
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.eventDate_id), "Event date dropdown not found");
            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.eventDate_today_xp), "List dropdown not found");
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.eventCheck_id), "Include wallet games check box not found");
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.WalletSearch_id), "Search bttn not found");
            //System.Threading.Thread.Sleep(5000);
            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.bet_settle_link_xp), "Remote ID link not found",0,false);
            string mainwin = imsDriver.CurrentWindowHandle;
            wAction.WaitAndMovetoPopUPWindow(imsDriver, "New window not found",10);
            string url = imsDriver.Url;
            System.Threading.Thread.Sleep(2000);
            if(  wAction.IsElementPresent(imsDriver, By.Id("username")))
            {
            wAction.Type(imsDriver, By.Id("username"), FrameGlobals.AdminName, "Username field not found");
            wAction.Type(imsDriver, By.Id("password"), FrameGlobals.AdminPass, "password field not found");
            wAction.Click(imsDriver, By.XPath("//input[@type='submit' and @value='Login']"), "Submit link not found");
            wAction.OpenURL(imsDriver, url, "target page not loaded");
            }

            wAction.WaitAndMovetoFrame(imsDriver, By.Name("MainArea"));
            wAction.Type(imsDriver, By.Name(IMS_Control_PlayerDetails.BetSettle_BetWinnings_name), winnings, "Winning box not found", 0, false);
            wAction.Type(imsDriver, By.Name(IMS_Control_PlayerDetails.BetSettle_BetRefund_name), refund, "refund box not found");
            wAction.Type(imsDriver, By.Name(IMS_Control_PlayerDetails.BetSettle_BetWinLines_name), win, "win box not found");
            wAction.Type(imsDriver, By.Name(IMS_Control_PlayerDetails.BetSettle_BetLoseLines_name), lose, "lose box not found");
            wAction.Type(imsDriver, By.Name(IMS_Control_PlayerDetails.BetSettle_BetVoidLines_name), voids, "void box not found");
            wAction.Type(imsDriver, By.Name(IMS_Control_PlayerDetails.BetSettle_BetComment_name), "Test", "comment box not found");
            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.BetSettle_Settle_XP), "Settle button not found");
           string winns =  wAction.GetText(imsDriver, By.XPath(IMS_Control_PlayerDetails.BetSettle_WinningAmt_XP));
            BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath(IMS_Control_PlayerDetails.BetSettle_Success_XP)), "success msg not found");
           if(AdjustStake!=null)
           {
               wAction.Click(imsDriver, By.XPath("//input[@value='Adjust Stake']"), "Adjust to stake button not found");
               wAction.SelectDropdownOption(imsDriver, By.Name("ManAdjChannel"), "1", "ManAdjChannel dropdown not found", 0, false);
               wAction.SelectDropdownOption(imsDriver, By.Name("ManAdjEventDesc"), "1", "ManAdjEventDesc dropdown not found");
               wAction.Type(imsDriver, By.Name("Amount"), AdjustStake, "Adjust stake amount text box not found");
               wAction.Click(imsDriver, By.XPath("//input[@value='Do Manual Adjustment']"), "Do Manual Adjustment button not found");
               wAction.WaitAndAccepAlert(imsDriver);
               System.Threading.Thread.Sleep(5000);
               BaseTest.Assert.IsTrue(wAction.IsElementPresent(imsDriver, By.XPath("//*[contains(text(),'Manual Adjustment Successful')]")), "success msg not found");
        
           }

            imsDriver.SwitchTo().Window(mainwin);

          
               
            BaseTest.Pass();
            return winns;
        
        }
        public void VerifyWalletHistoryForBet(IWebDriver imsDriver, string amount)
        {
            BaseTest.AddTestCase("Verify that wallet transaction history has the bet details", "Bet validation should be success");

            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.walletTransaction_id), "walletTransaction Navigation button not found", 0, false);
            System.Threading.Thread.Sleep(3000);
            wAction.Clear(imsDriver, By.Id(IMS_Control_PlayerDetails.eventDate_id), "Event date dropdown not found");
            wAction.Type(imsDriver, By.Id(IMS_Control_PlayerDetails.eventDate_id), "Today", "Event date dropdown not found");
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.eventDate_id), "Event date dropdown not found");
            wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.eventDate_today_xp), "List dropdown not found");
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.eventCheck_id), "Include wallet games check box not found");
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.WalletSearch_id), "Search bttn not found");
            DateTime today = DateTime.Today;
            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath("id('playerWalletActionsList_form')//tr[td[contains(text(),'"+amount+"')][1] and td[contains(text(),'"+today.ToString("yyyy-MM-dd")+"')]]/td[contains(string(),'Gaming')]")), "Wallet trans does not contains the bet history");
           
            
            //wAction.Click(imsDriver, By.XPath(IMS_Control_PlayerDetails.bet_settle_link_xp), "Remote ID link not found", 0, false);
            wAction.Click(imsDriver, By.XPath("id('playerWalletActionsList_form')//tr[td[contains(text(),'" + amount + "')][1] and td[contains(text(),'" + today.ToString("yyyy-MM-dd") + "')]]/td[8]/a"), "Remote ID link not found", 0, false);
            string mainwin = imsDriver.CurrentWindowHandle;
            wAction.WaitAndMovetoPopUPWindow(imsDriver, "New window not found", 10);
            string url = imsDriver.Url;
            System.Threading.Thread.Sleep(3000);
            if (wAction.IsElementPresent(imsDriver, By.Id("username")))
            {
                wAction.Type(imsDriver, By.Id("username"), FrameGlobals.AdminName, "Username field not found");
                wAction.Type(imsDriver, By.Id("password"), FrameGlobals.AdminPass, "password field not found");
                wAction.Click(imsDriver, By.XPath("//input[@type='submit' and @value='Login']"), "Submit link not found");
                wAction.OpenURL(imsDriver, url, "target page not loaded");
            }
            System.Threading.Thread.Sleep(5000);
            wAction.WaitAndMovetoFrame(imsDriver, By.Name("MainArea"));
            BaseTest.Assert.IsTrue(wAction.GetText(imsDriver, By.XPath(IMS_Control_PlayerDetails.BetSettle_Stake_XP), "Stake value not found").Contains(amount),"Stake amount n OB not matching");
            imsDriver.Close();
            imsDriver.SwitchTo().Window(mainwin);
            BaseTest.Pass();
            

        }
        public void AddStatementClearanceOptions(IWebDriver imsDriver, bool statementreportenabled = false, string stmtminpaymthreshold = null, string custom10=null,bool pulled=false,bool retain=false)
        {
            BaseTest.AddTestCase("Setup statement clearance option for the customer in IMS", "Statement clearance added successfully");

            if(statementreportenabled)
                wAction.Click(imsDriver, By.Id("statementreportenabled"), "statementreportenabled checkbox not found", 0, false);

            if(stmtminpaymthreshold!=null)
            wAction.Type(imsDriver, By.Id("stmtminpaymthreshold"), stmtminpaymthreshold, "stmtminpaymthreshold textbox not found");

            if (pulled)
            {
                wAction.Click(imsDriver, By.Id("includedinpulledcycle"), "includedinpulledcycle checkbox not found", 0, false);
                wAction.Type(imsDriver, By.Id("pulledcyclereason"), "includedinpulledcycle: " + DateTime.Today, "pulledcyclereason textbox not found");
            }

            if(retain)
                wAction.Click(imsDriver, By.Id("stmtretainfunds"), "stmtretainfunds checkbox not found");
            
            if (custom10 != null)
            {
                commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
                wAction.Type(imsDriver, By.Id("custom10"), custom10, "custom10 textbox not found");
            }

            wAction.Click(imsDriver, By.Id("update"), "update info btn not found");
            wAction.WaitAndAccepAlert(imsDriver);
            System.Threading.Thread.Sleep(3000);
            wAction.Click(imsDriver, By.XPath("id('updateSuccess')//input[@value='Close']"),noWait:false);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
           BaseTest.Pass();
        }
        public void SetAccountSweep(IWebDriver imsDriver)
        {
            BaseTest.AddTestCase("Add account sweep to the customer", "Customer sweep successfully set");


            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.usedpmenthods_ID), "usedpmenthods_ID tab not found");            
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction.Click(imsDriver, By.LinkText("Edit"));
            wAction.WaitAndMovetoPopUPWindow(imsDriver, "Sweep window not found");

            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.SweepingAcct_ID), "SweepingAcct_ID checkbox not found",0,false);
            wAction.Click(imsDriver, By.Id(IMS_Control_PlayerDetails.UpdateSweep_ID), "UpdateSweep_ID button not found");
      

            BaseTest.Assert.IsTrue(wAction.WaitUntilElementPresent(imsDriver, By.XPath(IMS_Control_PlayerDetails.SweepSuccess_XP)), "SweepSuccess_XP not successful");
            BaseTest.Pass();
            wAction.BrowserClose(imsDriver);
            wAction.WaitAndMovetoPopUPWindow_WithIndex(imsDriver, 0, "Main window missing");
            

        }

        public bool ValidateWalletBalance(IWebDriver imsDriver, string wallet, string amt)
        {
            
            BaseTest.AddTestCase("Verify the balance is updated with given balance :"+amt+" for given wallet:"+wallet, "Balance should match as expected");
            int i = 0; bool flag = false;

            wAction._Click(imsDriver, ORFile.IMSCustDetails, wActions.locatorType.id, "fundTransfer_lnk", "Fund Transfer link not found", 0, false);
           System.Threading.Thread.Sleep(2000);           

            while (i++ < 3)
            {
                wAction.Click(imsDriver, By.Id("refreshLink"), "Refresh link not found", 0, false);
                System.Threading.Thread.Sleep(5000);
                if (StringCommonMethods.ReadDoublefromString(wAction.GetText(imsDriver, By.XPath("id('bexgrid_table')//tr[td[contains(text(),'" + wallet + "')]]/td[3]"))) == StringCommonMethods.ReadDoublefromString(amt))
                { flag = true; break; }
            }

            BaseTest.Pass();
            return flag;

        }

    }//class
}//namespace
