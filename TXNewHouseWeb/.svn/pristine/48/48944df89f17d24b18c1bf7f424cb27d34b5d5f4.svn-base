<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_Developer>" %>

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
            <% using (Html.BeginForm("Search", "Developers", FormMethod.Get, new { Id = "searchForm", Name = "searchForm" }))
               {%>
            <span style="font-size: 14px;">城市：</span>
            <%=Html.DropDownListFor(model => model.ProvinceID, Model.Provinces, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.CityId, Model.Cities, new { @class = "w100" })%>
            <input type="text" name="BeginTime" value="<%=Server.UrlDecode(Model.BeginTime) %>"
                class="Wdate" id="d4321" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'d4322\',{d:-0});}'})"
                readonly />
            -<input type="text" name="EndTime" value="<%=Server.UrlDecode(Model.EndTime) %>"
                class="Wdate" id="d4322" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'d4321\',{d:0});}'})"
                readonly />
            &nbsp;&nbsp;<%=Html.TextBoxFor(model => model.Name, new { @placeholder = "公司名称", @style = "width: 200px;", @maxlength=40 })%>
            &nbsp;&nbsp;<select id="selStatus">
                            <option value="3"<%=string.IsNullOrEmpty(Request.Params["State"]) ? " selected=\"selected\"" : ("3".Equals(Request.Params["State"]) ? " selected=\"selected\"" : "") %>>待认证</option>
                            <option value="2"<%=Convert.ToString(Request.Params["State"] ?? "").Equals("2") ? " selected=\"selected\"" : "" %>>未通过</option>
                        </select>
            <input type="hidden" value="3" name="State" />
            <input type="button" value="" class="btn01" onclick="submitForm()"/>
            <%}%>
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
            $("#PremisesResult").load("<%=Url.SiteUrl().Developers_Search("searchresult")%>?State=<%=Model.State %>&Name=<%=Model.Name %>&ProvinceID=<%=Model.ProvinceID %>&CityId=<%=Model.CityId %>&BeginTime=<%=Model.BeginTime %>&EndTime=<%=Model.EndTime %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page+"&r="+Math.random());
        }
        function submitForm() {
            $("#Name").val(admincoms.StringHelper.cutByteString(40, $("#Name").val()));
            $("input[name='State']").val($("#selStatus").val());
            $("#searchForm").submit();
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
        && Convert.ToInt32(ViewData["CurrentPageIndex"]) > -1)
           {
        %>
        opts.current_page = <%=ViewData["CurrentPageIndex"] %>;
        <%
           } %>
        $(document).ready(function() {
            //文档加载完毕后, 初始化分页组件
            $.post("<%=Url.SiteUrl().Developers_Search("searchcount")%>?State=<%=Model.State %>&Name=<%=Model.Name %>&ProvinceID=<%=Model.ProvinceID %>&CityId=<%=Model.CityId %>&BeginTime=<%=Model.BeginTime %>&EndTime=<%=Model.EndTime %>&r=" + Math.random(), function(data, textStatus) {
                $("#premises_page").pagination(data.result, opts);
            });
            //加载城市
            var selectProvinces = $("#ProvinceID");
            var selectCities = $("#CityId");
            $(selectProvinces).change(function() {
                clearListItems(selectCities);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });
        });
    </script>
</asp:Content>