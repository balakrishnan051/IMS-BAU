namespace TestRepository
{
    public class Login_Control
    {
        public const string loginBtn = "//button[@title='Login']";
        public const string promptUserName = "//form[@id='loginPopupForm']//input[@name='username']";
        public const string promptPassword = "//form[@id='loginPopupForm']//input[@name='password']";
        public const string promptLoginBtn = "//form[@id='loginPopupForm']//button[text()='Login']";
        public const string promptWindow = "//div[@class='header clear']/h2";
        public const string modelWindow = "id('playtechModalMessages')//button[@class='ok']";
        public const string userDisplay = "id('welcome')/strong";
        public const string MsgAreaAlert = "//div[@class='message-area']";
        public const string accept = "//button[@class='accept']";



        #region Mobile
        
        public const string loadingIcon = "//div[@class='splash-title' and contains(text(),'Loading')]";
        public const string loggOutMsgArea = "//span[@class='loggedOutMessage']";
        public const string bannerArea = "//div[@class='promotionsContainer']";
        public const string bannerArea2 = "//div[@class='promotion_container']";
        public const string promoBtn = "footer_prom";
        public const string loginMenuLink = "//td[p[contains(string(),'Log In')]]";
        public const string loginSpin = "//span[@class='c spinner spin']";
        
        #endregion
    }
}
