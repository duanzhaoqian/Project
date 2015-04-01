<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<TXCommons.Index.IndexRanking>>" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
<div class="gray_title">
    <%=Util.getStrLenB(Util.ToString(ViewData["PremisesName"]),0,16)%>-<%=Util.getStrLenB(ViewData["pnum"].ToString(),0,4)%>号楼户型列表</div>
<div class="donginf_r donginf_r1">
    <%
        if (null != ViewData["buildingtemp"])
        {
            TXOrm.Building building = ViewData["buildingtemp"] as TXOrm.Building;
    %>
    <ul class="clearFix">
        <li class="kaip">开盘时间：<span class="col333"><%= Convert.ToDateTime(building.OpeningTime).ToString("yyyy-MM-dd")%></span></li>
        <li class="ruz">入住时间：<span class="col333"><%= Convert.ToDateTime(building.OthersTime).ToString("yyyy-MM-dd")%></span></li>
        <li class="w135 dany">单元：<span class="col333">含<%=ViewData["UnitListCount"]%>个单元</span></li>
        <li class="w135 cengs">层数：<span class="col333"><%=building.TheFloor%>层</span></li>
        <li class="w135 hus">户数：<span class="col333">共<%=building.FamilyNum%>户</span></li>
        <li class="w135 tispd">梯户配对：<span class="col333"><%=TXCommons.NewHouseWeb.BuildingType.GetLadder(Convert.ToInt32(building.Ladder))%></span></li>
    </ul>
    <%}
        else
        {%>
    暂无信息
    <%-- <ul class="clearFix">
        <li class="kaip">开盘时间：<span class="col333">暂无信息</span></li>
        <li class="ruz">入住时间：<span class="col333">暂无信息</span></li>
        <li class="w105 dany">单元：<span class="col333">含3个单元</span></li>
        <li class="w105 cengs">层数：<span class="col333">000层</span></li>
        <li class="w105 hus">户数：<span class="col333">共0户</span></li>
        <li class="w105 tispd">梯户配对：<span class="col333">1梯2户</span></li>
    </ul>--%>
    <%}%>
</div>
<ul class="hometype_ul">
    <%{
          List<TXModel.Web.PremisesHouseRoom> dtPremisesHouseRoom = ViewData["lsPremisesHouseRoomList"] as List<TXModel.Web.PremisesHouseRoom>;
          List<IndexRoom> sizeChartList = ViewData["sizeChartListViewData"] as List<IndexRoom>;

          if (null != dtPremisesHouseRoom && sizeChartList != null)
          {
              for (int i = 0; i < dtPremisesHouseRoom.Count; i++)
              {
                  if (i < 5)
                  {
                      string roomCountstring = dtPremisesHouseRoom[i].RoomCount.ToString();
    %>
    <li>
        <p class="jian">
            <%=dtPremisesHouseRoom[i].Room%>居户型图（<%=roomCountstring%>）</p>
        <div class="li_box">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1">
                <tr>
                    <th scope="col">
                        户型图名称
                    </th>
                    <th scope="col">
                        户型
                    </th>
                    <th scope="col">
                        面积
                    </th>
                </tr>
                <%{
                      List<IndexRoom> temp = sizeChartList.Where(s => Util.ToInt(s.Room) == dtPremisesHouseRoom[i].Room).ToList();
                      if (null != temp && temp.Count > 0)
                      {
                          for (int j = 0; j < temp.Count; j++)
                          {

                %>
                <tr>
                    <td>
                        <a href="<%=NHWebUrl.ImageDetailsUrl("hxt",Util.ToString(temp[j].PremisesID),Util.ToString(temp[j].RID),"",Util.ToString(temp[j].HouseID)) %>">
                            <%=Util.getStrLenB(temp[j].Title, 0, 16)%></a>
                    </td>
                    <td>
                        <%=temp[j].Room%>室<%=temp[j].Toilet%>卫
                    </td>
                    <%-- <td><%=dtlsHousetemp[j].Room%>室<%=dtlsHousetemp[j].Hall%>厅<%=dtlsHousetemp[j].Toilet%>卫<%=dtlsHousetemp[j].Kitchen%>厨</td>--%>
                    <td>
                        <%=temp[j].BuildingArea%>平方米
                    </td>
                </tr>
                <%}
                      }
                  } %>
            </table>
        </div>
    </li>
    <%
                  }
              }
          }
      } %>
</ul>
