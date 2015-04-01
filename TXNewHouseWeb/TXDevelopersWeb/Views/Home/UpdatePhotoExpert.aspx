<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXOrm.Developer>" %>
<%@ Import Namespace="TXCommons.PictureModular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	新房后台-修改头像-高级模式
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% 
    //用户图片集合
    UserPictureInfo picture = ViewData["picList"] as UserPictureInfo;//80px图片
%>
<div class="content">
  <div class="bgf8_box">
    <p class="ml85 clear"><input id="fileUpload" type="button" value="" class="bd_upload mr20 fl" style="margin-bottom:0px;" /></p>
    <span id="err" style="display:none;" class="ml85 red font12 dis_inb mt10"><i class="bd_no"></i>请选择文件。</span>
    <p class="ml85 font12 col333 mt10 mb5">限jpeg/jpg/gif/png格式且不超过5MB的文件。</p>
  </div>
  <div class="uploadbox clear">
    <div class="left">
      <table width="100%" height="300" border="0" cellspacing="0" cellpadding="0">
        <tbody>
          <tr>
            <td valign="middle" align="center" id="b_infoimage"><img id="imgShow300" src="<%=picture == null ? TXCommons.GetConfig.ImgUrl + "images/photo_d.jpg" : picture.Path%>" width="300px" height="300px"/></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="right">
        <div class="yellow_box mb30"><p class="col333 font12 ts1">您上传的图像会自动生成以下尺寸，请注意各尺寸图像是否清晰。</p></div>
        <p class="mb30" style="width:80px;height:80px;overflow:hidden;"><img id="imgShow80"  src="<%=picture == null ? TXCommons.GetConfig.ImgUrl + "images/photo_d.jpg" : picture.Path%>"><span class="font12 ml10 c-333">80x80像素</span></p>
        <p class="mb30" style="width:50px;height:50px;overflow:hidden;"><img id="imgShow50"  src="<%=picture == null ? TXCommons.GetConfig.ImgUrl + "images/photo_d.jpg" : picture.Path%>"><span class="font12 ml10 c-333">50x50像素</span></p>
        <p><span class="font12 c-333">切换到<a class="blue ml10" href="<%=TXCommons.GetConfig.BaseUrl%>home/updatephoto">普通上传模式</a></span></p>
     </div>
  </div>
  <div class="mt20 mb20 tac"><input id="btnSave" type="button" value="保存设置" class="btn_w97_green" /></div>
</div>
<link href="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/uploadify.css")%>" rel="stylesheet" type="text/css" />
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/jquery.uploadify.js")%>" type="text/javascript"></script>
<link href="<%=TXCommons.GetConfig.GetFileUrl("js/jquery.jcrop/jquery.Jcrop.css")%>" rel="stylesheet" type="text/css" />
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery.jcrop/jquery.Jcrop.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        //******上传图片插件******
        //设定参数
        $("#btnSave").removeAttr("disabled");
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
        var uploadImgName = "<%=picture == null ? String.Empty : picture.FileName%>"; //检查是否已上传图片
        var guid = '<%=ViewData["GUID"]%>';
        var pictype = '<%=ViewData["pictureType"]%>';
        var cid = '<%=ViewData["cityId"]%>';
        var upLoadName = '';
        var img_url = '';
        //初始化上传图片插件
        $("#fileUpload").uploadify({
            swf: '<%=TXCommons.GetConfig.BaseUrl + "js/uploadify/uploadify.swf"%>',
            uploader: "<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>user/UploadCommon.ashx?para=" + guid,
            formData: { "para": guid + "^" + pictype + "^1^" + cid },
            //显示参数
            width: 138,
            height: 34,
            buttonText: "",
            buttonCursor: "hand",
            buttonClass: "bd_upload",
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
                $("#imgShow300").attr("src", datad[0] + ".300_300.jpg"); //设置图片路径并显示
                $("#b_infoimage").html('<img id="imgShow300" src="' + datad[0] + '.300_300.jpg' + '" width="300px" height="300px"/>');
                $("#imgShow80").attr("src", datad[0] + ".300_300.jpg"); //设置图片路径并显示
                $("#imgShow50").attr("src", datad[0] + ".300_300.jpg"); //设置图片路径并显示
                img_url = datad[0];
                uploadImgName = datad[1]; //设置图片名称
                upLoadName = datad[1];
                initJcrop(); //初始化裁剪插件
            },
            //上传异常事件
            onUploadError: function (file, errorCode, errorMsg, errorString) {
                alert("上传错误，请稍候重试");
            }
        });

        //******裁剪图片插件******
        //宽、高
        //80的x、y
        //50的x、y
        var x, y, w80, h80, w50, h50;
        function showThumb(coords) {
            //显示坐标等信息
            x = coords.x;
            y = coords.y;
            w80 = coords.w;
            h80 = coords.h;
            //缩略图显示的大小
            var rx = 80 / coords.w;
            var ry = 80 / coords.h;
            //设定缩略图显示样式
            $("#imgShow80").css({
                width: Math.round(rx * $("#imgShow300").width()) + 'px',
                height: Math.round(ry * $("#imgShow300").height()) + 'px',
                marginLeft: '-' + Math.round(rx * coords.x) + 'px',
                marginTop: '-' + Math.round(ry * coords.y) + 'px'
            });
            //缩略图显示的大小
            rx = 50 / coords.w;
            ry = 50 / coords.h;
            //设定缩略图显示样式
            $("#imgShow50").css({
                width: Math.round(rx * $("#imgShow300").width()) + 'px',
                height: Math.round(ry * $("#imgShow300").height()) + 'px',
                marginLeft: '-' + Math.round(rx * coords.x) + 'px',
                marginTop: '-' + Math.round(ry * coords.y) + 'px'
            });
        }
        //初始化裁剪
        function initJcrop() {
            //初始化剪裁图片插件
            $("#imgShow300").Jcrop({
                onChange: showThumb,
                onSelect: showThumb,
                aspectRatio: 1,
                setSelect: [0, 0, 300, 300],
                minSize: [80, 80]
            });
        }
        //初始化时有图片则加载裁剪
        if (uploadImgName != "") {

            initJcrop();

        }

        //******初始化事件******
        //保存头像
        //        $("#btnSave").click(function () {

        //          
        //            //检查是否已上传图片
        //            if (uploadImgName == "") {
        //                $("#err").show();
        //            }
        //            else {
        //                //******
        //                //保存图片操作（王亮添加）
        //                //******
        //                $.post(mainUrl + "home/savephotoexpert", { imgName: uploadImgName, x: x, y: y, w80: w80, h80: h80, w50: w50, h50: h50, m: Math.random() }, function (data) {
        //                    if (data == "True") {
        //                        alert("保存成功");
        //                    }
        //                    else {
        //                        alert("保存失败");
        //                    }
        //                });
        //            }
        //        });

        $("#btnSave").bind("click", function () {
            var filename = uploadImgName;
            w50 = $("#imgShow300").width();
            h50 = $("#imgShow300").height();

            if (h80 == 0 || w80 == 0) {
                alert("图片裁剪尺寸太小，请重新裁剪");
                return;
            }

            if (filename) {
                var url = "<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>User/SaveSettingAdvanced.ashx?guid=" + guid + "&x=" + x + "&y=" + y + "&w=" + w80 + "&h=" + h80 + "&ow=" + w50 + "&oh=" + h50 + "&filename=" + filename;
                $("#btnSave").attr("disabled", "disabled");
                $("#btnSave").val("处理中...");
                $.ajax({
                    url: url,
                    type: "get",
                    dataType: "jsonp",
                    jsonp: "callbackparam",
                    jsonpCallback: "success_jsonpCallback",
                    success: function (data) {
                        $.map(data, function (item) {
                            $("#btnSave").removeAttr("disabled");
                            $("#btnSave").val("保存设置");
                            upLoadName = '';
                            if (item.result == "true") {
                                if (img_url == '') {
                                    img_url = $("#imgShow300").attr("src");
                                }
                                var murl = img_url + "?url=" + Math.random();
                                //alert("保存成功！");
                                window.location.reload();
                                $("#imgShow300").attr("src", murl); //设置图片路径并显示
                                $.Jcrop("#imgShow300").destroy();
                                $("#b_infoimage").html('<img id="imgShow300" src="' + murl + '" width="300px" height="300px"/>');


                                initJcrop();
                                $("#imgShow80").attr("src", img_url + murl); //设置图片路径并显示
                                $("#imgShow50").attr("src", img_url + murl); //设置图片路径并显示


                            }
                            else {

                                if (item.result != "false") {
                                    alert(item.result);

                                }
                                else {
                                    alert("保存出错!");

                                }
                            }
                        });
                    },
                    error: function () { alert("保存出错!"); }
                });
            } else {
                alert("请上传头像！");
            }
        });
    });
</script>
</asp:Content>
