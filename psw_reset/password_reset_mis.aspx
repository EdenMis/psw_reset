<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="password_reset_mis.aspx.cs" Inherits="psw_reset.password_reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>HR密碼還原網頁</title>
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
        <div class="div1">
            <h1>MIS用_HR密碼還原網頁</h1>
            <asp:TextBox ID="txb_login_emp_no" runat="server" Enabled="false" Visible="false"></asp:TextBox><asp:TextBox ID="txb_user_pwd" runat="server" Enabled="false" Visible="false"></asp:TextBox>
        </div>
        <br />
        <br />
        <div class="div1">
            <table class="TableStyle" align="center">
                <tr>
                    <td class="auto-style2">同工編號</td>
                    <td class="auto-style3">
                        <asp:TextBox class="TextStyle" ID="txb_emp_no" runat="server" Width="150px" Height="33px"></asp:TextBox>
                    </td>
                     <td class="auto-style3">
                         <asp:Button ID="btn_recover_psw" class="btn btn-success" runat="server" Text="還原密碼" OnClick="btn_recover_psw_Click" OnClientClick="if(confirm('是否確認要還原此同工之密碼')==false){return false}"/>
                    </td>
                </tr>
            </table>
            <br />
            <table class="TableStyle" align="center">
                  <tr>
                    <td>
                        <asp:Label ID="Lab_msg" runat="server" ForeColor="Red"></asp:Label></td>
                </tr>
            </table>
        </div>


        <script src="../jquery/jquery-1.9.1.js"></script>
        <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    </form>
</body>
</html>
