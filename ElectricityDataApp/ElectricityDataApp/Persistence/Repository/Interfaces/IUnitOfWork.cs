namespace ElectricityDataApp.Persistence.Repository.Interfaces;
public interface IUnitOfWork
{
    IRegionalElectricityConsumption RegionElectricityConsumptions { get; }

    Task<int> SaveChangesAsync();
}