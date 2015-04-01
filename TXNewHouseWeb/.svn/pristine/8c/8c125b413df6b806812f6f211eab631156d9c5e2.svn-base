<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="layer_box w300">
    <a style="cursor: pointer" class="close"></a>
    <div class="layer_title">
        模板命名</div>
    <div class="layer_content">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1">
            <tbody>
                <tr>
                    <td align="right">
                    </td>
                    <td>
                        <span id="error"></span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        模板名称：
                    </td>
                    <td>
                        <input id="Title" name="Title" type="text" class="input_wauto w180" onkeyup="yes()"
                            onmousedown="yes()" maxlength="25" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        模板备注：
                    </td>
                    <td>
                        <input id="Remark" name="Remark" type="text" class="input_wauto w180" onkeyup="yes()"
                            onmousedown="yes()" maxlength="25" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <input id="OkTemplate" type="button" value="确定" class="btn_w97_green" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        $("#div_dia .close").bind("click", function () {
            $("#div_dia").hide();
        });
        var PreName = $("#PremisesId").find("option:selected").text() + " " + $("#BuildingId").find("option:selected").text() +
        $("#UnitId").find("option:selected").text() + " " + $("#Room").val() + "室" + $("#Hall").val() + "厅" + $("#Toilet").val() + "卫" + $("#Kitchen").val() + "厨";
        $("#Title").val(PreName);
        $("#Remark").val($("#RemarkTemplate").val());

        $("#OkTemplate").bind("click", function () {
            var title = $.trim($("#Title").val());
            if (title == "") {
                $("#error").html("<span class=\"ie_valign_5 no\">请输入模版名称</span>");
                return false;
            }
            if (!/^[\u4e00-\u9fa5a-zA-Z0-9\s]+$/.test(title)) {
                $("#error").html("<span class=\"ie_valign_5 no\">名称只能包括中文字、英文字母、数字</span>");
                return false;
            }
            var remark = $.trim($("#Remark").val());
            if (remark && remark != '' && (!/^[\u4e00-\u9fa5a-zA-Z0-9\s]+$/.test(remark))) {
                $("#error").html("<span class=\"ie_valign_5 no\">备注只能包括中文字、英文字母、数字</span>");
                return false;
            }
            $("#TitleTemplate").val(title);
            $("#RemarkTemplate").val(remark);

            $("#div_dia").hide();

        });
    });

    function yes() {
        $("#error").html("");
    }
</script>
