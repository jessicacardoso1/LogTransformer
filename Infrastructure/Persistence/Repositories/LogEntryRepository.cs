using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Infrastructure.Persistence.Repositories
{
    public class LogEntryRepository : Repository<LogEntry>, ILogEntryRepository
    {
        public LogEntryRepository(LogDbContext context) : base(context)
        {
        }

        // Buscar todos os logs salvos
        public async Task<IEnumerable<LogEntry>> GetAllLogsAsync()
        {
            return await GetAllAsync();
        }

        // Buscar log de entrada por ID
        public async Task<LogEntry> GetLogByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        // Salvar um novo Log de Entrada
        public async Task<int> SaveLogAsync(LogEntry logEntry)
        {
            return await AddAsync(logEntry);
        }
    }
}
