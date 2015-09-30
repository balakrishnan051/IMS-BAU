using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ICE.DataRepository.Vegas_IMS_Data
{
    public class Registration_Data
    {
        public string fname = "Tester";
        public string lname = "QA";
        public string country_code = "United Kingdom";
        public string countryID = "GB";
        public string DOB = "1989-04-17";

        public const string uk_addr_street_2 = "Abcd";
        public string city = "Harrow";
        public string Mobcity = "Harrow";
        //public string RegChannel = "Hutchison";
        public string RegChannel = "Mobenga App";

        public string state = "Middx";
        public string fax = "12345";
        public string zip = "HA2 7JW";
        public string occupation = "xyz";
        public string currency = "GBP";


        public string uk_addr_street_1 = "Imperial Drive";
        public string uk_postcode = "HA2 7JW";
        public const string uk_postcd = "HA2 7JW";
        public const string other_postcode = "123446";
        public const string dob_day = "17";
        public static string title = "Mr";
        public static string depLimit = "5000";
        public const string securityQues = "Name of first school";
        public const string dob_month = "April";
        public const string dob_year = "1989";
        public string email = "test@playtech.com";
        public string houseNo = "12365";
        public string answer = "xyz";
        public string telephone = "12132183213";
        public string mobile = "12112119112";
        public string username = "User";
       // public static string password = "Lbr12345678";
        public string password = "Lbr123456";
        public const string response_1 = "xyz";
        public string registeredUsername = string.Empty;
        // public Dictionary<string,string> registeredUser = new Dictionary<string,string>();

        public const string affliateURL = "http://alfa.ladbrokes.com/tracking/trackingcookies.php";
        public const string affliateData = "AFF_TIMESTAMP|1385527483|ext_aff_tag|a_203379b_13554502c_d_4FD4D153988883FB93046267C7D87BEFe_1212|aff_id|93405";
        public const string affliateDataFF = "AFF_TIMESTAMP|1386753939|ext_aff_tag|a_203379b_13554487c_d_CCFBFCA33D759B50B7765B9EF50CF1FCe_livekunal|aff_id|93405";
        public const string affliateID = "93405";
        public const string affliateExAFF = "a_203379b_13554502c";
        public const string affliateExAFF_Native = "a_204226b_13534900c";
        public const string affliateProgram = "sportsbook";

      
        public void update_Registration_Data(string fsname, string country, string City, string passwd, string countrycode = "GB")
        {
            fname = fsname;
            country_code = country;
            city = City;
            password = passwd;
            countryID = countrycode;
        }
        public void update_temp_custDetails()
        {
            uk_addr_street_1 = "999";
            city = "Yeddm";
            uk_postcode = "HA2 7JW";
            email = "testerUpdt2@aditi.com";
            telephone = "99999823213";
            mobile = "88888782112";
        }
    }
}
