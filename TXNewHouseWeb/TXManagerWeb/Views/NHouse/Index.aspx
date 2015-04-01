<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVS_NH_House>" %>

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
            <% using (Html.BeginForm("Search", "NHouse", FormMethod.Get, new { Id = "searchForm", Name = "searchForm" }))
               {%>
            <span style="font-size: 14px;">筛选：</span>
            <%=Html.DropDownListFor(model => model.BuildingId, Model.Buildings, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.UnitId, Model.Units, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.Floor, Model.Floors, new { @class = "w100" })%>
            <%=Html.DropDownListFor(model => model.SalesStatus, Model.SalesStatuss, new { @class = "w100" })%>
            <input type="hidden" value="<%=Model.PremisesId %>" name="PremisesId" />
            <input type="hidden" value="<%=Model.CityId %>" name="CityId" />
            <input type="submit" value="" class="btn01" />
            <%}%>
            <br />
            <input type="checkbox" value="0" id="AllChecked" />
            全选&nbsp;&nbsp; <a href="javascript:;" id="DeleteCheckedData">删除</a> <a href="javascript:;"
                id="ModifState">更改销售状态</a>
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
            $("#PremisesResult").load("<%=Url.SiteUrl().NewHouse_Search("searchresult")%>?PremisesId=<%=Model.PremisesId %>&BuildingId=<%=Model.BuildingId %>&UnitId=<%=Model.UnitId %>&Floor=<%=Model.Floor %>&SalesStatus=<%=Model.SalesStatus %>&pageindex=" + page_index + "&pagesize=" + opts.items_per_page+"&r="+Math.random());
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
            $.post("<%=Url.SiteUrl().NewHouse_Search("searchcount")%>?PremisesId=<%=Model.PremisesId %>&BuildingId=<%=Model.BuildingId %>&UnitId=<%=Model.UnitId %>&Floor=<%=Model.Floor %>&SalesStatus=<%=Model.SalesStatus %>&r="+Math.random(), function (data, textStatus) {
                $("#premises_page").pagination(data.result, opts);
            });
             //加载城市
            var selectBuilding = $("#BuildingId");
            var selectUnit = $("#UnitId");
            var selectFloor = $("#Floor");
            $(selectBuilding).change(function() {
                clearListItems(selectUnit);
                clearListItems(selectFloor);
                if (this.value!=0) {
                    loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Units") %>?id=' + this.value + "&r=" + Math.random(), selectUnit);
                    loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("BuildingFloors") %>?id=' + this.value + "&r=" + Math.random(), selectFloor);
                }
            });
        });
        $("#AllChecked").click(function(){
            NewHouseManagerJs.AllChecked(this);
        });
        $("#DeleteCheckedData").click(function() {
            NewHouseManagerJs.DeleteData();
        });
        $("#ModifState").click(function() {
             NewHouseManagerJs.ModifyState();
        });
          var NewHouseManagerJs = {
            AllChecked: function(e) {
                $("input[name=houseIds]").each(function() {
                    if ($(e).is(":checked")) {
                         //$(this).attr("checked", 'true');
                        //alert($.fn.jquery);<1.6  $(this).attr("checked", 'true');
                        //>1.6
                       $(this).prop("checked", "checked");
                    } else
                        $(this).removeAttr("checked");
                });
            },
            DeleteData: function() {
                var vals= "";
                var cid = "<%=Model.CityId %>";
                $("input[name=houseIds]:checked").each(function(i) {
                    vals+=(i == 0 ? $(this).val() : ","+$(this).val());
                    //cid+=(i == 0 ? $(this).attr("cityid_data") : ","+$(this).attr("cityid_data"));
                });
                if (vals != "") {
                    if (confirm("确定删除？")) {
                        $.post("<%=Url.SiteUrl().Common("DeleteHouseByIds","NHouse")%>", {ids:vals,cid:cid,adminId: "<%=CurrentUser.Id %>",adminName: "<%=CurrentUser.Name %>" }, function (response) {
                            if (response.result) {
                                GoToNewPageForSend();
                            } else {
                                alert(response.message);
                            }
                        });
                    }
                } else
                    alert("请选择操作房源！");
            },
            ModifyState: function() {
                var cid = "<%=Model.CityId %>";
                var vals = "";
                $("input[name=houseIds]:checked").each(function(i) {
                    vals += (i == 0 ? $(this).val() : "," + $(this).val());
                });
                if (vals != "") {
                    var htmls = [];
                    htmls.push('<table width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>');
                    htmls.push('<tr class=""><td width="100%" align="center"><p id="textMsg" style="display:none; height:22px;" ><span style="display:block; height:22px; text-align:left;" class="lose ml10">请选择销售类型！</span></p></td></tr>');
                    htmls.push('<tr class=""><td width="100%" align="center">更改销售状态：<select class = "w100" id="modifstate" >' + $("#SalesStatus").html() + '</select></td></tr>');
                    htmls.push('</tbody></table>');
                    $.freeDialog.open({
                        width: 375,
                        content: htmls.join(''),
                        isHidden: false,
                        buttons: [{
                                text: '确定',
                                onclick: function(item, dialog) {
                                    var state = $("#modifstate").val();
                                    if (state == -1) {
                                        $("#textMsg").show();
                                    } else {
                                        $.post("<%=Url.SiteUrl().Common("UpdateHouseSalesStatusByIds","NHouse")%>", {ids:vals,cid:cid,state:state,adminId: "<%=CurrentUser.Id %>",adminName: "<%=CurrentUser.Name %>" }, function (response) {
                                                if (response.result) {
                                                    GoToNewPageForSend();
                                                } else {
                                                    dialog.close();
                                                    alert(response.message);
                                                }
                                            });
                                    }
                                }
                            }, {
                                text: '取消',
                                onclick: function(item, dialog) {
                                    dialog.close();
                                }
                            }],
                        Title: "更改销售状态",
                        showTitle: false
                    });
                }else
                    alert("请选择操作房源！");
            }
          };
    </script>
</asp:Content>