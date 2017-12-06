using System;
using Autofac;
using Common.Log;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.CashOperations;
using Lykke.Service.OperationsRepository.Client.Exchange;
using Lykke.Service.OperationsRepository.Client.Abstractions.Exchange;

namespace Lykke.Service.OperationsRepository.Client
{
    public static class AutofacExtension
    {
        public static void RegisterOperationsRepositoryClients(this ContainerBuilder builder, string serviceUrl, ILog log, int timeout)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterInstance(new CashOperationsRepositoryClient(serviceUrl, log, timeout)).As<ICashOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new CashOutAttemptRepositoryClient(serviceUrl, log, timeout)).As<ICashOutAttemptOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new TradeOperationsRepositoryClient(serviceUrl, log, timeout)).As<ITradeOperationsRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new TransferOperationsRepositoryClient(serviceUrl, log, timeout)).As<ITransferOperationsRepositoryClient>().SingleInstance();

            builder.RegisterInstance(new LimitOrdersRepositoryClient(serviceUrl, log, timeout)).As<ILimitOrdersRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new MarketOrdersRepositoryClient(serviceUrl, log, timeout)).As<IMarketOrdersRepositoryClient>().SingleInstance();
            builder.RegisterInstance(new LimitTradeEventsRepositoryClient(serviceUrl, log, timeout)).As<ILimitTradeEventsRepositoryClient>().SingleInstance();
        }
    }
}
