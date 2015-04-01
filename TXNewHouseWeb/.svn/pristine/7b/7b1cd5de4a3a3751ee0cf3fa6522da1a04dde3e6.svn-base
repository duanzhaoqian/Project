
using TXOrm;
using TXModel.Commons;
namespace TXBll.HouseData
{
    /// <summary>
    /// 楼盘BLL类-开发商后台
    /// </summary>
    public partial class PremisesBll
    {
        private readonly TXDal.HouseData.PremisesDal dal = new TXDal.HouseData.PremisesDal();

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/12 15:37:17
        /// 功能描述：楼盘分页列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="DeveloperId"></param>
        /// <returns></returns>
        public Paging<Premis> GetPremisList(Paging<Premis> paging, int DeveloperId)
        {
            return dal.GetPremisList(paging,DeveloperId);
        }
    }
}