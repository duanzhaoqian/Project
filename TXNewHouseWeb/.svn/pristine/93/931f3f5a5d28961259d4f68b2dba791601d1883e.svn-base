<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/YaoHao.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="TXModel.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    网络摇号-网络摇号直播页
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wlyhbj">
        <div class="wlyh_main pad_lr_20 clearFix">
            <div class="wlyh_title1">
                <%if (ViewData["activity"] != null)
                  {
                      ActivityList ac = ViewData["activity"] as ActivityList; 
                %>
                <strong class="whyh_t_s1">快有家网络摇号首页</strong><span class="right"><a href="<%=TXCommons.NHWebUrl.PremisesDetailUrl(ac.PremisesId.ToString()) %>"
                    class="btn_w105_qgreen mt5">楼盘详情</a></span></div>
            <%}%>
            <%if (ViewData["activity"] != null)
              {
                  ActivityList activity = ViewData["activity"] as ActivityList; 
                
            %>
            <dl class="wlyh_dl clearFix">
                <dt>
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/img_w380_h260.jpg" /></dt>
                <dd>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mt15">
                        <tr>
                            <th scope="row" width="65">
                                活动时间：
                            </th>
                            <td>
                                <%=activity.BeginTime %><%--2013年4月23日--%>
                            </td>
                            <th scope="row" width="65">
                                活动地点：
                            </th>
                            <td>
                                <%=activity.ActivitieLocation %><%--快有家网络直播间--%>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">
                                参与人数：
                            </th>
                            <td>
                                <%=activity.UserCount %>人
                            </td>
                            <th scope="row">
                                报名时间：
                            </th>
                            <td>
                                <%=activity.SignupBeginTime+"--"+activity.SignupEndTime %><%--2013-10-1--2013-12-1--%>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">
                                备选套数：
                            </th>
                            <td>
                                <%=activity.HouseCount %>套
                            </td>
                            <th scope="row">
                                房源：
                            </th>
                            <td>
                                <%=activity.Name %>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">
                                选房时间：
                            </th>
                            <td>
                                <%=activity.ChooseHouseTime == null ? "暂定" : activity.ChooseHouseTime.ToString()%>
                            </td>
                            <th scope="row">
                                摇号公证：
                            </th>
                            <td>
                                <%=activity.NotarialOffice %>
                            </td>
                        </tr>
                    </table>
                    <div class="line_h24 col666 pad10">
                        <p>
                            <strong class="col333">摇号须知：</strong></p>
                        <p>
                            <%=activity.Notice %></p>
                        <%--<p>
                            2、涉及房源为保利花园B区2#、3#、12#；</p>
                        <p>
                            3、主办方为保利（北京）房地产开发有限公司</p>
                        <p>
                            4、摇号方为快有家</p>--%>
                    </div>
                </dd>
            </dl>
            <%} %>
            <div class="wlyh_title2 font_normal">
                <span class="font12 col333 ml10 fl">查询：输入您的摇号卡号（票据号码）</span><input type="text" class="input3 mr10 ml10 mt10 fl" /><input
                    type="button" value="查询" class="btn_w68_green mt10 fl" /><span class="right"><a href=""
                        class="blue font12">查看全部</a></span></div>
            <div class="wlyh_list_title mt10">
                <strong class="font14">即时抽出号码列表</strong><span class="right font12">最后更新时间：2013-09-17
                    09：14</span></div>
            <ul class="wlyh_list_ul clearFix mb20 mt10">
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
                <li>第577位<a href="" class="blue ml5">130***06102532 </a></li>
            </ul>
            <div class="wlyh_spzb clearFix mb20">
                <div class="fl">
                    <h3 class="wlyh_h3_2 mb10">
                        视频直播</h3>
                    <div class="mt20">
                        <img src="<%=TXCommons.GetConfig.ImgUrl%>images/img.jpg" /></div>
                </div>
                <div class="fr">
                    <h3 class="wlyh_h3_2 mb10">
                        摇号流程</h3>
                    <div class="line_h24 col666">
                        <p>
                            <span class="col333">一、主办方：</span>北京恒成伟业房地产开发有限公司</p>
                        <p>
                            <span class="col333">二、承办方：</span>快有家</p>
                        <p>
                            <span class="col333">三、摇号的时间与地点：</span>2013年10月15日 快有家直播间</p>
                        <p>
                            <span class="col333">四、摇号的程序与步骤</span></p>
                        <div class="text">
                            <p>
                                1、 10月15日起，通过互联网，可在快有家上在线查询摇号信息是否无误。</p>
                            <p>
                                2、10月15日 正式摇号</p>
                            <p>
                                1) 工作人员对系统进行再次检查：现场打开电脑摇号软件，检查核对本次摇号数据库。</p>
                            <p>
                                2) 主办方代表发言：介绍摇号活动的时间安排及摇号中选者选房程序；
                            </p>
                            <p>
                                3) 承办方代表现场打开摇号软件，现场抽取选房顺序号；
                            </p>
                            <p>
                                4) 承办方代表操作摇号，本次摇号共分两批摇出，第一批为VIP客户，共分 11 组，每组摇出30个号；第二批为普通意向客户，共分 28 组，每组摇出 30 个号，由主持人现场公布；</p>
                            <p>
                                5) 公证员现场发表公证词。</p>
                            <p>
                                3、摇出选房顺序号的购房者须携带开盘相关有效证件，在2013年 10月15 日到指定的开盘现场选房，逾期视为自动放弃。</p>
                            <p>
                                4、本次摇号活动规则最终解释权归开发商所有。</p>
                            <p class="tar mt20">
                                北京恒成伟业房地产开发有限公司<span class="ml20"> 2013年10月14日</span></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
