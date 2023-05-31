using ElectricityDataApp.Models;
using ElectricityDataApp.Services;
using ElectricityDataApp.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace ElectricityDataApp.Tests
{
    [TestFixture]
    public class DataAggregationServiceTests
    {
        private IDataAggregationService _dataAggregationService;

        [SetUp]
        public void SetUp()
        {
            _dataAggregationService = new DataAggregationService();
        }

        [Test]
        public void AggregateData_CorrectDataAggregation_ElectricityData()
        {
            // Arrange
            var electricityData = new List<ElectricityData>
            {
                new ElectricityData { ChainRegion = "Region A", PPlus = 10.5, PMinus = 5.2 },
                new ElectricityData { ChainRegion = "Region B", PPlus = 8.3, PMinus = 3.7 },
                new ElectricityData { ChainRegion = "Region A", PPlus = 6.1, PMinus = 2.9 }
            };

            // Act
            var result = _dataAggregationService.AggregateData(electricityData);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var regionA = result.Find(r => r.ChainRegion == "Region A");
            Assert.IsNotNull(regionA);
            Assert.AreEqual(16.6, Math.Round(regionA.SumPPlus, 1));
            Assert.AreEqual(8.1, Math.Round(regionA.SumPMinus, 1));

            var regionB = result.Find(r => r.ChainRegion == "Region B");
            Assert.IsNotNull(regionB);
            Assert.AreEqual(8.3, Math.Round(regionB.SumPPlus, 1));
            Assert.AreEqual(3.7, Math.Round(regionB.SumPMinus, 1));
        }

        [Test]
        public void AggregateData_CorrectDataAggregation_RegionElectricityConsumptionEntity()
        {
            // Arrange
            var regionElectricityData = new List<RegionElectricityConsumptionEntity>
            {
                new RegionElectricityConsumptionEntity { ChainRegion = "Region A", SumPPlus = 20.1, SumPMinus = 15.9 },
                new RegionElectricityConsumptionEntity { ChainRegion = "Region B", SumPPlus = 12.7, SumPMinus = 8.4 },
                new RegionElectricityConsumptionEntity { ChainRegion = "Region A", SumPPlus = 9.3, SumPMinus = 5.1 }
            };

            // Act
            var result = _dataAggregationService.AggregateData(regionElectricityData);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var regionA = result.Find(r => r.ChainRegion == "Region A");
            Assert.IsNotNull(regionA);
            Assert.AreEqual(29.4, Math.Round(regionA.SumPPlus, 1));
            Assert.AreEqual(21.0, Math.Round(regionA.SumPMinus, 1));

            var regionB = result.Find(r => r.ChainRegion == "Region B");
            Assert.IsNotNull(regionB);
            Assert.AreEqual(12.7, Math.Round(regionB.SumPPlus, 1));
            Assert.AreEqual(8.4, Math.Round(regionB.SumPMinus, 1));
        }

        [Test]
        public void AggregateData_WithUnsupportedType_ReturnsNull()
        {
            // Arrange
            var unsupportedData = new List<string> { "data1", "data2", "data3" };

            // Act
            var result = _dataAggregationService.AggregateData(unsupportedData);

            // Assert
            Assert.IsNull(result);
        }
    }
}
