<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/uploadify.css")%>" rel="stylesheet"
    type="text/css" />
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/jquery.uploadify.js")%>"
    type="text/javascript"></script>
<%
    string guid = ViewData["guid"] == null ? string.Empty : ViewData["guid"].ToString();
    string picturetype = ViewData["picturetype"] == null ? string.Empty : ViewData["picturetype"].ToString();
    TXCommons.PictureModular.PremisesPictureInfo info = ViewData["logo"] as TXCommons.PictureModular.PremisesPictureInfo;
    string opt = ViewData["opt"] == null ? "update" : ViewData["opt"].ToString();
    string cityId = ViewData["cityId"] == null ? "0" : ViewData["cityId"].ToString();
%>
<div class="mt15">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
        <tr>
            <td id="thimglogo" <%=info!=null?"style='display:none;'":"" %>>
                <p class="font12 mt5 col333 mb10">
                    您可以上传JPG、JPEG、GIF、PNG文件；单张图片小于5M；<br />
                    为了获得良好的展示效果,推荐尺寸为140*140像素；
                </p>
                <input type="file" name="uploadifyLogo" id="uploadifyLogo" class="btn_w80 mr15" value="上传图片" />
            </td>
            <td id="tdimglogo" align="left">
                <%if (info != null)
                  { %>
                <img id="PremisesLogo" src="<%=info.Path%>.140_140.jpg" alt="LOGO" /><a href="###"
                    onclick="DeleteImageBoxLogo('<%=info.ID%>');return false;" class="ml15">删除图片</a>
                <%}%>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    $(document).ready(function () {

        $("#uploadifyLogo").uploadify({
            //基本参数
            swf: '<%=TXCommons.GetConfig.BaseUrl + "js/uploadify/uploadify.swf"%>',
            uploader: '<%=TXCommons .GetConfig.PremisesImgUploadBaseUrl%>Premises/upload.ashx?para=<%=guid %>^<%=picturetype %>^1^<%=cityId %>',
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
                //添加一个图片框
                if (data == "exceedmaxnum") {
                    alert("超出最大上传照片数量。");
                    return;
                }
                else if (data != "error") {
                    CreateImageBoxLogo(data);

                }

            },
            //上传异常事件
            onUploadError: function (file, errorCode, errorMsg, errorString) {
                alert("上传错误，请稍候重试");
            }
        });
    });

    function CreateImageBoxLogo(data) {

        var splitresult = data.split(',');
        var pictureurl = splitresult[0];
        var id = splitresult[1];

        var str = '  <img id="PremisesLogo" src="' + pictureurl + '.140_140.jpg' + '" /><a href="###" onclick="DeleteImageBoxLogo(\'' + id + '\');return false;" class="ml15">删除图片</a> ';
        $("#tdimglogo").html(str);
        $("#thimglogo").hide();
        $("#PreLogo").hide();

    }




    function DeleteImageBoxLogo(imgboxid) {
        var id = imgboxid;

        if (confirm("确定执行当前操作？")) {



            $.ajax({
                url: '<%=TXCommons .GetConfig.PremisesImgUploadBaseUrl%>Premises/Delete.ashx?guid=<%=guid %>&picturetype=<%=picturetype %>&minnum=0&cityId=0&id=' + id,
                type: "get",
                dataType: "jsonp",
                jsonp: "callbackparam",
                jsonpCallback: "success_jsonpCallback",
                success: function (data) {

                    $.map(data, function (item) {
                        if (item.result == "true") {
                            $("#tdimglogo").html('');
                            $("#thimglogo").show();
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
        var files = $('#uploadifyLogo').data('uploadify').queueData.files = [];
        for (var member in files) delete files[member];

    }
</script>
