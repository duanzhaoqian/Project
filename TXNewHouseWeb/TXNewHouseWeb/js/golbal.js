/**
* golbal javascript 1.3
* Copyright (c) 2013  huang ji hua
* email:455582663@qq.com 
* datatime:2013/2/15 16:53 
**/

jQuery.cookie = function (h, m, j) { if (typeof m != "undefined") { j = j || {}; if (m === null) { m = ""; j.expires = -1 } var f = ""; if (j.expires && (typeof j.expires == "number" || j.expires.toUTCString)) { var d; if (typeof j.expires == "number") { d = new Date(); d.setTime(d.getTime() + (j.expires * 24 * 60 * 60 * 1000)) } else { d = j.expires } f = "; expires=" + d.toUTCString() } var k = j.path ? "; path=" + (j.path) : ""; var e = j.domain ? "; domain=" + (j.domain) : ""; var l = j.secure ? "; secure" : ""; document.cookie = [h, "=", encodeURIComponent(m), f, k, e, l].join("") } else { var c = null; if (document.cookie && document.cookie != "") { var b = document.cookie.split(";"); for (var g = 0; g < b.length; g++) { var a = jQuery.trim(b[g]); if (a.substring(0, h.length + 1) == (h + "=")) { c = decodeURIComponent(a.substring(h.length + 1)); break } } } return c } }; jQuery.setCookie = function (a, b) { $.cookie(a, b, { domain: "cjkjb.com", expires: 30, path: "/" }) }; jQuery.getCookie = function (b) { var a = $.cookie(b); if (!(a == null || a == "" || a == undefined)) { return a } else { return null } }; Array.prototype.del = function (b) { for (var a = 0; a < this.length; a++) { if (this[a] == b) { this.splice(a, 1); break } } return this }; String.prototype.replaceAll = function (b, c, a) { if (!RegExp.prototype.isPrototypeOf(b)) { return this.replace(new RegExp(b, (a ? "gi" : "g")), c) } else { return this.replace(b, c) } }; String.prototype.trim = function () { return this.replace(/(^\s*)(\s*$)/g, "") }; String.prototype.gblen = function () { var b = 0; for (var a = 0; a < this.length; a++) { if (this.charCodeAt(a) > 127 || this.charCodeAt(a) == 94) { b += 2 } else { b++ } } return b }; Date.prototype.Format = function (a) { var c = { "M+": this.getMonth() + 1, "d+": this.getDate(), "h+": this.getHours(), "m+": this.getMinutes(), "s+": this.getSeconds(), "q+": Math.floor((this.getMonth() + 3) / 3), S: this.getMilliseconds() }; if (/(y+)/.test(a)) { a = a.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length)) } for (var b in c) { if (new RegExp("(" + b + ")").test(a)) { a = a.replace(RegExp.$1, (RegExp.$1.length == 1) ? (c[b]) : (("00" + c[b]).substr(("" + c[b]).length))) } } return a }; function toDatetime(a) { a = a.split("(")[1].split(")")[0]; return new Date(parseInt(a)).Format("yyyy-MM-dd") } function day(b, a) { b = parseInt(b.split("(")[1].split(")")[0]); a = parseInt(a.split("(")[1].split(")")[0]); return (a - b) / (24 * 60 * 60 * 1000) } function isNullOrEmptyOrUndefined(a) { return a == null || a == "" || a == undefined ? true : false } function navSelected(a) { $(".user_nav .w1190 a").eq(a).addClass("current").siblings().removeClass("current") } $(function () { $(".pub_listGroupA  .ttxxccA .wycj i.thisIcn").hover(function () { $(this).toggleClass("hover") }); $(".dataTAB table tbody tr").hover(function () { $(this).addClass("mousHover") }, function () { $(this).removeClass("mousHover") }); $(".bidContent .bidPos").hover(function () { $(this).toggleClass("divhover") }); $(".pub_listGroupA li").hover(function () { $(this).toggleClass("hoverColor") }); $("input.btn_chujia").hover(function () { $(this).toggleClass("hover") }); $(".pub_nav .RPart ul li").hover(function () { $(this).toggleClass("hover") }); $(".Evt2013 .wycj a i.thisIcn").hover(function () { $(this).toggleClass("hover") }); $("input.btn_wt01").hover(function () { $(this).toggleClass("wtColor") }); $(".search_web .state input").click(function () { $(this).parent().children(".pub_checkListC").show() }); $(".search_web .state a").click(function () { $(this).parent().parent().parent().children(".pub_checkListC").hide() }); $(".helpbox ul li").hover(function () { $(this).toggleClass("hoverColor") }); $(".lb label").click(function () { var a = $(".lb label").index(this); $(".ct").hide(); $(".ct").eq(a).show() }) }); function islogin(a, b) { $.ajax({ type: "post", url: a, cache: false, dataType: "json", data: {}, beforeSend: function (c) { }, success: function (c) { if (c.IsLogin) { if ((c.utype == 11 && b == true) || c.utype == 12) { var d = c.utype == 12 ? "/Center/MyParticipateBid" : "/Landlord/FDHouse"; $(".pub_head .w1190 .LPart span").html("您好！<a href='" + d + "'>" + c.lname + "</a>&nbsp;<a href='/Login/LogOut/'>[退出]</a>").siblings(".bmww").hide(); $(".pub_head .w1190 .RPart a:first").attr("href", d) } else { $(".pub_head .w1190 .LPart span").html('您好！请 <a href="/Login/login/">[登录]</a><a href="/Register/Agent/">[注册]</a>').siblings(".bmww").show() } } else { $(".pub_head .w1190 .LPart span").html('您好！请 <a href="/Login/login/">[登录]</a><a href="/Register/Agent/">[注册]</a>').siblings(".bmww").show() } }, complete: function (d, c) { }, error: function () { } }) } function FilterSpecialCharacter(d) { var c = new RegExp("[ `~!！@#$￥%^……&*(（)）—=+{【}】\\|、;；:：'\"’‘”“,.，。<>《》/?？]"); var a = new Array(); for (var b = 0; b < d.length; b++) { a.push(d.substr(b, 1).replace(c, "").replace("[", "").replace("]", "").replace("-", "")) } return a.join("") } (function (a) { a.fn.inputlimiter = function (e) { e = a.extend({}, a.fn.inputlimiter.defaults, e || {}); var d = function (h) { var g = 0; for (var f = 0; f < h.length; f++) { if (e.is_cn_en) { if (Math.abs(h.charCodeAt(f)) <= 255) { g++ } else { g += 2 } } else { g++ } } return g }, b = function (j) { var h = 0; var g = 0; for (var f = 0; f < j.length; f++) { if (e.is_cn_en) { if (Math.abs(j.charCodeAt(f)) <= 255) { h++ } else { h += 2 } } else { h++ } if (h > e.limit) { g = f; break } } return g }, c = function (f) { var g = f.val(); if (d(g) > e.limit) { f.val(g.substring(0, b(g))) } }; a(this).bind({ keyup: function () { return c(a(this)) }, keydown: function () { return c(a(this)) } }) }; a.fn.inputlimiter.defaults = { limit: 50, is_cn_en: true} })(jQuery);