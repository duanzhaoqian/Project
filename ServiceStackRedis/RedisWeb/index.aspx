<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RedisWeb.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>反编译Redis数据</title>
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        #key
        {
            width: 374px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!--RentHouse001001-->
    键值&nbsp;<input id="key" onfocus="this.select()" />&nbsp; <br /><a href="javascript:getRedis()">查询Key内容</a>&nbsp;&nbsp;&nbsp;
     <a href="javascript:getLikeRedis()">模糊查询Key值</a><br /><br />
     <%--<a href="javascript:addRedis()">测试添加Redis</a><br /><br />--%>
     键值&nbsp;<input id="removeKey" type="text" onfocus="this.select()"  /><br />
     <a href="javascript:removeRedis()">移除Redis键值对</a>&nbsp;&nbsp;&nbsp;
     <a href="javascript:removeLikeRedis()">模糊Key移除Redis键值对</a><br /><br />
    选择查询服务器：<br />

<%for (int i = 0; i < PostUrl.Length; i++)
{
    string check = string.Empty;
    if (i == 0) { check = "checked=checked"; }
      %>
<input type="radio" name="rad" <%=check %>  value="<%=PostUrl[i] %>" value="" id="chk<%=i %>">&nbsp;<label for="chk<%=i %>"><%=PostUrl[i] %></label><br />
<%}%>
<br /><br />
结果输出：<br /><br />
<div id="show" style="width:500px;color:Blue">
</div>
    <script>
    function getRedis() {
        var k = $("#key").val();
        if (k == ""){ $("#show").html("key为空"); return;}
        var rad = $("input:radio[name='rad']:checked").val();
        if (rad== undefined || rad == "") { $("#show").html("请选择查询服务器"); return; }
        $.post("/response/redis_do.aspx", { key: k, PostUrl: rad, m: Math.random() }, function (d) {
            if (d == "") {
                d = "数据为空";
            }
            $("#show").html(d);
        });
    }
    //模糊查询所有的Key
    function getLikeRedis(){
        var k = $("#key").val();
        if (k == "") { $("#show").html("key为空"); return; }
        var rad = $("input:radio[name='rad']:checked").val();
        if (rad == undefined || rad == "") { $("#show").html("请选择查询服务器"); return; }
        $.post("/response/HandleRedis.aspx", { key: k, PostUrl: rad, m: Math.random(),action:"like" }, function (d) {
            if (d == "") {
                d = "数据为空";
            }
            $("#show").html(d);
        });
    }
    //添加
    function addRedis() {
        var rad = $("input:radio[name='rad']:checked").val();
        if (rad == undefined || rad == "") { $("#show").html("请选择查询服务器"); return; }
        $.post("/response/HandleRedis.aspx", { PostUrl: rad, m: Math.random(),action:"add" }, function (data) {
            alert(data);
        });
    }
    //移除
    function removeRedis() {
        var k = $("#removeKey").val();
        if (k == "") { $("#show").html("key为空"); return; }
        var rad = $("input:radio[name='rad']:checked").val();
        if (rad == undefined || rad == "") { $("#show").html("请选择查询服务器"); return; }
        $.post("/response/HandleRedis.aspx", { key: k, PostUrl: rad, m: Math.random(),action:"remove" }, function (d) {
            if (d == "") {
                d = "数据为空";
            }
            $("#show").html(d);
        });
    }

    //模糊键移除
    function removeLikeRedis() {
        var k = $("#removeKey").val();

        var endChar = k.substr(k.length - 1);
        if (endChar != '*') {
            alert("请输入字符串中末尾处加*号");
        }

        if (k == "") { $("#show").html("key为空"); return; }
        var rad = $("input:radio[name='rad']:checked").val();
        if (rad == undefined || rad == "") { $("#show").html("请选择查询服务器"); return; }
        $.post("/response/HandleRedis.aspx", { key: k, PostUrl: rad, m: Math.random(), action: "removeLike" }, function (d) {
            if (d == "") {
                d = "数据为空";
            }
            $("#show").html(d);
        });
    }

    </script>
    </form>
</body>
</html>
