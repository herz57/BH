
namespace BH.Domain.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int MachineId { get; set; }

        public int Cost { get; set; }

        public int Win { get; set; }

        public bool PlayedOut { get; set; }

        public string Symbols { get; set; }


        public Machine Machine { get; set; }
    }
}
