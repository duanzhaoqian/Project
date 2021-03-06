﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_RegistrationUser>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .dingwei2
        {
            position: absolute;
            right: 80px;
            top: 1px;
        }
    </style>
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a>-砍价报名用户列表</span>
    </div>
    <!--//current-->
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <% using (Html.BeginForm("RegistrationUserSearch", "BargainActive", FormMethod.Post, new { Id = "searchForm", Name = "searchForm" }))
           { %>
        <div class="filterBar" style="display: block;">
            <%--开发商：<%= Html.TextBoxFor(model => model.DeveloperName, new { @placeholder = "开发商或者代理商名称", @maxlength = "100", @style = "width: 200px;" })%>
            <input type="submit" value="" class="btn01" />--%>
            <input id="" name="" type="hidden" value="<%=Model.Id %>" />
            <input id="Type" name="Type" type="hidden" value="<%=Model.Type %>" />
            <div class="icon_buttonA dingwei2">
                <span style=" float:left; padding-right:530px"><%=ViewData["GetHouseInfos"]%></span>
                <div class="L">
                </div>
                <div class="C">
                    <a href="<%=Url.SiteUrl().ActionUrl("ExportExcel","BargainActive") %>?Id=<%=Model.Id %>&Type=<%=Model.Type %>"
                        id="btn_output1">导出此列表</a></div>
                <div class="R">
                </div>
            </div>
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
         var IsActivityClose = '<%= Convert.ToDateTime(ViewData["endtime"]) < DateTime.Now %>';

        //根据页面索引取得内容
        function pageselectCallback(page_index, jq) {
            //opts.current_page = page_index;
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            $("#ListResult").load("<%=Url.SiteUrl().BargainUser_Search("RegistrationUserSearchResult")%>?Id=<%=Model.Id %>&HouseId=<%=Model.HouseId %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page);
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
            $.post("<%=Url.SiteUrl().BargainUser_Search("RegistrationUserSearchCount")%>?Id=<%=Model.Id %>&HouseId=<%=Model.HouseId %>", function (data, textStatus) {
                $("#pagebar").pagination(data.result, opts);
            });
        });
    </script>
</asp:Content>