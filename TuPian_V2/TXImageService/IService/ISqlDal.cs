using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace IService
{
    public interface ISqlDal
    {
        void Delete(string guid, string filename);
        List<PicModel> GetList(string guid);
        void Insert(PicModel picModel);
        void Update(PicModel picModel);
    }
}
