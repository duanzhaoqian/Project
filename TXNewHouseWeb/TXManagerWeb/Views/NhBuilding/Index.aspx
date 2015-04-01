<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_Building>" %>

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
                <%=AdminPageInfo.ItemName %></a>
            <%=(String.IsNullOrEmpty(Convert.ToString(ViewData["premisesName"])) 
            ? string.Empty 
            : string.Format("<i>&gt;</i> <a href=\"#\">{0}</a>",ViewData["premisesName"])) %></span>
    </div>
    <!--//current-->
    <div class="data">
        <div class="filterBar">
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
        //初始化分页参数
        var opts = { callback: pageselectCallback };
        opts.items_per_page = 20;       //每页的记录条数
        opts.next_text = "下一页";       //上一条的文本
        opts.prev_text = "上一页";       //下一条的文本
        opts.last_text = "尾页";
        opts.num_display_entries = 5;   //中间显示的页码个数
        opts.num_edge_entries = 2;      //两边显示的页码个数
        opts.link_to = "javascript:void(0);";
        
        //根据页面索引取得内容
        function pageselectCallback(pageIndex, jq) {
            opts.current_page = pageIndex;
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            var url = '<%=Url.SiteUrl().Building_Search("searchresult") %>';
            url += "?PremisesId=" + <%=Model.PremisesId %>;
            url += "&pageindex=" + pageIndex;
            url += "&pagesize=" + opts.items_per_page;
            url += "&ram=" + Math.random();
            $("#ListResult").load(url);
        }

        //文档加载完毕后, 初始化分页组件
        $(document).ready(function () {
            var url = '<%=Url.SiteUrl().Building_Search("searchcount") %>';
            url += "?PremisesId=" + <%=Model.PremisesId %>;
            url += "&ram=" + Math.random();
            $.post(url, function (data, textStatus) {
                $("#pagebar").pagination(data.result, opts);
            });
        });
        
        // 处理backUrl
        function GoToNewPage(path) {
            path = UrlPathHelper.filterUrlParamsRepeat(path);
            var cpath = UrlPathHelper.trimEndSharp(window.location.href);

            if (opts.current_page != undefined) {
                cpath = UrlPathHelper.appendParams(cpath, "pageindex=" + opts.current_page);
            } else {
                cpath = UrlPathHelper.appendParams(cpath, "pageindex=0");
            }
            window.location.href = UrlPathHelper.appendParams(path, "backurl=" + encodeURIComponent(cpath));
        }

        var building = {
            
            del: function(id) {
                if (!confirm("您确定该操作?")) {
                    return;
                }

                var url = '<%= Url.SiteUrl().Building_Search("DelNewBuildingDo") %>';
                var data = {buildingId: id};
                $.post(url,data, function(msg) {
                    if (msg.suc) {
                        var url2 = admincoms.UrlHelper.trimEndSharp(window.location.href);
                        url2 = admincoms.UrlHelper.appendParams(url2, "pageindex=" + opts.current_page);
                        window.location.href = url2;
                        return;
                    }
                    alert("删除失败");
                });
            }
        };
    </script>
</asp:Content>