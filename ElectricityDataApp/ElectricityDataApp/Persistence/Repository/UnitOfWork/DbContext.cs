using ElectricityDataApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricityDataApp.Persistence.Repository.UnitOfWork;

public class DBContext : DbContext
{
    public DbSet<RegionElectricityConsumptionEntity> RegionElectricityConsumptions { get; set; }
    private AppSettingsDto _appSettingsDto;
    private readonly IConfiguration _configuration;

    public DBContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var appSettingsSection = _configuration.GetSection("ConnectionStrings");
        _appSettingsDto = appSettingsSection.Get<AppSettingsDto>();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_appSettingsDto.DefaultConnection);
    }
}