using System;
using System.Collections.Generic;
using Webdiyer.WebControls.Mvc;
using System.Data;
using System.Reflection;
using TXCommons.Index;
using TXCommons.MsgQueue;
using TXDal.HouseData;
namespace TXBll.NHouseSearch
{
    public class SearchBll
    {
        /// <summary>
        /// 通过索引获取新房列表
        /// 作者：sunlin 
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static PagedList<IndexPremises> NHouseListResult(string conditions, string getdatapagename, int cityId = 253)
        {
            try
            {
                //var con = TXCommons.Index.IndexConditionInfo.GetIndexSearchCondiction(conditions);
                //string premises_xml_url = "http://" + TXCommons.MsgQueue.MQHelp.GetLuceneSearchByCityId(cityId) + "?condition=" + con;//查询生成xml文件http://index.kyjob.com/house/index.ashx?condition=
                var url = MQHelp.GetLuceneSearchUrlByCityId(cityId.ToString());
                string premises_xml_url = url + "?condition=" + conditions;//查询生成xml文件
                DataSet ds = new DataSet("Results");
                ds.ReadXml(premises_xml_url);
                if (ds.Tables.Count < 2)
                {
                    return null;
                }
                else
                {
                    DataRow PageRow = ds.Tables[0].Rows[0];
                    DataTable dt = ds.Tables[1];
                    if (dt.Rows.Count > 0)
                    {
                        List<IndexPremises> listform = new List<IndexPremises>();
                        TXCommons.Index.IndexPremises sf = new TXCommons.Index.IndexPremises();
                        //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            sf = new IndexPremises();
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //获取属性值需要指定属性名
                                PropertyInfo ProInfo = sf.GetType().GetProperty(dt.Columns[k].ColumnName);
                                ProInfo.SetValue(sf, Convert.ToString(dr[k]), null);
                            }
                            listform.Add(sf);
                        }
                        PagedList<IndexPremises> p = new PagedList<IndexPremises>(listform, Convert.ToInt32(PageRow["PageIndex"]), Convert.ToInt32(PageRow["PageSize"]), Convert.ToInt32(PageRow["TotalRecords"]));
                        return p;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("sunlin", string.Format("({0},{1})", conditions, getdatapagename), e);
                throw;
            }
        }
        #region 通过索引获取某个楼盘的参加营销活动的房源列表
        /// <summary>
        /// 通过索引获取某个楼盘的参加营销活动的房源列表
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="getdatapagename"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static List<IndexPremises> getHouseList(string conditions, string getdatapagename, int cityId = 253)
        {
            try
            {
                //var con = TXCommons.Index.IndexConditionInfo.GetIndexSearchCondiction(conditions);
                //string premises_xml_url = "http://" + TXCommons.MsgQueue.MQHelp.GetLuceneSearchByCityId(cityId) + "?condition=" + con;//查询生成xml文件http://index.kyjob.com/house/index.ashx?condition=
                var url = MQHelp.GetLuceneSearchUrlByCityId(cityId.ToString());
                string premises_xml_url = url + "?condition=" + conditions;//查询生成xml文件
                DataSet ds = new DataSet("Results");
                ds.ReadXml(premises_xml_url);
                if (ds.Tables.Count < 2)
                {
                    return null;
                }
                else
                {
                    DataRow PageRow = ds.Tables[0].Rows[0];
                    DataTable dt = ds.Tables[1];
                    if (dt.Rows.Count > 0)
                    {
                        List<IndexPremises> listform = new List<IndexPremises>();
                        TXCommons.Index.IndexPremises sf = new TXCommons.Index.IndexPremises();
                        //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            sf = new IndexPremises();
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //获取属性值需要指定属性名
                                PropertyInfo ProInfo = sf.GetType().GetProperty(dt.Columns[k].ColumnName);
                                ProInfo.SetValue(sf, Convert.ToString(dr[k]), null);
                            }
                            listform.Add(sf);
                        }
                        //  PagedList<IndexPremises> p = new PagedList<IndexPremises>(listform, Convert.ToInt32(PageRow["PageIndex"]), Convert.ToInt32(PageRow["PageSize"]), Convert.ToInt32(PageRow["TotalRecords"]));
                        return listform;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("sunlin", string.Format("({0},{1})", conditions, getdatapagename), e);
                throw;
            }
        }
        #endregion
        #region 通过索引获取最热楼盘列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0,最热，1最新</param>
        /// <param name="cityId">城市id</param>
        /// <returns></returns>
        public List<IndexRecList> PremisesList(int type, Enum enmu, int cityId = 253, string districtId = null)
        {
            List<IndexRecList> list = new List<IndexRecList>();
            try
            {
                string typename = string.Empty;
                if (type == 0)
                    typename = "HOT";
                else if (type == 1)
                    typename = "NEW";
                string key = typename + "HouseSearchBll:" + cityId;
                if (!string.IsNullOrEmpty(districtId))
                    key += ":" + districtId;
                list = TXCommons.Redis.GetValue<List<IndexRecList>>(key, enmu, 0);
                if (list == null || list.Count == 0)
                {
                    if (type == 0)
                    {
                        list = new Premises_Recomment().GetHotList(cityId, districtId);
                        TXCommons.Redis.SetValue(key, list, DateTime.Now.AddHours(24), enmu, 0);
                    }
                    else if (type == 1)
                    {
                        list = new Premises_Recomment().GetNewList(cityId, districtId);
                        TXCommons.Redis.SetValue(key, list, DateTime.Now.AddHours(1), enmu, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马欢", string.Format("({0},{1})", type, enmu.ToString()), ex);
                throw;
            }
            return list;
        }


        /// <summary>
        /// 通过索引获取最热楼盘列表
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="getdatapagename"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<IndexRanking> PremisesList(string ranking, string conditions, string getdatapagename, int cityId = 253)
        {
            try
            {
                IndexRankingConditionInfo conditionInfo = new IndexRankingConditionInfo();
                conditionInfo.CityID = cityId.ToString();
                /// 排序 (下标 5)（1 点击率 ，2 最新 3 均价）
                conditionInfo.Ranking = ranking;
                string temp = TXCommons.Index.IndexConditionInfo.GetSearchRankingCondiction(conditionInfo);
                var url = TXCommons.MsgQueue.MQHelp.GetLuceneRankingUrlByCityId(conditionInfo.CityID);
                string premises_xml_url = url + "?condition=" + temp;//查询生成xml文件
                DataSet ds = new DataSet("Results");
                ds.ReadXml(premises_xml_url);
                if (ds.Tables.Count < 2)
                {
                    return null;
                }
                else
                {
                    DataRow PageRow = ds.Tables[0].Rows[0];
                    DataTable dt = ds.Tables[1];
                    if (dt.Rows.Count > 0)
                    {
                        List<IndexRanking> listform = new List<IndexRanking>();
                        TXCommons.Index.IndexRanking sf = new TXCommons.Index.IndexRanking();
                        //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            sf = new IndexRanking();
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //获取属性值需要指定属性名
                                PropertyInfo ProInfo = sf.GetType().GetProperty(dt.Columns[k].ColumnName);
                                ProInfo.SetValue(sf, Convert.ToString(dr[k]), null);
                            }
                            listform.Add(sf);
                        }

                        return listform;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("崔利国A", string.Format("({0},{1})", conditions, getdatapagename), e);
                throw;
            }
        }
        #endregion

    }
}
