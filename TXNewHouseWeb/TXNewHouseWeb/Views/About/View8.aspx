<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	如何注册
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <div class="bgcolor">
	<div class="w1190"><i class="help_t"></i><i class="helptr"><a class="linkD" href="#"></a></i></div>
</div>
<!--//bgcolor end-->
<div class="clearfix">
<!-- InstanceBeginEditable name="EditRegion3" -->
  <div class="p_wrap">
    <div class="helpbox clearFix">
       <div class="silder">
          <%=Html.Partial("Menu")%>
      </div>
       <div class="main">
        <h6>如何注册</h6>
        <p><strong>第一步：</strong>进入网站首页，点击头部注册下的“开发商”。</p>
        <div class="screenshot mb15"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rz_1.jpg" /></div>
        <p><strong>第二步：</strong>进入注册页面，按要求填写相应信息，点击“完成注册”。</p>
        <div class="screenshot mb15"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rz_2.jpg" /></div>
        <p><strong>第三步：</strong>注册成功！您可以进入我的管理后台，也可以继续完善我的个人资料。</p>
        <div class="screenshot mb15"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rz_3.jpg" /></div>
      </div>
    </div>
    <div class="shadowHR">阴影</div>
  </div>
  <!-- InstanceEndEditable -->
</div>

</asp:Content>
