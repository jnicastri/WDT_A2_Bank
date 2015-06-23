<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="A2_NWBA.Default" %>
<%@ Register Src="~/UserControls/ATMSubNavControl.ascx" TagName="SubNavCtrl" TagPrefix="NWBA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="/CSS/Default.css" />
    <title>Deposit, Withdraw or Transfer Funds: NWBA</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>NWBA Deposit, Withdraw or Transfer Funds</h1>
    <section id="ATM-area">
        
        <div id="sub-nav-area">
            <NWBA:SubNavCtrl ID="ATMSubNavUc" runat="server" />
        </div>
        <div class="form-area">
            <asp:PlaceHolder ID="InitialPh" runat="server">
                    Please choose an available Transaction Type from the sub-menu located on the left to begin a transaction.
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="DepositPh" runat="server" Visible="false">
                    <fieldset class="form-fieldset">
                        <legend><strong>Deposit Money</strong></legend>
                        <table style="width:100%;">
                            <tr>
                                <td>Account to Deposit in to:</td>
                                <td><asp:DropDownList ID="DepositAccDdl" runat="server" /></td>
                                <td><asp:RequiredFieldValidator ID="DepositAccDdlRfv" 
                                        runat="server" 
                                        ControlToValidate="DepositAccDdl" 
                                        ErrorMessage="Please Choose An Account" 
                                        Text="*" 
                                        InitialValue="Choose an Account" />
                                </td>
                            </tr>
                            <tr>
                                <td>Amount To Deposit:</td>
                                <td><asp:TextBox ID="DepositAmountTb" runat="server" /></td>
                                <td><asp:RangeValidator ID="DepositAmountTbRv" 
                                        runat="server" 
                                        ControlToValidate="DepositAmountTb" 
                                        Text="*" 
                                        Type="Currency" 
                                        MinimumValue="0.01"
                                        MaximumValue="1000000"
                                        ErrorMessage="Invalid Amount Entered" />
                                    <asp:RequiredFieldValidator ID="DepositAmountTbRfv" runat="server"
                                        ControlToValidate="DepositAmountTb"
                                        Text="*"
                                        ErrorMessage="Please enter an amount to deposit" />
                                </td>
                            </tr>                
                            <tr>
                                <td>Transaction Comment:</td>
                                <td><asp:TextBox ID="DepositCommentTb" runat="server" /></td>
                            </tr>
                        </table>
                    </fieldset>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="TransferPh" runat="server"  Visible="false">
                <fieldset class="form-fieldset">
                    <legend><strong>Transfer Funds</strong></legend>
                    <table style="width:100%;">
                        <tr>
                            <td>Account to Withdraw from:</td>
                            <td><asp:DropDownList ID="TransferFromDdl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TransferFromDdl_SelectedIndexChanged"/></td>
                            <td><asp:RequiredFieldValidator ID="TransferFromDdlRfv" 
                                    runat="server" 
                                    ControlToValidate="TransferFromDdl" 
                                    ErrorMessage="Please Choose an Account to Transfer From" 
                                    Text="*" 
                                    InitialValue="Please choose an Account" />
                            </td>
                        </tr>
                        <tr>
                            <td>Available Balance:</td>
                            <td><asp:Label ID="TransferFromAccountBalanceLbl" runat="server" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Account to Transfer To:</td>
                            <td><asp:TextBox ID="TransferToAccountNumberTb" runat="server" /></td>
                            <td><asp:RangeValidator ID="TransferToTbRv" 
                                    runat="server" 
                                    ControlToValidate="TransferToAccountNumberTb" 
                                    ErrorMessage="Please enter a valid account number to transfer funds to" 
                                    Text="*" 
                                    Type="Integer"
                                    MinimumValue="1000"
                                    MaximumValue="1000000" />
                                <asp:RequiredFieldValidator ID="TransferToAccountNumberRfv" runat="server"
                                    ControlToValidate="TransferToAccountNumberTb"
                                    Text="*"
                                    ErrorMessage="Please enter an account number to transfer to" />
                            </td>
                        </tr>
                        <tr>
                            <td>Amount To Transfer</td>
                            <td><asp:TextBox ID="TransferAmountTb" runat="server" /></td>
                            <td><asp:RangeValidator ID="RangeValidator1" 
                                    runat="server" 
                                    ControlToValidate="TransferAmountTb" 
                                    Text="*" 
                                    Type="Currency" 
                                    MinimumValue="0.01"
                                    MaximumValue="1000000"
                                    ErrorMessage="Invalid Amount Entered" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="TransferAmountTb"
                                    Text="*"
                                    ErrorMessage="Please enter an amount to deposit" />
                            </td>
                        </tr>                
                        <tr>
                            <td>Transaction Comment:</td>
                            <td><asp:TextBox ID="TransferCommentTb" runat="server" /></td>
                        </tr>
                    </table>
                </fieldset>  
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="WithdrawPh" runat="server" Visible="false">
                <fieldset class="form-fieldset">
                    <legend><strong>Withdraw Funds</strong></legend>
                    <table style="width:100%;">
                        <tr>
                            <td>Account to Withdraw from:</td>
                            <td><asp:DropDownList ID="WithdrawAccountDdl" runat="server" OnSelectedIndexChanged="WithdrawAccountDdl_SelectedIndexChanged" AutoPostBack="true" /></td>
                            <td><asp:RequiredFieldValidator ID="WithdrawAccountDdlRfv" 
                                runat="server" 
                                ControlToValidate="WithdrawAccountDdl" 
                                ErrorMessage="Please Choose an Account to Withdraw From:" 
                                Text="*" 
                                InitialValue="Please choose an Account" />
                            </td>
                        </tr>
                        <tr>
                            <td>Available Balance:</td>
                            <td><asp:Label ID="WithdrawalAccountBalanceLbl" runat="server" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Amount To Withdraw</td>
                            <td><asp:TextBox ID="WithdrawalAmountTb" runat="server" /></td>
                            <td><asp:RangeValidator ID="WithdrawalAmountTbRv" 
                                runat="server" 
                                ControlToValidate="WithdrawalAmountTb" 
                                Text="*" 
                                Type="Currency" 
                                MinimumValue="0.01"
                                MaximumValue="1000000"
                                ErrorMessage="Invalid Amount Entered" />
                                <asp:RequiredFieldValidator ID="WithdrawalAmountTbRfv" runat="server"
                                    ControlToValidate="WithdrawalAmountTb"
                                    Text="*"
                                    ErrorMessage="Please enter an amount to withdraw" />
                            </td>
                        </tr>                
                        <tr>
                            <td>Transaction Comment:</td>
                            <td><asp:TextBox ID="WithdrawalCommentTb" runat="server" /></td>
                        </tr>
                    </table>
                </fieldset>  
                    
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="BtnRowPh" runat="server" Visible="false">
                <div id="button-row">
                    <asp:Button ID="SubmitTransactionBtn" runat="server" OnClick="SubmitTransactionBtn_Click" Text="Submit" /> &nbsp;&nbsp;
                    <asp:Button ID="ClearBtn" runat="server" Text="Clear" CausesValidation="false" OnClick="ClearBtn_Click" />
                    <div class="validation-summary">
                        <asp:ValidationSummary ID="DepositVs" runat="server" 
                            ShowSummary="true"  
                            HeaderText="Please correct the following errors before proceeding:"/>
                    </div>
                    <div>
                        <asp:Label ID="TransactionOutputLbl" runat="server" />
                    </div>
                </div>
            </asp:PlaceHolder>
        </div>
    </section>
    <div class="clearall"></div>
</asp:Content>
