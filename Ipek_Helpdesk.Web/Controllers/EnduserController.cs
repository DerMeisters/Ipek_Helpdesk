namespace Ipek_Helpdesk.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Ipek.App.Utils;

    using Ipek_Helpdesk.Tickets;

    [AllowAnonymous]
    public class EnduserController : Ipek_HelpdeskControllerBase
    {
        private readonly ITicketAppService _ticketService;

        public EnduserController(ITicketAppService ticketService)
        {
            _ticketService = ticketService;
        }

        // fem: from email (returns a different view if the call is within an email)
        public PartialViewResult View(int id, string refId, bool fem = false)
        {
            var model = _ticketService.Get(id);
            if (model.RefId.ToString() != refId)
            {
                throw new Exception("RefId not matching!");
            }

            return this.PartialView("Enduser/_Reopen", model);
        }

        [HttpPost]
        public string Reopen(TicketDto input)
        {
            var model = _ticketService.Get(input.Id);
            if (model.RefId.ToString() != input.RefId.ToString() || !model.IsClosed)
            {
                throw new Exception("RefId not matching or ticket already open!");
            } 

            _ticketService.Reopen(input.Id, input.OwnersReopenMessage);
            this.NotifyAgent(input.Id);
            return model.AssignedTo;
        }

        private void NotifyAgent(int ticketId)
        {
            var ticket = this._ticketService.Get(ticketId);
            const string Subject = "Ticket has been re-opened by the user!";
            string body = "<h2>Following ticket has been re-opened by the user:</h2>";
            body += "<b>Sender:</b> " + ticket.CreatedBy + "<br />";
            body += "<b>Subject:</b> " + ticket.Subject + "<br />";
            body += "<b>Re-open reason:</b> " + ticket.OwnersReopenMessage + "<br />";
            body += "<p>After completing the task you may <a href='http://helpdesk.ipek.edu.tr/agent/view/" + ticket.Id + "/" + ticket.RefId
                    + "?fem=true'> click here</a> or go to http://helpdesk.ipek.edu.tr/agent to close the ticket!";

            string error;
            Utils.SendMail(Subject, body, out error, null, new[] { ticket.AssignedTo + "@ipek.edu.tr" });
        }
	}
}