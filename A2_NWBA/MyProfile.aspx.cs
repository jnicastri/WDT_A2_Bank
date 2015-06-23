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
using A2_NWBA.Code.Utils;

namespace A2_NWBA
{
    public partial class MyProfile : System.Web.UI.Page
    {
        #region ViewState
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

        private int? PageMode
        {
            get { return ViewState["PageMode"] as int?; }
            set { ViewState["PageMode"] = value; }
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
                PageMode = (int)Enums.PageMode.View;
                BindPage();
            }
        }

        protected void BindPage()
        {
            List<String> stateList = new List<string>();

            stateList.Add("NSW");
            stateList.Add("QLD");
            stateList.Add("VIC");
            stateList.Add("SA");
            stateList.Add("WA");
            stateList.Add("ACT");
            stateList.Add("TAS");
            stateList.Add("NT");

            AddressStateDdl.DataSource = stateList;
            AddressStateDdl.DataBind();
            AddressStateDdl.Items.Insert(0, new ListItem("Choose your state"));
            AddressStateDdl.SelectedValue = CustomerDataSource.CustomerAddress.State.Trim();

            FirstNameLtr.Text = FirstNameTb.Text = CustomerDataSource.FirstName.Trim();
            LastNameLtr.Text = LastNameTb.Text = CustomerDataSource.LastName.Trim();
            TFNLtr.Text = TFNTb.Text = CustomerDataSource.TaxFileNumber.Trim();
            AddressDetailLtr.Text = AddressDetailTb.Text = CustomerDataSource.CustomerAddress.StreetDetail.Trim();
            AddressCityLtr.Text = AddressCityTb.Text = CustomerDataSource.CustomerAddress.City.Trim();
            AddressStateLtr.Text = CustomerDataSource.CustomerAddress.State.Trim();
            AddressZipLtr.Text = AddressZipTb.Text = CustomerDataSource.CustomerAddress.ZipCode.Trim();
            PhoneNumLtr.Text = PhoneNumTb.Text = CustomerDataSource.PhoneNumber.Trim();

            if (PageMode == (int)Enums.PageMode.View)
            {
                FirstNameLtr.Visible =
                LastNameLtr.Visible =
                TFNLtr.Visible =
                AddressDetailLtr.Visible =
                AddressStateLtr.Visible =
                AddressCityLtr.Visible =
                AddressZipLtr.Visible =
                PhoneNumLtr.Visible = true;

                FirstNameTb.Visible =
                LastNameTb.Visible =
                TFNTb.Visible =
                AddressDetailTb.Visible =
                AddressStateDdl.Visible =
                AddressCityTb.Visible =
                AddressZipTb.Visible =
                PhoneNumTb.Visible = false;

                EditBtn.Visible = true;
                UpdateBtn.Visible = false;
                CancelBtn.Visible = false;
            }
            else
            {
                FirstNameLtr.Visible =
                LastNameLtr.Visible =
                TFNLtr.Visible =
                AddressDetailLtr.Visible =
                AddressStateLtr.Visible =
                AddressCityLtr.Visible =
                AddressZipLtr.Visible =
                PhoneNumLtr.Visible = false;

                FirstNameTb.Visible =
                LastNameTb.Visible =
                TFNTb.Visible =
                AddressDetailTb.Visible =
                AddressStateDdl.Visible =
                AddressCityTb.Visible =
                AddressZipTb.Visible =
                PhoneNumTb.Visible = true;

                UpdateBtn.Visible = true;
                CancelBtn.Visible = true;

                EditBtn.Visible = false;
            }

            DetailUpdateOutputLtr.Text = "";
            PasswordUpdateLtr.Text = "";
        }

        protected void RefreshDataSource()
        {
            int customerId = CustomerDataSource.Id;
            Customer refreshedCustomer = CustomerManager.GetCustomer(customerId);

            Session["LoggedInCustomer"] = refreshedCustomer;
            CustomerDataSource = refreshedCustomer;

            this.PageMode = (int)Enums.PageMode.View;
            BindPage();
        }

        #endregion


        #region Events

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            this.PageMode = (int)Enums.PageMode.Edit;
            BindPage();
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.PageMode = (int)Enums.PageMode.View;
            BindPage();
        }

        

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                this.CustomerDataSource.FirstName = FirstNameTb.Text.Trim();
                this.CustomerDataSource.LastName = LastNameTb.Text.Trim();
                this.CustomerDataSource.TaxFileNumber = TFNTb.Text.Trim();
                this.CustomerDataSource.CustomerAddress.StreetDetail = AddressDetailTb.Text.Trim();
                this.CustomerDataSource.CustomerAddress.State = AddressStateDdl.SelectedIndex != 0 ? AddressStateDdl.SelectedValue.ToString().Trim() : "";
                this.CustomerDataSource.CustomerAddress.City = AddressCityTb.Text.Trim();
                this.CustomerDataSource.CustomerAddress.ZipCode = AddressZipTb.Text.Trim();
                this.CustomerDataSource.PhoneNumber = PhoneNumTb.Text.Trim();

                Customer newDataSource = CustomerManager.SaveCustomer(this.CustomerDataSource);

                this.CustomerDataSource = newDataSource;
                Session["LoggedInCustomer"] = newDataSource;
                this.PageMode = this.PageMode = (int)Enums.PageMode.View;
                this.BindPage();
                DetailUpdateOutputLtr.Text = "Your details have been updated";
            }
        }

        protected void UpdatePwdBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string currentPassword = UserValidation.GetHashedPassword(CurrentPwdTb.Text.Trim());
                string newPassword = UserValidation.GetHashedPassword(NewPasswordTb2.Text.Trim());
                string userId = (string)Session["UserId"];
                int? customerNumber = UserValidation.ValidateUserCredentials(userId, currentPassword);

                if ((int)customerNumber != -1)
                {
                    string newPwd = NewPasswordTb2.Text.Trim();

                    UserValidation.UpdateUserPassword(userId, newPassword, this.CustomerDataSource.Id);

                    CurrentPwdTb.Text = NewPasswordTb1.Text = NewPasswordTb2.Text = "";
                    this.BindPage();
                    PasswordUpdateLtr.Text = "Password Updated!";
                }
                else
                {
                    CurrentPwdTb.Text = NewPasswordTb1.Text = NewPasswordTb2.Text = "";
                    this.BindPage();
                    PasswordUpdateLtr.Text = "Your current password entered is incorrect";
                }
            }
        }
        #endregion
    }
}