using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.RentalGoodses;
using Webdiyer.WebControls.Mvc;
using System.Text.RegularExpressions;
using KYJ.ZS.BLL.RentalGoodses;

namespace KYJ.ZS.Search.Controllers
{
    public class SearchController:BaseController
    {

        GetRentalGoodsListBll getlistBll = new GetRentalGoodsListBll();

        public ActionResult Index(string condition)
        {
            RentalGoodsSearchEntity model = new RentalGoodsSearchEntity();

            try
            {
                #region 搜索数据处理

                bool iscurrentpage = Regex.IsMatch(condition, @"-i\d{1,}");//是否是页格式
                var url = Request.RawUrl.Substring(1);
                var key = Regex.Match(url, @"-w_\w+").ToString();//关键字
                if (!iscurrentpage)
                    if (key != "")
                    {
                        condition = condition.Replace(key, "") + "-i1" + key;
                        url = url.Replace(key, "") + "-i1" + key;
                    }
                    else
                    {
                        condition = condition + "-i1";
                        url = url + "-i1";
                    }//把key加到url最后

                //设置pagesize:36
                condition += "-s36";
                #endregion

                PagedList<RentalGoodsListItemEntity> list = getlistBll.RentalGoodsListResutl(condition, "");

                model.ItemList = list;
                model.PageSize = list.PageSize;
                model.TotalItemCount = list.TotalItemCount;
                model.CurrentPageIndex = list.CurrentPageIndex;
                ViewData["conPage"] = url;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<string>("wwang", condition, ex);
            }
            return View(model);
        }
    }
}
