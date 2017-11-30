using Lykke.Service.OperationsRepository.Core.Domain.Health;
using System.Collections.Generic;

namespace Lykke.Service.OperationsRepository.Core
{
    public interface IHealthService
    {
        string GetHealthViolationMessage();
        IEnumerable<HealthIssue> GetHealthIssues();
    }
}