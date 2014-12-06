using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Ipek_Helpdesk
{
    [DependsOn(typeof(AbpWebApiModule), typeof(Ipek_HelpdeskApplicationModule))]
    public class Ipek_HelpdeskWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(Ipek_HelpdeskApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
