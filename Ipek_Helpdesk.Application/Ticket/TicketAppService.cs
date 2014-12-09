namespace Ipek_Helpdesk.Tickets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    class TicketAppService : Ipek_HelpdeskAppServiceBase, ITicketAppService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketAppService(ITicketRepository ticketRepository)
        {
            this._ticketRepository = ticketRepository;
        }

        public void Create(TicketDto input)
        {
            _ticketRepository.Insert(Mapper.Map<Ticket>(input));
        }

        public TicketDto Get(int id)
        {
            return Mapper.Map<TicketDto>(_ticketRepository.Get(id));
        }

        public TicketDto Get(Guid refId)
        {
            return Mapper.Map<TicketDto>(_ticketRepository.GetAllList(x => x.RefId == refId).FirstOrDefault());
        }

        public List<TicketDto> GetAll()
        {
            return Mapper.Map<List<TicketDto>>(_ticketRepository.GetAllList().OrderByDescending(x => x.CreationTime));
        }

        public List<TicketDto> GetOpens()
        {
            return Mapper.Map<List<TicketDto>>(_ticketRepository.GetAllList(x => x.IsClosed == false && x.IsDeleted == false));
        }

        public List<TicketDto> GetClosed()
        {
            return Mapper.Map<List<TicketDto>>(_ticketRepository.GetAllList(x => x.IsClosed == true && x.IsDeleted == false));
        }

        public List<TicketDto> GetByAgent(string agent)
        {
            return Mapper.Map<List<TicketDto>>(_ticketRepository.GetAllList(x => x.AssignedTo == agent && x.IsDeleted == false));
        }

        public void Assign(int id, string agent)
        {
            var now = DateTime.Now;
            var ticket = _ticketRepository.Get(id);
            ticket.AssignedTo = agent;
            ticket.AssignTime = now;
            ticket.History += "Assigned to " + agent + " on " + now + "<br />";
        }

        public void Close(int id, string agentMessage)
        {
            var now = DateTime.Now;
            var ticket = _ticketRepository.Get(id);
            ticket.IsClosed = true;
            ticket.AgentClosingMessage = agentMessage;
            ticket.ClosingTime = now;
            ticket.History += "Closed by agent on " + now + "<br />";
        }

        public void Reopen(int id, string userMessage)
        {
            var now = DateTime.Now;
            var ticket = _ticketRepository.Get(id);
            ticket.IsClosed = false;
            ticket.ClosingTime = null;
            ticket.OwnersReopenMessage = userMessage;
            ticket.History += "Re-opened by user on " + now + "<br />";
        }

        public void Delete(int id)
        {
            _ticketRepository.Delete(id);
        }

        public void PokeAgent(int id)
        {
            var now = DateTime.Now;
            var ticket = _ticketRepository.Get(id);
            ticket.History += "Agent poked on " + now + "(" + ticket.ResponseTime + "hrs)<br />";
        }
    }
}
