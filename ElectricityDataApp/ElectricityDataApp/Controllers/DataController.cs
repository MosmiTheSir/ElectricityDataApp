using ElectricityDataApp.Models;
using ElectricityDataApp.Persistence.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityDataApp.Controllers;

[Route("api/")]
[ApiController]
public class DataController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DataController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [Route("GetAggregate")]
    [HttpGet]
    public async Task<IEnumerable<RegionElectricityConsumptionEntity>> Get()
    {
        var items = await _unitOfWork.RegionElectricityConsumptions.GetAllAsync();
        return items;
    }
}