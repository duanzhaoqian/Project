<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	营销活动说明
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="bgcolor">
	<div class="w1190"><i class="help_t"></i><i class="helptr"><a class="linkD" href="#"></a></i></div>
</div>
<div class="clearfix">
<!-- InstanceBeginEditable name="EditRegion3" -->
  <div class="p_wrap">
    <div class="helpbox clearFix">
      <div class="silder">
        <%=Html.Partial("Menu")%>
      </div>
      <div class="main">
        <h6>营销活动说明</h6>
        <div class="maotit"><a href="#1">秒杀-购房</a><em>|</em><a href="#2">竞价-购房</a><em>|</em><a href="#3">砍价-购房</a><em>|</em><a href="#4">一口价-购房</a><!--<em>|</em><a href="#5">限时折扣-购房</a><em>|</em><a href="#6">阶梯团购-购房</a><em>|</em><a href="#7">排号-购房</a>--></div>
        <p class="blod mb10"><a class="sek_tit" name="1">秒杀-购房</a></p>
        <p>用户在规定时间内， 按照先到先得方式在线抢房，速度最快的买家，即竞购成功。</p>
        <p>秒杀-购房流程：</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_1.jpg" /></div>
        <p><strong>第一步：</strong>登录网站。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_2.jpg" /></div>
        <p><strong>第二步：</strong>点击“立刻秒杀”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_3.jpg" /></div>
        <p><strong>第三步：</strong>秒杀成功、确认个人信息。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_4.jpg" /></div>
        <p><strong>第四步:</strong>支付保证金。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_5.jpg" /></div>
        <p><strong>第五步:</strong>竞购成功。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_5_1.jpg" /></div>
        <p><strong>第六步:</strong>线下签约。</p>
        <p class="blod mt15 mb10"><a class="sek_tit" name="2">竞价-购房</a></p>
        <p>在规定时间内，用户一次或多次提交愿意出的最高价，到截止时间后，出价最高的用户获得标的物，并按此价格付款。</p>
        <p>竞价-购房流程：</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_6.jpg" /></div>
        <p><strong>第一步:</strong>登录网站。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_7.jpg" /></div>
        <p><strong>第二步：</strong>点击“立刻出价”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_8.jpg" /></div>
        <p><strong>第三步:</strong>确认个人信息。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_9.jpg" /></div>
        <p><strong>第四步：</strong>支付保证金。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_10.jpg" /></div>
        <p><strong>第五步：</strong>出价。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_11.jpg" /></div>
        <p><strong>第六步：</strong>竞购成功。</p>
        <p><strong>第七步：</strong>线下签约。</p>
        <div class="screenshot"></div>
        <p class="blod mt15 mb10"><a class="sek_tit" name="3">砍价-购房</a></p>
        <p>在规定时间内开发商提供一定数量的房源并设置底价，用户出价最低且唯一则视为竞购成功。</p>
        <p>砍价-购房流程：</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_6.jpg" /></div>
        <p><strong>第一步：</strong>登录网站。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_13.jpg" /></div>
        <p><strong>第二步：</strong>点击“立刻砍价”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_14.jpg" /></div>
        <p><strong>第三步：</strong>确认个人信息。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_15.jpg" /></div>
        <p><strong>第四步：</strong>支付保证金。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_16.jpg" /></div>
        <p><strong>第五步：</strong>出价。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_17.jpg" /></div>
        <p><strong>第六步：</strong>竞购成功。</p>
        <p><strong>第七步：</strong>线下签约。</p>
        <p class="blod mt15 mb10"><a class="sek_tit" name="4">一口价-购房</a></p>
        <p>开发商针对一定数量的房源进行一口价出售，在规定时间内用户谁第一个出此价格则视为竞购成功。</p>
        <p>一口价-购房流程：</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_18.jpg" /></div>
        <p><strong>第一步：</strong>登录网站。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_19.jpg" /></div>
        <p><strong>第二步:</strong>点击“立刻购买”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_20.jpg" /></div>
        <p><strong>第三步:</strong>购买成功、确认个人信息。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_21.jpg" /></div>
        <p><strong>第四步：</strong>支付保证金。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_22.jpg" /></div>
        <p><strong>第五步：</strong>竞购成功。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/yh_22_1.jpg" /></div>
        <p><strong>第六步：</strong>线下签约。</p>
        <!--<p class="blod mt15 mb10"><a class="sek_tit" name="5">限时折扣-购房</a></p>
        <p>开发商针对一定数量的房源进行打折出售，在规定时间内报名，谁第一个出价则视为购买成功。</p>
        <p>限时折扣-购房流程：</p>
        <div class="screenshot"><img src="../images/help/yh_23.jpg" /></div>
        <p><strong>第一步：</strong>登录网站。</p>
        <div class="screenshot"><img src="../images/help/yh_24.jpg" /></div>
        <p><strong>第二步：</strong>点击“立刻购买”。</p>
        <div class="screenshot"><img src="../images/help/yh_25.jpg" /></div>
        <p><strong>第三步：</strong>购买成功、确认个人信息。</p>
        <div class="screenshot"><img src="../images/help/yh_26.jpg" /></div>
        <p><strong>第四步：</strong>支付保证金。</p>
        <div class="screenshot"><img src="../images/help/yh_27.jpg" /></div>
        <p><strong>第五步：</strong>竞购成功。</p>
        <p><strong>第六步：</strong>线下签约。</p>
        <p class="blod mt15 mb10"><a class="sek_tit" name="6">阶梯团购-购房</a></p>
        <p>开发商提供一定数量房源，用户以先到先得方式在线抢购房源，每达到一定的团购套数，所有抢购成功的买家，可获得对应的优惠折扣，活动结束后，按照统一优惠折扣进行付款。</p>
        <p>阶梯团购-购房流程：</p>
        <div class="screenshot"><img src="../images/help/yh_28.jpg" /></div>
        <p><strong>第一步：</strong>登录网站。</p>
        <div class="screenshot"><img src="../images/help/yh_29.jpg" /></div>
        <p><strong>第二步：</strong>点击“立刻报名”。</p>
        <div class="screenshot"><img src="../images/help/yh_30.jpg" /></div>
        <p><strong>第三步：</strong>确认个人信息（即报名）。</p>
        <div class="screenshot"><img src="../images/help/yh_31.jpg" /></div>
        <p><strong>第四步：</strong>支付保证金。</p>
        <div class="screenshot"><img src="../images/help/yh_32.jpg" /></div>
        <p><strong>第五步：</strong>竞购成功。</p>
        <p><strong>第六步：</strong>线下签约。</p>
        <p class="blod mt15 mb10"><a class="sek_tit" name="7">排号-购房</a></p>
        <p>在规定时间内，开发商提供一定数量房源，竞购人在线进行报名排号，达到设定的前多少名即可获得购房资格，并在活动结束后线下选房付款购房。</p>
        <p>排号-购房流程：</p>
        <div class="screenshot"><img src="../images/help/yh_33.jpg" /></div>
        <p><strong>第一步：</strong>登录网站。</p>
        <div class="screenshot"><img src="../images/help/yh_34.jpg" /></div>
        <p><strong>第二步：</strong>点击“立刻报名”。</p>
        <div class="screenshot"><img src="../images/help/yh_35.jpg" /></div>
        <p><strong>第三步：</strong>报名排号。</p>
        <div class="screenshot"><img src="../images/help/yh_36.jpg" /></div>
        <p><strong>第四步：</strong>支付保证金。</p>
        <div class="screenshot"><img src="../images/help/yh_37.jpg" /></div>
        <p><strong>第五步：</strong>竞购成功。</p>
        <p><strong>第六步：</strong>线下签约。</p>-->
      </div>
    </div>
    <div class="shadowHR">阴影</div>
  </div>
  <!-- InstanceEndEditable -->
</div>

</asp:Content>
