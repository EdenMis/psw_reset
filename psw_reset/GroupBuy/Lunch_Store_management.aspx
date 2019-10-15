<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Group_Buy.Master" AutoEventWireup="true" CodeBehind="Lunch_Store_management.aspx.cs" Inherits="psw_reset.GroupBuy.Lunch_Store_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-8">
        <div class="panel panel-warning">
            <div class="panel-heading">
                午餐店家
            </div>

            <div class="panel-body">
                <div class="col-md-6">
                    <asp:Button ID="btn_add_store" class="btm btn-success" runat="server" Text="新增店家" OnClick="btn_add_store_Click" Height="35px" Width="100px" />
                    <br />
                    <br />
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 70px; text-align: center">店名</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="txb_store_name" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 70px; text-align: center">地址</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="txb_store_addr" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 70px; text-align: center">聯絡電話</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="txb_store_tel" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 70px; text-align: center">店家備註</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="txb_note" runat="server" Width="250px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btn_set_lunch" class="btm btn-success" runat="server" Text="設定今日午餐" Height="35px" Width="100px" OnClick="btn_set_lunch_Click" OnClientClick="javascript:return set_lunch()" />
                    <br />
                    <br />
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 70px; text-align: center">今日午餐</td>
                            <td style="width: 150px">
                                <asp:Label ID="lab_today_lunch" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 70px; text-align: center">今日午餐相關備註</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="txb_lunch_note" runat="server" Width="250px" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-12">
                    <asp:GridView ID="GV_store_main" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PK" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GV_store_main_RowDataBound" OnSelectedIndexChanged="GV_store_main_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="店名" HeaderText="店名" SortExpression="店名" ReadOnly="true">
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="地址" HeaderText="地址" SortExpression="地址">
                                <ItemStyle HorizontalAlign="Center" Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="電話" HeaderText="電話" SortExpression="電話">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="備註" HeaderText="備註" SortExpression="備註">
                                <ItemStyle HorizontalAlign="Center" Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="good" HeaderText="好吃" SortExpression="good" ReadOnly="true">
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ordinary" HeaderText="普通" SortExpression="ordinary" ReadOnly="true">
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bad" HeaderText="難吃" SortExpression="bad" ReadOnly="true">
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <EditRowStyle BackColor="#ffff99" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#ffff99" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" 
                        SelectCommand="SELECT [PK], [店名], [地址], [電話], [備註], [good], [ordinary], [bad] FROM [Lunch_Store_main]" 
                        DeleteCommand="DELETE FROM [Lunch_Store_main] WHERE [PK] = @PK" 
                        InsertCommand="INSERT INTO [Lunch_Store_main] ([店名], [地址], [電話], [備註], [good], [ordinary], [bad]) VALUES (@店名, @地址, @電話, @備註, @good, @ordinary, @bad)" 
                        UpdateCommand="UPDATE [Lunch_Store_main] SET [地址] = @地址, [電話] = @電話, [備註] = @備註 WHERE [PK] = @PK">
                        <DeleteParameters>
                            <asp:Parameter Name="PK" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="地址" Type="String" />
                            <asp:Parameter Name="電話" Type="String" />
                            <asp:Parameter Name="備註" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="店名" Type="String" />
                            <asp:Parameter Name="地址" Type="String" />
                            <asp:Parameter Name="電話" Type="String" />
                            <asp:Parameter Name="備註" Type="String" />
                            <asp:Parameter Name="good" Type="Int32" />
                            <asp:Parameter Name="ordinary" Type="Int32" />
                            <asp:Parameter Name="bad" Type="Int32" />
                            <asp:Parameter Name="PK" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-warning">
            <div class="panel-heading">
                菜單
            </div>
            <div class="panel-body">
                <h3>
                    <asp:Label ID="lab_store_name" runat="server"></asp:Label></h3>
                <br />
                <table class="table table-bordered">
                    <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">菜單EXCEL上傳<br/>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="../NPOI/菜單範本.xlsx">範本下載</asp:HyperLink></td>
                          
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;</td>
                        <td>
                            <asp:Button ID="Button1" class="btn btn-info" runat="server" Text="匯入菜單" OnClick="Button1_Click" /></td>
                    </tr>
                </table>


                <table class="table table-bordered">
                    <tr>
                        <td class="info" style="width: 124px; height: 39px;">品名</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txb_meun_name" runat="server" Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 124px">價格</td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txb_meun_money" runat="server" Width="250"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_add_meun" class="btm btn-success" runat="server" Text="新增" OnClick="btn_add_meun_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:GridView ID="GV_store_meun" runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <%-- <asp:BoundField DataField="店名" HeaderText="店名" SortExpression="店名">
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="品名" HeaderText="品名" SortExpression="品名">
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="價格" HeaderText="價格" SortExpression="價格">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <EditRowStyle BackColor="#ffff99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#ffff99" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" 
                    SelectCommand="SELECT [PK], [店ID], [店名], [品名], [價格] FROM [Lunch_Store_meun] WHERE ([店ID] = @店ID)" 
                    DeleteCommand="DELETE FROM [Lunch_Store_meun] WHERE [PK] = @PK" 
                    InsertCommand="INSERT INTO [Lunch_Store_meun] ([店ID], [店名], [品名], [價格]) VALUES (@店ID, @店名, @品名, @價格)" 
                    UpdateCommand="UPDATE [Lunch_Store_meun] SET [品名] = @品名, [價格] = @價格 WHERE [PK] = @PK">
                    <DeleteParameters>
                        <asp:Parameter Name="PK" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="品名" Type="String" />
                        <asp:Parameter Name="價格" Type="Int32" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GV_store_main" Name="店ID" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="店ID" Type="String" />
                        <asp:Parameter Name="店名" Type="String" />
                        <asp:Parameter Name="品名" Type="String" />
                        <asp:Parameter Name="價格" Type="Int32" />
                        <asp:Parameter Name="PK" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>
            <div class="col-md-12" runat="server" visible="false">
                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3">
                    <Columns>
                        <asp:BoundField DataField="訂餐者同編" HeaderText="訂餐者同編" SortExpression="訂餐者同編" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="select [訂餐者同編] from lunch_order_table">
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function set_lunch() {
            var lab_today = document.getElementById("ContentPlaceHolder1_lab_today_lunch").value;
            if (lab_today == "尚未設定") {
                var msg = "是否確認要設定今日午餐?";
            }
            else {
                var msg = "是否確認要設定今日午餐?(若是重新設定，將導致已經下訂的人，需重新下訂)";
            }

            if (confirm(msg) == true) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
