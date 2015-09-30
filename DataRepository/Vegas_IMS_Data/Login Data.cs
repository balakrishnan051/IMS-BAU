using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.DataRepository.Vegas_IMS_Data
{
    public class Login_Data
    {

        public string username = "test_aditi_shaik1";
        public string password = "123456";
        public string fname = "srini";
        public const string  self_Cust_error = "self exclusion";
        public const string sus_Cust_error = "account not active";
        public static int loginAttempt = 3;

        public void update_Login_Data(string uname,string pass,string fname1=null)
        {
            username = uname;
            password = pass;
            fname = fname1;
        }


    }
}
