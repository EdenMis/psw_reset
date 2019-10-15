<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Internet_Bank_Application.aspx.cs" Inherits="psw_reset.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>伊甸基金會網路銀行匯款申請書</title>
    <link href="../bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../dateCss/datepicker.css" rel="stylesheet" />
    <link href="../dateCss/alertify.core.css" rel="stylesheet" />
    <link href="../dateCss/alertify.default.css" rel="stylesheet" id="toggleCSS" />
    <link href="../dateCss/magic-check.css" rel="stylesheet" type="text/css"/>
    
    <style>
        th {
            text-align: center;
        }

        .TableStyle {
            font-size: 12px;
            border-collapse: separate;
        }

            .TableStyle td {
                border: solid 1px white;
                padding: 2px;
            }

        .TableStyle2 {
            font-size: 12px;
            border-collapse: collapse;
        }

            .TableStyle2 td {
                border: solid 1px black;
                padding: 1px;
            }

        .TableTitle {
            font-family: Microsoft JhengHei;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            background-color: #bcd2ff;
        }
        /*.TextStyle {
            height:auto;
            font-size:10px;
        }*/

        .div1 {
            width: 700px;
            margin-left: auto;
            margin-right: auto;
            text-align: center
        }

        .auto-style4 {
            height: 39px;
        }
    </style>
</head>
<body style="background-color: #E6DFD5">
    <form id="form1" runat="server">
        <div class="div1">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/menu/EDEN.png" /><br/>
         <%--    <h2>
                <asp:Label ID="Label2" runat="server" Text="網路銀行匯款申請書" Font-Names="微軟正黑體"></asp:Label></h2>--%>
        </div>
        <br />
        <div class="div1">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    網路銀行匯款申請書
                </div>
                <div class="panel-body">
                    <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">類型&ensp;<span style="color: red; font-size: 18px">*</span></td>
                            <td style="text-align: left">
                                <asp:Label ID="lab_type" runat="server" Text=""></asp:Label>
                               <%-- <asp:RadioButton ID="rdo_type_company"  runat="server" Text="公司戶" GroupName="type" Checked="True" AutoPostBack="true" OnCheckedChanged="rdo_type_company_CheckedChanged" />&emsp;<asp:RadioButton ID="rdo_type_person" runat="server" Text="個人戶" GroupName="type" AutoPostBack="true" OnCheckedChanged="rdo_type_person_CheckedChanged" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">
                                <asp:Label ID="lab_id_title" runat="server" Text="統一編號/身分證字號"></asp:Label>&ensp;<span style="color: red; font-size: 18px">*</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txb_id" runat="server" MaxLength="10" Width="300px" AutoPostBack="True" OnTextChanged="txb_id_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">
                                <asp:Label ID="lab_name_title" runat="server" Text="公司全銜/個人戶"></asp:Label>&ensp;<span style="color: red; font-size: 18px">*</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txb_name" runat="server" MaxLength="80" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">聯絡人&ensp;<span style="color: red; font-size: 18px">*</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txb_Contact_person" runat="server" MaxLength="30" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">電話&ensp;<span style="color: red; font-size: 18px">*</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txb_tel" runat="server" MaxLength="30" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">傳真<br />
                                <span style="color: red; font-size: 14px">與Mail擇一必填</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txb_fax" runat="server" MaxLength="30" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">E-Mail<br />
                                <span style="color: red; font-size: 14px">與傳真擇一必填</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txb_mail" runat="server" MaxLength="80" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">銀行戶名&ensp;<span style="color: red; font-size: 18px">*</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txb_account_name" runat="server" MaxLength="80" Width="300px"></asp:TextBox>&emsp;<asp:Button ID="btn_same" runat="server" Text="同公司全銜" OnClick="btn_same_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px; height: 39px;">銀行代號&ensp;<span style="color: red; font-size: 18px">*</span></td>
                            <td style="text-align: left" class="auto-style4">
                                <asp:DropDownList ID="Dlist_bank_id" runat="server" DataSourceID="SqlDataSource1" DataTextField="BANK" DataValueField="ID_BANK" AutoPostBack="true" OnSelectedIndexChanged="Dlist_bank_id_SelectedIndexChanged"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ERP %>" SelectCommand="SELECT ID_BANK, ID_BANK + '_' + NM_BANK+'_'+NM_BANK_FULL  AS BANK FROM FCM2BANK;"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 75px; text-align: center; font-size: 14px">帳號&ensp;<span style="color: red; font-size: 18px">*</span></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txb_account"  onkeyup="if(isNaN(value))execCommand('undo')"   onafterpaste="if(isNaN(value))execCommand('undo')" runat="server" MaxLength="20" Width="300px"></asp:TextBox>&ensp;請填寫活期存款帳號
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; vertical-align: middle";width: 75px>
                                <asp:CheckBox ID="chk_yes"  runat="server" />
                            </td>
                            <td style="text-align: left; vertical-align: middle">
                                <span>1. 本公司同意財團法人伊甸社會福利基金會應付本公司款項，得以匯款方式支付，上述資料如有錯誤或發生糾紛，一概由本公司負責，與 貴基金會無關。</span><br />
                                <span>2. 預約轉帳&匯入款項通知，以E-mail或傳真管道，請務必詳實填列，謝謝。</span></td>
                        </tr>
                        <tr>
                            <td style="text-align: left;width: 75px">
                                <asp:Button ID="btn_word" class="btn-success" runat="server" Text="列印申請書" Width="100px" Height="60px" OnClick="btn_word_Click" /></td>
                            <td style="text-align: left;">
                                <asp:FileUpload ID="FileUpload1" runat="server" /><span style="color: red">請上傳用印之網路銀行匯款申請書紙本(jpg、png、tif、pdf)，並將正本寄至伊甸社會福利基金會：台北市文山區萬和階6號4樓-財務室收</span>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lab_bank_id" runat="server" Text="Label" Visible="false"></asp:Label><asp:Label ID="lab_bank_name" runat="server" Text="Label" Visible="false"></asp:Label><asp:Label ID="lab_bank_name_full" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Button ID="btn_send" class="btn btn-primary" runat="server" Text="送出" Width="100px" Height="30px"  OnClientClick="confirmAction(this, '貼心提醒：1. 送出檔案前，請先列印申請書並用印上傳！(jpg、png、tif、pdf) 再提醒這個格式。 WORD不能存2. 上傳後，同意書正本敬請寄回【伊甸-財務室】！感謝您！', performDelete); return false;"/>
                    <%--  <h5>
                        <asp:Label ID="Label4" runat="server" Text="交易資料" Font-Names="微軟正黑體" ></asp:Label></h5>--%>
                    <%--     <table class="table table-bordered">
                        <tr>
                            <td class="info" style="width: 100px; text-align: center; font-size: 14px">銀行戶名</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>&emsp;<asp:Button ID="Button1" runat="server" Text="同公司全銜/個人戶姓名" />
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 100px; text-align: center; font-size: 14px; height: 39px;">銀行代號</td>
                            <td style="text-align: left" class="auto-style4">
                                <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>&emsp;<asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="info" style="width: 100px; text-align: center; font-size: 14px">帳號</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>活期存款
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"  style="text-align: left">
                                <asp:CheckBox ID="CheckBox1" class="form-check-input" runat="server" text="本公司同意財團法人伊甸社會福利基金會應付本公司款項，得以匯款方式支付，上述資料如有錯誤或發生糾紛，一概由本公司負責，與貴公司無關"/></td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button3" class="btn-success" runat="server" Text="列印申請書" /></td>
                            <td style="text-align: center;">
                                <asp:FileUpload ID="FileUpload1" runat="server"/><br/>
                            </td>
                        </tr>
                    </table>--%>
                </div>
            </div>
        </div>
        <script src="../jquery/jquery-1.9.1.js"></script>
        <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
        <script src="../jquery/jquery.blockUI.js"></script>
                <script src="../jquery/alertify.min.js"></script>
        <link href="../colorbox/colorbox.css" rel="stylesheet" />
        <script src="../colorbox/jquery.colorbox.js"></script>
        <script type="text/javascript">
            function performDelete(a_element) {
                __doPostBack("update");
            }

            function confirmAction(a_element, message, action) {
                alertify.set({ labels: { ok: "已上傳", cancel: "重新上傳" } });
                alertify.confirm(message, function (e) {
                    if (e) {
                        // a_element is the <a> tag that was clicked
                        if (action) {
                            action(a_element);
                        }
                    }
                });
            }
        </script>
    </form>
</body>
</html>
