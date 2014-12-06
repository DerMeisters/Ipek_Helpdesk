namespace Ipek_Helpdesk.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ipek_Helpdesk.EntityFramework.Ipek_HelpdeskDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Ipek_Helpdesk";
        }

        protected override void Seed(Ipek_Helpdesk.EntityFramework.Ipek_HelpdeskDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
