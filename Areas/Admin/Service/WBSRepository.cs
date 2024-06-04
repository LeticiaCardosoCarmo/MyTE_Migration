using Microsoft.EntityFrameworkCore;
using MyTE_Migration.Context;

namespace MyTE_Migration.Areas.Admin.Service
{
    public class WBSRepository : IWBSRepository
    {
        private readonly AppDbContext _context;

        public WBSRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllWbsCodesAsync()
        {
            return await _context.WBS.Select(w => w.WBS_Codigo).ToListAsync();
        }
    }
}
