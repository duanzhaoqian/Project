<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_SecKill>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        <div class="filterBar">
            城市：<%= Html.DropDownListFor(model => model.ProvinceId, Model.Provinces, new {@class = "w100"}) %>
            <%= Html.DropDownListFor(model => model.CityId, Model.Cities, new {@class = "w100"}) %>
            开发商：<%= Html.TextBoxFor(model => model.DeveloperName, new { @placeholder = "开发商或者代理商名称", @maxlength = "40", @style = "width: 200px;" })%>
            <input type="hidden" id="DeveloperId" value="<%= Model.DeveloperId %>" />
            <input type="button" id="btn_submit" value="" class="btn01" />
        </div>
        <div class="filterBar" style="padding-top: 0;">
            <%= Html.DropDownListFor(model => model.PremisesId, Model.Premisess, new {@class = "w200"}) %>
            <%= Html.DropDownListFor(model => model.BuildingId, Model.Buildings, new {@class = "w100"}) %>
            <%= Html.DropDownListFor(model => model.UnitNum, Model.Units, new {@class = "w100"}) %>
            <%= Html.DropDownListFor(model => model.Floor, Model.Floors, new {@class = "w100"}) %>
            <%= Html.DropDownListFor(model => model.ActivitieState, Model.ActivitieStates, new {@class = "w100"}) %>
        </div>
        <div class="clearFix">
            <div id="ListResult">
            </div>
        </div>
        <div class="paging">
            <p id="pagebar" class="pubPage" style="border: none 0">
                <!-- 这里显示分页 -->
            </p>
        </div>
    </div>
    <script type="text/javascript">
        var derName = $("#DeveloperName");

        //初始化分页参数
        var opts = { callback: pageselectCallback };
        opts.items_per_page = 20; //每页的记录条数
        opts.next_text = "下一页"; //上一条的文本
        opts.prev_text = "上一页"; //下一条的文本
        opts.last_text = "尾页";
        opts.num_display_entries = 5; //中间显示的页码个数
        opts.num_edge_entries = 2; //两边显示的页码个数
        opts.link_to = "javascript:void(0);";
        <% if (!string.IsNullOrEmpty(Convert.ToString(ViewData["CurrentPageIndex"]))
               && Convert.ToInt32(ViewData["CurrentPageIndex"]) > -1)
           {
        %>
        opts.current_page = <%= ViewData["CurrentPageIndex"] %>;
        <% } %>
        
        //根据页面索引取得内容
        function pageselectCallback(pageIndex, jq) {
            opts.current_page = pageIndex;
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            var url = '<%=Url.SiteUrl().SecKill_Search("searchresult") %>';
            url += "?ram=" + Math.random();
            url += "&provinceid=<%= Model.ProvinceId %>";
            url += "&cityid=<%= Model.CityId %>";
            url += "&developerid=<%= Model.DeveloperId %>";
            url += "&developername=" + encodeURIComponent('<%= Model.DeveloperName %>');
            url += "&PremisesId=<%= Model.PremisesId %>";
            url += "&BuildingId=<%= Model.BuildingId %>";
            url += "&UnitNum=<%= Model.UnitNum %>";
            url += "&Floor=<%= Model.Floor %>";
            url += "&activitiestate=<%= Model.ActivitieState %>";
            url += "&pageindex=" + opts.current_page;
            $("#ListResult").load(url);
        }
        
        $(function () {
            derName.autocomplete(seckill.autocmp_Developer);

            $("#ProvinceId").bind("change", seckill.evt_ProvinceChanage);
            $("#CityId").bind("change", seckill.evt_CityChange);
            $("#btn_submit").bind("click", seckill.evt_Search1);

            $("#PremisesId").bind("change", seckill.evt_PremisesIdChange);
            $("#BuildingId").bind("change", seckill.evt_BuildingIdChange);
            $("#UnitNum").bind("change", seckill.evt_UnitNumChange);
            $("#Floor").bind("change", seckill.evt_FloorChange);
            $("#ActivitieState").bind("change", seckill.evt_ActivitieStateChange);

            var url = '<%=Url.SiteUrl().SecKill_Search("searchcount") %>';
            url += "?ram=" + Math.random();
            url += "&provinceid=<%= Model.ProvinceId %>";
            url += "&cityid=<%= Model.CityId %>";
            url += "&developerid=<%= Model.DeveloperId %>";
            url += "&developername=" + encodeURIComponent('<%= Model.DeveloperName %>');
            url += "&PremisesId=<%= Model.PremisesId %>";
            url += "&BuildingId=<%= Model.BuildingId %>";
            url += "&UnitNum=<%= Model.UnitNum %>";
            url += "&Floor=<%= Model.Floor %>";
            url += "&activitiestate=<%= Model.ActivitieState %>";
            
            $.post(url, function (data, textStatus) {
                $("#pagebar").pagination(data.result, opts);
            });
        });

        var seckill = {
            // 省、市联动
            evt_ProvinceChanage: function() {
                $("#DeveloperId").val("0");
                derName.val("");
                clearListItems($("#CityId"));
                clearListItems($("#PremisesId"));
                clearListItems($("#BuildingId"));
                clearListItems($("#UnitNum"));
                clearListItems($("#Floor"));
                if (0 == $("#ProvinceId").val()) {
                    return;
                }
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, $("#CityId"));
            },

            // 市联动
            evt_CityChange: function() {
                $("#DeveloperId").val("0");
                clearListItems($("#PremisesId"));
                clearListItems($("#BuildingId"));
                clearListItems($("#UnitNum"));
                clearListItems($("#Floor"));
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Premises") %>?cityId=' + this.value, $("#PremisesId"));
                //derName.val("");
            },

            // 搜索1
            evt_Search1: function() {
                var url = '<%=Url.SiteUrl().SecKill_Search("Search") %>';
               url += "?ram=" + Math.random();
                url += "&provinceid=" + $("#ProvinceId").val();
                url += "&cityid=" + $("#CityId").val();
                url += "&developerid=" + seckill.getDeveloperId();
                url += "&developername=" + encodeURIComponent(admincoms.StringHelper.cutByteString(40, derName.val()));
                url += "&PremisesId=" + $("#PremisesId").val();
                url += "&BuildingId=" + $("#BuildingId").val();
                url += "&UnitNum=" + $("#UnitNum").val();
                url += "&Floor=" + $("#Floor").val();
                url += "&activitiestate=" + $("#ActivitieState").val();

                window.location.href = url;
            },
            
            // 楼盘变动
            evt_PremisesIdChange: function() {
                clearListItems($("#BuildingId"));
                clearListItems($("#UnitNum"));
                clearListItems($("#Floor"));
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Buildings") %>?id=' + this.value, $("#BuildingId"));
            },

            // 楼栋变动
            evt_BuildingIdChange: function() {
                clearListItems($("#UnitNum"));
                clearListItems($("#Floor"));
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Units") %>?id=' + this.value, $("#UnitNum"));
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("BuildingFloors") %>?id=' + this.value, $("#Floor"));
            },
            
            // 单元变化
            evt_UnitNumChange: function() {
                return;
            },
            
            // 楼层变化
            evt_FloorChange: function() {
                return;
            },

            // 活动变化
            evt_ActivitieStateChange: function() {
                return;
            },

            // 开发商 联想输入
            autocmp_Developer: {
                source: function(request, response) {
                    $("#DeveloperId").val("0");
                    var url = '<%= Url.SiteUrl().SecKill_Search("autocmpdeveloper") %>';
                    var parms = { provinceId: $("#ProvinceId").val(), cityId: $("#CityId").val(), name: derName.val(), ram: Math.random() };
                    $.post(url, parms, function(datas) {
                        var list = datas.list;
                        if (!datas.suc) {
                            return;
                        }
                        response($.map(list, function(item) {
                            return {
                                label: item.Name,
                                value: item.Name,
                                id: 0
                            };
                        }));
                    });
                },
                minLength: 1,
                select: function(event, ui) {
                    //$("#DeveloperId").val(ui.item.id);
                }
            },

            // 获取开发商编号
            getDeveloperId: function() {
                return "0";
            },
            
            // 删除活动
            del: function(id) {
                if (confirm("您确定执行该操作?")) {
                    var url = '<%= Url.SiteUrl().SecKill_Search("DelByActivityId") %>';
                    var data = { activityId: id, ram: Math.random() };
                    $.post(url, data, function(msg) {
                        if (msg) {
                            alert("删除成功");

                            var url2 = admincoms.UrlHelper.trimEndSharp(window.location.href);
                            url2 = admincoms.UrlHelper.appendParams(url2, "pageindex=" + opts.current_page);
                            url2 = admincoms.UrlHelper.appendParams(url2, "ram=" + Math.random());
                            window.location.href = url2;
                            return;
                        }

                        alert("删除失败");
                    });
                }
            },
            
            // 编辑活动
            edit: function(id) {
                var url = '<%= Url.SiteUrl().SecKill_Search("EditActivity") %>?ram=' + Math.random() + '&activityid=' + id;
                var backurl = admincoms.UrlHelper.trimEndSharp(window.location.href);
                backurl = admincoms.UrlHelper.appendParams(backurl, "pageindex=" + opts.current_page);
                backurl = admincoms.UrlHelper.appendParams(backurl, "ram=" + Math.random());
                url = admincoms.UrlHelper.appendParams(url, "backurl=" + encodeURIComponent(backurl));
                window.location.href = url;
            }
        };
    </script>
</asp:Content>