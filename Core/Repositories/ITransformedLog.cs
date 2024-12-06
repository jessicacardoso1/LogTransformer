using LogTransformer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Core.Repositories
{
    public interface ITransformedLogRepository : IRepository<TransformedLog>
    {
        Task<IEnumerable<TransformedLog>> GetAllTransformedLogsAsync();

        Task<TransformedLog> GetTransformedLogByIdAsync(int id);

        Task<int> SaveTransformedLogAsync(TransformedLog transformedLog);
        string Transform(string logContent);
        Task<TransformedLog> GetTransformedLogByLogIdAsync(int id);
    }
}
