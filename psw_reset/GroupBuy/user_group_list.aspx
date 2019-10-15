<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Group_Buy.Master" AutoEventWireup="true" CodeBehind="user_group_list.aspx.cs" Inherits="psw_reset.GroupBuy.user_group_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-6">
        <div class="panel panel-primary">
            <div class="panel-heading">
                我的跟團群
            </div>
            <div class="panel-body">
                <asp:LinkButton ID="LinkButton2" class="btn btn-success" runat="server" href="edit_user_group_list.aspx?PK=0" Target="_blank" Text="建立新群"></asp:LinkButton><br /><br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="PK" DataNavigateUrlFormatString="edit_user_group_list.aspx?PK={0}" DataTextField="user_group_name" HeaderText="群名" Target="_blank">
                            <ItemStyle HorizontalAlign="Center" Width="250px"/>
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="dep" HeaderText="通知部門" SortExpression="dep">
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="emp" HeaderText="通知同工" SortExpression="emp">
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                             <asp:Button ID="btn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="if(confirm('是否確認刪除此筆資料')==false){return false}" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuy %>" 
                    SelectCommand="SELECT [user_group_name], [PK], [dep], [emp] FROM [user_group_list] WHERE ([owner] = @owner)"
                     DeleteCommand="DELETE FROM [user_group_list] WHERE [PK] = @PK">
                    <SelectParameters>
                        <asp:SessionParameter Name="owner" SessionField="User_ID" Type="String" />
                    </SelectParameters>
                       <DeleteParameters>
                        <asp:Parameter Name="PK" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>

