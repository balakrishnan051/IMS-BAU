namespace AdminSuite.CustomerCreation
{

    /// <summary>
    /// Customer Controls
    /// </summary>
    ///  Author: Yogesh
    /// <param name>strUserName</param>
    /// Ex: 
    /// <returns>None</returns>
    /// Created Date: 22-Dec-2011
    /// Modified Date: 22-Dec-2011
    /// Modification Comments
    public class CustomersPage
    {

        /// <summary>
        /// Controls for Customer Search & Customer Cretaion @Admin-->Customers LHN link
        /// </summary>
        #region Customer Search Criteria
        public const string MainAreaFrame = "//frame[@name='MainArea']";
        public const string AddCustomerBtn = "//input[@type='button' and @value='Add Customer']";
        public const string CusSearUsernameTxtBx = "//th[contains(text(), 'Customer Search Criteria')]/following::tr[1]/td/input[@type='text' and @name='Username' and @value='']";
        public const string FindCustomerUnBtn = "//input[@value='Add Customer']/preceding-sibling::input[@type='button' and @value='Find Customer']";
        public const string ErrSrchCustNtFnd = "//td[contains(text(), 'No customers match your search criteria')]";
        #endregion

        /// <summary>
        /// Control for Add Customer page @Admin-->Customers LHN link-->AddCustomer Button
        /// </summary>
        #region Funding
        public const string AccDepLstBx = "//Select[@name='acct_type']";
        #endregion

        #region Shop Customer
        #endregion

        #region Security
        public const string EntrUsrNmeTxtBx = "//input[@name='username']";
        public const string EntrPwdTxtBx = "//input[@name='password']";
        public const string EntrPwdAgnTxtBx = "//input[@name='password2' and @type='password']";
        public const string SgnfiDateTxtBx = "//td[text()='Significant date']/following-sibling::td/input[@name='sig_date']";
        public const string Chlg1TxtBx = "//td[text()='Challenge 1']/following-sibling::td/input[@name='challenge_1']";
        public const string Chlg2TxtBx = "//td[text()='Challenge 2']/following-sibling::td/input[@name='challenge_2']";
        public const string Resp1TxtBx = "//td[text()='Response 1']/following-sibling::td/input[@name='response_1']";
        public const string Resp2TxtBx = "//td[text()='Response 2']/following-sibling::td/input[@name='response_2']";
        public const string RegChTxtBx = "//td[text()= 'Registration Channel']/following-sibling::td/select[@name='source']";
        #endregion

        #region Regional
        #endregion

        #region Name/Address
        public const string FNameTxtBx = "//td[text()= 'Forename']/following-sibling::td/input[@name='fname']";
        public const string LNameTxtBx = "//td[text()= 'Surname']/following-sibling::td/input[@name='lname']";
        public const string HouseNumTxtBx = "//td[text()= 'House Number']/following-sibling::td/input[@name='addr_street_1']";
        public const string Addr2TxtBx = "//td[text()= 'Address 2']/following-sibling::td/input[@name='addr_street_2']";
        public const string Addr3TxtBx = "//td[text()= 'Address 3']/following-sibling::td/input[@name='addr_street_3']";
        public const string Addr4TxtBx = "//td[text()= 'Address 4 ']/following-sibling::td/input[@name='addr_street_4']";
        public const string CityTxtBx = "//td[text()= 'City/Town']/following-sibling::td/input[@name='addr_city']";
        public const string PostCodeTxtBx = "//td[text()= 'Postcode']/following-sibling::td/input[@name='addr_postcode']";
        public const string CntryTxtBx = "//select[@id='country_code']";
        public const string TelDialCdeTxtBx = "//td[text()= 'Telephone']/following-sibling::td/input[@id='tel_dial_code']";
        public const string TelPhTxtBx = "//input[@id='tel_dial_code']/following-sibling::input[@id='telephone']";
        public const string MbDialCodeTxtBx = "//td[text()= 'Mobile']/following-sibling::td/input[@id='mob_dial_code']";
        public const string MbPhTxtBx = "//input[@id='mob_dial_code']/following-sibling::input[@id='mobile']";
        public const string EmailTxtBx = "//td[text()= 'Email']/following-sibling::td/input[@name='email']";
        #endregion

        #region Personal
        public const string GenderRadioButton = "//td[text()= 'Gender']/following-sibling::td/input[1][@name='gender']";
        public const string DobTxtBox = "//td[text()= 'Date of Birth']/following-sibling::td/input[1][@name='dob']";
        #endregion

        public const string AddNewCustomer = "//th[@class='buttons']/input[@value='Add Customer']";
        ///Creation of Customer controls ends here


        /// <summary>
        /// Control for Customer details page @Admin-->Customers LHN link-->AddCustomer Successfully takes us to Customer Details page
        /// Control for Customer detals page @Admin-->Customers LHN link-->Find Customer Successfully takes us Customer Details page
        /// </summary>      
        #region Self & Company Exclusion region in Cusomer Account Details page
        public const string CustomerExcluPeriodLstBx = "//input[@name='exclusion_period' and @value='1Y']";
        public const string CustomerExcluDateTxtBx = "name=exclusion_date";
        public const string CustomerSelfExlusionBtn = "name=addSelfExclusion";
        public const string CompanyExcludedCustomerBtn = "//input[@name='addCompanyExclusion']";
        public const string NoOfSelfExcludedCustomer = "//tbody/tr/th[contains(text(),'Customer Self Exclusion/Play-Break Attributes')]//ancestor::tbody/tr/td/a[contains(text(),'Remove')]";
        public const string NoOfCompanyExludedCustomer = "//tbody/tr/th[contains(text(),'Company Exclusion')]//ancestor::tbody/tr/td/a[contains(text(),'Remove')]";
        #endregion

        #region Suspend customer region in Cusomer Account Details page
        public const string CustomerStatusLstBx = "//select[@name='Status']";
        public const string ReasonForSuspendingUserLstBx = "//select[@name='StatusReasonType']";
        public const string ReasonForSuspendingUserTxtBx = "//input[@name='Reason']";
        public const string StatusReasonLstBx = "//select[@name='StatusReason']";
        public const string SuspCustomerTxt = "//td[text()='Suspended']";

        #endregion

        #region Update buttons in Cusomer Account Details page
        public const string UpdateRegistrationButton = "//input[@value='Update Registration']";
        public const string UpdateCustomerButton = "//input[@value='Update Customer']";
        #endregion

        public const string ViewCustFlag = "//th[@class='buttons']/input[@value='View Customer Flags']";

        //End of Customer controls

        #region CreditCard

        public const string CardNoSearchText = "name=card_no";
        public const string FindMethodsBTN = "//table[tbody[tr[th[contains(string(),'Card Search')]]]]//input[@value='Find Methods']";
        public const string PayMethodTitle = "//th[contains(text(),'Payment Methods')]";
        public const string activeCardOwner = "//td[3]/a";
        //public const string activeCardOwner = "//tr[last()]/td[1]/a";
        public const string CustDetailsTitle = "//th[contains(string(),'Customer Details')]";
        public const string username = "//tr[td[contains(text(),'Username')]]/td[2]";
        public const string activeCard = "//tr[td[contains(string(),'Active')]]/td[a[contains(text(),'Debit')]]/a";
        public const string usernameCardDetails = "//tr[td[contains(text(),'Username')]]/td[2]/a";
        public const string cardStatus = "//tr/td/select[@name='status']";
        public const string UpdatePaymentMethods = "//input[@value='Update Payment Method']";        
        #endregion

        #region CustVerification
        public const string CustVerificationBTN = "//input[@value='Customer Verification']";
        public const string manualPass = "//input[@value='A' or (@name='status' and contains(text(),'Pass'))]";        
        public const string ovScore = "//input[@name='ovs_score']";
        public const string ovRef = "//input[@name='ovs_reference']";
        public const string ovNote = "//input[@name='notes']";
        public const string creditCard = "//input[@name='cc']";
        public const string StoreVerificationBTN = "//input[@value='Store Verification']";
        public const string SucessMsg = "//b[contains(text(),'Verification check stored successfully for customer')]";
        
        #endregion

    }
}