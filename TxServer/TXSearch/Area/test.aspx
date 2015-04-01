<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="TXSearch.Area.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type ="text/javascript">
        $(document).ready(function () {

            $.get("../Village/GetVillageIndex.ashx?CityId=253", function (data) {
                debugger;
                var b = JSON.parse("[]");
//                var a = eval(data);
                var c = JSON.parse(data);
            });

//            $.getJSON("GetAreaIndex.ashx?CityPY=beijing", function (json) {
//                debugger;
//                alert(json);
//            });

//            $.getJSON("../Traffic/GetTrafficIndex.ashx?CityPY=beijing", function (json) {
//                debugger;
//                alert(json);
//            });
        }); 
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
