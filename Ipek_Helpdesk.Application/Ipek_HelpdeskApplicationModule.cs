using System.Reflection;
using Abp.Modules;

namespace Ipek_Helpdesk
{
    [DependsOn(typeof(Ipek_HelpdeskCoreModule))]
    public class Ipek_HelpdeskApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DtoMappings.Map();
        }
    }
}
