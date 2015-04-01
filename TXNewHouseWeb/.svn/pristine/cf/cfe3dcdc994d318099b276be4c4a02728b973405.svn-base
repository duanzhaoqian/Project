<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXModel.Web.HouseAndBuilding>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    房源管理-新房
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="shaibox mt15 clearFix">
            <div class="fl">
                筛选：</div>
            <div class="selbox mr10">
                <span class="pl10" id="pid">选择楼盘</span>
                <div class="sel_list w300" style="display: none;" id="PremisList">
                </div>
            </div>
            <div class="selbox mr10">
                <span class="pl10" id="bid">选择楼栋</span>
                <div class="sel_list w300" style="display: none;" id="BuildingId">
                </div>
            </div>
            <div class="selbox mr10">
                <span class="pl10" id="uid">选择单元</span>
                <div class="sel_list" style="display: none;" id="Unit">
                </div>
            </div>
            <div class="selbox mr10">
                <span class="pl10" id="fid">选择楼层</span>
                <div class="sel_list" style="display: none;" id="Floor">
                </div>
            </div>
            <div class="selbox mr10">
                <span class="pl10" id="sid">选择状态</span>
                <div class="sel_list" style="display: none;" id="SalesStatus">
                </div>
            </div>
            <input id="Search" type="button" class="bnt_serch fl" />
        </div>
        <div class="mt10 font12">
            <input id="chk_all" type="checkbox" class="bnt_checkbox" />全选 <a id="Release" href="javascript:void(0);"
                class="mr20 ml20" <%=ViewData["rid"].ToString() == "1" ? "style='color:#999'" : "" %>>
                发布</a> <a id="DelAll" href="javascript:void(0);" class="mr20" <%=ViewData["rid"].ToString() == "2" ? "style='color:#999'" : "" %>>
                    删除</a> <a onclick="Dialog.showDialog('url', '<%=TXCommons.GetConfig.BaseUrl%>NHouse/UpdateSate', '<%=TXCommons.GetConfig.ImgUrl%>images/loading.gif')"
                        href="#" class="mr20">更改销售状态</a>
        </div>
        <div class="choose mt15">
            <span>
                <%if (!string.IsNullOrEmpty(ViewData["rid"].ToString()))
                  {  %>
                <% string rid = ViewData["rid"].ToString();%>
                <%  if (rid == "1")
                    {%>
                <a onclick="SelectState(1,0,0,0,0,-1)" href="javascript:void(0)" class="cur">已发布</a>
                <a onclick="SelectState(0,0,0,0,0,-1)" href="javascript:void(0)">未发布</a>
                <%-- <a href="javascript:void(0)" onclick="SelectState(2,0,0,0,0,-1)">已删除</a>--%>
                <% }
                    else if (rid == "2")
                    { %>
                <a onclick="SelectState(1,0,0,0,0,-1)" href="javascript:void(0)">已发布</a> <a onclick="SelectState(0,0,0,0,0,-1)"
                    href="javascript:void(0)">未发布</a>
                <%-- <a onclick="SelectState(2,0,0,0,0,-1)" href="javascript:void(0)" class="cur">已删除</a>--%>
                <%  }
                    else
                    {%>
                <a onclick="SelectState(1,0,0,0,0,-1)" href="javascript:void(0)">已发布</a> <a onclick="SelectState(0,0,0,0,0,-1)"
                    href="javascript:void(0)" class="cur">未发布</a>
                <%--         <a onclick="SelectState(2,0,0,0,0,-1)" href="javascript:void(0)">已删除</a>--%>
                <%} %>
                <% }
                  else
                  { %>
                <a onclick="SelectState(1,0,0,0,0,-1)" href="javascript:void(0)" class="cur">已发布</a>
                <a onclick="SelectState(0,0,0,0,0,-1)" href="javascript:void(0)">未发布</a>
                <%--<a href="javascript:void(0)" onclick="SelectState(2,0,0,0,0,-1)">已删除</a>--%>
                <%} %>
            </span>
        </div>
        <div id="divAjaxData" class="mt15 clearFix" style="position: relative;">
            <%=Html.Partial("HouseTable", Model)%>
        </div>
    </div>
    <input type="hidden" id="Searchrid" value="0" />
    <input type="hidden" id="Searchpid" value="0" />
    <input type="hidden" id="Searchbid" value="0" />
    <input type="hidden" id="Searchuid" value="0" />
    <input type="hidden" id="Searchfid" value="0" />
    <input type="hidden" id="Searchsid" value="-1" />
    <input type="hidden" id="Searchorder" value="0" />
    <script language="javascript" type="text/javascript">
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
        //填充楼盘下拉框
        function GetPremisList(rid, pid, bid, uid, fid, sid) {
            $("#PremisList").empty();
            $.ajax({
                type: "POST",
                url: mainUrl + "NHouse/PremisList",
                success: function (data) {
                    $.each(data, function (i, item) {
                        if (i == 0) {
                            $("#pid").text(item.Name)
                        }
                        var stmp = pid == item.Id ? $("#pid").text(item.Name) : "";
                        $("<a  href=\"javascript:Go(" + rid + "," + item.Id + "," + bid + "," + uid + "," + fid + "," + sid + ")\"></a>").text(item.Name).appendTo($("#PremisList"));

                    });
                }
            });
        }
        //填充楼栋下拉框
        function getBuildingList(rid, pid, bid, uid, fid, sid) {
            $("#BuildingId").empty();
            if (pid != 0) {
                $.ajax({
                    type: "POST",
                    url: mainUrl + "NHouse/SearchBuildingList",
                    data: "PremisesId=" + pid,
                    success: function (data) {
                        $.each(data, function (i, item) {
                            var stmp = bid == item.Id ? $("#bid").text(item.Name) : "";
                            $("<a href=\"javascript:Go(" + rid + "," + pid + "," + item.Id + "," + uid + "," + fid + "," + sid + ")\"></a>").text(item.Name).appendTo($("#BuildingId"));
                        });
                    }
                });
            } else {
                $("<a href=\"javascript:Go(0,0,0,0,0,-1)\"></a>").text("选择楼栋").appendTo($("#BuildingId"));
            }
        }
        //填充单元下拉框
        function GetUnitList(rid, pid, bid, uid, fid, sid) {
            $("#Unit").empty();
            if (pid != 0 && bid != 0) {

                $.ajax({
                    type: "POST",
                    url: mainUrl + "NHouse/SearchUnitList",
                    data: "PremisesId=" + pid + "&BuildingId=" + bid,
                    success: function (data) {
                        $.each(data, function (i, item) {
                            var stmp = uid == item.Num ? $("#uid").text(item.Name) : "";
                            $("<a href=\"javascript:Go(" + rid + "," + pid + "," + bid + "," + item.Num + "," + fid + "," + sid + ")\"></a>").text(item.Name).appendTo($("#Unit"));
                        });
                    }
                });
            } else {
                $("<a href=\"javascript:Go(0,0,0,0,0,-1)\"></a>").text("选择单元").appendTo($("#Unit"));
            }
        }
        //填充楼层下拉框
        function GetFloor(rid, pid, bid, uid, fid, sid) {
            $("#Floor").empty();
            if (bid != 0) {
                $.ajax({
                    type: "POST",
                    url: mainUrl + "NHouse/TheFloor",
                    data: "BuildingId=" + bid,
                    success: function (data) {
                        $("<a href=\"javascript:Go(" + rid + "," + pid + "," + bid + "," + uid + ",0," + sid + ")\"></a>").text("选择楼层").appendTo($("#Floor"));
                        for (var i = 1; i <= data; i++) {
                            //var stmp = fid == i ? $("#fid").text("F" + i) : "";
                            if (fid == 0) {
                                $("#fid").text("选择楼层");
                            } else {
                                var stmp = fid == i ? $("#fid").text("F" + i) : "";
                            }
                            $("<a href=\"javascript:Go(" + rid + "," + pid + "," + bid + "," + uid + "," + i + "," + sid + ")\"></a>").text("F" + i).appendTo($("#Floor"));

                        }
                    },
                    complete: function (d, c) {
                        $.ajax({
                            type: "POST",
                            url: mainUrl + "NHouse/Underloor",
                            data: "BuildingId=" + bid,
                            success: function (data) {
                                for (var i = -1; i >= -data; i--) {
                                    var stmp = fid == i ? $("#fid").text("B" + Math.abs(i)) : "";
                                    $("<a href=\"javascript:Go(" + rid + "," + pid + "," + bid + "," + uid + "," + i + "," + sid + ")\"></a>").text("B" + Math.abs(i)).appendTo($("#Floor"));
                                }
                            }
                        });
                    }
                });

            } else {
                $("<a href=\"javascript:Go(0,0,0,0,0,-1)\"></a>").text("选择楼层").appendTo($("#Floor"));
            }
        }
        //房源状态
        var HouseSaleState = [{ id: -1, value: "请选择" }, { id: 0, value: "待售" }, { id: 1, value: "开发商保留" }, { id: 2, value: "在售" }, { id: 3, value: "已认购" }, { id: 4, value: "已签约" }, { id: 5, value: "已备案" }, { id: 6, value: "已办产权" }, { id: 7, value: "被限制" }, { id: 8, value: "拆迁安置" }, { id: 9, value: "售罄"}];
        //填充房源状态下拉框
        function GetSalesStatus(rid, pid, bid, uid, fid, sid) {
            $("#SalesStatus").empty();
            //房源销售状态列表
            $.each(HouseSaleState, function (i, item) {
                var stmp = sid == (item.id) ? $("#sid").text(item.value) : "";
                $("<a href=\"javascript:Go(" + rid + "," + pid + "," + bid + "," + uid + "," + fid + "," + (i - 1) + ")\"></a>").text(item.value).appendTo($("#SalesStatus"));
            });
        }
        //发布状态变更
        function SelectState(rid, pid, bid, uid, fid, sid) {
            $("#Searchrid").val(rid);
            funSearch();
            //            pid = $("#Searchpid").val();
            //            bid = $("#Searchbid").val();
            //            uid = $("#Searchbid").val();
            //            fid = $("#Searchfid").val();
            //            var canshu = { rid: rid, pid: pid, bid: bid, uid: uid, fid: fid, sid: sid, m: Math.random() };
            //            window.location = "<%=TXCommons.GetConfig.BaseUrl%>NHouse/List?";
            //            $.post("<%=TXCommons.GetConfig.BaseUrl%>NHouse/List", canshu, function(data) {

            //                $("#divAjaxData").html(data);
            //            });
        }
        //更改房源状态
        function UpadteSate() {
            var vals = "";
            $("input[name='chkItem']:checkbox:checked").each(function (i) {
                vals += (i == 0 ? $(this).val() : "," + $(this).val());
            });
            if (vals == "") {
                alert("请选择要发布的房源");
            } else {
                $.ajax({
                    url: mainUrl + "NHouse/UpdateIsRelease?callback=?",
                    type: "post",
                    cache: false,
                    dataType: "jsonp",
                    data: { Ids: vals },
                    success: function (result) {
                        if (result) {
                            if (result.value > 0) {
                                alert("发布成功");
                            } else {
                                alert("发布失败");
                            }
                            location.reload();
                        }
                    }
                });
            }
        }
        //点击删除
        $("#DelAll").bind("click", function () {
            var rid1 = $("#Searchrid").val();
            if (rid1 != 2) {
                if (confirm("确定删除勾选的房源信息吗？")) {
                    var vals = "";
                    $("input[name='chkItem']:checkbox:checked").each(function (i) {
                        vals += (i == 0 ? $(this).val() : "," + $(this).val());
                    });
                    if (vals == "") {
                        alert("请选择要删除的房源");
                    }
                    else {
                        $.ajax({
                            url: mainUrl + "NHouse/DeleteHouseByIds",
                            type: "post",
                            cache: false,
                            data: { Ids: vals },
                            success: function (data) {
                                if (data) {
                                    alert(data.message);
                                    if (data.result)
                                        location.reload();
                                }
                            }
                        });
                    }
                }
            }
        });
        //搜索
        function funSearch() {
            var ridTemp = $("#Searchrid").val(); //发布状态
            var pidTemp = $("#Searchpid").val();
            var bidTemp = $("#Searchbid").val();
            var uidTemp = $("#Searchuid").val();
            var fidTemp = $("#Searchfid").val();
            var sidTemp = $("#Searchsid").val();
            var orderTemp = $("#Searchorder").val(); //发布时间 
            window.location = "<%=TXCommons.GetConfig.BaseUrl%>NHouse/List?rid=" + ridTemp + "&pid=" + pidTemp + "&bid=" + bidTemp + "&uid=" + uidTemp + "&fid=" + fidTemp + "&sid=" + sidTemp + "&order=" + orderTemp;
        }
        function Go(rid, pid, bid, uid, fid, sid) {
            //楼层
            GetPremisList(rid, pid, bid, uid, fid, sid);

            //销售状态
            GetSalesStatus(rid, pid, bid, uid, fid, sid);

            //楼栋
            getBuildingList(rid, pid, bid, uid, fid, sid);

            //单元
            GetUnitList(rid, pid, bid, uid, fid, sid);

            //楼层
            GetFloor(rid, pid, bid, uid, fid, sid);

            $("#Searchrid").val(rid);
            $("#Searchpid").val(pid);
            $("#Searchbid").val(bid);
            $("#Searchuid").val(uid);
            $("#Searchfid").val(fid);
            $("#Searchsid").val(sid);
        }

        $(document).ready(function () {
            //下拉框显示和隐藏
            $(".selbox").click(function () {
                $(this).children(".sel_list").toggle();
            });
            $(".selbox .sel_list").mouseleave(function () {
                $(this).hide();
            });
            //全选/反选
            $('#chk_all').click(function () {
                //1.第一种方式，以下两行代码  
                //var checkedOfAll=$("#chk_all").prop("checked");   
                //$("input[name='chk_list']").prop("checked", checkedOfAll);   
                //2.第二种方式，以下代码，官方代码  
                $("input[name='chkItem']").prop("checked", $("#chk_all").prop("checked"));
            });
            $("#Search").bind("click", function () {
                //调用搜索方法
                funSearch();
                //                var rid = $("#Searchrid").val();
                //                var pid = $("#Searchpid").val();
                //                //有bug存在无法进行异步跳转
                //                $.post("<%=TXCommons.GetConfig.BaseUrl%>NHouse/List", { rid: rid1, pid: pid1, bid: $("#Searchbid").val(), uid: $("#Searchuid").val(), fid: $("#Searchfid").val(), sid: $("#Searchsid").val(), m: Math.random() }, function (data) {
                //                    $("#divAjaxData").html(data);
                //                });
            });
            //点击发布
            $("#Release").bind("click", function () {
                var rid1 = $("#Searchrid").val();
                if (rid1 == 1) {
                    return false;
                } else {
                    return UpadteSate();
                }
            });

            

            var rid = '<%=ViewData["rid"]%>';
            var pid = '<%=ViewData["pid"]%>';
            var bid = '<%=ViewData["bid"]%>';
            var uid = '<%=ViewData["uid"]%>';
            var fid = '<%=ViewData["fid"]%>';
            var sid = '<%=ViewData["sid"]%>';
            var order = '<%=ViewData["order"] %>';


            $("#Searchrid").val(rid);
            $("#Searchpid").val(pid);
            $("#Searchbid").val(bid);
            $("#Searchuid").val(uid);
            $("#Searchfid").val(fid);
            $("#Searchsid").val(sid);
            $("#Searchorder").val(order);

            //楼栋
            GetPremisList(rid, pid, bid, uid, fid, sid);

            //销售状态
            GetSalesStatus(rid, pid, bid, uid, fid, sid);

            //楼栋
            getBuildingList(rid, pid, bid, uid, fid, sid);

            //单元
            GetUnitList(rid, pid, bid, uid, fid, sid);

            //楼层
            GetFloor(rid, pid, bid, uid, fid, sid);

            //发布时间排序
            $("#order").bind("click", function () {
                if (order == "0")
                    $("#Searchorder").val(1);
                else
                    $("#Searchorder").val(0);
                funSearch();
            });
        });
    </script>
</asp:Content>
