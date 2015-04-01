<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXOrm.Developer_AccountLog>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    账户管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="yellow_box font14">
            <p class="mt15 ml20 col333">
                <strong>我的账户余额：<em class="orange bold"><%=ViewData["AccountMoney"].ToString().Split('.')[0]%>
                    元</em></strong><span class="ml85">充值总额： <em class="orange">
                        <%=ViewData["TotalRecharge"].ToString().Split('.')[0]%>
                        元</em></span><span class="ml85">提现总额： <em class="colff8200">
                            <%=ViewData["Totaldraw"]%>元</em></span></p>
            <p class="mt15 ml20 mb15">
                <%--<span class="whao">关于金币</span>--%></p>
        </div>
        <div class="my_title mt10">
            <a href="javascript:void(0)">账户流水</a><a href="javascript:void(0)">我要充值</a><a href="javascript:void(0)">我要提现</a></div>
        <%-- 流水--%>
        <div id="div_flow" class="dd_skip">
            <div class="mt15 mb15 ml15">
                <strong class="font14">搜索：</strong><input id="starttime" readonly="readonly" type="text"
                    class="input_wauto w100 Wdate" value="<%=ViewData["starttime"] %>" />
                -
                <input id="endtime" readonly="readonly" type="text" class="input_wauto w100 Wdate"
                    value="<%=ViewData["endtime"] %>" />
                <select id="sl_type" class="select1 w180">
                    <option value="0">按类型筛选</option>
                    <option value="1">保证金</option>
                    <option value="2">账户提现</option>
                     <option value="3">账户充值</option>
                </select>
                <input type="button" value="查找" class="btn_w80" onclick="search()" />
            </div>
            <p class="font12 mb10 ml15">
                共<span class="red"><%=ViewData["TotalItemCount"] %>
                </span>条记录<span class="ml20 mr20">结算总计：<i class="green">出 -<%=ViewData["TotalExpenses"].ToString().Split('.')[0]%>元</i></span><span
                    class="orange">入：+<%=ViewData["TotalIncome"].ToString().Split('.')[0]%>元</span></p>
            <div id="divAjaxData">
                <%=Html.Partial("AccountTable", Model)%>
            </div>
        </div>
        <%-- 充值--%>
        <div id="div_Recharge" class="dd_skip pt10">
            <%using (Html.BeginForm("Recharge", "Account", FormMethod.Post))
              { %>
            <table id="tb_table2" width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1 font12 col666">
                <tr>
                    <th scope="row" align="right" width="100">
                        登录名：
                    </th>
                    <td>
                        <%=ViewData["LoginName"]%>
                    </td>
                </tr>
                <tr>
                    <th scope="row" align="right" valign="middle">
                        <p class="mt10">
                            充值金额：</p>
                    </th>
                    <td>
                        <div class="mt10">
                            <label>
                                <input name="radio" type="radio" value="1" class="valign2 mr5" />200元</label>
                            <label class="ml20">
                                <input name="radio" type="radio" value="2" class="valign2 mr5" />400元</label>
                            <label class="ml20">
                                <input name="radio" type="radio" value="3" class="valign2 mr5" />600元</label>
                            <input type="text" class="input_wauto w100 mr5" placeholder="其他金额" id="qtMoney"
                                name="qtMoney" />元<span style="display: none;" class="ie_valign_5 no" id="error1"></span>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="line">
            </div>
            <div class="mt20 ml85">
                <input type="submit" class="btn_w97_green" value="立即充值" onclick="return chongzhi()" />
            </div>
            <%} %>
        </div>
        <%-- 提现--%>
        <div id="div_Withdrawal" class="dd_skip">
            <%using (Html.BeginForm("Withdrawal", "Account", FormMethod.Post))
              {%>
            <table id="tb_table3" width="100%" border="0" cellspacing="0" cellpadding="0" class="table_box2 font12 mt10">
                <tr>
                    <td width="100" align="right">
                        登录名：
                    </td>
                    <td>
                        <%=ViewData["LoginName"]%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        可提现金额：
                    </td>
                    <td>
                        <%=ViewData["AccountMoney"].ToString().Split('.')[0]%>元
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="orange">*</span>本次提现金额：
                    </td>
                    <td>
                        <input id="drawMoney" name="drawMoney" type="text" class="input_wauto w100 mr10" /><span
                            class="mr10 ie_valign_5">元</span><span class="ie_valign_5 no" style="display: none;"
                                id="error2"></span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="orange">*</span>收款方式：
                    </td>
                    <td>
                        <label>
                            <input type="radio" name="receiveType" class="valign2 mr10" id="radio_zhifubao" value="0" /><strong
                                class="font14">支付宝</strong></label><label class="ml85"><input type="radio" name="receiveType"
                                    class="valign2 mr10" id="radio_yinhangka" value="1" /><strong class="font14">银行卡</strong></label>
                    </td>
                </tr>
                <tbody id="tab1">
                    <tr>
                        <td align="right">
                            <span class="orange">*</span>真实姓名：
                        </td>
                        <td>
                            <input id="reallyname" name="reallyname" type="text" placeholder="请输入您的真实姓名" value=""
                                class="input_wauto w200" />
                            <span class="ie_valign_5 no" style="display: none;" id="error3"></span>
                            <p class="col999 mt10">
                                请输入与支付宝账户相匹配的真实姓名，否则会导致无法收款！</p>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="orange">*</span>支付宝账户：
                        </td>
                        <td>
                            <input id="zfbaccount" name="zfbaccount" type="text" placeholder="邮箱地址/手机号码" value=""
                                class="input_wauto w200" />
                            <span class="ie_valign_5 no" style="display: none;" id="error4"></span>
                            <p class="col999 mt10">
                                请填写您的支付宝账户名，为了避免您无法收款，请认真核对是否有误！</p>
                        </td>
                    </tr>
                </tbody>
                <tbody id="tab2" style="display: none;">
                    <tr>
                        <td align="right">
                            <span class="orange">*</span>开户城市：
                        </td>
                        <td>
                            <select class="select1 mr10" id="province" name="province" onchange="changeprovince(this)">
                                <option value="0">选择省份</option>
                                <% var pList = ViewData["privent"] as TXCommons.bd.cjkjb.webservice.Area[];
                                   foreach (var item in pList)
                                   {
                                %>
                                <option value="<%=item.Id+","+item.Name %>">
                                    <%=item.Name %></option>
                                <%} %>
                            </select>
                            <span class="ie_valign_5 no" style="display: none;" id="error5"></span>
                            <select class="select1" id="city" name="city">
                                <option value="0">选择城市</option>
                            </select>
                            <span class="ie_valign_5 no" style="display: none;" id="error6"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="orange">*</span>开户银行：
                        </td>
                        <td>
                            <input type="text" id="bank" name="bank" value="" class="input_wauto w200" />
                            <span class="ie_valign_5 no" style="display: none;" id="error7"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="orange">*</span>开户支行：
                        </td>
                        <td>
                            <input type="text" id="bankson" name="bankson" value="" class="input_wauto w200" />
                            <span class="ie_valign_5 no" style="display: none;" id="error8"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="orange">*</span>户名：
                        </td>
                        <td>
                            <input type="text" id="accountname" name="accountname" value="" class="input_wauto w200" />
                            <span class="ie_valign_5 no" style="display: none;" id="error9"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="orange">*</span>银行账户：
                        </td>
                        <td>
                            <input type="text" id="bankno" name="bankno" value="" class="input_wauto w200" />
                            <span class="ie_valign_5 no" style="display: none;" id="error10"></span>
                        </td>
                    </tr>
                </tbody>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <input type="submit" value="申请提现" class="btn_w98_green mt15 mb15" onclick="return drawCash()" />
                    </td>
                </tr>
            </table>
            <%} %>
        </div>
    </div>
    <%--由于my97-4.7版本无法跨域，所以先引用根目录js文件--%>
    <script src="<%=TXCommons.GetConfig.BaseUrl + "js/my97datepicker/WdatePicker.js"%>" type="text/javascript"></script>
    <script type="text/javascript">
        var CurrentMenu = '<%=ViewData["CurrentMenu"] %>';
        $(function () {
            $(".dd_skip").eq(0).show();
            $(".my_title a").eq(0).addClass('on');
            switch (parseInt(CurrentMenu)) {
                case 61:
                    $(".my_title a").eq(0).addClass('on').siblings().removeClass('on');
                    break;
                case 62:
                    $(".my_title a").eq(1).addClass('on').siblings().removeClass('on');
                    $(".dd_skip").hide();
                    $(".dd_skip").eq(1).show();
                    break
                case 63:
                    $(".my_title a").eq(2).addClass('on').siblings().removeClass('on');
                    $(".dd_skip").hide();
                    $(".dd_skip").eq(2).show();
                    break;
            }
            $("#radio_zhifubao").attr("checked", "checked");
            $(".my_title a").click(function () {
                $(this).addClass('on').siblings().removeClass('on');
                var num = $(".my_title a").index(this);
                $(".dd_skip").hide();
                $(".ie_valign_5").hide();
                $(".dd_skip").eq(num).show();


                $("#divMenuTwo").find("ul").eq(5).find("li").eq(num).addClass('cur').siblings().removeClass('cur');//选中二级菜单
            })
            //类型
            if ('<%=ViewData["type"] %>' != null) {
                $("#sl_type").val('<%=ViewData["type"] %>');
            }
            else {
                $("#sl_type").val(0);
            }
            //日期控件
            $("#starttime").click(function () {
                WdatePicker({ dateFmt: 'yyyy-MM-dd' });
            });
            $("#endtime").click(function () {
                WdatePicker({ dateFmt: 'yyyy-MM-dd' });
            });
            //充值
            $("input:radio[name='radio']").each(function () {
                $(this).click(function () {
                    $("#qtMoney").val("");
                    $("#error1").hide();
                })
            })
            $("#qtMoney").keyup(function () {
                $("input:radio[name='radio']").attr("checked", false);
                $("#error1").hide();
            })
            $("#drawMoney").keyup(function () {
                $("#error2").hide();
            })
            $("#reallyname").keyup(function () {
                $("#error3").hide();
            })
            $("#zfbaccount").keyup(function () {
                $("#error4").hide();
            })
            $("#province").change(function () {
                $("#error5").hide();
            })
            $("#city").change(function () {
                $("#error6").hide();
            })
            $("#bank").keyup(function () {
                $("#error7").hide();
            })
            $("#bankson").keyup(function () {
                $("#error8").hide();
            })
            $("#accountname").keyup(function () {
                $("#error9").hide();
            })
            $("#bankno").keyup(function () {
                $("#error10").hide();
            })
            //提现
            $("#radio_zhifubao").click(function () {
                $(".ie_valign_5").hide();
                $("#tab1").show();
                $("#tab2").hide();
            })
            $("#radio_yinhangka").click(function () {
                $(".ie_valign_5").hide();
                $("#tab1").hide();
                $("#tab2").show();
            })
        })
        //查找
        function search() {
            var starttime = $("#starttime").val();
            var endtime = $("#endtime").val();
            var type = $("#sl_type").val();
            window.location = "<%=TXCommons.GetConfig.BaseUrl%>account/index?starttime=" + starttime + "&endtime=" + endtime + "&type=" + type + "";
        }
        //充值
        function chongzhi() {
            var qtMoney = $("#qtMoney").val();
            if ($('input:radio[name="radio"]:checked').val() == undefined && qtMoney == '') {
                $("#error1").show();
                $("#error1").html("请选择充值金额！");
                return false;
            }
            else {
                if ($.trim(qtMoney).length > 0) {
                    if (/^\d{0,15}\.{0,1}\d{0,2}$/.test(qtMoney)) {
                        if (parseInt(qtMoney) >= 1 && parseInt(qtMoney) <= 99999) {
                            return true;
                        } else {
                            $("#error1").show();
                            $("#error1").html("最小值为1元，最大值为99999元。");
                            return false;
                        }
                    }
                    else {
                        $("#error1").show();
                        $("#error1").html("只能输入数字！");
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
        }
        //提现
        function changeprovince(obj) {//选择城市
            var pid = $(obj).find('option:selected').val().split(',')[0];
            $.get("<%=TXCommons.GetConfig.BaseUrl%>account/GetCityList?pid=" + pid, function (result) {
                $("#city").html("").append("<option value='0'>选择城市</option>");
                $.each(result, function (item) {
                    var opt = "<option value='" + result[item].Id + "," + result[item].Name + "'>" + result[item].Name + "</option>";
                    $(opt).appendTo("#city");
                });
            });
        }
        function drawCash() {
            //提现金额验证
            var drawMoney = $("#drawMoney").val();
            var accountMoney = '<%=ViewData["AccountMoney"] %>';
            if ($.trim(drawMoney).length == 0) {
                $("#error2").show();
                $("#error2").html("金额不能为空！");
                return false;
            }
            else {
                if (/^\d{0,15}\.{0,1}\d{0,2}$/.test(drawMoney)) {
                    if (parseInt(drawMoney) < 1) {
                        $("#error2").show();
                        $("#error2").html("最小值为1元！");
                        return false;
                    }
                    if (parseInt(drawMoney) > accountMoney) {
                        $("#error2").show();
                        $("#error2").html("必须小于可取金额！");
                        return false;
                    }
                }
                else {
                    $("#error2").show();
                    $("#error2").html("只能输入数字！");
                    return false;
                }
            }
            var type = $('input[name="receiveType"]:checked').val();
            if (type == 0) { //支付宝
                //真实姓名验证
                var reallyname = $("#reallyname").val();
                if ($.trim(reallyname).length == 0) {
                    $("#error3").show();
                    $("#error3").html("真实姓名不能为空！");
                    return false;
                }
                if ($.trim(reallyname).length < 2 || $.trim(reallyname).length > 10) {
                    $("#error3").show();
                    $("#error3").html("真实姓名在2到10个字符之间！");
                    return false;
                }
                //支付宝账户验证
                var zfbaccount = $("#zfbaccount").val();
                var email = /^[a-zA-Z0-9_\.]+@[a-zA-Z0-9-]+[\.a-zA-Z]+$/;
                var phone = /^1[3|5|8][0-9]\d{4,8}$/;
                if ($.trim(zfbaccount).length == 0) {
                    $("#error4").show();
                    $("#error4").html("支付宝账号不能为空！");
                    return false;
                }
                if (email.test(zfbaccount) || phone.test(zfbaccount)) {
                    $("#error4").hide();
                } else {
                    $("#error4").show();
                    $("#error4").html("必须是手机号或者Email！");
                    return false;
                }
            }
            else { //银行卡
                //开户城市验证
                var province = $("#province").val();
                var city = $("#city").val();
                if (province == 0) {
                    $("#error5").show();
                    $("#error5").html("请选择省");
                    return false;
                }
                if (city == 0) {
                    $("#error6").show();
                    $("#error6").html("请选择市");
                    return false;
                }
                //开户银行系列验证
                var bank = $("#bank").val();
                var bankson = $("#bankson").val();
                var accountname = $("#accountname").val();
                var bankno = $("#bankno").val();
                if ($.trim(bank).length == 0) {
                    $("#error7").show();
                    $("#error7").html("银行不能为空");
                    return false;
                }
                if ($.trim(bankson).length == 0) {
                    $("#error8").show();
                    $("#error8").html("支行不能为空");
                    return false;
                }
                if ($.trim(accountname).length == 0) {
                    $("#error9").show();
                    $("#error9").html("户名不能为空");
                    return false;
                }
                if ($.trim(bankno).length == 0) {
                    $("#error10").show();
                    $("#error10").html("银行账户不能为空");
                    return false;
                }
            }
        }
    </script>
</asp:Content>
