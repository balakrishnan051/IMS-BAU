using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class Ecomm_Control
    {
       //HomePage
       public const string SelfEx_XP = "//p[text()='Your account is currently excluded. If you believe this is incorrect, please e-mail self exclusion@ladbrokes.com']";
       public const string Username_ID = "loginusername";
       public const string Pwd_ID = "loginPassword";
       public const string LoginBtn_ID = "submit-header";
       

       //quick bet
       public const string DepositBtn_Betslip_XP = "//a[text()='Deposit funds']";
       public const string qDep_Cvv_ID = "gdCvv2";
       public const string qDep_DepBtn_ID = "depositButton";
       public const string qDep_successmsg_XP = "//h1[contains(text(),'Your deposit was successful!')]";
       public const string qDep_close_ID = "btn-close";

       public const string BetSlip_Username_ID = "F-Betslip-Username";
       public const string BetSlip_Pwd_ID = "F-Betslip-Password";
       public const string BetSlip_Submit_ID = "F_Betslip-Submit";

       public const string exchange_Logout = "Logout";
       public const string exchange_Join = "//a[contains(text(),'Create Account')]";
    }
}
