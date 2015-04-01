<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    在线支付
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        var bankZfb = new KeyValuePair<string, string>("BANK_ZFB", Url.Content(TXCommons.GetConfig.GetFileUrl("/images/bank/zfb.gif")));
        var bankList = new List<KeyValuePair<string, string>>();
        bankList.Add(new KeyValuePair<string, string>("BOCB2C", "z1"));
        bankList.Add(new KeyValuePair<string, string>("ICBCB2C", "z2"));
        bankList.Add(new KeyValuePair<string, string>("CMB", "z3"));
        bankList.Add(new KeyValuePair<string, string>("CCB", "z4"));
        bankList.Add(new KeyValuePair<string, string>("ABC", "z5"));
        bankList.Add(new KeyValuePair<string, string>("SPDB", "z6"));
        bankList.Add(new KeyValuePair<string, string>("CIB", "z7"));
        bankList.Add(new KeyValuePair<string, string>("GDB", "z8"));
        bankList.Add(new KeyValuePair<string, string>("SDB", "z9"));
        bankList.Add(new KeyValuePair<string, string>("CMBC", "z10"));
        bankList.Add(new KeyValuePair<string, string>("COMM", "z11"));
        bankList.Add(new KeyValuePair<string, string>("CITIC", "z12"));
        bankList.Add(new KeyValuePair<string, string>("HZCBB2C", "z13"));
        bankList.Add(new KeyValuePair<string, string>("CEBBANK", "z14"));
        bankList.Add(new KeyValuePair<string, string>("SHBANK", "z15"));
        bankList.Add(new KeyValuePair<string, string>("NBBANK", "z16"));
        bankList.Add(new KeyValuePair<string, string>("SPABANK", "z17"));
        bankList.Add(new KeyValuePair<string, string>("BJRCB", "z18"));
        bankList.Add(new KeyValuePair<string, string>("FDB", "z19"));
        bankList.Add(new KeyValuePair<string, string>("POSTGC", "z20"));
    %>
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <div class="yellow_box">
            <p class="ml20 mt15">
                <strong class="col333 font14">在线支付</strong></p>
            <p class="ml20 mt15 mb15 font14 col333">
                <span>充值金币总额：</span><strong class="font24 orange"><span class="fontYaHei">￥</span><%=ViewData["Rechargeprice"] %></strong></p>
        </div>
        <form method="post" action="<%=Url.Content(TXCommons.GetConfig.BaseUrl) %>Account/AlipayOrderConfirm<%=ViewData["canshu"] %>">
        <input type="hidden" id="RechargeNo" name="RechargeNo" value="<%=ViewData["RechargeNo"]%>" />
        <input type="hidden" id="Rechargeprice" name="Rechargeprice" value="<%=ViewData["price"]%>" />
        <input type="hidden" id="qtMoney" name="qtMoney" value="<%=ViewData["qtMoney"]%>" />
        <div class="my_title mt10">
            <a class="on">选择支付方式</a> <span class="my_span" style="color: #333!important;">温馨提示：建议使用IE浏览器进行支付，非IE浏览器可能会导致支付失败！</span>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="bankGroups mt20">
            <tbody>
                <%--<tr>
                    <th align="right" width="150" valign="top">
                        <span class="font14 col666 pr10">网上银行：</span>
                    </th>
                    <td width="750" align="left">
                        <div class="blankList clearFix">
                            <ul>
                                <%for (int i = 0; i < bankList.Count; i++)
                                  {%>
                                <li>
                                    <label>
                                        <span class="fn-left">
                                            <input type="radio" class="unsaved" name="banks" value="<%=bankList[i].Key %>" <%= (i == 0 ? "checked=\"checked\"" : "") %>></span>
                                        <div class="icon-container ">
                                            <i class="<%=bankList[i].Value %>"></i>
                                        </div>
                                    </label>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                        <div class="mt5 mb20 ml10">
                            <a href="<%=TXCommons.GetConfig.BaseUrl%>Help/NewbieGuideTab8" class="blue font12">查看支付限额
                                &gt;&gt;</a>
                        </div>
                    </td>
                </tr>--%>
                <%--<tr>
                    <th align="right" width="150" valign="top">
                        <span class="font14 col666 pr10">第三方支付平台：</span>
                    </th>
                    <td>
                        <ul class="blankList clearFix">
                            <li style="width: 655px; margin-bottom: 0px;">
                                <label class="fl">
                                    <span class="fn-left">
                                        <input name="banks" class="unsaved" type="radio" value="<%=bankZfb.Key%>" /></span>
                                    <div class="icon-container">
                                        <i class="zfb"></i>
                                    </div>
                                </label>
                                <div class="ml10 fl font12 mt2">
                                    <p class="col666 mb5">
                                        支持支付宝余额支付、网银支付、快捷支付、找人代付等。</p>
                                    <p>
                                        <a href="<%=TXCommons.GetConfig.BaseUrl%>Help/NewbieGuideTab8" class="blue mt5">查看支付限额
                                            &gt;&gt;</a></p>
                                </div>
                            </li>
                        </ul>
                        <div class="font12 col666 ts1 ml10 mt10">
                            如果您的订单金额较大，可以先充值到支付宝账户，然后再使用账户余额一次性大额付款。</div>
                    </td>
                </tr>--%>
                <tr>
                    <th align="right" width="150" valign="top">
                        <span class="font14 col666 pr10">支付平台：</span>
                    </th>
                    <td>
                        <ul class="blankList clearFix">
                            <li style="width: 655px; margin-bottom: 0px;">
                                <label class="fl">
                                    <span class="fn-left">
                                        <input name="banks" class="unsaved" type="radio" checked="checked" value="<%=bankZfb.Key%>" /></span>
                                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/bank/kq.gif" />
                                </label>
                            </li>
                        </ul>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="line">
        </div>
        <div class="tac mb20">
            <input type="button" class="btn_w97_green mb20" value="确定付款" onclick="submitOnlinePay()" /></div>
        </form>
    </div>
    <script type="text/javascript">
        //线上支付
        function submitOnlinePay() {
            var id = $('#RechargeNo').val();
            var price = '<%=ViewData["Rechargeprice"]%>';
//            var price = $('#qtMoney').val();
            location.href = "<%=TXCommons.GetConfig.BaseUrl%>Account/Send?id=" + id + "&price=" + price;
        }
    </script>
</asp:Content>
