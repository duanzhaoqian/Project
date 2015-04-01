/****
search base data 
version:1.0
***/
//gobal data
var wylx = { prefix: '', text: ['楼盘', '住宅', '公寓', '别墅', '商铺', '写字楼'], value: ['all', 'house', 'flats', 'villa', 'shop', 'office']}//物业类型

//类型@
var leix = { prefix: 'l', text: ['不限', '住宅', '写字楼', '别墅', '商业'], value: ['', '1', '2', '3', '4'] };

//价格@
var price = { unit: '', prefix: 'p', text: ['不限', '10000以下', '10000-15000', '15000-20000', '20000-30000', '30000-40000', '40000以上'], value: ['', '1', '2', '3', '4', '5', '6'] }
//var price = { unit: '', prefix: 'p', text: ['不限', '10000以下', '10000-15000', '15000-20000', '20000-30000', '30000-40000', '40000以上'], value: ['', '0,9999', '10000,15000', '15000,20000', '20000,30000', '30000,40000', '40000,0'] }

//户型;@
var room = { prefix: 'r', text: ['不限', '1居', '2居', '3居', '4居', '5居', '5居以上'], value: ['', '1', '2', '3', '4', '5', '6'] }
var sousou = [];
sousou[0] = { prefix: '', text: ['按区域查询', '按地铁查询'], value: ['quyu', 'sub'] }
sousou[1] = { prefix: '', text: ['地图找房'], value: ['map'] }
//面积@
var area = { unit: '平米', prefix: 'm', text: ['不限', '50平米以下', '50-70平米', '70-90平米', '90-110平米', '110-130平米', '130-150平米', '150-200平米', '200-300平米', '300平米以上'], value: ['', '1', '2', '3', '4', '5', '6', '7', '8', '9'] }
//var area = { unit: '平米', prefix: 'm', text: ['不限', '50以下', '50-70', '70-90', '90-110', '110-130', '130-150', '150-200', '200-300', '300以上'], value: ['', '0,50', '50,70', '70,90', '90,110', '110,130', '130,150', '150,200', '200,300', '300,0'] }

//环线@
var loop_bj = { prefix: 'h', text: ['不限', '二环以内', '二至三环', '三至四环', '四至五环', '五至六环', '六环以外'], value: ['', '1', '2', '3', '4', '5', '6'] }//北京
var loop_sh = { prefix: 'h', text: ['不限', '内环以内', '中内环间', '中外环间', '外环以外'], value: ['', '1', '2', '3', '4'] }//上海
var loop_cd = { prefix: 'h', text: ['不限', '一环以内', '一至二环', '二至三环', '三环以外'], value: ['', '1', '2', '3', '4'] }//成都
var loop_tj = { prefix: 'h', text: ['不限', '内环以内', '内至中环', '中至外环', '外环以外'], value: ['', '1', '2', '3', '4'] }//天津
var loop_wh = { prefix: 'h', text: ['不限', '内环以内', '内至二环', '二至中环', '中环以外'], value: ['', '1', '2', '3', '4'] }//武汉
//建筑类别：板楼1 塔楼2 砖楼3 砖混4 平房5 钢混6

var jjlb = { prefix: 'd', text: ['不限', '板楼', '塔楼', '砖楼', '砖混', '平房', '钢混'], value: ['', '1', '2', '3', '4', '5', '6'] }

//特色楼盘
var tslp = { prefix: 't', text: ['不限', '打折优惠楼盘', '地铁沿线', '现房', '小户型', '低总价', '教育地产', '公园地产', '宜居生态地产', '综合体'], value: ['', '1', '2', '3', '4', '5', '6', '7', '8', '9'] }

//开盘时间
var kpsj = { prefix: 'k', text: ['不限', '本月开盘', '下月开盘', '三月内开盘', '六月内开盘', '前三个月已开', '前六个月已开'], value: ['', '1', '2', '3', '4', '5', '6'] }

//装修状况@1毛坯2简装修、3中等装修、4精装修、5豪华装修
var jxzk = { prefix: 'z', text: ['不限', '毛坯', '简装修', '精装修', '菜单式装修', '公共部分精装修', '全装修'], value: ['', '1', '2', '3', '4', '5', '6'] }

/*******下拉条件********/
//装修@
//var zx = { prefix: 'q', text: ['不限', '毛坯', '简装修', '中等装修', '精装修', '豪华装修'], value: ['', '1', '2', '3', '4', '5'] }

//排序
var sort = [];
sort[1] = { prefix: "s", text: ['默认排序', '按更新时间降序', '按房源评分降序', '按租金从低到高', '按租金从高到低', '按面积从低到高', '按面积从高到低'], value: ['', '1', '6', '2', '3', '4', '5'] }
sort[2] = { prefix: "s", text: ['默认排序', '按更新时间降序', '按房源评分降序', '按售价从低到高', '按售价从高到低', '按面积从低到高', '按面积从高到低'], value: ['', '1', '6', '2', '3', '4', '5'] }
sort[3] = { prefix: "", text: ['默认排序'], value: [''] }

var sx = { prefix: 'o', text: ['全部楼盘', '在售楼盘', '待售楼盘', '售罄楼盘'], value: ['', '1', '0', '2'] }

/* 该文件依赖于jquery.hashtable.js*/
//区域对象，商圈对象，地铁对象，地铁站对象
var quyu = { prefix: '', text: ['不限'], value: [''] }, sangquan = { prefix: '', text: ['不限'], value: [''] }, sub = { prefix: 'x', text: ['不限'], value: [''] }, station = { prefix: 'b', text: ['不限'], value: [''] };

//搜索选择条件-显示
var xztj = [];

//动态获取区域 商圈 地铁 站点  热点位置 等数据
// arrname(区域/地铁对像), py(城市), type(类型), parent（报价，砍价）
function getdatas(arrname, py, type, parent) {
    $.ajax({
        type: "post",
        url: globalvar.siteRoot + "/Ajax/GetBaseData",
        data: { type: type, city: py, pid: parent },
        cache: false,
        async: false,
        dataType: "json",
        success: function (result) {
            arrname.text = result.text;
            arrname.value = result.value;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(textStatus);
        }
    });
}

//获取楼盘特色
function getFeature() {
    $.ajax({
        type: "post",
        url: globalvar.siteRoot + "/Ajax/GetFeature",
        data: {},
        cache: false,
        async: false,
        dataType: "json",
        success: function (result) {
            tslp.text = result.text;
            tslp.value = result.value;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(textStatus);
        }
    });
}




