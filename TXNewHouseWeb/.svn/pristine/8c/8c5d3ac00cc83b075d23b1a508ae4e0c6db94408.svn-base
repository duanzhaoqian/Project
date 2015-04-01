<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXOrm.Premis>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-楼盘相册
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <div class="btit mt15 clearFix">
            <div class="tit_font fl">
                <%=Model.Name%>-楼盘相册</div>
            <div class="font12 fr mr35">
                <span class="red">*</span>为必填项</div>
        </div>
        <div class="choose mt15">
            <span class="left_80"><a href="#" id="a_1" class="cur"><i class="red pr5">*</i>户型图</a>
                <a href="#" id="a_3"><i class="red pr5">*</i>楼栋平面图</a><a href="#" id="a8"><i class="red pr5">*</i>楼盘封面广告图</a>
                <a href="#" id="a_2">规划图</a> <a href="#" id="a_4">交通图</a> <a href="#" id="a_5">位置图</a>
                <a href="#" id="a_6">实景图</a> <a href="#" id="a_7"><i class="red pr5">*</i>效果图</a>
            </span>
        </div>
        <div id="ddivd" class="clearFix">
            <div class="" id="d_1">
                <iframe style="width: 100%;" scrolling="no" frameborder="0" id="Iframe<%=TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString()%>"
                    name="frame<%=TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString()%>"
                    src="/Premises/UploadPremisesPhoto?guid=<%=Model.InnerCode%>&picturetype=<%=TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString()%>&cityId=<%=Model.CityId%>&id=<%=Model.Id%>"
                    allowtransparency="true"></iframe>
            </div>
            <!--end1-->
            <div style="display: none" id="d_3">
                <iframe style="width: 100%;" scrolling="no" frameborder="0" id="Iframe<%=TXCommons.PictureModular.PremisesPictureType.Plane.ToString()%>"
                    name="frame<%=TXCommons.PictureModular.PremisesPictureType.Plane.ToString()%>"
                    src="/Premises/UploadPremisesPhoto?guid=<%=Model.InnerCode%>&picturetype=<%=TXCommons.PictureModular.PremisesPictureType.Plane.ToString()%>&cityId=<%=Model.CityId%>&id=<%=Model.Id%>"
                    allowtransparency="true"></iframe>
            </div>
            <div style="display: none" id="d_8">
                <iframe style="width: 100%;" scrolling="no" frameborder="0" id="frame<%=TXCommons.PictureModular.PremisesPictureType.Advert.ToString()%>"
                    name="frame<%=TXCommons.PictureModular.PremisesPictureType.Advert.ToString()%>"
                    src="/Premises/UploadPremisesPhoto?guid=<%=Model.InnerCode%>&picturetype=<%=TXCommons.PictureModular.PremisesPictureType.Advert.ToString()%>&cityId=<%=Model.CityId%>&id=<%=Model.Id%>"
                    allowtransparency="true"></iframe>
            </div>
            <!--end2-->
            <div style="display: none" id="d_2">
                <iframe style="width: 100%;" scrolling="no" frameborder="0" id="Iframe<%=TXCommons.PictureModular.PremisesPictureType.Plan.ToString()%>"
                    name="frame<%=TXCommons.PictureModular.PremisesPictureType.Plan.ToString()%>"
                    src="/Premises/UploadPremisesPhoto?guid=<%=Model.InnerCode%>&picturetype=<%=TXCommons.PictureModular.PremisesPictureType.Plan.ToString()%>&cityId=<%=Model.CityId%>&id=<%=Model.Id%>"
                    allowtransparency="true"></iframe>
            </div>
            <!--end3-->
            <div style="display: none" id="d_4">
                <iframe style="width: 100%;" scrolling="no" frameborder="0" id="Iframe<%=TXCommons.PictureModular.PremisesPictureType.TRAFFIC.ToString()%>"
                    name="frame<%=TXCommons.PictureModular.PremisesPictureType.TRAFFIC.ToString()%>"
                    src="/Premises/UploadPremisesPhoto?guid=<%=Model.InnerCode%>&picturetype=<%=TXCommons.PictureModular.PremisesPictureType.TRAFFIC.ToString()%>&cityId=<%=Model.CityId%>&id=<%=Model.Id%>"
                    allowtransparency="true"></iframe>
            </div>
            <!--end4-->
            <div style="display: none" id="d_5">
                <iframe style="width: 100%;" scrolling="no" frameborder="0" id="Iframe<%=TXCommons.PictureModular.PremisesPictureType.Location.ToString()%>"
                    name="frame<%=TXCommons.PictureModular.PremisesPictureType.Location.ToString()%>"
                    src="/Premises/UploadPremisesPhoto?guid=<%=Model.InnerCode%>&picturetype=<%=TXCommons.PictureModular.PremisesPictureType.Location.ToString()%>&cityId=<%=Model.CityId%>&id=<%=Model.Id%>"
                    allowtransparency="true"></iframe>
            </div>
            <!--end5-->
            <div style="display: none" id="d_6">
                <iframe style="width: 100%;" scrolling="no" frameborder="0" id="Iframe<%=TXCommons.PictureModular.PremisesPictureType.Scene.ToString()%>"
                    name="frame<%=TXCommons.PictureModular.PremisesPictureType.Scene.ToString()%>"
                    src="/Premises/UploadPremisesPhoto?guid=<%=Model.InnerCode%>&picturetype=<%=TXCommons.PictureModular.PremisesPictureType.Scene.ToString()%>&cityId=<%=Model.CityId%>&id=<%=Model.Id%>"
                    allowtransparency="true"></iframe>
            </div>
            <!--end6-->
            <div style="display: none" id="d_7">
                <iframe style="width: 100%;" scrolling="no" frameborder="0" id="Iframe<%=TXCommons.PictureModular.PremisesPictureType.Effect.ToString()%>"
                    name="frame<%=TXCommons.PictureModular.PremisesPictureType.Effect.ToString()%>"
                    src="/Premises/UploadPremisesPhoto?guid=<%=Model.InnerCode%>&picturetype=<%=TXCommons.PictureModular.PremisesPictureType.Effect.ToString()%>&cityId=<%=Model.CityId%>&id=<%=Model.Id%>"
                    allowtransparency="true"></iframe>
            </div>
            <!--end7-->
        </div>
        <div class="boxcent20 mt15">
            <%Html.BeginForm("UploadPremisesPhotoSub", "Premises", FormMethod.Post);%>
            <%-- <%=TXCommons.PictureModular.GetPicture.GetPremisesPictureList(Model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString(), Model.CityId).Count() %>
            --%>
            <%-- <input type="submit" class="bnt_w120" value="发布楼盘" />--%>
            <%Html.EndForm();%>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
        // JavaScript Document
        //*发布楼盘-楼盘相册切换
        $(function () {
            $(function () {
                var $span_a = $(".choose span a");
                $span_a.click(function () {
                    $(this).addClass("cur")
				   .siblings().removeClass("cur");
                    var index = $span_a.index(this);
                    $("#ddivd > div")
					.eq(index).show()
					.siblings().hide();
                })
            })
            divshow();

            $(".big_img a.close").click(function () {
                $("div.big_img").hide();
                return false;
            });

        })
        function divshow() {

            var t = getQueryString("t")
            if (t && t >= 1 && t <= 8) {
                $("#a_" + t).addClass("cur").siblings().removeClass("cur");
                $("#d_" + t).show().siblings().hide();
            }
            else {
                $("#a_1").addClass("cur").siblings().removeClass("cur");
                $("#d_1").show().siblings().hide();
            }
        }
        function getQueryString(key) {
            var value = "";
            //获取当前文档的URL,为后面分析它做准备
            var sURL = window.document.URL;
            //URL中是否包含查询字符串
            if (sURL.indexOf("?") > 0) {
                //分解URL,第二的元素为完整的查询字符串
                //即arrayParams[1]的值为【first=1&second=2】
                var arrayParams = sURL.split("?");
                //分解查询字符串
                //arrayURLParams[0]的值为【first=1 】
                //arrayURLParams[2]的值为【second=2】
                var arrayURLParams = arrayParams[1].split("&");
                //遍历分解后的键值对
                for (var i = 0; i < arrayURLParams.length; i++) {
                    //分解一个键值对
                    var sParam = arrayURLParams[i].split("=");
                    if ((sParam[0] == key) && (sParam[1] != "")) {
                        //找到匹配的的键,且值不为空
                        value = sParam[1];
                        break;
                    }
                }
            }
            return value;
        }
    </script>
</asp:Content>
