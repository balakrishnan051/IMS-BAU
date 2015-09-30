namespace AdminSuite.CommonControls
{
    public class OB_Controls
    {
        #region CustomerDetails
        public const string FName = "//tr[td[contains(text(),'First Name')]]/td[2]";
        public const string lName = "//tr[td[contains(text(),'Last Name')]]/td[2]";
        public const string address = "//tr[td[contains(text(),'Address (1)')]]/td[2]";
        public const string city = "//tr[td[contains(text(),'City')]]/td[2]";
        public const string postal = "//tr[td[contains(text(),'Postcode')]]/td[2]";
        public const string RegChannel = "//tr[td[contains(text(),'Registration Channel')]]/td[2]";
        public const string custID = "//tr[td[contains(text(),'Customer Id:')]]/td[2]";
        public const string languageCD = "//tr[td[contains(text(),'Language code:')]]/td[2]";
        public const string mobile = "//tr[td[contains(text(),'Mobile:')]]/td[2]";
        public const string URN = "//tr[td[contains(text(),'URN')]]/td[2]";
        public const string URNMaster = "//tr[td[contains(text(),'(Master account)')]]/td[2]";
        public const string title = "//tr[td[contains(text(),'Title:')]]/td[2]";

        public const string country_ID = "id=addressCountry";
        public const string DOB_ID = "//tr[td[contains(text(),'Date of Birth')]]/td[2]/font";

        public const string email = "//tr[td[contains(text(),'Email')]]/td[2]/font";
        public const string sms = "//input[@name='contact_how_frag' and @value='S']";
        public const string telephone = "//font[@id='telephoneID']";
        public const string affliate_ID = "//tr[td[contains(text(),'Affiliate:')]]/td[2]";
        public const string affliate_EXT = "//tr[td[contains(text(),'ADM_EXT_AFF')]]/td[2]";
        #region EditCustomerDetails
        public const string updateRegistration = "//input[@value='Update Registration']";
        public const string updateEmail = "//input[@name='email']";
        public const string updateCustomer = "//input[@value='Update Customer']";
        public const string custTitle = "//th[contains(text(),'Customer Details')]";
        #endregion

        #endregion


    }
}
