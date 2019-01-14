using System;
using Autofac;
using JetBrains.Annotations;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.CashOperations;

namespace Lykke.Service.OperationsRepository.Client
{
    [PublicAPI]
    public static class AutofacExtension
    {
        public static void RegisterOperationsRepositoryClients(this ContainerBuilder builder, string serviceUrl, int timeoutInSeconds)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterInstance(new CashOperationsRepositoryClient(serviceUrl, timeoutInSeconds)).As<ICashOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new CashOutAttemptRepositoryClient(serviceUrl, timeoutInSeconds)).As<ICashOutAttemptOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new TradeOperationsRepositoryClient(serviceUrl, timeoutInSeconds)).As<ITradeOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new TransferOperationsRepositoryClient(serviceUrl, timeoutInSeconds)).As<ITransferOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new LimitTradeEventsRepositoryClient(serviceUrl, timeoutInSeconds)).As<ILimitTradeEventsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new LimitOrdersRepositoryClient(serviceUrl, timeoutInSeconds)).As<ILimitOrdersRepositoryClient>().SingleInstance();
        }

        public static void RegisterOperationsRepositoryClients(this ContainerBuilder builder, OperationsRepositoryServiceClientSettings settings)
        {
            builder.RegisterOperationsRepositoryClients(settings.ServiceUrl, settings.RequestTimeout);
        }
    }
}
