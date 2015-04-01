<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXOrm.Building>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-编辑楼栋
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <form id="BuildingForm" method="post">
        <h4 class="title_h4 mb10">
            <span>修改楼栋信息</span><em class="ts_right"></em>
        </h4>
        <div class="mt5">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="16%">
                        选择楼盘：
                    </th>
                    <td width="84%">
                        <%=Html.DropDownList("PremisesId", (IEnumerable<SelectListItem>)ViewData["PremisList"], new { @class = "selcss w200" })%>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btit clearFix mt15">
            <div class="tit_font fl">
                楼栋信息</div>
            <div class="font12 fr mr35">
                <span class="red">*</span>为必填项
            </div>
        </div>
        <div class="mt15 mb15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="16%">
                        <span class="mr2 red">*</span>楼栋名称：
                    </th>
                    <td width="84%">
                        <input type="text" class="txt22 w100" id="BuildingName" name="BuildingName" value="<%=Model.Name %>" />
                        <select class="selcss w100" id="NameType" name="NameType">
                            <option value="-1">请选择</option>
                            <option value="1" text="号楼">号楼</option>
                            <option value="2" text="栋">栋</option>
                            <option value="3" text="座">座</option>
                            <option value="4" text="幢">幢</option>
                        </select>
                        <span class="col999 ml20">（例如：1号楼,填写"1"）</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>楼盘期数：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="Periods" name="Periods" value="<%=Model.Periods%>" />
                        期
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>单元数量：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" value="" id="UnitNum" name="UnitNum" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>单元名称：
                    </th>
                    <td>
                        <span id="UnitInput"></span>
                        <%--1=
                        <input type="text" class="txt22 w100 mr15 ml5" value="例如：1单元" />
                        2=
                        <input type="text" class="txt22 w100 mr15 ml5" value="例如：2单元" />
                        3=
                        <input type="text" class="txt22 w100 mr15 ml5" value="例如：3单元" />
                        4=
                        <input type="text" class="txt22 w100 mr15 ml5" value="例如：4单元" />--%>
                    </td>
                </tr>
                <tr>
                    <th>
                        物业类型：
                    </th>
                    <td>
                        <input type="checkbox" class="bnt_checkbox" name="PropertyType" value="1" />
                        住宅
                        <input type="checkbox" class="bnt_checkbox" name="PropertyType" value="2" />
                        写字楼
                        <input type="checkbox" class="bnt_checkbox" name="PropertyType" value="3" />
                        别墅
                        <input type="checkbox" class="bnt_checkbox" name="PropertyType" value="4" />
                        商业
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>开盘时间：
                    </th>
                    <td>
                        <input id="OpeningTime" name="OpeningTime" type="text" onclick="WdatePicker()" class="txt22 w200 col999"
                            value="<%=Model.OpeningTime.ToString("yyyy-MM-dd") %>" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>入住时间：
                    </th>
                    <td>
                        <input id="OthersTime" name="OthersTime" type="text" onclick="WdatePicker()" class="txt22 w200 col999"
                            value="<%=Model.OthersTime.ToString("yyyy-MM-dd") %>" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>户数：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="FamilyNum" name="FamilyNum" value="<%=Model.FamilyNum %>" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>地上楼层：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="TheFloor" name="TheFloor" value="<%=Model.TheFloor %>" />
                        层
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>地下楼层：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="Underloor" name="Underloor" value="<%=Model.Underloor %>" />
                        层
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>梯户配比：
                    </th>
                    <td>
                        <select class="selcss w200" id="Ladder" name="Ladder">
                            <option value="-1">请选择</option>
                            <option value="1">1梯1户</option>
                            <option value="2">1梯2户</option>
                            <option value="3">1梯3户</option>
                            <option value="4">1梯4户</option>
                            <option value="5">1梯5户</option>
                            <option value="6">1梯6户</option>
                            <option value="7">2梯1户</option>
                            <option value="8">2梯2户</option>
                            <option value="9">2梯3户</option>
                            <option value="10">2梯4户</option>
                            <option value="11">2梯5户</option>
                            <option value="12">2梯6户</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>装修：
                    </th>
                    <td>
                        <select class="selcss w200" id="Renovation" name="Renovation">
                            <option value="-1">请选择</option>
                            <option value="1">毛胚</option>
                            <option value="2">简装修</option>
                            <option value="3">精装修</option>
                            <option value="4">菜单式装修</option>
                            <option value="5">公共部分精装修</option>
                            <option value="6">全装修</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>楼栋所处楼盘位置：
                    </th>
                    <td>
                        <select class="selcss w200" id="BuildingPosition" name="BuildingPosition">
                            <option value="-1">请选择</option>
                            <option value="1">临街</option>
                            <option value="2">中部</option>
                            <option value="3">东部</option>
                            <option value="4">西部</option>
                            <option value="5">南部</option>
                            <option value="6">北部</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>楼栋平面图：
                    </th>
                    <td>
                        <select class="selcss w200" id="PictureData" name="PictureData">
                            <option value="-1">选择楼栋平面图</option>
                        </select>
                        <input type="button" id="UpdatePic" class="bnt_w123 ml10" value="更新楼栋平面图" /><a id="UploadBuildPic"
                            href="javascript:void(0);" class="ml10">上传楼栋平面图</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>状态：
                    </th>
                    <td>
                        <select class="selcss w100" id="State" name="State">
                            <%--<option value="-1">请选择</option>
                            <option value="1">待售</option>
                            <option value="2">在售</option>
                            <option value="3">售罄</option>
                            <option value="4">建设中</option>
                            <option value="5">规划中</option>--%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>预售许可证：
                    </th>
                    <td>
                        <select class="selcss w200" id="PresaleId" name="PresaleId">
                            <option value="-1">请选择预售许可证</option>
                        </select>
                        <input id="PresaleName" name="PresaleName" type="text" class="txt22 w300 ml10 col999"
                            value="若选项中没有请在此输入预售许可证号" maxlength="20" />
                        <span id="err" style="display: none;" class="ie_valign_5 ts">许可证最多可输入10个，请选择！</span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="mt15">
        </div>
        <div class="btit mt15">
            <div class="tit_font">
                楼栋沙盘信息关联
            </div>
        </div>
        <div class="mt15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="16%">
                        <span class="mr2 red">*</span>沙盘楼栋编号：
                    </th>
                    <td width="84%">
                        <select class="selcss w200" id="PNum" name="PNum">
                            <option value="-1">请选择楼栋编号</option>
                        </select>
                        <span class="col999 ml10">关联上之后用户即可通过楼盘首页-楼盘沙盘了解楼栋概况</span>
                    </td>
                </tr>
            </table>
            <div class="boxcent20 mt15">
                <input type="submit" class="bnt_w120" value="保存" /></div>
        </div>
        <input type="hidden" id="PermitPresale" name="PermitPresale" />
        <input type="hidden" id="TypeN" name="TypeN" />
        </form>
        <!-- InstanceEndEditable -->
    </div>
    <%--由于my97-4.7版本无法跨域，所以先引用根目录js文件--%>
    <script src="<%=TXCommons.GetConfig.BaseUrl + "js/my97datepicker/WdatePicker.js"%>"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.js")%>"
        language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";

        $("#PremisesId").attr("disabled", "disabled");

        $("#PremisesId").val('<%=Model.PremisesId %>');

        $("#NameType option[text='<%=Model.NameType%>']").attr("selected", true);

        var ProperTY = '<%=Model.PropertyType %>';
        $("input[name='PropertyType']:checkbox").each(function () {
            if (ProperTY.indexOf($(this).val()) >= 0) {
                $(this).prop("checked", true);
            }
        });

        $('#PresaleName').focus(function () {
            var text = $("#PresaleName").val();
            if (text == "若选项中没有请在此输入预售许可证号") {
                $("#PresaleName").val("");
            }
        });

        $('#PresaleName').blur(function () {
            var str = $(this).val();
            str = $.trim(str);
            if (str == "") {
                $('#PresaleName').val("若选项中没有请在此输入预售许可证号");
            }
        });

        function GetUnit() {
            var count = 0;
            $("#PresaleId").empty();
            $.ajax({
                type: "POST",
                url: mainUrl + "NhBuilding/PermitPresaleList",
                data: "PremisesId=" + $("#PremisesId").val(),
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        count = i;
                        //$("<option></option>").val(item.Id).text(item.Name).appendTo($("#PresaleId"));
                        $('#PresaleId').append("<input id=PremisesId" + item.Id + " type=\"text\" class=\"txt22 w200 mr15 ml5\" name=\"PremisesId\" value=" + item.Name + " /><br/>");
                    });
                    //许可证最多允许10个，多余后不可进行添加
                    if (count > 8) {
                        $('#PresaleName').attr("readonly", "readonly");
                        $("#err").show();
                    }
                }
            });

            $("#UnitId").empty();
            $.ajax({
                type: "POST",
                url: mainUrl + "/NHouse/GetUnitList",
                data: "PremisesId=" + $("#PremisesId").val() + "&BuildingId=" + '<%=Model.Id %>',
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        i++;
                        // $("<option></option>").val(item.Id).text(item.Name).appendTo($("#UnitId"));
                        $('#UnitInput').append(i + "=<input value=" + item.Name + " id=UnitName" + i + " maxlength=\"5\" type=\"text\" class=\"txt22 w100 mr15 ml5\" name=\"UnitName\"/>");
                    });
                    $("#UnitNum").val(msg.length);
                }
            });
        }

        //沙盘楼栋编号
        function GetSandBoxList(PremisesId) {
            $("#PNum").empty();
            $("<option></option>").val("-1").text("请选择楼栋编号").appendTo($("#PNum"));
            $.ajax({
                type: "POST",
                url: mainUrl + "NhBuilding/GetSelectSandBoxList",
                data: "PremisesId=" + PremisesId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var temp = '<%=Model.PNum%>' == item.Id ? "selected" : "";
                        if (item.SandBox == "selected" && '<%=Model.PNum%>' != item.Id) {
                            $("<option " + temp + " disabled=\"disabled\"></option>").val(item.Id).text(item.Number).appendTo($("#PNum"));
                        }
                        else {
                            $("<option " + temp + "></option>").val(item.Id).text(item.Number).appendTo($("#PNum"));
                        }
                    });
                }
            });
        }

        //获取楼栋平面图列表
        function GetPremisesPic(PremisesId) {
            $("#PictureData").empty();
            $("<option></option>").val("-1").text("请选择楼栋平面图").appendTo($("#PictureData"));
            $.ajax({
                type: "POST",
                url: mainUrl + "NhBuilding/GetBuildingPic",
                data: "PremisesId=" + PremisesId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var temp = '<%=Model.PictureData %>' == item.ID ? "selected" : "";
                        $("<option " + temp + "></option>").val(item.ID).text(item.Title).appendTo($("#PictureData"));
                    });
                }
            });
        }


        function GetPremises(PremisesId) {
            $("#PresaleId").empty();
            $("<option></option>").val("-1").text("请选择预售许可证").appendTo($("#PresaleId"));
            $.ajax({
                type: "POST",
                url: mainUrl + "NhBuilding/PermitPresaleList",
                data: "PremisesId=" + $("#PremisesId").val(),
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var temp = '<%=Model.PresaleId %>' == item.Id ? "selected" : "";
                        $("<option " + temp + "></option>").val(item.Id).text(item.Name).appendTo($("#PresaleId"));
                    });
                }
            });
        }

        var jsBuilding = {
            PremisesId: $("#PremisesId").val(), //楼盘ID
            BuildingState: [{ id: -1, value: "请选择" }, { id: 1, value: "待售" }, { id: 2, value: "在售" }, { id: 3, value: "售罄" }, { id: 4, value: "建设中" }, { id: 5, value: "规划中"}],
            //获取该楼盘状态
            GetState: function (PremisesId) {
                var i = -1;
                $.ajax({
                    type: "POST",
                    async: false,
                    url: mainUrl + "NhBuilding/GetPremisesState",
                    data: "id=" + PremisesId,
                    success: function (data) {
                        i = data;
                    }
                });
                return i;
            },
            ShowStateList: function (SalesStatus) {
                $("#State").empty();
                $.each(jsBuilding.BuildingState, function (i, item) {

                    if (SalesStatus == 0) {
                        //如果楼盘为待售 则楼栋也只能发待售
                        if (item.id == 1 || item.id == -1) {
                            $("<option value=" + item.id + "></option>").text(item.value).appendTo($("#State"));
                        } else {
                            $("<option value=" + item.id + "  disabled=\"disabled\"></option>").text(item.value).appendTo($("#State"));
                        }
                    }
                    if (SalesStatus == 1) {
                        //如果楼盘为在售 则楼栋可以发布任意状态
                        $("<option value=" + item.id + "></option>").text(item.value).appendTo($("#State"));
                    }
                    if (SalesStatus == 2) {
                        //如果楼盘状态为售罄 则楼栋只能发布售罄状态
                        if (item.id == 3 || item.id == -1) {
                            $("<option value=" + item.id + "></option>").text(item.value).appendTo($("#State"));
                        } else {
                            $("<option value=" + item.id + "  disabled=\"disabled\"></option>").text(item.value).appendTo($("#State"));
                        }
                    }
                });
            }
        };



        $(document).ready(function () {

            var SalesStatus = jsBuilding.GetState($("#PremisesId").val()); //获取楼盘状态

            jsBuilding.ShowStateList(SalesStatus);

            //状态选择
            $("#State").change(function () {
                $.ajax({
                    type: "POST",
                    url: mainUrl + "Premises/GetLivingHouseCountByBuildingId",
                    data: "Id=" + '<%=Model.Id %>',
                    success: function (msg) {
                        if (msg > 0) {
                            alert("当前楼栋下有营销活动进行中，不能变更楼栋状态。");
                            $("#State").val('<%=Model.State %>');
                        }
                    }
                });
            });


            //            var dval = '<%=Model.State %>';
            //            var val = $("#State").val();
            //            if (dval == 1) {

            //                //alert("楼栋当前状态为 待售，只可修改为在售");
            //                // $("#State").val(1);
            //                $("#State option[value='3']").attr("disabled", "disabled");
            //                $("#State option[value='4']").attr("disabled", "disabled");
            //                $("#State option[value='5']").attr("disabled", "disabled");

            //            }
            //            if (dval == 2) {

            //                //alert("楼栋当前状态为 在售，只可修改为售罄");
            //                //$("#State").val(2);
            //                $("#State option[value='1']").attr("disabled", "disabled");
            //                $("#State option[value='4']").attr("disabled", "disabled");
            //                $("#State option[value='5']").attr("disabled", "disabled");

            //            }
            //            if (dval == 3) {

            //                //alert("楼栋当前状态为 售罄，不可修改");
            //                //$("#State").val(3);
            //                $("#State").attr("disabled", "disabled");


            //            }
            //            if (dval == 4) {

            //                //alert("楼栋当前状态为 建设中，只可修改为在售");
            //                //$("#State").val(4);
            //                $("#State option[value='1']").attr("disabled", "disabled");
            //                $("#State option[value='3']").attr("disabled", "disabled");
            //                $("#State option[value='5']").attr("disabled", "disabled");

            //            }
            //            if (dval == 5) {

            //                //alert("楼栋当前状态为 规划中，只可修改为在售");
            //                //$("#State").val(5);

            //                $("#State option[value='1']").attr("disabled", "disabled");
            //                $("#State option[value='3']").attr("disabled", "disabled");
            //                $("#State option[value='4']").attr("disabled", "disabled");

            //            }


            //上传楼栋平面图
            $("#UploadBuildPic").bind("click", function () {
                var id = $("#PremisesId").val();
                if (id == -1) {
                    alert("请先选择楼盘");
                    return false;
                }
                window.open(mainUrl + "/Premises/PremisesImage/" + id + '?t=3');
            });

            $("#UpdatePic").bind("click", function () {
                var id = $("#PremisesId").val();
                if (id == -1) {
                    alert("请先选择楼盘");
                    return false;
                }
                GetPremisesPic(id);
            });

            GetUnit();
            GetSandBoxList('<%=Model.PremisesId %>'); //沙盘列表
            GetPremises('<%=Model.PremisesId %>'); //预售许可证
            GetPremisesPic('<%=Model.PremisesId %>'); //楼栋平面图
            $("#Ladder").val('<%=Model.Ladder %>');
            $("#Renovation").val('<%=Model.Renovation %>');
            $("#BuildingPosition").val('<%=Model.BuildingPosition %>');
            $("#State").val('<%=Model.State %>');
            $("#UnitNum").keyup(function () {
                $('#UnitInput').empty();
                var unitnum = $("#UnitNum").val();
                for (var i = 1; i <= unitnum; i++) {
                    if (unitnum > 10)
                        return;
                    $('#UnitInput').append(i + "=<input id=UnitName" + i + " type=\"text\" class=\"txt22 w100 mr15 ml5\" name=\"UnitName\"/>");
                }
            });


            $("#PremisesId").change(function () {
                $("#PresaleId").empty();
                $("<option></option>").val("-1").text("请选择预售许可证").appendTo($("#PresaleId"));
                $.ajax({
                    type: "POST",
                    url: mainUrl + "NhBuilding/PermitPresaleList",
                    data: "PremisesId=" + $("#PremisesId").val(),
                    success: function (msg) {
                        $.each(msg, function (i, item) {
                            $("<option></option>").val(item.Id).text(item.Name).appendTo($("#PresaleId"));
                        });
                    }
                });
            });

            $("#PresaleId").change(function () {
                if ($("#PresaleId").val() != -1) {
                    $("#PresaleName").val($("#PresaleId").find("option:selected").text());
                }
                else {
                    $("#PresaleName").val("若选项中没有请在此输入预售许可证号");
                }
            });

            jQuery.validator.addMethod("IsPremises", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择楼盘！</span>");

            jQuery.validator.addMethod("IsNameType", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择楼栋类型！</span>");


            jQuery.validator.addMethod('IsUnitName', function (value, element) {
                var Count = 1;
                $("input[name='UnitName']").each(function () {
                    if ($(this).val() == "") {
                        Count = 0;
                    }
                });
                value = Count;
                return this.optional(element) || value <= 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请填写单元名称！</span>");

            jQuery.validator.addMethod("IsLadder", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择梯户配比！</span>");

            jQuery.validator.addMethod("IsPictureData", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择楼栋平面图！</span>");

            jQuery.validator.addMethod("IsRenovation", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择装修程度！</span>");

            jQuery.validator.addMethod("IsBuildingPosition", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择楼栋所处楼盘位置！</span>");

            jQuery.validator.addMethod("IsState", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择销售状态！</span>");

            jQuery.validator.addMethod("IsPresaleId", function (value, element) {
                var f = this.optional(element) || value < 0 ? false : true;
                var l = false;
                if (f) {
                    return true;
                }
                else {
                    if ($("#PresaleName").val() != "" && $("#PresaleName").val() != "若选项中没有请在此输入预售许可证号") {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }, "<span class=\"ie_valign_5 no\">请选择预售许可证！</span>");

            jQuery.validator.addMethod("IsPNum", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择楼栋编号！</span>");


            $("#BuildingForm").validate({
                rules: {
                    PremisesId: {
                        IsPremises: true
                    },
                    BuildingName: {
                        required: true,
                        maxlength: 10
                    },
                    NameType: {
                        IsNameType: true
                    },
                    Periods: {
                        required: true,
                        range: [1, 9]
                    },
                    UnitNum: {
                        required: true,
                        digits: true,
                        range: [1, 8]
                    },
                    UnitName: {
                        IsUnitName: true
                    },
                    OpeningTime: {
                        required: true,
                        date: true
                    },
                    OthersTime: {
                        required: true,
                        date: true
                    },
                    FamilyNum: {
                        required: true,
                        digits: true,
                        range: [1, 9999]
                    },
                    TheFloor: {
                        required: true,
                        digits: true,
                        range: [1, 999]
                    },
                    Underloor: {
                        required: true,
                        range: [0, 99]
                    },
                    Ladder: {
                        IsLadder: true
                    },
                    Renovation: {
                        IsRenovation: true
                    },
                    BuildingPosition: {
                        IsBuildingPosition: true
                    },
                    PictureData: {
                        IsPictureData: true
                    },
                    State: {
                        IsState: true
                    },
                    PresaleId: {
                        IsPresaleId: true
                    },
                    PNum: {
                        IsPNum: true
                    }
                },
                messages: {
                    BuildingName: {
                        required: "<span class=\"ie_valign_5 no\">请填写楼栋名称！</span>",
                        maxlength: "<span class=\"ie_valign_5 no\">楼栋名称最多可输入10个字符</span>"
                    },
                    Periods: {
                        required: "<span class=\"ie_valign_5 no\">请填写楼盘期数！</span>",
                        range: "<span class=\"ie_valign_5 no\">只可输入1位正数！</span>"
                    },
                    UnitNum: {
                        required: "<span class=\"ie_valign_5 no\">请填写单元数量！</span>",
                        digits: "<span class=\"ie_valign_5 no\">只可输入1位正数！</span>",
                        range: "<span class=\"ie_valign_5 no\">1到8之间！</span>"
                    },
                    OpeningTime: {
                        required: "<span class=\"ie_valign_5 no\">请填写开盘时间！</span>",
                        date: "<span class=\"ie_valign_5 no\">请输入正确的时间！</span>"
                    },
                    OthersTime: {
                        required: "<span class=\"ie_valign_5 no\">请填写入住时间！</span>",
                        date: "<span class=\"ie_valign_5 no\">请输入正确的时间！</span>"
                    },
                    FamilyNum: {
                        required: "<span class=\"ie_valign_5 no\">请填写户数！</span>",
                        digits: "<span class=\"ie_valign_5 no\">请输入正确的户数！</span>",
                        range: jQuery.format("<span class=\"ie_valign_5 no\">户数为{0}到{1}户！</span>")
                    },
                    TheFloor: {
                        required: "<span class=\"ie_valign_5 no\">请填写地上楼层！</span>",
                        digits: "<span class=\"ie_valign_5 no\">请输入正确的层数！</span>",
                        range: jQuery.format("<span class=\"ie_valign_5 no\">地上楼层为{0}到{1}层！</span>")
                    },
                    Underloor: {
                        required: "<span class=\"ie_valign_5 no\">请填写地下楼层！</span>",
                        range: jQuery.format("<span class=\"ie_valign_5 no\">地下楼层为{0}到{1}层！</span>")
                    }


                },
                onkeyup: false,
                errorElement: "span",
                errorPlacement: function (error, element) {
                    element.parent().find("span").remove();
                    error.appendTo(element.parent());
                },
                submitHandler: function (form) {
                    $("#TypeN").val($("#NameType").find("option:selected").text());
                    $("#PermitPresale").val($("#PresaleId").find("option:selected").text());


                    //                    var dval = '<%=Model.State %>';
                    //                    var val = $("#State").val();
                    //                    if (dval == 1) {
                    //                        if (val != 2 && val != 1) {
                    //                            alert("楼栋当前状态为 待售，只可修改为在售");
                    //                            $("#State").val(1);
                    //                            return false;
                    //                        }
                    //                    }
                    //                    if (dval == 2) {
                    //                        if (val != 3 && val != 2) {
                    //                            alert("楼栋当前状态为 在售，只可修改为售罄");
                    //                            $("#State").val(2);
                    //                            return false;
                    //                        }
                    //                    }
                    //                    if (dval == 3) {
                    //                        if (val != 3) {
                    //                            alert("楼栋当前状态为 售罄，不可修改");
                    //                            $("#State").val(3);
                    //                            return false;
                    //                        }
                    //                    }
                    //                    if (dval == 4) {
                    //                        if (val != 2 && val != 4) {
                    //                            alert("楼栋当前状态为 建设中，只可修改为在售");
                    //                            $("#State").val(4);
                    //                            return false;
                    //                        }
                    //                    }
                    //                    if (dval == 5) {
                    //                        if (val != 2 && val != 5) {
                    //                            alert("楼栋当前状态为 规划中，只可修改为在售");
                    //                            $("#State").val(5);
                    //                            return false;
                    //                        }
                    //                    }


                    form.submit();
                }
            });
        });
    </script>
</asp:Content>
