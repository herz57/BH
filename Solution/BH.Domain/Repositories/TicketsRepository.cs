using BH.Domain.Entities;
using BH.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using BH.Domain.Interfaces;
using BH.Common.Models;
using BH.Domain.Extensions;

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

        public async Task<List<int>> GetAvailableMachineCosts(int machineId)
        {
            var query = @"select t.Cost from dbo.Tickets t
                where t.MachineId = @machineId
                group by t.Cost";

            var param = new SqlParameter { ParameterName = "@machineId", Value = machineId };
            return await Context.Tickets
                .FromSqlRaw(query, param)
                .Select(t => t.Cost)
                .ToListAsync();
        }

        public IList<UserStatistic> GetUsersStatistics(int forDays)
        {
            var result = Context.LoadStoredProc("dbo.GetUsersStatistics")
               .WithSqlParam("forDays", forDays)
               .ExecuteStoredProc<UserStatistic>();

            return result;
        }
    }
}
