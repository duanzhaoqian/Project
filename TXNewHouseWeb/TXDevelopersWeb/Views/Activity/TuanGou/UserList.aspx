<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<List<TXModel.NHouseActivies.TuanGou.CT_UserList>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	营销中心-阶梯团购用户列表
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="content">
        <h4 class="title_h4 mt15">
            <span><%=ViewData["Title"] %>--阶梯团购用户列表 </span>
            <div class="font14 fr mr35 mt10">
                <a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/TuanGouExportExcel?id=<%=ViewData["Activityid"] %>" >导出此列表</a></div>
        </h4>
        <div class="mt15 clearFix" style="position: relative;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1" id="table1">
                <tr>
                    <th width="4%">
                        序号
                    </th>
                      <th width="7%">
                        出价人
                    </th>
                    <th width="8%">
                        真实姓名
                    </th>
                    <th width="14%">
                        身份证号
                    </th>
                    <th width="10%">
                        手机号
                    </th>
                     <th width="9%">
                        QQ号码
                    </th>
                    <th width="16%">
                        E-mail
                    </th>
                     <th width="21%">
                       团购房源
                    </th>
                    <th width="11%">
                        出价时间
                    </th>
                </tr>
                <%if (Model != null && Model.Count() > 0)
                  {
                      int i = 1;
                      foreach (TXModel.NHouseActivies.TuanGou.CT_UserList item in Model)
                      { %>
                <tr>
                    <td>
                        <%=i %>
                    </td>
                      <td>
                        <%=item.LoginName %>
                    </td>
                    <td>
                        <%=item.RealName %>
                    </td>
                    <td>
                        <%=item.IDCard %>
                    </td>
                    <td>
                        <%=item.Mobile %>
                    </td>
                     <td>
                        <%=item.QQ %>
                    </td>
                     <td>
                        <%=item.Email %>
                    </td>
                      <td>
                        <%=item.HouseName %>
                    </td>
                    <td>
                        <%=string.Format("{0:yyyy-MM-dd HH:mm}",item.CreateTime)%>
                    </td>
                </tr>
                <% i++;
                      }
                  }
                  else
                  {%>
                <tr>
                    <td colspan="9">
                        <center>对不起，暂无记录！</center>
                    </td>
                </tr>
                <%} %>
            </table>
        </div>
    </div>
</asp:Content>
