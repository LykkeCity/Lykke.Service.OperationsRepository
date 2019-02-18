using Autofac;
using AzureStorage.Tables;
using AzureStorage.Tables.Templates.Index;
using Common.Log;
using Lykke.Service.OperationsRepository.AzureRepositories.CashOperations;
using Lykke.Service.OperationsRepository.Core;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Services;
using Lykke.SettingsReader;

namespace Lykke.Service.OperationsRepository.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<OperationsRepositorySettings> _settings;
        private readonly ILog _log;

        public ServiceModule(IReloadingManager<OperationsRepositorySettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            RegisterAzureRepositories(builder);
        }

        private void RegisterAzureRepositories(ContainerBuilder builder)
        {
            builder.RegisterInstance<ICashOperationsRepository>(new CashOperationsRepository(
                AzureTableStorage<CashInOutOperationEntity>.Create(
                    _settings.ConnectionString(x => x.Db.CashOperationsConnString), "OperationsCash", _log),
                AzureTableStorage<AzureIndex>.Create(_settings.ConnectionString(x => x.Db.CashOperationsConnString),
                    "OperationsCash", _log)));

            builder.RegisterInstance<IClientTradesRepository>(new ClientTradesRepository(
                AzureTableStorage<ClientTradeEntity>.Create(_settings.ConnectionString(x => x.Db.ClientTradesConnString),
                    "Trades", _log)));

            builder.RegisterInstance<ITransferEventsRepository>(
                new TransferEventsRepository(
                    AzureTableStorage<TransferEventEntity>.Create(_settings.ConnectionString(x => x.Db.TransferConnString), "Transfers", _log),
                    AzureTableStorage<AzureIndex>.Create(_settings.ConnectionString(x => x.Db.TransferConnString), "Transfers", _log)));

            builder.RegisterInstance<ICashOutAttemptRepository>(new CashOutAttemptRepository(
                AzureTableStorage<CashOutAttemptEntity>.Create(
                    _settings.ConnectionString(x => x.Db.CashOutAttemptConnString), "CashOutAttempt", _log)));

            builder.RegisterInstance<ILimitTradeEventsRepository>(
                new LimitTradeEventsRepository(
                    AzureTableStorage<LimitTradeEventEntity>.Create(_settings.ConnectionString(x => x.Db.LimitTradesConnString), "LimitTradeEvents", _log)));

            builder.RegisterInstance<ILimitOrdersRepository>(new LimitOrdersRepository(
                AzureTableStorage<LimitOrderEntity>.Create(
                    _settings.ConnectionString(x => x.Db.LimitOrdersConnString), "LimitOrders", _log)));
        }
    }
}
