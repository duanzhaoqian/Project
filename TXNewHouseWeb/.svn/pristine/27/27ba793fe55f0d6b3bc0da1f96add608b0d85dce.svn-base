<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVM_NH_Developer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .left
        {
            text-align: left;
        }
    </style>
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a><i>&gt;</i> <a href="javascript:void(0);">审核</a></span>
    </div>
    <!--//current-->
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="outer">
            <div class="tab1">
                <table width="100%" border="0" class="tb1" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr class="" style="border-bottom: 1px solid #A8A5A5;">
                            <th width="13%" style="border: 1px solid #A8A5A5; text-align: center;">
                                公司资料
                            </th>
                            <td width="87%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>用户名：
                            </th>
                            <td>
                                <%=Model.LoginName %>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>公司类型：
                            </th>
                            <td>
                                <%=Model.TypeStr %>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2">*</span>公司名称：
                            </th>
                            <td>
                                <%=Model.Name %>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2">*</span>所在城市：
                            </th>
                            <td>
                                <%=Model.ProvinceName %>&nbsp;&nbsp;<%=Model.CityName %>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2">*</span>公司地址：
                            </th>
                            <td>
                                <%=Model.Address %>
                            </td>
                        </tr>
                        <tr class="">
                            <%var userimg = TXCommons.PictureModular.GetPicture.GetUserPictureInfo(Model.InnerCode, true, TXCommons.PictureModular.UserPictureType.Identification.ToString());
                            %>
                            <th class="left">
                                <span class="red mr2">*</span>公司营业执照复印件：
                            </th>
                            <td>
                                <a href="<%=userimg.Path %>" target="_blank"><img src="<%=userimg.Path %>" width="180" height="130"></a>
                            </td>
                        </tr>
                        
                        <tr class="" <%= Model.Type == 2 ? "" : "style=\"display: none;\"" %>>
                            <% var userfile = TXCommons.PictureModular.GetPicture.GetDocumentInfo(Model.InnerCode, true, TXCommons.PictureModular.DocumentType.PACT.ToString());
                            %>
                            <th class="left">
                                <span class="red mr2">*</span>委托代理协议：
                            </th>
                            <td>
                                <% if (userfile.ID == 0)
                                   {
                                %>
                                   未上传
                                   <%
                                   }
                                   else
                                   {
                                   %>
                                <a href="<%= userfile.Path %>" target="_blank"><%= userfile.FileName %></a>
                                <%
                                   }
                                %>
                            </td>
                        </tr>
                        
                        <tr class="" style="border-bottom: 1px solid #A8A5A5;">
                            <th width="13%" style="border: 1px solid #A8A5A5; text-align: center;">
                                联系人资料
                            </th>
                            <td width="87%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>真实姓名：
                            </th>
                            <td>
                                <%=Model.RealName %>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>手机号：
                            </th>
                            <td>
                                <%=Model.Mobile %>
                            </td>
                        </tr>
                        <tr class="">
                            <th class="left">
                                <span class="red mr2"></span>邮箱：
                            </th>
                            <td>
                                <%=Model.Email %>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="btnDiv tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tbody>
                        <tr class="">
                            <th width="14%">
                                &nbsp;
                            </th>
                            <td width="86%">
                                <% if (Model.State == 3)
                                   { %>
                                <input type="button" name="button6" id="IsOk" value="通过" class="btn3" />
                                <input type="button" name="button6" id="IsNo" value="不通过" class="btn3" />
                                <% } %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <%if (!string.IsNullOrWhiteSpace(Model.Refuse)) %>
                        <tr class="" style="border-bottom: 1px solid #A8A5A5;">
                            <th width="13%" style="border: 1px solid #A8A5A5; text-align: center;">
                                不通过理由
                            </th>
                            <td width="87%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="">
                            <td colspan="2">
                                <%=Model.Refuse%>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
         var flag = true;
         /*通过*/
         $("#IsOk").click(function() {
             if (IsFirstClick.Check() && confirm("您确认该操作？")) {
                 PostData("<%=Model.Id %>", "", 1);
             } else {
                 $("#IsNo").removeAttr("disabled");
                 $("#IsOk").removeAttr("disabled");
                 IsFirstClick.flag = true;
             }
         });
         /*拒绝*/
         $("#IsNo").click(function () {
                 var htmls = [];
                 htmls.push('<table width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>');
                 htmls.push('<tr class=""><td width="100%" align="center"><p id="textMsg" style="display:none; height:24px; text-align:left;" ><span style="display:block; height:24px;" class="lose ml10">最多可输入200个汉字！</span></p>');
                 htmls.push('<p id="textMsgkong" style="display:none; height:24px; text-align:left;" ><span style="display:block; height:24px;" class="lose ml10">请输入拒绝原因！（最多可输入200个汉字）</span></p></td></tr>');
                 htmls.push('<tr class=""><td width="100%" align="center"></td></tr>');
                 htmls.push('<tr class=""><td width="100%" align="center"><textarea name="textarea1" id="refusetextarea" placeholder="请输入拒绝原因" class="w320 h80"></textarea></td></tr>');
                 htmls.push('</tbody></table>');
                 $.freeDialog.open({
                     width: 375,
                     content: htmls.join(''),
                     isHidden: false,
                     buttons: [{
                         text: '提交',
                         onclick: function(item, dialog) {
                             var lenght = admincoms.StringHelper.getByteCount($("#refusetextarea").val());
                             if (0<lenght&&lenght<=400) {
                                 if (IsFirstClick.Check()) {
                                     var refuse = $("#refusetextarea").val();
                                     PostData("<%=Model.Id %>", refuse, 2);
                                     dialog.close();
                                 }
                             } else if(lenght<=0) {
                                 $("#textMsgkong").show();
                                 $("#textMsg").hide();
                             }else if (lenght > 400) {
                                 $("#textMsg").show();
                                 $("#textMsgkong").hide();
                             }
                         }
                     }],
                     Title: "拒绝原因",
                     showTitle: false
                 });
             
         });
         /*数据处理*/
         var PostData = function(id, con, state) {
             $.post("<%=Url.SiteUrl().Developers_Search("Handle")%>", { id: id, state: state, refuse: con, adminId: "<%=CurrentUser.Id %>", adminName: "<%=CurrentUser.Name %>", ram: Math.random() }, function(response) {
                 if (response.flag) {
                     $.freeDialog.success('操作成功！');
                     setTimeout(function() { window.location.href = "<%=ViewData["backurl"] %>"; }, 1500);
                 } else {
                     $.freeDialog.error(response.msg);
                 }
             });
         };
         var IsFirstClick = {
             flag: true,
             Check: function() {
                 if (this.flag) {
                     this.flag = false;
                      $("#IsNo").attr("disabled","disabled");
                      $("#IsOk").attr("disabled","disabled");
                     return true;
                 } else {
                     return false;
                 }
             }
         };
    </script>
</asp:Content>