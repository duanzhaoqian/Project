<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXModel.User.CT_FavoritePrem>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<ul id="ul_main">
    <%if (Model != null && Model.Count > 0)
      {
          foreach (var item in Model)
          { %>
    <li class="clearFix " id="li_<%=item.FavoriteId %>">
        <div class="pic-gr">
            <div class="pic-box">
                <a href="<%=TXCommons.NHWebUrl.PremisesIndexUrl(item.PremisId.ToString())%>" target="_blank">
                    <img width="168" height="129" src="<%=item.InnerCode %>" /></a></div>
        </div>
        <div class="zhuti">
            <h3>
                <a class="this-title linkB-333" href="<%=TXCommons.NHWebUrl.PremisesIndexUrl(item.PremisId.ToString())%>"
                    target="_blank">
                    <%=item.Name %></a>
                <%switch (item.SalesStatus)
                  {
                      case 0:%>
                <i class="zt-i-3"></i>
                <%break;
                      case 1: %>
                <i class="zt-i-1"></i>
                <%break;
                      case 2: %>
                <i class="zt-i-2"></i>
                <%break;
                  }
                %>
                <span class="col666">[<%=string.Join(",",TXCommons.PremisesType.GetPropertyTypeByIds(item.PropertyType)) %>]</span></h3>
            <p class="col666">
                <%switch (item.Ring)
                  {
                      case 1:%>
                [一环]
                <%break;
                      case 2: %>
                [二环]
                <%break;
                      case 3: %>
                [三环]
                <%break;
                      case 4: %>
                [四环]
                <%break;
                      case 5: %>
                [五环]
                <%break;
                      case 6: %>
                [六环]
                <%break;
                      default:%>
                [六环以外]
                <%break;
                  } %>
                <%=item.salesAddress %>
                <span class="ml5">
                    <%=item.BName %></span> <a href="<%=TXCommons.GetConfig.GetSiteRoot %>/lp-jtpt-<%=item.PremisId %>"
                        class="seemap linkD ml10" target="_blank">查看地图</a></p>
            <p class="col666 mt10">
                户型图：
                <%=item.Rooms %>
            </p>
            <p class="col666 mt10">
                开发商：<%=item.Developer %></p>
            <div class="col666 mt15 clearFix">
                <div class="w185 fl">
                    <%=item.Features%>
                </div>
                <%--<div class="show-ylbox linkD blue" id="<%=item.PremisId %>">
                    <span onmouseover="hover($(this).parent())" onmouseout="$(this).parent().children('.ylbox').hide();$(this).parent().css('z-index', '0')" >
                        +房源列表预览</span>
                    <div class="ylbox clearFix" onmouseover="$(this).show();$(this).parent().css('z-index', '9999')" onmouseout="out(this)"
                        style="display: none;">
                    </div>
                </div>--%>
            </div>
        </div>
        <div class="msg-box">
            <p class="font14 fontYaHei col666 mb20 mt20">
                均价 <span class="colff7100 font16"><strong>
                    <%=item.ReferencePrice.ToString().Split('.')[0] %>元</strong></span> /㎡</p>
            <p class="font14 fontYaHei col666">
                <span class="colff7100">
                    <%=item.TelePhone %></span></p>
            <div class="contro-box mt20">
                <a href="javascript:void(0)" style="color:#0068B7" onclick="Delete(<%=item.FavoriteId %>)">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/c-link-icon-3.png" width="12" height="12"
                        class="mr3 top2" />
                    取消收藏</a> <a href="javascript:void(0)" data="<%=item.Name %>" lang="<%=item.PremisId %>"
                        url="<%=TXCommons.NHWebUrl.PremisesIndexUrl(item.PremisId.ToString()) %>" class="duibi" style="color:#0068B7" >
                        <img src="<%=TXCommons.GetConfig.ImgUrl%>images/c-link-icon-2.gif" class="mr3" />
                        加入对比</a>
            </div>
        </div>
    </li>
    <%}
      }
      else
      { %>
    <li>
        <center>对不起，暂无记录！</center>
    </li>
    <%} %>
</ul>
<div class="bidFenyeBar bidFenyeBar1 clearFix">
    <%=Html.AjaxPager(Model, new PagerOptions
{
    CssClass = "pub_fenye1",
    SeparatorHtml = "",
    AlwaysShowFirstLastPageNumber=false,
    PageIndexParameterName = "page",
    ShowFirstLast = false,
    PrevCss = "getL",
    NextCss = "getR",
    PrevPageText = "<<",
    NextPageText = ">>",
    CurrentPagerItemWrapperFormatString = "<a class=\"current\">{0}</a>"
}, new AjaxOptions { UpdateTargetId = "divAjaxData"})%>
</div>
