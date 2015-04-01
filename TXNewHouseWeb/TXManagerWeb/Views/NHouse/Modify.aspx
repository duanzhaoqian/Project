<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVME_NH_House>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>-修改房源
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
    <%using (Html.BeginForm("Modify", "NHouse", FormMethod.Post, new { Id = "formadd", Name = "formadd" }))
      {%>
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="outer">
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            <em class="red">*</em> 请选择：
                        </th>
                        <td width="36%">
                            <%=Html.Hidden("PremisesId", Model.PvmHouse.PremisesId)%>
                            <%= Html.DropDownListFor(model => model.PremisesId, Model.PveHouse.Premises, new { @disabled = "disabled" })%>
                            <%= Html.DropDownListFor(model => model.BuildingId, Model.PveHouse.Buildings, new { @onblur = "Inspection.BuildingId()",@name="aas" })%>
                        </td>
                        <td width="14%">
                        </td>
                        <td width="36%">
                            <!--选用已保存的房源模板：
					<br/>
						<select class="w300">
						<option>东方家园</option>
						<option>东方家园</option>
						<option>东方家园</option>
						</select>
						<br/>
						<input type="button" name="button6" value="选用" class="btn3 mr10"/>-->
                        </td>
                    </tr>
                </table>
                <div class="dispose">
                    <h5>
                        房源信息</h5>
                    <span><em class="red">*</em> 为必填项</span>
                </div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            <em class="red">*</em> 房号：
                        </th>
                        <td width="86%">
                            <input type="text" value="<%=Model.PvmHouse.Name %>" id="Name" name="Name" onblur="Inspection.Name()" maxlength="5"><em
                                class="c-999">（例如：1201;A302）</em>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 单元：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model=>model.UnitId, Model.PveHouse.Units, new { @onblur = "Inspection.UnitId()" })%>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 所在楼层：
                        </th>
                        <td>
                            <label>
                                <input type="radio" name="loucenType" value="0" class="mr5 ver2" onblur="Inspection.LoucenType()"
                                    <%=Model.PvmHouse.Floor>0?"checked":"" %> />地上楼层</label>
                            <label>
                                <input type="radio" name="loucenType" value="1" class="mr5 ver2" onblur="Inspection.LoucenType()"
                                    <%=Model.PvmHouse.Floor<0?"checked":"" %> />地下楼层</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <em class="red">*</em> <input type="hidden" value="<%=Model.PvmHouse.Floor %>" id="Floor" name="Floor" />
                            <%= Html.DropDownListFor(model=>model.PvmHouse.Floor, Model.PveHouse.Floors.Where(it => int.Parse(it.Value) >= 0), new { @id = "dishang", @onblur = "Inspection.Loucen()", @style = Model.PvmHouse.Floor < 0 ? "display:none;" : "" })%>
                            <%= Html.DropDownListFor(model => model.PvmHouse.Floor, Model.PveHouse.Floors.Where(it => int.Parse(it.Value) <= 0).OrderBy(it=>it.Value), new { @id = "dixia", @onblur = "Inspection.Loucen()", @style = Model.PvmHouse.Floor > 0 ? "display:none;" : "" })%>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <p>
                                <em class="red">*</em> 户型：</p>
                        </th>
                        <td>
                            <input type="text" value="<%=Model.PvmHouse.Room %>" name="Room" id="Room" value=""
                                class="inp mr5 valid" style="width: 30px" maxlength="9" onblur="Inspection.Room()">
                            室
                            <input type="text" value="<%=Model.PvmHouse.Hall %>" name="Hall" id="Hall" value=""
                                class="inp mr5" style="width: 30px" maxlength="9" onblur="Inspection.Hall()">
                            厅
                            <input type="text" value="<%=Model.PvmHouse.Toilet %>" name="Toilet" id="Toilet"
                                value="" class="inp mr5" style="width: 30px" maxlength="9" onblur="Inspection.Toilet()">
                            卫
                            <input type="text" value="<%=Model.PvmHouse.Kitchen %>" name="Kitchen" id="Kitchen"
                                value="" class="inp mr5" style="width: 30px" maxlength="9" onblur="Inspection.Kitchen()">
                            厨
                            <%-- <input type="text" value="<%=Model.PvmHouse.Balcony %>" name="Balcony" id="Balcony" value="" class="inp mr5" style="width: 30px" onblur="Inspection.Balcony()">
                                阳台--%>
                            <em class="c-999">&nbsp;&nbsp;&nbsp;&nbsp;请填写数字（例如：3室2厅2卫1厨）</em>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <p>
                                <em class="red">*</em> 建筑形式：</p>
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model=>model.BuildingType, Model.PveHouse.BuildingType, new { @onblur = "Inspection.BuildingType()" })%>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 采光情况：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.Orientation, Model.PveHouse.Orientation, new { @onblur = "Inspection.Orientation()" })%>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <label>
                                <input type="radio" name="PriceType" id="ShiChangPrice" value="0" class="mr5 ver2"
                                    onblur="Inspection.PriceType()" <%=Model.PvmHouse.PriceType==0?"checked":"" %> />市场价格</label>
                            <label>
                                <input type="radio" name="PriceType" id="CanKaoPrice" value="1" class="mr5 ver2"
                                    onblur="Inspection.PriceType()" <%=Model.PvmHouse.PriceType==1?"checked":"" %> />参考价格</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> <em>总价：</em>
                        </th>
                        <td>
                            <input type="text" name="TotalPrice" id="TotalPrice" value="<%=Convert.ToDouble(Model.PvmHouse.TotalPrice)%>"
                                class="inp mr5 w100" onblur="Inspection.TotalPrice()">万元/套<em class="c-999">&nbsp;&nbsp;&nbsp;&nbsp;请填写数字（例如：15000）</em>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> <em>单价：</em>
                        </th>
                        <td>
                            <input type="text" name="SinglePrice" id="SinglePrice" value="<%=Convert.ToDouble(Model.PvmHouse.SinglePrice) %>"
                                class="inp mr5 w100" onblur="Inspection.SinglePrice()">万元/平方米<em class="c-999">&nbsp;&nbsp;&nbsp;&nbsp;请填写数字（例如：15000）</em>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> <em>首期付款：</em>
                        </th>
                        <td>
                            <input type="text" name="DownPayment" id="DownPayment" value="<%=Convert.ToDouble(Model.PvmHouse.DownPayment) %>"
                                class="inp mr5 w100" onblur="Inspection.DownPayment()">万元<em class="c-999">&nbsp;&nbsp;&nbsp;&nbsp;请填写数字（例如：15000）</em>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 销售状态：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model=>model.SalesStatus, Model.PveHouse.SalesStatuss, new {@onblur = "Inspection.SalesStatus()"}) %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 建筑面积：
                        </th>
                        <td>
                            <input type="text" value="<%=Model.PvmHouse.BuildingArea %>" name="BuildingArea"
                                id="BuildingArea" class="inp" onblur="Inspection.BuildingArea()" />平方米
                        </td>
                    </tr>
                    <tr>
                        <th>
                            使用面积：
                        </th>
                        <td>
                            <input type="text" value="<%=Model.PvmHouse.Area %>" name="Area" id="Area" class="inp"
                                onblur="Inspection.BuildingArea()" />平方米
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 户型图：
                        </th>
                        <td>
                            <span id="rviewNo" style="display: none">加载中...</span><span id="rviewOk"><%= Html.DropDownListFor(model => model.RId, Model.PveHouse.RIds, new { @onblur = "Inspection.RId()" })%></span>
                            &nbsp;&nbsp;&nbsp;&nbsp;<input type="button" name="UpdateRIds" id="UpdateRIds" value="更新户型图"
                                class="btn4 mr10" />
                            <a href="<%=Url.SiteUrl().PremisesImgs(Model.PremisesId,1)%>?tage=5" target="_blank">
                                上传户型图</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <em class="red">*</em> 房间位置标注：
                        </th>
                        <td>
                            <input type="button" name="RoomsMarked" id="RoomsMarked" value="点击标注" class="btn3 mr10">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnDiv tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            &nbsp;
                        </th>
                        <td width="86%">
                            <%= Html.Hidden("DeveloperId", Model.PvmHouse.DeveloperId)%>
                            <%= Html.Hidden("InnerCode", Model.PvmHouse.InnerCode)%>
                            <%= Html.Hidden("adminId", CurrentUser.Id)%>
                            <%= Html.Hidden("Id", Model.PvmHouse.Id)%>
                            <%= Html.Hidden("backurl", ViewData["backurl"])%>
                            <input type="hidden" id="PicSrc" />
                            <input type="hidden" name="CoordX" id="CoordX" value="<%=Model.PvmHouse.CoordX %>" />
                            <input type="hidden" name="CoordY" id="CoordY" value="<%=Model.PvmHouse.CoordY %>" />
                            <input type="button" name="button6" id="button6" value="保存" class="btn3 mr10" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="my_dialog" style="display: none;">
                <%Html.RenderAction("RoomMarked", "NHouse"); %>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <!--//data-->
    <% } %>
    <!--异步数据-->
    <script type="text/javascript">
    $(function() {
        var selectbuilding = $("#BuildingId");
        var selectunitid = $("#UnitId");
        var selectSalesStatus = $("#SalesStatus");
        $(selectbuilding).change(function() {
            clearListItems(selectunitid);
            clearListItems($("#dishang"));
            clearListItems($("#dixia"));
            clearListItems(selectSalesStatus);
            if (this.value != 0) {
                ManagerFloors.url = "<%=Url.SiteUrl().GeographyLocation("BuildingFloors") %>?id=" + this.value + "&r=" + Math.random();
                ManagerFloors.GetData();
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Units") %>?id=' + this.value + "&r=" + Math.random(), selectunitid);
                loadGeographyItems('<%= Url.SiteUrl().Common("GetSalesStatusListByBuildingId", "NHouse") %>?id=' + this.value + "&r=" + Math.random(), selectSalesStatus);
                ManagerBuildPic.Get(this.value);
            }
            if (this.value == 0)
                $("#PicSrc").val("");
        });
         //更新户型图
        $("#UpdateRIds").click(function() {
            $("#rviewNo").show();
            $("#rviewOk").hide();
            ManagerRIds.rid = -1;
            ManagerRIds.GetData();
        });
        /*标注房间位置*/
        $("#RoomsMarked").bind("click", function () {
            if (!Inspection.IsNullOrEmpty($("#PicSrc"))) {
                Show_dialog.RoomsMarked();//标注弹层
            } else
                alert("未选中楼栋！");
        });
        ManagerBuildPic.Get("<%=Model.BuildingId %>");//还原楼栋平面图
    });
    //楼层
    var ManagerFloors = {
        url:"",
        GetData: function() {
            $.getJSON(this.url, function (response) {
                if (response && response.success) {
                    $("#dixia").html("");
                    $.each(response.items,function() {
                        if (this.GeographyCode > 0) {
                            $("<option   value='" + this.GeographyCode +"' data='" + this.GeographyName + "' >" + this.GeographyName + "</option>").appendTo($("#dishang"));
                        }else if (this.GeographyCode < 0) {
                            $("<option   value='" + this.GeographyCode +"' data='" + this.GeographyName + "' >" + this.GeographyName + "</option>").prependTo($("#dixia"));
                        }
                    });
                     $("<option  value='0'>选择楼层</option>").prependTo($("#dixia"));
                }
            });
        }
    };
    //户型图
    var ManagerRIds = {
        rid: "<%=Model.RId %>",
        url: "<%=Url.SiteUrl().Common("AjaxGetRIds","NHouse")%>",
        GetData: function() {
            $.getJSON(this.url, { r: Math.random(), id: "<%=Model.PremisesId %>" }, function(response) {
                $("#rviewNo").hide();
                $("#rviewOk").show();
                if (response && response.success) {
                    $("#RId").html("");
                    $.each(response.items, function() {
                        $("<option   value='" + this.ID + "' data='" + this.Path + "' >" + this.Title + "</option>").appendTo($("#RId"));
                    });
                    $("<option  value='-1'>选择户型图</option>").prependTo($("#RId"));
                    $("#RId").val(ManagerRIds.rid);
                } else {
                }
            });
        }
    };
    /*标注房间位置*/
 var Show_dialog = {
        RoomsMarked: function() {
              $("#my_dialog").dialog({
                        modal: true,
                        title: "标注房间位置",
                        draggable: false,
                        width: 730

                    });
        }
    };
 /*获取楼栋平面图*/
 var ManagerBuildPic = {
     url:"<%=Url.SiteUrl().Common("SelectBuildingPic","NHouse")%>",
     Get: function(bid) {
         $.post(this.url, {r:Math.random(),premisesId:"<%=Model.PremisesId %>",buildingId:bid}, function(response) {
             if (response) {
                 $("#PicSrc").val(response);
             }
         });
     }
 };
    </script>
    <!--表单验证-->
    <script type="text/javascript">
        $(function () {
            //submit
            $("#button6").click(function () {
                Inspection.FromSubmit();
                if (Inspection.validresult)
                    $(this).attr("disabled", "disabled");
            });
            //楼层类型
            $("input[name='loucenType']").each(function () {
                $(this).click(function () {
                    if ($(this).val() == 0) {
                        $("#dishang").show();
                        $("#dixia").hide().val(0);
                        $("#dixia").parent().find("span").remove();
                    } else {
                        $("#dishang").hide().val(0);
                        $("#dixia").show();
                        $("#dixia").parent().find("span").remove();
                    }
                });
            });
            $("#dishang").change(function () {
                $("#Floor").val(this.value);
            });
            $("#dixia").change(function () {
                $("#Floor").val(this.value);
            });
        });
        /*验证*/
        var Inspection = {
            validresult: true,
            BuildingId: function () {
                var e = $("#BuildingId");
                if (!this.IsSelected_Diy(e, 0))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            Name: function() {
                var e = $("#Name");
                if (this.IsNullOrEmpty(e)) {
                    this.SetResult_ToParent(e, false, "请填写！");
                    return;
                }
                
                if (5 < e.val().length) {
                    this.SetResult_ToParent(e, false, "不能超过5个汉字");
                    return;
                }
                
//                if (10 < admincoms.StringHelper.getByteCount(e.val())) {
//                    this.SetResult_ToParent(e, false, "不能超过5个汉字");
//                    return;
//                }

                var result = Inspection.NameRepeat();
                if (!result.suc) {
                    this.SetResult_ToParent(e, false, result.msg);
                    return;
                }
                
                this.SetResult_ToParent(e, true, "");
            },
            NameRepeat: function () {
                var result = {};
                $.ajax({
                    url: '<%=Auxiliary.Instance.NhManagerUrl %>nhouse/getishousenamerepeat_update.html',
                    data: { name: $("#Name").val(), buildingid: '<%= Model.PvmHouse.BuildingId %>', id: '<%= Model.PvmHouse.Id %>', unitid: $("#UnitId").val(), ram: Math.random },
                    type: 'POST',
                    cache: false,
                    async: false,
                    success: function(msg) {
                        result = msg;
                    }
                });
                return result;
            },
            UnitId: function () {
                var e = $("#UnitId");
                if (!this.IsSelected(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            LoucenType: function () {
                var e = $("input[name='loucenType']");
                if (!this.IsRadioed(e))
                    this.SetResult_ToPParent(e[0], false, "请填写！");
                else
                    this.SetResult_ToPParent(e[0], true, "");
            },
            Loucen: function () {
                var type = $("input[name='loucenType']:checked").val();
                var e = $("#dishang");
                if (type == 1)
                    e = $("#dixia");
                if (!this.IsSelected(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            Room: function () {
                var e = $("#Room");
                var reg = /^\d+$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确的正数。");
                    else
                        this.Hall();
                }
            },
            Hall: function () {
                var e = $("#Hall");
                var reg = /^\d+$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确的正数。");
                    else
                        this.Toilet();
                }
            },
            Toilet: function () {
                var e = $("#Toilet");
                var reg = /^\d+$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确的正数。");
                    else
                        this.Kitchen();
                }
            },
            Kitchen: function () {
                var e = $("#Kitchen");
                var reg = /^\d+$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确的正数。");
                    else
                        this.SetResult_ToParent(e, true, ""); // this.Balcony();
                }
            }, Balcony: function () {
                var e = $("#Balcony");
                var reg = /^\d+$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确的正数。");
                    else
                        this.SetResult_ToParent(e, true, "");
                }
            },
            BuildingType: function () {
                var e = $("#BuildingType");
                if (!this.IsSelected(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            Orientation: function () {
                var e = $("#Orientation");
                if (!this.IsSelected(e))
                    this.SetResult_ToParent(e, false, "请选择采光情况！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            PriceType: function () {
                var e = $("input[name='PriceType']");
                if (!this.IsRadioed(e))
                    this.SetResult_ToPParent(e[0], false, "请填写！");
                else
                    this.SetResult_ToPParent(e[0], true, "");
            },
            TotalPrice: function () {
                var e = $("#TotalPrice");
                //var reg = /^([1-9]\d{0,8}|0\.\d{1,2}|0|[1-9]\d{0,8}\.\d{1,2})$/;
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else
                        this.SetResult_ToParent(e, true, "");
                }
            },
            SinglePrice: function () {
                var e = $("#SinglePrice");
                //var reg = /^([1-9]\d{0,8}|0\.\d{1,2}|0|[1-9]\d{0,8}\.\d{1,2})$/;
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else
                        this.SetResult_ToParent(e, true, "");
                }
            },
            DownPayment: function () {
                var e = $("#DownPayment");
                //var reg = /^([1-9]\d{0,8}|0\.\d{1,2}|0|[1-9]\d{0,8}\.\d{1,2})$/;
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else
                        this.SetResult_ToParent(e, true, "");
                }
            },
            SalesStatus: function () {
                var e = $("#SalesStatus");
                if (!this.IsSelected_Diy(e, -1))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            BuildingArea: function () {
                var e = $("#BuildingArea");
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else {
                        this.SetResult_ToParent(e, true, "");
                        this.Area();
                    }
                }
            },

            Area: function () {
                var e = $("#Area");
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else {
                        if (parseInt($("#BuildingArea").val()) < parseInt(e.val()))
                            this.SetResult_ToParent(e, false, "使用面积应该小于建筑面积。");
                        else
                            this.SetResult_ToParent(e, true, "");
                    }
                }
            },
            RId: function () {
                var e = $("#RId");
                if (!this.IsSelected_Diy(e, -1))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            }, Coord: function () {
                var e = $("#RoomsMarked");
                var e1 = $("#CoordX");
                var e2 = $("#CoordY");
                // TODO: 临时变动(2014-04-24 去掉约束)
                //                if (e1.val() == 0 && e2.val() == 0)
                //                    this.SetResult_ToParent(e, false, "请填写！");
                //                else
                    this.SetResult_ToParent(e, true, "");
            },
            IsNullOrEmpty: function (e) { //空
                var c = $(e).val();
                return c == null || c == "" || c == undefined ? true : false;
            },
            IsSelected: function (e) { //下拉框
                return $(e).val() == 0 || this.IsNullOrEmpty(e) ? false : true;
            },
            IsSelected_Diy: function (e, n) { //下拉框
                return $(e).val() == n || this.IsNullOrEmpty(e) ? false : true;
            },
            IsRadioed: function (es) { //单选
                var flag = false;
                $(es).each(function (i) {
                    if ($(this).is(":checked"))
                        flag = true;
                });
                return flag;
            },
            LengthLimit: function (e, param) { //长度
                var value = $(e).val();
                var length = value.length;
                for (var i = 0; i < value.length; i++) {
                    if (value.charCodeAt(i) > 127) {
                        length++;
                    }
                }
                return length >= param[0] && length <= param[1];
            },
            SetResult_ToParent: function (e, b, c) { //父级追加
                $(e).parent().find("span").remove();
                b ? $(e).parent().append('<span class="win ml10"></span>') : $(e).parent().append('<span class="lose ml10">' + c + '</span>');
                if (!b) {
                    if (this.validresult)
                        $(e).focus();

                    this.validresult = false;
                }
            },
            SetResult_ToPParent: function (e, b, c) { //父父级追加
                $(e).parent().parent().find("span").remove();
                b ? $(e).parent().parent().append('<span class="win ml10"></span>') : $(e).parent().parent().append('<span class="lose ml10">' + c + '</span>');
                if (!b) {
                    if (this.validresult)
                        $(e).focus();

                    this.validresult = false;
                }
            },
            SetResult_ToAssign: function (a, e, b, c) { //指定区域追加
                $(a).find("span").remove();
                b ? $(a).append('<span class="win ml10"></span>') : $(a).append('<span class="lose ml10">' + c + '</span>');
                if (!b) {
                    if (this.validresult)
                        $(e).focus();

                    this.validresult = false;
                }
            },
            FromSubmit: function () { //提交
                this.validresult = true;
                this.BuildingId();
                this.Name();
                this.UnitId();
                this.LoucenType();
                this.Loucen();
                this.Room();
                this.BuildingType();
                this.Orientation();
                this.PriceType();
                this.TotalPrice();
                this.SinglePrice();
                this.DownPayment();
                this.SalesStatus();
                this.BuildingArea();
                this.RId();
                this.Coord();
                //this.Area();
                if (this.validresult) {
                    //alert("通过");
                    $("#formadd").submit();
                } else {
                    //alert("未通过");
                    //$(".lose.ml10").eq(0).siblings().first().focus();
                }
            }
        };
    </script>
</asp:Content>