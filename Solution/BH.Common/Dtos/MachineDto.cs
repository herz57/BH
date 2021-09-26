using BH.Common.Enums;
using System.Collections.Generic;

namespace BH.Common.Dtos
{
    public class MachineDto
    {
        public int MachineId { get; set; }

        public DomainType DomainType { get; set; }

        public bool IsLocked { get; set; }

        public DomainDto Domain { get; set; }

        public IList<PlayResponseDto> Tickets { get; set; }
    }
}
