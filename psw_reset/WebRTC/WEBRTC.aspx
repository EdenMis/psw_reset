<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WEBRTC.aspx.cs" Inherits="psw_reset.WebRTC.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%--<link href="Plugins/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <h1>WebRTC測試</h1>
        <table class="table table-condensed">
            <tr>
                <td>鏡頭切換</td>
                <td><select id="videoSource"></select></td>
            </tr>
        </table>
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-6">
                    <h2>我的視訊</h2>
                    <video id="video" autoplay="" ></video><br />
                    <button id="open" type="button" class="btn btn-default btn-lg" onclick="opencamera()">打開視訊</button>
                </div>
                <div class="col-md-6">
                    <h2>視訊圖檔</h2>
                    <canvas id="canvas" width="640" height="480"></canvas><br />
                    <asp:Label ID="Label1" runat="server" Text="拍照次數"></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br/>
                    <asp:Label ID="Label2" runat="server" Text="檔案名稱"></asp:Label><asp:TextBox ID="txb_file_name" runat="server"></asp:TextBox><br/>
                    <button type="button" class="btn btn-primary btn-lg" onclick="getscreen()">擷取視訊</button>
                    <button id="download" type="button" onclick="downloadpic()" class="btn btn-success btn-lg">下載檔案</button>
                </div>
            </div>
        </div>
    </form>
</body>
<%--<script src="Plugins/Jquery/jquery-3.2.1.min.js"></script>
<script src="Plugins/Bootstrap/js/bootstrap.min.js"></script>--%>
<script>
    //開啟視訊
    function opencamera() {
        //判斷browser是否支援視訊功能
        if (navigator.getUserMedia || navigator.webkitGetUserMedia ||
            navigator.mozGetUserMedia) {
            //此方法已過時...雖然也可以用
            //navigator.mediaDevices.getUserMedia({ video: true }, handleSuccess, handleError);
            navigator.mediaDevices.getUserMedia({ video: true })
                //成功時的處理
                .then(function (stream) {
                    document.getElementById('video').srcObject = stream;
                    return navigator.mediaDevices.enumerateDevices();
                })
                //.then(function (deviceinfo) {
                //    var aa = "";
                //    for (let i = 0; i < deviceinfo.length; i++) {
                //        const option = document.createElement('option');
                //        if (deviceinfo[i].kind === 'videoinput') {
                //            option.text = deviceinfo[i].label;
                //            //aa += deviceinfo[i].value;
                //            document.getElementById('videoSource').appendChild(option);
                //        }
                //    }
                //    document.getElementById('aa').innerText = aa;
                //})
                //失敗時的處理
                .catch(function (error) {
                    alert("無法取得視訊影像");
                })
            //navigator.webkitGetUserMedia({ video: { mandatory: { chromeMediaSource: 'screen' } } }, getscreen);
        }
        else {
            alert("本功能不支援你的爛電腦拉!!!");
        }
    }
    function getscreen(stream) {
        const video = window.video = document.querySelector('video');
        console.log(video);
        document.getElementById('canvas').getContext('2d').drawImage
            (video, 0, 0, 480, 680);

    }
    function downloadpic() {
        var link = document.createElement('a');
        link.download = "my-image.png";
        link.href = document.getElementById('canvas').toDataURL("image/png").replace(/^data:image\/[^;]/, 'data:application/octet-stream');;
        link.click();
    }
</script>
</html>
