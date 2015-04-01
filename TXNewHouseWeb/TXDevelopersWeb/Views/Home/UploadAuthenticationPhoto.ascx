<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="TXCommons.PictureModular" %>

<% 
    //用户图片集合
    UserPictureInfo picture = ViewData["picList"] as UserPictureInfo;
%>
<link href="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/uploadify.css")%>" rel="stylesheet" type="text/css" />
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/jquery.uploadify.js")%>" type="text/javascript"></script>

<div id="divUpload">
    <span class="ps_r"><input id="fileUpload" type="file" value="上传图片" class="bnt_w123" /></span>
    <%--显示图片错误信息，用于身份认证页验证--%>
    <span id="errImg" style="display:none;" class="no">请选择文件。</span>
    <span class="col999">您可以上传JPG、JPEG、GIF、PNG文件；且文件小于5M</span>
</div>
<div class="mt10 col333"><span class="red">友情提示</span>：请您注意上传的公司营业执照复印件的正确及清晰，以免因审核不通过影响您的使用。</div>
<div id="divShow" class="mt10">
    <img src="<%=picture == null ? String.Concat(TXCommons.GetConfig.ImgUrl, "images/card.png") : String.Concat(picture.Path, ".180_130.jpg")%>" alt="认证图片" />
    <a onclick="DeleteImageBox('<%=picture == null ? 0 : picture.ID%>');return false;" href="javascript:void(0);" class="ml5">删除</a>
</div>
<%--保存是否上传图片，用于身份认证页验证--%>
<input id="hidImgName" type="hidden" value="<%=picture == null ? String.Empty : picture.FileName%>" />


<script type="text/javascript">

    //设定参数
    var colUpload = $("#divUpload");//上传控件容器
    var colShow = $("#divShow");//显示图片容器
    var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
    var uploadImgName =  "<%=picture == null ? String.Empty : picture.FileName%>"; //检查是否已上传图片
    var guid = '<%=ViewData["GUID"]%>';
    var pictype = '<%=TXCommons.PictureModular.UserPictureType.Identification.ToString()%>';
    var cid = '<%=ViewData["cityId"]%>';
    //初始化上传图片插件
    $("#fileUpload").uploadify({

        //基本参数
        swf: '<%=TXCommons.GetConfig.BaseUrl + "js/uploadify/uploadify.swf"%>',
        uploader: "<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>user/upload.ashx?para=" + guid + "^<%=TXCommons.PictureModular.UserPictureType.Identification.ToString()%>^1^0",

        //显示参数
        width: 123,
        height: 24,
        buttonText: "上传图片",
        buttonCursor: "hand",
        buttonClass: "bnt_w123",
        //规则参数
        fileSizeLimit: "5000KB",
        fileTypeExts: "*.jpg;*.jpeg;*.png;*.gif",
        fileTypeDesc: "请选择 jpg、jpeg、png、gif 文件",
        fileSizeLimit: "5MB",
        multi: false,
        removeTimeout: 1,
        //选择事件
        onSelect: function (file) {
            $("#errImg").hide();
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
            $("#fileUpload").uploadify('cancel', '*');
            if (data == "exceedmaxnum") {
                alert("超出最大上传照片数量。");
                return;
            }
            if (data.indexOf(',') > -1) {
                var splitresult = data.split(',');
                var pictureurl = splitresult[0];
                var id = splitresult[1];
                var str = '<img src="' + pictureurl + '.180_130.jpg" width="180" height="130" alt="认证图片" /><a href="javascript:void(0);" onclick="DeleteImageBox(\'' + id + '\');return false;" class="ml5">删除</a>'
                colShow.html(str);
                colUpload.hide();
                colUpload.next().hide();
                uploadImgName = id; //设置图片名称

                $("#hidImgName").val(uploadImgName); //存入上传图片名称，用于身份认证页验证
            }
        },
        //上传异常事件
        onUploadError: function (file, errorCode, errorMsg, errorString) {
            alert("上传错误，请稍候重试");
        }
    });
    //检查是否有图片
   
    if (uploadImgName == "") {
        colShow.html("");
        colUpload.show();
        colUpload.next().show();
    }
    else {
        colShow.show();
        colUpload.hide();
        colUpload.next().hide();
    }
    
    //删除图片
    function DeleteImageBox(imgboxid) {
        var splitresult = imgboxid.split('_');
        var id = imgboxid;
        if (confirm("确定执行当前操作？")) {
            $.ajax({
                url: '<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>user/delete.ashx?guid=' + guid + '&picturetype=<%=TXCommons.PictureModular.UserPictureType.Identification.ToString()%>&minnum=0&cityId=0&id=' + id,

                type: "get",
                dataType: "jsonp",
                jsonp: "callbackparam",
                jsonpCallback: "success_jsonpCallback",
                success: function (data) {
                    $.map(data, function (item) {
                        if (item.result == "true") {
                            colShow.html("");
                            colUpload.show();
                            colUpload.next().show();
                            $("#hidImgName").val('');
                        }
                        else {
                            if (item.result != "false") {
                                alert(item.result);
                            }
                            else {
                                alert("删除图片出错!");
                            }
                        }
                    });
                },
                error: function () { alert("删除图片出错!"); }
            });
        }
        var files = $('#fileUpload').data('uploadify').queueData.files = [];
        for (var member in files) delete files[member];
      
    }
</script>