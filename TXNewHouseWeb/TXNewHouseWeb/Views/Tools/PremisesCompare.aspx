<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<List<TXModel.Web.PremisesCompare>>" %>

<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .li_same { color: Red; }
    </style>
    <div class="clearfix">
        <div class="bgcolor">
            <div class="w1190 line_h50 font14 col666">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                    class="blue">北京新房</a> > 楼盘对比
            </div>
        </div>
        <div class="clearfix loubox brt2">
            <ul>
                <li class="w126">
                    <ul>
                        <li class="font14 col333 w105 nobor_r bkcol_f9f9f9"><strong>楼盘工具PK</strong></li>
                        <li class="w105 col333 font14 h195"><strong>楼盘</strong></li>
                        <li class="w105 font14 col333 bkcol_f9f9f9"><strong>基本信息</strong></li>
                        <li class="w105">参考价格</li>
                        <li class="w105">区域/板块</li>
                        <li class="w105">环线位置</li>
                        <li class="w105">所属商圈</li>
                        <li class="w105 h44">项目地址</li>
                        <li class="w105">开发商</li>
                        <li class="w105 font14 col333 bkcol_f9f9f9"><strong>建筑信息</strong></li>
                        <li class="w105">产权</li>
                        <li class="w105">建筑面积</li>
                        <li class="w105">占地面积</li>
                        <li class="w105">总户数</li>
                        <li class="w105">建筑类别</li>
                        <li class="w105 h44">项目特色</li>
                        <li class="w105 font14 col333 bkcol_f9f9f9"><strong>物业类型</strong></li>
                        <li class="w105">物业类型</li>
                        <li class="w105">容积率</li>
                        <li class="w105">物业费</li>
                        <li class="w105">车位信息</li>
                        <li class="w105">绿化率</li>
                        <li class="w105 font14 col333 bkcol_f9f9f9"><strong>交通配套</strong></li>
                        <li class="w105 h170">配套信息</li>
                        <li class="w105 h170">学校</li>
                        <li class="w105 h170">购物</li>
                        <li class="w105 h170">医院</li>
                        <li class="w105 h170">生活</li>
                        <li class="w105 h170">娱乐</li>
                        <li class="w105 h170">餐饮</li>
                        <li class="w105 h170">公交</li>
                        <li class="w105 h170">地铁</li>
                    </ul>
                </li>
                <%string htmlPart = "<li class='h195 pos_rel'><div class='bold'></div><div class='loupho'><img src='' width='170' height='130' alt='' /></div><div class='pos_rel'><input type='button' class='btn_del btn_yhui mr20' value='删除' style='display:none;' /><input type='button' class='btn_change btn_yhui' value='换楼盘'/><div class='lppk_yebox' style='display: none;'><div class='close'>&nbsp;</div><input type='text' class='inp_bgre' value='请输入楼盘名称' onfocus='storeonblur(this);'  onblur='storeonblur(this)'/><input type='button' class='inp_bgre_btn' style='cursor:pointer' value='' /></div></div></li><li class='nobor_r bkcol_f9f9f9'>&nbsp;</li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='w209 same_li'></li><li class='h44 same_li'></li><li class='same_li'></li><li class='nobor_r bkcol_f9f9f9'>&nbsp;</li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='h44 same_li'></li><li class='nobor_r bkcol_f9f9f9'>&nbsp;</li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='bkcol_f9f9f9'>&nbsp;</li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li></ul></li>";%>
                <%if (Model != null && Model.Count > 0)
                  {
                      int index = 0;
                      foreach (var item in Model)
                      {
                          index++;
                %>
                <li class="w266">
                    <ul class="ul_list">
                        <%if (index == 4)
                          { %>
                        <li class="bkcol_f9f9f9 cb_same">
                            <input id="input_cb" type="checkbox" class="btn_checkbox" /><label for="input_cb">灰色显示相同项目</label></li>
                        <%}
                          else
                          { %>
                        <li class="nobor_r bkcol_f9f9f9 nobor_r">&nbsp;</li>
                        <%} %>
                        <li class="h195 pos_rel">
                            <div class="bold">
                                <%=item.Name %></div>
                            <div class="loupho">
                                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/img_w168_h129.jpg" width="170" height="130"
                                    alt="" /></div>
                            <div class="pos_rel">
                                <input type="button" class="btn_del btn_yhui mr20" value="删除" /><input type="button"
                                    class="btn_change btn_yhui" value="换楼盘" />
                                <div class="lppk_yebox" style="display: none;">
                                    <div class="close">
                                        &nbsp;</div>
                                    <input type="text" class="inp_bgre" value="请输入楼盘名称" onfocus="storeonblur(this);"
                                        onblur="storeonblur(this)" /><input type="button" class="inp_bgre_btn" style="cursor: pointer"
                                            value="" />
                                </div>
                            </div>
                        </li>
                        <li class="nobor_r bkcol_f9f9f9">&nbsp;</li>
                        <li class="same_li">均价<%=Convert.ToDouble(item.ReferencePrice)%>元/平方米</li>
                        <li class="same_li">
                            <%=item.DName%></li>
                        <li class="same_li">
                            <%=item.Ring%>环</li>
                        <li class="w209 same_li">
                            <%=item.BName%></li>
                        <li class="h44 same_li">
                            <%=item.PremisesAddress%></li>
                        <li class="same_li">
                            <%=item.Developer%></li>
                        <li class="nobor_r bkcol_f9f9f9">&nbsp;</li>
                        <li class="same_li">
                            <%=item.PropertyRight%>年</li>
                        <li class="same_li">
                            <%=item.BuildingArea%>平方米</li>
                        <li class="same_li">
                            <%=item.Area%>平方米</li>
                        <li class="same_li">
                            <%=item.UserCount%></li>
                        <li class="same_li">
                            <%=TXCommons.NewHouseWeb.HouseType.GetBuildingType(item.BuildingType)%></li>
                        <li class="h44 same_li">
                            <%=item.Characteristic%></li>
                        <li class="nobor_r bkcol_f9f9f9">&nbsp;</li>
                        <li class="same_li">
                            <%=TXCommons.NewHouseWeb.BuildingType.GetPropertyType(item.PropertyType)%></li>
                        <li class="same_li">
                            <%=item.AreaRatio%></li>
                        <li class="same_li">
                            <%=Convert.ToDouble(item.PropertyPrice)%>元/平/月</li>
                        <li class="same_li">
                            <%=item.ParkingLot%></li>
                        <li class="same_li">
                            <%=item.GreeningRate%>%</li>
                        <li class="bkcol_f9f9f9">&nbsp;</li>
                        <li class="h170">
                            <%=item.SupportingIntroduction%></li>
                        <li class="h170">
                            <%=item.School%></li>
                        <li class="h170">
                            <%=item.Shopping%></li>
                        <li class="h170">
                            <%=item.Hospital%></li>
                        <li class="h170">
                            <%=item.Life%></li>
                        <li class="h170">
                            <%=item.Entertainment%></li>
                        <li class="h170">
                            <%=item.Catering%></li>
                        <li class="h170">
                            <%=item.Bus%></li>
                        <li class="h170">
                            <%=item.Subway%></li>
                    </ul>
                </li>
                <%if (index == 4) { break; }
                      }
                      if (index < 4)
                      {
                          for (int i = 0; i < 4 - index; i++)
                          {
                %>
                <li class="w266">
                    <ul class="ul_list">
                        <%if (i + index + 1 == 4)
                          { %><li class='bkcol_f9f9f9 cb_same'>
                              <input id="input_cb" type='checkbox' class='btn_checkbox' /><label for='input_cb'>灰色显示相同项目</label></li><%}
                          else
                          { %><li class="nobor_r bkcol_f9f9f9 nobor_r">&nbsp;</li><%} %><%=htmlPart %>
                        <%}
                      }
                  }
                  else
                  {
                      for (int i = 0; i < 4; i++)
                      {%>
                        <li class="w266">
                            <ul class="ul_list">
                                <%if (i == 3)
                                  { %><li class='bkcol_f9f9f9 cb_same'>
                                      <input id="input_cb" type='checkbox' class='btn_checkbox' /><label for='input_cb'>灰色显示相同项目</label></li><%}
                                  else
                                  { %><li class="nobor_r bkcol_f9f9f9 nobor_r">&nbsp;</li><%} %><%=htmlPart %>
                                <%}
                  } %>
                            </ul>
                            <div class="clear">
                            </div>
        </div>
    </div>
    <link href="<%=TXCommons.GetConfig.GetFileUrl("js/jq_autocomplete/jquery.autocomplete.css")%>"
        rel="stylesheet" type="text/css" />
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jq_autocomplete/jquery.autocomplete.min.js")%>"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/changePremise.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        function storeonblur(sender) {
            Release.auto("<%=TXCommons.GetConfig.GetSiteRoot%>/tools/SearchPremisesByName", sender);
        }

        //对比项元素比较
        function CompareItems(array) {
            if (array.length > 1) {
                for (var i = 0; i < array.length; i++) {
                    for (var j = i + 1; j < array.length; j++) {
                        if ($.trim(array[i]) != $.trim(array[j])) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //设置相同对比项样式
        function SetSameCompareItems(cbx) {
            $(".ul_list li").removeClass("bkcol_efefef");
            var ul = $(".ul_list");
            if (ul.length > 1) {
                if ($(cbx).attr("checked")) {
                    var arr_txt = new Array();
                    var arr_color = new Array();
                    var liLength = ul.eq(0).find("li[class*='same_li']").length;
                    for (var i = 0; i < liLength; i++) {
                        for (var j = 0; j < ul.length; j++) {
                            if (!isNullOrEmptyOrUndefined($(ul[j]).children("li").children(".bold").html())) {
                                var item = $(ul[j]).find("li[class*='same_li']").eq(i);
                                arr_txt.push(item.html());
                                arr_color.push(item);
                            }
                        }
                        if (arr_txt.length <= 1) {
                            return;
                        }
                        else if (CompareItems(arr_txt)) {
                            for (var index = 0; index < arr_color.length; index++) {
                                arr_color[index].addClass("bkcol_efefef");
                            }
                        }
                        arr_txt.length = 0;
                        arr_color.length = 0;
                    }
                }
            }
        }

        //删除对比项
        function DelCompareItem(btn_del) {
            var cb_ctl = $("#input_cb");
            $(btn_del).parents("li").remove();
            var cb_same = $(".cb_same");
            cb_same.closest("ul").prepend("<li class='nobor_r bkcol_f9f9f9 nobor_r'>&nbsp;</li>");
            cb_same.remove();
            $(".loubox").children("ul").append("<li class='w266'><ul class='ul_list'><li class='bkcol_f9f9f9 cb_same'><input id='input_cb' type='checkbox' class='btn_checkbox'/><label for='input_cb'>灰色显示相同项目</label></li><li class='h195 pos_rel'><div class='bold'></div><div class='loupho'><img src='' width='170' height='130' alt='' /></div><div class='pos_rel'><input type='button' class='btn_del btn_yhui mr20' value='删除' style='display:none;' /><input type='button' class='btn_change btn_yhui' value='换楼盘'/><div class='lppk_yebox' style='display: none;'><div class='close'>&nbsp;</div><input type='text' class='inp_bgre' value='请输入楼盘名称' onfocus='storeonblur(this);'  onblur='storeonblur(this)'/><input type='button' class='inp_bgre_btn' style='cursor:pointer' value='' /></div></div></li><li class='nobor_r bkcol_f9f9f9'>&nbsp;</li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='w209 same_li'></li><li class='h44 same_li'></li><li class='same_li'></li><li class='nobor_r bkcol_f9f9f9'>&nbsp;</li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='h44 same_li'></li><li class='nobor_r bkcol_f9f9f9'>&nbsp;</li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='same_li'></li><li class='bkcol_f9f9f9'>&nbsp;</li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li><li class='h170'></li></ul></li>");
            if (cb_ctl.attr("checked")) {
                $("#input_cb").trigger("click");
            }
        }

        $(function () {
            //设置相同对比项
            $("#input_cb").live("click", function () {
               
                if ($(this).attr("checked")) { 
                    SetSameCompareItems(this);
                }
                else {
                    $(".ul_list li").removeClass("bkcol_efefef");
                }
            });

            //删除对比项
            $(".btn_del").live("click", function () {
                DelCompareItem(this);
                SetSameCompareItems($("#input_cb"));
                $(this).hide();
            });

            //换楼盘
            $(".btn_change").live("click", function () {
                $(".lppk_yebox").hide();
                $(this).siblings(".lppk_yebox").show();
            });

            //关闭换楼盘弹出层
            $(".close").live("click", function () {
                $(".lppk_yebox").hide();
                $(".inp_bgre").val("请输入楼盘名称");
            });
            $("body").click(function (evt) {
                var target = $(evt.target);
                if (!target.is(".lppk_yebox") && !target.is(".btn_change") && !target.is(".inp_bgre") && !target.is(".inp_bgre_btn")) {
                    $(".lppk_yebox").hide();
                    $(".inp_bgre").val("请输入楼盘名称");
                }
            });

            //楼盘输入框得到、失去焦点事件
            $(".inp_bgre").live("focus", function () {
                if ($(this).val() == "请输入楼盘名称") {
                    $(this).val("");
                }
            }).live("blur", function () {
                if ($(this).val() == "") {
                    $(this).val("请输入楼盘名称");
                }
            });

            //更换对比项
            $(".inp_bgre_btn").live("click", function () {
                var thisChange = $(this);
                var thisUl = thisChange.closest("ul");
                var title = $(".bold");
                var premisesName = $.trim(thisChange.siblings(".inp_bgre").val());
                if (isNullOrEmptyOrUndefined(premisesName) || premisesName == "请输入楼盘名称") { alert("请输入楼盘名称"); return; }
                for (var i = 0; i < title.length; i++) {
                    if (premisesName == $.trim($(title[i]).html())) {
                        alert("已存在该楼盘");
                        return;
                    }
                }
                $.ajax({
                    url: globalvar.siteRoot + "/Tools/GetPremisesByName/",
                    type: "post",
                    dataType: "json",
                    data: { premisesName: premisesName },
                    success: function (jsonData) {
                        if (jsonData != "") {
                            $(".lppk_yebox").hide();
                            var result = eval(jsonData);
                            thisUl.children("li:eq(1)").children(".bold").html(result["Name"]);
                            thisUl.children("li:eq(1)").find(".loupho img").attr("src", "<%=TXCommons.GetConfig.ImgUrl%>images/img_w168_h129.jpg");
                            thisUl.children("li:eq(3)").html("均价" + result["ReferencePrice"] + "元/平方米");
                            thisUl.children("li:eq(4)").html(result["DName"]);
                            thisUl.children("li:eq(5)").html(result["Ring"] + "环");
                            thisUl.children("li:eq(6)").html(result["BName"]);
                            thisUl.children("li:eq(7)").html(result["PremisesAddress"]);
                            thisUl.children("li:eq(8)").html(result["Developer"]);
                            thisUl.children("li:eq(10)").html(result["PropertyRight"] + "年");
                            thisUl.children("li:eq(11)").html(result["BuildingArea"] + "平方米");
                            thisUl.children("li:eq(12)").html(result["Area"] + "平方米");
                            thisUl.children("li:eq(13)").html(result["UserCount"]);
                            thisUl.children("li:eq(14)").html(result["View_BuildingType"]);
                            thisUl.children("li:eq(15)").html(result["Characteristic"]);
                            thisUl.children("li:eq(17)").html(result["PropertyType"]);
                            thisUl.children("li:eq(18)").html(result["AreaRatio"]);
                            thisUl.children("li:eq(19)").html(result["PropertyPrice"] + "元/平/月");
                            thisUl.children("li:eq(20)").html(result["ParkingLot"]);
                            thisUl.children("li:eq(21)").html(result["GreeningRate"] + "%");
                            thisUl.children("li:eq(23)").html(result["SupportingIntroduction"]);
                            thisUl.children("li:eq(16)").html(result["School"]);
                            thisUl.children("li:eq(16)").html(result["Shopping"]);
                            thisUl.children("li:eq(16)").html(result["Hospital"]);
                            thisUl.children("li:eq(16)").html(result["Life"]);
                            thisUl.children("li:eq(16)").html(result["Catering"]);
                            thisUl.children("li:eq(16)").html(result["Bus"]);
                            thisUl.children("li:eq(16)").html(result["Subway"]);
                            //调换位置
                            var ul = $(".ul_list");
                            for (var i = 0; i < ul.length; i++) {
                                for (var j = i + 1; j < ul.length; j++) {
                                    var left = $.trim($(ul[i]).children("li").children(".bold").html());
                                    var right = $.trim($(ul[j]).children("li").children(".bold").html());
                                    if (left == "" && right != "") {
                                        DelCompareItem($(ul[i]).find(".btn_del"));
                                    }
                                }
                            }
                            SetSameCompareItems($("#input_cb"));

                            //显示删除对比按钮
                            thisChange.parent().parent().find('.btn_del').show();
                        } else {
                            alert("没有该楼盘");
                        }
                    }
                });
            });
        });

        //自动填充
        (function ($) {
            $.fn.AutoComplete = function (settings) {
                settings = $.extend({}, $.fn.AutoComplete.defautSetting, settings || {});
                settings.dataUrl = settings.dataUrl;
                var elem = $(this);
                $(this).autocomplete(settings.dataUrl, {
                    //width: elem.parent()[0].scrollWidth,
                    width: 152,
                    max: 10, //显示数据条数
                    minChars: 1, //在触发autoComplete前用户至少需要输入的字符数 默认为0 在输入框内双击或者删除输入框内内容时显示列表
                    matchCase: false,
                    matchContains: "word", //只要包含输入字符就会显示提示
                    autoFill: false, //自动填充输入框  
                    scroll: false, //不显示滚动条
                    dataType: "jsonp",
                    extraParams: {
                        keyword: function () { return elem.val(); },
                        cid: 1,
                        //cityid: function () { return "" },
                        pagesize: 10,
                        returntype: 'json',
                        typeid: function () { return 0 }
                    },
                    parse: function (data) {
                        var rows = [];
                        for (var i = 0; i < data.length; i++) {
                            rows[rows.length] = {
                                data: data[i], //每条数据对象 
                                value: $.trim(data[i].Name), //与输入的值比较的数据
                                result: $.trim(data[i].Name)  //选中的实际数据
                            };
                        }
                        return rows;
                    },
                    formatItem: function (row, i, max) {
                        return "<span class='ac_name' lang='" + row.Id + "'>" + row.Name + "</span>";
                    },
                    formatMatch: function (row, i, max) {
                        return row.Name + " " + row.Id;
                    },
                    formatResult: function (row) {
                        return $.trim(row.Id);
                    }
                }).result(function (event, data, formatted) {
                    if (settings.isAddAtribute) {
                        elem.attr("lang", settings.type == "city" ? data.Id : data.Name);
                        elem.attr("data", settings.type == "city" ? data.pyin : data.Name);
                    }
                });
            }
            $.fn.AutoComplete.defautSetting = {
                dataUrl: "",
                type: "city",
                isAddAtribute: false
            }
        })(jQuery);

        //自动填充
        //$(".inp_bgre").AutoComplete({ dataUrl: "需要相关接口地址", type: "sq", isAddAtribute: true });
    </script>
</asp:Content>
