<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master" Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_YaoHao>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=AdminPageInfo.PageTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a>
                <i>&gt;</i> <a href="javascript:void(0);">
                修改摇号活动</a></span>
    </div>
    <!--//current-->
      <!--//current-->
        <div class="data">
			<!-- InstanceBeginEditable name="EditRegion3" -->
            
                <div class="btnDiv tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                  <tr>
                    <th width="14%">活动名称：</th>
                    <td width="86%"><%=Model.Name %>&nbsp;网络摇号</td>
                  </tr>
                  <tr>
                    <th>报名时间：</th>
                    <td>
				 <%=Model.ApplyBeginTime.HasValue?Model.ApplyBeginTime.Value.ToString("yyyy-MM-dd HH:mm"):"--" %> - <%=Model.ApplyEndTime.HasValue?Model.ApplyEndTime.Value.ToString("yyyy-MM-dd HH:mm"):"--" %>
					</td>
                  </tr>
                  <tr>
                    <th>活动时间：</th>
                    <td><%=Model.BeginTime.ToString("yyyy-MM-dd HH:mm") %> - <%=Model.EndTime.ToString("yyyy-MM-dd HH:mm") %>
					</td>
                  </tr>
                  <tr>
                    <th>选房时间：</th>
                    <td>
                       <%=Model.LectHouseTime.ToString("yyyy-MM-dd HH:mm:ss")%>
						</td>
                  </tr>
                  <tr>
                    <th>摇号公证：</th>
                   <td><%=Model.NotarialOffice%>
                              </td>
                  </tr>
                  <tr>
                      <th>活动保证金金额：</th>
                      <td><%=Convert.ToDouble(Model.Bond) %>元</td>
                  </tr>
                  <tr>
                    <th>活动介绍：</th>
                    <td rowspan="2" style="word-wrap: break-word; word-break: break-all;">
						<%=Model.ActivitiesIntroduction%>
						</td>
                  </tr>
                  <tr>
                    <th></th>
                  </tr> <tr>
                    <th>摇号须知：</th>
                    <td rowspan="2" style="word-wrap: break-word; word-break: break-all;">
						<%=Model.ActivitiesNotice%>
						</td>
                  </tr>
                  <tr>
                    <th></th>
                  </tr> <tr>
                    <th>摇号流程：</th>
                    <td rowspan="2" style="word-wrap: break-word; word-break: break-all;">
						<%=Model.ActivitiesProcess%>
						</td>
                  </tr>
				  <tr>
				  <td></td>
				  <td></td>
				  </tr>
                </table>
                </div>
                <div class="btnDiv tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="tb1">
                  <tr>
                    <th width="14%">&nbsp;</th>
                    <td width="86%">
                    <input type="button" name="button6" id="button6" value="返回" onclick="window.location.href='<%= ViewData["backurl"]%>'" class="btn4 mr10"/>
                    </td>
                  </tr>
                </table>
                </div>            
			<!-- InstanceEndEditable -->
			</div>

</asp:Content>
