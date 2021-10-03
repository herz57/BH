using BH.Domain.Entities;
using BH.Domain.Interfaces;
using Domain;
using Domain.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BH.Domain.Repositories
{
    public class ProfilesRepository : BaseRepository<Profile>, IProfilesRepository
    {
        public ProfilesRepository(BhDbContext context) : base(context) { }

        public async Task<long> GetBalanceAsync(int profileId)
        {
            var query = "select p.Balance from dbo.Profiles p where p.ProfileId = @profileId";
            var result = await Context.Profiles
                .FromSqlRaw(query, new SqlParameter("@profileId", profileId))
                .Select(p => p.Balance)
                .SingleOrDefaultAsync();

            return result;
        }
    }
}
