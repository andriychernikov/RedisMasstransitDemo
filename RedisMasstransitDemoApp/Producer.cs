
using MassTransit;
using Messages;
using Microsoft.Extensions.Hosting;

public class Producer : BackgroundService
{
    readonly IBus _bus;

    public Producer(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = new ProduceMessage { Text = $"The time is {DateTimeOffset.Now}" };
            Console.WriteLine($"Produce message {message}");
            await _bus.Publish(message);
            await Task.Delay(1000, stoppingToken);
        }
    }
}