using Common.Log;
using Lykke.Service.OperationsRepository.Core;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Services
{
    public class ShutdownManager : IShutdownManager
    {
        private readonly ILog _log;

        public ShutdownManager(ILog log)
        {
            _log = log;
        }

        public async Task StopAsync()
        {
            // TODO: Implement your shutdown logic here. Good idea is to log every step

            await Task.CompletedTask;
        }
    }
}