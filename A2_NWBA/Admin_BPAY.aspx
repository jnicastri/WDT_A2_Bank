<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NWBA.Admin.Master" AutoEventWireup="true" CodeBehind="Admin_BPAY.aspx.cs" Inherits="A2_NWBA.Admin_BPAY" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage BPAY: NWBA Admin</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="customer-select-area">
        <h1>View/Cancel BPAY</h1>
        <div id="description-area">
            Below is a complete list of all BPAY items that are currently active.
        </div>
        <section id="list-area">
                <h2>BPAY Schedule</h2>
                <div class="clearall"></div>
                <asp:ListView ID="BPAYLv" runat="server" OnItemDataBound="BPAYLv_ItemDataBound" OnPagePropertiesChanging="BPAYLv_PagePropertiesChanging">
                    <LayoutTemplate>
                        <table id="admin-table">
                            <tr>
                                <th style="text-align:center;">BPAY ID</th>
                                <th style="text-align:center;">From Account</th>
                                <th style="text-align:left;">Payee Name</th>
                                <th style="text-align:center;">Amount</th>
                                <th style="text-align:center;">Next Schedule Date</th>
                                <th style="text-align:left;">Frequency</th>
                                <th style="text-align:center;">Last Updated Date</th>
                                <th style="text-align:center;">Action</th>
                            </tr>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                        </table>
                        <div class="clearall"></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="background-color:white; color:black;">
                            <td style="text-align:center;"><asp:Literal ID="BPAYIdLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="FromAccountLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="PayeeLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="AmountLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="ScheduleDateLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="FrequencyLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="UpdateDateLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Button ID="CancelBPAYBtn" runat="server" Text="Cancel" CommandName="Cancel" OnCommand="CancelBPAYBtn_Command" OnClientClick="return confirm('Are you sure you want to end this BPAY item?');" /></td>
                            
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#5f0000; color:white;">
                            <td style="text-align:center;"><asp:Literal ID="BPAYIdLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="FromAccountLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="PayeeLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="AmountLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="ScheduleDateLtr" runat="server" /></td>
                            <td style="text-align:left;"><asp:Literal ID="FrequencyLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Literal ID="UpdateDateLtr" runat="server" /></td>
                            <td style="text-align:center;"><asp:Button ID="CancelBPAYBtn" runat="server" Text="Cancel" CommandName="Cancel" OnCommand="CancelBPAYBtn_Command" OnClientClick="return confirm('Are you sure you want to end this BPAY item?');" /></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <div class="dataPagerStyle">
                    <asp:DataPager ID="BPAYLvDp" runat="server" PagedControlID="BPAYLv" PageSize="6">
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
