<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%
            TXNewHouseWeb.Common.SEOModel seo = ViewData["Seo"] as TXNewHouseWeb.Common.SEOModel;
        %>
        <%=seo==null?string.Empty:seo.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="bgcolor">
            <div class="w1000 line_h50 font14 col666">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>" class="blue">北京新房</a> > <a href=""
                    class="blue">工具</a> > 购房系统自查系统
            </div>
        </div>
        <div class="mt15 w1190  mb15 clearFix">
            <div class="manual_tit bort_7ebc11">
                购房资格自查系统</div>
            <div class="system_box">
                <p class="pt15 pl15 pb10 pr20">
                    购房资格自查系统为您提供了方便快捷的自查功能，通过简单的选择符合您实际情况的选项来自助查询您是否在具有购房资格，并根据您的实际情况为您提供贷款首付比例、贷款利率以及购房所需材料等参考信息</p>
                <div class="system_tit">
                    第一步：选择您在拿购买房子</div>
                <div class="system_city">
                    <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-gfzc.html">北京</a>│<a href="<%=TXCommons.GetConfig.GetSiteRoot%>/Tools/GouFangZiChaSH">上海</a>
                   <%-- │<a href="#">广州</a>│<a href="#">深圳</a>│<a href="#">天津</a>│<a
                        href="#">苏州</a>│<a href="#">杭州</a>│<a href="#">成都</a>│<a href="#">武汉</a>│<a href="#">南京</a>│<a
                            href="#">沈阳</a>│<a href="#">青岛</a>│<a href="#">西安</a>│<a href="#">长沙</a>│<a href="#">大连</a>│<a
                                href="#">合肥</a>│<a href="#">贵阳三亚</a>│<a href="#">海南</a>│<a href="#">昆明</a>│<a href="#">济南</a>│<a
                                    href="#">郑州</a>│<a href="#">无锡</a>│<a href="#">宁波</a>│<a href="#">南昌</a>│<a href="#">长春</a>│<a
                                        href="#">东莞</a>│<a href="#">南宁</a>│<a href="#">福州</a>│<a href="#">太原</a>│<a href="#">昆山</a>│<a
                                            href="#">佛山</a>│<a href="#">兰州</a>│<a href="#">厦门绍兴</a>│<a href="#">银川</a>│<a href="#">石家庄</a>│<a
                                                href="#">哈尔滨</a>│<a href="#">呼和浩特</a>--%></div>
                <div class="system_tit">
                    第二步：根据你的情况选择条件</div>
             <%--   <div class="system_city">
                    <div class="col666 pl5">
                        首先，您或您的家庭考虑购房的是属于以下哪种情况？</div>
                    <div>
                        <input type="radio" name="1" class="btn_radio" />三口之家（购房人夫妻双方+未成年子女）</div>
                    <div>
                        <input type="radio" name="1" class="btn_radio" />两口子（购房人夫妻双方）</div>
                    <div>
                        <input type="radio" name="1" class="btn_radio" />个人（未婚成年子女）</div>
                    <div class="mt15 mb15 pl5">
                        <input type="button" class="btn_grey_w66" value="提交" /></div>
                </div>--%>
                               
<div id="table0" class="system_city" >
<table width="500" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="144"></td>
    <td width="356" height="29"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left" class="STYLE5">首先，您或您的家庭考虑购房的是属于以下哪种情况？</div></td>
    </tr>
  <tr>
    <td rowspan="4"><%-- --%></td>
    <td height="50"><div align="left"><a href="javascript:selectdo(1);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="20" height="20" border="0"> 三口之家（购房人夫妻双方+未成年子女）</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><a href="javascript:selectdo(1);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="20" height="20" border="0"> 两口子（购房人夫妻双方）</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><a href="javascript:selectdo(1);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="20" height="20" border="0"> 个人（未婚成年子女）</a></div></td>
  </tr>
  <tr>
    <td height="30"></td></tr>
      <tr>
    <td width="144"></td>
    <td width="356" height="20"></td>
  </tr>
</table>
				 </div>


<div id="table1" style="display:none;" class="system_city" >
<table width="500" border="0" align="center" cellpadding="0" cellspacing="0" class="big">
  <tr>
    <td width="144"></td>
    <td width="356" height="20"></td>
    </tr>
  <tr>
    <td height="50" colspan="2"><div align="left" class="STYLE5">您是否是上海市户籍？</div></td>
    </tr>
  <tr>
    <td rowspan="3"> </td>
    <td height="50"><div align="left"><a href="javascript:selectdo(2);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0" /> 是</a></div></td>
    </tr>
  <tr>
    <td height="50"><div align="left"><a href="javascript:selectdo(3);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0" /> 不是</a></div></td>
    </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(0);"><<返回上一层</a></div></td>
    </tr>
    <tr>
      <td></td>
      <td height="30"></td>
      </tr>
</table>
</div>

<div id="table2" style="display:none;" class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td width="147"></td>
    <td width="353" height="20"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left"><span class="STYLE5">您及您的家庭，在上海已经购买了几套住宅？</span><br>
      （仅限住宅，商住或商业性质产权物业不纳入）</div></td>
    </tr>
  <tr>
    <td rowspan="4"> </td>
    <td height="50"><div align="left"><a href="javascript:selectdo(4);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0"> 0套</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><a href="javascript:selectdo(5);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0"> 1套</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><a href="javascript:selectdo(6);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0"> 2套及以上</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(1);"><<返回上一层</a></div></td>
  </tr>
    <tr>
      <td></td>
      <td height="30"></td></tr>
</table>
</div>

<div id="table3" style="display:none;" class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td width="138"></td>
    <td width="362" height="20"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left" class="STYLE5">您是否持有2年内在上海缴纳1年以上个人所得税缴纳证明或社会保险（城镇社会保险）缴纳证明？</div></td>
    </tr>
  <tr>
    <td rowspan="3"> </td>
    <td height="50"><div align="left"><a href="javascript:selectdo(7);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0"> 有相关证明</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><a href="javascript:selectdo(8);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images//1306137986236.gif" width="16" height="15" border="0"> 没有相关证明</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(1);"><<返回上一层</a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<div id="table4" style="display:none;" class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td height="20" colspan="2"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left" class="STYLE5">恭喜您，您是上海户籍且为首次置业，可在沪购买住宅！</div></td>
    </tr>
  <tr>
    <td width="162" rowspan="2"> </td>
    <td width="338" height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">购买首套住房的家庭，贷款首付款比例不得低于30％，贷款利率可享受最低7折优惠（目前沪上各大银行实际均执行基准利率）。</div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">购房所需资料：上海户籍居民及家庭：购房人家庭成员户口簿、结婚证（或单身证明）、本市拥有住房情况信息查询结果（房产税征免认定） </div></td>
  </tr>
  <tr>
    <td> </td>
        <td height="50"><div align="right"><a href="javascript:selectdo(0);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images//1306138730779.gif" border="0"/><strong>重新测试</strong></a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<div id="table5" style="display:none;" class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td height="20" colspan="2"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left" class="STYLE5">恭喜您，您是上海户籍且仅有1套住宅，可再购买1套住宅！</div></td>
    </tr>
  <tr>
    <td width="181" rowspan="2" valign="middle"> </td>
    <td width="319" height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">对居民家庭向商业银行贷款购买第二套住房的，其首付款比例不得低于60%，贷款利率不得低于基准利率的1.1倍。对为改善居住条件购买第二套住房的，住房公积金个人贷款首付比例不得低于60%，贷款利率不得低于基准利率的1.1倍。</div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">购房所需资料：上海户籍居民及家庭：购房人家庭成员户口簿、结婚证（或单身证明）、本市拥有住房情况信息查询结果（房产税征免认定） </div></td>
  </tr>
  <tr>
    <td> </td>
    <td height="50"><div align="right"><a href="javascript:selectdo(0);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306138730779.gif" border="0"/><strong>重新测试</strong></a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<div id="table6" style="display:none;" class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td height="100" colspan="2"><div align="left" class="STYLE5">很抱歉，您是上海户籍，但已经购置2套或以上住宅，不能在上海再购买住宅！</div></td>
    </tr>
  <tr>
    <td width="44" rowspan="2"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306139461428.jpg" width="130" height="300" /></td>
    <td width="456" height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">沪九条明确指出，对在本市已拥有2套及以上住房的本市户籍居民家庭暂停在本市向其售房。违反规定购房的，不予办理房地产登记。</div></td>
  </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(0);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306138730779.gif" border="0"/><strong>重新测试</strong></a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<div id="table7" style="display:none;" class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td width="69"></td>
    <td width="431" height="20"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left"><span class="STYLE5">您及您的家庭，在上海已经购买了几套住宅？</span><br>
      （仅限住宅，商住或商业性质产权物业不纳入）</div></td>
    </tr>
  <tr>
    <td rowspan="4"> </td>
    <td height="50"><div align="left"><a href="javascript:selectdo(9);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0"> 0套</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><a href="javascript:selectdo(10);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0"> 1套</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><a href="javascript:selectdo(11);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306137986236.gif" width="16" height="15" border="0"> 2套及以上</a></div></td>
  </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(3);"><<返回上一层</a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<div id="table8" style="display:none;"  class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td width="46"></td>
    <td width="454" height="20"></td>
  </tr>
  <tr>
    <td height="120" colspan="2"><div align="left" class="STYLE5">很抱歉，您是非上海户籍，且不能提供2年内在本市累计缴纳1年以上个人所得税缴纳证明或社会保险（城镇社会保险）缴纳证明，不能在上海再购买住宅！</div></td>
    </tr>
  <tr>
    <td rowspan="2"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306139461428.jpg" width="130" height="300" /></td>
    <td height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">沪九条明确指出，对于不能提供2年内在本市累计缴纳1年以上个人所得税缴纳证明或社会保险（城镇社会保险）缴纳证明的非本市户籍居民家庭，暂停在本市向其售房。违反规定购房的，不予办理房地产登记。</div></td>
  </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(0);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306138730779.gif" border="0"/><strong>重新测试</strong></a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<div id="table9" style="display:none;"  class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td width="183"></td>
    <td width="317" height="20"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left" class="STYLE5">恭喜您，您是非上海户籍但并未购置过住宅，可在沪购买1套住宅！</div></td>
    </tr>
  <tr>
    <td rowspan="3"> </td>
    <td height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">购买首套住房的家庭，贷款首付款比例不得低于30％，贷款利率可享受最低7折优惠（目前沪上各大银行实际均执行基准利率）。</div></td>
  </tr>
  <tr>
    <td height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">购房所需资料：非上海户籍居民及家庭：自购房之日起算的前2年内，在本市累计缴纳1年以上个人所得税缴纳证明或社会保险（城镇社会保险）缴纳证明。</div></td>
  </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(0);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306138730779.gif" border="0"/><strong>重新测试</strong></a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<div id="table10" style="display:none;"  class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td width="59"></td>
    <td width="441" height="20"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left" class="STYLE5">很抱歉，您是非上海户籍，且已有一套住宅，不能在上海再购买住宅！</div></td>
    </tr>
  <tr>
    <td rowspan="2"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306139461428.jpg" width="130" height="300" /></td>
    <td height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">沪九条明确指出，对于拥有1套及以上住房的非本市户籍居民家庭暂停在本市向其售房。违反规定购房的，不予办理房地产登记。</div></td>
  </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(0);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306138730779.gif" border="0"/><strong>重新测试</strong></a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<div id="table11" style="display:none;"  class="system_city" >
<table width="500" border="0" cellspacing="0" cellpadding="0" class="big" align="center">
  <tr>
    <td width="61"></td>
    <td width="439" height="20"></td>
  </tr>
  <tr>
    <td height="50" colspan="2"><div align="left" class="STYLE5">很抱歉，您是非上海户籍，且已有2套及以上住宅，不能在上海再购买住宅！</div></td>
    </tr>
  <tr>
    <td rowspan="2"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306139461428.jpg" width="130" height="300" /></td>
    <td height="50"><div align="left"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1305791108722.gif" width="16" height="15" border="0">沪九条明确指出，对于拥有1套及以上住房的非本市户籍居民家庭暂停在本市向其售房。违反规定购房的，不予办理房地产登记。</div></td>
  </tr>
  <tr>
    <td height="50"><div align="right"><a href="javascript:selectdo(0);"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/1306138730779.gif" border="0"/><strong>重新测试</strong></a></div></td>
  </tr>
      <tr>
        <td></td>
        <td height="30"></td></tr>
</table>
</div>

<script type="text/javascript">
    function selectdo(type) {
        var num = 12;
        var showname = 'table' + type;
        //alert(name);
        for (var i = 0; i < num; i++) {
            var name = 'table' + i;
            document.getElementById(name).style.display = 'none';
        }
        document.getElementById(showname).style.display = '';
    }
</script>                
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
</asp:Content>
