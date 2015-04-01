using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.OrderGoodses;

namespace KYJ.ZS.DAL.OrderGoodses
{
    /// <summary>
    ///作用：交易管理
    ///作者：李晓波
    ///编写日期：2014.4.17
    ///修改时间：
    ///修改人：
    /// </summary>

    public class OrderGoodsDal
    {
        #region 获取租用商品数据列表(根据订单状态)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-17
        /// Desc:获取租用商品数据列表(根据订单状态)
        /// </summary>   
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageIndex">查询条件</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="TotalPage">总页数</param>
        /// <returns></returns>
        public List<RentalOrderEntity> GetListForState(int id, RentalOrderEntity mdlRentalOrder, int pageIndex, int pageSize, out int totalRecord, out int TotalPage)
        {
            #region 分页
            // 列名
            string columnList = " o.order_id,o.order_number,o.order_createtime,o.order_updatetime,g.ordergoods_title,ordergoods_monthprice,o.order_freightcost,ordergoods_num,o.user_nickname,o.order_totalprice,o.order_state";

            #region 查询条件及参数

            // 商户所属
            string where_ = " where o.oper_id=@oper_id and g.ordergoods_isdel='false'";
            var param = new SqlParam();
            param.AddParam("@oper_id", id);
            // 订单商品类型
            if (mdlRentalOrder.OrderType != null)
            {
                where_ += " and o.order_type=@order_type";
                param.AddParam("@order_type", mdlRentalOrder.OrderType);

            }
            //订单状态
            if (mdlRentalOrder.State != null)
            {
                //where_ += " and o.order_state=@order_state";
                //if (mdlRentalOrder.State == 9)//退租包括（退租申请、退租验收、退租结算）
                //{
                //    where_ += " or o.order_state=10 or o.order_state=11";
                //}
                //if (mdlRentalOrder.State == 12)
                //{
                //    where_ += " or o.order_state=13 or o.order_state=14";
                //}
                if (mdlRentalOrder.State == 012)//未付款区（付款失败、待付款）
                {
                    where_ += " and o.order_state=1 or o.order_state=2";
                }
                else if (mdlRentalOrder.State == 345)
                {
                    where_ += " and o.order_state=3 or o.order_state=4 or o.order_state=5";
                }
                else if (mdlRentalOrder.State == 678)
                {
                    where_ += " and o.order_state=6 or o.order_state=7 or o.order_state=8";
                }
                else if (mdlRentalOrder.State == 999)
                {
                    where_ += " and o.order_state= or o.order_state=7 or o.order_state=8";
                }

            }
            //商品名称
            if (!string.IsNullOrEmpty(mdlRentalOrder.Title))
            {
                where_ += " and g.ordergoods_title like'%" + mdlRentalOrder.Title + "%'";
                //where_ += " and g.ordergoods_title='@ordergoods_title'";
                //param.AddParam("@ordergoods_title", mdlRentalOrder.Title);
            }
            //商品编号
            if (!string.IsNullOrEmpty(mdlRentalOrder.Number))
            {
                where_ += " and o.order_number=@order_number";
                param.AddParam("@order_number", mdlRentalOrder.Number);
            }
            //用户昵称
            if (!string.IsNullOrEmpty(mdlRentalOrder.NickName))
            {
                where_ += " and o.user_nickname like '%" + mdlRentalOrder.NickName + "%'";
                //where_ += " and u.user_nickname =@user_nickname";
                //param.AddParam("@user_nickname", mdlRentalOrder.NickName);
            }
            #endregion
            #region SQL及执行
            // 近三个月（type=1）
            StringBuilder sql = new StringBuilder("SELECT " + columnList + ",type=1" +
                                                    @" FROM OrderGoodses g with(nolock) inner join  Orders o with(nolock) on g.order_id =o.order_id"
                                                    + where_);
            // 三个月以前（type=2）
            StringBuilder oldSql = new StringBuilder("SELECT " + columnList + ",type=2" +
                                                    @" FROM oldOrderGoodses g with(nolock) inner join  oldorders o with(nolock) on g.order_id =o.order_id"
                                                    + where_);
            var recordParam = param.Copy();
            // 记录总个数
            var sqlRecord = "SELECT COUNT(1) FROM (" + sql.ToString() + " UNION " + oldSql.ToString() + ") AS T";
            totalRecord = Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sqlRecord, recordParam));
            // 总页数
            TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize);
            var sqlstr = "SELECT * FROM(SELECT *,ROW_NUMBER() OVER(order by order_createtime desc) AS RowNum FROM (" + sql.ToString() + " UNION "
                    + oldSql.ToString() + ") AS T) AS T1 WHERE RowNum BETWEEN @StartRowNum AND @EndRowNum";
            // 开始的rownum
            int startRowNum = pageSize * (pageIndex - 1) + 1;
            param.AddParam("@StartRowNum", startRowNum);
            param.AddParam("@EndRowNum", startRowNum + pageSize - 1);

            DataTable dt = KYJ_ZushouRDB.GetTable(sqlstr, param);

            #endregion

            #endregion
            if (Auxiliary.CheckDt(dt))
            {
                List<RentalOrderEntity> list = new List<RentalOrderEntity>();
                foreach (DataRow item in dt.Rows)
                {
                    RentalOrderEntity goods = new RentalOrderEntity();
                    goods.Id = Auxiliary.ToInt32(item["order_id"]);
                    goods.Type = Auxiliary.ToInt32(item["type"]);
                    goods.Number = item["order_number"].ToString();
                    goods.UpdateTime = Convert.ToDateTime(item["order_updatetime"]);
                    goods.Title = item["ordergoods_title"].ToString();
                    goods.Monthprice = Convert.ToDecimal(item["ordergoods_monthprice"]);
                    goods.Freightcost = Convert.ToDecimal(item["order_freightcost"]);
                    goods.Num = Auxiliary.ToInt32(item["ordergoods_num"]);
                    goods.NickName = item["user_nickname"].ToString();
                    goods.Totalprice = Convert.ToDecimal(item["order_totalprice"]);
                    goods.State = Auxiliary.ToInt32(item["order_state"]);
                    list.Add(goods);
                }
                return list;
            }
            return null;

        }
        #endregion
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-17
        /// Desc:获取租用商品数据列表(根据日期)
        /// </summary>   
        /// </summary>       
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageIndex">查询条件</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="TotalPage">总页数</param>
        /// <returns></returns>
        #region 获取租用商品数据列表(根据订单日期)
        public List<RentalOrderEntity> GetList(int id, RentalOrderEntity mdlRentalOrder, int pageIndex, int pageSize, out int totalRecord, out int TotalPage)
        {
            string tableList;
            string colList;
            tableList = " OrderGoodses g inner join Orders o on g.order_id =o.order_id ";
            colList = "o.order_id,o.order_number,o.order_createtime,o.order_updatetime,g.ordergoods_title,ordergoods_monthprice,o.order_freightcost,ordergoods_num,o.user_nickname,o.order_totalprice,o.order_state";//显示列
            string where_ = " o.oper_id=" + id;

            if (mdlRentalOrder.OrderType != null)
            {
                if (mdlRentalOrder.OrderType == 1)
                {
                    where_ += " and o.order_type=1";
                }
                else if (mdlRentalOrder.OrderType == 2)
                {
                    where_ += " and o.order_type=2";
                }
                else
                {
                    where_ += " and o.order_type=0";
                }
            }
            //订单状态
            if (mdlRentalOrder.State != null)
            {
                if (mdlRentalOrder.State == 912)//未付款区（付款失败、待付款）
                {
                    where_ += " and o.order_state=1 or o.order_state=2";
                }
                else if (mdlRentalOrder.State == 345)
                {
                    where_ += " and o.order_state=3 or o.order_state=4 or o.order_state=5";
                }
                else if (mdlRentalOrder.State == 678)
                {
                    where_ += " and o.order_state=6 or o.order_state=7 or o.order_state=8";
                }
                else if (mdlRentalOrder.State == 999)
                {
                    where_ += " and o.order_state=9 or o.order_state=10 or o.order_state=12 or o.order_state=13 or o.order_state=15 or o.order_state=16  or o.order_state=22 or o.order_state=25 ";
                }
                else if (mdlRentalOrder.State == 141)
                {
                    where_ += " and o.order_state=11 or o.order_state=14 or o.order_state=21";
                }
                else if (mdlRentalOrder.State == 78)
                {
                    where_ += " and o.order_state=17 or o.order_state=18";
                }
                else if (mdlRentalOrder.State == 90)
                {
                    where_ += " and o.order_state=19 or o.order_state=20";
                }
                else
                {
                    where_ += " and o.order_state=" + mdlRentalOrder.State;
                }
            }
            if (!string.IsNullOrEmpty(mdlRentalOrder.Title))
            {
                where_ += " and g.ordergoods_title like'%" + mdlRentalOrder.Title + "%'";
            }
            if (!string.IsNullOrEmpty(mdlRentalOrder.Number))
            {
                where_ += " and o.order_number like'%" + mdlRentalOrder.Number + "%'";
            }
            if (!string.IsNullOrEmpty(mdlRentalOrder.NickName))
            {
                where_ += " and o.user_nickname like'%" + mdlRentalOrder.NickName + "%'";
            }
            DataTable dt = KYJ_ZushouRDB.GetPages(pageIndex, where_, "o.order_updatetime desc", colList, tableList, pageSize, true, out totalRecord, out TotalPage);
            if (Auxiliary.CheckDt(dt))
            {
                List<RentalOrderEntity> list = new List<RentalOrderEntity>();
                foreach (DataRow item in dt.Rows)
                {
                    RentalOrderEntity goods = new RentalOrderEntity();
                    goods.Id = Auxiliary.ToInt32(item["order_id"]);
                    goods.Number = item["order_number"].ToString();
                    goods.UpdateTime = Convert.ToDateTime(item["order_updatetime"]);
                    goods.Title = item["ordergoods_title"].ToString();
                    goods.Monthprice = Convert.ToDecimal(item["ordergoods_monthprice"]);
                    goods.Freightcost = Convert.ToDecimal(item["order_freightcost"]);
                    goods.Num = Auxiliary.ToInt32(item["ordergoods_num"]);
                    goods.NickName = item["user_nickname"].ToString();
                    goods.Totalprice = Convert.ToDecimal(item["order_totalprice"]);
                    goods.State = Auxiliary.ToInt32(item["order_state"]);
                    list.Add(goods);
                }
                return list;
            }
            return null;

        }
        #endregion


        /// <summary>
        /// Auther:李晓波
        /// Time:2014-5-8
        /// Desc:根据商户操作改变订单状态
        /// </summary>  
        /// <param name="tblType">订单所属表</param>
        /// <param name="operationType">订单操作类型</param>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        public int ChangeOrderStatus(int operationType, int orderId)
        {
            string sql;
            sql = @"update orders set order_state=@order_state where order_id=@order_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", operationType);
            param.AddParam("@order_id", orderId);
            return KYJ_ZushouWDB.SqlExecute(sql, param);

        }
        #region 用户后台获取购买订单分页
        //        /// <summary>
        //        /// Author:baiyan
        //        /// Time:2014-4-18
        //        /// Desc:用户后台获取购买订单分页
        //        /// </summary>
        //        /// <param name="index">当前页</param>
        //        /// <param name="pageSize">每页显示个数</param>
        //        /// <param name="totalRecord">总个数</param>
        //        /// <param name="totalPage">总页数</param>
        //        public List<OrderGoodsEntity> GetUserSaleOrdersPage(int index, int pageSize, out int totalRecord, out int totalPage)
        //        {
        //            #region SQL
        //            //表名
        //            string tableName = @"OrderGoodses(nolock) as og inner join Orders(nolock) as o on og.order_id=o.order_id inner join LocalUsers(nolock) as l on o.oper_id=l.[user_id]";
        //            totalRecord = 0;
        //            totalPage = 0;
        //            //查询条件
        //            var where = "1=1 and og.ordergoods_type=2 and og.ordergoods_isdel='false' ";
        //            //排序
        //            string orderBy = "og.ordergoods_id desc";
        //            //显示的列
        //            string columnList = "og.ordergoods_id,og.order_id,og.goods_id,og.ordergoods_title,og.ordergoods_guid,og.ordercolor_name,og.ordercolor_code,og.ordernorm_name,og.ordergoods_num,o.order_number,o.order_createtime,o.order_monthprice,o.order_state,o.order_totaldeposit,o.order_freightcost,o.oper_id,o.[user_id],l.user_nickname,l.user_realname";
        //            #endregion
        //            #region 执行
        //            DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);
        //            #endregion
        //            #region 返回值
        //            if (Auxiliary.CheckDt(dt))
        //            {
        //                List<OrderGoodsEntity> list = new List<OrderGoodsEntity>();
        //                foreach (DataRow item in dt.Rows)
        //                {
        //                    OrderGoodsEntity goods = new OrderGoodsEntity();
        //                    goods.Id = Auxiliary.ToInt32(item["ordergoods_id"]);
        //                    goods.GoodsId = Auxiliary.ToInt32(item["goods_id"]);
        //                    goods.Number = item["order_number"].ToString();
        //                    goods.CreateTime = Convert.ToDateTime(item["order_createtime"]);
        //                    goods.Title = item["ordergoods_title"].ToString();
        //                    goods.FreightCost = Convert.ToDecimal(item["order_freightcost"]);
        //                    goods.Guid = item["ordergoods_guid"].ToString();
        //                    goods.ColorName = item["ordercolor_name"].ToString();
        //                    goods.ColorCode = item["ordercolor_code"].ToString();
        //                    goods.NormName = item["ordernorm_name"].ToString();
        //                    goods.Monthprice = Convert.ToDecimal(item["order_monthprice"]);
        //                    goods.Num = Auxiliary.ToInt32(item["ordergoods_num"]);
        //                    goods.State = Auxiliary.ToInt32(item["order_state"]);
        //                    goods.NickName = item["user_nickname"].ToString();
        //                    goods.RealName = item["user_realname"].ToString();
        //                    goods.TotalDeposit = Convert.ToDecimal(item["order_totaldeposit"]);
        //                    list.Add(goods);
        //                }
        //                return list;
        //            }
        //            return null;
        //            #endregion
        //        }
        //        #endregion
        //        #region 用户后台获取购买订单详情页
        //        /// <summary>
        //        /// Author:baiyan
        //        /// Time:2014-4-18
        //        /// Desc:用户后台获取购买订单详情页
        //        /// </summary>
        //        public OrderGoodsEntity GetUserSaleOrdersInfo(int orderGoodsId)
        //        {
        //            #region SQL
        //            var sql = @"select
        //                    og.ordergoods_id,
        //                    og.order_id,
        //                    og.goods_id,
        //                    og.ordergoods_title,
        //                    og.ordergoods_guid,
        //                    og.ordergoods_num,
        //                    o.order_state,
        //                    o.order_number,
        //                    o.order_provincename,
        //                    o.order_cityname,
        //                    o.order_districtname,
        //                    o.order_address,
        //                    o.order_paytype,
        //                    o.order_invoicemode,
        //                    o.order_invoicerise,
        //                    o.order_invoicetitle,
        //                    o.order_invoicetype,
        //                    o.order_isdiscoupon,
        //                    o.order_disprice,
        //                    o.order_iscomment,
        //                    o.order_monthprice
        //                    o.order_freightcost,
        //                    o.order_expresscompany,
        //                    o.order_expressnum
        //                    from OrderGoodses(nolock) as og
        //                    inner join Orders(nolock) as o on og.order_id=o.order_id
        //                    where ordergoods_id=@ordergoods_id and ordergoods_isdel='false'";
        //            #endregion
        //            #region 参数
        //            SqlParam param = new SqlParam();
        //            param.AddParam("@ordergoods_id", orderGoodsId);
        //            #endregion
        //            #region 执行
        //            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
        //            #endregion
        //            #region 返回值
        //            if (Auxiliary.CheckDt(dt))
        //            {
        //                OrderGoodsEntity goods = new OrderGoodsEntity();
        //                var item = dt.Rows[0];
        //                goods.Id = Auxiliary.ToInt32(item["ordergoods_id"]);
        //                goods.OrderId = Auxiliary.ToInt32(item["order_id"]);
        //                goods.GoodsId = Auxiliary.ToInt32(item["goods_id"]);
        //                goods.Title = item["ordergoods_title"].ToString();
        //                goods.Guid = item["ordergoods_guid"].ToString();
        //                goods.Num = Auxiliary.ToInt32(item["ordergoods_num"]);
        //                goods.State = Auxiliary.ToInt32(item["order_state"]);
        //                goods.Number = item["order_number"].ToString();
        //                goods.ProvinceName = item["order_provincename"].ToString();
        //                goods.CityName = item["order_cityname"].ToString();
        //                goods.DistrictName = item["order_districtname"].ToString();
        //                goods.Address = item["order_address"].ToString();
        //                goods.PayType = Auxiliary.ToInt32(item["order_paytype"]);
        //                goods.InvoiceMode = Auxiliary.ToInt32(item["order_invoicemode"]);
        //                goods.InvoiceRise = Auxiliary.ToInt32(item["order_invoicerise"]);
        //                goods.InvoiceTitle = item["order_invoicetitle"].ToString();
        //                goods.InvoiceType = Auxiliary.ToInt32(item["order_invoicetype"]);
        //                goods.IsDisCoupon = Convert.ToBoolean(item["order_isdiscoupon"]);
        //                goods.DisPrice = Convert.ToDecimal(item["order_disprice"]);
        //                goods.IsComment = Convert.ToBoolean(item["order_iscomment"]);
        //                goods.Monthprice = Convert.ToDecimal(item["order_monthprice"]);
        //                goods.ExpressCompany = item["order_expresscompany"].ToString();
        //                goods.ExpressNum = item["order_expressnum"].ToString();
        //                goods.FreightCost = Convert.ToDecimal(item["order_freightcost"]);
        //                return goods;
        //            }
        //            return null;
        //            #endregion
        //        }
        #endregion
        #region 用户后台获取出租订单分页
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-18
        /// Desc:用户后台获取出租订单分页
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        public List<OrderGoodsEntity> GetUserRentOrdersPage(OrderGoodsEntity _where, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            #region SQL
            totalRecord = 0;
            totalPage = 0;
            //表名
            string tableName = @"OrderGoodses(nolock) as og inner join Orders(nolock) as o on og.order_id=o.order_id inner join Merchants(nolock) as m on o.oper_id=m.merchant_id inner join OrderOthers(nolock) as oo on o.order_id=oo.order_id";
            totalRecord = 0;
            totalPage = 0;
            //查询条件
            var where = "og.ordergoods_isdel='false' and og.ordergoods_type=1 and o.order_isdel='false' and o.order_type=1 and o.[user_id]=" + _where.UserId;
            //标题
            if (!string.IsNullOrEmpty(_where.OrderGoodsTitle))
            {
                where += " and og.ordergoods_title like '%" + Auxiliary.InputText(_where.OrderGoodsTitle, 200) + "%'";
            }
            //商家名称
            if (!string.IsNullOrEmpty(_where.MerchantName))
            {
                where += " and m.merchant_name like '%" + Auxiliary.InputText(_where.MerchantName, 50) + "%'";
            }
            //订单号
            if (!string.IsNullOrEmpty(_where.OrderNumber))
            {
                where += " and o.order_number like '%" + Auxiliary.InputText(_where.OrderNumber, 100) + "%'";
            }
            //时间
            if (_where.OrderCreateTime != null)
            {
                where += " and o.order_createtime>='" + _where.OrderCreateTime.Value.ToString("yyyy-MM-dd") + "'";
            }
            if (_where.OrderUpdateTime != null)
            {
                where += " and o.order_createtime<='" + _where.OrderUpdateTime.Value.ToString("yyyy-MM-dd") + "'";
            }
            //用户后台首页根据订单状态显示
            if (!string.IsNullOrEmpty(_where.UserIndexShowState))
            {
                where += " and o.order_state in(" + _where.UserIndexShowState + ")";
            }
            //排序
            string orderBy = "o.order_createtime desc";
            //列名
            string columnList = "og.ordergoods_id,og.goods_id,og.order_id,og.ordergoods_title,og.ordergoods_guid,og.ordergoods_num,og.ordergoods_deposit,og.ordergoods_monthprice,og.ordernorm_name,og.ordercolor_name,o.order_number,o.order_realname,o.order_freightcost,o.order_state,o.order_updatetime,m.merchant_name,oo.orderother_paidtime,oo.orderother_deliverytime,oo.orderother_harvesttime,oo.orderother_sendsgreementtime,oo.orderother_accruetime,oo.orderother_orderstate,oo.orderother_loseprice,oo.orderother_rejectreason,o.order_totalprice";
            #endregion
            #region 执行并返回值
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);
                if (Auxiliary.CheckDt(dt))
                {
                    List<OrderGoodsEntity> list = new List<OrderGoodsEntity>();
                    foreach (DataRow item in dt.Rows)
                    {
                        OrderGoodsEntity goods = new OrderGoodsEntity();
                        goods.ID = Auxiliary.ToInt32(item["ordergoods_id"]);
                        goods.GoodsId = Auxiliary.ToInt32(item["goods_id"]);
                        goods.OrderId = Auxiliary.ToInt32(item["order_id"]);
                        goods.OrderGoodsTitle = item["ordergoods_title"].ToString();
                        goods.OrderGoodsGuid = item["ordergoods_guid"].ToString();
                        goods.OrderColorName = item["ordercolor_name"].ToString();
                        goods.OrderNormName = item["ordernorm_name"].ToString();
                        goods.OrderGoodsNum = Auxiliary.ToInt32(item["ordergoods_num"]);
                        goods.OrderGoodsDeposit = Convert.ToDecimal(item["ordergoods_deposit"]);
                        goods.OrderGoodsMonthPrice = Convert.ToDecimal(item["ordergoods_monthprice"]);
                        goods.OrderNumber = item["order_number"].ToString();
                        goods.OrderRealName = item["order_realname"].ToString();
                        goods.OrderFreightCost = Convert.ToDecimal(item["order_freightcost"]);
                        goods.OrderState = Auxiliary.ToInt32(item["order_state"]);
                        goods.OrderTotalPrice = Auxiliary.ToDecimal(item["order_totalprice"].ToString());
                        goods.OrderUpdateTime = Convert.ToDateTime(item["order_updatetime"]);
                        goods.MerchantName = item["merchant_name"].ToString();
                        goods.OrderOtherPaidTime = item["orderother_paidtime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(item["orderother_paidtime"]);
                        goods.OrderOtherDeliveryTime = item["orderother_deliverytime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(item["orderother_deliverytime"]);
                        goods.OrderOtherHarvestTime = item["orderother_harvesttime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(item["orderother_harvesttime"]);
                        goods.OrderOtherSendsGreementTime = item["orderother_sendsgreementtime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(item["orderother_sendsgreementtime"]);
                        goods.OrderOtherAccrueTime = item["orderother_accruetime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(item["orderother_accruetime"]);
                        goods.OrderOtherOrderState = Auxiliary.ToInt32(item["orderother_orderstate"]);
                        goods.OrderOtherLosePrice = item["orderother_loseprice"] == DBNull.Value ? 0 : Convert.ToDecimal(item["orderother_loseprice"]);
                        goods.OrderOtherRejectreason = item["orderother_rejectreason"].ToString();
                        list.Add(goods);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "用户后台获取出租订单分页", ex);
            }
            
            return null;
            #endregion
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-18
        /// Desc:用户后台获取出租订单分页
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        //public List<OrderGoodsEntity> GetUserRentOrdersPage(OrderGoodsEntity _where, int index, int pageSize, out int totalRecord, out int totalPage)
        //{
        //    #region SQL
        //    totalRecord = 0;
        //    totalPage = 0;
        //    //查询条件
        //    var where = " where og.ordergoods_isdel='false' and og.ordergoods_type=1 and o.order_isdel='false' and o.order_type=1 and o.[user_id]=" + _where.UserId;
        //    if (!string.IsNullOrEmpty(_where.OrderGoodsTitle))
        //    {
        //        where += " and og.ordergoods_title like '%" + _where.OrderGoodsTitle + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(_where.MerchantName))
        //    {
        //        where += " and m.merchant_name like '%" + _where.MerchantName + "%'";
        //    }
        //    if (!string.IsNullOrEmpty(_where.OrderNumber))
        //    {
        //        where += " and o.order_number like '%" + _where.OrderNumber + "%'";
        //    }
        //    if (_where.OrderCreateTime != null)
        //    {
        //        where += " and o.order_createtime>='" + _where.OrderCreateTime.Value.ToString("yyyy-MM-dd") + "'";
        //    }
        //    if (_where.OrderUpdateTime != null)
        //    {
        //        where += " and o.order_createtime<='" + _where.OrderUpdateTime.Value.ToString("yyyy-MM-dd") + "'";
        //    }
        //    if (!string.IsNullOrEmpty(_where.UserIndexShowState))
        //    {
        //        where += " and o.order_state in(" + _where.UserIndexShowState + ")";
        //    }
        //    //排序
        //    string orderBy = "o.order_createtime";
        //    //列名
        //    string columnList = "og.ordergoods_id,og.goods_id,og.order_id,og.ordergoods_title,og.ordergoods_guid,og.ordergoods_num,og.ordergoods_deposit,og.ordergoods_monthprice,o.order_number,o.order_realname,o.order_freightcost,o.order_state,o.order_createtime,o.order_updatetime,m.merchant_name";
        //    // 近三个月（type=1）
        //    StringBuilder sql = new StringBuilder("SELECT " + columnList + ",type=1" +
        //                                            @" FROM OrderGoodses(nolock) as og inner join Orders(nolock) as o on og.order_id=o.order_id inner join Merchants(nolock) as m on o.oper_id=m.merchant_id "
        //                                            + where);
        //    // 三个月以前（type=2）
        //    StringBuilder oldSql = new StringBuilder("SELECT " + columnList + ",type=2" +
        //                                            @" FROM OldOrderGoodses(nolock) as og inner join OldOrders(nolock) as o on og.order_id=o.order_id inner join Merchants(nolock) as m on o.oper_id=m.merchant_id "
        //                                            + where);

        //    // 记录总个数
        //    var sqlRecord = "SELECT COUNT(1) FROM (" + sql.ToString() + " UNION ALL " + oldSql.ToString() + ") AS T";
        //    totalRecord = Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sqlRecord));

        //    // 总页数
        //    totalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize);

        //    var sqlstr = "SELECT * FROM(SELECT *,ROW_NUMBER() OVER(order by order_createtime) AS RowNum FROM (" + sql.ToString() + " UNION ALL " + oldSql.ToString() + ") AS T) AS T1 WHERE RowNum BETWEEN @StartRowNum AND @EndRowNum";
        //    // 开始的rownum
        //    SqlParam param = new SqlParam();
        //    int startRowNum = pageSize * (index - 1) + 1;
        //    param.AddParam("@StartRowNum", startRowNum);
        //    param.AddParam("@EndRowNum", startRowNum + pageSize - 1);
        //    DataTable dt = KYJ_ZushouRDB.GetTable(sqlstr, param);
        //    #endregion
        //    #region 返回值
        //    if (Auxiliary.CheckDt(dt))
        //    {
        //        List<OrderGoodsEntity> list = new List<OrderGoodsEntity>();
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            OrderGoodsEntity goods = new OrderGoodsEntity();
        //            goods.ID = Auxiliary.ToInt32(item["ordergoods_id"]);
        //            goods.GoodsId = Auxiliary.ToInt32(item["goods_id"]);
        //            goods.OrderId = Auxiliary.ToInt32(item["order_id"]);
        //            goods.OrderGoodsTitle = item["ordergoods_title"].ToString();
        //            goods.OrderGoodsGuid = item["ordergoods_guid"].ToString();
        //            goods.OrderGoodsNum = Auxiliary.ToInt32(item["ordergoods_num"]);
        //            goods.OrderGoodsDeposit = Convert.ToDecimal(item["ordergoods_deposit"]);
        //            goods.OrderGoodsMonthPrice = Convert.ToDecimal(item["ordergoods_monthprice"]);
        //            goods.OrderNumber = item["order_number"].ToString();
        //            goods.OrderRealName = item["order_realname"].ToString();
        //            goods.OrderFreightCost = Convert.ToDecimal(item["order_freightcost"]);
        //            goods.OrderState = Auxiliary.ToInt32(item["order_state"]);
        //            goods.OrderCreateTime = Convert.ToDateTime(item["order_createtime"]);
        //            goods.OrderUpdateTime = Convert.ToDateTime(item["order_updatetime"]);
        //            goods.MerchantName = item["merchant_name"].ToString();
        //            goods.Type = Auxiliary.ToInt32(item["type"]);
        //            list.Add(goods);
        //        }
        //        return list;
        //    }
        //    return null;
        //    #endregion
        //}
        #endregion
        #region 用户后台获取出租订单详情页
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-21
        /// 用户后台获取出租订单详情页
        /// </summary>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public OrderGoodsEntity GetUserRentOrdersInfo(int orderGoodsId, int userId)
        {
            #region SQL
            var sql = @"select 
                    og.ordergoods_id--订单商品ID
                    ,og.order_id--订单ID
                    ,og.goods_id--商品ID
                    ,og.ordergoods_type--商品类型
                    ,og.ordergoods_title--商品标题
                    ,og.ordergoods_guid--GUID
                    ,og.ordercolor_name--颜色名称
                    ,og.ordercolor_code--颜色代码
                    ,og.ordernorm_name--规格名称
                    ,og.ordergoods_num--数量
                    ,og.ordergoods_deposit--押金
                    ,og.ordergoods_monthprice--月租金
                    ,og.ordergoods_latefees--滞纳金
                    ,og.ordergoods_lowestmonth--最低租期
                    ,og.ordergoods_month--租期
                    ,og.ordergoods_discount--折扣
                    ,o.[user_id]--用户ID
                    ,o.[user_nickname]--用户昵称
                    ,o.oper_id--商品所属用户或商户的ID
                    ,o.order_type--商品类型
                    ,o.order_number--订单号
                    ,o.order_provinceid--省份ID
                    ,o.order_provincename--省份名称
                    ,o.order_cityid--城市ID
                    ,o.order_cityname--城市名称
                    ,o.order_districtid--区域ID
                    ,o.order_districtname--区域名称
                    ,o.order_address--收货地址
                    ,o.order_tel--收货人联系方式
                    ,o.order_restel--备用电话
                    ,o.order_code--邮编
                    ,o.order_realname--收货人姓名
                    ,o.order_expresscompany--物流公司名称
                    ,o.order_expressnum--物流单号
                    ,o.order_freightcost--订单运费
                    ,o.order_paytype--支付方式
                    ,o.order_state--订单状态
                    ,o.order_totalprice--订单总价格
                    ,o.order_totaldeposit--订单总押金
                    ,o.order_shippingmethod--送货方式
                    ,o.order_isphoneconfirm--是否电话确认
                    ,o.order_monthprice--每月总价格
                    ,o.order_invoicemode--发票方式
                    ,o.order_invoicerise--发票抬头
                    ,o.order_invoicetitle--发票抬头内容
                    ,o.order_invoicetype--发票内容
                    ,o.order_isdiscoupon--是否使用优惠券
                    ,o.order_disprice--优惠券金额
                    ,o.order_iscomment--是否评论
                    ,o.order_createtime--订单创建时间
                    ,o.order_updatetime--订单更新时间
                    ,m.merchant_name--商家名称
                    ,oo.orderother_paidtime--支付时间
                    ,oo.orderother_deliverytime--发货时间
                    ,oo.orderother_harvesttime--收获时间
                    ,oo.orderother_sendsgreementtime--发送起租协议时间
                    ,oo.orderother_accruetime--起租时间
                    ,oo.orderother_orderstate--返回订单状态
                    ,oo.orderother_loseprice--赔损金额
                    ,oo.orderother_rejectreason--驳回理由
                    from OrderGoodses(nolock) as og
                    inner join Orders(nolock) as o on og.order_id=o.order_id
                    inner join Merchants(nolock) as m on o.oper_id=m.merchant_id
                    inner join OrderOthers(nolock) as oo on o.order_id=oo.order_id
                    where og.ordergoods_isdel='false' and og.ordergoods_type=1 and o.order_isdel='false' and o.order_type=1 and og.ordergoods_id=@ordergoods_id and o.[user_id]=@user_id";
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@ordergoods_id", orderGoodsId);
            param.AddParam("@user_id", userId);
            #endregion
            #region 执行并返回值
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    OrderGoodsEntity order = new OrderGoodsEntity();
                    var item = dt.Rows[0];
                    order.ID = Auxiliary.ToInt32(item["ordergoods_id"]);
                    order.GoodsId = Auxiliary.ToInt32(item["goods_id"]);
                    order.OrderId = Auxiliary.ToInt32(item["order_id"]);
                    order.OrderGoodsType = Auxiliary.ToInt32(item["ordergoods_type"]);
                    order.OrderGoodsTitle = item["ordergoods_title"].ToString();
                    order.OrderGoodsGuid = item["ordergoods_guid"].ToString();
                    order.OrderColorName = item["ordercolor_name"].ToString();
                    order.OrderColorCode = item["ordercolor_code"].ToString();
                    order.OrderNormName = item["ordernorm_name"].ToString();
                    order.OrderGoodsNum = Auxiliary.ToInt32(item["ordergoods_num"]);
                    order.OrderGoodsDeposit = Convert.ToDecimal(item["ordergoods_deposit"]);
                    order.OrderGoodsMonthPrice = Convert.ToDecimal(item["ordergoods_monthprice"]);
                    order.OrderGoodsLateFees = Convert.ToDecimal(item["ordergoods_latefees"]);
                    order.OrderGoodsLowestMonth = Auxiliary.ToInt32(item["ordergoods_lowestmonth"]);
                    order.OrderGoodsMonth = Auxiliary.ToInt32(item["ordergoods_month"]);
                    order.OrderGoodsDisCount = Auxiliary.ToInt32(item["ordergoods_discount"]);
                    order.UserId = Auxiliary.ToInt32(item["user_id"]);
                    order.UserNickName = item["user_nickname"].ToString();
                    order.OperId = Auxiliary.ToInt32(item["oper_id"]);
                    order.OrderType = Auxiliary.ToInt32(item["order_type"]);
                    order.OrderNumber = item["order_number"].ToString();
                    order.OrderProvinceId = Auxiliary.ToInt32(item["order_provinceid"]);
                    order.OrderProvinceName = item["order_provincename"].ToString();
                    order.OrderCityId = Auxiliary.ToInt32(item["order_cityid"]);
                    order.OrderCityName = item["order_cityname"].ToString();
                    order.OrderDistrictId = Auxiliary.ToInt32(item["order_districtid"]);
                    order.OrderDistrictName = item["order_districtname"].ToString();
                    order.OrderAddress = item["order_address"].ToString();
                    order.OrderTel = item["order_tel"].ToString();
                    order.OrderResTel = item["order_restel"].ToString();
                    order.OrderCode = item["order_code"].ToString();
                    order.OrderRealName = item["order_realname"].ToString();
                    order.OrderExpressNum = item["order_expressnum"].ToString();
                    order.OrderExpressMompany = item["order_expresscompany"].ToString();
                    order.OrderFreightCost = Convert.ToDecimal(item["order_freightcost"]);
                    order.OrderPayType = Auxiliary.ToInt32(item["order_paytype"]);
                    order.OrderState = Auxiliary.ToInt32(item["order_state"]);
                    order.OrderTotalPrice = Convert.ToDecimal(item["order_totalprice"]);
                    order.OrderTotalDeposit = Convert.ToDecimal(item["order_totaldeposit"]);
                    order.OrderShipPingMethod = Auxiliary.ToInt32(item["order_shippingmethod"]);
                    order.OrderIsPhoneConfirm = Auxiliary.ToBoolen(item["order_isphoneconfirm"]);
                    order.OrderMonthPrice = Convert.ToDecimal(item["order_monthprice"]);
                    order.OrderInvoiceMode = Auxiliary.ToInt32(item["order_invoicemode"]);
                    order.OrderInvoiceRise = Auxiliary.ToInt32(item["order_invoicerise"]);
                    order.OrderInvoiceTitle = item["order_invoicetitle"].ToString();
                    order.OrderInvoiceType = Auxiliary.ToInt32(item["order_invoicetype"]);
                    order.OrderIsDisCoupon = Auxiliary.ToBoolen(item["order_isdiscoupon"]);
                    order.OrderDisPrice = Convert.ToDecimal(item["order_disprice"]);
                    order.OrderIsComment = Auxiliary.ToBoolen(item["order_iscomment"]);
                    order.OrderCreateTime = Convert.ToDateTime(item["order_createtime"]);
                    order.OrderUpdateTime = Convert.ToDateTime(item["order_updatetime"]);
                    order.MerchantName = item["merchant_name"].ToString();
                    order.OrderOtherPaidTime = item["orderother_paidtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_paidtime"]);
                    order.OrderOtherDeliveryTime = item["orderother_deliverytime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_deliverytime"]);
                    order.OrderOtherHarvestTime = item["orderother_harvesttime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_harvesttime"]);
                    order.OrderOtherSendsGreementTime = item["orderother_sendsgreementtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_sendsgreementtime"]);
                    order.OrderOtherAccrueTime = item["orderother_accruetime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_accruetime"]);
                    order.OrderOtherOrderState = Auxiliary.ToInt32(item["orderother_orderstate"]);
                    order.OrderOtherLosePrice = item["orderother_loseprice"] == DBNull.Value ? 0 : Convert.ToDecimal(item["orderother_loseprice"]);
                    order.OrderOtherRejectreason = item["orderother_rejectreason"].ToString();
                    return order;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "用户后台获取出租订单详情页", ex);
            }
           
            return null;
            #endregion
        }
        #endregion
        #region 缴费历史
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 缴费历史
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<GoodsTenanci> GetPaymentRecord(int userId, int orderGoodsId)
        {
            var sql = @"SELECT [tenancy_id]
                      ,gt.[ordergoods_id]
                      ,gt.[tenancy_price]
                      ,gt.[tenancy_monthtime]
                      ,gt.[tenancy_isdelivery]
                      ,gt.[tenancy_isfavorable]
                      ,gt.[tenancy_favorableprice]
                      ,gt.[tenancy_sort]
                      ,gt.[tenancy_createtime]
                      ,gt.[tenancy_updatetime]
                      ,gt.[tenancy_isdel]
                      ,og.ordergoods_id
                      ,o.[user_id]
                  FROM [dbo].[GoodsTenancies](nolock) as gt 
                  INNER JOIN dbo.OrderGoodses(nolock) as og ON gt.ordergoods_id=og.ordergoods_id
                  INNER JOIN dbo.Orders(nolock) as o ON og.order_id=o.order_id 
                  where o.[user_id]=@user_id and og.ordergoods_id=@ordergoods_id and gt.[tenancy_isdelivery]='true' and gt.[tenancy_isdel]='false'  ORDER BY tenancy_monthtime desc";

            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);
            param.AddParam("@ordergoods_id", orderGoodsId);
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    List<GoodsTenanci> list = new List<GoodsTenanci>();
                    foreach (DataRow item in dt.Rows)
                    {
                        GoodsTenanci tenanci = new GoodsTenanci();
                        tenanci.Id = Auxiliary.ToInt32(item["tenancy_id"]);
                        tenanci.OrderGoodsId = Auxiliary.ToInt32(item["ordergoods_id"]);
                        tenanci.Price = Convert.ToDecimal(item["tenancy_price"]);
                        tenanci.MonthTime = Convert.ToDateTime(item["tenancy_monthtime"]);
                        tenanci.IsDelivery = Auxiliary.ToBoolen(item["tenancy_isdelivery"]);
                        tenanci.IsFavorable = Auxiliary.ToBoolen(item["tenancy_isfavorable"]);
                        tenanci.FavorablePrice = Convert.ToDecimal(item["tenancy_favorableprice"]);
                        tenanci.Sort = Auxiliary.ToInt32(item["tenancy_sort"]);
                        tenanci.CreateTime = Convert.ToDateTime(item["tenancy_createtime"]);
                        tenanci.UpdateTime = Convert.ToDateTime(item["tenancy_updatetime"]);
                        list.Add(tenanci);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "缴费历史", ex);
            }
            
            return null;
        }
        #endregion
        #region 查询缴费单
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-28
        /// 查询缴费单
        /// </summary>
        /// <param name="tenancyId">缴费单ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public GoodsTenanci GetPayment(int tenancyId, int userId)
        {
            var sql = @"SELECT gt.[tenancy_id]
                      ,gt.[ordergoods_id]
                      ,gt.[tenancy_price]
                      ,gt.[tenancy_monthtime]
                      ,gt.[tenancy_isdelivery]
                      ,gt.[tenancy_isfavorable]
                      ,gt.[tenancy_favorableprice]
                      ,gt.[tenancy_sort]
                      ,gt.[tenancy_createtime]
                      ,gt.[tenancy_updatetime]
                      ,gt.[tenancy_isdel]
                FROM [dbo].[GoodsTenancies](nolock) as gt 
	                   inner join dbo.OrderGoodses(nolock) as og on gt.[ordergoods_id]=og.ordergoods_id
	                   inner join dbo.Orders(nolock) as o on og.order_id=o.order_id
                WHERE tenancy_id=@tenancy_id and tenancy_isdelivery='false' and tenancy_isdel='false' and o.[user_id]=@user_id";

            SqlParam param = new SqlParam();
            param.AddParam("@tenancy_id", tenancyId);
            param.AddParam("@user_id", userId);
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    var item = dt.Rows[0];
                    GoodsTenanci tenanci = new GoodsTenanci();
                    tenanci.Id = Auxiliary.ToInt32(item["tenancy_id"]);
                    tenanci.OrderGoodsId = Auxiliary.ToInt32(item["ordergoods_id"]);
                    tenanci.Price = Convert.ToDecimal(item["tenancy_price"]);
                    tenanci.MonthTime = Convert.ToDateTime(item["tenancy_monthtime"]);
                    tenanci.IsDelivery = Auxiliary.ToBoolen(item["tenancy_isdelivery"]);
                    tenanci.IsFavorable = Auxiliary.ToBoolen(item["tenancy_isfavorable"]);
                    tenanci.FavorablePrice = Convert.ToDecimal(item["tenancy_favorableprice"]);
                    tenanci.Sort = Auxiliary.ToInt32(item["tenancy_sort"]);
                    tenanci.CreateTime = Convert.ToDateTime(item["tenancy_createtime"]);
                    tenanci.UpdateTime = Convert.ToDateTime(item["tenancy_updatetime"]);
                    return tenanci;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "查询缴费单", ex);
            }
            
            return null;
        }
        #endregion
        #region 缴费
        /// <summary>
        /// 缴费
        /// Author:baiyan
        /// Time:2014-4-28
        /// </summary>
        /// <param name="tenanci">租期实体</param>
        /// <returns></returns>
        public int Payment(GoodsTenanci tenanci)
        {
            var sql = @"UPDATE [dbo].[GoodsTenancies]
                       SET [tenancy_isdelivery] = @tenancy_isdelivery
                          ,[tenancy_isfavorable] = @tenancy_isfavorable
                          ,[tenancy_favorableprice] = @tenancy_favorableprice
                          ,[tenancy_sort] = @tenancy_sort
                     WHERE tenancy_id=@tenancy_id";
            SqlParam param = new SqlParam();
            param.AddParam("@tenancy_isdelivery", tenanci.IsDelivery);
            param.AddParam("@tenancy_isfavorable", tenanci.IsFavorable);
            param.AddParam("@tenancy_favorableprice", tenanci.FavorablePrice);
            param.AddParam("@tenancy_sort", tenanci.Sort);
            param.AddParam("@tenancy_id", tenanci.Id);
            try
            {

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "缴费", ex);
            }
            return 0;
        }
        #endregion
        #region 查询买断商品的总余额
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-4
        /// 查询买断商品的总余额
        /// </summary>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public decimal GetBuyoutSum(int orderGoodsId)
        {
            var sql = @"select sum(tenancy_price) as tenancy_price
                        from GoodsTenancies 
                        where ordergoods_id=@ordergoods_id and tenancy_isdelivery='false' and tenancy_isdel='false'";
            SqlParam param = new SqlParam();
            param.AddParam("@ordergoods_id", orderGoodsId);
            try
            {

                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    return Convert.ToDecimal(dt.Rows[0]["tenancy_price"]);
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "查询买断商品的总余额", ex);
            }
            return 0;
        }
        #endregion
        #region 买断
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-4
        /// 买断
        /// </summary>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public int Buyout(int orderGoodsId, int userId)
        {
            var sql = @"UPDATE [dbo].GoodsTenancies SET [tenancy_isdelivery] = 'true'
                        from [dbo].OrderGoodses 
                        inner join [dbo].GoodsTenancies on [dbo].OrderGoodses.ordergoods_id=[dbo].GoodsTenancies.ordergoods_id
                        inner join [dbo].Orders on [dbo].OrderGoodses.order_id=[dbo].Orders.order_id
                        WHERE [dbo].GoodsTenancies.ordergoods_id=@ordergoods_id
                        and [dbo].GoodsTenancies.tenancy_isdelivery='false' 
                        and [dbo].GoodsTenancies.tenancy_isdel='false'
                        and [dbo].Orders.[user_id]=@user_id";
            SqlParam param = new SqlParam();
            param.AddParam("@ordergoods_id", orderGoodsId);
            param.AddParam("@user_id", userId);
            try
            {
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "买断", ex);
            }
            return 0;
        }
        #endregion
        #region 取消订单
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-28
        /// 取消订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public int CancelOrders(int userId, int orderId, int state)
        {
            //获得商品ID 和 商品价格ID
            OrderGoodsEntity entity = GetOrderGoodsIdAndPriceIdByOrderId(orderId);
            #region 价格扩展
            //商品价格扩展
            string strGoodsOtherPrices = DBNull.Value.ToString();
            //判断是否需要修改,价格扩展的数据
            if (entity.GoodsPriceId > 0)
            {
                //取价格扩展
                RentalGoodses.RentalGoodsDal rgDal = new RentalGoodses.RentalGoodsDal();
                var list = rgDal.Web_GetRentalOtherPrice(entity.GoodsId).Decompress().FromJson<List<Models.DB.GoodsPrice>>();
                //修改价格扩展的商品数量
                foreach (var model in list)
                {
                    if (entity.GoodsPriceId == model.Id)
                    {
                        model.Number++; //商品数量减一
                        break;
                    }
                }
                //最新的价格扩展,进行序列化和压缩
                strGoodsOtherPrices = list.ToJson().Compress();
            }
            #endregion
            var sql = @"
                        declare @results int  --返回结果 -1未知0失败2成功
                        set @results=-1
                        begin tran --开始执行事务

                        /*修改订单状态*/
                        UPDATE [dbo].[Orders]
                        SET [order_state] = @order_state,[order_updatetime]=@order_updatetime
                        WHERE [user_id]=@user_id and order_id=@order_id

                        /*修改商品数量*/
                        If(@goodspriceid>0) -- 有尺寸有颜色
                        Begin
                            --商品扩展表
                            Update [dbo].[RentalOthers] Set [other_prices]=@goodsotherprices
                            Where rental_id=@goodsid
                            --商品价格表
	                        Update [dbo].[GoodsPrices] Set [goodsprice_num] = [goodsprice_num]+@goodsnum
	                        Where goodsprice_id=@goodspriceid
                        End
                        Else --其它
                        Begin
                            --商品扩展表
                            Update [dbo].[RentalGoodses] set [rental_number]=[rental_number]+@goodsnum
                            Where rental_id=@goodsid
                        End


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
                        select  @results as Id
              ";
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", state);
            param.AddParam("@user_id", userId);
            param.AddParam("@order_id", orderId);
            param.AddParam("@order_updatetime", System.DateTime.Now);
            param.AddParam("@goodsid", entity.GoodsId);
            param.AddParam("@goodspriceid", entity.GoodsPriceId);
            param.AddParam("@goodsotherprices", strGoodsOtherPrices);
            param.AddParam("@goodsnum",entity.OrderGoodsNum);
            try
            {

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "取消订单", ex);
            }
            return 0;
        }
        #endregion
        #region 删除订单
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-23
        /// 删除订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public int DelOrders(int userId, int orderGoodsId)
        {
            var sql = @"begin transaction
                                --订单
                                update Orders set order_isdel='true',order_updatetime=@order_updatetime,order_state=19
                                from OrderGoodses 
                                inner join Orders on OrderGoodses.order_id=Orders.order_id 
                                where OrderGoodses.ordergoods_id=@ordergoods_id and OrderGoodses.ordergoods_isdel='false' and Orders.[user_id]=@user_id
                                IF @@ERROR<>0
                                BEGIN
                                      ROLLBACK TRANSACTION
            	                      SELECT  0
                                END
                                --订单商品
                                update OrderGoodses set ordergoods_isdel='true' 
                                from Orders inner join OrderGoodses on OrderGoodses.order_id=Orders.order_id
                                where OrderGoodses.ordergoods_id=@ordergoods_id and OrderGoodses.ordergoods_isdel='false' and Orders.order_isdel='true'
                                IF @@ERROR<>0
                                BEGIN
                                     ROLLBACK TRANSACTION
                                     SELECT  0
                                END
                                --租金表
                                update GoodsTenancies
                                set tenancy_isdel='true' 
                                from OrderGoodses
                                 inner join GoodsTenancies on OrderGoodses.ordergoods_id = GoodsTenancies.ordergoods_id 
                                 where OrderGoodses.ordergoods_id = @ordergoods_id and OrderGoodses.ordergoods_isdel='true'
                                 IF @@ERROR<>0
                                BEGIN
                                      ROLLBACK TRANSACTION
                                      SELECT  0
                                END
                                commit TRANSACTION";
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);
            param.AddParam("@ordergoods_id", orderGoodsId);
            param.AddParam("@order_updatetime", System.DateTime.Now);
            try
            {

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "删除订单", ex);
            }
            return 0;
        }
        #endregion
        #region 确认收货
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-28
        /// 确认收货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="state">要更改的状态ID</param>
        /// <returns></returns>
        public int ConfirmReceipt(int orderId, int userId, int state)
        {
            var sql = @"BEGIN TRANSACTION
                   UPDATE [dbo].[Orders]
                       SET [order_state] = @order_state,[order_updatetime]=@order_updatetime
                       WHERE [user_id]=@user_id and order_id=@order_id
                  IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                     UPDATE dbo.OrderOthers    
                     SET [orderother_harvesttime]=@orderother_harvesttime
                     WHERE order_id=@order_id                
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                COMMIT TRANSACTION";
            int UpOrderState = Auxiliary.ToInt32(GetUpOrderState(orderId).Rows[0]["orderother_orderstate"] == null ? 0 : GetUpOrderState(orderId).Rows[0]["orderother_orderstate"]);
            if (UpOrderState == 7)
            {
                state = UpOrderState;
                sql = @"BEGIN TRANSACTION
                   UPDATE [dbo].[Orders]
                       SET [order_state] = @order_state,[order_updatetime]=@order_updatetime
                       WHERE [user_id]=@user_id and order_id=@order_id
                  IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                     UPDATE dbo.OrderOthers    
                     SET [orderother_harvesttime]=@orderother_harvesttime
                     WHERE order_id=@order_id                
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                    update dbo.OrderReturns set orderreturn_endtime=@orderreturn_endtime where orderreturn_id=(select top 1 orderreturn_id from dbo.OrderReturns where order_id=@order_id and orderreturn_state=2 order by orderreturn_createtime desc)
                 IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                COMMIT TRANSACTION";
            }
            DateTime time = System.DateTime.Now;
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", state);
            param.AddParam("@order_id", orderId);
            param.AddParam("@user_id", userId);
            param.AddParam("@orderother_harvesttime", time);
            param.AddParam("@order_updatetime", time);
            param.AddParam("@orderreturn_endtime", time);
            try
            {

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "确认收货", ex);
            }
            return 0;
        }
        #endregion
        #region 扣除赔损金额
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 扣除赔损金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int ConfirmPayout(int orderId, int userId, int state)
        {

            var sql = @"select o.order_id,o.[user_id],o.order_totaldeposit,oo.orderother_loseprice from
                    dbo.Orders(nolock) as o 
                    inner join dbo.OrderOthers(nolock) as oo on o.order_id=oo.order_id
                    where o.order_id=@order_id and o.[user_id]=@user_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_id", orderId);
            param.AddParam("@user_id", userId);
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    if (Auxiliary.ToDecimal(dt.Rows[0]["order_totaldeposit"].ToString()) >= Auxiliary.ToDecimal(dt.Rows[0]["orderother_loseprice"].ToString()))
                    {
                        return ConfirmPayout(orderId, userId, state.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "扣除赔损金额", ex);
            }
           
            return 0;
        }
        private int ConfirmPayout(int orderId, int userId, string state)
        {
            var sql = @"update dbo.Orders set order_state=@order_state,order_updatetime=@order_updatetime, order_totaldeposit=(
                        select 
                        o.order_totaldeposit-oo.orderother_loseprice
                        from
                        dbo.Orders as o
                        inner join dbo.OrderOthers as oo on o.order_id=oo.order_id 
                        where o.order_id=@order_id) where order_id=@order_id and [user_id]=@user_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_id", orderId);
            param.AddParam("@user_id", userId);
            param.AddParam("@order_state", state);
            param.AddParam("@order_updatetime", System.DateTime.Now);
            try
            {
                return KYJ_ZushouRDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "扣除赔损金额", ex);
            }
            return 0;
        }
        #endregion
        #region 申请退货
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 申请退货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="state">要更改的状态</param>
        /// <returns></returns>
        public int ApplicationReturnOfGoods(int orderId, int userId, int state)
        {
            //原有的订单状态
            int OriginalState = GetOrderState(orderId, userId);
            var sql = @"BEGIN TRANSACTION
                    UPDATE [dbo].[Orders]
	                SET [order_state] = @order_state,[order_updatetime]=@order_updatetime
	                WHERE [user_id]=@user_id and order_id=@order_id
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                     UPDATE dbo.OrderOthers    
                     SET [orderother_orderstate]=@orderother_orderstate
                     WHERE order_id=@order_id                  
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                COMMIT TRANSACTION";
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", state);
            param.AddParam("@order_id", orderId);
            param.AddParam("@user_id", userId);
            param.AddParam("@orderother_orderstate", OriginalState);
            param.AddParam("@order_updatetime", System.DateTime.Now);
            try
            {

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "申请退货", ex);
            }
            return 0;
        }
        #endregion
        #region 申请换货
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-7
        /// 申请换货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public int ReturnsGoods(int orderId, int userId, int state)
        {
            //原有的订单状态
            int OriginalState = GetOrderState(orderId, userId);
            var sql = @"BEGIN TRANSACTION
                    UPDATE [dbo].[Orders]
	                SET [order_state] = @order_state,[order_updatetime]=@order_updatetime
	                WHERE [user_id]=@user_id and order_id=@order_id
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                     UPDATE dbo.OrderOthers    
                     SET [orderother_orderstate]=@orderother_orderstate
                     WHERE order_id=@order_id                  
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                COMMIT TRANSACTION";
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", state);
            param.AddParam("@order_id", orderId);
            param.AddParam("@user_id", userId);
            param.AddParam("@orderother_orderstate", OriginalState);
            param.AddParam("@order_updatetime", System.DateTime.Now);
            try
            {

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "申请换货", ex);
            }
            return 0;
        }
        #endregion
        #region 撤消订单返回上一级操作
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-21
        /// Desc:撤消订单返回上一级操作
        /// </summary>
        /// <returns></returns>
        public int CancelOrderOperating(int orderId, int userId)
        {
            DataTable UpOrderState = GetUpOrderState(orderId);
            var sql = @"UPDATE [Orders]
                       SET [order_state] = @order_state,[order_updatetime]=@order_updatetime
                       WHERE [user_id]=@user_id and order_isdel='false' and order_id=@order_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", Auxiliary.ToInt32(UpOrderState.Rows[0]["orderother_orderstate"]));
            param.AddParam("@user_id", userId);
            param.AddParam("@order_id", orderId);
            if (Auxiliary.ToInt32(UpOrderState.Rows[0]["orderother_orderstate"]) == 3)
            {
                param.AddParam("@order_updatetime", UpOrderState.Rows[0]["orderother_paidtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(UpOrderState.Rows[0]["orderother_paidtime"]));
            }
            else if (Auxiliary.ToInt32(UpOrderState.Rows[0]["orderother_orderstate"]) == 4)
            {
                param.AddParam("@order_updatetime", UpOrderState.Rows[0]["orderother_deliverytime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(UpOrderState.Rows[0]["orderother_deliverytime"]));
            }
            else if (Auxiliary.ToInt32(UpOrderState.Rows[0]["orderother_orderstate"]) == 5)
            {
                param.AddParam("@order_updatetime", UpOrderState.Rows[0]["orderother_harvesttime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(UpOrderState.Rows[0]["orderother_harvesttime"]));
            }
            else if (Auxiliary.ToInt32(UpOrderState.Rows[0]["orderother_orderstate"]) == 6)
            {
                param.AddParam("@order_updatetime", UpOrderState.Rows[0]["orderother_sendsgreementtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(UpOrderState.Rows[0]["orderother_sendsgreementtime"]));
            }
            else if (Auxiliary.ToInt32(UpOrderState.Rows[0]["orderother_orderstate"]) == 7)
            {
                param.AddParam("@order_updatetime", UpOrderState.Rows[0]["orderother_accruetime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(UpOrderState.Rows[0]["orderother_accruetime"]));
            }
            else if (Auxiliary.ToInt32(UpOrderState.Rows[0]["orderother_orderstate"]) == 8)
            {
                param.AddParam("@order_updatetime", UpOrderState.Rows[0]["orderother_renewtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(UpOrderState.Rows[0]["orderother_renewtime"]));
            }
            else
            {
                param.AddParam("@order_updatetime", System.DateTime.Now);
            }
            try
            {
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "撤消订单返回上一级操作", ex);
            }
            return 0;
        }
        #endregion
        #region 确认租用
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-21
        /// 确认租用
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public int ConfirmHire(int orderId, int userId, int state)
        {
            var sql = @"BEGIN TRANSACTION
                   UPDATE [dbo].[Orders]
                       SET [order_state] = @order_state,[order_updatetime]=@order_updatetime
                       WHERE [user_id]=@user_id and order_id=@order_id
                  IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                     UPDATE dbo.OrderOthers    
                     SET [orderother_accruetime]=@orderother_accruetime
                     WHERE order_id=@order_id                
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                    DECLARE @goods_id INT
                    SELECT @goods_id=[goods_id] FROM [dbo].[OrderGoodses] where order_id=@order_id
                    UPDATE [dbo].[RentalGoodses]
                       SET [rental_rentnum] = (SELECT ([rental_rentnum]+1) as [rental_rentnum]
                       FROM.[dbo].[RentalGoodses] where rental_id=@goods_id)
                     WHERE rental_id=@goods_id
                    COMMIT TRANSACTION";
            DateTime time = System.DateTime.Now;
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", state);
            param.AddParam("@order_id", orderId);
            param.AddParam("@user_id", userId);
            param.AddParam("@orderother_accruetime", time);
            param.AddParam("@order_updatetime", time);
            try
            {
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "确认租用", ex);
            }
            return 0;
        }
        #endregion
        #region 申请退租
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 申请退租
        ///  <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="state">状态</param>
        /// </summary>
        /// <returns></returns>
        public int ApplicationSurrender(int orderId, int userId, int state)
        {

            //原有的订单状态
            int OriginalState = GetOrderState(orderId, userId);
            var sql = @"BEGIN TRANSACTION
                    UPDATE [dbo].[Orders]
	                SET [order_state] = @order_state,[order_updatetime]=@order_updatetime
	                WHERE [user_id]=@user_id and order_id=@order_id
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                     UPDATE dbo.OrderOthers    
                     SET [orderother_orderstate]=@orderother_orderstate
                     WHERE order_id=@order_id                  
                IF @@ERROR<>0
                BEGIN
	                ROLLBACK TRANSACTION
	                SELECT  0
                END
                COMMIT TRANSACTION";
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", state);
            param.AddParam("@order_id", orderId);
            param.AddParam("@user_id", userId);
            param.AddParam("@orderother_orderstate", OriginalState);
            param.AddParam("@order_updatetime", System.DateTime.Now);
            try
            {

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "申请退租", ex);
            }
            return 0;
        }

        /// <summary>
        /// 从订单扩展表中根据订单id取得订单状态
        /// </summary>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        public int getOderState(int orderId)
        {
            string sql;
            sql = @"select orderother_orderstate from  orderothers where order_id=@order_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_id", orderId);
            try
            {

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "从订单扩展表中根据订单id取得订单状态", ex);
            }
            return 0;
        }
        /// <summary>
        /// 从订单扩展表中根据订单状态取得订单时间
        /// </summary>
        /// <param name="orderId">订单状态</param>
        /// <returns></returns>
        public DateTime getOderTime(int orderState)
        {
            string sql;
            sql = "select ";
            if (orderState == 3)
            {
                sql += " orderother_paidtime";
            }
            else if (orderState == 4)
            {
                sql += " orderother_deliverytime";
            }
            else if (orderState == 5)
            {
                sql += " orderother_harvesttime";
            }
            else if (orderState == 6)
            {
                sql += " orderother_sendsgreementtime";
            }
            else if (orderState == 7)
            {
                sql += " orderother_accruetime";
            }
            sql += " from OrderOthers where orderother_orderstate=@orderother_orderstate";
            SqlParam param = new SqlParam();
            param.AddParam("@orderother_orderstate", orderState);
            try
            {
                return Convert.ToDateTime(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "从订单扩展表中根据订单状态取得订单时间", ex);
            }
            return System.DateTime.Now;
        }
        /// <summary>
        /// 更新订单表的状态和时间
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderState"></param>
        /// <param name="orderTime"></param>
        public bool updateOrder(int orderId, int orderState, DateTime orderTime)
        {
            var sql = @"UPDATE [dbo].[Orders]
                       SET [order_state] = @order_state,order_updatetime=@order_updatetime
                       WHERE order_id=@order_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", orderState);
            param.AddParam("@order_updatetime", orderTime);
            param.AddParam("@order_id", orderId);
            try
            {
                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "更新订单表的状态和时间", ex);
            }
            return false;
        }

        public bool updateRes(int orderId, string res)
        {
            var sql = @"UPDATE [dbo].[OrderOthers]
                       SET [orderother_rejectreason] = @orderother_rejectreason
                       WHERE order_id=@order_id";
            SqlParam param = new SqlParam();
            param.AddParam("@orderother_rejectreason", res);
            param.AddParam("@order_id", orderId);
            try
            {

                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {

                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "updateRes方法", ex);
            }
            return false;
        }

        #endregion
        #region 获得订单状态
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-21
        /// DESC:获得订单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>订单状态</returns>
        public int GetOrderState(int orderId, int userId)
        {
            var sql = @"SELECT [order_id]
                      ,[order_state]
                      ,[order_isdel]
                  FROM [dbo].[Orders] WHERE order_id=@order_id and [user_id]=@user_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_id", orderId);
            param.AddParam("@user_id", userId);
            try
            {
                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetTable(sql, param).Rows[0]["order_state"]);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "获得订单状态", ex);
            }
            return 0;
        }
        #endregion
        #region 获得上一级订单状态
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-21
        /// DESC:获得上一级订单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns>订单扩展表DataTable</returns>
        public DataTable GetUpOrderState(int orderId)
        {
            var sql = @"SELECT [order_id]
                      ,[orderother_paidtime]
                      ,[orderother_deliverytime]
                      ,[orderother_harvesttime]
                      ,[orderother_sendsgreementtime]
                      ,[orderother_accruetime]
                      ,[orderother_orderstate]
                      ,[orderother_renewtime]
                      FROM [dbo].[OrderOthers] WHERE [order_id]=@order_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_id", orderId);
            try
            {

                return KYJ_ZushouRDB.GetTable(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "获得上一级订单状态", ex);
            }
            return null;
        }
        #endregion
        #region 获得下次要交的租期信息
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-26
        /// 获得下次要交的租期信息
        /// </summary>
        /// <param name="OrderGoodsId"></param>
        /// <returns></returns>
        public GoodsTenanci GetNotPayment(int OrderGoodsId)
        {
            //获得所有租期中OrderGoodsId==OrderGoodsId的信息
            DataTable goodsTenancies = GetGoodsTenanciesCountByOrderGoodsId(OrderGoodsId);
            //判断租期表中是否有没交的租金
            DataRow[] row = goodsTenancies.Select("tenancy_isdelivery='false'", "tenancy_monthtime asc");
            //如果大于0说明有没交的租金
            if (row.Count() > 0)
            {
                GoodsTenanci tenanci = new GoodsTenanci();
                tenanci.Id = row[0]["tenancy_id"] == DBNull.Value ? 0 : Auxiliary.ToInt32(row[0]["tenancy_id"]);
                tenanci.OrderGoodsId = row[0]["ordergoods_id"] == DBNull.Value ? 0 : Auxiliary.ToInt32(row[0]["ordergoods_id"]);
                tenanci.Price = row[0]["tenancy_price"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(row[0]["tenancy_price"].ToString());
                tenanci.MonthTime = row[0]["tenancy_monthtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(row[0]["tenancy_monthtime"]);
                tenanci.IsDelivery = row[0]["tenancy_isdelivery"] == DBNull.Value ? false : Auxiliary.ToBoolen(row[0]["tenancy_isdelivery"]);
                tenanci.IsFavorable = row[0]["tenancy_isfavorable"] == DBNull.Value ? false : Auxiliary.ToBoolen(row[0]["tenancy_isfavorable"]);
                tenanci.FavorablePrice = row[0]["tenancy_favorableprice"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(row[0]["tenancy_favorableprice"].ToString());
                tenanci.Sort = row[0]["tenancy_sort"] == DBNull.Value ? 0 : Auxiliary.ToInt32(row[0]["tenancy_sort"]);
                tenanci.CreateTime = row[0]["tenancy_createtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(row[0]["tenancy_createtime"]);
                tenanci.UpdateTime = row[0]["tenancy_updatetime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(row[0]["tenancy_updatetime"]);
                //返回要交的租金实体
                return tenanci;
            }
            //如果租期表中没有要交的租金
            //查询订单商品表
            DataTable orderGoods = GetOrderGoodsesById(OrderGoodsId);
            //判断已交的租期如果小于商品的待售期
            if (goodsTenancies.Rows.Count < (orderGoods.Rows[0]["ordergoods_sellmonth"] == DBNull.Value ? 0 : Auxiliary.ToInt32(orderGoods.Rows[0]["ordergoods_sellmonth"])))
            {
                DateTime time;
                //判断租期表中是否存在付款信息
                if (goodsTenancies != null && goodsTenancies.Rows.Count > 0)
                {
                    //获得最后一次付款   月份+1 为下月付款信息
                    time = (goodsTenancies.Rows[0]["tenancy_monthtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(goodsTenancies.Rows[0]["tenancy_monthtime"])).AddMonths(1);
                }
                else
                {
                    //没有付款记录  添加本月日期
                    time = System.DateTime.Now;
                }
                //添加一条租期
                GoodsTenanci tenanci = new GoodsTenanci();
                tenanci.OrderGoodsId = OrderGoodsId;
                tenanci.Price = orderGoods.Rows[0]["ordergoods_monthprice"] == DBNull.Value ? Auxiliary.ToDecimal(goodsTenancies.Rows[0]["tenancy_price"].ToString()) : Auxiliary.ToDecimal(orderGoods.Rows[0]["ordergoods_monthprice"].ToString());
                tenanci.MonthTime = time;
                tenanci.IsDelivery = false;
                tenanci.IsFavorable = false;
                if (goodsTenancies != null && goodsTenancies.Rows.Count > 0)
                {
                    tenanci.Sort = Auxiliary.ToInt32(goodsTenancies.Rows[0]["tenancy_sort"]) + 1;
                }
                else
                {
                    tenanci.Sort = 1;
                }
                tenanci.CreateTime = System.DateTime.Now;
                tenanci.UpdateTime = tenanci.CreateTime;
                //tenanci.Id = AddGoodsTenancies(tenanci);
                return tenanci;
            }
            return null;
        }
        #endregion
        #region 添加租期并返回添加记录的ID
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-26
        /// 添加租期并返回添加记录的ID
        /// </summary>
        /// <param name="tenanci">租期实体</param>
        /// <returns></returns>
        public int AddGoodsTenancies(GoodsTenanci tenanci)
        {
            var sql = @"INSERT INTO [dbo].[GoodsTenancies]
                       ([ordergoods_id]
                       ,[tenancy_price]
                       ,[tenancy_monthtime]
                       ,[tenancy_isdelivery]
                       ,[tenancy_isfavorable]
                       ,[tenancy_favorableprice]
                       ,[tenancy_sort]
                       ,[tenancy_createtime]
                       ,[tenancy_updatetime])
                 VALUES
                       (@ordergoods_id
                       ,@tenancy_price
                       ,@tenancy_monthtime
                       ,@tenancy_isdelivery
                       ,@tenancy_isfavorable
                       ,@tenancy_favorableprice
                       ,@tenancy_sort
                       ,@tenancy_createtime
                       ,@tenancy_updatetime)select @@identity";
            SqlParam param = new SqlParam();
            param.AddParam("@ordergoods_id", tenanci.OrderGoodsId);
            param.AddParam("@tenancy_price", tenanci.Price);
            param.AddParam("@tenancy_monthtime", tenanci.MonthTime);
            param.AddParam("@tenancy_isdelivery", tenanci.IsDelivery);
            param.AddParam("@tenancy_isfavorable", tenanci.IsFavorable);
            param.AddParam("@tenancy_favorableprice", tenanci.FavorablePrice);
            param.AddParam("@tenancy_sort", tenanci.Sort);
            param.AddParam("@tenancy_createtime", tenanci.CreateTime);
            param.AddParam("@tenancy_updatetime", tenanci.UpdateTime);
            try
            {
                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "添加租期并返回添加记录的ID", ex);
            }
            return 0;
        }
        #endregion
        #region 根据订单商品ID获得订单商品信息
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-26
        /// 根据订单商品ID获得订单商品信息
        /// </summary>
        /// <param name="OrderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public DataTable GetOrderGoodsesById(int OrderGoodsId)
        {
            var sql = @"SELECT [ordergoods_id]
                      ,[order_id]
                      ,[goods_id]
                      ,[ordergoods_type]
                      ,[ordergoods_title]
                      ,[ordergoods_guid]
                      ,[ordercolor_name]
                      ,[ordercolor_code]
                      ,[ordernorm_name]
                      ,[ordergoods_num]
                      ,[ordergoods_deposit]
                      ,[ordergoods_monthprice]
                      ,[ordergoods_month]
                      ,[ordergoods_discount]
                      ,[ordergoods_isdel]
                      ,[ordergoods_latefees]
                      ,[ordergoods_lowestmonth]
                      ,[ordergoods_sellmonth]
                  FROM [dbo].[OrderGoodses](NOLOCK) WHERE ordergoods_id=@ordergoods_id";
            SqlParam param = new SqlParam();
            param.AddParam("@ordergoods_id", OrderGoodsId);
            try
            {
                return KYJ_ZushouRDB.GetTable(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "根据订单商品ID获得订单商品信息", ex);
            }
            return null;
        }
        #endregion
        #region 根据订单商品ID获得租期
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-26
        /// 根据订单商品ID获得租期
        /// </summary>
        /// <param name="OrderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public DataTable GetGoodsTenanciesCountByOrderGoodsId(int OrderGoodsId)
        {
            var sql = @"SELECT [tenancy_id]
                      ,[ordergoods_id]
                      ,[tenancy_price]
                      ,[tenancy_monthtime]
                      ,[tenancy_isdelivery]
                      ,[tenancy_isfavorable]
                      ,[tenancy_favorableprice]
                      ,[tenancy_sort]
                      ,[tenancy_createtime]
                      ,[tenancy_updatetime]
                      ,[tenancy_isdel]
                  FROM [dbo].[GoodsTenancies](NOLOCK) WHERE tenancy_isdel='false' and ordergoods_id=@ordergoods_id ORDER BY tenancy_monthtime DESC";
            SqlParam param = new SqlParam();
            param.AddParam("@ordergoods_id", OrderGoodsId);
            try
            {
                return KYJ_ZushouRDB.GetTable(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "根据订单商品ID获得已交的租期", ex);
            }
            return null;
        }
        #endregion
        #region 根据orderid获取订单扩展表
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-27
        /// 根据orderid获取订单扩展表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderOther GetOrderOtherByOrderId(int orderId)
        {
            var sql = @"SELECT [order_id]
                      ,[orderother_paidtime]
                      ,[orderother_deliverytime]
                      ,[orderother_harvesttime]
                      ,[orderother_sendsgreementtime]
                      ,[orderother_accruetime]
                      ,[orderother_orderstate]
                      ,[orderother_loseprice]
                      ,[orderother_rejectreason]
                      ,[orderother_renewtime]
                      ,[orderother_surrendertime]
                  FROM [dbo].[OrderOthers](NOLOCK) WHERE [order_id]=@order_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_id", orderId);
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    var item = dt.Rows[0];
                    OrderOther other = new OrderOther();
                    other.PaidTime = item["orderother_paidtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_paidtime"]);
                    other.DeliveryTime = item["orderother_deliverytime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_deliverytime"]);
                    other.HarvestTime = item["orderother_harvesttime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_harvesttime"]);
                    other.SendsGreementTime = item["orderother_sendsgreementtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_sendsgreementtime"]);
                    other.AccrueTime = item["orderother_accruetime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_accruetime"]);
                    other.LosePrice = Auxiliary.ToDecimal(item["orderother_loseprice"].ToString());
                    other.Rejectreason = item["orderother_rejectreason"].ToString();
                    other.RenewTime = item["orderother_renewtime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_renewtime"]);
                    other.SurrenderTime = item["orderother_surrendertime"] == DBNull.Value ? System.DateTime.Now : Convert.ToDateTime(item["orderother_surrendertime"]);
                    return other;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "根据orderid获取订单扩展表", ex);
            }
            return null;
        }
        #endregion
        /// <summary>
        /// 根据订单ID获取订单商品表中商品ID和商品价格ID
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderGoodsEntity GetOrderGoodsIdAndPriceIdByOrderId(int orderId)
        {
            var sql = @"SELECT [goods_id]
                      ,[goodsprice_id]
                      ,[ordergoods_num]
                  FROM [dbo].[OrderGoodses](NOLOCK) where order_id=@order_id";
            SqlParam param = new SqlParam();
            param.AddParam("@order_id", orderId);
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    OrderGoodsEntity entity = new OrderGoodsEntity();
                    var item = dt.Rows[0];
                    entity.GoodsId = Auxiliary.ToInt32(item["goods_id"]);
                    entity.GoodsPriceId = Auxiliary.ToInt32(item["goodsprice_id"]);
                    entity.OrderGoodsNum = Auxiliary.ToInt32(item["ordergoods_num"]);
                    return entity;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "根据订单ID获取订单商品表中商品ID和商品价格ID", ex);
            }
            return null;
        }
    }
}
