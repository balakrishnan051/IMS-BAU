using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.DataRepository.Vegas_IMS_Data
{
    public class MyAcct_Data
    {
        public double transferAmmt = 1;
        public string walletAll = "Sports;Gaming;Casino;Vegas;Exchange";
        public static string mainWallet = "Vegas";

        public string depositWallet = "Sports";
        public string withdrawWallet = "Sports";
        

        public string card = "5573480000000133";
        public string cardCSC = "133";
        public string depositAmt = "15";
        public static string startYear = "2012";
        public static string endYear = "2016";
        public string paymentType = "Credit/Debit Card";
        public string max_amt = "500";
        public string wallet1 = "Sportsbook";
        public string wallet2 = "Gaming";
        public string wallet3 = "Games";

        public string paypalUser = "mehaksharma001@gmail.com";
        public string paypalPwd = "Aditi01*";

        public string skrillUser = "lad.brokestester@gmail.com";
        public string skrillPwd = "ladgbp@mb";



        public string acctID,SortCode,PIN;
        public string SwiftBIC, sc, esc;
        public string wPayee, wBankName, wIBAN, wSwift_BIC, wCountry;



        public void update_csc()
        {
            cardCSC = card.ToString().Substring(card.Length-3);
        }
        public void update_Wallet_Data(string amt,string walletTypes,string wallet)
        {
            transferAmmt = int.Parse(amt);
            walletAll = walletTypes;
            mainWallet = wallet;

        }
        public void update_Wallets_Name(string Wallet1, string Wallet2 = null)
        {
            wallet1 = Wallet1;
            wallet2 = Wallet2;


        }
        public void Update_deposit_withdraw_Card(string cardD, string maxAmt, string depsAmt, string wallet, string withdrawWallets,string Cvv)
        {
            card=cardD;
            cardCSC = Cvv;
            paymentType = "Credit/Debit Card";
            max_amt = maxAmt;
            depositAmt = depsAmt;
            depositWallet = wallet;
            withdrawWallet = withdrawWallets;
        }
        public void Withdraw_Sofort(string Payee, string Bank_Name, string IBAN, string Swift_BIC, string country, string depsAmt, string withdrawWallets)
        {
            depositAmt = depsAmt;
            withdrawWallet = withdrawWallets;
            wPayee = Payee;
            wBankName = Bank_Name;
            wIBAN = IBAN;
            wSwift_BIC = Swift_BIC;
            wCountry = country;
            
        }
        public void Update_deposit_paypal_account(string userid, string userpwd, string depsAmt, string wallet)
        {
            paypalUser = userid;
            paypalPwd = userpwd;
            paymentType = "Paypall method";
            depositAmt = depsAmt;
            depositWallet = wallet;
        }
        public void Update_deposit_sofort_account(string acct, string pin, string sort, string depsAmt, string wallet)
        {
            acctID = acct;
            PIN = pin;
            SortCode = sort;
            depositAmt = depsAmt;
            depositWallet = wallet;
        }

        public void Update_deposit_giropay_account(string swif, string sc1, string esc1, string depsAmt, string wallet)
        {
            SwiftBIC = swif;
            sc = sc1;
            esc = esc1;
            depositAmt = depsAmt;
            depositWallet = wallet;
        }
        public void Update_deposit_Ideal_account(string depsAmt, string wallet)
        {
            
            depositAmt = depsAmt;
            depositWallet = wallet;
        }


        public void Update_deposit_skrill_account(string userid, string userpwd, string depsAmt, string wallet)
        {
            skrillUser = userid;
            skrillPwd = userpwd;
            depositAmt = depsAmt;
            depositWallet = wallet;
        }
    }
}
