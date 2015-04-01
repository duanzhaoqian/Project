<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVM_NH_Developer>" %>

<%@ Import Namespace="TXCommons.PictureModular" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .topCard a
        {
            display: block;
            float: left;
            font-size: 14px;
            height: 30px;
            line-height: 30px;
            text-align: center;
            width: 120px;
            border-right: solid 1px #ECEDEA;
        }
        .topCard .active
        {
            background-color: #71A814;
            color: #ffffff;
        }
    </style>
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a><i>&gt;</i> <a href="javascript:void(0);">修改开发商资料</a></span>
    </div>
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="topCard" style="height: 30px;">
            <a href="<%=Url.SiteUrl().DevelopersAccount_Handle(Model.Id) %>">基本资料</a> <a class="active">
                头像</a> <a href="<%=Url.SiteUrl().PasswordHandle(Model.Id) %>">密码</a> <a href="<%=Url.SiteUrl().AuthenticationHandle(Model.Id) %>">
                    身份认证</a>
        </div>
        <%--  <div class="outer">
          <div class="tab1">
            <table width="100%" border="0" class="tb1" cellspacing="0" cellpadding="0">
              <tbody>
              <tr class="">
                <th class="left"><img id="img1" src="<%=picture80 == null ? TXCommons.GetConfig.ImgUrl + "images/photo_96.jpg" : picture80.Path%>" width="80px" height="80px"></th>
                <td><%=Model.Address %></td>
              </tr>
              <tr class="">
                <th class="left"><span class="red mr2">*</span>公司营业执照复印件：</th>
                <td><img src="http://img.kuaiyoujia.net/user/98/3c/b2/983cb2fd-a3f9-4d2a-8d52-8d2e93133f73/do3i4u.jpg.130_100.jpg" width="180" height="130"></td>
              </tr>
              <tr class="" style=" border-bottom:1px solid #A8A5A5;">
                <th width="13%" style=" border:1px solid #A8A5A5; text-align:center;">联系人资料</th>
                <td width="87%">&nbsp;</td>
              </tr>
              <tr class="">
                <th class="left"><span class="red mr2"></span>真实姓名：</th>
                <td><%=Model.RealName %></td>
              </tr>
              <tr class="">
                <th class="left"><span class="red mr2"></span>手机号：</th>
                <td><%=Model.Mobile %></td>
              </tr>
              <tr class="">
                <th class="left"><span class="red mr2"></span>邮箱：</th>
                <td><%=Model.Email %></td>
              </tr>
            </tbody>
            </table>
          </div>
          <div class="btnDiv tab1">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
              <tbody><tr class="">
                <th width="14%">&nbsp;</th>
                <td width="86%">
                    
                  <input type="button" name="button6" id="IsOk" value="通过" class="btn3" />
                  <input type="button" name="button6" id="IsNo" value="不通过" class="btn3" />
                  
                </td>
              </tr>
            </tbody></table>
          </div>
        </div>--%>
        <!-- InstanceEndEditable -->
        <% 
            //用户图片集合
            UserPictureInfo picture80 = ViewData["picList"] as UserPictureInfo;//80px图片
        %>
        <div class="content">
            <div class="bg_fafafa">
                <div class="upload_photo clear">
                    <div style="margin-left: 300px; margin-top: 30px;">
                        <div class="left" style="margin: 20px;">
                            <img id="imgShow" src="<%=picture80 == null ? TXCommons.GetConfig.ImgUrl + "images/photo_96.jpg" : picture80.Path + ".80_80.jpg"%>"
                                width="80px" height="80px">
                        </div>
                        <div class="left">
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
                            <p class="mt10 mb20">
                                <span class="font12 col333">切换到<a href="<%=Url.SiteUrl().UpdatePhotoExpert(Model.Id)%>"
                                    class="ml10">高级上传模式</a></span></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <link href="<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.css"%>" rel="stylesheet"
        type="text/css" />
    <script src="<%= Auxiliary.Instance.NhManagerUrl +"js/uploadify/jquery.uploadify.js" %>"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //设定参数
            var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
            var uploadImgName = "<%=picture80 == null ? String.Empty : picture80.FileName%>"; //检查是否已上传图片
            var guid = '<%=ViewData["GUID"]%>';
            var pictype = '<%=ViewData["pictureType"]%>';
            var cid = '<%=ViewData["cityId"]%>';
            //初始化上传图片插件
            $("#fileUpload").uploadify({
                //基本参数
                swf: '<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.swf" %>',
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
                },
                //上传成功事件
                onUploadSuccess: function (file, data, response) {
                    //******
                    //显示图片操作（王亮添加）
                    //******

                    var datad = data.split(',');
                    $("#imgShow").attr("src", datad[0] + ".80_80.jpg"); //设置图片路径并显示
                    uploadImgName = datad[1]; //设置图片名称
                },
                //上传异常事件
                onUploadError: function (file, errorCode, errorMsg, errorString) {
                    alert("上传错误，请稍候重试");
                }
            });


            $("#btnSave").bind("click", function () {
                var filename = uploadImgName;
                if (filename) {
                    var url = "<%=TXCommons.GetConfig.PremisesImgUploadBaseUrl%>user/SaveCommon.ashx?para=<%=Model.InnerCode%>^" + filename;
                    $.ajax({
                        url: url,
                        type: "get",
                        dataType: "jsonp",
                        jsonp: "callbackparam",
                        jsonpCallback: "success_jsonpCallback",
                        success: function (data) {
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