namespace Ipek_Helpdesk.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Ipek.App.Utils;

    using Ipek_Helpdesk.Tickets;

    [Authorize(Roles = "IT")]
    public class AgentController : Ipek_HelpdeskControllerBase
    {
        private readonly ITicketAppService _ticketService;

        public AgentController(ITicketAppService ticketService)
        {
            _ticketService = ticketService;
        }

        public ActionResult Index()
        {
            var agent = User.Identity.Name.Split('\\')[1];
            var model = _ticketService.GetByAgent(agent);
            return View(model);
        }

        [AllowAnonymous]
        // fem: from email (returns a different view if the call is within an email)
        public PartialViewResult View(int id, string refId, bool fem = false)
        {
            var model = _ticketService.Get(id);
            if (model.RefId.ToString() != refId)
            {
                throw new Exception("RefId not matching!");
            }

            return fem ? this.PartialView("Agent/_Close", model) : this.PartialView("Agent/_View", model);
        }

        [AllowAnonymous]
        [HttpPost]
        public string Close(TicketDto input)
        {
            var model = _ticketService.Get(input.Id);
            if (model.RefId.ToString() != input.RefId.ToString() || model.IsClosed)
            {
                throw new Exception("RefId not matching or ticket already closed!");
            } 

            _ticketService.Close(input.Id, input.AgentClosingMessage);
            this.NotifyUser(input.Id);
            return model.AssignedTo;
        }

        public PartialViewResult GetAll(string agent)
        {
            agent = agent ?? User.Identity.Name.Split('\\')[1];
            var model = _ticketService.GetByAgent(agent);
            return this.PartialView("Agent/_List", model);
        }

        private void NotifyUser(int ticketId)
        {
            var ticket = this._ticketService.Get(ticketId);
            const string Subject = "Your helpdesk ticket has been closed by the agent!";
            string body = "<h2>Following ticket has been closed by the assigned agent:</h2>";
            body += "<b>Subject:</b> " + ticket.Subject + "<br />";
            body += "<b>Agent:</b> " + ticket.AssignedTo + "<br />";
            body += "<b>Agent notes:</b> " + ticket.AgentClosingMessage + "<br />";
            body += "<p>If you are not happy with the resolution you may <a href='http://helpdesk.ipek.edu.tr/enduser/view/" + ticket.Id + "/" + ticket.RefId
                    + "?fem=true'> click here</a> to re-open the ticket! Otherwise you may just ignore this message.";

            string error;
            Utils.SendMail(Subject, body, out error, null, new[] { ticket.OwnerEmail });
        }
	}
}