using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Repositories
{
    public class RepositorioErrors : IRepositorioErrors
    {
        private readonly ApplicationDBContext context;

        public RepositorioErrors(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task Create(Error error)
        {
            context.Add(error);
            await context.SaveChangesAsync();
        }

    }
}
