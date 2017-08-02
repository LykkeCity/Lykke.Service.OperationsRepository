using System;
using Autofac;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.CashOperations;

namespace Lykke.Service.OperationsRepository.Client
{
    public static class AutofacExtension
    {
        public static void RegisterOperationsRepositoryClients(this ContainerBuilder builder, string serviceUrl)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterInstance(new CashOperationsRepositoryClient(serviceUrl)).As<ICashOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new CashOutAttemptRepositoryClient(serviceUrl)).As<ICashOutAttemptOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new TradeOperationsRepositoryClient(serviceUrl)).As<ITradeOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new TransferOperationsRepositoryClient(serviceUrl)).As<ITransferOperationsRepositoryClient>().SingleInstance();
        }
    }
}
