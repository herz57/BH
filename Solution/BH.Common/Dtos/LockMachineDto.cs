using System.Collections.Generic;

namespace BH.Common.Dtos
{
    public class LockMachineDto
    {
        public int MachineId { get; set; }

        public List<int> AvailableCosts { get; set; }
    }
}
