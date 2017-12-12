using System.Threading.Tasks;
using Autofac;
using Common;
using Lykke.Service.OperationsRepository.Contract.History;

namespace Lykke.Service.OperationsRepository.Core
{
    public interface IOperationsHistoryPublisher: IStartable, IStopable
    {
        Task PublishAsync(OperationsHistoryMessage message);
    }
}