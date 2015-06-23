using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace A2_NWBA.UserControls
{
    public partial class ATMSubNavControl : System.Web.UI.UserControl
    {
        public event EventHandler SubMenuItemSelected;

        public int? ModeIndex
        {
            get
            {   
                return ViewState["ModeIndex"] as int?;
            }
            set
            {
                ViewState["ModeIndex"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void EventSubMenuItemSelected()
        {   
            if (SubMenuItemSelected != null)
            {
                SubMenuItemSelected(this, EventArgs.Empty);
            }
        }

        protected void DepositLb_Click(object sender, EventArgs e)
        {
            this.ModeIndex = (int?)Code.Enums.Enums.ATMMode.Deposit;
            EventSubMenuItemSelected();
        }

        protected void TransferLb_Click(object sender, EventArgs e)
        {
            this.ModeIndex = (int?)Code.Enums.Enums.ATMMode.Transfer;
            EventSubMenuItemSelected();
        }

        protected void WithdrawLb_Click(object sender, EventArgs e)
        {
            this.ModeIndex = (int?)Code.Enums.Enums.ATMMode.Withdraw;
            EventSubMenuItemSelected();
        }
    }
}