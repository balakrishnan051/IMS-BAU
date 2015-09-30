using System.Diagnostics;
using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Framework;
using OpenQA.Selenium.Remote;
using System;
//using ICE.ActionRepository;
using ICE.ObjectRepository;

namespace IMS_AdminSuite
{
    public class IMS_Base
    {
     //   public ISelenium IMSBrowser;
        public IWebDriver IMSDriver;
        WebCommonMethods commonWebMethods = new WebCommonMethods();
        wActions wAction = new wActions();

        public void Init()
        {
            BaseTest.AddTestCase("Login to IMS Admin", "Login should be sucessfull");
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
                IMSDriver = new FirefoxDriver();
               // IMSBrowser = new WebDriverBackedSelenium(IMSDriver, "https://admin-stg.ladbrokes.com");
            }
           // IMSBrowser.Start();
            wAction.OpenURL(IMSDriver, FrameGlobals.IMSURL, "Admin stage not loaded", FrameGlobals.reloadTimeOut);
            IMSDriver.Manage().Window.Maximize();
          //  IMSBrowser.WindowMaximize();

            wAction._Type(IMSDriver,ICE.ObjectRepository.ORFile.IMSBase, wActions.locatorType.name, "username",FrameGlobals.IMSUname, "Username Textbox not found", FrameGlobals.elementTimeOut, false);
            wAction._Type(IMSDriver, ICE.ObjectRepository.ORFile.IMSBase, wActions.locatorType.name, "password", FrameGlobals.IMSPass, "Password Textbox not found");
            wAction._Click(IMSDriver, ICE.ObjectRepository.ORFile.IMSBase, wActions.locatorType.name, "Submit", "Submit Textbox not found");
            
            IMSDriver.SwitchTo().DefaultContent();

            if (FrameGlobals.projectName != "IP2")
            {
                wAction.WaitAndMovetoFrame(IMSDriver, "menu", "Frame menu is not found", FrameGlobals.elementTimeOut);
                BaseTest.Assert.IsTrue(
                wAction._WaitUntilElementPresent(IMSDriver, ORFile.IMSBase, wActions.locatorType.link, "ManageUser"), "IMS user not logged in");
                BaseTest.Pass();
                IMSDriver.SwitchTo().DefaultContent();
                wAction.WaitAndMovetoFrame(IMSDriver, "top", "Top Frame not found", FrameGlobals.elementTimeOut);

                wAction._Click(IMSDriver, ORFile.IMSCommon, wActions.locatorType.xpath, "tryout_newlook_lnk");
            } 
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
