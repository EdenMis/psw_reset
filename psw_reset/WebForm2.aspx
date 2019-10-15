<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="psw_reset.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="原本"></asp:Label><asp:TextBox ID="txb_o" runat="server"></asp:TextBox>
            <br/>
            <asp:Label ID="Label2" runat="server" Text="追加"></asp:Label><asp:TextBox ID="txb_add" runat="server"></asp:TextBox>
            <br/>
            <asp:Label ID="Label3" runat="server" Text="追減"></asp:Label><asp:TextBox ID="txb_less" runat="server"></asp:TextBox>
            <br/>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
