<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
<%{
      List<TXOrm.Premis> dtInterestPremises111 = ViewData["lsInterestPremises"] as List<TXOrm.Premis>;
      if (null != dtInterestPremises111 && dtInterestPremises111.Count > 0)
      {
%>
<div class="r_title_lp">
    <strong class="title_span">您可能感兴趣的楼盘</strong></div>
<div class="pk_box clearFix mt20 mb20">
    <dl class="fl" style="width: 240px;">
        <dt>
            <img class="imgload lazy" data-original="<%=ViewData["ViewDatalistzlhxPath"]??(TXCommons.GetConfig.ImgUrl + "images/w212_h150.gif")%>"
                src="" /></dt>
        <dd>
            <a href="<%=ViewData["PremisesIndexUrl"]%>">
                <%=Util.getStrLenB(ViewData["InterestPremisesName"].ToString(), 0, 24)%></a><span
                    class="colff840b ml10">
                    <%=ViewData["InterestPremisesReferencePrice"] == "0" ? "价格待定" :ViewData["InterestPremisesReferencePrice"]+"元/平方米"%></span></dd>
        <%--        <dd>
            <strong class="colff840b">
                <%=ViewData["InterestPremisesReferencePrice"] == "0" ? "暂无数据" :ViewData["InterestPremisesReferencePrice"]+"元/平方米"%></strong></dd>--%>
    </dl>
    <div class="pk fl">
        <input type="button" onclick="premisesppk()" value="详细对比" class="btn_w98_gray" /></div>
    <%{
          List<TXOrm.Premis> dtInterestPremises = ViewData["lsInterestPremises"] as List<TXOrm.Premis>;
          if (null != dtInterestPremises && dtInterestPremises.Count > 0)
          {

              for (int i = 0; i < 1; i++)
              {
                  string IdPremis = dtInterestPremises[i].Id.ToString();
    %>
    <dl class="fr" style="width: 240px;">
        <dt>
            <img id="pkpremiseimg" name="pkpremiseimg" src="<%=string.IsNullOrEmpty(dtInterestPremises[i].PremisesIntroduction) ? (TXCommons.GetConfig.ImgUrl + "images/w186_h125.jpg") : dtInterestPremises[i].PremisesIntroduction%>" /></dt>
        <input id="hidpremisesid" name="hidpremisesid" value="<%=dtInterestPremises[i].Id%>"
            type="hidden" />
        <dd>
            <a href="<%=NHWebUrl.PremisesIndexUrl(IdPremis.ToString()) %>" id="pkpremisea" name="pkpremisea"
                class="imgload lazy" data-original="<%=NHWebUrl.PremisesIndexUrl(dtInterestPremises[i].Id.ToString()) %>"
                src="<%=NHWebUrl.PremisesIndexUrl(IdPremis.ToString()) %>">
                <%=Util.getStrLenB(dtInterestPremises[i].Name, 0, 24)%></a><span class="colff840b ml10"><%=dtInterestPremises[i].ReferencePrice == 0 ? "价格待定" : Convert.ToDouble(dtInterestPremises[i].ReferencePrice).ToString() + "元/平方米"%></span></dd>
        <%--<dd>
            <strong id="pkpremisestrong" name="pkpremisestrong" class="colff840b">
                </strong></dd>--%>
    </dl>
    <%}
          }
      } %>
</div>
<p class="font12 col666 ml30">
    <strong class="colff840b">小提示：</strong>可以选择楼盘进行切换对比哦!
</p>
<div class="housetype clearFix mt10 hover_xg">
    <%{
          List<TXOrm.Premis> dtInterestPremises = ViewData["lsInterestPremises"] as List<TXOrm.Premis>;
          if (null != dtInterestPremises && dtInterestPremises.Count > 0)
          {
              for (int i = 0; i < dtInterestPremises.Count; i++)
              {
                 
    %>
    <dl>
        <dt onmouseout="spanhide('<%=dtInterestPremises[i].Id%>')" onmousemove="spanshow('<%=dtInterestPremises[i].Id%>')">
            <img width="175" height="125" 
                class="imgload lazy" data-original='<%=string.IsNullOrEmpty(dtInterestPremises[i].PremisesIntroduction) ? (TXCommons.GetConfig.ImgUrl + "images/zwtp175-125.jpg") : dtInterestPremises[i].PremisesIntroduction%>'
                src="" />
            <span onclick="pkpremiseschange('<%=dtInterestPremises[i].Id%>','<%=dtInterestPremises[i].Name%>','<%=Convert.ToDouble(dtInterestPremises[i].ReferencePrice)%>','<%=dtInterestPremises[i].InnerCode%>',$(this).prev('img'))" id="span<%=dtInterestPremises[i].Id%>" class="hover_bj"></span></dt>
        <dd class="col666">
            <%=Util.getStrLenB(dtInterestPremises[i].Name, 0, 24)%>
        </dd>
        <dd>
            <a target="_blank" href="<%=NHWebUrl.PremisesIndexUrl(dtInterestPremises[i].Id.ToString()) %>"
                class="blue">地图</a> - <a target="_blank" href="<%=NHWebUrl.AlbumUrl("hxt",dtInterestPremises[i].Id.ToString(),"") %>"
                    class="blue">户型图</a> - <a target="_blank" href="<%=NHWebUrl.AlbumUrl("",dtInterestPremises[i].Id.ToString(),"") %>"
                        class="blue">相册</a></dd>
    </dl>
    <%}
          }
      } %>
</div>
<% 
      }
  } %>
<script type="text/javascript">
    function spanshow(id) {
        $("#span" + id).show();
    }
    function spanhide(id) {
        $("#span" + id).hide();
    }
</script>
