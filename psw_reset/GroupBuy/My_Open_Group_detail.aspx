<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="My_Open_Group_detail.aspx.cs" Inherits="psw_reset.GroupBuy.My_Open_Group_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />

    <title>我的開團-明細</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../dateCss/datepicker.css" rel="stylesheet" />
    <style type="text/css">
        th {
            text-align: center;
        }
    </style>
</head>
<body style="background-color: #E6DFD5">
    <form id="form1" runat="server">
        <div class="col-md-12">
            <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            <br />
            <br />
            <div class="col-md-4">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        開團資訊
                    </div>
                    <div class="panel-body">
                        <table class="table table-bordered">
                            <tr>
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">團名</td>
                                <td>
                                    <asp:TextBox class="TextStyle" ID="txb_group_name" runat="server" Width="250px"></asp:TextBox></td>
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
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">通知部門編號</td>
                                <td class="auto-style2">
                                    <asp:TextBox class="TextStyle" ID="txb_dep_Notice" runat="server" Width="250px"></asp:TextBox><br />
                                    <span style="color: red">若通知多部門，請使用空白分隔</span></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">通知同工編號</td>
                                <td>
                                    <asp:TextBox class="TextStyle" ID="txb_emp_Notice" runat="server" Width="250px"></asp:TextBox><br />
                                    <span style="color: red">若通知多位，請使用空白分隔</span></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">結單時間</td>
                                <td>
                                    <asp:TextBox class="TextStyle" ID="txb_end_time" runat="server" Width="250px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">開團備註</td>
                                <td>
                                    <asp:TextBox class="TextStyle" ID="txb_note" runat="server" Width="250px" TextMode="MultiLine" Height="90px"></asp:TextBox></td>
                            </tr>
                            <%-- <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">菜單類型</td>
                                <td class="auto-style2">
                                    <asp:RadioButton ID="rbo_open_meun" runat="server" Text="開放式菜單" GroupName="meun" Enabled="False" />
                                    &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbo_fixed_meun" runat="server" Text="固定式菜單" GroupName="meun" Enabled="False" />         
                                </td>
                            </tr>--%>
                        </table>
                        <%-- <table class="table table-bordered" id="table_fixed_meun_1" runat="server" visible="false">
                            <tr>
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">更換店家網址</td>
                                <td>
                                    <asp:TextBox ID="txb_store_addr" runat="server" Width="350px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">更換菜單</td>
                                <td>
                                    <asp:FileUpload ID="FileUpload2" runat="server" /></td>
                            </tr>
                        </table>
                        <table class="table table-bordered" id="table_open_meun_1" runat="server" visible="false">
                            <tr>
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">更換菜單網址</td>
                                <td>
                                    <asp:TextBox ID="txb_meun_addr" runat="server" Width="350px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">更換菜單圖片</td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                            </tr>
                        </table>--%>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_update_buy_info" class="btn btn-warning" runat="server" Text="修改" OnClientClick="if(confirm('是否確認要修改開團資訊?')==false){return false} else{showBlockUI1()}" OnClick="btn_update_buy_info_Click" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_update_buy_info_2" class="btn btn-warning" runat="server" Text="重發通知" OnClientClick="if(confirm('是否確認要修改開團資訊，並重發通知?')==false){return false} else{showBlockUI1()}" OnClick="btn_update_buy_info_2_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table class="table table-bordered">
                            <tr>
                                <td class="info" style="width: 233px; text-align: center; font-size: 14px">通知內容</td>
                                <td>
                                    <asp:TextBox class="TextStyle" ID="txb_mail_txt" runat="server" Width="250px" TextMode="MultiLine" Height="90px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_group_success" class="btn btn-success" runat="server" Text="成團結單" OnClientClick="if(confirm('是否確認此團要成團結單，並發送成團通知?')==false){return false} else{showBlockUI2()}" OnClick="btn_group_success_Click" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_group_lost" class="btn btn-danger" runat="server" Text="流團" OnClientClick="if(confirm('是否確認此團流團，並發送流團通知信?')==false){return false} else{showBlockUI3()}" OnClick="btn_group_lost_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <%--   <div class="panel panel-warning" id="div_open_meun_1" runat="server" visible="false">
                    <div class="panel-heading">
                        開放式菜單-名單
                    </div>
                    <div class="panel-body">
                        <asp:Button ID="btn_export" runat="server" Text="匯出" OnClick="btn_export_Click1"  />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="跟團者同編" HeaderText="跟團者同編" SortExpression="跟團者同編">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="跟團人姓名" HeaderText="跟團人姓名" SortExpression="跟團人姓名">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="跟團品項" HeaderText="跟團品項" SortExpression="跟團品項">
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="金額" HeaderText="金額" SortExpression="金額">
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="支付狀態" HeaderText="支付狀態" SortExpression="支付狀態">
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btn_pay_money" runat="server" Text="支付" CommandName="Update" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [跟團人姓名], [跟團者同編], [跟團品項], [金額], [PK],支付狀態 FROM [Follow_Group_table] WHERE ([團號] = @團號) order by 跟團者同編" UpdateCommand="UPDATE Follow_Group_table SET 支付狀態='已支付' WHERE PK=@PK">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="團號" QueryStringField="PK" Type="String" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="PK" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>--%>
                <div class="panel panel-warning" id="div_fixed_meun_1" runat="server">
                    <div class="panel-heading">
                        跟團名單
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12" id="div_out" runat="server">
                            <asp:Button ID="Button1" runat="server" Text="匯出" OnClick="Button1_Click1" /><br />
                            <br />
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    個人合計
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" OnPreRender="GridView2_PreRender" OnRowDataBound="GridView2_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="PK" HeaderText="PK" SortExpression="PK">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="跟團者同編" HeaderText="跟團者同編" SortExpression="跟團人姓名">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="跟團人姓名" HeaderText="跟團人姓名" SortExpression="跟團人姓名">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="跟團品項" HeaderText="跟團品項" SortExpression="跟團品項">
                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                            </asp:BoundField>
                               <%--             <asp:BoundField DataField="備註" HeaderText="備註" SortExpression="備註">
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="品項單價" HeaderText="品項單價" SortExpression="品項單價">
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="品項數量" HeaderText="品項數量" SortExpression="品項數量">
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="總額" HeaderText="總額" SortExpression="總額">
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="支付狀態" HeaderText="支付狀態" SortExpression="支付狀態">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:TemplateField></asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#FFEEBA" ForeColor="Black" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>"
                                        SelectCommand="SELECT [PK],[團號],[團名],[跟團人姓名],[跟團者同編],[跟團品項],[品項單價],[品項數量],[品項單價]*[品項數量] as [總額],[支付狀態],備註 FROM [fixed_follow_Group_table] WHERE ([團號] = @團號) order by 跟團者同編">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="團號" QueryStringField="PK" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="col-md-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    品項合計
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" ShowFooter="true" OnRowDataBound="GridView3_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="跟團品項" HeaderText="跟團品項" SortExpression="跟團品項">
                                                <ItemStyle Width="400px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="品項單價" HeaderText="品項單價" SortExpression="品項單價">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="總數量" HeaderText="總數量" ReadOnly="True" SortExpression="總數量">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="總價" HeaderText="總價" ReadOnly="True" SortExpression="總價">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#FFEEBA" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#FFEEBA" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [跟團品項],[品項單價],sum([品項數量]) as 總數量,[品項單價]*sum([品項數量]) as 總價 FROM [fixed_follow_Group_table] WHERE ([團號] = @團號) group by [跟團品項],[品項單價]">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="團號" QueryStringField="PK" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" runat="server" visible="false">
                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3">
                                <Columns>
                                    <asp:BoundField DataField="跟團者同編" HeaderText="跟團者同編" SortExpression="跟團者同編" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="  select [跟團者同編] from fixed_follow_Group_table where ([團號] = @團號) group by [跟團者同編]">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="團號" QueryStringField="PK" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <%--   <div class="panel panel-success" id="div_open_meun_2" runat="server" visible="false">
                <div class="panel-heading">
                    菜單
                </div>
                <div class="panel-body">
                    <asp:HyperLink ID="meun_addr" runat="server" Target="_blank">菜單連結</asp:HyperLink><br />
                    <asp:HyperLink ID="img_meun" class="img-responsive" runat="server" ImageWidth="800px">HyperLink</asp:HyperLink>
                </div>
            </div>--%>
            <div class="panel panel-success" id="div_fixed_meun_2" runat="server">
                <div class="panel-heading">
                    店家資訊
                </div>
                <div class="panel-body">
                    <div class="col-md-6">
                        <asp:Button ID="btn_show_meun" class="btn btn-info" runat="server" Text="菜單顯示" OnClick="btn_show_meun_Click" /><br />
                        <br />
                        <table class="table table-bordered">
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家名稱</td>
                                <td>
                                    <asp:Label ID="lab_store_name" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">連絡電話</td>
                                <td>
                                    <asp:Label ID="lab_store_tel" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 37px;">地址</td>
                                <td>
                                    <asp:Label ID="lab_store_addr" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家網址</td>
                                <td>
                                    <asp:HyperLink ID="hyp_store_web_addr" runat="server" Visible="false" Target="_blank">店家連結網址</asp:HyperLink></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家備註</td>
                                <td>
                                    <asp:TextBox class="txb_store_note" ID="txb_group_store_note" runat="server" Width="350px" TextMode="MultiLine" Height="90px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-6" id="div_meun" runat="server" visible="false">
                        <asp:HyperLink ID="img_meun" runat="server" ImageWidth="100%" Visible="True" Target="_blank">HyperLink</asp:HyperLink>
                        <%-- <asp:GridView ID="GV_fixed_meun" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource6" ForeColor="Black" GridLines="Vertical">
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
                        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT PK,[店名], [品名], [價格] FROM [follow_meun]"></asp:SqlDataSource>--%>
                    </div>
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
            $.blockUI({ message: '<h4>修改並重發通知作業處理中，請稍後</h4>' });
        }
        function showBlockUI2() {
            $.blockUI({ message: '<h4>成團結案處理中，請稍後</h4>' });
        }
        function showBlockUI3() {
            $.blockUI({ message: '<h4>流團處理中，請稍後</h4>' });
        }
        //javascript
        //刪除檔案
        function update_pay_state(obj) {
            if (confirm("是否確定已進行支付?")) {
                __doPostBack("update_pay_state", obj.id);
            }
        }

        function delete_order(obj) {
            if (confirm("是否確定刪除此筆跟團?")) {
                __doPostBack("delete_order", obj.id);
            }
        }
        //$(document).ready(function () {
        //    $("#img_meun").colorbox({
        //        width: "90%",
        //        height: "90%",
        //        iframe: true
        //    });
        //});

        //window.addEventListener("resize", function lightboxresize() {
        //$.colorbox.resize({ width: window.innerWidth - 100, height: window.innerHeight - 50 });

        //});

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

