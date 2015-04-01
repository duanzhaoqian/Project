<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master" Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_YaoHaoUsers>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=AdminPageInfo.PageTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
           <%=Model.PremisesName %>-摇号报名用户列表</a> </span>
    </div>
    <!--//current-->
     <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="filterBar">
           <!--筛选表单-->
           <%=Html.Hidden("ActivitiesId",Model.ActivitiesId) %>
           <%=Html.Hidden("PremisesId", Model.PremisesId)%>
        </div>
        <div class="clearFix">
            <div id="PremisesResult">
            </div>
        </div>
        <!--//clearFix-->
        <div class="paging">
            <!-- 分页 -->
            <p id="premises_page" class="pubPage" style="border: none 0">
                <!-- 这里显示分页 -->
            </p>
        </div>
        <!--//paging-->
        <!--//filterBar-->
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
          //load
        function pageselectCallback(page_index, jq) {
            opts.current_page = page_index;
            $("#PremisesResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            $("#PremisesResult").load("<%=Url.SiteUrl().YaoHao_Search("usersearchresult")%>?ActivitiesId=<%=Model.ActivitiesId %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page+"&r="+Math.random());
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
        <% if (!string.IsNullOrEmpty(Convert.ToString(ViewData["CurrentPageIndex"])) 
        && Convert.ToInt32(ViewData["CurrentPageIndex"]) > -1){%>
        opts.current_page = <%=ViewData["CurrentPageIndex"] %>;
        <%}%>
        $(document).ready(function() {
            //文档加载完毕后, 初始化分页组件
            $.post("<%=Url.SiteUrl().YaoHao_Search("usersearchcount")%>?ActivitiesId=<%=Model.ActivitiesId %>&r=" + Math.random(), function(data, textStatus) {
                $("#premises_page").pagination(data.result, opts);
            });
        });
        // 刷新本页
        function GoToNewPageForSend() {
            var path = UrlPathHelper.trimEndSharp(window.location.href);
            path = UrlPathHelper.appendParams(path, "pageindex=" + opts.current_page);
            window.location.href = path;
        }
    </script>
</asp:Content>
