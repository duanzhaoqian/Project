<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXOrm.Developer_AccountLog>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table id="tb_table1" width="100%" cellspacing="0" cellpadding="0" border="0" class="tab1">
    <tr>
        <th>
            时间
        </th>
        <th>
            金额
        </th>
        <th>
            类型
        </th>
        <th>
            说明
        </th>
    </tr>
    <%if (Model != null && Model.Count > 0)
      {
          foreach (TXOrm.Developer_AccountLog item in Model)
          { %>
    <tr>
        <td>
            <%=item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") %>
        </td>
        <td>
            <%if (item.PayType == 1)
              {%>
            <strong class="green">-<%=item.Price.ToString().Split('.')[0] %>元</strong>
            <%}
              else
              {%>
            <strong class="orange">+<%=item.Price.ToString().Split('.')[0]%>元</strong>
            <%} %>
        </td>
        <td>
            <%switch (item.Type)
              {
                  case 1: %>
            保证金
            <%break;
                  case 2:%>
            账户提现
            <%  break;
              } %>
        </td>
        <td>
            <%=item.Desc %>
        </td>
    </tr>
    <%}
      }
      else
      {%><tr>
          <td colspan="4">
              <center>
                  对不起，暂无记录！</center>
          </td>
      </tr>
    <%} %>
</table>
<div class="fen">
    <div class="tar">
        <span class="col333 font12 mr10">共<em class="col666">
            <%=ViewData["TotalItemCount"]%></em> 条记录</span>
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
