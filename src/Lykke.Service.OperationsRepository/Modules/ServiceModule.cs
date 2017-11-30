using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureStorage.Tables;
using AzureStorage.Tables.Templates.Index;
using Common.Log;
using Lykke.Service.OperationsRepository.AzureRepositories.CashOperations;
using Lykke.Service.OperationsRepository.Core;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.RabbitPublishers;
using Lykke.Service.OperationsRepository.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.OperationsRepository.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<OperationsRepositorySettings> _settings;
        private readonly ILog _log;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public ServiceModule(IReloadingManager<OperationsRepositorySettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;

            _services = new ServiceCollection();
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

            RegisterRabbitMqPublishers(builder);

            builder.Populate(_services);
        }

        private void RegisterRabbitMqPublishers(ContainerBuilder builder)
        {
            // TODO: You should register each publisher in DI container as publisher specific interface and as IStartable,
            // as singleton and do not autoactivate it

            builder.RegisterType<OperationsHistoryPublisher>()
                .As<IOperationsHistoryPublisher>()
                .As<IStartable>()
                .SingleInstance()
                .WithParameter(TypedParameter.From(_settings.CurrentValue.Rabbit));
        }

        private void RegisterAzureRepositories(ContainerBuilder builder)
        {
            builder.RegisterInstance<ICashOperationsRepository>(new CashOperationsRepository(
                AzureTableStorage<CashInOutOperationEntity>.Create(
                    _settings.ConnectionString(x => x.Db.RepoConnectionString), "OperationsCash", _log),
                AzureTableStorage<AzureIndex>.Create(_settings.ConnectionString(x => x.Db.RepoConnectionString),
                    "OperationsCash", _log)));

            builder.RegisterInstance<IClientTradesRepository>(new ClientTradesRepository(
                AzureTableStorage<ClientTradeEntity>.Create(_settings.ConnectionString(x => x.Db.RepoConnectionString),
                    "Trades", _log)));

            builder.RegisterInstance<ITransferEventsRepository>(
                new TransferEventsRepository(
                    AzureTableStorage<TransferEventEntity>.Create(_settings.ConnectionString(x => x.Db.RepoConnectionString), "Transfers", _log),
                    AzureTableStorage<AzureIndex>.Create(_settings.ConnectionString(x => x.Db.RepoConnectionString), "Transfers", _log)));

            builder.RegisterInstance<ICashOutAttemptRepository>(new CashOutAttemptRepository(
                AzureTableStorage<CashOutAttemptEntity>.Create(
                    _settings.ConnectionString(x => x.Db.RepoConnectionString), "CashOutAttempt", _log)));
        }
    }
}
