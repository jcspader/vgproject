namespace VG.Domain.Dto
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TruckDto
    {
        public int Id { get; set; }

        public short ManufactureYear { get; set; }
        public short ModelYear { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public int ModelId { get; set; }

        public string ModelName { get; set; }
    }
}
