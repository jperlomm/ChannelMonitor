using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
