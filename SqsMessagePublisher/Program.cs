using SqsMessagePublisher;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddSingleton<SqsService, SqsService>();

var host = builder.Build();
host.Run();
