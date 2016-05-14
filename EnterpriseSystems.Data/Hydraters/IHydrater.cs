using System.Collections.Generic;
using System.Data;

namespace EnterpriseSystems.Data.Hydraters
{
    public interface IHydrater<TEntity>
    {
        IEnumerable<TEntity> Hydrate(DataTable dataTable);
    }
}
