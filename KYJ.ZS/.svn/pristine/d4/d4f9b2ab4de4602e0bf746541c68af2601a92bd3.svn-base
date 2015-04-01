using System;
using System.Collections.Generic;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.FreightTemplates;
using System.Text;

namespace KYJ.ZS.DAL.FreightTemplates
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-17
    /// Desc：操作运费模板(数据库表FreightTemplates)，包括查询、添加、修改
    /// </summary>
    public class FreightTemplateDal
    {
        /// <summary>
        /// 校验模板名称
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="name">运费模板名称</param>
        /// <returns></returns>
        public int CheckName(int merchantId, string name)
        {
            try
            {
                #region SQL
                var sql = @"SELECT  COUNT(1)
                        FROM    [dbo].[FreightTemplates](NOLOCK)
                        WHERE   [merchant_id]=@merchant_id and [ftemp_title] = @ftemp_title and [ftemp_isdel]='false'";
                #endregion
                var param = new SqlParam();
                param.AddParam("@merchant_id", merchantId);
                param.AddParam("@ftemp_title", name);

                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", merchantId + "," + name, ex);
                return 1;
            }
        }

        /// <summary>
        /// 获取模板原有名称
        /// </summary>
        /// <param name="ftempId">运费模板ID</param>
        /// <returns></returns>
        public string GetName(int ftempId)
        {
            try
            {
                var sql = @"SELECT [ftemp_title]
                        FROM    [dbo].[FreightTemplates](NOLOCK)
                        WHERE   [ftemp_id]=@ftemp_id";

                var param = new SqlParam();
                param.AddParam("@ftemp_id", ftempId);

                return KYJ_ZushouRDB.GetFirst(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ftempId, ex);
                return "";
            }
        }

        /// <summary>
        /// 添加、修改运费模板(返回执行结果0失败，1成功)
        /// </summary>
        /// <param name="freightEntity">运费模板</param>
        /// <returns></returns>
        public int CreateOrUpdate(FreightJsonEntity freightEntity)
        {
            try
            {
                #region 修改模板时判断模板是否属于当前商户
                if (!string.IsNullOrEmpty(freightEntity.Id))
                {
                    var sqlstr = "select merchant_id from FreightTemplates(NOLOCK) where ftemp_id=@ftemp_id";
                    var paramList = new SqlParam();
                    paramList.AddParam("@ftemp_id", freightEntity.Id);

                    if (freightEntity.MerchantId != Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sqlstr, paramList)))
                        return 0;
                }

                #endregion

                #region SQL及参数
                StringBuilder sql = new StringBuilder(); //sql
                SqlParam param = new SqlParam(); //参数

                // 事务开始
                sql.Append(@"begin tran
	                        declare @tempId int, --运费模板ID
			                @results int --执行结果，0失败，1成功");
                #region 添加运费模板
                if (string.IsNullOrEmpty(freightEntity.Id))
                {
                    sql.Append(@"/*运费模板信息添加  返回最新的模板ID*/
                            INSERT INTO [dbo].[FreightTemplates]
                               ([merchant_id]
                               ,[ftemp_provinceid]
                               ,[ftemp_provincename]
                               ,[ftemp_cityid]
                               ,[ftemp_cityname]
                               ,[ftemp_districtid]
                               ,[ftemp_districtname]
                               ,[ftemp_title]
                               ,[ftemp_isfreeship]
                               ,[ftemp_mode]
                               ,[ftemp_isexpress]
                               ,[ftemp_isems]
                               ,[ftemp_issnailmail]
                               ,[ftemp_islogistics]
                               ,[ftemp_createtime]
                               ,[ftemp_updatetime])
                         VALUES
                               (@merchant_id
                               ,@ftemp_provinceid
                               ,@ftemp_provincename
                               ,@ftemp_cityid
                               ,@ftemp_cityname
                               ,@ftemp_districtid
                               ,@ftemp_districtname
                               ,@ftemp_title
                               ,@ftemp_isfreeship
                               ,@ftemp_mode
                               ,@ftemp_isexpress
                               ,@ftemp_isems
                               ,@ftemp_issnailmail
                               ,@ftemp_islogistics
                               ,@ftemp_createtime
                               ,@ftemp_updatetime)
                         SELECT @tempId=@@IDENTITY");

                    param.AddParam("@merchant_id", freightEntity.MerchantId);
                    param.AddParam("@ftemp_createtime", DateTime.Now);
                }
                #endregion
                #region 修改运费模板
                else
                {
                    sql.Append(@"/*记录运费模板ID*/
                            set @tempId=@ftemp_id

                            /*删除运费模板所有的运费计算*/
                            DELETE FROM     [dbo].[FreightCosts]
                            WHERE   ftemp_id = @ftemp_id

                            /*修改运费模板信息*/
                            
                            UPDATE [dbo].[FreightTemplates]
                            SET    [ftemp_provinceid] = @ftemp_provinceid
                                   ,[ftemp_provincename] = @ftemp_provincename
                                   ,[ftemp_cityid] = @ftemp_cityid
                                   ,[ftemp_cityname] = @ftemp_cityname
                                   ,[ftemp_districtid] = @ftemp_districtid
                                   ,[ftemp_districtname] = @ftemp_districtname
                                   ,[ftemp_title] = @ftemp_title
                                   ,[ftemp_isfreeship] = @ftemp_isfreeship
                                   ,[ftemp_mode] = @ftemp_mode
                                   ,[ftemp_isexpress] = @ftemp_isexpress
                                   ,[ftemp_isems] = @ftemp_isems
                                   ,[ftemp_issnailmail] = @ftemp_issnailmail
                                   ,[ftemp_islogistics] = @ftemp_islogistics
                                   ,[ftemp_updatetime] = @ftemp_updatetime
                            WHERE  ftemp_id = @ftemp_id");

                    param.AddParam("@ftemp_id", freightEntity.Id);
                }
                #endregion
                #region 公有参数
                param.AddParam("@ftemp_provinceid", freightEntity.ProvinceId);
                param.AddParam("@ftemp_provincename", freightEntity.ProvinceName);
                param.AddParam("@ftemp_cityid", freightEntity.CityId);
                param.AddParam("@ftemp_cityname", freightEntity.CityName);
                param.AddParam("@ftemp_districtid", freightEntity.DistrictId);
                param.AddParam("@ftemp_districtname", freightEntity.DistrictName);
                param.AddParam("@ftemp_title", freightEntity.Title);
                param.AddParam("@ftemp_isfreeship", freightEntity.IsFreeship);
                param.AddParam("@ftemp_mode", freightEntity.Mode);
                param.AddParam("@ftemp_isexpress", freightEntity.Type == 1);
                param.AddParam("@ftemp_isems", freightEntity.Type == 2);
                param.AddParam("@ftemp_issnailmail", freightEntity.Type == 3);
                param.AddParam("@ftemp_islogistics", freightEntity.Type == 4);
                param.AddParam("@ftemp_updatetime", DateTime.Now);
                #endregion
                #region 添加运费计算
                foreach (var freightCost in freightEntity.CostList)
                {
                    sql.AppendFormat(@"/*添加运费计算*/
                                INSERT INTO [dbo].[FreightCosts]
                                   ([ftemp_id]
                                   ,[fcost_type]
                                   ,[fcost_cityids]
                                   ,[fcost_firstime]
                                   ,[fcost_firstprice]
                                   ,[fcost_continuetime]
                                   ,[fcost_continueprice])
                                VALUES
                                   (@tempId
                                   ,{0}
                                   ,'{1}'
                                   ,{2}
                                   ,{3}
                                   ,{4}
                                   ,{5})", freightEntity.Type, freightCost.CityIds, freightCost.FirstIme, freightCost.FirstPrice, freightCost.ContinuetIme, freightCost.ContinuePrice);
                }
                #endregion

                // 事务的提交、回滚
                sql.Append(@"/*判断事务回滚或提交*/
	                    if @@error<>0 --判断有任何一条出现错误
	                    begin 
		                    rollback tran --开始执行事务的回滚
		                    set @results=0  
	                    end
	                    else
	                    begin
		                    commit tran --执行这个事务的操作
		                    set @results=1 
	                    end
                        /*返回执行结果*/
                        select  @results");

                #endregion

                #region 执行

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql.ToString(), param));

                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取运费模板(old)
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public FreightTemplateEntity GetFreightTemplate(int freightTemplateID)
        {
            try
            {
                #region SQL
                var sql = @"SELECT 
                               [merchant_id]
                               ,[ftemp_provinceid]
                               ,[ftemp_provincename]
                               ,[ftemp_cityid]
                               ,[ftemp_cityname]
                               ,[ftemp_districtid]
                               ,[ftemp_districtname]
                               ,[ftemp_title]
                               ,[ftemp_isfreeship]
                               ,[ftemp_mode]
                               ,[ftemp_isexpress]
                               ,[ftemp_isems]
                               ,[ftemp_issnailmail]
                               ,[ftemp_islogistics]
                               ,[ftemp_createtime]
                               ,[ftemp_updatetime]
                               ,[fcost_id]
                               ,[fcost_type]
                               ,[fcost_cityids]
                               ,[fcost_firstime]
                               ,[fcost_firstprice]
                               ,[fcost_continuetime]
                               ,[fcost_continueprice]
                               ,[fcost_isdel]
                         FROM  [dbo].[FreightTemplates](NOLOCK) t1
                                join [dbo].[FreightCosts](NOLOCK) t2
                                on t1.[ftemp_id] = t2.ftemp_id
                         WHERE t1.ftemp_id = @ftemp_id and t1.ftemp_isdel = 'false' ";
                #endregion

                var param = new SqlParam();
                param.AddParam("@ftemp_id", freightTemplateID);

                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                #region DataTable转换实体

                FreightTemplateEntity entity = new FreightTemplateEntity();

                FreightTemplate freightTemplate = new FreightTemplate();
                freightTemplate.Id = freightTemplateID;
                freightTemplate.MerchantId = Auxiliary.ToInt32(dt.Rows[0]["merchant_id"]);
                freightTemplate.ProvinceId = Auxiliary.ToInt32(dt.Rows[0]["ftemp_provinceid"]);
                freightTemplate.ProvinceName = dt.Rows[0]["ftemp_provincename"].ToString();
                freightTemplate.CityId = Auxiliary.ToInt32(dt.Rows[0]["ftemp_cityid"]);
                freightTemplate.CityName = dt.Rows[0]["ftemp_cityname"].ToString();
                freightTemplate.DistrictId = Auxiliary.ToInt32(dt.Rows[0]["ftemp_districtid"]);
                freightTemplate.DistrictName = dt.Rows[0]["ftemp_districtname"].ToString();
                freightTemplate.Title = dt.Rows[0]["ftemp_title"].ToString();
                freightTemplate.IsFreeship = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_isfreeship"]);
                freightTemplate.Mode = Auxiliary.ToInt32(dt.Rows[0]["ftemp_mode"]);
                freightTemplate.IsExpress = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_isexpress"]);
                freightTemplate.IsEms = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_isems"]);
                freightTemplate.IsSnailmail = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_issnailmail"]);
                freightTemplate.IsLogistics = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_islogistics"]);
                freightTemplate.CreateTime = Convert.ToDateTime(dt.Rows[0]["ftemp_createtime"]);
                freightTemplate.UpdateTime = Convert.ToDateTime(dt.Rows[0]["ftemp_updatetime"]);

                //运费模板
                entity.FreightTemplate = freightTemplate;

                List<FreightCost> costList = new List<FreightCost>();
                foreach (DataRow row in dt.Rows)
                {
                    FreightCost freightCost = new FreightCost();
                    freightCost.Id = Auxiliary.ToInt32(row["fcost_id"]);
                    freightCost.TempId = freightTemplateID;
                    freightCost.Type = Auxiliary.ToInt32(row["fcost_type"]);
                    freightCost.CityIds = row["fcost_cityids"].ToString();
                    freightCost.FirstIme = Convert.ToDecimal(row["fcost_firstime"]);
                    freightCost.FirstPrice = Convert.ToDecimal(row["fcost_firstprice"]);
                    freightCost.ContinuetIme = Convert.ToDecimal(row["fcost_continuetime"]);
                    freightCost.ContinuePrice = Convert.ToDecimal(row["fcost_continueprice"]);
                    freightCost.IsDel = Auxiliary.ToBoolen(row["fcost_isdel"]);

                    costList.Add(freightCost);
                }

                //运费计算
                entity.FreightCostList = costList;

                #endregion

                return entity;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", freightTemplateID, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取运费模板(new)
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public FreightTemplateEntity GetFreightTemplate(int merchantId, int freightTemplateID)
        {
            try
            {
                #region 判断模板是否属于当前商户
                var sqlstr = "select merchant_id from FreightTemplates(NOLOCK) where ftemp_id=@ftemp_id";
                var paramList = new SqlParam();
                paramList.AddParam("@ftemp_id", freightTemplateID);

                if (merchantId != Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sqlstr, paramList)))
                    return null;
                #endregion

                #region SQL
                var sql = @"SELECT 
                               [merchant_id]
                               ,[ftemp_provinceid]
                               ,[ftemp_provincename]
                               ,[ftemp_cityid]
                               ,[ftemp_cityname]
                               ,[ftemp_districtid]
                               ,[ftemp_districtname]
                               ,[ftemp_title]
                               ,[ftemp_isfreeship]
                               ,[ftemp_mode]
                               ,[ftemp_isexpress]
                               ,[ftemp_isems]
                               ,[ftemp_issnailmail]
                               ,[ftemp_islogistics]
                               ,[ftemp_createtime]
                               ,[ftemp_updatetime]
                               ,[fcost_id]
                               ,[fcost_type]
                               ,[fcost_cityids]
                               ,[fcost_firstime]
                               ,[fcost_firstprice]
                               ,[fcost_continuetime]
                               ,[fcost_continueprice]
                               ,[fcost_isdel]
                         FROM  [dbo].[FreightTemplates](NOLOCK) t1
                                join [dbo].[FreightCosts](NOLOCK) t2
                                on t1.[ftemp_id] = t2.ftemp_id
                         WHERE t1.ftemp_id = @ftemp_id and t1.ftemp_isdel = 'false' ";
                #endregion

                var param = new SqlParam();
                param.AddParam("@ftemp_id", freightTemplateID);

                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                #region DataTable转换实体

                FreightTemplateEntity entity = new FreightTemplateEntity();

                FreightTemplate freightTemplate = new FreightTemplate();
                freightTemplate.Id = freightTemplateID;
                freightTemplate.MerchantId = Auxiliary.ToInt32(dt.Rows[0]["merchant_id"]);
                freightTemplate.ProvinceId = Auxiliary.ToInt32(dt.Rows[0]["ftemp_provinceid"]);
                freightTemplate.ProvinceName = dt.Rows[0]["ftemp_provincename"].ToString();
                freightTemplate.CityId = Auxiliary.ToInt32(dt.Rows[0]["ftemp_cityid"]);
                freightTemplate.CityName = dt.Rows[0]["ftemp_cityname"].ToString();
                freightTemplate.DistrictId = Auxiliary.ToInt32(dt.Rows[0]["ftemp_districtid"]);
                freightTemplate.DistrictName = dt.Rows[0]["ftemp_districtname"].ToString();
                freightTemplate.Title = dt.Rows[0]["ftemp_title"].ToString();
                freightTemplate.IsFreeship = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_isfreeship"]);
                freightTemplate.Mode = Auxiliary.ToInt32(dt.Rows[0]["ftemp_mode"]);
                freightTemplate.IsExpress = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_isexpress"]);
                freightTemplate.IsEms = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_isems"]);
                freightTemplate.IsSnailmail = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_issnailmail"]);
                freightTemplate.IsLogistics = Auxiliary.ToBoolen(dt.Rows[0]["ftemp_islogistics"]);
                freightTemplate.CreateTime = Convert.ToDateTime(dt.Rows[0]["ftemp_createtime"]);
                freightTemplate.UpdateTime = Convert.ToDateTime(dt.Rows[0]["ftemp_updatetime"]);

                //运费模板
                entity.FreightTemplate = freightTemplate;

                List<FreightCost> costList = new List<FreightCost>();
                foreach (DataRow row in dt.Rows)
                {
                    FreightCost freightCost = new FreightCost();
                    freightCost.Id = Auxiliary.ToInt32(row["fcost_id"]);
                    freightCost.TempId = freightTemplateID;
                    freightCost.Type = Auxiliary.ToInt32(row["fcost_type"]);
                    freightCost.CityIds = row["fcost_cityids"].ToString();
                    freightCost.FirstIme = Convert.ToDecimal(row["fcost_firstime"]);
                    freightCost.FirstPrice = Convert.ToDecimal(row["fcost_firstprice"]);
                    freightCost.ContinuetIme = Convert.ToDecimal(row["fcost_continuetime"]);
                    freightCost.ContinuePrice = Convert.ToDecimal(row["fcost_continueprice"]);
                    freightCost.IsDel = Auxiliary.ToBoolen(row["fcost_isdel"]);

                    costList.Add(freightCost);
                }

                //运费计算
                entity.FreightCostList = costList;

                #endregion

                return entity;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", freightTemplateID, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取运费模板下拉列表
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        public IList<FreightSelectEntity> GetFreightTemplateList(int merchantId)
        {
            try
            {
                var sql = @"SELECT 
                                [ftemp_id] as Id
                                ,[ftemp_title] as Name
                        FROM    [dbo].[FreightTemplates](NOLOCK)
                        WHERE   merchant_id = @merchant_id and ftemp_isdel = 'false'";

                var param = new SqlParam();
                param.AddParam("@merchant_id", merchantId);

                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<FreightSelectEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", merchantId, ex);
                return null;
            }
        }


        /// <summary>
        /// 获取运费模板分页列表
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<FreightIndexEntity> GetFreightTemplateList(int merchantId, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            try
            {
                totalRecord = 0;
                totalPage = 0;

                #region 分页中的运费模板ID
                string tempIds = string.Empty;

                //表名
                string tableName = "FreightTemplates(NOLOCK)";
                //查询条件
                string where = " ftemp_isdel = 'false' and merchant_id=" + merchantId;
                //排序
                string orderBy = " ftemp_updatetime desc";
                //列名
                string columnList = "[ftemp_id]";

                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        tempIds += row["ftemp_id"].ToString() + ",";
                    }
                    tempIds = tempIds.Substring(0, tempIds.Length - 1);
                }

                #endregion

                #region 分页运费模板数据

                #region SQL及参数
                var sql = @"SELECT t1.[ftemp_id]
                                ,[ftemp_title]
                                ,[ftemp_mode]
                                ,[ftemp_cityname]
                                ,[ftemp_updatetime]
                                ,[fcost_id]
                                ,[fcost_type]
                                ,[ftemp_isfreeship]
                                ,[ftemp_isexpress]
                                ,[ftemp_isems]
                                ,[ftemp_issnailmail]
                                ,[ftemp_islogistics]
                                ,[fcost_cityids]
                                ,[fcost_firstime]
                                ,[fcost_firstprice]
                                ,[fcost_continuetime]
                                ,[fcost_continueprice]
                        FROM    FreightTemplates(NOLOCK) as t1 
                                join FreightCosts(NOLOCK) as t2 
                                on t1.ftemp_id = t2.ftemp_id
                        WHERE   t1.ftemp_isdel = 'false' 
                                and t1.merchant_id=@merchant_id
                                and t1.ftemp_id in (" + tempIds + @")
                        ORDER BY ftemp_updatetime desc";

                var param = new SqlParam();
                param.AddParam("@merchant_id", merchantId); //商户ID
                //param.AddParam("@ftemp_id", tempIds); //运费模板ID
                #endregion

                dt = KYJ_ZushouRDB.GetTable(sql, param);

                #region 返回值
                if (!Auxiliary.CheckDt(dt))
                    return null;

                List<FreightIndexEntity> entityList = new List<FreightIndexEntity>();
                FreightIndexEntity entity;

                foreach (DataRow row in dt.Rows)
                {
                    // 运费模板Id
                    int freightTemplateId = Auxiliary.ToInt32(row["ftemp_id"]);
                    #region 运费计算
                    FreightCostIndexEntity freightCost = new FreightCostIndexEntity();
                    freightCost.CostId = Auxiliary.ToInt32(row["fcost_id"]);
                    freightCost.Type = Auxiliary.ToInt32(row["fcost_type"]);
                    freightCost.CityIds = row["fcost_cityids"].ToString();
                    freightCost.FirstIme = Convert.ToDecimal(row["fcost_firstime"]);
                    freightCost.FirstPrice = Convert.ToDecimal(row["fcost_firstprice"]);
                    freightCost.ContinuetIme = Convert.ToDecimal(row["fcost_continuetime"]);
                    freightCost.ContinuePrice = Convert.ToDecimal(row["fcost_continueprice"]);
                    #endregion

                    #region FreightTemplateEntity构建

                    // 查找集合中是否存在该运费模板
                    entity = entityList.Find(c => c.Id == freightTemplateId);

                    if (entity == null)
                    {
                        entity = new FreightIndexEntity();

                        entity.Id = freightTemplateId;
                        entity.Title = row["ftemp_title"].ToString();
                        entity.CityName = row["ftemp_cityname"].ToString();
                        entity.Mode = Auxiliary.ToInt32(row["ftemp_mode"]);
                        entity.UpdateTime = Convert.ToDateTime(row["ftemp_updatetime"]);
                        entity.IsFreeship = Auxiliary.ToBoolen(row["ftemp_isfreeship"]);
                        entity.IsExpress = Auxiliary.ToBoolen(row["ftemp_isexpress"]);
                        entity.IsEms = Auxiliary.ToBoolen(row["ftemp_isems"]);
                        entity.IsLogistics = Auxiliary.ToBoolen(row["ftemp_islogistics"]);
                        entity.IsSnailmail = Auxiliary.ToBoolen(row["ftemp_issnailmail"]);

                        // 运费计算集合
                        entity.FreightCostList = new List<FreightCostIndexEntity>();
                        entity.FreightCostList.Add(freightCost);

                        entityList.Add(entity);
                    }
                    else
                    {
                        // 该对象的集合下标
                        int searchIndex = entityList.FindIndex(c => c.Id == entity.Id);

                        entity.FreightCostList.Add(freightCost);

                        // 替换集合中的对象
                        entityList.RemoveAt(searchIndex);
                        entityList.Insert(searchIndex, entity);
                    }
                    #endregion
                }
                return entityList;

                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalPage = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", merchantId + "," + index + "," + pageSize + "," + totalRecord + "," + totalPage, ex);
                return null;
            }
        }

        /// <summary>
        /// 删除运费模板
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public int Delete(int freightTemplateID)
        {
            try
            {
                var sql = @"UPDATE  [dbo].[FreightTemplates]
                        SET     ftemp_isdel='true'
                        WHERE   ftemp_id = @ftemp_id";

                var param = new SqlParam();
                param.AddParam("@ftemp_id", freightTemplateID);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", freightTemplateID, ex);
                return 0;
            }
        }

        /// <summary>
        /// 是否包邮
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public bool IsFreeship(int freightTemplateID)
        {
            #region SQL
            var sql = @"SELECT  [ftemp_isfreeship]
                        FROM    [dbo].[FreightTemplates](NOLOCK)
                        WHERE   [ftemp_id] = @ftemp_id";
            #endregion
            var param = new SqlParam();
            param.AddParam("@ftemp_id", freightTemplateID);

            return Auxiliary.ToBoolen(KYJ_ZushouRDB.GetFirst(sql, param));
        }

        /// <summary>
        /// 获取计价方式(0 未知，1 按件数，2 按重量，3 按体积)
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns>0 未知，1 按件数，2 按重量，3 按体积</returns>
        public int GetPriceMode(int freightTemplateID)
        {
            #region SQL
            var sql = @"SELECT  [ftemp_mode]
                        FROM    [dbo].[FreightTemplates](NOLOCK)
                        WHERE   [ftemp_id] = @ftemp_id";
            #endregion
            var param = new SqlParam();
            param.AddParam("@ftemp_id", freightTemplateID);

            return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
        }

        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：Web前端租商品详情页,运费模版列表
        /// </summary>
        /// <param name="ftemp_id">运费模版Id</param>
        /// <returns>运费模版列表</returns>
        public List<Web_FreightTemplateEntity> Web_GetFreightTemplateList(int ftemp_id)
        {
            try
            {
                #region sql
                var sql = @"SELECT ft.[ftemp_id] as Id --运费模版Id
                              ,ft.[ftemp_cityid] as CityId --城市Id
                              ,ft.[ftemp_cityname] as CityName --城市名称
                              ,ft.[ftemp_isfreeship] as IsFreeship --是否包邮
                              ,ft.[ftemp_mode] as Mode       --计价方式
                              ,fc.[fcost_cityids] as CityIds    --运费计算城市
                              ,fc.[fcost_firstime] as FirsTime  --首次
                              ,fc.[fcost_firstprice] as FirstPrice --首次费用
                              ,fc.[fcost_continuetime] as ContinueTime  --续次
                              ,fc.[fcost_continueprice] as ContinuePrice --续次费用
                        FROM [dbo].[FreightTemplates] ft with(nolock)
                        right join [dbo].[FreightCosts] fc with(nolock) on ft.ftemp_id=fc.ftemp_id
                        where ft.ftemp_id=@ftemp_id ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@ftemp_id", ftemp_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_FreightTemplateEntity>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "运费模版列表", ex);
                return null;
            }
        }
    }
}
