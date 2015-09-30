using MbUnit.Framework;
using System.Diagnostics;
using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Framework;

using OpenQA.Selenium.Remote;
using System;

namespace AdminSuite
{

    public class AdminBase
    {

        public ISelenium MyBrowser;
        public IWebDriver FfDriver;

        [SetUp]
        // UpdateResourceFile("pradeep","@E:\AdminScripts\ECommerce_CodeBase\PreRequisite_Suite\Resources\Customers.resx");

        /// <summary> To Launch the browser & to login to Open Bet application
        /// </summary>
        ///  Author: Yogesh
        /// Ex: AdminBase.init()
        /// <returns>None</returns>
        /// Created Date: 22-Dec-2011
        /// Modified Date: 
        /// Modification Comments:
        public void Init()
        {
            if (FrameGlobals.useGrid.ToUpper() == "YES")
            {
                FirefoxProfile ffProfile = new FirefoxProfile();
                DesiredCapabilities desriredCapibilities = null;
                desriredCapibilities = new DesiredCapabilities();
                desriredCapibilities = DesiredCapabilities.Firefox();
                desriredCapibilities.SetCapability(CapabilityType.BrowserName, "firefox");
                // ffProfile.SetPreference("general.useragent.override", FrameGlobals.userAgentValue);

                FfDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), desriredCapibilities, TimeSpan.FromSeconds(420.0));
                MyBrowser = new WebDriverBackedSelenium(FfDriver, "http://www.google.com"); //_seleniumContainer.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, MyBrowser); 
            }
            else
            {
                FfDriver = new FirefoxDriver();
                MyBrowser = new WebDriverBackedSelenium(FfDriver, FrameGlobals.OBUrl);
                //MyBrowser = new WebDriverBackedSelenium(FfDriver, "https://stg-gib.ladbrokes.com/admin");
            }
            MyBrowser.Start();
            MyBrowser.Open(FrameGlobals.OBUrl);
            MyBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);
            MyBrowser.Type(AdminSuite.CommonControls.AdminHomePage.UsrNmeTxtBx, FrameGlobals.AdminName);
            MyBrowser.Type(AdminSuite.CommonControls.AdminHomePage.PwdTxtBx, FrameGlobals.AdminPass);
           // MyBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut);
            MyBrowser.Click(AdminSuite.CommonControls.AdminHomePage.LoginBtn);
            MyBrowser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut.ToString());
           // MyBrowser.WindowMaximize();
            FfDriver.Manage().Window.Maximize();
            
        }

        //[TearDown]
        public virtual void Cleanup()
        {
        //    Process[] process = Process.GetProcessesByName("firefox");
        //    foreach (Process p in process)
        //    {
        //        p.Kill();
        //    }

            try
            {

                if (FfDriver != null)
                {
                    
                    FfDriver.Close();
                    FfDriver.Quit();
                    FfDriver.Dispose();
                }
            }
            catch (Exception) { }
        }

        public virtual void Quit()
        {
            try{
            if(MyBrowser!=null)
            MyBrowser.Close();
             }
            catch (Exception) { }
        }
    }
}
