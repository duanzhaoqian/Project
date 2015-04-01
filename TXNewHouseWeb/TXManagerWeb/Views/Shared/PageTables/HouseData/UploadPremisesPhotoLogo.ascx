<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="<%=Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.css" %>" rel="stylesheet"
    type="text/css" />
<%
    var guid = ViewData["guid"] == null ? string.Empty : ViewData["guid"].ToString();
    var picturetype = ViewData["picturetype"] == null ? string.Empty : ViewData["picturetype"].ToString();
    var info = ViewData["logo"] as TXCommons.PictureModular.PremisesPictureInfo;
    var opt = ViewData["opt"] == null ? "update" : ViewData["opt"].ToString();
    var cityId = ViewData["cityId"] == null ? "0" : ViewData["cityId"].ToString();
%>
<div class="mt15">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
        <tr>
            <td id="thimglogo" <%= info != null ? "style='display:none;'" : "" %>>
                <input type="file" name="uploadifyLogo" id="uploadifyLogo" class="btn_w80 mr15" value="上传图片" />
            </td>
            <td id="tdimglogo" align="left">
                <% if (info != null)
                   { %>
                <img src="<%= info.Path %>.170_100.jpg" alt="LOGO" /><a href="javascript:void(0);"
                    onclick="DeleteImageBoxLogo('<%= info.ID %>');return false;" class="ml15">删除图片</a>
                <% } %>
            </td>
        </tr>
    </table>
</div>
<script src="<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/jquery.uploadify.js" %>"
    type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#uploadifyLogo").uploadify({
            //基本参数
            swf: '<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.swf" %>',
            uploader: '<%= TXCommons.GetConfig.PremisesImgUploadBaseUrl %>Premises/upload.ashx?para=<%= guid %>^<%= picturetype %>^1^<%= cityId %>',
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
                    CreateImageBoxLogo(data);
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

    function CreateImageBoxLogo(data) {
        var splitresult = data.split(',');
        var pictureurl = splitresult[0];
        var id = splitresult[1];

        var str = '  <img src="' + pictureurl + '.170_100.jpg' + '" /><a href="javascript:void(0);" onclick="DeleteImageBoxLogo(\'' + id + '\');return false;" class="ml15">删除图片</a> ';
        $("#tdimglogo").html(str);
        $("#thimglogo").hide();
        $("#hid_logo").val("1");
    }

    function DeleteImageBoxLogo(imgboxid) {
        var id = imgboxid;

        if (confirm("确定执行当前操作？")) {
            $.ajax({
                url: '<%= TXCommons.GetConfig.PremisesImgUploadBaseUrl %>Premises/Delete.ashx?guid=<%= guid %>&picturetype=<%= picturetype %>&minnum=0&cityId=0&id=' + id,
                type: "get",
                dataType: "jsonp",
                jsonp: "callbackparam",
                jsonpCallback: "success_jsonpCallback",
                success: function (data) {
                    $.map(data, function (item) {
                        if (item.result == "true") {
                            $("#tdimglogo").html('');
                            $("#thimglogo").show();
                            $("#hid_logo").val("0");
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
    }
</script>
