using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using Ipek_Helpdesk.EntityFramework;

namespace Ipek_Helpdesk
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(Ipek_HelpdeskCoreModule))]
    public class Ipek_HelpdeskDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<Ipek_HelpdeskDbContext>(null);
        }
    }
}
