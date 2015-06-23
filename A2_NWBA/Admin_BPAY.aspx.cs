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
    public partial class Admin_BPAY : System.Web.UI.Page
    {
        #region ViewState
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

        #region Loading/Binding

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminSession"] == null)
            {
                Response.Redirect("/Login");
            }

            if (!IsPostBack)
            {
                PayeeList = BillPayManager.GetBillPayPayeeList();
                BPAYList = BillPayManager.Admin_GetAllBPAY();

                BindList();
            }
        }

        protected void BindList()
        {
            BPAYLv.DataSource = BPAYList;
            BPAYLv.DataBind();
        }

        protected void RefreshDataSource()
        {
            BPAYList = BillPayManager.Admin_GetAllBPAY();
            BindList();
        }

        protected void BPAYLv_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                BillPayItem item = (BillPayItem)e.Item.DataItem;

                Literal _BPAYIdLtr = (Literal)e.Item.FindControl("BPAYIdLtr");
                Literal _FromAccountLtr = (Literal)e.Item.FindControl("FromAccountLtr");
                Literal _PayeeLtr = (Literal)e.Item.FindControl("PayeeLtr");
                Literal _AmountLtr = (Literal)e.Item.FindControl("AmountLtr");
                Literal _ScheduleDateLtr = (Literal)e.Item.FindControl("ScheduleDateLtr");
                Literal _FrequencyLtr = (Literal)e.Item.FindControl("FrequencyLtr");
                Literal _UpdateDateLtr = (Literal)e.Item.FindControl("UpdateDateLtr");
                Button _CancelBPAYBtn = (Button)e.Item.FindControl("CancelBPAYBtn");

                _BPAYIdLtr.Text = item.Id.ToString();
                _FromAccountLtr.Text = item.PayerAccount.ToString();
                _AmountLtr.Text = String.Format("${0}", Math.Round(item.Amount, 2));
                _ScheduleDateLtr.Text = item.NextScheduledDate.ToString("dd/MM/yy");
                _FrequencyLtr.Text = item.FrequencyString.Trim();
                _UpdateDateLtr.Text = item.LastDateUpdated.ToString("dd/MM/yy");
                _CancelBPAYBtn.CommandArgument = item.Id.ToString();
                _PayeeLtr.Text = "Not Set";

                foreach(BillPayPayee payee in PayeeList){
                    if (payee.Id == item.Payee)
                    {
                        _PayeeLtr.Text = payee.Name.Trim();
                        break;
                    }
                }
            }
        }

        #endregion

        #region Events

        protected void BPAYLv_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            BPAYLvDp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList();
        }

        protected void CancelBPAYBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                int BPAYId = 0;

                try
                {
                    BPAYId = Int32.Parse(e.CommandArgument.ToString());
                }
                catch (FormatException) { }

                if (BPAYId != 0)
                {
                    BillPayManager.CancelBPAYItem(BPAYId);
                    RefreshDataSource();
                }
                
            }
        }

        #endregion
    }
}