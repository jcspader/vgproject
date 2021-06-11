using System.Collections.Generic;

namespace VG.Infra.Data.Entities
{
    public class ModelEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TruckEntity> Trucks { get; set; }
    }
}
