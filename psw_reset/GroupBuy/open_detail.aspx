<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="open_detail.aspx.cs" Inherits="psw_reset.GroupBuy.open_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />

    <title>跟團中</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../jquery/exif.js"></script>
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
    </style>

</head>
<body style="background-color: #E6DFD5">
    <form id="form1" runat="server">
        <div class="col-md-12">
            <%--            <div class="col-md-4" id="div_open_meun_1" runat="server" visible="false">
                <br />
                <br />
                <table class="table table-bordered">
                    <tr>
                        <td class="info" style="width: 170px; text-align: center; font-size: 14px">品名</td>
                        <td>
                            <asp:TextBox class="TextStyle" ID="txb_buy_list" runat="server" Width="300px" TextMode="MultiLine" Height="100px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 170px; text-align: center; font-size: 14px">金額</td>
                        <td>
                            <asp:TextBox class="TextStyle" ID="txb_buy_money" runat="server" Width="300px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Button ID="btn_follow" class="btn btn-primary" runat="server" Text="我要跟團" OnClick="btn_follow_Click" OnClientClick="if(confirm('是否確認要跟團')==false){return false}" /></td>

                    </tr>
                </table>
            </div>

            <div class="col-md-8" id="div_open_meun_2" runat="server" visible="false">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        目前跟團者
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="跟團人姓名" HeaderText="跟團人姓名" SortExpression="跟團人姓名">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="跟團品項" HeaderText="跟團品項" SortExpression="跟團品項">
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="金額" HeaderText="金額" SortExpression="金額">
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
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
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [跟團者同編], [跟團人姓名], [跟團品項], [金額] FROM [Follow_Group_table] WHERE ([團號] = @團號)">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="團號" QueryStringField="PK" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>--%>
            <div class="col-md-3" id="div_fixed_meun_1" runat="server">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        本團資訊
                    </div>
                    <div class="panel-body">
                        <table class="table table-bordered">
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">團名</td>
                                <td>
                                    <asp:Label ID="lab_group_name" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">開團人</td>
                                <td>
                                    <asp:Label ID="lab_group_emp_name" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">開團備註</td>
                                <td>
                                    <asp:TextBox class="TextStyle" ID="txb_group_note" runat="server" Width="280px" TextMode="MultiLine" Height="90px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">店家名稱</td>
                                <td>
                                    <asp:Label ID="lab_store_name" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">店家類型</td>
                                <td>
                                    <asp:Label ID="lab_store_type" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">店家介紹</td>
                                <td>
                                    <asp:HyperLink ID="hyp_store_web_addr" runat="server" Visible="false" Target="_blank">店家連結網址</asp:HyperLink></td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">店家備註</td>
                                <td>
                                    <asp:TextBox class="txb_store_note" ID="txb_group_store_note" runat="server" Width="280px" TextMode="MultiLine" Height="90px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-4" id="div1" runat="server">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        菜單
                    </div>
                    <div class="panel-body">
                        <asp:Button class="btn btn-success" ID="btn_follow2" runat="server" Text="下單" OnClick="btn_follow2_Click" OnClientClick="if(confirm('是否確認要下單?')==false){return false} else{}" /><br />
                        <span style="color: red">下單前請先確認品項有在菜單上</span>
                        <br />
                        <table class="table table-bordered">
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">品名</td>
                                <td>
                                    <asp:TextBox ID="txb_order_name" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-bordered" id="table_drink_order" runat="server" visible="false">
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">大小</td>
                                <td>
                                    <asp:DropDownList ID="Dlist_drink_size" runat="server">
                                        <asp:ListItem Value="請選擇">請選擇</asp:ListItem>
                                        <asp:ListItem>瓶裝</asp:ListItem>
                                        <asp:ListItem>大杯</asp:ListItem>
                                        <asp:ListItem>中杯</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">冰塊(含熱飲)</td>
                                <td>
                                    <asp:DropDownList ID="Dlist_drink_ice" runat="server">
                                        <asp:ListItem>請選擇</asp:ListItem>
                                        <asp:ListItem>正常冰</asp:ListItem>
                                        <asp:ListItem>少冰</asp:ListItem>
                                        <asp:ListItem>微冰</asp:ListItem>
                                        <asp:ListItem>去冰</asp:ListItem>
                                        <asp:ListItem>熱飲</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">甜度</td>
                                <td>
                                    <asp:DropDownList ID="Dlist_drink_sweetness" runat="server">
                                        <asp:ListItem>請選擇</asp:ListItem>
                                        <asp:ListItem>正常糖</asp:ListItem>
                                        <asp:ListItem>七分糖</asp:ListItem>
                                        <asp:ListItem>半糖</asp:ListItem>
                                        <asp:ListItem>三分糖</asp:ListItem>
                                        <asp:ListItem>無糖</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">備註</td>
                                <td>
                                    <asp:TextBox ID="txb_drink_note" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-bordered" id="table_other_order" runat="server" visible="false">
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">備註</td>
                                <td>
                                    <asp:TextBox ID="txb_other_note" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-bordered">
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">數量</td>
                                <td>
                                    <asp:TextBox ID="txb_order_num" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 100px; text-align: center; font-size: 14px">價格</td>
                                <td>
                                    <asp:TextBox ID="txb_meun_money" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <%--           <asp:TextBox ID="txb_meun_select" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_meun_select" runat="server" class="btn btn-primary" Text="搜尋菜單" OnClick="btn_meun_select_Click" />--%>
                        <br />
                        <br />
                        <span style="color: red">點擊圖片可看大圖，若無菜單圖片，請看店家介紹</span>
                        <br />
                        <asp:Panel ID="img_panel" runat="server">
                        </asp:Panel>
                        <asp:HyperLink ID="img_meun" runat="server" ImageWidth="100%" Visible="false" Target="_blank">HyperLink</asp:HyperLink>
                        <%--   <asp:GridView ID="GV_fixed_meun" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GV_fixed_meun_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="品名" HeaderText="品名" SortExpression="品名">
                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="價格" HeaderText="價格" SortExpression="價格">
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="數量">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txb_buy_num" runat="server" Width="50px" Text="0"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="備註">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txb_buy_note" runat="server" Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
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
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT PK,[店名], [品名], [價格] FROM [follow_meun] order by 品名"></asp:SqlDataSource>--%>
                    </div>
                </div>
            </div>
            <div class="col-md-5" id="div_fixed_meun_2" runat="server">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        跟團情況
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    個人合計
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" OnPreRender="GridView2_PreRender" OnRowDataBound="GridView2_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="跟團者同編" HeaderText="跟團者同編" SortExpression="跟團人姓名">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="跟團人姓名" HeaderText="跟團人姓名" SortExpression="跟團人姓名">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="跟團品項" HeaderText="跟團品項" SortExpression="跟團品項">
                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                            </asp:BoundField>
                                            <%--                                            <asp:BoundField DataField="備註" HeaderText="備註" SortExpression="備註">
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
                            <div class="col-md-12">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        品項合計
                                    </div>
                                    <div class="panel-body">
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" ShowFooter="true" OnRowDataBound="GridView3_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="跟團品項" HeaderText="跟團品項" SortExpression="跟團品項">
                                                    <ItemStyle Width="320px" HorizontalAlign="Center" />
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
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [跟團品項],[品項單價],[品項單價],sum([品項數量]) as 總數量,[品項單價]*sum([品項數量]) as 總價 FROM [fixed_follow_Group_table] WHERE ([團號] = @團號) group by [跟團品項],[品項單價]">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="團號" QueryStringField="PK" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--      <div class="col-md-12">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>開團相關資訊
                    </div>
                    <div class="panel-body">
                        <div class="col-md-6">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    開團資訊
                                </div>
                                <div class="panel-body">
                                    <table class="table table-bordered">
                                        <tr>
                                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">團名</td>
                                            <td>
                                                <asp:TextBox class="TextStyle" ID="txb_Group_name" runat="server" Width="300px" Enabled="false" BackColor="Gainsboro"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">開團人</td>
                                            <td>
                                                <asp:TextBox class="TextStyle" ID="txb_GroupEmp_name" runat="server" Width="300px" Enabled="false" BackColor="Gainsboro"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">開團備註</td>
                                            <td>
                                                <asp:TextBox class="TextStyle" ID="txb_group_note" runat="server" Width="300px" TextMode="MultiLine" Height="100px" Enabled="false" BackColor="Gainsboro"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-md-12" id="div_open_meun_3" runat="server" visible="false">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        菜單
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-md-12">
                                            <asp:HyperLink ID="meun_addr" runat="server" Target="_blank">菜單連結</asp:HyperLink><br />
                                            <asp:HyperLink ID="img_meun" runat="server" ImageWidth="100%" Visible="False">HyperLink</asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            </div>
    </form>
    <script src="../jquery/jquery-1.9.1.js"></script>
    <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <link href="../colorbox/colorbox.css" rel="stylesheet" />
    <script src="../colorbox/jquery.colorbox.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".img_group").colorbox({
                rel: "img_group",
                maxWidth: "90%"
            });
        });

        document.getElementById('img0').onload = function () {
            EXIF.getData(this, function () {
                var imgTarget = $('#img0');
                var orientation = EXIF.getTag(this, 'Orientation');
                if (orientation && orientation !== 1) {
                    switch (orientation) {
                        case 6:
                            imgTarget.css({ transform: "rotate(90deg)" });
                            break;
                        case 8:
                            //逆时针90度旋转

                            imgTarget.css({ transform: "rotate(270deg)" });
                            break;
                        case 3:
                            //180度旋转

                            imgTarget.css({ transform: "rotate(180deg)" });
                            break;
                    }
                }
            });
        }

        document.getElementById('img1').onload = function () {
            EXIF.getData(this, function () {
                var imgTarget = $('#img1');
                var orientation = EXIF.getTag(this, 'Orientation');
                if (orientation && orientation !== 1) {
                    switch (orientation) {
                        case 6:
                            imgTarget.css({ transform: "rotate(90deg)" });
                            break;
                        case 8:
                            //逆时针90度旋转

                            imgTarget.css({ transform: "rotate(270deg)" });
                            break;
                        case 3:
                            //180度旋转

                            imgTarget.css({ transform: "rotate(180deg)" });
                            break;
                    }
                }
            });
        }

        document.getElementById('img2').onload = function () {
            EXIF.getData(this, function () {
                var imgTarget = $('#img2');
                var orientation = EXIF.getTag(this, 'Orientation');
                if (orientation && orientation !== 1) {
                    switch (orientation) {
                        case 6:
                            imgTarget.css({ transform: "rotate(90deg)" });
                            break;
                        case 8:
                            //逆时针90度旋转

                            imgTarget.css({ transform: "rotate(270deg)" });
                            break;
                        case 3:
                            //180度旋转

                            imgTarget.css({ transform: "rotate(180deg)" });
                            break;
                    }
                }
            });
        }
        //window.addEventListener("resize", function lightboxresize() {
        //    $.colorbox.resize({ width: window.innerWidth - 100, height: window.innerHeight - 50 });

        //});


        //$(document).ready(function () {
        //    $("#img_meun").colorbox({
        //        width: "90%",
        //        height: "90%",
        //        iframe: true
        //    });
        //});

        //window.addEventListener("resize", function lightboxresize() {
        //    $.colorbox.resize({ width: window.innerWidth - 100, height: window.innerHeight - 50 });

        //});
    </script>
</body>
</html>

