<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_Discount>" %>

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
                <%=AdminPageInfo.ItemName %></a> <i>&gt;</i><a href="javascript:void(0);">修改限时折扣活动</a></span>
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
                            <%= Html.DropDownListFor(model=>model.PremisesId, Model.Premises,new {@disabled="disabled"}) %>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 第一步 End -->
            <div class="dispose">
                <h5>
                    第二步：设置基本信息</h5>
                <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="editdiscount_step2.model_open();">
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
                    <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="editdiscount_step3.updateInfo();">
                        修改</a>
                </div>
                <div class="filterBar">
                    筛选：<%= Html.DropDownListFor(model=>model.BuildingId, Model.Buildings, new{@class="w100"}) %>
                    <select id="unitId" class="w100">
                    </select>
                    <select id="floor" class="w100">
                    </select>
                    <%-- <%= Html.DropDownListFor(model=>model.SalesStatus, Model.SalesStatusList, new{@class="w100"}) %>--%>
                    <input type="button" id="btnSearch" value="搜索" />
                    <div class="filterBar" style="padding-bottom: 0; padding-left: 0;">
                        <input type="checkbox" id="chkSelAll" onchange="editdiscount_step3.selAll();" />全选
                        &nbsp;&nbsp;<a id="btn_batch_join" href="javascript:void(0);" onclick="editdiscount_step3.batchJoinHouse();">参加</a>&nbsp;&nbsp;<a
                            id="btn_batch_canceljoin" href="javascript:void(0);" onclick="editdiscount_step3.batchDelJoinHouse();">不参加</a>
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
            <input type="button" id="btn_SaveHouse" value="完成设置" />
            <!-- 第三步 End -->
            <div class="data">
                <div class="dispose">
                    <h5>
                        第四步：设置限时折扣</h5>
                    <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="editdiscount_step4.updateInfo();">
                        修改</a>
                </div>
                <div class="tab1">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                        <tr>
                            <th width="110">
                                批量设置折扣：
                            </th>
                            <td>
                                <input type="text" id="discountAll" />折&nbsp;&nbsp;<input type="button" id="btn_setDiscountAll"
                                    value="确定" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clearFix">
                    <div id="ListResult2">
                    </div>
                </div>
            </div>
            <input type="button" id="btn_submit" value="完成创建" />
            <!-- 第四步 End -->
        </div>
    </div>
    <script type="text/javascript">

        var json_Houses = [<%= ViewData["Join_Houses"] %>]; //[{ "HouseId": "4", "IsOriData": "1", "IsDel": "0" }, { "HouseId": "8", "IsOriData": "1", "IsDel": "0" }];
        var isFirstLoadPage_step3 = true;
        var isFirstLoadPage_step4 = true;

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
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            var url = '<%=Url.SiteUrl().Discount_Search("searchresult_edit1") %>';
            var datas = {
                ActivitiesId: '<%= Model.Id %>',
                PremisesId: $("#PremisesId").val(),
                BuildingId: $("#BuildingId").val(),
                UnitId: $("#unitId").val(),
                Floor: $("#floor").val(),
                SalesStatus: $("#SalesStatus").val(),
                pageindex: pageIndex,
                pagesize: opts.items_per_page,
                ram: Math.random()
            };
            $("#chkSelAll").prop("checked", false);
            if (isFirstLoadPage_step3) {
                $("#ListResult").load(url, datas, editdiscount_step3.initSignUpJoin0);
                isFirstLoadPage_step3 = false;
            } else {
                $("#ListResult").load(url, datas, editdiscount_step3.initSignUpJoin);
            }
        }

        $(function () {
            
            $("#begintime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });
            $("#endtime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });

            var selectBuildingId = $("#BuildingId");
            var selectUnits = $("#unitId");

            $(selectBuildingId).change(function () {
                editdiscount_step3.loadUnits();
            });
            $(selectUnits).change(function () {
                editdiscount_step3.loadFloor();
            });

            // 绑定 模块1确定事件
            $("#btn_saveBlock1").bind("click", editdiscount_step2.saveInfo);

            // 绑定 搜索事件
            $("#btnSearch").bind("click", editdiscount_step3.loadHouseList);

            // 绑定 模块3事件(选定参加活动房源事件)
            $("#btn_SaveHouse").bind("click", editdiscount_step3.saveInfo);

            // 绑定 批量生成折扣
            $("#btn_setDiscountAll").bind("click", editdiscount_step4.batchUpdateDiscount);

            // 绑定 完成创建
            $("#btn_submit").bind("click", editdiscount_step4.saveInfo);

            // 加载单元列表
            editdiscount_step3.loadUnits();
            
            // 加载分页控件
            editdiscount_step3.loadPageBar();
            
            // 显示参加房源的折扣价格信息
            editdiscount_step4.displayHouseCountList();
            
            // 关闭第二部分
            editdiscount_step2.model_close();
        });

        var editdiscount = {            
            icon_suc: "win ml10",
            icon_err: "lose ml10",
            err_content: "请填写",

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
                if (0 == editdiscount.getJoinHouseCount()) {
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
            }
        };

        // 第二步
        var editdiscount_step2 = {
            
            // 验证 活动名称
            valid_Name: function() {
                var value = $.trim($("#Name").val());
                var eerr = $("#err_Name");
                if ("" == value) {
                    eerr.attr("class", editdiscount.icon_err);
                    eerr.html(editdiscount.err_content);
                    return false;
                }

                eerr.attr("class", editdiscount.icon_suc);
                eerr.html("");
                return true;
            },
            
            // 验证 活动保证金金额
            valid_BondString: function() {
                var value = $.trim($("#BondString").val());
                var eerr = $("#err_BondString");
                if ("" == value) {
                    eerr.attr("class", editdiscount.icon_err);
                    eerr.html(editdiscount.err_content);
                    return false;
                }

                var pattern = /^[1-9]{1}\d{0,15}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", editdiscount.icon_err);
                    eerr.html("请输入正数");
                    return false;
                }

                eerr.attr("class", editdiscount.icon_suc);
                eerr.html("");
                return true;
            },
            
            // 验证 开始时间
            valid_BeginTime: function() {
                var value = $.trim($("#begintime").val());
                var eerr = $("#err_begintime");
                if ("" == value) {
                    eerr.attr("class", editdiscount.icon_err);
                    eerr.html(editdiscount.err_content);
                    return false;
                }

                eerr.attr("class", editdiscount.icon_suc);
                eerr.html("");
                return true;
            },
            
            // 验证 结束时间
            valid_EndTime: function() {
                var value = $.trim($("#endtime").val());
                var eerr = $("#err_endtime");
                if ("" == value) {
                    eerr.attr("class", editdiscount.icon_err);
                    eerr.html(editdiscount.err_content);
                    return false;
                }

                eerr.attr("class", editdiscount.icon_suc);
                eerr.html("");
                return true;
            },
            
            // 验证表单
            valid_all: function() {
                if (!editdiscount_step2.valid_Name()
                    | !editdiscount_step2.valid_BondString()
                    | !editdiscount_step2.valid_BeginTime()
                    | !editdiscount_step2.valid_EndTime()) {

                    return false;
                }

                return true;
            },
            
            // 打开模式
            model_open: function() {
                $("#Name").removeAttr("disabled");
                $("#BondString").removeAttr("disabled");
                $("#begintime").removeAttr("disabled");
                $("#endtime").removeAttr("disabled");
                $("#btn_saveBlock1").removeAttr("disabled");
            },
            
            // 关闭模式
            model_close: function() {
                $("#Name").attr("disabled", "disabled");
                $("#BondString").attr("disabled", "disabled");
                $("#begintime").attr("disabled", "disabled");
                $("#endtime").attr("disabled", "disabled");
                $("#btn_saveBlock1").attr("disabled", "disabled");
            },

            // 按钮 确定
            saveInfo: function() {
                if (!editdiscount_step2.valid_all()) {
                    return;
                }

                editdiscount_step2.model_close();
            }
        };

        // 第三步
        var editdiscount_step3 = {
            
            // 加载单元列表
            loadUnits: function() {
                var selectUnits = $("#unitId");
                $(selectUnits).empty();
                $("#unitId").prepend("<option data=\"\" lng=\"0\" lat=\"0\" value=\"0\">请选择单元</option>");
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetUnitsByBuildingId") %>?buildingId=' + $("#BuildingId").val(), selectUnits, editdiscount_step3.loadFloor);
            },

            // 加载楼层列表
            loadFloor: function() {
                var selectFloor = $("#floor");
                $(selectFloor).empty();
                $("#floor").prepend("<option data=\"\" lng=\"0\" lat=\"0\" value=\"0\">请选择楼层</option>");
                editdiscount_step3.edLoadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetFloorsByBuildingId") %>?id=' + $("#BuildingId").val(), selectFloor);
            },

            // 加载下拉列表
            edLoadGeographyItems: function(url, container, onComplete) {
                $.getJSON(url, function(response) {
                    if (response && response.success && container) {
                        editdiscount_step3.edFillListItems(container, response.items);
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
                var url = '<%=Url.SiteUrl().Discount_Search("searchcount_edit1") %>';
                var datas = {
                    ActivitiesId: '<%= Model.Id %>',
                    PremisesId: $("#PremisesId").val(),
                    BuildingId: $("#BuildingId").val(),
                    UnitId: $("#unitId").val(),
                    Floor: $("#floor").val(),
                    SalesStatus: $("#SalesStatus").val(),
                    ram: Math.random()
                };
                $.post(url, datas, function(data, textStatus) {
                    $("#pagebar").pagination(data.result, opts);
                });
            },

            // 加载房源列表
            loadHouseList: function() {
                opts.current_page = 0;
                editdiscount_step3.loadPageBar();
                $("#chkSelAll").prop("checked", false);
            },
            
            // 全选
            selAll: function() {
                $("input[id^=chk_h_]").each(function() {
                    $(this).prop("checked", $("#chkSelAll").prop("checked"));
                });
            },

            // 初始化标记参加(第一次加载页面时需要关闭第三步模块)
            initSignUpJoin0: function() {
                editdiscount_step3.initSignUpJoin();
                editdiscount_step3.model_close();
            },

            // 初始化标记参加
            initSignUpJoin: function() {
                var count = $("a[id^=join_]").length;
                if (0 == count) {
                    // 房源列表没有数据

                    return;
                }

                $("a[id^=join_]").each(function() {
                    if (editdiscount.isHouseJoin($(this).attr("value"))) {
                        $(this).html("不参加");
                    } else {
                        $(this).html("参加");
                    }
                });
            },

            // 标记参加、 不参加
            signupJoin: function(houseId) {
                if (editdiscount.isHouseJoin(houseId)) {
                    // 删除
                    editdiscount.delJoinHouse(houseId);
                } else {
                    // 添加
                    editdiscount.addJoinHouse(houseId);
                }

                // 刷新显示状态
                editdiscount_step3.initSignUpJoin();
            },
            
            // 批量不参加活动
            batchDelJoinHouse: function() {
                var count = $("input[id^=chk_h_]:checked").length;
                if (count < 1) {
                    alert("请选择不参加活动的房源");
                    return;
                }

                $("input[id^=chk_h_]:checked").each(function() {
                    editdiscount.delJoinHouse($(this).attr("value"));
                });

                // 刷新显示状态
                editdiscount_step3.initSignUpJoin();
            },

            // 批量参加活动
            batchJoinHouse: function() {
                var count = $("input[id^=chk_h_]:checked").length;
                if (count < 1) {
                    alert("请选择参加活动的房源");
                    return;
                }

                $("input[id^=chk_h_]:checked").each(function() {
                    editdiscount.addJoinHouse($(this).attr("value"));
                });

                // 刷新显示状态
                editdiscount_step3.initSignUpJoin();
            },

            // 打开模式
            model_open: function() {
                $("#BuildingId").removeAttr("disabled");
                $("#unitId").removeAttr("disabled");
                $("#floor").removeAttr("disabled");
                $("#SalesStatus").removeAttr("disabled");
                $("#btnSearch").removeAttr("disabled");
                $("#chkSelAll").removeAttr("disabled");

                $("#btn_batch_join").show();
                $("#btn_batch_canceljoin").show();

                $("input[id^=chk_h_]").each(function() {
                    $(this).removeAttr("disabled");
                });
                
                $("a[id^=join_]").each(function() {
                    $(this).show();
                });

                $("#pagebar").show();
                
                $("#btn_SaveHouse").removeAttr("disabled");
            },
            
            // 关闭模式
            model_close: function() {
                $("#BuildingId").attr("disabled", "disabled");
                $("#unitId").attr("disabled", "disabled");
                $("#floor").attr("disabled", "disabled");
                $("#SalesStatus").attr("disabled", "disabled");
                $("#btnSearch").attr("disabled", "disabled");
                $("#chkSelAll").attr("disabled", "disabled");
                
                $("#btn_batch_join").hide();
                $("#btn_batch_canceljoin").hide();

                $("input[id^=chk_h_]").each(function() {
                    $(this).attr("disabled", "disabled");
                });

                $("a[id^=join_]").each(function() {
                    $(this).hide();
                });

                $("#pagebar").hide();
                
                $("#btn_SaveHouse").attr("disabled", "disabled");
            },
            
            // 按钮 确定
            saveInfo: function() {
                // 关闭 第三步模块
                editdiscount_step3.model_close();
                
                // 初始化房源折扣信息
                isFirstLoadPage_step4 = true;
                editdiscount_step4.displayHouseCountList();
            },
            
            // 按钮 修改
            updateInfo: function() {
                // 打开 第三步模块
                editdiscount_step3.model_open();

                // 初始化标记“参加”、“不参加”
                editdiscount_step3.initSignUpJoin();
            }
        };

        // 第四步
        var editdiscount_step4 = {
            // 显示房源折扣列表
            displayHouseCountList: function() {
                $("#ListResult2").html("<div style=\"text-align: center;\"><img src=\'<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\' alt=\"正在加载中...\" /></div>");
                var url = '<%=Url.SiteUrl().Discount_Search("GetHousesTable_JoinActivity") %>';
                var datas = {
                    activitiesId: '<%= Model.Id %>',
                    houseIds: editdiscount.getJoinHouseJsonIds(),
                    ram: Math.random()
                };
                if (isFirstLoadPage_step4) {
                    $("#ListResult2").load(url, datas, editdiscount_step4.initAfterDiscountPrice0);
                    isFirstLoadPage_step4 = false;
                } else {
                    $("#ListResult2").load(url, datas, editdiscount_step4.initAfterDiscountPrice);
                }
            },
            
            // 打开模式
            model_open: function() {
                $("#discountAll").removeAttr("disabled");
                $("#btn_setDiscountAll").removeAttr("disabled");

                $("input[id^=discount_]").each(function() {
                    $(this).removeAttr("disabled");
                });

                $("a[id^=deljoin_]").each(function() {
                    $(this).show();
                });
            },
            
            // 关闭模式
            model_close: function() {
                $("#discountAll").attr("disabled", "disabled");
                $("#btn_setDiscountAll").attr("disabled", "disabled");

                $("input[id^=discount_]").each(function() {
                    $(this).attr("disabled", "disabled");
                });

                $("a[id^=deljoin_]").each(function() {
                    $(this).hide();
                });
            },
            
            // 按钮 修改
            updateInfo: function() {
                // 打开 第四步模块
                editdiscount_step4.model_open();
            },
            
            // 初始化折后价格(第一次加载页面时要关闭第四步模块)
            initAfterDiscountPrice0: function() {
                editdiscount_step4.initAfterDiscountPrice();
                editdiscount_step4.model_close();
            },
            
            // 初始化折后价格
            initAfterDiscountPrice: function() {
                $("input[id^=discount_]").each(function() {
                    editdiscount_step4.computeAfterDiscountPrice($(this).attr("houseId"));
                });
            },

            // 批量更新折扣
            batchUpdateDiscount: function() {
                var discount = $("#discountAll").val();
                var pattern = /^[1-9]$/;
                if (!pattern.test(discount)) {
                    alert("请输入正确折扣");
                    return;
                }

                $("input[id^=discount_]").each(function() {
                    var houseId = $(this).attr("houseId");
                    $("#discount_" + houseId).val(discount);
                    editdiscount_step4.computeAfterDiscountPrice(houseId);
                });
            },
            
            // 更新折扣
            updateDiscount: function(houseId) {
                var discount = $("#discount_" + houseId).val();
                var pattern = /^[1-9]$/;
                if ("" != discount && !pattern.test(discount)) {
                    alert("请输入正确折扣");
                    return;
                }
                editdiscount_step4.computeAfterDiscountPrice(houseId);
            },
            
            // 在折扣设置部分 不参加活动
            delJoinHouse_Discount: function(houseId) {
                editdiscount.delJoinHouse(houseId);
                $("#tr_" + houseId).remove();
                if ($("tr[id^=tr_]").length <= 0) {
                    $("#tbody_discount").append("<tr><td  colspan=\"9\">暂无数据！</td></tr>");
                }
            },

            // 计算折后价格
            computeAfterDiscountPrice: function(houseId) {
                var discount = $("#discount_" + houseId).val();
                if ("" == discount) {
                    $("#aftertotalprice_" + houseId).html(parseFloat($("#totalprice_" + houseId).html()).toFixed(2));
                    return;
                }

                $("#aftertotalprice_" + houseId).html(((discount / 10) * $("#totalprice_" + houseId).html()).toFixed(2));
            },
            
            // 验证 第四步
            valid_all: function() {
                var infos = $("input[id^=discount_]");
                if (0 == infos.length) {
                    alert("请选择房源");
                    return false;
                }

                var pattern = /^[1-9]{1}\d{0,15}$/;
                for (var i = 0; i < infos.length; i++) {
                    if (!pattern.test($(infos[i]).val())) {
                        alert("请填写正确的折扣信息");
                        return false;
                    }
                }

                return true;
            },

            // 获取所有参与活动的房源、折扣信息
            getAllJoinHouses: function() {
                var newHouseJson = [];
                var newHouseJsonString = "";
                for (var i = 0; i < json_Houses.length; i++) {
                    var discount = json_Houses[i].IsDel == 1 ? 0 : $("#discount_" + json_Houses[i].HouseId).val();
                    newHouseJsonString += "{\"HouseId\":\"" + json_Houses[i].HouseId + "\",\"Discount\":\"" + discount + "\",\"IsOriData\":\"" + json_Houses[i].IsOriData + "\",\"IsDel\":\"" + json_Houses[i].IsDel + "\"}";
                    if (i < json_Houses.length - 1) {
                        newHouseJsonString += ",";
                    }

                    newHouseJson.push({
                        HouseId: json_Houses[i].HouseId,
                        Discount: $("#discount_" + json_Houses[i].HouseId).val(),
                        IsOriData: json_Houses[i].IsOriData,
                        IsDel: json_Houses[i].IsDel
                    });
                }
                return ("[" + newHouseJsonString + "]");
            },

            // 保存信息
            saveInfo: function() {
                editdiscount_step4.model_close();

                if (!editdiscount_step2.valid_all()
                    | !editdiscount_step4.valid_all()) {
                    return;
                }

                $("#btn_submit").attr("style", "display: none;");

                var url = '<%= Auxiliary.Instance.NhManagerUrl %>discount/updatediscountdo.html';
                var data = {
                    id: '<%= Model.Id %>',
                    premisesid: $("#PremisesId").val(),
                    name: $("#Name").val(),
                    bond: $("#BondString").val(),
                    begintime: $("#begintime").val(),
                    endtime: $("#endtime").val(),
                    housesjsonstring: editdiscount_step4.getAllJoinHouses(),
                };

                $.post(url, data, function(msg) {
                    if (msg.suc) {
                        alert("保存成功");
                        return;
                    }
                    alert("保存失败");
                    $("#btn_submit").removeAttr("style");
                });
            }
        };
    </script>
</asp:Content>
