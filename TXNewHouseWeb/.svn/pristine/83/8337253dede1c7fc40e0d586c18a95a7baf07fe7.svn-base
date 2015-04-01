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
            <%= AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%= AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%= AdminPageInfo.ItemName %></a><i>&gt;</i> <a href="javascript:void(0);">修改开发商资料</a></span>
    </div>
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="topCard" style="height: 30px;">
            <a class="active">基本资料</a> <a href="<%= Url.SiteUrl().HeadPictureHandle(Model.Id) %>">
                头像</a> <a href="<%= Url.SiteUrl().PasswordHandle(Model.Id) %>">密码</a> <a href="<%= Url.SiteUrl().AuthenticationHandle(Model.Id) %>">
                    身份认证</a>
        </div>
        <div class="outer">
            <% using (Html.BeginForm("SubmitHandle", "DevelopersAccount", FormMethod.Post, new {Id = "HandleForm", Name = "HandleForm"}))
               { %>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            登录名：
                        </th>
                        <td width="86%">
                            <label id="LoginName">
                                <%= Model.LoginName %></label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            注册时间：
                        </th>
                        <td>
                            <label id="CreateTime">
                                <%= Model.CreateTime %></label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            电子邮箱：
                        </th>
                        <td>
                            <input class="inp" id="Email" name="Email" type="text" value="<%= Model.NHDeveloper.Email %>" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 手机号码：
                        </th>
                        <td>
                            <input class="inp" id="TxtMobile" name="Mobile" type="text" value="<%= Model.NHDeveloper.Mobile %>" />
                            <span id="DevelopersAccount_Mobile" style="color: Red" class="ie_valign_5 no"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            固定电话：
                        </th>
                        <td>
                            <input class="inp" id="Tel" name="Telephone" type="text" value="<%= Model.NHDeveloper.Telephone %>" /><span id="DevelopersAccount_Telephone" style="color: Red" class="ie_valign_5 no"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备用联系电话：
                        </th>
                        <td>
                            <input class="inp" id="BackupTel" name="SpareTelephone" type="text" value="<%= Model.NHDeveloper.SpareTelephone %>" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            所在城市：
                        </th>
                        <td>
                            <em>
                                <%
                                    //var pvsProvince = ViewData["Provinces"] as List<SelectListItem>;
                                    //var pvsCity = ViewData["Cities"] as List<SelectListItem>;
                                    var pvsModel = ViewData["PVS_NH_Developer"] as TXModel.AdminPVM.PVS_NH_Developer;
                                %>
                                <%= Html.DropDownListFor(model => Model.ProvinceId, pvsModel.Provinces, new {@class = "w100", id = "ProvinceId"}) %>
                            </em><em>
                                <%= Html.DropDownListFor(model => Model.CityId, pvsModel.Cities, new {@class = "w100", id = "CityId"}) %></em>
                            <input type="hidden" value="<%= Model.ProvinceName %>" name="ProvinceName" id="ProvinceName" />
                            <input type="hidden" value="<%= Model.CityName %>" name="CityName" id="CityName" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnDiv tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            &nbsp;
                        </th>
                        <td width="86%">
                            <%= Html.HiddenFor(model => Model.Id) %>
                            <input type="hidden" value="<%= ViewData["backurl"] %>" name="backurl" id="backurl" />
                            <input type="button" name="btn_update" id="btn_update" value="保存" onclick="DevelopersAccount.valid_Form()"
                                class="btn2 mr10" />
                            <%--<input type="button" name="btn_cancle" id="btn_cancle" value="取消" onclick="document.location.href='<%=ViewData["backurl"] %>';"
                                class="btn2 mr10" />--%>
                        </td>
                    </tr>
                </table>
            </div>
            <% }
            %>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
        $(function () {
            var selectProvinces = $("#ProvinceId");
            var selectCities = $("#CityId");

            $(selectProvinces).change(function () {
                $("#ProvinceName").val(this.options[this.selectedIndex].text);
                clearListItems(selectCities);
                loadGeographyItems('<%= Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });
            $(selectCities).change(function () {
                $("#CityName").val(this.options[this.selectedIndex].text);
            });

        });

        //手机号码验证
        var DevelopersAccount = {
            valid_Mobile: function () {
                var mobile = $.trim($("#TxtMobile").val());
                if (mobile == "") {
                    $("#DevelopersAccount_Mobile").html("请填写。");
                    return false;
                }
                $("#DevelopersAccount_Mobile").html("");
                return true;
            },

            valid_Form: function () {
                if (!DevelopersAccount.valid_Mobile()
                | !DevelopersAccount.valid_Telephone()) {
                    return;
                }



                if (!(DevelopersAccount.valid_Mobile()
                    && DevelopersAccount.valid_Telephone()
                )) {
                    return;
                }

                $("#ProvinceName").val($("#ProvinceId").find("option:selected").text());
                $("#CityName").val($("#CityId").find("option:selected").text());

                $("#HandleForm").submit();
            },

            valid_Telephone: function () {
                var tele = $.trim($("#Tel").val());
                if (tele == "") {
                    return true;
                }

                var reg = new RegExp("^[0-9\-()（）]{7,18}$");
                if (!reg.test(tele)) {
                    $("#DevelopersAccount_Telephone").html("请输入正确的电话格式，如：010-88888888-001 区号、分机可不填");
                    return false;
                }

                $("#DevelopersAccount_Telephone").html("");
                return true;
            }
        };
    </script>
</asp:Content>