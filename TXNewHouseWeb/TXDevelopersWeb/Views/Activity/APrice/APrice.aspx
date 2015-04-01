<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<HouseInfoModel>>" %>

<%@ Import Namespace="TXModel.NHouseActivies.Discount" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-创建一口价
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <h4 class="title_h4 mb10">
            <span>创建一口价</span><em class="ts_right"><a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/APriceList">返回一口价房源列表
            </a></em>
        </h4>
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                第一步：选择楼盘</div>
            <div class="font14 fr mr35">
                <a href="#" id="btn_update01">修改</a></div>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1">
            <tr>
                <th scope="row" width="100" align="right">
                    选择楼盘：
                </th>
                <td id="td_premises01">
                    <select class="select1">
                        <option value="-1">楼盘名称</option>
                        <%if (ViewData["Premise"] != null)
                          {
                              foreach (PremiseModel item in (ViewData["Premise"] as List<PremiseModel>))
                              {%>
                        <option value="<%=item.Id %>" data="<%=item.DeveloperId %>" data-cityid="<%=item.CityId %>">
                            <%=item.Name%></option>
                        <%}
                          }%>
                    </select>
                    <input type="button" value="确定" class="btn_w80 ml10" id="btn_sure01" />
                </td>
                <td id="td_premises02">
                    <span id="span_info02"></span>
                </td>
            </tr>
        </table>
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                第二步：选择房源</div>
            <div class="font14 fr mr35">
                <a id="a_update03" onclick="ShowCondition()" style="cursor: pointer">修改</a></div>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1"
            id="table_condition">
            <tr>
                <th scope="row" width="100" align="right">
                    <span class="font14">筛选：</span>
                </th>
                <td>
                    <div class="shaibox clearFix">
                        <div class="selbox mr10">
                            <span class="pl10" data-buidid="-1">--选择楼栋--</span>
                            <div style="display: none;" class="sel_list w300">
                                <%--<a data="1">1号楼栋</a>
                   <a>2号楼栋</a>--%>
                            </div>
                        </div>
                        <div class="selbox mr10">
                            <span class="pl10" data-unitid="-1">--选择单元--</span>
                            <div style="display: none;" class="sel_list">
                                <%-- <a data="1">1单元</a>
                   <a>2单元</a>--%>
                            </div>
                        </div>
                        <div class="selbox mr10">
                            <span class="pl10" data-floorid="-100">--选择楼层--</span>
                            <div style="display: none;" class="sel_list">
                                <%--<a>1层</a>
                   <a>2层</a>--%>
                            </div>
                        </div>
                        <div class="selbox mr10" style="display: none">
                            <span class="pl10" data-salsestate="-1">--选择状态--</span>
                            <div style="display: none;" class="sel_list">
                                <a data-salsestate="-1">--选择状态--</a> <a data-salsestate="0">未售</a> <a data-salsestate="1">
                                    在售</a> <a data-salsestate="2">售罄</a>
                            </div>
                        </div>
                        <input type="button" value="" class="bnt_serch fl" />
                    </div>
                </td>
            </tr>
        </table>
        <div id="divpages">
            <% Html.RenderPartial("Common/Arranging", Model);%>
        </div>
        <%--所选房源--%>
        <div id="divSelectHouse" class="mt15 clearFix" style="position: relative;">
        </div>
        <%--分页控件--%>
        <div class="fen">
            <div class="tar" id="divPager01">
            </div>
        </div>
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                第三步：设置基本信息</div>
            <div class="font14 fr mr35">
                <a id="btn_update2" style="cursor: pointer">修改</a></div>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1"
            id="table_active">
            <tr>
                <th scope="row" width="100" align="right">
                    一口价：
                </th>
                <td id="td_bidprice1">
                    <input type="text" class="input_wauto w180 mr10" id="txtBidPrice" /><span>万元</span>
                </td>
                <td id="td_bidprice2" style="display: none">
                    <span id="span_bidprice"></span>万元
                </td>
            </tr>
            <%-- <tr>
        <th scope="row" width="150" align="right">减价幅度：元必须为</th>
        <td id="td_addmargin01"><input type="text" class="input_wauto w400" id="txtAddPrice" /><span>元 的整数倍</span></td>
        <td id="td_addmargin02" style="display:none"><span id="span_addmargin02"></span>元 的整数倍</td>
      </tr>
      <tr>
        <th scope="row" width="150" align="right">最大幅度：单次减价不高于</th>
        <td id="td_maxmargin01"><input type="text" class="input_wauto w400" id="txtMaxPrice" /><span>元</span></td>
        <td id="td_maxmargin02" style="display:none"><span id="span_maxmargin"></span>元</td>
      </tr>--%>
            <tr>
                <th scope="row" width="100" align="right">
                    活动保证金金额：
                </th>
                <td id="td_bond01">
                    <input type="text" class="input_wauto w180 mr10" id="txtBond" /><span class="ie_valign_5"
                        id="span_yuan">元</span>
                </td>
                <%--隐藏/显示：活动保证金金额--%>
                <td id="td_bond02" style="display: none">
                    <span id="span_bond02"></span><span class="ie_valign_5">元</span>
                </td>
            </tr>
            <%--<tr>
        <th scope="row" width="100" align="right">报名时间：</th>
        <td id="td_baoming01"><span class="ie_valign_5">开始</span><input id="baomingStart" type="text" class="input_wauto w150 ml5 mr20" onclick="WdatePicker()" /><span class="ie_valign_5">结束</span><input type="text" id="baomingEnd" class="input_wauto w150 ml10" onclick="WdatePicker()"/></td>

        <td id="td_baoming02" style="display:none">
            <span class="ie_valign_5">开始</span><span id="span_baomingStart02"></span><span class="ie_valign_5">结束</span><span id="span_baomingEnd02"></span>
        </td>
      </tr>--%>
            <tr>
                <th scope="row" width="100" align="right">
                    时间：
                </th>
                <td id="td_date01">
                    <span class="ie_valign_5">开始</span>
                    <input id="txtStart" type="text" class="input_wauto w150 ml5 mr20" onfocus="WdatePicker({dateFmt:'yyyy-M-d',minDate:'%y-%M-%d'})"
                        readonly="readonly" />
                    <span class="ie_valign_5">结束</span><input type="text" id="txtEnd" class="input_wauto w150 ml10"
                        onfocus="WdatePicker({dateFmt:'yyyy-M-d',minDate:'#F{$dp.$D(\'txtStart\',{d:0})}',maxDate:'#F{$dp.$D(\'txtStart\',{d:29})}' })"
                        readonly="readonly" />
                </td>
                <%--隐藏/显示：时间--%>
                <td id="td_date02" style="display: none">
                    <span class="ie_valign_5">开始</span><span id="span_start02"></span><span class="ie_valign_5">结束</span><span
                        id="span_end02"></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="button" value="确定" class="btn_w80 ml20 mt15" id="btn_sure02" />
                </td>
            </tr>
        </table>
        <%--完成提交按钮--%>
        <div class="tac">
            <input type="button" value="完成创建" class="btn_w97_green" onclick="Complete()" />
        </div>
    </div>
    <%--由于my97-4.7版本无法跨域，所以先引用根目录js文件--%>
    <script src="<%=TXCommons.GetConfig.BaseUrl + "js/my97datepicker/WdatePicker.js"%>"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftAjax.js")%>"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftMvcAjax.js")%>"
        type="text/javascript"></script>
    <script type="text/javascript">

        var sec = false;

        function DisableSecCheckbox(disablevalue) {
            $("#div_house_list tr td").each(function () {
                $(this).find("[type=checkbox]").attr("disabled", disablevalue);
            });
            $("#divpages").find("[type=checkbox]").attr("disabled", disablevalue);
            $(".bnt_serch").attr("disabled", disablevalue);
        }
        function DisableThreeTxt(disablevalue) {

            $("#txtBidPrice").attr("disabled", disablevalue);
            //$("#txtAddPrice").attr("disabled", disablevalue);
            //$("#txtMaxPrice").attr("disabled", disablevalue);
            $("#txtBond").attr("disabled", disablevalue);
            $("#txtStart").attr("disabled", disablevalue);
            $("#txtEnd").attr("disabled", disablevalue);
            //$("#baomingStart").attr("disabled", disablevalue);
            //$("#baomingEnd").attr("disabled", disablevalue);
            $("#btn_sure02").attr("disabled", disablevalue);
            //$(".btn_w97_green").attr("disabled", disablevalue);

        }

        function SetSelboxClick() {

            $(".selbox").bind("click", function (event) {
                $(this).children(".sel_list").toggle();
            });
            $(".selbox .sel_list").bind("mouseleave", function (event) {
                $(this).hide();
            });
            $(".selbox div").eq(3).find("a").bind("click", function (event) {
                var data = $(this).attr("data-salsestate");
                var span = $(this).parent().prev();
                var pindex = $(this).parent().parent().index();
                span.attr("data-salsestate", data);
                span.html($(this).html());
            });
        }
        function SetSelboxUnClick() {
            $(".selbox").unbind();
            $(".selbox .sel_list").unbind();
            $(".selbox div").eq(3).find("a").unbind();
        }

        function InitThree() {
            $("#txtBidPrice").val('');
            $("#txtAddPrice").val('');
            $("#txtMaxPrice").val('');
            $("#txtBond").val('');
            $("#txtStart").val('');
            $("#txtEnd").val('');
            //$("#baomingStart").val('');
            //$("#baomingEnd").val('');
        }
        function InitSelectHouseArr() {
            jon_Arr = new Array();
            join_len = 0;

            nojoin_Arr = new Array();
            nojoin_len = 0;
        }
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
        $(document).ready(function () {


            jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Home/GetCurrentUserVipState", cache: false,
                success: function (data) {
                    if (data != 1) {
                        alert("您尚未通过身份认证，认证后方可操作。");
                        window.location = '/home/identification';
                    }
                }
            });

            DisableSecCheckbox(true);
            DisableThreeTxt(true);
            $(".btn_w97_green").attr("disabled", true);
            $("#table_condition").hide();
            $("#div_alljoin").hide();
            var ischange = false;
            $(".select1").change(function () {
                ischange = true;
            })

            //确定:1
            $("#btn_sure01").click(function () {

                if (ischange) {
                    InitSelectHouseArr();
                }

                var option = $(".select1  option:selected");
                if (option.val() == "-1") {
                    $(this).next().remove();
                    $(this).after("&nbsp;&nbsp;&nbsp;&nbsp;<span style='color:red'>*请选择楼盘</span>");
                    return;
                }
                $("#td_premises02").show();
                $("#span_info02").html(option.html());
                $("#td_premises01").hide();

                DisableSecCheckbox(false);
                sec = true;

                SetSelboxClick();

                var PremiseId = option.val();
                jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetBuilding", data: { PremisesId: PremiseId, m: Math.random() }, dataType: "json", cache: false,
                    success: function (data) {

                        RemoveBuildOption();
                        RemoveUnitOption();
                        RemoveFloorOption();

                        $.each(data, function (index, Building) {
                            var otherlink = "<a data-buidid='" + Building.Id + "' onclick='SelectOption(this,0);GetUnit(" + PremiseId + "," + Building.Id + ",this);GetFloor2(" + PremiseId + "," + Building.Id + ",this)'>" + Building.Name + Building.NameType + "</a>";
                            $(".selbox").eq(0).find("div").append(otherlink);
                        })
                    }
                });
                QueryHouse();
                //DisableSecCheckbox(true);
                InitThree();
                DisableThreeTxt(true);
                ShowCondition();

                DisableSecCheckbox(true);
                sec = false;
                $(".bnt_serch ").attr("disabled", false);
            })
            //修改1
            $("#btn_update01").click(function () {
                $("#td_premises01").show();
                $("#td_premises02").hide();

                DisableSecCheckbox(true);
                DisableThreeTxt(true);
                sec = false;
                SetSelboxUnClick();
            })
            //确定:2
            $("#btn_sure02").click(function () {
                var bidPrice = $("#txtBidPrice");  //起拍价格
                var bond = $("#txtBond"); //证金金额
                var start = $("#txtStart"); //竞拍开始时间
                var end = $("#txtEnd"); //竞拍结束

                bidPrice.next().next().remove();
                bond.next().next().remove();
                end.next().remove();

                if (Verify.IsEmpty(bidPrice.val())) return Verify.Error("一口价价格不能为空", bidPrice.next());
                if (!Verify.Integer2(bidPrice.val())) return Verify.Error("最多可输入9位正数", bidPrice.next());
                if (Verify.IsEmpty(bond.val())) return Verify.Error("活动保证金不能为空", bond.next());
                if (!Verify.Integer2(bond.val())) return Verify.Error("最多可输入9位正数", bond.next());


                if (!Verify.IsDate(start.val())) return Verify.Error("日期不能为空", end);
                if (!Verify.IsDate(end.val())) return Verify.Error("日期不能为空", end);
                //if (!Verify.Compare(start.val(), end.val())) return Verify.Error("日期不正确", end);
                //if (!Verify.Compare2(start.val(), end.val())) return Verify.Error("活动时间段最小范围为1天，最大范围为30天", end);

                $("#td_bidprice1").hide();
                $("#td_bidprice2").show();
                $("#span_bidprice").html(bidPrice.val());

                $("#td_bond01").hide();
                $("#td_bond02").show();
                $("#span_bond02").html(bond.val());

                $("#td_baoming01").hide();
                $("#td_baoming02").show();

                $("#td_date01").hide();
                $("#td_date02").show();
                $("#span_start02").html(start.val());
                $("#span_end02").html(end.val());

                $(".btn_w97_green").attr("disabled", false);
                $(this).hide();

            })
            //修改2
            $("#btn_update2").click(function () {
                Update02();
            })
            var saleState = $(".selbox span").eq(3).attr("data-salsestate");  //状态
            $(".bnt_serch").click(function () {
                QueryHouse();

                InitThree();
                DisableThreeTxt(true);
                ShowCondition();
            })
        })
        function Update02() {
            $("#td_bidprice1").show();
            $("#td_bidprice2").hide();

            $("#td_bond01").show();
            $("#td_bond02").hide();

            $("#td_date01").show();
            $("#td_date02").hide();

            $("#td_baoming01").show();
            $("#td_baoming02").hide();

            $("#btn_sure02").show();
            $(".btn_w97_green").attr("disabled", true);
        }

        //楼盘
        function GetPremise() {
            var PremiseId = $(this).find("option:selected").val();
            jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetBuilding", data: { PremisesId: PremiseId, m: Math.random() }, dataType: "json", cache: false,
                success: function (data) {

                    RemoveBuildOption();
                    RemoveUnitOption();
                    RemoveFloorOption();

                    $.each(data, function (index, Building) {
                        var otherlink = "<a data-buidid='" + Building.Id + "' onclick='SelectOption(this,0);GetUnit(" + PremiseId + "," + Building.Id + ",this);GetFloor2(" + PremiseId + "," + Building.Id + ",this)'>" + Building.Name + "</a>";
                        $(".selbox").eq(0).find("div").append(otherlink);
                    })
                }
            });
            QueryHouse();
            DisableSecCheckbox(true);
            InitThree();
            DisableThreeTxt(true);
            ShowCondition();
        }

        //显示选中的下拉项
        function SelectOption(alink, i) {
            var dataAttr = '';
            if (i == 0) {
                dataAttr = "data-buidid";
            }
            else if (i == 1) {
                dataAttr = "data-UnitId";
            } else if (i == 2) {
                dataAttr = "data-floorid";
            }
            var data = $(alink).attr(dataAttr);
            var span = $(alink).parent().prev();
            span.attr(dataAttr, data);
            span.html($(alink).html());
            return false;
        }
        function QueryHouse() {
            var premisesId = $(".select1  option:selected").val(); //楼盘Id
            var buildId = $(".selbox span").eq(0).attr("data-buidid"); //楼栋
            var unitId = $(".selbox span").eq(1).attr("data-unitid");  //单元
            var floorId = $(".selbox span").eq(2).attr("data-floorid");    //楼层
            //var saleState = $(".selbox span").eq(3).attr("data-salsestate");  //状态
            var saleState = 2;  //状态
            jQuery.ajax({ type: "get", async: false, url: mainUrl + "/Activity/APrice", data: { premisesId: premisesId, buildId: buildId, unitId: unitId, floorId: floorId, saleState: saleState, m: Math.random() }, cache: false,
                success: function (data) {
                    $("#divpages").html(data);
                }
            });
        }
        function RemoveBuildOption() {
            $(".selbox").eq(0).find("div").children().remove();
            $(".selbox").eq(0).find("span").attr("data-buidid", "-1").html("选择楼栋");
            var firstlink = "<a data-buidid='-1' onclick='SelectOption(this,0);RemoveUnitOption();RemoveFloorOption();'>选择楼栋</a>";
            $(".selbox").eq(0).find("div").append(firstlink);
        }
        function RemoveUnitOption() {
            //移除原有标签
            $(".selbox").eq(1).find("div").children().remove();
            $(".selbox").eq(1).find("span").attr("data-unitid", "-1").html("选择单元");
            //首个a标签
            var firstlink = "<a data-unitid='-1'  onclick='SelectOption(this,1);RemoveFloorOption()'>选择单元</a>";
            $(".selbox").eq(1).find("div").append(firstlink);
        }
        function RemoveFloorOption() {
            $(".selbox").eq(2).find("div").children().remove();
            $(".selbox").eq(2).find("span").attr("data-floorid", "-100").html("选择楼层");
            //首个a标签
            var firstlink = "<a data-floorid='-100'  onclick='return SelectOption(this,2)'>选择楼层</a>";
            $(".selbox").eq(2).find("div").append(firstlink);
        }
        //单元
        function GetUnit(PremiseId, BuildId, alink) {
            jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetUnit", data: { PremisesId: PremiseId, BuildingId: BuildId, m: Math.random() }, dataType: "json", cache: false,
                success: function (data) {

                    RemoveUnitOption();

                    $.each(data, function (index, Unit) {
                        var otherlink = "<a data-unitid='" + Unit.Id + "' onclick='SelectOption(this,2);GetFloor(" + PremiseId + "," + BuildId + "," + Unit.Id + ",this)'>" + Unit.Name + "</a>";
                        $(".selbox").eq(1).find("div").append(otherlink);
                    })
                }
            });

            //显示选中项
            SelectOption(alink, 0);

            return false;
        }
        //楼层
        function GetFloor(PremisesId, BuildingId, UnitId, alink) {

            jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetFloor", data: { PremisesId: PremisesId, BuildingId: BuildingId, UnitId: UnitId, m: Math.random() }, dataType: "json", cache: false,
                success: function (data) {

                    RemoveFloorOption();

                    $.each(data, function (index, Floor) {
                        var otherlink = "<a data-floorid='" + Floor + "' onclick='return SelectOption(this,2)'>" + Floor + "层</a>";
                        $(".selbox").eq(2).find("div").append(otherlink);
                    })
                }
            });

            //显示选中项
            SelectOption(alink, 1);

            return false;
        }
        function GetFloor2(PremisesId, BuildingId) {
            jQuery.ajax({ type: "post", async: false, url: mainUrl + "/Activity/GetFloor2", data: { PremisesId: PremisesId, BuildingId: BuildingId, m: Math.random() }, dataType: "json", cache: false,
                success: function (data) {

                    RemoveFloorOption();

                    $.each(data, function (index, Floor) {
                        var otherlink = "<a data-floorid='" + Floor + "' onclick='return SelectOption(this,2)'>" + Floor + "层</a>";
                        $(".selbox").eq(2).find("div").append(otherlink);
                    })
                }
            });

            //显示选中项
            //SelectOption(alink, 1);

            return false;
        }

        var jon_Arr = new Array();
        var join_len = 0;

        var nojoin_Arr = new Array();
        var nojoin_len = 0;
        //全选/反选
        function CheckAll() {
            var CheckInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") }
            var JoinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" }

            var box_every = CheckInfo.checkbox_every;
            var ischecked = CheckInfo.checkbox_all.prop('checked') ? true : false;

            $(box_every).prop("checked", ischecked).attr("join-data", 0); //[每个选择]

            JoinInfo.a_alljoin.html(JoinInfo.join_msg).attr("join-data", 0); //[所有参加]

            JoinInfo.a_singlejoin.html(JoinInfo.join_msg); //[单个参加]

        }
        //单个选择
        function CheckSingle(checkbox) {
            var CheckInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") }
            var JoinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" }

            var check = $(checkbox).prop('checked');
            if (!check) {
                $(checkbox).attr("join-data", 0);
                $(checkbox).parent().nextAll().find(".alinkjoin").html(JoinInfo.join_msg);

                var houseid = $(checkbox).val();
                var index = IsInArray(jon_Arr, houseid);
                if (index != -1) {
                    jon_Arr.splice(index, 1);
                    join_len--;
                }
                var index0 = IsInArray(nojoin_Arr, houseid);
                if (index0 != -1) {
                    nojoin_Arr.splice(index0, 1);
                    nojoin_len--;
                }

            } else if (check) {
                var houseid = $(checkbox).val();
                var index = IsInArray(nojoin_Arr, houseid);
                if (index == -1) {
                    nojoin_Arr[nojoin_len++] = CreateHouse(checkbox);
                }
            }
        }
        function CreateHouse(checkbox) {
            var house = new Object();
            house.Houseid = $(checkbox).val();
            house.HouseName = $(checkbox).attr("housename");
            house.Developid = $(checkbox).attr("developid");
            house.Premid = $(checkbox).attr("premid");
            house.Buildid = $(checkbox).attr("buildid");
            house.Floor = $(checkbox).parent().nextAll().eq(0).html();
            house.UnitId = $(checkbox).parent().nextAll().eq(1).html();
            house.HouseNo = $(checkbox).parent().nextAll().eq(2).html();
            house.Room = $(checkbox).parent().nextAll().eq(3).html();
            house.BuildingArea = $(checkbox).parent().nextAll().eq(4).html();
            house.SinglePrice = $(checkbox).parent().nextAll().eq(5).html();
            house.TotalPrice = $(checkbox).parent().nextAll().eq(6).html();
            house.SalesStatus = $(checkbox).parent().nextAll().eq(7).html();
            house.CreateTime = $(checkbox).parent().nextAll().eq(8).html();
            house.Join = $(checkbox).parent().nextAll().eq(9).html();
            return house;
        }
        //判断元素是否存在二维数组
        function IsInArray(Arr, val) {
            var index = -1;
            for (var k = 0; k < Arr.length; k++) {
                var house = Arr[k];
                if (house.Houseid == val) {
                    index = k;
                    break;
                }
            }
            return index;
        }
        //全参加
        function JoinAll() {
            if (sec == false) return;

            var CheckInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") }
            var JoinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" }

            var count = 0;
            var join = JoinInfo.a_alljoin.attr("join-data") == "0" ? false : true;
            CheckInfo.checkbox_every.each(function () {
                if ($(this).prop('checked')) {
                    count++;
                }
            });
            if (count == 0) {
                alert("请选择房源");
                return;
            }

            //-->参加
            if (!join) {
                JoinInfo.a_alljoin.attr("join-data", 1).html(JoinInfo.no_join_msg);
                CheckInfo.checkbox_every.each(function () {
                    if ($(this).prop('checked')) {
                        $(this).attr("join-data", 1);
                        $(this).parent().nextAll().find(".alinkjoin").html(JoinInfo.no_join_msg);

                        //添加houseId到jon_Arr数组
                        var houseid = $(this).val();
                        var index = IsInArray(jon_Arr, houseid);
                        if (index == -1) {
                            jon_Arr[join_len++] = CreateHouse(this);
                        }
                        //移除nojoin_Arr数组houseId
                        var index0 = IsInArray(nojoin_Arr, houseid);
                        if (index0 != -1) {
                            nojoin_Arr.splice(index0, 1);
                            nojoin_len--;
                        }
                    }
                })
            } else {//-->不参加
                JoinInfo.a_alljoin.attr("join-data", 0).html(JoinInfo.join_msg);
                CheckInfo.checkbox_every.each(function () {
                    if ($(this).prop('checked')) {
                        $(this).attr("join-data", 0);
                        $(this).parent().nextAll().find(".alinkjoin").html(JoinInfo.join_msg);

                        var houseid = $(this).val();
                        var index = IsInArray(nojoin_Arr, houseid);
                        if (index == -1) {
                            nojoin_Arr[nojoin_len++] = CreateHouse(this);
                        }
                        var index0 = IsInArray(jon_Arr, houseid);
                        if (index0 != -1) {
                            jon_Arr.splice(index0, 1);
                            join_len--;
                        }
                    }
                })
            }
        }
        Tool = {
            DataFormt: function (oldstr) {
                return oldstr.replace(/-/g, "/");
            }
        };
        //单个参加
        function JoinSingle(alink) {

            //if (sec == false) return;

            var CheckInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") }
            var JoinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" }

            var join = $(alink).html();
            var checkbox = $(alink).parent().prevAll().find("input[name='box']");

            //            if (!checkbox.prop('checked')) {
            //                alert("请选择房源");
            //                return;
            //            } else {
            //[参加]
            if (join == JoinInfo.join_msg) {

                if (jon_Arr.length == 0) {//只能是一个房源参加活动

                    checkbox.attr("join-data", 1);
                    $(alink).html(JoinInfo.no_join_msg);

                    var houseid = $(checkbox).val();
                    var index = IsInArray(jon_Arr, houseid);
                    if (index == -1) {
                        jon_Arr[join_len++] = CreateHouse(checkbox);
                    }

                    var index0 = IsInArray(nojoin_Arr, houseid);
                    if (index0 != -1) {
                        nojoin_Arr.splice(index0, 1);
                        nojoin_len--;
                    }
                } else {
                    alert("只能选择一个房源参加活动");
                }
            }
            //[不参加]
            else {
                checkbox.attr("join-data", 0);
                $(alink).html(JoinInfo.join_msg);

                var houseid = $(checkbox).val();
                var index0 = IsInArray(jon_Arr, houseid);
                if (index0 != -1) {
                    jon_Arr.splice(index0, 1);
                    join_len--;
                }
                var index = IsInArray(nojoin_Arr, houseid);
                if (index == -1) {
                    nojoin_Arr[nojoin_len++] = CreateHouse(checkbox);
                }
            }
            //            }
        }
        //验证
        var Verify = {
            IsEmpty: function (value) {
                return ($.trim(value) == "" || value == undefined || value == null);
            },
            Length: function (Min, Value, Max) {
                return (Min > Value.length || Value.length > Max);
            },
            Integer: function (Value) {
                var reg = /^[1-9]([0-9])*$/;
                return reg.test(Value);
            },
            Integer2: function (Value) {
                var reg = /^[1-9]{1}\d{0,8}$/;
                return reg.test(Value);
            },
            Error: function (Msg, TargetObj) {
                TargetObj.after("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*" + Msg + "</span>");
                return false;
            },
            Error2: function (Msg, TargetObj) {
                TargetObj.html("<span style='color:red;'>&nbsp;&nbsp;&nbsp;&nbsp;*" + Msg + "</span>");
                return false;
            },
            IsDate: function (date) {
                var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
                return reg.test(date);
            },
            Compare: function (mindate, bigdate) {
                return (new Date(Tool.DataFormt(bigdate))) > (new Date(Tool.DataFormt(mindate)));
            },
            Compare2: function (mindate, bigdate) {
                var dateMin = new Date(Tool.DataFormt(mindate));
                var dateMax = new Date(Tool.DataFormt(bigdate));
                if (!dateMax || !dateMax)
                    return false;
                var diff = (dateMax.getTime() - dateMin.getTime()) / (1000 * 3600 * 24);
                return (diff >= 1 && diff <= 30);
            },
            IsDouble: function (value) {
                var reg = /(^\d{1}\.\d{1,2}$)|(^[1-9]{1}$)/;
                return reg.test(value);
            }
        }

        function Complete() {
            var cityId = $(".select1  option:selected").attr("data-cityid"); //城市Id
            var premisesId = $(".select1  option:selected").val(); //楼盘Id
            var bidPrice = $("#txtBidPrice");  //起拍价格
            var bond = $("#txtBond"); //证金金额
            var start = $("#txtStart"); //竞拍开始时间
            var end = $("#txtEnd"); //竞拍结束
            var activState = 0; //活动开始状态

            var s = new Date(Tool.DataFormt(start.val()));
            var curDate = new Date();
            var nextDate = new Date((curDate / 1000 + 86400 * 1) * 1000); //当前时间+1天

            if ((s <= curDate) && (curDate <= nextDate)) {
                activState = 1;
            }

            var house = ""; //参加房源Id
            for (var i = 0; i < jon_Arr.length; i++) {
                var h = jon_Arr[i];
                if (!isNaN(h.Houseid))
                    house += h.Houseid.toString() + ",";
            }
            bidPrice.next().next().remove();
            bond.next().next().remove();
            end.next().remove();

            if (Verify.IsEmpty(bidPrice.val())) return Verify.Error("一口价价格不能为空", bidPrice.next());
            if (!Verify.Integer2(bidPrice.val())) return Verify.Error("最多可输入9位正数", bidPrice.next());
            if (Verify.IsEmpty(bond.val())) return Verify.Error("活动保证金不能为空", bond.next());
            if (!Verify.Integer2(bond.val())) return Verify.Error("最多可输入9位正数", bond.next());

            if (!Verify.IsDate(start.val())) return Verify.Error("日期不能为空", end);
            if (!Verify.IsDate(end.val())) return Verify.Error("日期不能为空", end);
            //if (!Verify.Compare(start.val(), end.val())) return Verify.Error("日期不正确", end);
            //if (!Verify.Compare2(start.val(), end.val())) return Verify.Error("活动时间段最小范围为1天，最大范围为30天", end);


            house = house.substr(0, house.length - 1);
            if (house == "") { alert("请选择房源"); return; }

            $.ajax({
                url: "/Activity/CreateAPrice",
                type: 'post',
                cache: false,
                dataType: "json",
                async: false,
                data: { cityId: cityId, premisesId: premisesId, bidPrice: bidPrice.val(), bond: bond.val(), start: start.val(), end: end.val(), Ids: house, activState: activState, m: Math.random() },
                success: function (data) {
                    if (data.sucess != "0") {
                        window.location = "<%=TXCommons.GetConfig.BaseUrl%>" + "Activity/APriceList";
                        //                        ShowCondition();
                        //                        QueryHouse();
                        //                        DisableThreeTxt(false);
                        //                        Update02();
                    }
                    else {
                        alert("创建失败!");
                    }
                }
            });
        }
        //返回上一页：文本框是否选中
        function SetCheckBox() {
            if (sec == false) {
                DisableSecCheckbox(true);
            }
            var CheckInfo = { checkbox_all: $("#checkbox-all"), checkbox_every: $("input[name = box]:checkbox") }
            var JoinInfo = { a_alljoin: $("#a-alljoin"), a_singlejoin: $(".alinkjoin"), join_msg: "参加", no_join_msg: "不参加" }
            CheckInfo.checkbox_every.each(function () {
                var houseid = $(this).val();
                var index = IsInArray(jon_Arr, houseid);
                if (index != -1) {
                    $(this).prop('checked', true).attr("join-data", 1);
                    $(this).parent().nextAll().find(".alinkjoin").html(JoinInfo.no_join_msg);
                } else {
                    var index0 = IsInArray(nojoin_Arr, houseid);
                    if (index0 != -1) {
                        $(this).prop('checked', true).attr("join-data", 0);
                        $(this).parent().nextAll().find(".alinkjoin").html(JoinInfo.join_msg);
                    }
                }
            })
        }
        //【完成设置】
        function CompleteSet() {
            DisableThreeTxt(false);

            $("#table_condition").hide();
            $("#div_alljoin").hide();
            $("#div_alljoin").hide();
            $("#divpages").hide();

            $("#divPager01").show();
            $("#divSelectHouse").show();
            $("#divPager01").html("");
            $("#divSelectHouse").html("");

            totalCount = jon_Arr.length;
            goPage(1);
        }
        //        function GetTable(arr) {
        //            var str = "";
        //            var len = arr.length;
        //            if (len > 0) {
        //                str += "     <table width='100%' border='0' cellspacing='0' cellpadding='0' class='tab1'>";
        //                str += "          <tr><th>房号</th><th>楼层</th><th>楼号</th><th>户型</th><th>建筑面积</th><th>参考单价</th><th>参考总价</th><th>销售状况</th><th>发布日期</th><th>操作</th></tr>";

        //                for (var i = 0; i < arr.length; i++) {
        //                    var o = new Object();
        //                    o = arr[i];
        //                    str += "<tr>";
        //                    str += "<td>" + o.Houseid + "</td>";
        //                    str += "<td>" + o.Floor + "层</td>";
        //                    str += "<td>" + o.HouseNo + "号楼</td>";
        //                    str += "<td>" + o.Room + "</td>";
        //                    str += "<td>" + o.BuildingArea + "平方米</td>";
        //                    str += "<td>" + o.SinglePrice + "元/平方米</td>";
        //                    str += "<td>" + o.TotalPrice + "万元/套</td>";
        //                    str += "<td>" + o.SalesStatus + "</td>";
        //                    str += "<td>" + o.CreateTime + "</td>";
        //                    //str += "<td><a onclick='del(this);'>删除</a></td>";
        //                    str += "<td>" + "参加" + "</td>";
        //                    str += "</tr>";
        //                }

        //                str += "       </table>";
        //            }
        //            return str;
        //        }
        //【修改】
        function ShowCondition() {
            $("#table_condition").show();
            $("#div_alljoin").show();
            $("#div_alljoin").show();
            $("#divpages").show();
            $("#divSelectHouse").hide();
            $("#divPager01").hide();

            DisableThreeTxt(true);


            $("#td_bidprice1").show();
            $("#td_bidprice2").hide();

            $("#td_addmargin01").show();
            $("#td_addmargin02").hide();

            $("#td_maxmargin01").show();
            $("#td_maxmargin02").hide();

            $("#td_bond01").show();
            $("#td_bond02").hide();

            $("#td_date01").show();
            $("#td_date02").hide();
        }
        /*-------------------------------JavaScript分页代码开始------------------------------
        js分页
        el:分页容器 
        count:总记录数 
        pageStep:每页显示多少个 
        pageNum:第几页 
        fnGo:分页跳转函数
        */
        var jsPage = function (el, count, pageStep, pageNum, fnGo) {
            var divPage = $("#" + el)
            this.getLink = function (fnGo, index, pageNum, text) {
                var s = '<a style=' + "cursor:pointer" + ' onclick="' + fnGo + '(' + index + ');" ';
                if (index == pageNum) {
                    s += 'class="hover" ';
                }
                text = text || index;
                s += '>' + text + '</a> ';
                return s;
            }
            //总页数
            var pageNumAll = Math.ceil(count / pageStep);

            if (pageNumAll == 1 || pageNumAll == 0) {
                divPage.html("");
                return "";
            }
            var itemNum = 5; //当前页左右两边显示个数
            pageNum = Math.max(pageNum, 1);
            pageNum = Math.min(pageNum, pageNumAll);
            var s = '';
            if (pageNum > 1) {
                s += this.getLink(fnGo, pageNum - 1, pageNum, '<<');
            } else {
                s += '<a class="no"><<</a> ';
            }
            var begin = 1;
            if (pageNum - itemNum > 1) {
                s += this.getLink(fnGo, 1, pageNum) + '... ';
                begin = pageNum - itemNum;
            }
            var end = Math.min(pageNumAll, begin + itemNum * 2);
            if (end == pageNumAll - 1) {
                end = pageNumAll;
            }
            for (var i = begin; i <= end; i++) {
                s += this.getLink(fnGo, i, pageNum);
            }
            if (end < pageNumAll) {
                s += '... ' + this.getLink(fnGo, pageNumAll, pageNum);
            }
            if (pageNum < pageNumAll) {
                s += this.getLink(fnGo, pageNum + 1, pageNum, '>>');
            } else {
                s += '<a>>></a> ';
            }
            return s;
        }

        var pageSzie = 5, totalCount = 0;
        var getTable = function (arrTh, arrTr) {
            var s = "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='tab1'>";
            s += '<tr>';
            for (var i = 0; i < arrTh.length; i++) {
                s += '<th>' + arrTh[i] + '</th>';
            }
            s += '</tr>';
            for (var i = 0; i < arrTr.length; i++) {
                var o = new Object();
                o = arrTr[i];
                if (o != undefined) {
                    s += "<tr>";
                    s += "<td>" + o.HouseName + "</td>";
                    s += "<td>" + o.Floor + "</td>";
                    s += "<td>" + o.UnitId + "</td>";
                    s += "<td>" + o.HouseNo + "</td>";                   
                    s += "<td>" + o.Room + "</td>";
                    s += "<td>" + o.BuildingArea + "</td>";
                    s += "<td>" + o.SinglePrice + "</td>";
                    s += "<td>" + o.TotalPrice + "</td>";
                    s += "<td>" + o.SalesStatus + "</td>";
                    s += "<td>" + o.CreateTime + "</td>";
                    //s += "<td><a onclick='del(this);'>删除</a></td>";
                    s += "<td>" + "参加" + "</td>";
                    s += "</tr>";
                }
            }
            s += '</table>';
            return s;
        }
        function goPage(pageIndex) {
            var arrTh = ['房号', '楼层', '单元', '楼号', '户型', '建筑面积', '单价', '总价', '销售状态', '发布日期', '操作'];
            var arrTr = [];
            for (var i = (pageIndex - 1) * pageSzie; i < pageSzie * pageIndex; i++) {
                arrTr.push(jon_Arr[i]);
            }

            var html = getTable(arrTh, arrTr);
            var re = new RegExp("<tr>", "g");
            var arr = html.match(re);

            if (arr.length == 1) {

                var str = "<tr><td colspan='10'>没有数据请重新选择</td></tr>";
                html = html.replace("</table>", "");
                html += str;
                html += "</table>";
                document.getElementById('divSelectHouse').innerHTML += str;
                InitThree();
                DisableThreeTxt(true);
            }

            document.getElementById('divSelectHouse').innerHTML = html;
            document.getElementById('divPager01').innerHTML = jsPage('divPager01', totalCount, pageSzie, pageIndex, 'goPage');


        }
    </script>
</asp:Content>
