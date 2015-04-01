<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_Premises>" %>

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
            <% using (Html.BeginForm("SearchNoDevel", "Premises", FormMethod.Post, new { Id = "searchForm", Name = "searchForm" }))
               {%>
            城市：<%=Html.DropDownListFor(model => model.ProvinceID, Model.Provinces, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.CityID, Model.Cities, new { @class = "w100" })%>
            开发商：<%=Html.TextBoxFor(model => model.DeveloperName, new { @placeholder = "", @maxlength = "40", @style = "width: 100px;" })%>
            楼盘名称：<%=Html.TextBoxFor(model => model.PremisesName, new { @placeholder = "", @maxlength = "50", @style = "width: 100px;" })%>
            销售状态：<%=Html.DropDownListFor(model => model.SalesStatus, Model.SalesState, new { @class = "w100" })%>
            <input type="button" value="" class="btn01" onclick="submitForm()" />
            <div>
                开发商:<%= Html.DropDownList("developlist",  ViewData["developlist"] as List<SelectListItem>, new {@class = "w100"}) %><input
                    type="button" value="关联" onclick="DevelopRelationPremises();" />
            </div>
            <%}
            %>
            <%--      <div class="icon_buttonA dingwei">
                <div class="L">
                </div>
                <div class="C">
                    <a href="<%=Url.SiteUrl().PublishPremises %>">发布楼盘</a>
                </div>
                <div class="R">
                </div>
            </div>--%>
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
        //根据页面索引取得内容
        function pageselectCallback(page_index, jq) {
            //opts.current_page = page_index;
            $("#PremisesResult").html('<div style="text-align: center;"><img src="<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>" alt="正在加载中..." /></div>');
            $("#PremisesResult").load('<%=Url.SiteUrl().Premises_SearchResult("SearchNoDevelResult",Model)%>?DeveloperName=<%=Model.DeveloperName %>&PremisesName=<%=Model.PremisesName %>&pageindex=' + page_index + '&pagesize=' + opts.items_per_page);
        }
        //初始化分页参数
        var opts = { callback: pageselectCallback };
        opts.items_per_page = 20;       //每页的记录条数
        opts.next_text = "下一页";       //上一条的文本
        opts.prev_text = "上一页";       //下一条的文本
        opts.last_text = "尾页";
        opts.num_display_entries = 5; //中间显示的页码个数
        opts.num_edge_entries = 2;     //两边显示的页码个数
        opts.link_to = "javascript:void(0);";
        //文档加载完毕后, 初始化分页组件
        $(document).ready(function () {
            $.post('<%=Url.SiteUrl().Premises_SearchResult("SearchNoDevelCount",Model)%>?DeveloperName=<%=Model.DeveloperName %>&PremisesName=<%=Model.PremisesName %>', function (data, textStatus) {
                $("#premises_page").pagination(data.result, opts);
            });
        });
        $(function () {
            var selectProvinces = $("#ProvinceID");
            var selectCities = $("#CityID");

            $(selectProvinces).change(function () {
                clearListItems(selectCities);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selectCities);
            });

            JQUiAotu.AutoId = "DeveloperName";
            JQUiAotu.Url = "<%= Auxiliary.Instance.NhManagerUrl %>Geography/GetDevelopersByKeyWord.html";
            JQUiAotu.Show();
        });

        function submitForm() {
            $("#PremisesName").val(admincoms.StringHelper.cutByteString(50, $("#PremisesName").val()));
            $("#DeveloperName").val(admincoms.StringHelper.cutByteString(40, $("#DeveloperName").val()));
            $("#searchForm").submit();
        }

        var JQUiAotu = {
            Url: null,
            AutoId: null,
            Show: function () {
                $("#" + JQUiAotu.AutoId).autocomplete({
                    source: function (request, response) {
                        $.post(JQUiAotu.Url, { q: $("#" + JQUiAotu.AutoId).val(), d: new Date().getTime(), r: Math.random() }, function (data) {
                            var redate = data.redate;
                            //if (!data.result)
                            //redate = [{ Name: "无数据", Id: "-1" }];
                            response($.map(redate, function (item) {
                                return {
                                    label: item.Name,
                                    value: item.Name,
                                    id: item.Id
                                };
                            }));
                        });
                    },
                    minLength: 1,
                    select: function (event, ui) {
                        //                         if (ui.item.id == -1)
                        //                             return false;
                    }
                });
            }
        };
    </script>
    <script>
        function DevelopRelationPremises() {
            var devel = $("#developlist");
            var checkpremies = $(".checkpremises");
            if ($.trim(devel.val()) == 0) {
                alert("请选择需要关联的开发商");
                return;
            }
            var pids = [];
            if (checkpremies) {
                for (var i = 0; i < checkpremies.length; i++) {
                    if ($(checkpremies[i]).is(':checked')) {
                        pids.push($(checkpremies[i]).val());
                    }
                }
            }
            if (pids.length == 0) {
                alert("请选择需要关联的楼盘");
                return;
            }
            $.post("<%= Auxiliary.Instance.NhManagerUrl %>premises/RelationPremise.html", { did: devel.val(), pids: pids.join(',') }, function (result) {
                alert(result.msg);
                $.post('<%=Url.SiteUrl().Premises_SearchResult("SearchNoDevelCount",Model)%>?DeveloperName=<%=Model.DeveloperName %>&PremisesName=<%=Model.PremisesName %>', function (data, textStatus) {
                    $("#premises_page").pagination(data.result, opts);
                });
            });
        }

    </script>
</asp:Content>
