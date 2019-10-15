<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pwd_reset.aspx.cs" Inherits="psw_reset.pwd_reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>密碼重置預設值</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../dateCss/datepicker.css" rel="stylesheet" />
    <style>
        th {
            text-align: center;
        }

        .TableStyle {
            font-size: 12px;
            border-collapse: separate;
        }

            .TableStyle td {
                border: solid 1px white;
                padding: 2px;
            }

        .TableStyle2 {
            font-size: 12px;
            border-collapse: collapse;
        }

            .TableStyle2 td {
                border: solid 1px black;
                padding: 1px;
            }

        .TableTitle {
            font-family: Microsoft JhengHei;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            background-color: #bcd2ff;
        }
        /*.TextStyle {
            height:auto;
            font-size:10px;
        }*/
        .auto-style2 {
            font-family: Microsoft JhengHei;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            background-color: #bcd2ff;
            height: 33px;
        }

        .auto-style3 {
            height: 33px;
        }

        .div1 {
            width: 450px;
            margin-left: auto;
            margin-right: auto;
            text-align: center
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <<div class="div1">
            <asp:TextBox ID="txb_user_no" runat="server" Visible="False"></asp:TextBox><asp:TextBox ID="txb_user_pwd" runat="server" Visible="False"></asp:TextBox>
            <br />
            <h1><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
        </div>
    </form>
</body>
</html>
