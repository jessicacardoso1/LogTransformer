using LogTransformer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Core.Repositories
{
    public interface ITransformedLogRepository : IRepository<TransformedLog>
    {
        Task<IEnumerable<TransformedLog>> GetAllTransformedLogsAsync(int page, int size);
        Task<TransformedLog> GetTransformedLogByIdAsync(int id);

        Task<int> SaveTransformedLogAsync(TransformedLog transformedLog);
        string Transform(string logContent);
        Task<List<TransformedLog>> GetTransformedLogsByLogIdAsync(int id);
    }
}
