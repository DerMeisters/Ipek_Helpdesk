namespace Ipek_Helpdesk.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                        CreatedBy = c.String(),
                        OwnerEmail = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        AssignedTo = c.String(),
                        AssignTime = c.DateTime(),
                        AgentClosingMessage = c.String(),
                        OwnersReopenMessage = c.String(),
                        IsClosed = c.Boolean(nullable: false),
                        ClosingTime = c.DateTime(),
                        History = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        RefId = c.Guid(nullable: false),
                        IsAgentReminded = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "Abp_SoftDelete", "True" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tickets",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "Abp_SoftDelete", "True" },
                });
        }
    }
}
