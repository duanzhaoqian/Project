<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	如何登录
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
        <h6>如何登录</h6>
        <p>进入网站首页，点击头部登录下的“开发商”，进入登录页面。</p>
        <div class="screenshot mb15"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/kfsdl.jpg" /></div><br /><div class="screenshot mb15"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rd_1.jpg" /></div>
      </div>
    </div>
    <div class="shadowHR">阴影</div>
  </div>
  <!-- InstanceEndEditable -->
</div>

</asp:Content>
