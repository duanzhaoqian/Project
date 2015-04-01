<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/uploadify.css")%>" rel="stylesheet"
    type="text/css" />
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/uploadify/jquery.uploadify.js")%>"
    type="text/javascript"></script>
<%
    string guid = ViewData["guid"] == null ? string.Empty : ViewData["guid"].ToString();
    string picturetype = ViewData["picturetype"] == null ? string.Empty : ViewData["picturetype"].ToString();
    TXCommons.PictureModular.PremisesPictureInfo info = ViewData["list"] as TXCommons.PictureModular.PremisesPictureInfo;
    string opt = ViewData["opt"] == null ? "update" : ViewData["opt"].ToString();
    string cityId = ViewData["cityId"] == null ? "0" : ViewData["cityId"].ToString();
%>
<script type="text/javascript">
    $(document).ready(function () {

        $("#uploadify").uploadify({
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
                    CreateImageBox(data);

                }

            },
            //上传异常事件
            onUploadError: function (file, errorCode, errorMsg, errorString) {
                alert("上传错误，请稍候重试");
            }
        });
    });

    function CreateImageBox(data) {
        $("#uploadify").uploadify('cancel', '*');
        var splitresult = data.split(',');
        var pictureurl = splitresult[0];
        var id = splitresult[1];
        $("#SandBoxPic").attr("src", pictureurl + ".180_130.jpg");
        $("#PicId").val(id);
        $("#PicSrc").val(pictureurl + ".692_450.jpg");
        $("#SandBoxName").show();
        $("#tdmgsp").hide();
        //var str = '  <img src="' + pictureurl + '.380_330.jpg' + '" /><a href="###" onclick="DeleteImageBox(\'' + id + '\');return false;" class="ml15">删除图片</a> <a  onclick="Sandbox();"  style="cursor: pointer" class="ml15">' +
        //   '楼栋标号及编辑</a>'
        //$("#tdimg").html(str);

    }



    var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";

    //删除楼盘沙盘图标记
    function DeleteSandBox(DeveloperId, PremisesId) {
        $.ajax({
            url: mainUrl + "Premises/DeleteSandBox",
            type: "post",
            cache: false,
            data: { Did: DeveloperId, Pid: PremisesId },
            success: function (result) {
                if (result) {
                    alert("删除成功");
                }
            }

        });
    }

    function DeleteImageBox(imgboxid) {

        var id = imgboxid;

        $.ajax({
            url: '<%=TXCommons .GetConfig.PremisesImgUploadBaseUrl%>Premises/Delete.ashx?guid=<%=guid %>&picturetype=<%=picturetype %>&minnum=0&cityId=0&id=' + id,
            type: "get",
            dataType: "jsonp",
            jsonp: "callbackparam",
            jsonpCallback: "success_jsonpCallback",
            success: function (data) {

                $.map(data, function (item) {
                    if (item.result == "true") {
                        //$("#tdimg").html('');
                        $("#SandBoxName").hide();
                        $("#tdmgsp").show();
                        //删除楼盘沙盘图 关联删除楼栋标记
                        DeleteSandBox($("#DeveloperId").val(), $("#PremisesId").val());
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

        var files = $('#uploadify').data('uploadify').queueData.files = [];
        for (var member in files) delete files[member];
    }
</script>
<div class="mt15">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
        <tr>
            <th width="11%" valign="top">
            </th>
            <td id="tdmgsp" width="89%" align="left">
                <p class="font12 mt5 col333 mb10">
                    您可以上传JPG、JPEG、GIF、PNG文件；且文件小于5M
                </p>
                <input type="file" name="uploadify" id="uploadify" class="btn_w80 mr15" value="上传沙盘图片" />
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td id="tdimg" align="left">
                <%--  <%if (info != null)
                  { %>
                <input id="PicSrc" type="hidden" value="<%=info.Path%>" />
                <img src="<%=info.Path%>.180_130.jpg" alt="沙盘图" /><a href="###" onclick="DeleteImageBox('<%=info.ID%>');return false;"
                    class="ml15">删除图片</a> <a id="Sandbox" style="cursor: pointer" class="ml15">楼栋标号及编辑</a>
                <%} %>--%>
                <%if (info != null)
                  {%>
                <div id="SandBoxName">
                    <img id="SandBoxPic" alt="沙盘图" src="<%=info.Path+".180_130.jpg"%>" />
                    <input id="PicId" type="hidden" value="<%=info.ID%>" />
                    <input id="PicSrc" type="hidden" value="<%=info.Path+".692_450.jpg"%>" />
                    <a href="javascript:void(0);" id="DelPic" class="ml15">删除图片</a> <a id="Sandbox" style="cursor: pointer"
                        class="ml15">楼栋标号及编辑</a>
                </div>
                <%}
                  else
                  {%>
                <div id="SandBoxName" style="display: none">
                    <img id="SandBoxPic" alt="沙盘图" />
                    <input id="PicId" type="hidden" />
                    <input id="PicSrc" type="hidden" />
                    <a href="javascript:void(0);" id="DelPic" class="ml15">删除图片</a> <a id="Sandbox" style="cursor: pointer"
                        class="ml15">楼栋标号及编辑</a>
                </div>
                <%}%>
            </td>
        </tr>
    </table>
</div>
<%if (info != null)
  {%>
<script type="text/javascript">

    $(document).ready(function () {
        $("#tdmgsp").hide();
    });
</script>
<%}%>
<script language="javascript" type="text/javascript">
    $("#DelPic").bind("click", function () {
        if (confirm("确定执行当前操作？")) {
            var id = $("#PicId").val();
            DeleteImageBox(id);
            $("#SandBoxName").hide();
            $("#tdmgsp").show();
        }
    });

    
</script>
