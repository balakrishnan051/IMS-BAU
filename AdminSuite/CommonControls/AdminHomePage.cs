namespace AdminSuite.CommonControls
{
    /// <summary>
    /// Left hand navigation Controls in Admin home page
    /// </summary>
    ///  Author: Yogesh
    /// <param name=></param>
    /// Ex: 
    /// <returns>None</returns>
    /// Created Date: 22-Dec-2011
    /// Modified Date: 22-Dec-2011
    /// Modification Comments
  public  class AdminHomePage
    {
        ///<summary>
        ///Controls for Admin Login page
        ///</summary>
        #region Admin Login Page
        public const string UsrNmeTxtBx = "id=username";
        public const string PwdTxtBx = "id=password";
        public const string LoginBtn = "//input[@type='submit' and @value='Login']";
        #endregion

        /// <summary>
        /// Controls for link element @ LHN->Queries-->Customers
        /// </summary>
        #region Queries in LHN
        //Control for link element @ LHN->Queries-->Customers
       // public const string CustomersLink = "//li[a[text()='Pools bet']]/following-sibling::li[a[text()='Customers']]";
        public const string CustomersLink = "link=Customers";
        public const string PaymentLink = "link=Pay Methods";
        #endregion

        /// <summary>
        /// All the links in the Left Hand Frame of Admin Home Page
        /// </summary>
        #region Admin Home Page> Left Hand Bar Links
        public const string EventNameLink = "link=Events";
        public const string EventClassesLink = "//li[@class='menu_item']/a[@class='menu_item' and text()='Event Classes']";
        public const string EventClassesName = "//a[text()='|Football|']";
        #endregion

        /// <summary>
        /// Events Search Page
        /// Go to Admin Home Page and Click on Events Link in the left Hand frame
        /// </summary>
        #region Admin HomePage>Events Page
        public const string OpenBetIdTextBox = "name=openbet_id";
        public const string OpenBetHierarchyLevelDrpLst = "name=hierarchy_level";
        public const string OpenBetHeierarchyName = "label=Even outcome";
        public const string EventFindBtn = "//input[@value='Find']";
        public const string OpenBetHeierarchyLevelEvent = "label=Event";
        public const string ResultStatus = "//select[@name='OcResult']";
        #endregion

        #region Customer Home Page
        public const string custVerifyCountry = "//select[@name='country']";
        #endregion

        #region freebettocken
        public const string Reward_Link = "link=Reward Adhoc token";
        public const string uname_name = "Username";
        public const string Search_user_XP = "//input[@type='Submit']";
        public const string SubmitToken_XP = "//input[@value='Reward token']";
        public const string successToken_XP = "//b[text()='Token Rewarded']";
        public const string Relative_name = "RelativeEx";
        public const string TokenValue_name = "Value";
        #endregion

        #region TransactionQuery
        public const string BetSettle_TransactionQuery_XP = "//input[@value='Transaction Query']";
        public const string BetSettle_TxnsPerPage_XP = "//*[@value='List Transactions']";
        public const string BetSettle_BetWinnings_name = "BetWinnings";
        public const string BetSettle_BetRefund_name = "BetRefund";
        public const string BetSettle_BetWinLines_name = "BetWinLines";
        public const string BetSettle_BetLoseLines_name = "BetLoseLines";
        public const string BetSettle_BetVoidLines_name = "BetVoidLines";
        public const string BetSettle_BetComment_name = "BetComment";
        public const string BetSettle_Settle_XP = "//input[@value='Settle Bet']";
        public const string BetSettle_Success_XP = "//*[contains(text(),'This bet has been settled')]";
        public const string BetSettle_WinningAmt_XP = "//tr[td[text()='Winnings']]/td[2]";
        #endregion
    }
}
