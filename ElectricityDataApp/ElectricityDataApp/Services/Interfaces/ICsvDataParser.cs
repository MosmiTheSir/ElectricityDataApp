using ElectricityDataApp.Models;

namespace ElectricityDataApp.Services.Interfaces;

public interface ICsvDataParser
{
    public List<ElectricityData> ParseData(string? csvData);
}