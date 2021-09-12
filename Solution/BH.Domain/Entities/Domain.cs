using BH.Common.Enums;
using System.Collections.Generic;

namespace BH.Domain.Entities
{
    public class Domain
    {
        public DomainType DomainType { get; set; }

        public string Description { get; set; }


        public virtual IList<Machine> Machines { get; set; } = new List<Machine>();
    }
}
