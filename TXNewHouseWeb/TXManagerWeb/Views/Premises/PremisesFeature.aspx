<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master" Inherits="WebRentViewPage<List<TXOrm.PremisesFeature>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=AdminPageInfo.PageTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .lptscgl{ margin:0px; padding:10px; font-size:12px; color:#333;}
        .lptscgl ul{ list-style:none;}
        .lptscgl ul li{ width:150px; height:20px; line-height:20px; padding:5px 10px; float:left; border:1px solid #ccc; border-radius:5px; margin:10px 10px 0px 0px;}
        .lptscgl ul li a{ color:#0000FF; text-decoration:none; margin-left:15px;}
    </style>
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
            添加楼盘特色词：<input id="txtPreFea" style="width:200px" maxlength="6" />
            <input id="btnAdd" type="button" value="添加" style="width:50px" onclick="" />
            <span id="errAdd" style="display:none;color:Red;">请输入特色词</span>
        </div>
        <div id="divShow" class="lptscgl clearFix">
            <%=Html.Partial("PremisesFeatureTable", Model) %>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAdd").click(function () {
                //添加
                var text = $("#txtPreFea").val();
                if (text == "") {
                    $("#errAdd").show();
                } else {
                    $("#errAdd").hide();
                    $.post("/premises/addpremisesfeature.html", { text: text, m: Math.random() }, function (data) {
                        if (data)
                            reload();
                        else
                            alert("添加失败");
                    });
                }
            });
        });
    </script>
</asp:Content>
