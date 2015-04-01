<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<ActivityHouse>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%@ Import Namespace="TXModel.NHouseActivies.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	营销中心-秒杀房源列表

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>

  <script type="text/javascript">
      var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
      $(document).ready(function () {

          $(".selbox").click(function (event) {
              $(this).children(".sel_list").toggle();
          })
          $(".selbox .sel_list").mouseleave(function () {
              $(this).hide();
          })
          $(".selbox div").eq(4).find("a").click(function () {
              var data = $(this).attr("data-activstate");
              var span = $(this).parent().prev();
              var pindex = $(this).parent().parent().index();
              span.attr("data-activstate", data);
              span.html($(this).html());

          })
          $(".selbox div").eq(0).find("a").click(function () {
              var data = $(this).attr("data-premiseid");
              var span = $(this).parent().prev();
              var pindex = $(this).parent().parent().index();
              span.attr("data-premiseid", data);
              span.html($(this).html());

              GetBuild();
          })
          var saleState = $(".selbox span").eq(3).attr("data-salsestate");  //状态
          $(".bnt_serch").click(function () {
              QueryHouse();
          })

          //楼盘
          $(".select1").change(function () {

              var PremiseId = $(this).find("option:selected").val();
              jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetBuilding", data: { PremisesId: PremiseId, m: Math.random() }, dataType: "json", cache: false,
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
                      $.each(data, function (index, Building) {
                          var otherlink = "<a data-buidid='" + Building.Id + "' onclick='return GetUnit(" + PremiseId + "," + Building.Id + ",this)'>" + Building.Name + "</a>";
                          $(".selbox").eq(0).find("div").append(otherlink);
                      })
                  }
              });

          })
      })
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
      function QueryHouse() {
          var premisesId = $(".selbox span").eq(0).attr("data-premiseid"); //楼盘Id
          var buildId = $(".selbox span").eq(1).attr("data-buidid"); //楼栋
          var unitId = $(".selbox span").eq(2).attr("data-unitid");  //单元
          var floorId = $(".selbox span").eq(3).attr("data-floorid");    //楼层
          var activState = $(".selbox span").eq(4).attr("data-activstate");  //状态

          jQuery.ajax({ type: "get", async: false, url: mainUrl + "/Activity/SecKillList", data: { premisesId: premisesId, buildId: buildId, unitId: unitId, floorId: floorId, activState: activState, m: Math.random() }, cache: false,
              success: function (data) {
                  $("#divpages01").html(data);
              }
          });
      }
      function GetBuild() {

          RemoveBuildOption();
          RemoveUnitOption();
          RemoveFloorOption();

          var PremiseId = $(".selbox span").eq(0).attr("data-premiseid"); //楼盘Id
          jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetBuilding", data: { PremisesId: PremiseId, m: Math.random() }, dataType: "json", cache: false,
              success: function (data) {

                  RemoveBuildOption();

                  $.each(data, function (index, Building) {
                      var otherlink = "<a data-buidid='" + Building.Id + "' onclick='SelectOption(this,1);GetUnit(" + PremiseId + "," + Building.Id + ",this);GetFloor2(" + PremiseId + "," + Building.Id + ",this)'>" + Building.Name + Building.NameType + "</a>";
                      $(".selbox").eq(1).find("div").append(otherlink);
                  })
              }
          });
      }
      function RemovePremisesIdOption() {
          $(".selbox").eq(0).find("div").children().remove();
          $(".selbox").eq(0).find("span").attr("data-premiseid", "-1").html("选择楼盘");
          var firstlink = "<a data-premiseid='-1' onclick='SelectOption(this,0);RemoveUnitOption();RemoveFloorOption();'>选择楼盘</a>";
          $(".selbox").eq(0).find("div").append(firstlink);
      }
      function RemoveBuildOption() {
          $(".selbox").eq(1).find("div").children().remove();
          $(".selbox").eq(1).find("span").attr("data-buidid", "-1").html("选择楼栋");
          var firstlink = "<a data-buidid='-1' onclick='SelectOption(this,1);RemoveUnitOption();RemoveFloorOption();'>选择楼栋</a>";
          $(".selbox").eq(1).find("div").append(firstlink);
      }
      function RemoveUnitOption() {
          $(".selbox").eq(2).find("div").children().remove();
          $(".selbox").eq(2).find("span").attr("data-unitid", "-1").html("选择单元");
          var firstlink = "<a data-unitid='-1'  onclick='SelectOption(this,2);RemoveFloorOption()'>选择单元</a>";
          $(".selbox").eq(2).find("div").append(firstlink);
      }
      function RemoveFloorOption() {
          $(".selbox").eq(3).find("div").children().remove();
          $(".selbox").eq(3).find("span").attr("data-floorid", "-100").html("选择楼层");
          var firstlink = "<a data-floorid='-100'  onclick='return SelectOption(this,3)'>选择楼层</a>";
          $(".selbox").eq(3).find("div").append(firstlink);
      }
      //单元
      function GetUnit(PremiseId, BuildId, alink) {
          jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetUnit", data: { PremisesId: PremiseId, BuildingId: BuildId, m: Math.random() }, dataType: "json", cache: false,
              success: function (data) {

                 // RemoveUnitOption();
                  $(".selbox").eq(2).find("div").children().remove();
                  $(".selbox").eq(2).find("span").attr("data-unitid", "-1").html("选择单元");
                  var firstlink = "<a data-unitid='-1'  onclick='SelectOption(this,2);'>选择单元</a>";
                  $(".selbox").eq(2).find("div").append(firstlink);

                  $.each(data, function (index, Unit) {
                      //var otherlink = "<a data-unitid='" + Unit.Id + "' onclick='SelectOption(this,2);GetFloor(" + PremiseId + "," + BuildId + "," + Unit.Id + ",this)'>" + Unit.Name + "</a>";
                      var otherlink = "<a data-unitid='" + Unit.Id + "' onclick='SelectOption(this,2);'>" + Unit.Name + "</a>";
                      $(".selbox").eq(2).find("div").append(otherlink);
                  })
              }
          });

          //显示选中项
          SelectOption(alink, 0);

          return false;
      }
      //楼层
      function GetFloor(PremisesId, BuildingId, UnitId, alink) {

          jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetFloor", data: { PremisesId: PremisesId, BuildingId: BuildingId, UnitId: UnitId, m: Math.random() }, dataType: "json", cache: false,
              success: function (data) {

                  RemoveFloorOption();

                  $.each(data, function (index, Floor) {
                      var otherlink = "<a data-floorid='" + Floor + "' onclick='return SelectOption(this,3)'>" + Floor + "层</a>";
                      $(".selbox").eq(3).find("div").append(otherlink);
                  })
              }
          });

          //显示选中项
          SelectOption(alink, 1);

          return false;
      }
      function GetFloor2(PremisesId, BuildingId) {

          jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetFloor2", data: { PremisesId: PremisesId, BuildingId: BuildingId, m: Math.random() }, dataType: "json", cache: false,
              success: function (data) {

                  RemoveFloorOption();

                  $.each(data, function (index, Floor) {
                      var otherlink = "<a data-floorid='" + Floor + "' onclick='return SelectOption(this,3)'>" + Floor + "层</a>";
                      $(".selbox").eq(3).find("div").append(otherlink);
                  })
              }
          });

          //显示选中项
          //SelectOption(alink, 1);

          return false;
      }
    </script>


<div class="content">
  <div class="mt15 btit clearFix">
    <div class="tit_font fl">秒杀房源列表</div>
    <div class="font14 fr mr35"><a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/SecKill">创建秒杀</a></div>
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
           <%--<a href="#">东方家园</a>
           <a href="#">东方青青</a>--%>
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
   <% Html.RenderPartial("Common/APriceSecKillHouseList", Model);%>
   </div>
<%--  <div class="mt15 clearFix" style="position:relative;">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
      <tr>
        <th width="5%">房号</th>
        <th width="5%"> 楼层 </th>
        <th width="5%">楼号</th>
        <th width="10%"> 参考价格 </th>
        <th width="10%"> 起拍价格 </th>
        <th width="12%"> 加价幅度 </th>
        <th width="12%"> 最大幅度 </th>
        <th width="9%"> 时间 </th>
        <th width="9%"> 剩余时间 </th>
        <th width="8%"> 活动保证金 </th>
        <th width="6%"> 状态 </th>
        <th width="9%">操作</th>
      </tr>
      <%for (int i = 0; i < Model.Count; i++)
        {%>
        <tr>
            <td> <%=((Model.CurrentPageIndex-1)*Model.PageSize)+1+i%> </td>
            <td><%=Model[i].Floor %>层</td> 
            <td><%=Model[i].Name %>号楼</td>
            <td> <%=Model[i].SinglePrice%> 万元/套</td>
            <td><%=Model[i].BidPrice%>万元/套</td>
            <td> 必须为<%=Model[i].AddPrice%>元的整数倍 </td>
            <td> 单次加价不高于<%=Model[i].MaxPrice%>元 </td>
            <td> <%=Model[i].BeginTime %> ---<%=Model[i].EndTime%> </td>
            <td> 2小时50分钟 </td>
            <td> <%=Model[i].Bond %>元 </td>
            <td>
                <%if (Model[i].ActivitieState == 0)
                    { %>
                        未开始
                <%}
                    else if (Model[i].ActivitieState == 1)
                    { %>
                        进行中
                <%}else if(Model[i].ActivitieState == 2){ %>
                        已结束
                <%} %>
           </td>
            <td><a href="#">查看用户报名</a></td>
      </tr>
      <%} %>
    


    </table>
  </div>
  <div class="fen">
            <div class="tar">
               <span class="col333 font12 mr10">共<em class="col666"><%=Model.TotalItemCount%></em> 条记录</span>
                    <%=Html.Pager(Model, new PagerOptions
                     {
                            ContainerTagName = "span",
                            CssClass = "no",
                            PageIndexParameterName = "Id",
                            NumericPagerItemCount = 10,
                            ShowFirstLast = false,
                            SeparatorHtml = "",
                            PrevPageText = "<<",
                            NextPageText = ">>",
                            AlwaysShowFirstLastPageNumber = false,
                            CurrentPagerItemWrapperFormatString = "<a class=\"hover\">{0}</a>",
                     })%>
            </div>
        </div>--%>
        
        </div>
</asp:Content>
