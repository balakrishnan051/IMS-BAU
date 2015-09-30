using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class Lottos_Control
    {
       //HomePage
       public const string uname_ID = "tbUsername";
       public const string pwdClr_ID = "password-clear";
       public const string pwd_ID = "tbPassword";
       public const string login_XP = "//a[contains(text(),'Login')]";
       public const string logout_lnk = "Logout";
       public const string depositLink = "//span[contains(text(),'Deposit')]";
    }
   public class Games_Control
   {
       //HomePage
       public const string uname_ID = "tbUsername";
       public const string pwd_ID = "tbPassword";
       public const string login_XP = "//*[text()='Login']";
       public const string logout_lnk = "Logout";
       public const string depositLink = "//*[text()='Deposit']";
       public const string userMenu = "id('fmLogin')//span/span";

       //Header
       public const string Language_XP = "//img[@class='sel-lang-img']";
       public const string Deutsch_XP = "//a[@title='German']";
       public const string espanol_XP = "//a[@title='Swedish']";
       public const string irish_XP = "//a[@title='Irish']";
   }

}
