using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using A2_NWBA.Code.Utils;
using A2_NWBA.Code.Objects;
using A2_NWBA.Code.Logic;


namespace A2_NWBA
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string password = "password!";
            //string hashed = UserValidation.GetHashedPassword(password);

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string hashedUserPassword = UserValidation.GetHashedPassword(PasswordTb.Text.Trim());
                int? customerNumber = UserValidation.ValidateUserCredentials(UserNameTb.Text.Trim(), hashedUserPassword);

                if (customerNumber == null || (int)customerNumber == -1)
                {
                    //Credentials don't match any user
                    LoginFailedLbl.Text = "Username/Password incorrect";
                    PasswordTb.Text = "";
                    UserNameTb.Text = "";
                }
                else if ((int)customerNumber == -5)
                {
                    //Credentials match an Admin User
                    Session["AdminSession"] = true;
                    Session["UserNameTitle"] = "Admin User";
                    Session["UserId"] = "Admin User";
                    Response.Redirect("/Admin");
                }
                else
                {
                    //Credentials match a Customer User
                    Customer loggedInCustomer = CustomerManager.GetCustomer((int)customerNumber);

                    Session["LoggedInCustomer"] = loggedInCustomer;
                    Session["UserNameTitle"] = loggedInCustomer.FirstName.Trim();
                    Session["UserId"] = UserNameTb.Text.Trim();
                    Response.Redirect("/Accounts/Action/ATM");

                }

            }
        }



    }
}