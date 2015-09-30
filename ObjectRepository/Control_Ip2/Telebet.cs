using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class Telebet_Control
    {
       //search page
       public const string LoyalityMsg_XP = "//p[text()='User is a Loyalty Customer']";
       public const string NoCustFoundMsg_XP = "//p[text()='No customers match your search. Please try again']";
       public const string FrozenCustMsg_XP = "//p[contains(text(),'Player is frozen')]";
       public const string LockedCustMsg_XP = "//p[text()='Please Note: The player account is temporarily locked for online login due to exceeding the number of failed login attempts']";
       public const string searchAcct_ID = "searchAccount";

       //cust creation
      
       public const string PopUpClose_DialogOK_XP = "//a[@class='btn popupClose terms-ok']";
       public const string PopUpClose_DialogOKBtn_XP = " //a[@class='btn popupClose']";


       //betslip
       public const string FootBall_XP = "//a[@title='Football']";
       public const string FootBall_Betpage_XP = "id('breadcrumbText')/span[contains(text(),'SOCCER')]";
       public const string EventExpand_Colapse_XP = "//*[@class='marketExpandArrow']";
       public const string RandomSelection_XP = "//*[contains(@class,'marketPrice')]";
       public const string stakeAmt_ID = "0_0_win_stake";
       public const string CheckBet_ID = "betSlipControlCheckBet";
      public const string PlaceBet_ID = "betSlipControlPlaceBet";
       public const string Receipt_XP="id('popupContent')/p";
       public const string okButton_ID =   "okBtn";

       public const string BackToHome_XP = "//*[text()='Back To Telebet']";


    }
}
