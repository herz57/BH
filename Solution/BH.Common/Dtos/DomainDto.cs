using BH.Common.Enums;
using System.Collections.Generic;

namespace BH.Common.Dtos
{
    public class DomainDto
    {
        public DomainType DomainType { get; set; }

        public string Description { get; set; }

        public IList<MachineDto> Machines { get; set; }
    }
}
