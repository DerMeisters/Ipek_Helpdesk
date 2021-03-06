﻿namespace Ipek_Helpdesk.EntityFramework
{
    using System.Data.Entity;

    using Abp.EntityFramework;

    using Ipek_Helpdesk.Tickets;

    public class Ipek_HelpdeskDbContext : AbpDbContext
    {
        public virtual IDbSet<Ticket> Tickets { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public Ipek_HelpdeskDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in Ipek_HelpdeskDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of Ipek_HelpdeskDbContext since ABP automatically handles it.
         */
        public Ipek_HelpdeskDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }

    //Example:
    //public class User : Entity
    //{
    //    public string Name { get; set; }

    //    public string Password { get; set; }
    //}
}
