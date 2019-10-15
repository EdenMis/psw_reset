<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_group_store.aspx.cs" Inherits="psw_reset.GroupBuy.edit_group_store" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width initial-scale=1" />
   
    <title>修改店家資訊</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        th {
            text-align: center;
        }
             img {
                border: 1px solid black;
                margin:2px;
                width: 400px;
                height:400px;
                object-fit: cover;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    店家資訊填寫
                </div>
                <div class="panel-body">
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店名</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_store_name" runat="server" Width="500px" Enabled="true"></asp:TextBox>
                                <asp:TextBox ID="txb_old_store_name" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">啟用狀態</td>
                            <td class="auto-style2">
                                <asp:RadioButton ID="rdo_en" runat="server" Text="啟用" GroupName="Enabled" />
                                &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rdo_dis_en" runat="server" Text="停用" GroupName="Enabled" />
                                <%-- <asp:CheckBox ID="chk_all_follow" runat="server" /><span style="color: red">若勾選，則全會可跟團</span></td>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">使用權限</td>
                            <td class="auto-style2">
                                <asp:RadioButton ID="rdo_dep_emp" runat="server" Text="僅可使用部門與同工" GroupName="follow" />
                                &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rdo_all" runat="server" Text="全會同工" GroupName="follow" />
                                <%-- <asp:CheckBox ID="chk_all_follow" runat="server" /><span style="color: red">若勾選，則全會可跟團</span></td>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">可使用部門編號</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_dep_Notice" runat="server" Width="500px"></asp:TextBox><br />
                                <span style="color: red">多部門，請使用空白分隔</span></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">可使用同工編號</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_emp_Notice" runat="server" Width="500px"></asp:TextBox><br />
                                <span style="color: red">多位，請使用空白分隔</span></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家類型</td>
                            <td>
                                <asp:DropDownList ID="Dlist_store_type" runat="server">
                                    <asp:ListItem Selected="True">請選擇</asp:ListItem>
                                    <asp:ListItem>點心</asp:ListItem>
                                    <asp:ListItem>飲料</asp:ListItem>
                                    <asp:ListItem>主餐</asp:ListItem>
                                    <asp:ListItem>其他</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">店家地址</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_store_addr" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px; height: 42px;">店家電話</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_store_tel" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家介紹網址</td>
                            <td>
                                <asp:TextBox ID="txb_group_store_web_addr" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">店家備註</td>
                            <td>
                                <asp:TextBox class="TextStyle" ID="txb_group_store_note" runat="server" Width="500px" TextMode="MultiLine" Height="90px"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table>
                           <tr>
                            <td><asp:Button ID="btn_update_group_store" class="btn btn-primary" runat="server" Text="修改" OnClientClick="if(confirm('是否確認要修改店家資訊?')==false){return false} else{}" OnClick="btn_update_group_store_Click" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    店家菜單
                </div>
                <div class="panel-body">
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 170px; text-align: center; font-size: 14px">更換菜單圖片<br/><span style="color: red">支援多檔上傳，圖片格式為jpg,png</span><br />
                    </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" /></td>
                            <td>
                                <asp:Button ID="Button1" class="btn btn-info" runat="server" Text="更換" OnClick="Button1_Click" /></td>
                        </tr>
                    </table>
                               <asp:Panel ID="img_panel" runat="server">

                        </asp:Panel>

<%--                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PK" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="品名" HeaderText="品名" SortExpression="品名">
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="價格" SortExpression="價格">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("價格") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("價格") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
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
                        DeleteCommand="DELETE FROM [follow_meun] WHERE [PK] = @PK"
                        InsertCommand="INSERT INTO [follow_meun] ([店名], [品名], [價格]) VALUES (@店名, @品名, @價格)"
                        SelectCommand="SELECT [PK], [店名], [品名], [價格] FROM [follow_meun] WHERE ([店名] = @店名) order by 品名"
                        UpdateCommand="UPDATE [follow_meun] SET [品名] = @品名, [價格] = @價格 WHERE [PK] = @PK">
                        <DeleteParameters>
                            <asp:Parameter Name="PK" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="店名" Type="String" />
                            <asp:Parameter Name="品名" Type="String" />
                            <asp:Parameter Name="價格" Type="Int32" />
                        </InsertParameters>
                             <SelectParameters>
                            <asp:ControlParameter ControlID="txb_group_store_name" Name="店名" PropertyName="Text" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="品名" Type="String" />
                            <asp:Parameter Name="價格" Type="Int32" />
                            <asp:Parameter Name="PK" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>      --%>
                </div>
            </div>
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
                width: "90%"
            });
        });
    </script>
</body>
</html>
