using ElectricityDataApp.Models;
using ElectricityDataApp.Persistence.Repository.Interfaces;
using ElectricityDataApp.Persistence.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElectricityDataApp.Persistence.Repository;

public class RegionalElectricityConsumption : IRegionalElectricityConsumption
{
    private readonly DBContext _dbContext;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RegionalElectricityConsumption(DBContext dbContext, IServiceScopeFactory serviceScopeFactory)
    {
        _dbContext = dbContext;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StoreAggregatedData(List<RegionElectricityConsumptionEntity> data)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DBContext>();
            var newData = new List<RegionElectricityConsumptionEntity>();
            foreach (var entity in data)
            {
                var existingData =
                    await context.RegionElectricityConsumptions.Where(ed => ed.ChainRegion == entity.ChainRegion).ToListAsync();
                var oldData =
                    existingData.Where(ed => ed.SumPPlus != entity.SumPPlus || ed.SumPMinus != entity.SumPMinus).ToList();

                if (existingData.Count > 1)
                {
                    context.RemoveRange(existingData);
                    if (!newData.Contains(entity))
                    {
                        newData.Add(entity);
                    }

                }
                else if (existingData.Count == 0)
                {
                    if (!newData.Contains(entity))
                    {
                        newData.Add(entity);
                    }
                }
                else if (oldData.Any())
                {
                    context.RemoveRange(oldData);
                    if (!newData.Contains(entity))
                    {
                        newData.Add(entity);
                    }
                }
            }

            context.AttachRange(newData);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<RegionElectricityConsumptionEntity>> GetAllAsync()
    {
        var allRecords = await _dbContext.RegionElectricityConsumptions.ToListAsync();
        return allRecords;
    }
}