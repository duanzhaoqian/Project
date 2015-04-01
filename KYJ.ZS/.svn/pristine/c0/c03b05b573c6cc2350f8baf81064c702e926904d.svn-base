using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Categories;

namespace KYJ.ZS.Commons.Index
{
    public static class IndexCommon
    {
        public static Relusts<CatResult> GetCategories(string query)
        {
            return Auxiliary.SerializeRead<Relusts<CatResult>>("http://searchweb.zushou.net/Categories/GetCategories.ashx?condition=" + query) as Relusts<CatResult>;
        }
    }
}
