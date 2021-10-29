using BH.Domain.Entities;
using BH.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using BH.Domain.Interfaces;
using BH.Domain.Extensions;
using BH.Common.Dtos;

namespace BH.Domain.Repositories
{
    public class TicketsRepository : BaseRepository<Ticket>, ITicketsRepository
    {
        public TicketsRepository(BhDbContext context) : base(context) { }
        
        public async Task<int> PlayAsync(string userId, int machineId, int ticketCost)
        {
            var query = "exec dbo.Play @userId, @machineId, @ticketCost, @ticketId out";
            var ticketId = new SqlParameter
            {
                ParameterName = "@ticketId",
                DbType = System.Data.DbType.Int32,
                Size = int.MaxValue,
                Direction = System.Data.ParameterDirection.Output
            };

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@userId", Value = userId },
                new SqlParameter { ParameterName = "@machineId", Value = machineId },
                new SqlParameter { ParameterName = "@ticketCost", Value = ticketCost },
                ticketId
            };

            await Context.Database.ExecuteSqlRawAsync(query, parms);
            return (int)ticketId.Value;
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            var query = "select * from dbo.Tickets t where t.TicketId = @ticketId";
            var param = new SqlParameter { ParameterName = "@ticketId", Value = ticketId };
            return await Context.Tickets.FromSqlRaw(query, param).SingleOrDefaultAsync();
        }

        public TicketLogDto GetTicketLog(int ticketId, string userId)
        {
            var result = Context.LoadStoredProc("dbo.GetTicketWonLog")
               .WithSqlParam("ticketId", ticketId)
               .WithSqlParam("userId", userId)
               .ExecuteStoredProc<TicketLogDto>()
               .Single();

            return result;
        }
    }
}
