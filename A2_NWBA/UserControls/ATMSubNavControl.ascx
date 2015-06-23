<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ATMSubNavControl.ascx.cs" Inherits="A2_NWBA.UserControls.ATMSubNavControl" %>

<div>
    <fieldset class="menu-fieldset">
        <legend><strong>Transaction Types</strong></legend>
        <ul>
            <li><asp:LinkButton ID="DepositLb" runat="server" Text="Deposit Funds" OnClick="DepositLb_Click" CausesValidation="false" /></li>
            <li><asp:LinkButton ID="TransferLb" runat="server" Text="Transfer Funds" OnClick="TransferLb_Click" CausesValidation="false" /></li>
            <li><asp:LinkButton ID="WithdrawLb" runat="server" Text="Withdraw Funds" OnClick="WithdrawLb_Click" CausesValidation="false" /></li>
        </ul>
    </fieldset>
</div>
