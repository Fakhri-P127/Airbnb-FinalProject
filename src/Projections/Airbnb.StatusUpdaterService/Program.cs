using Airbnb.StatusUpdaterService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<StatusUpdateWorker>();
    })
    .Build();

await host.RunAsync();
