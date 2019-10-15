<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Burger_meun.Master" AutoEventWireup="true" CodeBehind="User_Pirvilege_Inquire.aspx.cs" Inherits="psw_reset.Privilege_control.User_Pirvilege_Inquire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12">
        <h1>使用者權限查詢</h1>
        <hr>
    </div>
    <div class="col-md-12">
        <table>
            <tr>
                <td>
                    <h5>輸入同工編號：</h5>
                </td>
                <td>
                    <asp:TextBox ID="txb_user_id" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="取得使用者身分與功能權限" OnClick="Button1_Click" /></td>
            </tr>
        </table>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            詳細資料
        </div>
        <div class="panel-body">
            <div class="col-md-3">
                <table class="table table-bordered">
                    <tr>
                        <td class="info" style="width: 170px; text-align: center; font-size: 14px">同工部門</td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 170px; text-align: center; font-size: 14px">同工編號</td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="info" style="width: 170px; text-align: center; font-size: 14px">同工名稱</td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                </table>
            </div>
            <div class="col-md-9">
                <asp:GridView ID="GV_level_1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="first_level_function_name" HeaderText="第一層選單名稱" SortExpression="first_level_function_name" />
                        <asp:CheckBoxField DataField="Visible_state" HeaderText="是否可見" SortExpression="Visible_state" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Privilege_controlConnectionString %>" SelectCommand="SELECT id,group_name,[first_level_function_name], [Visible_state] FROM [First_level_function] where group_name=''"></asp:SqlDataSource>
                <br />
                <asp:GridView ID="GV_level_2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="first_level_function_name" HeaderText="對應選單" SortExpression="first_level_function_name" />
                        <asp:BoundField DataField="Second_level_function_name" HeaderText="功能名稱" SortExpression="Second_level_function_name" />
                        <asp:CheckBoxField DataField="fn_select" HeaderText="查詢權限" SortExpression="fn_select" />
                        <asp:CheckBoxField DataField="fn_delete" HeaderText="刪除權限" SortExpression="fn_delete" />
                        <asp:CheckBoxField DataField="fn_insert" HeaderText="新增權限" SortExpression="fn_insert" />
                        <asp:CheckBoxField DataField="fn_update" HeaderText="修改權限" SortExpression="fn_update" />
                        <asp:CheckBoxField DataField="fn_1" HeaderText="特殊權限1" SortExpression="fn_1" />
                        <asp:CheckBoxField DataField="fn_2" HeaderText="特殊權限2" SortExpression="fn_2" />
                        <asp:CheckBoxField DataField="fn_3" HeaderText="特殊權限3" SortExpression="fn_3" />
                        <asp:CheckBoxField DataField="fn_4" HeaderText="特殊權限4" SortExpression="fn_4" />
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
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Privilege_controlConnectionString %>" SelectCommand="SELECT id,[first_level_function_name], [Second_level_function_name], [fn_select], [fn_delete], [fn_insert], [fn_update], [fn_1], [fn_2], [fn_3], [fn_4] FROM [Second_level_function] where group_name=''"></asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
