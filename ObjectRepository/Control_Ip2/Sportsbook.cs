using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class Sportsbook_Control
    {
       public class betslip
       {
           public const string Single_Stake_XP = "//input[@placeholder='Stake']";
           public const string Double_Stake_XP = "//div[div[span[contains(text(),'Doubles')]]]//input[@placeholder='Stake']";
           public const string Trixie_Stake_XP = "//div[div[span[contains(text(),'Trixie')]]]//input[@placeholder='Stake']";
           public const string Patent_Stake_XP = "//div[div[span[contains(text(),'Patent')]]]//input[@placeholder='Stake']";
           public const string Yankee_Stake_XP = "//div[div[span[contains(text(),'Yankee')]]]//input[@placeholder='Stake']";
           public const string Lucky_Stake_XP = "//div[div[span[contains(text(),'Lucky')]]]//input[@placeholder='Stake']";
           public const string Canadian_Stake_XP = "//div[div[span[contains(text(),'Canadian')]]]//input[@placeholder='Stake']";
           public const string Heinz_Stake_XP = "//div[div[span[contains(text(),'Heinz')]]]//input[@placeholder='Stake']";
           public const string Treble_Stake_XP = "//div[div[span[contains(text(),'Treble')]]]//input[@placeholder='Stake']";
           public const string Accumulator_Stake_XP = "//div[div[span[contains(text(),'Accumulator')]]]//input[@placeholder='Stake']";
           
           public const string Placebet_XP ="//button[text()='PLACE BET']";

           //Forecast
           public const string Forcast_Tab = "//div[@class='title-container' and contains(text(),'Forecast')]";
           public const string Forcast_Selections_XP = "//span[@class='forecast']//input[@data-position='any']";
           public const string Tricast_Selections_XP = "//span[@class='tricast']//input[@data-position='any']";
           public const string ForecastAndTricast_Notification_XP = "//span[@class='forecastTricastCombo']";
           public const string AddtoBetslip="//span[text()='Add to betslip']";
           public const string Forecast_Stake_XP = "//div[@class='selection-information' and div[div[contains(text(),'Forecast')]]]//input";
           public const string Tricast_Stake_XP = "//div[@class='selection-information' and div[div[contains(text(),'Tricast')]]]//input";

           public const string NotLoggedInError_XP = "//li[contains(text(),'To place a bet you must log in')]";
       }
       //login
       public const string username_Id = "username";
       public const string pwd_Id = "password";
       public const string login_btn_XP = "//button[string()='Log In']";
       public const string join_XP = "//button[contains(string(),'Join')]";

       //HomePage
       public const string Football_lnk_XP = "id('ul_lhn_master')//a[normalize-space(text())='Football']";
       public const string InvalidLoginError_XP = "id('header')//p";
       public const string Logout_XP = "//*[text()='Logout']";
       public const string Deposit_XP = "id('header')//div[@class='top']//div[contains(translate(.,'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'deposit')]";
       public const string LoggedinMenu_XP = "//div[@class='top']//*[@class='name']";

       //Betslip
       public const string Bet_UserName_Txt_ID = "lbr_betslip_login_username";
       public const string Bet_Pwd_Txt_ID = "lbr_betslip_login_password";
       public const string Bet_Login_Btn_XP = "//input[@value='Login']";
       public const string BetSlip_SingleEventName = "id('betslipSingle0')//div[@class='event']";
       public const string BetSlip_wait = "//div[@class='countdown-container']/div[@class='countdown-container-message-1']";

       public const string BetSuccess1_xp = "//*[contains(text(),'placed successfully')]";
       public const string BetSuccess2_xp = "//div[contains(text(),'Your bet has now been placed')]";

       public const string OddText_Betslip = "id('betslipSingle0')//span";
       public const string oddList = "//div[contains(@class,'odds')]/span";
       
        //freebet
       public const string freebet_checkbox_xp = "id('freebets')//div[contains(normalize-space(),'TEST') and @class='greybox']//input";

       //Change Odd settings
       public const string SettingsMenu_XP = "//div[@class='icon-settings']";
       public const string Fractional_XP = "//input[@value='fractional']";
       public const string Decimal_XP = "//input[@value='decimal']";
    }
}
