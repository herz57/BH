using BH.Domain;
using Microsoft.EntityFrameworkCore;
using BH.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace BH.Infrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly BhDbContext _context;

        public LoggerService(BhDbContext context)
        {
            _context = context;
        }

        public void LogInfo(string message, 
            int? entityId, 
            string entityDiscriminator,
            string userId)
        {
            LogAsync(message, entityId, entityDiscriminator, userId, null, LogLevel.Information);
        }

        public void LogWarning(string message,
            int? entityId,
            string entityDiscriminator,
            string userId)
        {
            LogAsync(message, entityId, entityDiscriminator, userId, null, LogLevel.Warning);
        }

        public void LogError(string message, 
            string exception, 
            int? entityId, 
            string entityDiscriminator,
            string userId)
        {
            LogAsync(message, entityId, entityDiscriminator, userId, exception, LogLevel.Error);
        }

        private void LogAsync(string message,
            int? entityId,
            string entityDiscriminator,
            string userId,
            string exception,
            LogLevel level)
        {
            var query = @"insert into dbo.Logs (Message, EntityId, EntityDiscriminator, UserId, Exception, Level)
                values (@message, @entityId, @entityDiscriminator, @userId, @exception, @level)";

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@message", Value = message},
                new SqlParameter { ParameterName = "@entityId", Value = (object)entityId ?? DBNull.Value },
                new SqlParameter { ParameterName = "@entityDiscriminator", Value = (object)entityDiscriminator ?? DBNull.Value },
                new SqlParameter { ParameterName = "@userId", Value = (object)userId ?? DBNull.Value },
                new SqlParameter { ParameterName = "@exception", Value = (object)exception ?? DBNull.Value },
                new SqlParameter { ParameterName = "@level", Value = level },
            };

            _context.Database.ExecuteSqlRaw(query, parms);
        }
    }
}
