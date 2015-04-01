<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Premises>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="15%">
                楼盘名称
            </th>
            <th width="10%">
                物业类型
            </th>
            <th width="20%">
                开发商
            </th>
            <th width="10%">
                所在城市
            </th>
            <th width="10%">
                销售状态
            </th>
            <th width="35%" class="lasted">
                操作
            </th>
        </tr>
    </thead>
    <tbody class="test">
        <% if (Model != null && Model.Count > 0)
           {
               foreach (var item in Model)
               {
        %>
        <tr>
            <td>
                <%= item.Name %>
            </td>
            <td>
                <%=(item.PropertyTypeString).TrimEnd(',') %>
            </td>
            <td>
                <%= item.Developer %>
            </td>
            <td>
                <%= item.ProvinceName %>-<%=item.CityName %>
            </td>
            <td>
                <%=item.SalesStatusString %>
            </td>
            <td class="lasted">
                <a href="<%=Url.SiteUrl().ModifyPremises(item.Id)%>">编辑楼盘</a>&nbsp; &nbsp; <a href="<%=Url.SiteUrl().BuildingIndex %>?premisesid=<%=item.Id %>">
                    楼栋管理</a>&nbsp; &nbsp; <a href="<%=Url.SiteUrl().NewHouseIndex_FromPremises(item.Id) %>">
                        房源管理</a>&nbsp; &nbsp; <a href="<%=Url.SiteUrl().Building_Search("addnewbuilding") %>?premisesid=<%=item.Id %>">
                            发布楼栋</a>&nbsp; &nbsp; <a href="<%=Url.SiteUrl().AddNewHouse(item.Id,0)%>">发布房源</a>&nbsp;
                &nbsp; <a href="<%=Url.SiteUrl().PremisesImgs(item.Id,0)%>">楼盘相册</a> <a href="javascript:;"
                    onclick="delpremises(<%=item.Id %>,this);">删除楼盘</a>
            </td>
        </tr>
        <%
               }
           }
           else
           {
        %>
        <tr>
            <td colspan="12">
                暂无数据！
            </td>
        </tr>
        <%
           } %>
    </tbody>
</table>
