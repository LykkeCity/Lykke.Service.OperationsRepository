using Common.Log;
using Lykke.Service.OperationsRepository.Core;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Services
{
    public class StartupManager : IStartupManager
    {
        private readonly ILog _log;

        public StartupManager(ILog log)
        {
            _log = log;
        }

        public async Task StartAsync()
        {
            // TODO: Implement your startup logic here. Good idea is to log every step

            await Task.CompletedTask;
        }
    }
}