using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Enums;
using A2_NWBA.Code.Logic;

namespace A2_NWBA
{
    public partial class Default : System.Web.UI.Page
    {
        #region ViewState Data
        private Customer CustomerDataSource
        {
            get
            {
                return ViewState["CustomerDataSource"] as Customer;
            }
            set
            {
                ViewState["CustomerDataSource"] = value;
            }
        }

        private AccountList AccountsDataSource
        {
            get
            {
                return ViewState["AccountsDataSource"] as AccountList;
            }
            set
            {
                ViewState["AccountsDataSource"] = value;
            }
        }

        private decimal? FeeAmount
        {
            get { return ViewState["FeeAmount"] as decimal?; }
            set { ViewState["FeeAmount"] = value; }
        }

        #endregion

        #region Page Loading & Data Binding

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInCustomer"] == null){
                    Response.Redirect("~/Login");
            }

            ATMSubNavUc.SubMenuItemSelected += new EventHandler(ATMSubNavControl_SubMenuItemSelected); 

            if (!IsPostBack) {
                CustomerDataSource = (Customer)Session["LoggedInCustomer"];
                AccountsDataSource = AccountManager.GetAccountsByCustomer(CustomerDataSource);

                FeeAmount = Decimal.Parse(ConfigurationManager.AppSettings["Fee_ATMTransaction"]);
                BindData();
            }
        }

        protected void BindData()
        {

            DepositAccDdl.DataTextField = TransferFromDdl.DataTextField = WithdrawAccountDdl.DataTextField = "AccountNumber";
            DepositAccDdl.DataValueField = TransferFromDdl.DataValueField = WithdrawAccountDdl.DataValueField = "AccountNumber";
            DepositAccDdl.DataSource = TransferFromDdl.DataSource = WithdrawAccountDdl.DataSource = AccountsDataSource;

            DepositAccDdl.DataBind();
            TransferFromDdl.DataBind();
            WithdrawAccountDdl.DataBind();

            ListItem headerItem = new ListItem("Please choose an Account");

            DepositAccDdl.Items.Insert(0, headerItem);
            TransferFromDdl.Items.Insert(0, headerItem);
            WithdrawAccountDdl.Items.Insert(0, headerItem);

        }

        protected void ReLoadAccountsDataSource()
        {
            this.AccountsDataSource = AccountManager.GetAccountsByCustomer(CustomerDataSource);
            BindData();
        }

        #endregion

        #region Button Clicks/Events

        /// <summary>
        /// Handles a valid page being posted pack and calls appropriate transaction code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitTransactionBtn_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                int pageMode = -1;
                TransactionOutputLbl.Text = "";

                try{
                    pageMode = Int32.Parse(ATMSubNavUc.ModeIndex.ToString());
                }
                catch (FormatException) { }

                if(pageMode == (int)Code.Enums.Enums.ATMMode.Deposit){
                    
                    decimal amount = 0;
                    int toAccountNum = 0;

                    try
                    {
                        amount = Decimal.Parse(DepositAmountTb.Text);
                        toAccountNum = Int32.Parse(DepositAccDdl.SelectedValue);
                        DepositTransaction(toAccountNum, amount);
                    }
                    catch (FormatException) 
                    {
                        TransactionOutputLbl.Text = "An Error has occured whilst attempting this Deposit Transaction. Please try again.";
                    }
                }
                else if(pageMode == (int)Code.Enums.Enums.ATMMode.Transfer){

                    decimal amount = 0;
                    int fromAccountNum = 0;
                    int toAccountNum = 0;

                    try
                    {
                        amount = Decimal.Parse(TransferAmountTb.Text);
                        fromAccountNum = Int32.Parse(TransferFromDdl.SelectedValue);
                        toAccountNum = Int32.Parse(TransferToAccountNumberTb.Text);
                        TransferTransaction(fromAccountNum, toAccountNum, amount);
                    }
                    catch (FormatException)
                    {
                        TransactionOutputLbl.Text = "An Error has occured whilst attempting this Transfer Transaction. Please try again.";
                    }
                }
                else if (pageMode == (int)Code.Enums.Enums.ATMMode.Withdraw)
                {
                    decimal amount = 0;
                    int fromAccountNum = 0;

                    try
                    {
                        amount = Decimal.Parse(WithdrawalAmountTb.Text);
                        fromAccountNum = Int32.Parse(WithdrawAccountDdl.SelectedValue);
                        WithdrawalTransaction(fromAccountNum, amount);
                    }
                    catch (FormatException)
                    {
                        TransactionOutputLbl.Text = "An Error has occured whilst attempting this Withdrawal Transaction. Please try again.";
                    }
                }
            }
        }

        

        /// <summary>
        /// Handles the binding of the controls for each type of transaction that can be performed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
 
        private void ATMSubNavControl_SubMenuItemSelected(object sender, EventArgs e)
        {
            TransactionOutputLbl.Text = WithdrawalAccountBalanceLbl.Text = TransferFromAccountBalanceLbl.Text = "";
            TransferFromDdl.SelectedIndex = WithdrawAccountDdl.SelectedIndex = DepositAccDdl.SelectedIndex = 0;

            if (this.ATMSubNavUc.ModeIndex == (int?)Code.Enums.Enums.ATMMode.Deposit)
            {
                InitialPh.Visible = TransferPh.Visible = WithdrawPh.Visible = false;
                DepositPh.Visible = BtnRowPh.Visible = true;
            }
            else if (this.ATMSubNavUc.ModeIndex == (int?)Code.Enums.Enums.ATMMode.Transfer)
            {
                InitialPh.Visible = DepositPh.Visible = WithdrawPh.Visible = false;
                TransferPh.Visible = BtnRowPh.Visible = true;
            }
            else if (this.ATMSubNavUc.ModeIndex == (int?)Code.Enums.Enums.ATMMode.Withdraw)
            {
                InitialPh.Visible = DepositPh.Visible = TransferPh.Visible = false;
                WithdrawPh.Visible = BtnRowPh.Visible = true;
            }
            else
            {
                DepositPh.Visible = TransferPh.Visible = WithdrawPh.Visible = BtnRowPh.Visible = false;
                InitialPh.Visible = true;
            }
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            DepositAmountTb.Text = DepositCommentTb.Text = "";
            TransferAmountTb.Text = TransferToAccountNumberTb.Text = TransferCommentTb.Text = "";
            WithdrawalAmountTb.Text = WithdrawalCommentTb.Text = "";
            TransferFromAccountBalanceLbl.Text = WithdrawalAccountBalanceLbl.Text = "";

            DepositAccDdl.SelectedIndex = WithdrawAccountDdl.SelectedIndex = TransferFromDdl.SelectedIndex = 0;
        }

        protected void WithdrawAccountDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            WithdrawalAccountBalanceLbl.Text = "";

            if(WithdrawAccountDdl.SelectedIndex != 0){
                
                int selectedAccountNumber = Int32.Parse(WithdrawAccountDdl.SelectedValue);
                
                foreach(Account acc in AccountsDataSource)
                {
                    if(selectedAccountNumber == acc.AccountNumber)
                    {
                        WithdrawalAccountBalanceLbl.Text = String.Format("${0}", Math.Round(acc.AvailableBalance, 2));
                        break;
                    }
                }
            }
            else
            {
                WithdrawalAccountBalanceLbl.Text = "";
            }
        }

        protected void TransferFromDdl_SelectedIndexChanged(object sender, EventArgs e)
        {

            TransferFromAccountBalanceLbl.Text = "";

            if (TransferFromDdl.SelectedIndex != 0)
            {
                int selectedAccountNumber = Int32.Parse(TransferFromDdl.SelectedValue);
                foreach (Account acc in AccountsDataSource)
                {
                    if (selectedAccountNumber == acc.AccountNumber)
                    {
                        TransferFromAccountBalanceLbl.Text = String.Format("${0}", Math.Round(acc.AvailableBalance, 2));
                        break;
                    }
                }
            }
            else
            {
                TransferFromAccountBalanceLbl.Text = "";
            }
        }

        protected void DepositTransaction(int AccountNumber, decimal DepositAmount)
        {
            Account FromAccount = null;

            foreach (Account acc in AccountsDataSource)
            {
                if (acc.AccountNumber == AccountNumber)
                {
                    FromAccount = acc;
                    break;
                }
            }

            if (FromAccount != null)
            {
                AccountManager.Deposit(FromAccount, DepositAmount, DepositCommentTb.Text);
                TransactionOutputLbl.Text = "Deposit Transaction Successful";
                DepositAmountTb.Text = "";
                DepositCommentTb.Text = "";
                ReLoadAccountsDataSource();
            }
            else
            {
                TransactionOutputLbl.Text = "Deposit Transaction Failed. Please try again.";
            }
        }

        protected void WithdrawalTransaction(int FromAccountNumber, decimal WithdrawAmount)
        {
            Account FromAccount = null;
            

            foreach (Account acc in AccountsDataSource)
            {
                if (acc.AccountNumber == FromAccountNumber)
                {
                    FromAccount = acc;
                    break;
                }
            }

            if (FromAccount != null)
            {
                if (FromAccount.AvailableBalance >= WithdrawAmount) 
                { 
                    if ((FromAccount.AvailableBalance - WithdrawAmount - (decimal)FeeAmount) >= FromAccount.MinimumBalanceAllowed)
                    {
                        AccountManager.Withdraw(FromAccount, WithdrawAmount, WithdrawalCommentTb.Text);
                        TransactionOutputLbl.Text = "Withdrawal Transaction Successful!";
                        WithdrawalAmountTb.Text = WithdrawalCommentTb.Text = WithdrawalAccountBalanceLbl.Text = "";
                        ReLoadAccountsDataSource();
                    }
                    else
                    {
                        string accountType = FromAccount.Type == (int)Code.Enums.Enums.AccountType.Cheque ? "Cheque" : "Savings";
                        TransactionOutputLbl.Text = String.Format("Withdrawal aborted. {0} accounts require a minimum balance of ${1}. Overwithdrawing an account (even with fees) is prohibited.", accountType, Math.Round(FromAccount.MinimumBalanceAllowed, 2));
                    }
                }
                else
                {
                    TransactionOutputLbl.Text = String.Format("You have insufficient funds in this account to perform this transaction. Current account balance is: ${0}", Math.Round(FromAccount.AvailableBalance, 2));
                }
            }
        }

        protected void TransferTransaction(int FromAccountNumber, int ToAccountNumber, decimal TransferAmount)
        {
            Account FromAccount = null;

            if (ToAccountNumber == FromAccountNumber)
            {
                TransactionOutputLbl.Text = "Transfer Aborted! Can not transfer funds to the same account.";
                BindData();
                return;
            }

            foreach (Account acc in AccountsDataSource)
            {
                if (acc.AccountNumber == FromAccountNumber) 
                {  
                    FromAccount = acc;
                    break;
                }
            }

            if (FromAccount != null)
            {
                if (FromAccount.AvailableBalance >= TransferAmount)
                {
                    if ((FromAccount.AvailableBalance - TransferAmount - (decimal)FeeAmount) >= FromAccount.MinimumBalanceAllowed)
                    {
                        AccountManager.Transfer(FromAccount, ToAccountNumber, TransferAmount, TransferCommentTb.Text);
                        TransactionOutputLbl.Text = "Transfer Transaction Successful!";
                        TransferAmountTb.Text = TransferCommentTb.Text = TransferFromAccountBalanceLbl.Text = TransferToAccountNumberTb.Text = "";
                        ReLoadAccountsDataSource();
                    }
                    else
                    {
                        string accountType = FromAccount.Type == (int)Code.Enums.Enums.AccountType.Cheque ? "Cheque" : "Savings";
                        TransactionOutputLbl.Text = String.Format("Transfer aborted. {0} accounts require a minimum balance of ${1}. Overwithdrawing an account (even with fees) is prohibited.", accountType, Math.Round(FromAccount.MinimumBalanceAllowed, 2));
                    }
                }
                else
                {
                    TransactionOutputLbl.Text = String.Format("You have insufficient funds in this account to perform this transaction. Current account balance is: ${0}", Math.Round(FromAccount.AvailableBalance, 2));
                }
            }
        }


        #endregion

        
    }
}