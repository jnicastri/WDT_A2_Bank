using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Logic;
using A2_NWBA.Code.Enums;

namespace A2_NWBA
{
    public partial class Admin_Transactions : System.Web.UI.Page
    {
        #region ViewState Data
        private TransactionList TransactionDataSource
        {
            get { return ViewState["TransactionDataSource"] as TransactionList; }
            set { ViewState["TransactionDataSource"] = value; }
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

                    TransactionDataSource = TransactionManager.Admin_GetTransactionsByCustomer(CustomerDataSource.Id);
                    BindPage();
                }
                else
                    Response.Redirect("/Admin");
            }
        }

        protected void BindPage()
        {
            CustomerNameLtr.Text = CustomerDataSource.FullName.Trim();
            TransactionsLv.DataSource = TransactionDataSource;
            TransactionsLv.DataBind();
        }

        protected void TransactionsLv_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Transaction item = (Transaction)e.Item.DataItem;

                Literal _TransIdLtr = (Literal)e.Item.FindControl("TransIdLtr");
                Literal _TransTypeLtr = (Literal)e.Item.FindControl("TransTypeLtr");
                Literal _FromAccountLtr = (Literal)e.Item.FindControl("FromAccountLtr");
                Literal _ToAccountLtr = (Literal)e.Item.FindControl("ToAccountLtr");
                Literal _AmountLtr = (Literal)e.Item.FindControl("AmountLtr");
                Literal _CommentLtr = (Literal)e.Item.FindControl("CommentLtr");
                Literal _TransDateLtr = (Literal)e.Item.FindControl("TransDateLtr");

                _TransIdLtr.Text = item.Id.ToString();
                _FromAccountLtr.Text = item.SourceAccountId.ToString();
                _ToAccountLtr.Text = item.DestinationAccountNumber.ToString();
                _TransDateLtr.Text = item.TransactionDate.Value.ToString("dd/MM/yy");
                _TransTypeLtr.Text = item.TransTypeName;

                if (item.Comment != "" && item.Comment != null)
                    _CommentLtr.Text = item.Comment.Trim();

                if (item.TransactionAmount != null)
                {
                    if (item.Type == (int)Code.Enums.Enums.TransType.Credit)
                        _AmountLtr.Text = string.Format("${0}", Math.Round((decimal)item.TransactionAmount, 2));
                    else
                        _AmountLtr.Text = string.Format("-${0}", Math.Round((decimal)item.TransactionAmount, 2));
                }
                else
                    _AmountLtr.Text = "$0.00";
            }
        }

        #endregion

        #region Events

        protected void TransactionsLv_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            TransactionsLvDp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindPage();
        }

        #endregion
    }
}