using System.Threading.Tasks;

namespace Lanpuda.Server.Data;

public interface IServerDbSchemaMigrator
{
    Task MigrateAsync();
}
