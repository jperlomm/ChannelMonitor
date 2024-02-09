using ChannelMonitor.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api.Repositories
{
    public class RepositorioChannel : IRepositorioChannel
    {
        private readonly ApplicationDBContext context;
        private readonly HttpContext httpContext;

        public RepositorioChannel(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<int> Create(Channel channel)
        {
            context.Add(channel);
            await context.SaveChangesAsync();
            return channel.Id;
        }

        public async Task<List<Channel>> GetAll()
        {
            return await context.Channels.OrderBy(x => x.Number).ToListAsync();
        }

        public async Task Update(Channel channel)
        {
            context.Update(channel);
            await context.SaveChangesAsync();
        }

        public async Task<Channel?> GetById(int id)
        {
            return await context.Channels.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Delete(int id)
        {
            await context.Channels.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> Exist(int id)
        {
            return await context.Channels.AnyAsync(a => a.Id == id);
        }

    }
}
