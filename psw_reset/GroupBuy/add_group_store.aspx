<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_group_store.aspx.cs" Inherits="psw_reset.GroupBuy.add_group_store" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />
    
    <title>建立店家中</title>
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
                    店家資訊填寫
                </div>
                <div class="panel-body">
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店名</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_store_name" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">使用權限</td>
                            <td class="auto-style2">
                                <asp:RadioButton ID="rdo_dep_emp" runat="server" Text="僅可使用部門與同工" GroupName="follow" Checked="true" />
                                &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rdo_all" runat="server" Text="全會同工" GroupName="follow" />
                                <%-- <asp:CheckBox ID="chk_all_follow" runat="server" /><span style="color: red">若勾選，則全會可跟團</span></td>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">可使用部門編號</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_dep_Notice" runat="server" Width="500px"></asp:TextBox><br/><span style="color: red">多部門，請使用空白分隔</span></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">可使用同工編號</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_emp_Notice" runat="server" Width="500px"></asp:TextBox><br/><span style="color: red">多位，請使用空白分隔</span></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家類型</td>
                            <td>
                                <asp:DropDownList ID="Dlist_store_type" runat="server">
                                    <asp:ListItem Selected="True">請選擇</asp:ListItem>
                                    <asp:ListItem>點心</asp:ListItem>
                                    <asp:ListItem>飲料</asp:ListItem>
                                    <asp:ListItem>主餐</asp:ListItem>
                                    <asp:ListItem>其他</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">店家地址</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_store_addr" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">店家電話</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_store_tel" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家介紹網址</td>
                               <td>
                                <asp:TextBox ID="txb_group_store_web_addr" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家備註</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_store_note" runat="server" Width="500px" TextMode="MultiLine" Height="90px"></asp:TextBox></td>
                        </tr>
                    </table>

                    <table class="table table-bordered" id="table_fixed_meun" runat="server">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">菜單圖片上傳<br/><span style="color: red">支援多檔上傳，圖片格式為jpg,png</span></td>
                                <%--<br/><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://192.168.246.11:1001/NPOI/菜單範本.xlsx">範本下載</asp:HyperLink></td>--%>
                            <td>
                                <asp:FileUpload ID="FileUpload2" runat="server" AllowMultiple="True" Height="22px" /></td>
                            <td>
                                <asp:Button ID="btn_add_group_store" class="btn btn-primary" runat="server" Text="建立店家" OnClientClick="if(confirm('是否確認要建立店家?')==false){return false} else{}" OnClick="btn_add_group_store_Click"/></td>
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
