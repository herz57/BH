using BH.Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class TicketsRepository : BaseRepository<Ticket>, ITicketsRepository
    {
        public TicketsRepository(BhDbContext context) : base(context) { }
        
        public async Task<Ticket> GetRandomTicketByMachineIdAsync(int machineId)
        {
            var query = @"select top(1) * from Tickets t 
                where t.MachineId = {0}
                order by newid()";

            return await Context.Tickets.FromSqlRaw(query, machineId).SingleOrDefaultAsync();
        }
    }
}
