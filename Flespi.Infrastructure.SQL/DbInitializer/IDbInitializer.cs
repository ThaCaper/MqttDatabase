namespace Flespi.Infrastructure.SQL.DbInitializer
{
    public interface IDbInitializer
    {
        void Initialize(DatabaseContext context);
    }
}