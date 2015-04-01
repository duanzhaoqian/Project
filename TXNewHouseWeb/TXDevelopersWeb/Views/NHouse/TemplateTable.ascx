<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXOrm.HouseTemplate>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
    <tr>
        <th width="31%">
            模版名称
        </th>
        <th width="14%">
            模版备注
        </th>
        <th width="45%">
            操作
        </th>
    </tr>
    <%
        foreach (var p in Model)
        {
    %>
    <tr>
        <td>
            <%=p.Title%>
        </td>
        <td>
            <%=p.Remark%>
        </td>
        <td>
            <a href="/NHouse/TemplateEdit?id=<%=p.Id %>" class="mr10">编辑</a><a onclick="DelTemplate(<%=p.Id %>);" href="javascript:void(0)" class="mr10">删除</a>
        </td>
    </tr>
    <%  }%>
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
        <span class="col333 font12 mr10">共<em class="col666">
            <%=ViewData["totalCount"].ToString()%></em>条记录</span>
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

<script language="javascript" type="text/javascript">
    var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";

    function DelTemplate(tid) {
        if (confirm("您确定要删除该房源模板？")) {
            $.ajax({
                url: mainUrl + "NHouse/DeleteTemplateById",
                type: "post",
                cache: false,
                data: { id: tid },
                success: function(data) {
                    if (data) {
                        alert(data.message);
                        location.reload();
                    }
                }
            });
        }
    }    

</script>
