<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<h5>
    新手指南</h5>
<ul>
    <li id="v1"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view1">&gt; 普通用户如何注册</a></li>
    <li id="v2"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view2">&gt; 普通用户如何登录</a></li>
    <li id="v3"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view3">&gt; 如何找回密码</a></li>
    <li id="v4"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view4">&gt; 支付限额</a></li>
    <li id="v5"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view5">&gt; 找房指南</a></li>
    <li id="v6"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view6">&gt; 保证金制度</a></li>
    <li id="v7"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view7">&gt; 营销活动说明</a></li>
</ul>
<h5 class="mt15">
    开发商帮助</h5>
<ul>
    <li id="v8"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view8">&gt; 如何注册</a></li>
    <li id="v9"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view9">&gt; 如何登录</a></li>
    <li id="v10"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view10">&gt; 如何发布楼盘</a></li>
    <li id="v11"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view11">&gt; 如何发布楼栋</a></li>
    <li id="v12"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view12">&gt; 如何发布房源</a></li>
    <li id="v13"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view13">&gt; 如何创建营销活动</a></li>
</ul>
<h5 class="mt15">
    常见问题</h5>
<ul>
    <li id="v14"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view14">&gt; 如何修改手机号</a></li>
    <li id="v15"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/about/view15">&gt; 找不到图片上传按钮</a></li>
</ul>
<script type="text/javascript">
    $(function () {
        var para = '<%=ViewData["para"] %>';
        $("li").removeClass("current"); 
        if (!isNaN(para))
            $("#v" + para).addClass("current");
        else
            $("#v1").addClass("current");
    });
</script>
