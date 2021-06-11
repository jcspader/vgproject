using System;
using System.Collections.Generic;
using System.Text;
using VG.Infra.Data.Context;
using VG.Infra.Data.Entities;

namespace VG.Infra.Data.Repositories
{
    public class ModelRepository : RepositoryGeneric<ModelEntity>, IModelRepository
    {
        public ModelRepository(DataBaseContext dataBaseContext)
            : base(dataBaseContext)
        { }
    }
}
