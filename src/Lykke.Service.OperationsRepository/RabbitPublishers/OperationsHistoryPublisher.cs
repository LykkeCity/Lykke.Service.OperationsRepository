﻿using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.Core;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.OperationsRepository.Contract.History;
using Microsoft.CodeAnalysis.Operations;

namespace Lykke.Service.OperationsRepository.RabbitPublishers
{
    public class OperationsHistoryPublisher : IOperationsHistoryPublisher
    {
        private readonly ILog _log;
        private readonly RabbitMqSettings _rabbitSettings;

        private RabbitMqPublisher<OperationsHistoryMessage> _publisher;

        public OperationsHistoryPublisher(ILog log, RabbitMqSettings rabbitSettings)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _rabbitSettings = rabbitSettings ?? throw new ArgumentNullException(nameof(rabbitSettings));
        }

        public void Start()
        {
            // NOTE: Read https://github.com/LykkeCity/Lykke.RabbitMqDotNetBroker/blob/master/README.md to learn
            // about RabbitMq subscriber configuration

            var settings = RabbitMqSubscriptionSettings
                .ForPublisher(_rabbitSettings.ConnectionString, _rabbitSettings.ExchangeName);
            // TODO: Make additional configuration, using fluent API here:
            // ex: .MakeDurable()

            _publisher = new RabbitMqPublisher<OperationsHistoryMessage>(settings)
                .SetSerializer(new JsonMessageSerializer<OperationsHistoryMessage>())
                .SetPublishStrategy(new DefaultFanoutPublishStrategy(settings))
                .PublishSynchronously()
                .SetLogger(_log)
                .Start();
        }

        public void Dispose()
        {
            _publisher?.Dispose();
        }

        public void Stop()
        {
            _publisher?.Stop();
        }

        public async Task PublishAsync(OperationsHistoryMessage message)
        {
            try
            {
                await _publisher.ProduceAsync(message);
            }
            catch (Exception e)
            {
                _log.WriteError(nameof(PublishAsync), message, e);
                throw;
            }
        }
    }
}