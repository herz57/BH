using Microsoft.Extensions.Logging;
using System;

namespace BH.Domain.Entities
{
    public class Log
    {
        public int LogId { get; set; }

        public int? EntityId { get; set; }

        public string EntityDiscriminator { get; set; }

        public LogLevel Level { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public DateTime? Date { get; set; }

        public string UserId { get; set; }
    }
}
