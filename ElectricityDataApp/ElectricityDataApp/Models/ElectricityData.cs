namespace ElectricityDataApp.Models
{
    public class ElectricityData
    {
        public string? ChainRegion { get; set; }
        public string? Name { get; set; }
        public ObjGVType GvType { get; set; }
        public string? Number { get; set; }
        public double PPlus { get; set; }
        public DateTime Time { get; set; }
        public double PMinus { get; set; }
    }

    public enum ObjGVType
    {
        G,
        N,
        NGv,
    }
}