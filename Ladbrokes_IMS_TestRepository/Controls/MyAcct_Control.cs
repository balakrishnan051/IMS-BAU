namespace TestRepository
{
    public class MyAcct_Control
    {
        public const string transferTab = "//a[contains(text(),'Transfer')]";
        public const string transferBtn = "//input[@value='Transfer']";
        public const string emailText = "//input[@name='email']";
        public const string passwordText = "//input[@name='password']";
        public const string updateDetailsBtn = "//input[@class='button_upddetails']";
        public const string successMsg = "//td[b[contains(text(),'Your details have been updated successfully.')]]";

        //deposit page
        public const string WalletType_ID = "acct_dropdown";
        //public const string fromAcct_Name = "from_acct";
        public const string amount = "id('section_CC')//td[@class='cpmfield']/input[@name='amount']";
        public const string cardtitle = "//td[contains(text(),'Card Number')]";
        public const string cardtext_name = "card_no";
        public const string cardCSC = "id('section_CC')//td[@class='cpmfield']//input[@name='card_csc']";
        public const string depositPassword = "id('section_CC')//td[@class='cpmfield']/input[@name='password']";
        public const string depositButton = "id('help_submit_section')//input[@value='Deposit']";
        public const string depsoitTransactionHeading = "//h1[contains(text(),'Card Deposit')]";
        //public const string depsoitAuthenticationHeading = "//h1[contains(text(),'Card Deposit - Cardholder Authentication.')]";
        public const string depositAuthHeading = "//h1[contains(text(),'ACS Simulator')]";
        public const string depsoitAuthenticateBTN = "//input[@value='Authenticated']";
        public const string depsoitAuthHeading_ID   = "acct_heading";
        public const string depsoitSuccessMsg_ID = "txn_confirmation";
        public const string withdrawButton = "id('help_submit_section')//input[@value='Withdraw']";
        public const string StartYear_Name = "StartYear";
        public const string ExpiryYear_Name = "ExpiryYear";
        public const string hldr_name = "id('section_CC')//td[@class='cpmfield']/input[@name='hldr_name']";
        public const string selectDepositType = "id('acct_body')//select[@name='from_cpm']";
        //public const string selectDepositType = "id('acct_dropdown')";

    }
}
