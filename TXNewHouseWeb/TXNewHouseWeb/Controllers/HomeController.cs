using System.Web.Mvc;

namespace TXNewHouseWeb.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
