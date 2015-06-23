using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Logic;

namespace A2_NWBA
{
    public partial class Admin_Home : System.Web.UI.Page
    {
        #region ViewState

        private CustomerList CustomerDataSource
        {
            get { return ViewState["CustomerDataSource"] as CustomerList; }
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
                CustomerDataSource = CustomerManager.Admin_GetAllCustomers();
                BindList();
            }
        }

        protected void BindList()
        {
            CustomersLv.DataSource = CustomerDataSource;
            CustomersLv.DataBind();
        }

        protected void CustomersLv_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Customer item = (Customer)e.Item.DataItem;

                Literal _CustomerIdLtr = (Literal)e.Item.FindControl("CustomerIdLtr");
                Literal _CustomerNameLtr = (Literal)e.Item.FindControl("CustomerNameLtr");
                Literal _CustomerPhoneLtr = (Literal)e.Item.FindControl("CustomerPhoneLtr");
                Button _ViewAccountsBtn = (Button)e.Item.FindControl("ViewAccountsBtn");
                Button _ViewTransactionsBtn = (Button)e.Item.FindControl("ViewTransactionsBtn");

                _CustomerIdLtr.Text = item.Id.ToString();
                _CustomerNameLtr.Text = item.FullName.Trim();
                _CustomerPhoneLtr.Text = item.PhoneNumber.Trim();
                _ViewAccountsBtn.CommandArgument = item.Id.ToString();
                _ViewTransactionsBtn.CommandArgument = item.Id.ToString();
            }
        }

        #endregion

        #region Events

        protected void CustomersLv_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            CustomersLvDp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList();
        }

        protected void CustomerLvBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Accounts")
            {
                string arg = (string)e.CommandArgument;
                Response.Redirect("Admin/Accounts/View/" + arg.Trim());
            }
            else if (e.CommandName == "Transactions")
            {
                string arg = (string)e.CommandArgument;
                Response.Redirect("Admin/Accounts/Transactions/View/" + arg.Trim());
            }
        }

        #endregion
    }
}