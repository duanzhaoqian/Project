<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<List<TXOrm.BidPrice>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-用户报名列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <h4 class="title_h4 mt15">
            <span>
                <%=ViewData["Title"] %>-摇号报名用户列表 </span>
            <div class="font14 fr mr35 mt10">
                <a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/YaoHaoExportExcel?id=<%=ViewData["Activityid"] %>">
                    导出此列表</a></div>
        </h4>
        <div class="mt15 clearFix" style="position: relative;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
                <tr>
                    <th width="4%">
                        序号
                    </th>
                    <th width="7%">
                        报名人
                    </th>
                    <th width="14%">
                        身份证号
                    </th>
                    <th width="10%">
                        户口复印件
                    </th>
                    <th width="10%">
                        手机号
                    </th>
                    <th width="9%">
                        摇号号码
                    </th>
                    <th width="11%">
                        报名时间
                    </th>
                </tr>
                <%if (Model != null && Model.Count > 0)
                  {
                      int i = 1;
                      foreach (TXOrm.BidPrice item in Model)
                      {
                %>
                <tr>
                    <td>
                        <%=i %>
                    </td>
                    <td>
                        <%=item.BidUserName %>
                    </td>
                    <td>
                        <%=item.IDCard %>
                    </td>
                    <td>
                        <a href="<%=item.InnerCode %>">点击查看</a>
                    </td>
                    <td>
                        <%=item.BidUserMobile %>
                    </td>
                    <td>
                        <%=item.BidUserNumber %>
                    </td>
                    <td>
                        <%= string.Format("{0:yyyy-MM-dd HH:mm}",item.CreateTime)%>
                    </td>
                </tr>
                <% i++;
                      }
                  }
                  else
                  {  %>
                <tr>
                    <td colspan="8">
                        <center>对不起，暂无记录！</center>
                    </td>
                </tr>
                <%} %>
            </table>
        </div>
    </div>
</asp:Content>
