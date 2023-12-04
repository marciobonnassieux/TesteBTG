using SqsMessageConsummer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<SqsService, SqsService>();
        services.AddSingleton<DynamoService, DynamoService>();
        services.AddHostedService<Worker>();
        services.AddSingleton<ILoggerService, LoggerService>();

    })
    .Build();

await host.RunAsync();
