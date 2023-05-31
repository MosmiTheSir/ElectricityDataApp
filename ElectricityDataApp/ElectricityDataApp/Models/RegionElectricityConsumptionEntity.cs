using System.ComponentModel.DataAnnotations;

namespace ElectricityDataApp.Models;

public class RegionElectricityConsumptionEntity
{
    [Key]
    public int Id { get; set; }
    public string? ChainRegion { get; set; }
    public double SumPPlus { get; set; }
    public double SumPMinus { get; set; }
}