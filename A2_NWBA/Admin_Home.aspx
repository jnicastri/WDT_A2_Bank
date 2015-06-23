<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Admin.Master" AutoEventWireup="true" CodeBehind="Admin_Home.aspx.cs" Inherits="A2_NWBA.Admin_Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin Home: NWBA</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="customer-select-area">
        <h1>Admin Section</h1>
        <div id="description-area">
            All NWBA customers are listed below. Available options are:
            <ul>
                <li>View A Customers Accounts</li>
                <li>View All Transactions by a Customer</li>
            </ul>
        </div>
        <section id="list-area">
                <h2>Customers</h2>
                <div class="clearall"></div>
                <asp:ListView ID="CustomersLv" runat="server" OnItemDataBound="CustomersLv_ItemDataBound" OnPagePropertiesChanging="CustomersLv_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table id="admin-table">
                            <tr>
                                <th style="text-align:center;">ID</th>
                                <th style="text-align:left;">Name</th>
                                <th style="text-align:center;">Phone #</th>
                                <th style="text-align:center;">Accounts</th>
                                <th style="text-align:center;">Transactions</th>
                            </tr>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                        </table>
                        <div class="clearall"></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="background-color:white; color:black;">
                            <td style="text-align:center;"><asp:Literal ID="CustomerIdLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="CustomerNameLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="CustomerPhoneLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Button ID="ViewAccountsBtn" runat="server" Text="View Accounts" CommandName="Accounts" OnCommand="CustomerLvBtn_Command" /></td>
                            <td style="text-align:center;"><asp:Button ID="ViewTransactionsBtn" runat="server" Text ="View Transactions" CommandName="Transactions" OnCommand="CustomerLvBtn_Command" /></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#5f0000; color:white;">
                            <td style="text-align:center;"><asp:Literal ID="CustomerIdLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="CustomerNameLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="CustomerPhoneLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Button ID="ViewAccountsBtn" runat="server" Text="View Accounts" CommandName="Accounts" OnCommand="CustomerLvBtn_Command" /></td>
                            <td style="text-align:center;"><asp:Button ID="ViewTransactionsBtn" runat="server" Text ="View Transactions" CommandName="Transactions" OnCommand="CustomerLvBtn_Command" /></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <div class="dataPagerStyle">
                    <asp:DataPager ID="CustomersLvDp" runat="server" PagedControlID="CustomersLv" PageSize="6">
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
