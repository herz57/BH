using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Machine
    {
        public int MachineId { get; set; }

        public DomainType DomainType { get; set; }

        public bool IsLocked { get; set; }


        public Domain Domain { get; set; }

        public virtual IList<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
