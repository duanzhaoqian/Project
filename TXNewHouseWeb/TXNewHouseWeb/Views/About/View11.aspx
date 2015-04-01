<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	如何发布楼栋
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
        <h6>如何发布楼栋</h6>
        <p><strong>第一步：</strong>登录快有家开发商后台，选择楼栋管理中的发布楼栋或者选择楼盘管理中的发布楼栋，进入发布楼栋页。</p>
        <div class="screenshot mb15"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rfd_1.jpg" /></div>
        <div class="screenshot mb15"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rfd_2.jpg" /></div>
        <p><strong>第二步：</strong>选择楼盘，填写楼栋信息、楼栋沙盘信息关联，点击“发布楼栋”。</p>
        <div class="screenshot mb15"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rfd_3.jpg" /></div>
      </div>
    </div>
    <div class="shadowHR">阴影</div>
  </div>
  <!-- InstanceEndEditable -->
</div>

</asp:Content>
