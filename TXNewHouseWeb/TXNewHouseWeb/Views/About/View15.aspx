<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	找不到图片上传按钮
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><div class="bgcolor">
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
        <h6>找不到图片上传按钮</h6>
        <p>因为您的浏览器未安装Flash插件，会导致图片上传按钮不显示。需要针对您使用的浏览器安装对应版本的Flash插件。</p>
        <p><b>IE、谷歌等浏览器Flash插件安装步骤：</b></p>
        <p>通过该地址<a style="color:#1d58c9;" target="_blank" href="http://get.adobe.com/cn/flashplayer/">http://get.adobe.com/cn/flashplayer/</a>下载并安装flash插件。插件安装步骤：</p>
        <p class="mt20 mb10"><b>第一步：</b>取消勾选可选程序，然后点击“立即安装”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/zbdan04.jpg"></div>
        <p class="mt20 mb10"><b>第二步：</b>初始化完成后，选择保存地址，单击“保存文件”按钮，并关闭浏览器。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/zbdan05.jpg"></div>
        <p class="mt20 mb10"><b>第三步：</b>在本地找到已保存的flash插件后双击。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/zbdan06.jpg"></div>
        <p class="mt20 mb10"><b>第四步：</b>待安装完成后，单击“完成”按钮。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/zbdan07.jpg"></div>
        <p class="mt20 mb10"><b>第五步：</b>设置完成。返回快有家刷新原页面即可。</p>
        <p><b>火狐浏览器Flash插件安装步骤：</b></p>
        <p>通过该地址<a style="color:#1d58c9;" target="_blank" href="http://get.adobe.com/cn/flashplayer/">http://get.adobe.com/cn/flashplayer/</a>下载flash插件并安装，插件安装步骤同IE、谷歌。火狐浏览器安装插件后，需设置插件状态步骤如下：</p>
        <p class="mt20 mb10"><b>第一步：</b>重新打开火狐浏览器，点击菜单栏“工具”&mdash;＞“附件组件”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/zbdan01.jpg"></div>
        <p class="mt20 mb10"><b>第二步：</b>在“附加组件管理器”页面，选择“插件”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/zbdan02.jpg"></div>
        <p class="mt20 mb10"><b>第三步：</b>找到“Shockwave Flash  11.9.900.117”控件，在下拉框选择“总是激活”。</p>
        <div class="screenshot"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/help/zbdan03.jpg"></div>
        <p class="mt20 mb10"><b>第四步：</b>即设置完成，返回快有家刷新原页面即可。</p>
      </div>
    </div>
    <div class="shadowHR">阴影</div>
  </div>
  <!-- InstanceEndEditable -->
</div>

</asp:Content>
