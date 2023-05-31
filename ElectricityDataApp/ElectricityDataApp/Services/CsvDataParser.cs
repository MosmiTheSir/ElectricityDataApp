using CsvHelper;
using ElectricityDataApp.Models;
using ElectricityDataApp.Services.Interfaces;
using System.Globalization;

namespace ElectricityDataApp.Services;

public class CsvDataParser : ICsvDataParser
{

    public List<ElectricityData> ParseData(string? csvData)
    {
        using var reader = new StringReader(csvData);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = new List<ElectricityData>();
        csv.Read();
        csv.ReadHeader();
        while (csv.Read())
        {
            if (Enum.TryParse(csv.GetField("OBJ_GV_TIPAS"), true, out ObjGVType vartotojoTipas))
            {
                var record = new ElectricityData
                {
                    ChainRegion = csv.GetField<string>("TINKLAS"),
                    Name = csv.GetField<string>("OBT_PAVADINIMAS"),
                    PPlus = double.TryParse(csv.GetField<string>("P+"), out var doubleValuePlus) ? doubleValuePlus : default,
                    PMinus = double.TryParse(csv.GetField<string>("P-"), out var doubleValueMinus) ? doubleValueMinus : default,
                    GvType = vartotojoTipas,
                    Number = csv.GetField<string>("OBJ_NUMERIS"),
                    Time = DateTime.TryParse(csv.GetField<string>("PL_T"), out var DateTimeValue) ? DateTimeValue : default
                };

                if (string.IsNullOrEmpty(record.ChainRegion) || string.IsNullOrEmpty(record.Name) ||
                    record.PPlus == default || record.PMinus == default || string.IsNullOrEmpty(record.Number) ||
                    record.Time == default) continue;

                if (record.Name == "Butas") // filter only apartment data
                {
                    records.Add(record);
                }
            }
        }
        return records;
    }
}