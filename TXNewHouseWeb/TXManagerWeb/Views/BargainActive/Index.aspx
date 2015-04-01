<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_Bargain>" %>

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
        <% using (Html.BeginForm("search", "BargainActive", FormMethod.Post, new { Id = "searchForm", Name = "searchForm" }))
           {%>
        <div class="filterBar">
            城市：<%=Html.DropDownListFor(model => model.ProvinceID, Model.Provinces, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.CityId, Model.Cityes, new { @class = "w100" })%>
            开发商：<%=Html.TextBoxFor(model => model.DeveloperName, new { @placeholder = "开发商或者代理商名称", @maxlength = "40", })%>
            <input type="button" value="" class="btn01" onclick="submitform()" />
            <input type="hidden" value="<%=Model.Type %>" name="Type" id="Type" />
        </div>
        <div class="filterBar" style="padding-top: 0;">
            <%=Html.DropDownListFor(model => model.PremisesId, Model.Premises, new {@class = "w200"}) %>
            <%=Html.DropDownListFor(model => model.BuildingId, Model.Buildings, new {@class = "w200"}) %>
            <%=Html.DropDownListFor(model => model.UnitId, Model.Units, new {@class = "w200"}) %>
            <%=Html.DropDownListFor(model => model.FloorId, Model.Floors, new {@class = "w200"}) %>
            <%=Html.DropDownListFor(model => model.ActivitieState, Model.ActivitieStates, new { @class = "w100" })%>
        </div>
        <%}
        %>
        <div class="clearFix">
            <div id="ListResult">
            </div>
        </div>
        <!--//clearFix-->
        <div class="paging">
            <!-- 分页 -->
            <p id="list_page" class="pubPage" style="border: none 0">
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
            //opts.current_page = page_index;
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            $("#ListResult").load("<%=Url.SiteUrl().BargainActive_SearchResult("searchresult",Model)%>?DeveloperName=<%=Model.DeveloperName %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page);
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
            $.post("<%=Url.SiteUrl().BargainActive_SearchResult("searchcount",Model)%>?DeveloperName=<%=Model.DeveloperName %>", function (data, textStatus) {
                $("#list_page").pagination(data.result, opts);
            });
        });
        <% if (!string.IsNullOrEmpty(Convert.ToString(ViewData["CurrentPageIndex"])) 
        && Convert.ToInt32(ViewData["CurrentPageIndex"]) > -1)
           {
        %>
        opts.current_page = <%=ViewData["CurrentPageIndex"] %>;
        <%
           } %>

        function submitform() {
            $("#DeveloperName").val(admincoms.StringHelper.cutByteString(40, $("#DeveloperName").val()));
            $("#searchForm").submit();
        }

        $(function () {
            var selectProvinces = $("#ProvinceID");
            var selectCities = $("#CityId");
            var selectPremises = $("#PremisesId");
            var selectBuildings = $("#BuildingId");
            var selectUnits = $("#UnitId");
            var selectFloors = $("#FloorId");

            $(selectProvinces).change(function () {
                clearListItems(selectCities);
                clearListItems(selectPremises);
                clearListItems(selectBuildings);
                clearListItems(selectUnits);
                clearListItems(selectFloors);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });
            $(selectCities).change(function () {
                clearListItems(selectPremises);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Premises") %>?cityId=' + this.value, selectPremises);
            });
            $(selectPremises).change(function () {
                clearListItems(selectBuildings);
                clearListItems(selectUnits);
                clearListItems(selectFloors);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Buildings") %>?id=' + this.value, selectBuildings);
            });
            $(selectBuildings).change(function () {
                clearListItems(selectUnits);
                clearListItems(selectFloors);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Units") %>?id=' + this.value, selectUnits);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("BuildingFloors") %>?id=' + this.value, selectFloors);
            });
           
            JQUiAotu.AutoId = "DeveloperName";
            JQUiAotu.Url = "<%=Auxiliary.Instance.NhManagerUrl %>Geography/GetDevelopersByKeyWord.html";
            JQUiAotu.Show();

        });


        var JQUiAotu = {
             Url: null,
             AutoId: null,
             Show: function() {
                 $("#" + JQUiAotu.AutoId).autocomplete({
                     source: function(request, response) {
                         $.post(JQUiAotu.Url, { q: $("#" + JQUiAotu.AutoId).val(), d: new Date().getTime(), r: Math.random() }, function(data) {
                             var redate = data.redate;
                             if (!data.result)
                                 redate = [{ DeveloperName: "无数据", Id: "-1" }];
                             response($.map(redate, function(item) {
                                 return {
                                     label: item.Name,
                                     value: item.Name,
                                     //id: item.Id
                                 };
                             }));
                         });
                     },
                     minLength: 1,
                     select: function(event, ui) {
                         if (ui.item.id == -1)
                             return false;
                     }
                 });
             }
         };
    </script>
</asp:Content>