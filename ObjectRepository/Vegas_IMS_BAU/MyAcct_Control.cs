using System.Collections.Generic;
namespace ICE.ObjectRepository.Vegas_IMS_BAU
{
    public class MyAcct_Control
    {
        public static string transferTab_Xpath = "";
        public static string transferBtn_Xpath = "";
        public static string emailText_Xpath = "";
        public static string passwordText_Xpath = "";
        public static string updateDetailsBtn_Xpath = "";
        public static string successMsg_Xpath = "";

        //deposit page
        public static string WalletType_ID = "";
        //public static string fromAcct_Name = "";
        public static string amount_Xpath = "";
        public static string cardtitle_Xpath = "";
        public static string cardtext_Name = "";
        public static string cardCSC_Xpath = "";
        public static string depositPassword_Xpath = "";
        public static string depositButton_Xpath = "";
        public static string depsoitTransactionHeading_Xpath = "";

        public static string depositAuthHeading_Xpath = "";
        public static string depsoitAuthenticateBTN_Xpath = "";
        public static string depsoitAuthHeading_ID = "";
        public static string depsoitSuccessMsg_ID = "";
        public static string withdrawButton_Xpath = "";
        public static string StartYear_Name = "";
        public static string ExpiryYear_Name = "";
        public static string hldr_Name = "";
        public static string selectDepositType_Xpath = "";

        public static void UpdateVegasMyAcctControl(Dictionary<string, string> data)
        {
            transferTab_Xpath = data["transferTab_Xpath"].ToString();
            transferBtn_Xpath = data["transferBtn_Xpath"].ToString();
            emailText_Xpath = data["emailText_Xpath"].ToString();
            passwordText_Xpath = data["passwordText_Xpath"].ToString();
            updateDetailsBtn_Xpath = data["updateDetailsBtn_Xpath"].ToString();
            successMsg_Xpath = data["successMsg_Xpath"].ToString();


            WalletType_ID = data["WalletType_ID"].ToString();
            amount_Xpath = data["amount_Xpath"].ToString();
            cardtitle_Xpath = data["cardtitle_Xpath"].ToString();
            cardtext_Name = data["cardtext_Name"].ToString();
            cardCSC_Xpath = data["cardCSC_Xpath"].ToString();
            depositPassword_Xpath = data["depositPassword_Xpath"].ToString();
            depositButton_Xpath = data["depositButton_Xpath"].ToString();
            depsoitTransactionHeading_Xpath = data["depsoitTransactionHeading_Xpath"].ToString();

            depositAuthHeading_Xpath = data["depositAuthHeading_Xpath"].ToString();
            depsoitAuthenticateBTN_Xpath = data["depsoitAuthenticateBTN_Xpath"].ToString();
            depsoitAuthHeading_ID = data["depsoitAuthHeading_ID"].ToString();
            depsoitSuccessMsg_ID = data["depsoitSuccessMsg_ID"].ToString();
            withdrawButton_Xpath = data["withdrawButton_Xpath"].ToString();
            StartYear_Name = data["StartYear_Name"].ToString();
            ExpiryYear_Name = data["ExpiryYear_Name"].ToString();
            hldr_Name = data["hldr_Name"].ToString();
            selectDepositType_Xpath = data["selectDepositType_Xpath"].ToString(); 
        }
    }
}
