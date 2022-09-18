using StatusUpdaterService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ReservationStatusUpdateWorker>();
    })
    .Build();

await host.RunAsync();
