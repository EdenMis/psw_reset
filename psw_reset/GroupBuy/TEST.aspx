<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TEST.aspx.cs" Inherits="psw_reset.GroupBuy.TEST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="dep_id" HeaderText="dep_id" SortExpression="dep_id" />
                <asp:BoundField DataField="dep_cname" HeaderText="dep_cname" SortExpression="dep_cname" />
                <asp:BoundField DataField="emp_cname" HeaderText="emp_cname" SortExpression="emp_cname" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" SelectCommand="SELECT * FROM [TEMP] ORDER BY [dep_id]"></asp:SqlDataSource>
    </form>
</body>
</html>
