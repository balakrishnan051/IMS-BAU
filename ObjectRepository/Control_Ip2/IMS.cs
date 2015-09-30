using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class IMS_Control_PlayerDetails
   {
       public class transaction
       {
           public const string SubmitPaymentID = "paymentrequest:submit";
       }

       #region PlayerDetails
       public const string eventlog_byUsername_XP = "//td[contains(text(),'See event')]/a[contains(text(),'username')]";
       public const string Main_Frame_ID = "main-content";
       public const string reset_Login_attempt_ID = "failedlogins";

       //Messages
       public const string LockRemovedMsg_XP = "//div[text()='Temporary lock has been removed.']";
       #endregion

       //selfexclusion
       public const string selfEx_Delete_XP = "id('selfExclusionGrid_table')/tbody/tr/td/button";
       public const string selfExType_Dropdown_XP = "id('selfExclusion_section')//td[3]/select";
       public const string self_Save_XP = "//input[contains(@value,'Save exclusion')]";

       //navigation
       public const string Creditcard_Tab_id = "creditcards_navigation";
       public const string Login_Tab_id = "loginsessions_navigation";

       //Approve WD request from Transaction section
      public const string WaitingWD_Request_XP ="id('transactions')//tr[td[text()='waiting'] and td[normalize-space()='withdraw']]/td[1]/a";
      public const string DeclinedWD_Request_XP = "id('transactions')//tr[td[text()='declined'] and td[normalize-space()='declined']]/td[1]/a";
      public const string WaitingDep_Request_XP = "id('transactions')//tr[td[text()='waiting'] and td[normalize-space()='deposit']]/td[1]/a";
      public const string approveBtn_id = "approve";
      public const string transText_id = "tranid";
      public const string confirmBtn_XP = "//input[@id='approve' and @value='Confirm']";
      public const string lastWDStatus = "id('transactions')//tr[td[  a[text()='2015-03-19 09:20:43'] ] and td[normalize-space()='withdraw']]/td[7]";

       //Login Message
      public const string ClientPlatform_XP = "id('loginsessions')//tr[2]/td[3]";
      public const string LoginTime_XP = "id('loginsessions')//tr[2]/td[1]";
      public const string LogoutTime_XP = "id('loginsessions')//tr[2]/td[2]";


       //Wallettransaction bet settle
      public const string walletTransaction_id = "playerWalletActions_navigation";
      public const string eventDate_id = "pwtCrit_eventDate_from";
      public const string eventDate_today_xp = "//li[text()='Today']";
      public const string bet_settle_link_xp = "//td[@class='remoteCode']/a";
      public const string eventCheck_id = "includeGameActionsCheck";
      public const string WalletSearch_id = "walletActionsSearchSubmit";
      #region TransactionQuery
      public const string BetSettle_TransactionQuery_XP = "//input[@value='Transaction Query']";
      public const string BetSettle_TxnsPerPage_XP = "//*[@value='List Transactions']";
      public const string BetSettle_BetWinnings_name = "BetWinnings";
      public const string BetSettle_BetRefund_name = "BetRefund";
      public const string BetSettle_BetWinLines_name = "BetWinLines";
      public const string BetSettle_BetLoseLines_name = "BetLoseLines";
      public const string BetSettle_BetVoidLines_name = "BetVoidLines";
      public const string BetSettle_BetComment_name = "BetComment";
      public const string BetSettle_Settle_XP = "//input[@value='Settle Bet']";
      public const string BetSettle_Success_XP = "//*[contains(text(),'This bet has been settled')]";
      public const string BetSettle_WinningAmt_XP = "//tr[td[text()='Winnings']]/td[2]";

      public const string BetSettle_Stake_XP = "//tr[td[text()='Stake']]/td[2]";


       //AccountSweep
      public const string usedpmenthods_ID = "usedpmethods_navigation";
      public const string SweepingAcct_ID = "pmaccountinfo:sweepingaccount";
      public const string UpdateSweep_ID = "pmaccountinfo:submitUpdate";
      public const string SweepSuccess_XP = "//*[contains(text(),'updated')]";

      #endregion

      public const string DirectMailValidation_Checkbox = "//td[@id='contact_preferences']//tr[2][td[input]]/td[input][4]/input";

   }

   public class IMS_Control_CasinoLog
   {
       #region View casino log
       public const string TimePeriod_cmb_ID = "period";
       public const string Show_Btn_ID = "show";
       public const string eventName_cmb_ID = "eventname";
       public const string Login_err_msg_XP = "//td[contains(text(),'Login - incorrect username or password')]";
       public const string SelfExclerr_msg_XP = "//td[contains(string(),'self excluded by an administrator')]";

       public const string SessionLogout_XP = "//tr[td[text()='session_logout']]/td[7]";
       public const string SessionLogin_XP = "//tr[td[text()='session_login']]/td[7]";
       
       #endregion


   }
   public class IMS_Control_Product
   {
       #region banned country/withdraw limit
       public const string Product_Tab_ID = "product_settings_link";
       public const string Casino_Lnk = "Casinos";
       public const string Signupcountries_Lnk = "Signup countries";
       public const string AllowedCountry_ID = "ac[]";
       public const string BannedCountry_ID = "bc[]";
       public const string RightArrow_ID = "clb1";
       public const string LeftArrow_ID = "clb2";
       public const string UpdateBannedList_ID = "saveSignupCountries";
       #endregion
   }
   public class IMS_Control_Rules
   {
       #region block IP
       public const string riskMgmtLink_ID ="risk_management_link";
       public const string automationRules_XP ="id('risk_management')//a[text()='Automation rules']";
        public const string Search_XP="//input[@value='Search']";
    
        public const string Condition_ID = "condition";
        public const string enable_XP = "//input[@value='Enable']";
        public const string Edit_XP = "//input[@value='Edit']";
        public const string save_XP = "//input[@value='Save in Active mode']";
        public const string disable_XP = "//input[@value='Disable']";
       public const string saveMsg_XP = "//div[text()='Rule saved in Active mode.']";
       public const string DisableMsg_XP = "//div[contains(text(),'successful')]";

       //Rules name 
       public const string LoginByIP_Lnk_Test = "Restricted Login by IP";
       public const string DepositByIP_Lnk_Test = "Restrict Deposit by IP";
       public const string DepositByBannedCountry_Lnk_Test = "Banned countries";
       public static string RegByIP_Lnk_Test = "Banned IP Freeze on Reg";

       public static string RegByIP_Lnk_Stage = "Banned countries Freeze upon SU";
       public const string LoginByIP_Lnk_Stage = "Banned countries for login";
       public const string DepositByIP_Lnk_Stage = "Ban deposits IP/ Registered country";
       public const string DepositByBannedCountry_Lnk_Stage = "re-14_block_deposit_transfer";
       #endregion


       //badData
       public const string BadData_AddNew_XP = "//input[@value='Add new']";
       public const string BadData_Uname_ID = "username";
       public const string BadData_LoginBan_ID = "accessdisable";
       public const string BadData_comments_ID = "comments";
       public const string BadData_AddBtn_ID = "create_baddata";

       public const string BadData_BadDetails_XP = "//td[normalize-space()='Bad details in account']/input";
       public const string BadData_LadbrokesPLC_XP = "//td[normalize-space()='Ladbrokes Plc']/input";
       public const string BadData_Ladbrokes_XP = "//td[normalize-space()='Ladbrokes']/input";
       public const string BadData_BadDataSearchPage_XP = "//div[contains(text(),'Bad data search')]";
       public const string BadData_CumpolsiveGambler_XP = "//td[normalize-space()='Compulsive gambler']/input";
   }

   public class IMS_Control_Report
   {
       public const string Monitoring_Rpt_ID = "monitoring__reporting_link";
       public const string Rpt_Viewer_Lnk = "Report viewer";
       public const string Rpt_Grp_ID = "reportgroups";
       public const string Rpt_code_ID = "reportcode";
       public const string Casino_ID = "casino";
       public const string submit_Name = "action[submit]";

       //restricted IP
       public const string enddate_ID = "enddate";
       public const string suedate_ID = "suedate";
      public const string ruleName_ID =  "rulename";
       
   }
}
