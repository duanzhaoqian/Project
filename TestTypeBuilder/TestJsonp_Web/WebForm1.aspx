<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TestJsonp_Web.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="js/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function test(data) {
            var str = JSON.stringify(data);
            $("#body").append(str);
            // alert(str);
        };
    </script>
    <script type="text/javascript">

        var fun = function() {
            $.ajax({
                url: "http://mongoservice.kyjob.com/MongoHouseService.svc/get/1",
                type: "get",
                dataType: "jsonp",
                jsonp: "callback",
                jsonpCallback: "test",
                sucess: function(data) {
                    alert(1);
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert("error");
                }
            });
            $("#test").unbind("click");
            $("#test").attr("onclick", "fun()");
            $("#test").removeAttr("onclick");
        };
        $(function() {
            $("#test").bind("click", fun);
        });

//        $(function () {
//            $.getJSON("http://mongoservice.kyjob.com/MongoHouseService.svc/get/1?callback=?",
//              function (data) {
//                  //alert(data);
//                  var str=JSON.stringify(data);
//                  
//                  //var str = $.parseHTML(data);
//                  $("#body").html(str);
//              }
//          );
//        })
    </script>
</head>
<body id="body">
<input id="test" type="button" value="test"/>
<script type="text/javascript">
    var funtest=function() {
       // alert(123);
        return false;
    }
</script>
<form action="" method="POST" enctype="application/x-www-form-urlencoded" autocomplete="true">
    <input type="text" value=""/>
    <input type="button" value="btn1" onclick="funtest()"/>
    <input type="submit" value="sub1" onclick="funtest()"/>
</form>
</body>
</html>
