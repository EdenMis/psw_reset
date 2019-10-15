<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eClock.aspx.cs" Inherits="psw_reset.eClock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>伊甸線上打卡</title>
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
            width: 700px;
            margin-left: auto;
            margin-right: auto;
            text-align: center
        }
    </style>
</head>
<body style="background-color: #E6DFD5">
    <form id="form1" runat="server">
        <div class="div1">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/menu/EDEN.png" />
            <asp:Label ID="lab_media" runat="server"></asp:Label>
        </div>
        <div class="div1">
            <asp:CheckBox ID="psw_check_false" runat="server" Enabled="False" Visible="False" />
            <h4>
                <asp:Label ID="Label2" runat="server" Text="使用說明" Font-Names="微軟正黑體"></asp:Label></h4>
            <h4>
                <asp:Label ID="Label3" runat="server" Text="1.如需使用此打卡功能，需先向人資申請開通" Font-Names="微軟正黑體"></asp:Label><br />
                <asp:Label ID="Label4" runat="server" Text="2.輸入同工編號及EIP密碼即可打卡，不需選擇上下班" Font-Names="微軟正黑體"></asp:Label></h4>
        </div>
        <br />
        <div class="div1">
            <asp:TextBox ID="txb_ip" runat="server" Enabled="false" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txb_mac" runat="server" Enabled="false" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txb_name" runat="server" Enabled="false" Visible="false"></asp:TextBox>
            <h4>
                <asp:Label ID="lab_name" runat="server" Text="Label" Font-Names="微軟正黑體"></asp:Label></h4>
            <h4>
                <asp:Label ID="lab_IP" runat="server" Text="Label" Font-Names="微軟正黑體"></asp:Label></h4>
            <h4>
                <asp:Label ID="lab_mac" runat="server" Text="Label" Font-Names="微軟正黑體"></asp:Label></h4>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
                    </asp:Timer>
                    <h4>
                        <asp:Label ID="Label1" runat="server" Text="抓取伺服器時間中" Font-Names="微軟正黑體"></asp:Label></h4>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <div class="div1">
            <table class="TableStyle" align="center">
                <tr>
                    <td colspan="2">
                        <h2>
                            <asp:Label ID="lab_sys_msg" runat="server" ForeColor="" Font-Names="微軟正黑體"></asp:Label></h2>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="div1">
            <table class="TableStyle" align="center">
                <tr>
                    <td colspan="2">
                        <h4>
                            <asp:Label ID="Lab_msg" runat="server" ForeColor="Red" Font-Names="微軟正黑體"></asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:HyperLink ID="HyperLink1" runat="server">申請開通電腦</asp:HyperLink>
                    </td>
                    <td class="auto-style3">
                        <asp:HyperLink ID="HyperLink2" runat="server">調整打卡人員</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <table class="TableStyle" align="center">
                <tr>
                    <td class="auto-style2">同工編號</td>
                    <td class="auto-style3">
                        <asp:TextBox class="TextStyle" ID="txb_user_id" runat="server" Width="150px" Height="33px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">EIP密碼</td>
                    <td class="auto-style3">
                        <asp:TextBox class="TextStyle" ID="txb_user_psw" runat="server" Width="150px" Height="33px" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="auto-style3">
                        <asp:Button ID="btn_check" runat="server" Text="進行打卡" CssClass="btn btn-primary" ReadOnly="true" Height="33px" OnClick="btn_check_Click" /></td>
                </tr>
            </table>
            <br />
        </div>
        <script src="../jquery/jquery-1.9.1.js"></script>
        <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
        <script src="../jquery/jquery.blockUI.js"></script>
        <link href="../colorbox/colorbox.css" rel="stylesheet" />
        <script src="../colorbox/jquery.colorbox.js"></script>
        <script type="text/javascript">
            document.oncontextmenu = function () { return false; }

            $(document).ready(function () {
                $("#HyperLink1").colorbox(
                    {
                        width: "90%",
                        height: "70%",
                        iframe: true
                    });
            });

            $(document).ready(function () {
                $("#HyperLink2").colorbox(
                    {
                        width: "90%",
                        height: "70%",
                        iframe: true
                    });
            });
        </script>
    </form>
</body>
</html>
