using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Infrastructure.Persistence.Repositories
{
    public class LogEntryRepository : Repository<LogEntry>, ILogEntryRepository
    {
        private readonly LogDbContext _context;

        public LogEntryRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogEntry>> GetAllLogsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<LogEntry> GetLogByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<int> SaveLogAsync(LogEntry logEntry)
        {
            return await AddAsync(logEntry);
        }
        public async Task SaveTransformedLogAsync(TransformedLog transformedLog)
        {
            await _context.TransformedLogs.AddAsync(transformedLog);
            await _context.SaveChangesAsync();
        }
    }
}
