<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<HouseInfoModel>>" %>

<%@ Import Namespace="TXModel.NHouseActivies.Discount" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-创建限时折扣活动
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script src="<%=TXCommons.GetConfig.GetFileUrl("js/my97datepicker/WdatePicker.js")%>"
        language="javascript" type="text/javascript"></script>--%>
    <script src="<%=TXCommons.GetConfig.BaseUrl + "js/my97datepicker/WdatePicker.js"%>"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftAjax.js")%>"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftMvcAjax.js")%>"
        type="text/javascript"></script>
    <div class="content">
        <h4 class="title_h4 mb10">
            <span>创建限时折扣活动</span><em class="ts_right"><a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/DiscountList">返回限时折扣活动列表
            </a></em>
        </h4>
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                第一步：选择楼盘</div>
            <div class="font14 fr mr35">
                <a href="javascript:void(0);" id="btn_update01" style="cursor: pointer">修改</a></div>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1">
            <tr>
                <th scope="row" width="100" align="right">
                    选择楼盘：
                </th>
                <td id="td_premises01">
                    <select class="select1">
                        <option value="-1">楼盘名称</option>
                        <%if (ViewData["Premise"] != null)
                          {
                              foreach (PremiseModel item in (ViewData["Premise"] as List<PremiseModel>))
                              {%>
                        <option value="<%=item.Id %>" data="<%=item.DeveloperId %>" data-cityid="<%=item.CityId %>">
                            <%=item.Name%></option>
                        <%}
                          }%>
                    </select>
                    <input type="button" value="确定" class="btn_w80 ml10" id="btn_sure01" />
                </td>
                <td id="td_premises02">
                    <span id="span_info02"></span>
                </td>
            </tr>
        </table>
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                第二步：设置基本信息</div>
            <div class="font14 fr mr35">
                <a id="btn_update2" style="cursor: pointer">修改</a></div>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1"
            id="tabStep2">
            <tr>
                <th scope="row" width="100" align="right">
                    活动名称：
                </th>
                <td id="td_activname01">
                    <input type="text" class="input_wauto w400" id="txtActivityName" maxlength="15" />
                </td>
                <%--隐藏/显示：活动名称--%>
                <td id="td_activname02">
                    <span id="span_activname02"></span>
                </td>
            </tr>
            <tr>
                <th scope="row" width="100" align="right">
                    活动保证金金额：
                </th>
                <td id="td_bond01">
                    <input type="text" class="input_wauto w180 mr10" id="txtBond" /><span class="ie_valign_5"
                        id="span_yuan">元</span>
                </td>
                <%--隐藏/显示：活动保证金金额--%>
                <td id="td_bond02" style="display: none">
                    <span id="span_bond02"></span><span class="ie_valign_5">元</span>
                </td>
            </tr>
            <tr>
                <th scope="row" width="100" align="right">
                    时间：
                </th>
                <td id="td_date01">
                    <span class="ie_valign_5">开始</span>
                    <input id="txtStart" type="text" class="input_wauto w150 ml5 mr20" onfocus="WdatePicker({dateFmt:'yyyy-M-d',minDate:'%y-%M-%d'})"
                        readonly="readonly" />
                    <span class="ie_valign_5">结束</span><input type="text" id="txtEnd" class="input_wauto w150 ml10"
                        onfocus="WdatePicker({dateFmt:'yyyy-M-d',minDate:'#F{$dp.$D(\'txtStart\',{d:0})}',maxDate:'#F{$dp.$D(\'txtStart\',{d:29})}' })"
                        readonly="readonly" />
                </td>
                <%--隐藏/显示：时间--%>
                <td id="td_date02" style="display: none">
                    <span class="ie_valign_5">开始</span><span id="span_start02"></span><span class="ie_valign_5">结束</span><span
                        id="span_end02"></span>
                </td>
            </tr>
            <tr>
                <td colspan="2" id="td_btn">
                    <input type="button" value="确定" class="btn_w80 ml20 mt15" id="btn_sure02" />
                </td>
            </tr>
        </table>
        <div class="mt15 btit clearFix" id="divthree">
            <div class="tit_font fl">
                第三步：选择房源</div>
            <div class="font14 fr mr35">
                <a id="a_update03" onclick="ShowCondition()" style="cursor: pointer">修改</a></div>
        </div>
        <div id="tabStep3">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1"
                id="table_condition">
                <tr>
                    <th scope="row" width="100" align="right">
                        <span class="font14">筛选：</span>
                    </th>
                    <td>
                        <div class="shaibox clearFix">
                            <div class="selbox mr10">
                                <span class="pl10" data-buidid="-1">--选择楼栋--</span>
                                <div style="display: none;" class="sel_list">
                                    <%--<a data="1">1号楼栋</a>
                   <a>2号楼栋</a>--%>
                                </div>
                            </div>
                            <div class="selbox mr10">
                                <span class="pl10" data-unitid="-1">--选择单元--</span>
                                <div style="display: none;" class="sel_list">
                                    <%-- <a data="1">1单元</a>
                   <a>2单元</a>--%>
                                </div>
                            </div>
                            <div class="selbox mr10">
                                <span class="pl10" data-floorid="-100">--选择楼层--</span>
                                <div style="display: none;" class="sel_list">
                                    <%--<a>1层</a>
                   <a>2层</a>--%>
                                </div>
                            </div>
                            <%--<div class="selbox mr10">
                                <span class="pl10" data-salsestate="-1">--选择状态--</span>
                                <div style="display: none;" class="sel_list">
                                    <a data-salsestate="-1">--选择状态--</a> <a data-salsestate="0">未售</a> <a data-salsestate="1">
                                        在售</a> <a data-salsestate="2">售罄</a>
                                </div>
                            </div>--%>
                            <input type="button" value="" class="bnt_serch fl" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="divpages">
                <% Html.RenderPartial("Common/Arranging", Model);%>
            </div>
        </div>
        <%--第四步：设置限时折扣--%>
        <div id="divfour" class="mt15 btit clearFix" style="display: none">
            <div class="tit_font fl">
                第四步：设置限时折扣</div>
            <div class="font14 fr mr35">
                <a id="btnupdate4" onclick="CompleteSet()" style="cursor: pointer">修改</a></div>
        </div>
        <table id="tzhekou1" width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1"
            style="display: none">
            <tr>
                <th scope="row" width="100" align="right">
                    批量设置折扣：
                </th>
                <td>
                    <input type="text" class="input_wauto mr10" id="txtzhehou" /><span class="ie_valign_5">折</span><input
                        type="button" value="确定" class="btn_w80 ml10" id="btnZhekou" onclick="SetZhekou()" />
                </td>
            </tr>
        </table>
        <table id="tzhekou2" width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1"
            style="display: none">
            <tr>
                <th scope="row" width="100" align="right">
                    批量设置折扣：
                </th>
                <td>
                    <span id="spanzhekou"></span><span class="ie_valign_5">折</span>
                </td>
            </tr>
        </table>
        <%--所选房源--%>
        <div id="divSelectHouse" class="mt15 clearFix" style="position: relative;">
        </div>
        <%--分页控件--%>
        <div class="fen">
            <div class="tar" id="divPager01">
            </div>
        </div>
        <%--完成提交按钮--%>
        <div class="tac">
            <input type="button" value="完成创建" class="btn_w97_green" onclick="Complete()" />
        </div>
    </div>
    <script type="text/javascript">

        var succ1, succ2;

        //控制第二步，第三步可编辑状态
        function disableStep2(value) {
            $('#tabStep2').find('input').attr("disabled", value);
        }

        function disableStep3(value) {
            $('#tabStep3').find('input').attr("disabled", value);
            $('#tabStep3').find("[type=checkbox]").attr("disabled", value);
            if (value) {
                $("#table_condition").hide();
                $("#div_alljoin").hide();
                $("#divpages").show();
                $("#divSelectHouse").hide();
                $("#divPager01").hide();
                $("#divfour").hide();
            } else {
                $("#table_condition").show();
                $("#div_alljoin").show();
            }
        }
        Tool = {
            DataFormt: function (oldstr) {
                return oldstr.replace(/-/g, "/");
            }
        };
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
        $(function () {
            jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Home/GetCurrentUserVipState", cache: false,
                success: function (data) {
                    if (data != 1) {
                        alert("您尚未通过身份认证，认证后方可操作。");
                        //window.location = mainUrl + "/home/identification";
                    }
                }
            });

            disableStep2(true);
            disableStep3(true);
            $(".btn_w97_green").attr("disabled", true);

            //确定:1
            $("#btn_sure01").click(function () {
                var option = $(".select1  option:selected");
                if (option.val() == "-1") {
                    $(this).next().remove();
                    $(this).after("&nbsp;&nbsp;&nbsp;&nbsp;<span style='color:red'>*请选择楼盘</span>");
                    return;
                }
                $("#td_premises02").show();
                $("#span_info02").html(option.html());
                $("#td_premises01").hide();
                disableStep2(false);
                QueryHouse();
                succ1 = true;
                disableStep3(!(succ1 && succ2));
            });
            //修改1
            $("#btn_update01").click(function () {
                $("#td_premises01").show();
                $("#td_premises02").hide();
                disableStep2(true);
                disableStep3(true);
                succ1 = false;
                $(".btn_w97_green").attr("disabled", true);
            });

            //确定:2
            $("#btn_sure02").click(function () {
                var sec = 0;
                var activity = $("#txtActivityName");
                var bond = $("#txtBond");
                var start = $("#txtStart");
                var end = $("#txtEnd");

                activity.next().remove();
                if ($.trim(activity.val()) == "") {
                    activity.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动名称不能为空</span>");
                    sec++;
                }
                end.next().remove();
                if (start.val() == "" || end.val() == "") {
                    end.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*日期不能为空</span>");
                    sec++;
                } else if (new Date(Tool.DataFormt(start.val())) > new Date(Tool.DataFormt(end.val()))) {
                    end.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*开始时间不能大于结束时间</span>");
                    sec++;
                }


                bond.next().next().remove();
                if (Verify.IsEmpty(bond.val())) {
                    sec++;
                    return Verify.Error("活动保证金不能为空", bond.next());
                }
                else if (!Verify.Integer(bond.val())) {
                    sec++;
                    return Verify.Error("最多可输入9位正数", bond.next());
                }
                if (sec > 0) return false;

                $("#td_activname02").show();
                $("#span_activname02").html(activity.val());
                $("#td_activname01").hide();

                $("#td_bond02").show();
                $("#span_bond02").html(bond.val());
                $("#td_bond01").hide();

                $("#td_date02").show();
                $("#span_start02").html(start.val());
                $("#span_end02").html(end.val());
                $("#td_date01").hide();
                $("#td_btn").hide();
                succ2 = true;
                disableStep3(!(succ1 && succ2));
            });
            //修改2
            $("#btn_update2").click(function () {
                $("#td_activname01").show();
                $("#td_activname02").hide();

                $("#td_bond01").show();
                $("#td_bond02").hide();

                $("#td_date01").show();
                $("#td_date02").hide();
                $("#td_btn").show();

                disableStep3(true);
                succ2 = false;
                $(".btn_w97_green").attr("disabled", true);
            });

            //楼盘切换
            $(".select1").change(function () {
                var premiseId = $(this).find("option:selected").val();
                jQuery.ajax({
                    type: "post",
                    async: false,
                    url: "/Activity/GetBuilding",
                    data: { PremisesId: premiseId },
                    dataType: "json",
                    cache: false,
                    success: function (data) {

                        RemoveBuildOption();
                        RemoveUnitOption();
                        RemoveFloorOption();

                        //首个a标签
                        var attr = "buidid";

                        //其他标签
                        $.each(data, function (index, building) {
                            var link = "<a data-buidid='" + building.Id + "' onclick='return GetUnit(" + premiseId + "," + building.Id + ",this)'>" + building.Name + "</a>";
                            $(".selbox").eq(0).find("div").append(link);
                        });
                    }
                });
            });

            //下拉选择
            $(".selbox").click(function (event) {
                $(this).children(".sel_list").toggle();
            });
            $(".selbox .sel_list").mouseleave(function () {
                $(this).hide();
            });
            $(".selbox div").eq(3).find("a").click(function () {
                var data = $(this).attr("data-salsestate");
                var span = $(this).parent().prev();
                var pindex = $(this).parent().parent().index();
                span.attr("data-salsestate", data);
                span.html($(this).html());
            });
            var saleState = $(".selbox span").eq(3).attr("data-salsestate"); //状态

            $(".bnt_serch").click(function () {
                QueryHouse();
            });
        });

        //获取房源列表
        function QueryHouse() {
            var premisesId = $(".select1 option:selected").val(); //楼盘Id
            var buildId = $(".selbox span").eq(0).attr("data-buidid"); //楼栋
            var unitId = $(".selbox span").eq(1).attr("data-unitid");  //单元
            var floorId = $(".selbox span").eq(2).attr("data-floorid");    //楼层
            //var saleState = $(".selbox span").eq(3).attr("data-salsestate");  //状态
            var saleState = 2;

            jQuery.ajax({ type: "get", async: false, url: "/Activity/Discount", data: { premisesId: premisesId, buildId: buildId, unitId: unitId, floorId: floorId, saleState: saleState, m: Math.random() }, cache: false,
                success: function (data) {
                    $("#divpages").html(data);
                    $('#divpages .btn_w80').val('确定');
                }
            });
        }

        //单元
        function GetUnit(premiseId, buildId, alink) {
            jQuery.ajax({ type: "post", async: false, url: "/Activity/GetUnit", data: { PremisesId: premiseId, BuildingId: buildId }, dataType: "json", cache: false,
                success: function (data) {

                    //移除原有标签
                    $(".selbox").eq(1).find("div").children().remove();

                    //首个a标签
                    var link = "<a data-unitid='-1'  onclick='return SelectOption(this,1)'>选择单元</a>";
                    $(".selbox").eq(1).find("div").append(link);

                    //其他标签
                    $.each(data, function (index, unit) {
                        link = "<a data-unitid='" + unit.Id + "' onclick='return GetFloor(" + premiseId + "," + buildId + "," + unit.Id + ",this)'>" + unit.Name + "</a>";
                        $(".selbox").eq(1).find("div").append(link);
                    });

                    //显示选中项
                    SelectOption(alink, 0);
                }
            });
            return false;
        }
        //楼层
        function GetFloor(premisesId, buildingId, unitId, alink) {
            jQuery.ajax({ type: "post", async: false, url: "/Activity/GetFloor", data: { PremisesId: premisesId, BuildingId: buildingId, UnitId: unitId }, dataType: "json", cache: false,
                success: function (data) {
                    $(".selbox").eq(2).find("div").children().remove();
                    //首个a标签
                    var link = "<a data-floorid='-100'  onclick='return SelectOption(this,2)'>选择楼层</a>";
                    $(".selbox").eq(2).find("div").append(link);
                    //其他标签
                    $.each(data, function (index, floor) {
                        link = "<a data-floorid='" + floor + "' onclick='return SelectOption(this,2)'>" + floor + "层</a>";
                        $(".selbox").eq(2).find("div").append(link);
                    });

                    //显示选中项
                    SelectOption(alink, 1);
                }
            });
            return false;
        }

    </script>
    <script type="text/javascript">
        //移除下拉数据
        function RemoveBuildOption() {
            $(".selbox").eq(0).find("div").children().remove();
            $(".selbox").eq(0).find("span").attr("data-buidid", "-1").html("选择楼栋");
            var firstlink = "<a data-buidid='-1' onclick='SelectOption(this,0);RemoveUnitOption();RemoveFloorOption();'>选择楼栋</a>";
            $(".selbox").eq(0).find("div").append(firstlink);
        }
        function RemoveUnitOption() {
            //移除原有标签
            $(".selbox").eq(1).find("div").children().remove();
            $(".selbox").eq(1).find("span").attr("data-unitid", "-1").html("选择单元");
            //首个a标签
            var firstlink = "<a data-unitid='-1'  onclick='SelectOption(this,1);RemoveFloorOption()'>选择单元</a>";
            $(".selbox").eq(1).find("div").append(firstlink);
        }
        function RemoveFloorOption() {
            $(".selbox").eq(2).find("div").children().remove();
            $(".selbox").eq(2).find("span").attr("data-floorid", "-100").html("选择楼层");
            //首个a标签
            var firstlink = "<a data-floorid='-100'  onclick='return SelectOption(this,2)'>选择楼层</a>";
            $(".selbox").eq(2).find("div").append(firstlink);
        }

        //显示选中的下拉项
        function SelectOption(alink, i) {
            var dataAttr = '';
            if (i == 0) {
                dataAttr = "data-buidid";
            }
            else if (i == 1) {
                dataAttr = "data-UnitId";
            } else if (i == 2) {
                dataAttr = "data-floorid";
            }
            var data = $(alink).attr(dataAttr);
            var span = $(alink).parent().prev();
            span.attr(dataAttr, data);
            span.html($(alink).html());
            return false;
        }

        function SetSelboxClick() {

            $(".selbox").bind("click", function (event) {
                $(this).children(".sel_list").toggle();
            });
            $(".selbox .sel_list").bind("mouseleave", function (event) {
                $(this).hide();
            });
            $(".selbox div").eq(3).find("a").bind("click", function (event) {
                var data = $(this).attr("data-salsestate");
                var span = $(this).parent().prev();
                var pindex = $(this).parent().parent().index();
                span.attr("data-salsestate", data);
                span.html($(this).html());
            });
        }
        function SetSelboxUnClick() {
            $(".selbox").unbind();
            $(".selbox .sel_list").unbind();
            $(".selbox div").eq(3).find("a").unbind();
        }


    </script>
    <script type="text/javascript">
        var join_Arr = new Array();
        var join_len = 0;

        var nojoin_Arr = new Array();
        var nojoin_len = 0;

        //全选/反选
        function CheckAll() {
            var checkInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") };
            var joinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" };

            var boxEvery = checkInfo.checkbox_every;
            var ischecked = checkInfo.checkbox_all.prop('checked') ? true : false;

            $(boxEvery).prop("checked", ischecked).attr("join-data", 0); //[每个选择]

            joinInfo.a_alljoin.html(joinInfo.join_msg).attr("join-data", 0); //[所有参加]

            joinInfo.a_singlejoin.html(joinInfo.join_msg); //[单个参加]
        }

        //单个选择
        function CheckSingle(checkbox) {
            var checkInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") };
            var joinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" };

            var check = $(checkbox).prop('checked');
            if (!check) {
                $(checkbox).attr("join-data", 0);
                $(checkbox).parent().nextAll().find(".alinkjoin").html(joinInfo.join_msg);

                var houseid = $(checkbox).val();
                //var index = $.inArray(houseid, jon_Arr);
                var index = IsInArray(join_Arr, houseid);
                if (index != -1) {
                    join_Arr.splice(index, 1);
                    join_len--;
                }
                //var index0 = $.inArray(houseid, nojoin_Arr);
                var index0 = IsInArray(nojoin_Arr, houseid);
                if (index0 != -1) {
                    nojoin_Arr.splice(index0, 1);
                    nojoin_len--;
                }

            } else if (check) {

                houseid = $(checkbox).val();
                //var index = $.inArray(houseid, nojoin_Arr);
                index = IsInArray(nojoin_Arr, houseid);
                if (index == -1) {
                    //nojoin_Arr[++nojoin_len] = houseid;
                    nojoin_Arr[nojoin_len++] = CreateHouse(checkbox);
                }
            }
        }
        function CreateHouse(checkbox) {
            var house = new Object();
            house.Houseid = $(checkbox).val();
            house.Developid = $(checkbox).attr("developid");
            house.Premid = $(checkbox).attr("premid");
            house.Buildid = $(checkbox).attr("buildid");
            house.HouseName = $(checkbox).attr("housename");
            house.Floor = $(checkbox).parent().nextAll().eq(0).html();
            house.HouseNo = $(checkbox).parent().nextAll().eq(1).html();
            house.Room = $(checkbox).parent().nextAll().eq(2).html();
            house.BuildingArea = $(checkbox).parent().nextAll().eq(3).html();
            house.SinglePrice = $(checkbox).parent().nextAll().eq(4).html();
            house.TotalPrice = $(checkbox).parent().nextAll().eq(5).html();
            house.SalesStatus = $(checkbox).parent().nextAll().eq(6).html();
            house.CreateTime = $(checkbox).parent().nextAll().eq(7).html();
            house.Join = $(checkbox).parent().nextAll().eq(8).html();
            house.Unitid = $(checkbox).attr("unitid");
            return house;
        }
        //判断元素是否存在二维数组
        function IsInArray(arr, val) {
            var index = -1;
            for (var k = 0; k < arr.length; k++) {
                var house = arr[k];
                if (house.Houseid == val) {
                    index = k;
                    break;
                }
            }
            return index;
        }
        //全参加
        function JoinAll() {
            var checkInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") };
            var joinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" };

            var count = 0;
            var join = joinInfo.a_alljoin.attr("join-data") == "0" ? false : true;
            checkInfo.checkbox_every.each(function () {
                if ($(this).prop('checked')) {
                    count++;
                }
            });
            if (count == 0) {
                alert("请选择房源");
                return;
            }

            //-->参加
            if (!join) {
                joinInfo.a_alljoin.attr("join-data", 1).html(joinInfo.no_join_msg);
                checkInfo.checkbox_every.each(function () {
                    if ($(this).prop('checked')) {
                        $(this).attr("join-data", 1);
                        $(this).parent().nextAll().find(".alinkjoin").html(joinInfo.no_join_msg);

                        //添加houseId到jon_Arr数组
                        var houseid = $(this).val();
                        //var index = $.inArray(houseid, jon_Arr);
                        var index = IsInArray(join_Arr, houseid);
                        if (index == -1) {
                            //jon_Arr[++join_len] = houseid;
                            //jon_Arr[++join_len] = CreateHouse(this);
                            join_Arr[join_len++] = CreateHouse(this);
                        }
                        //移除nojoin_Arr数组houseId
                        //var index0 = $.inArray(houseid, nojoin_Arr);
                        var index0 = IsInArray(nojoin_Arr, houseid);
                        if (index0 != -1) {
                            nojoin_Arr.splice(index0, 1);
                            nojoin_len--;
                        }
                    }
                });
            } else {//-->不参加
                joinInfo.a_alljoin.attr("join-data", 0).html(joinInfo.join_msg);
                checkInfo.checkbox_every.each(function () {
                    if ($(this).prop('checked')) {
                        $(this).attr("join-data", 0);
                        $(this).parent().nextAll().find(".alinkjoin").html(joinInfo.join_msg);

                        //将不参加的房源Id移除jon_Arr数组
                        var houseid = $(this).val();
                        //var index = $.inArray(houseid, nojoin_Arr);
                        var index = IsInArray(nojoin_Arr, houseid);
                        if (index == -1) {
                            // nojoin_Arr[++nojoin_len] = houseid;
                            //nojoin_Arr[++nojoin_len] = CreateHouse(this);
                            nojoin_Arr[nojoin_len++] = CreateHouse(this);
                        }
                        //移除nojoin_Arr数组houseId
                        //var index0 = $.inArray(houseid, jon_Arr);
                        var index0 = IsInArray(join_Arr, houseid);
                        if (index0 != -1) {
                            join_Arr.splice(index0, 1);
                            join_len--;
                        }
                    }
                });
            }
        }
        //单个参加
        function JoinSingle(alink) {
            var checkInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") };
            var joinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" };

            var join = $(alink).html();
            var checkbox = $(alink).parent().prevAll().find("input[name='box']");

            //            if (!checkbox.prop('checked')) {
            //                alert("请选择房源");
            //                return;
            //            } else {
            //[参加]
            if (join == joinInfo.join_msg) {
                checkbox.prop('checked', true);
                checkbox.attr("join-data", 1);
                $(alink).html(joinInfo.no_join_msg);

                houseid = $(checkbox).val();
                //var index = $.inArray(houseid, jon_Arr);
                index = IsInArray(join_Arr, houseid);
                if (index == -1) {
                    //jon_Arr[++join_len] = houseid;
                    //jon_Arr[++join_len] = CreateHouse(checkbox);
                    join_Arr[join_len++] = CreateHouse(checkbox);
                }

                //var index0 = $.inArray(houseid, nojoin_Arr);
                index0 = IsInArray(nojoin_Arr, houseid);
                if (index0 != -1) {
                    nojoin_Arr.splice(index0, 1);
                    nojoin_len--;
                }
            }
            //[不参加]
            else {
                checkbox.prop('checked', false);
                checkbox.attr("join-data", 0);
                $(alink).html(joinInfo.join_msg);

                var houseid = $(checkbox).val();
                //var index0 = $.inArray(houseid, jon_Arr);
                var index0 = IsInArray(join_Arr, houseid);
                if (index0 != -1) {
                    join_Arr.splice(index0, 1);
                    join_len--;
                }
                //var index = $.inArray(houseid, nojoin_Arr);
                var index = IsInArray(nojoin_Arr, houseid);
                if (index == -1) {
                    // nojoin_Arr[++nojoin_len] = houseid;
                    nojoin_Arr[nojoin_len++] = CreateHouse(checkbox);
                }
            }
            //            }
        }
        //验证
        var Verify = {
            IsEmpty: function (value) {
                return ($.trim(value) == "" || value == undefined || value == null);
            },
            Length: function (min, value, max) {
                return (min > value.length || value.length > max);
            },
            Integer: function (value) {
                var reg = /^[1-9]{1}\d{0,8}$/;
                return reg.test(value);
            },
            Error: function (msg, targetObj) {
                targetObj.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*" + msg + "</span>");
                return false;
            },
            Error2: function (msg, targetObj) {
                targetObj.html("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*" + msRequestAnimationFramesg + "</span>");
                return false;
            },
            IsDate: function (date) {
                var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
                return reg.test(date);
            },
            Compare: function (mindate, bigdate) {
                return (new Date(Tool.DataFormt(bigdate))) >= (new Date(Tool.DataFormt(mindate)));
            },
            IsDouble: function (value) {
                var reg = /(^\d{1}\.\d{1,2}$)|(^[1-9]{1}$)/;
                return reg.test(value);
            }
        };

        function Complete() {

            var active = {
                Name: $("#txtActivityName"),
                Bond: $("#txtBond"),
                Start: $("#txtStart"),
                End: $("#txtEnd")
            };

            active.Name.next().remove();
            active.Bond.next().next().remove();
            active.End.next().remove();


            if (Verify.IsEmpty(active.Name.val())) return Verify.Error("活动名称不能为空", active.Name);
            else if (Verify.Length(1, active.Name.val(), 15)) return Verify.Error("活动名称最多可输入15个汉字", active.Name);
            if (Verify.IsEmpty(active.Bond.val())) return Verify.Error("活动保证金金额不能为空", active.Bond.next());
            else if (!Verify.Integer(active.Bond.val())) return Verify.Error("最多可输入9位正数", active.Bond.next());


            if (!Verify.IsDate(active.Start.val())) return Verify.Error("日期不能为空", active.End);
            else if (!Verify.IsDate(active.End.val())) return Verify.Error("日期不能为空", active.End);
            else if (!Verify.Compare(active.Start.val(), active.End.val())) return Verify.Error("日期不正确", active.End);


            var house = "";
            for (var i = 0; i < join_Arr.length; i++) {
                var h = join_Arr[i];
                if (!isNaN(h.Houseid))
                    house += h.Houseid.toString() + ",";
            }
            house = house.substr(0, house.length - 1);
            if (house == "") { alert("请选择房源"); return; }

            var postHouse = new Array(), postAcitvity;
            var reg = new RegExp("^[1-9]{1}$");
            var msg_zk = new Array();
            $.each(pageHouse_Arr, function (idx, p) {
                if (!reg.test(p.zhekou)) {
                    msg_zk.push({ msg: "序号" + p.house.Houseid + "楼盘折扣输入不正确！", page: p.page });
                }
                postHouse.push(p.house.Houseid + '&' + p.house.Buildid + '&' + p.house.Unitid + '&' + p.zhekou);
            });

            if (msg_zk.length > 0) {
                goPage(msg_zk[0].page);
                alert(msg_zk[0].msg);
                return false;
            }

            postAcitvity = [$("#txtActivityName").val(), $("#txtBond").val(), $("#txtStart").val(), $("#txtEnd").val(), $(".select1 option:selected").attr('data'), $(".select1 option:selected").val(), $(".select1 option:selected").attr('data-cityid')];

            $.ajax({
                url: "/Activity/CreateDiscount",
                type: 'post',
                cache: false,
                async: false,
                data: { activity: postAcitvity.join('&'), house: postHouse.join(',') },
                success: function (data) {
                    if (data.sucess == "1") {
                        window.location = "<%=TXCommons.GetConfig.BaseUrl%>" + "Activity/DiscountList";
                    }
                    else {
                        alert("创建失败!");
                    }
                }
            });
        }
        //返回上一页：文本框是否选中
        function SetCheckBox() {
            $('#divpages .btn_w80').val('确定');
            disableStep3(!(succ1 && succ2));
            var checkInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") };
            var joinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" };
            checkInfo.checkbox_every.each(function () {
                var houseid = $(this).val();
                //var index = $.inArray(houseid, jon_Arr);
                var index = IsInArray(join_Arr, houseid);
                if (index != -1) {
                    $(this).prop('checked', true).attr("join-data", 1);
                    $(this).parent().nextAll().find(".alinkjoin").html(joinInfo.no_join_msg);
                } else {
                    //var index0 = $.inArray(houseid, nojoin_Arr);
                    var index0 = IsInArray(nojoin_Arr, houseid);
                    if (index0 != -1) {
                        $(this).prop('checked', true).attr("join-data", 0);
                        $(this).parent().nextAll().find(".alinkjoin").html(joinInfo.join_msg);
                    }
                }
            });
        }

        var PageHouse = function (house, page, zk) {
            this.house = house;
            this.page = page;
            this.zhekou = zk;
        };

        var pageHouse_Arr = new Array();

        //【完成设置】
        function CompleteSet() {
            totalCount = join_Arr.length;
            if (totalCount == 0) {
                alert("请选择要参加活动的房源！");
                return;
            }

            $("#divpages").hide();
            $("#divPager01").show();
            $("#divSelectHouse").show();
            $("#divPager01").html("");
            $("#divSelectHouse").html("");

            pageHouse_Arr = new Array();
            $.each(join_Arr, function (idx, d) {
                pageHouse_Arr.push(new PageHouse(d, 1, ''));
            });
            setPageHouse();
            var html = goPage(1);
            if (html != "") {
                $("#divfour").show();
                $("#tzhekou1").show();
                $("#tzhekou2").hide();
            }

            $(".btn_w97_green").attr("disabled", false);
        }
        //【修改】
        function ShowCondition() {
            $(".btn_w97_green").attr("disabled", true);
            if (succ1 && succ2) {
                $("#table_condition").show();
                $("#div_alljoin").show();
                $("#divpages").show();
                $("#divSelectHouse").hide();
                $("#divPager01").hide();
                $("#divfour").hide();
                $("#tzhekou1").hide();
                $("#tzhekou2").hide();
            }
        }

        //设置折扣
        function SetZhekou() {
            var zhekou = $("#txtzhehou");
            $(this).next().remove();

            if ($.trim(zhekou.val()) == "") {
                $(this).after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动折扣不能为空</span>");
                return;
            }

            var reg = new RegExp("^[0-9]{1}$");
            if (!reg.test($.trim(zhekou.val()))) {
                $(this).after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动折扣输入不正确</span>");
                return;
            }
            $("input[name='txtezhehou']").each(function (ex1, ex2) {
                $(this).val(zhekou.val());
                $(this).attr("readonly", "readonly");
            });
            $("#tzhekou1").hide();
            $("#tzhekou2").show();
            $("#spanzhekou").html(zhekou.val());

            $.each(pageHouse_Arr, function (idx, p) {
                p.zhekou = zhekou.val();
            });
            var html = goPage(1);
            if (html != "") {
                $("#divfour").show();
                $("#tzhekou1").show();
                $("#tzhekou2").hide();
            }
        }

        function SetZhekou2(sender) {
            var id = $(sender).attr('hid');
            $.each(pageHouse_Arr, function (idx, p) {
                if (p.house.Houseid == id) {
                    p.zhekou = $(sender).val();
                }
            });
        }

        /*-------------------------------JavaScript分页代码开始------------------------------
        js分页
        el:分页容器 
        count:总记录数 
        pageStep:每页显示多少个 
        pageNum:第几页 
        fnGo:分页跳转函数
        */
        var jsPage = function (el, count, pageStep, pageNum, fnGo) {
            var divPage = document.getElementById(el);
            this.getLink = function (fnGo, index, pageNum, text) {
                var s = '<a style=' + "cursor:pointer" + ' onclick="' + fnGo + '(' + index + ');" ';
                if (index == pageNum) {
                    s += 'class="hover" ';
                }
                text = text || index;
                s += '>' + text + '</a> ';
                return s;
            };
            //总页数
            var pageNumAll = Math.ceil(count / pageStep);
            if (pageNumAll == 0) {
                alert(el);
                divPage.innerHTML = '';
                return;
            }
            var itemNum = 5; //当前页左右两边显示个数
            pageNum = Math.max(pageNum, 1);
            pageNum = Math.min(pageNum, pageNumAll);
            var s = '';
            if (pageNum > 1) {
                s += this.getLink(fnGo, pageNum - 1, pageNum, '<<');
            } else {
                s += '<a class="no"><<</a> ';
            }
            var begin = 1;
            if (pageNum - itemNum > 1) {
                s += this.getLink(fnGo, 1, pageNum) + '... ';
                begin = pageNum - itemNum;
            }
            var end = Math.min(pageNumAll, begin + itemNum * 2);
            if (end == pageNumAll - 1) {
                end = pageNumAll;
            }
            for (var i = begin; i <= end; i++) {
                s += this.getLink(fnGo, i, pageNum);
            }
            if (end < pageNumAll) {
                s += '... ' + this.getLink(fnGo, pageNumAll, pageNum);
            }
            if (pageNum < pageNumAll) {
                s += this.getLink(fnGo, pageNum + 1, pageNum, '>>');
            } else {
                s += '<a>>></a> ';
            }
            return s;
        };

        var pageSzie = 2, totalCount = 0;
        var getTable = function (arrTh, arrTr) {
            var s = "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='tab1'>";
            s += '<tr>';
            for (var i = 0; i < arrTh.length; i++) {
                s += '<th>' + arrTh[i] + '</th>';
            }
            s += '</tr>';
            for (var i = 0; i < arrTr.length; i++) {
                var o = new Object();
                o = arrTr[i].house;
                if (o != undefined) {
                    var hehe = new Object();
                    if (arrTr[i].zhekou != undefined && arrTr[i].zhekou != '') {
                        hehe = parseFloat(arrTr[i].zhekou) * (parseFloat(o.TotalPrice.toString().replace("万元/套", " ")) / 10);
                    } else {
                        hehe = o.TotalPrice;
                    }
                    s += "<tr>";
                    s += "<td>" + o.HouseName + "</td>";
                    s += "<td>" + o.Floor + "</td>";
                    s += "<td>" + o.HouseNo + "</td>";
                    s += "<td>" + o.Room + "</td>";
                    s += "<td>" + o.BuildingArea + "</td>";
                    s += "<td>" + o.TotalPrice + "</td>";
                    s += "<td><input type='text' class='input_wauto w80 mr10' name='txtezhehou' developid='" + o.DeveloperId + "' premid='" + o.Premid + "' buildid='" + o.Buildid + "' unitid='" + o.Unitid + "'  hid='" + o.Houseid + "' value='" + arrTr[i].zhekou + "' onkeyup='SetZhekou2(this)' />折</td>";
                    s += "<td>" + hehe + "万元/套</td>";
                    s += "<td><a onclick='del(" + o.Houseid + ");'>删除</a></td>";
                    s += "</tr>";
                }
            }
            s += '</table>';
            return s;
        };
        function goPage(pageIndex) {
            var arrTh = ['房号', '楼层', '楼号', '户型', '建筑面积', '参考总价', '限时折扣', '折后价格', '操作'];
            var arrTr = new Array();

            $.each(pageHouse_Arr, function (idx, p) {
                if (p.page == pageIndex) {
                    arrTr.push(p);
                }
            });

            //            for (var i = (pageIndex - 1) * pageSzie; i < pageSzie * pageIndex; i++) {
            //                arrTr.push(pageHouse_Arr[i]);
            //            }
            var html = getTable(arrTh, arrTr);
            document.getElementById('divSelectHouse').innerHTML = html;
            document.getElementById('divPager01').innerHTML = jsPage('divPager01', totalCount, pageSzie, pageIndex, 'goPage');
            return html;
        }

        function setPageHouse() {
            totalCount = pageHouse_Arr.length;
            $.each(pageHouse_Arr, function (idx, h) {
                h.page = Math.floor((idx + pageSzie) / pageSzie);
            });
        }

        //点击删除按钮
        function del(id) {
            // $(o).parent().parent().remove();
            var obj, index;
            if (pageHouse_Arr.length == 1) {
                alert("至少要有一套房源参与活动！");
                return;
            }

            $.each(pageHouse_Arr, function (idx, h) {
                if (h.house.Houseid == id) {
                    obj = h;
                    index = idx;
                }
            });
            pageHouse_Arr.splice(index, 1);

            setPageHouse();
            var page = Math.ceil(totalCount / pageSzie);
            page = obj.page > page ? page : obj.page;
            var html = goPage(page);
            if (html != "") {
                $("#divfour").show();
                $("#tzhekou1").show();
            }
        }
    </script>
</asp:Content>
