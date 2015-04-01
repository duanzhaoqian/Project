<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="layer_box w300">
    <a style="cursor: pointer" class="close" opertype="dialog_close"></a>
    <div class="layer_title">
        更改销售状态</div>
    <div class="layer_content">
        <div class="pt15 pl10 pb10 pr10">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab_3">
                <tbody>
                    <tr>
                        <th width="37%">
                            销售状态：
                        </th>
                        <td width="63%">
                            <select class="selcss w100" id="HouseSalesStatus" name="HouseSalesStatus">
                                <option value="0">待售</option>
                                <option value="1">开发商保留</option>
                                <option value="2">在售</option>
                                <option value="3">已认购</option>
                                <option value="4">已签约</option>
                                <option value="5">已备案</option>
                                <option value="6">已办产权</option>
                                <option value="7">被限制</option>
                                <option value="8">拆迁安置</option>
                                <option value="9">售罄</option>
                            </select>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="layer_btn">
            <input id="btnSave" type="button" class="btn_yel_w70 mr20" value="确定" /><input type="button"
                class="btn_grey_w70" opertype="dialog_close" value="取消" /></div>
    </div>
</div>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $("#btnSave").bind("click", function () {
            var vals = "";
            $("input[name='chkItem']:checkbox:checked").each(function (i) {
                vals += (i == 0 ? $(this).val() : "," + $(this).val());
            });

            if (vals == "") {
                alert("请选择要更改的房源");
                return false;
            }
            else {
                $.ajax({
                    url: mainUrl + "NHouse/UpdateHouseSalesStatusByIds",
                    type: "post",
                    cache: false,
                    data: { Ids: vals, state: $("#HouseSalesStatus").val() },
                    success: function (data) {
                        if (data) {
                            alert(data.message);
                            location.reload();
                        }
                    }
                });

            }
        });
    });
</script>
