<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXOrm.Developer>" %>

<%@ Import Namespace="TXCommons.PictureModular" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-修改头像-普通模式
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        //用户图片集合

        UserPictureInfo picture80 = ViewData["picList"] as UserPictureInfo;//80px图片
    %>
    <div class="content">
        <div class="bg_fafafa">
            <div class="upload_photo clear">
                <div class="left tar">
                    <img id="imgShow" src="<%=picture80 == null ? TXCommons.GetConfig.ImgUrl + "images/photo_96.jpg" : picture80.Path+".80_80.jpg"%>"
                        width="80px" height="80px"></div>
                <div class="right">
                    <p>
                        <strong class="font14 col333">从电脑中选择所需图像</strong></p>
                    <p class="font12 mt5 col333">
                        您可以上传JPG、JPEG、GIF、PNG文件；且文件小于5M
                    </p>
                    <p class="mt10">
                        <input id="fileUpload" type="file" value="选择文件" class="btn_w80" /></p>
                    <p class="mb20 mt5">
                        <span id="err" style="display: none;" class="no">请选择文件。</span></p>
                    <p class="mt10 mb20">
                        <input id="btnSave" type="button" value="保存设置" class="btn_w97_green" /></p>
                </div>
            </div>
            <div class="bg_line tac">
                <span class="font12 col333">切换到<a href="<%=TXCommons.GetConfig.BaseUrl%>home/updatephotoexpert"
                    class="ml10">高级上传模式</a></span></div>
        </div>
    </div>
    <link href="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/uploadify.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/jquery.uploadify.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //设定参数
            var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
            var uploadImgName = "<%=picture80 == null ? String.Empty : picture80.FileName%>"; //检查是否已上传图片
            var guid = '<%=ViewData["GUID"]%>';
            var pictype = '<%=ViewData["pictureType"]%>';
            var cid = '<%=ViewData["cityId"]%>';
            var upLoadName = '';
            //初始化上传图片插件
            $("#fileUpload").uploadify({
                //基本参数
                swf: '<%=TXCommons.GetConfig.BaseUrl + "js/uploadify/uploadify.swf"%>',
                uploader: "<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>user/UploadCommon.ashx?para=<%=Model.InnerCode%>",
                width: 80,
                height: 24,
                buttonText: "上传图片",
                buttonCursor: "hand",
                buttonClass: "btn_w80",
                //规则参数
                fileSizeLimit: "5000KB",
                fileTypeExts: "*.jpg;*.jpeg;*.png;*.gif",
                fileTypeDesc: "请选择 jpg、jpeg、png、gif 文件",
                multi: false,
                removeTimeout: 1,
                //选择事件
                onSelect: function (file) {
                    $("#err").hide();
                },
                //选择异常事件
                onSelectError: function (file, errorCode, errorMsg) {
                    if (errorCode == -110) {
                        //重写图片过大提示
                        this.queueData.errorMsg = "图片太大了，请选择5M以下大小的图片";
                    }
                    else {
                        this.queueData.errorMsg = "请选择正确的图片文件";
                    }
                },

                //上传成功事件
                onUploadSuccess: function (file, data, response) {
                    //******
                    //显示图片操作（王亮添加）
                    //******
                    var files = $('#fileUpload').data('uploadify').queueData.files = [];
                    for (var member in files) delete files[member];
                    var datad = data.split(',');
                    $("#imgShow").attr("src", datad[0] + ".80_80.jpg"); //设置图片路径并显示
                    uploadImgName = datad[1]; //设置图片名称
                    upLoadName = datad[1];
                },
                //上传异常事件
                onUploadError: function (file, errorCode, errorMsg, errorString) {
                    alert("上传错误，请稍候重试");
                }
            });


            $("#btnSave").bind("click", function () {
                var filename = upLoadName;
                if (filename) {
                    var url = "<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>user/SaveCommon.ashx?para=<%=Model.InnerCode%>^" + filename;
                    $("#btnSave").attr("disabled", "disabled");
                    $("#btnSave").val("处理中...")
                    $.ajax({
                        url: url,
                        type: "get",
                        dataType: "jsonp",
                        jsonp: "callbackparam",
                        jsonpCallback: "success_jsonpCallback",
                        success: function (data) {
                            upLoadName = '';
                            $("#btnSave").removeAttr("disabled");
                            $("#btnSave").val("保存设置")
                            $.map(data, function (item) {
                                if (item.result == "true") {
                                    alert('保存成功');
                                    
                                }
                                else {
                                    alert("保存失败");
                                }
                            });
                        },
                        error: function () { alert("保存失败!"); }
                    });
                } else {
                    alert("请上传头像！");
                }
            });
        });
    </script>
</asp:Content>
