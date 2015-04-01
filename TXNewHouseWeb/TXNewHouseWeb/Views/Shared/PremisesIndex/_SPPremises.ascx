<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
<div class="r_title_lp">
    <strong class="title_span">
        <%=Util.getStrLenB(ViewData["tempPictureName"].ToString(), 0, 24)%>沙盘</strong>
    <span class="font14 fontYahei col333 ml20">本项目今日成交信息：</span> <span class="col666 font12">
        认购套数:<i class="colff840b"><%=ViewData["SubscribedCount"]%></i></span> <span class="col666 font12 ml15">
            成交套数:<i class="colff840b"><%=ViewData["BargainCount"]%></i></span> <span class="col666 font12 ml15">
                总户数:<i class="colff840b"><%=ViewData["tempPictureUserCount"]%></i></span>
    <span class="right"><a href="" class="blue font12">查看此楼盘房源>> </a></span>
    <%--  <span class="font14 fontYahei col333 ml20">本项目今日成交信息：</span>
    <span class="col666 font12">认购套数:<i class="colff840b">1</i></span>
    <span class="col666 font12 ml15">成交套数:<i class="colff840b">0</i></span>
    <span class="col666 font12 ml15">总户数:<i class="colff840b"><%=ViewData["tempPictureUserCount"]%></i></span>--%>
    <span class="right"><a href="<%=NHWebUrl.PremisesHouseUrl(ViewData["tempPictureId"].ToString(),"","","") %>"
        class="blue font12">查看此楼盘房源>> </a></span>
</div>
<div class="sp_box clearFix">
    <%{
          if (null != ViewData["tempPictureModularPath"])
          {
               
    %>
    <div class="fl">
        <div class="img">
            <img class="imgload lazy" data-original="<%=ViewData["tempPictureModularPath"]%>.692_450.jpg"
                width="830" height="430" src=""></div>
        <p class="bj">
        </p>
        <p>
            <span class="bg_red"></span>在售<span class="bg_gray ml15"> </span>售罄 <span class="bg_green ml15">
            </span>建设中<span class="bg_blue ml15"> </span>规划中</p>
        <%{
              List<TXOrm.SandTableLabel> dtSandTableLabel = ViewData["SandTableLabelList"] as List<TXOrm.SandTableLabel>;
              if (null != dtSandTableLabel)
              {
                  for (int i = 0; i < dtSandTableLabel.Count; i++)
                  {
        %>
        <%--   <SPAN style="POSITION: absolute; top: <%=dtSandTableLabel[i].CoordY%>px; left: <%=dtSandTableLabel[i].CoordX%>px;" class=lp_bg_red>1号楼</SPAN> 
        --%>
        <div style="position: absolute; cursor: pointer; z-index: 99; left: <%=dtSandTableLabel[i].CoordX - 3%>px;
            top: <%=dtSandTableLabel[i].CoordY + 15%>px;" id="point_9" class="mapcsbs">
            <a href="javascript:Buildinginfo('<%=dtSandTableLabel[i].PremisesId%>','<%=dtSandTableLabel[i].Id%>','<%=dtSandTableLabel[i].Number%>')">
                <span id="span_<%=dtSandTableLabel[i].Number%>" <%=dtSandTableLabel[i].SandBox%>>
                    <%=dtSandTableLabel[i].Number%>#</span></a>
        </div>
        <%
                  }
              }
          } %>
    </div>
    <%}
      }
    %>
    <div id="divhxlist" class="fr" style="overflow-y: auto;">
        <div class="gray_title">
            <%=Util.getStrLenB(ViewData["tempPictureName"].ToString(), 0, 16)%>户型列表</div>
        <%-- <div class="donginf_r donginf_r1">
                        <ul class="clearFix">
                            <li class="kaip">开盘时间：<span class="col333">暂无信息</span></li>
                            <li class="ruz">入住时间：<span class="col333">暂无信息</span></li>
                            <li class="w135 dany">单元：<span class="col333">含3个单元</span></li>
                            <li class="w135 cengs">层数：<span class="col333">000层</span></li>
                            <li class="w135 hus">户数：<span class="col333">共0户</span></li>
                            <li class="w135 tispd">梯户配对：<span class="col333">1梯2户</span></li>
                        </ul>
                    </div>--%>
        <%{
              List<TXModel.Web.PremisesHouseRoom> dtPremisesHouseRoom11111 = ViewData["lsPremisesHouseRoomList"] as List<TXModel.Web.PremisesHouseRoom>;
              if (null != dtPremisesHouseRoom11111 && dtPremisesHouseRoom11111.Count > 0)
              {
        %>
        <ul class="hometype_ul">
            <%{
                  List<TXModel.Web.PremisesHouseRoom> dtPremisesHouseRoom = ViewData["lsPremisesHouseRoomList"] as List<TXModel.Web.PremisesHouseRoom>;
                  List<IndexRoom> sizeChartList = ViewData["sizeChartListViewData"] as List<IndexRoom>;

                  StringBuilder sb = new StringBuilder();

                  if (null != dtPremisesHouseRoom && sizeChartList != null)
                  {
                      for (int i = 0; i < dtPremisesHouseRoom.Count; i++)
                      {
                          string roomCountstring = dtPremisesHouseRoom[i].RoomCount.ToString();
                          if (i < 5)
                          {
                          
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
                          sb.AppendFormat("<a href=\"{0}\" target=\"_blank\" style=\"margin-left:0\" class=\"blue mr10\">{1}居({2})</a>", TXCommons.NHWebUrl.AlbumUrl("hxt", sizeChartList[0].PremisesID, "r" + dtPremisesHouseRoom[i].Room.ToString()), dtPremisesHouseRoom[i].Room, roomCountstring);
                      }
                  }%>
            <script type="text/javascript">
                $(function () {
                    var html = '<%=sb.ToString() %>'; 
                    $("#sp_zlhxt").html(html);
                    if (html != '') {
                        $("#sp_zlhxt").next().show();
                    }
                })
            </script>
            <%} %>
        </ul>
        <% 
              }
              else
              {%>
        <ul class="ul_list clearFix" style="display: block;">
            <li>暂无数据</li>
        </ul>
        <script>
            $(function () {
                $("#sp_zlhxt").html("暂无数据");
            });
        </script>
        <%}
          } 
        %>
    </div>
</div>
