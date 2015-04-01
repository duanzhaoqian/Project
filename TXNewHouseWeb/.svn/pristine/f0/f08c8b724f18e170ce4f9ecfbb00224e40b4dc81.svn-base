<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVM_NH_Developer>" %>

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
            <a href="<%=Url.SiteUrl().DevelopersAccount_Handle(Model.Id) %>">基本资料</a> <a href="<%=Url.SiteUrl().HeadPictureHandle(Model.Id) %>">
                头像</a> <a href="<%=Url.SiteUrl().PasswordHandle(Model.Id) %>">密码</a> <a class="active">
                    身份认证</a>
        </div>
        <div class="outer">
            <%using (Html.BeginForm("SubmitAuthentication", "DevelopersAccount", FormMethod.Post, new { Id = "HandleForm", Name = "HandleForm" }))
              {%>
            <% var pvsModel = ViewData["PVS_NH_Developer"] as TXModel.AdminPVM.PVS_NH_Developer; %>
            <div class="tab1">
                <table width="100%" border="0" class="tb1" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr class="" style="border-bottom: 1px solid #A8A5A5;">
                            <th width="13%" style="border: 1px solid #A8A5A5; text-align: center;">
                                公司资料
                            </th>
                            <td width="87%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>用户名：
                            </th>
                            <td>
                                <%=Model.LoginName %>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2">*</span>公司类型：
                            </th>
                            <td>
                                <%=Html.DropDownListFor(model => model.Type,pvsModel.CompanyTypeList , new { @class = "w100" })%>
                                <span id="DevelopersAccount_Type" style="color: Red" class="ie_valign_5"></span>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2">*</span>公司名称：
                            </th>
                            <td>
                                <input class="inp" id="Name" name="Name" type="text" value="<%=Model.Name %>" />
                                <span id="DevelopersAccount_Name" style="color: Red" class="ie_valign_5"></span>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2">*</span>所在城市：
                            </th>
                            <td>
                                <em>
                                    <%=Html.DropDownListFor(model => Model.ProvinceId, pvsModel.Provinces, new { @class = "w100", id = "ProvinceId" })%>
                                </em><em>
                                    <%=Html.DropDownListFor(model => Model.CityId, pvsModel.Cities, new { @class = "w100", id = "CityId" })%>
                                </em><em>
                                    <%=Html.DropDownListFor(model => Model.DistrictId, pvsModel.Districts, new { @class = "w100", id = "DistrictId" })%>
                                </em>
                                <input type="hidden" value="" name="ProvinceName" id="ProvinceName" />
                                <input type="hidden" value="" name="CityName" id="CityName" />
                                <input type="hidden" value="" name="DistrictName" id="DistrictName" />
                                <span id="DevelopersAccount_CityId" style="color: Red" class="ie_valign_5"></span>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2">*</span>公司地址：
                            </th>
                            <td>
                                <input class="inp" id="Address" name="Address" type="text" value="<%=Model.Address %>" />
                                <span id="DevelopersAccount_Address" style="color: Red" class="ie_valign_5"></span>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2">*</span>公司营业执照复印件：
                            </th>
                            <td>
                                <%Html.RenderAction("UploadAuthenticationPhoto", "DevelopersAccount", new { guid = Model.InnerCode, picturetype = TXCommons.PictureModular.UserPictureType.Identification.ToString(), cityId = Model.CityId }); %>
                                <span id="DevelopersAccount_Pic" style="color: Red" class="ie_valign_5"></span>
                            </td>
                        </tr>
                        <tr id="hetong" style="<%= Model.Type == 2 ? "" : "display: none;"  %>">
                            <th class="left">
                                <span class="red mr2">*</span>委托代理协议：
                            </th>
                            <td>
                                <%Html.RenderAction("UploadAuthenticationFile", "DevelopersAccount", new { guid = Model.InnerCode, filetype = TXCommons.PictureModular.DocumentType.PACT.ToString(), cityId = Model.CityId }); %>
                                <span id="DevelopersAccount_File" style="color: Red" class="ie_valign_5"></span>
                            </td>
                        </tr>
                        <tr class="" style="border-bottom: 1px solid #A8A5A5;">
                            <th width="13%" style="border: 1px solid #A8A5A5; text-align: center;">
                                联系人资料
                            </th>
                            <td width="87%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>姓名：
                            </th>
                            <td>
                                <input class="inp" id="RealName" name="RealName" type="text" value="<%=Model.NHDeveloperIdentity.UserName %>" />
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>手机号：
                            </th>
                            <td>
                                <input class="inp" id="Mobile" name="Mobile" type="text" value="<%=Model.NHDeveloperIdentity.UserMobile %>" />
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>邮箱：
                            </th>
                            <td>
                                <input class="inp" id="Email" name="Email" type="text" value="<%=Model.NHDeveloperIdentity.UserEmail %>" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="btnDiv tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            &nbsp;
                        </th>
                        <td width="86%">
                            <%=Html.HiddenFor(model => Model.Id)%>
                            <input type="hidden" value="<%=ViewData["backurl"] %>" name="backurl" id="backurl" />
                            <input type="button" name="btn_update" id="btn_update" value="提交" onclick="DevelopersAccount.valid_Form()"
                                class="btn2 mr10" />
                        </td>
                    </tr>
                </table>
            </div>
            <%} 
            %>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
        $(function () {
            var selectProvinces = $("#ProvinceId");
            var selectCities = $("#CityId");
            var selectDistricts = $("#DistrictId");

            $(selectProvinces).change(function () {
                $("#ProvinceName").val(this.options[this.selectedIndex].text);
                clearListItems(selectCities);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });
            $(selectCities).change(function () {
                $("#CityName").val(this.options[this.selectedIndex].text);
                clearListItems(selectDistricts);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("districts") %>?geographyCode=' + this.value, selectDistricts);
            });
            $(selectDistricts).change(function () {
                $("#DistrictName").val(this.options[this.selectedIndex].text);
            });
            $("#Type").bind("change", function () {
                if ($(this).val() == "2") {
                    $("#hetong").removeAttr("style");
                    return;
                }
                $("#hetong").attr("style","display: none;");
            });
        });

        //页面元素验证
        var DevelopersAccount = {
            valid_Type: function () {
                var types = $("#Type");
                if (types.val() == 0) {
                    $("#DevelopersAccount_Type").html("请选择公司类型。");
                    return false;
                }
                $("#DevelopersAccount_Type").html("");
                return true;
            },
            valid_Name: function () {
                var names = $.trim($("#Name").val());
                if (names == "") {
                    $("#DevelopersAccount_Name").html("请填写公司名称。");
                    return false;
                }
                $("#DevelopersAccount_Name").html("");
                return true;
            },
            valid_CityId: function () {
                var cityId = $("#CityId");
                if (cityId.val() == 0) {
                    $("#DevelopersAccount_CityId").html("请选择省市。");
                    return false;
                }

                cityId = $("#DistrictId");
                if (cityId.val() == 0) {
                    $("#DevelopersAccount_CityId").html("请选择省市。");
                    return false;
                }

                $("#DevelopersAccount_CityId").html("");
                return true;
            },
            valid_Address: function () {
                var address = $.trim($("#Address").val());
                if (address == "") {
                    $("#DevelopersAccount_Address").html("请填写公司地址。");
                    return false;
                }
                $("#DevelopersAccount_Address").html("");
                return true;
            },
            valid_Mobile: function () {
                var mobile = $.trim($("#Mobile").val());
                if (mobile == "") {
                    $("#DevelopersAccount_Mobile").html("请填写。");
                    return false;
                }
                $("#DevelopersAccount_Mobile").html("");
                return true;
            },
            valid_Img: function () {
                var mobile = $.trim($("#hidImgName").val());
                if (mobile == "") {
                    alert("请上传图片！");
                    return false;
                }

                var result = {};
                $.ajax({
                    url: '<%=Auxiliary.Instance.NhManagerUrl %>developersaccount/getauthenticationphotocount.html',
                    data: { guid: '<%=Model.InnerCode %>', picturetype: '<%=TXCommons.PictureModular.UserPictureType.Identification.ToString() %>', cityId: '<%=Model.CityId %>', ram: Math.random },
                    //data: { name: $("#Name").val(), buildingid: $("#BuildingId").val(), ram: Math.random },
                    type: 'POST',
                    cache: false,
                    async: false,
                    success: function (msg) {
                        result = msg;
                    }
                });

                if (!result.suc) {
                    alert("请上传图片！");
                    return false;
                }

                return true;
            },

            valid_File: function () {
                
                if ("2" != $("#Type").val()) {
                    return true;
                }

                // 类型为代理商时进行验证
                var fileid = $.trim($("#hidFileName").val());
                if ("" == fileid) {
                    alert("请上传委托代理协议！");
                    return false;
                }

                var result = {};
                $.ajax({
                    url: '<%=Auxiliary.Instance.NhManagerUrl %>developersaccount/getauthenticationfilecount.html',
                    data: { guid: '<%=Model.InnerCode %>', filetype: '<%=TXCommons.PictureModular.DocumentType.PACT.ToString() %>', cityId: '<%=Model.CityId %>', ram: Math.random },
                    //data: { name: $("#Name").val(), buildingid: $("#BuildingId").val(), ram: Math.random },
                    type: 'POST',
                    cache: false,
                    async: false,
                    success: function (msg) {
                        result = msg;
                    }
                });

                if (!result.suc) {
                    alert("请上传委托代理协议！");
                    return false;
                }

                return true;
            },

            valid_Form: function () {
                if (!(DevelopersAccount.valid_Img() & DevelopersAccount.valid_Type() & DevelopersAccount.valid_Name() & DevelopersAccount.valid_CityId() & DevelopersAccount.valid_Address() & DevelopersAccount.valid_File())) {
                    return;
                }

                $("#ProvinceName").val($("#ProvinceId").find("option:selected").text());
                $("#CityName").val($("#CityId").find("option:selected").text());
                $("#DistrictName").val($("#DistrictId").find("option:selected").text());

                $("#HandleForm").submit();
            }

        };
    </script>
</asp:Content>