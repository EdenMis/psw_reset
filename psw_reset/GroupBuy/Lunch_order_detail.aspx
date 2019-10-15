<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lunch_order_detail.aspx.cs" Inherits="psw_reset.GroupBuy.Lunch_order_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />
    
    <title>午餐開團-明細</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../dateCss/datepicker.css" rel="stylesheet" />
    <style type="text/css">
        th {
            text-align: center;
        }

        .auto-style1 {
            height: 37px;
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
                        午餐開團資訊
                    </div>
                    <div class="panel-body">
                        <table class="table table-bordered">
                            <tr>
                                <td class="info" style="width: 250px; text-align: center; font-size: 14px">通知內容</td>
                                <td>
                                    <asp:TextBox class="TextStyle" ID="txb_mail_txt" runat="server" Width="250px" TextMode="MultiLine" Height="90px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_group_success" class="btn btn-danger" runat="server" Text="關閉訂餐" OnClientClick="if(confirm('是否確認本日要關閉訂餐，並發送通知給訂餐同工?')==false){return false} else{showBlockUI2()}" OnClick="btn_group_success_Click1" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" class="btn btn-info" runat="server" Text="開放訂餐" OnClientClick="if(confirm('是否確認要再度開放訂餐?')==false){return false} else{showBlockUI2()}" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="panel panel-warning" id="div_fixed_meun_1" runat="server">
                    <div class="panel-heading">
                        午餐訂餐名單
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12" id="div_out" runat="server">
                            <div class="col-md-6">
                                <table class="table table-bordered">
                                    <tr>
                                        <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 37px;">匯出功能</td>
                                        <td class="auto-style1">
                                            <asp:Button ID="btn_export" runat="server" Text="匯出" OnClick="btn_export_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 37px;">標籤起始位置</td>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="Dlist_tag_start" runat="server">
                                                <asp:ListItem Selected="True">1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                <%--                <asp:ListItem>31</asp:ListItem>
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_export2" runat="server" Text="產生標籤檔" OnClick="btn_export2_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="col-md-12">
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
                                                <asp:BoundField DataField="訂餐者部門" HeaderText="訂餐者部門" SortExpression="訂餐者部門">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="訂餐者同編" HeaderText="訂餐者同編" SortExpression="訂餐者同編">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="訂餐人姓名" HeaderText="訂餐人姓名" SortExpression="訂餐人姓名">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="訂餐品項" HeaderText="訂餐品項" SortExpression="訂餐品項">
                                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                </asp:BoundField>
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
                                            SelectCommand="SELECT [PK],[訂餐日期],[午餐店家],[訂餐人姓名],[訂餐者同編],[訂餐者部門],[訂餐品項],[品項單價],[品項數量],[品項單價]*[品項數量] as [總額],[支付狀態],備註 FROM [lunch_order_table] WHERE ([訂餐日期] = @訂餐日期) order by 訂餐者部門,訂餐者同編">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="訂餐日期" QueryStringField="order_date" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
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
                                            <asp:BoundField DataField="訂餐品項" HeaderText="訂餐品項" SortExpression="訂餐品項">
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
                                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [訂餐品項],[品項單價],sum([品項數量]) as 總數量,[品項單價]*sum([品項數量]) as 總價 FROM [lunch_order_table] WHERE ([訂餐日期] = @訂餐日期) group by [訂餐品項],[品項單價]">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="訂餐日期" QueryStringField="order_date" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" runat="server" visible="false">
                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3">
                                <Columns>
                                    <asp:BoundField DataField="訂餐者同編" HeaderText="訂餐者同編" SortExpression="訂餐者同編" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="  select [訂餐者同編] from lunch_order_table where ([訂餐日期] = @訂餐日期) group by [訂餐者同編]">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="訂餐日期" QueryStringField="order_date" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                        <div class="col-md-12" runat="server" visible="false">
                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5">
                                <Columns>
                                    <asp:BoundField DataField="訂餐人姓名" HeaderText="訂餐人姓名" SortExpression="訂餐人姓名" />
                                    <asp:BoundField DataField="訂餐者部門" HeaderText="訂餐者部門" SortExpression="訂餐者部門" />
                                    <asp:BoundField DataField="訂餐者同編" HeaderText="訂餐者同編" SortExpression="訂餐者同編" />
                                    <asp:BoundField DataField="訂餐品項" HeaderText="訂餐品項" SortExpression="訂餐品項" />
                                    <asp:BoundField DataField="品項單價" HeaderText="品項單價" SortExpression="品項單價" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [訂餐人姓名],[訂餐者同編],[訂餐者部門],[訂餐品項],[品項單價] FROM [dbo].[lunch_order_table] where ([訂餐日期] = @訂餐日期) order by  [訂餐品項]">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="訂餐日期" QueryStringField="order_date" />
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
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 37px;">店家名稱</td>
                                <td class="auto-style1">
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
                                <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家備註</td>
                                <td>
                                    <asp:TextBox class="txb_store_note" ID="txb_group_store_note" runat="server" Width="350px" TextMode="MultiLine" Height="90px" Enabled="false" BackColor="Gainsboro"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 70px; text-align: center">好吃</td>
                                <td style="width: 150px">
                                    <asp:Label ID="lab_good" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 70px; text-align: center">普通</td>
                                <td style="width: 150px">
                                    <asp:Label ID="lab_ordinary" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="info" style="width: 70px; text-align: center">難吃</td>
                                <td style="width: 150px">
                                    <asp:Label ID="lab_bad" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-6" id="div_meun" runat="server" visible="false">
                        <div class="col-md-5">
                            <table class="table table-bordered">
                                <tr>
                                    <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 37px;">同工編號</td>
                                    <td class="auto-style1">
                                        <asp:TextBox ID="txb_emp_id" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_order" class="btn btn-primary" runat="server" Text="訂餐" OnClick="btn_order_Click" OnClientClick="if(confirm('是否確認要下單?')==false){return false} else{}" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="GV_lunch_meun" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GV_lunch_meun_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="品名" HeaderText="品名" SortExpression="品名">
                                        <ItemStyle Width="300px" HorizontalAlign="Center" />
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
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [PK],[店ID],[店名],[品名],[價格] FROM [Lunch_Store_meun] where 店ID=@店ID order by 品名">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="店ID" QueryStringField="store_id" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
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
            $.blockUI({ message: '<h4>成團結單處理中，請稍後</h4>' });
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
