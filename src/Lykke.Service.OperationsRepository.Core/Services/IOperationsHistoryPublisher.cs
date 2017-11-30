using Lykke.Service.OperationsRepository.Contract;
using System.Threading.Tasks;
using Autofac;
using Common;

namespace Lykke.Service.OperationsRepository.Core
{
    public interface IOperationsHistoryPublisher: IStartable, IStopable
    {
        Task PublishAsync(OperationsHistoryMessage message);
    }
}