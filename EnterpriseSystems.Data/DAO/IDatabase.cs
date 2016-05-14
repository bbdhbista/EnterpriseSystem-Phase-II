using System.Data;

namespace EnterpriseSystems.Data.DAO
{
    public interface IDatabase
    {
        IQuery CreateQuery(string queryStatement);
        DataTable RunSelect(IQuery query);
    }
}
