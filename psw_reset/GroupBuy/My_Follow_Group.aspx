<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Group_Buy.Master" AutoEventWireup="true" CodeBehind="My_Follow_Group.aspx.cs" Inherits="psw_reset.GroupBuy.My_Follow_Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-9">
        <div class="panel panel-primary">
            <div class="panel-heading">
                我的跟團
            </div>
            <div class="panel-body">
                <%--     <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            開放式菜單
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" PageSize="20" OnRowUpdating="GridView1_RowUpdating">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" />
                                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btn_edit" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" />
                                            &nbsp;<asp:Button ID="btn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="if(confirm('是否確認取消此筆跟團')==false){return false}" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="支付狀態" HeaderText="支付狀態" SortExpression="支付狀態" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="團號" HeaderText="團號" SortExpression="團號" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="團名" HeaderText="團名" SortExpression="團名" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="跟團品項" HeaderText="跟團品項" SortExpression="跟團品項">
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="金額" SortExpression="金額">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("金額") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("金額") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="狀態" HeaderText="狀態" SortExpression="狀態" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
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
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupBuyConnectionString %>"
                                SelectCommand="SELECT a.支付狀態,a.[PK],a.[團號],a.[團名],a.[跟團人姓名],a.[跟團者同編],a.[跟團品項],a.[金額],b.狀態 FROM [GroupBuy].[dbo].[Follow_Group_table] as a left join Group_main_table as b on a.團號=b.PK WHERE ([跟團者同編] = @跟團者同編) order by 團號 desc"
                                DeleteCommand="DELETE FROM [Follow_Group_table] WHERE [PK] = @PK"
                                InsertCommand="INSERT INTO [Follow_Group_table] ([團號], [團名], [跟團人姓名], [跟團者同編], [跟團品項], [金額]) VALUES (@團號, @團名, @跟團人姓名, @跟團者同編, @跟團品項, @金額)"
                                UpdateCommand="UPDATE [Follow_Group_table] SET [跟團品項] = @跟團品項, [金額] = @金額 WHERE [PK] = @PK">
                                <DeleteParameters>
                                    <asp:Parameter Name="PK" Type="Int32" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="團號" Type="String" />
                                    <asp:Parameter Name="團名" Type="String" />
                                    <asp:Parameter Name="跟團人姓名" Type="String" />
                                    <asp:Parameter Name="跟團者同編" Type="String" />
                                    <asp:Parameter Name="跟團品項" Type="String" />
                                    <asp:Parameter Name="金額" Type="String" />
                                </InsertParameters>
                                <SelectParameters>
                                    <asp:SessionParameter Name="跟團者同編" SessionField="User_ID" Type="String" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="跟團品項" Type="String" />
                                    <asp:Parameter Name="金額" Type="String" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>--%>

                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PK" DataSourceID="SqlDataSource2" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView2_RowDataBound" OnRowUpdating="GridView2_RowUpdating">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" />
                                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btn_edit" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" />
                                &nbsp;<asp:Button ID="btn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="if(confirm('是否確認取消此筆跟團')==false){return false}" />
                            </ItemTemplate>
                        </asp:TemplateField>
     <%--                   <asp:BoundField DataField="支付狀態" HeaderText="支付狀態" SortExpression="支付狀態" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="團號" HeaderText="團號" SortExpression="團號" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="團名" HeaderText="團名" SortExpression="團名" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="跟團品項" HeaderText="跟團品項" SortExpression="跟團品項">
                            <ItemStyle HorizontalAlign="Center" Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="備註" HeaderText="備註" SortExpression="備註">
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="品項單價" SortExpression="品項單價">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("品項單價") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("品項單價") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="品項數量" SortExpression="品項數量">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("品項數量") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("品項數量") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="總額" HeaderText="總額" SortExpression="總額" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="狀態" HeaderText="狀態" SortExpression="狀態" ReadOnly="True">
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
                    DeleteCommand="DELETE FROM [fixed_follow_Group_table] WHERE [PK] = @PK"
                    InsertCommand="INSERT INTO [fixed_follow_Group_table] ([團號], [團名], [跟團人姓名], [跟團者同編], [跟團品項], [品項單價], [品項數量], [總額], [支付狀態]) VALUES (@團號, @團名, @跟團人姓名, @跟團者同編, @跟團品項, @品項單價, @品項數量, @總額, @支付狀態)"
                    SelectCommand="SELECT a.PK,a.支付狀態,a.團號,a.團名,a.跟團品項,a.品項單價,a.品項數量,a.品項單價*a.品項數量 as 總額,b.狀態,a.備註 FROM fixed_follow_Group_table as a left join Group_main_table as b on a.團號=b.PK WHERE (a.跟團者同編 = @跟團者同編) order by a.PK desc"
                    UpdateCommand="UPDATE [fixed_follow_Group_table] SET 跟團品項=@跟團品項,品項單價=@品項單價,[品項數量] = @品項數量,[備註]=@備註 WHERE [PK] = @PK">
                    <DeleteParameters>
                        <asp:Parameter Name="PK" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="團號" Type="String" />
                        <asp:Parameter Name="團名" Type="String" />
                        <asp:Parameter Name="跟團人姓名" Type="String" />
                        <asp:Parameter Name="跟團者同編" Type="String" />
                        <asp:Parameter Name="跟團品項" Type="String" />
                        <asp:Parameter Name="品項單價" Type="Int32" />
                        <asp:Parameter Name="品項數量" Type="Int32" />
                        <asp:Parameter Name="總額" Type="Int32" />
                        <asp:Parameter Name="支付狀態" Type="String" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:SessionParameter Name="跟團者同編" SessionField="User_ID" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="品項數量" Type="Int32" />
                        <asp:Parameter Name="品項單價" Type="Int32" />
                          <asp:Parameter Name="跟團品項" Type="String" />
                        <asp:Parameter Name="備註" Type="String" />
                        <asp:Parameter Name="PK" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>

            </div>
        </div>
    </div>
</asp:Content>
