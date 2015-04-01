<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXModel.Dev.CT_DeveAndIdenInfo>" %>

<%@ Import Namespace="TXCommons.PictureModular" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-身份认证
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        if (Model.State == 1 || Model.State == 3)
        {
            //用户图片集合
            UserPictureInfo picture = ViewData["picList"] as UserPictureInfo;
            DocumentInfo document = ViewData["filelist"] as DocumentInfo;
    %>
    <!--已认证-->
    <div class="content">
        <% 
    if (Model.State == 3)
    {
        %>
        <div class="yellow_box mb10">
            <p class="font12 mt10 mb10">
                提交审核成功，请您耐心等待。我们将在1-2个工作日完成。请您关注审核结果。</p>
        </div>
        <%
        }
        %>
        <h4 class="title_h4 mb10">
            <span>公司资料</span></h4>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table_box1">
            <tr>
                <td align="right" width="150">
                    用户名：
                </td>
                <td>
                    <%=Model.LoginName%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    公司类型：
                </td>
                <td>
                    <%=Model.Type == 1 ? "开发商" : "代理商"%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    公司名称：
                </td>
                <td>
                    <%=Model.CompanyName %>
                </td>
            </tr>
            <tr>
                <td align="right">
                    所在城市：
                </td>
                <td>
                    <%=Model.ProvinceName_Iden%>
                    <%=Model.CityName_Iden%>
                    <%=Model.DName%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    公司地址：
                </td>
                <td>
                    <%=Model.CompanyAddress%>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <p class="mt5">
                        公司营业执照复印件：</p>
                </td>
                <td>
                    <span class="img">
                        <img src="<%=picture == null ? String.Concat(TXCommons.GetConfig.ImgUrl, "images/card.png") : String.Concat(picture.Path, ".180_130.jpg")%>" /></span>
                </td>
            </tr>
            <tr<%= Model.Type==2 ? "" : " style=\"display: none;\"" %>>
                <td align="right" valign="top"><p class="mt5"> 委托代理协议：</p></td>
                <td><%= document == null ? "" : document.FileName %></td>
            </tr>
        </table>
        <h4 class="title_h4 mb10">
            <span>联系人资料</span></h4>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table_box1">
            <tr>
                <td align="right" width="150">
                    姓名：
                </td>
                <td>
                    <%=Model.UserName%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    手机号：
                </td>
                <td>
                    <%=Model.UserMobile%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    邮箱：
                </td>
                <td>
                    <%=Model.UserEmail%>
                </td>
            </tr>
        </table>
    </div>
    <%
}
else
{ 
    %>
    <!--未认证-->
    <div class="content">
        <form id="frmIdentification" action="<%=TXCommons.GetConfig.BaseUrl%>home/doidentification" method="post">
        <% 
    if (Model.State == 2)
    {
        %>
        <div class="yellow_box mb10">
            <p class="font12 mt10 mb10">
                未通过原因：<%=Model.Refuse%></p>
        </div>
        <%
            }
        %>
        <h4 class="title_h4 mb10">
            <span>公司资料</span><em class="ts_right">带<i class="red">*</i>号为必填项目</em></h4>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table_box1">
            <tr>
                <td align="right" width="150">
                    <span class="orange">*</span> 用户名：
                </td>
                <td>
                    <%=Model.LoginName%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="orange">*</span> 公司类型：
                </td>
                <td>
                    <select id="selType" name="selType" class="select1 w180">
                        <option value="">选择类型</option>
                        <option value="1">开发商</option>
                        <option value="2">代理商</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="orange">*</span>公司名称：
                </td>
                <td>
                    <input id="txtCompanyName" name="txtCompanyName" type="text" class="input_wauto w300" value="<%=Model.CompanyName%>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="orange">*</span>所在城市：
                </td>
                <td>
                    <select id="selProvince" name="selProvince" class="select1 w100 mr10">
                        <option value="">选择省份</option>
                    </select>
                    <select id="selCity" name="selCity" class="select1 w100 mr10">
                        <option value="">选择城市</option>
                    </select>
                    <select id="selDistrict" name="selDistrict" class="select1 w100 mr10">
                        <option value="">选择区域</option>
                    </select>
                    <input id="hidProvince" name="hidProvince" type="hidden" value="<%=Model.ProvinceName_Iden%>" />
                    <input id="hidCity" name="hidCity" type="hidden" value="<%=Model.CityName_Iden%>" />
                    <input id="hidDistrict" name="hidDistrict" type="hidden" value="<%=Model.DName%>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="orange">*</span> 公司地址：
                </td>
                <td>
                    <input id="txtCompanyAddress" name="txtCompanyAddress" type="text" class="input_wauto w300" value="<%=Model.CompanyAddress%>" maxlength="100" />
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <p class="mt5">
                        <span class="orange">*</span> 公司营业执照复印件：</p>
                </td>
                <td>
                    <%Html.RenderPartial("UploadAuthenticationPhoto");%>
                </td>
            </tr>
            <tr id="tr_hetong">
                <td align="right" valign="top">
                    <p class="mt5">
                        <span class="orange">*</span> 委托代理协议：</p>
                </td>
                <td>
                    <%Html.RenderAction("UploadAuthenticationHeTong", "Home", new { guid = Model.InnerCode, filetype = DocumentType.PACT.ToString(), cityId = Model.CityId });%>
                </td>
            </tr>
        </table>
        <h4 class="title_h4 mb10">
            <span>联系人资料</span></h4>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table_box1">
            <tr>
                <td align="right" width="150">
                    <span class="orange">*</span> 姓名：
                </td>
                <td>
                    <input id="txtUserName" name="txtUserName" type="text" class="input_wauto w200" value="<%=Model.UserName%>" maxlength="20" />
                </td>
            </tr>
            <tr>
                <td align="right" width="150">
                    <span class="orange">*</span> 手机号：
                </td>
                <td>
                    <input id="txtUserMobile" name="txtUserMobile" type="text" class="input_wauto w200" value="<%=Model.UserMobile%>" maxlength="20" />
                </td>
            </tr>
            <tr>
                <td align="right" width="150">
                    <span class="orange">*</span> 邮箱：
                </td>
                <td>
                    <input id="txtUserEmail" name="txtUserEmail" type="text" class="input_wauto w200" value="<%=Model.UserEmail%>" maxlength="30" />
                </td>
            </tr>
            <tr>
                <td align="right" width="150">
                    &nbsp;
                </td>
                <td>
                    <input type="submit" value="提交" class="btn_w97_green" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jqRegExpMothod.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //网站域名
            var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
            var colProvince = $("#selProvince");
            var colCity = $("#selCity");
            var colDistrict = $("#selDistrict");
            var colProvinceName = $("#hidProvince");
            var colCityName = $("#hidCity");
            var colDistrictName = $("#hidDistrict");

            $("#selType").bind("change", function () {
                if ($(this).val() == "2") {
                    $("#tr_hetong").show();
                } else {
                    $("#tr_hetong").hide();
                }
            });

            function show_hide_hetong() {
                if ($("#selType").val() == "2") {
                    $("#tr_hetong").show();
                } else {
                    $("#tr_hetong").hide();
                }
            };

            //获得省份信息
            function getProvince(ctid, pid) {
                ///<summary>获得省份信息</summary>
                ///<param name="ctid">国籍Id：默认1(中国)</param>
                ///<param name="pid">选中的省份Id：默认0</param>
                //异步查询数据
                $.ajax({
                    type: "post",
                    url: mainUrl + "premises/searchprovincelist",
                    data: { countryid: ctid },
                    success: function (data) {
                        $.each(data, function (i, item) {
                            var temp = pid == item.Id ? "selected=\"selected\"" : "";
                            $("<option " + temp + "></option>").val(item.Id).text(item.Name).appendTo(colProvince);
                        });
                    }
                });
            }
            //获得城市信息
            function getCity(pid, cid) {
                ///<summary>获得城市信息</summary>
                ///<param name="pid">省份Id</param>
                ///<param name="cid">选中的城市Id：默认0</param>
                //清空城市
                colCity.empty();
                $("<option></option>").val("").text("选择城市").appendTo(colCity);
                //清空区域
                colDistrict.empty();
                $("<option></option>").val("").text("选择区域").appendTo(colDistrict);
                //异步查询数据
                $.ajax({
                    type: "post",
                    url: mainUrl + "premises/searchcitylist",
                    data: { ProvinceId: pid },
                    success: function (data) {
                        $.each(data, function (i, item) {
                            var temp = cid == item.Id ? "selected=\"selected\"" : "";
                            $("<option " + temp + "></option>").val(item.Id).text(item.Name).appendTo(colCity);
                        });
                    }
                });
            }
            //区域信息
            function getDistrict(cid, did) {
                ///<summary>获得城市信息</summary>
                ///<param name="cid">城市Id</param>
                ///<param name="did">选中的区域Id：默认0</param>
                //清空区域
                colDistrict.empty();
                $("<option></option>").val("").text("选择区域").appendTo(colDistrict);
                //异步查询数据
                $.ajax({
                    type: "post",
                    url: mainUrl + "premises/searchdidlist",
                    data: { CityId: cid },
                    success: function (data) {
                        $.each(data, function (i, item) {
                            var temp = did == item.Id ? "selected=\"selected\"" : "";
                            $("<option " + temp + "></option>").val(item.Id).text(item.Name).appendTo(colDistrict);
                        });
                    }
                });
            }

            //初始化
            $(colProvince).change(function () {
                getCity($(this).val(), 0); //加载城市
                colProvinceName.val($(this).find("option:selected").text());
            });
            $(colCity).change(function () {
                getDistrict($(this).val(), 0); //加载区域
                colCityName.val($(this).find("option:selected").text());
            });
            $(colDistrict).change(function () {
                colDistrictName.val($(this).find("option:selected").text())
            });
            //如未通过认证，则为区域赋值
            if ("<%=Model.State%>" == "2" && parseInt("<%=Model.DId%>") > 0) {
                //设置类型
                $("#selType").val("<%=Model.Type%>");
                getProvince(1, "<%=Model.ProvinceId_Iden%>"); //加载省份
                getCity("<%=Model.ProvinceId_Iden%>", "<%=Model.CityId_Iden%>"); //加载城市
                getDistrict("<%=Model.CityId_Iden%>", "<%=Model.DId%>"); //加载区域
            } else {
                getProvince(1, 0); //加载省份
            }

            //验证
            $("#frmIdentification").validate({
                errorClass: "ie_valign_5 no",       //设置错误提示css
                errorElement: "span",               //设置错误提示容器
                //验证通过事件
                submitHandler: function (form) {
                    var result = $("#hidImgName").val();
                    if (result.length == 0) {
                        $("#errImg").show();
                        return false;
                    }

                    result = $("#hidFileName").val();
                    if (result.length == 0 && ($("#selType").val() == 2)) {
                        alert("请上传委托代理协议");
                        return false;
                    }

                    form.submit();
                },
                rules: {
                    selType: {
                        required: true
                    },
                    txtCompanyName: {
                        required: true,
                        maxlength:50,
                        nameaddress: true
                    },
                    selDistrict: {
                        required: true
                    },
                    txtCompanyAddress: {
                        required: true,
                        nameaddress: true
                    },
                    txtUserName: {
                        required: true,
                        realname: true
                    },
                    txtUserMobile: {
                        required: true,
                        mobile: true
                    },
                    txtUserEmail: {
                        required: true,
                        email: true
                    }
                },
                messages: {
                    selType: {
                        required: "请选择公司类型"
                    },
                    txtCompanyName: {
                        required: "请输入公司名称",
                        maxlength: "最多可输入50个汉字",
                        nameaddress: "只能由中文、英文、数字及'_'、'-'、()、（）、#组成"
                    },
                    selDistrict: {
                        required: "请选择所在地"
                    },
                    txtCompanyAddress: {
                        required: "请输入公司地址",
                        nameaddress: "只能由中文、英文、数字及'_'、'-'、()、（）、#组成"
                    },
                    txtUserName: {
                        required: "请输入联系人姓名",
                        realname: "只能由全中文或全英文组成"
                    },
                    txtUserMobile: {
                        required: "请输入联系人手机号",
                        mobile: "请输入正确的手机格式"
                    },
                    txtUserEmail: {
                        required: "请输入联系人电子邮件",
                        email: "请输入正确的邮件格式"
                    }
                }
            });

            show_hide_hetong();
        });
    </script>
    <%
}
    %>
</asp:Content>
