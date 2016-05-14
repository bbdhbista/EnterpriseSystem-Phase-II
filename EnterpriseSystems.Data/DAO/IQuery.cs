namespace EnterpriseSystems.Data.DAO
{
    public interface IQuery
    {
        void AddParameter(object parameterValue, string parameterName);
    }
}
