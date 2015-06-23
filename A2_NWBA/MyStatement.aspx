<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Master" AutoEventWireup="true" CodeBehind="MyStatement.aspx.cs" Inherits="A2_NWBA.MyStatement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="/CSS/MyStatement.css" />
    <title>My Statement: NWBA</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>My Statement</h1>
    <div class="clearall"></div>
    <section id="statement-area">
        <div>
            <p>Please choose an account to view the transaction history:
                <asp:DropDownList ID="AccountsDdl" runat="server" OnSelectedIndexChanged="AccountsDdl_SelectedIndexChanged" AutoPostBack="true" />   
            </p> 
            <div class="clearall"></div>
        </div>
        <div class="clearall"></div>
        <asp:PlaceHolder ID="AccountDetailPh" runat="server" Visible="false">
            <section id="account-detail">
                <h2>Account Details</h2>
                <div class="clearall"></div>
                <table id="detail-table">
                    <tr>
                        <td style="width: 50%;">Account Customer Name:</td>
                        <td><asp:Literal ID="AccNameLtr" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">Account Number:</td>
                        <td><asp:Literal ID="AccNumLtr" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">Account Type:</td>
                        <td><asp:Literal ID="AccTypeLtr" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">Available Balance:</td>
                        <td><asp:Literal ID="AccAvailBalanceLtr" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">Last Date Updated:</td>
                        <td><asp:Literal ID="AccLastUpdateDateLtr" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">Minimum Allowed Balance:</td>
                        <td><asp:Literal ID="MinAccountBalanceLtr" runat="server" /></td>
                    </tr>
                </table>
                <div class="clearall"></div>
            </section>
            <div class="clearall"></div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="TransListPh" runat="server" Visible="false">
            <section id="list-area">
                <h2>Account Transactions</h2>
                <div class="clearall"></div>
                <asp:ListView ID="TransactionLv" runat="server" OnItemDataBound="TransactionLv_ItemDataBound" OnPagePropertiesChanging="TransactionLv_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table id="transaction-table">
                            <tr>
                                <th>ID</th>
                                <th style="text-align:left;">Transaction Type</th>
                                <th>Transaction Date</th>
                                <th style="text-align:left;">Transaction Description</th>
                                <th>Transaction Amount</th>
                            </tr>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                        </table>
                        <div class="clearall"></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="background-color:white;">
                            <td style="text-align:center;"><strong><asp:Literal ID="TransIdLtr" runat="server" /></strong></td>
                            <td><strong><asp:Literal ID="TransTypeLtr" runat="server" /></strong></td>
                            <td style="text-align:center;"><strong><asp:Literal ID="TransDateLtr" runat="server" /></strong></td>
                            <td><strong><asp:Literal ID="TransDescrLtr" runat="server" /></strong></td>
                            <td style="text-align:center;"><strong><asp:Literal ID="TransAmountLtr" runat="server" /></strong></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:rgba(31, 73, 125, 0.8);">
                            <td style="text-align:center;"><strong><asp:Literal ID="TransIdLtr" runat="server" /></strong></td>
                            <td><strong><asp:Literal ID="TransTypeLtr" runat="server" /></strong></td>
                            <td style="text-align:center;"><strong><asp:Literal ID="TransDateLtr" runat="server" /></strong></td>
                            <td><strong><asp:Literal ID="TransDescrLtr" runat="server" /></strong></td>
                            <td style="text-align:center;"><strong><asp:Literal ID="TransAmountLtr" runat="server" /></strong></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <div class="dataPagerStyle">
                    <asp:DataPager ID="TransactionLvDp" runat="server" PagedControlID="TransactionLv" PageSize="4">
                        <Fields>
                            <asp:NumericPagerField ButtonType="Link" PreviousPageText="&lt; Prev"
                                NextPageText="Next &gt;"
                                ButtonCount="8"
                                NextPreviousButtonCssClass="PrevNext"
                                CurrentPageLabelCssClass="SelectedPage"
                                NumericButtonCssClass="PageNumber" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </section> 
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="NoResultsPh" runat="server" Visible="false">
            <div class="no-results">No transactions exist for the selected account.</div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="UnableToProcessRequestPh" runat="server" Visible="false">
            <div class="no-results">You currently do not have enough funds in the selected account to cover
                the fee that is incurred <br />by requesting an online statement. Please add funds to the account to proceed.
            </div>
        </asp:PlaceHolder>
    </section> 
</asp:Content>
