<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pwd_forget.aspx.cs" Inherits="psw_reset.pwd_forget" %>

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
            <h1><asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" href="http://smart.eden.org.tw/重置密碼教學.pdf">重置密碼說明文件</asp:HyperLink></h1>
            <br />
            <asp:CheckBox ID="psw_check_false" runat="server" Enabled="False" Visible="False" />
            <br />
            <h3>網頁使用說明</h3><br />
            <span>1.請輸入欲重置密碼之同工編號與身分證字號</span><br />
            <span style="color: red">2.請主管收取信件，進行重置作業</span><br />
            <span>3.主管重置完成後，請使用預設密碼登入系統，再進行密碼變更</span><br />
            <span>4.若不知道變更密碼之方式，請參考</span><asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" href="http://faq.eden.org.tw/Sysquestion/SysQA.aspx?action=263&Type=35">FAQ</asp:HyperLink><br />

        </div>
        <br />
        <br />
        <div class="div1">
            <table class="TableStyle" align="center">
                <tr>
                    <td class="auto-style2">身分證字號</td>
                    <td class="auto-style3">
                        <asp:TextBox class="TextStyle" ID="txb_ID" runat="server" Width="150px" Height="33px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">同工編號</td>
                    <td class="auto-style3">
                        <asp:TextBox class="TextStyle" ID="txb_user_id" runat="server" Width="150px" Height="33px" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>       
                    <td class="auto-style3">
                        <asp:Button ID="btn_recover_psw" runat="server" Text="申請重置密碼" CssClass="btn btn-primary" ReadOnly="true" Height="33px" OnClick="btn_recover_psw_Click" OnClientClick="showBlockUI1()"/>
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
        <script src="../jquery/jquery.blockUI.js"></script>
        <script type="text/javascript">
            function showBlockUI1()
            {
                $.blockUI({ message: '<h4>處理中，請稍後</h4>' });
            }
        </script>
    </form>
</body>
</html>

