using MbUnit.Framework;
using System.Diagnostics;
using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Framework;

namespace AdminSuite
{
    public class WcmsBase
    {
        public ISelenium browser;
        public IWebDriver firefox;

        [SetUp]
        public void Init()
        {
            firefox = new FirefoxDriver();
            browser = new WebDriverBackedSelenium(firefox, "http://cms.uat-ecommerce.ladbrokes.com/hmc/hybris?wid=MC57x0");
            browser.Start();
            browser.Open("http://cms.uat-ecommerce.ladbrokes.com/hmc/hybris?wid=MC57x0");
            browser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut.ToString());
            browser.Type("id=Main_user", "mallikarjunmp");
            browser.Type("id=Main_password", "12345");
            browser.Click("id=Main_label");
            browser.WaitForPageToLoad(FrameGlobals.PageLoadTimeOut.ToString());
        }

        [TearDown]
        public virtual void Cleanup()
        {
            Process[] process = Process.GetProcessesByName("firefox");
            foreach (Process p in process)
            {
                p.Kill();
            }
        }
    }
}
