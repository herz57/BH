using BH.Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class TicketsRepository : BaseRepository<Ticket>, ITicketsRepository
    {
        public TicketsRepository(BhDbContext context) : base(context) { }
        
        public async Task<Ticket> GetRandomTicketByMachineIdAsync(int profileId, int machineId, int ticketCost)
        {
            var query = "exec dbo.Play @profileId, machineId, ticketCost";
            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@profileId", Value = profileId },
                new SqlParameter { ParameterName = "@machineId", Value = machineId },
                new SqlParameter { ParameterName = "@ticketCost", Value = ticketCost }
            };

            return await Context.Tickets.FromSqlRaw(query, parms.ToArray()).SingleOrDefaultAsync();
        }
    }
}
