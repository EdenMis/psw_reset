<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TEST.aspx.cs" Inherits="psw_reset.TEST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txb_emp" runat="server" Text="6475"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
            <asp:HyperLink ID="HyperLink1" runat="server">連結</asp:HyperLink>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br/>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click1" />
    </form>
</body>
</html>
