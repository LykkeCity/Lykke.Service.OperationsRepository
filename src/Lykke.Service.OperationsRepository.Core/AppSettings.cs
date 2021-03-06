﻿namespace Lykke.Service.OperationsRepository.Core
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
        public string ClientTradesConnString { get; set; }
        public string CashOperationsConnString { get; set; }
        public string TransferConnString { get; set; }
        public string CashOutAttemptConnString { get; set; }
        public string LimitTradesConnString { get; set; }
        public string LimitOrdersConnString { set; get; }
        public string ClientPersonalInfoConnString { get; set; }
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
