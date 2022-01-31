using MassTransit;
using Messages;
using Microsoft.Extensions.Caching.Distributed;
using ProduceDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisMasstransitConsumer.Redis;

namespace RedisMasstransitConsumer.Consumers
{
    public class ProducerMessageConsumer : IConsumer<ProduceMessage>
    {
        readonly ILogger<ProducerMessageConsumer> _logger;
        private readonly ProduceDbContext _context;
        private readonly IDistributedCache _cache;

        public ProducerMessageConsumer(ILogger<ProducerMessageConsumer> logger, ProduceDbContext context, IDistributedCache cache)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
        }

        public async Task Consume(ConsumeContext<ProduceMessage> context)
        {
            _logger.LogInformation("Received Text: {Text}", context.Message.Text);
            var produceMessage = context.Message;
            var record = produceMessage.Text;
            var redisCache = await _cache.GetRecordAsync<ProduceMessage>(produceMessage.Text);
            if (redisCache == null)
            {
                await _cache.SetRecordAsync<ProduceMessage>(record, produceMessage);
                _context.Records.Add(new ProduceDb.Entities.Record()
                {
                    AddedDate = DateTime.UtcNow,
                    Text = context.Message.Text
                });
                await _context.SaveChangesAsync();
                _logger.LogInformation("Saved: {Text}", context.Message.Text);

            } else
            {
                _logger.LogInformation("Skipped: {Text}", context.Message.Text);
            }
        }
    }
}
