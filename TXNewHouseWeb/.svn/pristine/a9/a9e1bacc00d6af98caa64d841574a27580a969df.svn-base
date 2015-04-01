<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_YaoHao>" %>

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
                <%=AdminPageInfo.ItemName %></a> <i>&gt;</i> <a href="javascript:void(0);">发布摇号活动</a></span>
    </div>
    <!--//current-->
    <!--//current-->
    <%using (Html.BeginForm("Add", "YaoHao", FormMethod.Post, new { Id = "formadd", Name = "formadd" }))
      {%>
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="btnDiv tab1">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                <tr>
                    <th width="14%">
                        活动名称：
                    </th>
                    <td width="86%">
                        <input type="text" value="" onblur="Inspection.Name()" id="Name" name="Name" onblur="Inspection.Name   ()">&nbsp;网络摇号
                    </td>
                </tr>
                <tr>
                    <th>
                        报名时间：
                    </th>
                    <td>
                        <input type="text" name="ApplyBeginTime" value="" onblur="Inspection.ApplyBeginTime()"
                            class="Wdate" id="ApplyBeginTime" onfocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',minDate:'%y-%M-%d %0:%0:%0',maxDate:'#F{$dp.$D(\'ApplyEndTime\',{d:-0});}'})"
                            readonly />
                        -<input type="text" name="ApplyEndTime" value="" onblur="Inspection.ApplyBeginTime()"
                            class="Wdate" id="ApplyEndTime" onfocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',minDate:'#F{$dp.$D(\'ApplyBeginTime\',{d:0});}'})"
                            readonly />
                    </td>
                </tr>
                <tr>
                    <th>
                        活动时间：
                    </th>
                    <td>
                        <input type="text" name="BeginTime" value="" onblur="Inspection.BeginTime()" class="Wdate"
                            id="BeginTime" onfocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',minDate:'%y-%M-%d %0:%0:%0',maxDate:'#F{$dp.$D(\'EndTime\',{d:-0});}'})"
                            readonly />
                        -<input type="text" name="EndTime" value="" onblur="Inspection.BeginTime()" class="Wdate"
                            id="EndTime" onfocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',minDate:'#F{$dp.$D(\'BeginTime\',{d:0});}'})"
                            readonly />
                    </td>
                </tr>

                <tr>
                    <th>
                        选房时间：
                    </th>
                    <td>
                        <input type="text" name="LectHouseTime" value="" onblur="Inspection.LectHouseTime()"
                            class="Wdate" id="LectHouseTime" onfocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',minDate:'%y-%M-%d %0:%0:%0'})"
                            readonly />
                    </td>
                </tr>
                <tr>
                    <th>
                        摇号公证：
                    </th>
                    <td>
                        <%=Html.DropDownList("NotarialOffice", Model.NotarialOfficeList, new { @onblur = "Inspection.NotarialOffice()" })%>
                    </td>
                </tr>
                <tr>
                    <th>
                        活动保证金金额：
                    </th>
                    <td>
                        <input type="text" value="" id="Bond" name="Bond" onblur="Inspection.Bond()">
                        元
                    </td>
                </tr>
                <tr>
                    <th>
                        活动介绍：
                    </th>
                    <td rowspan="2">
                        <textarea id="ActivitiesIntroduction" name="ActivitiesIntroduction" onblur="Inspection.ActivitiesIntroduction()"
                            cols="" rows="8" class="txtare_w500" style="width: 500px;"></textarea>
                    </td>
                </tr>
                <tr>
                    <th>
                    </th>
                </tr>
                <tr>
                    <th>
                        摇号须知：
                    </th>
                    <td rowspan="2">
                        <textarea id="ActivitiesNotice" name="ActivitiesNotice" onblur="Inspection.ActivitiesNotice()"
                            cols="" rows="8" class="txtare_w500" style="width: 500px;"></textarea>
                    </td>
                </tr>
                <tr>
                    <th>
                    </th>
                </tr>
                <tr>
                    <th>
                        摇号流程：
                    </th>
                    <td rowspan="2">
                        <textarea id="ActivitiesProcess" name="ActivitiesProcess" onblur="Inspection.ActivitiesProcess()"
                            cols="" rows="8" class="txtare_w500" style="width: 500px;"></textarea>
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
                        <%= Html.Hidden("InnerCode", Guid.NewGuid().ToString())%>
                        <%= Html.Hidden("adminId", CurrentUser.Id)%>
                        <%= Html.Hidden("Id", Model.Id)%>
                        <input type="button" name="button6" id="button6" value="发布，并开启报名入口" onclick="Inspection.FromSubmit()"
                            class="btn4 mr10" />
                    </td>
                </tr>
            </table>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <!--//data-->
    <% } %>
    <script type="">
        var Inspection = {
            validresult: true,
            Name: function () {
                var e = $("#Name");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else {
                    if (admincoms.StringHelper.getByteCount(e.val()) > 40)
                        this.SetResult_ToParent(e, false, "最多可输入20个汉字");
                    else
                        this.SetResult_ToParent(e, true, "");
                }
            },
            ApplyBeginTime: function () {
                var e = $("#ApplyBeginTime");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.ApplyEndTime();
            },
            ApplyEndTime: function () {
                var e = $("#ApplyEndTime");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            BeginTime: function () {
                var e = $("#BeginTime");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.EndTime();
            },
            EndTime: function () {
                var e = $("#EndTime");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            LectHouseTime: function () {
                var e = $("#LectHouseTime");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");
            },
            NotarialOffice: function () {
                var e = $("#NotarialOffice");
                if (!this.IsSelected_Diy(e, -1))
                    this.SetResult_ToParent(e, false, "请填写！");
                else
                    this.SetResult_ToParent(e, true, "");

            },
            Bond: function () {
                var e = $("#Bond");
                var reg = /^([1-9]\d{0,8}|0)$/;
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else {
                    if (!reg.test(e.val()))
                        this.SetResult_ToParent(e, false, "请输入9位正数。");
                    else
                        this.SetResult_ToParent(e, true, "");
                }

            },
            ActivitiesIntroduction: function () {
                var e = $("#ActivitiesIntroduction");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else {
                    if (admincoms.StringHelper.getByteCount(e.val()) > 1000)
                        this.SetResult_ToParent(e, false, "最多可输入500个汉字");
                    else
                        this.SetResult_ToParent(e, true, "");
                }
            }, ActivitiesNotice: function () {
                var e = $("#ActivitiesNotice");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else {
                    if (admincoms.StringHelper.getByteCount(e.val()) > 2000)
                        this.SetResult_ToParent(e, false, "最多可输入1000个汉字");
                    else
                        this.SetResult_ToParent(e, true, "");
                }
            }, ActivitiesProcess: function () {
                var e = $("#ActivitiesProcess");
                if (this.IsNullOrEmpty(e))
                    this.SetResult_ToParent(e, false, "请填写！");
                else {
                    if (admincoms.StringHelper.getByteCount(e.val()) > 1000)
                        this.SetResult_ToParent(e, false, "最多可输入500个汉字");
                    else
                        this.SetResult_ToParent(e, true, "");
                }
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
            FromSubmit_v01: function () { //提交                        
                this.validresult = true;
                this.Name();
                this.ApplyBeginTime();
                this.BeginTime();
                this.LectHouseTime();
                this.NotarialOffice()
                this.Bond();
                this.ActivitiesIntroduction();
                this.ActivitiesNotice();
                this.ActivitiesProcess();
                if (this.validresult) {
                    //alert("通过");
                    $("#formadd").submit();
                } else {
                    // alert("未通过");
                    //$(".lose.ml10").eq(0).siblings().first().focus();
                }
            },

            FromSubmit: function () {
                var url = '<%= Auxiliary.Instance.NhManagerUrl %>yaohao/add.html';
                var data = {
                    name: $("#Name").val(),
                    applybegintime: $("#ApplyBeginTime").val(),
                    applyendtime: $("#ApplyEndTime").val(),
                    begintime: $("#BeginTime").val(),
                    endtime: $("#EndTime").val(),
                    bond: $("#Bond").val(),
                    id: $("#Id").val(),
                    notarialoffice: $("#NotarialOffice").val(),
                    lecthousetime: $("#LectHouseTime").val(),
                    activitiesintroduction: $("#ActivitiesIntroduction").val(),
                    activitiesnotice: $("#ActivitiesNotice").val(),
                    activitiesprocess: $("#ActivitiesProcess").val()
                };
                this.validresult = true;
                this.Name();
                this.ApplyBeginTime();
                this.ApplyEndTime();
                this.BeginTime();
                this.EndTime();
                this.Bond();
                this.LectHouseTime();
                this.NotarialOffice();
                this.ActivitiesIntroduction();
                this.ActivitiesNotice();
                this.ActivitiesProcess();

                if (this.validresult) {
                    $.post(url, data, function (msg) {
                        if (msg.suc==1) {
                            alert("添加成功");
                            window.location.href = "<%=Url.SiteUrl().YaoHaoIndex(1) %>";
                        } else {
                            alert(msg.msg);
                        }
                    });
                }
            }
        };
    </script>
</asp:Content>
