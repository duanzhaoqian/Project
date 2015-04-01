<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TXModel.Web.PremisesHouse>" %>
<%@ Import Namespace="TXOrm" %>
<% 
    TXOrm.Activity _activity = new TXOrm.Activity();
    _activity = (TXOrm.Activity)ViewData["activity"];
    switch (Model.ActivityType)
    {
       case 5: //竞价活动房源
           //如果房源是可售状态并且活动时间没有结束时显示，否则不显示
           if (Model.SalesStatus == 1 && _activity.EndTime>DateTime.Now)
           {
%>
<div class="w1190 mb15">
    <div class="yellow_box1 r_y_jp clearFix">
        <div class="part1">
            <p class="col333 font18 fontYaHei">
                <%=Model.PremisesName%></p>
            <p class="col666 font16 fontYaHei mt5">
                <%=Model.BuildingName %> <%=Model.UnitName %> <%=Model.Floor.ToString() %>层 <%=Model.HouseName %>号房</p>
        </div>
        <div class="part2">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <th align="right" scope="row" width="80">
                            起 拍 价：
                        </th>
                        <td>
                            <strong class="green mr10"><%= _activity.BidPrice.ToString("0") %></strong>万元
                        </td>
                        <th align="right" scope="row" width="60">
                            市场价格：
                        </th>
                        <td>
                            <strong class="green mlr3"><%=Model.TotalPrice.ToString("0") %></strong>万元
                        </td>
                        <th align="right" scope="row" width="60">
                            竞购方式：
                        </th>
                        <td>
                            竞价<span class="wenhao ml5"></span>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            加价幅度：
                        </th>
                        <td colspan="3">
                            必须为<span class="green mlr3"><%=_activity.AddPrice.ToString("0") %></span>元的整数倍
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            最大幅度：
                        </th>
                        <td colspan="3">
                            单次加价不高于<span class="green mlr3"><%=_activity.MaxPrice.ToString("0") %></span>元
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="part3">
            <p class="clock">
                竞购时间</p>
            <p class="col333 font14">
                <%=_activity.BeginTime.ToString("yyyy-MM-dd") %> - <%=_activity.EndTime.ToString("yyyy-MM-dd") %></p>
            <p class="col999 font14">
                <strong class="colff6600">2</strong> 天 <strong class="colff6600">13</strong> 时 <strong
                    class="colff6600">26</strong> 分 <strong class="colff6600">05</strong> 秒</p>
        </div>
        <div class="part4">
    
<%
               //如果活动还没有开始
               if (_activity.BeginTime > DateTime.Now)
               {
%>
        
            <a href="javascript:void(0);" class="s-link-d mt10">即将开始</a>
        
<%
               }
               //如果活动正在进行中
               else if (_activity.BeginTime <= DateTime.Now && _activity.EndTime > DateTime.Now)
               {
%>
            <a href="#" class="s-link-d mt10">立刻出价</a>
<% 
               }
               //如果活动时间已经结束
               else
               {
%>
            <a href="void(0);" class="s-link-d mt10">活动结束</a>
<%
               }
%>
        </div>
    </div>
</div>
<%
           }
%>
         
<%          
       break;
    }
%>
