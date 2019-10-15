<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="psw_reset.GroupBuy.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />
 
    <title></title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../dateCss/datepicker.css" rel="stylesheet" />
    <style>
        th {
            text-align: center;
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
            <%-- 功能列表，申請人資訊，流程資訊 --%>
            <div class="container col-md-2 col-md-offset-5" style="text-align: center">
                <h1>訂餐系統</h1>
                <br />
                <br />
                <table class="table table-bordered">

                    <tr>
                        <td class="warning" style="width: 200px">登入帳號</td>
                        <td>

                            <asp:TextBox class="TextStyle" ID="txb_user_ID" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="warning" style="width: 200px">登入密碼</td>
                        <td>
                            <asp:TextBox class="TextStyle" ID="txb_password" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btn_login" runat="server" Text="登入" CssClass="btn btn-primary" ReadOnly="true" Height="33px" OnClick="btn_login_Click" /><br />
                <asp:Label ID="Label1" runat="server" ForeColor="Red" />
            </div>
        <script src="../jquery/jquery-1.9.1.js"></script>
        <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    </form>
</body>
</html>
