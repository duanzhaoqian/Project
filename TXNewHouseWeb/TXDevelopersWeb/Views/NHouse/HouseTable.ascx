<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXModel.Web.HouseAndBuilding>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
        <tr>
            <th width="7%">
                房号
            </th>
            <th width="5%">
                楼层
            </th>
            <th width="5%">
                单元
            </th>
            <th width="7%">
                楼栋名称
            </th>
            <th width="10%">
                户型
            </th>
            <th width="8%">
                建筑面积
            </th>
            <th width="11%">
                单价
            </th>
            <th width="11%">
                总价
            </th>
            <th width="8%">
                销售状态
            </th>
            <th width="12%">
                发布日期
                <%
                    if (Convert.ToInt32(ViewData["order"]) == 0)
                    {
                        %><a href="javascript:void(0)" id="order"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/arrow-tt.gif"></a><%
                    }
                    else
                    {
                        %><a href="javascript:void(0)" id="order"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/arrow-bb.gif"></a><%
                    }
                %>
            </th>
            <th width="15%">
                操作
            </th>
        </tr>
        <% foreach (var item in Model)
            { %>
        <tr>
            <td>
                <input type="checkbox" name="chkItem" class="bnt_checkbox" value="<%=item.Id %>" /><%=item.HouseName%>
            </td>
            <td>
                <%=item.Floor %>层
            </td>
            <td>
                <%=item.UnitName%>
            </td>
            <td>
                <%=item.BuildingName%><%=item.NameType%>
            </td>
            <td>
                <%=item.Room %>室<%=item.Hall %>厅<%=item.Toilet %>卫<%=item.Kitchen %>厨
            </td>
            <td>
                <%=item.Area %>平方米
            </td>
            <td>
                <%=string.Format("{0:F0}",item.SinglePrice)%>元/平方米
            </td>
            <td>
                <%=string.Format("{0:F0}",item.TotalPrice)%>万元/套
            </td>
            <td>
                <%string salesStatus = string.Empty; %>
                <% switch (item.SalesStatus)
                    { %>
                <% case 0: salesStatus = "待售"; break; %>
                <% case 1: salesStatus = "开发商保留"; break; %>
                <% case 2: salesStatus = "在售"; break; %>
                <% case 3: salesStatus = "已认购"; break; %>
                <% case 4: salesStatus = "已签约"; break; %>
                <% case 5: salesStatus = "已备案"; break; %>
                <% case 6: salesStatus = "已办产权"; break; %>
                <% case 7: salesStatus = "被限制"; break; %>
                <% case 8: salesStatus = "拆迁安置"; break; %>
                <% case 9: salesStatus = "售罄"; break; %>
                <%} %>
                <%=salesStatus%>
            </td>
            <td>
                <%=item.UpdateTime.ToString()%>
            </td>
            <td>
                <%
                    if (!string.IsNullOrEmpty(ViewData["rid"].ToString()))
                    {   
                        string rid = ViewData["rid"].ToString();  
                        if (rid == "1")
                    {
                        %>
                <a href="<%=TXCommons.GetConfig.BaseUrl%>NHouse/Edit?id=<%=item.Id%>" class="mr10">编辑</a> 
                <a href="javascript:void(0)" onclick="DelHouse(<%=item.Id%>);" class="mr10">删除</a>
                <a href="<%=TXCommons.GetConfig.GetSiteUrl%><%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.Id.ToString())%>" class="mr10" target="_blank">详情</a>
                <%
                    }
                    else if (rid == "0")
                    { 
                        %>
                <a href="javascript:void(0);" onclick="IsRelease(<%=item.Id%>)">发布</a> 
                <a href="<%=TXCommons.GetConfig.BaseUrl%>NHouse/Edit?id=<%=item.Id%>" class="mr10">编辑</a> 
                <a href="javascript:void(0)" onclick="DelHouse(<%=item.Id%>);" class="mr10">删除</a>
                <%
                    }
                    else
                    { 
                        %>
                <a href="<%=TXCommons.GetConfig.BaseUrl%>NHouse/Edit?id=<%=item.Id%>" class="mr10">编辑</a> 
                <a href="javascript:void(0);" onclick="IsRelease(<%=item.Id%>)">发布</a>
                <%
                    } 
                    }
                    else
                    { 
                        %>
                <a href="<%=TXCommons.GetConfig.BaseUrl%>NHouse/Edit?id=<%=item.Id%>" class="mr10">编辑</a> 
                <a href="javascript:void(0)" onclick="DelHouse(<%=item.Id%>);" class="mr10">删除</a>
                <a href="<%=TXCommons.GetConfig.GetSiteUrl%><%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(item.Id.ToString())%>" class="mr10" target="_blank">详情</a>
                <%
                    } 
                %>
            </td>
        </tr>
        <% } %>
        <%if (Model.Count <= 0)
            { %>
        <tr>
            <td colspan="11">
                对不起，暂无记录！
            </td>
        </tr>
        <%} %>
    </table>
    <div class="fen">
        <div class="tar">
            <span class="col333 font12 mr10">共<em class="col666"><%=ViewData["totalCount"].ToString()%></em>条记录</span>
            <%=Html.AjaxPager(Model, new PagerOptions
            {
                ContainerTagName = "span",
                CssClass = "",
                PageIndexParameterName = "page",
                NumericPagerItemCount = 10,
                ShowFirstLast = false,
                SeparatorHtml = "",
                PrevPageText = "<<",
                NextPageText = ">>",
                AlwaysShowFirstLastPageNumber = false,
                CurrentPagerItemWrapperFormatString = "<a class=\"hover\">{0}</a>",
            }, new AjaxOptions { UpdateTargetId = "divAjaxData" })%>
        </div>
    </div>
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/dialog_new.js")%>" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/ecmascript">
    var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
    //删除房源
    function DelHouse(hid) {
        if (confirm("确定要删除这条房源信息吗？")) {
            $.ajax({
                url: mainUrl + "NHouse/DeleteHouseById",
                type: "post",
                cache: false,
                data: { id: hid },
                success: function(data) {
                    if (data) {
                        alert(data.message);
                        location.reload();
                    }
                }
            });
        }
    }
    //单条发布房源
    function IsRelease(id) {
        if (confirm("确定要发布这条房源信息吗？")) {
            $.ajax({
                url: mainUrl + "NHouse/UpdateIsRelease?callback=?",
                type: "post",
                cache: false,
                dataType: "jsonp",
                data: { Ids: id },
                success: function(result) {
                    if (result) {
                        if (result.value > 0) {
                            alert("发布成功");
                        } else {
                            alert("发布失败");
                        }
                        location.reload();
                    }
                }
            });
        }
    }
</script>
