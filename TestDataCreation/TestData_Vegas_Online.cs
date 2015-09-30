using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using MbUnit.Framework;
using Ladbrokes_IMS_TestRepository;
using Selenium;
using AdminSuite;
using ICE.DataRepository.Vegas_IMS_Data;
using System.IO;
using System.Configuration;
using IMS_AdminSuite;
//using OpenQA.Selenium;
//using ICE.ActionRepository;
using ICE.DataRepository;
//using IMS_AdminSuite;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Xml;

[assembly: ParallelismLimit]
namespace TestDataCreation
{
    [TestFixture]
    public class TestData_Generator
    {
        Framework.StringCommonMethods stringComm = new StringCommonMethods();
        BaseTest BT = new BaseTest();
       // Registration_Data regData = new Registration_Data();
        Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
        readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
        IMS_AdminSuite.IMS_Base IMS = new IMS_AdminSuite.IMS_Base();
        IMS_AdminSuite.Common IMSCOmm = new IMS_AdminSuite.Common();
        BaseTest baseTest = new BaseTest();
        // Configuration testdata = TestDataInit();

       
        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_NewCustomer()
        {
            //  StringBuilder output = new StringBuilder();
           // Registration_Data regData = new Registration_Data();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.Accounts_Wallets;
         
           // Registration_Data validRegData = new Registration_Data();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                //IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "lgndata" || NodeName == "lgnsdata")
                    {
                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                            Testcomm.Createcustomer_PostMethod(ref regData);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            //IMSCOmm.CreateNewCustomer(IMS.IMSDriver, "user", baseTest.ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                        }


                    }

                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
           

            doc.Save(path);


        }//method

        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_CCCustomer()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            //Registration_Data regData = new Registration_Data();
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.Accounts_Wallets;

            
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "ccdata" || NodeName =="vccdata")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                             Registration_Data regData = new Registration_Data();
                            string CC = null;
                            //CC = BT.ReadxmlData(NodeName, "CCard", DataFilePath.Accounts_Wallets);
                            if(NodeName== "ccdata")
                            CC = createCreditCard("MasterCard").ToString(); 
                            else
                                CC = createCreditCard("Visa").ToString(); 

                            string oldUser = doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, oldUser);
                            
                             IMSCOmm.AVstatusSet(IMS.IMSDriver);
                            // IMSCOmm.AllowDuplicateCreditCard_FullNavigation_Newlook(IMS.IMSDriver, CC.Substring(0, 6), CC.Substring(CC.Length - 4, 4));

                             wActions waction = new wActions();
                          //   waction.Click(IMS.IMSDriver, By.LinkText(oldUser));
                             

                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");

                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                                
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                           
                          //  IMSCOmm.SetDepositLimit(IMS.IMSDriver, baseTest.ReadxmlData("depLimt", "Amnt", DataFilePath.Accounts_Wallets));
                            for (int search = node; (doc.SelectSingleNode(mainnode).ChildNodes[search + 1].Name == NodeName); search++)
                                if (doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["key"].Value == "CCard")
                                {

                                    IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                                    doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["value"].Value = IMSCOmm.AddCreditCard(IMS.IMSDriver, CC);
                                    waction.Click(IMS.IMSDriver, By.LinkText(regData.username));
                                }
                          
                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_LimitCustomer()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.Accounts_Wallets;

          //  Registration_Data regData = new Registration_Data();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);



                    if (NodeName == "depLimt")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                            //Registration_Data.depLimit = "200";

                            Testcomm.Createcustomer_PostMethod(ref regData, "mr", "200");
                            Console.WriteLine("Test Data for :" + NodeName);
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), "5");
                            

                        }



                    }//if
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            
            doc.Save(path);


        }//method
        [Test]
        [RepeatOnFail]
        [Timeout(3000)]
        public void TestDataGeneration_NettellerAndTransferCustomer()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.Accounts_Wallets;

          //  Registration_Data regData = new Registration_Data();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "depdata" || NodeName =="Teldata")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            IMSCOmm.AVstatusSet(IMS.IMSDriver);
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                          
                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), "3000");
                          //  IMSCOmm.AddCorrection_New(IMS.IMSDriver, "18000", "Cash Deposit","Player balance correction deposit");
                            IMSCOmm.AddCorrection_New(IMS.IMSDriver, "18000", baseTest.ReadxmlData("mc", "addBalance", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("mc", "TransactionType_Add", DataFilePath.Accounts_Wallets));
                            
                            
                                IMSCOmm.FundTransfer(IMS.IMSDriver, baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("ccdata", "depWallet_ex", DataFilePath.Accounts_Wallets), "4000");
                                IMSCOmm.FundTransfer(IMS.IMSDriver, baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("ccdata", "depWallet_g", DataFilePath.Accounts_Wallets), "4000");
                            
                         //   IMSCOmm.FundTransfer(IMS.IMSDriver, baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("depdata", "Wallet_Sports", DataFilePath.Accounts_Wallets),"4000");
                            
                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_Netteller()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.Accounts_Wallets;

           
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                   if (NodeName == "autodata" || NodeName == "betdata" || NodeName == "avdata" || NodeName == "qdepdata")
                   // if (NodeName == "avdata")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "country_UK", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "City_Web", DataFilePath.Accounts_Wallets), BT.ReadxmlData("regdata", "Password", DataFilePath.Accounts_Wallets));
                           
                            if (NodeName == "avdata")
                            regData.email = "abc@abc.com";
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            if (NodeName == "avdata")
                                IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username,false);
                            else
                                IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            
                            if(NodeName!="avdata")
                                IMSCOmm.AVstatusSet(IMS.IMSDriver);



                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                            if (NodeName == "betdata")
                                IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("depdata", "Wallet_Sports", DataFilePath.Accounts_Wallets), "3000");
                            else if (NodeName == "qdepdata")
                                IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("depdata", "Wallet_Sports", DataFilePath.Accounts_Wallets), "10");
                           
                            else
                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.Accounts_Wallets), "3000");
                           

                           

                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        /// <summary>
        /// Author : Roopa
        /// Create an AutoEvent in the open bet
        /// </summary>
        [Test]
        // [RepeatOnFail]
        [Timeout(1000)]
        [RepeatOnFail]
        public void CreateAutoEvent()
        {

            AdminTests Ob = new AdminTests();
            AdminSuite.Common comAdmin = new AdminSuite.Common();
            try
            {
               


        #region writetoXML
                    //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.Accounts_Wallets;

          //  Registration_Data regData = new Registration_Data();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            
               
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "eventdata")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "eventID")
                        {
                            Ob.Init();

                            //comAdmin.SearchCustomer("Useruolsmaha", Ob.MyBrowser);
                            string eventname = comAdmin.CreateEvent_Footbal(Ob.MyBrowser, eventName: "AutoEvt_" + StringCommonMethods.GenerateAlphabeticGUID());
                            Assert.IsTrue((eventname != null), "Event not created in the OB");
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = eventname.Split(':')[0].Trim();
                           
                            Console.WriteLine("New event: " + eventname);

                        }
                    }
                }
                doc.Save(path);
        #endregion

            }
            catch (Exception e)
            {
                //exceptionStack(e);
                // CaptureScreenshot(Ob.MyBrowser, "OB");
                throw new Exception("Event creation failed");
            }
            finally
            {
                Ob.Cleanup();
            
            }
        }


        // [Test]
        // [RepeatOnFail]
        public void New_Bonus_Creation()
        {
            StringBuilder output = new StringBuilder();
            IMS_Base admin = new IMS_Base();
            IMS_AdminSuite.Common commoniMS = new IMS_AdminSuite.Common();

            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName;

            string[] lines = System.IO.File.ReadAllLines(_testDataFilePath + "\\_output\\TestData.config");


            for (int index = 0; index < lines.LongLength; index++)
            {
                // Use a tab to indent each line of the file.
                if (lines[index].Contains("BonusID"))
                {
                    Registration_Data validRegData = new Registration_Data();
                    string name = ReadValue(lines[index]);
                    try
                    {
                        admin.Init();
                        commoniMS.CreateNewBonus(admin.IMSDriver, name, "1000");
                        BaseTest.AddTestCase("Bonus name: " + name, "");
                        BaseTest.Pass();
                    }
                    finally { admin.Quit(); }


                }//if

            }//for


        }//method

        //MasterCard,Visa
        public string createCreditCard(string type)
        {

            IWebDriver drive = new FirefoxDriver();
            string str = null;
            string finalVal = null;
            double val = 0;
            try
            {
                drive.Navigate().GoToUrl("http://www.getcreditcardnumbers.com/");

                wActions Ac = new wActions();
                Ac.SelectDropdownOption(drive, By.Id("id_card_type"), type, "Given Option " + type + " not available please re check");

                //  drive.FindElement(By.Id("id_card_type")).SendKeys("1");
                drive.FindElement(By.Id("id_no_entries")).Clear();
                drive.FindElement(By.Id("id_no_entries")).SendKeys("1");
                drive.FindElement(By.XPath("//button[@type='submit']")).Click();

                str = drive.FindElement(By.Id("textarea")).GetAttribute("value");
                string temp = str.Replace(",", string.Empty).Trim();
                Regex re = new Regex(@"\d+");
                Match Value = re.Match(temp);

                // val = double.Parse(Value.Value, System.Globalization.NumberStyles.None);
                finalVal = Value.ToString();
            }
            catch (Exception e)
            {
                BaseTest.Fail("Creation of credit card failed : " + e.Message.ToString());
            }
            finally
            {
                drive.Dispose();
            }
            return finalVal;
        }


        //        [Test]
        //        [RepeatOnFail]
        //        public void BannedCountrySet()
        //        {
        //            StringBuilder output = new StringBuilder();
        //            AdminSuite.AdminBase admin = new AdminSuite.AdminBase();
        //            AdminSuite.Common commonAdmin = new AdminSuite.Common();

        //            string _testDataFilePath = "";
        //            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName;
        //            string[] lines = System.IO.File.ReadAllLines(_testDataFilePath + "\\_output\\TestData.config");


        //            for (int index = 0; index < lines.LongLength; index++)
        //            {
        //                // Use a tab to indent each line of the file.
        //                if (lines[index].Contains("BannedCountry"))
        //                {
        //                    Registration_Data validRegData = new Registration_Data();
        //                    string name = ReadValue(lines[index]);
        //                    try
        //                    {
        //                        admin.Init();
        //                        commonAdmin.AddBannedCountryList(admin.MyBrowser, name);
        //                    }
        //                    finally { admin.Quit(); }

        //                }//if

        //            }//for



        //        }//method

        public string ReadValue(string input)
        {
            return stringComm.ReadSubString(input, "value=", "/>").Replace("\"", " ").Trim();
        }



    }//class

    [TestFixture]
    public class TestData_Generator_Ip2
    {
        Framework.StringCommonMethods stringComm = new StringCommonMethods();
        BaseTest BT = new BaseTest();
        // Registration_Data regData = new Registration_Data();
        Ladbrokes_IMS_TestRepository.Common Testcomm = new Ladbrokes_IMS_TestRepository.Common();
        readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
        IMS_AdminSuite.IMS_Base IMS = new IMS_AdminSuite.IMS_Base();
        IMS_AdminSuite.Common IMSCOmm = new IMS_AdminSuite.Common();
        BaseTest baseTest = new BaseTest();
        // Configuration testdata = TestDataInit();
        wActions wAction = new wActions();


        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_NewCustomer()
        {
             string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.IP2_Authetication;

           
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                //IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "lgndata" || NodeName == "lgnsdata" || NodeName == "ilgndata" || NodeName == "rlgndata" )
                    {
                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname",DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK",DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web",DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            //IMSCOmm.CreateNewCustomer(IMS.IMSDriver, "user", baseTest.ReadxmlData("regdata", "Password",DataFilePath.IP2_Authetication));
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                        }


                    }

                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_ClosedCustomer()
        {
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.IP2_Authetication;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                //IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "clgndata")
                    {
                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData);
                            IMS.Init();
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            wAction.Click(IMS.IMSDriver, By.Id("close"), "Close button not found", 0, false);
                            wAction.WaitAndAccepAlert(IMS.IMSDriver);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            //IMSCOmm.CreateNewCustomer(IMS.IMSDriver, "user", baseTest.ReadxmlData("regdata", "Password",DataFilePath.IP2_Authetication));
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);

                        }


                    }

                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method
       
        
        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_CCCustomer()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            //Registration_Data regData = new Registration_Data();
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath +DataFilePath.IP2_Authetication;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "vccdata" || NodeName == "qbetdata")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            string CC = null;
                            //CC = BT.ReadxmlData(NodeName, "CCard",DataFilePath.IP2_Authetication);
                            if (NodeName == "ccdata")
                                CC = createCreditCard("MasterCard").ToString();
                            else
                                CC = createCreditCard("Visa").ToString();

                            string oldUser = doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, oldUser);

                            IMSCOmm.AVstatusSet(IMS.IMSDriver);
                            // IMSCOmm.AllowDuplicateCreditCard_FullNavigation_Newlook(IMS.IMSDriver, CC.Substring(0, 6), CC.Substring(CC.Length - 4, 4));

                            wActions waction = new wActions();
                            //   waction.Click(IMS.IMSDriver, By.LinkText(oldUser));


                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname",DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK",DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web",DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");

                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;

                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);

                            //  IMSCOmm.SetDepositLimit(IMS.IMSDriver, baseTest.ReadxmlData("depLimt", "Amnt",DataFilePath.IP2_Authetication));
                            for (int search = node; (doc.SelectSingleNode(mainnode).ChildNodes[search + 1].Name == NodeName); search++)
                                if (doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["key"].Value == "card")
                                {

                                    IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                                    doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["value"].Value = IMSCOmm.AddCreditCard(IMS.IMSDriver, CC);
                                    waction.Click(IMS.IMSDriver, By.LinkText(regData.username));
                                }

                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_LimitCustomer()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath +DataFilePath.IP2_Authetication;

            //  Registration_Data regData = new Registration_Data();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);



                    if (NodeName == "depLimt")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            //Registration_Data.depLimit = "200";

                            Testcomm.Createcustomer_PostMethod(ref regData, "mr", "200");
                            Console.WriteLine("Test Data for :" + NodeName);
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), "5");


                        }



                    }//if
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }
            doc.Save(path);


        }//method
      
        [Test]
        [RepeatOnFail]
        [Timeout(3000)]
        public void TestDataGeneration_NettellerAndTransferCustomer()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath +DataFilePath.IP2_Authetication;

            //  Registration_Data regData = new Registration_Data();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "depdata" || NodeName == "Teldata")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK",DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web",DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            IMSCOmm.AVstatusSet(IMS.IMSDriver);
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);

                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id",DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "depWallet",DataFilePath.IP2_Authetication), "3000");
                           // IMSCOmm.AddCorrection_New(IMS.IMSDriver, "18000", baseTest.ReadxmlData("mc", "addBalance", DataFilePath.IP2_SeamlessWallet), "Player balance correction deposit");
                            IMSCOmm.AddCorrection_New(IMS.IMSDriver, "18000", baseTest.ReadxmlData("mc", "addBalance", DataFilePath.Accounts_Wallets), baseTest.ReadxmlData("mc", "TransactionType_Add", DataFilePath.Accounts_Wallets));

                            //   IMSCOmm.FundTransfer(IMS.IMSDriver, baseTest.ReadxmlData("depdata", "depWallet",DataFilePath.IP2_Authetication), baseTest.ReadxmlData("ccdata", "depWallet_ex",DataFilePath.IP2_Authetication),"4000");
                            IMSCOmm.FundTransfer(IMS.IMSDriver, baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "toWallet", DataFilePath.IP2_Authetication), "4000");
                           // IMSCOmm.FundTransfer(IMS.IMSDriver, baseTest.ReadxmlData("depdata", "depWallet",DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "Wallet_Sports",DataFilePath.IP2_Authetication), "4000");

                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_Netteller()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath +DataFilePath.IP2_Authetication;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "swdata" || NodeName == "rdepdata" )
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            if (NodeName == "avdata")
                                IMSCOmm.AVstatusSet(IMS.IMSDriver, "Failed");
                            else
                                IMSCOmm.AVstatusSet(IMS.IMSDriver);



                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                           
                                IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id",DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "depWallet",DataFilePath.IP2_Authetication), "3000");




                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_Netteller_USD()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.IP2_Authetication;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "sw2data")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");
                            regData.currency = "USD";
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            if (NodeName == "avdata")
                                IMSCOmm.AVstatusSet(IMS.IMSDriver, "Failed");
                            else
                                IMSCOmm.AVstatusSet(IMS.IMSDriver);



                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);

                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), "3000");




                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_Netteller_SW()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.IP2_SeamlessWallet;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "event")
                    {
                        string key = doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value;
                        
                        if ( key== "user" || key=="Tele_user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            //if (NodeName == "avdata")
                            //    IMSCOmm.AVstatusSet(IMS.IMSDriver, "Failed");
                            //else
                                IMSCOmm.AVstatusSet(IMS.IMSDriver);



                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);

                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), "9999");




                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method



        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void TestDataGeneration_SW_EventCreation()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.IP2_SeamlessWallet;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";

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


            try
            {
                adminBase.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "event")
                    {
                        string key =  doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value;
                        if (key == "handicap_eID")
                        {
                          
                            //  adminComm.CreateEvent_Horse(adminBase.MyBrowser, "EachWay");
                            string eventID = adminComm.CreateEvent_Footbal(adminBase.MyBrowser, eventName: "AutoEvt_" + StringCommonMethods.GenerateAlphabeticGUID(), HandiCap: true);
                            Console.WriteLine(key + " :" + eventID);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = eventID.Split(':')[1].Trim();
                            

                            for (int search = node; (doc.SelectSingleNode(mainnode).ChildNodes[search + 1].Name == NodeName); search++)
                                if (doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["key"].Value == "handicap_ename")
                                    doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["value"].Value = eventID.Split(':')[2].Trim();



                        }
                        else if (key == "BIR_eID")
                        {

                            //  adminComm.CreateEvent_Horse(adminBase.MyBrowser, "EachWay");
                            string eventID = adminComm.CreateEvent_Footbal(adminBase.MyBrowser, eventName: "AutoEvt_" + StringCommonMethods.GenerateAlphabeticGUID(), BIR: true);
                            Console.WriteLine(key + " :" + eventID);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = eventID.Split(':')[1].Trim();
                      
                            for (int search = node; (doc.SelectSingleNode(mainnode).ChildNodes[search + 1].Name == NodeName); search++)
                                if (doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["key"].Value == "BIR_name")
                                     doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["value"].Value = eventID.Split(':')[2].Trim();


                        }
                        else if (key == "tri_eID" || key == "Acc_eID" || key =="pat_eID")
                        {

                            //  adminComm.CreateEvent_Horse(adminBase.MyBrowser, "EachWay");
                            string eventID = adminComm.CreateEvent_Footbal(adminBase.MyBrowser, eventName: "AutoEvt_" + StringCommonMethods.GenerateAlphabeticGUID());
                            Console.WriteLine(key + " :" + eventID);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = eventID.Split(':')[1].Trim();

                            for (int search = node; (doc.SelectSingleNode(mainnode).ChildNodes[search + 1].Name == NodeName); search++)
                                if (doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["key"].Value == "tri_name")
                                    doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["value"].Value = eventID.Split(':')[2].Trim();

                        }
                        else if (key == "EW_eID")
                        {
                            string eventID = adminComm.CreateEvent_Horse(adminBase.MyBrowser, "EachWay");
                            //string eventID = adminComm.CreateEvent_Footbal(adminBase.MyBrowser, eventName: "AutoEvt_" + StringCommonMethods.GenerateAlphabeticGUID(), HandiCap: true);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = eventID.Split(':')[1].Trim();
                            Console.WriteLine(key + " :" + eventID);

                            for (int search = node; (doc.SelectSingleNode(mainnode).ChildNodes[search + 1].Name == NodeName); search++)
                                if (doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["key"].Value == "EW_name")
                                    doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["value"].Value = eventID.Split(':')[0].Trim();

                        }
                        else if (key == "yank_eID" || key == "luck_eID" || key == "Can_eID" || key == "l63_eID")
                        {
                            string eventID = adminComm.CreateEvent_Horse(adminBase.MyBrowser);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = eventID.Split(':')[1].Trim();
                            string name = "l63_name";
                            Console.WriteLine(key + " :" + eventID);
                            if (key == "yank_eID")
                                name = "yank_name";
                            else if (key == "luck_eID")
                                name = "luck_name";
                            else if (key == "Can_eID")
                                name = "Can_name";
                            else if (key == "l63_eID")
                                name = "l63_name";

                            for (int search = node; (doc.SelectSingleNode(mainnode).ChildNodes[search + 1].Name == NodeName); search++)
                                if (doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["key"].Value == name )
                                    doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["value"].Value = eventID.Split(':')[0].Trim();

                        }
                        else if (key  == "Forecast_eID")
                        {
                            string eventID = adminComm.CreateEvent_Horse(adminBase.MyBrowser, "Forecast/Tricast");
                            Console.WriteLine(key + " :" + eventID);
                            //string eventID = adminComm.CreateEvent_Footbal(adminBase.MyBrowser, eventName: "AutoEvt_" + StringCommonMethods.GenerateAlphabeticGUID(), HandiCap: true);
                            string[] test = eventID.Split(':').ToArray();
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = eventID.Split(':')[1].Trim();


                            for (int search = node; (doc.SelectSingleNode(mainnode).ChildNodes[search + 1].Name == NodeName); search++)
                                if (doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["key"].Value == "Forecast_name")
                                    doc.SelectSingleNode(mainnode).ChildNodes[search].Attributes["value"].Value = eventID.Split(':')[0].Trim();

                        }

                    }
                }//for
            }//try
            catch (Exception e) { doc.Save(path); 
                BaseTest.Fail("Test Data Generation Failed"); }
            finally { adminBase.Cleanup();
            doc.Save(path);
            }

        


        }//method


        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void SW_TestDataGeneration_Netteller()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.IP2_SeamlessWallet;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "event" || NodeName == "depdata" || NodeName=="lgnBdata")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            if (NodeName == "avdata")
                                IMSCOmm.AVstatusSet(IMS.IMSDriver, "Failed");
                            else
                                IMSCOmm.AVstatusSet(IMS.IMSDriver);



                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);

                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), "3000");




                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method


        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void SW_TestDataGeneration_Netteller_EUR()
        {
            //  StringBuilder output = new StringBuilder();
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.IP2_SeamlessWallet;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                IMS.Init();
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "event")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user_EUR")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData, "Mr", "10000");
                            regData.currency = "EUR";
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            IMSCOmm.SearchCustomer_Newlook(IMS.IMSDriver, regData.username);
                            if (NodeName == "avdata")
                                IMSCOmm.AVstatusSet(IMS.IMSDriver, "Failed");
                            else
                                IMSCOmm.AVstatusSet(IMS.IMSDriver);



                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);

                            IMSCOmm.AddNetteller(IMS.IMSDriver, baseTest.ReadxmlData("netdata", "account_id", DataFilePath.IP2_Authetication), baseTest.ReadxmlData("depdata", "depWallet", DataFilePath.IP2_Authetication), "3000");




                        }

                    }
                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method

        //[Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void SW_TestDataGeneration_NewCustomer()
        {
            string _testDataFilePath = "";
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + DataFilePath.IP2_SeamlessWallet;


            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            string mainnode = "/controls";
            try
            {
                
                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "lgnBdata")
                    {
                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "user")
                        {
                            Registration_Data regData = new Registration_Data();
                            Console.WriteLine("Test Data for :" + NodeName);
                            regData.update_Registration_Data(BT.ReadxmlData("regdata", "Fname", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "country_UK", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "City_Web", DataFilePath.IP2_Authetication), BT.ReadxmlData("regdata", "Password", DataFilePath.IP2_Authetication));
                            Testcomm.Createcustomer_PostMethod(ref regData);
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = regData.username;
                            Console.WriteLine("Customer Name :" + doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value);
                        }


                    }

                }//for
            }//try
            catch (Exception e) { BaseTest.Fail("Test Data Generation Failed"); }
            finally { IMS.Quit(); }

            doc.Save(path);


        }//method


        [Test]
        [Timeout(3000)]
        [RepeatOnFail]
        public void CreateEvent_Single()
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
            adminBase.Init();
         // Console.WriteLine(   adminComm.CreateEvent_Horse(adminBase.MyBrowser, "Single"));
          Console.WriteLine(  adminComm.CreateEvent_Footbal(adminBase.MyBrowser,eventName: "AutoEvt_" + StringCommonMethods.GenerateAlphabeticGUID(),BIR:true));
            adminBase.Cleanup();
        }



        /// <summary>
        /// Author : Roopa
        /// Create an AutoEvent in the open bet
        /// </summary>
       // [Test]
        // [RepeatOnFail]
        [Timeout(1000)]
        [RepeatOnFail]
        public void CreateAutoEvent()
        {

            AdminTests Ob = new AdminTests();
            AdminSuite.Common comAdmin = new AdminSuite.Common();
            try
            {



                #region writetoXML
                //  StringBuilder output = new StringBuilder();
                string _testDataFilePath = "";
                if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
                string path = _testDataFilePath + DataFilePath.Accounts_Wallets;

                //  Registration_Data regData = new Registration_Data();
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                string mainnode = "/controls";


                for (int node = 0; node < doc.SelectSingleNode(mainnode).ChildNodes.Count; node++)
                {
                    String NodeName = (doc.SelectSingleNode(mainnode).ChildNodes[node].Name);

                    if (NodeName == "eventdata")
                    {

                        if (doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["key"].Value == "eventID")
                        {
                            Ob.Init();

                            //comAdmin.SearchCustomer("Useruolsmaha", Ob.MyBrowser);
                            string eventname = comAdmin.CreateEvent_Footbal(Ob.MyBrowser, eventName: "AutoEvt_" + StringCommonMethods.GenerateAlphabeticGUID());
                            Assert.IsTrue((eventname != null), "Event not created in the OB");
                            doc.SelectSingleNode(mainnode).ChildNodes[node].Attributes["value"].Value = eventname.Split(':')[0].Trim();

                            Console.WriteLine("New event: " + eventname);

                        }
                    }
                }
                doc.Save(path);
                #endregion

            }
            catch (Exception e)
            {
                //exceptionStack(e);
                // CaptureScreenshot(Ob.MyBrowser, "OB");
                throw new Exception("Event creation failed");
            }
            finally
            {
                Ob.Cleanup();

            }
        }




        //MasterCard,Visa
        public string createCreditCard(string type)
        {

            IWebDriver drive = new FirefoxDriver();
            string str = null;
            string finalVal = null;
            double val = 0;
            try
            {
                drive.Navigate().GoToUrl("http://www.getcreditcardnumbers.com/");

                wActions Ac = new wActions();
                Ac.SelectDropdownOption(drive, By.Id("id_card_type"), type, "Given Option " + type + " not available please re check");

                //  drive.FindElement(By.Id("id_card_type")).SendKeys("1");
                drive.FindElement(By.Id("id_no_entries")).Clear();
                drive.FindElement(By.Id("id_no_entries")).SendKeys("1");
                drive.FindElement(By.XPath("//button[@type='submit']")).Click();

                str = drive.FindElement(By.Id("textarea")).GetAttribute("value");
                string temp = str.Replace(",", string.Empty).Trim();
                Regex re = new Regex(@"\d+");
                Match Value = re.Match(temp);

                // val = double.Parse(Value.Value, System.Globalization.NumberStyles.None);
                finalVal = Value.ToString();
            }
            catch (Exception e)
            {
                BaseTest.Fail("Creation of credit card failed : " + e.Message.ToString());
            }
            finally
            {
                drive.Dispose();
            }
            return finalVal;
        }


        //        [Test]
        //        [RepeatOnFail]
        //        public void BannedCountrySet()
        //        {
        //            StringBuilder output = new StringBuilder();
        //            AdminSuite.AdminBase admin = new AdminSuite.AdminBase();
        //            AdminSuite.Common commonAdmin = new AdminSuite.Common();

        //            string _testDataFilePath = "";
        //            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName;
        //            string[] lines = System.IO.File.ReadAllLines(_testDataFilePath + "\\_output\\TestData.config");


        //            for (int index = 0; index < lines.LongLength; index++)
        //            {
        //                // Use a tab to indent each line of the file.
        //                if (lines[index].Contains("BannedCountry"))
        //                {
        //                    Registration_Data validRegData = new Registration_Data();
        //                    string name = ReadValue(lines[index]);
        //                    try
        //                    {
        //                        admin.Init();
        //                        commonAdmin.AddBannedCountryList(admin.MyBrowser, name);
        //                    }
        //                    finally { admin.Quit(); }

        //                }//if

        //            }//for



        //        }//method

        public string ReadValue(string input)
        {
            return stringComm.ReadSubString(input, "value=", "/>").Replace("\"", " ").Trim();
        }



    }//class
}//namespace
