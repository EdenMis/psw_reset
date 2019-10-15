<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Group_Buy.Master" AutoEventWireup="true" CodeBehind="Group_store_management.aspx.cs" Inherits="psw_reset.GroupBuy.Group_store_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:BulletedList ID="BulletedList2" CssClass="nav nav-tabs" runat="server" DisplayMode="LinkButton" OnClick="BulletedList1_Click">
        <asp:ListItem href="#tabe1" class="active">啟用店家</asp:ListItem>
        <asp:ListItem href="#tabe2">停用店家</asp:ListItem>
    </asp:BulletedList>
    <div class="tab-content">
        <%--啟用店家--%>
        <asp:Panel ID="tabe1" runat="server" CssClass="tab-pane fade active in">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="PK" DataNavigateUrlFormatString="edit_group_store.aspx?PK={0}" DataTextField="店名" HeaderText="店名" Target="_blank">
                        <ItemStyle HorizontalAlign="Center" Width="300px" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="類型" HeaderText="類型" SortExpression="類型">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="連絡電話" HeaderText="連絡電話" SortExpression="連絡電話">
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="店家地址" HeaderText="店家地址" SortExpression="店家地址">
                        <ItemStyle HorizontalAlign="Center" Width="250px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="店家備註" HeaderText="店家備註" SortExpression="店家備註">
                        <ItemStyle HorizontalAlign="Center" Width="250px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="最後修改人" HeaderText="最後修改人" SortExpression="最後修改人">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="最後更新時間" HeaderText="最後更新時間" SortExpression="最後更新時間">
                        <ItemStyle HorizontalAlign="Center" Width="250px" />
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" 
                SelectCommand="SELECT [PK], [店名], [類型], [連絡電話], [店家地址], [店家備註], [最後更新時間], [最後修改人], [啟用狀態] FROM [Group_store_table] WHERE ([擁有者] = @擁有者 or 全會可用='1') and 啟用狀態='1'">
                <SelectParameters>
                    <asp:SessionParameter Name="擁有者" SessionField="User_ID" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
        </asp:Panel>

        <%--停用店家--%>
        <asp:Panel ID="tabe2" runat="server" CssClass="tab-pane fade">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                   <asp:HyperLinkField DataNavigateUrlFields="PK" DataNavigateUrlFormatString="edit_group_store.aspx?PK={0}" DataTextField="店名" HeaderText="店名" Target="_blank">
                        <ItemStyle HorizontalAlign="Center" Width="300px" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="類型" HeaderText="類型" SortExpression="類型">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="連絡電話" HeaderText="連絡電話" SortExpression="連絡電話">
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="店家地址" HeaderText="店家地址" SortExpression="店家地址">
                        <ItemStyle HorizontalAlign="Center" Width="250px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="店家備註" HeaderText="店家備註" SortExpression="店家備註">
                        <ItemStyle HorizontalAlign="Center" Width="250px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="最後修改人" HeaderText="最後修改人" SortExpression="最後修改人">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="最後更新時間" HeaderText="最後更新時間" SortExpression="最後更新時間">
                        <ItemStyle HorizontalAlign="Center" Width="250px" />
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
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" 
                SelectCommand="SELECT [PK], [店名], [類型], [連絡電話], [店家地址], [店家備註], [最後更新時間], [最後修改人], [啟用狀態] FROM [Group_store_table] WHERE ([擁有者] = @擁有者 or 全會可用='1') and 啟用狀態='0'">
                  <SelectParameters>
                    <asp:SessionParameter Name="擁有者" SessionField="User_ID" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:Panel>
    </div>

    <script src="../jquery/jquery-1.9.1.min.js"></script>
    <script src="../jquery/jquery.blockUI.js"></script>
    <script type="text/javascript">

        function showBlockUI1() {
            $.blockUI({ message: '<h4>訂單匯入及設備資料產生中</h4>' });
        }

        function showBlockUI2() {
            $.blockUI({ message: '<h4>設備資料產生中</h4>' });
        }
    </script>
</asp:Content>
