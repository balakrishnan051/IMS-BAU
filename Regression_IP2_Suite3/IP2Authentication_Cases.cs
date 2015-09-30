using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using Framework;
using System.Threading;
using Regression_Suite_IP2;


namespace Regression_IP2_Suite3
{

    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class Security:BaseTest
    {
        Security_Sports SecSp = new Security_Sports();
        Security_Reg_and_Login secReg = new Security_Reg_and_Login();
        Security_MyAcct_Cashier SecMy = new Security_MyAcct_Cashier();
        [Test(Order = 1)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Valid_User_InvalidPwd_8615()
        {
            SecSp.Verify_Invalid_LoginError_UserPwd_DebitCust();
        }
        [Test(Order = 2)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Casino_LockedDebitCust_Login_8614_8611()
        {
            secReg.Verify_Casino_LockedDebitCust_Login();
        }

        [Test(Order = 3)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void InvalidLogin_CreditCustomer_8610()
        {
            secReg.Verify_InvalidLogin_CreditCustomer_996();
        }
        [Test(Order = 4)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void AVFailed_Cust_Login_8608()
        {
            SecSp.Verify_AVFailed_Cust_Login_971();
        }

        [Test(Order = 5)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Frozen_Cust_Login_8607()
        {
            
            SecSp.Verify_Frozen_Cust_Login();
        }
        [Test(Order = 6)]
        [RepeatOnFail]
        [Timeout(1000)]
        [DependsOn("Casino_LockedDebitCust_Login_8614_8611")]
        public void Search_LockedCustomer_Telebet_8603()
        {
            Usernames["Verify_Casino_LockedCreditCust_Login"] = Usernames["Casino_LockedDebitCust_Login_8614_8611"];
            secReg.Verify_Search_LockedCustomer_Telebet();
        }

      //  [Test(Order = 7)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Sports_Bet_Quick_Deposit_8585()
        {

            SecSp.Verify_Sports_Bet_Quick_Deposit_1380();
        }
        
        [Test(Order = 8)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SearchLoyalityCust_Telebet_8583()
        {

            SecMy.Verify_SearchLoyalityCust_Telebet();
        }
        [Test(Order = 9)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Sports_Logout_8582()
        {

            SecSp.Verify_Sports_Logout_1544();
        }
      //  [Test(Order = 10)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Ecom_Login_Logout_8581()
        {

            SecSp.Verify_Ecom_Login_Logout_1719();
        }
        [Test(Order = 11)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SearchCustomer_Logout_Telebet_8576()
        {

            secReg.Verify_SearchCustomer_Logout_Telebet();
        }
        [Test(Order = 12)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SessionKill_Login_8574()
        {

            secReg.Verify_SessionKill_Login_1818();
        }
        [Test(Order = 12)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void All_PT_Login_Logout_8571()
        {

            secReg.Verify_All_PT_Login_964_1545();
        }
        [Test(Order = 13)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Invalid_LoginError_BothInvalid_DebitCust_8567()
        {

            secReg.Verify_Invalid_LoginError_BothInvalid_DebitCust();
        }
        [Test(Order = 14)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Autologin_AllProducts_8563()
        {

            secReg.Verify_PT_Registration_Autologin();
            BeforeTest();
            SecSp.Verify_Sports_Customer_Registration_Autologin();
        }
        [Test(Order = 15)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Sports_PlaceBet_Login_8572()
        {

            SecSp.Verify_Sports_PlaceBet_Login();

        }
    }

    [TestFixture, Timeout(15000)]
    [Parallelizable]
    public class SelfEx : BaseTest
    {
        SelfExcl_Admin_Portal SxAd = new SelfExcl_Admin_Portal();
        SelfExcl_Portal_Telebet SxP = new SelfExcl_Portal_Telebet();

        [Test(Order = 1)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_Login_Bingo_8609()
        {
            SxP.Verify_SelfExcl_Login_Bingo();
        }
        [Test(Order = 2)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_Login_Games_8604()
        {
            SxAd.Verify_SelfExcl_Login_Games_1109();
        }
       
       
        [Test(Order = 5)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void Withdraw_SelfEx_Approval_IMS_NonGBP_8596()
        {
            SxP.Verify_Withdraw_SelfEx_Approval_IMS_NonGBP();
        }

        [Test(Order = 6)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_TelebetCustomer_8590()
        {
            SxP.Verify_SelfExcl_TelebetCustomer_1114_1148();
            Usernames["Verify_SelfExcl_TelebetCustomer_1114_1148"] = Usernames["SelfExcl_TelebetCustomer_8590"];
        }


        [Test(Order = 7)]
        [RepeatOnFail]
        [Timeout(1000)]
        [DependsOn("SelfExcl_TelebetCustomer_8590")]
        public void Search_SelfEx_Removed_Telebet_8594()
        {
            SxP.Verify_Search_SelfEx_Removed_Telebet();
        }
        [Test(Order = 8)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_Login_Casino_8593()
        {
            SxP.Verify_SelfExcl_Login_Casino();
        }
        [Test(Order = 9)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_Login_Lottos_8592()
        {
            SxP.Verify_SelfExcl_Login_Lottos_1112();
        }

        [Test(Order = 10)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void All_Product_Credit_SelfExc_Removed_Login_8589()
        {
            SxAd.Verify_All_Product_Credit_SelfExc_Removed_Login();
        }
        [Test(Order = 11)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_Login_Nelson_8588()
        {
            SxP.Verify_SelfExcl_Login_Sports();
        }
        [Test(Order = 12)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_Login_Exchange_8587()
        {
            SxP.Verify_SelfExcl_Login_Exchange_1136();
        }
        [Test(Order = 13)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_Login_Sports_8586()
        {
            SxP.Verify_SelfExcl_Login_Sports();
        }
        [Test(Order = 14)]
        [RepeatOnFail]
        [Timeout(1000)]
        public void SelfExcl_CreditCustomer_8564()
        {
            SxAd.Verify_SelfExcl_CreditCustomer();
        }
    }

    
}//Regression_IP2_Suite3

