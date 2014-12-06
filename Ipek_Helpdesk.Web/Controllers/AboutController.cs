using System.Web.Mvc;

namespace Ipek_Helpdesk.Web.Controllers
{
    public class AboutController : Ipek_HelpdeskControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}