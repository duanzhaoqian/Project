<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_Building>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a>
            <%=(String.IsNullOrEmpty(Convert.ToString(ViewData["premisesName"])) 
            ? string.Empty 
            : string.Format("<i>&gt;</i> <a href=\"#\">{0}</a>",ViewData["premisesName"])) %>
            <%=(String.IsNullOrEmpty(Convert.ToString(ViewData["buildingName"])) 
            ? string.Empty
            : string.Format("<i>&gt;</i> <a href=\"#\">{0}</a>", ViewData["buildingName"]))%>&nbsp;&nbsp;编辑</span>
    </div>
    <!--//current-->
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="outer">
            <div class="dispose">
                <h5>
                    楼栋信息</h5>
                <span><em class="red">*</em> 为必填项</span></div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="150">
                            <em class="red">*</em> 楼栋名称：
                        </th>
                        <td>
                            <%= Html.TextBoxFor(model=>model.Name, new {@maxlength=20, @style="width: 200px;"}) %><span
                                id="namelength" style="color: #999999; padding-left: 10px;"></span><span id="err_name"></span>&nbsp;&nbsp;<%= Html.DropDownListFor(model=>model.NameType, Model.NameTypes, new { @class = "w100" }) %><span
                                    id="err_nametype"></span><span style="color: #999999">（例如：1号楼,填写"1"）</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            楼盘期数：
                        </th>
                        <td>
                            <input type="text" id="periods" maxlength="1" value="<%= Model.Periods == 0 ? "" : Convert.ToString(Model.Periods) %>" />&nbsp;期<span
                                id="err_periods"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 单元数量：
                        </th>
                        <td>
                            <input type="text" id="unitcount" maxlength="1" value="<%= Model.UnitCount %>" /><span
                                id="err_unitcount"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 单元名称：
                        </th>
                        <td>
                            <div id="unitnamelist">
                                <% for (int i = 0; i < Model.UnitCount; i++)
                                   { %>
                                <span style="width: 120px; float: left;">
                                    <%= (i + 1) %>=<input type="text" id="unitname_<%= (i + 1) %>" placeholder="例如：<%= (i + 1) %>单元"
                                        value="<%= Model.UnitNameList[i].Value %>" onkeyup="building.clearErrStyle('err_unitnamelist')"
                                        style="width: 80px;" maxlength="5" /></span>
                                <% } %>
                            </div>
                            <span id="err_unitnamelist"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 物业类型：
                        </th>
                        <td>
                            <% for (int i = 0; i < Model.PropertyTypes.Count; i++)
                               {
                            %><input type="checkbox" id="pt_<%= Model.PropertyTypes[i].Key %>" value="<%= Model.PropertyTypes[i].Key %>"
                                <%= Model.PropertyType.Contains("," + Model.PropertyTypes[i].Key + ",") ? " checked=\"checked\"" : string.Empty %>
                                onclick="building.clearErrStyle('err_propertytype')" />&nbsp;<%= Model.PropertyTypes[i].Value %>&nbsp;&nbsp;
                            <% } %><span id="err_propertytype"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 开盘时间：
                        </th>
                        <td>
                            <input type="text" id="openingtime" value="<%= Model.OpeningTime %>" /><span id="err_openingtime"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 入住时间：
                        </th>
                        <td>
                            <input type="text" id="otherstime" value="<%= Model.OthersTime %>" /><span id="err_otherstime"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 户数：
                        </th>
                        <td>
                            <input type="text" id="familynum" value="<%= Model.FamilyNum %>" />&nbsp;&nbsp;户<span
                                id="err_familynum"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 地上楼层：
                        </th>
                        <td>
                            <input type="text" id="thefloor" value="<%= Model.TheFloor %>" maxlength="3" />&nbsp;&nbsp;层<span
                                id="err_thefloor"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 地下楼层：
                        </th>
                        <td>
                            <input type="text" id="underloor" value="<%= Model.Underloor %>" maxlength="3" />&nbsp;&nbsp;层<span
                                id="err_underloor"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 梯户配比：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model=>model.Ladder, Model.Ladders, new { @class = "w100" }) %><span
                                id="err_ladder"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 装修：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.Renovation, Model.Renovations, new { @class = "w100" })%><span
                                id="err_renovation"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 楼栋所处楼盘位置：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.BuildingPosition, Model.BuildingPositions, new { @class = "w100" })%><span
                                id="err_buildingposition"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 楼栋平面图：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.PictureData, Model.BuildingPlans, new { @class = "w100" })%><span
                                id="err_buildingplanid"></span>&nbsp;&nbsp;
                            <input type="button" id="btnUpdateBuildingPlanList" value="更新楼栋平面图" />&nbsp;&nbsp;
                            <a href="<%=Url.SiteUrl().PremisesImgs(Model.PremisesId,1)%>?tage=6" target="_blank">
                                上传楼栋平面图</a>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 状态：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.State, Model.States, new { @class = "w100" })%><span
                                id="err_state"></span>
                            <input type="hidden" id="hid_State" value="<%= Model.State %>" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 预售许可证：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.PresaleId, Model.PermitPresales, new { @class = "w100" })%>
                            <span id="err_presaleid"></span>
                            <input type="text" id="nPermitPresale" placeholder="若选项中没有请在此输入预售许可证号" style="width: 230px;"
                                maxlength="20" />
                            <input type="button" id="addpt" value="添加" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dispose">
                <h5>
                    楼栋沙盘信息关联</h5>
                <span><em class="red">*</em> 为必填项</span></div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="150">
                            <em class="red">*</em> 沙盘楼栋编号：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.PNum, Model.PNums, new { @class = "w100" })%><span
                                id="err_pnum"></span><span style="color: #999999; padding-left: 10px;">关联上之后用户即可通过楼盘首页-楼盘沙盘了解楼栋概况</span>
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
                            <input type="button" name="btn_submit" id="btn_submit" value="保存" class="btn1" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">

        $(function () {
            $("#unitcount").bind("keyup", building.createUnitNameList);
            $("#openingtime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });
            $("#otherstime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });
            $("#addpt").bind("click", building.addNewPermitPresale);
            $("#btn_submit").bind("click", building.submit);
            $("#btnUpdateBuildingPlanList").bind("click", building.updateBuildingPlanList);
            building.dispNameLength();

            $("#Name").bind("keyup", building.evtKeyUpName);
            $("#NameType").bind("change", { a: "err_nametype" }, building.clearErrStyle_bind);
            $("#periods").bind("keyup", { a: "err_periods" }, building.clearErrStyle_bind);
            $("#openingtime").bind("click", { a: "err_openingtime" }, building.clearErrStyle_bind);
            $("#otherstime").bind("click", { a: "err_otherstime" }, building.clearErrStyle_bind);
            $("#familynum").bind("keyup", { a: "err_familynum" }, building.clearErrStyle_bind);
            $("#thefloor").bind("keyup", { a: "err_thefloor" }, building.clearErrStyle_bind);
            $("#underloor").bind("keyup", { a: "err_underloor" }, building.clearErrStyle_bind);
            
            $("#Ladder").bind("change", { a: "err_ladder" }, building.clearErrStyle_bind);
            $("#Renovation").bind("change", { a: "err_renovation" }, building.clearErrStyle_bind);
            $("#BuildingPosition").bind("change", { a: "err_buildingposition" }, building.clearErrStyle_bind);
            $("#PictureData").bind("change", { a: "err_buildingplanid" }, building.clearErrStyle_bind);
            $("#State").bind("change", { a: "err_state" }, building.clearErrStyle_bind);
            $("#PresaleId").bind("change", { a: "err_presaleid" }, building.clearErrStyle_bind);
            $("#PNum").bind("change", { a: "err_pnum" }, building.clearErrStyle_bind);
        });

        var building = {
            icon_suc: "win ml10",
            icon_err: "lose ml10",
            err_content: "请填写",

            // 验证 楼栋名称
            valid_name: function() {
                var eerr = $("#err_name");
                var value = $.trim($("#Name").val());
                if ("" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }
                
                if (10 - Math.round(building.getStringLengthForByte(value) / 2) < 0) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("楼栋名称限定10个汉字以内");
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 楼盘名称后缀
            valid_nametype: function() {
                var eerr = $("#err_nametype");
                var value = $("#NameType").val();
                if ("" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 楼盘期数
            valid_periods: function() {
                var eerr = $("#err_periods");
                var value = $.trim($("#periods").val());
                if ("" == value) {
                    eerr.removeAttr("class");
                    eerr.html("");
                    return true;
                }

                var pattern = /^\d{1}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("只可输入1位正数");
                    return false;
                }
                
                if (parseInt(value)<1) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("只可输入1位正数");
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 单元数量
            valid_unitcount: function() {
                var eerr = $("#err_unitcount");
                var value = $.trim($("#unitcount").val());
                if ("" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                var pattern = /^\d{1}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("单元数量最多可输入1位正数");
                    return false;
                }

                if (parseInt(value) < 1) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("单元数量最多可输入1位正数");
                    return false;
                }

                if (8 < parseInt(value)) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("最多可输入8个");
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 单元名称列表
            valid_unitnamelist: function() {
                var eerr = $("#err_unitnamelist");
                if (!building.valid_unitcount()) {
                    return false;
                }

                var flag = true;
//                var flag2 = true;
                $("input[id^='unitname_']").each(function() {
                    if ("" == $.trim($(this).val())) {
                        flag = false;
                    }
//                    if (10 < admincoms.StringHelper.getByteCount($.trim($(this).val()))) {
//                        flag2 = false;
//                    }
                });

                if (!flag) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("请输入");
                    return false;
                }
                
//                if (!flag2) {
//                    eerr.attr("class", building.icon_err);
//                    eerr.html("不能超过5个汉字");
//                    return false;
//                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 物业类型
            valid_propertytype: function() {
                var eerr = $("#err_propertytype");
                var count = $("input[id^='pt_']:checked").length;
                if (0 == count) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 开盘时间
            valid_openingtime: function() {
                var eerr = $("#err_openingtime");
                var value = $.trim($("#openingtime").val());
                if ("" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 入住时间
            valid_otherstime: function() {
                var eerr = $("#err_otherstime");
                var value = $.trim($("#otherstime").val());
                if ("" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 户数
            valid_familynum: function() {
                var eerr = $("#err_familynum");
                var value = $.trim($("#familynum").val());
                if ("" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                var pattern = /^\d{1,16}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("请输入正确的户数");
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 地上层数
            valid_thefloor: function() {
                var eerr = $("#err_thefloor");
                var value = $.trim($("#thefloor").val());
                if ("" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                var pattern = /^\d{1,16}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("请输入正确的层数");
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 地下层数
            valid_underloor: function() {
                var eerr = $("#err_underloor");
                var value = $.trim($("#underloor").val());
                if ("" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                var pattern = /^\d{1,16}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", building.icon_err);
                    eerr.html("请输入正确的层数");
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 梯户配比
            valid_ladder: function() {
                var eerr = $("#err_ladder");
                var value = $("#Ladder").val();
                if ("0" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 装修
            valid_renovation: function() {
                var eerr = $("#err_renovation");
                var value = $("#Renovation").val();
                if ("0" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 楼栋所处楼盘位置
            valid_buildingposition: function() {
                var eerr = $("#err_buildingposition");
                var value = $("#BuildingPosition").val();
                if ("0" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 楼栋平面图
            valid_picturedata: function() {
                var eerr = $("#err_buildingplanid");
                var value = $("#PictureData").val();
                
                // TODO: 临时变动(2014-04-24 去掉约束)
//                if ("0" == value) {
//                    eerr.attr("class", building.icon_err);
//                    eerr.html(building.err_content);
//                    return false;
//                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 状态
            valid_state: function() {
                var eerr = $("#err_state");
                var value = $("#State").val();
                if ("0" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 预售许可证
            valid_presaleid: function() {
                var eerr = $("#err_presaleid");
                var value = $("#PresaleId").val();
                if ("0" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 沙盘图
            valid_pnum: function() {
                var eerr = $("#err_pnum");
                var value = $("#PNum").val();
                if ("0" == value) {
                    eerr.attr("class", building.icon_err);
                    eerr.html(building.err_content);
                    return false;
                }

                eerr.attr("class", building.icon_suc);
                eerr.html("");
                return true;
            },

            // 生成 单元名称列表
            createUnitNameList: function() {
                if (!building.valid_unitcount()) {
                    return;
                }

                var unitcount = $.trim($("#unitcount").val());
                if ("" == unitcount) {
                    $("#unitnamelist").html("");
                    return;
                }

                var pattern = /^\d{1,2}$/;
                if (!pattern.test(unitcount)) {
                    $("#unitnamelist").html("");
                    return;
                }

                var count = parseInt(unitcount);
                var html = "";
                for (var i = 0; i < count; i++) {
                    html += "<span style=\"width: 120px; float: left;\">" + (i + 1) + "=<input type=\"text\" id=\"unitname_" + (i + 1) + "\" placeholder=\"例如：" + (i + 1) + "单元\" onkeyup=\"building.clearErrStyle('err_unitnamelist')\" style=\"width: 80px;\"  maxlength=\"5\" /></span>";
                }
                $("#unitnamelist").html(html);
            },

            // 清空错误提示
            clearErrStyle: function(name) {
                $("#" + name).removeAttr("class");
                $("#" + name).html("");
            },

            // 清空错误提示(bind)
            clearErrStyle_bind: function(e) {
                building.clearErrStyle(e.data.a);
            },
            
            // 获取物业类型编号集合
            getPropertyTypeIds: function() {
                var ids = '';
                $("input[id^='pt_']:checked").each(function() {
                    ids += $(this).val() + ',';
                });

                return ids;
            },
            
            // 获取单元名称列表
            getUnitNameList: function() {
                var list = "[";
                $("input[id^='unitname_']").each(function(i) {
                    list += "{\"Key\":\"" + (i + 1) + "\", \"Value\":\"" + ($(this).val()).replace(new RegExp("\"", "g"), "ニ") + "\"},";
                });
                list += "]";
                return list;
            },
            
            // 添加新预售许可证
            addNewPermitPresale: function() {
                var pt = $.trim($("#nPermitPresale").val());
                if ("" == pt) {
                    alert("预售许可证不能是空字符串");
                    return;
                }

                $("#addpt").attr("disabled", "disabled");
                var url = '<%=Url.SiteUrl().Building_Search("AddNewPermitPresale") %>';
                var data = { PremisesId: <%= Model.PremisesId %>, name: pt };
                $.post(url, data, function(msg) {
                    // 刷新预售许可证列表
                    clearListItems($("#PresaleId"));
                    loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetPermitPresalesByPremisesId") %>?id=<%= Model.PremisesId %>', $("#PresaleId"));
                    $("#addpt").removeAttr("disabled");
                    if (msg.suc) {
                        alert("添加成功");
                        $("#nPermitPresale").val("");
                    } else {
                        alert("请勿重复添加预售许可证号");
                    }
                });
            },
            
            // 刷新楼栋平面图列表
            updateBuildingPlanList: function() {
                ManagerPlanes.GetData();
                //clearListItems($("#BuildingPlanId"));
                //loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetBuildingPlanListByPremisesId") %>?id=<%= Model.PremisesId %>', $("#BuildingPlanId"));
            },
            
            //  获取字符串的字节长度
            getStringLengthForByte: function(str) {
                return str.replace(/[^\x00-\xFF]/g,'**').length;
            },
            
            // 显示还可输入字符数量
            dispNameLength: function() {
                var length = Math.round(building.getStringLengthForByte($("#Name").val()) / 2);
                $("#namelength").html("还可输入" + ((10 - length) > 0 ? (10 - length) : 0) + "字");
            },
            
            // 输入楼栋名称事件
            evtKeyUpName: function() {
                building.clearErrStyle("err_name");
                building.dispNameLength();
            },

            // 提交
            submit: function() {
                if (!(building.valid_name()
                    & building.valid_nametype()
                    & building.valid_periods()
                    & building.valid_unitcount()
                    & building.valid_unitnamelist()
                    & building.valid_propertytype()
                    & building.valid_openingtime()
                    & building.valid_otherstime()
                    & building.valid_familynum()
                    & building.valid_thefloor()
                    & building.valid_underloor()
                    & building.valid_ladder()
                    & building.valid_renovation()
                    & building.valid_buildingposition()
                    & building.valid_picturedata()
                    & building.valid_state()
                    & building.valid_presaleid()
                    & building.valid_pnum())) {
                    return;
                }
                
                // 验证楼栋状态是否修改
                if ($("#State").val() != $("#hid_State").val()) {
                    if (!confirm("楼栋的销售状态已修改，同时房源的销售状态也会相应修改，您是否确定修改?")) {
                        return;
                    }
                }

                var url = "<%=Url.SiteUrl().Building_Search("UpdateNewBuildingDo") %>";
                var data = {
                    Id: <%= Model.Id %>,
                    PremisesId: $("#PremisesId").val(),
                    Name: $.trim($("#Name").val()),
                    NameType: $("#NameType").val(),
                    Periods: $("#periods").val() == "" ? 0 : parseInt($("#periods").val()),
                    UnitCount: $("#unitcount").val(),
                    UnitNameListString: building.getUnitNameList(),
                    PropertyType: building.getPropertyTypeIds(),
                    OpeningTime: $("#openingtime").val(),
                    OthersTime: $("#otherstime").val(),
                    FamilyNum: $("#familynum").val(),
                    TheFloor: $("#thefloor").val(),
                    Underloor: $("#underloor").val(),
                    Ladder: $("#Ladder").val(),
                    Renovation: $("#Renovation").val(),
                    BuildingPosition: $("#BuildingPosition").val(),
                    State: $("#State").val(),
                    PresaleId: $("#PresaleId").val(),
                    PermitPresale: $("#PresaleId option:selected").text(),
                    PictureData: $("#PictureData").val(),
                    PNum: $("#PNum").val()
                };

                $("#btn_submit").attr("disabled", "disabled");
                admincoms.WaittingBar.show();
                $.post(url, data, function(msg) {
                    $("#btn_submit").removeAttr("disabled");
                    admincoms.WaittingBar.close();
                    if (msg.suc) {
                        alert("更新成功");
                        window.location.href = "<%=ViewData["backurl"] %>";
                        return;
                    }
                    alert(msg.msg);
                    var eerr = $("#err_name");
                    eerr.attr("class", building.icon_err);
                    eerr.html(msg.msg);
                });
            }
        };
        //平面图
        var ManagerPlanes = {
            rid: <%= Model.PremisesId %>,
            url: "<%=Url.SiteUrl().Common("AjaxGetPlanes","NhBuilding")%>",
            GetData: function() {
                $.getJSON(this.url, { r: Math.random(), id: "<%=Model.PremisesId %>" }, function(response) {
                    if (response && response.success) {
                        $("#PictureData").html("");
                        $.each(response.items, function() {
                            $("<option value='" + this.ID + "' data='" + this.Path + "'" + (ManagerPlanes.rid==this.ID ? " selected=\"selected\"" : "") +">" + this.Title + "</option>").appendTo($("#PictureData"));
                        });
                        $("<option  value='0'>请选择</option>").prependTo($("#PictureData"));
                    } else {
                    }
                });
            }
        };
        
        //沙盘楼栋编号
        var ManagerPNum = {
            id: "<%= Model.PremisesId %>",
            rid: -1,
            pnum: '<%= Model.PNum %>',
            url: "<%= Url.SiteUrl().Common("GetSandBoxList","NhBuilding")%>",
            GetData: function() {
                $.getJSON(this.url, { r: Math.random(), id: this.id, pnum:'<%= Model.PNum %>' }, function(response) {
                    if (response && response.success) {
                        $("#PNum").html("");
                        $.each(response.list, function(i, item) {
                            if (item.IsUsed) {
                                $("<option value=\"" + item.Id + "\" disabled=\"disabled\""+ (ManagerPNum.pnum == 0 ? "" : (item.Id == ManagerPNum.pnum ? " selected=\"selected\"" : "")) +">" + item.Number + "</option>").appendTo($("#PNum"));
                            } else {
                                $("<option value=\"" + item.Id + "\">" + item.Number + "</option>").appendTo($("#PNum"));
                            }
                        });
                    }
                    $("<option value=\"0\">请选择</option>").prependTo($("#PNum"));
                });
            }
        };

        $(function() {
            $("#PremisesId").change(function() {
                ManagerPlanes.id = $(this).val();
                ManagerPlanes.GetData();
                ManagerPNum.id = $(this).val();
                ManagerPNum.GetData();
            });

            ManagerPNum.GetData();
        });
    </script>
</asp:Content>