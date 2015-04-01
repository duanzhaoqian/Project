<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<dt>
    <%{
          if (null != ViewData["lsCarouselListImgSrc"])
          {
    %>
    <div>
        <img class="imgload" width="500" height="350" id="IDCARD2" data-original="<%=ViewData["lsCarouselListImgSrc"]%>" src="" />
    </div>
    <%}
          else
          { %>
    <div>
        <img class="imgload" width="500" height="350" id="Img1" data-original="<%=TXCommons.GetConfig.ImgUrl+"images/w430_h295.gif"%>" src=""/>
    </div>
    <% }
      }%>
    <ul class="clearFix">
        <%{
              List<TXModel.Web.PremisesCarousel> dtlsCarouselList = ViewData["lsCarouselList"] as List<TXModel.Web.PremisesCarousel>;
              if (null != dtlsCarouselList && dtlsCarouselList.Count > 0)
              {

                  for (int i = 0; i < dtlsCarouselList.Count; i++)
                  {
                      if (i == 0)
                      {    
        %>
        <li id="Carouselli<%=dtlsCarouselList[i].Id%>" class="on">
            <img width="80" height="60" class="imgload" onmouseover="changeimg('<%=dtlsCarouselList[i].ImgSrc%>',<%=dtlsCarouselList[i].Id%>)"
                data-original="<%=dtlsCarouselList[i].SmallImgSrc%>" src=""/><p class="bj">
                </p>
            <p>
                <%=dtlsCarouselList[i].ImgDesc%></p>
        </li>
        <%}
                      else
                      { %>
        <li id="Carouselli<%=dtlsCarouselList[i].Id%>">
            <img width="85" height="65" class="imgload" onmouseover="changeimg('<%=dtlsCarouselList[i].ImgSrc%>',<%=dtlsCarouselList[i].Id%>)"
                data-original="<%=dtlsCarouselList[i].SmallImgSrc%>" src=""/><p class="bj">
                </p>
            <p>
                <%=dtlsCarouselList[i].ImgDesc%></p>
        </li>
        <% }
                  }
              }
          } %>
    </ul>
</dt>
