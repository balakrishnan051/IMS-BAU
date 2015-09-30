namespace TestRepository
{
    public class Registration_Control
    {

        #region Online
        //public const string createAcct = "//a/span[contains(text(),'Create Account')]";
        public const string createAcct = "//span[string()='Join']";
        public const string demoPlay = "//a[contains(@class,'btn demo launcher')]";
        public const string playNowBanner = "id('hp-top-banner-1')/a[@class='launcher hp-banner btn']";
        public const string regFrame = "//iframe[contains(@title,'LB reg')]";
       



        public const string affliateDataSet = "//tr[td[text()='AFF_DATA']]/td/input[@type='button']";
        public const string affliateIDSet = "//tr[td[text()='AFF_ID']]/td/input[@type='button']";
        public const string affliateExAFFSet = "//tr[td[text()='EXT_AFF']]/td/input[@type='button']";


        public const string regConfirmationMsg = "id('registerdiv')//div/p";
        //public static string multiWheelGameLink = "//img[@src=\"http://cachevegas-stg.ladbrokes.com/library/games_images/vegas_games/multi_wheel_roulette.jpg\"]";
        #endregion

        #region mobile
        public const string registrationWindow = "//span[contains(string(),'Regist')]";
        #endregion

    }
}
