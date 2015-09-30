using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class MyAcctPage
   {
       //myacct username
       public const string myAcctHeader_uname_Xp = "//div[@class='header-username']";
       
       //PlayerDetails
       public const string LoadingPrompt_XP = "//div[@class='loading-animation']";
      
       //Password change
       public const string PwdFormatMeter_XP = "//*[text()='not valid']";

       //header
       public const string MyAcct_History_XP = "//*[text()='Account History']";
        public const string PersonalDetails_XP = "//*[ text()='Personal details']";
       public const string MyAcct_ResponsibleGambling_lnk = "Responsible Gambling";
       public const string MyAcct_Free_Bets_lnk = "Free Bets";
       public const string myAcct_tab_Id = "my-account";

       //freebet
       public const string Freebet_promotionCode_name = "promotionCode";
       public const string Freebet_Submit_XP = "//button/span/strong[text()='Submit']";
       public const string Freebet_Success_XP = "//div[@class='freebets-response-message success']";
       public const string Freebet_reflection_XP = "//div[contains(@class,'your-rewards')]/table[contains(@class,'freebets')]//tr/td";


       //Acct History
       public const string SportsBetHistory_XP = "//label[contains(text(),'Sports bets history')]";
       public const string EventNameInHistory_XP = "//table[@id='account-history-table']//tr[1]//td[6]";


   }

   
}
