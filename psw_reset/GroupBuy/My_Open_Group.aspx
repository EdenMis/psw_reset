<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Group_Buy.Master" AutoEventWireup="true" CodeBehind="My_Open_Group.aspx.cs" Inherits="psw_reset.GroupBuy.My_Open_Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                我的開團
            </div>
            <div class="panel-body">
                <asp:GridView runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="True" PageSize="20">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="PK" HeaderText="團號" SortExpression="PK">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="PK" DataNavigateUrlFormatString="My_Open_Group_detail.aspx?PK={0}" DataTextField="開團標題" HeaderText="開團標題" Target="_blank">
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="狀態" HeaderText="狀態" SortExpression="狀態">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [PK], [開團標題], [狀態] FROM [Group_main_table] WHERE ([開團人] = @開團人) order by PK desc">
                    <SelectParameters>
                        <asp:SessionParameter Name="開團人" SessionField="User_Name" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>

