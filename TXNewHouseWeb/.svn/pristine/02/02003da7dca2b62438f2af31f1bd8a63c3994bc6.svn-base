<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXModel.Web.PremisesBuilding>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%@ Import Namespace="TXModel.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    楼栋管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <div class="shaibox mt15 clearFix">
            <div class="fl">
                筛选：</div>
            <div class="selbox">
                <span class="pl10" id="pid">选择楼盘</span>
                <div class="sel_list w300" style="display: none;" id="PremisesList">
                </div>
            </div>
        </div>
        <div class="mt15 clearFix" style="position: relative;">
            <!--显示分页数据-->
            <div id="divAjaxData">
                <%=Html.Partial("NhBuildingTable", Model)%>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script language="javascript" type="text/javascript">
        //获取参数
        function getUrlParam(name) {

            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象

            var r = window.location.search.substr(1).match(reg);  //匹配目标参数

            if (r != null) return unescape(r[2]); return null; //返回参数值
        }

        function Search(pid, Pname) {
            $("#pid").text(Pname);
            //window.location.href = "<%=TXCommons.GetConfig.BaseUrl%>NhBuilding/List?pid=" + pid;
        }

        $(document).ready(function () {
            var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
            //下拉框显示和隐藏
            $(".selbox").click(function () {
                $(this).children(".sel_list").toggle();
            })
            $(".selbox .sel_list").mouseleave(function () {
                $(this).hide();
            });
            var pid = getUrlParam('pid');
            $.ajax({
                type: "POST",
                url: mainUrl + "/NhBuilding/GetPremises",
                success: function (data) {
                    $.each(data, function (i, item) {
                        if (i == 0) {
                            $("#pid").text(item.Name);
                        }
                        //$("<a href=\"javascript:void(0);\" onclick=\"Search(" + item.Id + ",'" + item.Name + "')\"></a>").text(item.Name).appendTo($("#PremisesList"));
                        if (pid == item.Id) {
                            $("#pid").text(item.Name);
                        }
                        $("<a href=\"?pid=" + item.Id + "\"></a>").text(item.Name).appendTo($("#PremisesList"));


                    });
                }
            });


        });
    </script>
</asp:Content>
