using System;

namespace BH.Domain.Entities
{
    public class TicketHistory
    {
        public int TicketId { get; set; }

        public DateTime? PlayedOutDate { get; set; }

        public string PlayedOutByUserId { get; set; }

        public Ticket Ticket { get; set; }
    }
}
