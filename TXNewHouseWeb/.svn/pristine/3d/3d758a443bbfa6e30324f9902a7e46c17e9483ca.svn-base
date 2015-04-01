using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXDal.HouseData;
using TXModel.AdminPVM;
using TXOrm;

namespace TXBll.HouseData
{
    public class PremiseImgMapBll
    {
        private PremiseImgMapDal dal = new PremiseImgMapDal();
        public List<PremiseImgMap> GetPermitImg(int cityid, int pageindex, int pagesize = 20)
        {
            if (cityid > 0 && pageindex > 0)
            {
                return dal.GetPermitImgList(cityid, pageindex, pagesize);
            }
            return null;
        }

        public int GetAllCount(int cityid)
        {
            return dal.GetAllPermitImgCount(cityid);
        }

        public int DelPermitImgMap(PremiseImgMap model)
        {
            if (model != null)
                return dal.DelPermitImgMap(model);
            else
                throw new ArgumentException("参数为空");
        }

        public List<PVM_NH_Premises> GetAllPremisesList()
        {
            return dal.GetAllPremisesList();
        }
    }
}
