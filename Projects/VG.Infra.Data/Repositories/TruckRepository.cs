using VG.Infra.Data.Context;
using VG.Infra.Data.Entities;

namespace VG.Infra.Data.Repositories
{
    public class TruckRepository : RepositoryGeneric<TruckEntity>, ITruckRepository
    {
        public TruckRepository(DataBaseContext dataBaseContext)
            : base(dataBaseContext)
        { }
    }
}
