using ElectricityDataApp.Models;
using ElectricityDataApp.Persistence.Repository.Interfaces;
using ElectricityDataApp.Services.Interfaces;

namespace ElectricityDataApp.Services;

public class DataService : IDataService
{
    private readonly AppSettingsDto _appSettingsDto;
    private readonly ILogger<DataService> _logger;
    private static readonly HttpClient HttpClient = new();
    private readonly IRegionalElectricityConsumption _regionalElectricityConsumption;

    public DataService(ILogger<DataService> logger, IConfiguration configuration, IRegionalElectricityConsumption regionalElectricityConsumption)
    {
        _logger = logger;
        var appSettingsSection = configuration.GetSection("AppSettings");
        _appSettingsDto = appSettingsSection.Get<AppSettingsDto>()!;
        _regionalElectricityConsumption = regionalElectricityConsumption;
    }

    public async void DownloadData()
    {
        var urls = _appSettingsDto.Urls;
        var data = new List<string?>();
        for (int i = 0; i < urls.Count; i++)
        {
            data.Add(await DownloadDataFromUrlAsync(urls[i]));
            _logger.LogInformation($"Downloaded file: {i+1}/{urls.Count}");
        }

        var csvDataParser = new CsvDataParser();
        var dataAggregationService = new DataAggregationService();
        var records = new List<RegionElectricityConsumptionEntity>();
        //token
        _logger.LogInformation("Started processing");
        var stoppingToken = CancellationToken.None;
        await Parallel.ForEachAsync(data, stoppingToken, async (csvFile, ct) =>
        {
            _logger.LogInformation("Parsing .csv file");
            var electricityData = csvDataParser.ParseData(csvFile);
            _logger.LogInformation("Aggregating data");
            var regionElectricityConsumptionEntities = dataAggregationService.AggregateData(electricityData);

            foreach (var regionElectricityConsumptionEntity in regionElectricityConsumptionEntities)
            {
                records.Add(regionElectricityConsumptionEntity);
            }
        });

        var aggregatedRegionElectricityConsumptionEntities = dataAggregationService.AggregateData(records);
        _logger.LogInformation("Storing aggregated data");
        await _regionalElectricityConsumption.StoreAggregatedData(aggregatedRegionElectricityConsumptionEntities);

    }

    public async Task<string?> DownloadDataFromUrlAsync(string url)
    {
        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (Exception e)
        {
           _logger.LogError(e, $"Error downloading data from URL {url}");
           return null;
        }
    }
}