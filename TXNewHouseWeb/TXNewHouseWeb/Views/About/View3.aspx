<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	如何找回密码
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
        <h6>如何找回密码</h6>
        <p class=""><strong>第一步：</strong>进入登录页面，点击“忘记密码”。</p>
        <div class="screenshot"><img width="700" src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rm_1.jpg"></div>
        <p><strong>第二步：</strong>输入您的手机号，点击“下一步”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rm_2.jpg"></div>
        <p class=""><strong>第三步：</strong>系统会将验证码发送到您报名注册时填写的手机号码，输入验证码和新密码，点击“提交”按钮。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rm_3.jpg"></div>
        <p class=""><strong>第四步：</strong>密码修改成功！</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/rm_4.jpg"></div>
        <p>如果是经纪人，也可以直接拨打客户服务热线：400-999-3535（工作日9:00-18:00）进行初始密码。然后登录管理后台，修改密码。</p>
      </div>
    </div>
    <div class="shadowHR">阴影</div>
  </div>
  <!-- InstanceEndEditable -->
</div>

</asp:Content>
