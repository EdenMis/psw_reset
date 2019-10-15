<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Group_Buy.Master" AutoEventWireup="true" CodeBehind="My_lunch_order.aspx.cs" Inherits="psw_reset.GroupBuy.My_lunch_order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="col-md-9">
        <div class="panel panel-primary">
            <div class="panel-heading">
                我的訂餐
            </div>
            <div class="panel-body">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PK" DataSourceID="SqlDataSource2" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView2_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="if(confirm('是否確認取消此筆訂餐')==false){return false}" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="支付狀態" HeaderText="支付狀態" SortExpression="支付狀態" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="訂餐日期" HeaderText="訂餐日期" SortExpression="訂餐日期" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="午餐店家" HeaderText="午餐店家" SortExpression="午餐店家" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="訂餐品項" HeaderText="訂餐品項" SortExpression="訂餐品項">
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="品項單價" SortExpression="品項單價">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("品項單價") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="品項數量" SortExpression="品項數量">         
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("品項數量") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="總價" HeaderText="總價" SortExpression="總價" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="是否可訂" HeaderText="結單狀態" SortExpression="是否可訂" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
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
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>"
                    DeleteCommand="DELETE FROM [lunch_order_table] WHERE [PK] = @PK"   
                    SelectCommand="SELECT a.支付狀態,a.PK,a.訂餐日期,a.午餐店家,a.訂餐品項,a.品項單價,a.品項數量,a.品項單價*a.品項數量 as 總價,case when b.是否可訂=1 then '尚未結單' else '已結單' end as 是否可訂 FROM dbo.lunch_order_table as a LEFT JOIN Lunch_set_main as b on a.訂餐日期=b.日期 where a.訂餐者同編=@訂餐者同編 order by 訂餐日期 desc">
                    <DeleteParameters>
                        <asp:Parameter Name="PK" Type="Int32" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:SessionParameter Name="訂餐者同編" SessionField="User_ID" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>
        </div>
    </div>
</asp:Content>
