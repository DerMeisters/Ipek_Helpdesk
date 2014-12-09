namespace Ipek_Helpdesk.Tickets
{
    using System;

    using Abp.Application.Services.Dto;

    public class TicketDto : EntityDto
    {
        public Guid RefId { get; set; }

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

        public bool IsAgentReminded { get; set; }

        public double? ResponseTime { get; set; }
    }
}
