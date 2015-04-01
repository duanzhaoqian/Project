<%@ Control Language="C#" Inherits="WebViewUserControl<TXModel.Geography.Z_GeographyCommon>" %>
<!--弹出层-->
<div class="w_layer w560" style="border-width: 0;">
    <div class="con">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="right" valign="top">
                    <em>线路</em>：
                </td>
                <td>
                    <%if (Model != null && Model.GeographyItems != null && Model.GeographyItems.Count > 0)
                      {
                    %>
                    <div id="traffic_lines" class="lines">
                        <%foreach (var m in Model.GeographyItems)
                          { %>
                        <a href="javascript:void(0);" id="line_<%= m.GeographyCode %>" onclick="traffic_subway.displayStations('<%=m.GeographyCode %>')">
                            <%=m.GeographyName %></a>
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
                <td width="7%" align="right" valign="top">
                    <em>站名</em>：
                </td>
                <td width="93%">
                    <div id="traffic_liner" class="liner">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="pub_tipsB w500">
                        请注意，同一楼盘最多可以添加10个站名。</div>
                    <ul id="ul_traffic_info" class="tbUls">
                    </ul>
                </td>
            </tr>
        </table>
    </div>
</div>