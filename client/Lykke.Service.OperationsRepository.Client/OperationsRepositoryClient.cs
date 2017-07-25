using System;
using Common.Log;

namespace Lykke.Service.OperationsRepository.Client
{
    public class OperationsRepositoryClient : IOperationsRepositoryClient, IDisposable
    {
        private readonly ILog _log;

        public OperationsRepositoryClient(string serviceUrl, ILog log)
        {
            _log = log;
        }

        public void Dispose()
        {
            //if (_service == null)
            //    return;
            //_service.Dispose();
            //_service = null;
        }
    }
}
