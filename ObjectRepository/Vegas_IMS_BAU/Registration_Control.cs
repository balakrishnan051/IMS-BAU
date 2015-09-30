using System.Collections.Generic;
namespace ICE.ObjectRepository.Vegas_IMS_BAU
{
    public class Registration_Control
    {

        #region Online       
        public static string createAcct_Xpath = "";
        public static string demoPlay_Xpath = "";
        public static string playNowBanner_Xpath = "";
        public static string regFrame_Xpath = "";
        public static string affliateDataSet_Xpath = "";
        public static string affliateIDSet_Xpath = "";
        public static string affliateExAFFSet_Xpath = "";
        public static string regConfirmationMsg_Xpath = "";       
        #endregion

        #region mobile
        public static string registrationWindow_Xpath = "";
        #endregion


        public static void UpdateGameControls(Dictionary<string, string> data)
        {
            createAcct_Xpath = data["createAcct_Xpath"].ToString();
            demoPlay_Xpath = data["demoPlay_Xpath"].ToString();
            playNowBanner_Xpath = data["playNowBanner_Xpath"].ToString();
            regFrame_Xpath = data["regFrame_Xpath"].ToString();
            affliateDataSet_Xpath = data["affliateDataSet_Xpath"].ToString();
            affliateIDSet_Xpath = data["affliateIDSet_Xpath"].ToString();
            affliateExAFFSet_Xpath = data["affliateExAFFSet_Xpath"].ToString();
            regConfirmationMsg_Xpath = data["regConfirmationMsg_Xpath"].ToString();

            //Mobile
            registrationWindow_Xpath = data["registrationWindow_Xpath"].ToString();

        }
    }
}
