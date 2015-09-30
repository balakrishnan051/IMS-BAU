using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE.ObjectRepository
{
   public class CashierPage
    {
       public const string CashierUser_element_name = "header_username";
       public const string Sofort_tab = "id('payment-type')/li[@data-submethodcode='SOFORT']/a";
        public const string Sofort_confirm_Id = "ctl00_mainContent_btnConfirm";
        public const string Sofort_Sortcode_Id = "TransactionsSessionSenderBankCode";
        public const string Sofort_Next_Btn = "id('WizardForm')//button";
        public const string Sofort_Acct_Number_Id = "BackendFormLOGINNAMEUSERID";
        public const string Sofort_PIN_Number_Id = "BackendFormUSERPIN";
        public const string Sofort_radiobtn_Id = "TransactionsSessionSenderAccountNumber12345678";
        public const string Sofort_Tan_Id = "BackendFormTan";
        public const string Sofort_close_lnk = "Close window";
        public const string Sofort_PayImage = "//td[contains(@onclick,'SOFORT') and @id='EnvoyPm']";
        public const string Sofort_withdraw_From_ID = "destinationWallet";
        public const string Sofort_withdraw_To_ID = "_cashieraccounts_WAR_CashierAccountsportlet_accountsCombo";
        public const string Sofort_Withdraw_Bank_Name_ID = "ctl00_mainContent_wizBankDetailsCapture_txtBankName3808452456D9E011B6450026B979425C";
        public const string Sofort_withdraw_BankName = "Demo Bank";
        public const string Sofort_Withdraw_Branch_Location_ID = "ctl00_mainContent_wizBankDetailsCapture_txtBranchAddress3808452456D9E011B6450026B979425C";
        public const string Sofort_Withdraw_BranchLocation = "Germany";
        public const string Sofort_Withdraw_Payee_ID = "ctl00_mainContent_wizBankDetailsCapture_txtCustomerName3808452456D9E011B6450026B979425C";
        public const string Sofort_Withdraw_Payee = "Warnecke Hans-Gerd";
        public const string Sofort_Withdraw_IBAN_ID = "ctl00_mainContent_wizBankDetailsCapture_txtIBAN3808452456D9E011B6450026B979425C";
        public const string Sofort_Withdraw_IBAN = "DE52000000000012345678";
        public const string Sofort_Withdraw_SWIFT_BIC_ID = "ctl00_mainContent_wizBankDetailsCapture_txtSWIFT3808452456D9E011B6450026B979425C";
        public const string Sofort_Withdraw_SWIFT_BIC = "SFRTDE20XXX";
        public const string Sofort_withdraw_submit_Btn_ID = "ctl00_mainContent_wizBankDetailsCapture_FinishNavigationTemplateContainerID_FinishCompleteButton";


        public const string Giropay_tab = "id('payment-type')/li[contains(@data-submethodcode,'GIROPAY')]/a";
        public const string Giropay_CustName_ID = "ctl00_ctl00_mainContent_ServiceContent_txtCustomerName";
        public const string Giropay_SwiftCode_ID = "ctl00_ctl00_mainContent_ServiceContent_txtGiroSWIFTCode";
        public const string Giropay_Confirm_ID ="ctl00_ctl00_mainContent_btnConfirm";
        public const string Giropay_Sc_Name = "sc";
        public const string Giropay_ExSc_Name = "extensionSc";
        public const string Giropay_submit_ID = "//input[@type='submit']";
        public const string GiroPay_PayImage = "EnvoyPm";

        public const string iDeal_tab = "id('payment-type')/li[@data-submethodcode='iDeal']/a";
       public const string iDeal_BnkName_ID = "ctl00_ctl00_mainContent_ServiceContent_ddlBanks";
       public const string iDeal_Proceed_ID = "ctl00_ctl00_mainContent_btnConfirm";
       public const string iDeal_Go_ID = "ctl00_mainContent_btnGo";
       public const string iDeal_PayImage = "//tr[contains(@id,'iDEAL')]/td[@id='EnvoyPm']/p/span";
       public const string iDeal_Submit_Name = "button.edit";

       public const string Skrill_tab_Xp = "id('payment-type')/li[@data-methodcode='Moneybookers' and not(@data-submethodcode='1-Tap')]/a";
       public const string Skrill_img_Xp = "//*[contains(text(),'Skrill')]";
       public const string Skrill_success_Xp = "//*[contains(text(),'Your deposit succeeded')]";
       public const string Used_CC_Text_name = "body";

       public const string Paypal_tab_Xp = "id('payment-type')/li[@data-methodcode='PayPal' and not(@data-submethodcode='1-Tap')]/a";
       public const string Paypal_img_Xp = "//*[contains(text(),'PayPal')]";

       public const string FundTransfer_From_ID = "dateRange_from";
       public const string FundTransfer_From_Today = "smdr-today";
       public const string FundTransfer_Transfer_From_ID = "select_14";
       public const string FundTransfer_Transfer_To_ID = "select_15";
       public const string FundTransfer_Transfer_Amt_path_ID = "amount";
       public const string FundTransfer_History_Submit_ID = "submit_14";
       

       //restricted IP
       public const string restrictedIP_Dep_Msg = "Ladbrokes does not accept Deposits from this territory. For further information please refer to our Restricted Territories article";
       public const string restricted_BannedCountry_Dep_Msg = "You are allowed to only Withdraw";
       

       //Deposit page

       public const string Close_DepSuccessPrompt_XP = "//button[contains(text(),'Ok')]";
       public const string WalletDropdown_Id = "_cashierdeposit_WAR_CashierDepositportlet_ftWalletNameCombo";

       //withdraw page
       public const string WithdrawMore_Error_XP = "//div[contains(text(),'The amount you wish to withdraw exceeds your current wallet balance')]";
    }
}
