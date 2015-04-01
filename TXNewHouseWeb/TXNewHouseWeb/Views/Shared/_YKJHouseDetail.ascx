<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TXModel.Web.PremisesHouse>" %>
<%@ Import Namespace="TXOrm" %>
<style type="text/css">
    .layer_box { padding: 5px 20px; font-size: 12px; }
</style>
<% 
    TXOrm.Activity _activity = new TXOrm.Activity();
    _activity = (TXOrm.Activity)ViewData["activity"];

    //如果房源是可售状态并且活动时间没有结束时显示，否则不显示
    if (Model.SalesStatus == 2 && _activity.EndTime > DateTime.Now)
    {
%>
<div class="w1190">
    <div class="yellow_box1 r_y_ykj clearFix">
        <div class="part1">
            <p class="col333 font18 fontYaHei">
                <%=TXCommons.Util.getStrLenB(Model.PremisesName,0,20)%></p>
            <p class="col666 font14 fontYaHei mt5">
                <%=Model.BuildingName %>
                <%=Model.UnitName %>
                <%=Model.Floor.ToString() %>层
                <%=Model.HouseName %>号房</p>
        </div>
        <div class="part2 mt15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <th scope="row" align="right" width="80">
                        一口价：
                    </th>
                    <td colspan="3">
                        <strong class="green mr3">
                            <%=(double)_activity.BidPrice%></strong>万元<span class="ts_w96">劲省<%= (double)(Model.TotalPrice - _activity.BidPrice)%>万元</span>
                    </td>
                </tr>
                <tr>
                    <th scope="row" align="right">
                        市场价格：
                    </th>
                    <td>
                        <strong class="green mr3">
                            <%=(double)Model.TotalPrice%></strong>万元
                    </td>
                    <th scope="row" align="right" width="80">
                        竞购方式：
                    </th>
                    <td>
                        一口价 <span class="r_wen_green ml20"><i class="r_layer_box">开发商针对一定数量的房源进行一口价出售，在规定时间内用户谁第一个出此价格则视为竞购成功。
                            <span class="sj"></span></i></span>
                    </td>
                </tr>
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
                <%=_activity.BeginTime.ToString("yyyy.MM.dd") %>
                -
                <%=_activity.EndTime.ToString("yyyy.MM.dd") %></p>
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
            <a href="javascript:void(0);" class="s-link-d mt10" onclick="startMS()">立刻竞购</a>
            <% 
              }
              //如果活动时间已经结束
              else
              {
            %>
            <a href="javascript:void(0);" class="s-link-d mt10">结束等待</a>
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
        //如果活动过期或结束，房源状态为出售状态则不显示，否则显示一口价信息
        if (Model.SalesStatus == 2 && (_activity.EndTime < DateTime.Now || _activity.ActivitieState == 2)) { }
        else
        {
    %>
    <%
            List<TXOrm.BidPrice> _bidPrice = (ViewData["BidPrice"] as List<TXOrm.BidPrice>).Where(b => b.Status > 0).OrderByDescending(b => b.Id).ToList();
            //如果房源已经认购或签约
            if ((Model.SalesStatus == 3 || Model.SalesStatus == 4) && _bidPrice.Count > 0)
            {
            
    %>
    <div class="r_title_lp">
        <strong class="title_span pad_0_30">一口价</strong><span class="col666 font12 ml15"><i
            class="colff840b mr5 ml5"><%=ViewData["viewCount"]%></i>人围观</span></div>
    <div class="yellow_box1 yellow_box2 clearFix">
        <div class="part2 ml10 mt10" style="width: 620px;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <th align="right" scope="row">
                            一口价：
                        </th>
                        <td>
                            <strong class="green mr3">
                                <%=(double)_activity.BidPrice%></strong>万元<span class="ts_w96"> 劲省<%= (double)(Model.TotalPrice - _activity.BidPrice)%>万元</span>
                        </td>
                        <th align="right" scope="row">
                            市场价格：
                        </th>
                        <td>
                            必须为<strong class="green mlr3"><%=(double)Model.TotalPrice%></strong>万元
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            竞购方式：
                        </th>
                        <td>
                            一口价 <span class="r_wen_green ml20"><i class="r_layer_box">开发商针对一定数量的房源进行一口价出售，在规定时间内用户谁第一个出此价格则视为竞购成功。
                                <span class="sj"></span></i></span>
                        </td>
                        <th scope="row">
                            竞购时间：
                        </th>
                        <td>
                            <%= _activity.BeginTime.ToString("yyyy年MM月dd日 HH:mm")%>
                            -
                            <%= _activity.EndTime.ToString("yyyy年MM月dd日 HH:mm")%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="part5 mt15 mr20">
            <div class="pos_abs">
                <input type="button" value="一口价结束" class="btn_w126_gray" /></div>
            <p>
                <span class="col333 mr3">
                    <%=_bidPrice[0].BidUserName%></span>以<span class="fontYahei font18 colff840b ml3"><%=(double)_activity.BidPrice%>万元</span></p>
            <p>
                购买成功</p>
        </div>
    </div>
    <%
            }
            else
            {
    %>
    <div class="r_title_lp">
        <strong class="title_span pad_0_30">营销活动规则</strong><span class="col666 font12 ml15"><i
            class="colff840b mr5 ml5"><%=ViewData["viewCount"]%></i>人围观</span></div>
    <div class="gray_box gray_box2 clearFix">
        <div class="left" style="display: none;">
            <!--如果没有登录--begin-->
            <%
                if (!(bool)ViewData["IsLogin"])
                {
            %>
            <p class="ml15">
                <strong class="red">请先登录！</strong></p>
            <div id="login_form" class="layer_box">
                <form id="loginForm" action="" method="post">
                <table border="0" cellspacing="0" cellpadding="0" class="tab_k">
                    <tr>
                        <th align="right" scope="row">
                            登录名：
                        </th>
                        <td>
                            <input type="text" id="username" name="username" placeholder="请输入用户名" class="input1"><%--<span class="red ml20">请输入正确信息</span>--%>
                        </td>
                    </tr>
                    <tr>
                        <th align="right" scope="row">
                            密&#12288;码：
                        </th>
                        <td>
                            <input type="password" placeholder="请输入密码" class="input1" id="password" name="password">
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">
                            &nbsp;
                        </th>
                        <td>
                            <input type="submit" class="btn_w110_yellow" value="登录">&nbsp;&nbsp;<label style="font-size:12px;vertical-align:bottom">没有账号？</label><a href="javascript:void(0);" onclick="window.location.href='<%=TXCommons.GetConfig.RegisterUrl%>'" style="font-size:12px;color:#2255DD;vertical-align:bottom">注册会员</a>
                        </td>
                    </tr>
                </table>
                </form>
            </div>
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

                function startMS() {
                    //未登录跳转到登录页面
                    $.freeDialog.open({ target: $('#login_form'), title: '用户登录', height: 300, width: 500, allowClose: true, isResize: true });
                }

            </script>
            <%
                }
                else
                {
            %>
            <!--如果没有登录--end-->
            <!--如果已登录--begin-->
            <%-- <p><strong class="red">请填写出价金额完成您的出价。</strong></p>--%>
            <div id="userInfo_form">
                <form id="bidpriceForm" action="" method="post">
                <table cellspacing="0" cellpadding="0" border="0" class="tab_k" style="margin: 0px 20px;">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                <div class="red">
                                    竞购成功，请完善或者核对您的个人信息；并在<%=TXCommons.GetConfig.MSOperatingTime%>分钟之内支付保证金，否者购房资格取消。</div>
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
                                <input type="text" value="" class="input1" id="code" name="code" />
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
                                <input type="submit" class="btn_w120_gray" value="提交" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                </form>
            </div>
            <div id="pay_msg">
                <div class="layer_box">
                    <p class="tac font14 mb20">
                        本次应支付保证金：<strong class="yellow"><%=(double)_activity.Bond %>元</strong></p>
                    <p class="tac mb20">
                        <input type="hidden" id="orderID" value="" />
                        <input type="button" value="支付保证金" class="btn_w80_gray" onclick="payBond()" /><input
                            type="button" value="取消" class="btn_w80_gray ml10" onclick="stopPay()" /></p>
                    <div class="line mb15">
                    </div>
                    <div class="line_h24">
                        <p class="col333">
                            温馨提示：</p>
                        <p class="col666">
                            1.为了确保交易的正常秩序，保护竞购参与者的权益，以及线下正签的顺利进行。需要支付保证金。</p>
                        <p class="col666">
                            2.订购完成之后需在<%=TXCommons.GetConfig.MSOperatingTime%>分钟之内支付保证金，否则购房资格取消。</p>
                        <p class="col666">
                            3.活动保证金，在竞购结束后7天之后自动返还至您的账户中。</p>
                    </div>
                </div>
            </div>
            <div id="overPay_msg">
                <div class="layer_box">
                    <p class="tac font14 mb20">
                        恭喜您获得【<%=Model.BuildingName%>
                        <%=Model.UnitName%>
                        <%=Model.Floor.ToString()%>层
                        <%=Model.HouseName%>号房】购房资格！<br />
                        1-2个工作日内，开发商会打电话与你联系进行线下交易。
                    </p>
                    <p class="tac mb20">
                        <input type="button" value="关闭" class="btn_w80_gray ml10" onclick="window.location.reload();" /></p>
                </div>
            </div>
            <script type="text/javascript">
                function startMS() {
                    var houseId = "<%=Model.HouseId %>";
                    var activityId = "<%=Model.ActivityId %>";
                    $.post("<%=TXCommons.GetConfig.GetSiteRoot%>/Premises/SubMSInfo", { houseId: houseId, activityId: activityId }, function (data) {
                        if (parseInt(data) > 0) {
                            $.freeDialog.open({ target: $('#userInfo_form'), title: '竞购成功', height: 420, width: 560, allowClose: true, isResize: true });
                        } else if (parseInt(data) < 0) {
                            $('#orderID').val(parseInt(data) * (-1));
                            $.freeDialog.open({ target: $('#pay_msg'), title: '支付保证金', height: 300, width: 450, allowClose: true, isResize: true });
                        } else {
                            globalvar.dialog("竞购失败!");
                        }
                    });
                }

                $(document).ready(function () {
                    //价格输入格式
                    jQuery.validator.addMethod("isprice", function (value, element) {
                        var c = /\d{1,10}(\.\d{1,2})?$/;
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

                    $("#bidpriceForm").validate({
                        rules: {
                            price: {
                                required: true,
                                isprice: true
                            },
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
                            var price = "<%=_activity.BidPrice %>";
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
                                data: { updatesalesstatus: 1, status: 1, bidtype: 4, price: price, realName: realName, code: code, mobile: mobile, qq: qq, email: email, activitiesId: activitiesId, developerId: developerId, houseId: houseId, type: 5 },
                                beforeSend: function (XMLHttpRequest) {

                                },
                                success: function (data) {
                                    var result = data.state;
                                    $('#orderID').val(data.orderID);
                                    $('.l-dialog-close').click();
                                    if (result > 0) {
                                        $.freeDialog.open({ target: $('#pay_msg'), title: '支付保证金', height: 300, width: 450, allowClose: true, isResize: true });
                                    }
                                    else {
                                        globalvar.dialog("提交失败！", function () { window.location.reload(); });
                                    }
                                }
                            });
                        },
                        messages: {
                            price: {
                                required: "<span class=\"red ml20\">用户名不能为空。</span>"
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
                function payBond() {
                    var orderID = $("#orderID").val();
                    $.post("<%=TXCommons.GetConfig.GetSiteRoot%>/Premises/UpdateMSOrder", { orderID: orderID }, function (data) {
                        if (parseInt(data) == 1) {
                            $.freeDialog.open({ target: $('#overPay_msg'), title: '竞购成功', height: 300, width: 450, allowClose: true, isResize: true, closeEvent: function () { window.location.reload(); } });
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
                //取消支付
                function stopPay() {
                    window.location.reload();
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
        </div>
        <div class="right" style="float: left; border-left: none;">
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
    </div>
    <% } %>
    <%  } %>
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
