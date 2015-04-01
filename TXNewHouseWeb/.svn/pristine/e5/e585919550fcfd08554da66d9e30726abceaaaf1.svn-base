<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<HouseInfoModel>>" %>
<%@ Import Namespace="TXModel.NHouseActivies.Discount" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {


        //下拉框显示和隐藏
        $(".selbox").click(function () {
            $(this).children(".sel_list").toggle();
        })
        $(".selbox .sel_list").mouseleave(function () {
            $(this).hide();
        })
        //下拉列表选择
        $(".selbox .sel_list a").click(function () {
            var data = $(this).attr("data");
            var span = $(this).parent().prev();
            var pindex = $(this).parent().parent().index();
            span.attr("data", data);
            span.html($(this).html());
        })
        //选择房源
        var SelectHouse = {
            property: $(".select1"),
            Premises: {
                span: $(".selbox").eq(0).find("span"),
                alink: $(".selbox").eq(0).find(".sel_list a")
            },
            Building: {
                span: $(".selbox").eq(0).find("span"),
                div: $(".selbox").eq(0).find("div")
            },
            Unit: {
                span: $(".selbox").eq(1).find("span"),
                div: $(".selbox").eq(1).find("div")
            },
            Floor: {
                span: $(".selbox").eq(2).find("span"),
                div: $(".selbox").eq(2).find("div")
            },
            Status: {
                span: $(".selbox").eq(3).find("span"),
                div: $(".selbox").eq(3).find("div")
            },
            BtnSearch: $(".bnt_serch"),
            BtnUpdate: $(".select1")
        }

        //选择楼盘
        $(".select1").change(function () {
            var option = $(".select1  option:selected");
            if (option.val() == "-1")
                var PremisesId = option.attr("data"); //楼盘Id

            $.getJSON("/Activity/QueryBuilding", { PremisesId: PremisesId }, function (data) {
                $(".selbox").eq(0).find("div").children().remove();
                $(".selbox").eq(0).find("div").append("<a data='-1'>--选择楼栋--</a>");
                $.each(data, function (Id, Name) {
                    $(".selbox").eq(0).find("div").append("<a data='" + Id + "'>" + Name + "</a>");
                })

                $(".selbox .sel_list").eq(0).find("a").click(function () {
                    var BuildingId = $(this).attr("data"); //楼栋Id
                    var span = $(this).parent().prev();
                    span.attr("data", BuildingId);
                    span.html($(this).html());

                    GetUnitByBuildingId(PremisesId, BuildingId);
                })
            });
        })
        //确定:1
        $("#btn1").click(function () {
            var option = $(".select1  option:selected");
            if (option.val() == "-1") {
                $(this).next().remove();
                $(this).after("&nbsp;&nbsp;&nbsp;&nbsp;<span style='color:red'>*请选择楼盘</span>");
                return;
            }
            $("#tdloupan2").show();
            $("#spanloupan").html(option.html());
            $("#tdloupan1").hide();
        })
        //确定:2
        $("#btn2").click(function () {
            var sec = 0;
            var activity = $("#txtactivityname");
            var bond = $("#txtBond");
            var start = $("#txtBeginTime");
            var end = $("#txtEndTime");

            activity.next().remove();
            if ($.trim(activity.val()) == "") {
                activity.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动名称不能为空</span>");
                sec++;
            }
            end.next().remove();
            if (start.val() == "" || end.val() == "") {
                end.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*日期不能为空</span>");
                sec++;
            }
            bond.next().remove();
            if ($.trim(bond.val()) == "") {
                bond.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动保证金金额不能为空</span>");
                sec++; return;
            }
            bond.next().remove();
            var reg = new RegExp("^[0-9]+\.{0,1}[0-9]{0,2}$");
            if (!reg.test($.trim(bond.val()))) {
                bond.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动保证金金额输入不正确</span>");
                sec++;
            }
            if (sec > 0) return;

            $("#tdinfo01").show();
            $("#spanactivityname").html(activity.val());
            $("#tdinfo0").hide();

            $("#tdinfo10").show();
            $("#spanbond").html(bond.val());
            $("#tdinfo1").hide();

            $("#tdinfo20").show();
            $("#spanstart").html(start.val());
            $("#spanend").html(end.val());
            $("#tdinfo2").hide();
        })
        //修改1
        $("#btnupdate1").click(function () {
            $("#tdloupan1").show();
            $("#tdloupan2").hide();
        })
        //修改2
        $("#btnupdate2").click(function () {
            $("#tdinfo0").show();
            $("#tdinfo01").hide();

            $("#tdinfo1").show();
            $("#tdinfo10").hide();

            $("#tdinfo2").show();
            $("#tdinfo20").hide();
        })
        //修改3
        $("#btnupdate3").click(function () {
            $("#divpages").show();
            $("#bu-31").show();
            $("#bu-32").show();
            $("#bu-33").show();
            $("#bu-34").show();
            $("#btnCompleteok").show();
        })
        //选择楼栋
        function GetUnitByBuildingId(PremisesId, BuildingId) {
            $.getJSON("/Activity/QueryUnit", { PremisesId: PremisesId, BuildingId: BuildingId }, function (data) {
                $(".selbox").eq(1).find("div").children().remove();
                $(".selbox").eq(1).find("div").append("<a data='-1'>--选择单元--</a>");
                $.each(data, function (Id, Name) {
                    $(".selbox").eq(1).find("div").append("<a data='" + Id + "'>" + Name + "</a>");
                })
                $(".selbox .sel_list").eq(1).find("a").click(function () {
                    var UnitId = $(this).attr("data");
                    var span = $(this).parent().prev();
                    var pindex = $(this).parent().parent().index();
                    span.attr("data", UnitId);
                    span.html($(this).html());
                    GetFloor(PremisesId, BuildingId, UnitId);
                })
            });
        }
        //选择楼层
        function GetFloor(PremisesId, BuildingId, UnitId) {
            $.getJSON("/Activity/QueryFloor", { PremisesId: PremisesId, BuildingId: BuildingId, UnitId: UnitId }, function (data) {
                $(".selbox").eq(2).find("div").children().remove();
                $(".selbox").eq(2).find("div").append("<a data='-100'>--选择楼层--</a>");
                for (var i = 0; i < data; i++) {
                    $(".selbox").eq(2).find("div").append("<a data='" + i + "'>" + (i + 1) + "层" + "</a>");
                }
                $(".selbox .sel_list").eq(2).find("a").click(function () {
                    var data = $(this).attr("data");
                    var span = $(this).parent().prev();
                    var pindex = $(this).parent().parent().index();
                    span.attr("data", data);
                    span.html($(this).html());
                })
            });
        }
        //单元
        $(".selbox").eq(1).find(".sel_list a").click(function () {
            var data = $(this).attr("data");
            $.get("/Activity/QueryFloor", { PremisesId: 3, BuildingId: 1, UnitId: 1 }, function (redata) {
                var c = 0; c = redata;
                $(".selbox").eq(2).find("div").children().remove();
                for (var i = 1; i < c + 1; i++) {
                    $(".selbox").eq(2).find("div").append("<a data='" + i + "'>" + i + "层</a>");
                }
            });
            $(".selbox .sel_list a").click(function () {
                var data = $(this).attr("data");
                var span = $(this).parent().prev();
                var pindex = $(this).parent().parent().index();
                span.attr("data", data);
                span.html($(this).html());
            })
        })
        //搜索
        SelectHouse.BtnSearch.click(function () {
            //            var d = $(".select1  option:selected").val(); //开发商Id
            var p = $(".select1  option:selected").attr("data"); //楼盘Id
            var b = $(".selbox span").eq(0).attr("data"); //楼栋
            var u = $(".selbox span").eq(1).attr("data");  //单元
            var f = $(".selbox span").eq(2).attr("data");    //楼层
            var s = $(".selbox span").eq(3).attr("data");  //状态

            $.getJSON("/Activity/Create", { p: p, b: b, u: u, f: f, s: s }, function (redata) { });
        })

        //设置折扣
        $("#btnZhekou").click(function () {
            var zhekou = $("#txtzhehou");
            $(this).next().remove();

            if ($.trim(zhekou.val()) == "") {
                $(this).after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动折扣不能为空</span>");
                return;
            }

            var reg = new RegExp("^[0-9]+\.{0,1}[0-9]{0,2}$");
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
        })
        $("#btnupdate4").click(function () {
            $("#tzhekou1").show();
            $("#tzhekou2").hide();
        })
        //点击折扣多选框
        $("[name ='boxzhekou']:checkbox").click(function () {
            var ischecked = $(this).prop('checked') ? 1 : 0;
            $(this).parent().nextAll().eq(5).find("input[name='txtezhehou']").attr("a", ischecked);
        })
        //完成创建
        $("#btnComplete2").click(function () {


            var option = $(".select1  option:selected");
            if (option.val() == "-1") {
                $("#btn1").next().remove();
                $("#btn1").after("&nbsp;&nbsp;&nbsp;&nbsp;<span style='color:red'>*请选择楼盘</span>");
                return;
            }
            var sec = 0;
            var activity = $("#txtactivityname");
            var bond = $("#txtBond");
            var start = $("#txtBeginTime");
            var end = $("#txtEndTime");

            activity.next().remove();
            if ($.trim(activity.val()) == "") {
                activity.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动名称不能为空</span>");
                sec++;
            }
            end.next().remove();
            if (start.val() == "" || end.val() == "") {
                end.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*日期不能为空</span>");
                sec++;
            }
            bond.next().remove();
            if ($.trim(bond.val()) == "") {
                bond.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动保证金金额不能为空</span>");
                sec++; return;
            }
            bond.next().remove();
            var reg = new RegExp("^[0-9]+\.{0,1}[0-9]{0,2}$");
            if (!reg.test($.trim(bond.val()))) {
                bond.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*活动保证金金额输入不正确</span>");
                sec++;
            }
            if (sec > 0) return;


            var acivityName = $(".input_wauto").val();  //活动名称
            var Bond = $("#txtBond").val();             //活动保证金
            var beginTime = $("#txtBeginTime").val();   //开始时间
            var EndTime = $("#txtEndTime").val();       //结束时间
            var activity = acivityName + "," + Bond + "," + beginTime + "," + EndTime;

            var house = "";
            $(":input[name='txtezhehou']").each(function () {
                var developid = $(this).attr("developid");
                var premid = $(this).attr("premid");
                var buildid = $(this).attr("buildid");
                var discount = $(this).val();
                var hid = $(this).attr("hid");
                house += developid + "," + premid + "," + buildid + "," + hid + "," + discount + "|";
            })
            $.ajax({
                url: "/Activity/CreateActivity",
                type: 'post',
                cache: false,
                dataType: "json",
                async: false,
                data: { activity: activity, house: house },
                success: function (data) {
                    if (data.sucess == "1") {
                        alert("创建成功!");
                        location.reload();
                    }
                    else {
                        alert("创建失败!");
                    }
                }
            });
        })

        //全选/反选
        $("#btnCheckAll").bind("click", function () {
            var allbox = "[name = box]:checkbox";
            var ischecked = $('#btnCheckAll').prop('checked') ? true : false;
            if (ischecked) {
                $(allbox).prop("checked", true);
            } else {
                $(allbox).prop("checked", false);
                $(allbox).each(function (ex1, ex2) {
                    $(this).parent().nextAll().find(".alinkjoin").attr('join', 0).html("参加");
                    $("#alljoin").attr('join', 0).html("参加");
                })
            }
        });
        //单个选择
        $("[name ='box']:checkbox").click(function () {
            var ischecked = $(this).prop('checked') ? 1 : 0;
            if (!ischecked) {
                $(this).parent().nextAll().find(".alinkjoin").attr('join', 0).html("参加");
            }
        })

        //全部参加
        var joinArr = new Array(); var i = 0;
        var nojoinArr = new Array(); var j = 0;

        //全参加/不参加
        $("#alljoin").click(function () {
            //1：设置自己
            var isjoin = $(this).attr("join") == "0" ? "1" : "0";
            var jointxt = $(this).attr("join") == "0" ? "不参加" : "参加";
            $(this).attr('join', isjoin).html(jointxt);

            if (isjoin == "1") {//--全参加
                //2：设置a标签
                $("input[name='box']:checkbox").each(function (ex1, ex2) {

                    //设置（参加/不参加）
                    $(this).parent().nextAll().find(".alinkjoin").attr('join', isjoin).html(jointxt);


                    if ($(this).prop('checked')) {
                        var exist = false;
                        var houseid = $(this).val(); //房源Id
                        var nodes = $(this).parent().nextAll();

                        for (var k = 0; k < joinArr.length; k++) {
                            var house = joinArr[k];
                            if (house.houseid == houseid) {
                                exist = true;
                                break;
                            }
                        }
                        if (!exist) {
                            var o = new Object();
                            o.houseid = houseid;
                            o.developid = $(this).attr("developid");
                            o.premid = $(this).attr("premid");
                            o.buildid = $(this).attr("buildid");
                            o.Floor = nodes.eq(0).html();
                            o.HouseNo = nodes.eq(1).html();
                            o.Room = nodes.eq(2).html();
                            o.BuildingArea = nodes.eq(3).html();
                            o.SinglePrice = nodes.eq(4).html();
                            o.TotalPrice = nodes.eq(5).html();
                            o.SalesStatus = nodes.eq(6).html();
                            o.CreateTime = nodes.eq(7).html();
                            o.Join = nodes.eq(8).html();
                            joinArr[i] = o; i++;
                        }
                    }
                })
            } else if (isjoin == "0") {

                $("input[name='box']:checkbox").each(function (ex1, ex2) {

                    if ($(this).prop('checked')) {
                        $(this).parent().nextAll().find(".alinkjoin").attr('join', isjoin).html(jointxt);
                        var houseid = $(this).val();
                        for (var s = 0; s < joinArr.length; s++) {
                            var house = joinArr[s];
                            if (house.houseid == houseid) {
                                joinArr.splice(s, 1); //从start的位置开始向后删除delCount个元素
                            }
                        }
                    }
                })
            }
        })
        var IsNoCheck = function (checkboxs) {
            var nocheck = false;
            checkboxs.each(function (ex1, ex2) {
                if ($(this).prop('checked')) {
                    nocheck;
                }
            })
            if (!nocheck) {
                return;
            }
        }
        var SetCharacter = function (alink) {
            isjoin = alink.attr("join") == "0" ? true : false;
            if (isjoin) {
                alink.attr('join', 1).html("不参加");
            } else {
                alink.attr('join', 0).html("参加");
            }
        }

        //单个参加
        $(".alinkjoin").click(function () {
            var isjoin = $(this).attr("join") == "0" ? "1" : "0";
            var jointxt = $(this).attr("join") == "0" ? "不参加" : "参加";

            var ischeck = $(this).parent().prevAll().find("input[name='box']").prop('checked');
            if (!ischeck) {
                alert("请选择房源");
                return;
            } else {
                if (isjoin == "0") {
                    var houseid = $(this).attr("data");
                    $(this).parent().prevAll().find("input[name='box']").prop('checked', 0);
                    for (var s = 0; s < joinArr.length; s++) {
                        var house = joinArr[s];
                        if (house.houseid == houseid) {
                            joinArr.splice(s, 1); //从start的位置开始向后删除delCount个元素
                        }
                    }
                } else if (isjoin == "1") {
                    var houseid = $(this).val(); //房源Id
                    var nodes = $(this).parent().nextAll();
                    var o = new Object();
                    o.houseid = houseid;
                    o.developid = $(this).attr("developid");
                    o.premid = $(this).attr("premid");
                    o.buildid = $(this).attr("buildid");
                    o.Floor = nodes.eq(0).html();
                    o.HouseNo = nodes.eq(1).html();
                    o.Room = nodes.eq(2).html();
                    o.BuildingArea = nodes.eq(3).html();
                    o.SinglePrice = nodes.eq(4).html();
                    o.TotalPrice = nodes.eq(5).html();
                    o.SalesStatus = nodes.eq(6).html();
                    o.CreateTime = nodes.eq(7).html();
                    o.Join = nodes.eq(8).html();
                    joinArr[joinArr.length] = o;
                }
            }
            $(this).attr("join", isjoin).html(jointxt);
            alert(joinArr.length);
        })

        //第三步：完成设置
        $("#btnCompleteok").click(function () {
            var str = GetTable(joinArr);
            if (str != "") {
                $("#divfour").show();
                $("#tzhekou1").show();
                $("#divSelectHouse").html(str);
            }
        })
        function GetTable(arr) {
            var str = "";
            var len = arr.length;
            if (len > 0) {
                str += "     <table width='100%' border='0' cellspacing='0' cellpadding='0' class='tab1'>";
                str += "          <tr><th>序号</th><th>楼层</th><th>楼号</th><th>户型</th><th>建筑面积</th><th>参考总价</th><th>限时折扣</th><th>折后价格</th><th>操作</th></tr>";

                for (var i = 0; i < arr.length; i++) {
                    var o = new Object();
                    o = arr[i];
                    str += "<tr>";
                    str += "<td>" + o.houseid + "</td>";
                    str += "<td>" + o.Floor + "层</td>";
                    str += "<td>" + o.HouseNo + "号楼</td>";
                    str += "<td>" + o.Room + "</td>";
                    str += "<td>" + o.BuildingArea + "平方米</td>";
                    str += "<td>" + o.TotalPrice + "万元/套</td>";
                    str += "<td><input type='text' class='input_wauto w80 mr10' name='txtezhehou' developid='" + o.developid + "' premid='" + o.premid + "' buildid='" + o.buildid + "'  hid='" + o.houseid + "' />折</td>";
                    str += "<td>" + "万元/套</td>";
                    str += "<td><a onclick='del(this);'>删除</a></td>";
                    str += "</tr>";
                }

                str += "       </table>";
            }
            return str;
        }
    })
    //点击删除按钮
    function del(o) {
        $(o).parent().parent().remove();
  }
</script>
  <div class="content">
  <!-- InstanceBeginEditable name="EditRegion1" -->
  <h4 class="title_h4 mb10"><span>创建限时折扣活动</span><em class="ts_right"><a href="">返回限时折扣活动列表 </a></em></h4>
  <div class="mt15 btit clearFix">
    <div class="tit_font fl">第一步：选择楼盘</div>
    <div class="font14 fr mr35"><a id="btnupdate1">修改</a></div>
  </div>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1">
      <tr>
        <th scope="row" width="100" align="right">选择楼盘：</th>
        <td id="tdloupan1">
        	<select class="select1">
                <option value="-1">楼盘名称</option>
                <%if (ViewData["Property"] != null)
                  {
                      foreach (PremiseModel item in (ViewData["Property"] as List<PremiseModel>))
                      {%>
                     <option value="<%=item.Id %>" data="<%=item.DeveloperId %>"><%=item.Name%></option>
                <%}
                  }%>
            </select><input type="button" value="确定" class="btn_w80 ml10" id="btn1" />
        </td>
        <td id="tdloupan2"><span id="spanloupan"></span></td>
      </tr>
  </table>
  <div class="mt15 btit clearFix">
    <div class="tit_font fl">第二步：设置基本信息</div>
    <div class="font14 fr mr35"><a id="btnupdate2">修改</a></div>
  </div>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1" id="tinfo1">
      <tr>
        <th scope="row" width="100" align="right">活动名称：</th>
        <td id="tdinfo0"><input type="text" class="input_wauto w400" id="txtactivityname" /></td>
        <td id="tdinfo01"><span id="spanactivityname"></span></td>
      </tr>
      <tr>
        <th scope="row" width="100" align="right">活动保证金金额：</th>
        <td id="tdinfo1"><input type="text" class="input_wauto w180 mr10" id="txtBond" /><span class="ie_valign_5">元</span></td>
        <td id="tdinfo10" style="display:none">
           <span id="spanbond"></span>
           <span class="ie_valign_5">元</span>
        </td>
      </tr>
      <tr>
        <th scope="row" width="100" align="right">时间：</th>
        <td id="tdinfo2">
         <span class="ie_valign_5">开始</span><input id="txtBeginTime" type="text" class="input_wauto w150 ml10" /><span class="ie_valign_5">结束</span><input id="txtEndTime" type="text" class="input_wauto w150 ml10" />
        </td>
         <td id="tdinfo20" style="display:none">
            <span class="ie_valign_5">开始</span><span id="spanstart"></span><span class="ie_valign_5">结束</span><span id="spanend"></span>
        </td>
      </tr>
      <tr>
        <td colspan="2"><input type="button" value="确定" class="btn_w80 ml20 mt15" id="btn2" /></td>
      </tr>
  </table>



  <div class="mt15 btit clearFix" id="bu-31">
    <div class="tit_font fl">第三步：选择房源</div>
    <div class="font14 fr mr35"><a id="btnupdate3">修改</a></div>
  </div>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1" id="bu-32">
      <tr>
        <th scope="row" width="100" align="right"><span class="font14">筛选：</span></th>
        <td>
        	<div class="shaibox clearFix">
             <div class="selbox mr10">
                <span class="pl10">--选择楼栋--</span>
                <div style="display:none;" class="sel_list">
                   <%--<a data="1">1号楼栋</a>
                   <a>2号楼栋</a>--%>
                </div>
             </div>
             <div class="selbox mr10">
                <span class="pl10">--选择单元--</span>
                <div style="display:none;" class="sel_list">
                  <%-- <a data="1">1单元</a>
                   <a>2单元</a>--%>
                </div>
             </div>
             <div class="selbox mr10">
                <span class="pl10">--选择楼层--</span>
                <div style="display:none;" class="sel_list">
                   <%--<a>1层</a>
                   <a>2层</a>--%>
                </div>
             </div>
             <div class="selbox mr10">
                <span class="pl10">--选择状态--</span>
                <div style="display:none;" class="sel_list">
                   <a data="0">未售</a>
                   <a data="1">在售</a>
                   <a data="2">售罄</a>
                </div>
             </div>
             <input type="button" value="" class="bnt_serch fl" />
         </div>
        </td>
      </tr>
  </table>
  <div class="font12 ml20 pl10" id="bu-33">
  <label class="ml20 pl10">
  <input type="checkbox" id="btnCheckAll" name="a" class="valign2 mr5" />全选</label><a class="ml20" id="alljoin" join="0">参加</a><%--<a class="ml20" id="allnojoin">不参加</a>--%>
  </div>
 <%  Html.RenderPartial("HousePager", Model);%>


  <div id="divfour" class="mt15 btit clearFix" style="display:none">
    <div class="tit_font fl">第四步：设置限时折扣</div>
    <div class="font14 fr mr35"><a id="btnupdate4">修改</a></div>
  </div>
  <table id="tzhekou1" width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1" style="display:none">
      <tr>
        <th scope="row" width="100" align="right">批量设置折扣：</th>
        <td>
        	<input type="text" class="input_wauto mr10" id="txtzhehou" /><span class="ie_valign_5">折</span><input type="button" value="确定" class="btn_w80 ml10" id="btnZhekou" />
        </td>
      </tr>
  </table>

   <table id="tzhekou2" width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1" style="display:none">
      <tr>
        <th scope="row" width="100" align="right">批量设置折扣：</th>
        <td>
        	<span id="spanzhekou"></span><span class="ie_valign_5">折</span>
        </td>
      </tr>
  </table>


<%--<%Html.RenderPartial("SelectHousePager", Model); %>--%>
     


  <div id="divSelectHouse" class="mt15 clearFix" style="position:relative;">
   <%-- <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
    <tr>
        <th>序号</th>
        <th>楼层</th>
        <th>楼号</th>
        <th>户型</th>
        <th>建筑面积</th>
        <th>参考总价</th>
        <th>限时折扣</th>
        <th>折后价格</th>
        <th>操作</th>
      </tr>

           <tr>
            <td><input type="checkbox" class="valign2 mr5" name="boxzhekou" value="<%=Model[i].Id %>" /><%=Model[i].Id %></td>
            <td><%=Model[i].Floor%>层</td>
            <td><%=Model[i].HouseNo%>号楼</td>
            <td><%=Model[i].Room%>室<%=Model[i].Hall%>厅<%=Model[i].Toilet%>卫<%=Model[i].Kitchen%>厨</td>
            <td><%=Model[i].BuildingArea%>平方米</td>
            <td><%=Model[i].TotalPrice%>万元/套</td>
            <td><input type="text" class="input_wauto w80 mr10" name="txtezhehou" a="1"  b="<%=Model[i].BuildingId %>" i="<%=Model[i].Id %>" />折</td>
            <td>259万元/套</td>
            <td><a onclick="del(this);">删除</a></td>
      </tr>

    </table>--%>
  </div>

<%--  <div class="fen">
    <div class="tar">
    <span class="col333 font12 mr10">共 <em class="col666">50</em> 条记录</span> 
    <a href="" class="no">&lt;&lt;</a>
    <a href="" class="hover">1</a>
    <a href="">2</a>
    <a href="">3</a>
    <a href="">4</a>
    <a href="">5</a>
    <a href="">6</a>
    <a href="">&gt;&gt;</a> 
    </div>
  </div>--%>


  <div class="tac">
        <input type="button" value="完成创建" class="btn_w97_green"  id="btnComplete2"/>
  </div>

  <!-- InstanceEndEditable --></div>

    <%--选择楼盘
    <input id="delevopid" type="hidden" />
    <input id="premisesid" type="hidden" />
    <%--设置基本信息--%>
    <input id="activname" type="hidden" />
    <input id="bond" type="hidden" />
    <input id="startdate" type="hidden" />
    <input id="enddate" type="hidden" />
</asp:Content>
