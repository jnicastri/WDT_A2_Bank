using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Enums;
using A2_NWBA.Code.Logic;

namespace A2_NWBA
{
    public partial class Admin_Accounts : System.Web.UI.Page
    {
        #region ViewState Data
        private AccountList AccountsDataSource
        {
            get { return ViewState["AccountsDataSource"] as AccountList; }
            set { ViewState["AccountsDataSource"] = value; }
        }

        private Customer CustomerDataSource
        {
            get { return ViewState["CustomerDataSource"] as Customer; }
            set { ViewState["CustomerDataSource"] = value; }
        }

        #endregion

        #region Loading/Binding
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminSession"] == null)
            {
                Response.Redirect("/Login");
            }

            if (!IsPostBack)
            {
                string customerIdString = (string)Page.RouteData.Values["CustomerId"];
                int customerId = 0;

                try
                {
                    customerId = Int32.Parse(customerIdString);
                }
                catch (FormatException) { }

                if (customerId != 0)
                {
                    CustomerDataSource = CustomerManager.GetCustomer(customerId);

                    if (CustomerDataSource.Id == 0)
                        Response.Redirect("/Admin");

                    AccountsDataSource = AccountManager.GetAccountsByCustomer(CustomerDataSource);
                    BindPage();
                }
                else
                    Response.Redirect("/Admin");
            }
        }

        protected void BindPage()
        {
            CustomerNameLtr.Text = CustomerDataSource.FullName.Trim();
            AccountsLv.DataSource = AccountsDataSource;
            AccountsLv.DataBind();
        }

        protected void AccountsLv_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Account item = (Account)e.Item.DataItem;

                Literal _AccountNumLtr = (Literal)e.Item.FindControl("AccountNumLtr");
                Literal _AccountTypeLtr = (Literal)e.Item.FindControl("AccountTypeLtr");
                Literal _CustomerIdLtr = (Literal)e.Item.FindControl("CustomerIdLtr");
                Literal _UpdatedDateLtr = (Literal)e.Item.FindControl("UpdatedDateLtr");
                Literal _BalanceLtr = (Literal)e.Item.FindControl("BalanceLtr");

                _AccountNumLtr.Text = item.AccountNumber.ToString();
                _AccountTypeLtr.Text = item.Type == (int)Code.Enums.Enums.AccountType.Cheque ? "Cheque" : "Savings";
                _CustomerIdLtr.Text = item.CustomerId.ToString();
                _UpdatedDateLtr.Text = item.LastUpdatedDate == null ? "" : item.LastUpdatedDate.Value.ToString("dd/MM/yy");
                _BalanceLtr.Text = String.Format("${0}", Math.Round(item.AvailableBalance, 2));
            }
        }

        #endregion

        #region Events

        protected void AccountsLv_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            AccountsLvDp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindPage();
        }

        #endregion
    }
}