<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<HouseInfoModel>>" %>
<%@ Import Namespace="TXModel.NHouseActivies.Discount" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<div class="font12 ml20 pl10" id="div_alljoin">
    <label class="ml20 pl10">
        <input type="checkbox" id="checkbox-all" onclick="CheckAll()" class="valign2 mr5" style="cursor: pointer" />全选
    </label>
    <a id="a-alljoin" join-data="0" class="ml20" onclick="JoinAll()" style="cursor: pointer">参加</a>
</div>
<div id="" class="mt15 clearFix" style="position: relative;">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1" id="div_house_list">
        <tr>
            <th>
                房号
            </th>
            <th>
                楼层
            </th>
            <th>
                单元
            </th>
            <th>
                楼号
            </th>
            <th>
                户型
            </th>
            <th>
                建筑面积
            </th>
            <th>
                单价
            </th>
            <th>
                总价
            </th>
            <th>
                销售状态
            </th>
            <th>
                发布日期
            </th>
            <th>
                操作
            </th>
        </tr>
        <% 
            if (Model != null && Model.Count > 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
        %>
        <tr>
            <td>
                <input type="checkbox" onclick="CheckSingle(this)" class="valign2 mr5" name="box" join-data="0" developid="<%=Model[i].DeveloperId %>" premid="<%=Model[i].PremisesId %>" buildid="<%=Model[i].BuildingId %>" unitid="<%=Model[i].UnitId %>" value="<%=Model[i].Id %>" housename="<%=Model[i].HouseNo%>" /><%=Model[i].HouseNo%>
            </td>
            <td>
                <%=Model[i].Floor%>层
            </td>
            <td><%=Model[i].UnitId%>单元</td>
            <td>
                <%=Model[i].BuildName%><%=Model[i].NameType%>
            </td>
            <td>
                <%=Model[i].Room%>室<%=Model[i].Hall%>厅<%=Model[i].Toilet%>卫<%=Model[i].Kitchen%>厨
            </td>
            <td>
                <%=Convert.ToInt32(Model[i].BuildingArea)%>平方米
            </td>
            <td>
                <%=Convert.ToInt32(Model[i].SinglePrice)%>元/平方米
            </td>
            <td>
                <%=Convert.ToInt32(Model[i].TotalPrice)%>万元/套
            </td>
            <td>
                <%= TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(Model[i].SalesStatus) %>
            </td>
            <td>
                <%=Model[i].CreateTime%>
            </td>
            <td>
                <a style="cursor: pointer" class="alinkjoin" join="0" data="<%=Model[i].Id %>" onclick="JoinSingle(this);">参加</a>
            </td>
        </tr>
        <%}
              }
              else
              {
        %><tr>
            <td colspan="10">
                对不起，暂无记录！
            </td>
        </tr>
        <%
          } 
        %>
    </table>
    <div class="fen clearFix" id="bu-34">
        <%if (Model != null)
          {
              if (Model.Count > 0)
              {%>
        <input type="button" value="完成设置" class="btn_w80 fl ml20" onclick="CompleteSet()" />
        <div class="tar fr">
            <span class="col333 font12 mr10">共 <em class="col666">
                <%=Model.TotalItemCount %></em> 条记录</span>
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
     }, new AjaxOptions { UpdateTargetId = "divpages", OnSuccess = "SetCheckBox" })%>
        </div>
        <%
           }
       } 
        %>
    </div>
</div>
