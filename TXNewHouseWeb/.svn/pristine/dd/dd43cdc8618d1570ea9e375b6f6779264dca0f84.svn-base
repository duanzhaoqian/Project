<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UploadPremisesPhoto</title>
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/manage.css")%>" rel="stylesheet"
        type="text/css" />
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/manage_c.css")%>" rel="stylesheet"
        type="text/css" charset="utf-8" />
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/public.css")%>" rel="stylesheet"
        type="text/css" />
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/common.css")%>" rel="stylesheet"
        type="text/css" charset="utf-8" />
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/global.css")%>" rel="stylesheet"
        type="text/css" charset="utf-8" />
    <link href="<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.css" %>"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <%
        string guid = ViewData["guid"] == null ? string.Empty : ViewData["guid"].ToString();
        string picturetype = ViewData["picturetype"] == null ? string.Empty : ViewData["picturetype"].ToString();
        string opt = ViewData["opt"] == null ? "update" : ViewData["opt"].ToString();
        string cityId = ViewData["cityId"] == null ? "0" : ViewData["cityId"].ToString();
        string id = ViewData["id"] == null ? "0" : ViewData["id"].ToString();
    %>
    <div class="loubox clearFix">
        <div class="pl9px mb15">
            <input type="button" name="uploadify" id="uploadify" value="上传图片" class="btn_w80 mr15" />
            <span class="font12 col333">最多可上传20张图片</span>
        </div>
        <div id="img">
        </div>
        <%Html.RenderAction("PremisesPhoto", "Premises", new { guid = guid, picturetype = picturetype, cityId = cityId }); %>
        <div class="big_img" style="display: none;">
            <img alt="" id="bigphoto" src="<%=TXCommons.GetConfig.GetFileUrl("images/img_2.jpg") %>"
                width="450" height="320" />
            <a href="javascript:;" class="close">&nbsp;</a>
        </div>
        <div class="clear">
        </div>
        <div class="clearFix w900">
            <%Html.BeginForm("ImageSub", "Premises", FormMethod.Post);%>
            <input type="hidden" id="guid" name="guid" value="<%=guid%>" />
            <input type="hidden" id="picturetype" name="picturetype" value="<%=picturetype%>" />
            <input type="hidden" id="cityId" name="cityId" value="<%=cityId%>" />
            <input type="hidden" id="data" name="data" value="" />
            <input type="hidden" id="pid" name="pid" value="<%=id%>" />
            <input type="button" class="btn_w97_green" onclick="imagesub()" value="保存" />
            <%Html.EndForm();%>
        </div>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery/jquery-1.9.1.min.js")%>"
        language="javascript" type="text/javascript"></script>
    <script src="<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/jquery.uploadify.js" %>"
        type="text/javascript"></script>
    <!-- InstanceBeginEditable name="EditRegion1" -->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                //基本参数
                swf: '<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.swf" %>',
                uploader: '<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>Premises/upload.ashx?para=<%=guid %>^<%=picturetype %>^20^<%=cityId %>',
                width: 80,
                height: 24,
                buttonText: "上传图片",
                buttonCursor: "hand",
                buttonClass: "btn_w80",
                buttonImage: '<%=TXCommons.GetConfig.GetFileUrl("Scripts/uploadify/btn_uploads.gif")%>',
                //规则参数
                fileSizeLimit: "5000KB",
                fileTypeExts: "*.jpg;*.jpeg;*.png;*.gif",
                fileTypeDesc: "请选择 jpg、jpeg、png、gif 文件",
                multi: true,

                removeTimeout: 1,

                //选择异常事件
                onSelectError: function (file, errorCode, errorMsg) {
                    if (errorCode == -110) {
                        //重写图片过大提示
                        this.queueData.errorMsg = "图片太大了，请选择5M以下大小的图片";
                    }
                },
                //上传成功事件
                onUploadSuccess: function (file, data, response) {
                    //添加一个图片框
                    if (data == "exceedmaxnum") {
                        alert("超出最大上传照片数量。");
                        $("#uploadify").uploadify('cancel', '*');
                        $("#uploadify").uploadify('stop');
                        return;
                    }
                    else if (data != "error") {

                        CreateImageBox(data);
                        SetIframeHeight();
                    }
                },
                //上传异常事件
                onUploadError: function (file, errorCode, errorMsg, errorString) {
                    if (errorString != 'Cancelled') {
                        alert("上传错误，请稍候重试");
                    }
                }
            });

        });

        $(document).ready(function () {
            SetIframeHeight();
            //放大图关闭
            $(".big_img a.close").click(function () {
                $("div.big_img").hide();
                return false;
            });
        });

        function SetIframeHeight() {
            $('[name="frame<%=picturetype%>"]', window.parent.document).css("height", window.document.documentElement.scrollHeight);
        }
    </script>
</body>
</html>
