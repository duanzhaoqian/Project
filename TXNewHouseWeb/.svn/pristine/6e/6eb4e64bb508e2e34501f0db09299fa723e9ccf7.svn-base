<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXModel.Dev.CT_DeveAndIdenInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	新房后台-首页
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="content">
    <% 
        if (Model.State != 1)
        {
            %>
            <div class="yellow_box mb10">
	            <p class="ts1 mt2 mb2">您尚未通过身份认证，通过认证方可使用本平台<input type="button" value="马上认证" class="btn_w80 ml10" onclick="javascript:window.location='<%=TXCommons.GetConfig.BaseUrl%>home/identification'" /></p>
            </div>
            <%
        }    
    %>    
    <div class="green_box">
	    <dl>
    	    <dt>
    	        <%TXCommons.PictureModular.UserPictureInfo picture = ViewData["UserPhoto"] as TXCommons.PictureModular.UserPictureInfo;
               string picurl = picture == null ? String.Concat(TXCommons.GetConfig.ImgUrl, "images/img.jpg") :string.IsNullOrEmpty( picture.Path)?String.Concat(TXCommons.GetConfig.ImgUrl, "images/img.jpg"):String.Concat(picture.Path, ".80_80.jpg");
    	               %>
    	        <img src="<%=picurl%>" onerror="javascript:this.src='<%=TXCommons.GetConfig.ImgUrl%>images/img.jpg'" width="80px" height="80px" /><p><a href="<%=TXCommons.GetConfig.BaseUrl%>home/updatephoto">修改头像</a> | <a href="<%=TXCommons.GetConfig.BaseUrl%>home/updatepassword">修改密码</a></p>
    	    </dt>
            <dd>
        	    <ul class="mt10">
            	    <li>登录名：<%=Model.LoginName%></li>
                    <li>手机号码：<%=Model.Mobile%></li>
                    <li>电子邮箱：<%=String.IsNullOrWhiteSpace(Model.Email) ? "--" : Model.Email%></li>
                    <li>公司名称：<%=String.IsNullOrWhiteSpace(Model.CompanyName) ? "--" : Model.CompanyName%></li>
                    <li>注册时间：<%=Model.CreateTime.ToString("yyyy-MM-dd HH:mm")%></li>
                    <li>上次登录：<%=Model.OldLoginTime.ToString("yyyy-MM-dd HH:mm")%></li>
                </ul>
                <div class="mt15">
                    <% 
                        if (Model.State == 1 && Model.Type == 1)
                        {
                            %><span class="rz_g_1"></span><%
                        }
                    %>                    
                    <% 
                        if (Model.State == 1 && Model.Type == 2)
                        {
                            %><span class="rz_y_1 ml5"></span><%
                        }
                    %>
                </div>
            </dd>
        </dl>
    </div>
</div>
</asp:Content>
