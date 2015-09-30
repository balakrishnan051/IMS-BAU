using System.Diagnostics;
using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Framework;
using OpenQA.Selenium.Remote;
using System;
using ICE.ActionRepository;
using ICE.ObjectRepository;
using OpenQA.Selenium.Chrome;

namespace Telebet_Suite
{
    public class IMS_Base
    {
     //   public ISelenium IMSBrowser;
        public IWebDriver IMSDriver;
        WebCommonMethods commonWebMethods = new WebCommonMethods();
        wActions wAction = new wActions();

        public void Init(IWebDriver driver=null)
        {
            BaseTest.AddTestCase("Login to IMS Admin", "Login should be sucessfull");
            if (driver != null)
                IMSDriver = driver;
            else
            {
                if (FrameGlobals.useGrid.ToUpper() == "YES")
                {

                    FirefoxProfile ffProfile = new FirefoxProfile();
                    DesiredCapabilities desriredCapibilities = null;
                    desriredCapibilities = new DesiredCapabilities();
                    desriredCapibilities = DesiredCapabilities.Firefox();
                    desriredCapibilities.SetCapability(CapabilityType.BrowserName, "firefox");
                    // ffProfile.SetPreference("general.useragent.override", FrameGlobals.userAgentValue);

                    IMSDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), desriredCapibilities, TimeSpan.FromSeconds(420.0));
                    // IMSBrowser = new WebDriverBackedSelenium(IMSDriver, "http://www.google.com"); //_seleniumContainer.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, MyBrowser); 
                }
                else
                {
                    IMSDriver = new ChromeDriver();
                    // IMSBrowser = new WebDriverBackedSelenium(IMSDriver, "https://admin-stg.ladbrokes.com");
                }
            }
            // IMSBrowser.Start();
            wAction.OpenURL(IMSDriver, "https://stg-gib.ladbrokes.com/telebet", "Telebet stage not loaded", FrameGlobals.reloadTimeOut);
            IMSDriver.Manage().Window.Maximize();
            //  IMSBrowser.WindowMaximize();

            wAction._Type(IMSDriver,ICE.ObjectRepository.ORFile.Telebet, wActions.locatorType.id, "username","subhasini_k", "Username Textbox not found", FrameGlobals.elementTimeOut, false);
            wAction._Type(IMSDriver, ICE.ObjectRepository.ORFile.Telebet, wActions.locatorType.name, "password", "123456", "Password Textbox not found");
            wAction._Type(IMSDriver, ICE.ObjectRepository.ORFile.Telebet, wActions.locatorType.id, "terminalcode", "test_4", "Terminal code Textbox not found", FrameGlobals.elementTimeOut, false);
            wAction._Click(IMSDriver, ICE.ObjectRepository.ORFile.Telebet, wActions.locatorType.id, "submit", "Submit Textbox not found");
            
            //IMSDriver.SwitchTo().DefaultContent();
            //wAction.WaitAndMovetoFrame(IMSDriver, "menu", "Frame menu is not found", FrameGlobals.elementTimeOut);           
            BaseTest.Assert.IsTrue(wAction._IsElementPresent(IMSDriver, ICE.ObjectRepository.ORFile.Telebet, wActions.locatorType.id, "searchacc"), "User not logged in");
        
            BaseTest.Pass();
            //IMSDriver.SwitchTo().DefaultContent();
            //wAction.WaitAndMovetoFrame(IMSDriver, "top", "Top Frame not found", FrameGlobals.elementTimeOut);
            //wAction._Click(IMSDriver, ORFile.IMSCommon, wActions.locatorType.xpath, "tryout_newlook_lnk", "Could not find try out new look link", 0, false);
              
        }

        public void Quit()
        {
            try
            {
                if (IMSDriver != null)
                {
                    commonWebMethods.BrowserQuit(IMSDriver);
                }
            }
            catch (Exception) { }
        }
    }
}
