﻿using LogTransformer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Core.Repositories
{
    public interface ILogEntryRepository : IRepository<LogEntry>
    {
        Task<IEnumerable<LogEntry>> GetAllLogsAsync();

        Task<LogEntry> GetLogByIdAsync(int id);

        Task<int> SaveLogAsync(LogEntry logEntry);
        Task SaveTransformedLogAsync(TransformedLog transformedLog);
    }
}
