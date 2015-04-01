<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXOrm.DeveloperMessage>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <%
        if (Model.Count > 0)
        { 
            foreach (var item in Model)
            {
                %>
                <tr>
                    <td align="center"><%=item.IsRead ? "<span class=\"read\"></span>" : "<span class=\"unread\"></span>" %></td>
                    <td width="70%" class="tx_l">
                        <p class="lineh_20"><%=item.IsRead ? item.Content : String.Format("<strong>{0}</strong>", item.Content)%><span class="col999 ml10">[<%=item.CreateTime%>]</span></p>
                    </td>
                    <td width="20%"><a href="javascript:void(0)" class="ml5 mr5" onclick="funReadOrDel(<%=item.Id%>, true)">标记已读</a> | <a href="javascript:void(0)" class="ml5" onclick="funReadOrDel(<%=item.Id%>, false)">移除该记录</a></td>
                </tr>
                <%
            }
        }
        else
        {
            %><tr><td><center>对不起，暂无记录！</center></td></tr><%
        }
    %>
</table>
<div class="fen">
    <div class="tar">
        <%=Ajax.Pager(Model, new PagerOptions
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
<input type="hidden" id="hidCurrPage" value="<%=Model.CurrentPageIndex%>" />
<input type="hidden" id="hidTotalItem" value="<%=Model.TotalItemCount%>" />
<script type="text/javascript">
    var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
    function funReadOrDel(mid, flag) {
        var pageSize = 10;
        var currPage = parseInt($("#hidCurrPage").val());
        var totalItem = parseInt($("#hidTotalItem").val());
        $.post(mainUrl + "home/ajaxupdatemessagereadordel", { mid: mid, flag: flag, m: Math.random() }, function (data) {
            if (data > 0) {
                //计算删除一个后的总页数
                if (!flag)
                    totalItem -= 1;
                var totalPage = parseInt((totalItem + pageSize - 1) / pageSize);
                var toPage = totalPage < currPage ? totalPage : currPage;
                $.post(mainUrl + "home/message", { page: toPage, key: '<%=ViewData["Keyword"]%>' }, function (data) {
                    $("#divAjaxData").html(data);
                });
            }
            else {
                alert("操作失败");
            }
        });
    }
</script>
