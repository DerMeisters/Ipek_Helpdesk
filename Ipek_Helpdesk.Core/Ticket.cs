namespace Ipek_Helpdesk.Tickets
{
    using System;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    public class Ticket : Entity, IHasCreationTime, ISoftDelete
    {
        public Ticket()
        {
            CreationTime = DateTime.Now;
        }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string CreatedBy { get; set; }

        public string OwnerEmail { get; set; }

        public DateTime CreationTime { get; set; }

        public string AssignedTo { get; set; }

        public DateTime? AssignTime { get; set; }

        public string AgentClosingMessage { get; set; }

        public string OwnersReopenMessage { get; set; }

        public bool IsClosed { get; set; }

        public DateTime? ClosingTime { get; set; }

        public string History { get; set; }

        public bool IsDeleted { get; set; }

        public Guid RefId { get; set; }

        public bool IsAgentReminded { get; set; }

        public virtual double? ResponseTime
        {
            get
            {
                return AssignedTo == null ? (double?)null : IsClosed ? Math.Round((ClosingTime - AssignTime).Value.TotalHours, 1) : Math.Round((DateTime.Now - AssignTime).Value.TotalHours, 1);
            }
        }
    }
}
