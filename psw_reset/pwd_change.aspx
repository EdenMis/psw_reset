<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pwd_change.aspx.cs" Inherits="psw_reset.pwd_change" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>密碼修改頁面</title>
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
    <form id="form1" runat="server" >
        <div class="div1">
            <h1>EIP密碼修改</h1>
            <br />
            <asp:CheckBox ID="psw_check_false" runat="server" Enabled="False" Visible="False" /><asp:TextBox ID="txb_emp_no" runat="server" Enabled="false" Visible="false"></asp:TextBox><asp:TextBox ID="txb_user_pwd" runat="server" Enabled="false" Visible="false"></asp:TextBox>
            <br />
            <h3>網頁使用說明</h3><br />
            <span>1.密碼可使用英文字母、數字與符號，長度需為6~10字之間</span><br />
            <span>2.新密碼欄位與新密碼確認欄位內容須一致</span><br />
            <span style="color: red">3.按下修改密碼，則密碼修改成功；再進入EIP時，請以新密碼登入</span><br />
             <span style="color: red">4.本功能將同步更新HR、BPM、ERP、PMS之密碼</span><br /><br /><br />

        </div>
        <div class="div1">
            <table class="TableStyle" align="center">        
                <tr>
                    <td class="auto-style2">新密碼</td>
                    <td class="auto-style3">
                        <asp:TextBox class="TextStyle" ID="txb_new_pwd" runat="server" Width="150px" Height="33px" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">新密碼確認</td>
                    <td class="auto-style3">
                        <asp:TextBox class="TextStyle" ID="txb_new_pwd_chk" runat="server" Width="150px" Height="33px" MaxLength="10" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="auto-style3">
                        <asp:Button ID="btn_change_psw" runat="server" Text="修改密碼" CssClass="btn btn-primary" ReadOnly="true" Height="33px" OnClick="btn_change_psw_Click"  />
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
