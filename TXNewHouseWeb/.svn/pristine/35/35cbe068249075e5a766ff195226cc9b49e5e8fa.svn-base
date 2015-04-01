<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXFinancialManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_WithdrawCash>" %>

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
        <div class="filterBar">
            <%using (Html.BeginForm("Index", "NhWithdrawCash", FormMethod.Post, new { Id = "searchForm", Name = "searchForm" }))
              {%>
            <span class="font12">申请时间：</span>
            <%=Html.TextBoxFor(model => model.BeginTime, new { @class = "", @readonly = "readonly " })%>
            -
            <%=Html.TextBoxFor(model => model.EndTime, new { @class = "", @readonly = "readonly " })%>
            <%=Html.DropDownListFor(model => model.UserType, Model.UserTypes, new {@readonly = "readonly"}) %>
            <%=Html.TextBoxFor(model => model.KeyWord, new { @placeholder = "登录名/手机号码", @maxlength = "100" })%>
            <%=Html.HiddenFor(model=>model.Status)%>
            <input type="submit" value="" class="btn01" />
            <%}
              if (Model.Status == 1)
              {
            %>
            <div class="icon_buttonA dingwei" style="top: 5px">
                <div class="L">
                </div>
                <div class="C">
                    <a href="<%= Url.SiteUrl().NhWithdrawCash("SearchResult_Export", Model) %>?UserType=<%= Model.UserType %>&BeginTime=<%= Model.BeginTime %>&EndTime=<%= Model.EndTime %>&KeyWord=<%= Model.KeyWord %>&Status=<%= Model.Status %>"
                        id="btn_output1">导出数据</a></div>
                <div class="R">
                </div>
            </div>
            <%
              }
            %>
        </div>
        <%--<div class="clearFix">--%>
        <div id="MemberResult">
        </div>
        <%-- </div>--%>
        <!--//clearFix-->
        <div class="paging">
            <!-- 分页 -->
            <p id="member_page" class="pubPage" style="border: none 0">
                <!-- 这里显示分页 -->
            </p>
        </div>
        <!--//paging-->
        <!--//filterBar-->
        <!-- InstanceEndEditable -->
    </div>
    <!--//data-->
    <script type="text/javascript">
        //根据页面索引取得内容
        function pageselectCallback(page_index, jq) {
            $("#MemberResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            $("#MemberResult").load("<%=Url.SiteUrl().NhWithdrawCash("searchresult",Model)%>?UserType=<%=Model.UserType %>&BeginTime=<%=Model.BeginTime %>&EndTime=<%=Model.EndTime %>&Status=<%=Model.Status %>&KeyWord=<%=Model.KeyWord %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page + "&ram=" + Math.random());
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
        $(document).ready(function() {
            
            $("#BeginTime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });
            $("#EndTime").bind("focus", function () { WdatePicker({ isShowWeek: true, dateFmt: 'yyyy-MM-dd', readOnly: true }); });

            $.post("<%=Url.SiteUrl().NhWithdrawCash("searchcount",Model)%>?UserType=<%=Model.UserType %>&BeginTime=<%=Model.BeginTime %>&EndTime=<%=Model.EndTime %>&Status=<%=Model.Status %>&KeyWord=<%=Model.KeyWord %>&ram=" + Math.random(), function(data, textStatus) {
                $("#member_page").pagination(data.result, opts);
            });
            $("#KeyWord").keyup(function() {
                $("#KeyWord").val(stripscript($("#KeyWord").val()));
            });

            function stripscript(s) {
                var pattern = new RegExp("[：,’”;\/<>.]");
                var rs = "";
                for (var i = 0; i < s.length; i++) {
                    rs = rs + s.substr(i, 1).replace(pattern, '');
                }
                return rs;
            }
        });
    </script>
</asp:Content>