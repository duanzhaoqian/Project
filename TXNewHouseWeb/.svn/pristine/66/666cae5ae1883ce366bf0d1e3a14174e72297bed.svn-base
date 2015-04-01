<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Premises>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="5%">
                操作
            </th>
            <th>
                楼盘名称
            </th>
            <th width="10%">
                物业类型
            </th>
            <th>
                开发商
            </th>
            <th width="10%">
                所在城市
            </th>
            <th width="10%">
                销售状态
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
                <input type="checkbox" value="<%=item.Id %>" class="checkpremises" />
            </td>
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
