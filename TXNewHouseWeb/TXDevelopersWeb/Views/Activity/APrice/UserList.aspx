<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<List<TXModel.NHouseActivies.APrice.CT_UserList>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	营销中心-一口价用户列表
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="content">
        <h4 class="title_h4 mt15">
            <span>
                <%=ViewData["Title"] %>-一口价用户列表 </span>
            <div class="font14 fr mr35 mt10">
                <a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/APriceExportExcel?id=<%=ViewData["HouseId"] %>">
                    导出此列表</a>
            </div>
        </h4>
        <div class="mt15 clearFix" style="position: relative;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1" id="table1">
                <tr>   
                    <th width="4%">
                        序号
                    </th>
                    <th width="5%">
                        出价人
                    </th>
                    <th width="7%">
                        真实姓名
                    </th>
                    <th width="14%">
                        身份证号
                    </th>
                    <th width="9%">
                        手机号
                    </th>
                    <th width="9%">
                        QQ号码
                    </th>
                    <th width="11%">
                        E-mail
                    </th>
                    <th width="12%">
                       出价时间
                    </th>
                </tr>
                <%if (Model != null && Model.Count() > 0)
                  {
                      int i = 1;
                      foreach (TXModel.NHouseActivies.APrice.CT_UserList item in Model)
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
                        <%=string.Format("{0:yyyy-MM-dd HH:mm}",item.BidTime)%>
                    </td>
                </tr>
                <% i++;
                      }
                  }
                  else
                  {%>
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
