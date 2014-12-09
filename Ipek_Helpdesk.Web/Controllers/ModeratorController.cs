namespace Ipek_Helpdesk.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;

    using Ipek.App.Utils;

    using Ipek_Helpdesk.Tickets;

    using Microsoft.Exchange.WebServices.Data;

    [Authorize(Users = "ipekuni\\hulker, ipekuni\\yerdal")]
    public class ModeratorController : Ipek_HelpdeskControllerBase
    {
        private readonly ITicketAppService _ticketService;

        public ModeratorController(ITicketAppService ticketService)
        {
            _ticketService = ticketService;
        }

        public ActionResult Index()
        {
            this.FetchAndPersistEmails();
            var model = _ticketService.GetAll();
            return View(model);
        }

        public PartialViewResult View(int id)
        {
            var model = _ticketService.Get(id);
            return this.PartialView("Moderator/_View", model);
        }

        public PartialViewResult Assign(int id)
        {
            var model = _ticketService.Get(id);
            return this.PartialView("Moderator/_Assign", model);
        }

        public void PokeAgent(int id)
        {
            var ticket = _ticketService.Get(id);
            if (ticket.AssignedTo != null && ticket.ResponseTime > 8)
            {
                this.PokeAgentByEmail(ticket);
                _ticketService.PokeAgent(id);
            }
        }

        [HttpPost]
        public void Assign(TicketDto input)
        {
            _ticketService.Assign(input.Id, input.AssignedTo);
            this.NotifyAgent(input.Id);
            this.NotifyUser(input.Id);
        }

        public void Delete(int id)
        {
            _ticketService.Delete(id);
        }

        public PartialViewResult GetAll()
        {
            var model = _ticketService.GetAll();
            return this.PartialView("Moderator/_List", model);
        }

        private void FetchAndPersistEmails()
        {
            var service = new ExchangeService(ExchangeVersion.Exchange2010_SP1)
                              {
                                  Credentials = new WebCredentials("helpdesk", "helpdesk2020BT"),
                                  Url = new Uri("https://mail.ipek.edu.tr/EWS/Exchange.asmx")
                              };
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var itempropertyset = new PropertySet(BasePropertySet.FirstClassProperties) { RequestedBodyType = BodyType.Text };
            var itemview = new ItemView(1000) { PropertySet = itempropertyset };
            try
            {
                FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, itemview);
                // service.LoadPropertiesForItems(findResults, itempropertyset);
                foreach (Item item in findResults)
                {
                    item.Load(itempropertyset);
                    var email = (EmailMessage)item;
                    string history = "Created by " + email.Sender.Name + " on " + DateTime.Now + "</br>";
                    email.Delete(deleteMode: DeleteMode.MoveToDeletedItems);
                    email.Update(conflictResolutionMode: ConflictResolutionMode.AlwaysOverwrite);
                    _ticketService.Create(new TicketDto { RefId = Guid.NewGuid(), Body = email.Body, CreatedBy = email.Sender.Name, OwnerEmail = email.Sender.Address, Subject = email.Subject, History = history });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong!" + ex.Message);
            }
        }

        private void NotifyAgent(int ticketId)
        {
            var ticket = this._ticketService.Get(ticketId);
            const string Subject = "A new helpdesk ticket has been assigned to you!";
            string body = "<h2>Following ticket has been assigned to you:</h2>";
            body += "<b>Sender:</b> " + ticket.CreatedBy + "<br />";
            body += "<b>Subject:</b> " + ticket.Subject + "<br />";
            body += "<b>Body:</b> " + ticket.Body + "<br />";
            body += "<p>After completing the task you may <a href='http://helpdesk.ipek.edu.tr/agent/view/" + ticket.Id + "/" + ticket.RefId
                    + "?fem=true'> click here</a> or go to http://helpdesk.ipek.edu.tr/agent to close the ticket!";

            string error;
            Utils.SendMail(Subject, body, out error, null, new[] { ticket.AssignedTo + "@ipek.edu.tr" });
        }

        private void PokeAgentByEmail(TicketDto ticket)
        {
            const string Subject = "You have been poked!";
            string body = "<h2>Following ticket has been open for: " + ticket.ResponseTime + " hrs</h2>";
            body += "<h2>Please complete the task and close the ticket as soon as possible!</h2>";
            body += "<b>Sender:</b> " + ticket.CreatedBy + "<br />";
            body += "<b>Subject:</b> " + ticket.Subject + "<br />";
            body += "<b>Body:</b> " + ticket.Body + "<br />";
            body += "<p>After completing the task you may <a href='http://helpdesk.ipek.edu.tr/agent/view/" + ticket.Id + "/" + ticket.RefId
                    + "?fem=true'> click here</a> or go to http://helpdesk.ipek.edu.tr/agent to close the ticket!";

            string error;
            Utils.SendMail(Subject, body, out error, null, new[] { ticket.AssignedTo + "@ipek.edu.tr" });
        }

        private void NotifyUser(int ticketId)
        {
            var ticket = this._ticketService.Get(ticketId);
            const string Subject = "Your helpdesk ticket has been assigned to an agent!";
            string body = "<h2>Following ticket has been assigned to:" + ticket.AssignedTo + "</h2>";
            body += "<b>Subject:</b> " + ticket.Subject + "<br />";
            body += "<b>Agent:</b> " + ticket.AssignedTo + "<br />";
            body += "<b>Assign time:</b> " + ticket.AssignTime + "<br />";
            
            string error;
            Utils.SendMail(Subject, body, out error, null, new[] { ticket.OwnerEmail });
        }
    }
}