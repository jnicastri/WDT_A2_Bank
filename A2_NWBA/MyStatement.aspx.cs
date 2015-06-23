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
    public partial class MyStatement : System.Web.UI.Page
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

        private TransactionList CurrentAccountListDataSource
        {
            get
            {
                return ViewState["CurrentAccountListDataSource"] as TransactionList;
            }
            set
            {
                ViewState["CurrentAccountListDataSource"] = value;
            }
        }

        private int? CurrentAccountNum 
        {
            get { return ViewState["CurrentAccountNum"] as int?; }
            set { ViewState["CurrentAccountNum"] = value; }
        }
        #endregion

        #region Loading/Binding
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInCustomer"] == null)
            {
                Response.Redirect("~/Login");
            }

            if (!IsPostBack)
            {
                CustomerDataSource = (Customer)Session["LoggedInCustomer"];
                AccountsDataSource = AccountManager.GetAccountsByCustomer(CustomerDataSource);

                BindData();
            }
        }

        protected void BindData()
        {
            AccountsDdl.DataValueField = "AccountNumber";
            AccountsDdl.DataTextField = "AccountNumber";
            AccountsDdl.DataSource = AccountsDataSource;
            AccountsDdl.DataBind();
            ListItem headerItem = new ListItem("Please choose an Account");

            AccountsDdl.Items.Insert(0, headerItem);

        }

        protected void BindAccount()
        {
            Account currentAccount = null;

            foreach (Account acc in AccountsDataSource)
            {
                if (acc.AccountNumber == CurrentAccountNum)
                {
                    currentAccount = acc;
                    break;
                }
            }

            if (currentAccount != null)
            {
                AccNameLtr.Text = CustomerDataSource.FullName.Trim();
                AccNumLtr.Text = currentAccount.AccountNumber.ToString();
                AccTypeLtr.Text = currentAccount.Type == (int)Code.Enums.Enums.AccountType.Cheque ? "Cheque" : "Savings";
                AccAvailBalanceLtr.Text = String.Format("${0}", Math.Round(currentAccount.AvailableBalance, 2));
                AccLastUpdateDateLtr.Text = currentAccount.LastUpdatedDate.Value.ToString("dd/MM/yy");
                MinAccountBalanceLtr.Text = currentAccount.MinimumBalanceAllowed.ToString();

            }

            AccountDetailPh.Visible = currentAccount != null ? true : false;
            TransListPh.Visible = ((CurrentAccountListDataSource.Count > 0) && (UnableToProcessRequestPh.Visible == false)) ? true : false;
            NoResultsPh.Visible = ((!TransListPh.Visible) && (!UnableToProcessRequestPh.Visible));
        }

        protected void BindList()
        {
            TransactionLv.DataSource = CurrentAccountListDataSource;
            TransactionLv.DataBind();
        }

        protected void TransactionLv_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Transaction item = (Transaction)e.Item.DataItem;

                Literal _TransIdLtr = (Literal)e.Item.FindControl("TransIdLtr");
                Literal _TransTypeLtr = (Literal)e.Item.FindControl("TransTypeLtr");
                Literal _TransDateLtr = (Literal)e.Item.FindControl("TransDateLtr");
                Literal _TransDescrLtr = (Literal)e.Item.FindControl("TransDescrLtr");
                Literal _TransAmountLtr = (Literal)e.Item.FindControl("TransAmountLtr");

                _TransIdLtr.Text = item.Id.ToString();
                _TransDateLtr.Text = item.TransactionDate.Value.ToString("dd/MM/yy");
                _TransTypeLtr.Text = item.TransTypeName;

                if (item.Comment != "" && item.Comment != null)
                    _TransDescrLtr.Text = item.Comment.Trim();

                if (item.TransactionAmount != null)
                {
                    if (item.Type == (int)Code.Enums.Enums.TransType.Credit)
                        _TransAmountLtr.Text = string.Format("${0}", Math.Round((decimal)item.TransactionAmount, 2));
                    else
                        _TransAmountLtr.Text = string.Format("-${0}", Math.Round((decimal)item.TransactionAmount, 2));
                }
                else
                    _TransAmountLtr.Text = "$0.00";

            }
        }

        #endregion

        #region Events

        protected void AccountsDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedAccNum = 0;
            string feeString = ConfigurationManager.AppSettings["Fee_TransactionHistory"];
            decimal statementFeeAmount = 0;

            UnableToProcessRequestPh.Visible = false;

            try
            {
                selectedAccNum = Int32.Parse(AccountsDdl.SelectedValue);
                statementFeeAmount = Decimal.Parse(feeString);
                
            }
            catch (FormatException) { }

            if (selectedAccNum > 0)
            {
                foreach (Account acc in AccountsDataSource)
                {
                    if (acc.AccountNumber == selectedAccNum)
                    {
                        if (!((acc.AvailableBalance - statementFeeAmount) < acc.MinimumBalanceAllowed)) 
                        {
                            CurrentAccountListDataSource = TransactionManager.GetTransactionList(acc.AccountNumber); 
                        }
                        else
                        {
                            CurrentAccountListDataSource = new TransactionList();
                            UnableToProcessRequestPh.Visible = true;
                        }
                        break;
                    }
                }

                CurrentAccountNum = selectedAccNum;
                BindList();
                BindAccount();
                
            }
        }

        protected void TransactionLv_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            TransactionLvDp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList();
        }
        #endregion       
    }
}