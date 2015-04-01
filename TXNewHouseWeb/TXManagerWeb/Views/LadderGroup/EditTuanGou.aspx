<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVM_NH_TuanGou>" %>

<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a> <i>&gt;</i><a href="javascript:void(0);">修改团购活动</a></span>
    </div>
    <div class="data">
        <div class="outer">
            <div class="dispose">
                <h5>
                    第一步：选择楼盘</h5>
            </div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="65">
                            选择楼盘：
                        </th>
                        <td>
                            <select id="PremisesId" disabled="disabled">
                                <option value="<%= Model.PremisesId %>">
                                    <%= Model.PremisesName %></option>
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 第一步 End -->
            <div class="data">
                <div class="dispose">
                    <h5>
                        第二步：选择房源 <span style="color: #999999">（可多选房源）</span></h5>
                    <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="tuangou.step1.disp(true);">
                        修改</a>
                </div>
                <div class="filterBar">
                    筛选：<%= Html.DropDownListFor(model=>model.BuildingId, Model.Buildings, new{@class="w100"}) %>
                    <select id="unitId" class="w100">
                    </select>
                    <select id="floor" class="w100">
                    </select>
                    <input type="button" id="btnSearch" value="搜索" />
                    <div class="filterBar" style="padding-bottom: 0; padding-left: 0;">
                        <input type="checkbox" id="chkSelAll" onchange="tuangou.step1.selAll();" />全选 &nbsp;&nbsp;<a
                            id="btn_batch_join" href="javascript:void(0);" onclick="tuangou.step1.batchJoinHouse();">参加</a>&nbsp;&nbsp;<a
                                id="btn_batch_canceljoin" href="javascript:void(0);" onclick="tuangou.step1.batchDelJoinHouse();">不参加</a>
                    </div>
                </div>
                <div class="clearFix">
                    <div id="ListResult">
                    </div>
                </div>
            </div>
            <div class="paging">
                <p id="pagebar" class="pubPage" style="border: none 0">
                    <!-- 这里显示分页 -->
                </p>
            </div>
            <input type="button" id="btn_SaveHouse" value="确定" />
            <!-- 第二步 End -->
            <div class="dispose">
                <h5>
                    第三步：设置基本信息</h5>
                <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="tuangou.step2.disp(true);">
                    修改</a>
            </div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="100">
                            活动名称：
                        </th>
                        <td>
                            <%= Html.TextBoxFor(model=>model.Name) %><span id="err_Name"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            活动保证金金额：
                        </th>
                        <td>
                            <%= Html.TextBoxFor(model=>model.BondString) %>&nbsp;&nbsp;元 <span id="err_BondString">
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            阶梯团购条件：
                        </th>
                        <td>
                            最多可设置4个
                        </td>
                    </tr>
                    <% for (int i = 0; i < 4; i++)
                       {
                    %>
                    <tr>
                        <th>
                            &nbsp;
                        </th>
                        <td>
                            当<input type="text" id="usercount_<%= (i + 1) %>" maxlength="9" value="<%= i < (Model.Socials.Count) ? Model.Socials[i].UserCount.ToString(CultureInfo.InvariantCulture) : "" %>" />人，团购价格为<input
                                type="text" id="discount_<%= (i + 1) %>" maxlength="9" value="<%= i < (Model.Socials.Count) ? Model.Socials[i].Discount.ToString(CultureInfo.InvariantCulture) : "" %>" />折<span
                                    id="err_discountinfo_<%= (i+1) %>"></span>
                        </td>
                    </tr>
                    <%
                       } %>
                    <tr>
                        <th>
                            时间：
                        </th>
                        <td>
                            开始&nbsp;&nbsp;<input type="text" id="begintime" value="<%= Model.BeginTimeString %>" /><span
                                id="err_begintime"></span>&nbsp;&nbsp; 结束&nbsp;&nbsp;<input type="text" id="endtime"
                                    value="<%= Model.EndTimeString %>" /><span id="err_endtime"></span>
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align: left;">
                            <input type="button" id="btn_saveBlock1" value="完成创建" />
                        </th>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        
        var json_Houses = [<%= ViewData["Join_Houses"] %>];

        var firstLoadPage = true;

        //初始化分页参数
        var opts = { callback: pageselectCallback };
        opts.items_per_page = 20;       //每页的记录条数
        opts.next_text = "下一页";       //上一条的文本
        opts.prev_text = "上一页";       //下一条的文本
        opts.last_text = "尾页";
        opts.num_display_entries = 5;   //中间显示的页码个数
        opts.num_edge_entries = 2;      //两边显示的页码个数
        opts.link_to = "javascript:void(0);";

        function pageselectCallback(pageIndex, jq) {
            opts.current_page = pageIndex;
            $("#ListResult").html('<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>');
            var url = '<%=Url.SiteUrl().LadderGroup_SearchResult("edittuangou_edit_houses") %>';
            var datas = {
                ActivityId: '<%= Request.Params["id"] %>',
                PremisesId: $("#PremisesId").val(),
                BuildingId: $("#BuildingId").val(),
                UnitNum: $("#unitId").val(),
                Floor: $("#floor").val(),
                SalesStatus: $("#SalesStatus").val(),
                pageindex: pageIndex,
                pagesize: opts.items_per_page,
                ram: Math.random()
            };
            $("#chkSelAll").prop("checked", false);
            $("#ListResult").load(url, datas, function () {
                if (firstLoadPage) {
                    tuangou.step1.initSignUpJoin(false);
                    firstLoadPage = false;
                }
            });
        }

        $(function() {
            $("#begintime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });
            $("#endtime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });

            $("#BuildingId").bind("change", tuangou.step1.loadUnit);
            $("#unitId").bind("change", tuangou.step1.loadFloor);
            $("#btnSearch").bind("click", tuangou.step1.loadPageBar);
            $("#btn_saveBlock1").bind("click", tuangou.step2.save);
            $("#btn_SaveHouse").bind("click", tuangou.step1.save);

            tuangou.step1.loadUnit();
            tuangou.step1.loadFloor();
            tuangou.step1.loadPageBar();

            tuangou.step1.disp(false);
            tuangou.step2.disp(false);
        });

        var tuangou = {
            icon_suc: "win ml10",
            icon_err: "lose ml10",
            err_content: "请填写",

            valid: {
                // 名称
                name: function() {
                    var value = $.trim($("#Name").val());
                    var eerr = $("#err_Name");
                    if ("" == value) {
                        eerr.attr("class", tuangou.icon_err);
                        eerr.html(tuangou.err_content);
                        return false;
                    }

                    eerr.attr("class", tuangou.icon_suc);
                    eerr.html("");
                    return true;
                },

                bond: function() {
                    var value = $.trim($("#BondString").val());
                    var eerr = $("#err_BondString");
                    if ("" == value) {
                        eerr.attr("class", tuangou.icon_err);
                        eerr.html(tuangou.err_content);
                        return false;
                    }

                    var pattern = /^[1-9]{1}\d{0,15}$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", tuangou.icon_err);
                        eerr.html("请输入正数");
                        return false;
                    }

                    eerr.attr("class", tuangou.icon_suc);
                    eerr.html("");
                    return true;
                },

                discountinfo: function() {
                    var flag = true;
                    var eerr;
                    for (var i = 0; i < 4; i++) {

                        var count = $.trim($("#usercount_" + (i + 1)).val());
                        var discount = $.trim($("#discount_" + (i + 1)).val());

                        if ("" == count && "" == discount) {
                            eerr = $("#err_discountinfo_" + (i + 1));
                            eerr.removeAttr("class");
                            eerr.html("");
                            continue;
                        }

                        var pattern = /^[1-9]{1}\d{0,15}$/;
                        if ("" != count && "" != discount) {
                            if (!pattern.test(count)) {
                                eerr = $("#err_discountinfo_" + (i + 1));
                                eerr.attr("class", tuangou.icon_err);
                                eerr.html("人数请输入正数");
                                flag = false;
                                continue;
                            }
                            if (!pattern.test(discount)) {
                                eerr = $("#err_discountinfo_" + (i + 1));
                                eerr.attr("class", tuangou.icon_err);
                                eerr.html("折扣请输入正数");
                                flag = false;
                                continue;
                            }

                            eerr = $("#err_discountinfo_" + (i + 1));
                            eerr.attr("class", tuangou.icon_suc);
                            eerr.html("");
                            continue;
                        }

                        eerr = $("#err_discountinfo_" + (i + 1));
                        eerr.attr("class", tuangou.icon_err);
                        eerr.html("请将信息填写完整");
                        flag = false;
                    }

                    return flag;
                },

                begintime: function() {
                    var value = $.trim($("#begintime").val());
                    var eerr = $("#err_begintime");
                    if ("" == value) {
                        eerr.attr("class", tuangou.icon_err);
                        eerr.html(tuangou.err_content);
                        return false;
                    }

                    eerr.attr("class", tuangou.icon_suc);
                    eerr.html("");
                    return true;
                },

                endtime: function() {
                    var value = $.trim($("#endtime").val());
                    var eerr = $("#err_endtime");
                    if ("" == value) {
                        eerr.attr("class", tuangou.icon_err);
                        eerr.html(tuangou.err_content);
                        return false;
                    }

                    eerr.attr("class", tuangou.icon_suc);
                    eerr.html("");
                    return true;
                }
            },

            // 步骤1
            step1: {
                loadUnit: function() {
                    var selectUnits = $("#unitId");
                    $(selectUnits).empty();
                    $("#unitId").prepend("<option data=\"\" lng=\"0\" lat=\"0\" value=\"0\">选择单元</option>");
                    loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetUnitsByBuildingId") %>?buildingId=' + $("#BuildingId").val(), selectUnits, tuangou.step1.loadFloor);
                },

                loadFloor: function() {
                    var selectFloor = $("#floor");
                    $(selectFloor).empty();
                    $("#floor").prepend("<option data=\"\" lng=\"0\" lat=\"0\" value=\"0\">选择楼层</option>");
                    tuangou.step1.edLoadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetFloorsByBuildingId") %>?id=' + $("#BuildingId").val(), selectFloor);
                },

                // 加载下拉列表
                edLoadGeographyItems: function(url, container, onComplete) {
                    $.getJSON(url, function(response) {
                        if (response && response.success && container) {
                            tuangou.step1.edFillListItems(container, response.items);
                            if (onComplete) {
                                onComplete(container);
                            }
                        }
                    });
                },

                // 辅助(加载下拉列表)
                edFillListItems: function(container, dataItems) {
                    if (dataItems) {
                        $.each(dataItems, function() {
                            if (this.GeographyCode == 0
                                && this.GeographyLat == "--"
                                && this.GeographyLng == "--") {
                                $("<optgroup label='" + this.GeographyName + "'>" + this.GeographyName + "</optgroup>").appendTo(container);
                            } else {
                                $("<option value='" + this.GeographyCode + "' data='" + this.GeographyName + "'>" + this.GeographyName + "</option>")
                                    .appendTo(container);
                            }
                        });
                    }
                },

                // 加载分页条
                loadPageBar: function() {
                    var url = '<%=Url.SiteUrl().PurchaseHouse_SearchResult("editpurchasehouse_edit_houses_count") %>';
                    var datas = {
                        ActivityId: '<%= Request.Params["id"] %>',
                        PremisesId: $("#PremisesId").val(),
                        BuildingId: $("#BuildingId").val(),
                        UnitNum: $("#unitId").val(),
                        Floor: $("#floor").val(),
                        ram: Math.random()
                    };
                    $.post(url, datas, function(data, textStatus) {
                        $("#pagebar").pagination(data.result, opts);
                    });
                },

                // 初始化标记参加
                initSignUpJoin: function(open) {
                    var count = $("a[id^=join_]").length;
                    if (0 == count) {
                        // 房源列表没有数据

                        return;
                    }

                    $("a[id^=join_]").each(function() {
                        if (tuangou.step1.isHouseJoin($(this).attr("value"))) {
                            $(this).html("不参加");
                        } else {
                            $(this).html("参加");
                        }

                        var houseId = $(this).attr("id").split("_")[1];
                        if (!open) {
                            $("#chk_" + houseId).attr("disabled", "disabled");
                            $("#join_" + houseId).hide();
                        } else {
                            $("#chk_" + houseId).removeAttr("disabled");
                            $("#join_" + houseId).show();
                        }
                    });
                },

                // 标记参加、 不参加
                signupJoin: function(houseId) {
                    if (tuangou.step1.isHouseJoin(houseId)) {
                        // 删除
                        tuangou.step1.delJoinHouse(houseId);
                    } else {
                        // 添加
                        tuangou.step1.addJoinHouse(houseId);
                    }

                    // 刷新显示状态
                    tuangou.step1.initSignUpJoin(true);
                },

                // 批量不参加活动
                batchDelJoinHouse: function() {
                    var count = $("input[id^=chk_]:checked").length;
                    if (count < 1) {
                        alert("请选择不参加活动的房源");
                        return;
                    }

                    $("input[id^=chk_]:checked").each(function() {
                        tuangou.step1.delJoinHouse($(this).attr("value"));
                    });

                    // 刷新显示状态
                    tuangou.step1.initSignUpJoin(true);
                },

                // 批量参加活动
                batchJoinHouse: function() {
                    var count = $("input[id^=chk_]:checked").length;
                    if (count < 1) {
                        alert("请选择参加活动的房源");
                        return;
                    }

                    $("input[id^=chk_]:checked").each(function() {
                        tuangou.step1.addJoinHouse($(this).attr("value"));
                    });

                    // 刷新显示状态
                    tuangou.step1.initSignUpJoin(true);
                },

                // 删除房源
                delJoinHouse: function(houseId) {
                    for (var i = 0; i < json_Houses.length; i++) {
                        if (json_Houses[i].HouseId == houseId) {
                            if (json_Houses[i].IsOriData == "1") {
                                json_Houses[i].IsDel = "1";
                                return;
                            } else {
                                json_Houses.splice(i, 1);
                            }
                        }
                    }
                },

                // 添加房源
                addJoinHouse: function(houseId) {
                    for (var i = 0; i < json_Houses.length; i++) {
                        if (json_Houses[i].HouseId == houseId) {
                            if (json_Houses[i].IsOriData == "1") {
                                json_Houses[i].IsDel = "0";
                                // 源数据 需要标记为参加
                                return;
                            } else {
                                // 新数据 不需要重复添加
                                return;
                            }
                        }
                    }

                    json_Houses.push({ "HouseId": houseId, "IsOriData": "0", "IsDel": "0" });
                },

                // 判断房源是否参与该活动
                isHouseJoin: function(houseId) {
                    for (var i = 0; i < json_Houses.length; i++) {
                        if (json_Houses[i].HouseId == houseId) {
                            if (json_Houses[i].IsOriData == "0") {
                                // 本次操作新添加

                                return true;
                            } else {
                                // 创建时添加的房源

                                if (json_Houses[i].IsDel == "0") {
                                    return true;
                                } else {
                                    return false;
                                }
                            }
                        }
                    }
                    return false;
                },

                // 获取参加活动的房源数量
                getJoinHouseCount: function() {
                    if (0 == json_Houses.length) {
                        return 0;
                    }

                    var count = 0;
                    for (var i = 0; i < json_Houses.length; i++) {
                        if ("1" == json_Houses[i].IsOriData) {
                            if ("0" == json_Houses[i].IsDel) {
                                count++;
                            }
                        } else {
                            count++;
                        }
                    }

                    return count;
                },

                // 获取参加活动的房源编号集合
                getJoinHouseJsonIds: function() {
                    if (0 == tuangou.step1.getJoinHouseCount()) {
                        return "";
                    }

                    var ids = new Array();
                    for (var i = 0; i < json_Houses.length; i++) {
                        if ("1" == json_Houses[i].IsOriData) {
                            if ("0" == json_Houses[i].IsDel) {
                                ids.push(json_Houses[i].HouseId);
                            }
                        } else {
                            ids.push(json_Houses[i].HouseId);
                        }
                    }

                    return ids.join(",");
                },

                // 获取所有参与活动的房源、折扣信息
                getAllJoinHouses: function() {
                    var newHouseJsonString = "";
                    for (var i = 0; i < json_Houses.length; i++) {
                        newHouseJsonString += "{\"HouseId\":\"" + json_Houses[i].HouseId + "\",\"IsOriData\":\"" + json_Houses[i].IsOriData + "\",\"IsDel\":\"" + json_Houses[i].IsDel + "\"}";
                        if (i < json_Houses.length - 1) {
                            newHouseJsonString += ",";
                        }
                    }
                    return ("[" + newHouseJsonString + "]");
                },

                // 全选
                selAll: function() {
                    $("input[id^=chk_]").each(function() {
                        $(this).prop("checked", $("#chkSelAll").prop("checked"));
                    });
                },

                // 打开/关闭
                disp: function(open) {
                    if (open) {
                        $("#BuildingId").removeAttr("disabled");
                        $("#unitId").removeAttr("disabled");
                        $("#floor").removeAttr("disabled");
                        $("#btnSearch").removeAttr("disabled");
                        $("#chkSelAll").removeAttr("disabled");
                        $("#btn_batch_join").show();
                        $("#btn_batch_canceljoin").show();
                        $("#pagebar").show();
                        $("#btn_SaveHouse").removeAttr("disabled");
                        tuangou.step1.initSignUpJoin(true);
                    } else {
                        $("#BuildingId").attr("disabled", "disabled");
                        $("#unitId").attr("disabled", "disabled");
                        $("#floor").attr("disabled", "disabled");
                        $("#btnSearch").attr("disabled", "disabled");
                        $("#chkSelAll").attr("disabled", "disabled");
                        $("#btn_batch_join").hide();
                        $("#btn_batch_canceljoin").hide();
                        $("#pagebar").hide();
                        $("#btn_SaveHouse").attr("disabled", "disabled");
                        tuangou.step1.initSignUpJoin(false);
                    }

                },

                valid: function() {
                    if (0 == tuangou.step1.getJoinHouseCount()) {
                        alert("请选择参加活动的房源");
                        return false;
                    }

                    return true;
                },

                save: function() {
                    if (!tuangou.step1.valid()) {
                        return;
                    }

                    tuangou.step1.disp(false);
                }
            },

            // 步骤2
            step2: {
                valid: function() {
                    return (tuangou.valid.name()
                        & tuangou.valid.bond()
                        & tuangou.valid.discountinfo()
                        & tuangou.valid.begintime()
                        & tuangou.valid.endtime());
                },

                save: function() {
                    if (!tuangou.step2.valid()) {
                        return;
                    }

                    tuangou.step2.disp();

                    if (!tuangou.step1.valid()) {
                        return;
                    }

                    $("#btn_saveBlock1").attr("style", "display: none;");

                    var url = '<%= Auxiliary.Instance.NhManagerUrl %>laddergroup/editdo.html';
                    var data = {
                        id: '<%= Request.Params["id"] %>',
                        premisesid: $("#PremisesId").val(),
                        name: $("#Name").val(),
                        bond: $("#BondString").val(),
                        begintime: $("#begintime").val(),
                        endtime: $("#endtime").val(),
                        housesjsonstring: tuangou.step1.getAllJoinHouses(),
                        socialsjsonstring: tuangou.step2.getAllDiscountInfoJsonString()
                    };

                    $.post(url, data, function(msg) {
                        if (msg.suc) {
                            alert("保存成功");
                            return;
                        }
                        alert("保存失败");
                        $("#btn_saveBlock1").removeAttr("style");
                    });
                },

                disp: function(open) {
                    var i;
                    if (open) {
                        $("#Name").removeAttr("disabled");
                        $("#BondString").removeAttr("disabled");
                        for (i = 0; i < 4; i++) {
                            $("#usercount_" + (i + 1)).removeAttr("disabled");
                            $("#discount_" + (i + 1)).removeAttr("disabled");
                        }
                        $("#begintime").removeAttr("disabled");
                        $("#endtime").removeAttr("disabled");
                    } else {
                        $("#Name").attr("disabled", "disabled");
                        $("#BondString").attr("disabled", "disabled");
                        for (i = 0; i < 4; i++) {
                            $("#usercount_" + (i + 1)).attr("disabled", "disabled");
                            $("#discount_" + (i + 1)).attr("disabled", "disabled");
                        }
                        $("#begintime").attr("disabled", "disabled");
                        $("#endtime").attr("disabled", "disabled");
                    }
                },

                getAllDiscountInfoJson: function() {
                    var json = [];
                    for (var i = 0; i < 4; i++) {
                        var count = $.trim($("#usercount_" + (i + 1)).val());
                        var discount = $.trim($("#discount_" + (i + 1)).val());
                        if ("" != count) {
                            json.push({ usercount: count, discount: discount });
                        }
                    }
                    return json;
                },

                getAllDiscountInfoJsonString: function() {
                    var jsonstr = "";
                    var json = tuangou.step2.getAllDiscountInfoJson();
                    for (var i = 0; i < json.length; i++) {
                        jsonstr += "{\"UserCount\":\"" + json[i].usercount + "\",\"Discount\":\"" + json[i].discount + "\"}";
                        if (i < json.length - 1) {
                            jsonstr += ",";
                        }
                    }
                    return "[" + jsonstr + "]";
                }

            }
        };
    </script>
</asp:Content>