using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using A2_NWBA.Code.Logic;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Objects.Collections;
using A2_NWBA.Code.Enums;

namespace A2_NWBA
{
    public partial class BPAY_View : System.Web.UI.Page
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

        private BillPayItemList BPAYList
        {
            get { return ViewState["BPAYList"] as BillPayItemList; }
            set { ViewState["BPAYList"] = value; }
        }

        private BillPayPayeeList PayeeList
        {
            get { return ViewState["PayeeList"] as BillPayPayeeList; }
            set { ViewState["PayeeList"] = value; }
        }

        #endregion

        #region Loading and Binding

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInCustomer"] == null)
            {
                Response.Redirect("/Login");
            }

            if (!IsPostBack)
            {
                CustomerDataSource = (Customer)Session["LoggedInCustomer"];
                BPAYList = BillPayManager.GetBillPayList(CustomerDataSource.Id);
                PayeeList = BillPayManager.GetBillPayPayeeList();
                BindPage();
            }
        }

        protected void BindPage()
        {
            BindList();

            if (BPAYList.Count < 1)
            {
                ListPh.Visible = false;
                NoResultsPh.Visible = true;
            }
            else
            {
                ListPh.Visible = true;
                NoResultsPh.Visible = false;
            }
        }

        protected void BindList()
        {
            BPAYListItemsLv.DataSource = BPAYList;
            BPAYListItemsLv.DataBind();
        }

        protected void BPAYListItemsLv_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                BillPayItem item = (BillPayItem)e.Item.DataItem;

                Literal _BPAYIdLtr = (Literal)e.Item.FindControl("BPAYIdLtr");
                Literal _FromAccLtr = (Literal)e.Item.FindControl("FromAccLtr");
                Literal _PayeeLtr = (Literal)e.Item.FindControl("PayeeLtr");
                Literal _AmountLtr = (Literal)e.Item.FindControl("AmountLtr");
                Literal _DateLtr = (Literal)e.Item.FindControl("DateLtr");
                Literal _FreqLtr = (Literal)e.Item.FindControl("FreqLtr");
                Literal _UpdatedDateLtr = (Literal)e.Item.FindControl("UpdatedDateLtr");
                Button _ModBtn = (Button)e.Item.FindControl("ModBtn");

                _ModBtn.CommandArgument = item.Id.ToString();
                _BPAYIdLtr.Text = item.Id.ToString();
                _FromAccLtr.Text = item.PayerAccount.ToString();
                _AmountLtr.Text = string.Format("${0}", Math.Round(item.Amount, 2));
                _DateLtr.Text = item.NextScheduledDate.ToString("dd/MM/yy");
                _UpdatedDateLtr.Text = item.LastDateUpdated.ToString("dd/MM/yy");
                _FreqLtr.Text = item.FrequencyString.Trim();

                foreach (BillPayPayee payee in PayeeList)
                {
                    if (payee.Id == item.Payee)
                    {
                        _PayeeLtr.Text = payee.Name.Trim();
                        break;
                    }
                }  
            }
        }

        #endregion

        #region Events/Clicks

        protected void BPAYListItemsLv_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            BPAYListLvDp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList();
        }

        protected void ModBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                Response.Redirect("/BPAY/Edit/" + e.CommandArgument.ToString().Trim());
            }
        }

        #endregion
       
    }
}