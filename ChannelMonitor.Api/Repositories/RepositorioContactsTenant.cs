using ChannelMonitor.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChannelMonitor.Api.Repositories
{
    public class RepositorioContactsTenant : IRepositorioContactsTenant
    {
        private readonly ApplicationDBContext _context;

        public RepositorioContactsTenant(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<ContactsTenant>> GetAll()
        {
            return await _context.ContactsTenants.ToListAsync();
        }
    }
}
