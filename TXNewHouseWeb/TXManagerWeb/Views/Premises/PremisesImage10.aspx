<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_Premises_Imgs>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>-楼盘相册
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.css" %>"
        rel="stylesheet" type="text/css" />
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a><i>&gt;</i><a href="javascript:void(0);">发布楼盘</a><i>&gt;</i><a
                    href="javascript:void(0);">楼盘相册</a></span>
    </div>
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="outer">
            <div class="btit clearFix">
                <div class="tit_font fl">
                    <h6>
                        <span class="tex">楼盘相册</span></h6>
                </div>
            </div>
            <div class="choose mt15">
                <% string tUrl = Auxiliary.Instance.NhManagerUrl + "premises/premisesimage/" + Model.Id + "/0/" + (new Random().Next(9999, 999999)) + ".html?tage=";
                   string tUrl10 = Auxiliary.Instance.NhManagerUrl + "premises/premisesimage10/" + Model.Id + "/0/" + (new Random().Next(9999, 999999)) + ".html?tage="; %>
                <span><a id="t0" href="<%=tUrl + "0" %>">规划图</a> <a id="t1" href="<%=tUrl + "1" %>">
                    交通图</a> <a id="t2" href="<%=tUrl + "2" %>">位置图</a> <a id="t3" href="<%=tUrl + "3" %>">
                        实景图</a> <a id="t4" href="<%=tUrl + "4" %>">效果图</a> <a id="t5" href="<%=tUrl + "5" %>">
                            户型图</a> <a id="t6" href="<%=tUrl + "6" %>">楼栋平面图</a> <a id="t10" href="<%=tUrl10 + "10" %>">
                                楼盘封面广告图</a></span>
            </div>
            <div class="loubox clearFix">
                <div class="pl9px mb15">
                    <input type="button" name="uploadify" id="uploadify" value="上传图片" class="btn_w80 mr15" />
                    <span class="tisbox" style="display: inline-block; margin-top: 0px;"><em></em>最少上传一张，最多上传20张图片，请上传1190*200的图片，否则上传的图片将会在前端展示有问题。
                    </span>
                </div>
                <div id="img">
                </div>
                <ul id='ul_box' class="loubox_ul clearFix">
                    <% var list = Model.PremisesPictureInfo as List<TXCommons.PictureModular.PremisesPictureInfo>;
                       if (null != list)
                       {
                           foreach (var info in list)
                           {
                    %>
                    <li id="imageBox_<%= info.ID %>">
                        <dl>
                            <dt>
                                <img src="<%= info.Path + ".550_185.jpg" %>" width="960" height="150" alt="图片" />
                                <a style="cursor: pointer;" data="imageBox_<%= info.ID %>" onclick="premisesimgs.delPic('<%= info.ID %>')"
                                    class="close">&nbsp;</a><%--<a style="cursor: pointer;" onclick="premisesimgs.bigPic('<%= info.Path %>')"
                                        class="big_pho"></a>--%>
                                <% if (Model.Tag == 10) // 5
                                   { %>
                                <a style="cursor: pointer;" id="hidzla_<%= info.ID %>" class="shetxt" onclick="premisesimgs.setIsForce('<%= info.ID %>')">
                                    <%= info.IsForce == 0 ? "设置为广告图" : "取消广告图"%>
                                </a>
                                <input id="hidzl_<%= info.ID %>" type="hidden" value="<%= info.IsForce %>" />
                                <% } %>
                            </dt>
                            <dd>
                                <span class="red mr5">*</span><input type="text" id="txt_<%= info.ID %>" value='<%= info.Title %>'
                                    maxlength="15" placeholder="标题" />
                                <div id="err_title_<%= info.ID %>" style="display: none;" class="tisbox">
                                    <em>&nbsp;</em>请输入标题。</div>
                            </dd>
                        </dl>
                    </li>
                    <% }
                       } %>
                </ul>
                <div class="big_img1" style="display: none;">
                    <img alt="大图" id="bigphoto" src="<%=TXCommons.GetConfig.GetFileUrl("images/img_2.jpg") %>"
                        width="550" height="185" />
                    <a href="javascript:void(0);" class="close" onclick="premisesimgs.closeBigPic()">&nbsp;</a>
                </div>
                <div class="clear">
                </div>
                <div class="clearFix w900">
                    <input type="button" class="btn_w97_green" onclick="premisesimgs.save()" value="保存" />
                </div>
            </div>
        </div>
    </div>
        <script src="<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/jquery.uploadify.js" %>"
        type="text/javascript"></script>

    <script type="text/javascript">

        // 楼盘相册（广告版）
        var premisesimgs = {
            // 选项卡编号
            tagnum: <%= Model.Tag %>,
            
            // 默认大图图片地址
            defaultBigPicSrc: '<%= TXCommons.GetConfig.GetFileUrl("images/img_2.jpg") %>',

            // 默认上传最小图片数量(广告图)
            minuploadcount_type10: 1,
            
            // 设置选项卡标记
            signLabel: function() {
                $("#t" + premisesimgs.tagnum).addClass("cur");
            },
            
            // 将刚刚上传的图片显示出来
            // data格式：http://premisesimg.yfxs.cn/19/47/97/19479744-e514-4360-a92a-79ad9638ed03/f7cjzg.jpg,2392
            addUploadPicToImgDiv: function(data) {
                var arrData = data.split(',');
                var picUrl = arrData[0];
                var id = arrData[1];

                var html_RoomType = "";
                // 户型图(设置主力户型)
                if (premisesimgs.tagnum == 10) {
                    html_RoomType = "<a style=\"cursor:pointer;\" onclick=\"premisesimgs.setIsForce('" + id + "')\" id=\"hidzla_" + id + "\" class=\"shetxt\">设置为广告图</a><input id=\"hidzl_" + id + "\" name=\"hidzl_" + id + "\" type=\"hidden\" value=\"0\" />";
                }

                var html = "";
                html += "<li id=\"imageBox_" + id + "\">";
                html += "<dl>";
                html += "<dt><img src=\"" + picUrl + ".550_185.jpg\" width=\"550\" height=\"185\" alt=\"图片\" />";
                //html += "<a style=\"cursor:pointer;\" data=\"imageBox_" + id + "\" onclick=\"premisesimgs.delPic('" + id + "')\" class=\"close\">&nbsp;</a><a style=\"cursor:pointer;\" class=\"big_pho\" onclick=\"premisesimgs.bigPic('" + picUrl + "')\"></a>" + html_RoomType + "</dt>";
                html += "<a style=\"cursor:pointer;\" data=\"imageBox_" + id + "\" onclick=\"premisesimgs.delPic('" + id + "')\" class=\"close\">" + html_RoomType + "</dt>";
                html += "<dd>";
                html += "<span class=\"red mr5\">*</span><input type=\"text\" id=\"txt_" + id + "\" placeholder=\"标题\" maxlength=15 />";
                html += "<div id=\"err_title_" + id + "\" style=\"display:none;\" class=\"tisbox\"><em>&nbsp;</em>请输入标题。</div>";
                html += "</dd>";
                html += "</dl>";
                html += "</li>";

                $("#ul_box").append(html);

            },
            
            // 验证
            valid_uploadpic: function() {
                var arrPic = $("[id^=txt_]");
                if (0 == arrPic.length) {
                    alert("请上传图片");
                    return false;
                }

                // 验证标题为空
                var i, j;
                var flag = true;
                var id;
                for (i = 0; i < arrPic.length; i++) {
                    id = $(arrPic[i]).attr("id").split('_')[1];
                    if ("" == $.trim($(arrPic[i]).val())) {
                        $("#err_title_" + id).show();
                        flag = false;
                    } else {
                        $("#err_title_" + id).hide();
                    }
                }
                if (!flag) {
                    return false;
                }

                // 验证标题重复
                for (i = 0; i < arrPic.length; i++) {
                    for (j = 0; j < arrPic.length; j++) {
                        if (i == j) {
                            continue;
                        }
                        if ($.trim($(arrPic[i]).val()) == $.trim($(arrPic[j]).val())) {
                            alert("图片名称已存在请重新输入");
                            return false;
                        }
                    }
                }

                return true;

//                var count;
//                if (premisesimgs.tagnum == 10) {
//                    count = premisesimgs.getUploadedPicsCount();
//                    if (count < premisesimgs.minuploadcount_type10) {
//                        alert('最少上传' + premisesimgs.minuploadcount_type10 + '张图片');
//                        return false;
//                    }
//                }

//                return true;
            },
            
            // 获取已经上传的图片数量
            getUploadedPicsCount: function() {
                var url = '<%=Auxiliary.Instance.NhManagerUrl %>Premises/GetImageCount.html';
                var data = {
                    guid: '<%= Model.InnerCode %>',
                    picturetype: '<%= Model.PictureType %>',
                    cityId: '<%= Model.CityId %>',
                    ram: Math.random
                };
                var result = "";
                $.ajax({
                    url: url,
                    data: data,
                    type: 'POST',
                    cache: false,
                    async: false,
                    success: function(msg) {
                        result = msg;
                    }
                });
                return result;
            },
            
            // 删除图片
            delPic: function(id) {
                if (!confirm("确定执行当前操作？")) {
                    return;
                }

                premisesimgs.ajaxDelPic(id);
            },
            
            // ajax删除图片
            ajaxDelPic: function(id) {
                var url = '<%= TXCommons.GetConfig.PremisesImgUploadBaseUrl %>Premises/Delete.ashx';
                var data = {
                    guid: '<%= Model.InnerCode %>',
                    picturetype: '<%= Model.PictureType %>',
                    minnum: 0,
                    cityId: '<%= Model.CityId %>',
                    id: id
                };

                $.ajax({
                    url: url,
                    data: data,
                    cache: false,
                    type: "get",
                    dataType: "jsonp",
                    jsonp: "callbackparam",
                    jsonpCallback: "success_jsonpCallback",
                    success: function(msg) {
                        var i;
                        for (i = 0; i < msg.length; i++) {
                            if (msg[i].result == "true") {
                                $("#imageBox_" + id).remove();
                            } else if (msg[i].result == "false") {
                                alert("删除图片出错!");
                            } else {
                                alert(msg[i].result);
                            }
                        }
                    },
                    error: function() { alert("删除图片处理出错!"); }
                });
            },
            
            // 显示大图
            bigPic: function(url) {
                $("#bigphoto").attr("src", url);
                $("div.big_img1").show();
            },
            
            // 关闭大图
            closeBigPic: function() {
                $("#bigphoto").attr("src", premisesimgs.defaultBigPicSrc);
                $("div.big_img1").hide();
            },

            // 设置为广告图
            setIsForce: function(id) {
                if ($("#hidzl_" + id).val() == 0) {
                    $.each($(".shetxt"), function(i, item) {
                        if ($(item).next().val() == 1) {
                            $(item).next().val("0");
                            $(item).html("设置为广告图");
                        }
                    });
                    $("#hidzl_" + id).val("1");
                    $("#hidzla_" + id).html("取消广告图");
                } else {
                    $("#hidzl_" + id).val("0");
                    $("#hidzla_" + id).html("设置为广告图");
                }
//                //tyh修改以下代码，因为只能选定一个为广告图，所以其他的需要自动取消
//                if ($("#hidzl_" + id).val() == 0) {
//                    $("#hidzl_" + id).val("1");
//                    $("#hidzla_" + id).html("取消广告图");
//                } else {
//                    $("#hidzl_" + id).val("0");
//                    $("#hidzla_" + id).html("设置为广告图");
//                }
            },

            // 获取图片信息
            getPicsData: function() {
                var arrPic = $("[id^=txt_]");
                var i;
                var str = "";
                for (i = 0; i < arrPic.length; i++) {
                    var id = $(arrPic[i]).attr("id").split('_')[1];
                    var isf = "0";
                    if (10 == premisesimgs.tagnum) {
                        isf = $("#hidzl_" + id).val();
                    }

                    str += id + "(,)" + $.trim($(arrPic[i]).val()) + "(,)" + isf + "(,)" + $.trim($("#des_" + id).val()) + "(,)|!|";
                }
                return str;
            },

            // 保存
            save: function() {
                if (!premisesimgs.valid_uploadpic()) {
                    return;
                }

                admincoms.WaittingBar.show();

                var url = '<%= Auxiliary.Instance.NhManagerUrl %>premises/imagesub.html';
                var ajaxdata = {
                    guid: '<%= Model.InnerCode %>',
                    picturetype: '<%= Model.PictureType %>',
                    cityId: '<%= Model.CityId %>',
                    data: premisesimgs.getPicsData(),
                    pid: '<%= Model.Id %>',
                    ram: Math.random
                };

                $.post(url, ajaxdata, function(msg) {
                    admincoms.WaittingBar.close();
                    if (!msg.suc) {
                        alert(msg.msg);
                    } else {
                        alert("保存成功");
                        window.location.href = '<%= Auxiliary.Instance.NhManagerUrl %>premises/premisesimage10/<%= Model.Id %>/0/<%= new Random().Next(9999,999999) %>.html?tage=' + premisesimgs.tagnum;
                    }
                });
            }
        };

        $(document).ready(function () {
            
            // 设置选项卡标记
            premisesimgs.signLabel();

            $("#uploadify").uploadify({
                //基本参数
                swf: '<%= Auxiliary.Instance.NhManagerUrl + "js/uploadify/uploadify.swf" %>',
                uploader: '<%= TXCommons.GetConfig.PremisesImgUploadBaseUrl %>Premises/upload.ashx?para=<%= Model.InnerCode %>^<%= Model.PictureType %>^20^<%= Model.CityId %>',
                width: 80,
                height: 24,
                buttonText: "上传图片",
                buttonCursor: "hand",
                buttonClass: "btn_w80",
                buttonImage: '<%= TXCommons.GetConfig.GetFileUrl("Scripts/uploadify/btn_uploads.gif") %>',
                //规则参数
                fileSizeLimit: "5000KB",
                fileTypeExts: "*.jpg;*.jpeg;*.png;*.gif",
                fileTypeDesc: "请选择 jpg、jpeg、png、gif 文件",
                multi: true,

                removeTimeout: 1,

                //选择异常事件
                onSelectError: function (file, errorCode, errorMsg) {
                    if (errorCode == -110) {
                        //重写图片过大提示
                        this.queueData.errorMsg = "图片太大了，请选择5M以下大小的图片";
                    }
                },
                //上传成功事件
                onUploadSuccess: function (file, data, response) {
                    //添加一个图片框
                    if (data == "exceedmaxnum") {
                        $("#uploadify").uploadify('stop');
                        $("#uploadify").uploadify('cancel', '*');
                        alert("超出最大上传照片数量。");
                        return;
                    } else if (data != "error") {
                        premisesimgs.addUploadPicToImgDiv(data);
                    }
                },
                //上传异常事件
                onUploadError: function (file, errorCode, errorMsg, errorString) {
                    if (errorString != 'Cancelled') {
                        alert("上传错误，请稍候重试");
                    }
                }
            });
        });
        
    </script>
</asp:Content>