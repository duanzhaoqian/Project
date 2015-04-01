<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<TXOrm.PremisesFeature>>" %>

<ul>
    <%
        if (Model != null && Model.Count > 0)
        {
            foreach (var item in Model)
            {
                %><li><%=item.Name%><a href="javascript:void(0);" onclick="showUpdate(<%=item.Id%>, '<%=item.Name%>')">编辑</a><a href="javascript:void(0);" onclick="del(<%=item.Id%>)">删除</a></li><%
            }
        }
    %>
</ul>
<div id="divOverlay" style="display:none;" class="ui-widget-overlay ui-front"></div>
<div id="divShowEdit" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-front ui-dialog-buttons" style="display:none; height: auto; width: 205px; top: 200px; left: 427px;">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>编辑</td>
            <td></td>
        </tr>
        <tr><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td colspan="2">
                <input id="txtUpdPreFea" type="text" style="width:200px" maxlength="6" />
                <input id="hidUpdId" type="hidden" />
            </td>
        </tr>
        <tr><td colspan="2">&nbsp;<span id="errUpd" style="display:none;color:Red;">请输入特色词</span></td></tr>
        <tr>
            <td align="right"><input type="button" name="name" value="确 定" onclick="update()" />&nbsp;</td>
            <td>&nbsp;<input type="button" name="name" value="取 消" onclick="hideUpdate()" /></td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    //删除
    function del(id) {
        if (confirm("您确定要删除吗？")) {
            $.post("/premises/deletepremisesfeature.html", { id: id, m: Math.random() }, function (data) {
                if(data)
                    reload();
                else
                    alert("删除失败");
            });
        }
    }
    //修改弹层
    function showUpdate(id, text) {
        $("#divOverlay").show();
        $("#divShowEdit").show();
        $("#txtUpdPreFea").val(text);
        $("#hidUpdId").val(id);
    }
    //取消修改弹层
    function hideUpdate() {
        $("#divOverlay").hide();
        $("#divShowEdit").hide();
        $("#txtUpdPreFea").val("");
        $("#hidUpdId").val("");
    }
    //确定修改
    function update() {
        var text = $("#txtUpdPreFea").val();
        var id = $("#hidUpdId").val();
        if (text == "") {
            $("#errUpd").show();
        } else {
            $("#errUpd").hide();
            $("#divOverlay").hide();
            $("#divShowEdit").hide();
            $.post("/premises/updatepremisesfeature.html", { id: id, text: text, m: Math.random() }, function (data) {
                if (data)
                    reload();
                else
                    alert("修改失败");
            });
        }
    }
    //重新读取数据
    function reload() {
        $.post("/premises/premisesfeature.html", function (data) {
            $("#divShow").html(data);
        });
    }
</script>
