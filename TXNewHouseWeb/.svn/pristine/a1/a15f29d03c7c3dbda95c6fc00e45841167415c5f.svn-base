<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master" Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_YaoHao>" %>

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
                <%=AdminPageInfo.ItemName %></a>
                <i>&gt;</i> <a href="javascript:void(0);">
                修改摇号活动</a></span>
    </div>
    <!--//current-->
      <!--//current-->
           <%using (Html.BeginForm("Modify", "YaoHao", FormMethod.Post, new { Id = "formadd", Name = "formadd" }))
          {%>
        <div class="data">
			<!-- InstanceBeginEditable name="EditRegion3" -->
            
                <div class="btnDiv tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                  <tr>
                    <th width="14%">活动名称：</th>
                    <td width="86%"><input type="text" value="<%=Model.Name %>" onblur="Inspection.Name()" id="Name" name="Name" onblur="Inspection.Name   ()" >&nbsp;网络摇号</td>
                  </tr>
                  <tr>
                    <th>报名时间：</th>
                    <td>
				 <input type="text" name="ApplyBeginTime" value="<%=Model.ApplyBeginTime.HasValue?Model.ApplyBeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss"):"--" %>" onblur="Inspection.ApplyBeginTime()" class="Wdate" id="ApplyBeginTime" onFocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',maxDate:'#F{$dp.$D(\'ApplyEndTime\',{d:-0});}'})" readonly/>
                  -<input type="text" name="ApplyEndTime"  value="<%=Model.ApplyEndTime.HasValue?Model.ApplyEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"):"--" %>" onblur="Inspection.ApplyBeginTime()" class="Wdate" id="ApplyEndTime" onFocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',minDate:'#F{$dp.$D(\'ApplyBeginTime\',{d:0});}'})" readonly/>
					</td>
                  </tr>
                  <tr>
                    <th>活动时间：</th>
                    <td> <input type="text" name="BeginTime" value="<%=Model.BeginTime.ToString("yyyy-MM-dd HH:mm:ss") %>" onblur="Inspection.BeginTime()" class="Wdate" id="BeginTime" onFocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',maxDate:'#F{$dp.$D(\'EndTime\',{d:-0});}'})" readonly/>
                  -<input type="text" name="EndTime"  value="<%=Model.EndTime.ToString("yyyy-MM-dd HH:mm:ss") %>" onblur="Inspection.BeginTime()" class="Wdate" id="EndTime" onFocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',minDate:'#F{$dp.$D(\'BeginTime\',{d:0});}'})" readonly/>
					
					</td>
                  </tr>
                  <tr>
                    <th>选房时间：</th>
                    <td>
                       <input type="text" name="LectHouseTime" value="<%=Model.LectHouseTime.ToString("yyyy-MM-dd HH:mm:ss")%>" onblur="Inspection.LectHouseTime()" class="Wdate" id="LectHouseTime" onFocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss'})" readonly/>
						</td>
                  </tr>
                  <tr>
                    <th>摇号公证：</th>
                   <td><%=Html.DropDownListFor(model=>model.NotarialOffice, Model.NotarialOfficeList, new { @onblur = "Inspection.NotarialOffice()" })%>
                              </td>
                  </tr>
                  <tr>
                      <th>活动保证金金额：</th>
                      <td><input type="text" value="<%=Convert.ToDouble(Model.Bond) %>" id="Bond" name="Bond" onblur="Inspection.Bond()" > 元</td>
                  </tr>
                  <tr>
                    <th>活动介绍：</th>
                    <td rowspan="2">
						<textarea id="ActivitiesIntroduction" name="ActivitiesIntroduction" onblur="Inspection.ActivitiesIntroduction()" cols="" rows="8" class="txtare_w500" style="width:500px;"><%=Model.ActivitiesIntroduction%></textarea>
						</td>
                  </tr>
                  <tr>
                    <th></th>
                  </tr> <tr>
                    <th>摇号须知：</th>
                    <td rowspan="2">
						<textarea id="ActivitiesNotice" name="ActivitiesNotice" onblur="Inspection.ActivitiesNotice()" cols="" rows="8" class="txtare_w500" style="width:500px;"><%=Model.ActivitiesNotice%></textarea>
						</td>
                  </tr>
                  <tr>
                    <th></th>
                  </tr> <tr>
                    <th>摇号流程：</th>
                    <td rowspan="2">
						<textarea id="ActivitiesProcess" name="ActivitiesProcess" onblur="Inspection.ActivitiesProcess()" cols="" rows="8" class="txtare_w500" style="width:500px;"><%=Model.ActivitiesProcess%></textarea>
						</td>
                  </tr>
				  <tr>
				  <td></td>
				  <td></td>
				  </tr>
                </table>
                </div>
                <div class="btnDiv tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0"  class="tb1">
                  <tr>
                    <th width="14%">&nbsp;</th>
                    <td width="86%">
                        <%= Html.Hidden("InnerCode", Guid.NewGuid().ToString())%>
                        <%= Html.Hidden("adminId", CurrentUser.Id)%>
                        <%= Html.Hidden("Id", Model.Id)%>
                        <%= Html.Hidden("backurl",ViewData["backurl"])%>
                    <input type="button" name="button6" id="button6" value="确定" onclick="Inspection.FromSubmit()" class="btn4 mr10"/>
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
                    FromSubmit: function () { //提交                        
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
                    }
                };
    </script>
</asp:Content>
