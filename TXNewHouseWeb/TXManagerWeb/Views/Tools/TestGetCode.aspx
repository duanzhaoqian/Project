<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<dynamic>" %>

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
    <div class="data">
        <div class="filterBar">
            <%using (Html.BeginForm("TestGetCode", "Tools", FormMethod.Post, new { id = "subForm" }))
              { %>
            手机号:<input type="text" id="mobiletb" name="mobile" />
            <input type="button" value="查询验证码" id="searchbt" />
            <span id="coderesult"></span>
            <% } %>
        </div>
    </div>
    <script>
        $(function () {
            $("#searchbt").click(function () {
                var mobile = $("#mobiletb");
                if ($.trim(mobile.val()).length == 0) {
                    alert("请输入查询的手机号");
                    return false;
                }
                var form = $("#subForm");
                $.ajax({
                    type: "POST",
                    url: form.attr("action"),
                    dataType: "json",
                    data: { mobile: mobile.val() },
                    success: function (data) {
                        if (data.has)
                            $("#coderesult").html(" 查询结果：" + data.code);
                        else
                            $("#coderesult").html(" 没有查询结果");
                    }
                });
            });

        });
    </script>
</asp:Content>
