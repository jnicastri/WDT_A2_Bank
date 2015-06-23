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
    public partial class BPAY_Edit : System.Web.UI.Page
    {
        #region ViewState Data

        private BillPayItem BPAYItem 
        { 
            get 
            { 
                return ViewState["BPAYItem"] as BillPayItem;
            }
            set
            {
                ViewState["BPAYItem"] = value;
            }
        }

        private BillPayPayeeList PayeeList
        {
            get
            {
                return ViewState["PayeeList"] as BillPayPayeeList;
            }
            set
            {
                ViewState["PayeeList"] = value;
            }
        }

        private int? PageMode
        {
            get { return ViewState["PageMode"] as int?; }
            set { ViewState["PageMode"] = value; }
        }

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

        private AccountList CustomerAccounts
        {
            get
            {
                return ViewState["CustomerAccounts"] as AccountList;
            }
            set
            {
                ViewState["CustomerAccounts"] = value;
            }
        }

        #endregion

        #region Loading/Binding

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInCustomer"] == null)
            {
                Response.Redirect("/Login");
            }

            if (!IsPostBack)
            {
                CustomerDataSource = (Customer)Session["LoggedInCustomer"];
                CustomerAccounts = AccountManager.GetAccountsByCustomer(CustomerDataSource);

                string BPAYId = (string)Page.RouteData.Values["Brn"];

                if (BPAYId != "New" && BPAYId != null && BPAYId != "")
                {
                    int parsedBPAYId = 0;

                    try
                    {
                        parsedBPAYId = Int32.Parse(BPAYId);
                    }
                    catch (FormatException) { }

                    if (parsedBPAYId != 0)
                    {
                        BPAYItem = BillPayManager.GetBillPayItem(parsedBPAYId);
                    }  
                }

                if (BPAYItem != null)
                {
                    
                    bool correctOwner = false;

                    // Confirming that BPAY item actually belongs to the logged in user.
                    foreach (Account acc in CustomerAccounts)
                    {
                        if (BPAYItem.PayerAccount == acc.AccountNumber)
                        {
                            correctOwner = true;
                            break;
                        }
                    }

                    if (!correctOwner)
                    {
                        BPAYItem = new BillPayItem();
                        BPAYItem.Id = 0;
                        PageMode = (int)Enums.PageMode.Edit;
                    }
                    else
                    {
                        PageMode = (int)Enums.PageMode.View;
                    }
                }
                else
                {
                    BPAYItem = new BillPayItem();
                    BPAYItem.Id = 0;
                    PageMode = (int)Enums.PageMode.Edit;
                }

                PayeeList = BillPayManager.GetBillPayPayeeList();

                BindData();
            }

        }

        protected void BindData()
        {
            DateTbRv.MinimumValue = DateTime.Today.ToString("dd/MM/yyyy");
            DateTbRv.MaximumValue = DateTime.Today.AddYears(2).ToString("dd/MM/yyyy");

            // DropDownLists
            FromAccountDdl.DataValueField = "AccountNumber";
            FromAccountDdl.DataTextField = "AccountNumber";
            FromAccountDdl.DataSource = CustomerAccounts;
            FromAccountDdl.DataBind();
            FromAccountDdl.Items.Insert(0, new ListItem("Choose an Account"));

            ToPayeeDdl.DataValueField = "Id";
            ToPayeeDdl.DataTextField = "Name";
            ToPayeeDdl.DataSource = PayeeList;
            ToPayeeDdl.DataBind();
            ToPayeeDdl.Items.Insert(0, new ListItem("Choose a Payee"));

            List<string> freqList = new List<string>();
            freqList.Add("Monthly");
            freqList.Add("Quarterly");
            freqList.Add("Annually");
            freqList.Add("Once Off");

            FrequencyDdl.DataSource = freqList;
            FrequencyDdl.DataBind();
            FrequencyDdl.Items.Insert(0, new ListItem("Select Frequency"));
            
            // Toggling Visibility for each mode
            if (PageMode == (int)Enums.PageMode.Edit)
            {
                FromAccountLtr.Visible =
                ToPayeeLtr.Visible =
                AmountLtr.Visible =
                DateLtr.Visible =
                FrequencyLtr.Visible =
                EditBtn.Visible = false;

                FromAccountDdl.Visible =
                ToPayeeDdl.Visible =
                AmountTb.Visible = DateTb.Visible =
                UpdateAddBtn.Visible =
                FrequencyDdl.Visible =
                CancelBtn.Visible = true;

                DeleteBtn.Visible = BPAYItem.Id == 0 ? false : true;
                EntryOutputLtr.Text = "";
            }
            else
            {
                FromAccountLtr.Visible =
                ToPayeeLtr.Visible =
                AmountLtr.Visible =
                DateLtr.Visible =
                FrequencyLtr.Visible =
                EditBtn.Visible = true;

                FromAccountDdl.Visible =
                ToPayeeDdl.Visible =
                AmountTb.Visible = DateTb.Visible =
                FrequencyDdl.Visible =
                UpdateAddBtn.Visible =
                CancelBtn.Visible =
                DeleteBtn.Visible = false;
            }

            if (BPAYItem.Id != 0)
            {
                FromAccountLtr.Text = BPAYItem.PayerAccount.ToString();
                foreach (BillPayPayee payee in PayeeList)
                {
                    if (payee.Id == BPAYItem.Payee)
                    {
                        ToPayeeLtr.Text = payee.Name.Trim();
                    }
                }
                AmountLtr.Text = String.Format("${0}", Math.Round(BPAYItem.Amount, 2).ToString()); 
                AmountTb.Text = Math.Round(BPAYItem.Amount, 2).ToString();
                FrequencyLtr.Text = BPAYItem.FrequencyString.Trim();
                DateLtr.Text = BPAYItem.NextScheduledDate.ToString("dd/MM/yy");
                DateTb.Text = BPAYItem.NextScheduledDate.ToString("dd/MM/yy");

                FromAccountDdl.SelectedValue = BPAYItem.PayerAccount.ToString();
                ToPayeeDdl.SelectedValue = BPAYItem.Payee.ToString();

                if (BPAYItem.FrequencyString.Trim() == "Monthly")
                    FrequencyDdl.SelectedValue = "Monthly";
                else if (BPAYItem.FrequencyString.Trim() == "Quarterly")
                    FrequencyDdl.SelectedValue = "Quarterly";
                else if (BPAYItem.FrequencyString.Trim() == "Annually")
                    FrequencyDdl.SelectedValue = "Annually";
                else if (BPAYItem.FrequencyString.Trim() == "NonReoccuring")
                    FrequencyDdl.SelectedValue = "Once Off";
                
            }

        }

        #endregion

        #region Events/Clicks

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            EntryOutputLtr.Text = "";
            PageMode = (int)Enums.PageMode.Edit;
            BindData();
        }

        protected void UpdateAddBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int newBPAYId = 0;
                decimal amount = Decimal.Parse(AmountTb.Text.ToString().Trim());
                int accountNum = Int32.Parse(FromAccountDdl.SelectedValue.ToString());
                int payeeId = Int32.Parse(ToPayeeDdl.SelectedValue.ToString());
                DateTime scheduleDate = DateTime.Parse(DateTb.Text.ToString().Trim());
                char frequency;

                if (FrequencyDdl.SelectedItem.Text == "Monthly")
                    frequency = 'M';
                else if (FrequencyDdl.SelectedItem.Text == "Quarterly")
                    frequency = 'Q';
                else if (FrequencyDdl.SelectedItem.Text == "Annually")
                    frequency = 'A';
                else
                    frequency = 'S';
                
                if (BPAYItem.Id == 0)
                {
                    newBPAYId = (int)BillPayManager.UpdateInsertBillPayItem(null, accountNum, amount, payeeId, scheduleDate, frequency);
                    EntryOutputLtr.Text = "New BPAY successfully created!";
                }
                else
                {
                    newBPAYId = (int)BillPayManager.UpdateInsertBillPayItem(BPAYItem.Id, accountNum, amount, payeeId, scheduleDate, frequency);
                    EntryOutputLtr.Text = "BPAY successfully updated!";
                }

                if (newBPAYId > 0)
                {
                    BPAYItem = BillPayManager.GetBillPayItem(newBPAYId);
                    PageMode = (int)Enums.PageMode.View;
                    BindData();
                }
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            PageMode = (int)Enums.PageMode.View;
            BindData();
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (BPAYItem.Id != 0) 
            { 
                BillPayManager.CancelBPAYItem(BPAYItem.Id);
                Response.Redirect("/BPAY/ViewAll");
            }
        }

        #endregion
    }
}