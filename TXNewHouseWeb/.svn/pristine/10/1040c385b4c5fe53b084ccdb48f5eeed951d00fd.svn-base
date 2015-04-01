using System.Collections.Generic;
using TXCommons.Admins;
using TXDal.FinancialData;
using TXModel.AdminPVM;

namespace TXBll.FinancialData
{
    public class BondBll
    {
        private readonly BondDal _bondDal = new BondDal();

        /// <summary>
        /// 获取保证金列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_Bond> GetPageList_BySearch_Bond(PVS_NH_Bond search, int pageIndex, int pageSize)
        {
            var list = _bondDal.GetPageList_BySearch_Bond(search, pageIndex, pageSize);
            if (null != list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].EndTimeString = string.Format("{0:yyyy-MM-dd HH:mm:ss}", list[i].EndTime.AddDays(7));
                    list[i].BondString = string.Format("{0:0.00}", list[i].Bond);
                    list[i].BondStatusString = AdminTypes.Db_NewHouse.Tb_Order.Fc_ById.For_BondStatus(list[i].BondStatus);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取保证金列表总数据
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_Bond(PVS_NH_Bond search)
        {
            return _bondDal.GetTotalCount_BySearch_Bond(search);
        }
    }
}