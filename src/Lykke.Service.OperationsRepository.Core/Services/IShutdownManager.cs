using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Core
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}