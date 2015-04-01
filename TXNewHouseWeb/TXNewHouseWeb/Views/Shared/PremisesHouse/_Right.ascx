<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="TXCommons" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons.PictureModular" %>
<div class="ml20 mt10">
    <div class="r_title_lp" style="position: static;">
        <strong class="title_span">列表模式</strong><span class="font14 fontYahei col333 ml20">本项目今日成交信息：</span><span
            class="col666 font12">认购套数：<i class="colff840b"><%=ViewData["SubscribedCount"] %></i></span><span class="col666 font12 ml15">成交套数：<i
                class="colff840b"><%=ViewData["BargainCount"]%></i></span><span class="col666 font12 ml15">总户数：<i class="colff840b"><%=ViewData["tempPictureUserCount"]%></i></span></div>
    <div class="mt10 font12 col666">
        <span class="bgcol_1">&nbsp;</span>开发商保留<span class="bgcol_2">&nbsp;</span>在售<span
            class="bgcol_3">&nbsp;</span>已认购<span class="bgcol_4">&nbsp;</span>已签约<span class="bgcol_5">&nbsp;</span>已备案<span
                class="bgcol_6">&nbsp;</span>已办产权<span class="bgcol_8">&nbsp;</span>被限制<span class="bgcol_7">&nbsp;</span>拆迁安置<span
                    class="bgcol_9">&nbsp;</span>待售<span class="bgcol_10">&nbsp;</span>售罄</div>
    <%-- <div class="mt10 font12 col666">
        <span class="bgcol_1 ml10">&nbsp;</span>开发商保留<span class="bgcol_2 ml35">&nbsp;</span>可售<span
            class="bgcol_3 ml35">&nbsp;</span>已认购<span class="bgcol_4 ml35">&nbsp;</span>已签约<span
                class="bgcol_5 ml35">&nbsp;</span>已备案<span class="bgcol_6 ml35">&nbsp;</span>已办产权<span
                    class="bgcol_1 ml35">&nbsp;</span>被限制<span class="bgcol_7 ml35">&nbsp;</span>拆迁安置
    </div>--%>
    <%{
          TXOrm.Building building = ViewData["buildingtemp"] as TXOrm.Building;
          List<TXOrm.Unit> dtUnit = ViewData["lsUnittemp"] as List<TXOrm.Unit>;
          for (int i = 0; i < dtUnit.Count; i++)
          {
              List<TXModel.Web.HouseFloor> dtlsHouseFloorInfoList = ViewData["lsHouseFloorInfoList"] as List<TXModel.Web.HouseFloor>;
              if (null != dtlsHouseFloorInfoList && dtlsHouseFloorInfoList.Count > 0)
              {
                  foreach (TXModel.Web.HouseFloor floorinfo in dtlsHouseFloorInfoList)
                  {
                      if (floorinfo.UnitId == dtUnit[i].Num)
                      {
                          if (floorinfo.BuildingId == building.Id)
                          {
                              TXBll.HouseData.PremisesBll _premisesbll = new TXBll.HouseData.PremisesBll();

                              int iGetHousecountbyunit = _premisesbll.GetHousecountbyunit(building.PremisesId, building.Id, floorinfo.UnitId, -10000);
                              int iGetHousecountbyunit2 = _premisesbll.GetHousecountbyunit(building.PremisesId, building.Id, floorinfo.UnitId, floorinfo.Floor);
                                             
                 
    %>
    <div class="font14 col666 mt20">
        <strong class="ml10">
            <%=building.Name%><%=building.NameType%>
            <%--<%=dtUnit[i].Num%>--%><%=dtUnit[i].Name%>
            （<%=iGetHousecountbyunit%>户）<%=floorinfo.Floor%>层（<%=iGetHousecountbyunit2%>户）</strong>
    </div>
    <div class="mt10 tab_b1">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th width="11%">
                    房号
                </th>
                <th width="17%">
                    建筑面积
                </th>
                <th width="24%">
                    户型
                </th>
                <th width="15%">
                    参考价格
                </th>
                <th width="16%">
                    销售状态
                </th>
                <th width="17%">
                    详情
                </th>
            </tr>
            <%{
                  List<TXOrm.House> dtlsHousetemp = ViewData["lsHousetemp"] as List<TXOrm.House>;
                  if (null != dtlsHousetemp && dtlsHousetemp.Count > 0)
                  {
                      for (int j = 0; j < dtlsHousetemp.Count; j++)
                      {
                          if (dtlsHousetemp[j].UnitId == dtUnit[i].Num)
                          {
                              if (floorinfo.Floor == dtlsHousetemp[j].Floor)
                              {
                                  if (dtlsHousetemp[j].BuildingId == building.Id)
                                  {
            %>
            <tr>
                <td>
                    <%--   <%=(j + 1).ToString()%>--%>
                    <%=dtlsHousetemp[j].Name%>
                </td>
                <td>
                    <%=dtlsHousetemp[j].BuildingArea%>平方米
                </td>
                <td>
                    <%=dtlsHousetemp[j].Room%>室<%=dtlsHousetemp[j].Hall%>厅<%=dtlsHousetemp[j].Toilet%>卫<%=dtlsHousetemp[j].Kitchen%>厨
                </td>
                <td>
                    <%=Convert.ToDouble(dtlsHousetemp[j].TotalPrice)%>万
                </td>
                <td><div><span <%=TXCommons.NewHouseWeb.HouseType.GetSalesStatusClass(Convert.ToInt32(dtlsHousetemp[j].SalesStatus))%>>&nbsp;</span><%=TXCommons.NewHouseWeb.HouseType.GetSalesStatus(Convert.ToInt32(dtlsHousetemp[j].SalesStatus))%></div>
                </td>
                <td>
                    <a href="<%=NHWebUrl.PremisesHouseInfoUrl(dtlsHousetemp[j].Id.ToString()) %>" class="linkD">
                        查看详情</a><%=dtlsHousetemp[j].PictureData%>
                </td>
            </tr>
            <%}
                              }
                          }
                      }
                  }
              } %>
        </table>
    </div>
    <%}
                      }
                  }
              }
          }
          if (dtUnit.Count == 0)
          {%>
    <div class="noResult_box tac mt35" style="border-bottom: 1px solid #E4E4E4;">
        <span class="icons01">暂无数据</span>
    </div>
    <%}
      }
    %>
</div>
