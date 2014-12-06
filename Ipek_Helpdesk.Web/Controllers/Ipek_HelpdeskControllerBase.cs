using Abp.Web.Mvc.Controllers;

namespace Ipek_Helpdesk.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class Ipek_HelpdeskControllerBase : AbpController
    {
        protected Ipek_HelpdeskControllerBase()
        {
            LocalizationSourceName = Ipek_HelpdeskConsts.LocalizationSourceName;
        }
    }
}