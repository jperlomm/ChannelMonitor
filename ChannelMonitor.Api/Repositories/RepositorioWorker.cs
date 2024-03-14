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

        public async Task Update(Worker worker)
        {
            context.Update(worker);
            await context.SaveChangesAsync();
        }

        public async Task<Worker?> GetById(int id)
        {
            return await context.Workers.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}
