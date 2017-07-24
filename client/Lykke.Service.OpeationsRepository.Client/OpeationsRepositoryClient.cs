using System;
using Common.Log;

namespace Lykke.Service.OpeationsRepository.Client
{
    public class OpeationsRepositoryClient : IOpeationsRepositoryClient, IDisposable
    {
        private readonly ILog _log;

        public OpeationsRepositoryClient(string serviceUrl, ILog log)
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
