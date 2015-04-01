using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Collections;

namespace KYJ.ZS.DAL.Collections
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-4-18
    /// Desc:操作数据库表Collections，用户添加收藏、删除收藏方法类
    /// </summary>
    public class CollectionDal
    {
        #region 删除收藏方法
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-25
        /// Desc:删除收藏方法
        /// </summary>
        /// <param name="collId">收藏表ID</param>
        /// <returns></returns>
        public int DelCollection(int userId, string collId)
        {

            try
            {
                var sql = @"UPDATE [dbo].[Collections] SET coll_isdel='true'
                           WHERE user_id=" + userId + " and coll_id in(" + collId + ")";
                return KYJ_ZushouWDB.SqlExecute(sql);
            }
            catch (Exception ex)
            {

                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "删除收藏方法", ex);
            }
            return 0;
        }
        #endregion
        /// <summary>
        /// 用户后台收藏列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public List<CollectionEntity> GetCollectionGoodsesPage(int userId, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            totalRecord = 0;
            totalPage = 0;
            string tableName = "dbo.Collections(nolock) as c inner join dbo.RentalGoodses(nolock) as r on c.goods_id=r.rental_id";
            string where = "c.coll_isdel='false' and c.[user_id]=" + userId;
            string orderBy = "c.coll_time desc";
            string columnList = "c.coll_id,c.[user_id],c.goods_id,c.coll_time,c.coll_isdel,c.coll_goodstype,r.rental_number,r.rental_brandname,r.rental_title,r.rental_price,r.rental_monthprice,r.rental_guid";
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);
                if (Auxiliary.CheckDt(dt))
                {
                    List<CollectionEntity> list = new List<CollectionEntity>();
                    foreach (DataRow item in dt.Rows)
                    {
                        CollectionEntity coll = new CollectionEntity();
                        coll.CollId = Auxiliary.ToInt32(item["coll_id"]);
                        coll.Time = Convert.ToDateTime(item["coll_time"]);
                        coll.GoodsId = Auxiliary.ToInt32(item["goods_id"]);
                        coll.GoodsType = Auxiliary.ToInt32(item["coll_goodstype"]);
                        coll.Title = item["rental_title"].ToString();
                        coll.Guid = item["rental_guid"].ToString();
                        coll.MonthPrice = Convert.ToDecimal(item["rental_monthprice"]);
                        coll.Price = Convert.ToDecimal(item["rental_price"]);
                        coll.Number = Auxiliary.ToInt32(item["rental_number"]);
                        list.Add(coll);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {

                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "用户后台收藏列表", ex);
            }

            return null;
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：商品添加收藏
        /// </summary>
        /// <param name="user_id">用户Id</param>
        /// <param name="goods_id">商品Id</param>
        /// <param name="goods_url">商品类型</param>
        /// <returns>返回执行结果0失败，1成功，2已存在</returns>
        public int Web_AddCollection(int user_id, int goods_id, int goods_type)
        {
            try
            {
                #region sql
                var sql = @"
                        declare @userid int,        @goodsid int,       @goodstype int
                        select  @userid=@user_id,   @goodsid=@goods_id, @goodstype=@goods_type
                        declare @results int  --执行结果，0失败，1成功，2已存在,3商品不正确
                        set @results=0
                        if not exists(select 1 from dbo.RentalGoodses with(nolock)
                              where rental_id=@goods_id
                           )
                        begin
                          set @results=3 --商品不正确
                        end
                        else if exists (select 1 from dbo.Collections with(nolock) 
                                   where [user_id]=@userid and goods_id=@goodsid  and coll_goodstype=@goodstype
                                )
                        begin
                          set @results=2 --已存在
                        end
                        else
                        begin
                            insert dbo.Collections ([user_id] ,[goods_id] ,[coll_time] ,[coll_isdel] ,[coll_goodstype])
                            values (@userid ,@goodsid ,getdate(),0,@goodstype)
                            if @@error=0 --判断有任何一条出现错误
                            begin 
                               set @results=1 --成功
                            end
                        end
                        /*返回执行结果*/
                        select  @results as Id
            ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@user_id", user_id, SqlDbType.Int, 4);
                param.AddParam("@goods_id", goods_id, SqlDbType.Int, 4);
                param.AddParam("@goods_type", goods_type, SqlDbType.Int, 4);
                #endregion
                #region 返回
                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "商品添加收藏", ex);
                return 0;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：商品收藏数
        /// </summary>
        /// <param name="goods_id">商品Id</param>
        /// <returns>收藏数量</returns>
        public int Web_CollectionCount(int goods_id)
        {
            try
            {
                #region sql
                var sql = @"
                        select  count(1) as CollectionNum
                        from  dbo.Collections  with(nolock)
                        where goods_id=@goods_id and 
                              coll_isdel=0 and 
                              coll_goodstype=1
                        ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@goods_id", goods_id, SqlDbType.Int, 4);
                #endregion
                #region 返回
                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "商品收藏数", ex);
                return 0;
            }
        }
    }
}
