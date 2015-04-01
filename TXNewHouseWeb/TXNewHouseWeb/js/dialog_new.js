/**
* jQuery的Dialog插件
* Author:tyh
* 操作类型：operation type = opertype 
* @param object contents内容
* @param object options 选项
* @return void
*
* 使用方法：
* 引入jquery.js和dialog.js
* 显示弹出层方法：Dialog.showDialog("url", "xxx.htm")
* 设置弹出层拖动功能：opertype="dialog_title" 如：<div opertype="dialog_title">标题栏</div>
* 设置弹出层关闭按钮：opertype="dialog_close" 如：<input type="button" opertype="dialog_close" value="关闭" />
* 设置弹出层隐藏按钮：opertype="dialog_hide" 如：<input type="button" opertype="dialog_hide" value="隐藏" />
* 关闭弹出层方法：Dialog.closeDialog()
* 隐藏弹出层方法：Dialog.hideDialog()
*/
function Dialog(contents, options) {
    var defaults_con = {//默认值-内容
        type: "html",   //类型
        value: "",      //值
        img: null       //loading图片路径
    };
    var defaults_opt = {//默认值-属性 
        draggable: true, //是否移动 
        modal: true,    //是否是模态对话框 
        center: true,   //是否居中。 
        fixed: true,    //是否跟随页面滚动。
        time: 0,        //自动关闭时间，为0表示不会自动关闭。 
        id: false       //对话框的id，若为false，则由系统自动产生一个唯一id。
    };
    // JQuery合并，方法后面的参数如果和前面的参数存在相同的名称，那么后面的会覆盖前面的参数值。
    var contents = $.extend(defaults_con, contents);
    var options = $.extend(defaults_opt, options);
    options.id = options.id ? options.id : 'dialog-' + Dialog._count; // 唯一ID
    var overlayId = options.id + '-overlay'; // 遮罩层ID
    var timeId = null;  // 自动关闭计时器 
    var isShow = false;
    var isIe = /msie/.test(navigator.userAgent.toLowerCase()); //jquery1.9新的检查ie6方式// $.browser.msie;
    var isIe6 = 'undefined' == typeof (document.body.style.maxHeight); //jquery1.9新的检查ie6方式//$.browser.msie && ('6.0' == $.browser.version);
    var dialog = this;

    //设置dialog容器层
    showDialog = $('<div id="' + options.id + '"></div>').hide();
    $('body').append(showDialog); //加入html的body中

    /******
    * 重置对话框的位置
    * 主要是在需要居中的时候，每次加载完内容，都要重新定位
    * @var创建私有方法，如同private
    * @return void
    ******/
    var resetPos = function () {
        ///<summary>重置对话框的位置</summary>
        /* 是否需要居中定位，必需在已经知道了dialog元素大小的情况下，才能正确居中，也就是要先设置dialog的内容。 */
        if (options.center) {
            var left = ($(window).width() - showDialog.width()) / 2;
            var top = ($(window).height() - showDialog.height()) / 2;
            if (!isIe6 && options.fixed) {
                showDialog.css({ top: top, left: left });
            }
            else {
                showDialog.css({ top: top + $(document).scrollTop(), left: left + $(document).scrollLeft() });
            }
        }
    }

    /******
    * 初始化位置及一些事件函数
    * 其中的this表示Dialog对象而不是init函数
    * @var创建私有方法，如同private
    * @return void
    ******/
    var init = function () {
        ///<summary>初始化位置及一些事件函数</summary>
        /* 是否需要初始化背景遮罩层 */
        if (options.modal) {
            $('body').append('<div id="' + overlayId + '" class="dialog-overlay"></div>');
            $('#' + overlayId).css({
                'left': 0,
                'top': 0,
                'width': $(document).width(),
                'height': $(document).height(),
                'z-index': ++Dialog._zindex,
                'position': 'absolute',
                'background': '#000',
                'opacity': '0.2'
            }).hide();
            $(window).resize(function () {
                $('#' + overlayId).css({
                    'width': $(document).width()
                });
            });
        }
        showDialog.css({ 'z-index': ++Dialog._zindex, 'position': options.fixed ? 'fixed' : 'absolute' });
        /*  IE6 兼容fixed代码 */
        if (isIe6 && options.fixed) {
            showDialog.css('position', 'absolute');
            resetPos();
            var top = parseInt(showDialog.css('top')) - $(document).scrollTop();
            var left = parseInt(showDialog.css('left')) - $(document).scrollLeft();
            $(window).scroll(function () {
                showDialog.css({ 'top': $(document).scrollTop() + top, 'left': $(document).scrollLeft() + left });
            });
        }
        // 自动关闭 
        if (0 != options.time) { timeId = setTimeout(this.close, options.time); }
    }

    /******
    * 设置对话框的内容 
    * @var创建私有方法，如同private
    * @param string obj 可以是HTML文本
    * @return void
    ******/
    var setContent = function (obj) {
        ///<summary>设置对话框的内容</summary>
        ///<param name="obj">contents对象</param>
        if ('object' == typeof (obj)) {
            //显示loading
            if (contents.img != null) {
                showDialog.html('<div style="font-size: 12px;"><center><br/>&nbsp;<img src="' + contents.img + '" height="15px" width="15px" />&nbsp;加载中...<br/></center></div>');
            }
            switch (obj.type.toLowerCase()) {
                case 'id': // 将ID的内容复制过来，原来的还在。
                    showDialog.html($('#' + obj.value).html());
                    break;
                case 'img':
                    $('<img alt="" />').load(function () { showDialog.empty().append($(this)); resetPos(); })
                    .attr('src', obj.value);
                    break;
                case 'url':
                    $.ajax({ url: obj.value.indexOf("?") > -1 ? obj.value + "&m=" + Math.random() : obj.value + "?m=" + Math.random(),
                        success: function (html) {
                            showDialog.html(html);
                            resetPos();
                            setBindEvent();
                        },
                        error: function (xml, textStatus, error) {
                            showDialog.html('<div><center><br/>出错啦！<br/></center></div>')
                        }
                    });
                    break;
                case 'iframe':
                    showDialog.append($('<iframe src="' + obj.value + '" />'));
                    break;
                case 'text':
                    showDialog.html(obj.value);
                    break;
                default:
                    showDialog.html(obj.value);
                    break;
            }
        }
        else {
            showDialog.html(obj);
        }
    }

    /******
    * 绑定对话框的事件
    * @var创建私有方法，如同private
    * @return void
    ******/
    var setBindEvent = function () {
        ///<summary>绑定对话框的事件</summary>
        //绑定关闭按钮事件
        showDialog.find("[opertype='" + Dialog.btnClose + "']").bind('click', dialog.close);
        //绑定隐藏按钮事件
        showDialog.find("[opertype='" + Dialog.btnHide + "']").bind('click', dialog.hide);
        /* 以下代码处理框体是否可以移动 */
        var mouse = { x: 0, y: 0 };
        function moveDialog(event) {
            var e = window.event || event;
            var top = parseInt(showDialog.css('top')) + (e.clientY - mouse.y);
            var left = parseInt(showDialog.css('left')) + (e.clientX - mouse.x);
            showDialog.css({ top: top, left: left });
            mouse.x = e.clientX;
            mouse.y = e.clientY;
        };
        showDialog.find("[opertype='" + Dialog.divTitle + "']").mousedown(function (event) {
            if (!options.draggable) { return; }

            var e = window.event || event;
            mouse.x = e.clientX;
            mouse.y = e.clientY;
            $(document).bind('mousemove', moveDialog);
        });
        $(document).mouseup(function (event) {
            $(document).unbind('mousemove', moveDialog);
        });
    }

    /******
    * 显示对话框
    * @this创建公有方法，如同public
    * @return void
    ******/
    this.show = function () {
        ///<summary>显示对话框</summary>
        if (undefined != options.beforeShow && !options.beforeShow())
        { return; }
        /******
        * 获得某一元素的透明度。IE从滤境中获得。
        * @return float
        ******/
        var getOpacity = function (id) {
            if (!isIe)
            { return $('#' + id).css('opacity'); }

            var el = document.getElementById(id);
            return (undefined != el
                    && undefined != el.filters
                    && undefined != el.filters.alpha
                    && undefined != el.filters.alpha.opacity)
                ? el.filters.alpha.opacity / 100 : 0.2;
        }
        /* 是否显示背景遮罩层 */
        if (options.modal)
        { $('#' + overlayId).fadeTo('fast', getOpacity(overlayId)); }
        showDialog.fadeTo('fast', 1, function () {
            if (undefined != options.afterShow) { options.afterShow(); }
            isShow = true;
        });
        // 自动关闭 
        if (0 != options.time) { timeId = setTimeout(this.close, options.time); }
        resetPos();
    }

    /******
    * 隐藏对话框，但并不取消窗口内容
    * @this创建公有方法，如同public
    * @return void
    ******/
    this.hide = function () {
        ///<summary>隐藏对话框，但并不取消窗口内容</summary>
        if (!isShow) { return; }
        if (undefined != options.beforeHide && !options.beforeHide())
        { return; }
        showDialog.fadeOut('fast', function () {
            if (undefined != options.afterHide) { options.afterHide(); }
        });
        if (options.modal)
        { $('#' + overlayId).fadeOut('fast'); }
        isShow = false;
    }

    /******
    * 关闭对话框 
    * @this创建公有方法，如同public
    * @return void
    ******/
    this.close = function () {
        ///<summary>关闭对话框</summary>
        if (undefined != options.beforeClose && !options.beforeClose())
        { return; }
        showDialog.fadeOut('slow', function () {
            $(this).remove();
            isShow = false;
            if (undefined != options.afterClose) { options.afterClose(); }
        });
        if (options.modal)
        { $('#' + overlayId).fadeOut('slow', function () { $(this).remove(); }); }
        clearTimeout(timeId);
    }

    //初始化方法
    init();
    setContent(contents)
    //为全局变量赋值
    Dialog._count++;
    Dialog._zindex++;
}
//为Dialog声明全局变量
Dialog._zindex = 500;
Dialog._count = 1;
Dialog.version = '0.1 beta';
Dialog.divTitle = "dialog_title";   //标题栏标识
Dialog.btnClose = "dialog_close";   //关闭按钮标识
Dialog.btnHide = "dialog_hide";     //隐藏按钮标识
//为Dialog声明静态方法
Dialog.closeDialog = function () {
    ///<summary>关闭对话框</summary>
    $("[opertype='" + Dialog.btnClose + "']").trigger("click", [true]);
}
Dialog.hideDialog = function () {
    ///<summary>隐藏对话框，但并不取消窗口内容</summary>
    $("[opertype='" + Dialog.btnHide + "']").trigger("click", [true]);
}
Dialog.showDialog = function (type, value, img) {
    ///<summary>显示对话框</summary>
    ///<param name="type">弹出层类型：id、img、url、iframe、text</param>
    ///<param name="value">弹出层值</param>
    new Dialog({ type: type, value: value, img: img }, null).show();
}