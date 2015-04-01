<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<ActivityHouse>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%@ Import Namespace="TXModel.NHouseActivies.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	营销中心-竞价房源列表

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftAjax.js")%>" type="text/javascript"></script>
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
    $(document).ready(function () {
        $(".selbox").click(function (event) {
            $(this).children(".sel_list").toggle();
        });
        $(".selbox .sel_list").mouseleave(function () {
            $(this).hide();
        });
        $(".selbox div").eq(4).find("a").click(function () {
            var data = $(this).attr("data-activstate");
            var span = $(this).parent().prev();
            var pindex = $(this).parent().parent().index();
            span.attr("data-activstate", data);
            span.html($(this).html());

        });
        $(".selbox div").eq(0).find("a").click(function () {
            var data = $(this).attr("data-premiseid");
            var span = $(this).parent().prev();
            var pindex = $(this).parent().parent().index();
            span.attr("data-premiseid", data);
            span.html($(this).html());
            GetBuild();
        });
        var saleState = $(".selbox span").eq(3).attr("data-salsestate"); //状态
        $(".bnt_serch").click(function () {
            QueryHouse();
        });
        //楼盘
        $(".select1").change(function () {
            var PremiseId = $(this).find("option:selected").val();
            jQuery.ajax({
                type: "post",
                async: false,
                url: mainUrl + "/Activity/GetBuilding",
                data: { PremisesId: PremiseId, m: Math.random() },
                dataType: "json",
                cache: false,
                success: function (data) {

                    $(".selbox").eq(0).find("div").children().remove();
                    $(".selbox").eq(1).find("div").children().remove();


                    $(".selbox").eq(1).find("span").attr("data-buidid", "-1").html("选择楼栋");
                    $(".selbox").eq(2).find("span").attr("data-unitid", "-1").html("选择单元");
                    $(".selbox").eq(3).find("span").attr("data-floorid", "-100").html("选择楼层");
                    $(".selbox").eq(4).find("span").attr("data-salsestate", "-1").html("选择状态");

                    //首个a标签
                    var attr = "buidid";
                    var firstlink = "<a data-buidid='-1' onclick='return SelectOption(this,0)'>选择楼栋</a>";
                    $(".selbox").eq(0).find("div").append(firstlink);

                    //其他标签
                    $.each(data, function(index, Building) {
                        var otherlink = "<a data-buidid='" + Building.Id + "' onclick='return GetUnit(" + PremiseId + "," + Building.Id + ",this)'>" + Building.Name + "</a>";
                        $(".selbox").eq(0).find("div").append(otherlink);
                    });
                }
            });
        });
    });
    //显示选中的下拉项
    function SelectOption(alink, i) {
        var dataAttr;
        if (i == 0) {
            dataAttr = "data-premiseid";
        }
        if (i == 1) {
            dataAttr = "data-buidid";
        }
        else if (i == 2) {
            dataAttr = "data-unitid";
        } else if (i == 3) {
            dataAttr = "data-floorid";
        }
        var data = $(alink).attr(dataAttr);
        var span = $(alink).parent().prev();
        span.attr(dataAttr, data);
        span.html($(alink).html());
        return false;
    }
    //查询房源
    function QueryHouse() {
        var premisesId = $(".selbox span").eq(0).attr("data-premiseid"); //楼盘Id
        var buildId = $(".selbox span").eq(1).attr("data-buidid"); //楼栋
        var unitId = $(".selbox span").eq(2).attr("data-unitid");  //单元
        var floorId = $(".selbox span").eq(3).attr("data-floorid");    //楼层
        var activState = $(".selbox span").eq(4).attr("data-activstate");  //状态

        jQuery.ajax({ type: "get", async: false, url: mainUrl + "/Activity/BidingList", data: { premisesId: premisesId, buildId: buildId, unitId: unitId, floorId: floorId, activState: activState, m: Math.random() }, cache: false,
            success: function (data) {
                $("#divpages01").html(data);
            }
        });
    }
    //得到楼栋
    function GetBuild() {

        RemoveBuildOption();
        RemoveUnitOption();
        RemoveFloorOption();

        var PremiseId = $(".selbox span").eq(0).attr("data-premiseid"); //楼盘Id
        jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetBuilding", data: { PremisesId: PremiseId, m: Math.random() }, dataType: "json", cache: false,
            success: function (data) {

                RemoveBuildOption();

                $.each(data, function(index, Building) {
                    var otherlink = "<a data-buidid='" + Building.Id + "' onclick='SelectOption(this,1);GetUnit(" + PremiseId + "," + Building.Id + ",this);GetFloor2(" + PremiseId + "," + Building.Id + ",this)'>" + Building.Name + Building.NameType + "</a>";
                    $(".selbox").eq(1).find("div").append(otherlink);
                });
            }
        });
    }
    //清空楼盘
    function RemovePremisesIdOption() {
        $(".selbox").eq(0).find("div").children().remove();
        $(".selbox").eq(0).find("span").attr("data-premiseid", "-1").html("选择楼盘");
        var firstlink = "<a data-premiseid='-1' onclick='SelectOption(this,0);RemoveUnitOption();RemoveFloorOption();'>选择楼盘</a>";
        $(".selbox").eq(0).find("div").append(firstlink);
    }
    //清空楼栋
    function RemoveBuildOption() {
        $(".selbox").eq(1).find("div").children().remove();
        $(".selbox").eq(1).find("span").attr("data-buidid", "-1").html("选择楼栋");
        var firstlink = "<a data-buidid='-1' onclick='SelectOption(this,1);RemoveUnitOption();RemoveFloorOption();'>选择楼栋</a>";
        $(".selbox").eq(1).find("div").append(firstlink);
    }
    //清空单元
    function RemoveUnitOption() {
        $(".selbox").eq(2).find("div").children().remove();
        $(".selbox").eq(2).find("span").attr("data-unitid", "-1").html("选择单元");
        var firstlink = "<a data-unitid='-1'  onclick='SelectOption(this,2);RemoveFloorOption()'>选择单元</a>";
        $(".selbox").eq(2).find("div").append(firstlink);
    }
    //清空楼层
    function RemoveFloorOption() {
        $(".selbox").eq(3).find("div").children().remove();
        $(".selbox").eq(3).find("span").attr("data-floorid", "-100").html("选择楼层");
        var firstlink = "<a data-floorid='-100'  onclick='return SelectOption(this,3)'>选择楼层</a>";
        $(".selbox").eq(3).find("div").append(firstlink);
    }
    //得到单元
    function GetUnit(PremiseId, BuildId, alink) {
        jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetUnit", data: { PremisesId: PremiseId, BuildingId: BuildId, m: Math.random() }, dataType: "json", cache: false,
            success: function (data) {

                //RemoveUnitOption();
                $(".selbox").eq(2).find("div").children().remove();
                $(".selbox").eq(2).find("span").attr("data-unitid", "-1").html("选择单元");
                var firstlink = "<a data-unitid='-1'  onclick='SelectOption(this,2);'>选择单元</a>";
                $(".selbox").eq(2).find("div").append(firstlink);

                $.each(data, function(index, Unit) {
                    //var otherlink = "<a data-unitid='" + Unit.Id + "' onclick='SelectOption(this,2);GetFloor(" + PremiseId + "," + BuildId + "," + Unit.Id + ",this)'>" + Unit.Name + "</a>";
                    var otherlink = "<a data-unitid='" + Unit.Id + "' onclick='SelectOption(this,2);'>" + Unit.Name + "</a>";
                    $(".selbox").eq(2).find("div").append(otherlink);
                });
            }
        });

        //显示选中项
        SelectOption(alink, 0);

        return false;
    }
    //得到楼层
    function GetFloor(PremisesId, BuildingId, UnitId, alink) {

        jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetFloor", data: { PremisesId: PremisesId, BuildingId: BuildingId, UnitId: UnitId, m: Math.random() }, dataType: "json", cache: false,
            success: function (data) {

                RemoveFloorOption();

                $.each(data, function(index, Floor) {
                    var otherlink = "<a data-floorid='" + Floor + "' onclick='return SelectOption(this,3)'>" + Floor + "层</a>";
                    $(".selbox").eq(3).find("div").append(otherlink);
                });
            }
        });

        //显示选中项
        SelectOption(alink, 1);

        return false;
    }
    //得到楼层
    function GetFloor2(PremisesId, BuildingId) {

        jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetFloor2", data: { PremisesId: PremisesId, BuildingId: BuildingId, m: Math.random() }, dataType: "json", cache: false,
            success: function (data) {

                RemoveFloorOption();

                $.each(data, function(index, Floor) {
                    var otherlink = "<a data-floorid='" + Floor + "' onclick='return SelectOption(this,3)'>" + Floor + "层</a>";
                    $(".selbox").eq(3).find("div").append(otherlink);
                });
            }
        });

        //显示选中项
        //SelectOption(alink, 1);

        return false;
    }
</script>
<div class="content">
  <div class="mt15 btit clearFix">
    <div class="tit_font fl">竞价房源列表</div>
    <div class="font14 fr mr35"><a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/Biding">创建竞价</a></div>
  </div>
  <div class="shaibox mt15 clearFix">
     <div class="fl">筛选：</div>
     <div class="selbox mr10">
        <span class="pl10" data-premiseid="-1">选择楼盘</span>
        <div class="sel_list w300" style="display:none;">
        <a data-premiseid="-1">选择楼盘</a>
         <%if (ViewData["Premise"] != null)
         {
             foreach (TXModel.NHouseActivies.Discount.PremiseModel item in (ViewData["Premise"] as List<TXModel.NHouseActivies.Discount.PremiseModel>))
            {%>
                <a data-premiseid="<%=item.Id %>"><%=item.Name%></a>
            <%}
         }%>
        </div>
     </div>
     <div class="selbox mr10">
        <span class="pl10">选择楼栋</span>
        <div class="sel_list w300" style="display:none;">
          <a data-buidid='-1' onclick='return SelectOption(this,0)'>选择楼栋</a>
        </div>
     </div>
     <div class="selbox mr10">
        <span class="pl10">选择单元</span>
        <div class="sel_list" style="display:none;">
         <a data-unitid='-1'  onclick='return SelectOption(this,1)'>选择单元</a>
        </div>
     </div>
     <div class="selbox mr10">
        <span class="pl10">选择楼层</span>
        <div class="sel_list" style="display:none;">
          <a data-floorid='-100'  onclick='return SelectOption(this,2)'>选择楼层</a>
        </div>
     </div>
     <div class="selbox mr10">
        <span class="pl10">选择状态</span>
        <div class="sel_list" style="display:none;">
           <a data-activstate=-1>选择状态</a>
           <a data-activstate=0>未开始</a>
           <a data-activstate=1>进行中</a>
           <a data-activstate=2>已结束</a>
        </div>
     </div>
     <input type="button" class="bnt_serch fl" value="" />
 </div>
  <div id="divpages01">
    <% Html.RenderPartial("Common/BarginBidingHouseList", Model);%>
  </div>
</div>
</asp:Content>
