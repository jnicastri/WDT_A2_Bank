<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="A2_NWBA.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login: NWBA</title>
    <link type="text/css" rel="stylesheet" href="/CSS/Reset.css" />
    <link type="text/css" rel="stylesheet" href="/CSS/Global.css" />
    <link type="text/css" rel="stylesheet" href="/CSS/Login.css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div id="loginheader">
                <a href="~/Login"><img src="/Images/HeadLogoTop.png" /></a>
            </div>
        </header>
    <div id="login-centre">
        
        <div id="left">
            <h1>Welcome to NWBA Online Banking</h1>
            <div class="content-block">
                The National Wealth Bank of Australasia is one of the regions largest banks.<br />
                See below for a brief summary of the services offered by NWBA.
            </div>
            <div class="content-block">
                <h3>Personal Online Banking</h3>
                The NWBA website allows Personal Banking customers with online access the ability to perform the following tasks:<br /><br />
                <ul>
                    <li>Basic ATM transactions (Withdraw, Transfer &amp; Deposit)</li>
                    <li>View Online Statements &amp; Transaction History</li>
                    <li>Manage BPAY payments</li>
                    <li>Review &amp; update your personal information</li>
                </ul>
            </div>
            <div class="content-block">
                <h3>Business Banking</h3>
                Whether your a multinational corporation, or a newly created start-up, NWBA business solutions are ready to provide solutions to take your business to the next step and beyond.
                Drop into your closest branch to have a chat with one of our Business Solution consultants, or give us call and we will arrange for our consultant to come to you.
            </div>
            <div class="content-block">
                <h3>Home &amp; Personal Loans</h3>
                Thinking of getting a new car? Going to make that big step and buy a property? NWBA has a vast array of loan solutions to get you achieving your goals. Drop in to speak to a 
                Loan consultant, or give us a call and we will send out our consultant to you to go through all our offers and talk you through the next steps. 
            </div>
            <div class="content-block">
                <h3>Credit Cards</h3>
                We have special deals on credit cards for different levels of use. With low interest rates and extended interest free days on purchases, NWBA has a credit card that will help you manage your spending.
            </div>
            <div class="content-block">
                <h3>ATMs</h3>
                Need an ATM? Or looking for a branch? We have 180 branches in Australia and over 400 ATM's. Grab our mobile app to help point out where your closest NWBA service is.
            </div>

        </div>
        <div id="right">
        <div>
            <fieldset class="form-fieldset">
                <legend>Login to NWBA</legend>
                <table style="width:100%;">
                    <tr>
                        <td>Username:</td>
                        <td><asp:TextBox ID="UserNameTb" runat="server" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="UserNameTbRfv" runat="server"
                                ControlToValidate="UserNameTb" 
                                Text="*" 
                                ErrorMessage="Please enter your Username" />
                        </td>
                    </tr>
                    <tr>
                        <td>Password:</td>
                        <td><asp:TextBox ID="PasswordTb" runat="server" TextMode="Password" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="PasswordTbRfv" runat="server" 
                                ControlToValidate="PasswordTb" 
                                Text="*" 
                                ErrorMessage="Please enter your password" />
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click" />
                </div>
            </fieldset>
            
            <asp:Label ID="LoginFailedLbl" runat="server" />
            <asp:ValidationSummary ID="LoginValidationSummary" runat="server" ShowSummary="true" HeaderText="The following errors have occured:"/>
        </div>

        </div>
    </div>
   
        
    </form>
</body>
</html>
