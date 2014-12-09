namespace Ipek_Helpdesk.Tickets
{
    using System;
    using System.Collections.Generic;

    using Abp.Application.Services;

    public interface ITicketAppService : IApplicationService
    {
        void Create(TicketDto input);

        TicketDto Get(int id);

        TicketDto Get(Guid refId);

        List<TicketDto> GetAll();

        List<TicketDto> GetOpens();

        List<TicketDto> GetClosed();

        List<TicketDto> GetByAgent(string agent); 

        void Assign(int id, string agent);

        void Close(int id, string agentMessage);

        void Reopen(int id, string userMessage);

        void Delete(int id);

        void PokeAgent(int id);
    }
}
