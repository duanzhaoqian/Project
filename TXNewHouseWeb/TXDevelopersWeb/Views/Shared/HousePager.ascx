<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<HouseInfoModel>>" %>
<%@ Import Namespace="TXModel.NHouseActivies.Discount" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>


  <div id="divpages" class="mt15 clearFix" style="position:relative;">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
      <tr>
        <th>序号</th>
        <th>楼层</th>
        <th>楼号</th>
        <th>户型</th>
        <th>建筑面积</th>
        <th>参考单价</th>
        <th>参考总价</th>
        <th>销售状态</th>
        <th>发布日期</th>
        <th>操作</th>
      </tr>
      <% 
          if (Model != null)
         {
             for (int i = 0; i < Model.Count; i++)
             {%>
              <tr>
                <td><input type="checkbox" class="valign2 mr5" name="box" join-data="0" developid="<%=Model[i].DeveloperId %>" premid="<%=Model[i].PremisesId %>" buildid="<%=Model[i].BuildingId %>" value="<%=Model[i].Id %>" /><%=(Model.CurrentPageIndex-1)*Model.PageSize+1+i%></td>
                <td><%=Model[i].Floor%>层</td>
                <td><%=Model[i].HouseNo%>号楼</td>
                <td><%=Model[i].Room%>室<%=Model[i].Hall%>厅<%=Model[i].Toilet%>卫<%=Model[i].Kitchen%>厨</td>
                <td><%=Model[i].BuildingArea%>平方米</td>
                <td><%=Model[i].SinglePrice%>元/平方米</td>
                <td><%=Model[i].TotalPrice%>万元/套</td>
                <td><%=Model[i].SalesStatus%></td>
                <td><%=Model[i].CreateTime%></td>
                <td><a class="alinkjoin" join="0" data="<%=Model[i].Id %>">参加</a></td>
             </tr>
       <%}
     }%>
    </table>
        
    <div class="fen clearFix" id="bu-34">
  	<input type="button" value="完成设置" class="btn_w80 fl ml20" id="btnCompleteok" />
    <div class="tar fr">

    <span class="col333 font12 mr10">共 <em class="col666"><%=Model.TotalItemCount %></em> 条记录</span> 
     <%= Ajax.Pager(Model, new PagerOptions()
    {   
        ContainerTagName = "span",
        PageIndexParameterName = "Id",
        NumericPagerItemCount = 10,
        ShowFirstLast = false,
        SeparatorHtml = "",
        PrevPageText = "<<",
        NextPageText = ">>",
        AlwaysShowFirstLastPageNumber = false,
        CurrentPagerItemWrapperFormatString = "<a class=\"hover\">{0}</a>"
    }, new AjaxOptions { UpdateTargetId = "divpages" })%>
    </div>
  </div>
  </div>