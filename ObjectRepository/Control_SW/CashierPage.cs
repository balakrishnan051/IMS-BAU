using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class Cashier_Control_SW
    {
       //Tab
       public const string CancelWithdraw = "//span[contains(text(),'Pending Withdrawals')]";


       //msgbox
       public const string TransactionNotification_OK_Dialog = "//strong[text()='OK']";
       
       //Creditcard:
       public const string redirectIframe = "depositRedirectIframe";


    }
}
