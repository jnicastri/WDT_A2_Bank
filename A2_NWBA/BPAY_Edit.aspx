<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Master" AutoEventWireup="true" CodeBehind="BPAY_Edit.aspx.cs" Inherits="A2_NWBA.BPAY_Edit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create or Edit BPAY Payments: NWBA</title>
    <link type="text/css" rel="stylesheet" href="/CSS/BPAY.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <AjaxToolKit:ToolkitScriptManager ID="ScriptManagerAct" runat="server" />
    <h1>Create/Edit BPAY Payments</h1>
    <section id="BPAY-detail-area">
        <fieldset class="form-fieldset">
            <legend><strong>BPAY Details</strong></legend>
            <table>
                <tr>
                    <td>From Account:</td>
                    <td>
                        <asp:Literal ID="FromAccountLtr" runat="server" />
                        <asp:DropDownList ID="FromAccountDdl" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="FromAccountDdlRfv" runat="server" Text="*" ErrorMessage="Please Select an Account" InitialValue="Choose an Account" ControlToValidate="FromAccountDdl" />
                    </td>
                </tr>
                <tr>
                    <td>To Payee:</td>
                    <td>
                        <asp:Literal ID="ToPayeeLtr" runat="server" />
                        <asp:DropDownList ID="ToPayeeDdl" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="ToPayeeDdlRfv" runat="server" Text="*" ErrorMessage="Please select a Payee to pay" InitialValue="Choose a Payee" ControlToValidate="ToPayeeDdl" />
                    </td>
                </tr>
                <tr>
                    <td>Amount:</td>
                    <td>
                        <asp:Literal ID="AmountLtr" runat="server" />
                        <asp:TextBox ID="AmountTb" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="AmountTbRfv" runat="server" Text="*" ErrorMessage="Please enter an amount" ControlToValidate="AmountTb" />
                        <asp:RangeValidator ID="AmountTbRv" runat="server" ControlToValidate="AmountTb" Text="*" ErrorMessage="Invalid Amount Entered" Type="Currency" MinimumValue="0.01" MaximumValue="1000000" />
                    </td>
                </tr>
                <tr>
                    <td>Scheduled Date:</td>
                    <td>
                        <asp:Literal ID="DateLtr" runat="server" Visible="false"/>
                        <asp:TextBox ID="DateTb" runat="server" Visible="false" />
                        <AjaxToolKit:CalendarExtender ID="DateCe" runat="server" TargetControlID="DateTb" CssClass="ajaxCalendar" Format="dd/MM/yy" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="DateTbRfv" runat="server" ControlToValidate="DateTb" Text="*" ErrorMessage="Please choose a date" />
                        <asp:RangeValidator ID="DateTbRv" runat="server" Type="Date" ErrorMessage="Invalid Date Entered" Text="*" ControlToValidate="DateTb" />
                    </td>
                </tr>
                <tr>
                    <td>Payment Reoccurance:</td>
                    <td>
                        <asp:Literal ID="FrequencyLtr" runat="server" />
                        <asp:DropDownList ID="FrequencyDdl" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="FrequencyDdlRfv" runat="server" ControlToValidate="FrequencyDdl" Text="*" ErrorMessage="Please choose a frequency type" InitialValue="Select Frequency" />
                    </td>
                </tr>
            </table>
            <div id="buttons">
                <asp:Button ID="EditBtn" runat="server" Text="Edit" CausesValidation="false" OnClick="EditBtn_Click" />
                <asp:Button ID="UpdateAddBtn" runat="server" Text="Save/Update" CausesValidation="true" Visible="false" OnClick="UpdateAddBtn_Click" />
                <asp:Button ID="CancelBtn" runat="server" Text="Cancel" CausesValidation="false" Visible="false" OnClick="CancelBtn_Click" />
                <asp:Button ID="DeleteBtn" runat="server" Text="Delete/End BPAY Item" CausesValidation="false" OnClick="DeleteBtn_Click" Visible="false" OnClientClick="return confirm('Are you sure you want to end this BPAY item? Any scheduled future payments will not occur.');" />
            </div>
        </fieldset>
        <asp:ValidationSummary ID="BPAYEditVs" runat="server" HeaderText="Please correct the following errors before proceeding:" ShowSummary="true" />
        <div id="message-area">
            <a style="color:blue;" href="/BPAY/ViewAll">Click here to view all your existing scheduled BPAY items</a><br /><br />
            <asp:Literal ID="EntryOutputLtr" runat="server" />
        </div>
    </section>




</asp:Content>
