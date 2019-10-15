<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Store_evaluation.aspx.cs" Inherits="psw_reset.GroupBuy.Store_evaluation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2><asp:Label ID="Label1" runat="server" Text="此店家目前尚無任何評價資料，歡迎投票" Visible="false"></asp:Label></h2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PK" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" AllowPaging="True" PageSize="25">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="投票日期" HeaderText="投票日期" SortExpression="投票日期">
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
    <%--                <asp:BoundField DataField="投票人同編" HeaderText="投票人同編" SortExpression="投票人同編">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="投票人姓名" HeaderText="投票人姓名" SortExpression="投票人姓名">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="投票人部門" HeaderText="投票人部門" SortExpression="投票人部門">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="選項" HeaderText="選項" SortExpression="選項">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="意見" HeaderText="意見" SortExpression="意見">
                        <ItemStyle HorizontalAlign="Center" Width="500px" />
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" SelectCommand="SELECT [PK], [店ID], CAST([投票日期] AS varchar(20)) AS 投票日期, [投票人同編], [投票人姓名], [投票人部門], [選項], [意見] FROM [lunch_Voting_detail] WHERE ([店ID] = @店ID) order by [投票日期]">
                <SelectParameters>
                    <asp:QueryStringParameter Name="店ID" QueryStringField="store_id" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
