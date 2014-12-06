using System.Web.Mvc;

namespace Ipek_Helpdesk.Web.Controllers
{
    public class HomeController : Ipek_HelpdeskControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}