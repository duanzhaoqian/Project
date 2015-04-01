<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<List<TXModel.NHouseActivies.Biding.CT_UserList>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-竞价用户列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <h4 class="title_h4 mt15">
            <span>
                <%=ViewData["Title"] %>-竞价用户列表 </span>
            <div class="font14 fr mr35 mt10">
                <a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/BidingExportExcel?id=<%=ViewData["HouseId"] %>">
                    导出此列表</a>
            </div>
        </h4>
        <div class="mt15 clearFix" style="position: relative;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1" id="table1">
                <tr>
                    <th width="5%">
                    </th>
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
                        最后出价时间
                    </th>
                    <th width="7%">
                        出价次数
                    </th>
                    <th width="10%">
                        最后出价金额
                    </th>
                    <th width="7%">
                        操作
                    </th>
                </tr>
                <%if (Model != null && Model.Count() > 0)
                  {
                      int i = 1;
                      foreach (TXModel.NHouseActivies.Biding.CT_UserList item in Model)
                      { %>
                <tr>
                    <td>
                        <span id="span_<%=item.Id %>" class="yellow_bjtb" style="display: none;">
                            <%switch (item.Status)
                              {
                                  case 0:
                            %>
                            未中标
                            <%break;
                                  case 1:%>
                            中标
                            <%break;
                                  case 2:%>
                            成交
                            <% break;
                              } %>
                        </span>
                    </td>
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
                        <%=string.Format("{0:yyyy-MM-dd HH:mm}",item.LastBidTime)%>
                    </td>
                    <td>
                        <%=item.BidCount %>
                    </td>
                    <td>
                        <%=item.LastBidPrice %>
                    </td>
                    <td>
                        <a href="javascript:void(0)" onclick="done(<%=item.Id %>,<%=item.HouseId %>)">成交</a>
                    </td>
                </tr>
                <% i++;
                      }
                  }
                  else
                  {%>
                <tr>
                    <td colspan="12">
                        <center>对不起，暂无记录！</center>
                    </td>
                </tr>
                <%} %>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".yellow_bjtb").each(function () {
                if ($.trim($(this).html()) == "未中标") {
                    $(this).hide();
                }
                else {
                    $(this).show();
                }
            })
            //是否已成交
            if ('<%=ViewData["Status"] %>' == 2) {
                $("#table1").find("a").each(function () {
                    $(this).css("display", "none");
                })
            }
        })
        function done(id, houseId) {
            if (confirm("确定成交？")) {
                $.get("<%=TXCommons.GetConfig.BaseUrl%>Activity/AnnounceActivitieResult", { bidPriceId: id, houseId: houseId }, function (data) {
                    if (data == "1") {
                        alert("操作成功！");
                        $("#span_" + id).html("成交");
                        $("#span_" + id).show();
                        $("#table1").find("a").each(function () {
                            $(this).css("display", "none");
                        })
                        return;
                    }
                    else {
                        alert("操作失败！");
                        return;
                    }
                })
            }
        }
    </script>
</asp:Content>
