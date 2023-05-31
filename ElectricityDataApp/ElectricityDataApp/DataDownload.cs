using ElectricityDataApp.Services.Interfaces;

namespace ElectricityDataApp;

public class DataDownload : BackgroundService
{
    private readonly ILogger<DataDownload> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public DataDownload(IServiceScopeFactory scopeFactory, ILogger<DataDownload> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            _logger.LogInformation("Started downloading");
            var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();
            dataService.DownloadData();
            return Task.CompletedTask;
        }
    }

}