<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_Discount>" %>

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
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <% using (Html.BeginForm("search", "discount", FormMethod.Get, new { Id = "searchForm", Name = "searchForm" }))
           { %>
        <div class="filterBar">
            城市：<%= Html.DropDownListFor(model => model.ProvinceId, Model.Provinces, new {@class = "w100"}) %>
            <%= Html.DropDownListFor(model => model.CityId, Model.Cityes, new {@class = "w100"}) %>
            开发商：<%= Html.TextBoxFor(model => model.DeveloperName, new { @placeholder = "", @maxlength = "20", @style = "width: 200px;" })%>
            <input type="submit" value="" class="btn01" />
            <input type="hidden" name="ram" value="<%= new Random().Next(9999, 999999) %>" />
        </div>
        <div class="filterBar" style="padding-top: 0;">
        <%= Html.DropDownListFor(model => model.PremisesId, Model.Premisess, new {@class = "w200"}) %>
        <%= Html.DropDownListFor(model => model.ActivitieState, Model.ActivitieStates, new {@class = "w100"}) %>
        </div>
        <% } %>
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
        //初始化分页参数
        var opts = { callback: pageselectCallback };
        opts.items_per_page = 20; //每页的记录条数
        opts.next_text = "下一页"; //上一条的文本
        opts.prev_text = "上一页"; //下一条的文本
        opts.last_text = "尾页";
        opts.num_display_entries = 5; //中间显示的页码个数
        opts.num_edge_entries = 2; //两边显示的页码个数
        opts.link_to = "javascript:void(0);";

        //根据页面索引取得内容
        function pageselectCallback(pageIndex, jq) {
            opts.current_page = pageIndex;
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            var url = '<%=Url.SiteUrl().Discount_Search("searchresult") %>';
            url += "?ProvinceId=<%=Model.ProvinceId %>";
            url += "&CityId=<%=Model.CityId %>";
            url += "&DeveloperId=<%=Model.DeveloperId %>";
            url += "&DeveloperName=<%=Model.DeveloperName %>";
            url += "&PremisesId=<%=Model.PremisesId %>";
            url += "&ActivitieState=<%=Model.ActivitieState %>";
            url += "&pageindex=" + pageIndex;
            url += "&pagesize=" + opts.items_per_page;
            url += "&ram=" + Math.random();
            $("#ListResult").load(url);
        }

        // 处理backUrl
        function GoToNewPage(path) {
            path = admincoms.UrlHelper.filterUrlParamsRepeat(path);
            var cpath = admincoms.UrlHelper.trimEndSharp(window.location.href);

            if (opts.current_page != undefined) {
                cpath = admincoms.UrlHelper.appendParams(cpath, "pageindex=" + opts.current_page);
            } else {
                cpath = admincoms.UrlHelper.appendParams(cpath, "pageindex=0");
            }
            window.location.href = admincoms.UrlHelper.appendParams(path, "backurl=" + encodeURIComponent(cpath));
        }

        var discount = {
            // 删除
            del: function (id) {
                var url = '<%= Url.SiteUrl().Discount_Search("DelDiscountDo") %>';
                var data = { id: id, ram: Math.random() };
                if (!confirm('您确定要执行该操作?')) {
                    return;
                }
                $.post(url, data, function (msg) {
                    if (msg.suc) {
                        var href = admincoms.UrlHelper.trimEndSharp(window.location.href);
                        window.location.href = href;
                        return;
                    }
                    alert(msg.msg);
                });
            }
        };

        $(function () {
            var selectProvinces = $("#ProvinceId");
            var selectCities = $("#CityId");
            var selectPremises = $("#PremisesId");
            var selectActivitieState = $("#ActivitieState");

            $(selectProvinces).change(function () {
                clearListItems(selectCities);
                if (0 == selectProvinces.val()) {
                    return;
                }
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });
            
            $(selectCities).change(function () {
                clearListItems(selectPremises);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Premises") %>?cityId=' + this.value, selectPremises);
                // loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });

            $(selectPremises).change(function () {
                return;
//                var url = '<%=Url.SiteUrl().Discount_Search("search") %>';
//                url += "?ProvinceId=<%=Model.ProvinceId %>";
//                url += "&CityId=<%=Model.CityId %>";
//                url += "&DeveloperId=<%=Model.DeveloperId %>";
//                url += "&DeveloperName=<%=Model.DeveloperName %>";
//                url += "&PremisesId=" + selectPremises.val();
//                url += "&ActivitieState=-1";
//                url += "&ram=" + Math.random();
//                window.location.href = url;
            });

            $(selectActivitieState).change(function () {
                return;
//                var url = '<%=Url.SiteUrl().Discount_Search("search") %>';
//                url += "?ProvinceId=<%=Model.ProvinceId %>";
//                url += "&CityId=<%=Model.CityId %>";
//                url += "&DeveloperId=<%=Model.DeveloperId %>";
//                url += "&DeveloperName=<%=Model.DeveloperName %>";
//                url += "&PremisesId=" + selectPremises.val();
//                url += "&ActivitieState=" + selectActivitieState.val();
//                url += "&ram=" + Math.random();
//                window.location.href = url;
            });

            var url = '<%=Url.SiteUrl().Discount_Search("searchcount") %>';
            url += "?ProvinceId=<%=Model.ProvinceId %>";
            url += "&CityId=<%=Model.CityId %>";
            url += "&DeveloperId=<%=Model.DeveloperId %>";
            url += "&DeveloperName=<%=Model.DeveloperName %>";
            url += "&PremisesId=<%=Model.PremisesId %>";
            url += "&ActivitieState=<%=Model.ActivitieState %>";
            url += "&ram=" + Math.random();
            $.post(url, function (data, textStatus) {
                $("#pagebar").pagination(data.result, opts);
            });
        });
    </script>
</asp:Content>