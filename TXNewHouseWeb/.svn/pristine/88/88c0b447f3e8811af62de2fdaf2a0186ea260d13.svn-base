<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% 
    string guid = ViewData["guid"] == null ? string.Empty : ViewData["guid"].ToString();
    List<TXCommons.PictureModular.PremisesPictureInfo> list = ViewData["list"] as List<TXCommons.PictureModular.PremisesPictureInfo>;
    string picturetype = ViewData["picturetype"] == null ? string.Empty : ViewData["picturetype"].ToString();
    string cityId = ViewData["cityId"] == null ? "0" : ViewData["cityId"].ToString();
%>
<ul id='ul_box'>
    <% if (list != null && list.Count > 0)
       {
    %>
    <%
           foreach (TXCommons.PictureModular.PremisesPictureInfo info in list)
           {
               string desc = info.Desc;
               if (string.IsNullOrEmpty(desc))
               {
                   desc = "请填写照片描述";
               }
    %>
    <li id="imageBox_<%= info.ID %>">
        <dl>
            <dt>
                <img src="<%= info.Path + ".180_130.jpg" %>" width="180" height="130" />
                <a style="cursor: pointer;" data="imageBox_<%= info.ID %>" onclick="deleteimage('imageBox_<%= info.ID %>')"
                    class="close">&nbsp;</a><a style="cursor: pointer;" onclick="bigimageclick('<%= info.Path %>');return false;"
                        class="big_pho"></a>
                <% if (picturetype == TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString())
                   { %>
                <a style="cursor: pointer;" onclick="SetIsForce('<%= info.ID %>');return false;"
                    id="hidzla_<%= info.ID %>" name="hidzla_<%= info.ID %>" class="shetxt">
                    <%= info.IsForce == 0 ? "设置为主力户型" : "取消主力户型" %>
                </a>
                <input id="hidzl_<%= info.ID %>" name="hidzl_<%= info.ID %>" type="hidden" value="<%= info.IsForce %>" />
                <% }
                %>
            </dt>
            <dd>
                <span class="red mr5">*</span><input type="text" onfocus="javascript:if($(this).val()=='标题')$(this).val('');"
                    id="txt_<%= info.ID %>" name="txt" value='<%= string.IsNullOrEmpty(info.Title) ? "标题" : info.Title %>'
                    maxlength="15" />
                <div id="div_<%= info.ID %>" style="display: none;" class="tisbox">
                    <em>&nbsp;</em>请输入标题。</div>
                <textarea class="" id="des_<%= info.ID %>" name="des" onfocus="javascript:if($(this).val()=='请填写照片描述')$(this).val('');"><%= string.IsNullOrEmpty(info.Desc) ? "请填写照片描述" : info.Desc %></textarea>
                <div id="div2_des_<%= info.ID %>" style ="display:none;" class="tisbox"><em>&nbsp;</em></div>
            </dd>
        </dl>
    </li>
    <% }
    %>
    <%
       }
    %>
</ul>
<script type="text/javascript">
    function CreateImageBox(data) {

        var splitresult = data.split(',');
        var pictureurl = splitresult[0];
        var id = splitresult[1];
        var picturetype = '';
        if ('<%=picturetype%>' == '<%=TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString()%>') {
            picturetype = '<a style ="cursor:pointer;" onclick="SetIsForce(\'' + id + '\');return false;" id="hidzla_' + id + '" name="hidzla_' + id + '" class="shetxt">设置为主力户型</a><input id="hidzl_' + id + '" name="hidzl_' + id + '" type ="hidden" value="0" />';
        }
        var str = '<li id="imageBox_' + id + '">' +
          '<dl>' +
            '<dt><img src="' + pictureurl + '.180_130.jpg' + '" width="180" height="130" />' +
            '<a style ="cursor:pointer;" data="imageBox_' + id + '" onclick ="deleteimage(\'imageBox_' + id + '\')"  class="close">&nbsp;</a><a style ="cursor:pointer;" class="big_pho"  onclick="bigimageclick(\'' + pictureurl + '\');return false;"></a>' + picturetype + '</dt>' +
            '<dd>' +
              '<span class="red mr5">*</span><input type="text" onfocus=\'javascript:if($(this).val()==\"标题\")$(this).val(\"\");\' name="txt" id="txt_' + id + '" value="标题" maxlength=15 />' +
              '<div id="div_' + id + '" style ="display:none;" class="tisbox"><em>&nbsp;</em>请输入标题。</div>' +
              '<textarea class="" id="des_' + id + '"  name="des"  onfocus=\'javascript:if($(this).val()==\"请填写照片描述\")$(this).val(\"\");\'>请填写照片描述</textarea>' +
              '<div id="div2_des_' + id + '" style ="display:none;" class="tisbox"><em>&nbsp;</em></div>' +
            '</dd>' +
          '</dl>' +
        '</li>';

        $("#ul_box").append(str);
    }


    function DeleteImageBox(imgboxid) {

        var splitresult = imgboxid.split('_');
        var id = splitresult[1];
        var isb = true;
        var url = '';
        if ('<%=picturetype%>' == 'ROOMTYPE') {
            url = "/Premises/GetROOMTYPEImageUseCount.html?id" + id;
        }
        if ('<%=picturetype%>' == 'Plane') {
            url = "/Premises/GetPlaneImageUseCount.html?id" + id;
        }
        if (url) {
            $.get(url, function (data) {
                if (data > 0) {
                    isb = confirm("当前图片被使用中，如删除后则图片不能显示。");
                }
                if (isb) {
                    ajaxDeleteImage(id, imgboxid);
                }
            });
        }
        else {
            ajaxDeleteImage(id, imgboxid);
        }
    }

    function ajaxDeleteImage(id, imgboxid) {
        $.ajax({
            url: '<%=TXCommons .GetConfig.PremisesImgUploadBaseUrl%>Premises/Delete.ashx?guid=<%=guid %>&picturetype=<%=picturetype %>&minnum=0&cityId=<%=cityId %>&id=' + id,
            type: "get",
            dataType: "jsonp",
            jsonp: "callbackparam",
            jsonpCallback: "success_jsonpCallback",
            success: function (data) {

                $.map(data, function (item) {

                    if (item.result == "true") {
                        $("#" + imgboxid).remove();
                        SetIframeHeight();
                    }
                    else {
                        if (item.result != "false") {
                            alert(item.result);
                        }
                        else {
                            alert("删除图片出错!");
                        }
                    }
                });
            },
            error: function () { alert("删除图片出错!"); }
        });
    }

    //          $(".close").click("click", function () {

    //              if (confirm("确定执行当前操作？")) {

    //                  var imgboxid = $(this).attr("data");

    //                  if (imgboxid) {
    //                      DeleteImageBox(imgboxid);
    //                  }
    //              }
    //          });

    function deleteimage(imgboxid) {
        if (confirm("确定执行当前操作？")) {

            if (imgboxid) {
                DeleteImageBox(imgboxid);
            }
        }
    }

    function imagesub() {

        var str = "";
        var b = true;
        if ($("input[name='txt']").length == 0) {
            alert("请上传图片");
            return;
        }

        var err1 = false;
        $.each($("textarea[id^='des_']"), function(i, item) {
            var t = $.trim($(this).val());
            if (500 < t.length) {
                $("#div2_" + $(this).attr("id")).show();
                $("#div2_" + $(this).attr("id")).html("<em>&nbsp;</em>图片描述最多可输入500个汉字");
                err1 = true;
                return false;
            } else {
                $("#div2_" + $(this).attr("id")).hide();
                $("#div2_" + $(this).attr("id")).html("");
            }
            
            if (err1) {
                return false;
            }
        });

        SetIframeHeight(); // UploadPremisesPhoto.aspx文件中的方法

        if (err1) {
            return false;
        }

        var bb = false;
        $.each($("input[name='txt']"), function (i, item) {
            var a = $.trim($(item).val());
            $.each($("input[name='txt']"), function (i, item2) {
                if (a == $.trim($(item2).val()) && $(item).attr("id") != $(item2).attr("id")) {
                    bb = true;
                    return false;
                }
            });
            if (bb)
                return false;
        });

        if (bb) {
            alert("图片名称已存在请重新输入");
            return;
        }

        $.each($("input[name='txt']"), function (i, item) {

            var tid = $(item).attr("id");
            var splitresult = tid.split('_');
            var id = splitresult[1];
            var txt = $(item).val();
            if (txt == '' || txt == '标题') {
                $("#div_" + id).show();
                b = false;
            } else {
                $("#div_" + id).hide();
            }
            var des = $("#des_" + id).val();

            if (des == '请填写照片描述')
                des = '';
            var isf = "0";
            if ('<%=picturetype%>' == '<%=TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString()%>') {
                isf = $("#hidzl_" + id).val();
            }
            str += id + "(,)" + txt + "(,)" + isf + "(,)" + des + "(,)|!|";

        });

        if (b) {
            $("#data").val(str);
            var icount = 2;

            if ('<%=picturetype%>' == 'Effect') {
                icount = 1;
            }
            // alert($("#data").val());
            if ('<%=picturetype%>' == 'ROOMTYPE' || '<%=picturetype%>' == 'Plane' || '<%=picturetype%>' == 'Effect') {
                $.get("/Premises/GetImageCount.html?guid=<%=guid%>&picturetype=<%=picturetype%>&cityId=<%=cityId%>", function (data) {


                    if (data >= icount) {
                        $("form:first").submit();
                    } else {
                        alert('最少上传' + icount + '张图片');
                    }
                });
            } else {
                $("form:first").submit();
            }

        }
    }

    function bigimageclick(url) {
        $("#bigphoto").attr("src", url);
        $("div.big_img").show();
        return false;
    }

    function SetIsForce(id) {
        if ($("#hidzl_" + id).val() == 0) {
            $("#hidzl_" + id).val("1");
            $("#hidzla_" + id).html("取消主力户型");
        } else {
            $("#hidzl_" + id).val("0");
            $("#hidzla_" + id).html("设置为主力户型");
        }
    }

</script>
