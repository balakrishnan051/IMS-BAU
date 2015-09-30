using System.Collections.Generic;
namespace ICE.ObjectRepository.Vegas_IMS_BAU
{
    public class Game_Control
    {
        public static string createAcct_Xpath = "";
        public static string RealPlayGame_Xpath = "//a[@title='The Incredible Hulk' and contains(@class,'launcher')]/img";
        public static string multiWheeldemoPlay_Xpath = "";
        public static string PayBtn_RealPlay_Xpath = "//a[contains(@class,'play') and @title='The Incredible Hulk']";
        public static string DemoPlayGame_Xpath = "//a[contains(@title,'Spider-man') and contains(@class,'launcher')]/img";
        public static string DemoPlayButton_Xpath = "//a[contains(@class,'demo') and contains(@title,'Spider-man')]";
        public static string launcherAuthentication_Xpath = "";

        //Mobile

        public static string realPlayGame_Mob_Xpath = "";
        public static string realplayButton_Mob_ID = "";
        public static string RegisterLink_Mob_PartialLink = "";

        public static void UpdateGameControls(Dictionary<string, string> data)
        {
            createAcct_Xpath = data["createAcct_Xpath"].ToString();
            //multiWheelGame_Xpath = data["multiWheelGame_Xpath"].ToString();
            multiWheeldemoPlay_Xpath = data["multiWheeldemoPlay_Xpath"].ToString();
          //  multiWheelRealPlay_Xpath = data["multiWheelRealPlay_Xpath"].ToString();
            DemoPlayGame_Xpath = data["DemoPlayGame_Xpath"].ToString();
            DemoPlayButton_Xpath = data["DemoPlayButton_Xpath"].ToString();
            launcherAuthentication_Xpath = data["launcherAuthentication_Xpath"].ToString(); 

            //Mobile
            realPlayGame_Mob_Xpath = data["realPlayGame_Mob_Xpath"].ToString();
            realplayButton_Mob_ID = data["realplayButton_Mob_ID"].ToString();
            RegisterLink_Mob_PartialLink = data["RegisterLink_Mob_PartialLink"].ToString(); 
        }

     }
}
