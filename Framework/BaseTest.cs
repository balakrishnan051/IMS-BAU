using System;
using System.Text;
using System.Collections;
using MbUnit.Framework;
using System.Globalization;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Threading;
using Gallio;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using Gallio.Framework.Pattern;
//using ICE.ActionRepository;
using Google.GData.Spreadsheets;

namespace Framework
{

    /// <summary>
    /// Author:Nagamanickam
    /// Enable Screenshot for Grid
    /// </summary>
    public class SnapShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public SnapShotRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, desiredCapabilities, commandTimeout)
        {
        }

        public Screenshot GetScreenshot()
        {
            // Get the screenshot as base64. 
            Response screenshotResponse =
            this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();

            // ... and convert it. 
            return new Screenshot(base64);
        }
    }



    [Factory("BeforeRunAssembly")]
    public class BaseTest
    {
        #region Declaration
        public static string className = null;
        public static string suiteName = null;
        readonly DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
        private static readonly log4net.ILog OutputLog = log4net.LogManager.GetLogger(typeof(BaseTest));
        public static Dictionary<string, string> excelComment = new Dictionary<string, string>();
        private static bool killTestOnNextMove = false;
        public ISelenium MyBrowser, RemBroswer;
        public static string FFversion, chromeversion, ieVersion;
        public IWebDriver ffDriver;
        public IWebDriver chromeDriver;
        public IWebDriver WebDriverObj, RemDriverObj;
        DesiredCapabilities desriredCapibilities = null;
        public static Dictionary<string, ISelenium> _seleniumContainer = new Dictionary<string, ISelenium>();
        public static Dictionary<string, Stack> testCaseKey = new Dictionary<string, Stack>();
        Framework.Common.Common commObj = new Framework.Common.Common();
        public static int init = 0;
        public static Dictionary<string, Queue> testStatus = new Dictionary<string, Queue>();
        public static Dictionary<string, string> thirdStatus = new Dictionary<string, string>();
        public static Dictionary<string, string> FailureReasons = new Dictionary<string, string>();
        public static Dictionary<string, string> Usernames = new Dictionary<string, string>();
        public static Dictionary<string, ISelenium> SeleniumContainer
        {
            get { return _seleniumContainer; }
        }

        #region tobeDeleted
        /// <summary>
        /// Setting this value to anything else besides null will cause this to be
        /// the HTML of the page seen in the logs. This is designed for web services
        /// or feeds that may not actually need the browser
        /// </summary>
        public static string CustomHTMLOfThePage = null;
        private static TimeSpan _maxTs = new TimeSpan(0, 2, 0);
        #endregion

        #endregion


        
        //  [FixtureTearDown]
        public static void EndOfExecution()
        {
          
            if (FrameGlobals.Mode != "Maintenance")
            {
                #region killBro
                Process[] process = null;
                if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                {
                    process = Process.GetProcessesByName("iedriverserver");
                    foreach (Process p in process)
                    {
                        p.Kill();
                    }
                    process = Process.GetProcessesByName("iexplore");

                    foreach (Process p in process)
                    {
                        p.Kill();
                    }
                }
                if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                {
                    process = Process.GetProcessesByName("chromedriver");
                    foreach (Process p in process)
                    {
                        p.Kill();
                    }
                    process = Process.GetProcessesByName("chrome");

                    foreach (Process p in process)
                    {
                        p.Kill();
                    }


                }

                process = Process.GetProcessesByName("firefox");
                foreach (Process p in process)
                {
                    p.Kill();
                }
                #endregion
            }
     
            #region DecExcel
            if (Gallio.Framework.TestContext.CurrentContext.Test.Parent.ToString().Contains("Framework"))
                return;
            Application Exl_App = new Application();
            Workbook ResultSheet = Exl_App.Workbooks.Add(1);
            Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)ResultSheet.Sheets[1];

            Exl_App.DisplayAlerts = false;
            #endregion

            #region Filldata
            worksheet.Cells[1, 1] = "Module/Class Name";
            worksheet.Cells[1, 2] = "Test Name";
            worksheet.Cells[1, 3] = "Status";
            worksheet.Cells[1, 4] = "TestData/Comments";


            Microsoft.Office.Interop.Excel.Range workSheet_range = worksheet.get_Range("A1", "D1");
            workSheet_range.Interior.Color = System.Drawing.Color.LightGray.ToArgb();
            workSheet_range.Font.Bold = true;


            int row = 2;
            foreach (KeyValuePair<string, Queue> kvp in testStatus)
            {
                if (suiteName.Contains("Regression_AnW_Suite1"))
                    className = "AccountsAndWallets1";
                else if (suiteName.Contains("Regression_AnW_Suite2"))
                    className = "AccountsAndWallets2";
                else if (suiteName.Contains("BVT_AnW_General_Suite1"))
                    className = "CountryRegistration";
                else if (suiteName.Contains("Non IMS Suite"))
                    className = "Non-IMS Suite";
                else if (suiteName.Contains("BVT_IP2"))
                    className = "BVT_IP2";
                else if (suiteName.Contains("Regression_IP2"))
                    className = "IP2_Authentication";
                else if (suiteName.Contains("BVT_BAUSuite"))
                    className = "BVT_BAUSuite";
                else if (suiteName.Contains("BVT_BAU"))
                    className = "BVT_BAU";
                else if (suiteName.Contains("IP2_Restricted_IP"))
                    className = "RestrictedTerritory";
                

                Queue s = kvp.Value;
                foreach (TestDetails tc in s)
                {
                    // TestDetails tc = (TestDetails)s.Pop();

                    //  className = kvp.Key.Replace("[Fixture]", "");
                    worksheet.Cells[row, 1] = kvp.Key.Replace("[Fixture]", "");
                    worksheet.Cells[row, 2] = tc.Name;
                    worksheet.Cells[row, 3] = tc.Status;
                    if (excelComment.ContainsKey(tc.Name))
                        worksheet.Cells[row, 4] = excelComment[tc.Name].ToString().Trim();

                    workSheet_range = (Range)worksheet.Cells[row, 3];
                    if (tc.Status == "Pass")
                        workSheet_range.Interior.Color = System.Drawing.Color.LightGreen.ToArgb();
                    else
                    {
                        workSheet_range.Interior.Color = System.Drawing.Color.LightYellow.ToArgb();
                        workSheet_range.Font.Color = System.Drawing.Color.Red.ToArgb();
                    }
                    row++;
                }

            }

            worksheet.Cells[3, 6] = "Total Run";
            worksheet.Cells[4, 6] = "Total Passed";
            worksheet.Cells[5, 6] = "Total Failed";
            worksheet.Cells[6, 6] = "Total Skipped";
            worksheet.Cells[3, 7] = "=SUM(G4:G6)";
            worksheet.Cells[4, 7] = "=COUNTIF(C:C,\"Pass\")";
            worksheet.Cells[5, 7] = "=COUNTIF(C:C,\"Fail\")";
            worksheet.Cells[6, 7] = "=COUNTIF(C:C,\"Not Run\")";
            workSheet_range = worksheet.get_Range("G3", "G6");
            workSheet_range.Interior.Color = System.Drawing.Color.LightSteelBlue.ToArgb();
            workSheet_range = worksheet.get_Range("F3", "F6");
            workSheet_range.Font.Bold = true;

            worksheet.Cells[3, 9] = "Browser Version";
            worksheet.Cells[4, 9] = "Device Version";

            // if (FrameGlobals.userAgent.ToUpper() != ("NONE"))
            worksheet.Cells[4, 10] = FrameGlobals.userAgent;

            if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                worksheet.Cells[3, 10] = "Chrome v" + chromeversion;
            else if (FrameGlobals.BrowserToLoad == BrowserTypes.Firefox)
                worksheet.Cells[3, 10] = "Firefox v" + FFversion;
            else if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                worksheet.Cells[3, 10] = "InternetExp v" + ieVersion;







            workSheet_range = worksheet.get_Range("A:A", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 35;
            workSheet_range = worksheet.get_Range("B:B", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 50;
            workSheet_range = worksheet.get_Range("C:C", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 8;
            workSheet_range = worksheet.get_Range("D:D", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 35;
            workSheet_range.Rows.AutoFit();


            workSheet_range = worksheet.get_Range("F:F", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 15;
            workSheet_range = worksheet.get_Range("G:G", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 5;

            workSheet_range = worksheet.get_Range("I:I", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 22;
            workSheet_range = worksheet.get_Range("J:J", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 30;

            workSheet_range = worksheet.get_Range("A:A", System.Type.Missing);
            Microsoft.Office.Interop.Excel.Borders border = workSheet_range.Borders;
            border.LineStyle =
               Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            workSheet_range = worksheet.get_Range("B:B", System.Type.Missing);
            border = workSheet_range.Borders;
            border.LineStyle =
               Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            workSheet_range = worksheet.get_Range("C:C", System.Type.Missing);
            border = workSheet_range.Borders;
            border.LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            workSheet_range = worksheet.get_Range("D:D", System.Type.Missing);
            border = workSheet_range.Borders;
            border.LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


            workSheet_range = worksheet.get_Range("F3:G6", System.Type.Missing);
            border = workSheet_range.Borders;
            border.LineStyle =
               Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            workSheet_range = worksheet.get_Range("I3:J4", System.Type.Missing);
            border = workSheet_range.Borders;
            border.LineStyle =
               Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            workSheet_range = worksheet.get_Range("I3:I4", System.Type.Missing);
            workSheet_range.Font.Bold = true;

            DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
            string resultFilePath = ""; if (_currentDirPath.Parent != null) resultFilePath = _currentDirPath.Parent.FullName;
            //string Bro = "Unknown";
            //if (FrameGlobals.userAgent.ToUpper() != ("NONE"))
            //    Bro = "Mobile_" + FrameGlobals.userAgent;
            //else
            //{
            //    if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
            //        Bro = "Chrome v" + chromeversion;
            //    else if (FrameGlobals.BrowserToLoad == BrowserTypes.Firefox)
            //        Bro = "Firefox v" + FFversion;
            //    else if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
            //        Bro = "InternetExp v" + ieVersion;
            //}

          //  Bro = className;
            #endregion


            DateTime dateFormat = DateTime.Now;
            string dateTime = (dateFormat.ToString("yyyyMMddHHmm"));

            if (!Directory.Exists(resultFilePath + "\\Result"))
                Directory.CreateDirectory(resultFilePath + "\\Result");


            resultFilePath = resultFilePath + "\\Result\\" + className + "_Result_" + dateTime + ".xls";
            ResultSheet.SaveAs(resultFilePath);
            ResultSheet.Close();
          

            Exl_App.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Exl_App);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ResultSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);

            uint col = 2;
            if (FrameGlobals.UseEmail.ToUpper().Contains("YES"))
            {
                SendMail(resultFilePath);


            }
            if (FrameGlobals.Mode != "Maintenance")
            {
                if (suiteName.Contains("Regression_AnW_Suite2"))
                {
                    col = 2;
                    UpdateGDoc("A&W_Regression_Suite_Base", "Auto Result", testStatus, col, FrameGlobals.Mode);
                }
                else if (suiteName.Contains("BVT_AnW_General_Suite1"))
                {
                    col = 6;
                    UpdateGDoc("A&W_Regression_Suite_Base", "Auto Result", testStatus, col, FrameGlobals.Mode);
                }
                else if (suiteName.Contains("Non IMS Suite"))
                {
                    col = 2;
                    UpdateGDoc("Non-IMS Product Regression Suite_Base", "Auto Result", testStatus, col, FrameGlobals.Mode);
                }
                
            }

        }//end Clear
        [SetUp]
        public virtual void BeforeTest()
        {


            #region TCmiss
            int i = 5;
        ab:
            if ((Gallio.Framework.TestContext.CurrentContext.Test.Name == null) || (Gallio.Framework.TestContext.CurrentContext.Test.Name == ""))
            {
                if (i-- != 0)
                {

                    goto ab;
                }

            }
            #endregion

            #region DelayBrowserInitPerParallelism
            if (FrameGlobals.parallelismCount > 1)
            {
                if (init > 5)
                    init = 1;
                else
                    init++;
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(init));
            }
            #endregion
            suiteName = Gallio.Framework.TestContext.CurrentContext.Test.Parent.Parent.ToString();

            if (_seleniumContainer.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                _seleniumContainer.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);

            //  AddTestCase("Open Vegas base site", "Site should load successfully");
            if (BaseTest.killTestOnNextMove)
            {
                throw new ApplicationException("The test was asked to be stopped.");
            }
            try
            {

                if (FrameGlobals.BrowserToLoad == BrowserTypes.Firefox)
                {
                    if (FrameGlobals.useGrid.ToUpper() == "NO")
                    {
                        //Creating a browser profile and setting the required user agent.
                        FirefoxProfile ffProfile = new FirefoxProfile();
                        if (FrameGlobals.UseAgent == "Yes")
                        {
                            ffProfile.SetPreference("general.useragent.override", FrameGlobals.userAgentValue);
                        }

                        //Creating a webdriver with above created firefox profile
                        WebDriverObj = new FirefoxDriver(ffProfile);
                        //Backing the selenium with webdriver
                        MyBrowser = new WebDriverBackedSelenium(WebDriverObj, FrameGlobals.BaseUrl1);

                        

                        WebDriverObj.Manage().Window.Maximize();
                        ICapabilities capabilities = ((RemoteWebDriver)WebDriverObj).Capabilities;
                        FFversion = capabilities.Version;
                    }
                    else
                    {

                        FirefoxProfile ffProfile = new FirefoxProfile();
                        if (FrameGlobals.UseAgent == "Yes")
                        {
                            ffProfile.SetPreference("general.useragent.override", FrameGlobals.userAgentValue);
                        }
                        desriredCapibilities = new DesiredCapabilities();
                        desriredCapibilities = DesiredCapabilities.Firefox();
                        desriredCapibilities.SetCapability(CapabilityType.BrowserName, "firefox");

                        RemDriverObj = new SnapShotRemoteWebDriver(new Uri("http://localhost:4444/wd/hub"),
                        desriredCapibilities, TimeSpan.FromSeconds(920.0));
                        RemBroswer = new WebDriverBackedSelenium(RemDriverObj, "http://www.google.com");
                        _seleniumContainer.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, RemBroswer);
                        ICapabilities capabilities = ((RemoteWebDriver)RemDriverObj).Capabilities;
                        FFversion = capabilities.Version;
                    }


                    Console.WriteLine("Browser type chosen: FireFox " + FFversion);


                }

                else if (FrameGlobals.BrowserToLoad == BrowserTypes.InternetExplorer)
                {


                    if (FrameGlobals.useGrid.ToUpper() == "NO")
                    {
                        InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                        ieOptions.AddAdditionalCapability("javascriptEnabled", true);
                        ieOptions.AddAdditionalCapability("applicationCacheEnabled", true);
                        ieOptions.EnableNativeEvents = false;
                        ieOptions.IgnoreZoomLevel = true;

                        ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                        //Clean the session before launching the browser
                        ieOptions.EnsureCleanSession = true;

                        //ieOptions.RequireWindowFocus = true;


                        //ieOptions.AddAdditionalCapability("requireWindowFocus", true);
                        //ieOptions.AddAdditionalCapability("platform", "ANY");
                        //Deleting cookies in ie browser through command line.
                        //var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 2");
                        // var proc = new System.Diagnostics.Process { StartInfo = procStartInfo };
                        // proc.Start();
                        WebDriverObj = new InternetExplorerDriver(ieOptions);
                        MyBrowser = new WebDriverBackedSelenium(WebDriverObj, FrameGlobals.BaseUrl1);
                        ICapabilities capabilities = ((RemoteWebDriver)WebDriverObj).Capabilities;
                        ieVersion = capabilities.Version;

                     
                    }
                    else
                    {

                        desriredCapibilities = new DesiredCapabilities();
                        desriredCapibilities = DesiredCapabilities.InternetExplorer();
                        desriredCapibilities.SetCapability("browserName", "iexplore");
                        desriredCapibilities.SetCapability("javascriptEnabled", true);




                        RemDriverObj = new SnapShotRemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), desriredCapibilities, TimeSpan.FromSeconds(90.0));
                        RemBroswer = new WebDriverBackedSelenium(RemDriverObj, "http://www.google.com");
                        _seleniumContainer.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, RemBroswer);
                        ICapabilities capabilities = ((RemoteWebDriver)RemDriverObj).Capabilities;
                        ieVersion = capabilities.Version;
                    }

                    Console.WriteLine("Browser type chosen: Internet Explorer " + ieVersion);
                }

                else if (FrameGlobals.BrowserToLoad == BrowserTypes.Chrome)
                {

                    if (FrameGlobals.useGrid.ToUpper() == "NO")
                    {
                        ChromeOptions chromeCapabilities = new ChromeOptions();

                        if (FrameGlobals.UseAgent == "Yes")
                        {
                            //Adding certain capablilities like maximize the window, ignore untrusted certificates, disable popup blocking...
                            var arr = new string[7] { "--start-maximized", "--ignore-certificate-errors", "--disable-popup-blocking", "--disable-default-apps", "--auto-launch-at-startup", "--always-authorize-plugins", "--user-agent= " + FrameGlobals.userAgentValue };
                            chromeCapabilities.AddArguments(arr);
                            Console.WriteLine("\n  Device type chosen >>> : " + FrameGlobals.userAgent);
                        }



                        else
                        {
                            var arr = new string[6] { "--start-maximized", "--ignore-certificate-errors", "--disable-popup-blocking", "--disable-default-apps", "--auto-launch-at-startup", "--always-authorize-plugins" };
                            chromeCapabilities.AddArguments(arr);

                        }
                        WebDriverObj = new ChromeDriver(chromeCapabilities);
                        ICapabilities capabilities = ((RemoteWebDriver)WebDriverObj).Capabilities;
                        chromeversion = capabilities.Version;
                        Console.WriteLine("Browser type chosen: Chrome " + chromeversion);

                        MyBrowser = new WebDriverBackedSelenium(WebDriverObj, FrameGlobals.BaseUrl1);

                    }
                    else
                    {
                        desriredCapibilities = new DesiredCapabilities();
                        ChromeOptions options = new ChromeOptions();
                        if (FrameGlobals.UseAgent == "Yes")
                        {
                            //Adding certain capablilities like maximize the window, ignore untrusted certificates, disable popup blocking...
                            var arr = new string[7] { "--start-maximized", "--ignore-certificate-errors", "--disable-popup-blocking", "--disable-default-apps", "--auto-launch-at-startup", "--always-authorize-plugins", "--user-agent= " + FrameGlobals.userAgentValue };
                            options.AddArguments(arr);
                            Console.WriteLine("\n  Device type chosen >>> : " + FrameGlobals.userAgent);
                        }
                        else
                        {
                            var arr = new string[6] { "--start-maximized", "--ignore-certificate-errors", "--disable-popup-blocking", "--disable-default-apps", "--auto-launch-at-startup", "--always-authorize-plugins" };
                            options.AddArguments(arr);

                        }

                        //ChromeOptions options = new ChromeOptions();
                        //options.AddArgument("--start-maximized");
                        desriredCapibilities = DesiredCapabilities.Chrome();



                        desriredCapibilities.SetCapability(ChromeOptions.Capability, options);
                        RemDriverObj = new SnapShotRemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), desriredCapibilities, TimeSpan.FromSeconds(300.0));

                        RemBroswer = new WebDriverBackedSelenium(RemDriverObj, "http://www.google.com");
                        _seleniumContainer.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, RemBroswer);

                        ICapabilities capabilities = ((RemoteWebDriver)RemDriverObj).Capabilities;
                        chromeversion = capabilities.Version;
                        Console.WriteLine("Browser type chosen: Chrome " + chromeversion);

                    }


                }

                if (FrameGlobals.useGrid.ToUpper() == "NO")
                {
                    // Console.WriteLine(Gallio.Framework.TestContext.CurrentContext.Test.Name);
                    _seleniumContainer.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, MyBrowser);

                    #region TBD
                    //MyBrowser.Start();

                    //Pass("Browser opened successfully.");
                    //AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
                    //try
                    //{
                    //    // Clear cahce and cookies before the test run begin
                    //    MyBrowser.DeleteAllVisibleCookies();
                    //}
                    //catch (Exception Ex)
                    //{
                    //    Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
                    //}

                    ////iBrowser.Open(FrameGlobals.BaseUrl1);
                    //WebCommonMethods wb = new WebCommonMethods();
                    //MyBrowser.Open(FrameGlobals.BaseUrl1);

                    //MyBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

                    //bool s = MyBrowser.IsElementPresent("id('column-6')");
                    //IWebDriver dr = ((WebDriverBackedSelenium)MyBrowser).UnderlyingWebDriver;
                    //s=wb.IsElementPresent(dr, By.XPath("id('column-6')"));

                    #endregion
                    #region deleted
                    // MyBrowser.Start();
                    // Clear cahce and cookies before the test run begin
                    //MyBrowser.DeleteAllVisibleCookies();

                    //WebCommonMethods commonWeb = new WebCommonMethods();
                    //commonWeb.OpenURL(MyBrowser, FrameGlobals.BaseUrl1, "Unable to load the Vegas site", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
                    //WebDriverObj.Manage().Window.Maximize();
                    //Pass("Vegas site loaded successfully");
                    #endregion
                }

            }

            catch (Exception e)
            {
                exceptionStack(e);
                CaptureScreenshot(MyBrowser);
                Fail("Browser failed to load due to an failure/exception");
            }
        }//End Initialize
        /// <summary>
        /// Clean up code to kill the browser instances after every test case execution.
        /// </summary>
        [TearDown]
        public virtual void AfterTest()
        {


            try
            {

                Console.WriteLine("TestCase Name: {0}, WebDriverObject: {1}", Gallio.Framework.TestContext.CurrentContext.Test.Name, _seleniumContainer[Gallio.Framework.TestContext.CurrentContext.Test.Name].GetHashCode());

                IWebDriver driv = ((WebDriverBackedSelenium)_seleniumContainer[Gallio.Framework.TestContext.CurrentContext.Test.Name]).UnderlyingWebDriver;
                   // driv.Close();
                   // driv.Quit();
                    driv.Dispose();
                    //_seleniumContainer[Gallio.Framework.TestContext.CurrentContext.Test.Name].Stop();

             

            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Warning during cleanup: " + e.Message.ToString());
            }//end catch
            finally
            {
                _seleniumContainer.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);

            }
        }//end MyTestCleanup
        public static void SendMail(string attachmentPath)
        {


            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                mailMessage.From = new MailAddress(FrameGlobals.fromMail, FrameGlobals.sender);

                if (FrameGlobals.toMail.Trim().Contains(";"))
                    foreach (string mailID in FrameGlobals.toMail.ToString().Split(';'))
                    {
                        if (mailID.Trim() != null && mailID.Trim() != string.Empty)
                            mailMessage.To.Add(mailID.Trim());
                    }
                else
                    mailMessage.To.Add(FrameGlobals.toMail.Trim());

                if(FrameGlobals.subject==null)
                    mailMessage.Subject = className + " execution summary attached- " + DateTime.Now.GetDateTimeFormats()[10];
                else
                mailMessage.Subject = FrameGlobals.subject + " - " + DateTime.Now.GetDateTimeFormats()[10];


                mailMessage.IsBodyHtml = true;
                mailMessage.Body = FrameGlobals.body;

                Attachment attachment = new Attachment(attachmentPath);

                //AlternateView av = AlternateView.CreateAlternateViewFromString("Test", null,"HTML");
                //mailMessage.AlternateViews.Add(av);
                mailMessage.Attachments.Add(attachment);

                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(FrameGlobals.fromMail, FrameGlobals.mailPass);

                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
        public static void UpdateGDoc(string docName, string sheetName, Dictionary<string, Queue> data, uint column, string Mode)
        {

            return;
            SpreadsheetsService service = new SpreadsheetsService("MySpreadsheetIntegration-v1");
            service.setUserCredentials(FrameGlobals.fromMail, FrameGlobals.mailPass);
            SpreadsheetQuery query = new SpreadsheetQuery();
            query.Title = docName;
            SpreadsheetFeed feed = service.Query(query);
            if (feed.Entries.Count == 0)
            {
                throw new Exception("No sheet found");
            }
            SpreadsheetEntry spreadsheet = (SpreadsheetEntry)feed.Entries[0];
            Console.WriteLine(spreadsheet.Title.Text);
            // Make a request to the API to fetch information about all
            // worksheets in the spreadsheet.
            WorksheetFeed wsFeed = spreadsheet.Worksheets;
            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries[0];
            //Iterate through each worksheet in the spreadsheet.


            foreach (WorksheetEntry entry in wsFeed.Entries)
            {
                //Get the worksheet's title, row count, and column count.
                String title = entry.Title.Text;
                string rowCount = entry.RowCount.Count.ToString();
                string colCount = entry.ColCount.Count.ToString();
                // Print the fetched information to the screen for this worksheet.
                uint row = 3;


                CellQuery cellQuery = new CellQuery(entry.CellFeedLink);
                CellFeed cellFeed = service.Query(cellQuery);
                CellEntry cellEntry;
                
                string TCName = Gallio.Framework.TestContext.CurrentContext.Test.Name;

                if (Mode != "Rerun")
                {
                    #region ClearAllData
                    uint LastRow = 3;

                    if (title == sheetName)
                    {
                        foreach (CellEntry cell in cellFeed.Entries)
                        {
                            if (cell.Cell.Value.Contains("-EOR-"))
                                if (cell.Cell.Column == column)
                                {
                                    LastRow = cell.Cell.Row;
                                    break;
                                }


                        }
                        for (uint rows = row; rows <= LastRow; rows++)
                        {
                            cellEntry = new CellEntry(rows, column, string.Empty);
                            cellFeed.Insert(cellEntry);
                            cellEntry = new CellEntry(rows, column + 1, string.Empty);
                            cellFeed.Insert(cellEntry);
                            cellEntry = new CellEntry(rows, column + 2, string.Empty);
                            cellFeed.Insert(cellEntry);


                        }

                    #endregion
                    #region InsertNewData
                        foreach (KeyValuePair<string, Queue> kvp in testStatus)
                        {

                            Queue s = kvp.Value;
                            foreach (TestDetails tc in s)
                            {

                                cellEntry = new CellEntry(row, column, tc.Name);
                                cellFeed.Insert(cellEntry);
                                cellEntry = new CellEntry(row, column + 1, tc.Status);
                                cellFeed.Insert(cellEntry);
                                if (FailureReasons.ContainsKey(tc.Name))
                                {
                                    cellEntry = new CellEntry(row++, column + 2, FailureReasons[tc.Name]);
                                    cellFeed.Insert(cellEntry);
                                }

                            }
                        }

                        cellEntry = new CellEntry(row, column, "-EOR-");
                        cellFeed.Insert(cellEntry);

                        #endregion
                 }
                }//if

                else                  
                        foreach (KeyValuePair<string, Queue> kvp in testStatus)
                        {

                            Queue s = kvp.Value;
                            foreach (TestDetails tc in s)                           
                                foreach (CellEntry cell in cellFeed.Entries)   
                                  if (title == sheetName)                    
                                    if (cell.Cell.Value.Contains(tc.Name) && cell.Cell.Column == column)
                                    {
                                        
                                        cellEntry = new CellEntry(cell.Cell.Row, column + 1, tc.Status);
                                        cellFeed.Insert(cellEntry);
                                        if (FailureReasons.ContainsKey(tc.Name))
                                        {
                                            cellEntry = new CellEntry(cell.Cell.Row, column + 2, FailureReasons[tc.Name]);
                                            cellFeed.Insert(cellEntry);
                                        }
                                        break;
                                    }//if
                               

                        }//for



                   


               
            }
        }
        #region Assert class
        public class Assert
        {
            public static void Equals<T>(T value1, T value2)
            {
                MbUnit.Framework.Assert.AreEqual(value1, value2);
            }
            public static void Equals<T>(T value1, T value2, string format)
            {
                MbUnit.Framework.Assert.AreEqual(value1, value2, format);
            }
            public static void Equals<T>(T value1, T value2, string format, params object[] args)
            {
                MbUnit.Framework.Assert.AreEqual(value1, value2, format, args);
            }
            public static void IsTrue(bool condition, string message)
            {
                if (!condition)
                {
                    BaseTest.Fail(message);
                }//end if
                MbUnit.Framework.Assert.IsTrue(condition, message);
            }//end IsTrue
            public static void IsFalse(bool condition, string message)
            {
                if (condition)
                {
                    BaseTest.Fail(message);
                }//end if
                MbUnit.Framework.Assert.IsFalse(condition, message);
            }//end IsFalse
            public static void Fail(string message)
            {
                BaseTest.Fail(message);
                MbUnit.Framework.Assert.Fail(message);
            }//end Fail
            //public static void Skip(string message)
            //{
            //    BaseTest.Skip(message);
            //    MbUnit.Framework.Assert.Ignore(message);
            //}//end Skip
        }//end class

        // private static Queue _tcList = new Queue();


        public struct TestCase
        {
            public string detail;
            public string expectedResult;

        }//end struct
        public struct TestDetails
        {
            public string Name;
            public string Status;

        }//end struct



        public static Stack GetStackByTestCaseName(Dictionary<string, Stack> TcStack, string testcaseName)
        {
            Stack localStack = null;
            //Boolean flag = false;
            foreach (KeyValuePair<string, Stack> pair in TcStack)
            {
                if (pair.Key == testcaseName)
                {
                    localStack = pair.Value;
                    //  flag = true;
                    break;
                }

            }
            return localStack;
        }


        public static void AddTestCase(string detail, string expectedResult)
        {
            Stack tcList = new Stack();
            TestCase tc = new TestCase();
            tc.detail = detail;
            tc.expectedResult = expectedResult;

            if (testCaseKey.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
            {
                tcList = GetStackByTestCaseName(testCaseKey, Gallio.Framework.TestContext.CurrentContext.Test.Name);
                tcList.Push(tc);
                testCaseKey.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
                testCaseKey.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, tcList);


            }
            else
            {

                tcList.Push(tc);
                testCaseKey.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, tcList);
            }

            if (OutputLog.IsDebugEnabled) { OutputLog.DebugFormat("Adding TC: {0}, Expected Result: {1}", detail, expectedResult); }

            // _tcList.Enqueue(tc);
            // _tcList.Push(tc);
        }//end AddTestCase
        public static void Pass(string whichOne = null)
        {
            Stack _tcList = new Stack();
            if (testCaseKey.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                _tcList = GetStackByTestCaseName(testCaseKey, Gallio.Framework.TestContext.CurrentContext.Test.Name);
            StringBuilder SB = new StringBuilder();

            if (_tcList.Count > 0)
            {
                //TestCase tc = (TestCase)_tcList.Dequeue();
                TestCase tc = (TestCase)_tcList.Pop();

                do
                {
                    try
                    {


                        string name = Guid.NewGuid().ToString();
                        Gallio.Framework.TestLog.EmbedHtml("P" + name.Substring(name.Length - 3),
                         "<u><i style =\"font-family:arial;color:green;font-size:8px;\">Done</i>" +
                         "<b style =\"font-family:arial;color:green;font-size:15px;\">&#10004</b></u>" +
                         "<b style=\"font-family:arial;color:blue;font-size:15px;\" >&nbsp Test Case: </b>" +
                          "<i>" + tc.detail + "</i>" +
                          "<br>"
                            //  "<b style=\"font-family:arial;color:brown;font-size:15px;\" >Result: </b>" +
                            //  "<i>" + whichOne + "</i>"                   
                          );
                        SB.Append("Done: >> " + tc.detail);
                        testCaseKey.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
                        testCaseKey.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, _tcList);

                        break;
                    }
                    catch (Exception) { continue; }


                } while (true);
                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<PASS Detail=\"" + tc.detail + "\" Result:\"" + tc.expectedResult + "\" />"); }

                //Console.WriteLine(SB.ToString());

            }//end if
        }//end Pass
        public static void Fail(string message)
        {
            Stack _tcList = new Stack();
            string TCName = Gallio.Framework.TestContext.CurrentContext.Test.Name;
            if (testCaseKey.ContainsKey(TCName))
                _tcList = GetStackByTestCaseName(testCaseKey, TCName);
            StringBuilder SB = new StringBuilder();

            if (_tcList.Count > 0)
            {
                string name = Guid.NewGuid().ToString();
                //TestCase tc = (TestCase)_tcList.Dequeue();
                TestCase tc = (TestCase)_tcList.Pop();

                do
                {
                    try
                    {
                        SB.Append("Failed: >>>" + tc.detail + "\n" +
                                        " - Expected:" + tc.expectedResult +
                                        "\n - Actual:" + message
                        );
                        Gallio.Framework.TestLog.EmbedHtml("f" + name.Substring(name.Length - 3),
                             "<i style =\"font-family:arial;color:red;font-size:10px;\">Fail</i>" +
                             "<b style =\"font-family:arial;color:red;font-size:17px;\">&#10008</b>" +
                            "<b style=\"font-family:arial;color:red;font-size:15px;\" >&nbsp Test Case: </b>" +
                            "<i>" + tc.detail + "</i>" +
                            "<br>" +

                           "<b style =\"font-family:arial;color:red;font-size:15px;\">&nbsp &nbsp &nbsp &#126</b>" +
                            "<b style=\"font-family:arial;color:brown;font-size:15px;\" >&nbsp Expected: </b>" +
                            "<i>" + tc.expectedResult + "</i>" +
                            "<br>" +

                            "<b style =\"font-family:arial;color:red;font-size:15px;\">&nbsp &nbsp &nbsp &#126</b>" +
                            "<b style=\"font-family:arial;color:brown;font-size:15px;\" >&nbsp Actual: </b>" +
                            "<i style=\"font-family:arial;color:red;font-size:14px;\" >" + message + "</i>"
                            );
                        testCaseKey.Remove(TCName);
                        testCaseKey.Add(TCName, _tcList);

                        break;
                    }
                    catch (Exception) { continue; }



                } while (true);
                //Console.WriteLine(SB.ToString());
                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />"); }
            }//end if
            //FailAll();
            //MbUnit.Framework.Assert.Fail();
            if(!FailureReasons.ContainsKey(TCName))
            FailureReasons.Add(TCName, message);
            MbUnit.Framework.Assert.Terminate(Gallio.Model.TestOutcome.Failed, message);
        }//end Fail
        public static void FailIndi(string message)
        {
            Stack _tcList = new Stack();
            if (testCaseKey.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                _tcList = GetStackByTestCaseName(testCaseKey, Gallio.Framework.TestContext.CurrentContext.Test.Name);


            if (_tcList.Count > 0)
            {
                //TestCase tc = (TestCase)_tcList.Dequeue();
                TestCase tc = (TestCase)_tcList.Pop();
                Console.WriteLine("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />");
                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />"); }
                testCaseKey.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
                testCaseKey.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, _tcList);

            }//end if
        }//end Fail_Indi
        public static void Skip(string message)
        {
            Stack _tcList = new Stack();
            if (testCaseKey.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                _tcList = GetStackByTestCaseName(testCaseKey, Gallio.Framework.TestContext.CurrentContext.Test.Name);


            if (_tcList.Count > 0)
            {
                do
                {
                    try
                    {

                        //TestCase tc = (TestCase)_tcList.Dequeue();
                        TestCase tc = (TestCase)_tcList.Pop();
                        string name = Guid.NewGuid().ToString();
                        Gallio.Framework.TestLog.EmbedHtml("S" + name.Substring(name.Length - 3),
                         "<u><i style =\"font-family:arial;color:green;font-size:8px;\">Skipped</i>" +
                         "<b style =\"font-family:arial;color:green;font-size:15px;\"> - !</b></u>" +
                         "<b style=\"font-family:arial;color:blue;font-size:15px;\" >&nbsp Test Case: </b>" +
                          "<i>" + tc.detail + "</i>" +
                          "<i style=\"font-family:arial;color:blue;font-size:15px;\"> Reason: " + message + "</i>" +
                          "<br>"
                            //  "<b style=\"font-family:arial;color:brown;font-size:15px;\" >Result: </b>" +
                            //  "<i>" + whichOne + "</i>"                   
                          );
                        break;
                    }
                    catch (Exception) { continue; }


                } while (true);
            }//end if
            // SkipAll();
        }//end Fail
        public static void SkipIndi(string message)
        {

            Stack _tcList = new Stack();
            if (testCaseKey.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                _tcList = GetStackByTestCaseName(testCaseKey, Gallio.Framework.TestContext.CurrentContext.Test.Name);

            if (_tcList.Count > 0)
            {
                //TestCase tc = (TestCase)_tcList.Dequeue();
                TestCase tc = (TestCase)_tcList.Pop();
                Console.WriteLine("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />");
                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />"); }
                testCaseKey.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
                testCaseKey.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, _tcList);

            }//end if
        }//end SKip_Indi
        public static void SkipAll()
        {
            Stack _tcList = new Stack();
            if (testCaseKey.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                _tcList = GetStackByTestCaseName(testCaseKey, Gallio.Framework.TestContext.CurrentContext.Test.Name);


            TestCase tc;
            for (; _tcList.Count > 0; )
            {
                //TestCase tc = (TestCase)_tcList.Dequeue();
                tc = (TestCase)_tcList.Pop();
                Console.WriteLine("<SKIP Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" />");
                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<SKIP Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" />"); }
            }//end for
            testCaseKey.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
            testCaseKey.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, _tcList);

        }//end Fail
        public static void FailAll()
        {
            Stack _tcList = new Stack();
            if (testCaseKey.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                _tcList = GetStackByTestCaseName(testCaseKey, Gallio.Framework.TestContext.CurrentContext.Test.Name);


            TestCase tc;

            for (; _tcList.Count > 0; )
            {
                //TestCase tc = (TestCase)_tcList.Dequeue();
                tc = (TestCase)_tcList.Pop();
                // Console.WriteLine("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" />");
                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" />"); }
            }//end for
            testCaseKey.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
            testCaseKey.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, _tcList);

        }//end Fail

        #endregion

        /// <summary> 
        /// Gets a <see cref="Screenshot"/> object representing the image of the page on the screen. 
        /// </summary> 
        /// <returns>A <see cref="Screenshot"/> object containing the image.</returns> 
        public static Screenshot GetScreenshot(RemoteWebDriver driver)
        {


            // Get the screenshot as base64. 
            Response screenshotResponse = (Response)driver.ExecuteScript(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();

            // ... and convert it. 
            return new Screenshot(base64);
        }
        public static void CaptureScreenshot(dynamic webObj)
        {
            System.Threading.Thread.Sleep(FrameGlobals.waitMilliSec_Screen + 500);
            if (FrameGlobals.waitMilliSec_Screen > 5000) FrameGlobals.waitMilliSec_Screen = 0;

            if (webObj == null)
                return;
            DateTime dateFormat;
              IWebDriver driverObj;
              if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                  driverObj = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
              else
                  driverObj = webObj;
            try
            {
              
              
                if (driverObj != null)
                {
                    driverObj.Manage().Window.Maximize();

                    string resultsFolder = null;
                    dateFormat = DateTime.Now;
                    DirectoryInfo currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                    string date1 = (dateFormat.ToString("yyyy-MM-dd"));

                    string fileName = DateTime.Now.Day.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Month.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Year.ToString(CultureInfo.CurrentCulture) + "_" + DateTime.Now.Hour.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Minute.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Hour.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Second.ToString(CultureInfo.CurrentCulture) + ".png";

                    resultsFolder = currentDirectory.Parent.FullName;
                    if (!Directory.Exists(resultsFolder + "\\" + "ScreenShots" + "\\" + date1.ToString() + "\\" + FrameGlobals.BrowserToLoad.ToString()))
                    {
                        Directory.CreateDirectory(resultsFolder + "\\" + "ScreenShots" + "\\" + date1.ToString() + "\\" + FrameGlobals.BrowserToLoad.ToString());
                    }
                    string relativePath = "\\ScreenShots\\" + date1 + "\\" + FrameGlobals.BrowserToLoad.ToString() + "\\" + fileName;
                    string filePath = resultsFolder + relativePath;
                    Screenshot snapShot = ((ITakesScreenshot)driverObj).GetScreenshot();
                    // Screenshot snapShot = GetScreenshot(driverObj);
                    snapShot.SaveAsFile(filePath, ImageFormat.Png);
                    System.Drawing.Image img1 = System.Drawing.Image.FromFile(filePath);
                    Gallio.Framework.TestLog.AttachImage("<Click here for SnapShot>(" + fileName + ")", img1);
                    //isFail = true;

                }//end if
            }//end try
            catch (Exception ex) { Console.Error.WriteLine(ex.Message); Console.Error.Flush(); }
            finally
            {
                driverObj.Dispose();
            }
        }//end TakeFailedScreenshot
        public static void CaptureScreenshot(dynamic webObj, string imageName)
        {
            System.Threading.Thread.Sleep(FrameGlobals.waitMilliSec_Screen + 500);
            if (FrameGlobals.waitMilliSec_Screen > 5000) FrameGlobals.waitMilliSec_Screen = 0;

            if (webObj == null)
                return;
            IWebDriver driverObj;
            if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                driverObj = ((WebDriverBackedSelenium)webObj).UnderlyingWebDriver;
            else
                driverObj = webObj;

            DateTime dateFormat;
            try
            {
                if (driverObj != null)
                {
                    driverObj.Manage().Window.Maximize();
                    string resultsFolder = null;
                    dateFormat = DateTime.Now;

                    DirectoryInfo currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                    string date1 = (dateFormat.ToString("yyyy-MM-dd"));

                    resultsFolder = currentDirectory.Parent.FullName;
                    if (!Directory.Exists(resultsFolder + "\\" + "ScreenShots" + "\\" + date1.ToString() + "\\" + FrameGlobals.BrowserToLoad.ToString()))
                    {
                        Directory.CreateDirectory(resultsFolder + "\\" + "ScreenShots" + "\\" + date1.ToString() + "\\" + FrameGlobals.BrowserToLoad.ToString());
                    }
                    string date2 = (dateFormat.ToString("yyyy-MM-dd HH-mm"));
                    string fileName = imageName.ToString() + " " + date2 + ".png";
                    string relativePath = "\\ScreenShots\\" + date1 + "\\" + FrameGlobals.BrowserToLoad.ToString() + "\\" + fileName;
                    string filePath = resultsFolder + relativePath;
                    Screenshot snapShot = ((ITakesScreenshot)driverObj).GetScreenshot();
                    // Screenshot snapShot = ((ScreenShotRemoteWebDriver)driverObj).GetScreenshot();
                    snapShot.SaveAsFile(filePath, ImageFormat.Png);
                    System.Drawing.Image img1 = System.Drawing.Image.FromFile(filePath);
                    Gallio.Framework.TestLog.AttachImage("<Click here for SnapShot>(" + fileName + ")", img1);
                    //isFail = true;


                }//end if
            }//end try
            catch (Exception ex) { Console.Error.WriteLine(ex.Message); Console.Error.Flush(); }
            finally
            {
                driverObj.Dispose();
            }
        }
        private static string MassageXML(string allOfIt)
        {
            try
            {
                StringBuilder massaged = new StringBuilder(allOfIt.Length);
                for (int i = 0; i < allOfIt.Length; i++)
                {
                    if (((int)allOfIt[i]) > 127)
                    {
                        massaged.Append("&#" + ((int)allOfIt[i]).ToString(CultureInfo.CurrentCulture) + ";");
                    }//end if
                    else
                    {
                        massaged.Append(allOfIt[i]);
                    }//end else
                }//end for

                return (massaged.ToString());
            }//end try
            catch (Exception ex)
            {
                if (OutputLog.IsErrorEnabled) { OutputLog.Error("Failed to massage the XML because of the following error.", ex); }
                return (allOfIt);
            }//end catch
        }//end MassageXML
        public static void exceptionStack(Exception e)
        {
            StringBuilder failtext = new StringBuilder();

            failtext.AppendLine("\n------------------------Exception point Details------------------------------------------------");
            failtext.AppendLine(e.Message.ToString());
            StackTrace st = new StackTrace(e, true);
            StackFrame[] frames = st.GetFrames();
            // Iterate over the frames extracting the information you need
            foreach (StackFrame frame in frames)
            {
                if (frame.GetFileLineNumber() > 0)
                    failtext.AppendLine(frame.GetFileName() + ":" + frame.GetMethod().Name + "(" + frame.GetFileLineNumber() + ")");

                //  Console.WriteLine("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
            }
            failtext.AppendLine("-----------------------------------------------------------------------------------------------\n\n");

            string stack = failtext.ToString();
            int i = 1;
        rep:
            try
            {
                Gallio.Framework.TestLog.AttachPlainText("Exception Stack info" + i, stack);
            }
            catch (Exception) { i++; goto rep; }
        }

        public static void UpdateThirdStatus(string status)
        {
            string tcname = Gallio.Framework.TestContext.CurrentContext.Test.Parent.ToString() + Gallio.Framework.TestContext.CurrentContext.Test.Name;

            if (thirdStatus.ContainsKey(tcname))
            {
                thirdStatus.Remove(tcname);
                thirdStatus.Add(tcname, status);
            }
            else
                thirdStatus.Add(tcname, status);
        }

        //***********************************
        //Regression Setups
        //***********************************
        #region Declaration
        public WebCommonMethods commonWebMethods = new WebCommonMethods();
        public wActions wAction = new wActions();
        public Framework.Common.Common commonFramework = new Framework.Common.Common();
        #endregion
        #region commonMethods

        public static void WriteUserName(string user)
        {
            if (Usernames.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                Usernames.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
            Usernames.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, user);

        }


        public static void WriteCommentToMailer(string message)
        {
            if (excelComment.ContainsKey(Gallio.Framework.TestContext.CurrentContext.Test.Name))
                excelComment.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);

            excelComment.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name,
             message);


        }
        public IWebDriver browserInitialize(ISelenium iBrowser)
        {
            iBrowser.Start();
            iBrowser.WindowMaximize();
            // Pass("Browser opened successfully.");
            AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
            try
            {
                // Clear cahce and cookies before the test run begin
                iBrowser.DeleteAllVisibleCookies();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
            }
            iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
            //iBrowser.Open(FrameGlobals.LadbrokesBase);
            OpenURL(iBrowser, FrameGlobals.BingoURL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
          //  iBrowser.Open(FrameGlobals.BingoURL);
            iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

            //bool s = iBrowser.IsElementPresent("id('column-6')");

            //iBrowser.WindowMaximize();
            //iBrowser.WindowFocus();
            IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
           // commonWebMethods.WaitforPageLoad(drive);
            drive.Manage().Window.Maximize();
            Pass();
            return drive;
        }
        public IWebDriver browserInitialize(ISelenium iBrowser, string URL)
        {
            iBrowser.Start();
            iBrowser.WindowMaximize();
            // Pass("Browser opened successfully.");
            AddTestCase("Open/Load the base portal", "Portal should be opened successfully");
            try
            {
                // Clear cahce and cookies before the test run begin
                iBrowser.DeleteAllVisibleCookies();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Delete Cookies Failed: {0}", Ex.Message);
            }
            iBrowser.SetTimeout(FrameGlobals.PageLoadTimeOut);
            //iBrowser.Open(FrameGlobals.LadbrokesBase);
            OpenURL(iBrowser, URL, "Base Site failed to open", int.Parse(FrameGlobals.PageLoadTimeOut) / 1000);
          
            iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);

            //bool s = iBrowser.IsElementPresent("id('column-6')");

            //iBrowser.WindowMaximize();
            //iBrowser.WindowFocus();
            IWebDriver drive = ((WebDriverBackedSelenium)iBrowser).UnderlyingWebDriver;
            iBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);
            drive.Manage().Window.Maximize();
            Pass();

            wAction.Click(drive, By.Id("COOKIE_CONTINUE"));
            //casino flash issue
            if (URL == FrameGlobals.CasinoURL)
                Thread.Sleep(TimeSpan.FromSeconds(1));
            else if(URL == FrameGlobals.GamesURL)
                wAction.Click(drive, By.XPath("//*[text()='No thanks']"));

            return drive;
        }
        public string ReadxmlData(string tag, string key, string filename)
        {

            string _testDataFilePath = null;
            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\Data_Files\\";
            string path = _testDataFilePath + filename;
            return XMLReader.ReadXMLQuery_ByKeyValue(path, tag, key);
        }

        //public void InitializeBase(IWebDriver driver)
        //{

        //    driver.Manage().Cookies.DeleteAllCookies();
        //    AddTestCase("Load Vegas site", "Site to load successfully");
        //    commonWebMethods.OpenURL(driver, FrameGlobals.BingoURL, "Base Site failed to load", FrameGlobals.reloadTimeOut);
        //    driver.Manage().Window.Maximize();
        //    Pass();

        //}
        #endregion

        public void OpenURL(ISelenium webObj, string URL, string failMsg, int timeOut = 0)
        {
            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (!webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    {
                        failMsg = "Object is not WebDriverBackedSelenium";
                        throw new Exception();
                    }
                    webObj.SetTimeout(FrameGlobals.PageLoadTimeOut);
                    webObj.Open(URL);
                    WaitforPageLoad(webObj);

                    //if (!IsElementPresent(webObj, By.XPath("//html/body")))
                    //   throw new Exception();

                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    continue;
                }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        public void OpenURL(IWebDriver webObj, string URL, string failMsg, int timeOut = 0)
        {
            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {
                    if (webObj.GetType().Name.ToString().Contains("WebDriverBackedSelenium"))
                    {
                        failMsg = "Object is not Webdriver";
                        throw new Exception();
                    }
                    IWebDriver drive = webObj;
                    ManageTimeOut(drive,5);
                    drive.Navigate().GoToUrl(URL);
                    WaitforPageLoad(drive);

                    //if (!IsElementPresent(webObj, By.XPath("//html/body")))
                    //    throw new Exception();

                    returnValue = true;
                    break;
                }
                catch (Exception e)
                {
                    continue;
                }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);

        }
        public void WaitforPageLoad(IWebDriver webObj)
        {

            ManageTimeOut(webObj, double.Parse(FrameGlobals.PageLoadTimeOut) / 1000);

        }
        public void WaitforPageLoad(ISelenium webObj)
        {
            webObj.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);
        }
        public void ManageTimeOut(IWebDriver driver, double timeOutSec)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOutSec));

        }
    }//end class
}//end namespace
