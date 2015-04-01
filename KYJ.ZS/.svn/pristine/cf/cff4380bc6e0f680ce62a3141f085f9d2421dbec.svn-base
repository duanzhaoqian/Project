using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.OrderGoodses
{
    //select o.order_id,o.order_createtime,g.ordergoods_title,ordergoods_monthprice,ordergoods_num,u.user_nickname,o.order_totalprice,o.order_state from  OrderGoodses g inner join  Orders o on g.order_id =o.order_id inner join LocalUsers u on o.[user_id]=u.[user_id] where o.order_createtime>= dateadd(month,-3,getdate()) and o.order_type=1	and g.ordergoods_isdel='false'

    public class RentalOrderEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string Number { get; set; }
          /// <summary>
        /// 图片guid
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }      
        /// <summary>
        /// 商品标题
        /// </summary>
        public string Title { get; set; }      
        /// <summary>
        /// 月租金
        /// </summary>
        public decimal Monthprice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Totalprice { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal Freightcost { get; set; }       
        /// <summary>
        /// 订单状态
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 订单时间范围
        /// </summary>
        public string OrderDate { get; set; }
         /// <summary>
        /// 订单商品类型
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// 订单所属表（近3个月还是以往的）
        /// </summary>
        public int Type { get; set; }       
        
    }
}
