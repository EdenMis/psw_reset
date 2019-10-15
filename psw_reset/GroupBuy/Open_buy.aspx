<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open_buy.aspx.cs" Inherits="psw_reset.GroupBuy.Open_buy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />
 
    <title>開團中</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        th {
            text-align: center;
        }
        img {
                border: 1px solid black;
                margin:2px;
                width: 600px;
                height:600px;
                object-fit: cover;
        }
    </style>
</head>
<body style="background-color: #E6DFD5">
    <form id="form1" runat="server">
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    開團相關資訊填寫
                </div>
                <div class="panel-body">
                    <%-- <table class="TableStyle"> .table-bordered--%>
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">團名</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_name" runat="server" Width="350px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">讀取權限</td>
                            <td class="auto-style2">
                                <asp:RadioButton ID="rdo_dep_emp" runat="server" Text="僅被通知部門與同工" GroupName="follow" Checked="true" />
                                &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rdo_all" runat="server" Text="全會同工" GroupName="follow" />
                                <%-- <asp:CheckBox ID="chk_all_follow" runat="server" /><span style="color: red">若勾選，則全會可跟團</span></td>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">我的跟團群</td>
                               <td class="auto-style2">
                                   <asp:DropDownList ID="dlist_user_group_list" runat="server" DataSourceID="SqlDataSource2" DataTextField="user_group_name" DataValueField="user_group_name" AppendDataBoundItems="true"  OnSelectedIndexChanged="dlist_user_group_list_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem>  </asp:ListItem>
                                </asp:DropDownList>
                                   <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../GroupBuy/user_group_list.aspx?PK=0" Target="_blank">建立我的跟團群</asp:HyperLink>
                                   <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" SelectCommand="SELECT [user_group_name] FROM [user_group_list] WHERE ([owner] = @owner)">
                                       <SelectParameters>
                                           <asp:SessionParameter Name="owner" SessionField="User_ID" Type="String" />
                                       </SelectParameters>
                                   </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">通知部門編號</td>
                            <td class="auto-style2">
                                <asp:TextBox class="TextStyle" ID="txb_dep_Notice" runat="server" Width="350px"></asp:TextBox><br />
                                <span style="color: red">若通知多部門，請使用空白分隔</span></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">通知同工編號</td>
                            <td class="auto-style3">
                                <asp:TextBox class="TextStyle" ID="txb_emp_Notice" runat="server" Width="350px"></asp:TextBox><br />
                                <span style="color: red">若通知多位，請使用空白分隔</span></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">結單時間</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_end_time" runat="server" Width="350px"></asp:TextBox></td>
                        </tr>
                                 <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">選擇店家</td>
                            <td>
                                <asp:DropDownList ID="Dlsit_store_type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Dlsit_store_type_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">請選擇</asp:ListItem>
                                    <asp:ListItem>點心</asp:ListItem>
                                    <asp:ListItem>飲料</asp:ListItem>
                                    <asp:ListItem>主餐</asp:ListItem>
                                    <asp:ListItem>其他</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp;
                                <asp:DropDownList ID="Dlist_store" runat="server" DataSourceID="SqlDataSource1" DataTextField="店名" DataValueField="店名" AppendDataBoundItems="true" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="Dlist_store_SelectedIndexChanged">
                                    <asp:ListItem>  </asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" SelectCommand="SELECT [PK], [店名],類型 FROM [Group_store_table] where 店名=''"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">開團備註</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_note" runat="server" Width="350px" TextMode="MultiLine" Height="90px"></asp:TextBox></td>
                        </tr>
<%--                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">菜單圖片上傳</td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                        </tr>--%>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btn_add_group" class="btn btn-primary" runat="server" Text="開團" OnClick="btn_add_group_Click" OnClientClick="if(confirm('是否確認要開團?')==false){return false} else{showBlockUI1()}" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
               <div class="col-md-8">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    店家相關資訊
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <table class="table table-bordered">
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">店名</td>
                                <td>
                                    <asp:Label ID="lab_store_name" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">類型</td>
                                <td>
                                    <asp:Label ID="lab_store_type" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">地址</td>
                                <td>
                                    <asp:Label ID="lab_store_addr" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">電話</td>
                                <td>
                                    <asp:Label ID="lab_store_tel" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">網址</td>
                                <td>
                                    <asp:HyperLink ID="hyp_store_web_addr" runat="server" Text="" Target="_blank"></asp:HyperLink></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家備註</td>
                                <td>
                                    <asp:TextBox class="TextStyle" ID="txb_group_store_note" runat="server" Width="350px" TextMode="MultiLine" Height="90px" Enabled="false" BackColor="Gainsboro"></asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-12">
                        <asp:Panel ID="img_panel" runat="server">

                        </asp:Panel>
                        <%--<asp:HyperLink ID="img_meun" runat="server" ImageWidth="100%" Visible="False" Target="_blank">HyperLink</asp:HyperLink>--%>
                     <%--   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="品名" HeaderText="品名" SortExpression="品名">
                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="價格" HeaderText="價格" SortExpression="價格">
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" SelectCommand="SELECT [PK], [店名], [品名], [價格] FROM [follow_meun] where 店名=''"></asp:SqlDataSource>--%>
                    </div>
                </div>
            </div>
        </div>
    </form>
<%--        <table class="table table-bordered" id="table_fixed_meun_1" runat="server" visible="false">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家介紹網址</td>
                               <td>
                                <asp:TextBox ID="txb_meun_addr_2" runat="server" Width="350px"></asp:TextBox></td>
                        </tr>
                    </table>

                    <table class="table table-bordered" id="table_fixed_meun" runat="server" visible="false">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">菜單EXCEL上傳<br/>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://192.168.246.10:1700/NPOI/菜單範本.xlsx">範本下載</asp:HyperLink></td>
                            <td>
                                <asp:FileUpload ID="FileUpload2" runat="server" /></td>
                            <td>
                                <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="開團" OnClientClick="if(confirm('是否確認要開團?')==false){return false} else{showBlockUI1()}" OnClick="Button2_Click" /></td>
                        </tr>
                    </table>

                    <table class="table table-bordered" id="table_open_meun_1" runat="server" visible="false">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">菜單網址</td>
                            <td>
                                <asp:TextBox ID="txb_meun_addr" runat="server" Width="350px"></asp:TextBox></td>
                        </tr>
                    </table>

                    <table class="table table-bordered" id="table_open_meun_2" runat="server" visible="false">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">菜單圖片上傳</td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                            <td>
                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="開團" OnClick="Button1_Click1" OnClientClick="if(confirm('是否確認要開團?')==false){return false} else{showBlockUI1()}" /></td>
                        </tr>
                    </table>--%>
    <script src="../jquery/jquery-1.9.1.js"></script>
    <script src="../jquery-ui-1.12.1/jquery-ui.min.js"></script>
    <link href="../jquery-ui-1.12.1/jquery-ui.min.css" rel="stylesheet" />
    <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <script src="../jquery/jquery.blockUI.js"></script>
    <link href="../dist/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />
    <script src="../dist/jquery-ui-timepicker-addon.min.js"></script>
        <link href="../colorbox/colorbox.css" rel="stylesheet" />
    <script src="../colorbox/jquery.colorbox.js"></script>
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

        $(document).ready(function () {
            $(".img_group").colorbox({
                rel: "img_group",
                width: "90%"
            });
        });
    </script>
</body>
</html>
