namespace VG.Infra.Data.Entities
{
    public class TruckEntity
    {
        public int Id { get; set; }

        public short ManufactureYear { get; set; }
        public short ModelYear { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }

        public int ModelId { get; set; }
        public virtual ModelEntity Model { get; set; }
    }
}
