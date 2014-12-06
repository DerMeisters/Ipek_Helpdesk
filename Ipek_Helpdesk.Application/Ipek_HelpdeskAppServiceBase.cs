using Abp.Application.Services;

namespace Ipek_Helpdesk
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class Ipek_HelpdeskAppServiceBase : ApplicationService
    {
        protected Ipek_HelpdeskAppServiceBase()
        {
            LocalizationSourceName = Ipek_HelpdeskConsts.LocalizationSourceName;
        }
    }
}