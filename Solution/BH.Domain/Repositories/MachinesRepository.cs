using BH.Common.Enums;
using BH.Domain.Entities;
using BH.Domain.Interfaces;
using BH.Domain.Repositories.Base;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using BH.Domain.Extensions;
using BH.Common.Dtos;

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
            var query = @"exec UnlockMachine @userId, @machineId";

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@userId", Value = userId },
                new SqlParameter { ParameterName = "@machineId", Value = machineId }
            };

            await Context.Database.ExecuteSqlRawAsync(query, parms);
        }

        public async Task UnlockMachinesAsync()
        {
            var query = "exec UnlockMachines";
            await Context.Database.ExecuteSqlRawAsync(query);
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

        public MachinesStateDto GetMachinesState()
        {
             var result = Context.LoadStoredProc("dbo.GetMachinesState")
               .ExecuteStoredProc<MachinesStateDto>()
               .Single();

            return result;
        }
    }
}
