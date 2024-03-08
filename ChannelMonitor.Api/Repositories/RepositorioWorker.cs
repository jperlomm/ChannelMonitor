using ChannelMonitor.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api.Repositories
{
    public class RepositorioWorker : IRepositorioWorker
    {
        private readonly ApplicationDBContext context;

        public RepositorioWorker(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<Worker?> GetAll()
        {
            return await context.Workers.FirstOrDefaultAsync();
        }
    }
}
