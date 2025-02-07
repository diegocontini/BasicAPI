using System.Diagnostics;
using System.Reflection;

namespace OmniApiServico
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);
                var t = new Thread(AbrirApi);
                t.Start();
                await Task.Delay(60000, stoppingToken);
            }
        }
        private void AbrirApi()
        {
            var p = Process.GetProcessesByName("BasicApi");
            if (p.Length == 0)
            {
                Task.Delay(10000);
                _logger.LogInformation("Worker running at: {time} - Start BasicApi", DateTimeOffset.Now);

                //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var path = "c:\\Program Files\\BasicApi";

                Process.Start(path + "\\BasicApi.exe" ?? "");
            }
        }
    }
}
