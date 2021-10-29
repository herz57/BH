using BH.Common.Dtos;
using BH.Common.Enums;
using BH.Domain.Entities;
using BH.Domain.Interfaces;
using BH.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MachinesService : IMachinesService
    {
        private readonly IMachinesRepository _machinesRepository;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly ILoggerService _logger;

        public MachinesService(IMachinesRepository machinesRepository,
            ITicketsRepository ticketsRepository,
            ILoggerService logger
            )
        {
            _machinesRepository = machinesRepository;
            _ticketsRepository = ticketsRepository;
            _logger = logger;
        }

        public async Task<LockMachineDto> LockMachineAsync(string userId, DomainType domainType)
        {
            var machineId = await _machinesRepository.LockMachineAsync(userId, domainType);
            _logger.LogInfo($"Machine {machineId} has been locked", machineId, nameof(Machine), userId);

            return new LockMachineDto
            {
                MachineId = machineId,
                AvailableCosts = await _machinesRepository.GetAvailableMachineCosts(machineId),
            };
        }

        public async Task UnlockMachineAsync(int machineId, string userId)
        {
            await _machinesRepository.UnlockMachineAsync(machineId, userId);
            _logger.LogInfo($"Machine {machineId} has been unlocked", machineId, nameof(Machine), userId);
        }

        public MachinesStateDto GetMachinesState()
        {
           return _machinesRepository.GetMachinesState();
        }
    }
}