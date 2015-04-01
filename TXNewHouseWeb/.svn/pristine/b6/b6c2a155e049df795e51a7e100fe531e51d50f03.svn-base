<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-申请摇号活动
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <h4 class="title_h4 mb10">
            <span>申请摇号活动</span><em class="ts_right"><a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/YaoHaoList">返回列表
            </a></em>
        </h4>
        <%using (Html.BeginForm("AddYaoHao", "Activity", FormMethod.Post))
          {%>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1">
            <tr>
                <th scope="row" width="100" align="right">
                    <span class="red">*</span>摇号活动名称：
                </th>
                <td>
                    <input type="text" id="name" name="name" class="input_wauto w200" /><span style="display: none;"
                        class="ie_valign_5 no" id="error1"></span>
                </td>
            </tr>
            <tr>
                <th scope="row" width="130" align="right">
                    <span class="red">*</span>选择楼盘或者楼栋：
                </th>
                <td>
                    <label class="mr35">
                        <input id="radio_loupan" type="radio" name="a" class="valign2 mr5" />整个楼盘</label><label><input
                            id="radio_loudong" type="radio" name="a" class="valign2 mr5" />楼栋</label>
                </td>
            </tr>
            <tr>
                <th scope="row" width="100" align="right">
                    &nbsp;
                </th>
                <td>
                    <span class="red">*</span>楼盘：
                    <select class="select1 w150" id="s_loupan" name="s_loupan">
                        <option value="0">选择</option>
                        <%if (ViewData["Premises"] != null)
                          {
                              foreach (TXOrm.Premis item in ViewData["Premises"] as List<TXOrm.Premis>)
                              {%>
                        <option value="<%=item.Id %>">
                            <%=item.Name%></option>
                        <%}
                          } %>
                    </select><span style="display: none;" class="ie_valign_5 no" id="error2"></span>
                </td>
            </tr>
            <tr id="tr_loudong" style="display: none;">
                <th scope="row" width="100" align="right">
                    &nbsp;
                </th>
                <td id="td_loudong">
                </td>
            </tr>
            <tr>
                <th scope="row" width="100" align="right">
                    &nbsp;
                </th>
                <td>
                    <input type="submit" value="申请创建摇号活动" id="submit" onclick="return save();" class="bnt_w123 mt20" />
                </td>
            </tr>
        </table>
        <input type="hidden" id="hid_loudong" name="hid_loudong" value="" />
        <input type="hidden" id="hid_IsAllPremises" name="hid_IsAllPremises" value="1" />
        <input type="hidden" id="hid" name="hid_check" value="0" />
        <%} %>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#radio_loupan").attr("checked", true);
            $("#radio_loupan").click(function () {
                $("#radio_loupan").attr("checked", true);
                $("#radio_loudong").attr("checked", false);
                $("#s_loupan").val(0);
                $("#hid_IsAllPremises").val(1)
                $("#td_loudong").html("");
                $("#tr_loudong").hide();
            })
            $("#radio_loudong").click(function () {
                $("#radio_loudong").attr("checked", true);
                $("#radio_loupan").attr("checked", false);
                $("#s_loupan").val(0);
                $("#hid_IsAllPremises").val(0)
                $("#td_loudong").html("");
                $("#tr_loudong").show();
            })
            $("#s_loupan").change(function () {
                if ($("#radio_loudong").attr("checked") == "checked") {
                    $("#td_loudong").html("");
                    if ($(this).val() != 0) {
                        var premisesId = $(this).val();
                        $.getJSON("<%=TXCommons.GetConfig.BaseUrl%>Activity/GetBuildingByPremisesId", { premisesId: premisesId }, function (data) {
                            if (data != null) {
                                var str = "<span class=\"red\">*</span>楼栋：";
                                for (var i = 0; i < data.length; i++) {
                                    str += " <label class=\"mr35\"><input type=\"checkbox\"  class=\"valign2 mr5\" onclick=\"ischecked(this)\"  value=\"" + data[i].Id + "\"/>" + data[i].Name + "</label>";
                                }
                                str += "<span style=\"display: none;\" class=\"ie_valign_5 no\" id=\"error3\"></span>";
                                $("#td_loudong").html(str);
                            }
                            else {
                                $("#td_loudong").html("");
                            }
                        })
                    }
                }
            })
            $("#name").keyup(function () {
                $("#error1").hide();
            })
            $("#s_loupan").change(function () {
                $("#error2").hide();
            })
        })
        function ischecked(obj) {
            if ($(obj).attr("checked", "checked")) {
                $("#error3").hide();
                $.get("<%=TXCommons.GetConfig.BaseUrl%>Activity/CheckYaohao", { premisesId: 0, buildingId: $(obj).val() }, function (data) {
                    if (data == "1") {
                        alert("该楼栋下已有房源参与摇号！");
                        $("#hid_check").val(1);
                        return false;
                    }
                })
            }
            else {
                $("#hid_check").val(0);
                $("#error3").show();
            }
        }
        //(楼盘)楼栋下是否有房源参与了摇号
        function CheckYaohao() {
            var premisesId = $("#s_loupan").val();
            $.get("<%=TXCommons.GetConfig.BaseUrl%>Activity/CheckYaohao", { premisesId: premisesId }, function (data) {
                if (data == "1") {
                    alert("该楼盘下所有楼栋已有房源参与摇号！");
                    return false;
                }
            })

        }
        //保存
        function save() {
            if ($.trim($("#name").val()) == "") {
                $("#error1").html("请填写活动名称！");
                $("#error1").show();
                return false;
            }
            if ($("#name").val().length > 15) {
                $("#error1").html("活动名称长度必须小于15字！");
                $("#error1").show();
                return false;
            }
            if ($("#s_loupan").val() == 0) {
                $("#error2").html("请选择楼盘！");
                $("#error2").show();
                return false;
            }
            if ($("#radio_loupan").attr("checked") != null) {
                CheckYaohao();
                $("#hid_loudong").val("全部");
                return true;
                $("#submit").attr("disabled", true);
            }
            else if ($("#radio_loudong").attr("checked") != null) {
                var buildids = " ";
                $("#td_loudong").find("input").each(function () {
                    if (this.checked == true) {
                        buildids += $(this).val() + ",";
                    }
                })
                if (buildids == " ") {
                    $("#error3").html("请选择楼栋！");
                    $("#error3").show();
                    return false;
                } else {
                    if ($("#hid_check").val() == 1) {
                        alert("楼栋已有房源参与摇号！");
                        return false;
                    }
                    $("#hid_loudong").val($.trim(buildids.toString().substring(0, buildids.length - 1)));
                    return true;
                }
            }
        }
    </script>
</asp:Content>
