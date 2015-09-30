using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Data;
using System.Text.RegularExpressions;
using Framework;
using Selenium;
using Framework.Common;
using System.Globalization;
using ICE.ObjectRepository.Vegas_IMS_BAU;
//using ICE.Vegas_IMS_Data;
//using ICE.ActionRepository;
using ICE.DataRepository.Vegas_IMS_Data;
using ICE.ObjectRepository;
using System.Threading;

namespace Ladbrokes_IMS_TestRepository
{
    public class Common
    {
        WebCommonMethods commonWebMethods = new WebCommonMethods();
        AdminSuite.Common suiteAdmin = new AdminSuite.Common();
        wActions wAction = new wActions();
    
        /// <summary>
        /// Author:Naga
        /// Enter the details of the customer in Registration page
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="regData">registration details</param>
        /// <returns></returns>
        public string Registration_AddCust(IWebDriver driverObj, ref Registration_Data regData)
        {
            if (FrameGlobals.waitMilliSec_Reg != 0)
                FrameGlobals.waitMilliSec_Reg = FrameGlobals.waitMilliSec_Reg + 10000;
            System.Threading.Thread.Sleep(FrameGlobals.waitMilliSec_Reg);
            if (FrameGlobals.waitMilliSec_Reg == 0)
                FrameGlobals.waitMilliSec_Reg = FrameGlobals.waitMilliSec_Reg + 10000;

            Object thisLock = new Object();
            lock (thisLock)
            {
                string uname = "Failed";

                try
                {
                    int accept = 3;

                fname:
                    commonWebMethods.Clear(driverObj, By.Id("fname"), "FirstName not found", 0, false);
                    //driverObj.FindElement(By.Id("fname")).SendKeys(regData.fname);
                    commonWebMethods.Type(driverObj, By.Id("fname"), regData.fname, "First name not found");
                lname:
                    driverObj.FindElement(By.Id("lname")).Clear();
                    //driverObj.FindElement(By.Id("lname")).SendKeys(regData.lname);
                    commonWebMethods.Type(driverObj, By.Id("lname"), regData.lname, "Last Name not found");

                    //driverObj.FindElement(By.Id("country_code")).SendKeys(regData.country_code);
                    commonWebMethods.SelectDropdownOption_ByPartialText(driverObj, By.Id("country_code"), regData.country_code, "Country Code not Found");
                   // commonWebMethods.Type(driverObj, By.Id("country_code"), regData.country_code, "Country Code not Found");

                address:
                    if (regData.country_code == "United Kingdom")
                    {
                        commonWebMethods.Clear(driverObj, By.Id("uk_addr_street_1"), "Address not found", 0, false);
                        //driverObj.FindElement(By.Id("uk_addr_street_1")).SendKeys(regData.uk_addr_street_1);
                        commonWebMethods.Type(driverObj, By.Id("uk_addr_street_1"), regData.uk_addr_street_1, "Address Not Found");

                        //driverObj.FindElement(By.Id("uk_postcode")).Clear();
                        commonWebMethods.Clear(driverObj, By.Id("uk_postcode"), "Psot Code Not found", 0, false);
                        //driverObj.FindElement(By.Id("uk_postcode")).SendKeys(regData.uk_postcode);
                        commonWebMethods.Type(driverObj, By.Id("uk_postcode"), regData.uk_postcode, "Post Code Not Found");
                        //if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                        //    commonWebMethods.FireEvent(driverObj, By.XPath("//a[contains(text(),'Find Address')]"),"click","Find Address not found", false);
                        //else
                        commonWebMethods.Click(driverObj, By.LinkText("Find Address"), "Find Address not found", 0, false);

                    }
                    else
                    {
                        if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                        {
                            System.Threading.Thread.Sleep(3000);
                            //driverObj.FindElement(By.Id("other_addr_street_2")).Click();
                            commonWebMethods.Click(driverObj, By.Id("other_addr_street_2"), "Address not Found", 0, false);
                            //commonWebMethods.FireEvent(driverObj, By.XPath("//button[text()='Ok']"), "click", "Alert not found", false,true);
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"), "Button not Found", 0, false);
                        }

                        else
                        {
                            //driverObj.FindElement(By.Id("other_addr_street_2")).Click();
                            commonWebMethods.Click(driverObj, By.Id("other_addr_street_2"), "Address not Found", 0, false);
                            commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath("//div[@class='ui-dialog-titlebar']/span"));
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"), "Button not Found", 0, false);
                        }

                        //driverObj.FindElement(By.Id("other_addr_street_2")).SendKeys(Registration_Data.uk_addr_street_2);
                        commonWebMethods.Type(driverObj, By.Id("other_addr_street_2"), Registration_Data.uk_addr_street_2, "Address Not Found");
                        //driverObj.FindElement(By.Id("other_addr_city")).SendKeys(regData.city);
                        commonWebMethods.Type(driverObj, By.Id("other_addr_city"), regData.city, "City Not Found");
                        //driverObj.FindElement(By.Id("other_postcode")).SendKeys(Registration_Data.other_postcode);
                        commonWebMethods.Type(driverObj, By.Id("other_postcode"), Registration_Data.other_postcode, "Post_Code Not Found");
                    }
                dob:
                commonWebMethods.SelectDropdownOption(driverObj, By.Id("dob_day"), Registration_Data.dob_day, "DOB_Day Not Found");
                   // commonWebMethods.Type(driverObj, By.Id("dob_day"), Registration_Data.dob_day, "DOB_Day Not Found");
                commonWebMethods.SelectDropdownOption(driverObj, By.Id("dob_month"), Registration_Data.dob_month, "DOB_Month Not Found");
                commonWebMethods.SelectDropdownOption_ByPartialText(driverObj, By.Id("dob_year"), Registration_Data.dob_year, "DOB_Year Not Found");

                email:
                    //driverObj.FindElement(By.Id("email")).Clear();
                    commonWebMethods.Clear(driverObj, By.Id("email"), "Email ID not found", 0, false);
                    //driverObj.FindElement(By.Id("email")).SendKeys(regData.email);
                    commonWebMethods.Type(driverObj, By.Id("email"), regData.email, "Email ID Not Found");


                tele:
                    //driverObj.FindElement(By.Id("telephone")).Clear();
                    commonWebMethods.Clear(driverObj, By.Id("telephone"), "Telephone Number not found", 0, false);
                    //driverObj.FindElement(By.Id("telephone")).SendKeys(regData.telephone);
                    commonWebMethods.Type(driverObj, By.Id("telephone"), regData.telephone, "Telephone Number Not Found");

                    //driverObj.FindElement(By.Id("mobile")).Clear();
                    commonWebMethods.Clear(driverObj, By.Id("mobile"), "Mobile Number not found", 0, false);
                    //driverObj.FindElement(By.Id("mobile")).SendKeys(regData.mobile);
                    commonWebMethods.Type(driverObj, By.Id("mobile"), regData.mobile, "Mobile Number Not Found");

                uname:
                    DateTime dateFormat = DateTime.Now;
                    string dateTime = (dateFormat.ToString("yyyyMMddHHmmss"));
                    uname = regData.username + dateTime;

                    //driverObj.FindElement(By.Id("username")).Clear();
                    commonWebMethods.Clear(driverObj, By.Id("username"), "User Name not found", 0, false);
                    //driverObj.FindElement(By.Id("username")).SendKeys(uname);
                    commonWebMethods.Type(driverObj, By.Id("username"), uname, "User Name Not Found");

                pass:
                    //driverObj.FindElement(By.Id("password")).Clear();
                    commonWebMethods.Clear(driverObj, By.Id("password"), "PassWord not found", 0, false);
                    //driverObj.FindElement(By.Id("password")).SendKeys(regData.password);
                commonWebMethods.Type(driverObj, By.Id("password"), regData.password, "PassWord Not Found");

                pconfirm:
                    //driverObj.FindElement(By.Id("vfy_password")).Clear();
                    commonWebMethods.Clear(driverObj, By.Id("vfy_password"), "PassWord not found", 0, false);
                    //driverObj.FindElement(By.Id("vfy_password")).SendKeys(regData.password);
                commonWebMethods.Type(driverObj, By.Id("vfy_password"), regData.password, "PassWord Not Found");

                security:
                    //driverObj.FindElement(By.Id("response_1")).Clear();
                    commonWebMethods.Clear(driverObj, By.Id("response_1"), "Security Answer not found", 0, false);
                    //driverObj.FindElement(By.Id("response_1")).SendKeys(Registration_Data.response_1);
                    commonWebMethods.Type(driverObj, By.Id("response_1"), Registration_Data.response_1, "PassWord Not Found");

                    //driverObj.FindElement(By.Id("fb_promo_code")).Clear();
                    commonWebMethods.Clear(driverObj, By.Id("fb_promo_code"), "Promo_Code Not Found", 0, false);


                    if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']")))
                        //commonWebMethods.Click(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']"));
                        commonWebMethods.Click(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']"), "Button not Found", 0, false);


                checkbox:
                    if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                    {
                        //IJavaScriptExecutor js = (IJavaScriptExecutor)driverObj;
                        //js.ExecuteScript("document.getElementByID('general').checked = true;");
                        //driverObj.FindElement(By.Id("general")).SendKeys(Keys.Space);
                        commonWebMethods.Type(driverObj, By.Id("general"), Keys.Space, "Accept Check Box Not Found");
                        //commonWebMethods.FireEvent(driverObj, By.Id("general"),"", "Accept check box not found");

                    }

                    else
                        commonWebMethods.Click(driverObj, By.Id("general"), null, 0, false);


                    if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                        commonWebMethods.Type(driverObj, By.Id("submit-form"), Keys.Space, "Submit not found");
                    else
                        commonWebMethods.Click(driverObj, By.Id("submit-form"), "Submit button not found", 0, false);

                    if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//li")))
                    {
                        if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'First Name')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto fname;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Last Name')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto lname;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Address')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto address;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Town/City')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto lname;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Date of Birth')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto dob;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Telephone Number')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto tele;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Password')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto pass;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Security')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto security;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Email')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto email;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Username')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto uname;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'Password confirm')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto pconfirm;
                        }
                        else if ((commonWebMethods.IsElementPresent(driverObj, By.XPath("//li[contains(text(),'General terms confirmation')]"))) && accept-- != 0)
                        {
                            //driverObj.FindElement(By.XPath("//button[text()='Ok']")).Click();
                            commonWebMethods.Click(driverObj, By.XPath("//button[text()='Ok']"));
                            goto checkbox;
                        }
                    }
                    else
                        if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//button[text()='Ok']")))
                            throw new Exception();

                }
                catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }
                return uname;
            }

        }


        /// <summary>
        /// Author:Naga
        /// Enter the details of the customer in Registration page_Mobile
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="regData">registration details</param>
        /// <returns></returns>
        public string Registration_AddCust_Mobile(IWebDriver driverObj, ref Registration_Data regData)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            if (FrameGlobals.waitMilliSec_Reg != 0)
                FrameGlobals.waitMilliSec_Reg = FrameGlobals.waitMilliSec_Reg + 1000;
            System.Threading.Thread.Sleep(FrameGlobals.waitMilliSec_Reg);
            if (FrameGlobals.waitMilliSec_Reg == 0)
                FrameGlobals.waitMilliSec_Reg = FrameGlobals.waitMilliSec_Reg + 1000;

            Object thisLock = new Object();
            lock (thisLock)
            {
                

                string uname = "Failed";

                try
                {
                 //   int accept = 3;

                    DateTime dateFormat = DateTime.Now;
                    string dateTime = (dateFormat.ToString("yyyyMMddHHmmss"));
                    regData.fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID();
              //  fname:
                    commonWebMethods.Clear(driverObj, By.Id("firstname"), "FirstName not found", FrameGlobals.reloadTimeOut, false);
                    commonWebMethods.Type(driverObj, By.Id("firstname"), regData.fname, "FirstName not found");

             //   lname:
                    dateFormat = DateTime.Now;
                    dateTime = (dateFormat.ToString("yyyyMMddHHmmss"));
                    regData.lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID();
         
                    
                    commonWebMethods.Clear(driverObj, By.Id("lastname"), "LastName not found", 0, false);
                    commonWebMethods.Type(driverObj, By.Id("lastname"), regData.lname, "LastName not found");
                                 

             //   address:
                    if (regData.country_code == "United Kingdom")
                    {
                        commonWebMethods.Clear(driverObj, By.Id("housename"), "address box not found", 0, false);
                        commonWebMethods.Type(driverObj, By.Id("housename"), regData.uk_addr_street_1, "House Name not found");
                        commonWebMethods.Clear(driverObj, By.Id("postcode"), "Postal not found", 0, false);                  
                        commonWebMethods.Type(driverObj, By.Id("postcode"), regData.uk_postcode, "Postal not found");
                        commonWebMethods.Click(driverObj, By.Id("findaddress"), "Find Address not found");

                    }
                    else
                    {
                        commonWebMethods.Type(driverObj, By.Id("country"), regData.country_code, "Country Code not found");
                        commonWebMethods.Clear(driverObj, By.Id("housename"), "address box not found", 0, false);
                        commonWebMethods.Type(driverObj, By.Id("housename"), regData.uk_addr_street_1, "House Name not found");
                        commonWebMethods.Clear(driverObj, By.Id("postcode"), "Postal not found", 0, false);
                        commonWebMethods.Type(driverObj, By.Id("postcode"), regData.uk_postcode, "Postal not found");
                        
                     //   commonWebMethods.Click(driverObj, By.Id("other_addr_street_2"), "Address2 not found");

                        commonWebMethods.Type(driverObj, By.Id("address"), Registration_Data.uk_addr_street_2, "uk_addr_street_2 not found");
                        commonWebMethods.Type(driverObj, By.Id("city"), regData.city, "City not found");
                      
                        

                    }
              //  dob:

                    commonWebMethods.Type(driverObj, By.Id("day"), Registration_Data.dob_day, "day field not found");
                    commonWebMethods.Type(driverObj, By.Id("month"), Registration_Data.dob_month, "month field not found");
                    commonWebMethods.Type(driverObj, By.Id("year"), Registration_Data.dob_year, "year field not found");


              //  email:
                    commonWebMethods.Clear(driverObj, By.Id("email"), "email not found", 0, false);
                    commonWebMethods.Type(driverObj, By.Id("email"), regData.email, "email not found");

                    if (!FrameGlobals.UseAgent.ToUpper().Contains("YES"))
                    {
                        // tele:
                        commonWebMethods.Clear(driverObj, By.Id("telnumber"), "telephone not found", 0, false);
                        commonWebMethods.Type(driverObj, By.Id("telnumber"), regData.telephone, "telephone not found");
                    }
                    commonWebMethods.Clear(driverObj, By.Id("mobnumber"), "mobnumber not found", 0, false);
                    commonWebMethods.Type(driverObj, By.Id("mobnumber"), regData.mobile, "mobnumber not found");


               // uname:

                    dateFormat = DateTime.Now;
                    dateTime = (dateFormat.ToString("yyyyMMddHHmmss"));
                    uname = regData.username + StringCommonMethods.GenerateAlphabeticGUID();
         
         
                    commonWebMethods.Clear(driverObj, By.Id("username"), "username not found", 0, false);
                    commonWebMethods.Type(driverObj, By.Id("username"), uname, "username not found");


                //pass:
                    commonWebMethods.Clear(driverObj, By.Id("password"), "password not found", 0, false);
                    commonWebMethods.Type(driverObj, By.Id("password"), regData.password, "password not found");



                //pconfirm:

                    commonWebMethods.Clear(driverObj, By.Id("confirmpassword"), "confirmpassword not found", 0, false);
                    commonWebMethods.Type(driverObj, By.Id("confirmpassword"), regData.password, "confirmpassword not found");


               // security:
                    commonWebMethods.Clear(driverObj, By.Id("maidennameans"), "maidennameans not found", 0, false);
                    commonWebMethods.Type(driverObj, By.Id("maidennameans"), Registration_Data.response_1, "maidennameans not found");

             

                
             // checkbox:
                   
                        commonWebMethods.Click(driverObj, By.Id("tandc"), null, 0, false);

                        string address = commonWebMethods.GetAttribute(driverObj, By.Id("address"),"value","No address box found");
                        if (address == string.Empty)
                            System.Threading.Thread.Sleep(15000);

                   commonWebMethods.Click(driverObj, By.Id("createaccount"), "Submit button not found", 0, false);

            
                }
                catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }
                return uname;
            }

        }

   

     
                  
      
        /// <summary>
        /// Author:Naga
        /// Verify the customer details in IMS
        /// </summary>
        /// <param name="adminBrowser">IMS browser</param>
        /// <param name="regData">registration details</param>
        public void VerifyCustDetailinIMS(IWebDriver imsDriver, Registration_Data regData)
        {
            imsDriver.SwitchTo().DefaultContent();
          

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("firstname")).GetAttribute("value").Contains(regData.fname), "First Name did not match in IMS Admin. Expected:" + regData.fname + " Actual:" + imsDriver.FindElement(By.Id("firstname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("lastname")).GetAttribute("value").Contains(regData.lname), "Last Name did not match in IMS Admin. Expected:" + regData.lname + " Actual:" + imsDriver.FindElement(By.Id("lastname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("email")).GetAttribute("value").Contains(regData.email), "Email did not match in IMS Admin. Expected:" + regData.email + " Actual:" + imsDriver.FindElement(By.Id("email")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value").Contains("GB"), "Country did not match in IMS Admin. Expected:" + regData.country_code + " Actual:" + imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value"));

            if (!FrameGlobals.UseAgent.ToUpper().Contains("YES"))
            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("phone")).GetAttribute("value").Contains(regData.telephone), "Telephone number did not match in IMS Admin. Expected:" + regData.telephone + " Actual:" + imsDriver.FindElement(By.Id("phone")).GetAttribute("value"));
            //if (FrameGlobals.UseAgent.ToUpper().Contains("YES"))
            //    BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("city")).GetAttribute("value").Contains(regData.Mobcity), "City did not match in IMS Admin. Expected:" + regData.Mobcity + " Actual:" + imsDriver.FindElement(By.Id("city")).GetAttribute("value"));
            //else
            //BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("city")).GetAttribute("value").Contains(regData.city), "City did not match in IMS Admin. Expected:" + regData.city + " Actual:" + imsDriver.FindElement(By.Id("city")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("address")).GetAttribute("value").Contains(regData.uk_addr_street_1), "Address did not match in IMS Admin. Expected:" + regData.uk_addr_street_1 + " Actual:" + imsDriver.FindElement(By.Id("address")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("zip")).GetAttribute("value").Contains(regData.uk_postcode), "Zip Code did not match in IMS Admin. Expected:" + regData.uk_postcode + " Actual:" + imsDriver.FindElement(By.Id("zip")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
            System.Threading.Thread.Sleep(3000);

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom01")).GetAttribute("value") == (string.Empty), "Custom 1 not updated");

            //BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom02")).GetAttribute("value").Contains("N"), "custom02 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom09")).GetAttribute("value") == (string.Empty), "custom09 not updated as expected");

            //BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom11")).GetAttribute("value").Contains("en"), "custom11 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom12")).GetAttribute("value") == (string.Empty), "custom12 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom18")).GetAttribute("value")==(string.Empty), "custom17 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom19")).GetAttribute("value")==(string.Empty), "custom18 not updated as expected");


        }

        /// <summary>
        /// Author:Naga
        /// Verify the customer details in IMS
        /// </summary>
        /// <param name="adminBrowser">IMS browser</param>
        /// <param name="regData">registration details</param>
        public void VerifyCustDetailinIMS_newLook(IWebDriver imsDriver, Registration_Data regData)
        {
            imsDriver.SwitchTo().DefaultContent();



            BaseTest.AddTestCase("Verified customer details in IMS for below fields:" +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<u>Fields verified</u>" + "&emsp;&emsp;&emsp;<u>Values found</u>" +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;First Name - " + "&emsp;&emsp;&emsp;" + regData.fname +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Last Name - " + "&emsp;&emsp;&emsp;" + regData.lname +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Email - " + "&emsp;&emsp;&emsp;" + regData.email +
                  "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;AV status - " + "&emsp;&emsp;&emsp;" + "Unknwon" +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Country - " + "&emsp;&emsp;&emsp;" + regData.country_code +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Zip Code - " + "&emsp;&emsp;&emsp;" + regData.zip +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Mobile number - " + "&emsp;&emsp;&emsp;" + regData.mobile +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Telephone number - " + "&emsp;&emsp;&emsp;" + regData.telephone +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;City - &emsp;&emsp;&emsp;" + regData.city, "Customer details should be verified");
            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("firstname")).GetAttribute("value").Contains(regData.fname), "First Name did not match in IMS Admin. Expected:" + regData.fname + " Actual:" + imsDriver.FindElement(By.Id("firstname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("lastname")).GetAttribute("value").Contains(regData.lname), "Last Name did not match in IMS Admin. Expected:" + regData.lname + " Actual:" + imsDriver.FindElement(By.Id("lastname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("email")).GetAttribute("value").Contains(regData.email), "Email did not match in IMS Admin. Expected:" + regData.email + " Actual:" + imsDriver.FindElement(By.Id("email")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value").Contains("GB"), "Country did not match in IMS Admin. Expected:" + regData.country_code + " Actual:" + imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value"));

           
           string AvStatus = wAction.GetSelectedDropdownOptionText(imsDriver, By.Id("ageverification"),"Av status drop down not found");
           BaseTest.Assert.IsTrue(AvStatus.Contains("Unknown"), "AV status is not unknown in IMS Admin. Expected:" + regData.country_code + " Actual:" + AvStatus);

            
            if (!FrameGlobals.UseAgent.ToUpper().Contains("YES"))
                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("phone")).GetAttribute("value").Contains(regData.telephone), "Telephone number did not match in IMS Admin. Expected:" + regData.telephone + " Actual:" + imsDriver.FindElement(By.Id("phone")).GetAttribute("value"));

                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("city")).GetAttribute("value").Contains(regData.city), "City did not match in IMS Admin. Expected:" + regData.city + " Actual:" + imsDriver.FindElement(By.Id("city")).GetAttribute("value"));

                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("address")).GetAttribute("value").Contains(regData.uk_addr_street_1), "Address did not match in IMS Admin. Expected:" + regData.uk_addr_street_1 + " Actual:" + imsDriver.FindElement(By.Id("address")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("zip")).GetAttribute("value").Contains(regData.uk_postcode), "Zip Code did not match in IMS Admin. Expected:" + regData.uk_postcode + " Actual:" + imsDriver.FindElement(By.Id("zip")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            /*commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
            System.Threading.Thread.Sleep(3000);

            BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom01")).GetAttribute("value") == (string.Empty), "Custom 1 not updated");

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom02")).GetAttribute("value").Contains("N"), "custom02 not updated as expected");

            BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom09")).GetAttribute("value") == (string.Empty), "custom09 not updated as expected");

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom11")).GetAttribute("value").Contains("en"), "custom11 not updated as expected");

            BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom12")).GetAttribute("value") == (string.Empty), "custom12 not updated as expected");*/

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom18")).GetAttribute("value")==(string.Empty), "custom17 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom19")).GetAttribute("value")==(string.Empty), "custom18 not updated as expected");


        }

        public void VerifyCustDetailinIMS_Modified(IWebDriver imsDriver, Dictionary<string, string> modDetails)
        {
            imsDriver.SwitchTo().DefaultContent();



            BaseTest.AddTestCase("Verified customer details in IMS for below fields:" +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<u>Fields verified</u>" + "&emsp;&emsp;&emsp;<u>Values found</u>" +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Mobile number - " + "&emsp;&emsp;&emsp;" + modDetails["mobile"] +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Telephone number - " + "&emsp;&emsp;&emsp;" + modDetails["phone"] +
               "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Address - " + "&emsp;&emsp;&emsp;" + modDetails["address"] 
          , "Customer details should be verified");


            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("phone")).GetAttribute("value").Contains(modDetails["phone"]), "Telephone number did not match in IMS Admin. Expected:" + modDetails["phone"] + " Actual:" + imsDriver.FindElement(By.Id("phone")).GetAttribute("value"));
            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("address")).GetAttribute("value").Contains(modDetails["address"]), "Address did not match in IMS Admin. Expected:" + modDetails["address"] + " Actual:" + imsDriver.FindElement(By.Id("address")).GetAttribute("value"));
            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(modDetails["mobile"]), "Mobile # did not match in IMS Admin. Expected:" + modDetails["mobile"] + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));


        }

        public void VerifyCustDetailinIMS_newLook_Paypal(IWebDriver imsDriver, Registration_Data regData)
        {
            imsDriver.SwitchTo().DefaultContent();

            BaseTest.AddTestCase("Verified customer details in IMS for below fields:" +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<u>Fields verified</u>" + "&emsp;&emsp;&emsp;<u>Values found</u>" +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;First Name - " + "&emsp;&emsp;&emsp;" + regData.fname +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Last Name - " + "&emsp;&emsp;&emsp;" + regData.lname +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Email - " + "&emsp;&emsp;&emsp;" + regData.email +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Country - " + "&emsp;&emsp;&emsp;" + regData.country_code +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Zip Code - " + "&emsp;&emsp;&emsp;" + regData.zip +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Mobile number - " + "&emsp;&emsp;&emsp;" + regData.mobile +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Telephone number - " + "&emsp;&emsp;&emsp;" + regData.telephone +
            "<br>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;City - &emsp;&emsp;&emsp;" + regData.city, "Customer details should be verified");
            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("firstname")).GetAttribute("value").Contains(regData.fname), "First Name did not match in IMS Admin. Expected:" + regData.fname + " Actual:" + imsDriver.FindElement(By.Id("firstname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("lastname")).GetAttribute("value").Contains(regData.lname), "Last Name did not match in IMS Admin. Expected:" + regData.lname + " Actual:" + imsDriver.FindElement(By.Id("lastname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("email")).GetAttribute("value").Contains(regData.email), "Email did not match in IMS Admin. Expected:" + regData.email + " Actual:" + imsDriver.FindElement(By.Id("email")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value").Contains("GB"), "Country did not match in IMS Admin. Expected:" + regData.country_code + " Actual:" + imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value"));

            if (!FrameGlobals.UseAgent.ToUpper().Contains("YES"))
                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("phone")).GetAttribute("value").Contains(regData.telephone), "Telephone number did not match in IMS Admin. Expected:" + regData.telephone + " Actual:" + imsDriver.FindElement(By.Id("phone")).GetAttribute("value"));

          //  BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("city")).GetAttribute("value").Contains(regData.city), "City did not match in IMS Admin. Expected:" + regData.city + " Actual:" + imsDriver.FindElement(By.Id("city")).GetAttribute("value"));

          //  BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("address")).GetAttribute("value").Contains(regData.uk_addr_street_1), "Address did not match in IMS Admin. Expected:" + regData.uk_addr_street_1 + " Actual:" + imsDriver.FindElement(By.Id("address")).GetAttribute("value"));

           // BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("zip")).GetAttribute("value").Contains(regData.uk_postcode), "Zip Code did not match in IMS Admin. Expected:" + regData.uk_postcode + " Actual:" + imsDriver.FindElement(By.Id("zip")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

          //  BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

          

        }

        public void VerifyCustDetailsInOB(ISelenium adminBrowser, Registration_Data regData)
        {
            suiteAdmin.SelectMainFrame(adminBrowser);
            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.FName).Contains(regData.fname), "First Name did not match in OB, Expected:" + regData.fname + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.FName));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.lName).Contains(regData.lname), "Last Name did not match in OB, Expected:" + regData.lname + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.lName));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.address).Contains(regData.uk_addr_street_1), "Address did not match in OB, Expected:" + regData.uk_addr_street_1 + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.address));

               BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.city).Contains(regData.city), "City did not match in OB. Expected:" + regData.city + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.city));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.postal).Contains(regData.uk_postcode), "Postal did not match in OB. Expected:" + regData.uk_postcode + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.postal));

           
            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.country_ID).Contains(regData.country_code), "Country did not match in OB. Expected:" + regData.country_code + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.country_ID));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.email).Contains(regData.email), "Email ID did not match in OB. Expected:" + regData.email + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.email));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.DOB_ID).Contains(regData.DOB), "DOB did not match in OB. Expected:" + regData.DOB + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.DOB_ID));

            //string a = adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.URN);
            BaseTest.Assert.IsFalse(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.custID).Contains("[]") || adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.custID) == (string.Empty), "Cust ID is not updated");

            BaseTest.Assert.IsFalse(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.URN).Contains("[]") || adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.custID) == (string.Empty), "URN is not updated");

            //BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.title).Contains("mr"), "Title is not updated as expected");

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.URNMaster).Contains("Y"), "URN Master is not updated as expected");

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.mobile).Contains(regData.mobile), "Mobile# is not updated as expected" + regData.mobile + " Actual: " + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.mobile));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.languageCD).Contains("en"), "Language Cd is not updated as expected, Actual: " + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.languageCD));

            // BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.IMS_Control_PlayerDetailss.sms).Contains("on"),"SMS is not selected as contact in OB");
            // BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.IMS_Control_PlayerDetailss.email).Contains("on"),"Email is not selected as contact in OB");


        }

        public void VerifyCustDetailsInOB_Modified(ISelenium adminBrowser, Dictionary<string, string> modDetails)
        {
            suiteAdmin.SelectMainFrame(adminBrowser);

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.address).Contains(modDetails["address"]), "Address did not match in OB, Expected:" + modDetails["address"] + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.address));
            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.mobile).Contains(modDetails["mobile"]), "Mobile# is not updated as expected" + modDetails["mobile"] + " Actual: " + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.mobile));

        }

        public void VerifyCustDetailsInOB_Paypal(ISelenium adminBrowser, Registration_Data regData)
        {
            suiteAdmin.SelectMainFrame(adminBrowser);
            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.FName).Contains(regData.fname), "First Name did not match in OB, Expected:" + regData.fname + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.FName));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.lName).Contains(regData.lname), "Last Name did not match in OB, Expected:" + regData.lname + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.lName));

            // BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.address).Contains(regData.uk_addr_street_1), "Address did not match in OB, Expected:" + regData.uk_addr_street_1 + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.address));

            //  BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.city).Contains(regData.city), "City did not match in OB. Expected:" + regData.city + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.city));

            //BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.postal).Contains(regData.uk_postcode), "Postal did not match in OB. Expected:" + regData.uk_postcode + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.postal));


            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.country_ID).Contains(regData.country_code), "Country did not match in OB. Expected:" + regData.country_code + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.country_ID));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.email).Contains(regData.email), "Email ID did not match in OB. Expected:" + regData.email + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.email));

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.DOB_ID).Contains(regData.DOB), "DOB did not match in OB. Expected:" + regData.DOB + " Actual:" + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.DOB_ID));

            //string a = adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.URN);
            BaseTest.Assert.IsFalse(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.custID).Contains("[]") || adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.custID) == (string.Empty), "Cust ID is not updated");

            BaseTest.Assert.IsFalse(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.URN).Contains("[]") || adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.custID) == (string.Empty), "URN is not updated");

            //BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.title).Contains("mr"), "Title is not updated as expected");

            //BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.URNMaster).Contains("N"), "URN Master is not updated as expected");

            BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.mobile).Contains(regData.mobile), "Mobile# is not updated as expected" + regData.mobile + " Actual: " + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.mobile));

            //BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.languageCD).Contains("en"), "Language Cd is not updated as expected, Actual: " + adminBrowser.GetText(AdminSuite.CommonControls.OB_Controls.languageCD));

            // BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.IMS_Control_PlayerDetailss.sms).Contains("on"),"SMS is not selected as contact in OB");
            // BaseTest.Assert.IsTrue(adminBrowser.GetText(AdminSuite.CommonControls.IMS_Control_PlayerDetailss.email).Contains("on"),"Email is not selected as contact in OB");


        }
        /// <summary>
        /// Author:Naga
        /// Verify the customer details in IMS
        /// </summary>
        /// <param name="adminBrowser">IMS browser</param>
        /// <param name="regData">registration details</param>
        public void VerifyAffiliateDetailinIMS(IWebDriver imsDriver, Registration_Data regData)
        {
            imsDriver.SwitchTo().DefaultContent();
            //DateTime varDateTime = DateTime.Now;
            //DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            //do
            //{
            //    varDateTime = DateTime.Now;
            //    try
            //    {
            //        imsDriver.SwitchTo().Frame("main");
            //        break;
            //    }
            //    catch (Exception) { }
            //} while (varDateTime <= varElapseTime);

            //commonWebMethods.WaitAndMovetoFrame(imsDriver, "main", "Mainframe not loaded in IMS", FrameGlobals.reloadTimeOut);

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("firstname")).GetAttribute("value").Contains(regData.fname), "First Name did not match in IMS Admin. Expected:" + regData.fname + " Actual:" + imsDriver.FindElement(By.Id("firstname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("lastname")).GetAttribute("value").Contains(regData.lname), "Last Name did not match in IMS Admin. Expected:" + regData.lname + " Actual:" + imsDriver.FindElement(By.Id("lastname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("email")).GetAttribute("value").Contains(regData.email), "Email did not match in IMS Admin. Expected:" + regData.email + " Actual:" + imsDriver.FindElement(By.Id("email")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value").Contains("GB"), "Country did not match in IMS Admin. Expected:" + regData.country_code + " Actual:" + imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value"));

            if (!FrameGlobals.UseAgent.ToUpper().Contains("YES"))
                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("phone")).GetAttribute("value").Contains(regData.telephone), "Telephone number did not match in IMS Admin. Expected:" + regData.telephone + " Actual:" + imsDriver.FindElement(By.Id("phone")).GetAttribute("value"));

            //if (FrameGlobals.UseAgent.ToUpper().Contains("YES"))
            //    BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("city")).GetAttribute("value").Contains(regData.Mobcity), "City did not match in IMS Admin. Expected:" + regData.Mobcity + " Actual:" + imsDriver.FindElement(By.Id("city")).GetAttribute("value"));
            //else 
            //BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("city")).GetAttribute("value").Contains(regData.city), "City did not match in IMS Admin. Expected:" + regData.city + " Actual:" + imsDriver.FindElement(By.Id("city")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("address")).GetAttribute("value").Contains(regData.uk_addr_street_1), "Address did not match in IMS Admin. Expected:" + regData.uk_addr_street_1 + " Actual:" + imsDriver.FindElement(By.Id("address")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("zip")).GetAttribute("value").Contains(regData.uk_postcode), "Zip Code did not match in IMS Admin. Expected:" + regData.uk_postcode + " Actual:" + imsDriver.FindElement(By.Id("zip")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
            System.Threading.Thread.Sleep(3000);



            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.XPath(" //tr[td[text()='ext_aff_tag']]/td[2]")).GetAttribute("value") != (string.Empty), "Affiliate data not updated");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom01")).GetAttribute("value") == (string.Empty), "Custom 1 not updated");

            //BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom02")).GetAttribute("value").Contains("N"), "custom02 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom09")).GetAttribute("value") == (string.Empty), "custom09 not updated as expected");

            //BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom11")).GetAttribute("value").Contains("en"), "custom11 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom12")).GetAttribute("value") == (string.Empty), "custom12 not updated as expected");

            //BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom05")).GetAttribute("value").Contains(Registration_Data.affliateID), "custom12 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom19")).GetAttribute("value")==(string.Empty), "custom19 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom18")).GetAttribute("value")==(string.Empty), "custom18 not updated as expected");


        }

        /// <summary>
        /// Author:Naga
        /// Verify the customer details in IMS
        /// </summary>
        /// <param name="adminBrowser">IMS browser</param>
        /// <param name="regData">registration details</param>
        public void VerifyAffiliateDetailinIMS_NewLook(IWebDriver imsDriver, Registration_Data regData)
        {

            imsDriver.SwitchTo().DefaultContent();

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("firstname")).GetAttribute("value").Contains(regData.fname), "First Name did not match in IMS Admin. Expected:" + regData.fname + " Actual:" + imsDriver.FindElement(By.Id("firstname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("lastname")).GetAttribute("value").Contains(regData.lname), "Last Name did not match in IMS Admin. Expected:" + regData.lname + " Actual:" + imsDriver.FindElement(By.Id("lastname")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("email")).GetAttribute("value").Contains(regData.email), "Email did not match in IMS Admin. Expected:" + regData.email + " Actual:" + imsDriver.FindElement(By.Id("email")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value").Contains("GB"), "Country did not match in IMS Admin. Expected:" + regData.country_code + " Actual:" + imsDriver.FindElement(By.Id("countrycode")).GetAttribute("value"));

            if (!FrameGlobals.UseAgent.ToUpper().Contains("YES"))
                BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("phone")).GetAttribute("value").Contains(regData.telephone), "Telephone number did not match in IMS Admin. Expected:" + regData.telephone + " Actual:" + imsDriver.FindElement(By.Id("phone")).GetAttribute("value"));

            //if (FrameGlobals.UseAgent.ToUpper().Contains("YES"))
            //    BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("city")).GetAttribute("value").Contains(regData.Mobcity), "City did not match in IMS Admin. Expected:" + regData.Mobcity + " Actual:" + imsDriver.FindElement(By.Id("city")).GetAttribute("value"));
            //else
            //    BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("city")).GetAttribute("value").Contains(regData.city), "City did not match in IMS Admin. Expected:" + regData.city + " Actual:" + imsDriver.FindElement(By.Id("city")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("address")).GetAttribute("value").Contains(regData.uk_addr_street_1), "Address did not match in IMS Admin. Expected:" + regData.uk_addr_street_1 + " Actual:" + imsDriver.FindElement(By.Id("address")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("zip")).GetAttribute("value").Contains(regData.uk_postcode), "Zip Code did not match in IMS Admin. Expected:" + regData.uk_postcode + " Actual:" + imsDriver.FindElement(By.Id("zip")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value").Contains(regData.mobile), "Mobile # did not match in IMS Admin. Expected:" + regData.mobile + " Actual:" + imsDriver.FindElement(By.Id("cellphone")).GetAttribute("value"));

            commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
            System.Threading.Thread.Sleep(3000);

            BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom01")).GetAttribute("value") == (string.Empty), "Custom 1 not updated");

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom02")).GetAttribute("value").Contains("N"), "custom02 not updated as expected");

            BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom09")).GetAttribute("value") == (string.Empty), "custom09 not updated as expected");

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom11")).GetAttribute("value").Contains("en"), "custom11 not updated as expected");

            BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom12")).GetAttribute("value") == (string.Empty), "custom12 not updated as expected");

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom05")).GetAttribute("value").Contains(Registration_Data.affliateID), "custom12 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom19")).GetAttribute("value")==(string.Empty), "custom19 not updated as expected");

            //BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom18")).GetAttribute("value")==(string.Empty), "custom18 not updated as expected");


        }

        //public void VerifyCustSourceDetailinIMS_newLook(IWebDriver imsDriver)
        //{
        //    imsDriver.SwitchTo().DefaultContent();
        //    commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
        //    System.Threading.Thread.Sleep(3000);

        //    BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom18")).GetAttribute("value").Contains("GAM_WEB"), "custom18 not updated as expected");

        //    BaseTest.Assert.IsFalse(imsDriver.FindElement(By.Id("custom17")).GetAttribute("value").Contains("GAM"), "custom17 not updated as expected");

        //    //Sign up Client Type - Games

        //}
        public void VerifyCustSourceDetailinIMS_newLook(IWebDriver imsDriver)
        {
            imsDriver.SwitchTo().DefaultContent();
            commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
            System.Threading.Thread.Sleep(3000);
            string clientTypePath = "//td[contains(string(),'Sign up client type')]/following-sibling::td[1]";
            string clientPlatfrom = "//td[contains(string(),'clientplatform')]/following-sibling::td[1]";
            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.XPath(clientTypePath)).Text.Contains("games"), "client type is not updated as expected");
            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.XPath(clientPlatfrom)).Text.Contains("web"), "client platfrom is not updated as expected");

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom18")).GetAttribute("value").Contains("GAM_WEB"), "custom18 not updated as expected");

            BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom17")).GetAttribute("value").Contains("GAM"), "custom17 not updated as expected");

            //Sign up Client Type - Games

        }

        #region ToBeDeleted
        ///// <summary>
        ///// Author:Naga
        ///// Validate the deposit 
        ///// </summary>
        ///// <param name="driverObj">browser</param>
        ///// <param name="toWallet">to wallet name</param>
        ///// <param name="beforeValToWallet"> before value of to wallet</param>
        ///// <param name="transValue">transfer values</param>
        //public void Validate_Deposit_wallet(IWebDriver driverObj, string toWallet, double beforeValToWallet, double transValue)
        //{
        //    #region deleted
        //    //string img1, img2, img;
        //    //img1 = "//tr[td[img[@src='/images/sb3_portal/en/banking/small_dep_arrow_green.gif']]]/td[@class='tdbal']";
        //    //img2 = "//tr[td[img[@src='/images/sb3_portal/en/banking/small_dep_arrow_white.gif']]]/td[@class='tdbal']";
        //    //if (commonWebMethods.IsElementPresent(driverObj, By.XPath(img1)))
        //    //{
        //    //    img = img1;
        //    //}
        //    //else
        //    //{
        //    //    img = img2;
        //    //}
        //    #endregion
        //    string img = "//tr[td[contains(string(),'" + toWallet + "')]]/td[@class='tdbal']";
        //    string img1 = "//tr[td[contains(string(),'" + toWallet + "')]]/td[@class='tdbal']";
        //    string img2 = "//tr[td[contains(string(),'" + toWallet + "')]]/td[@class='tdbal_bold']";

        //    DateTime varDateTime, varElapseTime;

        //    varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
        //    do
        //    {
        //        varDateTime = DateTime.Now;
        //        try
        //        {
        //            try
        //            {

        //                if (driverObj.FindElement(By.XPath(img1)).Text.ToString().Contains("Ret"))
        //                    continue;
        //                else
        //                {
        //                    if (toWallet == "Casino")
        //                        double.Parse(driverObj.FindElement(By.XPath(img1)).Text.ToString().Trim());
        //                    else
        //                        double.Parse(driverObj.FindElement(By.XPath(img1)).Text.ToString().Replace('£', ' ').Trim());

        //                    img = img1;
        //                    break;

        //                }
        //            }
        //            catch (Exception)
        //            {
        //                if (commonWebMethods.IsElementPresent(driverObj, By.XPath(img2)))
        //                {
        //                    img = img2;
        //                    break;
        //                }
        //            }
        //        }
        //        catch (Exception) { }
        //    } while (varDateTime <= varElapseTime);


        //    if (driverObj.FindElement(By.XPath(img)).Text.ToString().Contains("Ret") || driverObj.FindElement(By.XPath(img)).Text.ToString().Contains("N/A") || driverObj.FindElement(By.XPath(img)).Text.ToString().Contains("Unavailable"))
        //        BaseTest.Skip("Unable to retrive any value from the wallets even after the specified time");

        //    if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']")))
        //        commonWebMethods.Click(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']"));


        //    if (toWallet == "Casino")
        //        BaseTest.Assert.IsTrue(beforeValToWallet + transValue == double.Parse(driverObj.FindElement(By.XPath(img)).Text.ToString().Trim()), "Transfer Failed - Amount not added to " + toWallet + " Wallet");
        //    else
        //        BaseTest.Assert.IsTrue(beforeValToWallet + transValue == double.Parse(driverObj.FindElement(By.XPath(img)).Text.ToString().Replace('£', ' ').Trim()), "Transfer Failed - Amount not added to " + toWallet + " Wallet");

        //}

        ///// <summary>
        ///// Author:Nagamanickam
        ///// Validate withdraw from a wallet
        ///// </summary>
        ///// <param name="driverObj">browser</param>
        ///// <param name="fromWallet">from a wallet</param>
        ///// <param name="beforeValFromWallet">before value of a from wallet</param>
        ///// <param name="transValue">transfer value</param>
        //public void Validate_Withdraw_wallet(IWebDriver driverObj, string fromWallet, double beforeValFromWallet, double transValue)
        //{
        //    #region deleted
        //    //string img1 = "//tr[td[img[@src='/images/sb3_portal/en/banking/small_wtd_arrow_green.gif']]]/td[@class='tdbal']";
        //    //string img2 = "//tr[td[img[@src='/images/sb3_portal/en/banking/small_wtd_arrow_white.gif']]]/td[@class='tdbal']";
        //    //string img;
        //    //if (commonWebMethods.IsElementPresent(driverObj, By.XPath(img1)))
        //    //{
        //    //    img = img1;
        //    //}
        //    //else
        //    //{
        //    //    img = img2;
        //    //}
        //    #endregion
        //    string img;
        //    string img1 = "//tr[td[contains(string(),'" + fromWallet + "')]]/td[@class='tdbal']";
        //    string img2 = "//tr[td[contains(string(),'" + fromWallet + "')]]/td[@class='tdbal_bold']";

        //    img = img1;

        //    DateTime varDateTime, varElapseTime;


        //    varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
        //    do
        //    {
        //        varDateTime = DateTime.Now;
        //        try
        //        {
        //            try
        //             {

        //            if (driverObj.FindElement(By.XPath(img1)).Text.ToString().Contains("Ret"))
        //                continue;
        //            else
        //            {
        //                   if (fromWallet == "Casino")
        //                        double.Parse(driverObj.FindElement(By.XPath(img1)).Text.ToString().Trim());
        //                    else
        //                        double.Parse(driverObj.FindElement(By.XPath(img1)).Text.ToString().Replace('£', ' ').Trim());
                           
        //                    img = img1;
        //                    break;
                        
        //             }
        //            }
        //            catch (Exception) {
        //                if (commonWebMethods.IsElementPresent(driverObj, By.XPath(img2)))
        //                {
        //                    img = img2;
        //                    break;
        //                }
        //            }
        //        }
        //        catch (Exception) { }
        //    } while (varDateTime <= varElapseTime);

        //    if (driverObj.FindElement(By.XPath(img)).Text.ToString().Contains("Ret") || driverObj.FindElement(By.XPath(img)).Text.ToString().Contains("N/A") || driverObj.FindElement(By.XPath(img)).Text.ToString().Contains("Unavailable"))
        //        BaseTest.Skip("Unable to retrive any value from the wallets even after the specified time");

        //    if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']")))
        //        commonWebMethods.Click(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']"));

        //    if (fromWallet == "Casino")
        //        BaseTest.Assert.IsTrue(beforeValFromWallet - transValue == double.Parse(driverObj.FindElement(By.XPath(img)).Text.ToString().Trim()), "Transfer Failed - Amount not deducted from " + fromWallet + " Wallet");
        //    else
        //        BaseTest.Assert.IsTrue(beforeValFromWallet - transValue == double.Parse(driverObj.FindElement(By.XPath(img)).Text.ToString().Replace('£', ' ').Trim()), "Transfer Failed - Amount not deducted from " + fromWallet + " Wallet");



        //}
        #endregion


        /// <summary>
          /// Author: Roopa
        /// Common method for verifying deposit and withdraw limit
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void EnterDepositAmount(IWebDriver driverObj, string secNumber)
        {

            double beforeVal = 0;
            //BaseTest.Assert.IsTrue(commonWebMethods.WaitAndMovetoFrame(driverObj, "acctmidnav"),"My Acct Window not loaded due to server error");
            bool flag = false;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            commonWebMethods.Click(driverObj, By.XPath(Login_Control.userDisplay_Xpath), "Logout Drop down not found", FrameGlobals.elementTimeOut, false);
            commonWebMethods.Click(driverObj, By.LinkText("Deposit"), "Deposit link not found", 0, false);
            string newWindow = driverObj.WindowHandles.ToArray()[1].ToString();
            driverObj.SwitchTo().Window(newWindow);
            commonWebMethods.WaitforPageLoad(driverObj);
            Thread.Sleep(5000);
            int i = 3;
        tryAgain:
            i--;
            wAction.Click(driverObj, By.XPath("id('NETellerPm')/p/span"), "Netteller payment metod not found", 0, false);
            //driverObj.FindElement(By.XPath("//span[span[@name='genericDeposit_limit']]"));
            if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt"))
            {
                if (i != 0)
                    goto tryAgain;
            }

            string[] temp = driverObj.FindElement(By.XPath("//span[span[@name='genericDeposit_limit']]")).Text.Split(' ');
            string value = temp[temp.Length - 1];
            double depositAmount = 0;
            double.TryParse(value.Replace("£",""), out depositAmount);
            depositAmount = depositAmount + 10;
            Thread.Sleep(2000);
            string depositAmountTextBox_Id = "textAmount";
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", secNumber, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);

            driverObj.FindElement(By.Id(depositAmountTextBox_Id)).Clear();
            commonWebMethods.Type(driverObj, By.Id(depositAmountTextBox_Id), depositAmount.ToString() + Keys.Return, "Deposit Amount text box not found, {Error in:depositAmountTextBox_Id}");
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");


            string errorMessageID = "//span[@class='error' and @for='textAmount']";
            Thread.Sleep(2000);
            if (!commonWebMethods.IsElementPresent(driverObj, By.XPath(errorMessageID)))
            {
                BaseTest.Fail("Error message is not displayed on ecxeeding deposit limit ");
            }
            else
            {
                //The amount exceeds allowed limits
                string msg = commonWebMethods.GetText(driverObj, By.XPath(errorMessageID), "Max Limit Error message not found", false);
                if (!(msg.Contains("exceed")))
                {
                    BaseTest.Fail("Max limit error message not found, Actual: " + commonWebMethods.GetText(driverObj, By.Id("acct_error"), "Max Limit Error message not found", false));
                }
            }
            string withdrawAmountTextBox_Id = "amount";
            ////string withDrawTab_Id = "//li//span[contains(string(),'Withdraw') and not(contains(string(),'Cancel'))]";
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
            commonWebMethods.WaitforPageLoad(driverObj);

            temp = driverObj.FindElement(By.XPath("//span[span[@name='genericWithdraw_limit']]")).Text.Split(' ');
            value = temp[temp.Length - 1];
            double.TryParse(value.Replace("£", ""), out depositAmount);
            depositAmount = depositAmount + 10;
            wAction.Click(driverObj, By.Id(withdrawAmountTextBox_Id), "Amount text box bnot found", 0, false);

            commonWebMethods.Type(driverObj, By.Id(withdrawAmountTextBox_Id), depositAmount.ToString() + Keys.Return, "Deposit Amount text box not found, {Error in:depositAmountTextBox_Id}");
            errorMessageID = "//span[@class='error' and @for='amount']";
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "Withdraw_btn not found");

            Thread.Sleep(2000);
            if (!commonWebMethods.IsElementPresent(driverObj, By.XPath(errorMessageID)))
            {
                Framework.BaseTest.Assert.Fail("Error message is  displayed before entering the amount");
            }
            else
            {
                //The amount exceeds allowed limits
                string msg = commonWebMethods.GetText(driverObj, By.XPath(errorMessageID), "Max Limit Error message not found", false);
                if (!(msg.Contains("exceed")))
                {
                    BaseTest.Fail("Max limit error message not found, Actual: " + commonWebMethods.GetText(driverObj, By.Id("acct_error"), "Max Limit Error message not found", false));
                }
            }

        }


        /// <summary>
        /// Author: Roopa
        /// Common method for verifying  withdraw limit
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void EnterAmount_Withdraw(IWebDriver driverObj)
        {
            BaseTest.AddTestCase("Verify that the amount cannot be Withdrawn if greater than the limit set", "Withdraw should not be successfull");
            double beforeVal = 0;
            //BaseTest.Assert.IsTrue(commonWebMethods.WaitAndMovetoFrame(driverObj, "acctmidnav"),"My Acct Window not loaded due to server error");
            bool flag = false;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            commonWebMethods.WaitforPageLoad(driverObj);
             double depositAmount = 0;
            string withdrawAmountTextBox_Id = "amount";
            //string withDrawTab_Id = "//li//span[contains(string(),'Withdraw') and not(contains(string(),'Cancel'))]";
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
            commonWebMethods.WaitforPageLoad(driverObj);

           string[] temp = driverObj.FindElement(By.XPath("//span[span[@name='genericWithdraw_limit']]")).Text.Split(' ');
           string value = temp[temp.Length - 1];
            if(value.Contains("£"))
                double.TryParse(value.Replace("£",""), out depositAmount);
            else
            double.TryParse(value, out depositAmount);
            depositAmount = depositAmount + 10;

                driverObj.FindElement(By.Id(withdrawAmountTextBox_Id)).Clear();
            commonWebMethods.Type(driverObj, By.Id(withdrawAmountTextBox_Id), depositAmount.ToString() + Keys.Return, "Deposit Amount text box not found, {Error in:depositAmountTextBox_Id}");
            string errorMessageID = "//span[@class='error' and @for='amount']";

            if (!commonWebMethods.IsElementPresent(driverObj, By.XPath(errorMessageID)))
            {
                driverObj.FindElement(By.Id(withdrawAmountTextBox_Id)).SendKeys(Keys.Return); 
            }
            

            if (!commonWebMethods.IsElementPresent(driverObj, By.XPath(errorMessageID)))
            {
                Framework.BaseTest.Assert.Fail("Error message is not displayed on ecxeeding deposit limit");
            }
            BaseTest.Pass();

        }

        /// <summary>
        /// Author: Roopa
        /// Common method for verifying deposit and withdraw limit
        /// </summary>
        /// <param name="driverObj">broswer</param>
        public void EnterAmount_ToWithdrawMoreThanBalance(IWebDriver driverObj,string wallet)
        {

            OpenCashier(driverObj);
            CommonWithdrawMoreThanBalance(driverObj, wallet);
        }
        public void CommonWithdrawMoreThanBalance(IWebDriver driverObj,string wallet,bool inWithdrawPage=false,bool WinSwitch=false)
        {

            string portal=null;
            if (WinSwitch)
            {
                 portal = driverObj.WindowHandles.ToArray()[0].ToString();
                driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
            }
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
         
            
            if(!inWithdrawPage)
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
            commonWebMethods.WaitforPageLoad(driverObj);
       
            double depositAmount = 0; string withdrawAmountTextBox_Id = "amount";
            string depositWalletPath = "//tr[td[contains(text(),'" + wallet + "')]]/td[2]";
            string balAmount = wAction.GetText(driverObj, By.XPath(depositWalletPath), wallet + "Wallet value not found", false).ToString().Trim();

            if (balAmount.Contains("£"))
                double.TryParse(balAmount.Replace("£", ""), out depositAmount);
            else if (balAmount.Contains("$"))
                double.TryParse(balAmount.Replace("$", ""), out depositAmount);
            else
                double.TryParse(balAmount, out depositAmount);

            depositAmount = depositAmount + 10;
            wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferFrom_cmb", wallet, "destinationWallet_cmb not found");
            //wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "destinationWallet_cmb", wallet, "destinationWallet_cmb not found");
            //commonWebMethods.Clear(driverObj, By.Id(withdrawAmountTextBox_Id),  "Deposit Amount text box not found, {Error in:depositAmountTextBox_Id}");
            //commonWebMethods.Type(driverObj, By.Id(withdrawAmountTextBox_Id), depositAmount.ToString(), "Deposit Amount text box not found, {Error in:depositAmountTextBox_Id}");
            wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
            wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", depositAmount.ToString(), "Amount_txt not found");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");
                    
            if (!commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath(CashierPage.WithdrawMore_Error_XP)))
            {
                BaseTest.Assert.Fail("Error message is not displayed on exceeding deposit limit");

            }

            wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Confirmation_Dlg", "Confirmation_Dlg button not found");

            if (WinSwitch)
            {
                driverObj.SwitchTo().Window(portal);
            }
        }



        /// <summary>
        /// Common method for deposit 
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">My acct details</param>
        /// <param name="amount">amount to deposit</param>
        /// <returns>returns before value</returns>
        public double CommonDeposit(IWebDriver driverObj, MyAcct_Data acctData, string amount)
        {
            driverObj.SwitchTo().DefaultContent();
            double beforeVal = 0;
            //BaseTest.Assert.IsTrue(commonWebMethods.WaitAndMovetoFrame(driverObj, "acctmidnav"),"My Acct Window not loaded due to server error");
            bool flag = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("acctmidnav"), "acctmidnav Frame not found", FrameGlobals.elementTimeOut);
         

            driverObj.FindElement(By.LinkText("Deposit")).Click();
            driverObj.SwitchTo().DefaultContent();
            commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("main"), "Frame not found", FrameGlobals.elementTimeOut);
         

            string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[@class='tdbal']";
            //commonWebMethods.WaitUntilElementPresent(driverObj, By.LinkText("Deposit"));
            System.Threading.Thread.Sleep(5000);
            varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                   // beforeVal = double.Parse(driverObj.FindElement(By.XPath(depositWalletPath)).Text.ToString().Replace('£', ' ').Trim());
                    string temp = driverObj.FindElement(By.XPath(depositWalletPath)).Text;
                    if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                        beforeVal = StringCommonMethods.ReadDoublefromString(temp);
                    else
                        BaseTest.Fail("Wallet value is null/Blank");

                    break;
                }
                catch (Exception) { }
            } while (varDateTime <= varElapseTime);

            if (commonWebMethods.IsElementPresent(driverObj, By.XPath(MyAcct_Control.selectDepositType_Xpath)))
                commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.selectDepositType_Xpath), acctData.paymentType, acctData.paymentType + " not present in the deposit type selection");

              commonWebMethods.Type(driverObj, By.Id(MyAcct_Control.WalletType_ID), acctData.depositWallet, acctData.depositWallet + " not present in the deposit wallet");


            System.Threading.Thread.Sleep(3000);

            if (commonWebMethods.IsElementPresent(driverObj, By.Name(MyAcct_Control.cardtext_Name)))
            {
                commonWebMethods.Type(driverObj, By.Name(MyAcct_Control.cardtext_Name), acctData.card, "Card box not found, {Error in:MyAcct_Control.cardtext_Name}");
                commonWebMethods.Type(driverObj, By.Name(MyAcct_Control.StartYear_Name), MyAcct_Data.startYear, "start year not found, {Error in:MyAcct_Control.StartYear_Name}");
                commonWebMethods.Type(driverObj, By.Name(MyAcct_Control.ExpiryYear_Name), MyAcct_Data.endYear, "End year not found {Error in:MyAcct_Control.ExpiryYear_Name}");
                

            }
            if (commonWebMethods.IsElementPresent(driverObj, By.XPath(MyAcct_Control.hldr_Name)))
                commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.hldr_Name), "Gary", "Card Holder name input not found, {Error in:MyAcct_Control.hldr_Name}");
            commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.cardCSC_Xpath), acctData.cardCSC, "CSC card box not found, {Error in:MyAcct_Control.cardCSC_Xpath}");
            commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.amount_Xpath), amount, "Amount text not found, {Error in:MyAcct_Control.amount_Xpath}");
            //commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.depositPassword_Xpath), regData.password, "depositPassword box not found, {Error in:MyAcct_Control.depositPassword_Xpath}");

            int i = 2;
            do
            {
                try
                {
                    if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                        driverObj.FindElement(By.XPath(MyAcct_Control.depositButton_Xpath)).SendKeys(Keys.Enter);
                    else
                        driverObj.FindElement(By.XPath(MyAcct_Control.depositButton_Xpath)).Click();
                    break;

                }
                catch (Exception)
                {
                    if (i == 0)
                        BaseTest.Fail("deposit button not found");

                    if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']")))
                        commonWebMethods.Click(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']"));

                    break;
                }

            } while (i-- != 0);


            commonWebMethods.WaitforPageLoad(driverObj);
            if (commonWebMethods.IsElementPresent(driverObj, By.XPath(MyAcct_Control.depsoitTransactionHeading_Xpath)))
            {
                driverObj.FindElement(By.LinkText("Continue")).Click();

                commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath(MyAcct_Control.depositAuthHeading_Xpath));

                driverObj.FindElement(By.XPath(MyAcct_Control.depsoitAuthenticateBTN_Xpath)).Click();
                System.Threading.Thread.Sleep(8000);
                commonWebMethods.WaitforPageLoad(driverObj);
            }
            return beforeVal;
        }

        /// <summary>
        /// Common method for withdraw 
        /// </summary>
        /// <param name="driverObj">broswer</param>
        /// <param name="acctData">My acct details</param>
        /// <param name="amount">amount to withdraw</param>
        /// <returns>returns before value</returns>       
        public double CommonWithdraw(IWebDriver driverObj, MyAcct_Data acctData, string amount = null)
        {

           
            driverObj.SwitchTo().DefaultContent();
            double beforeVal = 0;
            bool flag = false;
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            

            commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("acctmidnav"), "acctmidnav Frame not found", FrameGlobals.elementTimeOut);
            #region tobeDeleted
            //do
            //{
            //    varDateTime = DateTime.Now;
            //    try
            //    {
            //        driverObj.SwitchTo().Frame("acctmidnav");
            //        flag = true;
            //        break;
            //    }
            //    catch (Exception) { }
            //} while (varDateTime <= varElapseTime);
            //BaseTest.Assert.IsTrue(flag, "My acct page not loaded");
            #endregion

            driverObj.FindElement(By.LinkText("Withdraw")).Click();
            driverObj.SwitchTo().DefaultContent();

            commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("main"), "Frame not found", FrameGlobals.elementTimeOut);
            #region toBeDeleted
            //varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            //flag = false;
            //do
            //{
            //    varDateTime = DateTime.Now;
            //    try
            //    {
            //        driverObj.SwitchTo().Frame("main");
            //        flag = true;
            //        break;
            //    }
            //    catch (Exception) { }
            //} while (varDateTime <= varElapseTime);
            //BaseTest.Assert.IsTrue(flag, "My Withdraw page not loaded");
            #endregion

            commonWebMethods.WaitUntilElementPresent(driverObj, By.LinkText("Withdraw"));
            System.Threading.Thread.Sleep(5000);
            string tdClass;
            if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[@class='tdbal']")))
                tdClass = "tdbal";
            else
                tdClass = "tdbal_bold";
            if (commonWebMethods.GetText(driverObj, By.XPath("//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[@class='" + tdClass + "']"), acctData.withdrawWallet + "not found in wallet").Contains("Unavailable"))
                BaseTest.Fail("Withdraw wallet is disabled \"Unavailable\" shown");
            else if (commonWebMethods.GetText(driverObj, By.XPath("//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[@class='" + tdClass + "']"), acctData.withdrawWallet + "not found in wallet").Contains("N/A"))
                BaseTest.Fail("Withdraw wallet is disabled (N/A) shown");
            else if (commonWebMethods.GetText(driverObj, By.XPath("//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[@class='" + tdClass + "']"), acctData.withdrawWallet + "not found in wallet").Contains("Ret"))
            {
                int i = 2;
                do
                {
                    if (driverObj.FindElement(By.XPath("//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[@class='" + tdClass + "']")).Text.ToString().Contains("Ret"))
                        System.Threading.Thread.Sleep(5000);
                } while (i-- != 0);

            }
            varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    string temp = driverObj.FindElement(By.XPath("//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[@class='" + tdClass + "']")).Text;
                    if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                        beforeVal = StringCommonMethods.ReadDoublefromString(temp);
                    else
                        BaseTest.Fail("Wallet value is null/Blank");
                    
                    break;
                }
                catch (Exception) { }
            } while (varDateTime <= varElapseTime);



            if (amount == null)
                amount = (beforeVal + 5).ToString();
            commonWebMethods.Type(driverObj, By.Id(MyAcct_Control.WalletType_ID), acctData.withdrawWallet, acctData.withdrawWallet + " not present in the withdrawal wallet");
            driverObj.FindElement(By.XPath(MyAcct_Control.cardCSC_Xpath)).SendKeys(acctData.cardCSC);
            driverObj.FindElement(By.XPath(MyAcct_Control.amount_Xpath)).SendKeys(amount);
         //   driverObj.FindElement(By.XPath(MyAcct_Control.depositPassword_Xpath)).SendKeys(acctData.password);

            int ind = 2;
            do
            {
                try
                {
                    if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                        driverObj.FindElement(By.XPath(MyAcct_Control.withdrawButton_Xpath)).SendKeys(Keys.Enter);
                    else
                        driverObj.FindElement(By.XPath(MyAcct_Control.withdrawButton_Xpath)).Click();
                    break;
                }
                catch (Exception)
                {
                    if (ind == 0)
                        BaseTest.Fail("Withdraw button not found");
                    if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']")))
                        commonWebMethods.Click(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']"));
                }

            } while (ind-- != 0);


            return beforeVal;

        }


         public string HomePage_Balance(IWebDriver driver) 
         {
             wAction.PageReload(driver);
        String Balance = null;
             // commonWebMethods.WaitUntilElementPresent(driver,By.PartialLinkText("show"));
          if (commonWebMethods.IsElementPresent(driver,By.PartialLinkText("show"))) 
               commonWebMethods.Click(driver,By.PartialLinkText("show"));
         
           Balance=commonWebMethods.GetText(driver,By.ClassName("balance-in-header"),"Balance not found");
           
              return Balance;
         }

         public string HomePage_Balance_mobile(IWebDriver driver)
         {
             String Balance = null;

             Balance = commonWebMethods.GetText(driver, By.XPath("//span[@data-bind='balance']"), "Balance not found",false);

             return Balance;
         }

         /// <summary>
         ///  Enter the details of the customer in Registration page - Modified for Playtech page
         /// </summary>
         /// <param name="driverObj">broswer</param>
         /// <param name="regData">registration details</param>
         /// <returns></returns>

         public string PP_Registration(IWebDriver driverObj, ref Registration_Data regData, string bonus = null,bool checkFindAddress=false,bool invalidBonus=false)
         {
             var random = new Random();
             ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
             myBrowser.Start();
             myBrowser.WindowMaximize();
             System.Threading.Thread.Sleep(2000);
            
             string fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             string lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             
             
             Random r = new Random();
             int number = r.Next(9);

             string username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower() + number;
             string email = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + regData.email;
            
            

             Object thisLock = new Object();
             lock (thisLock)
             {
                 string uname = "Failed";

                 try
                 {
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

                         if (checkFindAddress)
                             BaseTest.Assert.IsTrue(wAction.IsElementPresent(driverObj, By.Id("house")), "Find address button did not work");
                         else
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

                     BaseTest.AddTestCase("Is HTTPS in Reg page?", "Url should be https");
                     BaseTest.Assert.IsTrue(driverObj.Url.Contains("https:"), "URL is not https");
                     BaseTest.Pass();


                 uname:

                 regData.username = username;

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", regData.username, "User name  Not Found", 0, false);

                 pass:

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
                 wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);

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
                     if(FrameGlobals.projectName=="IP2")
                        wAction.SelectDropdownOption(myBrowser, By.Id("depositLimit"), "5000", "Limit not found", 0, false);
                     else
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code", "promo_code not found", 0, false);
                     if (bonus != null)
                     {
                         wAction.Type(driverObj, By.Id("coupon"), bonus);
                     }

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
                       
                         System.Threading.Thread.Sleep(10000);
                       //  myBrowser.WaitForPageToLoad("5000");

                         if (invalidBonus)
                         {
                           if( wAction.IsElementPresent(driverObj,By.XPath("//p[contains(text(),'We encountered an error during the registration process')]")))
                             return "Success";
                         }
               

              

                     ClkAgain:
                         wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
                        if (wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.ModelDialogOK)))

                             goto ClkAgain;

                 }
                 catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }

                 BaseTest.AddTestCase("Is Cashier HTTPS?", "Url should be https");
                 BaseTest.Assert.IsTrue(driverObj.Url.Contains("https:"), "URL is not https");
                 BaseTest.Pass();

                 regData.registeredUsername = regData.username;
                 return regData.username;
             }
         }

         public string PP_Registration_RegulatedCountry(IWebDriver driverObj, ref Registration_Data regData)
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
         
                 

                 
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);

                 
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", fname, "First name not found", 0, false);

                 
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", lname, "Last name not found", 0, false);

                 
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);

                 
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", email, "Email  Not Found", 0, false);

                 

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found", 0, false);

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);

                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);
                     if (FrameGlobals.projectName == "IP2")
                         wAction.SelectDropdownOption(myBrowser, By.Id("depositLimit"), "5000", "Limit not found", 0, false);
                     else
                         wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

                     System.Threading.Thread.Sleep(8000);
                     BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.regulatedCountry_Msg_XP)).Contains("If your country of residence is " + regData.country_code + " then please register on the " + regData.country_code + " website"), "Warning message did not appear/message incorrect");
                     wAction.Click(driverObj, By.LinkText("Cancel"), "cancel button on warning message not found");


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

               

                         regData.username = username;

                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", regData.username, "User name  Not Found", 0, false);

                   

                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);

                    
                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", "pwdverify_txt not found", 0, false);
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", regData.password, "Password  Not Found", 0, false);


                  
                         wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_cmb", Registration_Data.securityQues, "Title not found", 0, false);

                    
                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", "answer_txt not found", 0, false);
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", regData.answer, "House  Not Found", 0, false);

                   
                         wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "currency_ddn", regData.currency, "currency not found", 0, false);

                   
                         if (FrameGlobals.projectName == "IP2")
                             wAction.SelectDropdownOption(myBrowser, By.Id("depositLimit"), "5000", "Limit not found", 0, false);
                         else
                             wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code", "promo_code not found", 0, false);

                  
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

                      

                         System.Threading.Thread.Sleep(5000);
                         BaseTest.Assert.IsTrue(wAction.GetText(driverObj, By.XPath(Reg_Control.regulatedCountry_Msg_XP)).Contains("If your country of residence is " + regData.country_code + " then please register on the " + regData.country_code + " website"), "Warning message did not appear/message incorrect");
                 

                         regData.registeredUsername = regData.username;
                         return regData.username;
                     
         }
         public string PP_Registration_BannedCountry(IWebDriver driverObj, ref Registration_Data regData)
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




             wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);


             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", fname, "First name not found", 0, false);


             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", lname, "Last name not found", 0, false);


             wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
             wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
             wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);


             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", email, "Email  Not Found", 0, false);



             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found", 0, false);

             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);

             wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);
             if (FrameGlobals.projectName == "IP2")
                 wAction.SelectDropdownOption(myBrowser, By.Id("depositLimit"), "5000", "Limit not found", 0, false);
             else
                 wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

       

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



             regData.username = username;

             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", regData.username, "User name  Not Found", 0, false);



             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);


             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", "pwdverify_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", regData.password, "Password  Not Found", 0, false);



             wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_cmb", Registration_Data.securityQues, "Title not found", 0, false);


             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", "answer_txt not found", 0, false);
             wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", regData.answer, "House  Not Found", 0, false);


             wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "currency_ddn", regData.currency, "currency not found", 0, false);


             if (FrameGlobals.projectName == "IP2")
                 wAction.SelectDropdownOption(myBrowser, By.Id("depositLimit"), "5000", "Limit not found", 0, false);
             else
                 wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

             wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code", "promo_code not found", 0, false);


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


             bool flag = false;
             System.Threading.Thread.Sleep(5000);
              flag = wAction.GetText(driverObj, By.XPath(Reg_Control.bannedCountry_Msg_XP)).Contains("error");
             if (!flag)
             {
             ClkAgain:
                 wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
                 if (wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.ModelDialogOK)))
                     goto ClkAgain;

                 wAction.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame_ID), null, 10);
                 System.Threading.Thread.Sleep(2000);
                 string isReg = wAction.GetText(driverObj, By.Name("header_username"), "No username header found post reg", false);
                 flag = isReg.Contains(regData.username);
             }

             BaseTest.Assert.IsTrue(flag, "Banned country restriction did not happen");


             regData.registeredUsername = regData.username;
             return regData.username;

         }

         public string PP_Registration_FV(IWebDriver driverObj, ref Registration_Data regData)
         {
             var random = new Random();
             ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
             myBrowser.Start();
             myBrowser.WindowMaximize();
             System.Threading.Thread.Sleep(2000);
            
             regData.fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
            
             regData.email = "test@playtech.com";
            
             Object thisLock = new Object();
             lock (thisLock)
             {
                 string uname = "Failed";

                 try
                 {
                  
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);


                     #region Fname
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "A"+Keys.Tab);
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
                 
                     if (!(wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "fname_error")))
                         BaseTest.Fail("First name is accepting < 2 char as minimum");

                        string _34Letter = "QWERXXXXOPASDFGHJKLZXCVBNMQWERtyui";
                 
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", _34Letter + Keys.Tab);
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
                 
                     string fnameVal = wAction._GetAttribute(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "value");
                     if (fnameVal != _34Letter && fnameVal == _34Letter.Substring(0, 30))
                     {
                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", regData.fname, "First name not found");
                     }
                     else
                         BaseTest.Fail("First name is accepting > 30 char as maximum");
                     #endregion

                     #region Lname
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "A" + Keys.Tab);

                     if (!(wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "lname_error")))
                         BaseTest.Fail("Last name is accepting < 2 char as minimum");

                      _34Letter = "QWERXXXXOPASDFGHJKLZXCVBNMQWERtyui";

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt");
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", _34Letter + Keys.Tab);
                     string lnameVal = wAction._GetAttribute(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "value");
                     if (lnameVal != _34Letter && fnameVal == _34Letter.Substring(0, 30))
                     {
                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt");
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", regData.fname, "First name not found");
                     }
                     else
                         BaseTest.Fail("First name is accepting > 30 char as maximum");
                     #endregion

                     #region DOB
                     DateTime dt = DateTime.Today.AddYears(-17);
                     string yrBelo18 = dt.Year.ToString();

                     if(!wAction.IsElementPresent(driverObj,By.XPath("//select[@id='birthYear']/option[text()='"+yrBelo18+"']")))
                     {
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);
                     }
                     else
                         BaseTest.Fail("Date of birth can be entered below 18 as well");
                     #endregion

                     // email:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", regData.email, "Email  Not Found");


                     #region Phone
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "12"+Keys.Tab, "Phone number  Not Found");
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt");
                     if (!(wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "phone_error")))
                         BaseTest.Fail("Phone number is accepting < 2 char as minimum");


                     string _17Letter = "12345678901234567";

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt");
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", _17Letter + Keys.Tab);
                     string phones = wAction._GetAttribute(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "value");
                     if (phones != _17Letter && phones == _17Letter.Substring(0, 15))
                     {
                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt");
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found");
                     }
                     else
                         BaseTest.Fail("Phone number is accepting > 15 char as maximum");                   



                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);
                   #endregion
                     
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

                     uname = regData.username;

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", uname, "User name  Not Found", 0, false);

                 pass:

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
                 wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);

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
                     wAction._Click(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "DepositLimitPeriod_Radio", "Limit period radio not found/not clickable", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code", "promo_code not found", 0, false);
                     
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
                     System.Threading.Thread.Sleep(10000);
                     myBrowser.WaitForPageToLoad("5000");

                   
                     ClkAgain:
                     wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
                     if (wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.ModelDialogOK)))

                         goto ClkAgain;

                 }
                 catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }
                 regData.registeredUsername = uname;
                 return uname;
             }
         }

         public string PP_Registration_Germany(IWebDriver driverObj, ref Registration_Data regData, string bonus = null, bool checkFindAddress = false, bool invalidBonus = false)
         {
             var random = new Random();
             ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
             myBrowser.Start();
             myBrowser.WindowMaximize();
             System.Threading.Thread.Sleep(2000);
             //if (FrameGlobals.waitMilliSec_Reg != 0)
             //    FrameGlobals.waitMilliSec_Reg = FrameGlobals.waitMilliSec_Reg + 10000;
             //System.Threading.Thread.Sleep(FrameGlobals.waitMilliSec_Reg);
             //if (FrameGlobals.waitMilliSec_Reg == 0)
             //    FrameGlobals.waitMilliSec_Reg = FrameGlobals.waitMilliSec_Reg + 10000;

             regData.fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             //regData.email = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + "@gmil.com";
             regData.email = "test@playtech.com";
             //regData.uk_postcode = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + random.Next(11, 99).ToString(CultureInfo.InvariantCulture);
             //regData.uk_addr_street_1 = regData.uk_addr_street_1.Replace("499house", "123" + StringCommonMethods.GenerateAlphabeticGUID().ToLower());

             Object thisLock = new Object();
             lock (thisLock)
             {
                 string uname = "Failed";

                 try
                 {
                     int accept = 3;

                 title:
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);

                 fname:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", regData.fname, "First name not found", 0, false);

                 lname:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", regData.lname, "Last name not found", 0, false);

                 dob:
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);

                 email:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", regData.email, "Email  Not Found", 0, false);

                 tele:

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found", 0, false);

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);

                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);

                 address:
                     
                      bool flag = false;

                 if (!wAction._IsElementPresent(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Postal_Mandatory"))
                     throw new Exception("Postal Code not mandatory field");

                 wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "1234"+Keys.Tab, "Post Code Not Found");
                     if (!wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Postal_error"))
                         throw new Exception("Postal Code accepting less than 5 digits");

                wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "post_txt not found", 0, false);
                     
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "123456" + Keys.Tab, "Post Code Not Found");

                     if (wAction._GetAttribute(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "value") == "123456")
                         throw new Exception("Postal Code accepting less than 5 digits");

                 
                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "post_txt not found", 0, false);
                Postal:
                     

                     if (flag )
                     {
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", regData.uk_postcode, "Post Code Not Found");
                         goto LastStep;
                     }
                    
                     else
                     {
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", string.Empty, "Post Code Not Found");

                     }
                                 wAction.Type(driverObj, By.Id("address"), regData.uk_addr_street_1, "address box not found");
                                 wAction.Type(driverObj, By.Id("city"), regData.city, "City box not found");
                         
                        // wAction.Type(driverObj, By.Id("address"), regData.uk_addr_street_1, "address box not found");
                       //  wAction.Type(driverObj, By.Id("city"), regData.city, "City box not found");
                    

                 uname:

                     uname = regData.username;

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", uname, "User name  Not Found", 0, false);

                 pass:

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);

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
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code", "promo_code not found", 0, false);
                     if (bonus != null)
                     {
                         wAction.Type(driverObj, By.Id("coupon"), bonus);
                     }
                 checkbox:
                     if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                     {
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "terms_check", Keys.Space, "Accept Check Box Not Found", 0, false);

                     }

                     else
                         wAction._Click(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "terms_check", "Accept Check Box Not Found", 0, false);


                     Thread.Sleep(TimeSpan.FromSeconds(2));
                     LastStep:
                     if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcc_Btn", Keys.Space, "Submit not found", 0, false);
                     else

                         wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcc_Btn", "Submit button not found", 0, false);
                     System.Threading.Thread.Sleep(10000);
                     myBrowser.WaitForPageToLoad("5000");

                     

                     //if (wAction._IsElementPresent(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "reg_error"))
                     //{
                         if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Postal_error")) && accept-- != 0)
                         {
                             flag=true;
                             goto Postal;
                         }
                         /*
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_error")) && accept-- != 0)
                     {

                         goto title;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "lname_error")) && accept-- != 0)
                     {

                         goto lname;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "addr_error")) && accept-- != 0)
                     {

                         goto address;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "city_error")) && accept-- != 0)
                     {

                         goto address;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "bob_error")) && accept-- != 0)
                     {

                         goto dob;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "phone_error")) && accept-- != 0)
                     {

                         goto tele;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "pwd_error")) && accept-- != 0)
                     {

                         goto pass;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_error")) && accept-- != 0)
                     {

                         goto security;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "email_error")))
                     {

                         goto email;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "uname_error")) && accept-- != 0)
                     {

                         goto uname;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "pwdConfirm_error")) && accept-- != 0)
                     {

                         goto pconfirm;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "depLimit_error")) && accept-- != 0)
                     {

                         goto deplimit;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "answer_error")) && accept-- != 0)
                     {

                         goto answer;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "houseno_error")) && accept-- != 0)
                     {

                         goto address;
                     }
                     else if ((wAction._IsElementPresent(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "terms_error")) && accept-- != 0)
                     {

                         goto checkbox;
                     }
                 }
                 else
                     if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//button[text()='Ok']")))
                         throw new Exception();
                }
                 */
                 


                     if(!flag)
                         throw new Exception("Empty Postal code has been registered");
                     ClkAgain:
                     wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
                     if (wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.ModelDialogOK)))

                         goto ClkAgain;

                 }
                 catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }
                 regData.registeredUsername = uname;
                 return uname;
             }
         }
         public string PP_Registration_IreLand(IWebDriver driverObj, ref Registration_Data regData, string bonus = null, bool checkFindAddress = false, bool invalidBonus = false)
         {
             var random = new Random();
             ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
             myBrowser.Start();
             myBrowser.WindowMaximize();
             System.Threading.Thread.Sleep(2000);
            
             regData.fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();            
             regData.email = "test@playtech.com";

             BaseTest.AddTestCase("Is HTTPS?", "Url should be https");
             BaseTest.Assert.IsTrue(driverObj.Url.Contains("https:"), "URL is not https");
             BaseTest.Pass();

                 string uname = "Failed";

                 try
                 {
                     int accept = 3;

                 title:
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "title_cmb", Registration_Data.title, "Title not found", 0, false);

                 fname:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", "fname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt", regData.fname, "First name not found", 0, false);

                 lname:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", "lname_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt", regData.lname, "Last name not found", 0, false);

                 dob:
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);

                 email:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "email_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", regData.email, "Email  Not Found", 0, false);

                 tele:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", "phone_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found", 0, false);

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);

                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);

                 address:
                     bool flag = false;

                     if (wAction._IsElementPresent(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Postal_Mandatory"))
                         throw new Exception("Postal Code is mandatory field for Ireland");
                        if (!wAction._IsElementPresent(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "County_Mandatory"))
                         throw new Exception("County field is not mandatory field for Ireland");


                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "post_txt not found", 0, false);

                     //wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "123456" + Keys.Tab, "Post Code Not Found");

                     //if (wAction._GetAttribute(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "post_txt", "value") == "123456")
                     //    throw new Exception("Postal Code accepting less than 5 digits");

                              
                     wAction.Type(driverObj, By.Id("address"), regData.uk_addr_street_1, "address box not found");
                     wAction.Type(driverObj, By.Id("city"), regData.city, "City box not found");
                     wAction.Type(driverObj,By.Id("county"),"County", "address box not found");

               

                 uname:

                     uname = regData.username;

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", uname, "User name  Not Found", 0, false);

                 pass:

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);

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
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", Registration_Data.depLimit, "Limit not found", 0, false);

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "promo_code", "promo_code not found", 0, false);
                     if (bonus != null)
                     {
                         wAction.Type(driverObj, By.Id("coupon"), bonus);
                     }
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
                     System.Threading.Thread.Sleep(10000);
                     myBrowser.WaitForPageToLoad("5000");                   
                     
                 ClkAgain:
                     wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
                     if (wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.ModelDialogOK)))

                         goto ClkAgain;

                   

                 }
                 catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }
                 regData.registeredUsername = uname;
                 return uname;
           
         }

         public string PP_Registration_QuickReg(IWebDriver driverObj, ref Registration_Data regData,MyAcct_Data acctData)
         {
             var random = new Random();
             ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
             myBrowser.Start();
             myBrowser.WindowMaximize();
             System.Threading.Thread.Sleep(2000);

             regData.fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();

            

             Object thisLock = new Object();
             lock (thisLock)
             {
                 string uname = "Failed";

                 try
                 {

                 fname:
                  regData.fname =    wAction._GetAttribute(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "fname_txt","value", "fname_txt not found",false);
                 

                 lname:                   
                  regData.lname =   wAction._GetAttribute(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "lname_txt","value", "Last name not found");

                   
                     #region DOB
                    
                         wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "day_cmb", Registration_Data.dob_day, "DOB_day not found", 0, false);
                         wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "month_cmb", Registration_Data.dob_month, "DOB_month not found", 0, false);
                         wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "year_cmb", Registration_Data.dob_year, "DOB_year not found", 0, false);
                     
                     #endregion

                     // email:
                         System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                         BaseTest.Assert.IsTrue(wAction._GetAttribute(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "email_txt", "value", "Email  Not Found").Contains(acctData.paypalUser), "Paypal user email is incorrect in registration window");
                         regData.email = acctData.paypalUser;


                     #region Phone
                    
                         wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt");
                         wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_txt", regData.telephone, "Phone number  Not Found");
                    

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", "mobile_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mobile_txt", regData.mobile, "Mobile number  Not Found", 0, false);
                     #endregion

                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", regData.country_code, "Country Code not Found", 0, false);

                 address:   
                     wAction.Type(driverObj, By.Id("house"), regData.houseNo, "House number box not found");

                 uname:

                     uname = regData.username;

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", "username_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "username_txt", uname, "User name  Not Found", 0, false);

                 pass:

                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", "userPwd_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "userPwd_txt", regData.password, "Password  Not Found", 0, false);

                 pconfirm:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", "pwdverify_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "pwdverify_txt", regData.password, "Password  Not Found", 0, false);


                 security:
                     wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_cmb", Registration_Data.securityQues, "Title not found", 0, false);

                 answer:
                     wAction._Clear(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", "answer_txt not found", 0, false);
                     wAction._Type(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "answer_txt", regData.answer, "House  Not Found", 0, false);

                 BettingCurrency:
                     BaseTest.Assert.IsTrue(wAction._GetAttribute(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "currency_ddn", "value", "currency not found").ToString().Contains("USD"), "Unable to fetch selected currency value or selected value not equal to Preset value");

               
                 deplimit:
                  BaseTest.Assert.IsTrue(wAction._GetAttribute(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "deplimit_cmb", "value", "Deposit Limit not found").ToString().Contains("1000"), "Unable to fetch selected dep limit value or selected value not equal to Preset value");
                   BaseTest.Assert.IsTrue(wAction.IsElementPresent(myBrowser,By.XPath("//input[@class='checked' and @type='radio' and @data-select='weekly']")),"Deposit limit is not set to weekly as preset value");
                   
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
                     System.Threading.Thread.Sleep(10000);
                     myBrowser.WaitForPageToLoad("5000");


               //  ClkAgain:
                     wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));
                 wAction.Click(driverObj, By.XPath(Reg_Control.ModelDialogOK));

                     //if (wAction.IsElementPresent(driverObj, By.XPath(Reg_Control.ModelDialogOK)))

                     //    goto ClkAgain;

                 }
                 catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }
                 regData.registeredUsername = uname;
                 return uname;
             }
         }

         public void EnterAmount_ToDepositLessThanMinimum(IWebDriver driverObj, MyAcct_Data acctData)
         {

             // double beforeVal = 0;
             //BaseTest.Assert.IsTrue(commonWebMethods.WaitAndMovetoFrame(driverObj, "acctmidnav"),"My Acct Window not loaded due to server error");
             bool flag = false;
             DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
             commonWebMethods.Click(driverObj, By.XPath(Login_Control.userDisplay_Xpath), "Logout Drop down not found", FrameGlobals.elementTimeOut, false);
             commonWebMethods.Click(driverObj, By.LinkText("Deposit"), "Deposit link not found", 0, false);
             string newWindow = driverObj.WindowHandles.ToArray()[1].ToString();
             driverObj.SwitchTo().Window(newWindow);
             Thread.Sleep(4000);
             commonWebMethods.WaitforPageLoad(driverObj);
             //string withdrawAmountTextBox_Id = "amount";
             ////string withDrawTab_Id = "//li//span[contains(string(),'Withdraw') and not(contains(string(),'Cancel'))]";

             commonWebMethods.WaitforPageLoad(driverObj);
             wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

             double depositAmount = 1.00;

             BaseTest.AddTestCase("Verify that customer cannot deposit less than 5GBP into the wallet", "The customer should get the min limit error message");
             wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
             wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");

             //   wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "CVV_txt",acctData.cardCSC, "CVV_txt not found");
             wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");
             wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", depositAmount.ToString() + Keys.Return, "Amount_txt not found");

             Thread.Sleep(4000);
             string name = "playtech.cashier.generic.deposit.error.amount.small";
             if (!wAction.IsElementPresent(driverObj, By.Name(name)))
             {
                 Framework.BaseTest.Assert.Fail("Min Limit Error message is not displayed");
             }
             BaseTest.Pass();
         }

         #region TBD
         ///// <summary>
         ///// Common method for deposit 
         ///// </summary>
         ///// <param name="driverObj">broswer</param>
         ///// <param name="acctData">My acct details</param>
         ///// <param name="amount">amount to deposit</param>
         ///// <returns>returns before value</returns>
         //public bool CommonDeposit_Netteller_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount, string window)
         //{

         //    //double beforeVal = 0;
         //    //BaseTest.Assert.IsTrue(commonWebMethods.WaitAndMovetoFrame(driverObj, "acctmidnav"),"My Acct Window not loaded due to server error");
         //    //bool flag = false;
         //    //DateTime varDateTime;
         //    try
         //    {
         //        string depositWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";
         //        DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
         //        //   string depositWalletPath = "id('ssec_ftbal')/td/table/tbody/tr/td/table/tbody/tr[contains(string(),'" + acctData.depositWallet + "')]/td[2]";


         //        //    commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
         //        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
         //        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
         //        double beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.withdrawWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());

         //        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

         //        BaseTest.AddTestCase("Verify that Payment method is added to the customer", "The customer should have the payment option added to it");
         //        wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
         //        wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
         //        wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
         //        //   wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "CVV_txt",acctData.cardCSC, "CVV_txt not found");
         //        wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

         //        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
         //        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
         //        BaseTest.Pass();

         //        driverObj.SwitchTo().DefaultContent();
         //        wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Generic_Xpath), null, 0, false);


         //        //#region TempUpdatedCode

         //        ////commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), "Unable to switch back to the deposit page", FrameGlobals.elementTimeOut);
         //        ////wAction.Click(driverObj, By.LinkText("Go back"), null, 0, false);
         //        ////wAction.Click(driverObj, By.XPath("id('depositResultDialog')//button"), null, 0, false);

         //        //driverObj.Close();
         //        //return true;

         //        ////wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);

         //        ////wAction.WaitAndMovetoPopUPWindow(driverObj, window, "Base portal window not found",5);
         //        ////wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", 0, false);
         //        ////driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
         //        ////commonWebMethods.WaitAndMovetoFrame(driverObj, Reg_Control.CashierFrame, "Unable to switch back to the deposit page", FrameGlobals.elementTimeOut);
         //        ////wAction.Click(driverObj, By.XPath("id('depositResultDialog')//button"), "Deposit confirmation message not found", 0, false);



         //        // #endregion


         //        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
         //        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
         //        double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());

         //        BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
         //        BaseTest.Pass();
         //        // Console.WriteLine("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal);

         //        wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
         //        driverObj.SwitchTo().DefaultContent();

         //        if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
         //            return true;
         //        else
         //            return false;
         //    }
         //    catch (Exception e) { BaseTest.Fail("Deposit Failed:" + e.Message.ToString()); }
         //    return true;
         //}

#endregion

         /// <summary>
         /// Common method for deposit when deposit limit is unset 
         /// </summary>
         /// <param name="driverObj">broswer</param>
         /// <param name="acctData">My acct details</param>

         /// <summary>
         /// Author : Roopa
         /// </summary>
         /// <param name="driverObj"></param>
         /// <param name="countryName"></param>
         /// <param name="countryCode"></param>
         /// <returns></returns>
         public bool RegionCode_RegPage(IWebDriver driverObj, string countryName, string countryCode)
         {
             var random = new Random();
             ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
             myBrowser.Start();
             myBrowser.WindowMaximize();
             System.Threading.Thread.Sleep(2000);
             string mobileCode = null;
             string phoneCode = null;


             try
             {
                 wAction._SelectDropdownOption(myBrowser, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "country_cmb", countryName, "Country Code not Found", 0, false);
                 phoneCode = wAction._GetAttribute(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "phone_box", "value", "phone code box is not found", false);
                 mobileCode = wAction._GetAttribute(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.id, "mob_box", "value", "mobile code box is not found", false);

                 if (phoneCode.Equals(mobileCode))
                 {
                     if (!phoneCode.Contains(countryCode))
                     {
                         return false;
                     }

                 }
                 return true;


             }
             catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }
             return false;
         }

         public bool Common_Deposit_LimitUnset(IWebDriver driverObj, MyAcct_Data acctData)
         {

            

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
               
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                 if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepLimitUnset_err", "Error message not found", false).Contains("unset deposit limits"))
                     goto finish;
                 else
                 {

                     BaseTest.AddTestCase("Verify deposit limit unset error message is displayed when the customer tries to deposit", "Appropriate error message should be displayed");
                     wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "neteller_payment_tab", "Neteller paymyemt method is not found", FrameGlobals.reloadTimeOut, false);

                     wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "neteller_accountId", acctData.cardCSC, "Neteller account id textbox is not found", FrameGlobals.reloadTimeOut, false);
                     wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
                     wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
                     wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
                     //   wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "CVV_txt",acctData.cardCSC, "CVV_txt not found");
                     wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

                     System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                     wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
                     System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                 }
                 finish:
                 if (wAction._GetText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepLimitUnset_err", "Error message not found", false).Contains("unset deposit limits"))
                     BaseTest.Pass();
                 else
                     return false;
             
             return true;
         }
         public bool CommonWithdraw_Netteller_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount, bool closeWindow=true,bool inWithdrawPage=false, string site=null)
         {


             
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string withWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";

                 
                 double beforeVal = 0;
             //    commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

                 if (!inWithdrawPage)
                 {
                     if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                         wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                     else
                         wAction._Click_Javascript(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 }
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

                 // wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found");
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", acctData.depositAmt, "Amount_txt not found");
                 //   wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "CVV_txt",acctData.cardCSC, "CVV_txt not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "destinationWallet_cmb", acctData.withdrawWallet, "destinationWallet_cmb not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 //driverObj.SwitchTo().DefaultContent();
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Confirmation_Dlg", "Deposit not successfull", 0, false);

                 //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), "Unable to switch back to the deposit page", FrameGlobals.elementTimeOut);

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

              //   wAction.Click(driverObj, By.XPath("id('depositResultDialog')//button"), null, 0, false);
              //   System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
                 wAction.Click(driverObj, By.Name("genericDeposit_ok"));
                 wAction.Click(driverObj, By.LinkText("Go back"), null, 0, false);

                 wAction.WaitforPageLoad(driverObj);
                 double AfterVal = 0;
                 temp = wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                 driverObj.SwitchTo().DefaultContent();

                 if(closeWindow)
                 driverObj.Close();
                 BaseTest.AddTestCase("Wallet:" + acctData.withdrawWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before Withdrawal:" + beforeVal + ", Amount after Withdrawal:" + AfterVal, "Amount should be calulated accordingly");
                 BaseTest.Pass();
                 if (AfterVal == beforeVal - double.Parse(acctData.depositAmt))
                     return true;
                 else
                     return false;
             
            // return true;
         }
         public bool CommonWithdraw_Sofort_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount, bool closeWindow = true, bool inWithdrawPage = false)
         {


               DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string withWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";

                 double beforeVal = 0;
                 //    commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

                 if (!inWithdrawPage)
                     wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 string temp = wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");

                 BaseTest.AddTestCase("Verify that the amount in the " + acctData.withdrawWallet + " wallet is less than the desired amount" + acctData.depositAmt, "Amount should be more than the desired amount");
                 if (beforeVal < double.Parse(acctData.depositAmt))
                     BaseTest.Fail("Insufficient balance in the wallet to withdraw");
                 else
                     BaseTest.Pass();
                 wAction.SelectDropdownOption_ByPartialText(driverObj, By.Id(CashierPage.Sofort_withdraw_From_ID), acctData.withdrawWallet, "Gaming option not found", 0, false);
                 wAction.SelectDropdownOption(driverObj, By.Id(CashierPage.Sofort_withdraw_To_ID), "Bank Transfer", "Bank transfer option not found", 0, false);

                 
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", acctData.depositAmt, "Amount_txt not found");
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Confirmation_Dlg", "Deposit not successfull", 0, false);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                 wAction.Click(driverObj, By.Name("genericDeposit_ok"));
                 wAction.WaitAndMovetoFrame(driverObj, By.Name("RedirectTarget"),null,5);
                 
                 wAction.Type(driverObj, By.XPath("//input[@title='Enter the receiving bank name']"), CashierPage.Sofort_withdraw_BankName, "unable to type name", 0, false);
                 wAction.Type(driverObj, By.XPath("//input[@title='Enter the receiving bank’s branch location']"), CashierPage.Sofort_Withdraw_BranchLocation, "unable to type branch location", 0, false);
                 wAction.Type(driverObj, By.XPath("//input[contains(@title,'Enter the recipient’s account name')]"), CashierPage.Sofort_Withdraw_Payee, "unable to type payee", 0, false);
                 wAction.Type(driverObj, By.XPath("//input[contains(@title,'Enter the recipient’s IBAN')]"), CashierPage.Sofort_Withdraw_IBAN, "unable to type IBAN", 0, false);
                 wAction.Type(driverObj, By.XPath("//input[contains(@title,'Enter the receiving bank’s SWIFT BIC ')]"), CashierPage.Sofort_Withdraw_SWIFT_BIC, "unable to type Swift_BIC", 0, false);
                 wAction.Click(driverObj, By.XPath("//input[@type='submit']"), "Submit button not found",2, false);
                 //By.Id(CashierPage.Sofort_withdraw_submit_Btn_ID)
                 //closing window
                 wAction.Click(driverObj, By.LinkText("Close window"), null, 0, false);
                 wAction.WaitforPageLoad(driverObj);
                 double AfterVal = 0;
                 driverObj.SwitchTo().DefaultContent();
                 
                 temp = wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                 

                 if (closeWindow)
                     driverObj.Close();
                 BaseTest.AddTestCase("Wallet:" + acctData.withdrawWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before Withdrawal:" + beforeVal + ", Amount after Withdrawal:" + AfterVal, "Amount should be calulated accordingly");
                 BaseTest.Pass();
                 if (AfterVal == beforeVal - double.Parse(acctData.depositAmt))
                     return true;
                 else
                     return false;
             
         }
         public bool Withdraw_TextBoXValidation(IWebDriver driverObj, MyAcct_Data acctData, string amount, bool closeWindow = true)
         {


             try
             {
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string withWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";


               //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
              
                 wAction.SelectDropdownOption_ByPartialText(driverObj, By.Id(CashierPage.Sofort_withdraw_To_ID), "NETeller", "Netteller option not found", 0, false);

                 
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Test"+Keys.Tab, "Unable to type on Amount_txt");
                 
                 //wAction._SelectDropdownOption(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "destinationWallet_cmb", acctData.withdrawWallet, "destinationWallet_cmb not found");

               //  System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "IncorrectWithdrawAmt_err"))
                     throw new Exception("Incorrect error message did not appear");


                 //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                 //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", Keys.Tab, "Unable to type on Amount_txt");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", " "+Keys.Tab, "Unable to type on Amount_txt");
               // wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");

                 //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 if (!wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "IncorrectWithdrawAmt_err"))
                     throw new Exception("Below limit error message did not appear");


                 
             }
             catch (Exception e) { BaseTest.Fail("Withdraw Failed:" + e.Message.ToString()); }
             return true;
         }
         public bool CommonWithdraw_PT(IWebDriver driverObj, MyAcct_Data acctData, bool closeWindow = true)
         {
             string amount = acctData.depositAmt;

             try
             {
                 string BeforeVal = null;
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string withWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";


             //    commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 string temp = wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 if (temp.Contains("USD"))
                 {
                     BeforeVal = temp.Replace("USD", "").Trim();
                 }
                 else if(temp.Contains("£"))
                 {
                     BeforeVal = temp.Replace("£","").Trim();
                 }
                 double beforeVal = double.Parse(BeforeVal);

                 BaseTest.AddTestCase("Verify that the amount in the " + acctData.withdrawWallet + " wallet is less than the desired amount" + acctData.depositAmt, "Amount should be more than the desired amount");
                 if (beforeVal < double.Parse(acctData.depositAmt))
                     BaseTest.Fail("Insufficient balance in the wallet to withdraw");
                 else
                     BaseTest.Pass();

                 //wAction.SelectDropdownOption_ByPartialText(driverObj, By.Id(CashierPage.Sofort_withdraw_To_ID), "NETeller", "Netteller option not found", 0, false);

                 // wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found");
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", acctData.depositAmt, "Amount_txt not found");
                 //   wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "CVV_txt",acctData.cardCSC, "CVV_txt not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "destinationWallet_cmb", acctData.withdrawWallet, "destinationWallet_cmb not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 //driverObj.SwitchTo().DefaultContent();
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Confirmation_Dlg", "Deposit not successfull", 0, false);

                 //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), "Unable to switch back to the deposit page", FrameGlobals.elementTimeOut);

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                 wAction.Click(driverObj, By.XPath("id('depositResultDialog')//button"), null, 0, false);
                 wAction.Click(driverObj, By.LinkText("Go back"), null, 0, false);

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                 
                 
                 
                 temp = wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 if (temp.Contains("USD"))
                 {
                     BeforeVal = temp.Replace("USD", "").Trim();
                 }
                 else if (temp.Contains("£"))
                 {
                     BeforeVal = temp.Replace("£", "").Trim();
                 }
                 double AfterVal = double.Parse(BeforeVal);
                 
                 //double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString().Replace('£', ' ').Trim());

                 driverObj.SwitchTo().DefaultContent();

                 if (closeWindow)
                     driverObj.Close();
                 BaseTest.AddTestCase("Wallet:" + acctData.withdrawWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before Withdrawal:" + beforeVal + ", Amount after Withdrawal:" + AfterVal, "Amount should be calulated accordingly");
                 BaseTest.Pass();
                 if (AfterVal == beforeVal - double.Parse(acctData.depositAmt))
                     return true;
                 else
                     return false;
             }
             catch (Exception e) { BaseTest.Fail("Withdraw Failed:" + e.Message.ToString()); }
             return true;
         }
         public bool CommonCancelWithdraw_Netteller_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount, bool closeWindow = true, bool inWithdrawPage=false)
         {


                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string depositWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";


                // commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), null, FrameGlobals.elementTimeOut);

                 if(!inWithdrawPage)
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 string temp = wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 double beforeVal = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "CancelWithdrawTab", "CancelWithdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6));

                 BaseTest.AddTestCase("Check if any Withdraw transaction availabel to cancel", "Minimum one withdraw request should be present");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Cancel_btn",Keys.Enter, "No Withdraw request available / Cancel button not found", 0, false);
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


                 if(closeWindow)
                 driverObj.Close();
                 BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before Cancellation:" + beforeVal + ", Amount after Cancellation:" + AfterVal, "Amount should be calculated accordingly");
                 BaseTest.Pass();


                 if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                     return true;
                 else
                     return false;
           
         }

         public bool CommonTransferWithdraw_Netteller_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount)
         {



                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string Wallet1Path = "//tr[td[contains(text(),'" + acctData.wallet1 + "')]]/td[2]";
                 string Wallet2Path = "//tr[td[contains(text(),'" + acctData.wallet2 + "')]]/td[2]";


              //   commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                   if(! wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Transfer_Btn"))
               {                 
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
               }
                   System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 string temp = wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet1 + "Wallet value not found", false).ToString();
                 double beforeVal1=0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal1 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                  temp = wAction.GetText(driverObj, By.XPath(Wallet2Path), acctData.wallet2 + "Wallet value not found", false).ToString();
                 double beforeVal2 = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal2 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                 BaseTest.AddTestCase("Verify that the amount in the " + acctData.depositWallet + " wallet is not less than the desired amount: " + acctData.depositAmt, "Amount should be more than the desired amount");
                 if (beforeVal1 < double.Parse(acctData.depositAmt))
                     BaseTest.Fail("Insufficient balance in the wallet to Transfer");
                 else
                     BaseTest.Pass();



                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferFrom_cmb", acctData.wallet1, "transferFrom wallet not found in dropdown");
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferTo_cmb", acctData.wallet2, "transferTo wallet not found in dropdown");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TAmount_txt", acctData.depositAmt, "Amount textbox not found");
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Transfer_Btn", "Transfer_Btn not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
              BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "ConfirmationOK_btn"), "Success confirmation dialog not found");


                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

                  temp = wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet1 + "Wallet value not found", false).ToString();
                  double AfterVal1 = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal1 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                 temp = wAction.GetText(driverObj, By.XPath(Wallet2Path), acctData.wallet2 + "Wallet value not found", false).ToString();
                 double AfterVal2 = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal2 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");



                 BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transfer Amount:" + acctData.depositAmt + " => Amount Before Transfer:" + beforeVal1 + ", Amount after Transfer:" + AfterVal1, "Amount should be calculated accordingly");
                 BaseTest.Pass();
                 BaseTest.AddTestCase("Wallet:" + acctData.withdrawWallet + " Transfer Amount:" + acctData.depositAmt + " => Amount Before Transfer:" + beforeVal2 + ", Amount after Transfer:" + AfterVal2, "Amount should be calculated accordingly");
                 BaseTest.Pass();


                 

                 if ((AfterVal1 == beforeVal1 - double.Parse(acctData.depositAmt))
                     && (AfterVal2 == beforeVal2 + double.Parse(acctData.depositAmt)))
                     return true;
                 else
                     return false;

           
         }

         public bool CommonTransfer_Netteller_PT_InsuffBal(IWebDriver driverObj, MyAcct_Data acctData)
         {


             try
             {
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string Wallet1Path = "//tr[td[contains(text(),'" + acctData.wallet1 + "')]]/td[2]";
                 
           //      commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                string temp = wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet1 + "Wallet value not found", false).ToString();
                double beforeVal1 = 0;
                if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                    beforeVal1 = StringCommonMethods.ReadDoublefromString(temp);
                else
                    BaseTest.Fail("Wallet value is null/Blank");

                 double temp1 = beforeVal1 + 10;
                 acctData.depositAmt = temp1.ToString();


                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TAmount_txt", acctData.depositAmt, "Amount textbox not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferFrom_cmb", acctData.depositWallet, "transferFrom wallet not found in dropdown");
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferTo_cmb", acctData.withdrawWallet, "transferTo wallet not found in dropdown");

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Transfer_Btn", "Transfer_Btn not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 BaseTest.Assert.IsTrue(wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Transer_insuffBal"), "Insufficient fund error message not found");
                 BaseTest.Pass();

             }
             catch (Exception e) { BaseTest.Fail("Transfer Failed:" + e.Message.ToString()); }
             return true;
         }

         public bool CommonTransfer_Vegas_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount)
         {


             try
             {
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string Wallet1Path = "//tr[td[contains(text(),'" + acctData.wallet1 + "')]]/td[2]";
                 string Wallet2Path = "//tr[td[contains(text(),'" + acctData.wallet2 + "')]]/td[2]";


                 //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("redirectCCFrame"), "Cashier Frame not found", FrameGlobals.elementTimeOut);
                 //driverObj.SwitchTo().DefaultContent();
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);

                string temp = wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet1 + "Wallet value not found", false).ToString();
                double beforeVal1 = 0;
                double beforeVal2 = 0;

                if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                    beforeVal1 = StringCommonMethods.ReadDoublefromString(temp);
                else
                    BaseTest.Fail("Wallet value is null/Blank");

                 temp= wAction.GetText(driverObj, By.XPath(Wallet2Path), acctData.wallet2 + "Wallet value not found", false).ToString();
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal2 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");
                  

                 BaseTest.AddTestCase("Verify that the amount in the " + acctData.depositWallet + " wallet is not less than the desired amount: " + acctData.depositAmt, "Amount should be more than the desired amount");
                 if (beforeVal1 < double.Parse(acctData.depositAmt))
                     BaseTest.Fail("Insufficient balance in the wallet to Transfer");
                 else
                     BaseTest.Pass();


                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TAmount_txt", acctData.depositAmt, "Amount textbox not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferFrom_cmb", acctData.depositWallet, "transferFrom wallet not found in dropdown");
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferTo_cmb", acctData.withdrawWallet, "transferTo wallet not found in dropdown");

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Transfer_Btn", "Transfer_Btn not found", 0, false);
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "ConfirmationOK_btn", "Success confirmation dialog not found", 0, false);


                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

               //  double AfterVal1 = double.Parse(wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());
               //  double AfterVal2 = double.Parse(wAction.GetText(driverObj, By.XPath(Wallet2Path), acctData.withdrawWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());
                 temp = wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet1 + "Wallet value not found", false).ToString();
                 double AfterVal1 = 0;
                 double AfterVal2 = 0;

                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal1 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");

                 temp = wAction.GetText(driverObj, By.XPath(Wallet2Path), acctData.wallet2 + "Wallet value not found", false).ToString();
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal2 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");

                 BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transfer Amount:" + acctData.depositAmt + " => Amount Before Transfer:" + beforeVal1 + ", Amount after Transfer:" + AfterVal1, "Amount should be calculated accordingly");
                 BaseTest.Pass();
                 BaseTest.AddTestCase("Wallet:" + acctData.withdrawWallet + " Transfer Amount:" + acctData.depositAmt + " => Amount Before Transfer:" + beforeVal2 + ", Amount after Transfer:" + AfterVal2, "Amount should be calculated accordingly");
                 BaseTest.Pass();




                 if ((AfterVal1 == beforeVal1 - double.Parse(acctData.depositAmt))
                     && (AfterVal2 == beforeVal2 + double.Parse(acctData.depositAmt)))
                     return true;
                 else
                     return false;

             }
             catch (Exception e) { BaseTest.Fail("Transfer Failed:" + e.Message.ToString()); }
             return true;
         }

         public bool CommonTransfer_InvalidAmt_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount)
         {


             try
             {
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string Wallet1Path = "//tr[td[contains(text(),'" + acctData.wallet1 + "')]]/td[2]";
                 string Wallet2Path = "//tr[td[contains(text(),'" + acctData.wallet2 + "')]]/td[2]";


                 //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Name("redirectCCFrame"), "Cashier Frame not found", FrameGlobals.elementTimeOut);
                 //driverObj.SwitchTo().DefaultContent();
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);

                // double beforeVal1 = double.Parse(wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet1 + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());
               //  double beforeVal2 = double.Parse(wAction.GetText(driverObj, By.XPath(Wallet2Path), acctData.wallet2 + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());
                 string temp = wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet1 + "Wallet value not found", false).ToString();
                 double beforeVal1 = 0;
                 double beforeVal2 = 0;

                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal1 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");

                 temp = wAction.GetText(driverObj, By.XPath(Wallet2Path), acctData.wallet2 + "Wallet value not found", false).ToString();
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal2 = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");
                  


                 double transfeAmt = beforeVal1 + 10;
                 acctData.depositAmt = transfeAmt.ToString();

                 /*
                 BaseTest.AddTestCase("Verify that the amount in the " + acctData.depositWallet + " wallet is not less than the desired amount: " + acctData.depositAmt, "Amount should be more than the desired amount");
                 if (beforeVal1 < double.Parse(acctData.depositAmt))
                     BaseTest.Fail("Insufficient balance in the wallet to Transfer");
                 else
                     BaseTest.Pass();*/


                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TAmount_txt", acctData.depositAmt, "Amount textbox not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferFrom_cmb", acctData.depositWallet, "transferFrom wallet not found in dropdown");
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferTo_cmb", acctData.withdrawWallet, "transferTo wallet not found in dropdown");

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Transfer_Btn", "Transfer_Btn not found", 0, false);
                 //wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "ConfirmationOK_btn", "Success confirmation dialog not found", 0, false);

                 //BaseTest.AddTestCase("Verify whether error message is displayed for insufficient balance", "Amount should be calculated accordingly");
                 if (wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Invalidamt_error"))
                 {
                     goto end;
                 }
                 else
                 {
                     
                     return false;
                 }
                 
             }
             catch (Exception e) { BaseTest.Fail("Transfer Failed:" + e.Message.ToString()); }
             end:
             return true;
         }
         public void VerifyCustSourceDetailinIMS_Exchange(IWebDriver imsDriver)
         {
             imsDriver.SwitchTo().DefaultContent();
             commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
             System.Threading.Thread.Sleep(3000);
             string clientTypePath = "//td[contains(string(),'Sign up client type')]/following-sibling::td[1]";
             string clientPlatfrom = "//td[contains(string(),'clientplatform')]/following-sibling::td[1]";
             BaseTest.Assert.IsTrue(imsDriver.FindElement(By.XPath(clientTypePath)).Text.ToLower().Contains("sportsbook"), "client type is not updated as expected");
             BaseTest.Assert.IsTrue(imsDriver.FindElement(By.XPath(clientPlatfrom)).Text.Contains("web"), "client platfrom is not updated as expected");

           //  BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom18")).GetAttribute("value").Contains("OBE_NEW"), "custom18 not updated as expected");

             BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom17")).GetAttribute("value").Contains("OBE"), "custom17 not updated as expected");

             //Sign up Client Type - Games

         }
         public bool AddRandomEvent_Bestlip_sports(IWebDriver driverObj)
         {
             string eventOddsListPath = "//div[contains(@id,'eventTime')]//ancestor::tr/td[@class='odds']/a";
             //  List<IWebElement> odds=commonWebMethods.Click

           
                //List<IWebElement> evnts = wAction._returnWebElements(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "oddsUpcoming", "", 0, false);
                
                    wAction._ClickAll(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "oddsUpcoming",null, 0, false);
              
                
             List<IWebElement> odds = wAction._returnWebElements(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "eventOddsListPath", null, 0, false);
          
             
             if (odds.Count > 0)
             {
                 foreach (IWebElement odd in odds)
                 {
                     if (odd.Text.ToString() != string.Empty)
                     {
                         odd.Click();
                         break;
                     }
                 }
                 wAction.WaitUntilElementPresent(driverObj, wAction.returnLocatorObject(ORFile.Betslip, wActions.locatorType.id, "check_btn"));

             }
             else
             {

                 return false;
             }
             string betslipCount = wAction._GetText(driverObj, ORFile.Betslip, wActions.locatorType.id, "BetSlip_count", "Betslip count text not found", false);
             if (betslipCount.Contains("1"))
             {
                 return true;
             }
             else
             {
                 return false;

             }
         }

         //public bool AddEvent_Bestlip_sports(IWebDriver driverObj,string evnt)
         //{
         //    string eventOddsListPath = "//div[contains(@id,'eventTime')]//ancestor::tr/td[@class='odds']/a";
         //    //  List<IWebElement> odds=commonWebMethods.Click

         //    wAction._ClickAll(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "oddsUpcoming", "", 0, false);


         //    List<IWebElement> odds = wAction._returnWebElements(driverObj, ORFile.Betslip, wActions.locatorType.xpath, "eventOddsListPath", "", 0, false);
         //    if (odds.Count > 0)
         //    {
         //        odds[0].Click();
         //        wAction.WaitUntilElementPresent(driverObj, wAction.returnLocatorObject(ORFile.Betslip, wActions.locatorType.id, "check_btn"));

         //    }
         //    else
         //    {

         //        return false;
         //    }
         //    string betslipCount = wAction._GetText(driverObj, ORFile.Betslip, wActions.locatorType.id, "BetSlip_count", "Betslip count text not found", false);
         //    if (betslipCount.Contains("1"))
         //    {
         //        return true;
         //    }
         //    else
         //    {
         //        return false;

         //    }
         //}


        #region Deleted
        ///// <summary>
        ///// To be deleted
        ///// </summary>
        ///// <param name="imsDriver"></param>
        ///// <param name="regData"></param>
        //public void VerifyUpdatedEmailCustDetailinIMS(IWebDriver imsDriver, Registration_Data regData)
        //{
        //    imsDriver.SwitchTo().DefaultContent();
        //    DateTime varDateTime = DateTime.Now;
        //    DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
        //    do
        //    {
        //        varDateTime = DateTime.Now;
        //        try
        //        {
        //            imsDriver.SwitchTo().Frame("main");
        //            break;
        //        }
        //        catch (Exception) { }
        //    } while (varDateTime <= varElapseTime);

        //    BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("email")).GetAttribute("value").Contains(regData.email), "Upadated EmailID did not match in IMS Admin");
        //}

        //public double ChangeCard_Deposit(IWebDriver driverObj, MyAcct_Data acctData, string amount)
        //{
        //    driverObj.SwitchTo().DefaultContent();
        //    double beforeVal=0;
        //    //BaseTest.Assert.IsTrue(commonWebMethods.WaitAndMovetoFrame(driverObj, "acctmidnav"),"My Acct Window not loaded due to server error");
        //    bool flag = false;
        //    DateTime varDateTime;
        //    DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
        //    do
        //    {
        //        varDateTime = DateTime.Now;
        //        try
        //        {
        //            driverObj.SwitchTo().Frame("acctmidnav");
        //            flag = true;
        //            break;
        //        }
        //        catch (Exception) { }
        //    } while (varDateTime <= varElapseTime);
        //    BaseTest.Assert.IsTrue(flag, "My acct page not loaded");

        //    driverObj.FindElement(By.LinkText("Deposit")).Click();
        //    driverObj.SwitchTo().DefaultContent();
        //    varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
        //    flag = false;
        //    do
        //    {
        //        varDateTime = DateTime.Now;
        //        try
        //        {
        //            driverObj.SwitchTo().Frame("main");
        //            flag = true;
        //            break;
        //        }
        //        catch (Exception) { }
        //    } while (varDateTime <= varElapseTime);
        //    BaseTest.Assert.IsTrue(flag, "My Deposit page not loaded");



        //   // System.Threading.Thread.Sleep(5000);

        //    varElapseTime = DateTime.Now.AddSeconds(60);
        //    do
        //    {
        //        varDateTime = DateTime.Now;
        //        try
        //        {
        //            beforeVal = double.Parse(driverObj.FindElement(By.XPath("//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[@class='tdbal']")).Text.ToString().Replace('£', ' ').Trim());
        //            break;               
        //        }
        //        catch (Exception) { }
        //    } while (varDateTime <= varElapseTime);


        //    commonWebMethods.Click(driverObj, By.LinkText("Change your card"),"Change card link not present",0,false);


        //    //driverObj.FindElement(By.Id(MyAcct_Control.toAcct_Name)).SendKeys(MyAcct_Data.depositWallet);
        //    //commonWebMethods.Type(driverObj, By.Id(MyAcct_Control.toAcct_Name), MyAcct_Data.withdrawWallet, MyAcct_Data.withdrawWallet + " not present in the withdrawal wallet");
        //    commonWebMethods.Type(driverObj, By.Id(MyAcct_Control.WalletType_ID), acctData.depositWallet, acctData.depositWallet + " not present in the deposit wallet");


        //    System.Threading.Thread.Sleep(3000);

        //    if (commonWebMethods.IsElementPresent(driverObj, By.Name(MyAcct_Control.cardtext_name)))
        //    {
        //        commonWebMethods.Type(driverObj, By.Name(MyAcct_Control.cardtext_name), acctData.card, "Card box not found");
        //        commonWebMethods.Type(driverObj, By.Name(MyAcct_Control.StartYear_Name), MyAcct_Data.startYear, "start year not found");
        //        commonWebMethods.Type(driverObj, By.Name(MyAcct_Control.ExpiryYear_Name), MyAcct_Data.endYear, "End year not found");
        //        commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.hldr_name), "Gary", "Card Holder name input not found");

        //    }

        //    commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.cardCSC_Xpath), acctData.cardCSC, "CSC card box not found");
        //    commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.amount_Xpath), amount, "Amount text not found");
        //    commonWebMethods.Type(driverObj, By.XPath(MyAcct_Control.depositPassword_Xpath), regData.password, "depositPassword box not found");

        //    int i = 2;
        //    do
        //    {
        //        try
        //        {
        //            if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
        //                driverObj.FindElement(By.XPath(MyAcct_Control.depositButton_Xpath)).SendKeys(Keys.Enter);
        //            else
        //                driverObj.FindElement(By.XPath(MyAcct_Control.depositButton_Xpath)).Click();
        //            break;

        //        }
        //        catch (Exception)
        //        {
        //            if (i == 0)
        //                BaseTest.Fail("deposit button not found");
        //            driverObj.SwitchTo().ActiveElement();
        //            try
        //            {

        //                if (commonWebMethods.IsElementPresent(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']")))
        //                    commonWebMethods.Click(driverObj, By.XPath("//img[@class='lpInviteChatImgClose']"));
        //            }
        //            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }
        //            break;
        //        }

        //    } while (i-- != 0);




        //    commonWebMethods.WaitforPageLoad(driverObj);
        //    if (commonWebMethods.IsElementPresent(driverObj, By.XPath(MyAcct_Control.depsoitTransactionHeading_Xpath)))
        //    {
        //        driverObj.FindElement(By.LinkText("Continue")).Click();

        //        commonWebMethods.WaitUntilElementPresent(driverObj, By.XPath(MyAcct_Control.depositAuthHeading_Xpath));

        //        driverObj.FindElement(By.XPath(MyAcct_Control.depsoitAuthenticateBTN_Xpath)).Click();
        //        System.Threading.Thread.Sleep(8000);
        //        commonWebMethods.WaitforPageLoad(driverObj);
        //    }
        //    return beforeVal;
        //}
        #endregion


         /// <summary>
         ///  Enter the details of the customer in Registration page - Modified for Playtech page
         /// </summary>
         /// <param name="driverObj">broswer</param>
         /// <param name="regData">registration details</param>
         /// <returns></returns>

         public string Mobile_Registration(IWebDriver driverObj, ref Registration_Data regData)
         {
             var random = new Random();
             System.Threading.Thread.Sleep(2000);
             if (FrameGlobals.waitMilliSec_Reg != 0)
                 FrameGlobals.waitMilliSec_Reg = FrameGlobals.waitMilliSec_Reg + 10000;
             System.Threading.Thread.Sleep(FrameGlobals.waitMilliSec_Reg);
             if (FrameGlobals.waitMilliSec_Reg == 0)
                 FrameGlobals.waitMilliSec_Reg = FrameGlobals.waitMilliSec_Reg + 10000;

             regData.fname = regData.fname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.lname = regData.lname + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.email = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + "@gmil.com";
             //regData.uk_postcode = StringCommonMethods.GenerateAlphabeticGUID().ToLower() + random.Next(11, 99).ToString(CultureInfo.InvariantCulture);
             //regData.uk_addr_street_1 = regData.uk_addr_street_1.Replace("499house", "123" + StringCommonMethods.GenerateAlphabeticGUID().ToLower());
             ISelenium myBrowser = new WebDriverBackedSelenium(driverObj, "http://www.google.com");
             myBrowser.Start();
             myBrowser.WindowMaximize();

             Object thisLock = new Object();
             lock (thisLock)
             {
                 string uname = "Failed";

                 try
                 {
                     int accept = 3;

                 title:
                     commonWebMethods.SelectDropdownOption_ByValue(driverObj, By.Id("title"), "Mr", "Unable to locate title dropbox in registration page", 0, false);

                 fname:
                     commonWebMethods.Clear(driverObj, By.Id("firstname"), "First name not found", 0, false);
                     commonWebMethods.Type(driverObj, By.Id("firstname"), regData.fname, "First name not found", 0, false);

                 lname:
                     commonWebMethods.Clear(driverObj, By.Id("lastname"), "Last name not found", 0, false);
                     commonWebMethods.Type(driverObj, By.Id("lastname"), regData.lname, "First name not found", 0, false);

                 dob:
                     commonWebMethods.SelectDropdownOption_ByValue(driverObj, By.Id("day"), Registration_Data.dob_day, "Unable to locate day dropbox in registration page", 0, false);
                     myBrowser.Select("css=#month", "March");
                     commonWebMethods.SelectDropdownOption_ByValue(driverObj, By.Id("year"), Registration_Data.dob_year, "Unable to locate year dropbox in registration page", 0, false);
                 address:
                     if (regData.country_code == "United Kingdom")
                     {
                         commonWebMethods.Type(driverObj, By.Id("housename"), regData.houseNo, "House Name field is not displayed in registration page", 0, false);
                         commonWebMethods.Type(driverObj, By.Id("postcode"), regData.uk_postcode, "Post code field is not displayed in registration page", 0, false);
                         commonWebMethods.Click(driverObj, By.Id("findaddress"), "findaddress field is not displayed in registration page", 0, false);
                         myBrowser.WaitForPageToLoad("5000");
                         System.Threading.Thread.Sleep(5000);
                         commonWebMethods.Type(driverObj, By.Id("address2"), regData.uk_addr_street_1, "Post code field is not displayed in registration page", 0, false);

                     }
                     else
                     {
                         commonWebMethods.SelectDropdownOption_ByValue(driverObj, By.Id("country"), regData.country_code, "Unable to locate Country dropbox", 0, false);
                         commonWebMethods.Type(driverObj, By.Id("housename"), regData.houseNo, "House Name field is not displayed in registration page", 0, false);
                         commonWebMethods.Type(driverObj, By.Id("postcode"), regData.uk_postcode, "Post code field is not displayed in registration page", 0, false);
                         commonWebMethods.Type(driverObj, By.Id("address"), regData.uk_addr_street_1, "Address field is not displayed in registration page", 0, false);
                         commonWebMethods.Type(driverObj, By.Id("city"), regData.city, "City field is not displayed in registration page", 0, false);
                     }

                 email:
                     commonWebMethods.Type(driverObj, By.Id("email"), regData.email, "Email field is not displayed in registration page", 0, false);

                 tele:

                     commonWebMethods.Type(driverObj, By.Id("mobnumber"), regData.mobile, "Mobile field is not displayed in registration page", 0, false);
                 //commonWebMethods.SelectDropdownOption_ByValue(driverObj, By.Id("accountcurrency"), Registration_Data., "Unable to locate Country dropbox", 0, false);

                 uname:

                     uname = regData.username;
                     commonWebMethods.Clear(driverObj, By.Id("username"), "username name not found", 0, false);
                     commonWebMethods.Type(driverObj, By.Id("username"), uname, "username name not found", 0, false);
                     System.Threading.Thread.Sleep(3000);

                 pass:
                     commonWebMethods.Clear(driverObj, By.Id("password"), "password name not found", 0, false);
                     commonWebMethods.Type(driverObj, By.Id("password"), regData.password, "password name not found", 0, false);

                 pconfirm:
                     commonWebMethods.Clear(driverObj, By.Id("confirmpassword"), "password name not found", 0, false);
                     commonWebMethods.Type(driverObj, By.Id("confirmpassword"), regData.password, "password name not found", 0, false);

                 security:
                 //wAction._SelectDropdownOption(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "securityQues_cmb", Registration_Data.securityQues, "Title not found", 0, false);

                 answer:
                     commonWebMethods.Type(driverObj, By.Id("maidennameans"), "Test", "Security question field not found", 0, false);

                 checkbox:
                     commonWebMethods.Click(driverObj, By.Id("tandc"), "tandc name not found", 0, false);
                     commonWebMethods.Click(driverObj, By.Id("createaccount"), "createaccount button not found", 0, false);
                     myBrowser.WaitForPageToLoad("6000");
                     System.Threading.Thread.Sleep(2000);
                     if (!commonWebMethods.IsElementPresent(driverObj, By.Id("depositlink")))
                     {
                         throw new Exception();
                     }

                 }
                 catch (Exception e) { BaseTest.Assert.Fail("Failed to input info in Customer registration details page"); }
                 regData.registeredUsername = uname;
                 return uname;
             }
         }

         /// <summary>
         /// Common method for deposit 
         /// </summary>
         /// <param name="driverObj">broswer</param>
         /// <param name="acctData">My acct details</param>
         /// <param name="amount">amount to deposit</param>
         /// <returns>returns before value</returns>
         public bool CommonDeposit_CreditCard_PT(IWebDriver driverObj, MyAcct_Data acctData, string window)
         {
             try
             {
                 string depositWalletPath = "//tr[td[contains(text(),'" +acctData.depositWallet+ "')]]/td[2]";
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);

                 wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt");
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                 //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), "Cashier Frame not found", FrameGlobals.elementTimeOut);
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 //double beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.withdrawWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());


                 //wAction.WaitUntilElementPresent(driverObj, By.XPath("//div[@class='fund-transfer']"));
                 string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());
                 


                 string bval = temp.Replace(",", string.Empty).Trim();
                 Regex re = new Regex(@"\d+");
                 Match b4Value = re.Match(bval);

                 double beforeVal = double.Parse(b4Value.Value);
                 

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                 

                 //BaseTest.AddTestCase("Verify that Deposit is successful to the customer", "The customer should have the payment option added to it");

                 wAction.WaitforPageLoad(driverObj);
                 Thread.Sleep(5000);
                     if(wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath,"Visa_img"))
                         wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Visa_img", "Visa not found");

                     else if (wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "EntroPay_img"))
                         wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "EntroPay_img", "EntroPay_img not found");

                     else if (wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "MasterCard_img"))
                         wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "MasterCard_img", "MasterCard_img not found");

                     else
                         wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Mastro_img", "Mastro_img not found");



                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "creditcard_cvv", "CVV_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "creditcard_cvv", acctData.cardCSC, "CVV_txt not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

                // wAction.Click(driverObj,By.XPath( "//a/span[contains(text(),'close')]"), "Go back link not found",0,false);

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.link, "goback_lnk");
                 //BaseTest.Pass();

                 #region TempUpdatedCode

                 //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), "Unable to switch back to the deposit page", FrameGlobals.elementTimeOut);
                 //wAction.Click(driverObj, By.LinkText("Go back"), null, 0, false);
                 //wAction.Click(driverObj, By.XPath("id('depositResultDialog')//button"), null, 0, false);

                 //driverObj.Close();
                 //return true;

                 ////wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", "Customer menu Link not found", FrameGlobals.reloadTimeOut, false);

                 ////wAction.WaitAndMovetoPopUPWindow(driverObj, window, "Base portal window not found",5);
                 ////wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Deposit_lnk", "Deposit Link not found", 0, false);
                 ////driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
                 ////commonWebMethods.WaitAndMovetoFrame(driverObj, Reg_Control.CashierFrame, "Unable to switch back to the deposit page", FrameGlobals.elementTimeOut);
                 ////wAction.Click(driverObj, By.XPath("id('depositResultDialog')//button"), "Deposit confirmation message not found", 0, false);



                 #endregion

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                 
                 temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Trim());

                  bval = temp.Replace(",", string.Empty).Trim();                  
                  b4Value = re.Match(bval);

                 double AfterVal = double.Parse(b4Value.Value);
                
                 //double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());

                BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                BaseTest.Pass();



                Console.WriteLine("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal);

                wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                driverObj.SwitchTo().DefaultContent();

                if (AfterVal == beforeVal + double.Parse(acctData.depositAmt))
                {
                    BaseTest.Pass();
                    return true;
                }
                else
                    return false;
                
                 
             }
             catch (Exception e) { 
                 BaseTest.Fail("Deposit Failed:" + e.Message.ToString());
             }
             return true;
         }

         /// <summary>
         /// Common method for withdrawl
         /// </summary>
         /// <param name="driverObj">broswer</param>
         /// <param name="acctData">My acct details</param>
         /// <param name="amount">amount to withdraw</param>
         /// <param name="closeWindow"></param>
         /// <returns></returns>
         public bool CommonWithdraw_CreditCard_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount, bool closeWindow = true)
         {


             try
             {
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";


             //    commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                 if(wAction._IsElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn"))
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                // double beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + " Wallet value not found", false).ToString().Replace('£', ' ').Trim());
                 string temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + " Wallet value not found", false).ToString().Trim());

                 string bval = temp.Replace(",", string.Empty).Trim();
                 Regex re = new Regex(@"\d+");
                 Match b4Value = re.Match(bval);

                 double beforeVal = double.Parse(b4Value.Value);


                 BaseTest.AddTestCase("Verify that the amount in the " + acctData.depositWallet + " wallet is less than the desired amount" + acctData.depositAmt, "Amount should be more than the desired amount");
                 if (beforeVal < double.Parse(acctData.depositAmt))
                     BaseTest.Fail("Insufficient balance in the wallet to withdraw");
                 else
                     BaseTest.Pass();

                 //wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);

                 // wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found");
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", acctData.depositAmt, "Amount_txt not found");
                 //wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "creditcard_cvv", "CVV_txt not found");
                 //wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "creditcard_cvv", acctData.cardCSC, "CVV_txt not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "destinationWallet_cmb", acctData.depositWallet, "destinationWallet_cmb not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "withdraw_btn not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 //driverObj.SwitchTo().DefaultContent();
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Confirmation_Dlg", "Deposit not successfull", 0, false);

                 //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), "Unable to switch back to the deposit page", FrameGlobals.elementTimeOut);

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                 wAction.Click(driverObj, By.XPath("id('depositResultDialog')//button"));
                 wAction.Click(driverObj, By.Name("genericDeposit_ok"));
                 
                 wAction.Click(driverObj, By.LinkText("Go back"), null, 0, false);

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                 //double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString().Replace('£', ' ').Trim());

                 temp = (wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + " Wallet value not found", false).ToString().Trim());

                  bval = temp.Replace(",", string.Empty).Trim();
                  re = new Regex(@"\d+");
                  b4Value = re.Match(bval);

                  double AfterVal = double.Parse(b4Value.Value);


                 driverObj.SwitchTo().DefaultContent();

                 if (closeWindow)
                     driverObj.Close();
                 BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before Withdrawal:" + beforeVal + ", Amount after Withdrawal:" + AfterVal, "Amount should be calulated accordingly");
                 BaseTest.Pass();
                 if (AfterVal == beforeVal - double.Parse(acctData.depositAmt))
                     return true;
                 else
                     return false;
             }
             catch (Exception e) { return false; }
             return true;
         }

         public bool NetTellerDeposit(IWebDriver driverObj, MyAcct_Data acctData, string amount, bool closeWindow)
         {
             try
             {
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                 BaseTest.AddTestCase("Verify that Payment method is added to the customer", "The customer should have the payment option added to it");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
                 //   wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "CVV_txt",acctData.cardCSC, "CVV_txt not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
                 if (closeWindow)
                     driverObj.Close();
                 BaseTest.Pass();
                
             }
             catch(Exception e){ BaseTest.Fail("Withdraw Failed:" + e.Message.ToString()); }
             return true;
         }

         public bool VerifyWithdraw_Limit(IWebDriver driverObj, string minVal, string maxVal)
         {

            try
             {
                 BaseTest.AddTestCase("Verifying Withdraw limit in Portal", "Withdrawal limit is set correctly");
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);

               //  commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(7));

                 if (driverObj.FindElement(By.XPath("//span[@name='genericWithdraw_limit']//parent::span")).Text.Contains(minVal.ToString()) && driverObj.FindElement(By.XPath("//span[@name='genericWithdraw_limit']//parent::span")).Text.Contains(maxVal.ToString()))
                 {
                     BaseTest.Pass();
                     BaseTest.AddTestCase("Verifying Withdraw limit boundry value in Portal", "Withdrawal limit is not functioning correctly");
                     //Verifying minimum limit
                     wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                     wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", (Convert.ToDecimal(minVal) - 1).ToString(), "Amount_txt not found");
                     wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");
                     System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                     if (!wAction.GetText(driverObj,By.XPath("//span[contains(@name,'playtech.cashier.generic.deposit.error')]")).Contains("Amount is below limits"))
                     {
                         BaseTest.Fail("No correct error message is displayed on entring the amount less than minimum withdrawal amount");
                     }

                     //Verifying maximum limit
                     wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                     wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", (Convert.ToDecimal(maxVal) + 1).ToString(), "Amount_txt not found");
                     wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");
                     System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                     if (!wAction.GetText(driverObj,By.XPath("//span[contains(@name,'playtech.cashier.generic.deposit.error')]")).Contains("The amount exceeds allowed limits"))
                     {
                         BaseTest.Fail("No correct error message is displayed on entring the amount more than maximum withdrawal amount");
                     }
                     BaseTest.Pass();

                 }
                 else { BaseTest.Fail("Withdraw limit is not set correctly in Portal, Withdraw tab"); }

             }
             catch (Exception e) { BaseTest.Fail("Unable to verify withdraw limit:" + e.Message.ToString()); }
             return true;
         }

         public bool CommonWithdraw_Skrill_PT(IWebDriver driverObj, MyAcct_Data acctData, string amount, bool closeWindow = true, bool inWithdrawPage = false)
         {

             acctData.withdrawWallet = acctData.depositWallet;
             try
             {
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
                 string withWalletPath = "//tr[td[contains(text(),'" + acctData.withdrawWallet + "')]]/td[2]";


                 //    commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

                 if (!inWithdrawPage)
                     wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 string temp =wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 double beforeVal = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                 BaseTest.AddTestCase("Verify that the amount in the " + acctData.withdrawWallet + " wallet is less than the desired amount" + acctData.depositAmt, "Amount should be more than the desired amount");
                 if (beforeVal < double.Parse(acctData.depositAmt))
                     BaseTest.Fail("Insufficient balance in the wallet to withdraw");
                 else
                     BaseTest.Pass();

                 wAction.SelectDropdownOption_ByPartialText(driverObj, By.Id(CashierPage.Sofort_withdraw_To_ID), "Skrill", "Skrill option not found", 0, false);

                 // wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found");
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "wAmount_txt", acctData.depositAmt, "Amount_txt not found");
                 //   wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "CVV_txt",acctData.cardCSC, "CVV_txt not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "destinationWallet_cmb", acctData.withdrawWallet, "destinationWallet_cmb not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Withdraw_btn", "deposit_btn not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 //driverObj.SwitchTo().DefaultContent();
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Confirmation_Dlg", "Deposit not successfull", 0, false);

                 //commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame), "Unable to switch back to the deposit page", FrameGlobals.elementTimeOut);

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                 //   wAction.Click(driverObj, By.XPath("id('depositResultDialog')//button"), null, 0, false);
                 //   System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
                 wAction.Click(driverObj, By.Name("genericDeposit_ok"));
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction.Click(driverObj, By.LinkText("Go back"));

                 wAction.WaitforPageLoad(driverObj);

               //  double AfterVal = double.Parse(wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString().Replace('£', ' ').Trim());
                 temp = wAction.GetText(driverObj, By.XPath(withWalletPath), acctData.withdrawWallet + " Wallet value not found", false).ToString();
                 double AfterVal = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal = StringCommonMethods.ReadDoublefromString(temp);
                 else
                     BaseTest.Fail("Wallet value is null/Blank");


                 driverObj.SwitchTo().DefaultContent();

                 if (closeWindow)
                     driverObj.Close();
                 BaseTest.AddTestCase("Wallet:" + acctData.withdrawWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before Withdrawal:" + beforeVal + ", Amount after Withdrawal:" + AfterVal, "Amount should be calulated accordingly");
                 BaseTest.Pass();
                 if (AfterVal == beforeVal - double.Parse(acctData.depositAmt))
                     return true;
                 else
                     return false;
             }
             catch (Exception e) { BaseTest.Fail("Withdraw Failed:" + e.Message.ToString()); }
             return true;
         }


         public void Createcustomer_PostMethod(ref Registration_Data regData, string title = "mr", string limit=null)
         {


             regData.fname =  StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             regData.lname =  StringCommonMethods.GenerateAlphabeticGUID().ToLower();
             Random r = new Random();
             int number = r.Next(9);
             regData.username = regData.username + StringCommonMethods.GenerateAlphabeticGUID().ToLower()+number;
             BaseTest.AddTestCase("Create new customer " + regData.username, "Customer creation should be successfull");

            // regData.email = "test@playtech.com";
             string emailid;
             if(FrameGlobals.projectName=="IP2")
                   emailid =  StringCommonMethods.GenerateAlphabeticGUID() + regData.email;
             else if (regData.email != "test@playtech.com")
                   emailid =  StringCommonMethods.GenerateAlphabeticGUID() +  regData.email;
             else
                 emailid = regData.email;

             emailid.Replace("@", "%40");
             string systemSource = "BNG";
             string systemSourceID = "BIN_WEB";
             // Create a request using a URL that can receive a post. 
             #region postmethod
             string reqURL = "https://account-stg.ladbrokes.com/en/registration?p_p_id=registration_WAR_accountportlet&p_p_lifecycle=2&p_p_state=normal&p_p_mode=view&p_p_resource_id=registerAccount&p_p_cacheability=cacheLevelPage&p_p_col_id=column-2&p_p_col_count=2 HTTP/1.1";
             if(FrameGlobals.projectName=="IP2")
                 reqURL = "https://account-test.ladbrokes.com/en/registration?p_p_id=registration_WAR_accountportlet&p_p_lifecycle=2&p_p_state=normal&p_p_mode=view&p_p_resource_id=registerAccount&p_p_cacheability=cacheLevelPage&p_p_col_id=column-2&p_p_col_count=2";

             WebRequest request = WebRequest.Create(reqURL);
             // Set the Method property of the request to POST.
             request.Method = "POST";
             // Create POST data and convert it to a byte array.
             String username = regData.username;
             string postData = null;
             if(limit==null)
              limit = Registration_Data.depLimit;
             
             if (regData.countryID == "GB")
                 postData = "enabledPostcodeLookup=true&clienttype=&oldserial=&language=&skin=&advertiser=&profileid=&bannerid=&creferer=&refererurl=&clientPlatform=&clientChannel=I"
                    + "&systemSource=" + systemSource + "&systemSourceId=" + systemSourceID + "&afterRegistrationAction=%2Fen%2Fcashierfirstdeposit"
                    + "&title=" + title + "&firstName=" + regData.fname + "&lastName=" + regData.lname + "&gender=M"
                    + "&birthDay=04&birthMonth=07&birthYear=1969&email=" + emailid + "&emailVerification=" + emailid
                    + "&country=GB"
                    + "&phoneAreaCode=%2B44&phone=" + regData.mobile + "&mobileAreaCode=%2B44&mobile="
                    + "&postCode=HA2+7JW&house=1&address=Middlx&city=Harrow"
                    + "&wantPromotion=true&promotionalNotificationEmail=true&_promotionalNotificationEmail=on&promotionalNotificationPhone=true&_promotionalNotificationPhone=on&promotionalNotificationSms=true&_promotionalNotificationSms=on"
                    + "&userName=" + regData.username + "&userPassword=" + regData.password + "&passwordVerify=" + regData.password
                    + "&verificationQuestion=playtech.ladbrokes.portlet.registration.question1&verificationAnswer=asd&currency=" + regData.currency
                    + "&depositActive=1&deposit-period=on&depositLimitPeriod=daily&depositLimit=" + limit
                    + "&coupon=&termsAndConditions=on&trackingData=";
             // else if(regData.countryID=="RU")
             else
                 postData = "enabledPostcodeLookup=true&clienttype=casino&oldserial=&language=&skin=&advertiser=&profileid=&bannerid=&creferer=&refererurl=&clientPlatform=web&clientChannel=I"
                     + "&systemSource=" + systemSource + "&systemSourceId=" + systemSourceID + "&afterRegistrationAction=%2Fen%2Fcashierfirstdeposit"
                     + "&title=mr&firstName=" + regData.fname + "&lastName=" + regData.lname + "&gender=M"
                     + "&birthDay=19&birthMonth=11&birthYear=1979&email=" + emailid + "&emailVerification=" + emailid
                     + "&country=" + regData.countryID
                     + "&phoneAreaCode=%2B7&phone=" + regData.mobile + "&mobileAreaCode=%2B7&mobile=" + regData.mobile
                     + "&postCode=" + regData.uk_postcode + "&house=&address=abc&city=city&county=&wantPromotion=true&promotionalNotificationEmail=true"
                     + "&_promotionalNotificationEmail=on&promotionalNotificationPhone=true&_promotionalNotificationPhone=on&promotionalNotificationSms=true&_promotionalNotificationSms=on&promotionalNotificationDirectMail=false&"
             + "userName=" + regData.username + "&userPassword=" + regData.password + "&passwordVerify=" + regData.password + "&verificationQuestion=playtech.ladbrokes.portlet.registration.question1&verificationAnswer=xyz&"
             + "currency=" + regData.currency + "&depositActive=1&deposit-period=on&depositLimitPeriod=daily&depositLimit=" + limit + "&coupon=&termsAndConditions=on&trackingData=;";



             byte[] byteArray = Encoding.UTF8.GetBytes(postData);
             // Set the ContentType property of the WebRequest.
             request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
             // Set the ContentLength property of the WebRequest.
             request.ContentLength = byteArray.Length;
             // Get the request stream.
             Stream dataStream = request.GetRequestStream();
             // Write the data to the request stream.
             dataStream.Write(byteArray, 0, byteArray.Length);
             // Close the Stream object.
             dataStream.Close();
             // Get the response.
             WebResponse response = request.GetResponse();
             // Display the status.
            // Console.WriteLine(((HttpWebResponse)response).StatusDescription);
             // Get the stream containing content returned by the server.
             dataStream = response.GetResponseStream();
             //WebHeaderCollection<wh> = response.Headers;
             // Open the stream using a StreamReader for easy access.
             StreamReader reader = new StreamReader(dataStream);
             // Read the content.
             string responseFromServer = reader.ReadToEnd();
             //  progressBar1.Value = 60;
             // Display the content.
             // Console.WriteLine(responseFromServer);
             if (!responseFromServer.Contains("Successful"))
             {
                 BaseTest.Fail("Customer creation failed");
             }
             // Clean up the streams.
             reader.Close();
             dataStream.Close();
             response.Close();
             BaseTest.Pass();

             #endregion
         }


         /// <summary>
         /// Common method for deposit for customer with AV status failed
         /// </summary>
         /// <param name="driverObj">broswer</param>
         /// <param name="acctData">My acct details</param>
         /// <param name="amount">amount to deposit</param>
         /// <returns>returns before value</returns>
         public bool CommonDeposit_Netteller_Non_AV(IWebDriver driverObj, MyAcct_Data acctData)
         {
         
                 string depositWalletPath = "//tr[td[contains(text(),'" + acctData.depositWallet + "')]]/td[2]";
                 DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                 string temp = wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false);
                
                 double beforeVal = 0;
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     beforeVal = StringCommonMethods.ReadDoublefromString(temp);

                 //double beforeVal=0;
                 //if (temp != null)
                 //{
                 //    string bval = temp.Replace(",", string.Empty).Trim();
                 //    Regex re = new Regex(@"\d+");
                 //    Match b4Value = re.Match(bval);
                 //    beforeVal = double.Parse(b4Value.Value);
                 //}
                     //beforeVal = double.Parse(wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false).ToString().Replace('£', ' ').Trim());

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);

                 BaseTest.AddTestCase("Verify that Payment method is added to the customer", "The customer should have the payment option added to it");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "NettellerPwd_txt", acctData.card, "Net teller Security Text not found", FrameGlobals.reloadTimeOut, false);
                 wAction._Clear(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", "Amount_txt not found");
                 wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "Amount_txt", acctData.depositAmt, "Amount_txt not found");
                 //   wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "CVV_txt",acctData.cardCSC, "CVV_txt not found");
                 wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.id, "sourceWallet_cmb", acctData.depositWallet, "sourceWallet_cmb not found");

                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "deposit_btn", "deposit_btn not found");
                 BaseTest.Pass();

                 //driverObj.SwitchTo().DefaultContent();
                // wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Generic_Xpath), "Deposit not successfull", 0, false);


               

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "WithdrawTab", "Withdraw Tab not found", 0, false);
                 System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                 temp = wAction.GetText(driverObj, By.XPath(depositWalletPath), acctData.depositWallet + "Wallet value not found", false);
                 double AfterVal = 0;                
                 if (StringCommonMethods.ReadDoublefromString(temp) != -1)
                     AfterVal = StringCommonMethods.ReadDoublefromString(temp);


                
                             
                 BaseTest.AddTestCase("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal, "Deposit has failed");
                 BaseTest.Pass();
                 // Console.WriteLine("Wallet:" + acctData.depositWallet + " Transaction Amount:" + acctData.depositAmt + " => Amount Before deposit:" + beforeVal + ", Amount after deposit:" + AfterVal);

                 wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "DepositTab", "Deposit Tab not found", 0, false);
                 driverObj.SwitchTo().DefaultContent();

                 if (AfterVal == beforeVal)
                     return true;
                 else
                     return false;
          
         }

         public bool FundTransfer_Non_AV(IWebDriver driverObj, MyAcct_Data acctData, string amount)
         {

             DateTime varElapseTime = DateTime.Now.AddSeconds(FrameGlobals.reloadTimeOut);
             string Wallet1Path = "//tr[td[contains(text(),'" + acctData.wallet1 + "')]]/td[2]";
             string Wallet2Path = "//tr[td[contains(text(),'" + acctData.wallet2 + "')]]/td[2]";
             //   commonWebMethods.WaitAndMovetoFrame(driverObj, By.Id(Reg_Control.CashierFrame));

             wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TransferTab", "Transfer Tab not found", 0, false);
             string temp = wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet1 + "Wallet value not found", false).ToString();
              double beforeVal1=0;
             if(StringCommonMethods.ReadDoublefromString(temp)!=-1)
               beforeVal1 = StringCommonMethods.ReadDoublefromString(temp);
            
             // temp = wAction.GetText(driverObj, By.XPath(Wallet1Path), acctData.wallet2 + "Wallet value not found", false).ToString();
             //double beforeVal2 = 0;
             //if (StringCommonMethods.ReadDoublefromString(temp) != -1)
             //    beforeVal2 = StringCommonMethods.ReadDoublefromString(temp);


             BaseTest.AddTestCase("Verify that the amount in the " + acctData.depositWallet + " wallet is not less than the desired amount: " + acctData.depositAmt, "Amount should be more than the desired amount");
             if (beforeVal1 < double.Parse(acctData.depositAmt))
                 BaseTest.Fail("Insufficient balance in the wallet to Transfer");
             else
                 BaseTest.Pass();

             wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferFrom_cmb", acctData.depositWallet, "transferFrom wallet not found in dropdown");
             System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
             wAction._SelectDropdownOption_ByPartialText(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "transferTo_cmb", acctData.withdrawWallet, "transferTo wallet not found in dropdown");
             wAction._Type(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "TAmount_txt", acctData.depositAmt, "Amount textbox not found");
             System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
             wAction._Click(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "Transfer_Btn", "Transfer_Btn not found", 0, false);
             System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
             if (wAction._WaitUntilElementPresent(driverObj, ORFile.Accounts_Wallets_Banking, wActions.locatorType.xpath, "av_transfer_error"))
                 return true;
             else
                 return false;

         }

         public void VerifyDepLimit_newLook(IWebDriver imsDriver)
         {
             BaseTest.AddTestCase("Verify deposit limit is set as expected", "Deposit limit should be same as set during reg");
             imsDriver.SwitchTo().DefaultContent();

             wAction.Click(imsDriver, By.XPath("id('depositlimits_navigation')"), "deposit limit section not found", noWait: false);

             BaseTest.Assert.IsTrue( StringCommonMethods.ReadDoublefromString(wAction.GetText(imsDriver, By.XPath("id('depositlimits')//tr[3]/td[5]"), "Deposit limit value not found", false)) == int.Parse(Registration_Data.depLimit), "Deposit limit not matching" + Registration_Data.depLimit + " Actual:" + wAction.GetText(imsDriver, By.XPath("id('depositlimits')//tr[3]/td[5]")));
             BaseTest.Pass();
           
             
         }

         public double NoException_DoubleValueReturn(IWebDriver driverObj, By locator,bool wait)
         {
             string value = "0";
             try
             {
                 value = wAction.GetText(driverObj, locator, null, wait).ToString();
                 return StringCommonMethods.ReadDoublefromString(value);

             }
             catch (Exception)
             {
                 return double.Parse(value);
             }
         }



        //========================IMS==================
         public void VerifyCustSourceDetailinIMS(IWebDriver imsDriver, string cusValue1, string cusValue2)
         {
             imsDriver.SwitchTo().DefaultContent();
             commonWebMethods.Click(imsDriver, By.Id("imgsec_customs"), "Customs Link not found");
             System.Threading.Thread.Sleep(3000);

             BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom17")).GetAttribute("value").Contains(cusValue1), "custom17 not updated as expected");
             BaseTest.Assert.IsTrue(imsDriver.FindElement(By.Id("custom18")).GetAttribute("value").Contains(cusValue2), "custom18 not updated as expected");

             //Sign up Client Type - Games

         }


        //Common
         public string OpenRegistrationPage(IWebDriver driverObj, bool switchback = false)
         {

             BaseTest.AddTestCase("Open My Register Page", "Reg page should opened successfully");
             string url = driverObj.Url.ToLower();
             string Urlcode;
             if (url.Contains("exchange"))
                 Urlcode = "exchange";
             else if (url.Contains("mobenga"))
                 Urlcode = "sports";
             else
                 Urlcode = url.Substring(0, url.IndexOf("ladbrokes.com"));



             if (Urlcode.Contains("exchange"))
                 wAction._Click_JavaScript(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Ecom_Btn", "Join button not found", 0, false);

             else if (Urlcode.Contains("games"))
                 wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "Join_Games_Btn", "Join button not found", 0, false);


             else if (Urlcode.Contains("sports"))
                 wAction.Click(driverObj, By.XPath(Sportsbook_Control.join_XP), "join button not found", 0, false);
             else
                 //   wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "createAcct_Xpath", "Join button not found", 0, false);
                 wAction.Click(driverObj, By.XPath("//a[normalize-space()='Join']"), "Join button not found", 0, false);

             string portal = driverObj.WindowHandles.ToArray()[0].ToString();

             wAction.WaitAndMovetoPopUPWindow(driverObj, "Registration window not opened", 20);
             wAction.WaitforPageLoad(driverObj);
             driverObj.Manage().Window.Maximize();
             wAction.WaitUntilElementPresent(driverObj, By.Id("registration"));

             if (switchback)
                 driverObj.SwitchTo().Window(portal);
             BaseTest.Pass();

             return portal;
         }
         public string OpenCashier(IWebDriver driverObj, bool switchBack = false)
         {

             BaseTest.AddTestCase("Open Cashier Page", "Cashier page should opened successfully");
             string url = driverObj.Url.ToLower();
             string Urlcode;
             if (url.Contains("mobenga"))
                 Urlcode = "sports";
             else
                 Urlcode = url.Substring(0, url.IndexOf("ladbrokes.com"));


             if (Urlcode.Contains("sports"))
                 // wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.xpath, "Sports_banking_lnk", "Banking Link not found", FrameGlobals.reloadTimeOut, false);
                 wAction.Click(driverObj, By.XPath(Sportsbook_Control.Deposit_XP), "Deposit link not found", FrameGlobals.reloadTimeOut, false);

             else
             {

                 //if ((!Urlcode.Contains("ecom")) && (!Urlcode.Contains("games")))
                 //    wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.id, "Menu_Btn", noWait: false);

                 if ((Urlcode.Contains("ecom")))
                     wAction._Click(driverObj, ORFile.Ladbrokes_Header, wActions.locatorType.link, "Banking_lnk", "Banking Link not found", 0, false);
                 else                 
                 wAction.Click(driverObj, By.XPath(Games_Control.depositLink), "Deposit link not found", 0, false);

                 


             }
             
             string portal = driverObj.WindowHandles.ToArray()[0].ToString();

             wAction.WaitAndMovetoPopUPWindow(driverObj, "Cashier window not opened", 20);
             wAction.WaitforPageLoad(driverObj);
             driverObj.Manage().Window.Maximize();
             wAction.WaitUntilElementPresent(driverObj, By.Id("navigation"));
             if (switchBack)
             {
                 driverObj.SwitchTo().Window(portal);
             }
             BaseTest.Pass();

             return portal;
         }
         public string OpenMyAcct(IWebDriver driverObj, bool switchback = false)
         {

             BaseTest.AddTestCase("Open My Account Page", "MyAcct page should opened successfully");
             string url = driverObj.Url.ToLower();

             wAction.Click(driverObj, By.XPath(Cashier_Control_SW.TransactionNotification_OK_Dialog));
             string Urlcode;
             if (url.Contains("mobenga"))
                 Urlcode = "sports";
             else
                 Urlcode = url.Substring(0, url.IndexOf("ladbrokes.com"));


             if (!wAction.IsElementPresent(driverObj, By.XPath(Portal_Control.myAcct_xpath)))
             {
                 if ((!Urlcode.Contains("ecom")) && (!Urlcode.Contains("games") && (!Urlcode.Contains("sports"))))
                     wAction.Click(driverObj, By.Id(Portal_Control.Customer_Menu_Id), "Menu not found", FrameGlobals.reloadTimeOut, false);

                 else
                     wAction.Click(driverObj, By.XPath(Sportsbook_Control.LoggedinMenu_XP), "Menu not found", FrameGlobals.reloadTimeOut, false);
             }

             wAction.Click(driverObj, By.XPath(Portal_Control.myAcct_xpath), "My Account link not found");

             string portal = driverObj.WindowHandles.ToArray()[0].ToString();

             wAction.WaitAndMovetoPopUPWindow(driverObj, "My Acct window not opened", 20);
             //             driverObj.SwitchTo().Window(driverObj.WindowHandles.ToArray()[1].ToString());
             wAction.WaitforPageLoad(driverObj);
             wAction.WaitUntilElementDisappears(driverObj, By.XPath(MyAcctPage.LoadingPrompt_XP));
             wAction.WaitUntilElementPresent(driverObj, By.Id(MyAcctPage.myAcct_tab_Id));

             //============Prompt================
             wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Xpath));
             System.Threading.Thread.Sleep(2000);

         clickAgain2:
             wAction.Click(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath));
             wAction.WaitforPageLoad(driverObj);
             if (wAction.IsElementPresent(driverObj, By.XPath(Login_Control.modelWindow_Decline_Xpath)))
                 goto clickAgain2;
             //====================================

             driverObj.Manage().Window.Maximize();
             if (switchback)
                 driverObj.SwitchTo().Window(portal);
             BaseTest.Pass();

             return portal;
         }

    }//class
}//namespace
