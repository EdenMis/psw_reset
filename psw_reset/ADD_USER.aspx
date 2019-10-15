<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADD_USER.aspx.cs" Inherits="psw_reset.ADD_USER" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />

    <title></title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        th {
            text-align: center;
        }

        img {
            border: 1px solid black;
            margin: 2px;
            width: 180px;
            height: 180px;
            object-fit: cover;
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
        <div class="col-md-3">
        </div>
        <div class="col-md-6">

            <span style="color: red">請輸入要使用本台電腦打卡人員之同工編號，並使用逗號作為分隔(EX:6475,7139,7183)</span>
            <table class="table table-bordered">
                <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">本台電腦名稱</td>
                    <td>
                        <asp:Label ID="lab_name" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">本台電腦IP</td>
                    <td>
                        <asp:Label ID="lab_ip" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">本台電腦MAC</td>
                    <td>
                        <asp:Label ID="lab_mac" runat="server" Text="Label"></asp:Label></td>
                </tr>
                    <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">電腦所在單位(代碼)</td>
                    <td>
                        <asp:TextBox class="TextStyle" ID="txb_PC_dep" runat="server" Width="500px" MaxLength="3" AutoPostBack="True" OnTextChanged="txb_PC_dep_TextChanged"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">可使用同工</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TextStyle" ID="txb_emp_Notice" runat="server" Width="500px" Height="60px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>        
                <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">申請人姓名</td>
                    <td>
                        <asp:TextBox class="TextStyle" ID="txb_name" runat="server" Width="500px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">申請人聯絡電話</td>
                    <td>
                        <asp:TextBox class="TextStyle" ID="txb_tel" runat="server" Width="500px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">申請原因</td>
                    <td>
                        <asp:TextBox class="TextStyle" ID="txb_re" runat="server" Width="500px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="info" style="width: 170px; text-align: center; font-size: 14px">電腦所在地址</td>
                    <td>
                        <asp:TextBox class="TextStyle" ID="txb_PC_addr" runat="server" Width="500px"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btn_open" runat="server" CssClass="btn btn-primary" Text="申請開通" OnClick="btn_open_Click" OnClientClick="showBlockUI1()" /></td>
                </tr>
            </table>
        </div>
    </form>
    <script src="../jquery/jquery-1.9.1.js"></script>
    <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <link href="../colorbox/colorbox.css" rel="stylesheet" />
    <script src="../colorbox/jquery.colorbox.js"></script>
    <script src="../jquery/jquery.blockUI.js"></script>
    <script type="text/javascript">
        function showBlockUI1() {
            $.blockUI({ message: '<h4>處理中，請稍後</h4>' });
        }
        $(document).ready(function () {
            $(".img_group").colorbox({
                rel: "img_group",
                width: "90%"
            });
        });
    </script>
</body>
</html>

