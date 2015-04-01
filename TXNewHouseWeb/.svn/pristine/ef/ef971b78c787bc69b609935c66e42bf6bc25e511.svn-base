<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXOrm.Developer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	新房后台-修改资料
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
      <div class="reg_box clearFix mt5" >
        <h4 class="title_h4 mb10"><span>修改资料</span><em class="ts_right"><i class="red">*</i>为必填项</em></h4>
          <form id="frmUpdateUserInfo" action="/" method="post">
            <div class="table_box">
              <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tdpd">
                <tr>
                  <td align="right">登录名：</td>
                  <td><%=Model.LoginName%></td>
                </tr>
                <%--<tr>
                  <td align="right">真实姓名：</td>
                  <td><%=Model.RealName%></td>
                </tr>--%>
                <tr>
                  <td width="150" align="right">电子邮箱：</td>
                  <td><input id="txtEmail" name="txtEmail" type="text" value="<%=Model.Email%>" class="input_reg" maxlength="30"/></td>
                </tr>
                <tr>
                  <td width="150" align="right"><span class="orange">*</span>手机号码：</td>
                  <td><span id="showMobile"><%=Model.Mobile%></span><a id="aShowUpdateMobile" href="javascript:void(0);" class="ml20">修改手机号码&gt;&gt;</a></td>
                </tr>
                <tr>
                  <td width="150" align="right"> 固定电话：</td>
                  <td><input id="txtTelephone" name="txtTelephone" type="text" value="<%=Model.Telephone%>" class="input_reg" maxlength="20"/></td>
                </tr>
                <tr>
                  <td width="150" align="right">备用联系电话：</td>
                  <td><input id="txtSpareTelephone" name="txtSpareTelephone" type="text" value="<%=Model.SpareTelephone%>" class="input_reg" maxlength="20"/></td>
                </tr>
                <tr>
                  <td width="150" align="right">所在城市：</td>
                  <td>
                    <select id="selProvince" class="selcss">
                      <option value="">选择省份</option>
                    </select>
                    <select id="selCity" class="selcss ml10">
                      <option value="">选择城市</option>
                    </select>
                    <input id="hidProvince" name="hidProvince" type="hidden" value="<%=Model.ProvinceName%>" />
                    <input id="hidCity" name="hidCity" type="hidden" value="<%=Model.CityName%>" />
                  </td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                  <td><input type="submit" value="保存" class="btn_w98_green mt15 mb15" />&nbsp;&nbsp;<span id="err" class="ie_valign_5 ts" style="display:none;"></span></td>
                </tr>
              </table>
            </div>
          </form>
      </div>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/dialog_new.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jqRegExpMothod.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //网站域名
            var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
            var colProvince = $("#selProvince");
            var colCity = $("#selCity");
            var colProvinceName = $("#hidProvince");
            var colCityName = $("#hidCity");
            var colShowUpdateMobile = $("#aShowUpdateMobile");

            //获得省份信息
            function getProvince(ctid, pid) {
                ///<summary>获得省份信息</summary>
                ///<param name="ctid">国籍Id：默认1(中国)</param>
                ///<param name="pid">选中的省份Id：默认0</param>
                //异步查询数据
                $.ajax({
                    type: "post",
                    url: mainUrl + "premises/searchprovincelist",
                    data: { countryid: ctid },
                    success: function (data) {
                        $.each(data, function (i, item) {
                            var temp = pid == item.Id ? "selected=\"selected\"" : "";
                            $("<option " + temp + "></option>").val(item.Id).text(item.Name).appendTo(colProvince);
                        });
                    }
                });
            }
            //获得城市信息
            function getCity(pid, cid) {
                ///<summary>获得城市信息</summary>
                ///<param name="pid">省份Id</param>
                ///<param name="cid">选中的城市Id：默认0</param>
                //清空城市
                colCity.empty();
                $("<option></option>").val("").text("选择城市").appendTo(colCity);
                //异步查询数据
                $.ajax({
                    type: "post",
                    url: mainUrl + "premises/searchcitylist",
                    data: { ProvinceId: pid },
                    success: function (data) {
                        $.each(data, function (i, item) {
                            var temp = cid == item.Id ? "selected=\"selected\"" : "";
                            $("<option " + temp + "></option>").val(item.Id).text(item.Name).appendTo(colCity);
                        });
                    }
                });
            }

            //初始化
            $(colShowUpdateMobile).click(function () {
                //弹出修改手机
                Dialog.showDialog('url', '<%=TXCommons.GetConfig.BaseUrl%>home/updatemobile', '<%=TXCommons.GetConfig.ImgUrl%>images/loading.gif');
            });
            $(colProvince).change(function () {
                getCity($(this).val(), 0); //加载城市
                colProvinceName.val($(this).find("option:selected").text());
            });
            $(colCity).change(function () {
                colCityName.val($(this).find("option:selected").text());
            });
            //如有省份信息则赋值
            if (parseInt("<%=Model.ProvinceId%>") > 0) {
                getProvince(1, "<%=Model.ProvinceId%>"); //加载省份
            }
            else {
                getProvince(1, 0); //加载省份
            }
            //如有城市信息则赋值
            if (parseInt("<%=Model.CityId%>") > 0) {
                getCity("<%=Model.ProvinceId%>", "<%=Model.CityId%>");
            }
            //验证
            $("#frmUpdateUserInfo").validate({
                errorClass: "ie_valign_5 no",       //设置错误提示css
                errorElement: "span",               //设置错误提示容器
                //验证通过事件
                submitHandler: function(form) {
                    var err = $("#err");
                    err.removeClass();
                    err.addClass("ts");
                    err.html("保存中...<img src='<%=TXCommons.GetConfig.ImgUrl%>images/loading.gif' height='14px' width='14px' />");
                    err.show();
                    var email = $("#txtEmail").val();
                    var tele = $("#txtTelephone").val();
                    var spatele = $("#txtSpareTelephone").val();
                    var pid = colProvince.val();
                    var pname = colProvinceName.val();
                    var cid = colCity.val();
                    var cname = colCityName.val();
                    $.post(mainUrl + "home/doupdateuserinfo", { email: email, tel: tele, spatel: spatele, pid: pid, pname: pname, cid: cid, cname: cname, m: Math.random() }, function(data) {
                        if (data > 0) {
                            err.removeClass();
                            err.addClass("ok");
                            $("#err").text("保存成功");
                        } else {
                            $("#err").text("保存失败");
                        }
                    });
                },
                rules: {
                    txtEmail: {
                        email: true
                    },
                    txtTelephone: {
                        telephone: true
                    },
                    txtSpareTelephone: {
                        mobileortel: true
                    }
                },
                messages: {
                    txtEmail: {
                        email: "请输入正确的邮件格式"
                    },
                    txtTelephone: {
                        telephone: "请输入正确的电话格式，如：010-88888888-001 区号、分机可不填"
                    },
                    txtSpareTelephone: {
                        mobileortel: "请输入正确的电话或手机格式"
                    }
                }
            });
        });
    </script>
</asp:Content>
