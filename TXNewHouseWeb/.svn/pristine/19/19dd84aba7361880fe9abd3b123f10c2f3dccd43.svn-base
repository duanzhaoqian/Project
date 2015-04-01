<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVM_NH_Developer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .topCard a
        {
            display: block;
            float: left;
            font-size: 14px;
            height: 30px;
            line-height: 30px;
            text-align: center;
            width: 120px;
            border-right: solid 1px #ECEDEA;
        }
        .topCard .active
        {
            background-color: #71A814;
            color: #ffffff;
        }
    </style>
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a><i>&gt;</i> <a href="javascript:void(0);">修改开发商资料</a></span>
    </div>
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="topCard" style="height: 30px;">
            <% var Id = int.Parse(ViewData["Id"].ToString()); %>
            <a href="<%=Url.SiteUrl().DevelopersAccount_Handle(Id) %>">基本资料</a> <a href="<%=Url.SiteUrl().HeadPictureHandle(Id) %>">
                头像</a> <a class="active">密码</a> <a href="<%=Url.SiteUrl().AuthenticationHandle(Id) %>">
                    身份认证</a>
        </div>
        <div class="clearFix">
            <div style="padding-left: 15px; padding-top: 15px;">
                <input type="button" onclick="Pwd.sendPwd()" value="初始密码并发送给开发商" />
                <input type="hidden" value="<%=Id %>" name="DeveloperId" id="DeveloperId" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var Pwd = {
            sendPwd: function () {
                $.post("<%=Url.SiteUrl().GetNewPwd() %>", { id: $("#DeveloperId").val(), m: Math.random() }, function (data) {
                    if (data.result) {
                        alert("发送成功!");
                    }
                    else {
                        alert("发送失败!");
                    }
                });
            }
        }
    </script>
</asp:Content>