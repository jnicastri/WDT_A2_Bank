<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Master" AutoEventWireup="true" CodeBehind="BPAY_View.aspx.cs" Inherits="A2_NWBA.BPAY_View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View BPAY Payments: NWBA</title>
    <link type="text/css" rel="stylesheet" href="/CSS/BPAY.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>View BPAY Payments</h1>
    <section id="BPAY-view-area">
        <asp:PlaceHolder ID="ListPh" runat="server">
            <asp:ListView ID="BPAYListItemsLv" runat="server" OnItemDataBound="BPAYListItemsLv_ItemDataBound" OnPagePropertiesChanging="BPAYListItemsLv_PagePropertiesChanging">
                <LayoutTemplate>
                    <table id="BPAY-item-table">
                        <tr>
                            <th>BPAY Id</th>
                            <th>From Account</th>
                            <th>Payee</th>
                            <th>Amount</th>
                            <th>Scheduled Payment Date</th>
                            <th>Payment Frequency</th>
                            <th>Last Date Updated</th>
                            <th>Action</th>
                        </tr>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr style="background-color:white;">
                        <td style="text-align:center;"><strong><asp:Literal ID="BPAYIdLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="FromAccLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="PayeeLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="AmountLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="DateLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="FreqLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="UpdatedDateLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Button ID="ModBtn" runat="server" Text="Modify" CommandName="Modify" OnCommand="ModBtn_Command" /></strong></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="background-color:rgba(31, 73, 125, 0.8);">
                        <td style="text-align:center;"><strong><asp:Literal ID="BPAYIdLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="FromAccLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="PayeeLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="AmountLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="DateLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="FreqLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Literal ID="UpdatedDateLtr" runat="server" /></strong></td>
                        <td style="text-align:center;"><strong><asp:Button ID="ModBtn" runat="server" Text="Modify" CommandName="Modify" OnCommand="ModBtn_Command" /></strong></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
            <div class="dataPagerStyle">
                <asp:DataPager ID="BPAYListLvDp" runat="server" PagedControlID="BPAYListItemsLv" PageSize="8">
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
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="NoResultsPh" runat="server" Visible="false">
            <div class="no-results">You currently do not have an scheduled BPAY payments.</div>
        </asp:PlaceHolder>
    </section>

</asp:Content>
