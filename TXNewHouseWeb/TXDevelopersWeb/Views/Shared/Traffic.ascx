<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<TXModel.Geography.Z_Traffic>>" %>
<div class="layer_box w560">
    <a style="cursor: pointer" class="close" opertype="dialog_close"></a>
    <div class="layer_title" opertype="dialog_title" style="cursor: move">
        设置周边地铁</div>
    <div class="layer_content">
        <div class="pt10 pl10 pb10 pr10">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab_3">
                <tbody>
                    <tr>
                        <th width="9%">
                            线路：
                        </th>
                        <td width="91%">
                            <%if (Model != null && Model.Count > 0)
                              {
                            %>
                            <div id="traffic_lines" class="lines">
                                <%-- <a href="javascript:void(0);" data="1_1号线">1号线</a><a href="javascript:void(0);" data="2_2号线">2号线</a><a href="javascript:void(0);" data="3_4号线">4号线</a><a href="javascript:void(0);" data="4_5号线">5号线</a><a href="javascript:void(0);" data="5_8号线">8号线</a><a href="javascript:void(0);" data="6_9号线">9号线</a><a href="javascript:void(0);" data="7_10号线">10号线</a><a href="javascript:void(0);"  data="8_13号线">13号线</a><a href="javascript:void(0);" data="9_八通线">八通线</a><a href="javascript:void(0);"  data="10_机场专线">机场专线</a><a href="javascript:void(0);"  data="11_房山线">房山线</a><a href="javascript:void(0);"  data="12_大兴线">大兴线</a><a href="javascript:void(0);" data="13_亦庄线">亦庄线</a><a href="javascript:void(0);" data="14_昌平线">昌平线</a>--%>
                                <%foreach (var m in Model)
                                  { %>
                                <a class="mr10" href="javascript:void(0);" data="<%=m.TId %>_<%=m.Name %>">
                                    <%=m.Name%></a>
                                <%} %>
                            </div>
                            <%}
                              else
                              { %>
                            <div id="Div1" class="lines">
                                该城市无地铁数据！
                            </div>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <th valign="top">
                            站名：
                        </th>
                        <td>
                            <div class="stat">
                                <div id="traffic_liner" class="liner">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="yeltisbox">
                                <span>&nbsp;</span>请注意。同一小区最多可以添加10个站名</div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <ul id="ul_traffic_info">
                            </ul>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="layer_btn">
            <input type="button" id="ok_traffic" class="btn_yel_w70 mr20" value="确定" />
            <input type="button" class="btn_grey_w70" value="取消" opertype="dialog_close" />
        </div>
    </div>
</div>
<script type="text/javascript">
    var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";


    $(document).ready(function () {
        //加载数据
        traffic_subway.selected_stations = traffic_subway.cloneData(traffic_subway.new_stations);
        traffic_subway.displaySelectedStation();


        $("#div_dia .close").bind("click", function () {
            $("#div_dia").hide();
        });

        $("#div_dia .btn_grey_w70").bind("click", function () {
            $("#div_dia").hide();
        });

        $("#traffic_lines a").bind("click", function () {
            var datas = $(this).attr("data").split('_');
            $("#traffic_lines a").removeClass("orange bold");
            $(this).addClass("orange bold");
            loadTrafficItems(mainUrl + 'Premises/GetTrafficByLine?lineId=' + datas[0]);
        });

        $("#ok_traffic").bind("click", function () {
            if (traffic_subway.selected_stations == "") {
                alert("请选择站名");
                return false;
            }

            traffic_subway.btn_Enter();

            $("#div_dia").hide();

        });

    });

    var loadTrafficItems = function (url, onComplete) {
        $.getJSON(url, function (response) {
            if (response && response.success) {
                fillTrafficItems(response.items);
            }
        });
    };
    var fillTrafficItems = function (dataItems) {
        if (dataItems) {
            var strHtml = "";
            $.each(dataItems, function () {
                strHtml += "<a id=\"station_" + this.TId + "\"  onclick=\"traffic_subway.selectedStation('" + this.TId + "','" + this.Name + "')\" href='javascript:void(0);' data='" + this.TId + "_" + this.Name + "'>" + this.Name + "</a>";
            });
            $("#traffic_liner").html(strHtml);

            traffic_subway.initSelectedStation();
        }
    };
</script>
