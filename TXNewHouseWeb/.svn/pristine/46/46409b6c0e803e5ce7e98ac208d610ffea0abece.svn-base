<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_Biding>" %>

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
            <% using (Html.BeginForm("Search", "NhBiding", FormMethod.Get, new { Id = "searchForm", Name = "searchForm" }))
               {%>
            <span style="font-size: 14px;">城市：</span>
            <%=Html.DropDownListFor(model => model.ProvinceId, Model.Provinces, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.CityId, Model.Cities, new { @class = "w100" })%>
            <span style="font-size: 14px;">开发商：</span>
            <%=Html.TextBoxFor(model => model.Name, new { @placeholder = "开发商或者代理商名称", @style = "width: 140px;", @maxlength = "40" })%>
            <br />
            <span style="font-size: 14px;"></span>
            <%=Html.DropDownListFor(model => model.PremisesId, Model.Premises, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.BuildingId, Model.Buildings, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.UnitId, Model.Units, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.Floor, Model.Floors, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.ActivitieState, Model.ActivitieStates, new { @class = "w100" })%>
            <input type="button" value="" class="btn01" onclick="submitform()" />
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
            $("#PremisesResult").load("<%=Url.SiteUrl().NhBiding_Search(Model,"searchresult")%>?Name=<%=Model.Name %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page+"&r="+Math.random());
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
        $(document).ready(function () {
            //文档加载完毕后, 初始化分页组件
            $.post("<%=Url.SiteUrl().NhBiding_Search(Model,"searchcount")%>?Name=<%=Model.Name %>&r="+Math.random(), function (data, textStatus) {
                $("#premises_page").pagination(data.result, opts);
            });
            
        });
        
        function submitform() {
            $("#Name").val(admincoms.StringHelper.cutByteString(40, $("#Name").val()));
            $("#searchForm").submit();
        }

         $(function () {
            var selectProvinces = $("#ProvinceId");
            var selectCities = $("#CityId");
            var selectPremises = $("#PremisesId");
            var selectBuildings = $("#BuildingId");
            var selectUnits = $("#UnitId");
            var selectFloors = $("#Floor");

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
            JQUiAotu.AutoId = "Name";
            JQUiAotu.Url = "<%= Auxiliary.Instance.NhManagerUrl %>Geography/GetDevelopersByKeyWord.html";
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
                             //if (!data.result)
                                 //redate = [{ Name: "无数据", Id: "-1" }];
                             response($.map(redate, function(item) {
                                 return {
                                     label: item.Name,
                                     value: item.Name,
                                     id: item.Id
                                 };
                             }));
                         });
                     },
                     minLength: 1,
                     select: function(event, ui) {
//                         if (ui.item.id == -1)
//                             return false;
                     }
                 });
             }
         };
         // 刷新本页
        function GoToNewPageForSend() {
            var path = UrlPathHelper.trimEndSharp(window.location.href);
            path = UrlPathHelper.appendParams(path, "pageindex=" + opts.current_page);
            window.location.href = path;
        }
    </script>
</asp:Content>