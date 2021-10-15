using BH.Common.Dtos;
using BH.Common.Enums;
using BH.Domain.Interfaces;
using BH.Infrastructure.Exceptions;
using BH.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MachinesService : IMachinesService
    {
        private readonly IMachinesRepository _machinesRepository;
        private readonly ITicketsRepository _ticketsRepository;

        public MachinesService(IMachinesRepository machinesRepository,
            ITicketsRepository ticketsRepository
            )
        {
            _machinesRepository = machinesRepository;
            _ticketsRepository = ticketsRepository;
        }

        public async Task<LockMachineDto> LockMachineAsync(string userId, DomainType domainType)
        {
            try
            {
                var machineId = await _machinesRepository.LockMachineAsync(userId, domainType);
                return new LockMachineDto
                {
                    MachineId = machineId,
                    AvailableCosts = await _ticketsRepository.GetAvailableMachineCosts(machineId),
                };
            }
            catch (SqlException ex)
            {
                throw new ApiHandledException(ex.Message);
            }
        }

        public async Task UnlockMachineAsync(int machineId, string userId)
        {
            await _machinesRepository.UnlockMachineAsync(machineId, userId);
        }
    }
}