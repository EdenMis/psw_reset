<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TEST2.aspx.cs" Inherits="psw_reset.TEST2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../jquery/exif.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="img0" class="img_group" runat="server" ImageUrl="~/menu/201981161310統一冷藏目錄.jpg" />
            <asp:Image ID="img1" class="img_group" runat="server" ImageUrl="~/menu/201981161310統一冷藏目錄.jpg" />
        </div>
    </form>
    <script src="../jquery/jquery-1.9.1.js"></script>
    <script src="../bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <link href="../colorbox/colorbox.css" rel="stylesheet" />

    <script src="../colorbox/jquery.colorbox.js"></script>
    <script type="text/javascript">

        //var size = $('.img_group').size();
        //for (var i = 0; i < size; i++)
        //{
        //    var imgId = 'img' + i;
        //    var TimgId = '#img' + i;
        //    document.getElementById(imgId).onload = function () {
        //        EXIF.getData(this, function () {
        //            var imgTarget = $(TimgId);
        //            var orientation = EXIF.getTag(this, 'Orientation');
        //            if (orientation && orientation !== 1) {
        //                switch (orientation) {
        //                    case 6:
        //                        imgTarget.css({ transform: "rotate(90deg)" });
        //                        break;
        //                    case 8:
        //                        //逆时针90度旋转

        //                        imgTarget.css({ transform: "rotate(270deg)" });
        //                        break;
        //                    case 3:
        //                        //180度旋转

        //                        imgTarget.css({ transform: "rotate(180deg)" });
        //                        break;
        //                }
        //            }
        //        });
        //    }
        //}



            document.getElementById('img0').onload = function () {
                EXIF.getData(this, function () {
                    var imgTarget = $('#img0');
                    var orientation = EXIF.getTag(this, 'Orientation');
                    if (orientation && orientation !== 1) {
                        switch (orientation) {
                            case 6:
                                imgTarget.css({ transform: "rotate(90deg)" });
                                break;
                            case 8:
                                //逆时针90度旋转

                                imgTarget.css({ transform: "rotate(270deg)" });
                                break;
                            case 3:
                                //180度旋转

                                imgTarget.css({ transform: "rotate(180deg)" });
                                break;
                        }
                    }
                });
        }

                document.getElementById('img1').onload = function () {
                EXIF.getData(this, function () {
                    var imgTarget = $('#img1');
                    var orientation = EXIF.getTag(this, 'Orientation');
                    if (orientation && orientation !== 1) {
                        switch (orientation) {
                            case 6:
                                imgTarget.css({ transform: "rotate(90deg)" });
                                break;
                            case 8:
                                //逆时针90度旋转

                                imgTarget.css({ transform: "rotate(270deg)" });
                                break;
                            case 3:
                                //180度旋转

                                imgTarget.css({ transform: "rotate(180deg)" });
                                break;
                        }
                    }
                });
            }


    </script>
</body>
</html>
