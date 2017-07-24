namespace Lykke.Service.OpeationsRepository.Core
{
    public class AppSettings
    {
        public OpeationsRepositorySettings OpeationsRepositoryService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }

    public class OpeationsRepositorySettings
    {
        public DbSettings Db { get; set; }
    }

    public class DbSettings
    {
        public string LogsConnString { get; set; }
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
