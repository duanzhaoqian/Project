<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_SecKill_Edit>" %>

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
                <%=AdminPageInfo.ItemName %></a> <i>&gt;</i><a href="javascript:void(0);">修改<%= ViewData["houseinfo"] %></a></span>
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
                            <%= Html.DropDownListFor(model=>model.PremisesId, Model.Premisess,new {@disabled="disabled"}) %>
                        </td>
                    </tr>
                </table>
                <!-- ***** ***** ***** 第一步 End -->
            </div>
            <div class="data">
                <div class="dispose">
                    <h5>
                        第二步：选择房源 <span style="color: #999999">（只能选择一个房源）</span></h5>
                    <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="seckill_edit_step2.model_open()">
                        修改</a>
                </div>
                <div class="filterBar">
                    筛选：<%= Html.DropDownListFor(model=>model.BuildingId, Model.Buildings, new{@class="w100"}) %>
                    <select id="unitId" class="w100">
                        <option value="0">选择单元</option>
                    </select>
                    <select id="floor" class="w100">
                        <option value="0">选择楼层</option>
                    </select>
                    <%= Html.DropDownListFor(model=>model.SalesStatus, Model.SalesStatusList, new{@class="w100"}) %>
                    <input type="button" id="btnSearch" value="搜索" />
                </div>
                <div id="ListResult">
                </div>
            </div>
            <div class="paging">
                <p id="pagebar" class="pubPage" style="border: none 0">
                    <!-- 这里显示分页 -->
                </p>
            </div>
            <input type="hidden" id="hid_ActivityHouseId" value="<%= Model.HouseId %>" />
            <input type="hidden" id="hid_ActivityId" value="<%= Model.ActivityId %>" />
            <input type="button" id="btn_SaveHouse" value="完成设置" />
            <!-- ***** ***** ***** 第二步 End -->
            <div class="dispose">
                <h5>
                    第三步：设置基本信息</h5>
                <a href="javascript: void(0);" style="float: right; margin-right: 15px;" onclick="seckill_edit_step3.model_open();">
                    修改</a>
            </div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="100">
                            秒杀价格：
                        </th>
                        <td>
                            <%= Html.TextBoxFor(model => model.BidPriceString, new { @maxlength = 9 })%>&nbsp;&nbsp;万元
                            <span id="err_BidPrice"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            活动保证金金额：
                        </th>
                        <td>
                            <%= Html.TextBoxFor(model => model.BondString, new { @maxlength = 9 })%>&nbsp;&nbsp;元
                            <span id="err_Bond"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            活动时间：
                        </th>
                        <td>
                            开始&nbsp;&nbsp;<input type="text" id="begintime" value="<%= Model.BeginTimeString %>" /><span
                                id="err_begintime"></span>&nbsp;&nbsp; 结束&nbsp;&nbsp;<input type="text" id="endtime"
                                    value="<%= Model.EndTimeString %>" /><span id="err_endtime"></span>
                        </td>
                    </tr>
                </table>
            </div>
            <input type="button" id="btn_saveBlock1" value="完成创建" />
            <!-- ***** ***** ***** 第三步 End -->
        </div>
    </div>
    <script type="text/javascript">

        var isFirstLoadPage_step2 = true; // 首次加载页面标志

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
            var url = '<%=Url.SiteUrl().SecKill_Search("editactivity_searchresult") %>';
            var datas = {
                PremisesId: $("#PremisesId").val(),
                BuildingId: $("#BuildingId").val(),
                UnitNum: $("#unitId").val(),
                Floor: $("#floor").val(),
                SalesStatus: $("#SalesStatus").val(),
                ActivityId: $("#hid_ActivityId").val(),
                pageindex: pageIndex,
                pagesize: opts.items_per_page,
                ram: Math.random()
            };
            if (isFirstLoadPage_step2) {
                $("#ListResult").load(url, datas, seckill_edit_step2.initJoinSign2);
                isFirstLoadPage_step2 = false;
            } else {
                $("#ListResult").load(url, datas, seckill_edit_step2.initJoinSign);
            }
        }

        $(function () {
            $("#BuildingId").bind("change", seckill_edit_step2.evt_change_building);
            $("#btnSearch").bind("click", seckill_edit_step2.loadPageBar);
            $("#btn_SaveHouse").bind("click", seckill_edit_step2.model_close);
            $("#begintime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd HH:mm', readOnly: true }); });
            $("#endtime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd HH:mm', readOnly: true, minDate:'#F{$dp.$D(\'begintime\',{H:1});}' }); });
            $("#btn_saveBlock1").bind("click", seckill_edit_step3.btnSave);

            seckill_edit_step2.loadPageBar();

            seckill_edit_step3.model_close();
        });

        var seckill_edit = {
            icon_suc: "win ml10",
            icon_err: "lose ml10",
            err_content: "请填写"
        };

        // 第二步
        var seckill_edit_step2 = {
            // 联动楼栋
            evt_change_building: function () {
                clearListItems($("#unitId"));
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetUnitsByBuildingId") %>?buildingId=' + $("#BuildingId").val(), $("#unitId"));

                clearListItems($("#floor"));
                seckill_edit_step2.edLoadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetFloorsByBuildingId") %>?id=' + $("#BuildingId").val(), $("#floor"));
            },

            // 加载下拉列表
            edLoadGeographyItems: function (url, container, onComplete) {
                $.getJSON(url, function (response) {
                    if (response && response.success && container) {
                        seckill_edit_step2.edFillListItems(container, response.items);
                        if (onComplete) {
                            onComplete(container);
                        }
                    }
                });
            },

            // 辅助(加载下拉列表)
            edFillListItems: function (container, dataItems) {
                if (dataItems) {
                    $.each(dataItems, function () {
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
            loadPageBar: function () {
                var url = '<%=Url.SiteUrl().SecKill_Search("editactivity_searchcount") %>';
                var datas = {
                    PremisesId: $("#PremisesId").val(),
                    BuildingId: $("#BuildingId").val(),
                    UnitNum: $("#unitId").val(),
                    Floor: $("#floor").val(),
                    ActivityId: $("#hid_ActivityId").val(),
                    SalesStatus: $("#SalesStatus").val(),
                    ram: Math.random()
                };
                $.post(url, datas, function (data, textStatus) {
                    $("#pagebar").pagination(data.result, opts);
                });
            },

            // 初始化参与活动的房源
            initJoinSign: function () {
                $("a[id^=join_]").each(function () {
                    if ($("#hid_ActivityHouseId").val() == $(this).attr("value")) {
                        $(this).html("不参加");
                    } else {
                        $(this).html("参加");
                    }
                });
            },

            // 初始化参与活动的房源2
            initJoinSign2: function () {
                seckill_edit_step2.initJoinSign();
                seckill_edit_step2.model_close();
            },

            // 参与活动
            join_activity: function (id) {
                if (id != $("#hid_ActivityHouseId").val()) {
                    $("#hid_ActivityHouseId").val(id);
                    seckill_edit_step2.initJoinSign();
                }
            },
            
            // 验证房源
            valid_house: function() {
                if ("" == $("#hid_ActivityHouseId").val()) {
                    alert("请选择参与活动的房源");
                    return false;
                }
                return true;
            },

            // 打开模式
            model_open: function () {

                seckill_edit_step3.model_close();

                $("#BuildingId").removeAttr("disabled");
                $("#unitId").removeAttr("disabled");
                $("#floor").removeAttr("disabled");
                $("#SalesStatus").removeAttr("disabled");
                $("#btnSearch").removeAttr("disabled");

                seckill_edit_step2.initJoinSign();

                $("#pagebar").show();

                $("#btn_SaveHouse").removeAttr("disabled");
            },

            // 关闭模式
            model_close: function () {
                $("#BuildingId").attr("disabled", "disabled");
                $("#unitId").attr("disabled", "disabled");
                $("#floor").attr("disabled", "disabled");
                $("#SalesStatus").attr("disabled", "disabled");
                $("#btnSearch").attr("disabled", "disabled");

                $("a[id^=join_]").each(function () {
                    $(this).html("");
                });

                $("#pagebar").hide();

                $("#btn_SaveHouse").attr("disabled", "disabled");
            }
        };

        // 第三步
        var seckill_edit_step3 = {

            // 秒杀价格
            valid_bidprice: function () {
                var eerr = $("#err_BidPrice");
                var value = $.trim($("#BidPriceString").val());
                if ("" == value) {
                    eerr.attr("class", seckill_edit.icon_err);
                    eerr.html(seckill_edit.err_content);
                    return false;
                }

                var pattern = /^\d{1,9}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", seckill_edit.icon_err);
                    eerr.html("请输入正确的9位正数");
                    return false;
                }

                if (value < 1) {
                    eerr.attr("class", seckill_edit.icon_err);
                    eerr.html("请输入正确的9位正数");
                    return false;
                }

                eerr.attr("class", seckill_edit.icon_suc);
                eerr.html("");
                return true;
            },

            // 活动保证金金额
            valid_bond: function () {
                var eerr = $("#err_Bond");
                var value = $.trim($("#BondString").val());
                if ("" == value) {
                    eerr.attr("class", seckill_edit.icon_err);
                    eerr.html(seckill_edit.err_content);
                    return false;
                }

                var pattern = /^\d{1,9}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", seckill_edit.icon_err);
                    eerr.html("请输入9位正数");
                    return false;
                }

                if (value < 1) {
                    eerr.attr("class", seckill_edit.icon_err);
                    eerr.html("请输入9位正数");
                    return false;
                }

                eerr.attr("class", seckill_edit.icon_suc);
                eerr.html("");
                return true;
            },

            // 开始时间
            valid_begintime: function () {
                var eerr = $("#err_begintime");
                var value = $.trim($("#begintime").val());
                if ("" == value) {
                    eerr.attr("class", seckill_edit.icon_err);
                    eerr.html(seckill_edit.err_content);
                    return false;
                }

                eerr.attr("class", seckill_edit.icon_suc);
                eerr.html("");
                return true;
            },

            // 结束时间
            valid_endtime: function () {
                var eerr = $("#err_endtime");
                var value = $.trim($("#endtime").val());
                if ("" == value) {
                    eerr.attr("class", seckill_edit.icon_err);
                    eerr.html(seckill_edit.err_content);
                    return false;
                }

                if (seckill_edit_step3.valid_begintime()) {
                    var date1 = new Date($("#begintime").val().replace(new RegExp("-", "g"), "/"));
                    var date2 = new Date($("#endtime").val().replace(new RegExp("-", "g"), "/"));
                    if (date2 < date1) {
                        eerr.attr("class", seckill_edit.icon_err);
                        eerr.html("活动开始时间要小于结束时间");
                        return false;
                    }
                }

                eerr.attr("class", seckill_edit.icon_suc);
                eerr.html("");
                return true;
            },

            // 打开模式
            model_open: function () {
                seckill_edit_step2.model_close();

                $("#BidPriceString").removeAttr("disabled");
                $("#BondString").removeAttr("disabled");
                $("#begintime").removeAttr("disabled");
                $("#endtime").removeAttr("disabled");
            },

            // 关闭模式
            model_close: function () {
                $("#BidPriceString").attr("disabled", "disabled");
                $("#BondString").attr("disabled", "disabled");
                $("#begintime").attr("disabled", "disabled");
                $("#endtime").attr("disabled", "disabled");
            },

            // 保存活动信息
            btnSave: function () {
                if ((!seckill_edit_step2.valid_house())
                    | (!seckill_edit_step3.valid_bidprice())
                    | (!seckill_edit_step3.valid_bond())
                    | (!seckill_edit_step3.valid_begintime())
                    | (!seckill_edit_step3.valid_endtime())) {
                    return;
                }

                var url = '<%= Url.SiteUrl().SecKill_Search("editactivity_save") %>';
                var datas = {
                    ActivityId: $("#hid_ActivityId").val(),
                    HouseId: $("#hid_ActivityHouseId").val(),
                    BidPrice: $("#BidPriceString").val(),
                    Bond: $("#BondString").val(),
                    BeginTime: $("#begintime").val(),
                    EndTime: $("#endtime").val(),
                    ram: Math.random()
                };

                $.post(url, datas, function (msg) {
                    if (!msg.suc) {
                        alert(msg.msg);
                    } else {
                        alert(msg.msg);
                        window.location.href = '<%= ViewData["backurl"] %>';
                    }
                });
            }
        };
    </script>
</asp:Content>