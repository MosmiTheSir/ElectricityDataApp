using ElectricityDataApp.Persistence.Repository.Interfaces;

namespace ElectricityDataApp.Persistence.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private DBContext _dbContext;
    public IRegionalElectricityConsumption RegionElectricityConsumptions { get; }
    public UnitOfWork(DBContext dbContext, IRegionalElectricityConsumption regionalElectricityConsumption)
    {
        _dbContext = dbContext;
        RegionElectricityConsumptions = regionalElectricityConsumption;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}