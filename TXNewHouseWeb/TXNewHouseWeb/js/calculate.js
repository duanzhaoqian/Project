var _$ = function(id){
   return document.getElementById(id);
}

function changeDIV(id){
	if(id == "1"){
		_$('title01').className = "maintitletd1";
		_$('title02').className = "maintitletda font01";
		_$('title03').className = "maintitletdb font01";
		_$('title04').className = "maintitletda font01";
		_$('title05').className = "maintitletd font01";
	}else if(id == "2"){
		_$('title01').className = "maintitletd font01";
		_$('title02').className = "maintitletda1";
		_$('title03').className = "maintitletdb font01";
		_$('title04').className = "maintitletda font01";
		_$('title05').className = "maintitletd font01";
	}else if(id == "3"){
		_$('title01').className = "maintitletd font01";
		_$('title02').className = "maintitletda font01";
		_$('title03').className = "maintitletdb1";
		_$('title04').className = "maintitletda font01";
		_$('title05').className = "maintitletd font01";
	}else if(id == "4"){
		_$('title01').className = "maintitletd font01";
		_$('title02').className = "maintitletda font01";
		_$('title03').className = "maintitletdb font01";
		_$('title04').className = "maintitletda1";
		_$('title05').className = "maintitletd font01";
	}else if(id == "5"){
		_$('title01').className = "maintitletd font01";
		_$('title02').className = "maintitletda font01";
		_$('title03').className = "maintitletdb font01";
		_$('title04').className = "maintitletda font01";
		_$('title05').className = "maintitletd1";
	}
	for(var i = 1; i < 6; i++){
		if(_$('calculate0' + i) != null){
			_$('calculate0' + i).style.display = "none";
		}
		if(_$('note0' + i) != null){
			_$('note0' + i).style.display = "none";
		}
	}
	_$('calculate0' + id).style.display = "block";
	_$('note0' + id).style.display = "block";
}

//62.180.9

//定义利率
lilv_array = new Array;          
//2004年之前的旧利率
lilv_array[1] = new Array;
lilv_array[1][1] = new Array;
lilv_array[1][2] = new Array;
lilv_array[1][1][5] = 0.0477;//商贷 1～5年 4.77%
lilv_array[1][1][10] = 0.0504;//商贷 5-30年 5.04%
lilv_array[1][2][5] = 0.0360;//公积金 1～5年 3.60%
lilv_array[1][2][10] = 0.0405;//公积金 5-30年 4.05%

//2005年	1月的新利率
lilv_array[2] = new Array;
lilv_array[2][1] = new Array;
lilv_array[2][2] = new Array;
lilv_array[2][1][5] = 0.0495;//商贷 1～5年 4.95%
lilv_array[2][1][10] = 0.0531;//商贷 5-30年 5.31%
lilv_array[2][2][5] = 0.0378;//公积金 1～5年 3.78%
lilv_array[2][2][10] = 0.0423;//公积金 5-30年 4.23%

//2006年	1月的新利率下限
lilv_array[3] = new Array;
lilv_array[3][1] = new Array;
lilv_array[3][2] = new Array;
lilv_array[3][1][5] = 0.0527;//商贷 1～5年 5.27%
lilv_array[3][1][10] = 0.0551;//商贷 5-30年 5.51%
lilv_array[3][2][5] = 0.0396;//公积金 1～5年 3.96%
lilv_array[3][2][10] = 0.0441;//公积金 5-30年 4.41%
			
//2006年	1月的新利率上限
lilv_array[4] = new Array;
lilv_array[4][1] = new Array;
lilv_array[4][2] = new Array;
lilv_array[4][1][5] = 0.0527;//商贷 1～5年 5.27%
lilv_array[4][1][10] = 0.0612;//商贷 5-30年 6.12%
lilv_array[4][2][5] = 0.0396;//公积金 1～5年 3.96%
lilv_array[4][2][10] = 0.0441;//公积金 5-30年 4.41%

//2006年	4月28日的新利率下限
lilv_array[5] = new Array;
lilv_array[5][1] = new Array;
lilv_array[5][2] = new Array;
lilv_array[5][1][5] = 0.0551;//商贷 1～5年 5.51%
lilv_array[5][1][10] = 0.0575;//商贷 5-30年 5.75%
lilv_array[5][2][5] = 0.0414;//公积金 1～5年 4.14%
lilv_array[5][2][10] = 0.0459;//公积金 5-30年 4.59%

//2006年	4月28日的新利率上限
lilv_array[6] = new Array;
lilv_array[6][1] = new Array;
lilv_array[6][2] = new Array;
lilv_array[6][1][5] = 0.0612;//商贷 1～5年 6.12%
lilv_array[6][1][10] = 0.0639;//商贷 5-30年 6.39%
lilv_array[6][2][5] = 0.0414;//公积金 1～5年 4.14%
lilv_array[6][2][10] = 0.0459;//公积金 5-30年 4.59%

//2006年	8月19日的新利率下限
lilv_array[7] = new Array;
lilv_array[7][1] = new Array;
lilv_array[7][2] = new Array;
lilv_array[7][1][5] = 0.0551;//商贷 1～5年 5.51%
lilv_array[7][1][10] = 0.0581;//商贷 5-30年 5.81%
lilv_array[7][2][5] = 0.0414;//公积金 1～5年 4.14%
lilv_array[7][2][10] = 0.0459;//公积金 5-30年 4.59%

//2006年	8月19日的新利率上限
lilv_array[8] = new Array;
lilv_array[8][1] = new Array;
lilv_array[8][2] = new Array;
lilv_array[8][1][5] = 0.0648;//商贷 1～5年 6.48%
lilv_array[8][1][10] = 0.0684;//商贷 5-30年 6.84%
lilv_array[8][2][5] = 0.0414;//公积金 1～5年 4.14%
lilv_array[8][2][10] = 0.0459;//公积金 5-30年 4.59%


//2007年	3月18日的新利率下限
lilv_array[9] = new Array;
lilv_array[9][1] = new Array;
lilv_array[9][2] = new Array;
lilv_array[9][1][5] = 0.0574;//商贷 1～5年 5.74%
lilv_array[9][1][10] = 0.0604;//商贷 5-30年 6.04%
lilv_array[9][2][5] = 0.0432;//公积金 1～5年 4.32%
lilv_array[9][2][10] = 0.0477;//公积金 5-30年 4.77%

//2007年	3月18日的新利率上限
lilv_array[10] = new Array;
lilv_array[10][1] = new Array;
lilv_array[10][2] = new Array;
lilv_array[10][1][5] = 0.0675;//商贷 1～5年 6.75%
lilv_array[10][1][10] = 0.0711;//商贷 5-30年 7.11%
lilv_array[10][2][5] = 0.0432;//公积金 1～5年 4.32%
lilv_array[10][2][10] = 0.0477;//公积金 5-30年 4.77%


//2007年	5月19日的新利率下限
lilv_array[11] = new Array;
lilv_array[11][1] = new Array;
lilv_array[11][2] = new Array;
lilv_array[11][1][5] = 0.0589;//商贷 1～5年 5.89%
lilv_array[11][1][10] = 0.0612;//商贷 5-30年 6.12%
lilv_array[11][2][5] = 0.0441;//公积金 1～5年 4.41%%
lilv_array[11][2][10] = 0.0486;//公积金 5-30年 4.86%%

//2007年	5月19日的新利率上限
lilv_array[12] = new Array;
lilv_array[12][1] = new Array;
lilv_array[12][2] = new Array;
lilv_array[12][1][5] = 0.0693;//商贷 1～5年 6.93%
lilv_array[12][1][10] = 0.0720;//商贷 5-30年 7.20%
lilv_array[12][2][5] = 0.0441;//公积金 1～5年 4.41%%
lilv_array[12][2][10] = 0.0486;//公积金 5-30年 4.86%%

//2007年	7月21日的新利率下限
lilv_array[13] = new Array;
lilv_array[13][1] = new Array;
lilv_array[13][2] = new Array;
lilv_array[13][1][5] = 0.0612;//商贷 1～5年 6.12%
lilv_array[13][1][10] = 0.06273;//商贷 5-30年 6.273%
lilv_array[13][2][5] = 0.0450;//公积金 1～5年 4.50%%
lilv_array[13][2][10] = 0.0495;//公积金 5-30年 4.95%%

//2007年	7月21日的新利率上限
lilv_array[14] = new Array;
lilv_array[14][1] = new Array;
lilv_array[14][2] = new Array;
lilv_array[14][1][5] = 0.0720;//商贷 1～5年 7.20%
lilv_array[14][1][10] = 0.0738;//商贷 5-30年 7.38%
lilv_array[14][2][5] = 0.0450;//公积金 1～5年 4.50%%
lilv_array[14][2][10] = 0.0495;//公积金 5-30年 4.95%%

//2007年	8月22日的新利率下限
lilv_array[15] = new Array;
lilv_array[15][1] = new Array;
lilv_array[15][2] = new Array;
lilv_array[15][1][5] = 0.06273;//商贷 1～5年 6.273%
lilv_array[15][1][10] = 0.06426;//商贷 5-30年 6.426%
lilv_array[15][2][5] = 0.0459;//公积金 1～5年 4.59%
lilv_array[15][2][10] = 0.0504;//公积金 5-30年 5.04%

//2007年	8月22日的新利率上限
lilv_array[16] = new Array;
lilv_array[16][1] = new Array;
lilv_array[16][2] = new Array;
lilv_array[16][1][5] = 0.0738;//商贷 1～5年 7.38%
lilv_array[16][1][10] = 0.0756;//商贷 5-30年 7.56%
lilv_array[16][2][5] = 0.0459;//公积金 1～5年 4.59%
lilv_array[16][2][10] = 0.0504;//公积金 5-30年 5.04%

//2007年	9月15日的新利率下限
lilv_array[17] = new Array;
lilv_array[17][1] = new Array;
lilv_array[17][2] = new Array;
lilv_array[17][1][5] = 0.06503;//商贷 1～5年 6.503%
lilv_array[17][1][10] = 0.06656;//商贷 5-30年 6.656%
lilv_array[17][2][5] = 0.0477;//公积金 1～5年 4.77%
lilv_array[17][2][10] = 0.0522;//公积金 5-30年 5.22%

//2007年	9月15日的新利率上限
lilv_array[18] = new Array;
lilv_array[18][1] = new Array;
lilv_array[18][2] = new Array;
lilv_array[18][1][5] = 0.0765;//商贷 1～5年 7.65%
lilv_array[18][1][10] = 0.0783;//商贷 5-30年 7.83%
lilv_array[18][2][5] = 0.0477;//公积金 1～5年 4.77%
lilv_array[18][2][10] = 0.0522;//公积金 5-30年 5.22%

//2007年	9月15日新利率(第二套房)
lilv_array[19] = new Array;
lilv_array[19][1] = new Array;
lilv_array[19][2] = new Array;
lilv_array[19][1][5] = 0.08415;//商贷 1～5年 8.415%
lilv_array[19][1][10] = 0.08613;//商贷 5-30年 8.613%
lilv_array[19][2][5] = 0.0477;//公积金 1～5年 4.77%
lilv_array[19][2][10] = 0.0522;//公积金 5-30年 5.22%


//2007年	12月21日的新利率下限
lilv_array[20] = new Array;
lilv_array[20][1] = new Array;
lilv_array[20][2] = new Array;
lilv_array[20][1][5] = 0.06579;//商贷 1～5年 6.579%
lilv_array[20][1][10] = 0.06656;//商贷 5-30年 6.656%
lilv_array[20][2][5] = 0.0477;//公积金 1～5年 4.77%
lilv_array[20][2][10] = 0.0522;//公积金 5-30年 5.22%

//2007年	12月21日的新利率上限
lilv_array[21] = new Array;
lilv_array[21][1] = new Array;
lilv_array[21][2] = new Array;
lilv_array[21][1][5] = 0.0851;//商贷 1～5年 8.514%
lilv_array[21][1][10] = 0.0861;//商贷 5-30年 8.613%
lilv_array[21][2][5] = 0.0477;//公积金 1～5年 4.77%
lilv_array[21][2][10] = 0.0522;//公积金 5-30年 5.22%

//2007年	12月21日新利率(第二套房)
lilv_array[22] = new Array;
lilv_array[22][1] = new Array;
lilv_array[22][2] = new Array;
lilv_array[22][1][5] = 0.0851;//商贷 1～5年 8.514%
lilv_array[22][1][10] = 0.0861;//商贷 5-30年 8.613%
lilv_array[22][2][5] = 0.0477;//公积金 1～5年 4.77%
lilv_array[22][2][10] = 0.0522;//公积金 5-30年 5.22%

//2008年	09月16日新利率下限
lilv_array[23] = new Array;
lilv_array[23][1] = new Array;
lilv_array[23][2] = new Array;
lilv_array[23][1][5] = 0.06426;//商贷 1～5年 6.426%
lilv_array[23][1][10] = 0.06579;//商贷 5-30年 6.579%
lilv_array[23][2][5] = 0.0459;//公积金 1～5年 4.59%
lilv_array[23][2][10] = 0.0513;//公积金 5-30年 5.13%

//2008年	09月16日新利率上限
lilv_array[24] = new Array;
lilv_array[24][1] = new Array;
lilv_array[24][2] = new Array;
lilv_array[24][1][5] = 0.0832;//商贷 1～5年 8.32%
lilv_array[24][1][10] = 0.0851;//商贷 5-30年 8.51%
lilv_array[24][2][5] = 0.0459;//公积金 1～5年 4.59%
lilv_array[24][2][10] = 0.0513;//公积金 5-30年 5.13%

//2008年	09月16日新利率(第二套房)
lilv_array[25] = new Array;
lilv_array[25][1] = new Array;
lilv_array[25][2] = new Array;
lilv_array[25][1][5] = 0.0832;//商贷 1～5年 8.32%
lilv_array[25][1][10] = 0.0851;//商贷 5-30年 8.51%
lilv_array[25][2][5] = 0.0459;//公积金 1～5年 4.59%
lilv_array[25][2][10] = 0.0513;//公积金 5-30年 5.13%

//2008年	10月09日新利率下限
lilv_array[26] = new Array;
lilv_array[26][1] = new Array;
lilv_array[26][2] = new Array;
lilv_array[26][1][5] = 0.062;//商贷 1～5年 6.20%
lilv_array[26][1][10] = 0.0635;//商贷 5-30年 6.35%
lilv_array[26][2][5] = 0.0432;//公积金 1～5年 4.32%
lilv_array[26][2][10] = 0.0486;//公积金 5-30年 4.86%

//2008年	10月09日新利率上限
lilv_array[27] = new Array;
lilv_array[27][1] = new Array;
lilv_array[27][2] = new Array;
lilv_array[27][1][5] = 0.0802;//商贷 1～5年 8.02%
lilv_array[27][1][10] = 0.0822;//商贷 5-30年 8.22%
lilv_array[27][2][5] = 0.0432;//公积金 1～5年 4.32%
lilv_array[27][2][10] = 0.0486;//公积金 5-30年 4.86%

//2008年	10月09日新利率(第二套房)
lilv_array[28] = new Array;
lilv_array[28][1] = new Array;
lilv_array[28][2] = new Array;
lilv_array[28][1][5] = 0.0802;//商贷 1～5年 8.02%
lilv_array[28][1][10] = 0.0822;//商贷 5-30年 8.22%
lilv_array[28][2][5] = 0.0432;//公积金 1～5年 4.32%
lilv_array[28][2][10] = 0.0486;//公积金 5-30年 4.86%

//2008年	10月27日新利率下限
lilv_array[29] = new Array;
lilv_array[29][1] = new Array;
lilv_array[29][2] = new Array;
lilv_array[29][1][5] = 0.051;//商贷 1～5年 5.10%
lilv_array[29][1][10] = 0.0523;//商贷 5-30年 5.23%
lilv_array[29][2][5] = 0.0405;//公积金 1～5年 4.05%
lilv_array[29][2][10] = 0.0459;//公积金 5-30年 4.59%

//2008年	10月27日新利率上限
lilv_array[30] = new Array;
lilv_array[30][1] = new Array;
lilv_array[30][2] = new Array;
lilv_array[30][1][5] = 0.0802;//商贷 1～5年 8.02%
lilv_array[30][1][10] = 0.0822;//商贷 5-30年 8.22%
lilv_array[30][2][5] = 0.0405;//公积金 1～5年 4.05%
lilv_array[30][2][10] = 0.0459;//公积金 5-30年 4.59%

//2008年	10月27日新利率基准
lilv_array[31] = new Array;
lilv_array[31][1] = new Array;
lilv_array[31][2] = new Array;
lilv_array[31][1][5] = 0.0729;//商贷 1～5年 7.29%
lilv_array[31][1][10] = 0.0747;//商贷 5-30年 7.47%
lilv_array[31][2][5] = 0.0405;//公积金 1～5年 4.05%
lilv_array[31][2][10] = 0.0459;//公积金 5-30年 4.59%

//2008年	10月27日新利率(第二套房)
lilv_array[32] = new Array;
lilv_array[32][1] = new Array;
lilv_array[32][2] = new Array;
lilv_array[32][1][5] = 0.0802;//商贷 1～5年 8.02%
lilv_array[32][1][10] = 0.0822;//商贷 5-30年 8.22%
lilv_array[32][2][5] = 0.0405;//公积金 1～5年 4.05%
lilv_array[32][2][10] = 0.0459;//公积金 5-30年 4.59%

//2008年	10月30日新利率下限
lilv_array[33] = new Array;
lilv_array[33][1] = new Array;
lilv_array[33][2] = new Array;
lilv_array[33][1][5] = 0.0491;//商贷 1～5年 4.91%
lilv_array[33][1][10] = 0.0504;//商贷 5-30年 5.04%
lilv_array[33][2][5] = 0.0405;//公积金 1～5年 4.05%
lilv_array[33][2][10] = 0.0459;//公积金 5-30年 4.59%

//2008年	10月30日新利率上限
lilv_array[34] = new Array;
lilv_array[34][1] = new Array;
lilv_array[34][2] = new Array;
lilv_array[34][1][5] = 0.0772;//商贷 1～5年 7.72%
lilv_array[34][1][10] = 0.0792;//商贷 5-30年 7.92%
lilv_array[34][2][5] = 0.0405;//公积金 1～5年 4.05%
lilv_array[34][2][10] = 0.0459;//公积金 5-30年 4.59%

//2008年	10月30日新利率基准
lilv_array[35] = new Array;
lilv_array[35][1] = new Array;
lilv_array[35][2] = new Array;
lilv_array[35][1][5] = 0.0702;//商贷 1～5年 7.02%
lilv_array[35][1][10] = 0.0720;//商贷 5-30年 7.20%
lilv_array[35][2][5] = 0.0405;//公积金 1～5年 4.05%
lilv_array[35][2][10] = 0.0459;//公积金 5-30年 4.59%

//2008年	10月30日新利率(第二套房)
lilv_array[36] = new Array;
lilv_array[36][1] = new Array;
lilv_array[36][2] = new Array;
lilv_array[36][1][5] = 0.0772;//商贷 1～5年 7.72%
lilv_array[36][1][10] = 0.0792;//商贷 5-30年 7.92%
lilv_array[36][2][5] = 0.0405;//公积金 1～5年 4.05%
lilv_array[36][2][10] = 0.0459;//公积金 5-30年 4.59%

//2008年	11月27日新利率下限
lilv_array[37] = new Array;
lilv_array[37][1] = new Array;
lilv_array[37][2] = new Array;
lilv_array[37][1][5] = 0.0416;//商贷 1～5年 4.16%
lilv_array[37][1][10] = 0.0428;//商贷 5-30年 4.28%
lilv_array[37][2][5] = 0.0351;//公积金 1～5年 3.51%
lilv_array[37][2][10] = 0.0405;//公积金 5-30年 4.05%

//2008年	11月27日新利率上限
lilv_array[38] = new Array;
lilv_array[38][1] = new Array;
lilv_array[38][2] = new Array;
lilv_array[38][1][5] = 0.0653;//商贷 1～5年 6.53%
lilv_array[38][1][10] = 0.0673;//商贷 5-30年 6.73%
lilv_array[38][2][5] = 0.0351;//公积金 1～5年 3.51%
lilv_array[38][2][10] = 0.0405;//公积金 5-30年 4.05%

//2008年	11月27日新利率基准
lilv_array[39] = new Array;
lilv_array[39][1] = new Array;
lilv_array[39][2] = new Array;
lilv_array[39][1][5] = 0.0594;//商贷 1～5年 5.94%
lilv_array[39][1][10] = 0.0612;//商贷 5-30年 6.12%
lilv_array[39][2][5] = 0.0351;//公积金 1～5年 3.51%
lilv_array[39][2][10] = 0.0405;//公积金 5-30年 4.05%

//2008年	11月27日新利率(第二套房)
lilv_array[40] = new Array;
lilv_array[40][1] = new Array;
lilv_array[40][2] = new Array;
lilv_array[40][1][5] = 0.0653;//商贷 1～5年 6.53%
lilv_array[40][1][10] = 0.0673;//商贷 5-30年 6.73%
lilv_array[40][2][5] = 0.0351;//公积金 1～5年 3.51%
lilv_array[40][2][10] = 0.0405;//公积金 5-30年 4.05%

//2008年	12月23日新利率下限(7折)
lilv_array[41] = new Array;
lilv_array[41][1] = new Array;
lilv_array[41][2] = new Array;
lilv_array[41][1][5] = 0.0403;//商贷 1～5年 4.03%
lilv_array[41][1][10] = 0.0416;//商贷 5-30年 4.16%
lilv_array[41][2][5] = 0.0333;//公积金 1～5年 3.33%
lilv_array[41][2][10] = 0.0387;//公积金 5-30年 3.87%

//2008年	12月23日新利率下限(85折)
lilv_array[42] = new Array;
lilv_array[42][1] = new Array;
lilv_array[42][2] = new Array;
lilv_array[42][1][5] = 0.0490;//商贷 1～5年 4.90%
lilv_array[42][1][10] = 0.0505;//商贷 5-30年 5.05%
lilv_array[42][2][5] = 0.0333;//公积金 1～5年 3.33%
lilv_array[42][2][10] = 0.0387;//公积金 5-30年 3.87%

//2008年	12月23日新利率上限(1.1倍)
lilv_array[43] = new Array;
lilv_array[43][1] = new Array;
lilv_array[43][2] = new Array;
lilv_array[43][1][5] = 0.0634;//商贷 1～5年 6.34%
lilv_array[43][1][10] = 0.0653;//商贷 5-30年 6.53%
lilv_array[43][2][5] = 0.0333;//公积金 1～5年 3.33%
lilv_array[43][2][10] = 0.0387;//公积金 5-30年 3.87%

//2008年	12月23日新利率基准
lilv_array[44] = new Array;
lilv_array[44][1] = new Array;
lilv_array[44][2] = new Array;
lilv_array[44][1][5] = 0.0576;//商贷 1～5年 5.76%
lilv_array[44][1][10] = 0.0594;//商贷 5-30年 5.94%
lilv_array[44][2][5] = 0.0333;//公积金 1～5年 3.33%
lilv_array[44][2][10] = 0.0387;//公积金 5-30年 3.87%

//2008年	12月23日新利率(第二套房)(1.1倍)
lilv_array[45] = new Array;
lilv_array[45][1] = new Array;
lilv_array[45][2] = new Array;
lilv_array[45][1][5] = 0.0634;//商贷 1～5年 6.34%
lilv_array[45][1][10] = 0.0653;//商贷 5-30年 6.53%
lilv_array[45][2][5] = 0.0333;//公积金 1～5年 3.33%
lilv_array[45][2][10] = 0.0387;//公积金 5-30年 3.87%

//2010年	10月20日利率下限(7折)
lilv_array[46]=new Array;
lilv_array[46][1]=new Array;
lilv_array[46][2]=new Array;
lilv_array[46][1][5]=0.0596*0.7;//商贷 1～5年 5.76%    
lilv_array[46][1][10]=0.0614*0.7;//商贷 5-30年 5.94%   
lilv_array[46][2][5]=0.0350;//公积金 1～5年 3.50%  
lilv_array[46][2][10]=0.0405;//公积金 5-30年 4.05% 

//2010年	10月20日利率下限(85折)
lilv_array[47]=new Array;
lilv_array[47][1]=new Array;
lilv_array[47][2]=new Array;
lilv_array[47][1][5]=0.0596*0.85;//商贷 1～5年 5.76%    
lilv_array[47][1][10]=0.0614*0.85;//商贷 5-30年 5.94%   
lilv_array[47][2][5]=0.0350;//公积金 1～5年 3.50%  
lilv_array[47][2][10]=0.0405;//公积金 5-30年 4.05% 

//2010年	10月20日基准利率
lilv_array[48]=new Array;
lilv_array[48][1]=new Array;
lilv_array[48][2]=new Array;
lilv_array[48][1][5]=0.0596;//商贷 1～5年 5.76%
lilv_array[48][1][10]=0.0614;//商贷 5-30年 5.94%
lilv_array[48][2][5]=0.0350;//公积金 1～5年 3.50%  
lilv_array[48][2][10]=0.0405;//公积金 5-30年 4.05% 

//2010年	10月20日利率上限(1.1倍)
lilv_array[49]=new Array;
lilv_array[49][1]=new Array;
lilv_array[49][2]=new Array;
lilv_array[49][1][5]=0.0596*1.1;//商贷 1～5年 5.76%    
lilv_array[49][1][10]=0.0614*1.1;//商贷 5-30年 5.94%   
lilv_array[49][2][5]=0.0350;//公积金 1～5年 3.50%  
lilv_array[49][2][10]=0.0405;//公积金 5-30年 4.05% 


//10年12月26日基准利率
lilv_array[9] = new Array;
lilv_array[9][1] = new Array;
lilv_array[9][2] = new Array;
lilv_array[9][1][5] = 0.0622;//商贷 1～5年 6.22%
lilv_array[9][1][10] = 0.0640;//商贷 5-30年 6.4%
lilv_array[9][2][5] = 0.0375;//公积金 1～5年 3.75%
lilv_array[9][2][10] = 0.0430;//公积金 5-30年 4.3%
//10年12月26日利率下限(7折)
lilv_array[10] = new Array;
lilv_array[10][1] = new Array;
lilv_array[10][2] = new Array;
lilv_array[10][1][5] = 0.04354;//商贷 1～5年 4.354%
lilv_array[10][1][10] = 0.0448;//商贷 5-30年 4.48%
lilv_array[10][2][5] = 0.0375;//公积金 1～5年 3.75%
lilv_array[10][2][10] = 0.0430;//公积金 5-30年 4.3%
//10年12月26日利率上限(1.1倍)
lilv_array[11] = new Array;
lilv_array[11][1] = new Array;
lilv_array[11][2] = new Array;
lilv_array[11][1][5] = 0.06842;//商贷 1～5年 6.842%
lilv_array[11][1][10] = 0.0704;//商贷 5-30年 7.04%
lilv_array[11][2][5] = 0.0375;//公积金 1～5年 3.75%
lilv_array[11][2][10] = 0.0430;//公积金 5-30年 4.3%
//11年2月9日基准利率
lilv_array[12] = new Array;
lilv_array[12][1] = new Array;
lilv_array[12][2] = new Array;
lilv_array[12][1][5] = 0.0645;//商贷 1～5年 6.45%
lilv_array[12][1][10] = 0.0660;//商贷 5-30年 6.6%
lilv_array[12][2][5] = 0.0400;//公积金 1～5年 4%
lilv_array[12][2][10] = 0.0450;//公积金 5-30年 4.5%
//11年2月9日利率下限(7折)
lilv_array[13] = new Array;
lilv_array[13][1] = new Array;
lilv_array[13][2] = new Array;
lilv_array[13][1][5] = 0.04515;//商贷 1～5年 4.515%
lilv_array[13][1][10] = 0.04620;//商贷 5-30年 4.62%
lilv_array[13][2][5] = 0.0400;//公积金 1～5年 4%
lilv_array[13][2][10] = 0.0450;//公积金 5-30年 4.5%
//11年2月9日利率下限(85折)
lilv_array[14] = new Array;
lilv_array[14][1] = new Array;
lilv_array[14][2] = new Array;
lilv_array[14][1][5] = 0.054825;//商贷 1～5年 5.4825%
lilv_array[14][1][10] = 0.0561;//商贷 5-30年 5.61%
lilv_array[14][2][5] = 0.0400;//公积金 1～5年 4%
lilv_array[14][2][10] = 0.0450;//公积金 5-30年 4.5%
//11年2月9日利率上限(1.1倍)
lilv_array[15] = new Array;
lilv_array[15][1] = new Array;
lilv_array[15][2] = new Array;
lilv_array[15][1][5] = 0.07095;//商贷 1～5年 7.095%
lilv_array[15][1][10] = 0.0726;//商贷 5-30年 7.26%
lilv_array[15][2][5] = 0.0400;//公积金 1～5年 4%
lilv_array[15][2][10] = 0.0450;//公积金 5-30年 4.5%
//11年4月5日基准利率
lilv_array[16] = new Array;
lilv_array[16][1] = new Array;
lilv_array[16][2] = new Array;
lilv_array[16][1][5] = 0.0665;//商贷 1～5年 6.65%
lilv_array[16][1][10] = 0.0680;//商贷 5-30年 6.8%
lilv_array[16][2][5] = 0.0420;//公积金 1～5年 4.2%
lilv_array[16][2][10] = 0.0470;//公积金 5-30年 4.7%
//11年4月5日利率下限（7折）
lilv_array[17] = new Array;
lilv_array[17][1] = new Array;
lilv_array[17][2] = new Array;
lilv_array[17][1][5] = 0.04655;//商贷 1～5年 4.655%
lilv_array[17][1][10] = 0.0476;//商贷 5-30年 4.76%
lilv_array[17][2][5] = 0.0420;//公积金 1～5年 4.2%
lilv_array[17][2][10] = 0.0470;//公积金 5-30年 4.7%
//11年4月5日利率下限（85折）
lilv_array[18] = new Array;
lilv_array[18][1] = new Array;
lilv_array[18][2] = new Array;
lilv_array[18][1][5] = 0.056525;//商贷 1～5年 5.6525%
lilv_array[18][1][10] = 0.0578;//商贷 5-30年 5.78%
lilv_array[18][2][5] = 0.0420;//公积金 1～5年 4.2%
lilv_array[18][2][10] = 0.0470;//公积金 5-30年 4.7%
//11年4月5日利率上限（1.1倍）
lilv_array[19] = new Array;
lilv_array[19][1] = new Array;
lilv_array[19][2] = new Array;
lilv_array[19][1][5] = 0.07315;//商贷 1～5年 7.315%
lilv_array[19][1][10] = 0.0748;//商贷 5-30年 7.48%
lilv_array[19][2][5] = 0.0420;//公积金 1～5年 4.2%
lilv_array[19][2][10] = 0.0470;//公积金 5-30年 4.7%
//11年7月6日基准利率
lilv_array[20] = new Array;
lilv_array[20][1] = new Array;
lilv_array[20][2] = new Array;
lilv_array[20][1][5] = 0.0690;//商贷 1～5年 6.9%
lilv_array[20][1][10] = 0.0705;//商贷 5-30年 7.05%
lilv_array[20][2][5] = 0.0445;//公积金 1～5年 4.45%
lilv_array[20][2][10] = 0.0490;//公积金 5-30年 4.9%
//11年7月6日利率下限（7折）
lilv_array[21] = new Array;
lilv_array[21][1] = new Array;
lilv_array[21][2] = new Array;
lilv_array[21][1][5] = 0.0483;//商贷 1～5年 4.83%
lilv_array[21][1][10] = 0.04935;//商贷 5-30年 4.935%
lilv_array[21][2][5] = 0.0445;//公积金 1～5年 4.45%
lilv_array[21][2][10] = 0.0490;//公积金 5-30年 4.9%
//11年7月6日利率下限（85折）
lilv_array[22] = new Array;
lilv_array[22][1] = new Array;
lilv_array[22][2] = new Array;
lilv_array[22][1][5] = 0.05865;//商贷 1～5年 5.865%
lilv_array[22][1][10] = 0.059925;//商贷 5-30年 5.9925%
lilv_array[22][2][5] = 0.0445;//公积金 1～5年 4.45%
lilv_array[22][2][10] = 0.0490;//公积金 5-30年 4.9%
//11年7月6日利率上限（1.1倍）
lilv_array[23] = new Array;
lilv_array[23][1] = new Array;
lilv_array[23][2] = new Array;
lilv_array[23][1][5] = 0.0759;//商贷 1～5年 7.59%
lilv_array[23][1][10] = 0.07755;//商贷 5-30年 7.755%
lilv_array[23][2][5] = 0.0445;//公积金 1～5年 4.45%
lilv_array[23][2][10] = 0.0490;//公积金 5-30年 4.9%


//12年6月8日基准利率
lilv_array[24] = new Array;
lilv_array[24][1] = new Array;
lilv_array[24][2] = new Array;
lilv_array[24][1][5] = 0.0665;//商贷 1～5年 6.65%
lilv_array[24][1][10] = 0.0680;//商贷 5-30年 6.8%
lilv_array[24][2][5] = 0.0420;//公积金 1～5年 4.2%
lilv_array[24][2][10] = 0.0470;//公积金 5-30年 4.7%
//12年6月8日利率下限（7折）
lilv_array[25] = new Array;
lilv_array[25][1] = new Array;
lilv_array[25][2] = new Array;
lilv_array[25][1][5] = 0.04655;//商贷 1～5年 4.655%
lilv_array[25][1][10] = 0.0476;//商贷 5-30年 4.76%
lilv_array[25][2][5] = 0.0420;//公积金 1～5年 4.2%
lilv_array[25][2][10] = 0.0470;//公积金 5-30年 4.7%
//12年6月8日利率下限（85折）
lilv_array[26] = new Array;
lilv_array[26][1] = new Array;
lilv_array[26][2] = new Array;
lilv_array[26][1][5] = 0.056525;//商贷 1～5年 5.6525%
lilv_array[26][1][10] = 0.0578;//商贷 5-30年 5.78%
lilv_array[26][2][5] = 0.0420;//公积金 1～5年 4.2%
lilv_array[26][2][10] = 0.0470;//公积金 5-30年 4.7%
//12年6月8日利率上限（1.1倍）
lilv_array[27] = new Array;
lilv_array[27][1] = new Array;
lilv_array[27][2] = new Array;
lilv_array[27][1][5] = 0.07315;//商贷 1～5年 7.315%
lilv_array[27][1][10] = 0.0748;//商贷 5-30年 7.48%
lilv_array[27][2][5] = 0.0420;//公积金 1～5年 4.2%
lilv_array[27][2][10] = 0.0470;//公积金 5-30年 4.7%
//12年7月6日基准利率
lilv_array[28] = new Array;
lilv_array[28][1] = new Array;
lilv_array[28][2] = new Array;
lilv_array[28][1][5] = 0.0640;//商贷 1～5年 6.4%
lilv_array[28][1][10] = 0.0655;//商贷 5-30年 6.55%
lilv_array[28][2][5] = 0.0400;//公积金 1～5年 4%
lilv_array[28][2][10] = 0.0450;//公积金 5-30年 4.5%
//12年7月6日利率下限（7折）
lilv_array[29] = new Array;
lilv_array[29][1] = new Array;
lilv_array[29][2] = new Array;
lilv_array[29][1][5] = 0.0448;//商贷 1～5年 4.48%
lilv_array[29][1][10] = 0.04585;//商贷 5-30年 4.585%
lilv_array[29][2][5] = 0.0400;//公积金 1～5年 4%
lilv_array[29][2][10] = 0.0450;//公积金 5-30年 4.5%
//12年7月6日利率下限（85折）
lilv_array[30] = new Array;
lilv_array[30][1] = new Array;
lilv_array[30][2] = new Array;
lilv_array[30][1][5] = 0.0544;//商贷 1～5年 5.44%
lilv_array[30][1][10] = 0.055675;//商贷 5-30年 5.5675%
lilv_array[30][2][5] = 0.0400;//公积金 1～5年 4%
lilv_array[30][2][10] = 0.0450;//公积金 5-30年 4.5%
//12年7月6日利率上限（1.1倍）
lilv_array[31] = new Array;
lilv_array[31][1] = new Array;
lilv_array[31][2] = new Array;
lilv_array[31][1][5] = 0.0704;//商贷 1～5年 7.04%
lilv_array[31][1][10] = 0.07205;//商贷 5-30年 7.205%
lilv_array[31][2][5] = 0.0400;//公积金 1～5年 4%
lilv_array[31][2][10] = 0.0450;//公积金 5-30年 4.5%

function myround(v, e){
    var t = 1;
    e = Math.round(e);
    for(; e > 0; t *= 10, e--);
    for(; e < 0; t /= 10, e++);
    return Math.round(v * t) / t;
}

function ShowLilv(month, lt){
	var year = (month > 5) ? 10 : 5;
	_$("sdlv").innerHTML = myround(lilv_array[lt][1][year] * 100, 2) + "%";
	_$("gjlv").innerHTML = myround(lilv_array[lt][2][year] * 100, 2);
}
function exc_zuhe(fmobj, v){
	if (fmobj.name == "calc1"){
	    if (v == 3) {
	        document.all.calc1_zuhe.style.display = 'block';
	        document.all.calc1_js_div0.style.display = 'none';
	        document.all.calc1_js_div1.style.display = 'none';
	        document.all.calc1_js_div2.style.display = 'none';
	        document.all.calc1_js_div4.style.display = 'none';
		}else{
			document.all.calc1_zuhe.style.display = 'none';
			document.all.calc1_js_div0.style.display = 'block';
			document.all.calc1_js_div1.style.display = 'block';
			document.all.calc1_js_div2.style.display = 'block';
			document.all.calc1_js_div4.style.display = 'block';
		}
	}else{
		if(v == 3){
			document.all.calc2_zuhe.style.display = 'block';
			fmobj.jisuan_radio[1].checked = true;
			exc_js(fmobj, 2);
		}else{
			document.all.calc2_zuhe.style.display = 'none';
		}
	}
}

function exc_js(fmobj, v){
	//var fmobj=document.calc1;
	if (fmobj.name == "calc1"){
		if (v == 1){
			document.all.calc1_js_div1.style.display = 'block';
			document.all.calc1_js_div2.style.display = 'none';
//			document.all.calc1_zuhe.style.display = 'none';
			fmobj.type.value = 1;
		}else{
			document.all.calc1_js_div1.style.display = 'none';
			document.all.calc1_js_div2.style.display = 'block';
		}
	}else{
		if (v == 1){
			document.all.calc2_js_div1.style.display = 'block';
			document.all.calc2_js_div2.style.display = 'none';
			document.all.calc2_zuhe.style.display = 'none';
			fmobj.type.value = 1;
		}else{
			document.all.calc2_js_div1.style.display = 'none';
			document.all.calc2_js_div2.style.display = 'block';
		}
	}
}

function formReset(fmobj){
	fmobj.reset();
	if (fmobj.name == "calc1"){
		document.all.calc1_js_div1.style.display = 'block';
		document.all.calc1_js_div2.style.display = 'none';
//		document.all.calc1_zuhe.style.display = 'none';
		document.all.calc1_benjin.style.display = 'none';
	}else{
		document.all.calc2_js_div1.style.display = 'block';
		document.all.calc2_js_div2.style.display = 'none';
		document.all.calc2_zuhe.style.display = 'none';
		document.all.calc2_benxi.style.display = 'none';
	}
}

//显示右边的比较div
function showRightDiv(fmobj){
	if (ext_total(fmobj) == false){return;}
	//alert(document.calc1.month_money2.value);
	var a = window.open('', 'calc_win', 'status=yes,scrollbars=yes,resizable=yes,width=550,height=500,left=0,top=0')//790*520
	if (fmobj.name == "calc1"){
		document.calc1.target = "calc_win";
		document.calc1.submit();
	}else{
		document.calc2.target = "calc_win";
		document.calc2.submit();
	}
}

//验证是否为数字
function reg_Num(str){
	if (str.length == 0){return false;}
	var Letters = "1234567890.";

	for(i = 0; i < str.length; i++){
		var CheckChar = str.charAt(i);
		if (Letters.indexOf(CheckChar) == -1){return false;}
	}
	return true;
}

//得到利率
function getlilv(lilv_class, type, years){
	var lilv_class = parseInt(lilv_class);
    if (years <= 5){
		 return lilv_array[lilv_class][type][5];
	}else{
		return lilv_array[lilv_class][type][10];
	}
}

//本金还款的月还款额(参数: 年利率 / 贷款总额 / 贷款总月份 / 贷款当前月0～length-1)
function getMonthMoney2(lilv, total, month, cur_month){
	var lilv_month = lilv / 12;//月利率
	//return total * lilv_month * Math.pow(1 + lilv_month, month) / ( Math.pow(1 + lilv_month, month) -1 );
	var benjin_money = total / month;
	return (total - benjin_money * cur_month) * lilv_month + benjin_money;
}

//本息还款的月还款额(参数: 年利率/贷款总额/贷款总月份)
function getMonthMoney1(lilv, total, month){
	var lilv_month = lilv / 12;//月利率
	return total * lilv_month * Math.pow(1 + lilv_month, month) / ( Math.pow(1 + lilv_month, month) -1 );
}

function ext_total(fmobj){
//	 alert('贷款计算器');
	//var fmobj = document.calc1;
	//先清空月还款数下拉框
//	while ((k = fmobj.month_money2.length - 1) >= 0){
//		fmobj.month_money2.options.remove(k);
//	}
	var years = fmobj.years.value;
	var month = fmobj.years.value * 12;

//	fmobj.month1.value = month + "(月)";
	document.getElementById("month1").innerHTML = month + "(月)";

		fmobj.month2.value = month + "(月)";
		if (document.calc1.type[2].checked) {
		//--  组合型贷款(组合型贷款的计算，只和商业贷款额、和公积金贷款额有关，和按贷款总额计算无关)
		if(!reg_Num(fmobj.total_sy.value)){alert("混合型贷款请填写商贷比例");fmobj.total_sy.focus();return false;}
		if(!reg_Num(fmobj.total_gjj.value)){alert("混合型贷款请填写公积金比例");fmobj.total_gjj.focus();return false;}
		if(fmobj.total_sy.value == null){fmobj.total_sy.value = 0;}
		if(fmobj.total_gjj.value == null){fmobj.total_gjj.value = 0;}
		var total_sy = fmobj.total_sy.value;
		var total_gjj = fmobj.total_gjj.value;
//		fmobj.fangkuan_total1.value = "略"; //房款总额
		document.getElementById("fangkuan_total1").innerHTML = "略"; //房款总额
		fmobj.fangkuan_total2.value = "略";//房款总额
		//		fmobj.money_first1.value = 0;//首期付款
		document.getElementById("money_first1").innerHTML = 0; //首期付款
		fmobj.money_first2.value = 0;//首期付款

		//贷款总额
		var total_sy = parseInt(fmobj.total_sy.value);
		var total_gjj = parseInt(fmobj.total_gjj.value);
		var daikuan_total = total_sy + total_gjj;
		//		fmobj.daikuan_total1.value = daikuan_total;
		document.getElementById("daikuan_total1").innerHTML = daikuan_total;
		fmobj.daikuan_total2.value = daikuan_total;

		//月还款
		var lilv_sd = getlilv(fmobj.lilv.value,1, years);//得到商贷利率
		var lilv_gjj = getlilv(fmobj.lilv.value,2, years);//得到公积金利率

		//1.本金还款
			//月还款
			var all_total2 = 0;
			var month_money2 = "";
			for(j = 0; j < month; j++){
				//调用函数计算: 本金月还款额
				huankuan = getMonthMoney2(lilv_sd, total_sy, month, j) + getMonthMoney2(lilv_gjj, total_gjj, month, j);
				all_total2 += huankuan;
				huankuan = Math.round(huankuan * 100) / 100;
				//fmobj.month_money2.options[j] = new Option( (j + 1) +"月," + huankuan + "(元)", huankuan);
				month_money2 += (j + 1) + "月," + huankuan + "(元)\n";
			}
			fmobj.month_money2.value = month_money2;
			//还款总额
			fmobj.all_total2.value = Math.round(all_total2 * 100) / 100;
			//支付利息款
			fmobj.accrual2.value = Math.round((all_total2 - daikuan_total) * 100) / 100;

		//2.本息还款
			//月均还款
			var month_money1 = getMonthMoney1(lilv_sd, total_sy, month) + getMonthMoney1(lilv_gjj, total_gjj, month);//调用函数计算
			//			fmobj.month_money1.value = Math.round(month_money1 * 100) / 100 + "(元)";
			if ($("#dengeben2").attr("checked")) {
			    document.getElementById("month_money11").value = Math.round(month_money1 * 100) / 100 + "(元)";
			}
			else {
			    document.getElementById("month_money1").innerHTML = Math.round(month_money1 * 100) / 100 + "(元)";
			}
		
			//还款总额
			var all_total1 = month_money1 * month;
//			fmobj.all_total1.value = Math.round(all_total1 * 100) / 100;
			document.getElementById("all_total1").innerHTML = Math.round(all_total1 * 100) / 100;
			//支付利息款
//			fmobj.accrual1.value = Math.round((all_total1 - daikuan_total) * 100) / 100;
			document.getElementById("accrual1").innerHTML = Math.round((all_total1 - daikuan_total) * 100) / 100;
} else {
    if (document.calc1.type[0].checked) {
//            alert(1);
            var typetempcalc1 = 1;
        }
        if (document.calc1.type[1].checked) {
//            alert(2);
            var typetempcalc1 = 2;
        }
        if (document.calc1.type[2].checked) {
//            alert(3);
            var typetempcalc1 = 3;
        }
		//--  商业贷款、公积金贷款
        var lilv = getlilv(fmobj.lilv.value, typetempcalc1, fmobj.years.value); //得到利率
//        alert(lilv);
		if(fmobj.jisuan_radio[0].checked == true){
			//------------ 根据单价面积计算
			if(!reg_Num(fmobj.price.value)){alert("请填写单价");fmobj.price.focus();return;}
			if(!reg_Num(fmobj.sqm.value)){alert("请填写面积");fmobj.sqm.focus();return;}

			//房款总额
			var fangkuan_total = fmobj.price.value * fmobj.sqm.value;
//			fmobj.fangkuan_total1.value = fangkuan_total;
			//			fmobj.fangkuan_total2.value = fangkuan_total;
			fmobj.fangkuan_total2.value = fangkuan_total;
			document.getElementById("fangkuan_total1").innerHTML = fangkuan_total;
      
			//贷款总额
			var daikuan_total = (fmobj.price.value * fmobj.sqm.value) * (fmobj.anjie.value / 10);
//			fmobj.daikuan_total1.value = daikuan_total;
			fmobj.daikuan_total2.value = daikuan_total;
			document.getElementById("daikuan_total1").innerHTML = daikuan_total;
      
			//首期付款
			var money_first = fangkuan_total - daikuan_total;
//			fmobj.money_first1.value = money_first
			fmobj.money_first2.value = money_first;
			document.getElementById("money_first1").innerHTML = money_first;
		}else{
			//------------ 根据贷款总额计算
			if (fmobj.daikuan_total000.value.length == 0){alert("请填写贷款总额");fmobj.daikuan_total000.focus();return false;}

			//房款总额
//			fmobj.fangkuan_total1.value = "略";
			fmobj.fangkuan_total2.value = "略";

			document.getElementById("fangkuan_total1").innerHTML = "略";
//			document.getElementById("fangkuan_total2").innerHTML = "略";
      
			//贷款总额
			var daikuan_total = fmobj.daikuan_total000.value * 10000;
//			fmobj.daikuan_total1.value = daikuan_total;
			fmobj.daikuan_total2.value = daikuan_total;
			document.getElementById("daikuan_total1").innerHTML = daikuan_total;
//			document.getElementById("daikuan_total2").innerHTML = daikuan_total;
      
			//首期付款
//			fmobj.money_first1.value = 0;
			//			fmobj.money_first2.value = 0;
			document.getElementById("money_first1").innerHTML = 0;
//			document.getElementById("money_first2").innerHTML = 0;
		}
		//1.本金还款
			//月还款
			var all_total2 = 0;
			var month_money2 = "";
			for(j = 0; j < month; j++){
				//调用函数计算: 本金月还款额
				huankuan = getMonthMoney2(lilv, daikuan_total, month, j);
				all_total2 += huankuan;
				huankuan = Math.round(huankuan * 100) / 100;
				//fmobj.month_money2.options[j] = new Option( (j + 1) +"月," + huankuan + "(元)", huankuan);
				month_money2 += (j + 1) + "月," + huankuan + "(元)\n";
}

            			fmobj.month_money2.value = month_money2;
//document.getElementById("month_money1").innerHTML = month_money2;

			//还款总额
			fmobj.all_total2.value = Math.round(all_total2 * 100) / 100;
			//支付利息款
			fmobj.accrual2.value = Math.round( (all_total2 - daikuan_total) * 100) / 100;
//document.getElementById("all_total1").innerHTML = Math.round(all_total2 * 100) / 100;
//document.getElementById("accrual1").innerHTML = Math.round((all_total2 - daikuan_total) * 100) / 100;
		//2.本息还款
			//月均还款
			var month_money1 = getMonthMoney1(lilv, daikuan_total, month);//调用函数计算
			//			fmobj.month_money1.value = Math.round(month_money1 * 100) / 100 + "(元)";
			//===================================
			if ($("#dengeben2").attr("checked")) {
			    document.getElementById("month_money11").value = Math.round(month_money1 * 100) / 100 + "(元)";
			}
			else {
			    document.getElementById("month_money1").innerHTML = Math.round(month_money1 * 100) / 100 + "(元)";
			 	}
            //====================================
//			document.getElementById("month_money1").innerHTML = Math.round(month_money1 * 100) / 100 + "(元)";
//			document.getElementById("month_money1").value = Math.round(month_money1 * 100) / 100 + "(元)";
			//还款总额
			var all_total1 = month_money1 * month;
//			fmobj.all_total1.value = Math.round(all_total1 * 100) / 100;
//			//支付利息款
			//			fmobj.accrual1.value = Math.round( (all_total1 - daikuan_total) * 100) / 100;
			document.getElementById("all_total1").innerHTML = Math.round(all_total1 * 100) / 100;
			document.getElementById("accrual1").innerHTML = Math.round((all_total1 - daikuan_total) * 100) / 100;
		
	}
if (_$("dengeben2").checked) {
//    alert(fmobj.fangkuan_total2.value);
    document.getElementById("fangkuan_total1").innerHTML = fmobj.fangkuan_total2.value;
    document.getElementById("daikuan_total1").innerHTML = fmobj.daikuan_total2.value;
    document.getElementById("all_total1").innerHTML = fmobj.all_total2.value;
    document.getElementById("accrual1").innerHTML = fmobj.accrual2.value;
    document.getElementById("money_first1").innerHTML = fmobj.money_first2.value;
    document.getElementById("month1").innerHTML = fmobj.month2.value;
    if ($("#dengeben2").attr("checked")) {
        document.getElementById("month_money11").value = fmobj.month_money2.value;
    }
    else {
        document.getElementById("month_money1").innerHTML = fmobj.month_money2.value;
    }
   
  
//		fmobj.fangkuan_total1.value = fmobj.fangkuan_total2.value;
//		fmobj.daikuan_total1.value = fmobj.daikuan_total2.value;
//		fmobj.all_total1.value = fmobj.all_total2.value;
//		fmobj.accrual1.value = fmobj.accrual2.value;
//		fmobj.money_first1.value = fmobj.money_first2.value;
//		fmobj.month1.value = fmobj.month2.value;

//		fmobj.month_money1.value = fmobj.month_money2.value;
	}
}

function chanage_type_mm() {
    if ($("#dengeben2").attr("checked")) {
        $("#type1_mm1").hide();
        $("#type1_mm2").show();
//        document.all.type1_mm2.style.display = 'block';
//        document.all.type1_mm1.style.display = 'none';
//        document.getElementById("type1_mm1").appendChild = '';
    }
    else {
        $("#type1_mm2").hide();
        $("#type1_mm1").show();
//        document.all.type1_mm2.style.display = 'none';
//        document.all.type1_mm1.style.display = 'block';
//        document.getElementById("type1_mm1").appendChild = ' <th>每月还款：</th> <td><div  name="month_money1" id="month_money1"  class="jisuan_result fl"></div><em class="ml5 fl">元</em></td>';
    }
}

//提前还歀计算
function play() {
	if (document.tqhdjsq.dkzws.value == ''){
		alert('请填入贷款总额');
		return;
	}else dkzys = parseFloat(document.tqhdjsq.dkzws.value) * 10000;
	if(document.tqhdjsq.tqhkfs[1].checked && document.tqhdjsq.tqhkws.value == ''){
		alert('请填入部分提前还款额度');
		return;
	}
	s_yhkqs=parseInt(document.tqhdjsq.yhkqs.value);

	if (document.tqhdjsq.tqhklx[0].checked) {
	    if (s_yhkqs > 60) {
	        dklv = getlilv(document.tqhdjsq.dklv_class.value, 2, 10) / 12; //公积金贷款利率5年以上4.23%
	    } else {
	        dklv = getlilv(document.tqhdjsq.dklv_class.value, 2, 3) / 12;  //公积金贷款利率5年(含)以下3.78%
	    }
	}
	if (document.tqhdjsq.tqhklx[1].checked) {
	    if (s_yhkqs > 60) {
	        dklv = getlilv(document.tqhdjsq.dklv_class.value, 1, 10) / 12; //商业性贷款利率5年以上5.31%
	    } else {
	        dklv = getlilv(document.tqhdjsq.dklv_class.value, 1, 3) / 12; //商业性贷款利率5年(含)以下4.95%
	    }
	}

	//月利率
//	if(_$("tqhklx").value == 1){
//		if (s_yhkqs > 60){
//			dklv = getlilv(document.tqhdjsq.dklv_class.value, 2, 10)/12; //公积金贷款利率5年以上4.23%
//		}else{
//			dklv = getlilv(document.tqhdjsq.dklv_class.value, 2, 3)/12;  //公积金贷款利率5年(含)以下3.78%
//		}
//	}
//	if(_$("tqhklx").value == 0){
//		if(s_yhkqs > 60){
//			dklv = getlilv(document.tqhdjsq.dklv_class.value, 1, 10)/12; //商业性贷款利率5年以上5.31%
//		}else{
//			dklv = getlilv(document.tqhdjsq.dklv_class.value, 1, 3)/12; //商业性贷款利率5年(含)以下4.95%
//		}
//	}

	//已还贷款期数
	yhdkqs = (parseInt(document.tqhdjsq.tqhksjn.value) * 12 + parseInt(document.tqhdjsq.tqhksjy.value)) - (parseInt(document.tqhdjsq.yhksjn.value) * 12 + parseInt(document.tqhdjsq.yhksjy.value));

	if(yhdkqs < 0 || yhdkqs > s_yhkqs){
		alert('预计提前还款时间与第一次还款时间有矛盾，请查实');
		return false;
	}

	yhk = dkzys * (dklv * Math.pow((1 + dklv), s_yhkqs)) / (Math.pow((1 + dklv), s_yhkqs) - 1);
	yhkjssj = Math.floor((parseInt(document.tqhdjsq.yhksjn.value) * 12 + parseInt(document.tqhdjsq.yhksjy.value) + s_yhkqs - 2) / 12) + '年' + ((parseInt(document.tqhdjsq.yhksjn.value) * 12 + parseInt(document.tqhdjsq.yhksjy.value) + s_yhkqs - 2) % 12 + 1) + '月';
	yhdkys = yhk * yhdkqs;

	yhlxs = 0;
	yhbjs = 0;
	for(i = 1; i <= yhdkqs; i++){
		yhlxs = yhlxs + (dkzys - yhbjs) * dklv;
		yhbjs = yhbjs + yhk - (dkzys - yhbjs) * dklv;
	}

	remark = '';
	if(document.tqhdjsq.tqhkfs[1].checked){
		tqhkys = parseInt(document.tqhdjsq.tqhkws.value) * 10000;
		if(tqhkys + yhk >= (dkzys - yhbjs) * (1 + dklv)){
			remark = '您的提前还款额已足够还清所欠贷款！';
		}else{
			yhbjs = yhbjs + yhk;
            byhk = yhk + tqhkys;
			if(document.tqhdjsq.clfs[0].checked){
				yhbjs_temp = yhbjs + tqhkys;
				for(xdkqs = 0; yhbjs_temp <= dkzys; xdkqs++) yhbjs_temp = yhbjs_temp + yhk - (dkzys - yhbjs_temp) * dklv;
				xdkqs = xdkqs - 1;
				xyhk = (dkzys - yhbjs - tqhkys) * (dklv * Math.pow((1 + dklv), xdkqs)) / (Math.pow((1 + dklv), xdkqs) - 1);
				jslx = yhk * s_yhkqs - yhdkys - byhk - xyhk * xdkqs;
				xdkjssj = Math.floor((parseInt(document.tqhdjsq.tqhksjn.value) * 12 + parseInt(document.tqhdjsq.tqhksjy.value) + xdkqs - 2) / 12) + '年' + ((parseInt(document.tqhdjsq.tqhksjn.value) * 12 + parseInt(document.tqhdjsq.tqhksjy.value) + xdkqs - 2) % 12 + 1) + '月'; 
			}else{
				xyhk = (dkzys - yhbjs - tqhkys) * (dklv * Math.pow((1 + dklv), (s_yhkqs - yhdkqs))) / (Math.pow((1 + dklv), (s_yhkqs - yhdkqs)) - 1);
				jslx = yhk * s_yhkqs - yhdkys - byhk - xyhk * (s_yhkqs - yhdkqs);
				xdkjssj = yhkjssj;
			}
		}
	}

	if(document.tqhdjsq.tqhkfs[0].checked || remark != ''){
		byhk = (dkzys - yhbjs) * (1 + dklv);
		xyhk = 0;
		jslx = yhk * s_yhkqs - yhdkys - byhk;
		xdkjssj = document.tqhdjsq.tqhksjn.value + '年' + document.tqhdjsq.tqhksjy.value + '月';
	}
        document.getElementById("ykhke").innerHTML = Math.round(yhk * 100) / 100;
        document.getElementById("yhkze").innerHTML= Math.round(yhdkys * 100) / 100;
        document.getElementById("gyyihke").innerHTML= Math.round(byhk * 100) / 100;
        document.getElementById("xyqyhke").innerHTML= Math.round(xyhk * 100) / 100;
        document.getElementById("jslxzc").innerHTML= Math.round(jslx * 100) / 100;
        document.getElementById("yzhhkq").innerHTML= yhkjssj;
        document.getElementById("xdzhhkq").innerHTML = xdkjssj;
        document.getElementById("jsjgts").innerHTML = remark;
}

function runjs3(obj){
	var dj3 = parseFloat(obj.dj3.value);
	var mj3 = parseFloat(obj.mj3.value);
	var fkz3 = dj3 * mj3;
	var yh = fkz3 * 0.0005;
	if (dj3 <= 9432) var q = fkz3 * 0.015;
	else if (dj3 > 9432) var q = fkz3 * 0.03;
	if (mj3 <= 120) var fw = 500;
	else if (120 < mj3 <= 5000) var fw = 1500;
	if (mj3 > 5000) var fw = 5000;
	var gzh = fkz3 * 0.003;
    document.getElementById("yh").innerHTML= Math.round(yh * 100,5) / 100;
    document.getElementById("fkz3").innerHTML = Math.round(fkz3 * 100, 5) / 100;
    document.getElementById("q").innerHTML= Math.round(q * 100,5) / 100;
    document.getElementById("gzh").innerHTML = Math.round(gzh * 100,5) / 100;
    document.getElementById("wt").innerHTML= Math.round(gzh * 100,5) / 100;
    document.getElementById("fw").innerHTML = Math.round(fw * 100, 5) / 100;
//    alert(Math.round(fw * 100, 5) / 100);
}

//个人公积金
var l1_5 = 0.0405;
var l6_30 = 0.0459;

function isIdCardNo(sNo){
	sNo = sNo.toString();
	if (sNo.length == 18){
		var a, b, c;
		if(!isInt(sNo.substr(0, 17))) {return false;}

		a = parseInt(sNo.substr(0, 1)) * 7 + parseInt(sNo.substr(1, 1)) * 9 + parseInt(sNo.substr(2, 1)) * 10;
		a = a + parseInt(sNo.substr(3, 1)) * 5 + parseInt(sNo.substr(4, 1)) * 8 + parseInt(sNo.substr(5, 1)) * 4;
		a = a + parseInt(sNo.substr(6, 1)) * 2 + parseInt(sNo.substr(7, 1)) * 1 + parseInt(sNo.substr(8, 1)) * 6;
		a = a + parseInt(sNo.substr(9, 1)) * 3 + parseInt(sNo.substr(10, 1)) * 7 + parseInt(sNo.substr(11, 1)) * 9;
		a = a + parseInt(sNo.substr(12, 1)) * 10 + parseInt(sNo.substr(13, 1)) * 5 + parseInt(sNo.substr(14, 1)) * 8;
		a = a + parseInt(sNo.substr(15, 1)) * 4 + parseInt(sNo.substr(16, 1)) * 2;
		b = a % 11;

		if(b == 2){
			c = sNo.substr(17, 1);
			//c = sNo.substr(17, 1).toUpperCase();
		}else{
			c = parseInt(sNo.substr(17, 1));
		}

		switch(b){
			case 0: if(c != 1) {return false;}break;
			case 1: if(c != 0) {return false;}break;
			case 2: if(c != "X") {return false;}break;
			case 3: if(c != 9) {return false;}break;
			case 4: if(c != 8) {return false;}break;
			case 5: if(c != 7) {return false;}break;
			case 6: if(c != 6) {return false;}break;
			case 7: if(c != 5) {return false;}break;
			case 8: if(c != 4) {return false;}break;
			case 9: if(c != 3) {return false;}break;
			case 10: if(c != 2){return false;}
		}
	}else{
		if(!isInt(sNo)) {return false;}
	}

	switch(sNo.length){
		case 15: if(isValidDate(sNo.substr(6, 2), sNo.substr(8, 2), sNo.substr(10, 2))) {return true;}
		case 18: if(isValidDate(sNo.substr(6, 4), sNo.substr(10, 2), sNo.substr(12, 2))) {return true;}
	}
	return false;
}

function isValidDate(iY, iM, iD){
	var undefined;
	if(iY != undefined && !isNaN(iY) && iY >= 0 && iY <= 9999 && iM != undefined && !isNaN(iM) && iM >= 1 && iM <= 12 && iD != undefined && !isNaN(iD) && iD >= 1 && iD <= 31){
		if(iY < 50) iY = 2000 + iY;
		else if (iY < 100) iY = 1900 + iY;
		if(iM == 2 && (isLeapYear(iY) && iD > 29 || !isLeapYear(iY) && iD > 28) || iD == 31 && (iM < 7 && iM % 2 == 0 || iM > 7 && iM % 2 == 1))
			return false;
		else return true;
	}else
		return false;
}

function isLeapYear(year){
	if((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0)){
		return true;
	}
	return false;
}

function isEmpty(str){
	if((str == null) || (str.length == 0)) return true;
	else return(false);
}

function isInt(theStr){
	var flag = true;

	if(isEmpty(theStr)){flag = false;}
	else{
		for(var i = 0; i < theStr.length; i++){
			if(theStr.substring(i, i + 1) == "."){
				flag = false;
				break;
			}
		}
	}
	return(flag);
}

function isnumeric(p)
{
 if (p == "")
  return false;
 var l = p.length;
 var count=0;
 for(var i=0; i<l; i++)
 {
  var digit = p.charAt(i);
  if(digit == "." )
 {
    ++count;
    if(count>1) return false;
   }
  else if(digit < "0" || digit > "9")
  return false;
 }
 return true;
}
function Format(num,dotLen) {

    var dot=0
    var num1=0
//change by liyugen
    if (typeof dotLen=="undefined")
        dot=2
    else
        dot=dotLen
    if (isNaN(parseFloat(num)))
        return 0
    else
        num1=parseFloat(num)
    var n1=Math.pow(10,dot)
    if (n1==0)
        var iValue= Math.round(num1)
    else
        var iValue=    Math.round(num1*n1)/n1
  var sValue = iValue.toString();
  if (sValue.indexOf(".") == -1)
  {
      sValue = sValue + ".00";
  }
  else
  {
      if (sValue.indexOf(".") == sValue.length - 1)
      {
          sValue = sValue + "00";
      }
      else if (sValue.indexOf(".") == sValue.length - 2)
      {
          sValue = sValue + "0";
      }
  }
return sValue
}
function c_id_card(obj){
var id_card=obj.id_card.value;//身份证
var age=0;
age_qx.innerText='';
if(id_card.length>0)
{
if(!isInt(id_card)){alert('身份证号码必须是数字');return false;}
if(id_card.length!=15 && id_card.length!=18){alert('身份证号码长度为15或18位');return false;}
if(!isIdCardNo(id_card)){alert('请输入正确的身份证号码');return false;}
var  a=new  Date();
var  y=Number(a.getFullYear());
if(id_card.length==15){age=y-Number('19'+id_card.substr(6,2));}else{age=y-Number(id_card.substr(6,4));}
var max_qx=70-age;if(max_qx>30){max_qx=30;}
age_qx.innerText='最长贷款'+max_qx+'年';
}
}
function gjjloan1(obj)
{
//	alert('个人住房公积金贷款计算器');
  var gryjce;//住房公积金个人月缴存额
  var poyjce;//配偶住房公积金个人月缴存额
  var grjcbl;//缴存比例
  var pojcbl;//配偶缴存比例
  var xy;//个人信用
  var fwzj;//房屋总价
  var fwxz;//房屋性质
  var dknx;//贷款申请年限
  var syhk;//首月还款额

  var dked;//需要贷款额度
  var hkfs;//还款方式
  var bxhj;//本息合计
  var bxhj2;//本息合计等本金

//中间变量
 var gbl;
 var jtysr;//家庭月收入
 var r;//月还款
 var gjjdka;//住房公积金贷款额度a
 var gjjdkb;//住房公积金贷款额度b
 var gjjdkc;//住房公积金贷款额度c

 gryjce=obj.mount2.value;
if(gryjce<=0){alert('住房公积金个人月缴存额不能为空,请输入');
                     obj.mount2.value='';obj.mount2.focus();return;}

 poyjce=obj.mount3.value;
 if(poyjce.length>0 && !isnumeric(poyjce))
{alert("配偶月缴存额录入不正确");return;}

if (obj.jcbl.value=="" || !isInt(obj.jcbl.value) || Number(obj.jcbl.value)<=0 ||Number(obj.jcbl.value)>=100)
{
	alert("缴存比例不正确");return;
}
if (poyjce.length>0 &&(obj.p_bl.value=="" || !isInt(obj.p_bl.value) || Number(obj.p_bl.value)<=0||Number(obj.p_bl.value)>=100) )
{
	alert("配偶缴存比例不正确");return;
}
grjcbl=obj.jcbl.value/100;
pojcbl=obj.p_bl.value/100;
/*
if (obj.xz[0].checked==true){fwxz=0.9;}
else {fwxz=0.95;}
*/
if (obj.xz[0].checked==true){fwxz=0.9;}
else {fwxz=0.8;}

if (obj.xy[0].checked==true){xy=1.3;}
else if(obj.xy[1].checked==true){xy=1.15;}
else {xy=1;}

 fwzj=obj.mount.value;

if(fwzj<=0){alert('＂房屋评估价值或实际购房款＂不能为空,请输入');
                     obj.mount.value='';return;}

 dknx=Math.round(obj.mount10.value);

if(dknx<=0){alert('贷款申请年限不能为空,请输入');
                     obj.mount10.value='';return;}
if(dknx>30){alert('贷款申请年限不能大于30年,请重新输入');
                     obj.mount10.value='';return;}
                     var bcv=0;
                     if (dknx>5)
                     {
                      bcv=Math.round( 1000000 * l6_30/12 ) / 1000000;
                    }else{
                    	 bcv=Math.round( 1000000 * l1_5/12 ) / 1000000;
                    	}
r=adv_format((10000*bcv*Math.pow(1+bcv,12*dknx))/(Math.pow(1+bcv,12*dknx)-1),2);


if(poyjce.length>0)
{
	jtysr=Math.ceil((gryjce/grjcbl+poyjce/pojcbl)*10)/10;
}
else
{
	jtysr=Math.ceil((gryjce/grjcbl)*10)/10;
}
if(jtysr<=400){alert('家庭月收入低于400，不符合贷款条件');
                     return;}

gjjdka=Math.min(Math.round((jtysr-400)/r*10000*10)/10,600000);
gjjdkb=Math.round(gjjdka*xy*10)/10;
gjjdkc=Math.round(fwzj*fwxz*10)/10;
//obj.ze2.value=gjjdka; //jtysr;
//obj.ze2.value = Math.floor(Math.min(gjjdkb, gjjdkc) / 10000 * 10) / 10;
//obj.ze2.value = Math.floor(Math.min(gjjdkb, gjjdkc) / 10000 * 10) / 10;
//alert(Math.floor(Math.min(gjjdkb, gjjdkc) / 10000 * 10) / 10);
document.getElementById("ze2").innerHTML = Math.floor(Math.min(gjjdkb, gjjdkc) / 10000 * 10) / 10;
zgdk = Math.floor(Math.min(gjjdkb, gjjdkc) / 10000 * 10) / 10; //最高贷款额度













//计算申请的最高贷款额度

/*
说明
家庭月收入＝住房公积金个人月缴存额÷缴存比例+配偶住房公积金个人月缴存额÷缴存比例

住房公积金贷款额度a＝（家庭月收入－400）÷等额均还月还款额每万元月还款额，且不大于40万元

对于个人信用等级为AAA的，住房公积金贷款额度b＝住房公积金贷款额度a×130％

对于个人信用等级为AA的，住房公积金贷款额度b＝住房公积金贷款额度a×115％

对于个人信用等级其它的，住房公积金贷款额度b＝住房公积金贷款额度a

对房屋性质为商品房期房的，住房公积金贷款额度c＝房屋总价×90％

对房屋性质为其它的，住房公积金贷款额度c＝房屋总价×95％

最高贷款额度d＝min（b，c）

等额均还还款公式：


等额本金还款公式

首月还款额=P/（n×12）+借款总额×I

其中：P为贷款本金，I为月利率，n为贷款年限。


  */

}
function adv_format(value,num)   //四舍五入
    {
    var a_str = formatnumber(value,num);
    var a_int = parseFloat(a_str);
    if (value.toString().length>a_str.length)
        {
        var b_str = value.toString().substring(a_str.length,a_str.length+1)
        var b_int = parseFloat(b_str);
        if (b_int<5)
            {
            return a_str
            }
        else
            {
            var bonus_str,bonus_int;
            if (num==0)
                {
                bonus_int = 1;
                }
            else
                {
                bonus_str = "0."
                for (var i=1; i<num; i++)
                    bonus_str+="0";
                bonus_str+="1";
                bonus_int = parseFloat(bonus_str);
                }
            a_str = formatnumber(a_int + bonus_int, num)
            }
        }
        return a_str
    }
function formatnumber(value,num)    //直接去尾
    {
    var a,b,c,i
    a = value.toString();
    b = a.indexOf('.');
    c = a.length;
    if (num==0)
        {
        if (b!=-1)
            a = a.substring(0,b);
        }
    else
        {
        if (b==-1)
            {
            a = a + ".";
            for (i=1;i<=num;i++)
                a = a + "0";
            }
        else
            {
            a = a.substring(0,b+num+1);
            for (i=c;i<=b+num;i++)
                a = a + "0";
            }
        }
    return a
    }
function gjjloan2(obj)
{

  var gryjce;//住房公积金个人月缴存额
  var poyjce;//配偶住房公积金个人月缴存额
  var grjcbl;//缴存比例
  var pojcbl;//配偶缴存比例
  var xy; //个人信用
  var fwzj;//房屋总价
  var fwxz;//房屋性质
  var dknx;//贷款申请年限
  var syhk; //首月还款额

  var dked;//需要贷款额度
  var hkfs;//还款方式
  var bxhj; //本息合计
  var bxhj2; //本息合计等本金

//中间变量
 var gbl;
 var jtysr; //家庭月收入
 var r;//月还款
 var rb;
 var gjjdka;//住房公积金贷款额度a
 var gjjdkb;//住房公积金贷款额度b
 var gjjdkc;//住房公积金贷款额度c


 gryjce=obj.mount2.value;
if(gryjce<=0){
	alert('住房公积金个人月缴存额不能为空,请输入.');
         obj.mount2.value='';
         obj.mount.focus();
         return;
}

 poyjce=obj.mount3.value;
/*if (obj.jcbl[0].checked==true)
{grjcbl=0.08;}
else {grjcbl=0.1;}

if (obj.p_bl[0].checked==true){pojcbl=0.08;}
else {pojcbl=0.1;}
*/
grjcbl=obj.jcbl.value/100;
pojcbl=obj.p_bl.value/100;
if (obj.xz[0].checked==true){fwxz=0.9;}
else {fwxz=0.8;}

if (obj.xy[0].checked==true){xy=1.15;}
else if(obj.xy[1].checked==true){xy=1.3;}
else {xy=1;}



 fwzj=obj.mount.value;

if(fwzj<=0){alert('房屋总价不能为空,请输入');
                     obj.mount.value='';return;}

 dknx=Math.round(obj.mount10.value);
//alert(dknx);
if(dknx<=0){alert('贷款申请年限不能为空,请输入');
                     obj.mount10.value='';return;}





var bcv=0;
                     if (dknx>5)
                     {
                      bcv=Math.round( 1000000 * l6_30/12 ) / 1000000;
                    }else{
                    	 bcv=Math.round( 1000000 * l1_5/12 ) / 1000000;
                    	}
r=adv_format((10000*bcv*Math.pow(1+bcv,12*dknx))/(Math.pow(1+bcv,12*dknx)-1),2);

jtysr=Math.ceil((gryjce/grjcbl+poyjce/pojcbl)*10)/10;
gjjdka=Math.min(Math.round((jtysr-400)/r*10000*10)/10,600000);
gjjdkb=Math.round(gjjdka*xy*10)/10;
gjjdkc=Math.round(fwzj*fwxz*10)/10;
//obj.ze2.value=gjjdka; //jtysr;
//obj.ze2.value=Math.floor(Math.min(gjjdkb,gjjdkc)/10000*10)/10;

//计算2
//alert(document.getElementById("ze2").innerHTML);
//zgdk=obj.ze2.value; //最高贷款额度
zgdk = document.getElementById("ze2").innerHTML; //最高贷款额度

//dked=Math.round(obj.need.value*10)/10;
dked = Math.round(document.getElementById("need").value * 10) / 10;

//obj.need.value = dked;
document.getElementById("need").value = dked;

if(dked==0){alert('需要的贷款额度不能为空,请输入');
                     obj.need.value='';return;}
if(dked<0){alert('输入的贷款额度不符合要求,请输入');
                     obj.need.value='';return;}


if(dked>zgdk){alert('不能高于最高贷款额度,请重新输入');
                     obj.need.value='';return;}


hkfs=obj.select.value;

if(hkfs==1){
//obj.ze22.value=Math.ceil(dked*r*100)/100;
//==============================modify by xujianfei
var ylv_new ;

if(dknx>=1&&dknx<=5)
ylv_new = l1_5/12;
else
ylv_new = l6_30/12;


var ncm  = parseFloat(ylv_new) + 1;//n次幂

var dknx_new = dknx*12;



total_ncm = Math.pow(ncm, dknx_new)

//obj.ze22.value = Math.round(((dked * 10000 * ylv_new * total_ncm) / (total_ncm - 1)) * 100) / 100;
//obj.ze22.value = Math.round(((dked * 10000 * ylv_new * total_ncm) / (total_ncm - 1)) * 100) / 100;
document.getElementById("ze22").innerHTML = Math.round(((dked * 10000 * ylv_new * total_ncm) / (total_ncm - 1)) * 100) / 100;

var pp = Math.round(((dked*10000*ylv_new*total_ncm)/(total_ncm-1))*100)/100;

//=========================================================
gbl=Math.round(Math.ceil(dked*r*100)/100/jtysr*1000)/10;
obj.yj2.value=gbl;
//bxhj=Math.round(r*dked*dknx*12*100)/100;
bxhj = Math.round(pp*dknx*12*100)/100;
//obj.lx2.value=bxhj;
//obj.lx2.value = bxhj;
document.getElementById("lx2").innerHTML = bxhj;
}
if (hkfs==2)
{
if (dknx>5)
{
	rb=l6_30*100;
	}else{
		rb=l1_5*100;
		}

syhk=Math.round((dked*10000/(dknx*12)+dked*10000*rb/(100*12))*100)/100;
//obj.sfk2.value=syhk;
//obj.sfk2.value = syhk;
document.getElementById("sfk2").innerHTML = syhk;
      
var yhke; //月还款额
var bxhj; //本息合计
var dkys; //贷款月数
var sydkze;//当前剩余贷款总额
var yhkbj; //月还款本金
dkys=dknx*12;
yhkbj=dked*10000/dkys;

yhke=syhk;
sydkze=dked*10000-yhkbj;
bxhj=syhk;
for (var count=2;count<=dkys; ++count)
	{
       		//yhke=Math.round((dked*10000/(dknx*12)+sydkze*rb/(100*12))*100)/100;
		yhke=dked*10000/dkys+sydkze*rb/1200;
		sydkze-=yhkbj;
		bxhj+=yhke;
	}
//obj.lx3.value= Math.round(bxhj*100)/100;
//obj.lx3.value = Math.round(bxhj * 100) / 100;
document.getElementById("lx3").innerHTML = Math.round(bxhj * 100) / 100;
}

if (hkfs==3)
{

switch(dknx){//自由还款还款方式月最低还款额参照表,调整利率不修改
	case 1 :  rb=83.04/100; break;
	case 2 :  rb=81.08/100; break;
	case 3 :  rb=79.12/100; break;
	case 4 :  rb=77.16/100; break;
	case 5 :  rb=75.20/100; break;
	case 6 :  rb=73.24/100; break;
	case 7 :  rb=71.28/100; break;
	case 8 :  rb=69.32/100; break;
	case 9 :  rb=67.36/100; break;
	case 10 :  rb=65.40/100; break;
	case 11 :  rb=63.44/100; break;
	case 12 :  rb=61.48/100; break;
	case 13 :  rb=59.52/100; break;
	case 14 :  rb=57.56/100; break;
	case 15 :  rb=55.60/100; break;
	case 16 :  rb=53.64/100; break;
	case 17 :  rb=51.68/100; break;
	case 18 :  rb=49.72/100; break;
	case 19 :  rb=47.76/100; break;
	case 20 :  rb=45.80/100; break;
	case 21 :  rb=43.84/100; break;
	case 22 :  rb=41.88/100; break;
	case 23 :  rb=39.92/100; break;
	case 24 :  rb=37.96/100; break;
	case 25 :  rb=36.00/100; break;
	case 26 :  rb=34.04/100; break;
	case 27 :  rb=32.08/100; break;
	case 28 :  rb=30.12/100; break;
	case 29 :  rb=28.16/100; break;
	case 30 :  rb=26.20/100; break;
}
var yhke; //月还款额
var ll;//利率
var zhbj;//最后一期本金
zhbj = Math.round(dked*10000*rb*100)/100;
if (dknx<=5)
{
	ll=l1_5/12;
	zdhkll=0.0378/12;
	var total_gjj = Math.pow(zdhkll + 1, dknx*12);
	syhk=Math.ceil(dked*10000*zdhkll*total_gjj/(total_gjj-1));
}
else
{
	ll=l6_30/12;
	zdhkll=0.0423/12;
	var total_gjj = Math.pow(zdhkll + 1, dknx*12-1);
	syhk=Math.ceil((dked*10000-zhbj)*zdhkll*total_gjj/(total_gjj-1)+zhbj*zdhkll);
}
//	obj.sfk3.value=syhk;       //最低还款额
//obj.sfk3.value = syhk;       //最低还款额
document.getElementById("sfk3").innerHTML = syhk;       //最低还款额
	var zhyqbj=dked*10000;
	var zchlx=0;//总偿还利息
	for (i=1;i<dknx*12 ;i++ )
	{
		zchlx+=Math.round(zhyqbj*ll*100)/100;
		zhyqbj=Math.round((zhyqbj-(syhk-Math.round(zhyqbj*ll*100)/100))*100)/100;
	}
	var sydkze=dked*10000-syhk;
//	obj.lx4.value=zhyqbj;    //最后期本金
	//	obj.lx5.value=Math.round(zhyqbj*ll*100)/100;

//	obj.lx4.value = zhyqbj;    //最后期本金
//	obj.lx5.value = Math.round(zhyqbj * ll * 100) / 100;
	document.getElementById("lx4").innerHTML = zhyqbj;    //最后期本金
	document.getElementById("lx5").innerHTML = Math.round(zhyqbj * ll * 100) / 100;

	/*for (i=1;i<dknx*12 ;i++ )
	{
		zchlx+=Math.round((dked*10000-(syhk-Math.round(dked*10000*ll*100)/100))*ll*100)/100;
	}*/
	zchlx+=Math.round(zhyqbj*ll*100)/100;
	//	obj.lx6.value=Math.round(zchlx*100)/100;
//	obj.lx6.value = Math.round(zchlx * 100) / 100;
	document.getElementById("lx6").innerHTML = Math.round(zchlx * 100) / 100;
}


//计算申请的最高贷款额度
/*
说明
家庭月收入＝住房公积金个人月缴存额÷缴存比例+配偶住房公积金个人月缴存额÷缴存比例

住房公积金贷款额度a＝（家庭月收入－400）÷等额均还月还款额每万元月还款额，且不大于40万元

对于个人信用等级为AAA的，住房公积金贷款额度b＝住房公积金贷款额度a×130％

对于个人信用等级为AA的，住房公积金贷款额度b＝住房公积金贷款额度a×115％

对于个人信用等级其它的，住房公积金贷款额度b＝住房公积金贷款额度a

对房屋性质为商品房期房的，住房公积金贷款额度c＝房屋总价×90％

对房屋性质为其它的，住房公积金贷款额度c＝房屋总价×95％

最高贷款额度d＝min（b，c）

等额均还还款公式：

等额本金还款公式

首月还款额=P/（n×12）+借款总额×I

其中：P为贷款本金，I为月利率，n为贷款年限。 */
}
function gjjloan3(obj)
{
var dkye=0;//所需要偿还的余额
var dkzqs=0;//贷款总期数
var gdhke=0;//固定还款额
var sxhke=0;//所需还款额
var sxhky=0; //所需要的还款月数
var zhhke=0;//最后还款额
var zglx=0;//总共利息
var dylx=0 ; //当月利息
var syqs=0;
syqs=Number(obj.lx8_sy.value);
dkye=Number(obj.lx7.value);
if(dkye<=0 || dkye >780000 || isNaN(dkye) )
{
	alert("贷款余额输入不正确");
	return;
}
var ll;   //月利率
if(obj.lx8[1].checked)
{ll=Math.round( 1000000 * l6_30/12 ) / 1000000;}
else
{ll=Math.round( 1000000 * l1_5/12 ) / 1000000;}
/*if(dkzqs<=0 || dkzqs>360 || isNaN(dkzqs))
{
	alert("贷款总期数不正确!");
	return;
}*/

gdhke=Number(obj.lx9.value);
if( Number(syqs) <= 0  || isNaN(syqs))
{
	alert("请输入正确的剩于期数!");
	return;
}
if( Number(gdhke) <= 0  || isNaN(gdhke))
{
	alert("请输入正确的固定还款额!");
	return;
}

var first_lx=Math.round( dkye * ll *100 ) /100 ;
if (first_lx > gdhke)
{alert('固定还款额必须大于首月利息 '+first_lx);obj.lx9.focus();obj.lx9.select();return;}
var yjqs=0;//Math.ceil(dkye/gdhke);
var sxhky=0;
for(var i=1;i<syqs;i++){
     //需要还款月+1
     sxhky =sxhky + 1;
     //计算一个月的利息
     dylx = Math.round( dkye * ll *100 ) /100 ;
     //累计利息
     zglx = zglx+dylx;
//Math.round ((zglx + dylx) *100) /100 ;

      if (dkye + dylx >= gdhke  )
       {
		  if(dkye + dylx == gdhke)zhhke= dkye +  dylx;
          dkye = Math.round((dkye-( gdhke - dylx ))*100)/100;
//Math.round(  (dkye - ( gdhke - dylx ))*100  ) /100;
       }
      else
       {
	   zhhke= dkye +  dylx;
	   dkye=-1;
        break;
       }

}
if(dkye>0){
	sxhky =sxhky + 1;
	dylx = Math.round( dkye * ll *100 ) /100 ;
	zglx = zglx+dylx;
	zhhke= dkye +  dylx
}


if (sxhky > syqs)
   {
      alert("输入不正确,请重新核对贷款余额和月固定还款额!"+sxhky);
      return;
   }



    obj.lx10.value=sxhky ;
    obj.lx11.value=Format(zhhke,2);
    obj.lx12.value=Format(zglx,2) ;
      //如果剩余本金+利息< 固定还款额度   ==> 还款结束  ->最后期还款额
}

//购房能力评估
rhb=new Array(440.104,301.103,231.7,190.136,163.753,144.08,129.379,117.991,108.923,101.542,95.425,90.282,85.902,82.133,78.861,75.997,73.473,71.236,69.241,67.455,65.848,64.397,63.082,61.887,60.798,59.802,58.890,58.052,57.282)
yhz=new Array(1.978,2.9344,3.8699,4.7847,5.6794,6.5544,7.4102,8.2472,9.0657,9.8662,10.6491,11.4148,12.1636,12.8959,13.6121,14.3126,14.9977,15.6677,16.3229,16.9637,17.5904,18.2034,18.8028,19.389,19.9624,20.5231,21.0715,21.6078,22.1323)
function chk01()
{
if (parseFloat(document.myform.rg01.value)<4.7)
	alert("--您确定是"+parseFloat(document.myform.rg01.value)+"万元?--"+"\n\n"+"那么您目前尚不具备购房能力，"+"\n\n"+"建议积攒积蓄或能筹集更多的资金。")
if (parseFloat(document.myform.rg01.value)>10000)
	alert("您确定拥有超过一亿元的购房资金？");

}
function chk02()
{
if (parseFloat(document.myform.rg03.value)>parseFloat(document.myform.rg02.value)*0.7)
	{
	alert("您预计家庭每月可用于购房支出已超过家庭月收入的70%，"+"\n\n"+"是否确定不会影响您的正常生活消费？"+"\n\n"+"建议在40%（"+parseFloat(document.myform.rg02.value)*0.4+"元）左右")
	}
}
function chk03()
{
//	alert('购房能力评估计算器');
if (document.myform.rg01.value=="")
	alert("请填写现可用于购房的资金")
else
	if (document.myform.rg02.value=="")
			alert("请填写现家庭月收入")
	else	
		if (document.myform.rg03.value=="")
			alert("请填写预计家庭每月可用于购房支出")
	else	
		if (document.myform.rg06.value=="")
			alert("请填写您计划购买房屋的面积")
	else
		chk04()

}
function chk04()
{
//您可购买的房屋总价=（家庭月收入-家庭月固定支出）×( (（1＋月利率）＾还款月数)－1  )÷［月利率×（1＋月利率）＾还款月数］+持有资金 
var month = parseInt(document.myform.rg04.options[document.myform.rg04.selectedIndex].value);
var year = parseInt(month/12);
var lilu=0.00576;
if(year>5)
	lilu=0.00594;
js00=parseFloat(document.myform.rg01.value);//现持有
js01=parseFloat(document.myform.rg02.value);//月收入
js02=parseFloat(document.myform.rg03.value);//月支出
js03=parseFloat(document.myform.rg06.value);//面积

var d1 = js01-js02;
var d2 = Math.pow(1 + lilu , month) - 1;
var d3= lilu * Math.pow(1+lilu,month)

//document.myform.rg07.value = Math.round( ((d1 * d2)/d3)+js00 );
//document.myform.rg08.value = parseFloat(document.myform.rg07.value)/js03;
//alert(Math.round(((d1 * d2) / d3) + js00));
document.getElementById("rg07").innerHTML = Math.round(((d1 * d2) / d3) + js00);
document.getElementById("rg08").innerHTML = parseFloat(Math.round(((d1 * d2) / d3) + js00)) / js03;

//pl();
}

function pl(){
	var XmlHttp;
	XmlHttp = new ActiveXObject("Msxml2.XMLHTTP.3.0");
	XmlHttp.open("POST", "/newhouse_calculateTool_projList.php?s=" + Math.random() + "&city=" + encodeURIComponent(_$('abcity').value) + "&price=" + Math.round(document.myform.rg08.value), false);
	XmlHttp.send(null);
	if(XmlHttp.status == 200){
		_$('projList').innerHTML = XmlHttp.responseText;
	}
}
function closeCityList() {
	if(_$('searchExpert2').style.display!="none") _$('searchExpert2').style.display='none';
}
function ChangeCity(city) {_$('abcity').value=city;_$('showcity').innerHTML=city;closeCityList()}