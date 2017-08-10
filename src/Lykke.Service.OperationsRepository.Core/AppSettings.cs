namespace Lykke.Service.OperationsRepository.Core
{
    public class AppSettings
    {
        public OperationsRepositorySettings OperationsRepositoryService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }

    public class OperationsRepositorySettings
    {
        public DbSettings Db { get; set; }
    }

    public class DbSettings
    {
        public string LogsConnString { get; set; }
        public string RepoConnectionString { get; set; }
        public string HistoryConnString { get; set; }
    }

    public class SlackNotificationsSettings
    {
        public AzureQueueSettings AzureQueue { get; set; }

        public int ThrottlingLimitSeconds { get; set; }
    }

    public class AzureQueueSettings
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
