<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
 <%{
       List<TXCommons.PictureModular.PremisesPictureInfo> dtlistPremisesPictureOrder = ViewData["listPremisesPictureOrder"] as List<TXCommons.PictureModular.PremisesPictureInfo>;
       if (null != dtlistPremisesPictureOrder && dtlistPremisesPictureOrder.Count > 0)
       { %>
                             <div class="r_title_lp mt10">
    <strong class="title_span">
        <%=Util.getStrLenB(ViewData["PremisesmodelName"].ToString(),0,24) %>楼盘相册</strong><span
            class="col666 font12">(共<%=ViewData["iPremisesPicturecount"]%>张)</span>
    <%{
          string url = ViewData["PremisesmodelId"].ToString();
          string iPremisesLISTcount = ViewData["iPremisesLISTcount"].ToString();
    %>
    <span class="right">
        <%-- //  0 效果图 2 规划图 3 位置图 4 楼栋平面图 5 实景图 6 交通图
       //xgt效果图|hxt户型图|ght规划图|wzt位置图|ldt楼栋平面图|sjt实景图|jtt交通图--%>
       <%-- <a href="<%=NHWebUrl.AlbumUrl(0,url.ToString()) %>" class="blue font12 mr10">楼盘沙盘图(<%=ViewData["iPremisesLISTcount"]%>)</a>--%>
        <a href="<%=NHWebUrl.AlbumUrl("hxt",url.ToString(),"") %>" class="blue font12 mr10">
            户型图(<%=ViewData["iROOMTYPEcount"]%>)</a> <a href="<%=NHWebUrl.AlbumUrl("ght",url.ToString(),"") %>"
                class="blue font12 mr10">规划图(<%=ViewData["iPlancount"]%>)</a> <a href="<%=NHWebUrl.AlbumUrl("ldt",url.ToString(),"") %>"
                    class="blue font12 mr10">楼栋平面图(<%=ViewData["iPlanecount"]%>)</a> <a href="<%=NHWebUrl.AlbumUrl("jtt",url.ToString(),"") %>"
                        class="blue font12 mr10">交通图(<%=ViewData["iTRAFFICcount"]%>)</a>
        <a href="<%=NHWebUrl.AlbumUrl("wzt",url.ToString(),"") %>" class="blue font12 mr10">位置图(<%=ViewData["iLocationcount"]%>)</a>
        <a href="<%=NHWebUrl.AlbumUrl("sjt",url.ToString(),"") %>" class="blue font12 mr10">实景图(<%=ViewData["iScenecount"]%>)</a>
        <a href="<%=NHWebUrl.AlbumUrl("xgt",url.ToString(),"") %>" class="blue font12 mr10">效果图(<%=ViewData["iEffectcount"]%>)
        </a><a href="<%=NHWebUrl.AlbumUrl("",url.ToString(),"") %>" class="blue font12 ml30">更多>>
        </a></span>
    <%}
    %>
</div>
                    <%}
                 }
                %>
    <div class="housetype clearFix mt10">
    <%{
          List<TXCommons.PictureModular.PremisesPictureInfo> dtlistPremisesPictureOrder = ViewData["listPremisesPictureOrder"] as List<TXCommons.PictureModular.PremisesPictureInfo>;
          if (null != dtlistPremisesPictureOrder && dtlistPremisesPictureOrder.Count > 0)
          {
              for (int i = 0; i < dtlistPremisesPictureOrder.Count; i++)
              {
                  if (i < 6)
                  {
                      if (null != dtlistPremisesPictureOrder[i])
                      {
                          string sPictureType = dtlistPremisesPictureOrder[i].PictureType.ToLower().ToString();
                          string id1 = dtlistPremisesPictureOrder[i].ID.ToString();
                          string sModelId1 = ViewData["PremisesmodelId"].ToString();
                          string strclass = string.Empty;
                          if (i == 5) {
                              strclass = "class=\"last\" ";
                          }
                 
    %>
    <dl <%=strclass%> >
        <dt><a href="<%=NHWebUrl.ImageDetailsUrl(sPictureType,sModelId1.ToString(),id1,"","")%>"
            class="blue">
            <img  width="175" height="125" class="imgload lazy" data-original="<%=dtlistPremisesPictureOrder[i].Path%>.211_150.jpg"
                src="" />
        </a></dt>
        <dd>
            <a href="<%=NHWebUrl.ImageDetailsUrl(sPictureType,sModelId1.ToString(),id1,"","")%>"
                class="blue">
                <%=Util.getStrLenB(dtlistPremisesPictureOrder[i].Title??"",0,24)%></a></dd>
    </dl>
    <%}
                  }
              }
          }
          else
          {%>
   <%-- <div class="noResult_box tac">
        <span class="icons01">暂无数据</span>
    </div>--%>
    <%}
      } 
    %>
</div>
