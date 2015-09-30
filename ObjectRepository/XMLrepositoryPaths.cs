using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ICE.ObjectRepository
{
    public class ORFile
    {
        static DirectoryInfo _currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
        public static string IMSBase = "IMSBase.xml";
        public static string IMSCommon = "IMSCommon.xml";
        public static string IMSCreateCustomer = "IMSCreateCustomer.xml";
        public static string IMSBonus = "IMSBonus.xml";
        public static string IMSSearchDatabase = "IMSSearchDatabase.xml";
        public static string IMSCustDetails = "IMSCustDetails.xml";


        public static string VegasMobile_Banking = "VegasMobile_Banking.xml";
        public static string VegasWeb_Registration = "VegasWeb_Registration.xml";
        public static string Accounts_Wallets_Registration = "Playtech_Registration.xml";
        public static string Accounts_Wallets_Banking = "Banking.xml";
        public static string Ladbrokes_Header = "Header.xml";
        public static string Accounts_Wallets_Mobile_Registration = "Mobile_Registration.xml";
        public static string Accounts_Wallets_Mobile_Banking = "Mobile_Banking.xml";

        public static string Poker_Banking = "Banking.xml";
        public static string Poker_Registration = "Regitration.xml";

        public static string Betslip = "Betslip.xml";

        public static string Telebet = "TB2.xml";


        public void initORPath()
        {
            string _testDataFilePath="";
            string folderMobile = "Mobile\\";
            string folderIMS = "IMS Admin\\";
            string folderVegas = "Vegas Portal\\";
            string AW = "Accounts_Wallets\\";
            string folderHeader = "Ladbrokes_Header\\";
            string folderPoker = "Poker\\";
            string telebetfolder = "Telebet\\";

            if (_currentDirPath.Parent != null) _testDataFilePath = _currentDirPath.Parent.FullName + "\\_output\\OR_Files\\";
            VegasMobile_Banking = _testDataFilePath + folderMobile + VegasMobile_Banking;
            VegasWeb_Registration = _testDataFilePath + folderVegas + VegasWeb_Registration;
            Accounts_Wallets_Registration = _testDataFilePath + AW+"\\Playtech_Registration.xml";
            
            Accounts_Wallets_Banking = _testDataFilePath + AW + Accounts_Wallets_Banking;
            Ladbrokes_Header = _testDataFilePath + folderHeader + Ladbrokes_Header;
            Accounts_Wallets_Mobile_Registration = _testDataFilePath + AW + Accounts_Wallets_Mobile_Registration;
            Accounts_Wallets_Mobile_Banking = _testDataFilePath + AW + Accounts_Wallets_Mobile_Banking;


            //ims admin
            IMSBase = _testDataFilePath + folderIMS + IMSBase;
            IMSCommon = _testDataFilePath + folderIMS + IMSCommon;
            IMSCreateCustomer = _testDataFilePath + folderIMS + IMSCreateCustomer;
            IMSBonus = _testDataFilePath + folderIMS + IMSBonus;
            IMSSearchDatabase = _testDataFilePath + folderIMS + IMSSearchDatabase;
            IMSCustDetails = _testDataFilePath + folderIMS + IMSCustDetails;
            Betslip = _testDataFilePath + AW + Betslip;

            //Poker
            Poker_Banking = _testDataFilePath + folderPoker + Poker_Banking;
            Poker_Registration = _testDataFilePath + folderPoker + Poker_Registration;

            Telebet = _testDataFilePath + telebetfolder+Telebet;
        }
    }

}
