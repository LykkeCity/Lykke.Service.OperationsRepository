using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureStorage.Tables;
using AzureStorage.Tables.Templates.Index;
using Common.Log;
using Lykke.Service.OperationsHistory.HistoryWriter.Abstractions;
using Lykke.Service.OperationsHistory.HistoryWriter.Implementation;
using Lykke.Service.OperationsRepository.AzureRepositories.CashOperations;
using Lykke.Service.OperationsRepository.AzureRepositories.Entities;
using Lykke.Service.OperationsRepository.AzureRepositories.Exchange;
using Lykke.Service.OperationsRepository.Core;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Core.Exchange;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.OperationsRepository.Modules
{
    public class ServiceModule : Module
    {
        private readonly OperationsRepositorySettings _settings;
        private readonly ILog _log;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public ServiceModule(OperationsRepositorySettings settings, ILog log)
        {
            _settings = settings;
            _log = log;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_settings)
                .SingleInstance();

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterInstance<ICashOperationsRepository>(
                new CashOperationsRepository(
                    new AzureTableStorage<CashInOutOperationEntity>(_settings.Db.RepoConnectionString,
                        "OperationsCash", _log),
                    new AzureTableStorage<AzureIndex>(_settings.Db.RepoConnectionString, "OperationsCash", _log)));

            builder.RegisterInstance<IClientTradesRepository>(
                new ClientTradesRepository(
                    new AzureTableStorage<ClientTradeEntity>(_settings.Db.RepoConnectionString, "Trades", _log)));

            builder.RegisterInstance<ILimitTradeEventsRepository>(
               new LimitTradeEventsRepository(
                   new AzureTableStorage<LimitTradeEventEntity>(_settings.Db.RepoConnectionString, "LimitTradeEvents", _log)));

            builder.RegisterInstance<ILimitOrdersRepository>(
               new LimitOrdersRepository(
                   new AzureTableStorage<LimitOrderEntity>(_settings.Db.HMarketOrdersConnString, "LimitOrders", _log)));

            builder.RegisterInstance<IMarketOrdersRepository>(
            new MarketOrdersRepository(
                new AzureTableStorage<MarketOrderEntity>(_settings.Db.HMarketOrdersConnString, "MarketOrders", _log)));

            builder.RegisterInstance<ITransferEventsRepository>(
               new TransferEventsRepository(
                   new AzureTableStorage<TransferEventEntity>(_settings.Db.RepoConnectionString, "Transfers", _log),
                   new AzureTableStorage<AzureIndex>(_settings.Db.RepoConnectionString, "Transfers", _log)));

            builder.RegisterInstance<ICashOutAttemptRepository>(
                new CashOutAttemptRepository(
                    new AzureTableStorage<CashOutAttemptEntity>(_settings.Db.RepoConnectionString, "CashOutAttempt",
                        _log)));

            builder.RegisterInstance<IHistoryWriter>(new HistoryWriter(_settings.Db.HistoryConnString, _log));

            builder.Populate(_services);
        }
    }
}
