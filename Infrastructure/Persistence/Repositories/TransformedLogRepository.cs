using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Infrastructure.Persistence.Repositories
{
    public class TransformedLogRepository : Repository<TransformedLog>, ITransformedLogRepository
    {
        public TransformedLogRepository(LogDbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<TransformedLog>> GetAllTransformedLogsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<TransformedLog> GetTransformedLogByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<int> SaveTransformedLogAsync(TransformedLog transformedLog)
        {
            return await AddAsync(transformedLog);
        }
    }
}
