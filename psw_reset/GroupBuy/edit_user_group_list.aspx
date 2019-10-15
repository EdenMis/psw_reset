<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_user_group_list.aspx.cs" Inherits="psw_reset.GroupBuy.edit_user_group_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />
    
    <title>跟團群建立/調整中</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            height: 39px;
        }
        </style>
</head>
<body style="background-color: #E6DFD5">
    <form id="form1" runat="server">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    跟團群資訊
                </div>
                <div class="panel-body">
                    <asp:Button ID="btn_add_group_store" class="btn btn-primary" runat="server" Text="存檔" OnClientClick="if(confirm('是否確認要存檔?')==false){return false} else{}" OnClick="btn_add_group_store_Click"/><br/><br/>
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">群名</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_user_group_name" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">通知部門</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_dep_Notice" runat="server" Width="500px" TextMode="MultiLine" Height="90px"></asp:TextBox><br/><span style="color: red">多部門，請使用空白分隔</span></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px;">通知同工</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_emp_Notice" runat="server" Width="500px" TextMode="MultiLine" Height="90px"></asp:TextBox><br/><span style="color: red">多位，請使用空白分隔</span></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
    <script src="../jquery/jquery-1.9.1.js"></script>
    <script src="../jquery-ui-1.12.1/jquery-ui.min.js"></script>
    <link href="../jquery-ui-1.12.1/jquery-ui.min.css" rel="stylesheet" />
    <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <script src="../jquery/jquery.blockUI.js"></script>
    <link href="../dist/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />
    <script src="../dist/jquery-ui-timepicker-addon.min.js"></script>
</body>
</html>
