<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="psw_reset.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../dateCss/alertify.core.css" rel="stylesheet" />
    <link href="../dateCss/alertify.default.css" rel="stylesheet" id="toggleCSS" />
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btn_send" class="btn btn-primary" runat="server" Text="送出" Width="100px" Height="30px" OnClientClick="defaults = { ok: '已上傳', cancel: '重新上傳'};if(confirm('是否確認要建立店家?')==false){return false} else{}" />
        </div>
        <script src="../jquery/jquery-1.9.1.js"></script>
        <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
        <script src="../jquery/jquery.blockUI.js"></script>
        <script src="../jquery/alertify.min.js"></script>

        <script type="text/javascript">

            function performDelete(a_element) {
                return true;
            }

            function confirmAction(a_element, message, action) {
                alertify.set({ labels: { ok: "已上傳", cancel: "重新上傳" } });
                alertify.confirm(message, function (e) {
                    if (e) {
                        // a_element is the <a> tag that was clicked
                        if (action) {
                            action(a_element);
                        }
                    }
                });
            }

            $(function newbox() {
                $.messager.defaults = { ok: "是", cancel: "否" };

                $.messager.confirm("操作提示", "您确定要执行操作吗？", function (data) {
                    if (data) {
                        alert("是");
                    }
                    else {
                        alert("否");
                    }
                });
            });
        </script>
    </form>
</body>
</html>
