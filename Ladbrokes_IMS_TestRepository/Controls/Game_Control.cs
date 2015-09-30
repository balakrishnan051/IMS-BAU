namespace TestRepository
{
    public class Game_Control
    {
        public const string createAcct = "//a/span[contains(text(),'Create Account')]";

        public const string multiWheelGame = "//div[@class='pt-item winner-view' and h3[a[contains(text(),'Multiwheel Roulette')]]]";
        public const string multiWheeldemoPlay = "//a[contains(@class,'btn demo launcher') and @title='Multiwheel Roulette']";
        public const string multiWheelRealPlay = "//a[contains(@class,'btn launcher  play') and @title='Multiwheel Roulette']";

        //public const string DemoPlay_Game = "//li[@class='gm-item-wrap' and @data-name='Funky Fruits Farm' ]";
        //public const string DemoPlay_Button = "//a[contains(@class,'btn demo launcher') and @title='Funky Fruits Farm']";

        public const string DemoPlay_Game = "//li[@class='gm-item-wrap' and @data-name='Alien Hunter' ]";
        public const string DemoPlay_Button = "//a[contains(@class,'btn demo launcher') and @title='Alien Hunter']";


        public const string launcherAuthentication = "//a[@class='launcher image']";

        //public static string multiWheelGameLink = "//img[@src=\"http://cachevegas-stg.ladbrokes.com/library/games_images/vegas_games/multi_wheel_roulette.jpg\"]";
    }
}
