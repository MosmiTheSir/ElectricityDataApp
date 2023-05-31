using ElectricityDataApp.Models;
using ElectricityDataApp.Persistence.Repository;
using ElectricityDataApp.Persistence.Repository.Interfaces;
using ElectricityDataApp.Persistence.Repository.UnitOfWork;
using ElectricityDataApp.Services;
using ElectricityDataApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectricityDataApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        Logger = loggerFactory.CreateLogger<Startup>();
    }

    public IConfiguration Configuration { get; }
    public ILogger Logger { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DBContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRegionalElectricityConsumption, RegionalElectricityConsumption>();
        services.AddScoped<IDataService, DataService>();
        services.AddScoped<ICsvDataParser, CsvDataParser>();
        services.AddScoped<IDataAggregationService, DataAggregationService>();
        services.Configure<AppSettingsDto>(Configuration.GetSection("AppSettings"));
        services.AddControllers();
        services.AddLogging();
        services.AddHostedService<DataDownload>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DBContext context)
    {
        context.Database.Migrate();
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        context.Dispose();
    }
}