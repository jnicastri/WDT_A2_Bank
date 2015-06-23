<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Admin.Master" AutoEventWireup="true" CodeBehind="Admin_Transactions.aspx.cs" Inherits="A2_NWBA.Admin_Transactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Customer Transaction: NWBA Admin</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="customer-select-area">
        <h1>Transactions by Customer</h1>
        <div id="description-area">
            Below is a list of Transactions from accounts belonging to Customer: <asp:Literal ID="CustomerNameLtr" runat="server" />
        </div>
        <section id="list-area">
                <h2>Transactions</h2>
                <div class="clearall"></div>
                <asp:ListView ID="TransactionsLv" runat="server" OnItemDataBound="TransactionsLv_ItemDataBound" OnPagePropertiesChanging="TransactionsLv_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table id="admin-table">
                            <tr>
                                <th style="text-align:center;">Transaction ID</th>
                                <th style="text-align:left;">Transaction Type</th>
                                <th style="text-align:center;">From Account</th>
                                <th style="text-align:center;">To Account</th>
                                <th style="text-align:center;">Amount</th>
                                <th style="text-align:left;">Comment</th>
                                <th style="text-align:center;">Transaction Date</th>
                            </tr>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                        </table>
                        <div class="clearall"></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="background-color:white; color:black;">
                            <td style="text-align:center;"><asp:Literal ID="TransIdLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="TransTypeLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="FromAccountLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="ToAccountLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="AmountLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="CommentLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="TransDateLtr" runat="server" /></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#5f0000; color:white;">
                            <td style="text-align:center;"><asp:Literal ID="TransIdLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="TransTypeLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="FromAccountLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="ToAccountLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="AmountLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="CommentLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="TransDateLtr" runat="server" /></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <div class="dataPagerStyle">
                    <asp:DataPager ID="TransactionsLvDp" runat="server" PagedControlID="TransactionsLv" PageSize="8">
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
    </section>
</asp:Content>
