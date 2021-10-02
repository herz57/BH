using BH.Common.Enums;
using BH.Domain.Entities;
using BH.Domain.Interfaces;
using Domain;
using Domain.Repositories.Base;
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
    }
}
