using ElectricityDataApp.Models;
using ElectricityDataApp.Services.Interfaces;

namespace ElectricityDataApp.Services;

public class DataAggregationService : IDataAggregationService
{
    public List<RegionElectricityConsumptionEntity> AggregateData<T>(List<T> data)
    {
        if (data is List<ElectricityData> electricityData)
        {
            return electricityData
                .GroupBy(d => d.ChainRegion)
                .Select(g => new RegionElectricityConsumptionEntity
                {
                    ChainRegion = g.Key,
                    SumPPlus = g.Sum(d => d.PPlus),
                    SumPMinus = g.Sum(d => d.PMinus)
                }).ToList();
        }
        else if (data is List<RegionElectricityConsumptionEntity> regionElectricityData)
        {
            return regionElectricityData
                .GroupBy(d => d.ChainRegion)
                .Select(g => new RegionElectricityConsumptionEntity
                {
                    ChainRegion = g.Key,
                    SumPPlus = g.Sum(d => d.SumPPlus),
                    SumPMinus = g.Sum(d => d.SumPMinus)
                }).ToList();
        }
        else
        {
            return null;
        }
    }
}