using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DeliveryAddresses;

namespace KYJ.ZS.BLL.DeliveryAddresses
{
    public class DeliveryAddressBll
    {
        public DeliveryAddressDal _dal = null;
        public DeliveryAddressBll()
        {
            if (_dal == null)
            {
                _dal = new DeliveryAddressDal();
            }
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:用户后台添加收货地址
        /// </summary>
        /// <param name="address">收货地址实体</param>
        /// <returns></returns>
        public int AddDeliveryAddress(DeliveryAddress address)
        {
            return _dal.AddDeliveryAddress(address);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:用户后台修改收货地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public int UpdateDeliveryAddress(DeliveryAddress address)
        {
            return _dal.UpdateDeliveryAddress(address);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:用户后台删除收货地址
        /// </summary>
        /// <param name="deliveryAddressId">收货地址ID</param>
        /// <returns></returns>
        public int DelDeliveryAddress(int deliveryAddressId,int userId)
        {
            return _dal.DelDeliveryAddress(deliveryAddressId, userId);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:根据User_Id查找收货地址
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns>收货地址集合</returns>
        public List<DeliveryAddress> DeliveryAddresses(int userId)
        {
            return _dal.DeliveryAddresses(userId);
        }
          /// <summary>
        /// Author:baiyan
        /// Time:2014-4-24
        /// Desc:根据收货地址ID查找收货地址
        /// </summary>
        /// <param name="deliveryId">收货地址的ID</param>
        /// <returns></returns>
        public DeliveryAddress FindIdDeliveryAddresses(int deliveryId,int userId)
        {
            return _dal.FindIdDeliveryAddresses(deliveryId, userId);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：订单页—收获地址列表
        /// </summary>
        /// <param name="user_id">用户Id</param>
        /// <returns></returns>
        public List<DeliveryAddress> Web_DeliVeryAddressList(int user_id)
        {
            return _dal.Web_DeliVeryAddressList(user_id);
        }

    }

}
