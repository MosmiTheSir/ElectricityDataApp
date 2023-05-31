using ElectricityDataApp.Models;

namespace ElectricityDataApp.Persistence.Repository.Interfaces;

public interface IRegionalElectricityConsumption
{
    Task StoreAggregatedData(List<RegionElectricityConsumptionEntity> data);
    Task<List<RegionElectricityConsumptionEntity>> GetAllAsync();
}