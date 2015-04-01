<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
   <%{
                         List<PremisesActivitiesHouse> dtPremises = ViewData["lsPremises"] as List<PremisesActivitiesHouse>;
                         string strNamestring = ViewData["PremismodelName"].ToString();
                         string strIdstring = ViewData["PremismodelId"].ToString();
          if (null != dtPremises && dtPremises.Count > 0)
          { %>
                             <div class="r_title_lp mt10">
                <strong class="title_span">
                    <%=Util.getStrLenB(strNamestring, 0, 24)%>房源列表</strong><span class="col666 font12"></span><span
                        class="right"><a href="<%=NHWebUrl.PremisesHouseUrl(strIdstring.ToString(),"","","") %>"
                            class="blue font12">更多>> </a></span>
</div>
                    <%}
                 }
                %>
<div class="r_list_box clearFix">
    <%{
          List<PremisesActivitiesHouse> dtPremises = ViewData["lsPremises"] as List<PremisesActivitiesHouse>;
          if (null != dtPremises && dtPremises.Count > 0)
          {
              for (int i = 0; i < dtPremises.Count; i++)
              {
                  string strTypedesc = string.Empty;
                  if ((null != dtPremises[i])&&(null != dtPremises[i].Typedesc)){
                   strTypedesc = dtPremises[i].Typedesc.Trim();
                  }
    %>
    <dl>
        <dt>
            <%-- <img src="<%=ViewData["lsPictureListImgUrl"]%>" />--%>
            <img class="imgload lazy" data-original="<%=TXCommons.GetConfig.ImgUrl+"images/mrt.jpg"%>"
                width="71" height="53" src="" />
               <%{
                        if (!string.Empty.Equals(strTypedesc)){ %>
                              <p class="bj"></p>
                    <%}
                 }
                %>
            <p>
                <%=dtPremises[i].Typedesc%></p>
        </dt>
        <dd>
            <p>
                <a href="<%=NHWebUrl.PremisesHouseInfoUrl(dtPremises[i].HouseId.ToString()) %>" class="blue">
                    <%=Util.getStrLenB(dtPremises[i].PremisesName, 0, 16)%>
                    <%=Util.getStrLenB(dtPremises[i].BuildingName,0,6)%><%=dtPremises[i].BuildingNameType%>
                    <%=dtPremises[i].UnitName%>
                    <%=dtPremises[i].Floor%>层<%=Util.getStrLenB(dtPremises[i].HouseName, 0, 6)%></a></p>
            <p>
                <%=dtPremises[i].BuildingArea%>㎡|<%=dtPremises[i].Room%>室<%=dtPremises[i].Hall%>厅|<%=dtPremises[i].Floor%>/<%=dtPremises[i].TheFloor%>层<span
                    class="ml20">已有<i class="colff840b"><%=dtPremises[i].Hits %>
                    </i>人关注</span></p>
        </dd>
        <dd>
           <%{
                        if (!string.Empty.Equals(strTypedesc)){ %>
                              <p>
                起价：<strong class="colff840b"><%=Convert.ToDouble(dtPremises[i].BidPrice)%>万元</strong></p>
                    <%}
                 }
                %>
            <p>
                市场价：<strong class="colff840b"><%=Convert.ToDouble(dtPremises[i].TotalPrice)%>万元</strong></p>
        </dd>
    </dl>
    <%}
          }
          else
          { %>
   
    <% }
      }%>
</div>
