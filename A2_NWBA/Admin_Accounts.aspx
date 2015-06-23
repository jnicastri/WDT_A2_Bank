<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Admin.Master" AutoEventWireup="true" CodeBehind="Admin_Accounts.aspx.cs" Inherits="A2_NWBA.Admin_Accounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Customer Accounts: NWBA Admin</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="customer-select-area">
        <h1>Accounts by Customer</h1>
        <div id="description-area">
            Below is a list of all accounts for Customer: <asp:Literal ID="CustomerNameLtr" runat="server" />
        </div>
        <section id="list-area">
                <h2>Accounts</h2>
                <div class="clearall"></div>
                <asp:ListView ID="AccountsLv" runat="server" OnItemDataBound="AccountsLv_ItemDataBound" OnPagePropertiesChanging="AccountsLv_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table id="admin-table">
                            <tr>
                                <th style="text-align:center;">Account Number</th>
                                <th style="text-align:left;">Account Type</th>
                                <th style="text-align:center;">Customer Id</th>
                                <th style="text-align:center;">Last Date Updated</th>
                                <th style="text-align:center;">Available Balance</th>
                            </tr>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                        </table>
                        <div class="clearall"></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="background-color:white; color:black;">
                            <td style="text-align:center;"><asp:Literal ID="AccountNumLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="AccountTypeLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="CustomerIdLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="UpdatedDateLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="BalanceLtr" runat="server" /></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#5f0000; color:white;">
                            <td style="text-align:center;"><asp:Literal ID="AccountNumLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="AccountTypeLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="CustomerIdLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="UpdatedDateLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="BalanceLtr" runat="server" /></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <div class="dataPagerStyle">
                    <asp:DataPager ID="AccountsLvDp" runat="server" PagedControlID="AccountsLv" PageSize="6">
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
