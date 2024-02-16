using ChannelMonitor.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api.Repositories
{
    public class RepositorioFailureLogging : IRepositorioFailureLogging
    {
        private readonly ApplicationDBContext context;
        private readonly HttpContext httpContext;

        public RepositorioFailureLogging(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<int> Create(FailureLogging failureLogging)
        {
            context.Add(failureLogging);
            await context.SaveChangesAsync();
            return failureLogging.Id;
        }

        public async Task<List<FailureLogging>> GetAll(int channelId)
        {
            return await context.FailureLoggings.Where(c => c.ChannelId == channelId).ToListAsync();
        }

    }
}
