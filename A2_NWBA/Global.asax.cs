using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace A2_NWBA
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void RegisterRoutes(RouteCollection URLRoutes)
        {
            URLRoutes.MapPageRoute("LoginRoute", "Login", "~/Login.aspx");
            URLRoutes.MapPageRoute("BPAYEditRoute", "BPAY/Edit/{Brn}", "~/BPAY_Edit.aspx");
            URLRoutes.MapPageRoute("BPAYViewRoute", "BPAY/ViewAll", "~/BPAY_View.aspx");
            URLRoutes.MapPageRoute("MyStatementRoute", "Accounts/Statement/View", "~/MyStatement.aspx");
            URLRoutes.MapPageRoute("MyProfileRoute", "Customer/Profile", "~/MyProfile.aspx");
            URLRoutes.MapPageRoute("AtmRoute", "Accounts/Action/ATM", "~/Default.aspx");
            URLRoutes.MapPageRoute("AdminAccountsRoute", "Admin/Accounts/View/{CustomerId}", "~/Admin_Accounts.aspx");
            URLRoutes.MapPageRoute("AdminTransactionsRoute", "Admin/Accounts/Transactions/View/{CustomerId}", "~/Admin_Transactions.aspx");
            URLRoutes.MapPageRoute("AdminBPAYViewRoute", "Admin/BPAY/ViewAll", "~/Admin_BPAY.aspx");
            URLRoutes.MapPageRoute("AdminHomeRoute", "Admin", "~/Admin_Home.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}