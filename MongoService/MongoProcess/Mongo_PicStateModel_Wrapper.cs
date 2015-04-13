using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXCommons.Mongo;

namespace MongoProcess
{
    public class Mongo_PicStateModel_Wrapper
    {
        public Mongo_PicStateModel_Wrapper()
        {
            MongoPicStateModel=new Mongo_PicStateModel();
        }
        public int Id { get; set; }
        public Mongo_PicStateModel MongoPicStateModel { get; set; }
    }
}
