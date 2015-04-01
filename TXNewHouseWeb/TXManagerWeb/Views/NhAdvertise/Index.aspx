<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_HN_ADvertise>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%=Auxiliary.Instance.NhWebThemeUrl("js/jquery.form.js") %>" language="javascript"
        type="text/javascript"></script>
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a></span>
    </div>
    <!--//current-->
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <% using (Html.BeginForm("Index", "NhAdvertise", FormMethod.Post, new { Id = "searchForm", Name = "searchForm" }))
           {%>
        <div class="filterBar">
            城市：<%=Html.DropDownListFor(model => model.ProvinceID, Model.Provinces, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.CityId, Model.Cityes, new { @class = "w100" })%>
            选择位置：<%=Html.DropDownListFor(model => model.Type, Model.Types, new { @class = "w100" })%>
            <input type="submit" value="" class="btn01" />
        </div>
        <%}
        %>
        <div class="filterBar hidediv" style="padding-top: 0; display: none;">
            <%--<%=Html.DropDownListFor(model => model.PremisesId, Model.Premises, new {@class = "w200"}) %>--%>添加楼盘:
            <%=Html.TextBox("PremisesName", "", new { autocomplete = "on" })%>
            <%=Html.Hidden("PremisesId") %><input type="button" value="添加" onclick="AddNewAd()"
                disabled="disabled" id="addbt" />
            <span style="display: none">最多可添加<span id="maxnum">4</span>个楼盘</span>
        </div>
        <div class="filterBar hidediv" style="padding-top: 0; display: none;">
            <%--<%=Html.DropDownListFor(model => model.PremisesId, Model.Premises, new {@class = "w200"}) %>--%>批量设置推广时间:
            <input type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'AllEndDate\',{d:-0});}'})"
                id="AllBeginDate" readonly="readonly" />
            --
            <input type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'AllBeginDate\',{d:0});}'})"
                id="AllEndDate" readonly="readonly" /><input type="button" value="设置" onclick="AllSetDate()" />
        </div>
        <div class="clearFix">
            <div id="ListResult">
            </div>
        </div>
        <div style="text-align: center; display: none" class="hidediv">
            <input type="button" value="保存" id="savebt" /></div>
        <%--  <div class="paging">
            <!-- 分页 -->
            <p id="list_page" class="pubPage" style="border: none 0">
                <!-- 这里显示分页 -->
            </p>
        </div>--%>
    </div>
    <script type="text/javascript">
        ///搜索
        var provincesearchid = "<%=Model.ProvinceID %>";
        var citysearchid = "<%=Model.CityId %>";
        var positionsearch = "<%=Model.Type %>";
        var maxNum = $("#maxnum");
        function disabledAdd() {
            $("#addbt").attr("disabled", true);
            provincesearchid = "0";
            citysearchid = "0";
            positionsearch = "0";
            $("#PremisesName").val("");
            $("#PremisesId").val("");
            $("#ListResult").empty();
            $(".hidediv").css("display", "none");
        }

        $(function () {
            var selectProvinces = $("#ProvinceID");
            var selectCities = $("#CityId");
            $(selectProvinces).change(function () {
                clearListItems(selectCities);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });
            $("#ProvinceID").change(disabledAdd);
            $("#CityId").change(disabledAdd);
            $("#Type").change(function () {
                var val = $.trim($("#Type").val());

                disabledAdd();
                if (val != "0" && val.length > 0) {
                    maxNum.parent("span").css("display", "inline");
                    if (val == "4") { //即将开盘有8个的限制
                        maxNum.html("8");
                    }
                    else {
                        maxNum.html("4");
                    }
                } else {
                    maxNum.parent("span").css("display", "none");
                }
            });

            $("#searchForm").submit(function () {
                var form = $("#searchForm");
                var selectTypes = $("#Type");
               
                if ($.trim(selectProvinces.val()) == "" || $.trim(selectCities.val()) == "" || $.trim(selectCities.val()) == "请选择") {
                    alert("请选择省/市");
                }
                else if ($.trim(selectTypes.val()) == "") {
                    alert("请选择广告位置");
                } else {
                    $(".hidediv").css("display", "block");
                    $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
                    $("#addbt").removeAttr("disabled");
                    var option = {
                        type: "POST",
                        url: form.attr("action"),
                        dataType: "html",
                        data: form.formSerialize(),
                        success: function(data) {
                            //                        $("#list_page").pagination(data.result, opts);
                            $("#ListResult").html(data);
                            provincesearchid = selectProvinces.val();
                            citysearchid = selectCities.val();
                            positionsearch = selectTypes.val();
                        }
                    };
                    form.ajaxSubmit(option);
                }
                return false;
            });
        });
        $(function () {//输入下拉框显示楼盘
            $("#PremisesName").change(function () {
                $("#PremisesId").val("");
            });
            JQUiAotu.AutoId = "PremisesName";
            JQUiAotu.Url = "<%=Auxiliary.Instance.NhManagerUrl %>Geography/CityPremisesByKeyword.html";
            JQUiAotu.Show();
        });
        var JQUiAotu = {
            Url: null,
            AutoId: null,
            ProvinceID: $("#ProvinceID"),
            CityID: $("#CityId"),
            Show: function () {
                $("#" + JQUiAotu.AutoId).autocomplete({
                    source: function (request, response) {
                        $.post(JQUiAotu.Url, { q: $("#" + JQUiAotu.AutoId).val(), p: JQUiAotu.ProvinceID.val(), c: JQUiAotu.CityID.val(), d: new Date().getTime(), r: Math.random() }, function (data) {
                            if (data.success) {
                                var redate = eval(data.result);
                                if (!data.result)
                                    redate = [{ DeveloperName: "无数据", Id: "-1"}];
                                response($.map(redate, function (item) {
                                    return {
                                        label: item.name,
                                        value: item.id,
                                        id: item.id
                                    };
                                }));
                            }
                        });
                    },
                    minLength: 1,
                    change: function(event, ui) {
                         $("#PremisesName").val(ui.item.label); // display the selected text
                        $("#PremisesId").val(ui.item.id);
                    },
                    select: function (event, ui) {
                        if (ui.item.value == -1)
                            return false;
                        $("#PremisesName").val(ui.item.label); // display the selected text
                        $("#PremisesId").val(ui.item.id);
                        return false;
                    }
                   
                });
            }
        };
    </script>
    <%--<script type="text/javascript">
        //        function pageselectCallback(page_index, jq) {
        //            //opts.current_page = page_index;
        //            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
        //                    $.ajax({
        //              type: "POST",
        //              url: "",
        //              dataType: "html",
        //              data: {Province:provincesearchid,Cityid:citysearchid,Postion:positionsearch},
        //              success: function(data) {
        //                  $("#ListResult").html(data);
        //              }
        //            });

        ////     $("#ListResult").load("<%=Url.SiteUrl().BargainActive_SearchResult("searchresult",Model)%>?\pageindex=" + page_index + "&pagesize=" + opts.items_per_page);
        //        }
        //        //初始化分页参数
        //        var opts = { callback: pageselectCallback };
        //        opts.items_per_page = 20;       //每页的记录条数
        //        opts.next_text = "下一页";       //上一条的文本
        //        opts.prev_text = "上一页";       //下一条的文本
        //        opts.last_text="尾页";
        //        opts.num_display_entries = 5; //中间显示的页码个数
        //        opts.num_edge_entries = 2;     //两边显示的页码个数
        //        opts.link_to = "javascript:void(0);";
        //        //文档加载完毕后, 初始化分页组件
        //        $(document).ready(function () {
        ////            $.post("<%=Url.SiteUrl().BargainActive_SearchResult("searchcount",Model)%>", function (data, textStatus) {
        ////                $("#list_page").pagination(data.result, opts);
        ////            });
        //        });
        //        <% if (!string.IsNullOrEmpty(Convert.ToString(ViewData["CurrentPageIndex"])) 
        //        && Convert.ToInt32(ViewData["CurrentPageIndex"]) > -1)
        //           {
        //        %>
        //        opts.current_page = <%=ViewData["CurrentPageIndex"] %>;
        //        <%
        //           } %>
    </script>--%>
    <script type="text/javascript">
        Tool = {
            DataFormt: function (oldstr) {
                return oldstr.replace(/-/g, "/");
            }
        };


        $(function () {
            $("#savebt").click(function () {
                var cityid = $("#CityId").val();
                var cityname = $("#CityId").find("option:selected").text();
                var postion = $("#Type").val();
                var list = $("#AdList tbody").children("tr");
                var postarg = new Array();
                for (var i = 0; i < list.length; i++) {
                    var td = $(list[i]).children("td");
                    var seqtd = td.eq(0);
                    var premisestd = td.eq(1);
                    var datetd = td.eq(2);
                    var adidtd = td.eq(3);
                    var seq = $(seqtd.children(".seq")[0]).val();
                    if ($.trim(seq).lenght == 0) {
                        $(seqtd.children(".seq")[0]).focus();
                        alert("排序不能为空");
                        return;
                    }
                    var premisename = $.trim($(premisestd.children(".pname")[0]).html());
                    var premiseid = $(premisestd.children("input")[0]).val();
                    var begindate = $(datetd.children("input")[0]).val();
                    if ($.trim(begindate).length == 0) {
                        $(datetd.children("input")[0]).focus();
                        alert("开始时间不能为空");
                        return;
                    }
                    var enddate = $(datetd.children("input")[1]).val();
                    if ($.trim(enddate).length == 0) {
                        $(datetd.children("input")[1]).focus();
                        alert("结束时间不能为空");
                        return;
                    }
                    var data = cityid + "﹡" + cityname + "﹡" + postion + "﹡" + seq + "﹡" + premisename + "﹡" + premiseid + "﹡" + begindate + "﹡" + enddate;
                    var adid = adidtd.children("input");
                    data += "﹡";
                    if (adid.length > 0)
                        data += $(adid[0]).val();
                    postarg.push(data);
                }
                $("#savebt").attr("disabled", true);
                CommAjax("POST", "/NhAdvertise/SaveAD.html", { data: postarg }, "json", function (result) {
                    if (!result.success && result.success != undefined)
                        alert(result.msg);
                    else {
                        alert("保存成功");

                        $("#ListResult").html(result);
                    }
                    $("#savebt").removeAttr("disabled");
                });
            });
        });
        ///添加
        function AddNewAd() {
            if ($.trim(provincesearchid) == "0" || $.trim(citysearchid) == "0" || $.trim(positionsearch) == "0") {
                alert("请先搜索选择城市/位置");
                return;
            }
            var blist = $("#AdList tbody");
            var premisename = $.trim($("#PremisesName").val());
            var premiseid = $.trim($("#PremisesId").val());
            if (CheckPremises(premiseid)) {
                if (premiseid.length > 0 && premisename.length > 0) {
                    var seqMax = $("#AdList .seq");
                    var trcount = seqMax.length;
                    if (trcount >= maxNum.html()) {
                        alert("超出数量限制");
                        return;
                    }
                    var maxseq = GetMaxVal(seqMax) + 1; //获取已经存在的最大序号+1
                    var html = "<tr>";
                    html += "<td><input type='text' value='" + maxseq + "' class='seq'/></td>";
                    html += "<td><span class='pname'>" + premisename + "</span><input type='hidden' value='" + premiseid + "'/></td>";
                    html += "<td><input type='text' class='begin' onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\\'endtime" + trcount + "\\',{d:-0});}'})\" id='begintime" + trcount + "' />-<input type='text' class='end' onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\\'begintime" + trcount + "\\',{d:0});}'})\" id='endtime" + trcount + "' /></td>";
                    html += "<td><a href='javascript:void()'  class='del' onclick='deleteAd(this)'>删除</a></td>";
                    blist.append(html);
                    $("#AdCountLb").html(trcount + 1);
                } else {
                    alert("请选择楼盘");
                    return;
                }
            } else {
                alert("该楼盘已经存在");
            }
        }
        //获取数组的最大值
        function GetMaxVal(obj) {
            if (obj.length > 0) {
                var seqObj = new Array();
                obj.each(
                    function () {
                        seqObj.push($(this).val());
                    }
                );
                return parseInt(Math.max.apply(null, seqObj));
            }
            return 0;
        }
        //检查楼盘是否添加过
        function CheckPremises(preid) {
            var hasids = $(".pname~input[type='hidden']");
            for (var i = 0; i < hasids.length; i++) {
                if ($(hasids[i]).val() == preid)
                    return false;
            }
            return true;
        }

        function deleteAd(obj) {
            if (confirm("确定删除吗？")) {
                var delval = $($(obj).closest("tr").find(".seq")[0]).val();
                var isnew = $(obj).next("input");
                if (isnew.length > 0) {
                    var delid = $(isnew[0]).val();
                    CommAjax("POST", "/NhAdvertise/DelAd.html", { adid: delid }, "json", function (data) {
                        if (data.success) {
                            alert("删除成功");
                            $(obj).closest("tr").remove();
                            $("#AdCountLb").html($("#AdCountLb").html() - 1);
                            UpdateSeq(delval);
                        }
                    });
                } else {
                    $(obj).closest("tr").remove();
                    $("#AdCountLb").html($("#AdCountLb").html() - 1);
                    UpdateSeq(delval);
                }

            }
        }

        function UpdateSeq(delsel) {
            var seqMax = $("#AdList .seq");
            var del = parseInt(delsel);
            seqMax.each(function () {
                var intval = parseInt($(this).val());
                if (intval > del)
                    $(this).val(intval - 1);
            });
        }

        function CommAjax(type, url, data, datatype, successcallback) {
            $.ajax({
                type: type,
                url: url,
                data: data,
                datatype: datatype,
                success: successcallback,
                traditional: true
            });

        }
    </script>
    <script type="text/javascript">
        ///检查序号，时间范围
        //批量设置时间
        function AllSetDate() {
            var allbegin = $("#AllBeginDate").val();
            var allend = $("#AllEndDate").val();
            if (allbegin.length > 3 && allend.length > 3) {
                if ((new Date(Tool.DataFormt(allbegin))) > (new Date(Tool.DataFormt(allend)))) {
                    alert("时间范围错误");
                } else {
                    $(".begin").val(allbegin);
                    $(".end").val(allend);
                }

            } else {
                alert("开始/结束时间不能为空");
            }
        }
        $(function () {
            //检查序号是否存在
            jQuery("body").on("focusout", ".seq", function () {
                var seqval = $.trim($(this).val());
                var reg = /^\d+$/;
                if (!reg.test(seqval)) {
                    alert("请输入正确的序号");
                    $(this).focus();
                }
                var seqMax = $("#AdList .seq").not($(this));
                seqMax.each(function () {
                    if (seqval == $.trim($(this).val())) {
                        alert("该序号已经存在！请重新输入");
                        $(this).focus();
                        return;
                    }
                });
            });
            //            jQuery("body").on("focusout", ".begin", function () {
            //                var beginval = $(this).val();
            //                var enddate = $(this).next("input");
            //                var endval = $(enddate).val();
            //                if ($.trim(beginval).length == 0) {
            //                    alert("开始时间不能为空");
            //                    $(this).focus();
            //                    return;
            //                } else
            //                    if ($.trim(endval).length == 0)
            //                        return;
            //                    else if ((new Date(Tool.DataFormt(beginval))) > ((new Date(Tool.DataFormt(endval))))) {
            //                        alert("时间范围错误");
            //                        $(enddate).focus();
            //                    }
            //            });
            //            jQuery("body").on("focusout", ".end", function () {
            //                var endval = $(this).val();
            //                var begindate = $(this).prev("input");
            //                var beginval = $(begindate).val();
            //                if ($.trim(endval).length == 0) {
            //                    alert("截止时间不能为空");
            //                    $(this).focus();
            //                    return;
            //                } else
            //                    if ($.trim(endval).length == 0)
            //                        return;
            //                    else if ((new Date(Tool.DataFormt(beginval))) > ((new Date(Tool.DataFormt(endval))))) {
            //                        alert("时间范围错误");
            //                        $(this).focus();
            //                    }
            //            });
        });
    </script>
</asp:Content>
