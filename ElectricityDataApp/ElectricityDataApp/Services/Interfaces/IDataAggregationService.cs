using ElectricityDataApp.Models;

namespace ElectricityDataApp.Services.Interfaces;

public interface IDataAggregationService
{
    public List<RegionElectricityConsumptionEntity> AggregateData<T>(List<T> data);
}