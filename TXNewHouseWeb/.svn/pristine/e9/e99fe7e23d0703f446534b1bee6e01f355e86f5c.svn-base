<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXFinancialManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_Bond>" %>

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
            <% using (Html.BeginForm("index", "nhguarantee", FormMethod.Post, new { Id = "searchForm", Name = "searchForm" }))
               { %>
            <%--<span class="font12" style="font-size: 14px">注册起止日期：</span>--%>
            <%= Html.DropDownListFor(model => model.ProvinceId, Model.Provinces, new {@readonly = "readonly "}) %>
            <%= Html.DropDownListFor(model => model.CityId, Model.Cities, new {@readonly = "readonly "}) %>
            <span class="font12" style="font-size: 14px">预计退还时间：</span>
            <%= Html.TextBoxFor(model => model.BeginTime, new {@readonly = "readonly "}) %>
            -
            <%= Html.TextBoxFor(model => model.EndTime, new {@readonly = "readonly"}) %>
            <%= Html.DropDownListFor(model => model.UserType, Model.UserTypes, new {@readonly = "readonly"}) %>
            <%= Html.TextBoxFor(model => model.KeyWord, new {@placeholder = "房源编号/登录名/手机号码", @maxlength = "100", @style = "width: 200px;"}) %>
            <input type="submit" value="" class="btn01" />
            <% } %>
        </div>
        <div class="clearFix">
            <div id="ListResult">
            </div>
        </div>
        <!--//clearFix-->
        <div class="paging">
            <!-- 分页 -->
            <p id="shouse_page" class="pubPage" style="border: none 0">
                <!-- 这里显示分页 -->
            </p>
        </div>
        <!--//paging-->
        <!--//filterBar-->
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
        //根据页面索引取得内容
        function pageselectCallback(page_index, jq) {
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            $("#ListResult").load("<%=Url.SiteUrl().NhGuarantee("searchresult")%>?ProvinceId=<%=Model.ProvinceId %>&CityId=<%=Model.CityId %>&BeginTime=<%=Model.BeginTime %>&EndTime=<%=Model.EndTime %>&UserType=<%=Model.UserType %>&KeyWord=<%=Model.KeyWord %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page + "&ram=" + Math.random());
        }
        
        //初始化分页参数
        var opts = { callback: pageselectCallback };
        opts.items_per_page = 20;       //每页的记录条数
        opts.next_text = "下一页";       //上一条的文本
        opts.prev_text = "上一页";       //下一条的文本
        opts.last_text = "尾页";
        opts.num_display_entries = 5;   //中间显示的页码个数
        opts.num_edge_entries = 2;      //两边显示的页码个数
        opts.link_to = "javascript:void(0);";
        
        //文档加载完毕后, 初始化分页组件
        $(document).ready(function () {
            $.post("<%=Url.SiteUrl().NhGuarantee("searchcount")%>?ProvinceId=<%=Model.ProvinceId %>&CityId=<%=Model.CityId %>&BeginTime=<%=Model.BeginTime %>&EndTime=<%=Model.EndTime %>&UserType=<%=Model.UserType %>&KeyWord=<%=Model.KeyWord %>&ram=" + Math.random(), function (data, textStatus) {
                $("#shouse_page").pagination(data.result, opts);
            });
        });

        $(function () {
            var selectProvinces = $("#ProvinceId");
            var selectCities = $("#CityId");

            $(selectProvinces).change(function () {
                clearListItems(selectCities);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value + "&ram=" + Math.random(), selectCities);
            });
        });

        $("#KeyWord").keyup(function () {
            $("#KeyWord").val(stripscript($("#KeyWord").val()));
        });

        function stripscript(s) {
            var pattern = new RegExp("[：,’”;\/<>.#]");
            var rs = "";
            for (var i = 0; i < s.length; i++) {
                rs = rs + s.substr(i, 1).replace(pattern, '');
            }
            return rs;
        }
    </script>
</asp:Content>