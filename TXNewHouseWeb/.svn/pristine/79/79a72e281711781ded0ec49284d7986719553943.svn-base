<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="TXCommons" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons.PictureModular" %>
<div class="donginf clearFix">
    <%{
          TXOrm.Building building = ViewData["buildingtemp"] as TXOrm.Building;
          if (null != building && building.Id > 0)
          {
              string strPremisesmodelId = ViewData["PremisesmodelId"].ToString();  
    %>
    <div class="fl">
        <%{
              List<PremisesPictureInfo> dtlsPlane = ViewData["lsPlane"] as List<PremisesPictureInfo>;
              string url = TXCommons.GetConfig.ImgUrl + "images/w212_h150.gif";
              string strPremisesmodelId1 = ViewData["PremisesmodelId"].ToString();
              string url2 = NHWebUrl.AlbumUrl("ldt", strPremisesmodelId1.ToString(),"");
              if (null != dtlsPlane && dtlsPlane.Count > 0)
              {
                  var temp = dtlsPlane.FirstOrDefault();
                  if (null != temp)
                  {
                      url = temp.Path.ToString();
                  }
              }
        %>
        <div class="dong_img">
            <img src="<%=url%>" width="170" height="130" />
        </div>
        <div class="font12 tac pt5">
            <a href="<%=url2%>" class="linkD">查看楼栋平面图 </a>
        </div>
        <%
          }
        %>
    </div>
    <div class="donginf_r fr pos_rel">
        <h4>
        <%=Util.getStrLenB(building.Name, 0, 10)%><%=building.NameType%>基本信息
        </h4>
        <ul class=" pos_rel clearFix">
            <li class="kaip">开盘时间：<span class="col333"><%=Convert.ToDateTime(building.OpeningTime).ToString("yyyy-MM-dd")%></span></li>
            <li class="ruz">入住时间：<span class="col333"><%=Convert.ToDateTime(building.OthersTime).ToString("yyyy-MM-dd")%></span></li>
            <li class="dany">单元：<span class="col333">含<%=ViewData["UnitListCount"]%>个单元</span></li>
            <li class="w70 cengs">层数：<span class="col333"><%=building.TheFloor%>层</span></li>
            <li class="hus">户数：<span class="col333">共<%=building.FamilyNum%>户</span></li>
            <li class="tispd">梯户配对：<span class="col333"><%=TXCommons.NewHouseWeb.BuildingType.GetLadder(Convert.ToInt32(building.Ladder))%></span></li>
            <li class="zhuangx">装修：<span class="col333"><%=TXCommons.NewHouseWeb.BuildingType.GetRenovation(Convert.ToInt32(building.Renovation))%></span></li>
            <li class="w580 loudwz">楼栋所处楼盘位置：<span class="col333"><%=TXCommons.NewHouseWeb.BuildingType.GetBuildingPosition(Convert.ToInt32(building.BuildingPosition))%></span>
                <a target="_blank" href="<%=NHWebUrl.PremisesIndexUrl(strPremisesmodelId) %>" class="linkD ml20">
                    查看此楼栋在沙盘中的位置</a></li>
            <li class="w580 hux pos_rel pr10" style="z-index: 1px;"><span class="fl">户型：</span>
                <%{
                      List<TXModel.Web.BuildingHouseHuX> dtlsBuildingHouseHuX = ViewData["lsBuildingHouseHuX"] as List<TXModel.Web.BuildingHouseHuX>;

                      if (null != dtlsBuildingHouseHuX && dtlsBuildingHouseHuX.Count > 0)
                      {
                          int iforeach1 = 0;
                          foreach (TXModel.Web.BuildingHouseHuX tempBuildingHouseHuX1 in dtlsBuildingHouseHuX)
                          {

                              if (iforeach1 < 3)
                              {
                %>
                <a href="<%=NHWebUrl.ImageDetailsUrl("hxt", strPremisesmodelId,tempBuildingHouseHuX1.Rid.ToString(),"",tempBuildingHouseHuX1.HouseId.ToString())%>"
                    class="linkD" target="_blank">
                    <%=tempBuildingHouseHuX1.Room%>室<%=tempBuildingHouseHuX1.Hall%>厅<%=tempBuildingHouseHuX1.Toilet%>卫<%=tempBuildingHouseHuX1.Kitchen%>厨
                    <%=tempBuildingHouseHuX1.BuildingArea%>㎡</a>
                <%
                              }
                              iforeach1++;
                          }
                      }
                  } %>
                <span class="zhankai" <%=ViewData["zhankai"]%>>展开</span></li>
        </ul>
        <div class="zhankai_box" style="display: none;">
            <%{
                  List<TXModel.Web.BuildingHouseHuX> dtlsBuildingHouseHuX = ViewData["lsBuildingHouseHuX"] as List<TXModel.Web.BuildingHouseHuX>;
                  if (null != dtlsBuildingHouseHuX && dtlsBuildingHouseHuX.Count > 0)
                  {
                      int iforeach2 = 0;
                      foreach (TXModel.Web.BuildingHouseHuX tempBuildingHouseHuX1 in dtlsBuildingHouseHuX)
                      {
                          if (iforeach2 > 2)
                          {
            %>
            <a href="<%=NHWebUrl.ImageDetailsUrl("hxt", strPremisesmodelId,tempBuildingHouseHuX1.Rid.ToString(),"",tempBuildingHouseHuX1.HouseId.ToString())%>"
                class="linkD" target="_blank">
                <%=tempBuildingHouseHuX1.Room%>室<%=tempBuildingHouseHuX1.Hall%>厅<%=tempBuildingHouseHuX1.Toilet%>卫<%=tempBuildingHouseHuX1.Kitchen%>厨
                <%=tempBuildingHouseHuX1.BuildingArea%>㎡</a>
            <%
                          }
                          iforeach2++;
                      }
                  }
              } %>
            <span class="shousuo">收起</span>
        </div>
    </div>
    <%
          }
      } %>
</div>
