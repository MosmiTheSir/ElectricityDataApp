using ElectricityDataApp.Models;
using ElectricityDataApp.Services;

namespace ElectricityDataApp.Tests
{
    [TestFixture]
    public class CsvDataParserTests
    {
        private CsvDataParser _csvDataParser;

        [SetUp]
        public void Setup()
        {
            _csvDataParser = new CsvDataParser();
        }

        [Test]
        public void ParseData_ValidCsvData_ReturnsParsedRecords()
        {
            // Arrange
            string? csvData = "TINKLAS,OBT_PAVADINIMAS,P+,P-,OBJ_GV_TIPAS,OBJ_NUMERIS,PL_T\n" +
                              "Region1,Butas,10.5,5.5,G,123,2021-01-01 10:00:00\n" +
                              "Region2,Butas,7.8,3.2,N,456,2021-01-01 11:00:00\n";

            // Act
            List<ElectricityData> result = _csvDataParser.ParseData(csvData);

            // Assert
            Assert.AreEqual(2, result.Count);

            // Verify the values of the first record
            ElectricityData firstRecord = result[0];
            Assert.AreEqual("Region1", firstRecord.ChainRegion);
            Assert.AreEqual("Butas", firstRecord.Name);
            Assert.AreEqual(10.5, firstRecord.PPlus);
            Assert.AreEqual(5.5, firstRecord.PMinus);
            Assert.AreEqual(ObjGVType.G, firstRecord.GvType);
            Assert.AreEqual("123", firstRecord.Number);
            Assert.AreEqual(new DateTime(2021, 1, 1, 10, 0, 0), firstRecord.Time);

            // Verify the values of the second record
            ElectricityData secondRecord = result[1];
            Assert.AreEqual("Region2", secondRecord.ChainRegion);
            Assert.AreEqual("Butas", secondRecord.Name);
            Assert.AreEqual(7.8, secondRecord.PPlus);
            Assert.AreEqual(3.2, secondRecord.PMinus);
            Assert.AreEqual(ObjGVType.N, secondRecord.GvType);
            Assert.AreEqual("456", secondRecord.Number);
            Assert.AreEqual(new DateTime(2021, 1, 1, 11, 0, 0), secondRecord.Time);
        }

        [Test]
        public void ParseData_InvalidCsvData_ReturnsEmptyList()
        {
            // Arrange
            string? csvData = "Invalid,CSV,Data";

            // Act
            List<ElectricityData> result = _csvDataParser.ParseData(csvData);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
