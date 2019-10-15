using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using HtmlAgilityPack;
using System.Text;
using System.Net;

namespace psw_reset
{
    public partial class TEST3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //個案基本資料
            string id = "";
            string tel = "";
            string name = "";
            string day = "";
            string Indigenous = "";
            string Indigenous_type = "";
            string sex = "";
            string live_state = "";
            string Residence_address = "";
            string address = "";
            string language = "";
            string disability_card = "";
            string disability_type = "";
            string disability_rank = "";
            string welfare_type = "";
            string work_state = "";
            string work_hope = "";
            string live_mechanism = "";
            string hospitalization = "";
            string nurse_state = "";
            string nurse_num = "";
            string sick_state = "";
            string service_type = "";


            if (FileUpload1.HasFile != false)
            {
                string filename = FileUpload1.FileName;
                string filename2 = DateTime.Now + FileUpload1.FileName;
                String savePath = Request.PhysicalApplicationPath + "Upload_html\\";

                if (Directory.Exists(savePath) == false)
                {
                    Directory.CreateDirectory(savePath);
                }
                string serverFilePath = Path.Combine(savePath, filename);
                FileUpload1.SaveAs(serverFilePath);

                var webget = new HtmlWeb();  //New一個HtmlWeb
                var doc = webget.Load(serverFilePath); //匯入Html檔案之路徑
               
                //HtmlNode name_node = doc.DocumentNode.SelectSingleNode("//span[@id='name']");   //HTML的屬性跟ＩＤ名稱

                //身分證字號
                HtmlNode id_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[9]/td[2]/p/span"); //身分證結點之XPath
                if (id_node != null)
                {
                    id = id_node.InnerText;  //取得身份證字號結點的值
                }
                else
                {
                    id = "not found";
                }

                //電話
                HtmlNode tel_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[9]/td[4]/p/span");
                if (tel_node != null)
                {
                    tel = tel_node.InnerText;
                }
                else
                {
                    tel = "not found";
                }

                //個案姓名
                HtmlNode name_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[10]/td[2]/p/span");  
                if (name_node != null)
                {
                    name = name_node.InnerText;
                }
                else
                {
                    name = "not found";
                }

                //個案生日
                HtmlNode day_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[10]/td[4]/p/span"); 
                if (day_node != null)
                {
                    day = day_node.InnerText;
                }
                else
                {
                    day = "not found";
                }

                //原住民身分
                HtmlNode Indigenous_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[11]/td[2]/p/span"); 
                if (Indigenous_node != null)
                {
                    Indigenous = Indigenous_node.InnerText;
                }
                else
                {
                    Indigenous = "not found";
                }

                //原住民族別
                HtmlNode Indigenous_type_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[11]/td[4]/p/span"); 
                if (Indigenous_type_node != null)
                {
                    Indigenous_type = Indigenous_type_node.InnerText;
                }
                else
                {
                    Indigenous_type = "not found";
                }

                //性別
                HtmlNode sex_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[12]/td[2]/p/span"); 
                if (sex_node != null)
                {
                    sex = sex_node.InnerText;
                }
                else
                {
                    sex = "not found";
                }

                //居住狀態
                HtmlNode live_state_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[12]/td[4]/p/span");  
                if (live_state_node != null)
                {
                    live_state = live_state_node.InnerText;
                }
                else
                {
                    live_state = "not found";
                }

                //戶籍地址
                HtmlNode Residence_address_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[13]/td[2]/p/span");
                if (Residence_address_node != null)
                {
                    Residence_address = Residence_address_node.InnerText;
                }
                else
                {
                    Residence_address = "not found";
                }

                //地址
                HtmlNode address_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[14]/td[2]/p/span"); 
                if (address_node != null)
                {
                    address = Residence_address_node.InnerText;
                }
                else
                {
                    address = "not found";
                }

                //常用語言
                for (int i = 1; i <= 14; i++)  //於該html上的該td有14個span，使用Chrome找出最後一個span的數值
                {
                    HtmlNode language_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[15]/td[2]/p/span[" + i + "]");
                    if (language_node != null)
                    {
                        if (language_node.InnerText == "&#9745;") //於html上為☑的項目，需用機碼做比對
                        {
                            switch (i)
                            {
                                case 1:
                                    language = language + "國語,";
                                    break;
                                case 4:
                                    language = language + "台語,";
                                    break;
                                case 7:
                                    language = language + "客語,";
                                    break;
                                case 10:
                                    language = language + "原住民語,";
                                    break;
                                case 13:
                                    language = language + "其他,";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        language = "not found";
                    }
                }

                //身心障礙手冊
                HtmlNode disability_card_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[16]/td[2]/p/span"); 
                if (disability_card_node != null)
                {
                    disability_card = disability_card_node.InnerText;
                }
                else
                {
                    disability_card = "not found";
                }

                //障礙類別
                HtmlNode disability_type_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[17]/td[2]/p/span"); 
                if (disability_type_node != null)
                {
                    disability_type = disability_type_node.InnerText;
                }
                else
                {
                    disability_type = "not found";
                }

                //障礙程度
                HtmlNode disability_rank_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[18]/td[2]/p/span");
                if (disability_rank_node != null)
                {
                    disability_rank = disability_rank_node.InnerText;
                }
                else
                {
                    disability_rank = "not found";
                }

                //福利身分別
                HtmlNode welfare_type_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[18]/td[4]/p/span"); 
                if (welfare_type_node != null)
                {
                    welfare_type = welfare_type_node.InnerText;
                }
                else
                {
                    welfare_type = "not found";
                }

                //就業狀態
                HtmlNode work_state_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[19]/td[2]/p/span");
                if (work_state_node != null)
                {
                    work_state = work_state_node.InnerText;
                }
                else
                {
                    work_state = "not found";
                }

                //就業意願
                HtmlNode work_hope_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[19]/td[4]/p/span"); 
                if (work_hope_node != null)
                {
                    work_hope = work_hope_node.InnerText;
                }
                else
                {
                    work_hope = "not found";
                }

                //居住機構
                HtmlNode live_mechanism_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[20]/td[2]/p/span");
                if (live_mechanism_node != null)
                {
                    live_mechanism = live_mechanism_node.InnerText;
                }
                else
                {
                    live_mechanism = "not found";
                }

                //住院狀態
                HtmlNode hospitalization_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[21]/td[2]/p/span");
                if (hospitalization_node != null)
                {
                    hospitalization = hospitalization_node.InnerText;
                }
                else
                {
                    hospitalization = "not found";
                }

                //看戶狀態
                HtmlNode nurse_state_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[22]/td[2]/p/span"); 
                if (nurse_state_node != null)
                {
                    nurse_state = nurse_state_node.InnerText;
                }
                else
                {
                    nurse_state = "not found";
                }

                //看戶數量
                HtmlNode nurse_num_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[22]/td[4]/p/span"); 
                if (nurse_num_node != null)
                {
                    nurse_num = nurse_num_node.InnerText;
                }
                else
                {
                    nurse_num = "not found";
                }


                //疾病狀態
                HtmlNode sick_state_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[23]/td[2]/p/span"); 
                if (sick_state_node != null)
                {
                    sick_state = sick_state_node.InnerText;
                }
                else
                {
                    sick_state = "not found";
                }

                //服務類型
                for (int i = 1; i <= 47; i++)
                {
                    HtmlNode service_node = doc.DocumentNode.SelectSingleNode("/html/body/div/table[2]/tr[24]/td[2]/p/span[" + i + "]");
                    if (service_node != null)
                    {
                        if (service_node.InnerText == "&#9745;")
                        {
                            switch (i)
                            {
                                case 1:
                                    service_type = service_type + "居家服務,";
                                    break;
                                case 4:
                                    service_type = service_type + "社區職能治療,";
                                    break;
                                case 7:
                                    service_type = service_type + "日間照顧,";
                                    break;
                                case 10:
                                    service_type = service_type + "社區物理治療,";
                                    break;
                                case 13:
                                    service_type = service_type + "家庭托顧,";
                                    break;
                                case 16:
                                    service_type = service_type + "輔具購買、租借及居家無障礙環境改善,";
                                    break;
                                case 19:
                                    service_type = service_type + "居家喘息服務,";
                                    break;
                                case 22:
                                    service_type = service_type + "老人營養餐飲服務,";
                                    break;
                                case 25:
                                    service_type = service_type + "機構喘息服務,";
                                    break;
                                case 28:
                                    service_type = service_type + "交通接送服務,";
                                    break;
                                case 31:
                                    service_type = service_type + "居家護理,";
                                    break;
                                case 34:
                                    service_type = service_type + "機構服務,";
                                    break;
                                case 37:
                                    service_type = service_type + "居家職能治療,";
                                    break;
                                case 40:
                                    service_type = service_type + "密集性照護,";
                                    break;
                                case 43:
                                    service_type = service_type + "居家物理治療,";
                                    break;
                                case 46:
                                    service_type = service_type + "其他,";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        service_type = "not found";
                    }
                }

                Response.Write("身分證字號：" + id +
                    "<br/>電話：" + tel +
                    "<br/>個案姓名：" + name +
                    "<br/>生日：" + day +
                    "<br/>原住民身分：" + Indigenous +
                    "<br/>原住民族別：" + Indigenous_type +
                    "<br/>性別：" + sex +
                    "<br/>目前居住狀態：" + live_state +
                    "<br/>戶籍地址：" + Residence_address +
                    "<br/>現居地址：" + address +
                    "<br/>常用語言：" + language +
                    "<br/>身心障礙手冊：" + disability_card +
                    "<br/>障礙類別：" + disability_type +
                    "<br/>障礙程度：" + disability_rank +
                    "<br/>福利身分別：" + welfare_type +
                    "<br/>就業中：" + work_state +
                    "<br/>就業意願：" + work_hope +
                    "<br/>目前居住機構：" +live_mechanism +
                    "<br/>就業意願：" + work_hope +
                    "<br/>最近三個月是否住院：" + hospitalization +
                    "<br/>目前聘請看護幫忙照顧：" +nurse_state+
                    "<br/>目前聘請外籍看護工人數：" + nurse_num +
                    "<br/>罹患疾病：" +sick_state +
                    "<br/>服務類型：" + service_type);
            }
            else
            {
                string script = "alert('請選擇檔案')";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msg", script, true);
            }
        }
    }
}