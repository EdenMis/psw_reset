<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Group_Buy.Master" AutoEventWireup="true" CodeBehind="Lunch_order_list.aspx.cs" Inherits="psw_reset.GroupBuy.Lunch_order_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-9">
        <div class="panel panel-primary">
            <div class="panel-heading">
                每日午餐列表
            </div>
            <div class="panel-body">
                <asp:GridView runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PK" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="日期,店ID" DataNavigateUrlFormatString="Lunch_order_detail.aspx?order_date={0}&store_id={1}" DataTextField="日期" HeaderText="日期" Target="_blank">
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="店ID" HeaderText="店ID" SortExpression="店ID">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="店名" HeaderText="店名" SortExpression="店名">
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="備註" HeaderText="備註" SortExpression="備註">
                            <ItemStyle HorizontalAlign="Left" Width="500px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="是否可訂" HeaderText="結單狀態" SortExpression="是否可訂">
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" 
                    SelectCommand="SELECT [PK], [店ID], [店名], [備註], CAST([日期] AS varchar(20)) as 日期, case when [是否可訂]=1 then '尚未結單' else '已結單' end as 是否可訂 FROM [Lunch_set_main] order by 日期 desc"></asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>

