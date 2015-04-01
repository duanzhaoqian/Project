<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<List<TXModel.NHouseActivies.Discount.CT_UserList>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-限时折扣报名用户列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <h4 class="title_h4 mt15">
            <span>
                <%=ViewData["Title"] %>--限时折扣报名用户列表 </span>
            <div class="font14 fr mr35 mt10">
                <a href="javascript:void(0)" onclick="ExportExcel()">导出此列表</a></div>
        </h4>
        <div class="shaibox mt15 clearFix">
            <div class="fl">
                筛选：</div>
            <div class="selbox mr10" id="div_loupan">
                <span id="span_loupan" class="pl10">请选择</span>
                <div class="sel_list w300" style="display: none;">
                    <a href="javascript:void(0)" onclick="premisesChange(0,'请选择')">请选择</a>
                    <%if (ViewData["Premises"] != null)
                      {
                          foreach (TXOrm.Premis item in ViewData["Premises"] as List<TXOrm.Premis>)
                          { 
                    %>
                    <a href="javascript:void(0)" onclick="premisesChange(<%=item.Id %>,'<%=item.Name %>')">
                        <%=item.Name %></a>
                    <% }
                      }
                    %>
                </div>
            </div>
            <div class="selbox mr10" id="div_loudong">
                <span class="pl10" id="span_loudong">请选择</span>
                <div class="sel_list w300" style="display: none;" id="div_loudonglist">
                    <a href="javascript:void(0)" onclick="buildingChange(0,'请选择')">请选择</a>
                </div>
            </div>
            <div class="selbox mr10" id="div_danyuan">
                <span class="pl10" id="span_danyuan">请选择</span>
                <div class="sel_list" style="display: none;" id="div_danyuanlist">
                    <a href="javascript:void(0)" onclick="danyuanChange(0,'请选择')">请选择</a>
                </div>
            </div>
            <input type="button" class="bnt_serch fl" onclick="search()" />
            <input type="hidden" id="hid_Premises" name="hid_Premises" value="0" />
            <input type="hidden" id="hid_Building" name="hid_Building" value="0" />
            <input type="hidden" id="hid_Unit" name="hid_Unit" value="0" />
        </div>
        <div class="mt15 clearFix" style="position: relative;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
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
                        出价的房源
                    </th>
                    <th width="11%">
                        出价时间
                    </th>
                </tr>
                <tbody id="tbody">
                    <%if (Model != null && Model.Count() > 0)
                      {
                          int i = 1;
                          foreach (TXModel.NHouseActivies.Discount.CT_UserList item in Model)
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
                            <center>
                                对不起，暂无记录！</center>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#div_loupan").click(function () {
                $(this).find("div").toggle();
            });
            $("#div_loudong").click(function () {
                $(this).find("div").toggle();
            });
            $("#div_danyuan").click(function () {
                $(this).find("div").toggle();
            });
            $(".sel_list").mouseleave(function () {
                $(this).hide();
            });
        });
        //选择楼盘
        function premisesChange(pid, name) {
            $("#hid_Premises").val(pid);
            $("#hid_Building").val(0);
            $("#hid_Unit").val(0);
            var PremisesId = $("#hid_Premises").val(); //楼盘
            $("#span_loupan").html(name);
            $("#span_loudong").html("请选择");
            $("#div_loudonglist").html("<a href=\"javascript:void(0)\" onclick=\"buildingChange(0,'请选择')\">请选择</a>");
            $("#span_danyuan").html("请选择");
            $("#div_danyuanlist").html("<a href=\"javascript:void(0)\" onclick=\"danyuanChange(0,'请选择')\">请选择</a>");
            $.getJSON("<%=TXCommons.GetConfig.BaseUrl%>Activity/GetBuildingByPremisesId", { premisesId: PremisesId }, function (data) {
                if (data.length > 0) {
                    var str = "<a href=\"javascript:void(0)\" onclick=\"buildingChange(0,'请选择')\">请选择</a>";
                    for (var i = 0; i < data.length; i++) {
                        str += "<a href=\"javascript:void(0)\" onclick=\"buildingChange(" + data[i].Id + ",'" + data[i].Name + "')\">" + data[i].Name + "</a>";
                    }
                    $("#div_loudonglist").html(str);
                }
            });
        }
        //选择楼栋
        function buildingChange(bid, name) {
            $("#hid_Building").val(bid);
            $("#hid_Unit").val(0);
            var BuildingId = $("#hid_Building").val(); //楼栋
            $("#span_loudong").html(name);
            $("#span_danyuan").html("请选择");
            $("#div_danyuanlist").html("<a href=\"javascript:void(0)\" onclick=\"danyuanChange(0,'请选择')\">请选择</a>");
            $.getJSON("<%=TXCommons.GetConfig.BaseUrl%>Activity/GetUnitByBuildingId", { buildingId: BuildingId }, function (data) {
                if (data.length > 0) {
                    var str = "<a href=\"javascript:void(0)\" onclick=\"danyuanChange(0,'请选择')\">请选择</a>";
                    for (var i = 0; i < data.length; i++) {
                        str += "<a href=\"javascript:void(0)\" onclick=\"danyuanChange(" + data[i].Num + ",'" + data[i].Name + "')\">" + data[i].Name + "</a>";
                    }
                    $("#div_danyuanlist").html(str);
                }
            });
        }
        //选择单元
        function danyuanChange(uid, name) {
            $("#hid_Unit").val(uid); //单元
            $("#span_danyuan").html(name);
        }
        //搜索
        function search() {
            var premisesId = $("#hid_Premises").val();
            var buildingId = $("#hid_Building").val();
            var unitId = $("#hid_Unit").val();
            $("#tbody").html("");
            var str = "";
            $.getJSON("<%=TXCommons.GetConfig.BaseUrl%>Activity/SearchDiscountUser", { activityId: '<%=ViewData["ActivityId"] %>', premisesId: premisesId, buildingId: buildingId, unitId: unitId }, function (data) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        str += "<tr><td>" + parseInt(i + 1) + "</td><td>" + data[i].LoginName + "</td><td>" + data[i].RealName + "</td><td>" + data[i].IDCard + "</td><td>" + data[i].Mobile + "</td><td>" + data[i].QQ + "</td><td>" + data[i].Email + "</td><td>" + data[i].HouseName + "</td><td>" + data[i].CreateTime + "</td></tr>";
                    }
                } else {
                    str = "<tr><td colspan=\"9\"><center>对不起，暂无记录！</center></td></tr>";
                }
                $("#tbody").html(str);
            });
        }
        //导出
        function ExportExcel() {
            var activityId = '<%=ViewData["ActivityId"] %>';
            var premisesId = $("#hid_Premises").val();
            var buildingId = $("#hid_Building").val();
            var unitId = $("#hid_Unit").val();
            window.location = "<%=TXCommons.GetConfig.BaseUrl%>Activity/DiscountExportExcel?activityId=" + activityId + "&premisesId=" + premisesId + "&buildingId=" + buildingId + "&unitId=" + unitId + "";
        }
    </script>
</asp:Content>
