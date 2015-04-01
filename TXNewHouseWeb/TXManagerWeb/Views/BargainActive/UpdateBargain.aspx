<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVM_NH_BargainActive>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--Style Sheets--%>
    <style type="text/css">
        .managermsg
        {
            font-family: 宋体;
            font-size: 12px;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            color: #999999;
        }
        .fontset
        {
            font-family: 宋体;
            font-size: 12px;
            font-weight: normal;
        }
    </style>
    <%--Current Position--%>
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a><i>&gt;</i><a href="javascript:void(0);">修改
                    <%=Model.PremisesName +" "+ Model.BuildingName+Model.NameType+" "+Model.UnitName+" "+Model.Floor+"层 "+Model.HouseNum %>
                    砍价活动</a></span>
    </div>
    <%--Current Content--%>
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="outer">
            <!--第一步 begin-->
            <div class="dispose">
                <h5>
                    第一步：选择楼盘</h5>
            </div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            选择楼盘:
                        </th>
                        <td width="86%">
                            <%=Html.DropDownListFor(model => model.PremisesId, Model.Premises, new { @disabled = "disabled" })%>
                        </td>
                    </tr>
                </table>
            </div>
            <!--第一步 end-->
            <!--第二步 begin-->
            <div class="dispose">
                <h5>
                    第二步：选择房源<span class="managermsg">（只能选择一个房源）</span></h5>
                <span style="float: right"><a href="javascript:;" id="NoEr">修改</a></span>
            </div>
            <div class="filterBar" style="background-color: White; border-bottom: 1px solid #999999;">
                <!--筛选-->
                <span class="fontset">筛选：</span>
                <%=Html.DropDownListFor(it=>it.BuildingId,Model.Buildings) %>
                <%=Html.DropDownListFor(it=>it.UnitId,Model.Units) %>
                <%=Html.DropDownListFor(it=>it.Floor,Model.Floors) %>
                <%=Html.DropDownListFor(it =>it.SalesStatus , Model.SalesStatuss)%>
                <input type="button" name="searchHouse" id="searchHouse" value="搜索" class="btn3 mr10" />
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
            <!--第二步 end-->
            <input type="button" name="btn_update" id="OkEr" value="完成设置" class="btn3 mr10" />
            <!--第三步 begin-->
            <div class="dispose" style="margin-top: 15px;">
                <h5>
                    第三步：设置基本信息</h5>
                <span style="float: right"><a href="javascript:;" id="NoSan">修改</a></span>
            </div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="8%">
                            保底价:
                        </th>
                        <td width="92%">
                            <%=Html.TextBox("BidPrice", string.Format("{0:F0}", Model.BidPrice))%>万元
                        </td>
                    </tr>
                    <tr>
                        <th width="8%">
                            减价幅度：
                        </th>
                        <td width="92%">
                            必须为<%=Html.TextBox("AddPrice", string.Format("{0:F0}", Model.AddPrice))%>万元 的整数倍
                        </td>
                    </tr>
                    <tr>
                        <th width="8%">
                            最大幅度:
                        </th>
                        <td width="92%">
                            单次减价不高于<%=Html.TextBox("MaxPrice", string.Format("{0:F0}", Model.MaxPrice))%>万元
                        </td>
                    </tr>
                    <tr>
                        <th width="8%">
                            活动保证金金额:
                        </th>
                        <td width="92%">
                            <%=Html.TextBox("Bond", string.Format("{0:F0}", Model.Bond))%>元
                        </td>
                    </tr>
                    <tr>
                        <th width="12%">
                            竞拍时间:
                        </th>
                        <td width="88%">
                            开始<input type="text" name="BeginTime" value="<%=Model.BeginTime.ToString("yyyy-MM-dd") %>"
                                class="Wdate" id="BeginTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'EndTime\',{d:-0});}'})"
                                readonly />
                            结束<input type="text" name="EndTime" value="<%=Model.EndTime.ToString("yyyy-MM-dd") %>"
                                class="Wdate" id="EndTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'BeginTime\',{d:0});}'})"
                                readonly />
                        </td>
                    </tr>
                </table>
            </div>
            <!--第三步 end-->
        </div>
        <div class="btnDiv tab1">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                <tr>
                    <th width="1%">
                        <input type="button" name="btn_update" id="OkSan" value="完成创建" class="btn3 mr10" />
                    </th>
                    <td width="99%">
                        <input type="hidden" value="<%=ViewData["backurl"] %>" name="backurl" id="backurl" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--Javascript--%>
    <script type="text/javascript">
    var BidingModify = {
        IsSearch: false,//是否是搜索操作
        IsFirst: true,//是否第一次请求
        CheckIsDisabl_Er:true,//第二步是否禁用了
        CheckOk_San: true,
        ParticipateHouseId: "<%=Model.HouseId %>",
        Data: function () {
            //搜索post 数据
            var date = {
                ActivitiesId: "<%=Model.ActivitiesId %>",
                PremisesId: $("#PremisesId").val(),
                BuildingId:
                    $("#BuildingId").val(),
                UnitId:
                    $("#UnitId").val(),
                Floor:
                    $("#Floor").val(),
                SalesStatus:
                    $("#SalesStatus").val(),
                r:
                    Math.random()
            };
            return date;
        },
        Url: function () {
            //load url
            var dataurl = "&ActivitiesId=<%=Model.ActivitiesId %>";
            dataurl += "&PremisesId=" + BidingModify.Data().PremisesId;
            dataurl += "&BuildingId=" + BidingModify.Data().BuildingId;
            dataurl += "&UnitId=" + BidingModify.Data().UnitId;
            dataurl += "&Floor=" + BidingModify.Data().Floor;
            dataurl += "&SalesStatus=" + BidingModify.Data().SalesStatus;
            dataurl += "&pagesize=" + opts.items_per_page;
            dataurl += "&HouseId=" + BidingModify.ParticipateHouseId;
            dataurl += "&r=" + Math.random();
            return dataurl;
        },
        LoadData: function (pageindex) {
            //搜索操作 pageindex置为0
            if (!BidingModify.IsSearch) {
                opts.current_page = pageindex;
            } else {
                pageindex = 0;
                BidingModify.IsSearch = false;
            }
            //加载文档
            $("#ListResult").html("<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>");
            $("#ListResult").load('<%=Url.SiteUrl().BargainUser_Search("housesearchresult")%>?&pageindex=' + pageindex + BidingModify.Url(), function () {
                if (BidingModify.IsFirst) {
                    BidingModify.Disable_Er();
                    BidingModify.IsFirst = false;
                }
            });
        },
        PostCount: function () {
            //搜索操作 current_page置为0
            if (BidingModify.IsSearch)
                opts.current_page = 0;
            //请求数据 并生产分页链接
            $.post(
                '<%=Url.SiteUrl().BargainUser_Search("housesearchcount")%>',
                BidingModify.Data(),
                function (data) {
                    $("#shouse_page").pagination(data.result, opts);
                });
        },
        SearchData: function () {
            BidingModify.ParticipateHouseId = 0;
            BidingModify.IsSearch = true;
            BidingModify.PostCount();
        },
        SetHouse: function (e, id) {
            if ($(e).attr("flag") == "true") {
                $(".canjiahouse").each(function () {
                    $(this).attr("flag", "false");
                    $(this).html("参加");
                });
                BidingModify.ParticipateHouseId = 0;
            } else {
                $(".canjiahouse").each(function () {
                    $(this).attr("flag", "false");
                    $(this).html("参加");
                });
                $(e).attr("flag", "true");
                $(e).html("不参加");
                BidingModify.ParticipateHouseId = id;
            }
        },
        Disable_Er: function () {
            BidingModify.CheckIsDisabl_Er = true;
            //下拉框
            $(".filterBar").find("select").each(function () {
                $(this).prop("disabled", "disabled");
            });
            //列表操作按钮
            $(".canjiahouse").each(function () {
                $(this).hide();
            });
            //分页
            $(".paging").hide();
            //搜索按钮
            $(".filterBar").find("input").eq(0).prop("disabled", "disabled");
            //完成按钮
            $("#OkEr").prop("disabled", "disabled");
        },
        ReliefDisable_Er: function () {
            BidingModify.CheckIsDisabl_Er = false;
            //下拉框
            $(".filterBar").find("select").each(function () {
                $(this).removeAttr("disabled");
            });
            //列表操作按钮
            $(".canjiahouse").each(function () {
                $(this).show();
            });
            //分页
            $(".paging").show();
            //搜索按钮
            $(".filterBar").find("input").eq(0).removeAttr("disabled");
            //完成按钮
            $("#OkEr").removeAttr("disabled");
        },
        Disable_San: function () {
            //输入框
            $(".outer .tab1").eq(1).find("input").each(function () {
                $(this).prop("disabled", "disabled");
            });
            //完成按钮
            $("#OkSan").prop("disabled", "disabled");
        },
        ReliefDisable_San: function () {
            //输入框
            $(".outer .tab1").eq(1).find("input").each(function () {
                $(this).removeAttr("disabled");
            });
            //完成按钮
            $("#OkSan").removeAttr("disabled");
        },
        Check_Er: function () {
            if (BidingModify.ParticipateHouseId != 0) {
                return true;
            }
            return false;
        },
        Check_San: function () {
            BidingModify.CheckOk_San = true;
            BidingModify.Check_San_CKS.BidPrice();
            BidingModify.Check_San_CKS.AddPrice();
            BidingModify.Check_San_CKS.MaxPrice();
            BidingModify.Check_San_CKS.Bond();
            BidingModify.Check_San_CKS.BeginTime();
            return BidingModify.CheckOk_San;
        },
        Check_San_CKS: {
            BidPrice:function() {
                var e = $("#BidPrice");
                //var reg = /^([1-9]\d{0,8}|0\.\d{1,2}|0|[1-9]\d{0,8}\.\d{1,2})$/;
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (BidingModify.IsNullOrEmpty(e))
                    BidingModify.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                      BidingModify.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else
                        BidingModify.SetResult_ToParent(e, true, "");
                }
            },
            AddPrice:function() {
                var e = $("#AddPrice");
                //var reg = /^([1-9]\d{0,8}|0\.\d{1,2}|0|[1-9]\d{0,8}\.\d{1,2})$/;
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (BidingModify.IsNullOrEmpty(e))
                    BidingModify.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        BidingModify.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else
                        BidingModify.SetResult_ToParent(e, true, "");
                }
            },
            MaxPrice:function() {
                var e = $("#MaxPrice");
                //var reg = /^([1-9]\d{0,8}|0\.\d{1,2}|0|[1-9]\d{0,8}\.\d{1,2})$/;
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (BidingModify.IsNullOrEmpty(e))
                    BidingModify.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        BidingModify.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else
                        BidingModify.SetResult_ToParent(e, true, "");
                }
            },
            Bond:function() {
                var e = $("#Bond");
                //var reg = /^([1-9]\d{0,8}|0\.\d{1,2}|0|[1-9]\d{0,8}\.\d{1,2})$/;
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (BidingModify.IsNullOrEmpty(e))
                    BidingModify.SetResult_ToParent(e, false, "请填写。");
                else {
                    if (!reg.test(e.val()))
                        BidingModify.SetResult_ToParent(e, false, "请输入正确9位正数。");
                    else
                        BidingModify.SetResult_ToParent(e, true, "");
                }
            },
            BeginTime: function () {
                var e = $("#BeginTime");

                if (BidingModify.IsNullOrEmpty(e))
                    BidingModify.SetResult_ToParent(e, false, "请填写。");
                else
                    BidingModify.Check_San_CKS.EndTime();
            },
            EndTime: function () {
                var e = $("#EndTime");
                if (BidingModify.IsNullOrEmpty(e))
                    BidingModify.SetResult_ToParent(e, false, "请填写。");
                else
                    BidingModify.SetResult_ToParent(e, true, "");
            }
        },
        IsNullOrEmpty: function (e) { //空
            var c = $(e).val();
            return c == null || c == "" || c == undefined ? true : false;
        },
        SetResult_ToParent: function (e, b, c) { //父级追加
            $(e).parent().find("span").remove();
            b ? $(e).parent().append('<span class="win ml10"></span>') : $(e).parent().append('<span class="lose ml10">' + c + '</span>');
            if (!b) {
                if (this.CheckOk_San)
                    $(e).focus();
                this.CheckOk_San = false;
            }
        },
        SetResult_ToPParent: function (e, b, c) { //父父级追加
            $(e).parent().parent().find("span").remove();
            b ? $(e).parent().parent().append('<span class="win ml10"></span>') : $(e).parent().parent().append('<span class="lose ml10">' + c + '</span>');
            if (!b) {
                if (this.CheckOk_San)
                    $(e).focus();
                this.CheckOk_San = false;
            }
        },
        ModifyDate: function() {
            var date = {
                HouseId: BidingModify.ParticipateHouseId,
                ActivitiesHouseId: "<%=Model.ActivitiesHouseId %>",
                ActivitiesId: "<%=Model.ActivitiesId %>",
                BidPrice: $("#BidPrice").val(),
                AddPrice: $("#AddPrice").val(),
                MaxPrice: $("#MaxPrice").val(),
                BeginTime: $("#BeginTime").val(),
                EndTime: $("#EndTime").val(),
                Bond: $("#Bond").val(),
                adminId: "<%=CurrentUser.Id %>",
                adminName: "<%=CurrentUser.Name %>",
                r:Math.random()
            };
            return date;
        },
        Modify: function () {
            $.post("<%=Url.SiteUrl().BargainUser_Search("Modify")%>", BidingModify.ModifyDate(), function (result) {
                if (result.result) {
                    alert("修改成功");
                    window.location.href = $("#backurl").val();
                } else {
                    alert(result.message);
                    //$.freeDialog.hide();
                }
            });
        },
        Londing: function() {
//            $.freeDialog.open({
//                width: 275,
//                content: "<div style=\"text-align: center;\"><img src=\"<%=Auxiliary.Instance.NhWebThemeUrl("images/loading.gif") %>\" alt=\"正在加载中...\" /></div>",
//                showTitle: true,
//                Title: "处理中...",
//                allowClose:false
//        });
        }
    };
    function pageselectCallback(pageindex, jq) {
        BidingModify.LoadData(pageindex,jq);
    }
    //初始化分页参数
    var opts = { callback: pageselectCallback };
    opts.items_per_page = 5;       //每页的记录条数
    opts.next_text = "下一页";       //上一条的文本
    opts.prev_text = "上一页";       //下一条的文本
    opts.last_text="尾页";
    opts.num_display_entries = 5; //中间显示的页码个数
    opts.num_edge_entries = 2;     //两边显示的页码个数
    opts.link_to = "javascript:void(0);";
    $(function () {
        //加载完毕 获取数据
        BidingModify.PostCount();
        //禁用第三步
        BidingModify.Disable_San();
        //搜索
        $("#searchHouse").click(function () {
            BidingModify.SearchData();
        });
        //第二步禁用
        $("#OkEr").click(function () {
            if (BidingModify.Check_Er())
                BidingModify.Disable_Er();
            else
                alert("请选择一个房源");
        });
        //第二步解除禁用
        $("#NoEr").click(function () {
            BidingModify.ReliefDisable_Er();
            BidingModify.Disable_San();
            //第三步完成按钮
            $("#OkSan").removeAttr("disabled");
        });
         //第三步禁用
        $("#OkSan").click(function () {
            if (!BidingModify.Check_Er())
                alert("请先完成第二步，选择一个房源！");
            else if (!BidingModify.CheckIsDisabl_Er)
                alert("请确定第二步！");
            else {
                if (BidingModify.Check_San()) {
                    BidingModify.Disable_San();
                    BidingModify.Londing();
                    //更新数据 
                    BidingModify.Modify();
                } 
            }
        });
        //第三步解除禁用
        $("#NoSan").click(function () {
            BidingModify.ReliefDisable_San();
            BidingModify.Disable_Er();
        });
        /*联动加载*/
        var selectBuilding = $("#BuildingId");
        var selectUnit = $("#UnitId");
        var selectFloor = $("#Floor");
        $(selectBuilding).change(function () {
            clearListItems(selectUnit);
            clearListItems(selectFloor);
            if (this.value != 0) {
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("Units") %>?id=' + this.value + "&r=" + Math.random(), selectUnit);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("BuildingFloors") %>?id=' + this.value + "&r=" + Math.random(), selectFloor);
            }
        });
        /*end*/
    });
    </script>
</asp:Content>