using System.Reflection;
using Abp.Modules;

namespace Ipek_Helpdesk
{
    public class Ipek_HelpdeskCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
