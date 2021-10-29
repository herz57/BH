using BH.Common.Models;
using System.Collections.Generic;

namespace BH.Common.Dtos
{
    public class StatisticDto
    {
        public MachinesStateDto MachinesState { get; set; }

        public List<UserStatisticDto> UserStatistics { get; set; }
    }
}
