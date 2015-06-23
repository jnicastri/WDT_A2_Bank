<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="A2_NWBA.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>My Profile: NWBA</title>
    <link type="text/css" rel="stylesheet" href="/CSS/MyProfile.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>My Profile</h1>
    <section id="customer-info">
        <fieldset class="form-fieldset">
            <legend><strong>Personal Details</strong></legend>
            <table>
                <tr>
                    <td>First Name:</td>
                    <td>
                        <asp:Literal ID="FirstNameLtr" runat="server" />
                        <asp:TextBox ID="FirstNameTb" runat="server" Visible="false" />
                    </td>
                    <td><asp:RequiredFieldValidator ID="FirstNameTbRfv" runat="server" ControlToValidate="FirstNameTb" Text="*" ErrorMessage="A first name is required" ValidationGroup="Info" /></td>
                </tr>
                <tr>
                    <td>Last Name:</td>
                    <td>
                        <asp:Literal ID="LastNameLtr" runat="server" />
                        <asp:TextBox ID="LastNameTb" runat="server" Visible="false"  />
                    </td>
                    <td><asp:RequiredFieldValidator ID="LastNameTbRfv" runat="server" ControlToValidate="LastNameTb" Text="*" ErrorMessage="A last name is required" ValidationGroup="Info" /></td>
                </tr>
                <tr>
                    <td>TFN:</td>
                    <td>
                        <asp:Literal ID="TFNLtr" runat="server" />
                        <asp:TextBox ID="TFNTb" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="TFNTbRev" ValidationGroup="Info" runat="server" Text="*" ErrorMessage="Tax File Number is not valid" ControlToValidate="TFNTb" ValidationExpression="^[0-9]{9,15}$" />
                    </td>
                </tr>
                <tr>
                    <td>Street Address:</td>
                    <td>
                        <asp:Literal ID="AddressDetailLtr" runat="server" />
                        <asp:TextBox ID="AddressDetailTb" runat="server" Visible="false" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Suburb:</td>
                    <td>
                        <asp:Literal ID="AddressCityLtr" runat="server" />
                        <asp:TextBox ID="AddressCityTb" runat="server" Visible="false" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>State:</td>
                    <td>
                        <asp:Literal ID="AddressStateLtr" runat="server" />
                        <asp:DropDownList ID="AddressStateDdl" runat="server" Visible="false" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Post Code</td>
                    <td>
                        <asp:Literal ID="AddressZipLtr" runat="server" />
                        <asp:TextBox ID="AddressZipTb" runat="server" Visible="false" />
                    </td>
                    <td><asp:RegularExpressionValidator ID="ZipTbRev" runat="server" ValidationGroup="Info" ValidationExpression="(\d{4})" ControlToValidate="AddressZipTb" Text="*" ErrorMessage="Post code is invalid" /></td>  
                </tr>
                <tr>
                    <td>Phone Number:</td>
                    <td>
                        <asp:Literal ID="PhoneNumLtr" runat="server" />
                        <asp:TextBox ID="PhoneNumTb" runat="server" Visible="false" />
                    </td>
                    <td><asp:RegularExpressionValidator ID="PhoneNumTbRev" runat="server" 
                        ControlToValidate="PhoneNumTb" ValidationExpression="^\({0,1}((0|\+61)(2|4|3|7|8)){0,1}\){0,1}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{1}(\ |-){0,1}[0-9]{3}$"
                        Text="*" ErrorMessage="Invalid Phone Number Entered" ValidationGroup="Info" /> <!--Regex Source: http://www.alectang.com/blog/archive/2009/05/11/regular-expression-for-validating-australian-phone-numbers-29.aspx -->
                        <asp:RequiredFieldValidator ID="PhoneNumTbRfv" runat="server" ValidationGroup="Info" ControlToValidate="PhoneNumTb" Text="*" ErrorMessage="A Phone number is required" />
                    </td>
                </tr>
            </table>
        
            <div id="detail-button-row">
                <asp:Button ID="EditBtn" runat="server" Text="Edit Details" OnClick="EditBtn_Click" CausesValidation="false" />
                <asp:Button ID="UpdateBtn" runat="server" Text="Update" Visible="false" OnClick="UpdateBtn_Click" CausesValidation="true" ValidationGroup="Info" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="CancelBtn" runat="server" Text="Cancel" Visible="false" OnClick="CancelBtn_Click" CausesValidation="false" />
            </div>
            <div class="update-output">
                <asp:Literal ID="DetailUpdateOutputLtr" runat="server" />
            </div>
        </fieldset>
        <div class="validation-summary">
            <asp:ValidationSummary ID="MyProfileVs" runat="server" ShowSummary="true" ValidationGroup="Info" HeaderText="The following error will need to be corrected before updating:" />
        </div>
    </section>

    <section id="password-update-area">
        <fieldset class="form-fieldset">
            <legend><strong>Change Password</strong></legend>
            <table>
                <tr>
                    <td>Current Password:</td>
                    <td><asp:TextBox ID="CurrentPwdTb" runat="server" TextMode="Password" /></td>
                    <td><asp:RequiredFieldValidator ID="CurrentPwdTbRfv" runat="server" ValidationGroup="Pwd" ControlToValidate="CurrentPwdTb" Text="*" ErrorMessage="Please Enter your current password" /></td>
                </tr>
                <tr>
                    <td>New Password:</td>
                    <td><asp:TextBox ID="NewPasswordTb1" runat="server" TextMode="Password" /></td>
                    <td><asp:RequiredFieldValidator ID="NewPwdTb1Rfv" ValidationGroup="Pwd" runat="server" ControlToValidate="NewPasswordTb1" Text ="*" ErrorMessage="First new password value is invalid" /></td>
                </tr>
                <tr>
                    <td>Confrim New Password:</td>
                    <td><asp:TextBox ID="NewPasswordTb2" runat="server" TextMode="Password" /></td>
                    <td>
                        <asp:RequiredFieldValidator ID="NewPwdTb2Rfv" ValidationGroup="Pwd" runat="server" Text="*" ControlToValidate="NewPasswordTb2" ErrorMessage="Second new password value is invalid" />
                        <asp:CompareValidator ID="NewPwdTb2Cv" runat="server" ValidationGroup="Pwd" ControlToValidate="NewPasswordTb2" ControlToCompare="NewPasswordTb1" Text ="*" ErrorMessage="New Passwords do not match"/>
                    </td>
                </tr>
            </table>
            <div id="password-button-row">
                <asp:Button ID="UpdatePwdBtn" runat="server" Text="Change Password" CausesValidation="true" ValidationGroup="Pwd" OnClick="UpdatePwdBtn_Click" />
            </div>
            <div class="update-output">
                <asp:Literal ID="PasswordUpdateLtr" runat="server" />
            </div>
        </fieldset>
        <div class="validation-summary">
            <asp:Validationsummary ID="PasswordChangeVs" runat="server" ShowSummary="true" ValidationGroup="Pwd" HeaderText="The following errors are preventing you from changing your password:"/>
        </div>
    </section>
    <div class="clearall"></div>
</asp:Content>
