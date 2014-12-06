using Abp.Web.Mvc.Views;

namespace Ipek_Helpdesk.Web.Views
{
    public abstract class Ipek_HelpdeskWebViewPageBase : Ipek_HelpdeskWebViewPageBase<dynamic>
    {

    }

    public abstract class Ipek_HelpdeskWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected Ipek_HelpdeskWebViewPageBase()
        {
            LocalizationSourceName = Ipek_HelpdeskConsts.LocalizationSourceName;
        }
    }
}