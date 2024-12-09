using LogTransformer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Core.Repositories
{
    public interface ILogEntryRepository : IRepository<LogEntry>
    {
        Task<IEnumerable<LogEntry>> GetAllLogsAsync(int page, int size);

        Task<LogEntry> GetLogByIdAsync(int id);

        Task<int> SaveLogAsync(LogEntry logEntry);
    }
}
