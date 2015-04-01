<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ToolsMain.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%
        TXNewHouseWeb.Common.SEOModel seo = ViewData["Seo"] as TXNewHouseWeb.Common.SEOModel;
    %>
    <%=seo==null?string.Empty:seo.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/calculate.js")%>" type="text/javascript"></script>
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="bgcolor">
            <div class="w1190 line_h50 font14 col666">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                    class="blue">北京新房</a> > <a href="<%=TXCommons.GetConfig.GetQTBaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-1.html"
                        class="blue">工具</a> > 购房计算器
            </div>
        </div>
        <div class="bg_fbfbfb">
            <div class="pb15 w1190 clearFix">
                <div class="r_title_lp">
                    <strong class="title_span">购房计算器</strong></div>
                <div class="jisuan_left">
                    <ul>
                        <li class="hover" id="one1" onclick="setTab('one',1,5)"><a href="#">贷款计算器</a></li>
                        <li id="one2" onclick="setTab('one',2,5)"><a href="#">购房能力评估计算器</a></li>
                        <li id="one3" onclick="setTab('one',3,5)"><a href="#">公积金贷款计算器</a></li>
                        <li id="one4" onclick="setTab('one',4,5)"><a href="#">提前还款计算器</a></li>
                        <li id="one5" onclick="setTab('one',5,5)"><a href="#">税费计算器</a></li>
                    </ul>
                    <div class="jisuan_tit">
                        公积金贷款利率</div>
                    <div class="jisuan_tab">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th>
                                    年限
                                </th>
                                <th>
                                    1-5年
                                </th>
                                <th class="nobor_r">
                                    5-30年
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    12.07.06后
                                </td>
                                <td>
                                    4.00%
                                </td>
                                <td class="nobor_r">
                                    4.50%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    12.06.08后
                                </td>
                                <td>
                                    4.20%
                                </td>
                                <td class="nobor_r">
                                    4.70%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    11.07.06后
                                </td>
                                <td>
                                    4.45%
                                </td>
                                <td class="nobor_r">
                                    4.90%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    11.04.05后
                                </td>
                                <td>
                                    4.20%
                                </td>
                                <td class="nobor_r">
                                    4.70%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    11.02.09后
                                </td>
                                <td>
                                    4.00%
                                </td>
                                <td class="nobor_r">
                                    4.50%
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="jisuan_tit">
                        商业贷款利率</div>
                    <div class="jisuan_tab">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th>
                                    年限
                                </th>
                                <th>
                                    1-5年
                                </th>
                                <th class="nobor_r">
                                    5-30年
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    12.07.06后
                                </td>
                                <td>
                                    6.40%
                                </td>
                                <td class="nobor_r">
                                    6.55%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    12.06.08后
                                </td>
                                <td>
                                    6.65%
                                </td>
                                <td class="nobor_r">
                                    6.80%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    11.07.06后
                                </td>
                                <td>
                                    6.90%
                                </td>
                                <td class="nobor_r">
                                    7.05%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    11.04.05后
                                </td>
                                <td>
                                    6.65%
                                </td>
                                <td class="nobor_r">
                                    6.80%
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    11.02.09后
                                </td>
                                <td>
                                    6.45%
                                </td>
                                <td class="nobor_r">
                                    6.60%
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!--end left-->
                <div class="jisuan_right" id="t_one_1" style="display: block;">
                    <form name="calc1">
                    <div class="w500">
                        <div class="tit_1">
                            &nbsp;</div>
                        <div class="mt10 jisuan_fgx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="30%">
                                        还款方式：
                                    </th>
                                    <td width="70%">
                                        <input id="dengeben1" name="dengeben" value="radiobutton" checked="checked" onclick="chanage_type_mm()"
                                            type="radio" class="btn_radio" />等额本息<input type="radio" onclick="chanage_type_mm()"
                                                id="dengeben2" name="dengeben" value="radiobutton" class="btn_radio" />等额本金
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        贷款类别：
                                    </th>
                                    <td>
                                        <input name="type" type="radio" class="btn_radio" onclick="exc_zuhe(this.form,1);" />商业贷款
                                        <input name="type" type="radio" checked class="btn_radio" onclick="exc_zuhe(this.form,2);" />公积金
                                        <input name="type" type="radio" class="btn_radio" onclick="exc_zuhe(this.form,3);" />组合型
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        计算方式：
                                    </th>
                                    <td>
                                        <div id="calc1_js_div0" class="mb20 colff0000">
                                            <input id="calc1_radio1" onclick="exc_js(this.form,1);" checked="checked" value="1"
                                                name="jisuan_radio" type="radio" class="btn_radio" />根据面积、单价计算</div>
                                        <ul id="calc1_js_div1" style="display: block" class="tlist1">
                                            <li>
                                                <label>
                                                    单&nbsp;&nbsp;&nbsp;&nbsp;价：</label><input name="price" type="text" class="inptxt1 w80 mr3" />元/平米</li>
                                            <li>
                                                <label>
                                                    面&nbsp;&nbsp;&nbsp;&nbsp;积：</label><input name="sqm" type="text" class="inptxt1 w80 mr3" />平米</li>
                                            <li>
                                                <label>
                                                    按揭成数：</label>
                                                <select size="1" name="anjie" class="selcss_1 w80">
                                                    <option value="9">9成</option>
                                                    <option value="8">8成</option>
                                                    <option value="7" selected="selected">7成</option>
                                                    <option value="6">6成</option>
                                                    <option value="5">5成</option>
                                                    <option value="4">4成</option>
                                                    <option value="3">3成</option>
                                                    <option value="2">2成</option>
                                                    <%-- <option value="2成" selected="">2成</option>
                              <option value="3成">3成</option>
                              <option value="4成">4成</option>
                              <option value="5成">5成</option>
                              <option value="6成">6成</option>
                              <option value="7成">7成</option>
                              <option value="8成">8成</option>
                              <option value="9成">9成</option>--%>
                                                </select></li>
                                        </ul>
                                        <div id="calc1_js_div4" class="mb10 mt5 colff0000">
                                            <input id="calc1_radio2" onclick="exc_js(this.form,2);" value="2" name="jisuan_radio"
                                                type="radio" class="btn_radio" />根据贷款总额计算</div>
                                        <ul id="calc1_js_div2" style="display: none" class="tlist1">
                                            <li>
                                                <label>
                                                    贷款总额：</label><input maxlength="8" size="10" name="daikuan_total000" type="text" class="inptxt1 w80 mr3" />万元</li>
                                        </ul>
                                        <ul id="calc1_zuhe" style="display: none" class="tlist1">
                                            <li>
                                                <label>
                                                    商业贷款：</label><input name="total_sy" maxlength="8" size="8" type="text" class="inptxt1 w80 mr3" />元</li>
                                            <li>
                                                <label>
                                                    公积金贷款：</label><input name="total_gjj" maxlength="8" size="8" type="text" class="inptxt1 w80 mr3" />元</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        按揭年数：
                                    </th>
                                    <td>
                                        <select id="years" size="1" name="years" class="selcss_1">
                                            <option value="1">1年（12期）</option>
                                            <option value="2">2年（24期）</option>
                                            <option value="3">3年（36期）</option>
                                            <option value="4">4年（48期）</option>
                                            <option value="5">5年（60期）</option>
                                            <option value="6">6年（72期）</option>
                                            <option value="7">7年（84期）</option>
                                            <option value="8">8年（96期）</option>
                                            <option value="9">9年（108期）</option>
                                            <option value="10">10年（120期）</option>
                                            <option value="11">11年（132期）</option>
                                            <option value="12">12年（144期）</option>
                                            <option value="13">13年（156期）</option>
                                            <option value="14">14年（168期）</option>
                                            <option value="15">15年（180期）</option>
                                            <option value="16">16年（192期）</option>
                                            <option value="17">17年（204期）</option>
                                            <option value="18">18年（216期）</option>
                                            <option value="19">19年（228期）</option>
                                            <option value="20" selected="selected">20年（240期）</option>
                                            <option value="25">25年（300期）</option>
                                            <option value="30">30年（360期）</option>
                                            <%--  <option value="1年（12期）" selected="">1年（12期）</option>
                        <option value="2年（24期）">2年（24期）</option>
                        <option value="3年（36期）">3年（36期）</option>
                        <option value="4年（48期）">4年（48期）</option>
                        <option value="5年（60期）">5年（60期）</option>
                        <option value="6年（72期）">6年（72期）</option>
                        <option value="7年（84期）">7年（84期）</option>
                        <option value="8年（96期）">8年（96期）</option>
                        <option value="9年（108期）">9年（108期）</option>
                        <option value="10年（120期）">10年（120期）</option>
                        <option value="11年（132期）">11年（132期）</option>
                        <option value="12年（144期）">12年（144期）</option>
                        <option value="13年（156期）">13年（156期）</option>
                        <option value="14年（168期）">14年（168期）</option>
                        <option value="15年（180期）">15年（180期）</option>
                        <option value="16年（192期）">16年（192期）</option>
                        <option value="17年（204期）">17年（204期）</option>
                        <option value="18年（216期）">18年（216期）</option>
                        <option value="19年（228期）">19年（228期）</option>
                        <option value="20年（240期）">20年（240期）</option>
                        <option value="25年（300期）">25年（300期）</option>
                        <option value="30年（360期）">30年（360期）</option>--%>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        利率：
                                    </th>
                                    <td>
                                        <select id="lilv" name="lilv" class="selcss_1">
                                            <option value="31">12年7月6日利率上限（1.1倍）</option>
                                            <option value="30">12年7月6日利率下限（85折）</option>
                                            <option value="29">12年7月6日利率下限（7折）</option>
                                            <option value="28" selected="selected">12年7月6日基准利率</option>
                                            <option value="27">12年6月8日利率上限（1.1倍）</option>
                                            <option value="26">12年6月8日利率下限（85折）</option>
                                            <option value="25">12年6月8日利率下限（7折）</option>
                                            <option value="24">12年6月8日基准利率</option>
                                            <option value="23">11年7月6日利率上限（1.1倍）</option>
                                            <option value="22">11年7月6日利率下限（85折）</option>
                                            <option value="21">11年7月6日利率下限（7折）</option>
                                            <option value="20">11年7月6日基准利率</option>
                                            <option value="19">11年4月5日利率上限（1.1倍）</option>
                                            <option value="18">11年4月5日利率下限（85折）</option>
                                            <%-- <option value="12年07月06日基准利率" selected="">12年07月06日基准利率</option>
                      <option value="12年07月06日利率下限(7折)">12年07月06日利率下限(7折)</option>
                      <option value="12年07月06日利率下限(85折)">12年07月06日利率下限(85折)</option>
                      <option value="12年07月06日利率上限(1.1倍)">12年07月06日利率上限(1.1倍)</option>
                      <option value="12年06月08日基准利率">12年06月08日基准利率</option>
                      <option value="12年06月08日利率下限(7折)">12年06月08日利率下限(7折)</option>
                      <option value="12年06月08日利率下限(85折)">12年06月08日利率下限(85折)</option>
                      <option value="12年06月08日利率上限(1.1倍)">12年06月08日利率上限(1.1倍)</option>
                      <option value="11年07月06日基准利率">11年07月06日基准利率</option>
                      <option value="11年07月06日利率下限(7折)">11年07月06日利率下限(7折)</option>
                      <option value="11年07月06日利率下限(85折)">11年07月06日利率下限(85折)</option>
                      <option value="11年07月06日利率上限(1.1倍)">11年07月06日利率上限(1.1倍)</option>
                      <option value="11年04月05日基准利率">11年04月05日基准利率</option>
                      <option value="11年04月05日利率下限(7折)">11年04月05日利率下限(7折)</option>
                      <option value="11年04月05日利率下限(85折)">11年04月05日利率下限(85折)</option>
                      <option value="11年04月05日利率上限(1.1倍)">11年04月05日利率上限(1.1倍)</option>
                      <option value="11年02月09日基准利率">11年02月09日基准利率</option>
                      <option value="11年02月09日利率下限(7折)">11年02月09日利率下限(7折)</option>
                      <option value="11年02月09日利率下限(85折)">11年02月09日利率下限(85折)</option>
                      <option value="11年02月09日利率上限(1.1倍)">11年02月09日利率上限(1.1倍)</option>--%>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <input onclick="javascript:ext_total(document.calc1);" type="button" class="btn_w90_yellow mr10"
                                            value="开始计算" />
                                        <input onclick="javascript:formReset(document.calc1);" type="button" class="btn_w90_grey"
                                            value="重新填写" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="w390">
                        <div class="tit_2">
                            &nbsp;</div>
                        <div class="mt10">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="30%">
                                        房屋总价：
                                    </th>
                                    <td width="70%">
                                        <div name="fangkuan_total1" id="fangkuan_total1" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        贷款总额：
                                    </th>
                                    <td>
                                        <div name="daikuan_total1" id="daikuan_total1" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        还款总额：
                                    </th>
                                    <td>
                                        <div name="all_total1" id="all_total1" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        首期付款：
                                    </th>
                                    <td>
                                        <div name="money_first1" id="money_first1" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        支付利息款：
                                    </th>
                                    <td>
                                        <div name="accrual1" id="accrual1" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        贷款月数：
                                    </th>
                                    <td>
                                        <div name="month1" id="month1" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr id="type1_mm1" name="type1_mm1">
                                    <th>
                                        每月还款：
                                    </th>
                                    <td>
                                        <div name="month_money1" id="month_money1" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr id="type1_mm2" name="type1_mm2" style="display: none;">
                                    <th>
                                        月均金额：
                                    </th>
                                    <td>
                                        <textarea name="month_money11" id="month_money11" style="line-height: 18px; font-size: 12px"
                                            rows="6"></textarea>
                                    </td>
                                </tr>
                                <div id="calc1_benjin" style="display: none;">
                                    <input name="fangkuan_total2" type="hidden">
                                    <input name="daikuan_total2" type="hidden">
                                    <input name="all_total2" type="hidden">
                                    <input name="accrual2" type="hidden">
                                    <input name="money_first2" type="hidden">
                                    <input name="month2" type="hidden">
                                    <input name="month_money2" type="hidden">
                                </div>
                                <tr>
                                    <td colspan="2" class="col999">
                                        <div class="ml35 pt10">
                                            &nbsp;*以上结果仅供参考</div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="clearFix mt35 jisuan_tisbox">
                        <p class="blod colff0000">
                            等额本息还款法：</p>
                        <p>
                            把按揭贷款的本金总额与利息总额相加，然后平均分摊到还款期限的每个月中。作为还款人，每个月还给银行固定金额，但每月还款额中的本金比重逐月递增、利息比重逐月递减。</p>
                        <p>
                            <strong>优点</strong> 每月还相同的数额，作为贷款人，操作相对简单。每月承担相同的款项也方便安排收支。</p>
                        <p>
                            <strong>缺点</strong> 由于利息不会随本金数额归还而减少，银行资金占用时间长，还款总利息较等额本金还款法高。</p>
                        <p class="blod colff0000 mt20">
                            注：</p>
                        <p>
                            1.目前北京公积金贷款最高额度为80万元人民币。</p>
                        <p>
                            2.对已贷款购买一套住房但人均面积低于当地平均水平，再申请购买第二套普通自住房的居民，比照执行首次贷款购买普通自住房的优惠政策。</p>
                    </div>
                    </form>
                </div>
                <!--end1-->
                <div class="jisuan_right" id="t_one_2" style="display: none;">
                    <form name="myform">
                    <div class="w500">
                        <div class="tit_1">
                            &nbsp;</div>
                        <div class="mt10 jisuan_fgx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="30%">
                                        购房资金：
                                    </th>
                                    <td width="70%">
                                        <input id="rg01" name="rg01" type="text" class="inptxt1 w60 mr3" />万元<span class="col999 pl5">可用于购房的资金总和</span>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        家庭月收入：
                                    </th>
                                    <td>
                                        <input type="text" id="rg02" name="rg02" class="inptxt1 w60 mr3" />元<span class="col999 pl5">现家庭月收入</span>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        购房支出：
                                    </th>
                                    <td>
                                        <input type="text" id="rg03" name="rg03" class="inptxt1 w60 mr3" />元<span class="col999 pl5">预计家庭每月可用于购房支出</span>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        贷款年限：
                                    </th>
                                    <td>
                                        <div class="mb10">
                                            <select id="rg04" name="rg04" class="selcss_1">
                                                <option value="24">2年(24期)</option>
                                                <option value="36">3年(36期)</option>
                                                <option value="48">4年(48期)</option>
                                                <option value="60">5年(60期)</option>
                                                <option value="72">6年(72期)</option>
                                                <option value="84">7年(84期)</option>
                                                <option value="96">8年(96期)</option>
                                                <option value="108">9年(108期)</option>
                                                <option value="120">10年(120期)</option>
                                                <option value="180">15年(180期)</option>
                                                <option value="240" selected>20年(240期)</option>
                                                <option value="300">25年(300期)</option>
                                                <option value="360">30年(360期)</option>
                                            </select></div>
                                        <div class="col999">
                                            你期望偿还贷款的年限</div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        房屋面积：
                                    </th>
                                    <td>
                                        <input type="text" id="rg06" name="rg06" class="inptxt1 w60 mr3" />平方米<span class="col999 pl5">您计划购买的房屋面积</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <input type="button" onclick="javascript:chk03();" class="btn_w90_yellow mr10" value="开始计算" /><input
                                            type="button" onclick="javascript:document.myform.reset()" class="btn_w90_grey"
                                            value="重新填写" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="w390">
                        <div class="tit_2">
                            &nbsp;</div>
                        <div class="mt10">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="30%" valign="top">
                                        房屋总价：
                                    </th>
                                    <td width="70%">
                                        <div class="clearFix mb10">
                                            <div id="rg07" name="rg07" class="jisuan_result fl">
                                            </div>
                                            <em class="ml5 fl">元</em></div>
                                        <div class="col999">
                                            您可购买的房屋总体价格</div>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        房屋单价：
                                    </th>
                                    <td>
                                        <div class="clearFix mb10">
                                            <div id="rg08" name="rg08" class="jisuan_result fl">
                                            </div>
                                            <em class="ml5 fl">元</em></div>
                                        <div class="col999">
                                            您可购买的房屋单价</div>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        契&nbsp;&nbsp;&nbsp;&nbsp;税：
                                    </th>
                                    <td>
                                        <div class="clearFix mb10">
                                            <div class="jisuan_result fl">
                                            </div>
                                            <em class="ml5 fl">元</em></div>
                                        <div class="col999 pr10">
                                            房屋实际成交价格高于同级别土地上住房平均交易价格的1.2倍以上，契税按3%收取</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="col999">
                                        <div class="ml35 pt10">
                                            &nbsp;*以上结果仅供参考</div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    </form>
                    <div class="clear">
                    </div>
                    <div class="clearFix mt35 jisuan_tisbox">
                        <p class="blod colff0000 mt20">
                            注：</p>
                        <p>
                            1.目前北京公积金贷款最高额度为80万元人民币。</p>
                        <p>
                            2.对已贷款购买一套住房但人均面积低于当地平均水平，再申请购买第二套普通自住房的居民，比照执行首次贷款购买普通自住房的优惠政策。</p>
                    </div>
                </div>
                <!--end2-->
                <div class="jisuan_right" id="t_one_3" style="display: none;">
                    <form name="formt2">
                    <div class="w500">
                        <div class="tit_1">
                            &nbsp;</div>
                        <div class="mt10 jisuan_fgx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="44%">
                                        房屋性质：
                                    </th>
                                    <td width="56%">
                                        <input name="xz" type="radio" value="" class="btn_radio" />政策性住房<input name="xz"
                                            type="radio" value="" checked class="btn_radio" />其他
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        个人月缴存额：
                                    </th>
                                    <td>
                                        <input id="mount2" name="mount2" type="text" class="inptxt1 w80 mr3" />元
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        缴存比例：
                                    </th>
                                    <td>
                                        <input id="jcbl" name="jcbl" type="text" class="inptxt1 w80 mr3" />%
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        配偶月缴存额：
                                    </th>
                                    <td>
                                        <input id="mount3" name="mount3" type="text" class="inptxt1 w80 mr3" />元
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        缴存比例：
                                    </th>
                                    <td>
                                        <input id="p_bl" name="p_bl" type="text" class="inptxt1 w80 mr3" />%
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        房屋评估价或实际购房款：
                                    </th>
                                    <td>
                                        <input id="mount" name="mount" type="text" class="inptxt1 w80 mr3" />元
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        贷款申请年限：
                                    </th>
                                    <td>
                                        <input id="mount10" name="mount10" type="text" class="inptxt1 w80 mr3" />年
                                        <div class="col999 pr10">
                                            注：贷款期限最长可以计算到借款人70周岁，同时不得超过30年。</div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        个人信用等级：
                                    </th>
                                    <td>
                                        <input name="xy" type="radio" value="" class="btn_radio mr3">AAA<input name="xy"
                                            type="radio" value="" class="btn_radio mr3">AA<input name="xy" type="radio" value=""
                                                checked class="btn_radio mr3">其他
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <input type="button" onclick="javascript:gjjloan1(document.formt2)" class="btn_w90_yellow mr10"
                                            value="开始计算" /><input onclick="javascript:document.formt2.reset()" type="button"
                                                class="btn_w90_grey" value="重新填写" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="w390">
                        <div class="tit_2">
                            &nbsp;</div>
                        <div class="mt10">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="35%">
                                        可申请的最高贷款额：
                                    </th>
                                    <td width="65%">
                                        <div id="ze2" name="ze2" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">万元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        您所需要的贷款额度：
                                    </th>
                                    <td>
                                        <div class="mb10">
                                            <input id="need" name="need" type="text" class="inptxt1 w80 mr3" />万元</div>
                                        <div class="col999">
                                            (不能高于最高贷款额度)</div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        请选择还款方式：
                                    </th>
                                    <td>
                                        <select name="select" id="select" class="selcss_1">
                                            <option value="1">等额本息</option>
                                            <option value="2">等额本金</option>
                                            <option value="3">自由还款</option>
                                            <%--<option value="等额本息" selected="">等额本息</option>
<option value="等额本金">等额本金</option>
<option value="自由还款">自由还款</option>--%>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <input type="button" onclick="javascript:gjjloan2(document.formt2)" class="btn_w90_yellow mr10"
                                            value="开始计算" />
                                    </td>
                                </tr>
                                <%--  等额本息方式计算结果--%>
                                <tr class="jstype1">
                                    <th>
                                        <strong class="col333">等额本息计算结果</strong>
                                    </th>
                                    <td>
                                    </td>
                                </tr>
                                <tr class="jstype1">
                                    <th>
                                        月均还款额：
                                    </th>
                                    <td>
                                        <div name="ze22" id="ze22" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr class="jstype1">
                                    <th>
                                        本息合计：
                                    </th>
                                    <td>
                                        <div name="lx2" id="lx2" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <%--  等额本金方式计算结果--%>
                                <tr class="jstype2">
                                    <th>
                                        <strong class="col333">等额本金计算结果</strong>
                                    </th>
                                    <td>
                                    </td>
                                </tr>
                                <tr class="jstype2">
                                    <th>
                                        首月还款额：
                                    </th>
                                    <td>
                                        <div name="sfk2" id="sfk2" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr class="jstype2">
                                    <th>
                                        本息合计：
                                    </th>
                                    <td>
                                        <div name="lx3" id="lx3" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <%--  自由还款方式计算结果--%>
                                <tr class="jstype3">
                                    <th>
                                        <strong class="col333">自由还款计算结果</strong>
                                    </th>
                                    <td>
                                        <input type="hidden" name="yj2" class="jsq">
                                    </td>
                                </tr>
                                <tr class="jstype3">
                                    <th>
                                        最低还款额：
                                    </th>
                                    <td>
                                        <div name="sfk3" id="sfk3" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr class="jstype3">
                                    <th>
                                        最后期本金：
                                    </th>
                                    <td>
                                        <div name="lx4" id="lx4" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr class="jstype3">
                                    <th>
                                        最后期利息：
                                    </th>
                                    <td>
                                        <div name="lx5" id="lx5" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr class="jstype3">
                                    <th>
                                        总偿还利息：
                                    </th>
                                    <td>
                                        <div name="lx6" id="lx6" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="col999">
                                        <div class="ml35 pt10">
                                            &nbsp;*以上结果仅供参考</div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="clearFix mt35 jisuan_tisbox">
                        <p class="blod colff0000">
                            住房公积金贷款（以下简称贷款）：</p>
                        <p>
                            是指由住房公积金管理中心运用住房公积金，委托银行向购买、建造、翻建、大修自住住房购买人提供的政策性低息贷款。</p>
                        <p class="blod colff0000 mt20">
                            具体说明如下：</p>
                        <p>
                            1.1～5年期限贷款年利率为4%，6～30年期限贷款年利率为4.5%。</p>
                        <p>
                            2.借款人的贷款期限最长可以计算到借款人70周岁，同时不得超过30年。</p>
                        <p>
                            3.目前单笔贷款最高额度为80万元；个人信用评估机构评定的信用等级为AA级的可上浮15％，即92万元，AAA级的借款申请 人，贷款金额可上浮30％，即104万元</p>
                    </div>
                    </form>
                </div>
                <!--end3-->
                <div class="jisuan_right" id="t_one_4" style="display: none;">
                    <form name="tqhdjsq">
                    <div class="w500">
                        <div class="tit_1">
                            &nbsp;</div>
                        <div class="mt10 jisuan_fgx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="30%">
                                        贷款类型：
                                    </th>
                                    <td width="70%">
                                        <input name="tqhklx" type="radio" checked value="" class="btn_radio" />公积金贷款
                                        <input name="tqhklx" type="radio" value="" class="btn_radio" />商业性贷款
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        贷款总额：
                                    </th>
                                    <td>
                                        <input name="dkzws" type="text" class="inptxt1 w80 mr3" />万元
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        原还款期限：
                                    </th>
                                    <td>
                                        <select name="yhkqs" class="selcss_1">
                                            <option value="24">2年(24期)</option>
                                            <option value="36">3年(36期)</option>
                                            <option value="48">4年(48期)</option>
                                            <option value="60">5年(60期)</option>
                                            <option value="72">6年(72期)</option>
                                            <option value="84">7年(84期)</option>
                                            <option value="96">8年(96期)</option>
                                            <option value="108">9年(108期)</option>
                                            <option value="120">10年(120期)</option>
                                            <option value="132">11年(132期)</option>
                                            <option value="144">12年(144期)</option>
                                            <option value="156">13年(156期)</option>
                                            <option value="168">14年(168期)</option>
                                            <option value="180">15年(180期)</option>
                                            <option value="240" selected>20年(240期)</option>
                                            <option value="300">25年(300期)</option>
                                            <option value="360">30年(360期)</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        利率：
                                    </th>
                                    <td>
                                        <select id="lilv" name="dklv_class" class="selcss_1">
                                            <option value="23">11年7月6日利率上限（1.1倍）</option>
                                            <option value="22">11年7月6日利率下限（85折）</option>
                                            <option value="21">11年7月6日利率下限（7折）</option>
                                            <option value="20" selected="true">11年7月6日基准利率</option>
                                            <option value="19">11年4月5日利率上限（1.1倍）</option>
                                            <option value="18">11年4月5日利率下限（85折）</option>
                                            <option value="17">11年4月5日利率下限（7折）</option>
                                            <option value="16">11年4月5日基准利率</option>
                                            <option value="15">11年2月9日利率上限(1.1倍)</option>
                                            <option value="14">11年2月9日利率下限(85折)</option>
                                            <option value="13">11年2月9日利率下限(7折)</option>
                                            <option value="12">11年2月9日基准利率</option>
                                            <option value="11">10年12月26日利率上限(1.1倍)</option>
                                            <option value="10">10年12月26日利率下限(7折)</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        首次还款时间：
                                    </th>
                                    <td>
                                        <select name="yhksjn" class="selcss_1 mr5">
                                            年
                                            <%--<option value="年" selected="">年</option>--%>
                                        </select>
                                        <select name="yhksjy" class="selcss_1">
                                            <option value="1">1月</option>
                                            <option value="2">2月</option>
                                            <option value="3">3月</option>
                                            <option value="4">4月</option>
                                            <option value="5">5月</option>
                                            <option value="6">6月</option>
                                            <option value="7">7月</option>
                                            <option value="8">8月</option>
                                            <option value="9">9月</option>
                                            <option value="10">10月</option>
                                            <option value="11">11月</option>
                                            <option value="12">12月</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        提前还款时间：
                                    </th>
                                    <td>
                                        <select name="tqhksjn" class="selcss_1 mr5">
                                            年
                                            <%--<option value="年" selected="">年</option>--%>
                                        </select>
                                        <select name="tqhksjy" class="selcss_1">
                                            <%--<option value="月" selected="">月</option>--%>
                                            <option value="1">1月</option>
                                            <option value="2">2月</option>
                                            <option value="3">3月</option>
                                            <option value="4">4月</option>
                                            <option value="5">5月</option>
                                            <option value="6">6月</option>
                                            <option value="7">7月</option>
                                            <option value="8">8月</option>
                                            <option value="9">9月</option>
                                            <option value="10">10月</option>
                                            <option value="11">11月</option>
                                            <option value="12">12月</option>
                                        </select>
                                        <script language="javascript">                                            d = new Date();
                                            for (var i = parseInt(d.getFullYear() - 7); i <= parseInt(d.getFullYear() + 30); i++) {
                                                document.tqhdjsq.tqhksjn.options.add(new Option(i, i));
                                            }
                                            for (var i = parseInt(d.getFullYear() - 12); i <= parseInt(d.getFullYear()); i++) {
                                                document.tqhdjsq.yhksjn.options.add(new Option(i, i));
                                            }
                                            //	    alert(d.getFullYear());
                                            document.tqhdjsq.yhksjn.value = d.getFullYear();
                                            document.tqhdjsq.tqhksjn.value = d.getFullYear();
                                            document.tqhdjsq.yhksjy.value = parseInt(d.getMonth()) + 1;
                                            document.tqhdjsq.tqhksjy.value = parseInt(d.getMonth()) + 1;																</script>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        提前还款：
                                    </th>
                                    <td>
                                        <div class="mb10">
                                            <input name="tqhkfs" checked type="radio" value="" class="btn_radio mr3" />一次提前还清</div>
                                        <div class="mb10">
                                            <input name="tqhkfs" type="radio" value="" class="btn_radio mr3">部分提前还款
                                            <input name="tqhkws" type="text" class="inptxt1 w60 ml5 mr3" />万元</div>
                                        <div class="col999">
                                            (不含当月应还款额)</div>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        处理方式：
                                    </th>
                                    <td>
                                        <div class="mb10">
                                            <input name="clfs" type="radio" checked value="" class="btn_radio mr3" />缩短还款年限，月还款额不变</div>
                                        <div class="">
                                            <input name="clfs" type="radio" value="" class="btn_radio mr3" />减少月还款额，还款期不变</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <input type="button" onclick="javascript:play()" class="btn_w90_yellow mr10" value="开始计算" /><input
                                            type="button" onclick="javascript:document.tqhdjsq.reset()" class="btn_w90_grey"
                                            value="重新填写" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="w390">
                        <div class="tit_2">
                            &nbsp;</div>
                        <div class="mt10">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="30%">
                                        原月还款额：
                                    </th>
                                    <td width="70%">
                                        <div id="ykhke" name="ykhke" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        原最后还款期：
                                    </th>
                                    <td>
                                        <div name="yzhhkq" id="yzhhkq" class="jisuan_result fl">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        已还款总额：
                                    </th>
                                    <td>
                                        <div name="yhkze" id="yhkze" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        该月一次还款额：
                                    </th>
                                    <td>
                                        <div name="gyyihke" id="gyyihke" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        下月起月还款额：
                                    </th>
                                    <td>
                                        <div name="xyqyhke" id="xyqyhke" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        节省利息支出：
                                    </th>
                                    <td>
                                        <div name="jslxzc" id="jslxzc" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        新的最后还款期：
                                    </th>
                                    <td>
                                        <div name="xdzhhkq" id="xdzhhkq" class="jisuan_result fl">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        计算结果提示：
                                    </th>
                                    <td>
                                        <div name="jsjgts" id="jsjgts" class="jisuan_result fl">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="clearFix mt35 jisuan_tisbox">
                        <p class="blod colff0000 mt20">
                            注：</p>
                        <p>
                            1.目前北京公积金贷款最高额度为80万元人民币。</p>
                        <p>
                            2.对已贷款购买一套住房但人均面积低于当地平均水平，再申请购买第二套普通自住房的居民，比照执行首次贷款购买普通自住房的优惠政策。</p>
                    </div>
                    </form>
                </div>
                <!--end4-->
                <div class="jisuan_right" id="t_one_5" style="display: none;">
                    <form name="formt3">
                    <div class="w500">
                        <div class="tit_1">
                            &nbsp;</div>
                        <div class="mt10">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="30%">
                                        单&nbsp;&nbsp;&nbsp;&nbsp;价：
                                    </th>
                                    <td width="70%">
                                        <input type="text" id="dj3" name="dj3" class="inptxt1 w80 mr3" />元/平方米
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        面&nbsp;&nbsp;&nbsp;&nbsp;积：
                                    </th>
                                    <td>
                                        <input type="text" name="mj3" id="mj3" class="inptxt1 w80 mr3" />平方米
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <input type="button" onclick="javascript:checkformt3()" class="btn_w90_yellow mr10"
                                            value="开始计算" /><input onclick="javascript:document.formt3.reset()" type="button"
                                                class="btn_w90_grey" value="重新填写" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="w390">
                        <div class="tit_2">
                            &nbsp;</div>
                        <div class="mt10 jisuan_fgx_l">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jisuan_tab1">
                                <tr>
                                    <th width="32%">
                                        房屋总价：
                                    </th>
                                    <td width="68%">
                                        <div name="fkz3" id="fkz3" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        印花税：
                                    </th>
                                    <td>
                                        <div name="yh" id="yh" class="jisuan_result fl">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        公证费：
                                    </th>
                                    <td>
                                        <div name="gzh" id="gzh" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        契税：
                                    </th>
                                    <td>
                                        <div name="q" id="q" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        产权代办手续费：
                                    </th>
                                    <td>
                                        <div name="wt" id="wt" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        房屋买卖手续费：
                                    </th>
                                    <td>
                                        <div name="fw" id="fw" class="jisuan_result fl">
                                        </div>
                                        <em class="ml5 fl">元</em>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="col999 ml35">
                                            *以上结果仅供参考</div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    </form>
                    <div class="clear">
                    </div>
                    <div class="clearFix mt35 jisuan_tisbox">
                        <p class="blod colff0000 mt20">
                            税费说明：</p>
                        <p>
                            购房需交纳的第一组税费是契税、印花税、交易手续费、权属登记费。</p>
                        <p>
                            1.契税金额是房价的1.5%，一般情况下是在交易签证时交50%，入住后拿房产证时交50%。</p>
                        <p>
                            2.印花税金额为房价的0.05%，在交易签证时交纳。</p>
                        <p>
                            3.交易手续费一般是每平方米2.5元，也在交易签证时交纳。</p>
                        <p>
                            4.权属登记费100元到200元之间。</p>
                    </div>
                </div>
                <input type="hidden" id="toolType"  name="toolType" value="<%=ViewData["tooltype"]%>" />
                <!--end5-->
                <script type="text/javascript">
                    $(function () {
                        setTab('one',parseInt($("#toolType").val()), 5)//描点初始化
                        $('.jstype2').hide();
                        $('.jstype3').hide();
                        $("#select").bind('change', function () {
                            $('.jstype1').hide();
                            $('.jstype2').hide();
                            $('.jstype3').hide();

                            $('.jstype' + $(this).val()).show();
                        });
                    });

                    function checkformt3() {
                        if (document.formt3.dj3.value == "" || document.formt3.mj3.value == "") {
                            alert("请填写单价和面积");
                            return;
                        } else {
                            runjs3(document.formt3);
                        }
                    }
                    function setTab(name, cursel, n) {
                        for (i = 1; i <= n; i++) {
                            var menu = document.getElementById(name + i);
                            var t = document.getElementById("t_" + name + "_" + i);
                            menu.className = i == cursel ? "hover" : "";
                            t.style.display = i == cursel ? "block" : "none";
                        }
                    };
                </script>
                <!--end right-->
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
</asp:Content>
