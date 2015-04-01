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
    <div class="yellow_box1 r_y_jp clearFix">
        <div class="part1">
            <p class="col333 font18 fontYaHei">
                <%=TXCommons.Util.getStrLenB(Model.PremisesName,0,20)%></p>
            <p class="col666 font16 fontYaHei mt5">
                <%=Model.BuildingName %>
                <%=Model.UnitName %>
                <%=Model.Floor.ToString() %>层
                <%=Model.HouseName %>号房</p>
        </div>
        <div class="part2">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <th align="right" scope="row" width="80">
                            起 拍 价：
                        </th>
                        <td>
                            <strong class="green mr10">
                                <%= (double)_activity.BidPrice%></strong>万元
                        </td>
                        <th align="right" scope="row" width="60">
                            市场价格：
                        </th>
                        <td>
                            <strong class="green mlr3">
                                <%=(double)Model.TotalPrice%></strong>万元
                        </td>
                        <th align="right" scope="row" width="60">
                            竞购方式：
                        </th>
                        <td>
                            竞价 <span class="r_wen_green ml20"><i class="r_layer_box">在规定时间内，用户一次或多次提交愿意出的最高价，到截止时间后，出价最高的用户获得标的物，并按此价格付款。
                                <span class="sj"></span></i></span>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            加价幅度：
                        </th>
                        <td colspan="3">
                            必须为<span class="green mlr3"><%=(double)_activity.AddPrice%></span>万元的整数倍
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            最大幅度：
                        </th>
                        <td colspan="3">
                            单次加价不高于<span class="green mlr3"><%=(double)_activity.MaxPrice%></span>万元
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <%
        //倒计时
        string miaoshaSeconds = string.Empty;
        System.TimeSpan ts = TimeSpan.Zero;

        DateTime startTime = Convert.ToDateTime(Model.ActBeginTime);
        DateTime endTime = Convert.ToDateTime(Model.ActEndTime);
        if (startTime <= DateTime.Now)
        {
            ts = endTime.Subtract(DateTime.Now);
            miaoshaSeconds = ts.TotalSeconds.ToString();
        }
        
        %>
        <div class="part3">
            <p class="clock">
                竞购时间</p>
            <p class="col333 font14">
                <%=_activity.BeginTime.ToString("yyyy-MM-dd") %>
                -
                <%=_activity.EndTime.ToString("yyyy-MM-dd") %></p>
            <%if (startTime <= DateTime.Now && endTime > DateTime.Now)
              { %>
            <p class="col999 font14" id="timer" lang="<%=Model.ActivityState  %>" data="<%=miaoshaSeconds %>">
                <strong class="colff6600">2</strong> 天 <strong class="colff6600">0</strong> 小时 <strong
                    class="colff6600">0</strong> 分 <strong class="colff6600">0</strong> 秒</p>
            <%} %>
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
            <a href="#chujia" class="s-link-d mt10">立刻出价</a>
            <% 
              }
              //如果活动时间已经结束
              else
              {
            %>
            <a href="javascript:void(0);" class="s-link-d mt10">活动结束</a>
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
<input type="hidden" id="pageIndex" value="1" />
<input type="hidden" id="totalPageCount" value="<%=ViewData["pageCount"] %>" />
<%Html.RenderPartial("~/Views/Shared/_HouseDetail.ascx", Model);%>
<div class="w1190">
    <% 
        //如果活动过期或结束，房源状态为出售状态则不显示，否则显示一口价信息
        if (Model.SalesStatus == 2 && _activity.ActivitieState == 2) { }
        else
        {
    %>
    <!--如果房源已经认购或签约--begin-->
    <%
            List<TXOrm.BidPrice> _bidPrice = (ViewData["BidPrice"] as List<TXOrm.BidPrice>).Where(b => b.Status == 2).OrderByDescending(b => b.Id).ToList();
            //如果房源已经认购或签约 或者活动等待公布结果
            if (Model.SalesStatus == 3 || Model.SalesStatus == 4 || (Model.SalesStatus == 2 && Model.ActEndTime < DateTime.Now))
            {
                ArrayList arrNum = ViewData["bidNum"] as ArrayList;
    %>
    <div class="r_title_lp">
        <strong class="title_span pad_0_30">竞价</strong><span class="col666 font12 ml20 mr15">买家出价:<i
            class="colff840b mr5 ml5"><%=arrNum[0] %></i>次</span><span class="col666 font12 mr15">当前共<i
                class="colff840b mr5 ml5"><%=arrNum[1] %></i>人参加</span><span class="col666 font12 mr15"><i
                    class="colff840b mr5 ml5"><%=arrNum[2] %></i>人围观</span></div>
    <div class="yellow_box1 yellow_box2 clearFix mb15">
        <div class="part2 ml10">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <th align="right" scope="row">
                            起拍价：
                        </th>
                        <td>
                            <strong class="green mr3">
                                <%=(double)_activity.BidPrice%></strong>万元
                        </td>
                        <th align="right" scope="row">
                            加价幅度：
                        </th>
                        <td>
                            必须为<span class="green mlr3"><%=(double)_activity.AddPrice%></span>万元的整数倍
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            市场价格：
                        </th>
                        <td>
                            <strong class="green mr3">
                                <%=(double)Model.TotalPrice%></strong>万元
                        </td>
                        <th align="right" scope="row">
                            最大幅度：
                        </th>
                        <td>
                            单次加价不高于<span class="green mlr3"><%=(double)_activity.MaxPrice%></span>万元
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            竞购方式：
                        </th>
                        <td>
                            竞拍 <span class="r_wen_green ml20"><i class="r_layer_box">在规定时间内，用户一次或多次提交愿意出的最高价，到截止时间后，出价最高的用户获得标的物，并按此价格付款。
                                <span class="sj"></span></i></span>
                        </td>
                        <th align="right" scope="row">
                            竞购时间：
                        </th>
                        <td>
                            <%=_activity.BeginTime.ToString("yyyy年M月d日")%>
                            -
                            <%=_activity.EndTime.ToString("yyyy年M月d日")%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <%
                if (_bidPrice.Count > 0)
                {
        %>
        <div class="part5 mt15 mr20">
            <div class="pos_abs">
                <input type="button" value="竞拍结束" class="btn_w126_gray"></div>
            <p class="tar">
                成交价格：<span class="fontYahei font18 colff840b"><%=(double)_bidPrice[0].BidUserPrice%>万元</span></p>
            <p class="tar">
                劲省：<span class="fontYahei green"><%=(double)(Model.TotalPrice - _bidPrice[0].BidUserPrice)%>万元</span></p>
        </div>
        <%
                }
                else
                {
        %>
        <div class="part5 mt15 mr20" style="border: 0; background: #fdf9f0;">
            <div class="pos_abs">
                <input type="button" value="等待公布结果" class="btn_w163_gray"></div>
        </div>
        <%
                }
        %>
    </div>
    <div class="gray_box gray_box1 clearFix">
        <div class="right">
            <ul class="r_qh_title clearFix">
                <li class="on">出价记录</li>
            </ul>
            <div class="margin15">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table_box1 td_col333">
                    <tbody id="result">
                    </tbody>
                </table>
            </div>
            <div class="page tac border_top">
                <span class="col333 font12 mr10">共 <em class="col666" id="totalCount"></em>条记录</span>
                <span id="page"></span>
            </div>
        </div>
        <div class="left mr20">
            <div class="text_title">
                营销活动规则</div>
            <p class="text_p">
                1.参与本次活动需注册为快有家会员，符合国家规定的具有购买本竞购房源资格的方可参加本次竞购活动。（请准确、真实填写购房用户的姓名、身份证号、联系方式等个人信息资料，否则不可竞购）</p>
            <p class="text_p">
                2.参与竞购时您需要缴纳活动保证金，保证金支付成功后，即可参加。（活动结束之后，不管是否成功，保证金一周之后自动退还到您的账户中）</p>
            <p class="text_p">
                3.秒杀、一口价、限时折扣竞购成功之后，需在<%=TXCommons.GetConfig.MSOperatingTime%>分钟之内支付活动保证金，否则视为放弃。
            </p>
            <p class="text_p">
                4.凡赢得竞购的获胜者开发商会在最短的时间内与您联系，办理线下的购房手续。
            </p>
            <p class="text_p">
                5.凡赢得竞购的获胜者在规定时间内不履行购房手续的用户，快有家有权没收其保证金。
            </p>
            <p class="text_p">
                6.凡被监控到恶意竞购的非合法用户，快有家有权没收其保证。
            </p>
        </div>
    </div>
    <input type="hidden" id="_type" value="3" />
    <!--如果房源已经认购或签约--end-->
    <%  
            }
            //如果房源在售状态，并且未到活动结束时间
            if (Model.SalesStatus == 2 && Model.ActEndTime >= DateTime.Now && Model.ActBeginTime <= DateTime.Now)
            {
                ArrayList arrNum = ViewData["bidNum"] as ArrayList;
    %>
    <a name="chujia"></a>
    <div class="r_title_lp">
        <strong class="title_span pad_0_30">竞价</strong><span class="col666 ml20 mr15">买家出价:<i
            class="colff840b mr5 ml5"><%=arrNum[0] %></i>次</span><span class="col666 mr15">当前共<i
                class="colff840b mr5 ml5"><%=arrNum[1] %></i>人参加</span><span class="col666 mr15"><i
                    class="colff840b mr5 ml5"><%=arrNum[2] %></i>人围观</span></div>
    <div class="gray_box clearFix">
        <div class="left">
            <!--如果没有登录--begin-->
            <%
                if (!(bool)ViewData["IsLogin"])
                {
            %>
            <p>
                <input type="hidden" id="_type" value="1" />
                <strong class="red">请先登录再进行报名！</strong></p>
            <form id="loginForm" action="" method="post">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="login_table">
                <tbody>
                    <tr>
                        <th align="right" scope="row">
                            登录名：
                        </th>
                        <td>
                            <input type="text" id="username" name="username" placeholder="请输入用户名" class="input1" /><%--<span class="red ml20">请输入正确信息</span>--%>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            密&#12288;码：
                        </th>
                        <td>
                            <input type="password" placeholder="请输入密码" class="input1" id="password" name="password" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">
                            &nbsp;
                        </th>
                        <td>
                            <input type="submit" class="btn_w110_yellow" value="登录" />
                        </td>
                    </tr>
                </tbody>
            </table>
            </form>
            <script type="text/javascript" language="javascript">
                $(document).ready(function () {
                    //验证输入格式
                    jQuery.validator.addMethod("IsVaildName", function (value, element) {
                        var c = /^(\w|[\u4E00-\u9FA5])*$/;
                        return c.test(value);
                    }, "<span class=\"red ml20\">登录名输入格式不正确</span>");

                    $("#loginForm").validate({
                        rules: {
                            username: {
                                required: true,
                                IsVaildName: true
                            },
                            password: {
                                required: true,
                                minlength: 6
                            }
                        },
                        submitHandler: function (form) {
                            var username = $("#username").val();
                            var password = $("#password").val();
                            $.ajax({
                                type: "post",
                                url: globalvar.siteRoot + "/login/login",
                                dataType: "json",
                                data: { username: username, password: password },
                                beforeSend: function (XMLHttpRequest) {
                                    //                                    $("#dl").attr("disabled", "disabled");
                                    //                                    $("#dl").removeClass("dl_submit");
                                    //                                    $("#dl").addClass("dl_loading");
                                },
                                success: function (data) {
                                    if (data.IsLogin) {
                                        window.location.reload();
                                    } else {
                                        globalvar.dialog(data.Message);
                                    }
                                }
                            });
                        },
                        messages: {
                            username: {
                                required: "<span class=\"red ml20\">用户名不能为空。</span>"
                            },
                            password: {
                                required: "<span class=\"red ml20\">密码不能为空</span>",
                                minlength: jQuery.format("<span class=\"red ml20\">密码不能小于{0}个字符</span>")
                            }
                        },
                        onkeyup: false,
                        errorPlacement: function (error, element) {
                            element.parent().find("span").remove();
                            error.appendTo(element.parent());
                        }
                    });
                });

            </script>
            <%
                }
                else
                {
            %>
            <!--如果没有登录--end-->
            <!--如果已登录--begin-->
            <input type="hidden" id="_type" value="2" />
            <form id="bidpriceForm" action="" method="post">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="login_table"
                <%=(ViewData["payBond"].ToString()=="True"?"":"style=\"display:none;\"")%>>
                <tbody>
                    <tr>
                        <td colspan="2">
                            <p>
                                <strong class="red">请填写出价金额完成您的出价。</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            <span class="red mr5">*</span>出价金额：
                        </th>
                        <td>
                            <input type="text" value="" class="input1" id="price" name="price" /><span class="ml10">万元</span>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">
                            &nbsp;
                        </th>
                        <td>
                            <input type="submit" class="btn_w126_yellow" value="提交我的出价" />
                        </td>
                    </tr>
                </tbody>
            </table>
            </form>
            <form id="userinfoForm" action="" method="post">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="login_table"
                <%=(ViewData["payBond"].ToString()=="True"?"style=\"display:none;\"":"")%>>
                <tbody>
                    <tr>
                        <td>
                            <p>
                                <strong class="red">请完善或者核对您的个人信息。</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            <span class="red mr5">*</span>真实姓名：
                        </th>
                        <td>
                            <input type="text" value="<%=ViewData["realName"] %>" class="input1" id="realname"
                                name="realname" <%=(string.IsNullOrEmpty(TXCommons.Util.ToString(ViewData["realName"]))?"":"readonly=\"readonly\"") %> />
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            <span class="red mr5">*</span>身份证号：
                        </th>
                        <td>
                            <input type="text" value="<%=ViewData["code"] %>" class="input1" id="code" name="code"
                                <%=(string.IsNullOrEmpty(TXCommons.Util.ToString(ViewData["code"]))?"":"readonly=\"readonly\"") %> />
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            <span class="red mr5">*</span>手 机 号：
                        </th>
                        <td>
                            <input type="text" value="<%=ViewData["mobile"] %>" class="input1" id="mobile" name="mobile"
                                <%=(string.IsNullOrEmpty(TXCommons.Util.ToString(ViewData["mobile"]))?"":"readonly=\"readonly\"") %> />
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            QQ 号：
                        </th>
                        <td>
                            <input type="text" value="<%=ViewData["qq"] %>" class="input1" id="qq" name="qq"
                                <%=(string.IsNullOrEmpty(TXCommons.Util.ToString(ViewData["qq"]))?"":"readonly=\"readonly\"") %> />
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            邮 箱：
                        </th>
                        <td>
                            <input type="text" value="<%=ViewData["email"] %>" class="input1" id="email" name="email"
                                <%=(string.IsNullOrEmpty(TXCommons.Util.ToString(ViewData["email"]))?"":"readonly=\"readonly\"") %> />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">
                            &nbsp;
                        </th>
                        <td>
                            <input type="submit" class="btn_w126_yellow" value="提交我的信息" />
                        </td>
                    </tr>
                </tbody>
            </table>
            </form>
            <% TXOrm.BidPrice _lastbidPrice = (ViewData["BidPrice"] as List<TXOrm.BidPrice>).OrderByDescending(b => b.Id).FirstOrDefault();
               decimal lastPrice = _lastbidPrice != null ? _lastbidPrice.BidUserPrice : _activity.BidPrice; %>
            <script type="text/javascript">
                $(document).ready(function () {
                    //价格输入格式
                    jQuery.validator.addMethod("isprice", function (value, element) {
                        var c = /^\d{1,10}(\.\d{1,2})?$/;
                        return c.test(value);
                    }, "<span class=\"red ml20\">价格不正确</span>");

                    //真实姓名输入格式
                    jQuery.validator.addMethod("isrealname", function (value, element) {
                        var c = /([a-zA-Z\u4e00-\u9fa5]){2,18}/;
                        return c.test(value);
                    }, "<span class=\"red ml20\">真实姓名不正确</span>");

                    //身份证输入格式
                    jQuery.validator.addMethod("iscode", function (value, element) {
                        return isIdCardNo(value);
                    }, "<span class=\"red ml20\">身份证不正确</span>");

                    //手机号输入格式
                    jQuery.validator.addMethod("ismobile", function (value, element) {
                        var c = /^(((1[0-9]{1}))+\d{9})$/;
                        return value == "" ? true : c.test(value);
                    }, "<span class=\"red ml20\">手机号不正确</span>");


                    //出价验证
                    $("#bidpriceForm").validate({
                        rules: {
                            price: {
                                required: true,
                                isprice: true
                            }
                        },
                        submitHandler: function (form) {
                            var price = $("#price").val();
                            var realName = $("#realname").val();
                            var code = $("#code").val();
                            var mobile = $("#mobile").val();
                            var qq = $("#qq").val();
                            var email = $("#email").val();
                            var developerId = "<%=Model.DeveloperId %>";
                            var activitiesId = "<%=Model.ActivityId %>";
                            var houseId = "<%=Model.HouseId %>";
                            var lastPrice = parseFloat('<%=lastPrice %>');

                            $.post(globalvar.siteRoot + "/ajax/GetLastBidPrice", { activityId: activitiesId, houseId: houseId }, function (d) {
                                if (d && d != 0) {
                                    lastPrice = parseFloat(d);
                                }

                                var errMsg = [];
                                if (parseFloat(price) > parseFloat('<%=Model.TotalPrice %>') || parseFloat(price) <= parseFloat('<%=_activity.BidPrice %>')) {
                                    errMsg.push('<li>出价范围必须在起拍价和市场价格之间</li>');
                                }
                                if (parseFloat(price) <= lastPrice) {
                                    errMsg.push('<li>出价不能低于上次出价（上次出价' + lastPrice + '万元）</li>');
                                }
                                if (parseFloat(parseFloat(price) - lastPrice) % parseFloat('<%=_activity.AddPrice %>') != 0) {
                                    errMsg.push('<li>加价幅度必须是<%=(double)_activity.AddPrice %>万元的整数倍</li>');
                                }
                                if (parseFloat(price) - lastPrice > parseFloat('<%=_activity.MaxPrice %>')) {
                                    errMsg.push('<li>单次加价不能高于<%=(double)_activity.MaxPrice %>万元</li>');
                                }

                                if (errMsg.length > 0) {
                                    var tip = '<div style="padding:0 30px;"><p><b>对不起，您的出价无效，请您重新出价！</b></p>';
                                    tip += '<ul style="list-style: disc;margin: 20px 0;line-height: 25px;">' + errMsg.join('');
                                    tip += '</ul></div>';
                                    $.freeDialog.open({ content: tip, title: '出价提示', height: 240, width: 380, allowClose: true, isResize: true });
                                    return;
                                }

                                $.ajax({
                                    type: "post",
                                    url: globalvar.siteRoot + "/Premises/SubBidPriceInfo",
                                    dataType: "json",
                                    data: { updatesalesstatus: 0, status: 0, bidtype: 1, price: price, realName: realName, code: code, mobile: mobile, qq: qq, email: email, activitiesId: activitiesId, developerId: developerId, houseId: houseId, type: 5 },
                                    beforeSend: function (XMLHttpRequest) {

                                    },
                                    success: function (data) {
                                        var result = data.state;
                                        var orderID = data.orderID;
                                        var bond = '<%=(double)_activity.Bond %>';

                                        //========
                                        var str = " ";
                                        str += "<div style=\"padding:20px;\">";
                                        str += "<div class=\"layer_box\"><input id=\"orderID\" type=\"hidden\" value=\"" + orderID + "\"/>";
                                        str += "<p class=\"tac font14 mb20\">本次应支付保证金：<strong class=\"yellow\">" + bond + "元</strong></p>";
                                        str += "<p class=\"tac mb20\">";
                                        str += "<input type=\"button\" onclick=\"getVerificationCode()\" value=\"支付保证金\" class=\"btn_w80_gray\" /><input type=\"button\" onclick=\"$('.l-dialog-close').click();\" value=\"取消\" class=\"btn_w80_gray ml10\" /></p>";
                                        str += "<div class=\"line mb15\"></div>";
                                        str += "<div class=\"line_h24\"><p class=\"col333\">温馨提示：</p>";
                                        str += "<p class=\"col666\">1.为了确保交易的正常秩序，保护竞购参与者的权益，以及线下正签的顺 利进行。需要支付保证金。</p>";
                                        str += "<p class=\"col666\">2.竞购成功后，您随时都可以登录用户中心查看您购得的房屋。您与开发商双方约定时间签署《意向成交确认书》。保证金冲抵相应额度首付款。</p>";
                                        str += "<p class=\"col666\">3.竞购不成功，在竞购结束后7天之后自动返还至您的账户中。</p></div></div>";

                                        if (result == 1) {
                                            var tip = '<div style="padding:0 30px; text-align:center "><p><b>恭喜，您本次出价已成功！</b></p>';
                                            tip += '<p style="margin-top:20px;">您的本次出价金额为：' + price + '万元</p></div>';
                                            tip += '<div style="padding: 20px;">温馨提示：<br/>您也可以继续出价，出价次数越多，成交几率越大</div>';

                                            $.freeDialog.open({
                                                content: tip,
                                                title: '出价提示',
                                                height: 240,
                                                width: 380,
                                                allowClose: true,
                                                isResize: true,
                                                closeEvent: function () { window.location.reload(); }
                                            });
                                        }
                                        else if (result == 2) {
                                            $.freeDialog.open({ content: str, title: '支付保证金', height: 380, width: 520, allowClose: true, isResize: true });
                                        }
                                        else if (result == 3) {
                                            $.freeDialog.open({ content: str, title: '支付保证金', height: 380, width: 520, allowClose: true, isResize: true });
                                        }
                                    }
                                });


                            }, 'text');
                        },
                        messages: {
                            price: {
                                required: "<span class=\"red ml20\">价格不能为空。</span>"
                            },
                            realname: {
                                required: "<span class=\"red ml20\">真实姓名不能为空</span>"
                            },
                            code: {
                                required: "<span class=\"red ml20\">身份证不能为空</span>"
                            },
                            mobile: {
                                required: "<span class=\"red ml20\">手机号不能为空</span>"
                            },
                            qq: {
                                digits: "<span class=\"red ml20\">QQ格式不正确</span>"
                            },
                            email: {
                                email: "<span class=\"red ml20\">邮箱格式不正确</span>"
                            }
                        },
                        onkeyup: false,
                        errorPlacement: function (error, element) {
                            var pele = element.parent().find("span");
                            $(pele).each(function () {
                                if ($(this).attr("class") != "ml10")
                                    $(this).remove();
                            });
                            error.appendTo(element.parent());
                        }
                    });

                    //用户信息验证
                    $("#userinfoForm").validate({
                        rules: {
                            realname: {
                                required: true,
                                isrealname: true
                            },
                            code: {
                                required: true,
                                iscode: true
                            },
                            mobile: {
                                required: true,
                                ismobile: true
                            },
                            qq: {
                                digits: true
                            },
                            email: {
                                email: true
                            }
                        },
                        submitHandler: function (form) {
                            var price = $("#price").val();
                            var realName = $("#realname").val();
                            var code = $("#code").val();
                            var mobile = $("#mobile").val();
                            var qq = $("#qq").val();
                            var email = $("#email").val();
                            var developerId = "<%=Model.DeveloperId %>";
                            var activitiesId = "<%=Model.ActivityId %>";
                            var houseId = "<%=Model.HouseId %>";
                            $.ajax({
                                type: "post",
                                url: globalvar.siteRoot + "/Premises/SubBidPriceInfo",
                                dataType: "json",
                                data: { updatesalesstatus: 0, status: 0, bidtype: 1, price: price, realName: realName, code: code, mobile: mobile, qq: qq, email: email, activitiesId: activitiesId, developerId: developerId, houseId: houseId, type: 5 },
                                beforeSend: function (XMLHttpRequest) {

                                },
                                success: function (data) {
                                    var result = data.state;
                                    var orderID = data.orderID;
                                    var bond = '<%=(double)_activity.Bond %>';

                                    //========
                                    var str = " ";
                                    str += "<div style=\"padding:20px;\">";
                                    str += "<div class=\"layer_box\"><input id=\"orderID\" type=\"hidden\" value=\"" + orderID + "\"/>";
                                    str += "<p class=\"tac font14 mb20\">本次应支付保证金：<strong class=\"yellow\">" + bond + "元</strong></p>";
                                    str += "<p class=\"tac mb20\">";
                                    str += "<input type=\"button\" onclick=\"getVerificationCode()\" value=\"支付保证金\" class=\"btn_w80_gray\" /><input type=\"button\" value=\"取消\" onclick=\"$('.l-dialog-close').click();\" class=\"btn_w80_gray ml10\" /></p>";
                                    str += "<div class=\"line mb15\"></div>";
                                    str += "<div class=\"line_h24\"><p class=\"col333\">温馨提示：</p>";
                                    str += "<p class=\"col666\">1.为了确保交易的正常秩序，保护竞购参与者的权益，以及线下正签的顺 利进行。需要支付保证金。</p>";
                                    str += "<p class=\"col666\">2.竞购成功后，您随时都可以登录用户中心查看您购得的房屋。您与开发商双方约定时间签署《意向成交确认书》。保证金冲抵相应额度首付款。</p>";
                                    str += "<p class=\"col666\">3.竞购不成功，在竞购结束后7天之后自动返还至您的账户中。</p></div></div>";

                                    if (result == 1) {
                                        globalvar.dialog("出价成功！", function () { window.location.reload(); });
                                    }
                                    else if (result == 2) {
                                        $.freeDialog.open({ content: str, title: '支付保证金', height: 380, width: 520, allowClose: true, isResize: true });
                                    }
                                    else if (result == 3) {
                                        $.freeDialog.open({ content: str, title: '支付保证金', height: 380, width: 520, allowClose: true, isResize: true });
                                    }
                                }
                            });
                        },
                        messages: {
                            price: {
                                required: "<span class=\"red ml20\">价格不能为空。</span>"
                            },
                            realname: {
                                required: "<span class=\"red ml20\">真实姓名不能为空</span>"
                            },
                            code: {
                                required: "<span class=\"red ml20\">身份证不能为空</span>"
                            },
                            mobile: {
                                required: "<span class=\"red ml20\">手机号不能为空</span>"
                            },
                            qq: {
                                digits: "<span class=\"red ml20\">QQ格式不正确</span>"
                            },
                            email: {
                                email: "<span class=\"red ml20\">邮箱格式不正确</span>"
                            }
                        },
                        onkeyup: false,
                        errorPlacement: function (error, element) {
                            var pele = element.parent().find("span");
                            $(pele).each(function () {
                                if ($(this).attr("class") != "ml10")
                                    $(this).remove();
                            });
                            error.appendTo(element.parent());
                        }
                    });

                });

                //支付保证金
                function getVerificationCode() {

                    var orderID = $("#orderID").val();

                    $.post("<%=TXCommons.GetConfig.GetSiteRoot%>/Premises/UpdateOrder", { orderID: orderID }, function (data) {
                        if (parseInt(data) == 1) {
                            //alert("订单费用已从账户中扣除，订单处理成功！");
                            window.location.reload();
                        }
                        else if (parseInt(data) == 2) {
                            //跳转第三方支付
                            window.location.href = "<%=TXCommons.GetConfig.GetSiteRoot%>/PayOrder/OnlinePayDeposit?type=2&id=" + orderID;
                        }
                        else {
                            alert("未能完成支付，订单处理失败！");
                        }
                    });
                }


                function isIdCardNo(num) {
                    num = num.toUpperCase();
                    //身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X。   
                    if (!(/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(num))) {
                        //alert('输入的身份证号长度不对，或者号码不符合规定！\n15位号码应全为数字，18位号码末位可以为数字或X。');
                        return false;
                    }
                    //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。 
                    //下面分别分析出生日期和校验位 
                    var len, re;
                    len = num.length;
                    if (len == 15) {
                        re = new RegExp(/^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/);
                        var arrSplit = num.match(re);
                        //检查生日日期是否正确 
                        var dtmBirth = new Date('19' + arrSplit[2] + '/' + arrSplit[3] + '/' + arrSplit[4]);
                        var bGoodDay;
                        bGoodDay = (dtmBirth.getYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
                        if (!bGoodDay) {
                            //alert('输入的身份证号里出生日期不对！');
                            return false;
                        }
                        else {
                            //将15位身份证转成18位 
                            //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。 
                            var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
                            var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                            var nTemp = 0, i;
                            num = num.substr(0, 6) + '19' + num.substr(6, num.length - 6);
                            for (i = 0; i < 17; i++) {
                                nTemp += num.substr(i, 1) * arrInt[i];
                            }
                            num += arrCh[nTemp % 11];
                            return num;
                        }
                    }
                    if (len == 18) {
                        re = new RegExp(/^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/);
                        var arrSplit = num.match(re);
                        //检查生日日期是否正确 
                        var dtmBirth = new Date(arrSplit[2] + "/" + arrSplit[3] + "/" + arrSplit[4]);
                        var bGoodDay;
                        bGoodDay = (dtmBirth.getFullYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
                        if (!bGoodDay) {
                            //alert(dtmBirth.getYear());
                            //alert(arrSplit[2]);
                            //alert('输入的身份证号里出生日期不对！');
                            return false;
                        }
                        else {
                            //检验18位身份证的校验码是否正确。 
                            //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。 
                            var valnum;
                            var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
                            var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                            var nTemp = 0, i;
                            for (i = 0; i < 17; i++) {
                                nTemp += num.substr(i, 1) * arrInt[i];
                            }
                            valnum = arrCh[nTemp % 11];
                            if (valnum != num.substr(17, 1)) {
                                //alert('18位身份证的校验码不正确！应该为：' + valnum);
                                return false;
                            }
                            return num;
                        }
                    }
                    return false;
                }
            </script>
            <!--如果已登录--end-->
            <% } %>
            <div class="text_title">
                营销活动规则</div>
            <p class="text_p">
                1.参与本次活动需注册为快有家会员，符合国家规定的具有购买本竞购房源资格的方可参加本次竞购活动。（请准确、真实填写购房用户的姓名、身份证号、联系方式等个人信息资料，否则不可竞购）</p>
            <p class="text_p">
                2.参与竞购时您需要缴纳活动保证金，保证金支付成功后，即可参加。（活动结束之后，不管是否成功，保证金一周之后自动退还到您的账户中）</p>
            <p class="text_p">
                3.秒杀、一口价、限时折扣竞购成功之后，需在<%=TXCommons.GetConfig.MSOperatingTime%>分钟之内支付活动保证金，否则视为放弃。
            </p>
            <p class="text_p">
                4.凡赢得竞购的获胜者开发商会在最短的时间内与您联系，办理线下的购房手续。
            </p>
            <p class="text_p">
                5.凡赢得竞购的获胜者在规定时间内不履行购房手续的用户，快有家有权没收其保证金。
            </p>
            <p class="text_p">
                6.凡被监控到恶意竞购的非合法用户，快有家有权没收其保证。
            </p>
        </div>
        <div class="right">
            <ul class="r_qh_title clearFix">
                <li class="on" style="cursor: pointer;" id="all">大家正在出价</li>
                <li id="self" style="cursor: pointer;">我的出价记录</li>
            </ul>
            <div class="margin15">
                <table width="100%" cellspacing="0" cellpadding="0" border="0" class="table_box1 td_col333">
                    <tbody id="result">
                    </tbody>
                </table>
            </div>
            <div class="page tac border_top">
                <span class="col333 font12 mr10">共 <em class="col666" id="totalCount"></em>条记录</span>
                <span id="page"></span>
            </div>
        </div>
    </div>
    <%
            }

            if (Model.SalesStatus == 2 && Model.ActBeginTime > DateTime.Now)
            {%>
    <div class="r_title_lp">
        <strong class="title_span pad_0_30">营销活动规则</strong><span class="col666 mr15"><i class="colff840b mr5 ml5"><%=ViewData["viewCount"]%></i>人围观</span></div>
    <div class="gray_box gray_box1 clearFix">
        <div class="left" style="float: left; padding: 10px 0px 30px 20px;">
            <div class="text_title">
                营销活动规则</div>
            <p class="text_p">
                1.参与本次活动需注册为快有家会员，符合国家规定的具有购买本竞购房源资格的方可参加本次竞购活动。（请准确、真实填写购房用户的姓名、身份证号、联系方式等个人信息资料，否则不可竞购）</p>
            <p class="text_p">
                2.参与竞购时您需要缴纳活动保证金，保证金支付成功后，即可参加。（活动结束之后，不管是否成功，保证金一周之后自动退还到您的账户中）</p>
            <p class="text_p">
                3.秒杀、一口价、限时折扣竞购成功之后，需在<%=TXCommons.GetConfig.MSOperatingTime%>分钟之内支付活动保证金，否则视为放弃。
            </p>
            <p class="text_p">
                4.凡赢得竞购的获胜者开发商会在最短的时间内与您联系，办理线下的购房手续。
            </p>
            <p class="text_p">
                5.凡赢得竞购的获胜者在规定时间内不履行购房手续的用户，快有家有权没收其保证金。
            </p>
            <p class="text_p">
                6.凡被监控到恶意竞购的非合法用户，快有家有权没收其保证。
            </p>
        </div>
    </div>
    <%} 
    %>
    <%  } %>
    <script type="text/javascript">
        var index = $("#pageIndex").val()
        var activityId = '<%=Model.ActivityId %>';
        var houseId = '<%=Model.HouseId%>';
        var userId = '<%=ViewData["userId"] %>';
        if (userId == "0") {
            $("#self").hide();
        }
        $("#all").click(function () {
            $("#_type").val(1);
            search(index, $("#_type").val());
        });
        $("#self").click(function () {
            $("#_type").val(2);
            search(index, $("#_type").val());
        });

        search(1, $("#_type").val());
        //上一页
        function OnPage() {
            if (parseInt($("#pageIndex").val()) <= 1) return;
            search(parseInt($("#pageIndex").val()) - 1, $("#_type").val());
        }
        //下一页
        function NextPage() {
            if (parseInt($("#pageIndex").val()) >= parseInt($("#totalPageCount").val())) return;
            search(parseInt($("#pageIndex").val()) + 1, $("#_type").val());
        }
        //第几页
        function InPage(page) {
            search(page, $("#_type").val());
        }

        function search(index, type1) {
            if (type1 == 2) {
                $(".right .r_qh_title li").removeClass();
                $("#self").attr("class", "on");
            }
            if (type1 == 1) {
                $(".right .r_qh_title li").removeClass();
                $("#all").attr("class", "on");
            }
            $.post("<%=TXCommons.GetConfig.GetSiteRoot%>/Premises/BidPricesPage", { m: Math.random(), activityId: activityId, houseId: houseId, pageIndex: index, type: type1 }, function (d) {
                if (d != "0") {
                    var json = eval("(" + d + ")");
                    var str = "";
                    var t = "";
                    var tval = "";
                    if (type1 == 3) {
                        t += "<th scope=\"col\">&nbsp;</th>";
                    }
                    var result = "<tr>" + t + "<th scope=\"col\">出价金额</th><th scope=\"col\">出价时间</th><th scope=\"col\">买家</th></tr>";
                    var RevList = json.list;
                    for (var i = 0; i < RevList.length; i++) {
                        var item = RevList[i];
                        if (type1 == 3) {
                            tval = "<td><span " + (item.Status == 0 ? "" : "class='yellow_bjtb'") + ">" + (item.Status == 0 ? "出局" : "成交") + "</span></td>";
                        }
                        result += "<tr>" + tval + "<td>" + item.BidUserPrice + "万元</td><td>" + item.CreateTime.toString() + "</td><td>" + (item.BidUserMobile.length == 0 ? "" : item.BidUserMobile.replace(/(\d{3})\d{4}(\d{4})/, '$1****$2')) + "</td></tr>";

                    }
                    $("#totalPageCount").val(json.page.pagecount);
                    $("#pageIndex").val(json.page.index);
                    $("#totalCount").html(json.page.totalcount);
                    //分页显示[up][1][2][3][4][5][6][down]
                    var pagecount = $("#totalPageCount").val();
                    var myindex = index;
                    var temp = 0;
                    if (pagecount <= 6) {
                        if (myindex == 1) {
                            //上一页（不可点击）
                            str += "<a class=\"no\">&lt;&lt;</a>";
                        }
                        else {
                            //上一页（可点击）
                            str += "<a href=\"javascript:OnPage()\">&lt;&lt;</a>";
                        }
                        for (var i = 0; i < pagecount; i++) {
                            if (i + 1 == myindex) {
                                //选中
                                str += "<a class=\"hover\">" + (i + 1) + "</a>";
                            }
                            else {
                                //不选中
                                str += "<a href=\"javascript:InPage(" + (i + 1) + ")\">" + (i + 1) + "</a>";
                            }
                        }
                        if (myindex == pagecount) {
                            //下一页（不可点击）
                            str += "<a class=\"no\">&gt;&gt;</a>";
                        }
                        else {
                            //下一页（可点击）
                            str += "<a href=\"javascript:NextPage()\">&gt;&gt;</a>";
                        }
                    }
                    else {
                        if (myindex == 1) {
                            //上一页（不可点击）
                            str += "<a class=\"no\">&lt;&lt;</a>";
                        }
                        else {
                            //上一页（可点击）
                            str += "<a href=\"javascript:OnPage()\">&lt;&lt;</a>";
                        }
                        if (myindex > 3) {
                            if (pagecount - myindex > 3) {
                                for (var i = myindex - 3; i < pagecount; i++) {
                                    temp++;
                                    if (temp <= 6) {
                                        if (i + 1 == myindex) {
                                            //选中
                                            str += "<a class=\"hover\">" + (i + 1) + "</a>";
                                        }
                                        else {
                                            //不选中
                                            str += "<a href=\"javascript:InPage(" + (i + 1) + ")\">" + (i + 1) + "</a>";
                                        }
                                    }
                                }
                            } else {
                                for (var i = pagecount - 6; i < pagecount; i++) {
                                    temp++;
                                    if (temp <= 6) {
                                        if (i + 1 == myindex) {
                                            //选中
                                            str += "<a class=\"hover\">" + (i + 1) + "</a>";
                                        }
                                        else {
                                            //不选中
                                            str += "<a href=\"javascript:InPage(" + (i + 1) + ")\">" + (i + 1) + "</a>";
                                        }
                                    }
                                }
                            }
                        }
                        else {
                            for (var i = 0; i < pagecount; i++) {
                                temp++;
                                if (temp <= 6) {
                                    if (i + 1 == myindex) {
                                        //选中
                                        str += "<a class=\"hover\">" + (i + 1) + "</a>";
                                    }
                                    else {
                                        //不选中
                                        str += "<a href=\"javascript:InPage(" + (i + 1) + ")\">" + (i + 1) + "</a>";
                                    }
                                }
                            }
                        }
                        if (myindex == pagecount) {
                            //下一页（不可点击）
                            str += "<a class=\"no\">&gt;&gt;</a>";
                        }
                        else {
                            //下一页（可点击）
                            str += "<a href=\"javascript:NextPage()\">&gt;&gt;</a>";
                        }
                    }
                    $("#page").html(str);
                    $("#result").html(result);
                    $("#pageIndex").val(index);
                } else {
                    $("#totalPageCount").val(0);
                    $("#pageIndex").val(1);
                    $("#totalCount").html(0);
                    $("#page").html("");
                    $("#result").html("<span>暂无记录！</span>");
                }
            });
        }

    </script>
    <script type="text/javascript">
        //秒杀倒计时
        function showTime(index, time_distance, sign) {
            this.index = index;
            this.time_distance = time_distance * 1000;
            this.sign = sign;
        }
        showTime.prototype.SetShowTime = function () {
            var int_day, int_hour, int_minute, int_second, ms;
            time_distance = this.time_distance;
            this.time_distance = this.time_distance - 100;
            if (time_distance > 0) {
                //计算时、分、秒现在不现实天，所以不要解开注释
                int_day = Math.floor(time_distance / 86400000);
                time_distance -= int_day * 86400000;
                int_hour = Math.floor(time_distance / 3600000);
                time_distance -= int_hour * 3600000;
                int_minute = Math.floor(time_distance / 60000);
                time_distance -= int_minute * 60000;
                int_second = Math.floor(time_distance / 1000);
                //time_distance -= int_minute * 1000;
                ms = Math.floor((time_distance % 1000) / 100);
                if (int_hour < 10) { int_hour = "0" + int_hour; }
                if (int_minute < 10) { int_minute = "0" + int_minute; }
                if (int_second < 10) { int_second = "0" + int_second; }
                //赋值
                var timer = $("#" + this.index);
                timer.children("strong").eq(0).html(int_day);
                timer.children("strong").eq(1).html(int_hour);
                timer.children("strong").eq(2).html(int_minute);
                timer.children("strong").eq(3).html(int_second + "." + ms);
                var self = this;
                //调用
                setTimeout(function () { self.SetShowTime(); }, 100);
            }
        }
        $(function () {
            //秒杀倒计时调用
            var timerIndex;
            var timer = $(".col999");
            $.each(timer, function () {
                var $this = $(this);
                if (!$this.attr("data")) { return; }
                var st = new showTime($this.attr("id"), $this.attr("data"), $this.attr("lang"));
                st.SetShowTime();
            });

        });
    </script>
</div>
