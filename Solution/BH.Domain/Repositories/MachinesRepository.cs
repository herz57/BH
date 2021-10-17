using BH.Common.Enums;
using BH.Domain.Entities;
using BH.Domain.Interfaces;
using BH.Domain;
using BH.Domain.Repositories.Base;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BH.Domain.Repositories
{
    public class MachinesRepository : BaseRepository<Machine>, IMachinesRepository
    {
        public MachinesRepository(BhDbContext context) : base(context) { }

        public async Task<int> LockMachineAsync(string userId, DomainType domainType)
        {
            var query = "exec dbo.LockMachine @userId, @domainType, @machineId out";
            var machineId = new SqlParameter
            {
                ParameterName = "@machineId",
                DbType = System.Data.DbType.Int32,
                Size = int.MaxValue,
                Direction = System.Data.ParameterDirection.Output
            };

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@userId", Value = userId },
                new SqlParameter { ParameterName = "@domainType", Value = domainType },
                machineId
            };

            await Context.Database.ExecuteSqlRawAsync(query, parms);
            return (int)machineId.Value;
        }

        public async Task UnlockMachineAsync(int machineId, string userId)
        {
            var query = @"update m set LockedByUserId = null
                from dbo.Machines m
                inner join dbo.AspNetUsers u on u.Id = m.LockedByUserId
                where u.Id = @userId and m.MachineId = @machineId";

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@userId", Value = userId },
                new SqlParameter { ParameterName = "@machineId", Value = machineId }
            };

            await Context.Database.ExecuteSqlRawAsync(query, parms);
        }

        public async Task UnlockMachinesAsync()
        {
            var query = @"update m set LockedByUserId = null
                from dbo.Machines m
                inner join dbo.Tickets t on t.MachineId = m.MachineId
                inner join dbo.TicketHistories th on th.TicketId = t.TicketId
                where LockedByUserId is not null and th.PlayedOutDate < dateadd(minute, -2, getutcdate())";

            await Context.Database.ExecuteSqlRawAsync(query);
        }
    }
}
