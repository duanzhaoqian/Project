<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="TXCommons.PictureModular" %>

<% 
    // 用户合同
    var document = ViewData["file"] as DocumentInfo;
%>

<div id="divShow_File">
    <span id="download_file_href"><%= document == null ? "" : document.FileName%></span>
</div>

<div id="divUpload_File" class="mt10">
    <span class="ps_r"><input id="fileUpload_hetong" type="file" value="上传文件" class="bnt_w123" /></span>
    <%--显示图片错误信息，用于身份认证页验证--%>
    <span id="errDocument" style="display:none;" class="lose">请选择文件。</span>
    <span class="col999">您可以上传JPG、JPEG、GIF、PNG文件或者PDF文件，且文件小于5M；如果图片过多的话您可以打一个压缩包上传</span>
</div>

<%--保存是否上传图片，用于身份认证页验证--%>
<input id="hidFileName" type="hidden" value="<%= document == null ? String.Empty : document.FileName %>" />

<script type="text/javascript">

    var file_hetong = {
        
        // 上传文件控件容器
        cont_upload_file: $("#divUpload_File"),

        // 显示文件容器
        cont_show_file: $("#divShow_File"),

        url_main: '<%=TXCommons.GetConfig.BaseUrl %>',

        upload_file_name: '<%=document == null ? String.Empty : document.FileName %>',

        guid: '<%=ViewData["GUID"] %>',

        pictype: '<%= DocumentType.PACT.ToString() %>',

        cityid: '<%=ViewData["cityId"]%>',

        initData: function() {
            
            //检查是否有文件
            if (file_hetong.upload_file_name == "") {
                file_hetong.cont_show_file.hide();
                file_hetong.cont_upload_file.show();
            }
            else {
                file_hetong.cont_show_file.show();
                file_hetong.cont_upload_file.show();
            }
        }

    };

    //初始化上传图片插件
    $("#fileUpload_hetong").uploadify({

        //基本参数
        swf: '<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.swf" %>',
        uploader: "<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>document/upload.ashx?para=" + guid + "^<%=TXCommons.PictureModular.DocumentType.PACT.ToString()%>",

        //显示参数
        width: 123,
        height: 24,
        buttonText: "上传文件",
        buttonCursor: "hand",
        buttonClass: "bnt_w123",
        //规则参数
        fileSizeLimit: "5000KB",
        multi: false,
        removeTimeout: 1,
        //选择事件
        onSelect: function (file) {
            $("#errDocument").hide();
        },
        //选择异常事件
        onSelectError: function (file, errorCode, errorMsg) {
            if (errorCode == -110) {
                //重写图片过大提示
                this.queueData.errorMsg = "文件太大了，请选择5M以下大小的文件";
            }
        },
        //上传成功事件
        onUploadSuccess: function (file, data, response) {

            if (data == "exceedmaxnum") {
                alert("超出最大上传文件数量。");
                $("#uploadify").uploadify('cancel', '*');
                $("#uploadify").uploadify('stop');
                return;
            }

            if (data.indexOf(',') > -1) {
                var filepath = data.split(',')[0];
                var filename = data.split(',')[1];
                // var fileid = data.split(',')[2];

                // $("#download_file_href").attr("href", filepath);
                $("#download_file_href").html(filename);
                file_hetong.cont_show_file.show();
                $("#hidFileName").val(filename);
            }

            return;
        },
        //上传异常事件
        onUploadError: function (file, errorCode, errorMsg, errorString) {
            if (errorString != 'Cancelled') {
                alert("上传错误，请稍候重试");
            }
        }
    });

    file_hetong.initData();
    
</script>