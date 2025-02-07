using OmniApiServico;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "svcBasicApi";
});

builder.Services.AddHostedService<Worker>();
//builder.Services.AddLogging();
var host = builder.Build();
host.Run();
