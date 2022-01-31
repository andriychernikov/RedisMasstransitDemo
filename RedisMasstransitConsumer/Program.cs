using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProduceDb;
using RedisMasstransitConsumer.Consumers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ProducerMessageConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMassTransitHostedService(true);

        services.AddDbContext<ProduceDbContext>(options =>
                options.UseSqlServer(
                    "Server=(local);Initial Catalog=masstransit;Trusted_Connection=True;MultipleActiveResultSets=true",
                    b => b.MigrationsAssembly(typeof(ProduceDbContext).Assembly.FullName)));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:5002";
            options.InstanceName = "RedisDemo_";
        });
    })
    .Build();

await host.RunAsync();