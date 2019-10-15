<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Group_Buy.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="psw_reset.GroupBuy.index1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-6" id="lunch_div" runat="server" visible="false">
        <div class="panel panel-primary">
            <div class="panel-heading">
                今日午餐
            </div>
            <div class="panel-body">
                <table class="table table-bordered">
                    <tr>
                        <td class="info" style="width: 70px; text-align: center">店名</td>
                        <td style="width: 150px">
                            <asp:Label ID="lab_store_name" runat="server" Text="本日午餐尚未設定"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 70px; text-align: center">好吃</td>
                        <td style="width: 150px">
                            <asp:Label ID="lab_good" runat="server" Text="本日午餐尚未設定"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 70px; text-align: center">普通</td>
                        <td style="width: 150px">
                            <asp:Label ID="lab_ordinary" runat="server" Text="本日午餐尚未設定"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 70px; text-align: center">難吃</td>
                        <td style="width: 150px">
                            <asp:Label ID="lab_bad" runat="server" Text="本日午餐尚未設定"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 70px; text-align: center">評價</td>
                        <td style="width: 150px">
                            <asp:HyperLink ID="hyp_evaluation" runat="server" Text="本日午餐尚未設定"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 70px; text-align: center">今日午餐備註</td>
                        <td style="width: 150px">
                            <asp:Label ID="lab_lunch_note" runat="server" Text="本日午餐尚未設定"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinkButton ID="LinkButton2" class="btn btn-success" runat="server" href="Lunch_voting.aspx" Target="_blank" Text="我要投票" Visible="false"></asp:LinkButton><br />
                <asp:Button ID="btn_order" class="btn btn-primary" runat="server" Text="訂餐" OnClick="btn_order_Click" OnClientClick="if(confirm('是否確認要下單?')==false){return false} else{}" /><br />
                <br />
                <asp:GridView ID="GV_lunch_meun" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource3" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GV_lunch_meun_RowDataBound">
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
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [PK],[店ID],[店名],[品名],[價格] FROM [Lunch_Store_meun] where 店ID='' order by 品名"></asp:SqlDataSource>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="panel panel-success">
            <div class="panel-heading">
                目前開團資訊<span style="color: red">(點團名可進團)</span>
            </div>
            <div class="panel-body">
                <asp:LinkButton ID="LinkButton1" class="btn btn-success" runat="server" href="Open_buy.aspx" Target="_blank" Text="我要開團" Visible="false"></asp:LinkButton><br />
                <br />
                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" CssClass="auto-style1" DataKeyNames="PK">
                    <Columns>
                        <%--                        <asp:HyperLinkField DataNavigateUrlFields="PK" DataNavigateUrlFormatString="open_detail.aspx?PK={0}" DataTextField="PK" HeaderText="開團編號" Target="_blank">
                            <ItemStyle HorizontalAlign="Center" Width="80px"/>
                        </asp:HyperLinkField>--%>

                        <asp:BoundField DataField="開團人部門" HeaderText="開團人部門" SortExpression="開團人部門">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="開團人" HeaderText="開團人" SortExpression="開團人">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="PK" DataNavigateUrlFormatString="open_detail.aspx?PK={0}" DataTextField="開團標題" HeaderText="開團標題" Target="_blank">
                            <ItemStyle HorizontalAlign="Center" Width="300px" />
                        </asp:HyperLinkField>
                        <%--                <asp:BoundField DataField="開團標題" HeaderText="開團標題" SortExpression="開團標題">
                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                        </asp:BoundField>--%>
                        <%--                        <asp:CheckBoxField DataField="結單" HeaderText="結單" SortExpression="結單">
                            <ItemStyle HorizontalAlign="Center" Width="100px"/>
                        </asp:CheckBoxField>--%>
                        <asp:BoundField DataField="狀態" HeaderText="狀態" SortExpression="狀態">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="開團時間" HeaderText="開團時間" SortExpression="開團時間" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <ItemStyle HorizontalAlign="Center" Width="175px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="結單時間" HeaderText="結單時間" SortExpression="結單時間" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <ItemStyle HorizontalAlign="Center" Width="175px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>" SelectCommand="SELECT [PK], [開團人部門], [開團人], [開團標題],[通知部門],[通知同工][菜單檔名], [狀態],[開團時間],結單時間,[全開放] FROM [Group_main_table] where 狀態 ='開團中'  and (通知部門 like '%'+@通知部門+'%' or 通知同工 like '%'+@通知同工+'%' or 全開放='true') and 結單時間 > GETDATE()  order by PK desc">
                    <SelectParameters>
                        <asp:SessionParameter Name="通知部門" SessionField="User_Dep_ID" Type="String" />
                        <asp:SessionParameter Name="通知同工" SessionField="User_ID" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
    <script src="../jquery/jquery-1.9.1.min.js"></script>
    <script src="../jquery/jquery.blockUI.js"></script>
    <link href="../colorbox/colorbox.css" rel="stylesheet" />
    <script src="../colorbox/jquery.colorbox.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#ContentPlaceHolder1_hyp_evaluation").colorbox({
                width: "90%",
                height: "90%",
                iframe: true
            });
        });

        //colorbox隨流覽器調整寬度
        window.addEventListener("resize", function lightboxresize() {
            //var pagename = location.pathname.replace('/Web/', '');
            //if (pagename == "CreateCase.aspx"|| pagename == "SearchCase.aspx") {
            $.colorbox.resize({ width: window.innerWidth - 100, height: window.innerHeight - 50 });
            //}
        });
    </script>
</asp:Content>
