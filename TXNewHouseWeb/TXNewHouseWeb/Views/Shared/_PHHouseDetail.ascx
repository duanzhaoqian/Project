<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TXModel.Web.PremisesHouse>" %>
<%@ Import Namespace="TXOrm" %>
<% 
    TXOrm.Activity _activity = new TXOrm.Activity();
    _activity = (TXOrm.Activity)ViewData["activity"];

    //如果房源是可售状态并且活动时间没有结束时显示，否则不显示
    if (Model.SalesStatus == 2 && _activity.EndTime > DateTime.Now)
    {
%>
<div class="w1190 mb15">
    <div class="yellow_box1 r_y_ph clearFix">
        <div class="part1">
            <p class="col333 font18 fontYaHei">
                <%=Model.PremisesName%></p>
            <p class="col666 font16 fontYaHei mt5">
                <%=Model.BuildingName %>
                <%=Model.UnitName %>
                <%=Model.Floor.ToString() %>层
                <%=Model.HouseName %>号房</p>
        </div>
        <div class="part2">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <th scope="row" align="right" width="80">活动名称：</th>
                <td colspan="3"><strong class="col666 mr10"> <%=Model.PremisesName%></strong><strong class="col666">排号购房</strong></td>
              </tr>
              <tr>
                <th scope="row" align="right">有效排号：</th>
                <td width="130">前<strong class="green mlr3">100</strong>名</td>
                <th scope="row" align="right" width="80">房源套数：</th>
                <td><strong class="green mr3">200</strong>套</td>
              </tr>
              <tr>
                <th scope="row" align="right">竞购方式：</th>
                <td>排号购房<span class="wenhao ml5"></span></td>
                <th scope="row">&nbsp;</th>
                <td>&nbsp;</td>
              </tr>
            </table>
        </div>
        <div class="part3">
            <p class="clock">
                竞购时间</p>
            <p class="col333 font14">
                <%=_activity.BeginTime.ToString("yyyy-MM-dd") %>
                -
                <%=_activity.EndTime.ToString("yyyy-MM-dd") %></p>
            <p class="col999 font14">
                <strong class="colff6600">2</strong> 天 <strong class="colff6600">13</strong> 时 <strong
                    class="colff6600">26</strong> 分 <strong class="colff6600">5.16</strong> 秒</p>
        </div>
        <div class="part4">
<%
        //如果活动还没有开始
        if (_activity.BeginTime > DateTime.Now)
        {
%>
            <a href="javascript:void(0);" class="s-link-d mt10">活动即将开始</a>
<%
        }
        //如果活动正在进行中
        else if (_activity.BeginTime <= DateTime.Now && _activity.EndTime > DateTime.Now)
        {
%>
            <a href="#" class="s-link-d mt10">立刻排号</a>
<% 
        }
        //如果活动时间已经结束
        else
        {
%>
            <a href="void(0);" class="s-link-d mt10">活动结束等待</a>
<%
        }
%>
        </div>
    </div>
</div>
<%
    }
%>
<!--房源基本信息-->
<%Html.RenderPartial("~/Views/Shared/_HouseDetail.ascx", Model);%>

<div class="w1190">
<%
    //如果房源已经认购或签约
    if (Model.SalesStatus == 3 || Model.SalesStatus == 4)
    {
%>
    <div class="r_title_lp"><strong class="title_span pad_0_30">排名购房</strong><span class="col666 font12 ml20 mr15">买家出价:<i class="colff840b mr5 ml5">72</i>次</span><span class="col666 font12 mr15">当前共<i class="colff840b mr5 ml5">22</i>人参加</span><span class="col666 font12 mr15"><i class="colff840b mr5 ml5">2206</i>人围观</span></div>
    <div class="yellow_box1 yellow_box2 clearFix mb15">
        <div class="part2">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
              <tbody>
              <tr>
                <th align="right" scope="row">市场价格：</th>
                <td><strong class="green mr3">99.1</strong>万元</td>
                <th align="right" scope="row">房源套数：</th>
                <td><strong class="green mr3">200</strong>套</td>
              </tr>
              <tr>
                <th align="right" scope="row">有效排号：</th>
                <td>前<strong class="green mr3">100</strong>名</td>
                <th align="right" scope="row">竞购方式：</th>
                <td>排号购房<span class="wenhao ml5"></span></td>
              </tr>
              <tr>
                <th align="right" scope="row">竞购时间：</th>
                <td colspan="3">2012年07月28日 13:00 - 13:15</td>
              </tr>
            </tbody></table>
		</div>
        <div class="part5 mt15 mr20">
        	<div class="pos_abs pos_abs2"><input type="button" value="排号购房结束" class="btn_w163_gray"></div>
            <p class="tar mt10">共<span class="fontYahei font18 colff840b">100</span>人获得购房资格</p>
        </div>
    </div>
    <div class="gray_box gray_box1 clearFix">
        <div class="right">
        	<ul class="r_qh_title clearFix">
                <li class="on">前100名报名记录</li>
            </ul>
            <div class="margin15">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table_box1 td_col333">
                  <tr>
                  	<th scope="col">&nbsp;</th>
                    <th scope="col">买家</th>
                    <th scope="col">报名号</th>
                    <th scope="col">报名时间</th>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                  	<td><span class="yellow_bjtb">成交</span></td>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                </table>
            </div>
            <div class="page tac border_top">
                <span class="col333 font12 mr10">共 <em class="col666">50</em> 条记录</span> <a class="no">&lt;&lt;</a><a class="hover" href="">1</a><a href="">2</a><a href="">3</a><a href="">4</a><a href="">5</a><a href="">6</a><a href="">&gt;&gt;</a>
            </div>
        </div>
        <div class="left mr20">
            <div class="text_title">营销活动规则</div>
            <p class="text_p">  1、任何快有家用户会员均可参与房源出价；</p>
            <p class="text_p">  2、竞价中每次出价金额不得低于保底价，不高于封顶价，可多次参与同套房源出价；</p>
            <p class="text_p">  3、砍价中每次出价金额不得高于封顶价，不低于保底价价，可多次参与同套房源出价；</p>
            <%--<div class="text_title">QA答疑</div>
            <p class="text_p">Q：是否我出价最高就一定能成交吗？</p>
            <p class="text_p">A：出价信息只是给房东或经纪人一个价格意向，是否成交取决于与房东或经纪人线下磋商的结果。</p>
            <p class="text_p">Q：是否我出价最高就一定能成交吗？</p>
            <p class="text_p">A：出价信息只是给房东或经纪人一个价格意向，是否成交取决于与房东或经纪人线下磋商的结果。</p>--%>
        </div>
    </div>
<%
    }
    else
    {
%>
    <div class="r_title_lp"><strong class="title_span">参加本次排号购房的房源</strong></div>
    <ul class="home_ul clearFix">
    	<li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
        <li><a href="" class="blue">12号楼01单元2层201</a></li>
    </ul>
    <div class="r_title_lp"><strong class="title_span pad_0_18">排名购房</strong><span class="col666 mr15 ml20">当前共<i class="colff840b mr5 ml5">22</i>人参加</span><span class="col666 mr15"><i class="colff840b mr5 ml5">2206</i>人围观</span></div>
    <div class="gray_box clearFix">
        <div class="left">
<!--如果没有登录--begin-->
<%
        if (!(bool)ViewData["IsLogin"])
        {
%>
            <p class="ml15">
                <strong class="red">请先登录再进行报名！</strong></p>
            <table border="0" cellspacing="0" cellpadding="0" class="login_table">
                <tr>
                    <th scope="row" align="right">
                        登录名：
                    </th>
                    <td>
                        <input type="text" class="input1" value="请输入用户名" /><span class="red ml20">请输入正确信息</span>
                    </td>
                </tr>
                <tr>
                    <th scope="row" align="right">
                        密 码：
                    </th>
                    <td>
                        <input type="text" class="input1" value="请输入密码" />
                    </td>
                </tr>
                <tr>
                    <th scope="row">
                        &nbsp;
                    </th>
                    <td>
                        <input type="button" value="登录" class="btn_w110_yellow" />
                    </td>
                </tr>
            </table>
<%
        }
        else
        {
%>
<!--如果没有登录--end-->
<!--如果已登录--begin-->
            <p><strong class="red">请填写出价金额完成您的出价。</strong></p>
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="login_table">
              <tbody>
              <tr>
                <th align="right" scope="row"><span class="red mr5">*</span>出价金额：</th>
                <td><input type="text" value="" class="input1"><span class="ml10">万元</span></td>
              </tr>
              <tr>
                <th align="right" scope="row"><span class="red mr5">*</span>真实姓名：</th>
                <td><input type="text" value="" class="input1"></td>
              </tr>
              <tr>
                <th align="right" scope="row"><span class="red mr5">*</span>身份证号：</th>
                <td><input type="text" value="" class="input1"></td>
              </tr>
              <tr>
                <th align="right" scope="row">手 机 号：</th>
                <td><input type="text" value="" class="input1"></td>
              </tr>
              <tr>
                <th align="right" scope="row">QQ　　号：</th>
                <td><input type="text" value="" class="input1"></td>
              </tr>
              <tr>
                <th align="right" scope="row">邮　　箱：</th>
                <td><input type="text" value="" class="input1"></td>
              </tr>
              <tr>
                <th scope="row">&nbsp;</th>
                <td><input type="button" class="btn_w126_yellow" value="提交我的出价"></td>
              </tr>
              </tbody>
            </table>
<!--如果已登录--end-->
<% 
        } 
%>
            <div class="text_title">
                营销活动规则</div>
            <p class="text_p">
                1、任何快有家用户会员均可参与房源出价；</p>
            <p class="text_p">
                2、竞价中每次出价金额不得低于保底价，不高于封顶价，可多次参与同套房源出价；</p>
            <p class="text_p">
                3、砍价中每次出价金额不得高于封顶价，不低于保底价价，可多次参与同套房源出价；</p>
            <%--<div class="text_title">
                QA答疑</div>
            <p class="text_p">
                Q：是否我出价最高就一定能成交吗？</p>
            <p class="text_p">
                A：出价信息只是给房东或经纪人一个价格意向，是否成交取决于与房东或经纪人线下磋商的结果。</p>
            <p class="text_p">
                Q：是否我出价最高就一定能成交吗？</p>
            <p class="text_p">
                A：出价信息只是给房东或经纪人一个价格意向，是否成交取决于与房东或经纪人线下磋商的结果。</p>--%>
        </div>
        <div class="right">
        	<ul class="r_qh_title clearFix">
                <li class="on">报名记录</li>
            </ul>
            <div class="margin15">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table_box1 td_col333">
                  <tr>
                    <th scope="col">买家</th>
                    <th scope="col">报名号</th>
                    <th scope="col">报名时间</th>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                  <tr>
                    <td>184****3820</td>
                    <td>1号</td>
                    <td>2012.11.03  11:26:11</td>
                  </tr>
                </table>
            </div>
            <div class="page tac border_top">
                <span class="col333 font12 mr10">共 <em class="col666">50</em> 条记录</span> <a class="no">&lt;&lt;</a><a class="hover" href="">1</a><a href="">2</a><a href="">3</a><a href="">4</a><a href="">5</a><a href="">6</a><a href="">&gt;&gt;</a>
            </div>
        </div>
    </div>
<%
    }
%>
</div>
