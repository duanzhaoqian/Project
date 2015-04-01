<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<List<TXCommons.PictureModular.PremisesPictureInfo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房图片管理
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
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <% using (Html.BeginForm("SearchImg", "ImgManager", FormMethod.Post, new { Id = "searchForm" }))
           { %>
        <div class="filterBar">
            城市：<%= Html.DropDownList("ProvinceID",null, new {@class = "w100"}) %>
            <%= Html.DropDownList("CityId",null,new {@class = "w100"}) %>
            <input type="submit" value="" class="btn01" />
        </div>
        <% } %>
        <div class="clearFix">
            <div id="ListResult">
            </div>
        </div>
        <div class="paging">
            <!-- 分页 -->
            <p id="premisesimg_page" class="pubPage" style="border: none 0">
                <!-- 这里显示分页 -->
            </p>
        </div>
        <div style="text-align: center;" id="pageloding">
            <img src="<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>" alt="正在加载中..." /></div>
    </div>
    <script>
        $(function () {
            var selectProvinces = $("#ProvinceID");
            var selectCities = $("#CityId");
            $(selectProvinces).change(function () {
                clearListItems(selectCities);
                $container.empty();
                $("#premisesimg_page").empty();
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
                //reset();
            });
            $(selectCities).change(function () {
                pindex = 1;
                $container.empty();
                $("#premisesimg_page").empty();
            });
        })
    </script>
    <script>
        $(function () {//分页
            var selectProvinces = $("#ProvinceID");
            var selectCities = $("#CityId");
            $("#searchForm").submit(function () {
                var form = $("#searchForm");
                if ($.trim(selectProvinces.val()) == "" || $.trim(selectCities.val()) == "" || $.trim(selectCities.val()) == "请选择") {
                    alert("请选择省/市");
                } else {

                    cityval = selectCities.val();
                    provinceval = selectProvinces.val();
                    var option = {
                        type: "POST",
                        url: form.attr("action"),
                        dataType: "html",
                        data: { city: selectCities.val(), province: selectProvinces.val(), pindex: pindex, pagesize: opts.items_per_page },
                        success: function (data) {
                            $("#ListResult").html(data);
                            $.post("<%= Auxiliary.Instance.NhManagerUrl %>imgmanager/SearchImgCount.html", { cityid: cityval }, function (result) {
                                $("#premisesimg_page").pagination(result, opts);
                            }); //分页

                        },
                        error: function () {
                            alert(arguments[0]);
                        }
                    };
                    form.ajaxSubmit(option);
                }
                return false;
            });
        });
    </script>
    <script type="text/javascript">
        var pindex = 1;
        var cityval;
        var provinceval;

        //        function reset() {
        //            pindex = 1;
        //            $container.empty();
        //            $("#pageloding").html('<img src="<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>" alt="正在加载中..." />');
        //        }

        //        $(function () {//分页
        //            var selectProvinces = $("#ProvinceID");
        //            var selectCities = $("#CityId");
        //            $("#searchForm").submit(function () {
        //                var form = $("#searchForm");
        //                if ($.trim(selectProvinces.val()) == "" || $.trim(selectCities.val()) == "" || $.trim(selectCities.val()) == "请选择") {
        //                    alert("请选择省/市");
        //                } else {
        //                    reset();
        //                    $("#pageloding").show("slow");
        //                    $("#ListResult").removeAttr("style");
        //                    cityval = selectCities.val();
        //                    provinceval = selectProvinces.val();
        //                    var option = {
        //                        type: "POST",
        //                        url: form.attr("action"),
        //                        dataType: "html",
        //                        data: { city: selectCities.val(), province: selectProvinces.val(), pindex: pindex },
        //                        success: function (data) {
        //                            ShowResult(data);

        //                        },
        //                        error: function () {
        //                            alert(arguments[0]);
        //                        }
        //                    };
        //                    form.ajaxSubmit(option);
        //                }
        //                return false;
        //            });
        //        });
    </script>
    <%-- <script>
        $(function () {//瀑布流分页
            var selectProvinces = $("#ProvinceID");
            var selectCities = $("#CityId");
            $(window).bind("scroll", function () {

                // 判断窗口的滚动条是否接近页面底部
                if ($(document).scrollTop() + $(window).height() > $(document).height() - 10) {
                    // 判断下一页链接是否为空
                    if (!islast) {//不是最后一页翻页
                        if (pindex > 1 && $("#pageloding").is(":hidden") && $.trim(cityval).length > 0 && cityval != "请选择") {
                            //第一页要加载完毕,上页加载完毕
                            $("#pageloding").show("fast");
                            // Ajax 翻页
                            $.ajax({
                                url: $("#searchForm").attr("action"),
                                data: { city: selectCities.val(), province: selectProvinces.val(), pindex: pindex },
                                type: "POST",
                                success: function (data) {
                                    //                                    $container.append(data);
                                    ShowResult(data);


                                    //                                });
                                },
                                complete: function () {
                                    // setTimeout(sentIt = true, 600);
                                    //                                    alert("完成");
                                }
                            });
                        }
                    } else {
                        $("#pageloding").html("木有了噢，最后一页了！");
                        $("#pageloding").show("fast");
                    }
                }
            });
        });
        function ShowResult(data) {
            if (data == "-1") {
                islast = true;
                $("#pageloding").html("木有了噢，最后一页了！");
                $("#pageloding").show("fast");
                return;
            }
            var reg = /\r|\n/g;
            data = data.replace(reg, "");
            var $newElems = $(data).find(".imgbox").css({ opacity: 0 });
            $container.append($newElems);
            //$newElems.imagesLoaded(function () {
            //            $container.masonry('appended', $newElems);//图片有叠加
            //渐显新的内容
            $newElems.animate({ opacity: 1 });
            // 隐藏正在加载模块
            $("#pageloding").hide("slow");
            pindex++;
        }
    </script>--%>
    <script>
        //根据页面索引取得内容
        function pageselectCallback(page_index) {
            //opts.current_page = page_index;
            page_index += 1;
            $("#ListResult").html('<div style="text-align: center;"><img src="<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>" alt="正在加载中..." /></div>');
            $.post("<%= Auxiliary.Instance.NhManagerUrl %>imgmanager/SearchImg.html", { city: cityval, province: provinceval, pindex: page_index, pagesize: opts.items_per_page }, function (data) {
                $("#ListResult").html(data);
            });
        }
        //初始化分页参数
        var opts = { callback: pageselectCallback };
        opts.items_per_page = 50;       //每页的记录条数
        opts.next_text = "下一页";       //上一条的文本
        opts.prev_text = "上一页";       //下一条的文本
        opts.last_text = "尾页";
        opts.num_display_entries = 5; //中间显示的页码个数
        opts.num_edge_entries = 2;     //两边显示的页码个数
        opts.link_to = "javascript:void(0);";
        //文档加载完毕后, 初始化分页组件
        //        $(document).ready(function () {
        //            $.post("/imgmanager/SearchImgCount.html",{ },function (data) {
        //                $("#premisesimg_page").pagination(data.result, opts);
        //            });
        //        });
    </script>
    <script type="text/javascript">
        var $container = $('#ListResult');
        var islast = false;
        $(function () {
            var x = 10;
            var y = 20;
            $container.on("mouseover", ".imgbox img", function (e) {
                this.myTitle = $(this).attr("data");
                var tooltip = "<div id='tooltip'>" + this.myTitle + "<\/div>"; //创建 div 元素 文字提示 
                $("body").append(tooltip);    //把它追加到文档中
                tiplocation(e).show("fast");      //设置x坐标和y坐标，并且显示
            }).mouseout(function () {
                $("#tooltip").remove();   //移除 
            }).mousemove(function (e) {
                tiplocation(e);
            });
            $container.on("click", ".imgbox img", function (e) {
                $("#imgShow").css({ width: document.body.clientWidth + "px", height: document.body.clientHeight + "px" }).html("<div style='position: absolute; z-index: 9999;top: " + (e.pageY > 200 ? e.pageY - 200 : 200) + "px;left:300px; border: 1px solid rgb(134, 0, 1); padding: 5px; background: rgb(239, 239, 239);'><img src='" + $(this).attr("src") + "' style='max-width:900px' /><div>" + $(this).attr("data") + "</div></div>").show();
            });
            $("*").click(function (e) {
                if ($(e.target).closest("#imgShow").attr("id") == "imgShow") {
                    $("#imgShow").hide();
                }
            });
            function tiplocation(e) {
                var ey = e.pageY + y;
                var ex = e.pageX + x;
                $("#tooltip")
                    .css({
                        "top": ey + "px",
                        "left": ex + "px"
                    });
                return $("#tooltip");
            }

            //            $container.masonry({
            //                // 每一列数据的宽度，若所有栏目块的宽度相同，可以不填这段
            //                columnWidth: 279,
            //                // .imgbox 为各个图片的容器
            //                itemSelector: '.imgbox'
            //                //                isFitWidth: true,
            //                //                isOriginLeft: false
            //            });
            //            });


        });
       
    </script>
    <script>
        function DelImg(id, innercode, pictype, cityid, mapid, obj) {
            if (!confirm("确定执行当前操作？")) {
                return;
            }
            var url = '<%= TXCommons.GetConfig.PremisesImgUploadBaseUrl %>Premises/Delete.ashx';
            var data = {
                guid: innercode,
                picturetype: pictype,
                minnum: 0,
                cityId: cityid,
                id: id
            };
            $.ajax({
                url: url,
                data: data,
                cache: false,
                type: "get",
                dataType: "jsonp",
                jsonp: "callbackparam",
                jsonpCallback: "success_jsonpCallback",
                success: function (msg) {
                    var i;
                    for (i = 0; i < msg.length; i++) {
                        if (msg[i].result == "true") {
                            //                            $(obj).closest(".imgbox").remove();
                            $(obj).next("img").attr("src", '<%=Auxiliary.Instance.NhWebThemeUrl("images/icons/icon_delpng.png")%>');
                            $(obj).removeAttr("onclick");
                            $(obj).click(function () {
                                alert('该图片已经删除');
                            });
                            ajaxDelPicFromDb(id, pictype);
                            ajaxDelPremiseImgMap(mapid);

                        } else if (msg[i].result == "false") {
                            alert("删除图片出错!");
                        } else {
                            alert(msg[i].result);
                        }
                    }
                },
                error: function () {
                    alert("删除图片处理出错!");
                }
            });
        }

        function ajaxDelPremiseImgMap(mapid) {
            var url = '<%= Auxiliary.Instance.NhManagerUrl %>imgmanager/DelPerimseMap.html';
            $.ajax({
                url: url,
                data: { id: mapid },
                cache: false,
                type: "POST",
                success: function (msg) {
                    if (msg > 0)
                        alert('删除成功');
                },
                error: function () { }
            });
        }

        function ajaxDelPicFromDb(id, pictype) {
            var url = '<%= Auxiliary.Instance.NhManagerUrl %>premises/ResetHouseRIdByDelPicId.html';
            var data = {
                picturetype: pictype,
                id: id
            };
            $.ajax({
                url: url,
                data: data,
                cache: false,
                type: "get",
                success: function (msg) {
                },
                error: function () { }
            });
        }
    </script>
    <style>
        #ListResult
        {
            padding: 20px;
            position: relative;
        }
        .imgbox
        {
            display: inline;
            padding: 10px;
            float: left;
            position: relative;
        }
        .imgbox img
        {
            height: 200px;
        }
        #tooltipmsg img
        {
            height: auto;
        }
        #pageloding
        {
            display: none;
        }
        .close:hover, close:hover
        {
            background: url('<%= Auxiliary.Instance.NhWebThemeUrl("images/colose_a.gif")%>') left top no-repeat;
        }
        .close
        {
            width: 13px;
            height: 12px;
            background: url('<%= Auxiliary.Instance.NhWebThemeUrl("images/colose.gif")%>') left top no-repeat;
            display: inline-block;
            position: absolute;
        }
        #tooltip
        {
            position: absolute;
            border: 2px solid #E7D9D9;
            background-color: White;
            padding: 1px;
            color: #333;
            display: none;
            padding: 4px;
            font-size: 12px;
            z-index: 100;
        }
        #imgShow
        {
            position: absolute;
            z-index: 900;
            top: 0px;
            left: 0px;
            background: rgb(51, 57, 60);
            text-align: center;
            padding: 50px;
            display: none;
        }
        #imgShow div
        {
            font-size: 13px;
            text-align: left;
        }
    </style>
    <div id="imgShow">
    </div>
</asp:Content>
