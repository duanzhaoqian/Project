<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl< List<TXOrm.Advertise>>" %>
<div class="pubPage">
    当前有<span id="AdCountLb">
        <%=Model.Count %></span>条广告设置</div>
<table class="DataTableA" border="0" cellpadding="0" width="100%" id="AdList">
    <thead>
        <tr>
            <th width="5%">
                排序号
            </th>
            <th>
                楼盘名称
            </th>
            <th>
                推荐时间设置
            </th>
            <th>
                操作
            </th>
        </tr>
    </thead>
    <tbody>
        <%if (Model != null)
          {
        %>
        <% for (int i = 0; i < Model.Count; i++)
           { %>
        <tr>
            <td>
                <%= Html.TextBoxFor(m => Model[i].SequenceNum, new { @class="seq"})%>
            </td>
            <td>
                <span class="pname">
                    <%=Model[i].PremisesName%></span>
                <%=Html.HiddenFor(m => Model[i].PremisesId)%>
            </td>
            <td>
                <input type="text" class="begin" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'endtime<%=i %>\',{d:-0});}'})"
                    id="begintime<%=i %>" value="<%=Model[i].BeginTime.ToString("yyyy-MM-dd") %>" />
                -
                <input type="text" class="end" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'begintime<%=i %>\',{d:0});}'})"
                    id="endtime<%=i %>" value="<%=Model[i].EndTime.ToString("yyyy-MM-dd") %>" />
            </td>
            <td>
                <a href="javascript:void()" class="del" onclick="deleteAd(this)">删除</a>
                <input type="hidden" value="<%=Model[i].Id %>" />
            </td>
        </tr>
        <% } %>
        <%} %>
    </tbody>
</table>

