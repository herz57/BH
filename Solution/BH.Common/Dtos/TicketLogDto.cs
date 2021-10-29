using System;

namespace BH.Common.Dtos
{
    public class TicketLogDto
    {
        public int LogId { get; set; }

        public string UserName { get; set; }

        public int Win { get; set; }

        public int Cost { get; set; }

        public long ProfileBalance { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public DateTime Date { get; set; }
    }
}
