using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface ILoggerService
    {
        void LogInfo(string message,
            int? entityId,
            string entityDiscriminator,
            string userId);

        void LogWarning(string message,
            int? entityId,
            string entityDiscriminator,
            string userId);

        void LogError(string message,
            string exception,
            int? entityId,
            string entityDiscriminator,
            string userId);
    }
}
