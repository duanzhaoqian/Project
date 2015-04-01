<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_LadderBuy>" %>

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
        <% using (Html.BeginForm("search", "PurchaseHouse", FormMethod.Get, new { Id = "searchForm", Name = "searchForm" }))
           { %>
        <div class="filterBar">
            城市：<%= Html.DropDownListFor(model => model.ProvinceID, Model.Provinces, new {@class = "w100"}) %>
            <%= Html.DropDownListFor(model => model.CityId, Model.Cityes, new {@class = "w100"}) %>
            开发商：<%= Html.TextBoxFor(model => model.DeveloperName, new { @placeholder = "开发商或者代理商名称", @maxlength = "20", @style = "width: 200px;" })%>
            <input type="submit" value="" class="btn01" />
            <input id="Type" name="Type" type="hidden" value="<%=Model.Type %>" />
        </div>
        <div class="filterBar" style="padding-top: 0;">
            <%= Html.DropDownListFor(model => model.PremisesId, Model.Premises, new {@class = "w200"}) %>
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
        //根据页面索引取得内容
        function pageselectCallback(page_index, jq) {
            //opts.current_page = page_index;
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            $("#ListResult").load("<%=Url.SiteUrl().PurchaseHouse_SearchResult("searchresult",Model)%>?DeveloperName=<%=Model.DeveloperName %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page);
        }
        //初始化分页参数
        var opts = { callback: pageselectCallback };
        opts.items_per_page = 20;       //每页的记录条数
        opts.next_text = "下一页";       //上一条的文本
        opts.prev_text = "上一页";       //下一条的文本
        opts.last_text="尾页";
        opts.num_display_entries = 5; //中间显示的页码个数
        opts.num_edge_entries = 2;     //两边显示的页码个数
        opts.link_to = "javascript:void(0);";
        //文档加载完毕后, 初始化分页组件
        $(document).ready(function () {
            $.post("<%=Url.SiteUrl().PurchaseHouse_SearchResult("searchcount",Model)%>?DeveloperName=<%=Model.DeveloperName %>", function (data, textStatus) {
                $("#pagebar").pagination(data.result, opts);
            });
        });
        

        $(function () {
            var selectProvinces = $("#ProvinceID");
            var selectCities = $("#CityId");
            var selectPremise = $("#PremisesId");

            $(selectProvinces).change(function () {
                clearListItems(selectCities);
                clearListItems(selectPremise);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });
            $(selectCities).change(function () {
                clearListItems(selectPremise);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Premises") %>?cityId=' + this.value, selectPremise);
            });
        });


        var purchase = {
            del: function(id) {
                if (!confirm("您确定删除?")) {
                    return;
                }
                var url = '<%= Auxiliary.Instance.NhManagerUrl %>purchasehouse/del.html';
                var data = { id: id };
                $.post(url, data, function(msg) {
                    if (msg.suc) {
                        alert("删除成功");
                        window.location.href = window.location.href;
                        return;
                    }
                    alert("删除失败");
                });
            }
        };
    </script>
</asp:Content>