using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TXSearch
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.Commons.Index;
    using KYJ.ZS.Models.Categories;
    using KYJ.ZS.Search.Api.Common;
    using KYJ.ZS.Search.Api.Action;
    /// <summary>
    /// Index 的摘要说明
    /// </summary>
    public class Index : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";

            Director director = new Director();
            Builder action = null;

            switch (context.Request["action"])
            {
                case "Attributes" :
                    action = new KYJ.ZS.Search.Api.Action.Attributes();
                    action.Ini = new KYJ.ZS.Search.Api.Ini(Auxiliary.ConfigKey("AttributesIndexPath"), KYJ.ZS.Search.Api.IndexType.IndexSearcher);
                    action.Ini.condition = context.Request["condition"]; 
                    break;
                case "Categories":
                    action = new KYJ.ZS.Search.Api.Action.Categories();
                    action.Ini = new KYJ.ZS.Search.Api.Ini(Auxiliary.ConfigKey("CategoriesIndexPath"), KYJ.ZS.Search.Api.IndexType.IndexSearcher);
                    action.Ini.condition = context.Request["condition"];           
                    break;
                case "Brands":
                    action = new KYJ.ZS.Search.Api.Action.Categories();
                    action.Ini = new KYJ.ZS.Search.Api.Ini(Auxiliary.ConfigKey("BrandsIndexPath"), KYJ.ZS.Search.Api.IndexType.IndexSearcher);
                    action.Ini.condition = context.Request["condition"];
                    break;
                case "RentalGoodses":
                    action = new KYJ.ZS.Search.Api.Action.Categories();
                    action.Ini = new KYJ.ZS.Search.Api.Ini(Auxiliary.ConfigKey("RentalGoodsesIndexPath"), KYJ.ZS.Search.Api.IndexType.IndexSearcher);
                    action.Ini.condition = context.Request["condition"];
                    break;
                case "SaleGoodses":
                    action = new KYJ.ZS.Search.Api.Action.Categories();
                    action.Ini = new KYJ.ZS.Search.Api.Ini(Auxiliary.ConfigKey("SaleGoodsesIndexPath"), KYJ.ZS.Search.Api.IndexType.IndexSearcher);
                    action.Ini.condition = context.Request["condition"];
                    break;
            }

            var relusts = director.Exec(action);
            
            if (relusts != null)
            {
                //result = Auxiliary.SerializeWrite<Relusts<CatResult>>(relusts);
                result = relusts.ToXml();
                //var o = SerializeHelper.DeserializeXmlToObject<Relusts<CatResult>>(result);
                
            }
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}