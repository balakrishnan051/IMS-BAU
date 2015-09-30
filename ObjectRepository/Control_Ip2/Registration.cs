using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class Reg_Control
    {
       //regpage
       public const string regulatedCountry_Msg_XP = "//div[@class='message-description']";
       //banned
       public const string bannedCountry_Msg_XP = "id('registration')/div/p";
       //First Dep
       public const string CashierFrame_ID = "_cashier2iframe_WAR_cashierportlet_cashier2";
       public const string SelectPayment_Lbl_XP = "//span[text()='Select Payment Method']";
       
       //login
       public const string invalidLoginErr_PT_XP = "//div[@class='login-popup']//div[@class='portlet-msg portlet-msg-error']"; //text()='Either your username or password is incorrect. The account will be temporarily locked after 3 attempts. Please use the forgotten password link to reset your password.']";
       public const string invalidLoginErr_Ecom_XP =  "id('login-error-1')/div/p"; //[text()='Either your username or password is incorrect. The account will be temporarily locked after 3 attempts. Please use the forgotten password link to reset your password']";
       public const string invalidLoginErr_Lottos_ID = "loginErrorMessage"; //"//p[contains(text(),'Either your username or password is incorrect. The account will be temporarily locked after 3 attempts. Please use the forgotten password link to reset your password')]";
       public const string invalidLoginErr_Games_XP = "id('mainContainerLogin')/div[1]" ;  //[text()='Either your username or password is incorrect. The account will be temporarily locked after 3 attempts. Please use the forgotten password link to reset your password']";
       public const string invalidLoginErr_backgmon_XP = "loginErrorMessage";
       public const string invalidLogin_ModelBox_PT_XP = "id('playtechModalMessages')//div[contains(@class,'popup-content')]";
       
       public const string ClosedCust_PT_XP = "id('playtechModalMessages')//div[@class='message-area']";
      // public const string FrozenCust_PT_XP = "id('playtechModalMessages')//div[normalize-space()='This account has been frozen for security reasons. Please contact Customer Support at support@vegas.ladbrokes.com for further assistance. We apologize for any inconvenience caused.']";
       public const string SelExCust_PT_XP = "id('playtechModalMessages')/div[contains(normalize-space(),'Your account is currently excluded. If you believe this is incorrect, please e-mail')]";
       
       //restrictedIP
       public const string RestrictLogin_Games_XP = "//div[contains(text(),'Ladbrokes does not allow bets/activity from restricted territories')]";

       public const string ModelDialogOK = "d('playtechModalMessages')//button/span/strong[text()='OK']";

    }

   public class Portal_Control
   {

       //Header
       public const string Language_XP = "//a[@class='active main-toggle']/span";
       public const string Deutsch_XP = "//span[contains(text(),'Deutsch')]";
       public const string espanol_XP = "//span[contains(text(),'español')]";
       public const string irish_XP = "//span[contains(text(),'Interlingue (Ireland)')]";
       public const string svenska_XP = "//span[contains(text(),'svenska')]";

       public const string Vegas_Tab_XP = "id('subpage')//a[normalize-space()='Vegas']";
        public const string Casino_Tab_XP = "id('subpage')//a[normalize-space()='Casino']";
        public const string Bingo_Tab_XP = "id('subpage')//a[normalize-space()='Bingo']";
        public const string Poker_Tab_XP = "id('subpage')//a[normalize-space()='Poker']";

        public const string Customer_Menu_Id = "welcome";

        public const string myAcct_xpath = "//*[text()='My Account']";

        public const string username_name = "username";
   }
}
