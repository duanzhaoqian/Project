using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IService;

namespace SqlDalImpl
{
    public abstract class BaseSqlDal : ISqlDal
    {

        public abstract void Delete(string guid, string filename);

        public abstract List<Model.PicModel> GetList(string guid);

        public abstract void Insert(Model.PicModel picModel);

        public abstract void Update(Model.PicModel picModel);
    }
}
