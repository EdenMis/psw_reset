<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Burger_meun.Master" AutoEventWireup="true" CodeBehind="Group_Privilege_Control.aspx.cs" Inherits="psw_reset.Privilege_control.Group_Privilege_Control" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12" style="left: 0px; top: 0px">
        <h1>群組權限設定</h1>
        <hr>
    </div>
    <div class="col-md-12">
        <table>
            <tr>
                <td>
                    <h5>群組名稱：</h5>
                </td>
                <td>
                    <asp:TextBox ID="txb_group_name" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_add_group" runat="server" Text="新增群組" OnClick="btn_add_group_Click" /></td>
            </tr>
        </table>
    </div>
    <div class="col-md-12">
        <table>
            <tr>
                <td>
                    <h5>選擇要調整權限之群組：</h5>
                </td>
                <td>
                    <asp:DropDownList ID="Dlist_group" runat="server" DataSourceID="SqlDataSource1" DataTextField="group_name" DataValueField="group_name" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="Dlist_group_SelectedIndexChanged" Style="height: 22px">
                        <asp:ListItem>  </asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btn_delete_group" runat="server" Text="刪除選擇的群組" OnClick="btn_delete_group_Click" />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Privilege_controlConnectionString %>" SelectCommand="SELECT [group_name] FROM [group_list]"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <div class="col-md-12">
        <div class="panel panel-success">
            <div class="panel-heading">
                權限資料
            </div>
            <div class="panel-body">
                <asp:GridView ID="GV_first_fn" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowEditing="GV_first_fn_RowEditing" OnRowUpdated="GV_first_fn_RowUpdated" OnRowCancelingEdit="GV_first_fn_RowCancelingEdit">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                        <asp:BoundField DataField="First_level_function_name" HeaderText="功能選單名稱" SortExpression="First_level_function_id" ReadOnly="true" />
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
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Privilege_controlConnectionString %>"
                    SelectCommand="SELECT a.[id]
      ,a.[First_level_function_id]
	  ,b.first_level_function_name
      ,[group_name]
      ,[Visible_state]
  FROM [Privilege_control].[dbo].[First_level_function_detail] as a left join
  First_level_function as b on a.First_level_function_id=b.First_level_function_id where group_name=''"
                    UpdateCommand="UPDATE [First_level_function_detail] SET [Visible_state] = @Visible_state WHERE [id] = @id">
                    <UpdateParameters>
                        <asp:Parameter Name="Visible_state" Type="Boolean" />
                        <asp:Parameter Name="id" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <br />
                <br />
                <asp:GridView ID="GV_Second_fn" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="id" DataSourceID="SqlDataSource3" ForeColor="Black" GridLines="Vertical" OnRowCancelingEdit="GV_Second_fn_RowCancelingEdit" OnRowEditing="GV_Second_fn_RowEditing" OnRowUpdated="GV_Second_fn_RowUpdated">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                        <asp:BoundField DataField="Second_level_function_name" HeaderText="功能名稱" ReadOnly="True" SortExpression="Second_level_function_name" />
                        <asp:CheckBoxField DataField="Visible_state" HeaderText="是否可見" SortExpression="Visible_state" />
                        <asp:CheckBoxField DataField="fn_select" HeaderText="搜尋權限" SortExpression="fn_select" />
                        <asp:CheckBoxField DataField="fn_update" HeaderText="修改權限" SortExpression="fn_update" />
                        <asp:CheckBoxField DataField="fn_insert" HeaderText="寫入權限" SortExpression="fn_insert" />
                        <asp:CheckBoxField DataField="fn_delete" HeaderText="刪除權限" SortExpression="fn_delete" />
                        <asp:CheckBoxField DataField="fn_1" HeaderText="fn_1" SortExpression="fn_1" />
                        <asp:CheckBoxField DataField="fn_2" HeaderText="fn_2" SortExpression="fn_2" />
                        <asp:CheckBoxField DataField="fn_3" HeaderText="fn_3" SortExpression="fn_3" />
                        <asp:CheckBoxField DataField="fn_4" HeaderText="fn_4" SortExpression="fn_4" />
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
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Privilege_controlConnectionString %>"
                    SelectCommand="  SELECT 
  a.id,
  a.Second_level_function_id,
  b.Second_level_function_name,
  a.group_name,
  a.Visible_state,
  a.fn_select,
  a.fn_update,
  a.fn_insert,
  a.fn_delete,
  a.fn_1,
  a.fn_2,
  a.fn_3,
  a.fn_4
  FROM Second_level_function_detail AS a left join 
  Second_level_function as b on a.Second_level_function_id=b.Second_level_function_id WHERE a.group_name=''"
                    UpdateCommand="UPDATE [Second_level_function_detail] SET [Visible_state] = @Visible_state, [fn_select] = @fn_select, [fn_delete] = @fn_delete, [fn_insert] = @fn_insert, [fn_update] = @fn_update, [fn_1] = @fn_1, [fn_2] = @fn_2, [fn_3] = @fn_3, [fn_4] = @fn_4 WHERE [id] = @id">
                    <UpdateParameters>
                        <asp:Parameter Name="Visible_state" Type="Boolean" />
                        <asp:Parameter Name="fn_select" Type="Boolean" />
                        <asp:Parameter Name="fn_delete" Type="Boolean" />
                        <asp:Parameter Name="fn_insert" Type="Boolean" />
                        <asp:Parameter Name="fn_update" Type="Boolean" />
                        <asp:Parameter Name="fn_1" Type="Boolean" />
                        <asp:Parameter Name="fn_2" Type="Boolean" />
                        <asp:Parameter Name="fn_3" Type="Boolean" />
                        <asp:Parameter Name="fn_4" Type="Boolean" />
                        <asp:Parameter Name="id" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="panel panel-success">
            <div class="panel-heading">
                所屬成員
            </div>
            <div class="panel-body">
                <table>
                    <tr>
                        <td>
                            <h5>新增使用者：</h5>
                        </td>
                        <td>
                            <asp:TextBox ID="txb_new_user_id" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="txb_add_user" runat="server" Text="新增使用者" /></td>
                    </tr>
                </table>
                <asp:GridView ID="GV_User" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" DataKeyNames="id" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="user_id" HeaderText="同工編號" SortExpression="user_id" />
                        <asp:BoundField DataField="user_name" HeaderText="同工姓名" SortExpression="user_name" />
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
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
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Privilege_controlConnectionString %>"
                    SelectCommand="SELECT [id], [user_id], [user_name], [user_group] FROM [group_user] where user_group=''"
                    DeleteCommand="DELETE FROM [group_user] WHERE [id] = @id"
                    InsertCommand="INSERT INTO [group_user] ([user_id], [user_name], [user_group]) VALUES (@user_id, @user_name, @user_group)"
                    UpdateCommand="UPDATE [group_user] SET [user_id] = @user_id, [user_name] = @user_name, [user_group] = @user_group WHERE [id] = @id">
                    <DeleteParameters>
                        <asp:Parameter Name="id" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="user_id" Type="String" />
                        <asp:Parameter Name="user_name" Type="String" />
                        <asp:Parameter Name="user_group" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="user_id" Type="String" />
                        <asp:Parameter Name="user_name" Type="String" />
                        <asp:Parameter Name="user_group" Type="String" />
                        <asp:Parameter Name="id" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="panel panel-success">
            <div class="panel-heading">
                所屬部門
            </div>
            <div class="panel-body">
                    <table>
                    <tr>
                        <td>
                            <h5>新增部門：</h5>
                        </td>
                        <td>
                            <asp:TextBox ID="txb_new_dep_id" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_add_dep" runat="server" Text="新增部門" /></td>
                    </tr>
                </table>
                <asp:GridView ID="GV_Dep" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" DataKeyNames="id" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="dep_id" HeaderText="部門編號" SortExpression="dep_id" />
                        <asp:BoundField DataField="dep_name" HeaderText="部門名稱" SortExpression="dep_name" />
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
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
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Privilege_controlConnectionString %>"
                    SelectCommand="SELECT [id], [dep_id], [dep_name], [dep_group] FROM [group_dep] where dep_group=''"
                    DeleteCommand="DELETE FROM [group_dep] WHERE [id] = @id"
                    InsertCommand="INSERT INTO [group_dep] ([dep_id], [dep_name], [dep_group]) VALUES (@dep_id, @dep_name, @dep_group)"
                    UpdateCommand="UPDATE [group_dep] SET [dep_id] = @dep_id, [dep_name] = @dep_name, [dep_group] = @dep_group WHERE [id] = @id">
                    <DeleteParameters>
                        <asp:Parameter Name="id" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="dep_id" Type="String" />
                        <asp:Parameter Name="dep_name" Type="String" />
                        <asp:Parameter Name="dep_group" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="dep_id" Type="String" />
                        <asp:Parameter Name="dep_name" Type="String" />
                        <asp:Parameter Name="dep_group" Type="String" />
                        <asp:Parameter Name="id" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
