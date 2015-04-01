<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXModel.User.CT_Bid>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%if (Model != null && Model.Count > 0)
  {
      foreach (var item in Model)
      {
          //if (item.ActivieType == 2)//限时折扣
          //{
%>
<%--<div class="bidContent borderB">
    <div class="clearFix pd20 bidPos">
        <div class="bidImg fl mr20">
            <img src="<%=item.InnerCode %>" />
        </div>
        <div class="bidCt fl">
            <p class="font18 fontYahei bidHeight">
                <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                    target="_blank" class="linkB fl">
                    <%=item.PremisesName%>
                    <%=item.BuildingName%>
                    <%=item.Unit%>
                    <%=item.Floor%>层
                    <%=item.Name%>号房</a><span class="bq_ms">限时折扣</span></p>
            <p class="col333 font14 mt10">
                <span class="col666">项目地址：</span><%=item.ActivieAddress%></p>
            <p class="font14 col666 mt8">
                <%=item.BuildingArea%>平方米
                <%=item.Room%>室<%=item.Hall%>厅
                <%=item.Floor%>/<%=item.TheFloor%>层
            </p>
            <p class="font12 col333 mt8">
                <span class="col666">竞购时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BeginTime)%>-<%=string.Format("{0:yyyy.MM.dd}", item.EndTime)%></p>
            <p class="font12 col333 mt8">
                <span class="col666">参与时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BidTime)%>
            </p>
        </div>
        <div class="bidBtn ml60">
            <div class="font14  col666 clearFix">
                <div class="fl">
                    限时折扣价：<span class="font24 colff8200 arialfont bold"><%=item.BidPrice.ToString("#.##") %></span><span
                        class="colff8200">万元</span></div>
            </div>
            <div class="font14 mb5 col666 clearFix">
                <div class="fl">
                    市场价格：<span class="font18 col666 arialfont bold"><del><%=item.Marketprice.ToString("#.##")%></del></span>
                    <span class="col666">万元</span></div>
            </div>
            <p class="font12 mb10 col666">
                报名人数：<%=item.UserCount %>
                人</p>
            <p class="font12 mb0 col666">
                围观人数：2206人</p>
        </div>
        <div class="otherv tar">
            <%if (item.BondState == 0)
              {%>
            <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                target="_blank" class="linkA ml10">缴纳保证金</a>
            <%}
              if (item.BondState == 2)
              {
            %>
            <span class="fl mr10 col6aa700">保证金已退回</span>
            <%}
            %>
            <%if (item.HouseState == 3)
              {
                  if (item.BidPriceState == 1)
                  {%>
            <span class="colff8200 fl">出价成功</span>
            <%}
                  else
                  { %>
            <span class="colff8200 fl">出价失败</span>
            <%}
              }
              else
              {
                  if (item.BondState == 1)
                  { 
            %>
            <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                class="linkA fr" target="_blank">我要出价</a>
            <%}
              } %></div>
        <div class="pos_iico">
            <%if (item.ActivieStatus == 2)
              { %>
            <span class="ico_yjs">已结束</span><%}
              else
              { %>
            <span class="ico_jxz">进行中</span>
            <% } %></div>
    </div>
</div>--%>
<% //}
          //else if (item.ActivieType == 3)//排号购房
          //{
%>
<%--<div class="bidContent borderB">
    <div class="clearFix pd20 bidPos">
        <div class="bidImg fl mr20">
            <img src="<%=item.InnerCode %>" />
        </div>
        <div class="bidCt fl">
            <p class="font18 fontYahei bidHeight">
                <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                    target="_blank" class="linkB fl">
                    <%=item.ActivitieName %></a><span class="bq_phgf">排号购房</span></p>
            <p class="font14 col333 mt10">
                <span class="col666">楼盘名称：</span><%=item.PremisesName %><span class="col666 ml30">房源套数：</span>200套</p>
            <p class="col333 font14 mt8">
                <span class="col666">项目地址：</span><%=item.ActivieAddress%></p>
            <p class="font12 col333 mt8">
                <span class="col666">竞购时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BeginTime)%>-<%=string.Format("{0:yyyy.MM.dd}", item.EndTime)%></p>
            <p class="font12 col333 mt8">
                <span class="col666">参与时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BidTime)%>
            </p>
        </div>
        <div class="bidBtn ml60">
            <div class="font14 mb5 col666 clearFix">
                <div class="fl">
                    有效排号：<span class="font24 colff8200 arialfont bold">前100名</span></div>
            </div>
            <p class="font12 mb10 col666">
                报名人数：<%=item.UserCount %>
                人</p>
            <p class="font12 mb0 col666">
                围观人数：2206人</p>
        </div>
        <div class="otherv tar">
            <%if (item.BondState == 0)
              {%>
            <a href="javascript:void(0)" target="_blank" class="linkA ml10">缴纳保证金</a>
            <%}
              if (item.BondState == 2)
              {
            %>
            <span class="fl mr10 col6aa700">保证金已退回</span>
            <%
              } %><a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                  target="_blank" class="fr linkA">查看详情 &gt;&gt;</a></div>
        <div class="pos_iico">
            <%if (item.ActivieStatus == 2)
              { %>
            <span class="ico_yjs">已结束</span><%}
              else
              { %>
            <span class="ico_jxz">进行中</span>
            <% } %></div>
    </div>
    <div class="promptCt">
        <p class="promptIco colff3300">
            当前共<%=item.UserCount %>人报名，您当前为第10名</p>
    </div>
</div>--%>
<%//}
          //else if (item.ActivieType == 4)//阶梯团购
          //{
%>
<%--<div class="bidContent borderB">
    <div class="clearFix pd20 bidPos">
        <div class="bidImg fl mr20">
            <img src="<%=item.InnerCode %>" />
        </div>
        <div class="bidCt fl">
            <p class="font18 fontYahei bidHeight">
                <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                    target="_blank" class="linkB fl">
                    <%=item.PremisesName%>
                    <%=item.BuildingName%>
                    <%=item.Unit%>
                    <%=item.Floor%>层
                    <%=item.Name%>号房</a><span class="bq_jttg">阶梯团购</span></p>
            <p class="col333 font14 mt10">
                <span class="col666">项目地址：</span><%=item.ActivieAddress%></p>
            <p class="font14 col666 mt8">
                <%=item.BuildingArea%>平方米
                <%=item.Room%>室<%=item.Hall%>厅
                <%=item.Floor%>/<%=item.TheFloor%>层
            </p>
            <p class="font12 col333 mt8">
                <span class="col666">竞购时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BeginTime)%>-<%=string.Format("{0:yyyy.MM.dd}", item.EndTime)%></p>
            <p class="font12 col333 mt8">
                <span class="col666">参与时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BidTime)%>
            </p>
        </div>
        <div class="bidBtn ml60">
            <div class="font14 mb5 col666 clearFix">
                <div class="fl">
                    最低团购价：<span class="font24 colff8200 arialfont bold"><%=item.BidPrice.ToString("#.##") %></span><span
                        class="colff8200">万元</span></div>
            </div>
            <p class="font12 mb10 col666">
                <span class="mr20">折扣幅度：5折-9折</span></p>
            <p class="font12 mb10 col666">
                报名人数：<%=item.UserCount %>
                人</p>
            <p class="font12 mb0 col666">
                围观人数：2206人</p>
        </div>
        <div class="otherv tar">
            <%if (item.BondState == 0)
              {%>
            <a href="javascript:void(0)" target="_blank" class="linkA ml10">缴纳保证金</a>
            <%}
              if (item.BondState == 2)
              {
            %>
            <span class="fl mr10 col6aa700">保证金已退回</span>
            <%
              } %><a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                  target="_blank" class="fr linkA">查看详情 &gt;&gt;</a></div>
        <div class="pos_iico">
            <%if (item.ActivieStatus == 2)
              { %>
            <span class="ico_yjs">已结束</span><%}
              else
              { %>
            <span class="ico_jxz">进行中</span>
            <% } %></div>
    </div>
    <div class="promptCt">
        <p class="promptIco colff3300">
            当前共<%=item.UserCount %>人报名，可享受 6折的优惠，团购价为<%=item.BidPrice.ToString("#.##") %>万元</p>
    </div>
</div>--%>
<%//}
          if (item.ActivieType == 5)//竞价
          { %>
<div class="bidContent borderB">
    <div class="clearFix pd20 bidPos">
        <div class="bidImg fl mr20">
            <img width="180" height="123" src="<%=item.InnerCode %>" />
        </div>
        <div class="bidCt fl">
            <p class="font18 fontYahei bidHeight">
                <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                    target="_blank" class="linkB fl">
                    <%=TXCommons.Util.getStrLenB(item.Name,0,40) %></a><span class="bq_jj">竞价</span></p>
            <p class="col333 font14 mt10">
                <span class="col666">项目地址：</span><%=item.ActivieAddress%></p>
            <p class="font14 col666 mt8">
                <%=item.BuildingArea%>平方米
                <%=item.Room%>室<%=item.Hall%>厅
                <%=item.Floor%>/<%=item.TheFloor%>层
            </p>
            <p class="font12 col333 mt8">
                <span class="col666">竞购时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BeginTime)%>-<%=string.Format("{0:yyyy.MM.dd}", item.EndTime)%></p>
            <p class="font12 col333 mt8">
                <span class="col666">参与时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BidTime)%>
            </p>
        </div>
        <div class="bidBtn ml60">
            <div class="font14 mb5 col666 clearFix">
                <div class="fl">
                    起拍价格：<span class="font24 colff8200 arialfont bold"><%=item.BidPrice.ToString("#.##") %></span>
                    <span class="colff8200">万元</span></div>
            </div>
            <p class="font12 mb10 col666">
                出价次数：<%=item.BidCount %>
                次</p>
            <p class="font12 mb10 col666">
                参与人数：<%=item.UserCount %>
                人</p>
            <p class="font12 mb0 col666">
                围观人数：<%=item.Clicks %>人</p>
        </div>
        <div class="otherv tar">
            <%if (item.BondState == 0 && item.ActivieStatus != 2)
              {%>
            <a href="javascript:void(0)" class="linkA ml10" onclick="Dialog.showDialog('url','<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/paybond?bond=<%=item.Bond%>&orderid=<%=item.OrderId %>','<%=TXCommons.GetConfig.ImgUrl%>user/images/loading.gif')">
                缴纳保证金</a>
            <%}
              if (item.BondState == 2)
              {
            %>
            <span class="fl mr10 col6aa700">保证金已退回</span>
            <%
              } %>
            <%if (item.ActivieStatus == 2)
              {
                  if (item.BidPriceState > 0)
                  {%>
            <span class="colff8200 fl">出价成功</span>
            <%}
                  else
                  { %>
            <span class="colff8200 fl">出价失败</span>
            <%}%>
            <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bidRecord?houseid=<%=item.HouseId %>&name=<%=HttpUtility.UrlEncode(item.Name)%>"
                class="linkA ml10" target="_blank">我的出价记录</a>
            <%}
              else
              {
                  if (item.BondState == 1)
                  { 
            %>
            <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                target="_blank" class="linkA mr10">我要出价</a><span class="col666">|</span><a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bidRecord?houseid=<%=item.HouseId %>&name=<%=HttpUtility.UrlEncode(item.Name)%>"
                    target="_blank" class="linkA ml10">我的出价记录</a>
            <%}
              } %>
        </div>
        <div class="pos_iico">
            <%if (item.ActivieStatus == 2)
              { %>
            <span class="ico_yjs">已结束</span><%}
              else
              { %>
            <span class="ico_jxz">进行中</span>
            <% } %></div>
    </div>
</div>
<%}
          else if (item.ActivieType == 6)//砍价
          {%>
<div class="bidContent borderB">
    <div class="clearFix pd20 bidPos">
        <div class="bidImg fl mr20">
            <img width="180" height="123" src="<%=item.InnerCode %>" />
        </div>
        <div class="bidCt fl">
            <p class="font18 fontYahei bidHeight">
                <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                    target="_blank" class="linkB fl">
                    <%=TXCommons.Util.getStrLenB(item.Name,0,40) %></a><span class="bq_kaj">砍价</span></p>
            <p class="col333 font14 mt10">
                <span class="col666">项目地址：</span><%=item.ActivieAddress%></p>
            <p class="font14 col666 mt8">
                <%=item.BuildingArea%>平方米
                <%=item.Room%>室<%=item.Hall%>厅
                <%=item.Floor%>/<%=item.TheFloor%>层
            </p>
            <p class="font12 col333 mt8">
                <span class="col666">竞购时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BeginTime)%>-<%=string.Format("{0:yyyy.MM.dd}", item.EndTime)%></p>
            <p class="font12 col333 mt8">
                <span class="col666">参与时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BidTime)%>
            </p>
        </div>
        <div class="bidBtn ml60">
            <div class="font14 mb5 col666 clearFix">
                <div class="fl">
                    市场价格：<span class="font24 colff8200 arialfont bold"><%=item.BidPrice.ToString("#.##") %></span>
                    <span class="colff8200">万元（封顶价）</span></div>
            </div>
            <p class="font12 mb10 col666">
                出价次数：<%=item.BidCount %>
                次</p>
            <p class="font12 mb10 col666">
                参与人数：<%=item.UserCount %>
                人</p>
            <p class="font12 mb0 col666">
                围观人数：<%=item.Clicks %>人</p>
        </div>
        <div class="otherv tar">
            <%if (item.BondState == 0 && item.ActivieStatus != 2)
              {%>
            <a href="javascript:void(0)" class="linkA ml10" onclick="Dialog.showDialog('url','<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/paybond?bond=<%=item.Bond%>&orderid=<%=item.OrderId %>','<%=TXCommons.GetConfig.ImgUrl%>user/images/loading.gif')">
                缴纳保证金</a>
            <%}
              if (item.BondState == 2)
              {
            %>
            <span class="fl mr10 col6aa700">保证金已退回</span>
            <%
              }%>
            <%if (item.ActivieStatus == 2)
              {
                  if (item.BidPriceState > 0)
                  {%>
            <span class="colff8200 fl">出价成功</span>
            <%}
                  else
                  { %>
            <span class="colff8200 fl">出价失败</span>
            <%}%>
            <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bidRecord?houseid=<%=item.HouseId %>&name=<%=HttpUtility.UrlEncode(item.Name)%>"
                class="linkA ml10" target="_blank">我的出价记录</a>
            <%}
              else
              {
                  if (item.BondState == 1)
                  {  %>
            <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                class="linkA mr10" target="_blank">我要出价</a><span class="col666">|</span><a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bidRecord?houseid=<%=item.HouseId %>&name=<%=HttpUtility.UrlEncode(item.Name)%>"
                    class="linkA ml10" target="_blank">我的出价记录</a>
            <%}
              } %>
        </div>
        <div class="pos_iico">
            <%if (item.ActivieStatus == 2)
              { %>
            <span class="ico_yjs">已结束</span><%}
              else
              { %>
            <span class="ico_jxz">进行中</span>
            <% } %></div>
    </div>
</div>
<%}
          else if (item.ActivieType == 7)//秒杀
          { %>
<div class="bidContent borderB">
    <div class="clearFix pd20 bidPos">
        <div class="bidImg fl mr20">
            <img width="180" height="123" src="<%=item.InnerCode %>" />
        </div>
        <div class="bidCt fl">
            <p class="font18 fontYahei bidHeight">
                <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                    target="_blank" class="linkB fl">
                    <%=TXCommons.Util.getStrLenB(item.Name,0,40) %></a><span class="bq_ms">秒杀</span></p>
            <p class="col333 font14 mt10">
                <span class="col666">项目地址：</span><%=item.ActivieAddress%></p>
            <p class="font14 col666 mt8">
                <%=item.BuildingArea%>平方米
                <%=item.Room%>室<%=item.Hall%>厅
                <%=item.Floor%>/<%=item.TheFloor%>层
            </p>
            <p class="font12 col333 mt8">
                <span class="col666">竞购时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BeginTime)%>-<%=string.Format("{0:yyyy.MM.dd}", item.EndTime)%></p>
            <p class="font12 col333 mt8">
                <span class="col666">参与时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BidTime)%>
            </p>
        </div>
        <div class="bidBtn ml60">
            <div class="font14 mb5 col666 clearFix">
                <div class="fl">
                    秒杀价格：<span class="font24 colff8200 arialfont bold"><%=item.BidPrice.ToString("#.##")%></span>
                    <span class="colff8200">万元</span></div>
            </div>
            <div class="font14 mb10 col666 clearFix">
                <div class="fl">
                    市场价格：<span class="font18 col666 arialfont bold"><del><%=item.Marketprice.ToString("#.##")%></del></span>
                    <span class="col666">万元</span></div>
            </div>
            <p class="font12 mb0 col666">
                <span>围观人数：<%=item.Clicks%>人</span></p>
        </div>
        <div class="otherv tar">
            <%if (item.BondState == 0 && item.HouseState != 3 && item.ActivieStatus != 2)
              {%>
            <%--<a href="javascript:void(0)" class="linkA ml10" onclick="Dialog.showDialog('url','<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/upinfo?bond=<%=item.Bond%>&orderid=<%=item.OrderId %>&house=<%=HttpUtility.UrlEncode(item.Name)%>','<%=TXCommons.GetConfig.ImgUrl%>user/images/loading.gif')">
                缴纳保证金</a>--%>
            <%}
              if (item.BondState == 2)
              {
            %>
            <span class="fl mr10 col6aa700">保证金已退回</span>
            <%
              } %>
            <%if (item.ActivieStatus == 2)
              {
                  if (item.BidPriceState == 1)
                  {%>
            <span class="colff8200 fl">秒杀成功</span>
            <%}
                  else
                  { %>
            <span class="colff8200 fl">秒杀失败</span>
            <%}
              }
            %>
        </div>
        <div class="pos_iico">
            <%if (item.ActivieStatus == 2)
              { %>
            <span class="ico_yjs">已结束</span><%}
              else
              { %>
            <span class="ico_jxz">进行中</span>
            <% } %></div>
    </div>
</div>
<%}
          else if (item.ActivieType == 8)//一口价
          { %>
<div class="bidContent borderB">
    <div class="clearFix pd20 bidPos">
        <div class="bidImg fl mr20">
            <img width="180" height="123" src="<%=item.InnerCode %>" />
        </div>
        <div class="bidCt fl">
            <p class="font18 fontYahei bidHeight">
                <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.HouseId.ToString()) %>"
                    target="_blank" class="linkB fl">
                    <%=TXCommons.Util.getStrLenB(item.Name,0,40) %></a><span class="bq_ykj">一口价</span></p>
            <p class="col333 font14 mt10">
                <span class="col666">项目地址：</span><%=item.ActivieAddress%></p>
            <p class="font14 col666 mt8">
                <%=item.BuildingArea%>平方米
                <%=item.Room%>室<%=item.Hall%>厅
                <%=item.Floor%>/<%=item.TheFloor%>层
            </p>
            <p class="font12 col333 mt8">
                <span class="col666">竞购时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BeginTime)%>-<%=string.Format("{0:yyyy.MM.dd}", item.EndTime)%></p>
            <p class="font12 col333 mt8">
                <span class="col666">参与时间：</span><%=string.Format("{0:yyyy.MM.dd}", item.BidTime)%>
            </p>
        </div>
        <div class="bidBtn ml60">
            <div class="font14 mb5 col666 clearFix">
                <div class="fl">
                    一口价：<span class="font24 colff8200 arialfont bold"><%=item.BidPrice.ToString("#.##") %></span>
                    <span class="colff8200">万元</span></div>
            </div>
            <div class="font14 mb10 col666 clearFix">
                <div class="fl">
                    市场价格：<span class="font18 col666 arialfont bold"><del><%=item.Marketprice.ToString("#.##")%></del></span>
                    <span class="col666">万元</span></div>
            </div>
            <p class="font12 mb0 col666">
                围观人数：<%=item.Clicks %>人</p>
        </div>
        <div class="otherv tar">
            <%if (item.BondState == 0 && item.HouseState != 3 && item.ActivieStatus != 2)
              {%>
            <%--<a href="javascript:void(0)" class="linkA ml10" onclick="Dialog.showDialog('url','<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/upinfo?bond=<%=item.Bond%>&orderid=<%=item.OrderId %>&house=<%=HttpUtility.UrlEncode(item.Name)%>','<%=TXCommons.GetConfig.ImgUrl%>user/images/loading.gif')">
                缴纳保证金</a>--%>
            <%}
              if (item.BondState == 2)
              {
            %>
            <span class="fl mr10 col6aa700">保证金已退回</span>
            <%
              } %>
            <%if (item.ActivieStatus == 2)
              {
                  if (item.BidPriceState == 1)
                  {%>
            <span class="colff8200 fl">购买成功</span>
            <%}
                  else
                  { %>
            <span class="colff8200 fl">购买失败</span>
            <%}
              }
            %>
        </div>
        <div class="pos_iico">
            <%if (item.ActivieStatus == 2)
              { %>
            <span class="ico_yjs">已结束</span><%}
              else
              { %>
            <span class="ico_jxz">进行中</span>
            <% } %></div>
    </div>
</div>
<%}
      }
  }
  else
  { %>
<div class="clearFix pd20 bidPos">
    <center>
        对不起，暂无记录！</center>
</div>
<%}%>
<div class="bidFenyeBar clearFix">
    <%=Html.AjaxPager(Model, new PagerOptions
{
    CssClass = "pub_fenye1",
    SeparatorHtml = "",
    AlwaysShowFirstLastPageNumber=false,
    PageIndexParameterName = "page",
    ShowFirstLast = false,
    ShowPrevNext=true,
    PrevCss = "getL",
    NextCss = "getR",
    PrevPageText = "<<",
    NextPageText = ">>",
    CurrentPagerItemWrapperFormatString = "<a class=\"current\">{0}</a>"
}, new AjaxOptions { UpdateTargetId = "divAjaxData"})%>
</div>
