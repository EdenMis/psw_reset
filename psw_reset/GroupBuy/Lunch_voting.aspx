<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lunch_voting.aspx.cs" Inherits="psw_reset.GroupBuy.Lunch_voting" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />
    
    <title>投票中</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        th {
            text-align: center;
        }
    </style>
</head>
<body style="background-color: #E6DFD5">
    <form id="form1" runat="server">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    投票
                </div>
                <div class="panel-body">
                    <%-- <table class="TableStyle"> .table-bordered--%>
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店名</td>
                            <td>
                                <asp:Label ID="lab_store_name" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">評價</td>
                            <td class="auto-style2">
                                <asp:RadioButton ID="rdo_bad" runat="server" Text="難吃" GroupName="voting" />
                                <br/><asp:RadioButton ID="rdo_ordinary" runat="server" Text="普通" GroupName="voting" Checked="true"/>
                                <br/><asp:RadioButton ID="rdo_good" runat="server" Text="好吃" GroupName="voting" />
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">意見</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_note" runat="server" Width="350px" TextMode="MultiLine" Height="90px"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btn_voting" class="btn btn-primary" runat="server" Text="投票" OnClientClick="if(confirm('是否確認要投票?')==false){return false} else{}" OnClick="btn_voting_Click" style="height: 36px" /></td>
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
    <script type="text/javascript">
        function showBlockUI1() {
            $.blockUI({ message: '<h4>發信通知中，請稍後</h4>' });
        }

        $('#txb_end_time').prop("readonly", true).datetimepicker({
            timeText: '時間',
            hourText: '小時',
            minuteText: '分鐘',
            secondText: '秒',
            currentText: '現在',
            closeText: '完成',
            showSecond: true, //显示秒  
            dateFormat: "yy-mm-dd",
            timeFormat: 'HH:mm:ss' //格式化时间 
        });
    </script>
</body>
</html>
