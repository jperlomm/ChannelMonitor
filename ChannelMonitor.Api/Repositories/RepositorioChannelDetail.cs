
using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api.Repositories
{
    public class RepositorioChannelDetail : IRepositorioChannelDetail
    {
        private readonly ApplicationDBContext context;
        private readonly HttpContext httpContext;

        public RepositorioChannelDetail(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<bool> Exist(int id)
        {
            return await context.ChannelDetails.AnyAsync(a => a.Id == id);
        }
    }
}
