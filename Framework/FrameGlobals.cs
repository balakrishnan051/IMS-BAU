using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using ICE.ObjectRepository.Vegas_IMS_BAU;
using ICE.ObjectRepository;
using ICE.DataRepository;

namespace Framework
{
    public static class FrameGlobals
    {
        public static BrowserTypes BrowserToLoad;
        public static DateTime StartedTime = DateTime.MinValue;
        public static string LadbrokesIMSBase = null;
        public static string projectName = null;
        //Urls
        public static string BaseUrl1 = null;
        public static string BaseUrl2 = null;
        public static string BaseUrl3 = null;
        public static string BaseUrl4 = null;
        public static string BaseUrl5= null;
        public static string BaseUrl6 = null;
        public static string BaseUrl7 = null;
        public static string BaseUrl8 = null;
        public static string BaseUrl9 = null;
        public static string BaseUrl10 = null;
    

        public static string VegasURL = null;
        public static string BingoURL = null;
        public static string CasinoURL = null;
        public static string SportsURL = null;
        public static string EcomURL = null;
        public static string IMSURL = null;
        public static string PokerURL = null;
        public static string LiveDealerURL = null;
        public static string GamesURL = null;
        public static string OBUrl = null;
        public static string ExchangeURL = null;
        public static string telebet = null;
        public static string LottosURL = null;
          public static string  BackgammonURL = null;

        public static string IMSUname;
        public static string IMSPass;
        public static string AdminPass;
        public static string AdminName;
       
        //Used for Fiddler Integration
        private static Configuration _frameGlobalsConfig = null;
        public static string LadbrokesSplashPage = null;
        public static string PageLoadTimeOut = "200000";
        public static int generalTimeOut = 5000;
        public static int elementTimeOut = 20;
        public static int reloadTimeOut = 20;
        public const int TestCaseTimeOut = 300;
        public static string UseAgent = null;
        public static string userAgent = null;
        public static string userAgentValue = null;
        public static bool ThrowOnJSErrors = false;
        public static bool UseCulturePrompt = false;
        public static string ServiceAddress = "";
        public static bool ShowBrowser = true;
        public static bool ProdEnvironmentFlag = false;
        public static string useGrid = "no";
        public static int waitMilliSec_Reg = 0;
        public static int waitMilliSec_Screen = 0;
        public static Boolean rerunEnabled = true;
        public static int rerunCountBase = 2;
        public static int repeatCount = 1;
        public static int parallelismCount = 0;
        //mail
        public static string fromMail;
        public static string toMail;
        public static string mailPass;
        public static string body;
        public static string subject;
        public static string sender;
        public static string UseEmail;
        public static string Mode;


        static DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
        public static string LadbrokesIMSdirect;

        public static string ReadConfig(Configuration dllConfig, string name)
        {
            string value = null;
            try
            {
                value = dllConfig.AppSettings.Settings[name].Value;
            }
            catch (Exception) { }
            return value;

        }
        public static void Init()
        {
            ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
            string constFileName = System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase.ToLower(CultureInfo.CurrentCulture);
            constFileName = constFileName.Replace("file:///", "").Replace("/", "\\");
            constFileName = constFileName.Replace("%20", " ");
            ecf.ExeConfigFilename = constFileName.Replace("framework.dll", "testsettings.config");

            Configuration dllConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
            _frameGlobalsConfig = dllConfig;

            StartedTime = DateTime.Now;
            projectName = dllConfig.AppSettings.Settings["ProjectName"].Value;
            IMSUname = dllConfig.AppSettings.Settings["UserName1"].Value;
            IMSPass = dllConfig.AppSettings.Settings["Password1"].Value;

            AdminName = dllConfig.AppSettings.Settings["UserName2"].Value;
            AdminPass = dllConfig.AppSettings.Settings["Password2"].Value;
            
            

            BaseUrl1 = dllConfig.AppSettings.Settings["BaseURL1"].Value;
            BaseUrl2 = dllConfig.AppSettings.Settings["BaseURL2"].Value;
            BaseUrl3 = dllConfig.AppSettings.Settings["BaseURL3"].Value;
            BaseUrl4 = dllConfig.AppSettings.Settings["BaseURL4"].Value;
            if (ReadConfig(dllConfig, "BaseURL5") != null)
                BaseUrl5 = dllConfig.AppSettings.Settings["BaseURL5"].Value;
            if (ReadConfig(dllConfig, "BaseURL6") != null)
                BaseUrl6 = dllConfig.AppSettings.Settings["BaseURL6"].Value;
            if (ReadConfig(dllConfig, "BaseURL7") != null)
                BaseUrl7 = dllConfig.AppSettings.Settings["BaseURL7"].Value;
            if (ReadConfig(dllConfig, "BaseURL8") != null)
                BaseUrl8 = dllConfig.AppSettings.Settings["BaseURL8"].Value;
            if (ReadConfig(dllConfig, "BaseURL9") != null)
                OBUrl = dllConfig.AppSettings.Settings["BaseURL9"].Value;

            
           
                if (ReadConfig(dllConfig, "BaseURL10") != null)
                    if (FrameGlobals.projectName == "IP2")
                    telebet = dllConfig.AppSettings.Settings["BaseURL10"].Value;            
                else
                    GamesURL = dllConfig.AppSettings.Settings["BaseURL10"].Value;
                

            if (FrameGlobals.projectName == "IP2")
                GamesURL = "https://stg-games.ladbrokes.com/en";
            else
                telebet = "https://stg-gib.ladbrokes.com/telebet";

            BingoURL = BaseUrl1;
            BaseUrl1 = "http://www.google.com";
            CasinoURL = BaseUrl2;
            SportsURL = BaseUrl3;
            VegasURL = BaseUrl4;
            ExchangeURL = BaseUrl5;
            IMSURL = BaseUrl6;
            PokerURL = BaseUrl7;
            LiveDealerURL = BaseUrl8;

           // ExchangeURL = EcomURL + "/betexchange";
           // ExchangeURL = "http://test-sports.ladbrokes.com/en-gb/betexchange";
           // ExchangeURL = "http://uat-ecommerce-leeds.ladbrokes.com/exchange";
            
            //dllConfig.AppSettings.Settings["ladbrokes_DirectURL"].Value;
       

            fromMail = (dllConfig.AppSettings.Settings["FromMail"].Value);
            toMail = (dllConfig.AppSettings.Settings["ToMail"].Value);
            mailPass = (dllConfig.AppSettings.Settings["MailPass"].Value);
            body = (dllConfig.AppSettings.Settings["Body"].Value);
            if (body.Contains("\n"))
                body = body.Replace("\n", "<br/>");

            if (ReadConfig(dllConfig, "Subject") != null)
            subject = (dllConfig.AppSettings.Settings["Subject"].Value);
            sender = (dllConfig.AppSettings.Settings["Sender"].Value);

            PageLoadTimeOut = (dllConfig.AppSettings.Settings["TimeOut2"].Value);
            elementTimeOut = int.Parse(dllConfig.AppSettings.Settings["TimeOut3"].Value);
            reloadTimeOut = int.Parse(dllConfig.AppSettings.Settings["TimeOut4"].Value);
            generalTimeOut = int.Parse(dllConfig.AppSettings.Settings["TimeOut1"].Value) * 1000;
            //UseAgent = dllConfig.AppSettings.Settings["UseAgent"].Value;


            UseEmail = dllConfig.AppSettings.Settings["SendEmail"].Value.ToString();
            //// Getting the value from Config file
           // userAgent = dllConfig.AppSettings.Settings["DeviceType"].Value.ToString();
           // if (userAgent.ToUpper() == "NONE")
                UseAgent = "No";
          //  else
          //      UseAgent = "Yes";
            useGrid = dllConfig.AppSettings.Settings["useGrid"].Value.ToString();
            Mode = dllConfig.AppSettings.Settings["Mode"].Value.ToString();

            //if (dllConfig.AppSettings.Settings["RepeatOnFail"].Value.ToString().ToUpper() == "YES")
            //    rerunEnabled = true;
            //else
            //    rerunEnabled = false;

            repeatCount = int.Parse(dllConfig.AppSettings.Settings["RerunLimit"].Value.ToString()) + 1;
            parallelismCount = int.Parse(dllConfig.AppSettings.Settings["DegreeOfParalellism"].Value.ToString());

            string s = dllConfig.AppSettings.Settings["BrowserType"].Value;
            BrowserToLoad = (BrowserTypes)Enum.Parse(typeof(BrowserTypes), dllConfig.AppSettings.Settings["BrowserType"].Value.ToString(CultureInfo.InvariantCulture));


            if (projectName == "AccountsAndWallets Perf")
                DataFilePath.Accounts_Wallets = DataFilePath.Accounts_Wallets_perf;

            else if (projectName == "AccountsAndWallets Stg")
            {
                DataFilePath.Accounts_Wallets = DataFilePath.Accounts_Wallets_stg;
                DataFilePath.IP2_Authetication = DataFilePath.IP2_Stage;
                DataFilePath.IP2_SeamlessWallet = DataFilePath.Internal_SW_Stage;
            }
            else if (projectName == "IP2")
            {
                DataFilePath.IP2_Authetication = DataFilePath.IP2_Test;
                      DataFilePath.IP2_SeamlessWallet = DataFilePath.Internal_SW_Test;
            }

                BaseTest bt = new BaseTest();
                LottosURL = bt.ReadxmlData("Url", "lottos_url", DataFilePath.IP2_Authetication);
                BackgammonURL = bt.ReadxmlData("Url", "backgomman_url", DataFilePath.IP2_Authetication);
            



                #region userAgent
                // Assing values
                /*
                    IOS4
                    IOS5
                    Samsung - S2 (4.0.4)
                    Samsung - S4 (4.1.2)
                    HTC/LG (2.3.4)
                    iPod IoS6
                    iPad3
                    iPad Mini
                    Samsung Andriod Tab
                 */
                if (UseAgent == "Yes")
                {
                    switch (userAgent)
                    {
                        //iPhone ios4
                        case "ios4":
                            userAgentValue = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3 like Mac OS X; de-de) AppleWebKit/533.17.9 (KHTML, like Gecko) Mobile/8F190";
                            break;

                        //iPhone ios4s
                        case "ios4s":
                            userAgentValue = "Mozilla/5.0 (iPhone; CPU iPhone OS6_1_3 like Mac OS X)App|eWebKit/536.26 (KHTML, likeGecko) Version/6.0 Mobile/10B329Safari/8536.25";
                            break;

                        //iPhone ios5
                        case "ios5":
                            userAgentValue = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_0 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9A334 Safari/7534.48";
                            break;

                        case "ios6":
                            userAgentValue = "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25";
                            break;


                        case "ios7":
                            userAgentValue = "Mozilla/5.0 (iPhone; CPU iPhone OS 7_0 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53";
                            break;

                        // iPod IoS3
                        case "ipod":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 4.0.4; en-gb; GT-I9300 Build/IMM76D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
                            break;

                        //Nexus 4 (4.2.2)
                        case "nexus4":
                            userAgentValue = "Mozilla/5.0 (Linux; Android 4.2.2; Galaxy Nexus Build/JDQ39) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.123 Mobile Safari/537.22";
                            break;

                        //HTC pyramid/LG (2.3.4)
                        case "htc":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 2.3.3; zh-tw; HTC_Pyramid Build/GRI40) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1";
                            break;

                        //Samsung - S2 (4.0.3)
                        case "sams2":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 4.0.3; fr-fr; GT-I9100 Samsung Galaxy S2 Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
                            break;

                        //Samsung - S3 (4.1.1)
                        case "sams3":
                            userAgentValue = "Mozilla/5.0 (Linux; Android 4.1.1; SGH-T999 Build/JRO03L) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19";
                            break;

                        //Samsung - S4 (4.2.2)
                        case "sams4":
                            userAgentValue = "Mozilla/5.0 (Linux; Android 4.2.2; Nexus 4Build/JDQ39) App|eWebKit/535.19 (KHTML,like Gecko) Chrome/18.0.1025.166 MobileSafari/535.19";
                            break;

                        //Samsung - galaxy ace (2.2.1)
                        case "samace":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 2.2.1; en-gb; GT-S5830 Build/FROYO) AppleWebKit/525.10 (KHTML, like Gecko) Version/3.0.4 Mobile Safari/523.12.2";
                            break;

                        //Sony - experia arc (2.3)
                        case "sonyxarc":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 2.3; xx-xx; SonyEricssonLT26i Build/6.0.A.0.507) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1";
                            break;

                        // iPad Mini
                        case "ipadmini":
                            userAgentValue = "Mozilla/5.0 (iPad; CPU 08 6_1 like Mac OS X)AppIeWebKit/536.26 (KHTML, like Gecko)Version/6.0 Mobile/10B141 Safari/8536.25";
                            break;

                        // ipad 
                        case "ipad":
                            userAgentValue = "Mozilla/5.0 (iPad; U; CPU OS 4_2_1 like Mac OS X; ja-jp) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8C148 Safari/6533.18.5";
                            break;

                        // iPad2                        
                        case "ipad2":
                            userAgentValue = "Mozilla/5.0(iPad; U; CPU OS 4_3 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8F191 Safari/6533.18.5";
                            break;

                        // iPad5                        
                        case "ipad5":
                            userAgentValue = "Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5355d Safari/8536.25";
                            break;

                        // iPad6                        
                        case "ipad6":
                            userAgentValue = "Mozilla/5.0 (iPad; CPU OS 5_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko ) Version/5.1 Mobile/9B176 Safari/7534.48.3";
                            break;

                        //Samsung Tab
                        case "samtab":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 4.0.4; en-gb; GT-N8000Buildl|MM76D) App|eWebKitl534.30 (KHTML, like Gecko)Version/4.0 Safaril534.30";
                            break;
                    }
                }
            #endregion
            Initialize_OR();

        }//end Init
        public static void Initialize_OR()
        {


            ORFile XMLrep = new ORFile();
            XMLrep.initORPath();



        }

        public static double FractionToDouble(string fraction)
        {
            double result;

            if (double.TryParse(fraction, out result))
            {
                return result;
            }

            string[] split = fraction.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;

                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return (double)a / b;
                    }

                    int c;

                    if (int.TryParse(split[2], out c))
                    {
                        return a + (double)b / c;
                    }
                }
            }

            throw new FormatException("Not a valid fraction.");
        }
    }

    public enum BrowserTypes
    {
        InternetExplorer = 0,
        Firefox = 1,
        Chrome = 2
    }


}
