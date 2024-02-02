
using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api.Repositories
{
    public class RepositorioAlertStatus : IRepositorioAlertStatus
    {
        private readonly ApplicationDBContext context;
        private readonly HttpContext httpContext;

        public RepositorioAlertStatus(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<bool> Exist(int id)
        {
            return await context.AlertStatus.AnyAsync(a => a.Id == id);
        }
    }
}
