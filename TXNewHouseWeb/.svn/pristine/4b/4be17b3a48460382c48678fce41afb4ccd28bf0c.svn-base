using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data;
using Commons.DB;
using Commons;
using ServiceStack;

using TXCommons;
using TXCommons.DataToEntity;
using TXCommons.Index;
using TXCommons.MsgQueue;
using System.Data.SqlClient;
using TXCommons.PictureModular;
using Redis = TXCommons.PictureModular.Redis;

namespace TXGenerationRedisRankServices.BLL
{
    public class PremisesBLL
    {
        private int Top = Commons.Tool.ToInt32(Commons.Tool.ConfigKey("Top"));

        private DataTable dtClick;
        private List<clickRanking> dtClickList;
        public void main()
        {
            try
            {
                #region 点击量处理

                DataTable dt = GetAllPremises(); //所有的楼盘
                OptLog.Log("Msg：", "msg", "所有楼盘已获取", false);

                dtClick = GetClickCount(dt); //更新获取所有的点击量
                OptLog.Log("Msg：", "msg", "更新获取所有的点击量", false);

                RemovePremisesClickStatistics(); //清理点击量
                OptLog.Log("Msg：", "msg", "清理点击量", false);
                SavePremisesClickStatistics(dtClick); //重新生成点击量
                OptLog.Log("Msg：", "msg", "重新生成点击量", false);

                #endregion


                #region 生成真实排行榜
                UpdataAdPosition();
                #endregion
            }
            catch (Exception ex)
            {
                OptLog.Log("Msg：", "", "生成真实排行榜" + ex.Message, true);
            }


            //TopMain();



        }

        #region 点击量

        /// <summary>
        /// 批量保存点击量
        /// </summary>
        /// <param name="dtClick">带有点击量的数据集</param>
        private void SavePremisesClickStatistics(DataTable dtClick)
        {
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(Commons.Tool.ConfigKey("NewHouseDB"));
            sqlBulkCopy.DestinationTableName = "PremisesClickStatistics";
            sqlBulkCopy.WriteToServer(dtClick);
            sqlBulkCopy.Close();
        }

        /// <summary>
        /// 移除所有点击量
        /// </summary>
        private void RemovePremisesClickStatistics()
        {
            string sql = "TRUNCATE TABLE PremisesClickStatistics";
            NewHouseDB.SqlExecute(sql);
        }

        /// <summary>
        /// 所有房源+城市的数据集
        /// </summary>
        /// <returns></returns>
        private DataTable GetAllPremises()
        {
            string sql = "SELECT id as PremisesId,CityId FROM dbo.Premises(NOLOCK) where isdel=0";
            return NewHouseDB.GetTable(sql);
        }

        /// <summary>
        /// 获取所有房源的点击量
        /// </summary>
        /// <param name="AllPremises">所有楼盘数据集</param>
        /// <returns>带有点击量的数据集</returns>
        private DataTable GetClickCount(DataTable AllPremises)
        {
            //获取楼盘点击量
            DataTable dt = new DataTable();
            dt.Columns.Add("PremisesId");
            dt.Columns.Add("Count");
            dt.Columns.Add("CityId");
            foreach (DataRow item in AllPremises.Rows)
            {
                int PremisesId = Commons.Tool.ToInt32(item["PremisesId"]);
                int CityId = Commons.Tool.ToInt32(item["CityId"]);
                if (PremisesId == 0)
                {
                    continue;
                }
                DataRow row = dt.NewRow();
                row["PremisesId"] = PremisesId.ToString();
                row["CityId"] = CityId.ToString();
                row["Count"] = Redis.GetOneViewCountValue("NewHouseView_" + PremisesId, ServiceStack.FunctionTypeEnum.NewHouseViewCount, CityId);
                dt.Rows.Add(row);
            }
            dtClickList = TableToEntity.GetTableEntity<clickRanking>(dt);
            return dt;
        }
        #endregion
        #region 真实排行榜
        private void TopMain()
        {
            DataTable dtCity = GetCityId();
            RemoveCurrentPremisesRanking();
            foreach (DataRow item in dtCity.Rows)
            {
                //1.热点新盘推荐(点击量);
                //2.最新楼盘推荐(发布时间);
                //3.精品楼盘(标签数量);
                //4.即将开盘(待售楼盘)
                List<Advertise> DtAdvertiseData = GetAdvertiseData();
                int CityId = Commons.Tool.ToInt32(item["CityId"]);
                //SaveClickRank(DtAdvertiseData, CityId);
            }
        }

        #region 生成四个排行榜数据
        /// <summary>
        /// 点击量排行
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="CityId"></param>
        private void SaveClickRank(DataTable dt, int CityId)
        {
            DataRow[] rows = dt.Select("TypeId=1");
            int AllCount = 0;
            SavePremisesRanking(rows, 1, out AllCount);
            if (AllCount >= Top)
            {
                //如果手动排行满足需求就不执行数据填充
                return;
            }
            DataTable dtRankByCity = GetPremisesClickStatistics(CityId, AllCount);
            SavePremisesRanking(dtRankByCity, 1, out AllCount);
        }
        #endregion

        /// <summary>
        /// 获取有点击量的城市
        /// </summary>
        /// <returns>城市集合</returns>
        private DataTable GetCityId()
        {
            string sql = "SELECT DISTINCT CityId FROM dbo.PremisesClickStatistics(NOLOCK)";
            return NewHouseDB.GetTable(sql);
        }

        /// <summary>
        /// 获取指定数量的排序 
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns></returns>
        private DataTable GetPremisesClickStatistics(int CityId, int GetTop)
        {
            string sql = "select top " + GetTop + " PremisesId,CityId from PremisesClickStatistics(nolock) where cityid=" + CityId + " order  by Count desc";
            return NewHouseDB.GetTable(sql);
        }
        /// <summary>
        /// 写入真实排行榜
        /// </summary>
        /// <param name="dt"></param>
        private void SavePremisesRanking(DataRow[] rows, int TypeId, out int AllCount)
        {

            int Sort = 0;
            foreach (DataRow item in rows)
            {
                Sort++;
                SaveOnePremisesRanking(TypeId, Sort, item);
            }
            AllCount = Sort;
        }
        /// <summary>
        /// 写入真实排行榜
        /// </summary>
        /// <param name="dt"></param>
        private void SavePremisesRanking(DataTable dt, int TypeId, out int AllCount)
        {

            int Sort = 0;
            foreach (DataRow item in dt.Rows)
            {
                Sort++;
                SaveOnePremisesRanking(TypeId, Sort, item);
            }
            AllCount = Sort;
        }
        private static void SaveOnePremisesRanking(int TypeId, int Sort, DataRow item)
        {
            int psort = Commons.Tool.ToInt32(item["RSort"]);
            if (psort <= 0)
            {
                psort = Sort;
            }
            string sql = @"
                    INSERT INTO dbo.PremisesRanking
                            ( PremisesId,CityId, TypeId, RSort, Creatime )
                    VALUES  ( @PremisesId, -- PremisesId - int
                              @CityId, -- CityId - int
                              @TypeId, -- TypeId - int
                              @RSort, -- RSort - int
                              GETDATE()  -- Creatime - datetime
                              )";
            SqlParam parm = new SqlParam();
            parm.AddParam("@PremisesId", Commons.Tool.ToInt32(item["PremisesId"]));
            parm.AddParam("@CityId", Commons.Tool.ToInt32(item["CityId"]));
            parm.AddParam("@TypeId", TypeId);
            parm.AddParam("@RSort", psort);
            NewHouseDB.SqlExecute(sql, parm);
        }

        /// <summary>
        /// 删除当天的排行榜，以免生成时有重复数据
        /// </summary>
        private void RemoveCurrentPremisesRanking()
        {
            string sql = "DELETE PremisesRanking WHERE CONVERT(CHAR(10),Creatime,120)=CONVERT(CHAR(10),GETDATE(),120)";
            NewHouseDB.SqlExecute(sql);
        }



        #endregion
        #region 广告位获取生成
        private readonly int _hotNum = Commons.Tool.ToInt32(Commons.Tool.ConfigKey("HotNum"));//热点楼盘数量
        private readonly int _newNum = Commons.Tool.ToInt32(Commons.Tool.ConfigKey("NewNum"));//最新楼盘
        private readonly int _goodNum = Commons.Tool.ToInt32(Commons.Tool.ConfigKey("GoodNum"));//精品楼盘
        private readonly int _futureNum = Commons.Tool.ToInt32(Commons.Tool.ConfigKey("FutureNum"));//即将开盘
        //1热点新盘推荐 2最新楼盘推荐 3精品楼盘 4即将开盘
        /// <summary>
        /// 获取当天新房广告管理表
        /// </summary>
        private List<Advertise> GetAdvertiseData()
        {
            string sql = @"SELECT  ad.CityId ,
                            ad.PremisesId ,
                            ad.Position AS TypeId ,
                            ad.SequenceNum AS RSort
                    FROM    dbo.Advertise ad
                            INNER JOIN dbo.Premises p ON ad.PremisesId = p.Id
                    WHERE   p.IsDel = 0
                            AND ad.IsDel = 0
                            AND CONVERT(CHAR(10), BeginTime, 120) <= CONVERT(CHAR(10), GETDATE(), 120)
                            AND CONVERT(CHAR(10), EndTime, 120) >= CONVERT(CHAR(10), GETDATE(), 120)";
            var dt = NewHouseDB.GetTable(sql);
            return TableToEntity.GetTableEntity<Advertise>(dt);
        }
        #region lucene 数据
        /// <summary>
        /// 热门楼盘,点击数
        /// </summary>
        /// <returns></returns>
        private List<Advertise> GetHotPremises(string cityid)
        {
            if (dtClickList != null && !string.IsNullOrWhiteSpace(cityid))
            {
                var click = dtClickList.Where(m => m.CityId == cityid).ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("CityId");
                dt.Columns.Add("PremisesId");
                dt.Columns.Add("TypeId");
                dt.Columns.Add("RSort");
                if (click != null && click.Count > 0)
                {
                    foreach (var row in click)
                    {
                        DataRow newrow = dt.NewRow();
                        newrow["CityId"] = row.CityId;
                        newrow["PremisesId"] = row.PremisesId;
                        newrow["TypeID"] = 0;
                        newrow["RSort"] = 0;
                        dt.Rows.Add(newrow);
                    }
                }
                return TableToEntity.GetTableEntity<Advertise>(dt);
            }
            return null;
        }
        /// <summary>
        /// 最新楼盘
        /// </summary>
        /// <returns></returns>
        public List<IndexPremises> GetNewList(string cityid)
        {
            List<IndexPremises> list = new List<IndexPremises>();

            string sql = @"SELECT top 100 ID AS PremisesId ,
                            p.cityId                           
                    FROM    premises (NOLOCK) p
                            LEFT JOIN PremisesClickStatistics (NOLOCK) click ON p.ID = click.PremisesID
                    WHERE p.isdel=0 and  p.cityid =  " + cityid;

            sql += " ORDER BY p.id DESC";
            DataTable dt = NewHouseDB.GetTable(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    IndexPremises model = new IndexPremises();
                    model.PremisesID = row["PremisesId"].ToString();
                    model.CityID = row["cityId"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        private List<IndexPremises> GetNewPremises(string cityId)
        {
            if (!string.IsNullOrWhiteSpace(cityId))
            {
                List<IndexPremises> list = new List<IndexPremises>();
                list = GetNewList(cityId);
                return list;
            }
            //var url = MQHelp.GetLuceneSearchUrlByCityId(cityId);
            //var city = _citys.SingleOrDefault(m => m.Id == cityId);
            //if (city != null)
            //{
            //    string cityname = PinyinHelper.GetPinyin(city.Name);
            //    List<IndexPremises> futurePremises =
            //        PremisesData.PremisesListResult<IndexPremises>(string.Format("{0}-xinfang-quyu-o1", cityname), url, Convert.ToInt32(city.Id)); //即将开盘
            //    return futurePremises;
            //}
            return null;
        }



        /// <summary>
        /// 精品楼盘
        /// </summary>
        /// <returns></returns>
        private List<IndexPremises> GetGoodPremises(string cityid)
        {
            var url = MQHelp.GetLuceneSearchUrlByCityId(cityid);
            //var url = "http://nhindex.kuaiyoujia.com/premises/index.ashx";
            var city = _citys.SingleOrDefault(m => m.Id == cityid);


            if (city != null)
            {
                string cityname = PinyinHelper.GetPinyin(city.Name);
                List<IndexPremises> futurePremises =
                    PremisesData.PremisesListResult<IndexPremises>(string.Format("{0}-xinfang-quyu-c7", cityname),
                                                                   url,
                                                                   Convert.ToInt32(city.Id)); //即将开盘
                return futurePremises;
            }

            return null;
        }
        /// <summary>
        /// 即将开盘楼盘
        /// </summary>
        /// <returns></returns>
        private List<IndexPremises> GetFuturePremises(string cityid)
        {
            var url = MQHelp.GetLuceneSearchUrlByCityId(cityid);
            //var url = "http://nhindex.kuaiyoujia.com/premises/index.ashx";
            var city = _citys.SingleOrDefault(m => m.Id == cityid);
            if (city != null)
            {
                string cityname = PinyinHelper.GetPinyin(city.Name);
                List<IndexPremises> futurePremises =
                    PremisesData.PremisesListResult<IndexPremises>(string.Format("{0}-xinfang-quyu-o0", cityname), url,
                                                                   Convert.ToInt32(city.Id)); //即将开盘
                return futurePremises;
            }
            return null;
        }
        #endregion
        public List<City> GetCitys()
        {
            string sql = @"SELECT Id,Name FROM Area WHERE PId>1";//  AND IsOpenHOTELHigh=1
            //string constr = System.Configuration.ConfigurationSettings.AppSettings["WebDB"];
            //var dt = NewHouseDB.GetTable(sql, null, constr);
            return TableToEntity.GetTableEntity<City>(WebDB.GetTable(sql));
        }
        private List<City> _citys;
        private string _cityname;
        private void UpdataAdPosition()
        {
            //获取所有广告配置
            //获取所有点击量,热门楼盘
            //根据城市分组，分别找出相对应的广告位置，如果广告位置设置没有达到相应的数量，从其他数据源中获取。

            List<Advertise> adsets = GetAdvertiseData(); //获取所有已有的广告设置
            DataTable premiseRanktb = new DataTable();
            premiseRanktb.Columns.Add("PremisesId");
            premiseRanktb.Columns.Add("cityId");
            premiseRanktb.Columns.Add("cityName");
            premiseRanktb.Columns.Add("TypeId");
            premiseRanktb.Columns.Add("Rsort");
            _citys = GetCitys();
            _citys.ForEach(m => m.Name = m.Name.TrimEnd('市'));
            if (_citys != null && _citys.Count > 0) //为每个城市生成相应的广告位
            {
                OptLog.Log("Msg：", "msg", "开始处理数据。。", true);
                foreach (var city in _citys)
                {

                    List<Advertise> cityadset = adsets != null
                                                    ? adsets.Where(m => m.CityId.ToString() == city.Id).ToList()
                                                    : null;

                    string cityname = PinyinHelper.GetPinyin(city.Name);
                    _cityname = cityname;
                    GetHotRank(city.Id, premiseRanktb, cityadset, Position.Hot);
                    GetPremisesRank(city.Id, premiseRanktb, cityadset, Position.New);
                    GetPremisesRank(city.Id, premiseRanktb, cityadset, Position.Good);
                    GetPremisesRank(city.Id, premiseRanktb, cityadset, Position.Future);

                }
                OptLog.Log("Msg：", "msg", "数据处理完毕。。", false);
            }

            try
            {
                try
                {
                    RemovePremisesRank();
                }
                catch (Exception ex)
                {
                    OptLog.Log("Msg：", "msg", "RemovePremisesRank" + ex.Message, false);
                }
                try
                {

                    DataTable newdt = GetFullInfo(premiseRanktb);
                    insertRank(newdt);
                    OptLog.Log("Msg：", "msg", string.Format("共获取到{0}条广告设置插入数据库", newdt.Rows.Count), false);
                    ClearRedisData(newdt);
                    OptLog.Log("Msg：", "msg", "清除redis", false);
                    SetRedisData(newdt);
                    OptLog.Log("Msg：", "msg", string.Format("更新redis {0}条广告设置", newdt.Rows.Count), true);
                }
                catch (Exception ex)
                {

                    OptLog.Log("Msg：", "msg", "ddd" + ex.Message, false);
                }
            }
            catch (Exception ex)
            {
                OptLog.Log("Msg：", "msg", ex.Message + ex.StackTrace, false);
            }

        }
        #region 获取图片 楼盘名称等等附加信息

        public DataTable GetFullInfo(DataTable infodt)
        {

            if (infodt != null && infodt.Rows.Count > 0)
            {
                DataTable fulldt = new DataTable();
                fulldt.Columns.Add("PremisesId");
                fulldt.Columns.Add("CityId");
                fulldt.Columns.Add("TypeId");
                fulldt.Columns.Add("RSort");
                fulldt.Columns.Add("CityName");
                fulldt.Columns.Add("PremisesName");
                fulldt.Columns.Add("Ring"); //环线
                fulldt.Columns.Add("ImgPath"); //效果图
                fulldt.Columns.Add("Developer");
                fulldt.Columns.Add("Agent");
                fulldt.Columns.Add("PremisesAddress");
                fulldt.Columns.Add("PremisesMoney");
                fulldt.Columns.Add("Phone");
                fulldt.Columns.Add("Feature"); //特色
                fulldt.Columns.Add("HouseTypesStatistics");
                fulldt.Columns.Add("HouseTypes");
                var featurelist = GetPremisesFeature();
                foreach (DataRow row in infodt.Rows)
                {
                    int cityid = Convert.ToInt32(row["CityId"]);
                    int premisesid = Convert.ToInt32(row["PremisesId"]);
                    //var premisemodel = new TXBll.HouseData.PremisesBll().GetPVM_PremisesById(premisesid);
                    DataTable dtPremises = GetPremisesInfo(premisesid);
                    if (dtPremises.Rows == null || dtPremises.Rows.Count <= 0)
                    {
                        continue;
                    }
                    DataRow rowPremises = dtPremises.Rows[0];

                    string featureids = rowPremises["Characteristic"].ToString();
                    DataRow newrow = fulldt.NewRow();
                    newrow["PremisesId"] = row["PremisesId"];
                    newrow["CityId"] = row["cityId"];
                    newrow["CityName"] = row["cityName"];
                    newrow["TypeId"] = row["TypeId"];
                    newrow["Rsort"] = row["Rsort"];
                    newrow["PremisesName"] = rowPremises["PremisesName"];
                    newrow["Developer"] = rowPremises["Developer"];
                    newrow["Agent"] = rowPremises["Agent"];
                    newrow["PremisesAddress"] = rowPremises["PremisesAddress"];
                    int i = 0;
                    string price = "0";
                    string dataprice = rowPremises["ReferencePrice"].ToString();
                    if (dataprice.IndexOf('.') > 0)
                    {
                        if (int.TryParse(dataprice.Substring(0, dataprice.IndexOf('.')), out
                        i) &&
                        i > 0)
                        {
                            price = i.ToString();
                        }
                    }
                    newrow["PremisesMoney"] = price;

                    newrow["HouseTypesStatistics"] = "";
                    newrow["HouseTypes"] = "";
                    string tel = rowPremises["TelePhone"].ToString();
                    if (rowPremises["IsShow400"].ToString() == "True" &&
                        !string.IsNullOrWhiteSpace(rowPremises["Phone400"].ToString()))
                    {
                        tel = rowPremises["Phone400"].ToString();
                    }
                    newrow["Phone"] = tel;

                    newrow["Ring"] = GetRingLine(cityid, Commons.Tool.ToInt32(rowPremises["Ring"]));
                    List<PremisesPictureInfo> listEffect =
                        Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(
                            KeyManager.GetPremisesPictureKey(rowPremises["InnerCode"].ToString(),
                                                             PremisesPictureType.Effect.ToString()),
                            FunctionTypeEnum.HouseImage, cityid);
                    string imgpath = string.Empty;
                    if (listEffect != null && listEffect.Count > 0)
                    {
                        //listEffect = listEffect.OrderByDescending(m => m.CreateTime).ToList();
                        imgpath = Redis.IMGWEB_BASE + listEffect.First().Path;
                    }
                    newrow["ImgPath"] = imgpath;
                    string feature = string.Empty;
                    if (!string.IsNullOrWhiteSpace(featureids))
                    {
                        List<string> fids = featureids.Split(',').Where(m => !string.IsNullOrWhiteSpace(m)).ToList();
                        List<string> newfids = new List<string>();
                        foreach (var f in fids)
                        {
                            int fidd;
                            if (int.TryParse(f, out fidd))
                            {

                                if (featurelist.Keys.Contains(fidd))
                                {
                                    string val = featurelist[fidd];
                                    if (!string.IsNullOrWhiteSpace(val))
                                        newfids.Add(val);
                                }

                            }
                            feature = string.Join(",", newfids);
                        }
                    }
                    newrow["Feature"] = feature;
                    fulldt.Rows.Add(newrow);

                }
                return fulldt;
            }

            return null;
        }

        #endregion
        #region  楼盘实体信息

        public DataTable GetPremisesInfo(int id)
        {
            string sql = "SELECT top 1 name as PremisesName,Developer,Agent,PremisesAddress,Characteristic,ReferencePrice,Ring,IsShow400,Phone400,InnerCode,TelePhone FROM dbo.Premises(NOLOCK) WHERE id=@id order by id desc";
            SqlParam parm = new SqlParam();
            parm.AddParam("@id", id);
            return NewHouseDB.GetTable(sql, parm);
        }

        #endregion
        #region 环线
        private string GetRingLine(int cityId, int ring)
        {
            var list = new List<KeyValuePair<int, string>>();
            switch (cityId)
            {
                case 253:
                    //北京市
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "二环以内"),
                        new KeyValuePair<int, string>(2, "二环至三环"),
                        new KeyValuePair<int, string>(3, "三环至四环"),
                        new KeyValuePair<int, string>(4, "四环至五环"),
                        new KeyValuePair<int, string>(5, "五环至六环"),
                        new KeyValuePair<int, string>(6, "六环以外")
                    }); break;
                case 239:
                    // 上海
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "内环以内"),
                        new KeyValuePair<int, string>(2, "中内环间"),
                        new KeyValuePair<int, string>(3, "中外环间"),
                        new KeyValuePair<int, string>(4, "外环以外")
                    }); break;
                case 155:
                    // 成都
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "一环以内"),
                        new KeyValuePair<int, string>(2, "一至二环"),
                        new KeyValuePair<int, string>(3, "二至三环"),
                        new KeyValuePair<int, string>(4, "三环以外")
                    }); break;
                case 205:
                    // 天津
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "内环以内"),
                        new KeyValuePair<int, string>(2, "内至中环"),
                        new KeyValuePair<int, string>(3, "中至外环"),
                        new KeyValuePair<int, string>(4, "外环以外")
                    }); break;
                case 224:
                    // 武汉
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "内环以内"),
                        new KeyValuePair<int, string>(2, "内至二环"),
                        new KeyValuePair<int, string>(3, "二至中环"),
                        new KeyValuePair<int, string>(4, "中环以外")
                    }); break;
                default:
                    return "";
            }
            if (list != null)
            {
                var pring = list.SingleOrDefault(m => m.Key == ring);
                if (!string.IsNullOrWhiteSpace(pring.Value))
                    return pring.Value;
            }
            return "";
        }
        #endregion
        #region 获取楼盘特色
        private Dictionary<int, string> GetPremisesFeature()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            string sql = @"SELECT  Id,Name
                           FROM    PremisesFeature WHERE IsDel=0";
            var dt = NewHouseDB.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dic.Add(Convert.ToInt32(row["Id"]), row["Name"].ToString());
                }
            }
            return dic;
        }
        #endregion
        #region 插入广告数据库，更新redis
        private void insertRank(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder sbsql = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    sbsql.AppendFormat(@"INSERT INTO dbo.PremisesRanking
        ( PremisesId ,
          Ring ,
          PremisesName ,
          ImgPath ,
          Developer ,
          Agent ,
          PremisesAddress ,
          PremisesMoney ,
          Phone ,
          Feature ,
          CityId ,
          CityName ,
          TypeId ,
          RSort  
        )
VALUES  ( {0} , -- PremisesId - int
          '{1}' , -- Ring - varchar(500)
          '{2}' , -- PremisesName - varchar(500)
          '{3}' , -- ImgPath - varchar(500)
          '{4}' , -- Developer - varchar(500)
          '{5}' , -- Agent - varchar(500)
          '{6}' , -- PremisesAddress - varchar(500)
          {7} , -- PremisesMoney - int
          '{8}' , -- Phone - varchar(500)
          '{9}' , -- Feature - varchar(500)
          {10} , -- CityId - int
          '{11}' , -- CityName - varchar(50)
          {12} , -- TypeId - int
          {13}            
        );", row["PremisesId"], row["Ring"], row["PremisesName"], row["ImgPath"], row["Developer"], row["Agent"], row["PremisesAddress"].ToString().Replace("'", ""), row["PremisesMoney"], row["Phone"], row["Feature"], row["CityId"], row["CityName"], row["TypeId"], row["RSort"]);
                }
                int result = NewHouseDB.SqlExecute(sbsql.ToString());
            }
        }

        /// <summary>
        /// 写入redis服务器数据
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="type">数据类型</param>
        /// <param name="cityname">城市拼音</param>
        private static void SetRedisData(DataTable dt)
        {
            var dtgroup = dt.Rows.Cast<DataRow>().GroupBy(m => new { type = m["TypeId"], city = m["CityName"] });
            foreach (var g in dtgroup)
            {
                DataTable newdt = dt.Clone();
                g.ToList().ForEach(m => newdt.Rows.Add(m.ItemArray));
                Position type;
                if (Enum.TryParse(g.Key.type.ToString(), out type))
                {
                    string key = type.ToString().ToUpper() + ":" + g.Key.city;
                    Redis.SetValue<DataTable>(
                       key,
                        newdt,
                        null,
                        ServiceStack.FunctionTypeEnum.NewHouseRelatedRecommendations,
                        0
                        );
                }
            }

        }

        private void ClearRedisData(DataTable dt)
        {
            if (dt != null)
            {
                DataTable newdt = dt.Clone();
                foreach (var city in _citys)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        Position type;
                        if (Enum.TryParse(i.ToString(), out type))
                        {
                            string key = type.ToString().ToUpper() + ":" + PinyinHelper.GetPinyin(city.Name).ToLower();
                            Redis.SetValue<DataTable>(
                                key,
                                newdt,
                                null,
                                ServiceStack.FunctionTypeEnum.NewHouseRelatedRecommendations,
                                0
                                );
                        }
                    }
                }
            }
        }

        #endregion
        private void RemovePremisesRank()
        {
            string sql = "TRUNCATE TABLE PremisesRanking";
            NewHouseDB.SqlExecute(sql);
        }
        #region 最新，精品，即将开盘
        /// <summary>
        /// 最新，精品，即将开盘广告位设置
        /// </summary>
        /// <param name="cityid"></param>
        /// <param name="dt"></param>
        /// <param name="adlist"></param>
        /// <param name="type"></param>
        private void GetPremisesRank(string cityid, DataTable dt, List<Advertise> adlist, Position type)
        {
            if (null == adlist)
            {
                adlist = new List<Advertise>();
            }
            var adset = adlist.Where(m => m.TypeId == ((int)type).ToString());
            int maxcount = GetPostionMaxNum(type);
            int lazy = maxcount - adset.Count();
            foreach (var hot in adset)
            {
                DataRow newrow = dt.NewRow();
                newrow["PremisesId"] = hot.PremisesId;
                newrow["cityId"] = hot.CityId;
                newrow["cityName"] = _cityname;
                newrow["TypeId"] = hot.TypeId;
                newrow["Rsort"] = hot.RSort;
                dt.Rows.Add(newrow);
            }
            if (lazy > 0)//数量有剩余，就去数据库中取没有重复的数据填充
            {
                int maxsort = adset.Count() > 0 ? adset.Max(m => Convert.ToInt32(m.RSort)) : 0;
                GetRankDB(cityid, dt, lazy, maxsort, adset.ToList(), type);
            }
        }

        private void GetRankDB(string cityid, DataTable dt, int lazy, int maxsort, List<Advertise> hasad, Position type)
        {
            try
            {
                var dbpremise = GetDbpremises(type, cityid);
                if (dbpremise != null && dbpremise.Count > 0)
                {
                    if (hasad != null)
                    {
                        hasad.ForEach(m => dbpremise.RemoveAll(n => n.PremisesID == m.PremisesId.ToString()));
                    }
                    if (dbpremise != null && dbpremise.Count > 0)
                    {
                        dbpremise = dbpremise.Take(lazy).ToList();

                        for (int i = 0; i < dbpremise.Count; i++)
                        {
                            maxsort += 1;
                            DataRow newrow = dt.NewRow();
                            newrow["PremisesId"] = dbpremise[i].PremisesID;
                            newrow["cityId"] = dbpremise[i].CityID;
                            newrow["cityName"] = _cityname;
                            newrow["TypeId"] = (int)type;
                            newrow["Rsort"] = maxsort;
                            dt.Rows.Add(newrow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 返回相应的lucene数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cityid"></param>
        /// <returns></returns>
        private List<IndexPremises> GetDbpremises(Position type, string cityid)
        {
            try
            {
                switch (type)
                {
                    case Position.New:
                        return GetNewPremises(cityid);
                    case Position.Good:
                        return GetGoodPremises(cityid);
                    case Position.Future:
                        return GetFuturePremises(cityid);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        #endregion
        #region 热门
        /// <summary>
        /// 热门楼盘广告位设置
        /// </summary>
        /// <param name="cityid"></param>
        /// <param name="dt"></param>
        /// <param name="adlist"></param>
        /// <param name="type"></param>
        private void GetHotRank(string cityid, DataTable dt, List<Advertise> adlist, Position type)
        {

            if (null == adlist)
            {
                adlist = new List<Advertise>();
            }

            var adset = adlist.Where(m => m.TypeId == ((int)type).ToString());

            int maxcount = GetPostionMaxNum(type);

            int lazy = maxcount - adset.Count();
            int maxRSort = 0;

            foreach (var hot in adset)
            {
                DataRow newrow = dt.NewRow();
                newrow["PremisesId"] = hot.PremisesId;
                newrow["cityId"] = hot.CityId;
                newrow["cityName"] = _cityname;
                newrow["TypeId"] = hot.TypeId;
                newrow["Rsort"] = hot.RSort;
                int n = 0;
                int.TryParse(hot.RSort, out n);
                if (maxRSort < n)
                {
                    maxRSort = n;
                }
                dt.Rows.Add(newrow);
            }

            if (lazy > 0)
            {
                //int maxsort = adset.Count() > 0 ? adset.Max(m => Commons.Tool.ToInt32(m.RSort)) : 0;
                GetHotFromDB(cityid, dt, lazy, maxRSort, adset.ToList());
            }



        }

        private void GetHotFromDB(string cityid, DataTable dt, int lazy, int maxsort, List<Advertise> hasad)
        {

            var hot = GetHotPremises(cityid);

            if (hot != null && hot.Count > 0)
            {
                if (hasad != null)
                {
                    foreach (var v in hasad)
                    {
                        var h = hot.Where(m => m.PremisesId == v.PremisesId);
                        if (h.Count() > 0)
                            hot.RemoveAll(m => m.PremisesId == v.PremisesId);
                    }
                    //hasad.ForEach(m => hot.RemoveAll(n => n.PremisesId == m.PremisesId));

                }

                if (hot != null && hot.Count > 0)
                {
                    hot = hot.Take(lazy).ToList();

                    for (int i = 0; i < hot.Count; i++)
                    {
                        maxsort += 1;
                        DataRow newrow = dt.NewRow();
                        newrow["PremisesId"] = hot[i].PremisesId;
                        newrow["cityId"] = hot[i].CityId;
                        newrow["cityName"] = _cityname;
                        newrow["TypeId"] = 1;
                        newrow["Rsort"] = maxsort;
                        dt.Rows.Add(newrow);
                    }

                }

            }


        }
        #endregion
        private int GetPostionMaxNum(Position postion)
        {
            switch (postion)
            {
                case Position.Hot:
                    return _hotNum;
                case Position.New:
                    return _newNum;
                case Position.Good:
                    return _goodNum;
                case Position.Future:
                    return _futureNum;
            }
            return 4;
        }

        #endregion
    }
    //(1热点新盘推荐 2最新楼盘推荐 3精品楼盘 4即将开盘 )
    public enum Position
    {
        /// <summary>
        /// 热门楼盘
        /// </summary>
        Hot = 1,
        /// <summary>
        /// 最新楼盘
        /// </summary>
        New,
        /// <summary>
        /// 精品楼盘
        /// </summary>
        Good,
        /// <summary>
        /// 即将开盘
        /// </summary>
        Future
    }

    public class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Advertise
    {
        public string CityId { get; set; }
        public string PremisesId { get; set; }
        public string TypeId { get; set; }
        public string RSort { get; set; }
    }

    public class clickRanking
    {
        public string PremisesId { get; set; }
        public string Count { get; set; }
        public string CityId { get; set; }
    }
}
