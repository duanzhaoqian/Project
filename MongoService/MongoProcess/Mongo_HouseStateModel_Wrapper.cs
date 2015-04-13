using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXCommons.Mongo;

namespace MongoProcess
{
    public class Mongo_HouseStateModel_Wrapper
    {
        public Mongo_HouseStateModel_Wrapper()
        {
            MongoHouseStateModel=new Mongo_HouseStateModel();
        }
        public int Id { get; set; }
        public Mongo_HouseStateModel MongoHouseStateModel { get; set; }
    }
}
