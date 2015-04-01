<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVM_NH_Purchase>" %>

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
                <%=AdminPageInfo.ItemName %></a> <i>&gt;</i><a href="javascript:void(0);">修改排号购房活动</a></span>
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
                            <select id="PremisesId" disabled="disabled"><option value="<%= Model.PremisesId %>"><%= Model.PremisesName %></option></select>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 第一步 End -->
            <div class="dispose">
                <h5>
                    第二步：设置基本信息</h5>
                <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="purchase.step1_disp(true);">
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
                            取前：
                        </th>
                        <td>
                            <%= Html.TextBoxFor(model=>model.UserCount) %>&nbsp;&nbsp;名，为有效报名。<span id="err_UserCount">
                            </span>
                        </td>
                    </tr>
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
                            <input type="button" id="btn_saveBlock1" value="确定" />
                        </th>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 第二步 End -->
            <div class="data">
                <div class="dispose">
                    <h5>
                        第三步：选择房源 <span style="color: #999999">（可多选房源）</span></h5>
                    <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="purchase.step2_disp(true);">
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
                        <input type="checkbox" id="chkSelAll" onchange="purchase.selAll();" />全选
                        &nbsp;&nbsp;<a id="btn_batch_join" href="javascript:void(0);" onclick="purchase.batchJoinHouse();">参加</a>&nbsp;&nbsp;<a
                            id="btn_batch_canceljoin" href="javascript:void(0);" onclick="purchase.batchDelJoinHouse();">不参加</a>
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
            <input type="button" id="btn_SaveHouse" value="完成创建" />
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
        
        //根据页面索引取得内容
        function pageselectCallback(pageIndex, jq) {
            opts.current_page = pageIndex;
            $("#ListResult").html('<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>');
            var url = '<%=Url.SiteUrl().PurchaseHouse_SearchResult("editpurchasehouse_edit_houses") %>';
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
            $("#ListResult").load(url, datas, function() {
                if (firstLoadPage) {
                    purchase.initSignUpJoin(false);
                    firstLoadPage = false;
                }
            });
        }

        $(function() {
            $("#begintime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });
            $("#endtime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });

            $("#btn_saveBlock1").bind("click", purchase.step1_save);
            $("#BuildingId").bind("change", purchase.step2_loadUnit);
            $("#unitId").bind("change", purchase.step2_loadFloor);
            $("#btnSearch").bind("click", purchase.step2_loadPageBar);
            $("#btn_SaveHouse").bind("click", purchase.step2_save);

            purchase.step2_loadUnit();
            purchase.step2_loadFloor();

            purchase.step2_loadPageBar();

            purchase.step1_disp(false);
            purchase.step2_disp(false);
        });

        var purchase = {
            icon_suc: "win ml10",
            icon_err: "lose ml10",
            err_content: "请填写",

            valid: {
                // 名称
                name: function() {
                    var value = $.trim($("#Name").val());
                    var eerr = $("#err_Name");
                    if ("" == value) {
                        eerr.attr("class", purchase.icon_err);
                        eerr.html(purchase.err_content);
                        return false;
                    }

                    eerr.attr("class", purchase.icon_suc);
                    eerr.html("");
                    return true;
                },

                bond: function() {
                    var value = $.trim($("#BondString").val());
                    var eerr = $("#err_BondString");
                    if ("" == value) {
                        eerr.attr("class", purchase.icon_err);
                        eerr.html(purchase.err_content);
                        return false;
                    }

                    var pattern = /^[1-9]{1}\d{0,15}$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", purchase.icon_err);
                        eerr.html("请输入正数");
                        return false;
                    }

                    eerr.attr("class", purchase.icon_suc);
                    eerr.html("");
                    return true;
                },

                usercount: function() {
                    var value = $.trim($("#UserCount").val());
                    var eerr = $("#err_UserCount");
                    if ("" == value) {
                        eerr.attr("class", purchase.icon_err);
                        eerr.html(purchase.err_content);
                        return false;
                    }

                    var pattern = /^[1-9]{1}\d{0,15}$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", purchase.icon_err);
                        eerr.html("请输入正数");
                        return false;
                    }

                    eerr.attr("class", purchase.icon_suc);
                    eerr.html("");
                    return true;
                },

                begintime: function() {
                    var value = $.trim($("#begintime").val());
                    var eerr = $("#err_begintime");
                    if ("" == value) {
                        eerr.attr("class", purchase.icon_err);
                        eerr.html(purchase.err_content);
                        return false;
                    }

                    eerr.attr("class", purchase.icon_suc);
                    eerr.html("");
                    return true;
                },

                endtime: function() {
                    var value = $.trim($("#endtime").val());
                    var eerr = $("#err_endtime");
                    if ("" == value) {
                        eerr.attr("class", purchase.icon_err);
                        eerr.html(purchase.err_content);
                        return false;
                    }

                    eerr.attr("class", purchase.icon_suc);
                    eerr.html("");
                    return true;
                },

                houses: function() {
                    if (0 == purchase.getJoinHouseCount()) {
                        alert("请选择参加活动的房源");
                        return false;
                    }
                    return true;
                }
            },

            // 步骤1 保存
            step1_save: function() {
                if (!purchase.step1_valid()) {
                    return;
                }
                purchase.step1_disp(false);
            },

            // 步骤1 验证
            step1_valid: function() {
                var flag = purchase.valid.name()
                    & purchase.valid.bond()
                    & purchase.valid.usercount()
                    & purchase.valid.begintime()
                    & purchase.valid.endtime();
                return flag;
            },

            // 步骤1 打开/关闭
            step1_disp: function(open) {
                if (open) {
                    $("#Name").removeAttr("disabled");
                    $("#BondString").removeAttr("disabled");
                    $("#UserCount").removeAttr("disabled");
                    $("#begintime").removeAttr("disabled");
                    $("#endtime").removeAttr("disabled");
                    $("#btn_saveBlock1").removeAttr("disabled");
                } else {
                    $("#Name").attr("disabled", "disabled");
                    $("#BondString").attr("disabled", "disabled");
                    $("#UserCount").attr("disabled", "disabled");
                    $("#begintime").attr("disabled", "disabled");
                    $("#endtime").attr("disabled", "disabled");
                    $("#btn_saveBlock1").attr("disabled", "disabled");
                }
            },

            // 步骤2 加载单元
            step2_loadUnit: function() {
                var selectUnits = $("#unitId");
                $(selectUnits).empty();
                $("#unitId").prepend("<option data=\"\" lng=\"0\" lat=\"0\" value=\"0\">选择单元</option>");
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetUnitsByBuildingId") %>?buildingId=' + $("#BuildingId").val(), selectUnits, purchase.step2_loadFloor);
            },

            // 步骤2 加载楼层
            step2_loadFloor: function() {
                var selectFloor = $("#floor");
                $(selectFloor).empty();
                $("#floor").prepend("<option data=\"\" lng=\"0\" lat=\"0\" value=\"0\">选择楼层</option>");
                purchase.step2_edLoadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetFloorsByBuildingId") %>?id=' + $("#BuildingId").val(), selectFloor);
            },

            // 加载下拉列表
            step2_edLoadGeographyItems: function(url, container, onComplete) {
                $.getJSON(url, function(response) {
                    if (response && response.success && container) {
                        purchase.step2_edFillListItems(container, response.items);
                        if (onComplete) {
                            onComplete(container);
                        }
                    }
                });
            },

            // 辅助(加载下拉列表)
            step2_edFillListItems: function(container, dataItems) {
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
            step2_loadPageBar: function() {
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

            // 步骤2 打开/关闭
            step2_disp: function(open) {
                if (open) {
                    $("#BuildingId").removeAttr("disabled");
                    $("#unitId").removeAttr("disabled");
                    $("#floor").removeAttr("disabled");
                    $("#btnSearch").removeAttr("disabled");
                    $("#chkSelAll").removeAttr("disabled");
                    $("#btn_batch_join").show();
                    $("#btn_batch_canceljoin").show();
                    $("#pagebar").show();
                    purchase.initSignUpJoin(true);
                } else {
                    $("#BuildingId").attr("disabled", "disabled");
                    $("#unitId").attr("disabled", "disabled");
                    $("#floor").attr("disabled", "disabled");
                    $("#btnSearch").attr("disabled", "disabled");
                    $("#chkSelAll").attr("disabled", "disabled");
                    $("#btn_batch_join").hide();
                    $("#btn_batch_canceljoin").hide();
                    $("#pagebar").hide();
                    purchase.initSignUpJoin(false);
                }
            },

            // 步骤2 保存
            step2_save: function() {
                if (!purchase.valid.houses()) {
                    return;
                }
                purchase.step2_disp(false);

                if (!purchase.step1_valid()) {
                    return;
                }

                $("#btn_SaveHouse").attr("style", "display: none;");
                
                var url = '<%= Auxiliary.Instance.NhManagerUrl %>purchasehouse/editpurchasehousedo.html';
                var data = {
                    id: '<%= Request.Params["id"] %>',
                    premisesid: $("#PremisesId").val(),
                    name: $("#Name").val(),
                    usercount: $("#UserCount").val(),
                    bond: $("#BondString").val(),
                    begintime: $("#begintime").val(),
                    endtime: $("#endtime").val(),
                    housesjsonstring: purchase.getAllJoinHouses()
                };

                $.post(url, data, function(msg) {
                    if (msg.suc) {
                        alert("保存成功");
                        return;
                    }
                    alert("保存失败");
                    $("#btn_SaveHouse").removeAttr("style");
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
                    if (purchase.isHouseJoin($(this).attr("value"))) {
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
                if (purchase.isHouseJoin(houseId)) {
                    // 删除
                    purchase.delJoinHouse(houseId);
                } else {
                    // 添加
                    purchase.addJoinHouse(houseId);
                }

                // 刷新显示状态
                purchase.initSignUpJoin(true);
            },
            
            // 批量不参加活动
            batchDelJoinHouse: function() {
                var count = $("input[id^=chk_]:checked").length;
                if (count < 1) {
                    alert("请选择不参加活动的房源");
                    return;
                }

                $("input[id^=chk_]:checked").each(function() {
                    purchase.delJoinHouse($(this).attr("value"));
                });

                // 刷新显示状态
                purchase.initSignUpJoin(true);
            },

            // 批量参加活动
            batchJoinHouse: function() {
                var count = $("input[id^=chk_]:checked").length;
                if (count < 1) {
                    alert("请选择参加活动的房源");
                    return;
                }

                $("input[id^=chk_]:checked").each(function() {
                    purchase.addJoinHouse($(this).attr("value"));
                });

                // 刷新显示状态
                purchase.initSignUpJoin(true);
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
                if (0 == purchase.getJoinHouseCount()) {
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
            }
        };
    </script>
</asp:Content>