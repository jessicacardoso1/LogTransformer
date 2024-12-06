using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTransformer.Infrastructure.Persistence.Repositories
{
    public class TransformedLogRepository : Repository<TransformedLog>, ITransformedLogRepository
    {
        private readonly LogDbContext _context;
        public TransformedLogRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransformedLog>> GetAllTransformedLogsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<TransformedLog> GetTransformedLogByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public Task<TransformedLog> GetTransformedLogByLogIdAsync(int id)
        {
           return _context.TransformedLogs
                .Include(p => p.OriginalLog)
                .SingleOrDefaultAsync(p => p.OriginalLogId == id);
        }

        public async Task<int> SaveTransformedLogAsync(TransformedLog transformedLog)
        {
            return await AddAsync(transformedLog);
        }
        public string Transform(string logContent)
        {
            var transformedContent = new StringBuilder();

            var lines = logContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 5)
                {
                    var provider = "\"MINHA CDN\""; 
                    var httpMethod = parts[3].Split(' ')[0].Replace("\"", "");
                    var uriPath = parts[3].Split(' ')[1];

                    transformedContent.AppendLine($"{provider} {httpMethod} {parts[1]} {uriPath} {parts[4]} {parts[0]} {parts[2]}");
                }
            }

            return transformedContent.ToString().TrimEnd();
        }

    }
}
