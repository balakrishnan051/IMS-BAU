using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;
using OpenQA.Selenium;
//using ICE.ActionRepository;
using ICE.DataRepository.Vegas_IMS_Data;
using Framework;

namespace Ladbrokes_IMS_TestRepository.VirtualScript
{
    public class Virtual_AnW
    {
        wActions wAction = new wActions();
        AccountAndWallets AnW = new AccountAndWallets();
        public void RealPlayCust(IWebDriver driverObj, Registration_Data regData)
        {
                    AnW.OpenRealPlay(driverObj);
                    System.Threading.Thread.Sleep(2000);
                    wAction._Click(driverObj, ICE.ObjectRepository.ORFile.Accounts_Wallets_Registration, wActions.locatorType.xpath, "reg_invalid_Prpmt_lgn", "Registration button not found in login pop up", 0, false);
                    AnW.Registration_PlaytechPages(driverObj, ref regData);
              
        }
        //public void RegisterIMS(AdminSuite.AdminBase adminBase, Registration_Data regData, IMS_AdminSuite.Common commIMS)
        //{
        //    baseIMS.Init();
        //    //  regData.username = "testCDTIP1";
        //    regData.username = commIMS.CreateNewCustomer(baseIMS.IMSDriver, regData.username, regData.password, "Credit");
        //    // commIMS.SearchCustomer(baseIMS.IMSDriver, regData.username);

        //    WriteCommentToMailer("UserName: " + regData.username + ";\nPassword: " + regData.password);
        //    AddTestCase("Customer created: " + regData.username, "");
        //    Pass();
        //    adminBase.Init();
        //    //Thread.Sleep(TimeSpan.FromMinutes(1));
        //    adminComm.SearchCustomer(regData.username, adminBase.MyBrowser);
        //    //adminComm.ManualAdjustment("100", adminBase.MyBrowser);
        //    //adminComm.SetCreditLimit(adminBase.MyBrowser);
        //    adminBase.Quit();
              
        //}

    }
}
