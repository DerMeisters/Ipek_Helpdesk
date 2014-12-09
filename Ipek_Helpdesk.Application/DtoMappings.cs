namespace Ipek_Helpdesk
{
    using AutoMapper;

    using Ipek_Helpdesk.Tickets;

    internal static class DtoMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<TicketDto, Ticket>();
            Mapper.CreateMap<Ticket, TicketDto>();
        }
    }
}
