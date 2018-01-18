using System;
using Autofac;
using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.CashOperations;

namespace Lykke.Service.OperationsRepository.Client
{
    public static class AutofacExtension
    {
        public static void RegisterOperationsRepositoryClients(this ContainerBuilder builder, string serviceUrl, ILog log, int timeoutInSeconds)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterInstance(new CashOperationsRepositoryClient(serviceUrl, log, timeoutInSeconds)).As<ICashOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new CashOutAttemptRepositoryClient(serviceUrl, log, timeoutInSeconds)).As<ICashOutAttemptOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new TradeOperationsRepositoryClient(serviceUrl, log, timeoutInSeconds)).As<ITradeOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new TransferOperationsRepositoryClient(serviceUrl, log, timeoutInSeconds)).As<ITransferOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new LimitTradeEventsRepositoryClient(serviceUrl, log, timeoutInSeconds)).As<ILimitTradeEventsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new LimitOrdersRepositoryClient(serviceUrl, log, timeoutInSeconds)).As<ILimitOrdersRepositoryClient>().SingleInstance();
        }

        public static void RegisterOperationsRepositoryClients(this ContainerBuilder builder,
            OperationsRepositoryServiceClientSettings settings, ILog log)
        {
            builder.RegisterOperationsRepositoryClients(settings.ServiceUrl, log, settings.RequestTimeout);
        }
    }
}
