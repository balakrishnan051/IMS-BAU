using System.Collections.Generic;
namespace ICE.ObjectRepository.Vegas_IMS_BAU
{
    public class Login_Control
    {
        public static string loginBtn_Xpath = "//button[@title='Login']";
        public static string promptUserName_Xpath = "//input[@id='username']";
        public static string promptPassword_Xpath = "//input[@id='password']";
        public static string promptLoginBtn_Xpath = "//button[text()='Login']";
        public static string promptWindow_Xpath = "//div[@class='login-popup']";
        public static string modelWindow_Xpath = "id('playtechModalMessages')//button[@class='ok']";
        public static string modelWindow_OK_Xpath = "id('playtechModalMessages')//button[span[strong[containscontains(translate(.,'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'ok')]]]";
        public static string modelWindow_Decline_Xpath = "id('playtechModalMessages')//button[contains(@class,'decline')]";
        public static string modelWindow_Generic_Xpath = "id('playtechModalMessages')//button";
        public static string userDisplay_Xpath = "//span[@id='welcome']/strong";
        public static string msgAreaAlert_Xpath = "";
        public static string accept_Xpath = "";

        #region Mobile        
        public static string loadingIcon_Xpath = "";
        public static string loggOutMsgArea_Xpath = "";
        public static string bannerArea_Xpath = "";
        public static string bannerArea2_Xpath = "";
        public static string promoBtn_ID = "";
        public static string loginMenuLink_Xpath = "";
        public static string loginSpin_Xpath = "";
        
        #endregion


        public static void UpdateLoginControls(Dictionary<string, string> data)
        {

            loginBtn_Xpath = data["loginBtn_Xpath"].ToString();
            promptUserName_Xpath = data["promptUserName_Xpath"].ToString();
            promptPassword_Xpath = data["promptPassword_Xpath"].ToString();
            promptLoginBtn_Xpath = data["promptLoginBtn_Xpath"].ToString();
            promptWindow_Xpath = data["promptWindow_Xpath"].ToString();
            modelWindow_Xpath = data["modelWindow_Xpath"].ToString();
            userDisplay_Xpath = data["userDisplay_Xpath"].ToString();
            msgAreaAlert_Xpath = data["msgAreaAlert_Xpath"].ToString();
            accept_Xpath = data["accept_Xpath"].ToString();
            loadingIcon_Xpath = "//div[@class='splash-title' and contains(text(),'Loading')]";
            loggOutMsgArea_Xpath = "//span[@class='loggedOutMessage']";
            bannerArea_Xpath = "//div[@class='promotionsContainer']";
            bannerArea2_Xpath = "//div[@class='promotion_container']";
            promoBtn_ID = "footer_prom";
            loginMenuLink_Xpath = data["loginMenuLink_Xpath"].ToString();
            loginSpin_Xpath = data["loginSpin_Xpath"].ToString();

        }
    }
}
