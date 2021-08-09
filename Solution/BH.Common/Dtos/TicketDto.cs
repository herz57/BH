using System;
using System.Collections.Generic;
using System.Text;

namespace BH.Common.Dtos
{
    public class TicketDto
    {
        public int TicketId { get; set; }

        public int MachineId { get; set; }

        public int Cost { get; set; }

        public int Win { get; set; }

        public bool PlayedOut { get; set; }

        public string Symbols { get; set; }

        public MachineDto Machine { get; set; }
    }
}
