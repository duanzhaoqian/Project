<%--<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<TXModel.AdminPVM.PVE_NH_PremisesAllImg<TXCommons.PictureModular.PremisesPictureInfo>>>" %>--%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl< List<TXOrm.PremiseImgMap>>" %>
<% if (Model != null && Model.Count > 0)
   {
%><div>
    <% foreach (var item in Model)
       { %>
    <div class="imgbox">
        <% if (item.ImgIsDel == null || !item.ImgIsDel.Value)
           { %>
        <a style="cursor: pointer; position: absolute; right: 10px;" onclick="DelImg('<%= item.ImgID %>','<%= item.InnerCode %>','<%= item.PictureType %>','<%= item.CityID %>','<%= item.ID %>',this)"
            class="close">&nbsp;</a>
        <img data="<%= "楼盘：" + item.PremiseName + "<br/>图片类型：" + item.PictureTypeName + "<br/>图片标题：" + item.ImgTitle + "<br/>图片描述：" + item.ImgDes + "<br/>" %>"
            src="<%= item.ImgPath %>" />
        <% }
           else
           { %>
        <a style="cursor: pointer; position: absolute; right: 10px;" class="close" onclick="javascript:alert('图片已经删除')">
        </a>
        <img data="<%= "楼盘：" + item.PremiseName + "<br/>图片类型：" + item.PictureTypeName + "<br/>图片标题：" + item.ImgTitle + "<br/>图片描述：" + item.ImgDes + "<br/>" %>"
            src="<%=Auxiliary.Instance.NhWebThemeUrl("images/icons/icon_delpng.png") %>" />
        <% } %>
    </div>
    <% } %>
</div>
<%--<div>
    <%
       foreach (var item in Model)
       {  %>
    <% if (item.ImgList != null && item.ImgList.Count > 0)
       {
           foreach (var img in item.ImgList)
           {  %>
    <div class="imgbox">
        <a style="cursor: pointer;" onclick="DelImg('<%= img.ID %>','<%=item.InnerCode %>','<%=img.PictureType %>','<%=item.CityID %>',this)"
            class="close">&nbsp;</a>
        <img data="<%="楼盘："+item.PremisesName+"<br/>图片类型："+img.PictureTypeName+"<br/>图片标题："+ img.Title +"<br/>图片描述："+img.Desc+"<br/>"%>"
            src="<%=img.Path %>" />
    </div>
    <% }
       } %>
    <% }%>
</div>--%>
<%
   } %>
