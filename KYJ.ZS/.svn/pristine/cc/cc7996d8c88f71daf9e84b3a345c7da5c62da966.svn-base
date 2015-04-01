using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace KYJ.ZS.Commons.Index
{
    
    public class IndexResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string pid { get; set; }

    }

    [DataContract(Namespace = "http://www.kuaiyoujia.com/")]
    public class Summary
    {
        [DataMember]
        public int TotalRecords { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int TotalPage { get; set; }
        [DataMember]
        public int RealRecords { get; set; }

    }

    [DataContract(Namespace = "http://www.kuaiyoujia.com/", Name = "Relusts")]
    public class Relusts<Categories>
    {
        public List<Categories> List { get; set; }

        [DataMember(Order = 1)]
        public List<Categories> FatherCategories { get; set; }
        [DataMember(Order = 2)]
        public List<Categories> SonCategories { get; set; }
    }
}
