using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Sources.Xml;
using Abp.Modules;

namespace Ipek_Helpdesk.Web
{
    [DependsOn(typeof(Ipek_HelpdeskDataModule), typeof(Ipek_HelpdeskApplicationModule), typeof(Ipek_HelpdeskWebApiModule))]
    public class Ipek_HelpdeskWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flag-tr"));

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new XmlLocalizationSource(
                    Ipek_HelpdeskConsts.LocalizationSourceName,
                    HttpContext.Current.Server.MapPath("~/Localization/Ipek_Helpdesk")
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<Ipek_HelpdeskNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
